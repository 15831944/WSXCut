using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using System;
using System.Text;
using System.Windows.Forms;
using WSX.Logger;
using WSX.WSXCut.Manager;

namespace WSX.WSXCut
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                #region Register global exception events
                //设置应用程序处理异常方式：ThreadException处理
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                #endregion

                #region Application Entrance
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                BonusSkins.Register();
                SkinManager.EnableFormSkins();

                if (BootstrapManager.IsLaunched)
                {
                    XtraMessageBox.Show("该程序已经在运行！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Environment.Exit(0);
                }

                if (BootstrapManager.IsHardwareExist)
                {
                    var result = MessageBox.Show("激光危险，请小心使用！", "消息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    XtraMessageBox.Show("找不到运动控制卡，将进入到演示模式！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                MainView frm = new MainView();
                //BootstrapManager.Attatch("加载主界面…", () => frm = new MainView());
                if (!BootstrapManager.Intialize())
                {
                    Environment.Exit(0);
                }

                //Get associated file path if valid
                var args = Environment.GetCommandLineArgs();
                if (args.Length > 1)
                {
                    frm.ProjectFilePath = args[1];
                }

                Application.Run(frm);
                #endregion
            }
            catch (Exception ex)
            {
                string str = GetExceptionMsg(ex, string.Empty);
                MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogUtil.Instance.Error(str);
            }
        }

        #region Global exception handler
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            LogUtil.Instance.Error(str);
        }
   
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            LogUtil.Instance.Error(str);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
        #endregion
    }
}
