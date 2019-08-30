using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WSX.ControlLibrary.Common
{
    /// <summary>
    /// 扩展文件打开对话框。不可继承该类。
    /// 支持自定义的文件预览功能。
    /// </summary>
    [DefaultEvent("OnFileSelectChanged")]
    public sealed class OpenFileDialogEx : Component
    {
        #region 字段区域

        private string m_fileName = string.Empty;
        private string m_filter = string.Empty;
        private Control m_previewControl;

        #endregion

        #region 属性区域

        /// <summary>
        /// 获取或设置当前选择的文件名。
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FileName
        {
            get { return m_fileName; }
            set { m_fileName = value ?? string.Empty; }
        }

        /// <summary>
        /// 获取或设置文件筛选条件。
        /// </summary>
        [Description("文件筛选条件。")]
        public string Filter
        {
            get { return m_filter; }
            set { m_filter = value ?? string.Empty; }
        }

        /// <summary>
        /// 获取或设置文件预览控件。
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control PreviewControl
        {
            get { return m_previewControl; }
            set { m_previewControl = value; }
        }

        #endregion

        #region 方法区域

        /// <summary>
        /// 显示模式对话框。
        /// </summary>
        /// <returns></returns>
        public DialogResult ShowDialog()
        {
            return ShowDialog(null);
        }

        /// <summary>
        /// 显示模式对话框。
        /// </summary>
        /// <param name="owner">宿主控件。</param>
        /// <returns></returns>
        public DialogResult ShowDialog(IWin32Window owner)
        {
            using (OpenFileDialog dialog = new OpenFileDialog() { FileName = m_fileName, Filter = m_filter })
            {
                //在Vista、WIN7、WIN8上按XP风格显示对话框
                dialog.AutoUpgradeEnabled = false;

                OpenFileDialogHostForm hostForm = new OpenFileDialogHostForm(this, dialog);
                if (owner != null)
                    hostForm.Show(owner);
                else hostForm.Show(Application.OpenForms[0]);

                //隐藏中间窗体
                Win32.SetWindowPos(hostForm.Handle, IntPtr.Zero, 0, 0, 0, 0,
                    SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOOWNERZORDER | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_HIDEWINDOW);

                //将median作为openfileDialog的owner
                DialogResult result = dialog.ShowDialog(hostForm);
                if (result == DialogResult.OK)
                {
                    m_fileName = dialog.FileName;
                }

                hostForm.Close();
                hostForm.Dispose();

                return result;
            }
        }

        #endregion

        #region 事件委托

        /// <summary>
        /// 选择文件时引发该事件。
        /// </summary>
        public event EventHandler<OpenFileDialogExPathEventArgs> OnFileSelectChanged;

        /// <summary>
        /// 打开路径时引发该事件。
        /// </summary>
        public event EventHandler<OpenFileDialogExPathEventArgs> OnPathOpened;

        /// <summary>
        /// 选择文件时调用该方法。
        /// </summary>
        /// <param name="fileName"></param>
        private void FileSelectChanged(string fileName)
        {
            if (OnFileSelectChanged != null && !string.IsNullOrEmpty(fileName)
                && !string.IsNullOrEmpty(System.IO.Path.GetExtension(fileName)))
            {
                OnFileSelectChanged(this, new OpenFileDialogExPathEventArgs(fileName));
            }
        }

        /// <summary>
        /// 打开路径时调用该方法。
        /// </summary>
        /// <param name="path"></param>
        private void PathOpened(string path)
        {
            if (OnPathOpened != null && !string.IsNullOrEmpty(path))
            {
                OnPathOpened(this, new OpenFileDialogExPathEventArgs(path));
            }
        }

        #endregion

        #region 内部类型

        /// <summary>
        /// OpenFileDialog宿主窗体。
        /// </summary>
        class OpenFileDialogHostForm : Form
        {
            #region 构造区域

            /// <summary>
            /// 构造函数。
            /// </summary>
            /// <param name="dialogEx"></param>
            /// <param name="dialog"></param>
            public OpenFileDialogHostForm(OpenFileDialogEx dialogEx, OpenFileDialog dialog)
            {
                m_dialogEx = dialogEx;
                m_dialog = dialog;

                this.StartPosition = FormStartPosition.Manual;
                this.Location = new System.Drawing.Point(-1000, -1000); //隐藏窗口，避免界面闪烁
            }

            #endregion

            #region 字段区域

            private OpenFileDialogEx m_dialogEx;
            private OpenFileDialog m_dialog = null;
            private DialogNativeWindow m_nativeWindow;

            #endregion

            #region 方法区域

            /// <summary>
            /// 窗口关闭前。
            /// </summary>
            /// <param name="e"></param>
            protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
            {
                if (m_nativeWindow != null)
                    m_nativeWindow.Dispose();
                base.OnClosing(e);
            }

            /// <summary>
            /// 处理窗口消息。
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                //m.LParam为要打开的窗口句柄，开始监听OpenFileDialog的Windows消息
                if (m.Msg == (int)Msg.WM_ACTIVATE)
                {
                    //跳过不需要监听的窗口
                    bool needInitNative = true;
                    if (Application.OpenForms != null && Application.OpenForms.Count > 0)
                    {
                        foreach (Form frm in Application.OpenForms)
                        {
                            if (m.LParam == frm.Handle && frm.Handle != this.Handle)
                                needInitNative = false;
                        }
                    }
                    if (m_nativeWindow == null && needInitNative)
                        m_nativeWindow = new DialogNativeWindow(m_dialogEx, m.LParam, m_dialog);
                }
                base.WndProc(ref m);
            }

            #endregion
        }

        /// <summary>
        /// OpenFileDialog钩子窗口。
        /// </summary>
        private class DialogNativeWindow : NativeWindow, IDisposable
        {
            #region 构造区域

            /// <summary>
            /// 构造函数。
            /// </summary>
            /// <param name="dialogEx"></param>
            /// <param name="handle">要监视的窗口句柄。</param>
            /// <param name="dialog">打开文件的对话框。</param>
            public DialogNativeWindow(OpenFileDialogEx dialogEx, IntPtr handle, OpenFileDialog dialog)
            {
                m_dialogEx = dialogEx;
                m_dialog = dialog;
                AssignHandle(handle);
            }

            #endregion

            #region 字段区域

            private OpenFileDialogEx m_dialogEx;
            private OpenFileDialog m_dialog; //待扩展OpenFileDialog
            private ChildControlNativeWindow m_childNative;
            private bool m_isInited;//自定义控件是否已初始化
            private bool m_isDisposed;

            #endregion

            #region 属性区域

            /// <summary>
            /// 获取一个值，该值指示当前资源是否已被释放。
            /// </summary>
            public bool IsDisposed
            {
                get { return m_isDisposed; }
            }

            #endregion

            #region 方法区域

            /// <summary>
            /// 处理窗口消息。
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case (int)Msg.WM_SHOWWINDOW:
                        InitChildNative();
                        InitCustomControl();
                        break;
                    case (int)Msg.WM_SIZING:
                        UpdateSize();
                        break;
                    case (int)Msg.WM_WINDOWPOSCHANGING:
                        UpdateLocation(m);
                        break;
                }
                base.WndProc(ref m);
            }

            /// <summary>
            /// 初始化子控件的NativeWindow。
            /// </summary>
            private void InitChildNative()
            {
                //查找openfileDialog中的子控件
                Win32.EnumChildWindows(this.Handle, new Win32.EnumWindowsCallBack((IntPtr handle, int lparam) =>
                {
                    StringBuilder sb = new StringBuilder(256);
                    Win32.GetClassName(handle, sb, sb.Capacity);//获取控件类名

                    if (sb.ToString().StartsWith("#32770")) //找到目标控件
                    {
                        m_childNative = new ChildControlNativeWindow(handle);
                        m_childNative.SelectFileChanged += new ChildControlNativeWindow.SelectFileChangedEventHandler(childNative_SelectFileChanged);
                        m_childNative.SelectPathChanged += new ChildControlNativeWindow.SelectPathChangedEventHandler(childNative_SelectPathChanged);
                        return true;
                    }
                    return true;
                }),
                    0);
            }

            /// <summary>
            /// 初始化自定义控件。
            /// </summary>
            private void InitCustomControl()
            {
                if (m_dialogEx.PreviewControl != null && !m_dialogEx.PreviewControl.IsDisposed)
                {
                    //添加控件到OpenFileDialog界面
                    Win32.SetParent(m_dialogEx.PreviewControl.Handle, this.Handle);

                    //调整对话框的宽度
                    WINDOWINFO info = new WINDOWINFO();
                    Win32.GetWindowInfo(this.Handle, out info);
                    Win32.SetWindowPos(this.Handle, IntPtr.Zero, (int)info.rcWindow.left, (int)info.rcWindow.top, 500, (int)info.rcWindow.Height, SetWindowPosFlags.SWP_SHOWWINDOW);

                    //计算自定义控件的位置和尺寸
                    RECT rc = new RECT();
                    Win32.GetClientRect(this.Handle, ref rc);
                    m_dialogEx.PreviewControl.Height = (int)rc.Height;
                    m_dialogEx.PreviewControl.Location = new Point((int)(rc.Width - m_dialogEx.PreviewControl.Width), 0);
                }
                m_isInited = true;
            }

            /// <summary>
            /// 更新自定义控件的位置。
            /// </summary>
            /// <param name="m"></param>
            private void UpdateLocation(Message m)
            {
                if (m_dialogEx.PreviewControl != null && !m_dialogEx.PreviewControl.IsDisposed)
                {
                    if (!m_isInited && !this.IsDisposed)
                    {
                        WINDOWPOS pos = (WINDOWPOS)Marshal.PtrToStructure(m.LParam, typeof(WINDOWPOS));
                        if (pos.flags != 0 && ((pos.flags & (int)SWP_Flags.SWP_NOSIZE) != (int)SWP_Flags.SWP_NOSIZE))
                        {
                            pos.cx += m_dialogEx.PreviewControl.Width; //修改OpenfileDialog的宽度
                            Marshal.StructureToPtr(pos, m.LParam, true);

                            RECT rc = new RECT();
                            Win32.GetClientRect(this.Handle, ref rc);
                            m_dialogEx.PreviewControl.Height = (int)rc.Height;
                        }
                    }
                }
            }

            /// <summary>
            /// 更新自定义控件的尺寸。
            /// </summary>
            private void UpdateSize()
            {
                if (m_dialogEx.PreviewControl != null && !m_dialogEx.PreviewControl.IsDisposed)
                {
                    if (!this.IsDisposed)
                    {
                        //新添加的控件与openfileDialog大小一致
                        RECT rc = new RECT();
                        Win32.GetClientRect(this.Handle, ref rc);
                        Win32.SetWindowPos(m_dialogEx.PreviewControl.Handle, (IntPtr)ZOrderPos.HWND_BOTTOM, 0, 0, (int)m_dialogEx.PreviewControl.Width, (int)rc.Height,
                            SetWindowPosFlags.SWP_NOACTIVATE |
                            SetWindowPosFlags.SWP_NOOWNERZORDER |
                            SetWindowPosFlags.SWP_NOMOVE |
                            SetWindowPosFlags.SWP_ASYNCWINDOWPOS |
                            SetWindowPosFlags.SWP_DEFERERASE);
                    }
                }
            }

            /// <summary>
            /// 释放资源。
            /// </summary>
            public void Dispose()
            {
                ReleaseHandle();

                if (m_childNative != null)
                {
                    m_childNative.SelectFileChanged -= new ChildControlNativeWindow.SelectFileChangedEventHandler(childNative_SelectFileChanged);
                    m_childNative.SelectPathChanged -= new ChildControlNativeWindow.SelectPathChangedEventHandler(childNative_SelectPathChanged);
                    m_childNative.Dispose();
                }

                m_isDisposed = true;
            }

            /// <summary>
            /// 选择目录发生变化。
            /// </summary>
            /// <param name="path"></param>
            void childNative_SelectPathChanged(string path)
            {
                m_dialogEx.PathOpened(path);
            }

            /// <summary>
            /// 选择文件发生变化。
            /// </summary>
            /// <param name="fileName"></param>
            void childNative_SelectFileChanged(string fileName)
            {
                m_dialogEx.FileSelectChanged(fileName);
            }

            #endregion
        }

        /// <summary>
        /// 子控件钩子窗口。
        /// </summary>
        private class ChildControlNativeWindow : NativeWindow, IDisposable
        {
            #region 构造区域

            /// <summary>
            /// 构造函数。
            /// </summary>
            /// <param name="handle"></param>
            public ChildControlNativeWindow(IntPtr handle)
            {
                AssignHandle(handle);
            }

            #endregion

            #region 方法区域

            /// <summary>
            /// 处理窗口消息。
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case (int)Msg.WM_NOTIFY:
                        OFNOTIFY ofNotify = (OFNOTIFY)Marshal.PtrToStructure(m.LParam, typeof(OFNOTIFY));
                        if (ofNotify.hdr.code == (uint)DialogChangeStatus.CDN_SELCHANGE) //openfileDialog选择文件发生变化
                        {
                            StringBuilder sb = new StringBuilder(256);
                            Win32.SendMessage(Win32.GetParent(this.Handle), (int)DialogChangeProperties.CDM_GETFILEPATH, (int)256, sb);
                            if (SelectFileChanged != null)
                                SelectFileChanged(sb.ToString()); //通知注册者
                        }
                        else if (ofNotify.hdr.code == (uint)DialogChangeStatus.CDN_FOLDERCHANGE) //openfileDialog选择目录发生变化
                        {
                            StringBuilder sb = new StringBuilder(256);
                            Win32.SendMessage(Win32.GetParent(this.Handle), (int)DialogChangeProperties.CDM_GETFOLDERPATH, (int)256, sb);
                            if (SelectPathChanged != null)
                                SelectPathChanged(sb.ToString()); //通知注册者
                        }
                        break;
                }
                base.WndProc(ref m);
            }

            /// <summary>
            /// 释放资源。
            /// </summary>
            public void Dispose()
            {
                ReleaseHandle();
            }

            #endregion

            #region 事件委托

            //当openfileDialog的选择文件发生变化时发生
            public delegate void SelectFileChangedEventHandler(string fileName);
            public event SelectFileChangedEventHandler SelectFileChanged;

            //当openfileDialog的选择目录发生变化时发生
            public delegate void SelectPathChangedEventHandler(string path);
            public event SelectPathChangedEventHandler SelectPathChanged;

            #endregion
        }

        #endregion
    }

    /// <summary>
    /// 路径事件参数。
    /// </summary>
    [Serializable]
    public class OpenFileDialogExPathEventArgs : EventArgs
    {
        #region 构造区域

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="path">与事件相关的路径名（文件名或文件夹名）。</param>
        public OpenFileDialogExPathEventArgs(string path)
        {
            m_path = path;
        }

        #endregion

        #region 字段区域

        private string m_path = string.Empty;

        #endregion

        #region 属性区域

        /// <summary>
        /// 获取与事件相关的路径名（文件名或文件夹名）。
        /// </summary>
        public string Path
        {
            get { return m_path; }
        }

        #endregion
    }

    #region Win32
    public static class Win32
    {
        #region Delegates
        public delegate bool EnumWindowsCallBack(IntPtr hWnd, int lParam);
        #endregion

        #region USER32
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowInfo(IntPtr hwnd, out WINDOWINFO pwi);
        [DllImport("User32.Dll")]
        public static extern void GetClassName(IntPtr hWnd, StringBuilder param, int length);
        [DllImport("user32.Dll")]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsCallBack lpEnumFunc, int lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, StringBuilder param);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int Width, int Height, SetWindowPosFlags flags);
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, ref RECT rect);
        #endregion
    }

    #region SWP_Flags
    [Flags]
    public enum SWP_Flags
    {
        SWP_NOSIZE = 0x0001,
        SWP_NOMOVE = 0x0002,
        SWP_NOZORDER = 0x0004,
        SWP_NOACTIVATE = 0x0010,
        SWP_FRAMECHANGED = 0x0020, /* The frame changed: send WM_NCCALCSIZE */
        SWP_SHOWWINDOW = 0x0040,
        SWP_HIDEWINDOW = 0x0080,
        SWP_NOOWNERZORDER = 0x0200, /* Don't do owner Z ordering */

        SWP_DRAWFRAME = SWP_FRAMECHANGED,
        SWP_NOREPOSITION = SWP_NOOWNERZORDER
    }
    #endregion

    #region DialogChangeStatus
    public enum DialogChangeStatus : long
    {
        CDN_FIRST = 0xFFFFFDA7,
        CDN_INITDONE = (CDN_FIRST - 0x0000),
        CDN_SELCHANGE = (CDN_FIRST - 0x0001),
        CDN_FOLDERCHANGE = (CDN_FIRST - 0x0002),
        CDN_SHAREVIOLATION = (CDN_FIRST - 0x0003),
        CDN_HELP = (CDN_FIRST - 0x0004),
        CDN_FILEOK = (CDN_FIRST - 0x0005),
        CDN_TYPECHANGE = (CDN_FIRST - 0x0006),
    }
    #endregion

    #region DialogChangeProperties
    public enum DialogChangeProperties
    {
        CDM_FIRST = (0x400 + 100),
        CDM_GETSPEC = (CDM_FIRST + 0x0000),
        CDM_GETFILEPATH = (CDM_FIRST + 0x0001),
        CDM_GETFOLDERPATH = (CDM_FIRST + 0x0002),
        CDM_GETFOLDERIDLIST = (CDM_FIRST + 0x0003),
        CDM_SETCONTROLTEXT = (CDM_FIRST + 0x0004),
        CDM_HIDECONTROL = (CDM_FIRST + 0x0005),
        CDM_SETDEFEXT = (CDM_FIRST + 0x0006)
    }
    #endregion

    #region ZOrderPos
    public enum ZOrderPos
    {
        HWND_TOP = 0,
        HWND_BOTTOM = 1,
        HWND_TOPMOST = -1,
        HWND_NOTOPMOST = -2
    }
    #endregion

    #region ChildFromPointFlags
    public enum ChildFromPointFlags
    {
        CWP_ALL = 0x0000,
        CWP_SKIPINVISIBLE = 0x0001,
        CWP_SKIPDISABLED = 0x0002,
        CWP_SKIPTRANSPARENT = 0x0004
    }
    #endregion

    #region Windows Messages
    public enum Msg
    {
        WM_NULL = 0x0000,
        WM_CREATE = 0x0001,
        WM_DESTROY = 0x0002,
        WM_MOVE = 0x0003,
        WM_SIZE = 0x0005,
        WM_ACTIVATE = 0x0006,
        WM_SETFOCUS = 0x0007,
        WM_KILLFOCUS = 0x0008,
        WM_ENABLE = 0x000A,
        WM_SETREDRAW = 0x000B,
        WM_SETTEXT = 0x000C,
        WM_GETTEXT = 0x000D,
        WM_GETTEXTLENGTH = 0x000E,
        WM_PAINT = 0x000F,
        WM_CLOSE = 0x0010,
        WM_QUERYENDSESSION = 0x0011,
        WM_QUIT = 0x0012,
        WM_QUERYOPEN = 0x0013,
        WM_ERASEBKGND = 0x0014,
        WM_SYSCOLORCHANGE = 0x0015,
        WM_ENDSESSION = 0x0016,
        WM_SHOWWINDOW = 0x0018,
        WM_CTLCOLOR = 0x0019,
        WM_WININICHANGE = 0x001A,
        WM_SETTINGCHANGE = 0x001A,
        WM_DEVMODECHANGE = 0x001B,
        WM_ACTIVATEAPP = 0x001C,
        WM_FONTCHANGE = 0x001D,
        WM_TIMECHANGE = 0x001E,
        WM_CANCELMODE = 0x001F,
        WM_SETCURSOR = 0x0020,
        WM_MOUSEACTIVATE = 0x0021,
        WM_CHILDACTIVATE = 0x0022,
        WM_QUEUESYNC = 0x0023,
        WM_GETMINMAXINFO = 0x0024,
        WM_PAINTICON = 0x0026,
        WM_ICONERASEBKGND = 0x0027,
        WM_NEXTDLGCTL = 0x0028,
        WM_SPOOLERSTATUS = 0x002A,
        WM_DRAWITEM = 0x002B,
        WM_MEASUREITEM = 0x002C,
        WM_DELETEITEM = 0x002D,
        WM_VKEYTOITEM = 0x002E,
        WM_CHARTOITEM = 0x002F,
        WM_SETFONT = 0x0030,
        WM_GETFONT = 0x0031,
        WM_SETHOTKEY = 0x0032,
        WM_GETHOTKEY = 0x0033,
        WM_QUERYDRAGICON = 0x0037,
        WM_COMPAREITEM = 0x0039,
        WM_GETOBJECT = 0x003D,
        WM_COMPACTING = 0x0041,
        WM_COMMNOTIFY = 0x0044,
        WM_WINDOWPOSCHANGING = 0x0046,
        WM_WINDOWPOSCHANGED = 0x0047,
        WM_POWER = 0x0048,
        WM_COPYDATA = 0x004A,
        WM_CANCELJOURNAL = 0x004B,
        WM_NOTIFY = 0x004E,
        WM_INPUTLANGCHANGEREQUEST = 0x0050,
        WM_INPUTLANGCHANGE = 0x0051,
        WM_TCARD = 0x0052,
        WM_HELP = 0x0053,
        WM_USERCHANGED = 0x0054,
        WM_NOTIFYFORMAT = 0x0055,
        WM_CONTEXTMENU = 0x007B,
        WM_STYLECHANGING = 0x007C,
        WM_STYLECHANGED = 0x007D,
        WM_DISPLAYCHANGE = 0x007E,
        WM_GETICON = 0x007F,
        WM_SETICON = 0x0080,
        WM_NCCREATE = 0x0081,
        WM_NCDESTROY = 0x0082,
        WM_NCCALCSIZE = 0x0083,
        WM_NCHITTEST = 0x0084,
        WM_NCPAINT = 0x0085,
        WM_NCACTIVATE = 0x0086,
        WM_GETDLGCODE = 0x0087,
        WM_SYNCPAINT = 0x0088,
        WM_NCMOUSEMOVE = 0x00A0,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_NCLBUTTONUP = 0x00A2,
        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_NCRBUTTONDOWN = 0x00A4,
        WM_NCRBUTTONUP = 0x00A5,
        WM_NCRBUTTONDBLCLK = 0x00A6,
        WM_NCMBUTTONDOWN = 0x00A7,
        WM_NCMBUTTONUP = 0x00A8,
        WM_NCMBUTTONDBLCLK = 0x00A9,
        WM_NCXBUTTONDOWN = 0x00AB,
        WM_NCXBUTTONUP = 0x00AC,
        WM_NCXBUTTONDBLCLK = 0x00AD,
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_CHAR = 0x0102,
        WM_DEADCHAR = 0x0103,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
        WM_SYSCHAR = 0x0106,
        WM_SYSDEADCHAR = 0x0107,
        WM_KEYLAST = 0x0108,
        WM_IME_STARTCOMPOSITION = 0x010D,
        WM_IME_ENDCOMPOSITION = 0x010E,
        WM_IME_COMPOSITION = 0x010F,
        WM_IME_KEYLAST = 0x010F,
        WM_INITDIALOG = 0x0110,
        WM_COMMAND = 0x0111,
        WM_SYSCOMMAND = 0x0112,
        WM_TIMER = 0x0113,
        WM_HSCROLL = 0x0114,
        WM_VSCROLL = 0x0115,
        WM_INITMENU = 0x0116,
        WM_INITMENUPOPUP = 0x0117,
        WM_MENUSELECT = 0x011F,
        WM_MENUCHAR = 0x0120,
        WM_ENTERIDLE = 0x0121,
        WM_MENURBUTTONUP = 0x0122,
        WM_MENUDRAG = 0x0123,
        WM_MENUGETOBJECT = 0x0124,
        WM_UNINITMENUPOPUP = 0x0125,
        WM_MENUCOMMAND = 0x0126,
        WM_CTLCOLORMSGBOX = 0x0132,
        WM_CTLCOLOREDIT = 0x0133,
        WM_CTLCOLORLISTBOX = 0x0134,
        WM_CTLCOLORBTN = 0x0135,
        WM_CTLCOLORDLG = 0x0136,
        WM_CTLCOLORSCROLLBAR = 0x0137,
        WM_CTLCOLORSTATIC = 0x0138,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_MOUSEWHEEL = 0x020A,
        WM_XBUTTONDOWN = 0x020B,
        WM_XBUTTONUP = 0x020C,
        WM_XBUTTONDBLCLK = 0x020D,
        WM_PARENTNOTIFY = 0x0210,
        WM_ENTERMENULOOP = 0x0211,
        WM_EXITMENULOOP = 0x0212,
        WM_NEXTMENU = 0x0213,
        WM_SIZING = 0x0214,
        WM_CAPTURECHANGED = 0x0215,
        WM_MOVING = 0x0216,
        WM_DEVICECHANGE = 0x0219,
        WM_MDICREATE = 0x0220,
        WM_MDIDESTROY = 0x0221,
        WM_MDIACTIVATE = 0x0222,
        WM_MDIRESTORE = 0x0223,
        WM_MDINEXT = 0x0224,
        WM_MDIMAXIMIZE = 0x0225,
        WM_MDITILE = 0x0226,
        WM_MDICASCADE = 0x0227,
        WM_MDIICONARRANGE = 0x0228,
        WM_MDIGETACTIVE = 0x0229,
        WM_MDISETMENU = 0x0230,
        WM_ENTERSIZEMOVE = 0x0231,
        WM_EXITSIZEMOVE = 0x0232,
        WM_DROPFILES = 0x0233,
        WM_MDIREFRESHMENU = 0x0234,
        WM_IME_SETCONTEXT = 0x0281,
        WM_IME_NOTIFY = 0x0282,
        WM_IME_CONTROL = 0x0283,
        WM_IME_COMPOSITIONFULL = 0x0284,
        WM_IME_SELECT = 0x0285,
        WM_IME_CHAR = 0x0286,
        WM_IME_REQUEST = 0x0288,
        WM_IME_KEYDOWN = 0x0290,
        WM_IME_KEYUP = 0x0291,
        WM_MOUSEHOVER = 0x02A1,
        WM_MOUSELEAVE = 0x02A3,
        WM_CUT = 0x0300,
        WM_COPY = 0x0301,
        WM_PASTE = 0x0302,
        WM_CLEAR = 0x0303,
        WM_UNDO = 0x0304,
        WM_RENDERFORMAT = 0x0305,
        WM_RENDERALLFORMATS = 0x0306,
        WM_DESTROYCLIPBOARD = 0x0307,
        WM_DRAWCLIPBOARD = 0x0308,
        WM_PAINTCLIPBOARD = 0x0309,
        WM_VSCROLLCLIPBOARD = 0x030A,
        WM_SIZECLIPBOARD = 0x030B,
        WM_ASKCBFORMATNAME = 0x030C,
        WM_CHANGECBCHAIN = 0x030D,
        WM_HSCROLLCLIPBOARD = 0x030E,
        WM_QUERYNEWPALETTE = 0x030F,
        WM_PALETTEISCHANGING = 0x0310,
        WM_PALETTECHANGED = 0x0311,
        WM_HOTKEY = 0x0312,
        WM_PRINT = 0x0317,
        WM_PRINTCLIENT = 0x0318,
        WM_THEME_CHANGED = 0x031A,
        WM_HANDHELDFIRST = 0x0358,
        WM_HANDHELDLAST = 0x035F,
        WM_AFXFIRST = 0x0360,
        WM_AFXLAST = 0x037F,
        WM_PENWINFIRST = 0x0380,
        WM_PENWINLAST = 0x038F,
        WM_APP = 0x8000,
        WM_USER = 0x0400,
        WM_REFLECT = WM_USER + 0x1c00
    }
    #endregion

    #region SetWindowPosFlags
    public enum SetWindowPosFlags
    {
        SWP_NOSIZE = 0x0001,
        SWP_NOMOVE = 0x0002,
        SWP_NOZORDER = 0x0004,
        SWP_NOREDRAW = 0x0008,
        SWP_NOACTIVATE = 0x0010,
        SWP_FRAMECHANGED = 0x0020,
        SWP_SHOWWINDOW = 0x0040,
        SWP_HIDEWINDOW = 0x0080,
        SWP_NOCOPYBITS = 0x0100,
        SWP_NOOWNERZORDER = 0x0200,
        SWP_NOSENDCHANGING = 0x0400,
        SWP_DRAWFRAME = 0x0020,
        SWP_NOREPOSITION = 0x0200,
        SWP_DEFERERASE = 0x2000,
        SWP_ASYNCWINDOWPOS = 0x4000
    }
    #endregion

    #region WINDOWINFO
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        public UInt32 cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public UInt32 dwStyle;
        public UInt32 dwExStyle;
        public UInt32 dwWindowStatus;
        public UInt32 cxWindowBorders;
        public UInt32 cyWindowBorders;
        public UInt16 atomWindowType;
        public UInt16 wCreatorVersion;
    }
    #endregion

    #region POINT
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;

        #region Constructors
        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public POINT(Point point)
        {
            x = point.X;
            y = point.Y;
        }
        #endregion
    }
    #endregion

    #region RECT
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public uint left;
        public uint top;
        public uint right;
        public uint bottom;

        #region Properties
        public POINT Location
        {
            get { return new POINT((int)left, (int)top); }
            set
            {
                right -= (left - (uint)value.x);
                bottom -= (bottom - (uint)value.y);
                left = (uint)value.x;
                top = (uint)value.y;
            }
        }

        public uint Width
        {
            get { return right - left; }
            set { right = left + value; }
        }

        public uint Height
        {
            get { return bottom - top; }
            set { bottom = top + value; }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return left + ":" + top + ":" + right + ":" + bottom;
        }
        #endregion
    }
    #endregion

    #region WINDOWPOS
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPOS
    {
        public IntPtr hwnd;
        public IntPtr hwndAfter;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public uint flags;

        #region Overrides
        public override string ToString()
        {
            return x + ":" + y + ":" + cx + ":" + cy + ":" + ((SWP_Flags)flags).ToString();
        }
        #endregion
    }
    #endregion

    #region NMHDR
    [StructLayout(LayoutKind.Sequential)]
    public struct NMHDR
    {
        public IntPtr hwndFrom;
        public uint idFrom;
        public uint code;
    }
    #endregion

    #region OFNOTIFY
    [StructLayout(LayoutKind.Sequential)]
    public struct OFNOTIFY
    {
        public NMHDR hdr;
        public IntPtr OPENFILENAME;
        public IntPtr fileNameShareViolation;
    }
    #endregion
    #endregion

}
