using DevExpress.Mvvm;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.Common;
using WSX.DataCollection.Utilities;
using WSX.DrawService.CanvasControl;
using WSX.GlobalData.Model;
using WSX.Logger;
using WSX.WSXCut.Utils;
using WSX.WSXCut.Views.CustomControl.Draw;

namespace WSX.WSXCut.Manager
{
    /// <summary>
    /// 图形文件处理
    /// </summary>
    public class FileManager
    {
        public const string DEFAULT_FILE_NAME = "New";
        public event Action<string> OnFileNameChanged;
        public string FilePath { get; private set; }
        public string FileName { get; private set; }
        private List<FigureBaseModel> oldFigures { get; set; }
        private static FileManager instance;
        public static FileManager Instance
        {
            get
            {
                return instance ?? (instance = new FileManager());
            }
        }
        public void New(UCCanvas canvas)
        {
            if (!IsNeedToSave(canvas)) return;
            canvas.Model.DrawingLayer.Objects.Clear();
            GlobalModel.TotalDrawObjectCount = 0;//计数归零
            canvas.DoInvalidate(true);
            this.oldFigures = null;
            this.UpdateFilePath(null);
        }
        private bool IsNeedToSave(UCCanvas canvas)
        {
            var curfigures = FigureHelper.ToFigureBaseModel(canvas.Model.DrawingLayer.Objects);
            if (oldFigures != null && oldFigures.Count == 0) oldFigures = null;
            if (curfigures != null && curfigures.Count == 0) curfigures = null;
            string oldHashcode = JsonConvert.SerializeObject(oldFigures);
            string curHashcode = JsonConvert.SerializeObject(curfigures);
            if (oldHashcode.GetHashCode() != curHashcode.GetHashCode())
            {
                var result = XtraMessageBox.Show(string.Format("是否保存对\"{0}\"的修改？", this.FileName), "消息", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.SaveFile(canvas.Model.DrawingLayer.Objects);
                    return true;
                }
                else if (result == DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public void OpenFileDialog(UCCanvas canvas)
        {
            try
            {
                if (!IsNeedToSave(canvas)) return;
                List<FigureBaseModel> figures = null;
                FigureFilePreview figurePreview = new FigureFilePreview();
                OpenFileDialogEx open = new OpenFileDialogEx();
                open.Filter = "所有文件(*.*)|*.WXF;*.DXF;|WSX默认切割文件(*.WXF)|*.WXF|AutoCAD图形文件(*.DXF)|*.DXF";
                open.PreviewControl = figurePreview;
                open.OnFileSelectChanged += (s, e) =>
                {
                    if (figurePreview.IsPreView)
                    {
                        figures = ParseFigureFile(e.Path);
                        figurePreview.FigurePreview(figures);
                    }
                    else
                    {
                        figurePreview.FigurePreview(new List<FigureBaseModel>());
                    }
                };
                if (open.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(open.FileName, canvas, false, figures);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void OpenFile(string fileName, UCCanvas canvas, bool isCheck, List<FigureBaseModel> figures = null)
        {
            try
            {
                if (isCheck && !IsNeedToSave(canvas)) return;
                if (figures == null)
                {
                    figures = ParseFigureFile(fileName);
                }
                FigureHelper.AddToDrawObject(canvas, figures, true);
                Messenger.Default.Send<object>(null, "OnPreview");
                this.oldFigures = FigureHelper.ToFigureBaseModel(canvas.Model.DrawingLayer.Objects);
                this.UpdateFilePath(fileName);
                LoggerManager.AddSystemInfos(string.Format("打开文件：{0}", fileName), Logger.LogLevel.Info);
            }
            catch (Exception ex)
            {
                LoggerManager.AddSystemInfos(string.Format("打开文件异常：{0}，原因：{1}", fileName, ex.Message), Logger.LogLevel.Error);
                XtraMessageBox.Show(ex.Message, "消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateFilePath(string path)
        {
            this.FilePath = path;
            this.FileName = string.IsNullOrEmpty(path) ? DEFAULT_FILE_NAME : Path.GetFileName(path);//Path.GetFileNameWithoutExtension(path);
            this.OnFileNameChanged?.Invoke(this.FileName);
        }
        /// <summary>
        /// 解析图形文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private List<FigureBaseModel> ParseFigureFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string exFileName = Path.GetExtension(fileName);
                switch (exFileName.ToUpper())
                {
                    case ".DXF":
                        {
                            return DxfHelper.LoadDXF(fileName);
                        }
                    case ".WXF":
                        {
                            return WxfHelper.XMLReadByFile(fileName);
                        }
                }
            }
            return null;
        }
        public void SaveFileDialog(List<DrawService.CanvasControl.IDrawObject> figures, string exName = ".WXF")
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = exName.Equals(".DXF") ? "图形文件(*.DXF)|*.DXF" : "WSX默认切割文件|*.WXF";
                if (save.ShowDialog() == DialogResult.OK)
                {
                    WriteToFile(figures, save.FileName);
                    this.UpdateFilePath(save.FileName);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.AddSystemInfos("保存文件异常:" + ex, LogLevel.Error);
                XtraMessageBox.Show(ex.Message, "消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SaveFile(List<DrawService.CanvasControl.IDrawObject> figures)
        {
            try
            {
                if (string.IsNullOrEmpty(this.FilePath) || Path.GetExtension(this.FilePath).ToUpper() != ".WXF")
                {
                    this.SaveFileDialog(figures);
                }
                else
                {
                    this.WriteToFile(figures, this.FilePath);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.AddSystemInfos("保存文件异常" + ex, LogLevel.Error);
                XtraMessageBox.Show(ex.Message, "消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void WriteToFile(List<DrawService.CanvasControl.IDrawObject> figures, string fileName)
        {
            string exFileName = Path.GetExtension(fileName);
            switch (exFileName.ToUpper())
            {
                case ".DXF":
                    {
                        var figs = FigureHelper.ToFigureBaseModel(figures);
                        DxfHelper.WriteDXF(figs, fileName);
                        this.oldFigures = figs;
                    }
                    break;
                case ".WXF":
                    {
                        var figs = FigureHelper.ToFigureBaseModel(figures);
                        WxfHelper.XMLWriteToFile(figs, fileName);
                        this.oldFigures = figs;
                    }
                    break;
            }
        }
        public void RegisterFileType()
        {
            var info = new FileTypeRegInfo()
            {
                ExtendName = ".WXF",
                Description = "WSX Cut Document(.WXF)",
                IcoPath = Application.StartupPath + @"\Assets\file2.ico",
                ExePath = Application.StartupPath + @"\WSXCut.exe"
            };
            FileAssociator.RegisterFileType(info);
        }

    }
}
