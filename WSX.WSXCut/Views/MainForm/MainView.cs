using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.Utilities;
using WSX.DrawService.DrawTool;
using WSX.Engine.Models;
using WSX.GlobalData;
using WSX.GlobalData.Messenger;
using WSX.GlobalData.Model;
using WSX.ViewModels.Common;
using WSX.WSXCut.Manager;
using WSX.WSXCut.Models;
using WSX.WSXCut.Views.CustomControl;
using WSX.WSXCut.Views.CustomControl.Draw;
using WSX.WSXCut.Views.CustomControl.Menu;
using WSX.WSXCut.Views.Forms;

namespace WSX.WSXCut
{
    public partial class MainView : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region 参数窗口
        private FrmReleaseOrSmoothing frmReleaseOrSmoothing;//倒圆角/释放角
        private FrmMicroConnectParams frmMicroConnectParams;//微连参数
        private FrmCompensation frmCompensation;//补偿
        private FrmRingCut frmRingCut;//环切
        private FrmAutoCoolingPoint frmAutoCoolingPoint;//自动冷却点
        private FrmAutoMicroConnect frmAutoMicroConnect;//自动微连
        #endregion
        public string ProjectFilePath { get; internal set; }
        private readonly string TITLE_FORMAT = "{0} - 万顺兴切割控制平台" + VersionInfo.SoftVersion + "{1}";
        private readonly RibPageMachine pageMachine;
        private readonly UCStatusBarInfo statusInfo;

        public MainView()
        {
            DispatcherHelper.Initialize(this);

            InitializeComponent();

            GlobalData.Model.GlobalModel.CurrentLayerId = GlobalData.Model.LayerId.One;
            this.ucLayerParaBar1.OnLayerIdChangedEvent += (sender, e) => this.canvasWrapper1.OnLayerIdChanged();
            this.ucLayerParaBar1.OnLayerWindowShow += (sender, e) => btnCraftItem_ItemClick(null, null);
            this.ucLayerParaBar1.OnMachineEnabledChanged += (sender, e) => this.canvasWrapper1.OnMachineEnabledChanged(e.IsMachineEnabled);

            this.pageMachine = new RibPageMachine(this.Ribbon);
            this.pageMachine.DisplayChanged += PageMachine_DisplayChanged;

            this.canvasWrapper1.OnSortDrawObjects += (sender, e) => this.btnSortItem_ItemClick(null, null);

            this.statusInfo = new UCStatusBarInfo
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = System.Drawing.Color.Transparent
            };
            this.statusInfo.Location = new Point(this.ribbonStatusBar1.Width - this.statusInfo.Width - 10, 0);
            this.ribbonStatusBar1.Controls.Add(this.statusInfo);

            this.SizeChanged += (sender, e) =>
            {
                if (this.panelRightMain.VerticalScroll.Visible)
                {
                    this.panelRightMain.Width = 306;
                }
                else
                {
                    this.panelRightMain.Width = 290;
                }
            };
            Messenger.Instance.Register("DrawObjectSelectedChange", this.DrawObjectSelectedChange);
            Messenger.Instance.Register("DrawObjectSelectedIsSigleGroupChange", this.DrawObjectSelectedIsSingleGroupChange);
            Messenger.Instance.Register("OnAutoRoundAngle", this.AutoRoundAngle);
            Messenger.Instance.Register("GroupInsideSortAction", this.GroupInsideSortAction);
        }

        private void PageMachine_DisplayChanged(object sender, DisplayChangedEventArgs e)
        {
            Action<object, EventArgs> resizeHandler = (sender1, e1) =>
             {
                 //int x = this.panelMain.Location.X;
                 //int y = this.panelMain.Location.Y;
                 //int height = this.pageMachine.Height;
                 //this.pageMachine.Location = new Point(x, y - height - 4);
                 if (this.WindowState == FormWindowState.Maximized)
                 {
                     this.pageMachine.Location = new Point(0, 58);
                 }
                 else
                 {
                     this.pageMachine.Location = new Point(0, 50);
                 }

             };

            if (e.IsDisplay)
            {
                this.Controls.Add(this.pageMachine);
                resizeHandler(null, null);
                this.SizeChanged += new System.EventHandler(resizeHandler);
                /*********** It is strange that setting enabled property causes error, maybe it's a DevExpress's bug  ********/
                //this.btnNewFile.Enabled = false;
                //this.btnOpenFile.Enabled = false;
                //this.btnLayoutItem.Enabled = false;
                //this.btnImportPart.Enabled = false;
                this.btnNewFile.Visibility = BarItemVisibility.Never;
                this.btnOpenFile.Visibility = BarItemVisibility.Never;
                this.btnLayoutItem.Visibility = BarItemVisibility.Never;
                this.btnImportPart.Visibility = BarItemVisibility.Never;
            }
            else
            {
                this.Controls.Remove(this.pageMachine);
                this.SizeChanged -= new System.EventHandler(resizeHandler);
                //this.btnNewFile.Enabled = true;
                //this.btnOpenFile.Enabled = true;
                //this.btnLayoutItem.Enabled = true;
                //this.btnImportPart.Enabled = true;
                this.btnNewFile.Visibility = BarItemVisibility.Always;
                this.btnOpenFile.Visibility = BarItemVisibility.Always;
                this.btnLayoutItem.Visibility = BarItemVisibility.Always;
                this.btnImportPart.Visibility = BarItemVisibility.Always;
            }
        }

        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<ViewModels.MainViewModel>();
            fluent.SetBinding(this.ckDisplayGapFigureFrame, c => c.Checked, x => x.AdditionalInfo.IsShowBoundRect);
            fluent.SetBinding(this.ckRedDisplayGapFigure, c => c.Checked, x => x.AdditionalInfo.IsShowNotClosedFigureInRed);
            fluent.SetBinding(this.ckDisplaySerialNumber, c => c.Checked, x => x.AdditionalInfo.IsShowFigureSN);
            fluent.SetBinding(this.ckDispalyPathStartPoint, c => c.Checked, x => x.AdditionalInfo.IsShowStartMovePoint);
            fluent.SetBinding(this.ckDisplayProcessPath, c => c.Checked, x => x.AdditionalInfo.IsShowMachinePath);
            fluent.SetBinding(this.ckDisplayEmptyMovePath, c => c.Checked, x => x.AdditionalInfo.IsShowVacantPath);
            fluent.SetBinding(this.ckDisplayMicroHyphen, c => c.Checked, x => x.AdditionalInfo.IsShowMicroConnectFlag);

            var viewModel = mvvmContext1.GetViewModel<ViewModels.MainViewModel>();
            string info = $"Version: {VersionInfo.SoftVersion}";
            if (SystemContext.IsDummyMode)
            {
                info += ", Dummy mode";
            }
            viewModel.AddSysLog(info, Color.Black);
        }

        #region 按钮事件

        #region 文件
        private void btnNewFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            FileManager.Instance.New(this.canvasWrapper1.GetDrawCanvas());
        }
        private void btnOpenFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            FileManager.Instance.OpenFileDialog(this.canvasWrapper1.GetDrawCanvas());
        }
        private void btnSaveFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            var figures = this.canvasWrapper1.GetDrawCanvas().Model.DrawingLayer.Objects;
            FileManager.Instance.SaveFile(figures);
        }
        private void btnSaveFileWXF_ItemClick(object sender, ItemClickEventArgs e)
        {
            var figures = this.canvasWrapper1.GetDrawCanvas().Model.DrawingLayer.Objects;
            FileManager.Instance.SaveFileDialog(figures, ".WXF");
        }
        private void btnSaveFileAutoCADDXF_ItemClick(object sender, ItemClickEventArgs e)
        {
            var figures = this.canvasWrapper1.GetDrawCanvas().Model.DrawingLayer.Objects;
            FileManager.Instance.SaveFileDialog(figures, ".DXF");
        }
        private void btnImportPart_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnImportPart, e.Item);

        }
        private void btnImportStandardPart_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnImportStandardPart, e.Item);

        }
        private void btnImportFileLXD_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnImportFileLXD, e.Item);

        }
        private void btnImportFileDXF_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnImportFileDXF, e.Item);

        }
        private void btnImportFilePLT_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnImportFilePLT, e.Item);

        }
        private void btnImportFileAI_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnImportFileAI, e.Item);

        }
        private void btnImportFileGerBer_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnImportFileGerBer, e.Item);

        }
        private void btnImportFileNC_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnImportFileNC, e.Item);

        }
        private void btnProcessingReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnProcessingReport, e.Item);

        }
        private void btnLayoutReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnLayoutReport, e.Item);

        }
        private void btnUseReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnUseReport, e.Item);

        }
        private void btnSetUserParam_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Messenger.Instance.Send(MainEvent.OnSetUserParam, e.Item);
            FrmUserConfig frm = new FrmUserConfig(GlobalModel.Params.UserConfig);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.UserConfig = frm.Model;
            }
        }
        private void btnBackUpParam_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBackUpParam, e.Item);

        }
        private void btnControlCardMonitor_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnControlCardMonitor, e.Item);

        }
        private void btnExtendIOMonitor_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnExtendIOMonitor, e.Item);

        }
        private void btnGasDACorrection_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnGasDACorrection, e.Item);

        }
        private void btnBCS100Monitor_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCS100Monitor, e.Item);

        }
        private void btnBurnTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBurnTest, e.Item);

        }
        #endregion

        #region 图形
        private void btnLine_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnLine, e.Item);

        }

        private void btnRectangleItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnRectangleItem, e.Item);

        }

        private void btnRectangle_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnRectangle, e.Item);

        }

        private void btnFilletRectangle_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnFilletRectangle, e.Item);

        }

        private void btnTrackRectangle_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnTrackRectangle, e.Item);

        }

        private void btnCircleItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCircleItem, e.Item);

        }

        private void btnCircle_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCircle, e.Item);

        }

        private void btnThreePointArc_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnThreePointArc, e.Item);

        }

        private void btnScanArc_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnScanArc, e.Item);

        }

        private void btnNewEllipse_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnNewEllipse, e.Item);

        }

        private void btnCircleReplaceToAcnode_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCircleReplaceToAcnode, e.Item);

        }

        private void btnReplaceToCircle_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnReplaceToCircle, e.Item);

        }

        private void btnMultiLineItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMultiLineItem, e.Item);

        }

        private void btnMultiLine_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMultiLine, e.Item);

        }

        private void btnPolygon_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnPolygon, e.Item);

        }

        private void btnStellate_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnStellate, e.Item);

        }

        private void btnDot_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnDot, e.Item);

        }

        private void btnWord_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnWord, e.Item);

        }
        #endregion

        #region 工艺参数


        private void btnShowCarft_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarCheckItem barCheckItem = e.Item as BarCheckItem;

            if (barCheckItem != null && barCheckItem.Tag != null && string.IsNullOrEmpty(barCheckItem.Tag.ToString()) == false)
            {
                int layerid = 0;
                int.TryParse(barCheckItem.Tag.ToString(), out layerid);

                this.canvasWrapper1.OnShowCarft(layerid, barCheckItem.Checked);
            }

        }

        private void btnOnlyShowCarft_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarButtonItem barButtonItem = e.Item as BarButtonItem;

            if (barButtonItem != null && barButtonItem.Tag != null && string.IsNullOrEmpty(barButtonItem.Tag.ToString()) == false)
            {
                int layerid = 0;
                int.TryParse(barButtonItem.Tag.ToString(), out layerid);

                this.canvasWrapper1.OnOnlyShowCarft(layerid);

                foreach (var item in btnShowFigureLayerItem.LinksPersistInfo)
                {
                    if (item != null)
                    {
                        var links = item as LinkPersistInfo;
                        if (links != null && links.Item != null)
                        {

                            BarCheckItem barCheckItem = links.Item as BarCheckItem;
                            if (barCheckItem != null && barCheckItem.Name != null)
                            {
                                if (barCheckItem.Name.StartsWith("ckShowCarft"))
                                {
                                    if (layerid == 0)
                                    {

                                        barCheckItem.Checked = true;
                                        continue;
                                    }
                                    if (barCheckItem.Name.Equals("ckShowCarft" + barButtonItem.Tag.ToString()) == false)
                                    {
                                        barCheckItem.Checked = false;
                                    }
                                    else
                                    {
                                        barCheckItem.Checked = true;
                                    }
                                }
                            }

                        }
                    }

                }

            }

        }

        private void btnLockCarft_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarCheckItem barCheckItem = e.Item as BarCheckItem;

            if (barCheckItem != null && barCheckItem.Tag != null && string.IsNullOrEmpty(barCheckItem.Tag.ToString()) == false)
            {
                int layerid = 0;
                int.TryParse(barCheckItem.Tag.ToString(), out layerid);

                this.canvasWrapper1.OnLockCarft(layerid, barCheckItem.Checked);
            }

        }

        private void btnDXFShowCarft_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnDXFFigureMap, e.Item);
        }
        #endregion

        #region 排样
        private void btnDeleteAllLayoutPart_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnDeleteAllLayoutPart, e.Item);

        }

        private void btnSetSheetMaterial_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnSetSheetMaterial, e.Item);

        }

        private void btnDeleteAllSheetMaterial_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnDeleteAllSheetMaterial, e.Item);

        }
        #endregion

        #region 选择

        private void btnSelectAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnSelectAll();
        }
        private void btnInvertSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnInvertSelect();

        }
        private void btnCancelSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnCancelSelectAll();

        }
        private void btnBatchChange_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBatchChange, e.Item);

        }
        private void ckBanFastDragCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnBanFastDragCopy();

        }
        private void btnSelectGapFigure_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnSelectGapFigure();


        }
        private void btnSelectSimilarFigure_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnSelectSimilarFigure, e.Item);

        }
        private void btnSelectAllExternalModes_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnSelectAllExternalModes();

        }
        private void btnSelectAllInternalModes_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnSelectAllInternalModes();

        }
        private void btnSelectAllFigureSmaller_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnSelectAllFigureSmaller, e.Item);

        }
        private void btnSelectLayerCraft_ItemClick(object sender, ItemClickEventArgs e)
        {

            BarButtonItem barButtonItem = e.Item as BarButtonItem;

            if (barButtonItem != null && barButtonItem.Tag != null && string.IsNullOrEmpty(barButtonItem.Tag.ToString()) == false)
            {
                int layerid = 0;
                int.TryParse(barButtonItem.Tag.ToString(), out layerid);

                this.canvasWrapper1.OnSelectLayerCraft(layerid);
            }

        }

        private void btnLockSelectBackground_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnLockSelectBackground, e.Item);

        }
        private void btnSelectAllMultiLines_ItemClick(object sender, ItemClickEventArgs e)
        {

            this.canvasWrapper1.OnSelectAllMultiLines();


        }
        private void btnSelectAllCircles_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnSelectAllCircles();


        }
        private void btnSelectAllBezierCurves_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnSelectAllBezierCurves, e.Item);

        }

        private void btnSelectItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.canvasWrapper1.GetDrawingComponent().GetDrawingLayer().SelectedObjects == null || this.canvasWrapper1.GetDrawingComponent().GetDrawingLayer().SelectedObjects.Count <= 0)
            {
                btnCopy.Enabled = false;
                btnCut.Enabled = false;
                btnCopyBasePoint.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                btnCopy.Enabled = true;
                btnCut.Enabled = true;
                btnCopyBasePoint.Enabled = true;
                btnDelete.Enabled = true;
            }

            if (this.canvasWrapper1.GetDrawCanvas().Model.CopyObjectInClipBoard == null || this.canvasWrapper1.GetDrawCanvas().Model.CopyObjectInClipBoard.Count <= 0)
            {
                btnPaste.Enabled = false;
            }
            else
            {
                btnPaste.Enabled = true;
            }
        }
        private void btnCut_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnCut();

        }
        private void btnCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnCopy();

        }
        private void btnCopyBasePoint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCopyBasePoint, e.Item);

        }
        private void btnPaste_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnPaste();

        }
        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {

            this.canvasWrapper1.OnDelete();
        }
        #endregion

        #region 显示
        private void ckDisplayGapFigureFrame_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnShowBoundRect();
        }

        private void ckRedDisplayGapFigure_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnShowNotClosedFigure();
        }

        private void ckDisplaySerialNumber_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnShowFigureSN();
        }

        private void ckDispalyPathStartPoint_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnShowStartMovePoint();
        }

        private void ckDisplayProcessPath_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnShowMachinePath();
        }

        private void ckDisplayEmptyMovePath_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnShowVacantPath();
        }

        private void ckDisplayMicroHyphen_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnShowMicroConnectFlag();
        }

        private void btnFigureCenter_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnCenterAligment(null);
        }

        #endregion

        #region 工艺设置

        private void btnClearMicroConnect_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnClearMicroConnect();
        }
        private void btnCancelGapCompensation_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnCompensation(false, null);
        }
        private void btnStartPoint_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnSettingStartPoint(null);
        }
        private void btnDockPosition_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmDockPosition frm = new FrmDockPosition(GlobalModel.Params.DockPosition);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.DockPosition = frm.Model;
            }
        }
        private void btnGapCompensation_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (frmCompensation == null)
            {
                frmCompensation = new FrmCompensation();
            }
            if (frmCompensation.ShowDialog() == DialogResult.OK)
            {
                this.canvasWrapper1.OnCompensation(frmCompensation.IsAllFigure, frmCompensation.IsCancel ? null : frmCompensation.Param);
            }
        }
        private void btnOuterCut_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnOuterCut();

        }
        private void btnInnerCut_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnInnerCut();
        }
        private void btnRingCut_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (frmRingCut == null)
            {
                frmRingCut = new FrmRingCut();
            }
            if (frmRingCut.ShowDialog() == DialogResult.OK)
            {
                this.canvasWrapper1.OnCornerRing(frmRingCut.IsCancel ? null : frmRingCut.Param);
            }
        }
        private void btnReleaseAngle_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.frmReleaseOrSmoothing == null)
            {
                this.frmReleaseOrSmoothing = new FrmReleaseOrSmoothing();
            }
            frmReleaseOrSmoothing.SetHeaderParams("释放角");
            if (this.frmReleaseOrSmoothing.ShowDialog() == DialogResult.Yes)
            {
                this.canvasWrapper1.OnReleaseAngle(this.frmReleaseOrSmoothing.RoundRadius);
            }
        }

        private void btnRoundAngle_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.frmReleaseOrSmoothing == null)
            {
                this.frmReleaseOrSmoothing = new FrmReleaseOrSmoothing();
            }
            frmReleaseOrSmoothing.SetHeaderParams("倒圆角");
            if (this.frmReleaseOrSmoothing.ShowDialog() == DialogResult.Yes)
            {
                this.canvasWrapper1.OnRoundAngle(this.frmReleaseOrSmoothing.RoundRadius);
            }
        }

        private void AutoRoundAngle(object sender)
        {
            if (this.frmReleaseOrSmoothing == null)
            {
                this.frmReleaseOrSmoothing = new FrmReleaseOrSmoothing();
            }
            frmReleaseOrSmoothing.SetHeaderParams("倒圆角");
            if (this.frmReleaseOrSmoothing.ShowDialog() == DialogResult.Yes)
            {
                this.canvasWrapper1.OnAutoRoundAngle(this.frmReleaseOrSmoothing.RoundRadius);
            }
            
        }
        private void btnMicroConnectItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.frmMicroConnectParams == null)
            {
                this.frmMicroConnectParams = new FrmMicroConnectParams();
            }
            if (this.frmMicroConnectParams.ShowDialog() == DialogResult.Yes)
            {
                this.canvasWrapper1.OnMicroConncect(this.frmMicroConnectParams.MicroConnLength, this.frmMicroConnectParams.IsApplyingToSimilarGraphics);
            }
            else
            {
                //TODO:取消微连模式
            }
        }
        private void btnMicroConnectionAuto_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (frmAutoMicroConnect == null)
            {
                frmAutoMicroConnect = new FrmAutoMicroConnect();
            }
            if (frmAutoMicroConnect.ShowDialog() == DialogResult.OK)
            {
                this.canvasWrapper1.OnAutoMicroConnect(frmAutoMicroConnect.IsAllFigure, frmAutoMicroConnect.Param);
            }
        }
        private void btnMicroConnectBlowOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMicroConnectBlowOpen, e.Item);
            //this.canvasWrapper1.OnMicroConnectBlowOpen();
        }
        private void btnReverseItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnReverseDirection(null);
        }
        private void btnReverse_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnReverse, e.Item);

        }
        private void btnClockwise_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnClockwise, e.Item);

        }
        private void btnAnticlockwise_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnAnticlockwise, e.Item);

        }

        #region 封口、缺口、过切、多圈
        private float pos = 2;
        private void btnSealingItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.btnSealing_ItemClick(null, null);
        }
        private void btnSealing_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnOverCutting(0, false);
        }

        private void btnGaping_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmGapOrOverCutSizeSet frm = new FrmGapOrOverCutSizeSet(0, pos);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.pos = frm.Pos;
                this.canvasWrapper1.OnOverCutting(-this.pos, false);
            }
        }
        private void btnOverCut_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmGapOrOverCutSizeSet frm = new FrmGapOrOverCutSizeSet(1, pos);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.pos = frm.Pos;
                this.canvasWrapper1.OnOverCutting(this.pos, false);
            }
        }

        private void btnMutilGaps_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmGapOrOverCutSizeSet frm = new FrmGapOrOverCutSizeSet(2, pos);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.pos = frm.Pos;
                if (this.pos > 10) { this.pos = 10; }
                this.canvasWrapper1.OnOverCutting(this.pos, true);
            }
        }
        #endregion

        private void btnCoolingDotItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnSetCoolPoint();
        }
        private void btnCoolingDotClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnClearCoolPoint();
        }
        private void btnCoolingDotAuto_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (frmAutoCoolingPoint == null)
            {
                frmAutoCoolingPoint = new FrmAutoCoolingPoint();
            }
            if (frmAutoCoolingPoint.ShowDialog() == DialogResult.OK)
            {
                this.canvasWrapper1.OnSetAutoCoolPoint(frmAutoCoolingPoint.IsLeadIn, frmAutoCoolingPoint.IsCorner, frmAutoCoolingPoint.MaxAngle);
            }
        }
        private void btnCraftItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmLayerConfig frm = new FrmLayerConfig(GlobalModel.Params.LayerConfig, this.canvasWrapper1.GetLayerIdCollection());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.LayerConfig = frm.Model;
            }
        }
        #endregion

        #region 排序
        private void btnSortItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (var item in btnSortItem.ItemLinks)
            {
                if (item != null)
                {
                    BarCheckItemLink barCheckItemLink = item as BarCheckItemLink;
                    if (barCheckItemLink != null)
                    {
                        if (barCheckItemLink.Item != null)
                        {
                            BarCheckItem barCheckItem = barCheckItemLink.Item as BarCheckItem;

                            if (barCheckItem != null && barCheckItem.GroupIndex == 1 && barCheckItem.Checked)
                            {
                                btnSort_ItemClick(null, new ItemClickEventArgs(barCheckItem, barCheckItemLink));
                                break;
                            }
                        }

                    }
                }
            }
        }

        private void GroupInsideSortAction(object sender)
        {
           var temp =  sender as ToolStripMenuItem;
            UnitPoint unitPoint = ZeroPoint();
            if (temp != null && temp.Tag != null)
            {
                this.canvasWrapper1.OnSort(temp.Tag.ToString(), unitPoint, ckSortProhibitChangDirection.Checked, ckSortDistinguishInsideOutside.Checked, ckSortShadeCutOutermost.Checked, true);
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSort_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarButtonItem barButtonItem = e.Item as BarButtonItem;
            BarCheckItem barCheckItem = e.Item as BarCheckItem;
            UnitPoint unitPoint = ZeroPoint();
            if (barButtonItem != null && barButtonItem.Tag != null && string.IsNullOrEmpty(barButtonItem.Tag.ToString()) == false)
            {
                this.canvasWrapper1.OnSort(barButtonItem.Tag.ToString(), unitPoint, ckSortProhibitChangDirection.Checked, ckSortDistinguishInsideOutside.Checked, ckSortShadeCutOutermost.Checked, false);
            }
            else if (barCheckItem != null && barCheckItem.Tag != null && string.IsNullOrEmpty(barCheckItem.Tag.ToString()) == false)
            {
                this.canvasWrapper1.OnSort(barCheckItem.Tag.ToString(), unitPoint, ckSortProhibitChangDirection.Checked, ckSortDistinguishInsideOutside.Checked, ckSortShadeCutOutermost.Checked, false);
            }
        }

        private static UnitPoint ZeroPoint()
        {
            int index = SystemContext.CoordinatePara.RefZeroIndex;
            if (index == 0)   //Floating coordinate
            {
                var p = SystemContext.Hardware.GetCurrentPosition();
                SystemContext.CoordinatePara.RefZeroSeries[0] = p;
            }
            var zeroPoint = SystemContext.CoordinatePara.RefZeroSeries[index];
            UnitPoint unitPoint = new UnitPoint(zeroPoint);
            return unitPoint;
        }

        private void btnSortRangeTagets_EditValueChanged(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region 工具
        private void btnStockLayoutItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Messenger.Instance.Send(MainEvent.OnStockLayoutItem, e.Item);
            FrmAutoLayout frm = new FrmAutoLayout(GlobalModel.Params.AutoLayout);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.AutoLayout = frm.Model;
            }
        }

        private void btnStockLayoutClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnStockLayoutClear, e.Item);

        }
        /// <summary>
        /// 阵列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnArrayLayoutItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmArrayRectangle frm = new FrmArrayRectangle(GlobalModel.Params.ArrayRectangle);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.ArrayRectangle = frm.Model;
                this.canvasWrapper1.OnArray(frm.Model);
            }
        }
        /// <summary>
        /// 环形阵列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnArrayLayoutAnnular_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Messenger.Instance.Send(MainEvent.OnArrayLayoutAnnular, e.Item);
            FrmArrayAnnular frm = new FrmArrayAnnular(GlobalModel.Params.ArrayAnnular);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.ArrayAnnular = frm.Model;
                this.canvasWrapper1.OnArray(frm.Model);
            }
        }
        /// <summary>
        /// 矩形阵列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnArrayLayoutRectangle_ItemClick(object sender, ItemClickEventArgs e)
        {
            btnArrayLayoutItem_ItemClick(null, null);
        }
        /// <summary>
        /// 交互式阵列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnArrayLayoutInteractive_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Messenger.Instance.Send(MainEvent.OnArrayLayoutInteractive, e.Item);
            FrmArrayInteractive frm = new FrmArrayInteractive(GlobalModel.Params.ArrayInteractive);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.ArrayInteractive = frm.Model;
                this.canvasWrapper1.OnArray(frm.Model);
            }

        }
        /// <summary>
        /// 布满
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnArrayLayoutFull_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Messenger.Instance.Send(MainEvent.OnArrayLayoutFull, e.Item);
            FrmArrayFull frm = new FrmArrayFull(GlobalModel.Params.ArrayFull);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.ArrayFull = frm.Model;
                this.canvasWrapper1.OnArray(frm.Model);
            }
        }

        private void btnGroupItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (btnGroupItem.Tag.ToString().Equals("1"))
            { this.canvasWrapper1.OnGroup(); }
            else
            {
                this.canvasWrapper1.OnGroupScatterSelected();
            }

        }
        /// <summary>
        /// 选中所有群组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupSelectAll_ItemClick(object sender, ItemClickEventArgs e)
        {

            this.canvasWrapper1.OnGroupSelectAll();

        }
        /// <summary>
        /// 打散所有群组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupScatterAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnGroupScatterAll();
        }
        /// <summary>
        /// 打散选中群组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnGroupScatterSelected_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnGroupScatterSelected();
        }
        /// <summary>
        /// 炸开图形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupBlowOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnGroupBlowOpen, e.Item);

        }

        private void btnMultiContourCotangent_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Messenger.Instance.Send(MainEvent.OnMultiContourCotangent, e.Item);
            FrmMultiContourConnectCut frm = new FrmMultiContourConnectCut(GlobalModel.Params.MultiContourConnectCut);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.MultiContourConnectCut = frm.Model;
            }
        }

        private void btnBlowOpenMultiContourCotangent_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBlowOpenMultiContourCotangent, e.Item);

        }

        #region 飞切

        private void btnFlyingCutItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedObjects = this.canvasWrapper1.GetDrawingComponent().GetDataModel().DrawingLayer.SelectedObjects;

            int count = 0;
            foreach (var item in selectedObjects)
            {
                var obj = item as WSX.DrawService.DrawTool.CircleTool.Circle;
                if (obj == null || obj.Center == null || obj.Radius <= 0)
                    continue;
                count++;
            }

            if(count > 0 && count == selectedObjects.Count)
            {
                this.btnFlyingCutArc_ItemClick(null, null);
            }
            else
            {
                this.btnFlyingCutLine_ItemClick(null, null);
            }
        }

        private void btnFlyingCutLine_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmLineFlyingCut frm = new FrmLineFlyingCut(GlobalModel.Params.LineFlyingCut);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.LineFlyingCut = frm.Model;

                this.canvasWrapper1.OnLineFlyCut(frm.Model);
            }
        }

        private void btnFlyingCutArc_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmArcFlyingCut frm = new FrmArcFlyingCut(GlobalModel.Params.ArcFlyingCut);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.ArcFlyingCut = frm.Model;

                if(frm.Model.IsFirstSort && frm.Model.SortType == CommomModel.ParaModel.ArcFlyingCutSortTypes.LocalShortestEmptyMove)
                {
                    //局部最短空移 先调用排序
                    UnitPoint unitPoint = ZeroPoint();
                    this.canvasWrapper1.OnSort("SortShortestMove", unitPoint, ckSortProhibitChangDirection.Checked, ckSortDistinguishInsideOutside.Checked, ckSortShadeCutOutermost.Checked, false);
                }

                this.canvasWrapper1.OnArcFlyCut(frm.Model);
            }
        }

        #endregion

        #region 共边
        private void btnCommonSideItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.canvasWrapper1.IsCommonSide())
            {
                FrmCommonSide frm = new FrmCommonSide(GlobalModel.Params.CommonSideRectangle);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    GlobalModel.Params.CommonSideRectangle = frm.Model;
                    this.canvasWrapper1.OnCommonSide(GlobalModel.Params.CommonSideRectangle);
                }
            }
            else
            {
                GlobalModel.Params.CommonSideRectangle.CommonSideStyle = CommomModel.ParaModel.CommonSideStyles.Irregular;
                this.canvasWrapper1.OnCommonSide(GlobalModel.Params.CommonSideRectangle);
            }
        }
        /// <summary>
        /// C字形共边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommonSideTypeC_ItemClick(object sender, ItemClickEventArgs e)
        {
            GlobalModel.Params.CommonSideRectangle.CommonSideStyle = CommomModel.ParaModel.CommonSideStyles.Cglyph;
            this.canvasWrapper1.OnCommonSide(GlobalModel.Params.CommonSideRectangle);
        }
        #endregion

        private void btnBridging_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnBridge(() => {
                FrmBridging frm = new FrmBridging(GlobalModel.Params.Bridging);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    GlobalModel.Params.Bridging = frm.Model;
                    return frm.Model;
                }
                return null;
            });
        }

        private void btnMeasure_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnMeasure();

        }

        private void btnCurveSmoothing_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Messenger.Instance.Send(MainEvent.OnCurveSmoothing, e.Item);
            FrmCurveSmoothing frm = new FrmCurveSmoothing(GlobalModel.Params.CurveSmoothing);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.CurveSmoothing = frm.Model;
            }
        }

        private void btnCurveSegment_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCurveSegment, e.Item);

        }

        private void btnDeleteRepeatLine_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnDeleteRepeatLine, e.Item);

        }

        private void btnDeleteSmallFigure_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Messenger.Instance.Send(MainEvent.OnDeleteSmallFigure, e.Item);
            FrmDeleteSmallFigure frm = new FrmDeleteSmallFigure(GlobalModel.Params.DeleteSmallFigure);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.DeleteSmallFigure = frm.Model;
            }
        }

        private void btnMergeConnectLine_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Messenger.Instance.Send(MainEvent.OnMergeConnectLine, e.Item);
            FrmMergeConnectLine frm = new FrmMergeConnectLine(GlobalModel.Params.MergeConnectLine);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.MergeConnectLine = frm.Model;
            }
        }

        private void btnCutUp_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Messenger.Instance.Send(MainEvent.OnCutUp, e.Item);
            FrmCutUp frm = new FrmCutUp(GlobalModel.Params.CutUp);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.CutUp = frm.Model;
            }
        }

        private void btnConnectKnife_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Messenger.Instance.Send(MainEvent.OnConnectKnife, e.Item);
            FrmConnectKnife frm = new FrmConnectKnife(GlobalModel.Params.ConnectKnife);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                GlobalModel.Params.ConnectKnife = frm.Model;
            }
        }
        #endregion

        #region 模拟
        private void btnRangeSimulateSpeed_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnRangeSimulateSpeed, e.Item);

        }

        private void btnSimulate_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnSimulate, e.Item);

        }

        private void btnSubSpeed_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnSubSpeed, e.Item);

        }

        private void btnAddSpeed_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnAddSpeed, e.Item);

        }
        #endregion

        #region 加工
        private void btnFindEdgeItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnFindEdgeItem, e.Item);

        }

        private void ckCapacitanceEdge_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCapacitanceEdge, e.Item);

        }

        private void ckDotEdge_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnDotEdge, e.Item);

        }

        private void ckManualEdge_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnManualEdge, e.Item);

        }

        private void ckAutoClearEdgeProcessed_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnAutoClearEdgeProcessed, e.Item);

        }

        private void ckClearEdgeAngle_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnClearEdgeAngle, e.Item);

        }

        private void btnPLCProcessItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnPLCProcessItem, e.Item);

        }
        private void btnSaveProcessTask_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnSaveProcessTask, e.Item);

        }
        private void btnLoadProcessTask_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnLoadProcessTask, e.Item);

        }
        private void btnEditPLCProcess_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEditPLCProcess, e.Item);

        }

        private void btnChangeToWorkbenchA_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnChangeToWorkbenchA, e.Item);

        }

        private void btnChangeToWorkbenchB_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnChangeToWorkbenchB, e.Item);

        }

        private void btnCustomProcess1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess1, e.Item);

        }

        private void btnCustomProcess2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess2, e.Item);

        }

        private void btnCustomProcess3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess3, e.Item);

        }

        private void btnCustomProcess4_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess4, e.Item);

        }

        private void btnCustomProcess5_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess5, e.Item);

        }

        private void btnCustomProcess6_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess6, e.Item);

        }

        private void btnCustomProcess7_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess7, e.Item);

        }

        private void btnCustomProcess8_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess8, e.Item);

        }

        private void btnCustomProcess9_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess9, e.Item);

        }

        private void btnCustomProcess10_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess10, e.Item);

        }

        private void btnCustomProcess11_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess11, e.Item);

        }

        private void btnCustomProcess12_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess12, e.Item);

        }

        private void btnCustomProcess13_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess13, e.Item);

        }

        private void btnCustomProcess14_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess14, e.Item);

        }

        private void btnCustomProcess15_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess15, e.Item);

        }

        private void btnCustomProcess16_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess16, e.Item);

        }

        private void btnCustomProcess17_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess17, e.Item);

        }

        private void btnCustomProcess18_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess18, e.Item);

        }

        private void btnCustomProcess19_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess19, e.Item);

        }

        private void btnCustomProcess20_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnCustomProcess20, e.Item);

        }
        #endregion

        #region 数控
        private void btnGoOriginItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnGoOriginItem, e.Item);

        }

        private void btnGoOriginAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnGoOriginAll, e.Item);

        }

        private void btnGoOriginX_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnGoOriginX, e.Item);

        }

        private void btnGoOriginY_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnGoOriginY, e.Item);

        }

        private void btnGantryInit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnGantryInit, e.Item);

        }

        private void ckExecuteGantrySyncGoOrigin_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnExecuteGantrySyncGoOrigin, e.Item);

        }

        private void ckAdjustHeightenGoOrigin_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnAdjustHeightenGoOrigin, e.Item);

        }

        private void ckElectricFocusgoOrigin_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnElectricFocusgoOrigin, e.Item);

        }

        private void btnLightPathDebug_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnLightPathDebug, e.Item);

        }

        private void btnBCSStop_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSStop, e.Item);

        }

        private void btnBCSGoBerth_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSGoBerth, e.Item);

        }

        private void btnBCSGoOrigin_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSGoOrigin, e.Item);

        }

        private void btnBCSFollow1mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollow1mm, e.Item);

        }

        private void btnBCSFollow2mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollow2mm, e.Item);

        }

        private void btnBCSFollow3mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollow3mm, e.Item);

        }

        private void btnBCSFollow4mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollow4mm, e.Item);

        }
        private void btnBCSFollow5mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollow5mm, e.Item);

        }
        private void btnBCSFollow6mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollow6mm, e.Item);

        }
        private void btnBCSFollow7mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollow7mm, e.Item);

        }
        private void btnBCSFollow8mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollow8mm, e.Item);

        }
        private void btnBCSFollow9mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollow9mm, e.Item);

        }
        private void btnBCSFollow10mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollow10mm, e.Item);

        }

        private void btnBCSFollowStop1mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop1mm, e.Item);

        }

        private void btnBCSFollowStop2mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop2mm, e.Item);

        }

        private void btnBCSFollowStop3mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop3mm, e.Item);

        }

        private void btnBCSFollowStop4mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop4mm, e.Item);

        }

        private void btnBCSFollowStop5mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop5mm, e.Item);

        }

        private void btnBCSFollowStop6mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop6mm, e.Item);

        }

        private void btnBCSFollowStop7mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop7mm, e.Item);

        }

        private void btnBCSFollowStop8mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop8mm, e.Item);

        }

        private void btnBCSFollowStop9mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop9mm, e.Item);

        }

        private void btnBCSFollowStop10mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop10mm, e.Item);

        }

        private void btnBCSFollowStop15mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop15mm, e.Item);

        }

        private void btnBCSFollowStop20mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop20mm, e.Item);

        }

        private void btnBCSFollowStop25mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop25mm, e.Item);

        }

        private void btnBCSFollowStop30mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSFollowStop30mm, e.Item);

        }

        private void btnEmptyMoveRelative1mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelative1mm, e.Item);

        }

        private void btnEmptyMoveRelative2mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelative2mm, e.Item);

        }

        private void btnEmptyMoveRelative3mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelative3mm, e.Item);

        }

        private void btnEmptyMoveRelative4mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelative4mm, e.Item);

        }

        private void btnEmptyMoveRelative5mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelative5mm, e.Item);

        }

        private void btnEmptyMoveRelative6mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelative6mm, e.Item);

        }

        private void btnEmptyMoveRelative7mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelative7mm, e.Item);

        }

        private void btnEmptyMoveRelative8mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelative8mm, e.Item);

        }

        private void btnEmptyMoveRelative9mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelative9mm, e.Item);

        }

        private void btnEmptyMoveRelative10mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelative10mm, e.Item);

        }

        private void btnEmptyMoveRelativeMinus1mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelativeMinus1mm, e.Item);

        }

        private void btnEmptyMoveRelativeMinus2mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelativeMinus2mm, e.Item);

        }

        private void btnEmptyMoveRelativeMinus3mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelativeMinus3mm, e.Item);

        }

        private void btnEmptyMoveRelativeMinus4mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelativeMinus4mm, e.Item);

        }

        private void btnEmptyMoveRelativeMinus5mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelativeMinus5mm, e.Item);

        }

        private void btnEmptyMoveRelativeMinus6mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelativeMinus6mm, e.Item);

        }

        private void btnEmptyMoveRelativeMinus7mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelativeMinus7mm, e.Item);

        }

        private void btnEmptyMoveRelativeMinus8mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelativeMinus8mm, e.Item);

        }

        private void btnEmptyMoveRelativeMinus9mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelativeMinus9mm, e.Item);

        }

        private void btnEmptyMoveRelativeMinus10mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEmptyMoveRelativeMinus10mm, e.Item);

        }

        private void btnMoveAbsolutePositionZ0_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ0, e.Item);

        }

        private void btnMoveAbsolutePositionZ1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ1, e.Item);

        }

        private void btnMoveAbsolutePositionZ2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ2, e.Item);

        }

        private void btnMoveAbsolutePositionZ3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ3, e.Item);

        }

        private void btnMoveAbsolutePositionZ4_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ4, e.Item);

        }


        private void btnMoveAbsolutePositionZ5_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ5, e.Item);

        }

        private void btnMoveAbsolutePositionZ6_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ6, e.Item);

        }

        private void btnMoveAbsolutePositionZ7_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ7, e.Item);

        }

        private void btnMoveAbsolutePositionZ8_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ8, e.Item);

        }

        private void btnMoveAbsolutePositionZ9_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ9, e.Item);

        }

        private void btnMoveAbsolutePositionZ10_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ10, e.Item);

        }

        private void btnMoveAbsolutePositionZ15_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ15, e.Item);

        }

        private void btnMoveAbsolutePositionZ20_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ20, e.Item);

        }

        private void btnMoveAbsolutePositionZ25_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ25, e.Item);

        }

        private void btnMoveAbsolutePositionZ30_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ30, e.Item);

        }

        private void btnMoveAbsolutePositionZ40_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ40, e.Item);

        }

        private void btnMoveAbsolutePositionZ50_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMoveAbsolutePositionZ50, e.Item);

        }

        private void btnBCSKeyCalibration_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSKeyCalibration, e.Item);

        }

        private void btnBCSRegisteredEncrypt_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSRegisteredEncrypt, e.Item);

        }

        private void btnBCSUpdate_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSUpdate, e.Item);

        }

        private void btnBCSSaveParams_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSSaveParams, e.Item);

        }

        private void btnBCSRecordZCoordinate_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBCSRecordZCoordinate, e.Item);

        }

        private void btnQCW_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnQCW, e.Item);

        }

        private void btnErrorMeasure_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnErrorMeasure, e.Item);

        }

        private void btnGantryErrorMonitor_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnGantryErrorMonitor, e.Item);

        }
        #endregion

        #region 视图
        private void btnSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnSelect, e.Item);

        }

        private void btnEditNode_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEditNode, e.Item);

        }

        private void btnTranslateView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnTranslateView, e.Item);

        }

        private void btnSortMode_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnSortMode, e.Item);

        }

        private void ckEnglish_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnEnglish, e.Item);

        }

        private void ckSimplifiedChinese_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnSimplifiedChinese, e.Item);

        }

        private void ckTraditionalChinese_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnTraditionalChinese, e.Item);

        }

        private void btnBackFileBottomFigure_ItemClick(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnBackFileBottomFigure, e.Item);

        }

        private void ckMetricUnit_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnMetricUnit, e.Item);

        }

        private void ckImperailUnit_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            Messenger.Instance.Send(MainEvent.OnImperailUnit, e.Item);

        }


        #endregion

        #endregion

        #region 主窗体事件
        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If engine is busy, then tip
            var viewModel = this.mvvmContext1.GetViewModel<ViewModels.MainViewModel>();
            if (viewModel.OperStatus != OperationStatus.Idle)
            {
                XtraMessageBox.Show("系统正在运行中, 请先停止！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                return;
            }

            //If project file is changed, then tip

            //保存配置信息
            SerializeUtil.JsonWriteToFile(GlobalModel.Params, GlobalModel.ConfigFileName);
        }

        private void MainView_Load(object sender, System.EventArgs e)
        {
            if (!mvvmContext1.IsDesignMode)
            {
                InitializeBindings();
            }
            this.Text = string.Format(TITLE_FORMAT, FileManager.DEFAULT_FILE_NAME, SystemContext.IsDummyMode ? "(演示版)" : null);
            FileManager.Instance.OnFileNameChanged += (fileName) => 
            {
                this.Text = string.Format(TITLE_FORMAT, fileName, SystemContext.IsDummyMode ? "(演示版)" : null);
            };
            if (!string.IsNullOrEmpty(this.ProjectFilePath))
            {
                FileManager.Instance.OpenFile(this.ProjectFilePath, this.canvasWrapper1.GetDrawCanvas(), true);
            }
            //DrawObjectSelectedChange(true, 2);
        }

        private void MainView_DragDrop(object sender, DragEventArgs e)
        {
            object[] file = (object[])e.Data.GetData(DataFormats.FileDrop);
            if (file != null && file.Length > 0)
            {
                FileManager.Instance.OpenFile(file[0].ToString(), this.canvasWrapper1.GetDrawCanvas(), true);
            }
        }

        private void MainView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))    //判断拖来的是否是文件
            {
                object[] file = (object[])e.Data.GetData(DataFormats.FileDrop);
                if (file != null && file.Length > 0)
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(file[0].ToString());
                    if ((info.Attributes & System.IO.FileAttributes.Directory) == 0)
                    {
                        string exFileName = Path.GetExtension(file[0].ToString());
                        if (exFileName.ToUpper().Equals(".WXF") || exFileName.ToUpper().Equals(".DXF"))
                        {
                            e.Effect = DragDropEffects.All;                //是则将拖动源中的数据连接到控件
                        }
                    }
                }
            }
            else e.Effect = DragDropEffects.None;
        }
        #endregion

        #region 根据图形是否选中改变菜单状态
        private void DrawObjectSelectedChange(object selectCount)
        {
            bool singleEnable, multiEnable;
            int count = (int)selectCount;
            singleEnable = count > 0 ? true : false;
            multiEnable = count > 1 ? true : false;
            this.btnBatchChange.Enabled = singleEnable;
            this.btnCut.Enabled = singleEnable;
            this.btnCopy.Enabled = singleEnable;
            this.btnCopyBasePoint.Enabled = singleEnable;
            this.btnPaste.Enabled = singleEnable;
            this.btnDelete.Enabled = singleEnable;

            this.btnFigureSizeItem.Enabled = singleEnable;
            this.btnFigureTransformItem.Enabled = singleEnable;
            this.btnClearItem.Enabled = singleEnable;

            this.btnGapCompensation.Enabled = singleEnable;
            this.btnInnerCut.Enabled = singleEnable;
            this.btnOuterCut.Enabled = singleEnable;

            this.btnReverseItem.Enabled = singleEnable;
            this.btnSealingItem.Enabled = singleEnable;

            this.btnArrayLayoutItem.Enabled = singleEnable;
            this.btnFlyingCutItem.Enabled = multiEnable;
            this.btnCommonSideItem.Enabled = multiEnable;

            this.btnCurveSmoothing.Enabled = singleEnable;
            this.btnDeleteRepeatLine.Enabled = singleEnable;
            this.btnMergeConnectLine.Enabled = singleEnable;
        }

        private void DrawObjectSelectedIsSingleGroupChange(object isSingGroup)
        {
            bool sigleGroup = (bool)isSingGroup;
            if (sigleGroup == false)
            {
                this.btnGroupItem.ImageOptions.Image = Properties.Resources.group32;
                this.btnGroupItem.ImageOptions.LargeImage = Properties.Resources.group32;
                this.btnGroupItem.Tag = 1;
                this.btnGroupItem.Caption = "群组";
            }
            else
            {
                this.btnGroupItem.ImageOptions.Image = Properties.Resources.groupscatter32;
                this.btnGroupItem.ImageOptions.LargeImage = Properties.Resources.groupscatter32;
                this.btnGroupItem.Tag = 0;
                this.btnGroupItem.Caption = "打散";
            }
        }

        #endregion

        #region 数字输入
        private void MainView_KeyPress(object sender, KeyPressEventArgs e)
        {                   
            var all = UITreeHelper.GetControls(this.ActiveControl);
            var item = all.Find(x => x.Focused);         
            if (item != null && (item is TextBox || item is RichTextBox))
            {
                return;
            }

            char s = e.KeyChar;
            int i = e.KeyChar;
            if ((i >= 97 && i <= 122) || (i >= 65 && i <= 90) || (i >= 48 && i <= 57) || (i >= 45 && i <= 46))//字母 数字 小数点 减号
            {
                ucLogDetail1.FocusWindowsToShowDrawMsg(sender, s);
            }
        }
        #endregion

        
    }
}
