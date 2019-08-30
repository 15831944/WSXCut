
using DevExpress.XtraBars;

namespace WSX.WSXCut
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.appMenu = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.btnNewFile = new DevExpress.XtraBars.BarButtonItem();
            this.btnOpenFile = new DevExpress.XtraBars.BarButtonItem();
            this.btnSaveFile = new DevExpress.XtraBars.BarButtonItem();
            this.btnSaveAsItem = new DevExpress.XtraBars.BarSubItem();
            this.btnSaveFileWXF = new DevExpress.XtraBars.BarButtonItem();
            this.btnSaveFileAutoCADDXF = new DevExpress.XtraBars.BarButtonItem();
            this.btnReportItem = new DevExpress.XtraBars.BarSubItem();
            this.btnProcessingReport = new DevExpress.XtraBars.BarButtonItem();
            this.btnLayoutReport = new DevExpress.XtraBars.BarButtonItem();
            this.btnUseReport = new DevExpress.XtraBars.BarButtonItem();
            this.btnUserSetItem = new DevExpress.XtraBars.BarSubItem();
            this.btnSetUserParam = new DevExpress.XtraBars.BarButtonItem();
            this.btnBackUpParam = new DevExpress.XtraBars.BarButtonItem();
            this.btnDiagnoseToolsItem = new DevExpress.XtraBars.BarSubItem();
            this.btnControlCardMonitor = new DevExpress.XtraBars.BarButtonItem();
            this.btnExtendIOMonitor = new DevExpress.XtraBars.BarButtonItem();
            this.btnGasDACorrection = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCS100Monitor = new DevExpress.XtraBars.BarButtonItem();
            this.btnBurnTest = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnSelectItem = new DevExpress.XtraBars.BarSubItem();
            this.btnSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.btnInvertSelect = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelSelect = new DevExpress.XtraBars.BarButtonItem();
            this.btnBatchChange = new DevExpress.XtraBars.BarButtonItem();
            this.ckBanFastDragCopy = new DevExpress.XtraBars.BarCheckItem();
            this.btnSelectGapFigure = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectSimilarFigure = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectAllExternalModes = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectAllInternalModes = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectAllFigureSmaller = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectFigureLayerItem = new DevExpress.XtraBars.BarSubItem();
            this.btnSelectLayerCraft0 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft2 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft3 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft4 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft5 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft6 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft7 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft8 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft9 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft10 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft11 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft12 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft13 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft14 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft15 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectLayerCraft16 = new DevExpress.XtraBars.BarButtonItem();
            this.btnLockSelectBackground = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectTypeItem = new DevExpress.XtraBars.BarSubItem();
            this.btnSelectAllMultiLines = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectAllCircles = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelectAllBezierCurves = new DevExpress.XtraBars.BarButtonItem();
            this.btnCut = new DevExpress.XtraBars.BarButtonItem();
            this.btnCopy = new DevExpress.XtraBars.BarButtonItem();
            this.btnCopyBasePoint = new DevExpress.XtraBars.BarButtonItem();
            this.btnPaste = new DevExpress.XtraBars.BarButtonItem();
            this.btnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.btnDisplayItem = new DevExpress.XtraBars.BarSubItem();
            this.ckDisplayGapFigureFrame = new DevExpress.XtraBars.BarCheckItem();
            this.ckRedDisplayGapFigure = new DevExpress.XtraBars.BarCheckItem();
            this.ckDisplaySerialNumber = new DevExpress.XtraBars.BarCheckItem();
            this.ckDispalyPathStartPoint = new DevExpress.XtraBars.BarCheckItem();
            this.ckDisplayProcessPath = new DevExpress.XtraBars.BarCheckItem();
            this.ckDisplayEmptyMovePath = new DevExpress.XtraBars.BarCheckItem();
            this.ckDisplayMicroHyphen = new DevExpress.XtraBars.BarCheckItem();
            this.btnFigureCenter = new DevExpress.XtraBars.BarButtonItem();
            this.btnFigureTransformItem = new DevExpress.XtraBars.BarSubItem();
            this.btnTranslation = new DevExpress.XtraBars.BarButtonItem();
            this.btnInteractiveZoom = new DevExpress.XtraBars.BarButtonItem();
            this.btnAlignItem = new DevExpress.XtraBars.BarSubItem();
            this.btnAlignLeft = new DevExpress.XtraBars.BarButtonItem();
            this.btnAlignRight = new DevExpress.XtraBars.BarButtonItem();
            this.btnAlignHorizontalCenter = new DevExpress.XtraBars.BarButtonItem();
            this.btnAlignTop = new DevExpress.XtraBars.BarButtonItem();
            this.btnAlignBottom = new DevExpress.XtraBars.BarButtonItem();
            this.btnAlignVerticalCenter = new DevExpress.XtraBars.BarButtonItem();
            this.btnAlignCenter = new DevExpress.XtraBars.BarButtonItem();
            this.btnMirrorHorizontal = new DevExpress.XtraBars.BarButtonItem();
            this.btnMirrorVertical = new DevExpress.XtraBars.BarButtonItem();
            this.btnMirrorAnyAngle = new DevExpress.XtraBars.BarButtonItem();
            this.btnAnticlockwise90 = new DevExpress.XtraBars.BarButtonItem();
            this.btnClockwise90 = new DevExpress.XtraBars.BarButtonItem();
            this.btnRotate180 = new DevExpress.XtraBars.BarButtonItem();
            this.btnRotateAnyAngle = new DevExpress.XtraBars.BarButtonItem();
            this.btnClearItem = new DevExpress.XtraBars.BarSubItem();
            this.btnClearLeadInOrOutWire = new DevExpress.XtraBars.BarButtonItem();
            this.btnClearMicroConnect = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancelGapCompensation = new DevExpress.XtraBars.BarButtonItem();
            this.btnStartPoint = new DevExpress.XtraBars.BarButtonItem();
            this.btnDockPosition = new DevExpress.XtraBars.BarButtonItem();
            this.btnGapCompensation = new DevExpress.XtraBars.BarButtonItem();
            this.btnOuterCut = new DevExpress.XtraBars.BarButtonItem();
            this.btnInnerCut = new DevExpress.XtraBars.BarButtonItem();
            this.btnSurroundCut = new DevExpress.XtraBars.BarButtonItem();
            this.btnMicroConnectItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnMicroConnectionAuto = new DevExpress.XtraBars.BarButtonItem();
            this.btnMicroConnectBlowOpen = new DevExpress.XtraBars.BarButtonItem();
            this.btnReverseItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnReverse = new DevExpress.XtraBars.BarButtonItem();
            this.btnClockwise = new DevExpress.XtraBars.BarButtonItem();
            this.btnAnticlockwise = new DevExpress.XtraBars.BarButtonItem();
            this.btnSealingItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnSealing = new DevExpress.XtraBars.BarButtonItem();
            this.btnGaping = new DevExpress.XtraBars.BarButtonItem();
            this.btnCutOver = new DevExpress.XtraBars.BarButtonItem();
            this.btnMutilGaps = new DevExpress.XtraBars.BarButtonItem();
            this.btnSortItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.ckSortGrid = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortShortestMove = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortKnife = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortSmallFigurePriority = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortInsideToOut = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortLeftToRight = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortRightToLeft = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortTopToBottom = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortBottomToTop = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortClockwise = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortAnticlockwise = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortProhibitChangDirection = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortDistinguishInsideOutside = new DevExpress.XtraBars.BarCheckItem();
            this.ckSortShadeCutOutermost = new DevExpress.XtraBars.BarCheckItem();
            this.btnArrayLayoutItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnArrayLayoutRectangle = new DevExpress.XtraBars.BarButtonItem();
            this.btnArrayLayoutInteractive = new DevExpress.XtraBars.BarButtonItem();
            this.btnArrayLayoutAnnular = new DevExpress.XtraBars.BarButtonItem();
            this.btnArrayLayoutFull = new DevExpress.XtraBars.BarButtonItem();
            this.btnFlyingCutItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnFlyingCutLine = new DevExpress.XtraBars.BarButtonItem();
            this.btnFlyingCutArc = new DevExpress.XtraBars.BarButtonItem();
            this.btnCommonSideItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnCommonSideTypeC = new DevExpress.XtraBars.BarButtonItem();
            this.btnBridging = new DevExpress.XtraBars.BarButtonItem();
            this.btnMeasure = new DevExpress.XtraBars.BarButtonItem();
            this.btnMajorizationItem = new DevExpress.XtraBars.BarSubItem();
            this.btnCurveSmoothing = new DevExpress.XtraBars.BarButtonItem();
            this.btnCurveSegment = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteRepeatLine = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteSmallFigure = new DevExpress.XtraBars.BarButtonItem();
            this.btnMergeConnectLine = new DevExpress.XtraBars.BarButtonItem();
            this.btnCutUp = new DevExpress.XtraBars.BarButtonItem();
            this.btnConnectKnife = new DevExpress.XtraBars.BarButtonItem();
            this.btnCraftItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnShowFigureLayerItem = new DevExpress.XtraBars.BarSubItem();
            this.btnShowBackgroundCarft = new DevExpress.XtraBars.BarButtonItem();
            this.ckShowCarft1 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft2 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft3 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft4 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft5 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft6 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft7 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft8 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft9 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft10 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft11 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft12 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft13 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft14 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft15 = new DevExpress.XtraBars.BarCheckItem();
            this.ckShowCarft16 = new DevExpress.XtraBars.BarCheckItem();
            this.btnOnlyShowFigureLayerItem = new DevExpress.XtraBars.BarSubItem();
            this.btnOnlyShowCarftALL = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft2 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft3 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft4 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft5 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft6 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft7 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft8 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft9 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft10 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft11 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft12 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft13 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft14 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft15 = new DevExpress.XtraBars.BarButtonItem();
            this.btnOnlyShowCarft16 = new DevExpress.XtraBars.BarButtonItem();
            this.btnLockFigureLayerItem = new DevExpress.XtraBars.BarSubItem();
            this.ckLockCarftBackground = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft1 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft2 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft3 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft4 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft5 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft6 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft7 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft8 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft9 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft10 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft11 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft12 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft13 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft14 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft15 = new DevExpress.XtraBars.BarCheckItem();
            this.ckLockCarft16 = new DevExpress.XtraBars.BarCheckItem();
            this.btnDXFFigureMap = new DevExpress.XtraBars.BarButtonItem();
            this.btnRectangleItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnRectangle = new DevExpress.XtraBars.BarButtonItem();
            this.btnFilletRectangle = new DevExpress.XtraBars.BarButtonItem();
            this.btnTrackRectangle = new DevExpress.XtraBars.BarButtonItem();
            this.btnCircleItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnCircle = new DevExpress.XtraBars.BarButtonItem();
            this.btnThreePointArc = new DevExpress.XtraBars.BarButtonItem();
            this.btnScanArc = new DevExpress.XtraBars.BarButtonItem();
            this.btnNewEllipse = new DevExpress.XtraBars.BarButtonItem();
            this.btnCircleReplaceToAcnode = new DevExpress.XtraBars.BarButtonItem();
            this.btnReplaceToCircle = new DevExpress.XtraBars.BarButtonItem();
            this.btnMultiLineItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnMultiLine = new DevExpress.XtraBars.BarButtonItem();
            this.btnPolygon = new DevExpress.XtraBars.BarButtonItem();
            this.btnStellate = new DevExpress.XtraBars.BarButtonItem();
            this.btnDot = new DevExpress.XtraBars.BarButtonItem();
            this.btnWord = new DevExpress.XtraBars.BarButtonItem();
            this.btnGroupItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnGroupSelectAll = new DevExpress.XtraBars.BarButtonItem();
            this.btnGroupScatterSelected = new DevExpress.XtraBars.BarButtonItem();
            this.btnGroupScatterAll = new DevExpress.XtraBars.BarButtonItem();
            this.btnGroupBlowOpen = new DevExpress.XtraBars.BarButtonItem();
            this.btnMultiContourCotangent = new DevExpress.XtraBars.BarButtonItem();
            this.btnBlowOpenMultiContourCotangent = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.btnFindEdgeItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.ckCapacitanceEdge = new DevExpress.XtraBars.BarCheckItem();
            this.ckDotEdge = new DevExpress.XtraBars.BarCheckItem();
            this.ckManualEdge = new DevExpress.XtraBars.BarCheckItem();
            this.ckAutoClearEdgeProcessed = new DevExpress.XtraBars.BarCheckItem();
            this.ckClearEdgeAngle = new DevExpress.XtraBars.BarCheckItem();
            this.btnPLCProcessItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnEditPLCProcess = new DevExpress.XtraBars.BarButtonItem();
            this.btnExecutePLCProcess = new DevExpress.XtraBars.BarSubItem();
            this.btnChangeToWorkbenchA = new DevExpress.XtraBars.BarButtonItem();
            this.btnChangeToWorkbenchB = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess2 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess3 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess4 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess5 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess6 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess7 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess8 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess9 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess10 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess11 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess12 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess13 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess14 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess15 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess16 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess17 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess18 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess19 = new DevExpress.XtraBars.BarButtonItem();
            this.btnCustomProcess20 = new DevExpress.XtraBars.BarButtonItem();
            this.btnGoOriginItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnGoOriginAll = new DevExpress.XtraBars.BarButtonItem();
            this.btnGoOriginX = new DevExpress.XtraBars.BarButtonItem();
            this.btnGoOriginY = new DevExpress.XtraBars.BarButtonItem();
            this.btnGantryInit = new DevExpress.XtraBars.BarButtonItem();
            this.ckExecuteGantrySyncGoOrigin = new DevExpress.XtraBars.BarCheckItem();
            this.ckAdjustHeightenGoOrigin = new DevExpress.XtraBars.BarCheckItem();
            this.ckElectricFocusgoOrigin = new DevExpress.XtraBars.BarCheckItem();
            this.btnBCS100Item = new DevExpress.XtraBars.BarSubItem();
            this.btnBCSStop = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSGoBerth = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSGoOrigin = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowItem = new DevExpress.XtraBars.BarSubItem();
            this.btnBCSFollow1mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollow2mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollow3mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollow4mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollow5mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollow6mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollow7mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollow8mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollow9mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollow10mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStopItem = new DevExpress.XtraBars.BarSubItem();
            this.btnBCSFollowStop1mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop2mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop3mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop4mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop5mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop6mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop7mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop8mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop9mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop10mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop15mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop20mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop25mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSFollowStop30mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelativeItem = new DevExpress.XtraBars.BarSubItem();
            this.btnEmptyMoveRelative1mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelative2mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelative3mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelative4mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelative5mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelative6mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelative7mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelative8mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelative9mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelative10mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelativeMinus1mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelativeMinus2mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelativeMinus3mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelativeMinus4mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelativeMinus5mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelativeMinus6mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelativeMinus7mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelativeMinus8mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelativeMinus9mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnEmptyMoveRelativeMinus10mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionItem = new DevExpress.XtraBars.BarSubItem();
            this.btnMoveAbsolutePositionZ0 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ1 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ2 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ3 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ4 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ5 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ6 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ7 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ8 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ9 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ10 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ15 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ20 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ25 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ30 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ40 = new DevExpress.XtraBars.BarButtonItem();
            this.btnMoveAbsolutePositionZ50 = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSKeyCalibration = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSRegisteredEncrypt = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSSaveParams = new DevExpress.XtraBars.BarButtonItem();
            this.btnBCSRecordZCoordinate = new DevExpress.XtraBars.BarButtonItem();
            this.btnErrorMeasure = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelect = new DevExpress.XtraBars.BarButtonItem();
            this.btnEditNode = new DevExpress.XtraBars.BarButtonItem();
            this.btnTranslateView = new DevExpress.XtraBars.BarButtonItem();
            this.btnSortMode = new DevExpress.XtraBars.BarButtonItem();
            this.btnLanguageItem = new DevExpress.XtraBars.BarSubItem();
            this.ckEnglish = new DevExpress.XtraBars.BarCheckItem();
            this.ckSimplifiedChinese = new DevExpress.XtraBars.BarCheckItem();
            this.ckTraditionalChinese = new DevExpress.XtraBars.BarCheckItem();
            this.btnBackFileBottomFigure = new DevExpress.XtraBars.BarButtonItem();
            this.btnSystemUnitItem = new DevExpress.XtraBars.BarSubItem();
            this.ckMetricUnit = new DevExpress.XtraBars.BarCheckItem();
            this.ckImperailUnit = new DevExpress.XtraBars.BarCheckItem();
            this.btnPartItem = new DevExpress.XtraBars.BarSubItem();
            this.btnImportPart = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportStandardPart = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteAllLayoutPart = new DevExpress.XtraBars.BarButtonItem();
            this.btnSheetMaterialItem = new DevExpress.XtraBars.BarSubItem();
            this.btnSetSheetMaterial = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteAllSheetMaterial = new DevExpress.XtraBars.BarButtonItem();
            this.barDockingMenuItem1 = new DevExpress.XtraBars.BarDockingMenuItem();
            this.btnLayoutItem = new DevExpress.XtraBars.BarSubItem();
            this.btnImportItem = new DevExpress.XtraBars.BarSubItem();
            this.btnImportFileLXD = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportFileDXF = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportFilePLT = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportFileAI = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportFileGerBar = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportFileNC = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem39 = new DevExpress.XtraBars.BarSubItem();
            this.btnLeadInOrOutWireItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnCheckLeadInOrOutWire = new DevExpress.XtraBars.BarButtonItem();
            this.btnLeadInOrOutWireDifferentiateMode = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem65 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem66 = new DevExpress.XtraBars.BarButtonItem();
            this.btnFigureSizeItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnFigureSize100mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnFigureSize200mm = new DevExpress.XtraBars.BarButtonItem();
            this.btnFigureSize0_5 = new DevExpress.XtraBars.BarButtonItem();
            this.btnFigureSize2 = new DevExpress.XtraBars.BarButtonItem();
            this.btnFigureSize4 = new DevExpress.XtraBars.BarButtonItem();
            this.btnFigureSize8 = new DevExpress.XtraBars.BarButtonItem();
            this.btnFigureSize10 = new DevExpress.XtraBars.BarButtonItem();
            this.btnReleaseAngle = new DevExpress.XtraBars.BarButtonItem();
            this.btnRoundAngle = new DevExpress.XtraBars.BarButtonItem();
            this.btnCoolingDotItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnCoolingDotAuto = new DevExpress.XtraBars.BarButtonItem();
            this.btnCoolingDotClear = new DevExpress.XtraBars.BarButtonItem();
            this.btnFrontMost = new DevExpress.XtraBars.BarButtonItem();
            this.btnBackMost = new DevExpress.XtraBars.BarButtonItem();
            this.btnForward = new DevExpress.XtraBars.BarButtonItem();
            this.btnBackward = new DevExpress.XtraBars.BarButtonItem();
            this.btnPreviousTarget = new DevExpress.XtraBars.BarButtonItem();
            this.btnNextTarget = new DevExpress.XtraBars.BarButtonItem();
            this.btnStockLayoutItem = new WSX.WSXCut.Views.CustomControl.Menu.ComboHeadBarSubItem();
            this.btnStockLayoutClear = new DevExpress.XtraBars.BarButtonItem();
            this.btnProcessTaskItem = new DevExpress.XtraBars.BarSubItem();
            this.btnSaveProcessTask = new DevExpress.XtraBars.BarButtonItem();
            this.btnLoadProcessTask = new DevExpress.XtraBars.BarButtonItem();
            this.btnLightPathDebug = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem266 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem275 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem279 = new DevExpress.XtraBars.BarButtonItem();
            this.btnQCW = new DevExpress.XtraBars.BarButtonItem();
            this.btnGantryErrorMonitor = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonGroup4 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup5 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup6 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup11 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup7 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup8 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup9 = new DevExpress.XtraBars.BarButtonGroup();
            this.btnSortRangeTagets = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTrackBar5 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.ribPageCommon = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribPageGroupView = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribPageGroupTransform = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribPageGroupProcessSet = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribPageGroupTool1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribPageGroupParam = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribPageLayout = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribPageGroupLayout = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribPageCNC = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribPageGroupProcess = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribPageGroupTool2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemPopupContainerEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit();
            this.repositoryItemRangeTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemRangeTrackBar();
            this.repositoryItemTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.repositoryItemTrackBar2 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.repositoryItemTrackBar3 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.repositoryItemTrackBar4 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitMiddle = new DevExpress.XtraEditors.SplitContainerControl();
            this.canvasWrapper1 = new WSX.WSXCut.Views.CustomControl.Draw.CanvasWrapper();
            this.ucLogDetail1 = new WSX.WSXCut.Views.CustomControl.UCLogDetail();
            this.barButtonItem26 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem3 = new DevExpress.XtraBars.BarEditItem();
            this.ucMachineControl1 = new WSX.WSXCut.Views.CustomControl.RightPanel.UCMachineControl();
            this.panelRightMain = new System.Windows.Forms.Panel();
            this.ucLayerParaBar1 = new WSX.WSXCut.Views.CustomControl.Operation.UCLayerParaBar();
            this.ucMannual1 = new WSX.WSXCut.Views.CustomControl.RightPanel.UCMannual();
            this.ucMachineCount1 = new WSX.WSXCut.Views.CustomControl.RightPanel.UCMachineCount();
            this.panelMain = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRangeTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMiddle)).BeginInit();
            this.splitMiddle.SuspendLayout();
            this.panelRightMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).BeginInit();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(WSX.ViewModels.MainViewModel);
            // 
            // appMenu
            // 
            this.appMenu.ItemLinks.Add(this.btnNewFile);
            this.appMenu.ItemLinks.Add(this.btnOpenFile);
            this.appMenu.ItemLinks.Add(this.btnSaveFile);
            this.appMenu.ItemLinks.Add(this.btnSaveAsItem);
            this.appMenu.ItemLinks.Add(this.btnReportItem, true);
            this.appMenu.ItemLinks.Add(this.btnUserSetItem);
            this.appMenu.ItemLinks.Add(this.btnBackUpParam, true);
            this.appMenu.ItemLinks.Add(this.btnDiagnoseToolsItem);
            this.appMenu.Name = "appMenu";
            this.appMenu.Ribbon = this.ribbonControl1;
            this.appMenu.RightPaneWidth = 280;
            this.appMenu.ShowRightPane = true;
            // 
            // btnNewFile
            // 
            this.btnNewFile.Caption = "新建";
            this.btnNewFile.Id = 159;
            this.btnNewFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNewFile.ImageOptions.Image")));
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNewFile_ItemClick);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Caption = "打开";
            this.btnOpenFile.Id = 343;
            this.btnOpenFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFile.ImageOptions.Image")));
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOpenFile_ItemClick);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Caption = "保存";
            this.btnSaveFile.Id = 160;
            this.btnSaveFile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveFile.ImageOptions.Image")));
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveFile_ItemClick);
            // 
            // btnSaveAsItem
            // 
            this.btnSaveAsItem.Caption = "另存为";
            this.btnSaveAsItem.Id = 162;
            this.btnSaveAsItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAsItem.ImageOptions.Image")));
            this.btnSaveAsItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveFileWXF),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveFileAutoCADDXF)});
            this.btnSaveAsItem.Name = "btnSaveAsItem";
            // 
            // btnSaveFileWXF
            // 
            this.btnSaveFileWXF.Caption = "WXF格式（默认）";
            this.btnSaveFileWXF.Description = "WsxCut原生文件格式（默认）";
            this.btnSaveFileWXF.Id = 163;
            this.btnSaveFileWXF.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveFileWXF.ImageOptions.Image")));
            this.btnSaveFileWXF.Name = "btnSaveFileWXF";
            this.btnSaveFileWXF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveFileWXF_ItemClick);
            // 
            // btnSaveFileAutoCADDXF
            // 
            this.btnSaveFileAutoCADDXF.Caption = "AutoCAD DXF格式";
            this.btnSaveFileAutoCADDXF.Description = "保存为DXF可供其它软件打开";
            this.btnSaveFileAutoCADDXF.Id = 163;
            this.btnSaveFileAutoCADDXF.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveFileAutoCADDXF.ImageOptions.Image")));
            this.btnSaveFileAutoCADDXF.Name = "btnSaveFileAutoCADDXF";
            this.btnSaveFileAutoCADDXF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveFileAutoCADDXF_ItemClick);
            // 
            // btnReportItem
            // 
            this.btnReportItem.Caption = "报告";
            this.btnReportItem.Id = 5;
            this.btnReportItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnReportItem.ImageOptions.Image")));
            this.btnReportItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnProcessingReport),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLayoutReport),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnUseReport)});
            this.btnReportItem.Name = "btnReportItem";
            // 
            // btnProcessingReport
            // 
            this.btnProcessingReport.Caption = "加工报告单";
            this.btnProcessingReport.Description = "打印当前显示的图形的加工报告";
            this.btnProcessingReport.Id = 18;
            this.btnProcessingReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnProcessingReport.ImageOptions.Image")));
            this.btnProcessingReport.Name = "btnProcessingReport";
            this.btnProcessingReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnProcessingReport_ItemClick);
            // 
            // btnLayoutReport
            // 
            this.btnLayoutReport.Caption = "排样报告";
            this.btnLayoutReport.Description = "排样结果的完整报告";
            this.btnLayoutReport.Id = 19;
            this.btnLayoutReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLayoutReport.ImageOptions.Image")));
            this.btnLayoutReport.Name = "btnLayoutReport";
            this.btnLayoutReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLayoutReport_ItemClick);
            // 
            // btnUseReport
            // 
            this.btnUseReport.Caption = "使用报告";
            this.btnUseReport.Description = "对一段时间内机床的使用情况进行统计分析";
            this.btnUseReport.Id = 20;
            this.btnUseReport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUseReport.ImageOptions.Image")));
            this.btnUseReport.Name = "btnUseReport";
            this.btnUseReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnUseReport_ItemClick);
            // 
            // btnUserSetItem
            // 
            this.btnUserSetItem.Caption = "用户设置";
            this.btnUserSetItem.Id = 6;
            this.btnUserSetItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUserSetItem.ImageOptions.Image")));
            this.btnUserSetItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSetUserParam)});
            this.btnUserSetItem.Name = "btnUserSetItem";
            // 
            // btnSetUserParam
            // 
            this.btnSetUserParam.Caption = "用户参数";
            this.btnSetUserParam.Description = "设置与您的操作习惯有关的参数,包括自动优化和排样参数";
            this.btnSetUserParam.Id = 21;
            this.btnSetUserParam.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSetUserParam.ImageOptions.Image")));
            this.btnSetUserParam.Name = "btnSetUserParam";
            this.btnSetUserParam.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSetUserParam_ItemClick);
            // 
            // btnBackUpParam
            // 
            this.btnBackUpParam.Caption = "参数备份";
            this.btnBackUpParam.Id = 7;
            this.btnBackUpParam.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBackUpParam.ImageOptions.Image")));
            this.btnBackUpParam.Name = "btnBackUpParam";
            this.btnBackUpParam.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBackUpParam_ItemClick);
            // 
            // btnDiagnoseToolsItem
            // 
            this.btnDiagnoseToolsItem.Caption = "诊断工具";
            this.btnDiagnoseToolsItem.Id = 8;
            this.btnDiagnoseToolsItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDiagnoseToolsItem.ImageOptions.Image")));
            this.btnDiagnoseToolsItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnControlCardMonitor, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExtendIOMonitor),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGasDACorrection),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCS100Monitor),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBurnTest)});
            this.btnDiagnoseToolsItem.Name = "btnDiagnoseToolsItem";
            // 
            // btnControlCardMonitor
            // 
            this.btnControlCardMonitor.Caption = "控制卡监控";
            this.btnControlCardMonitor.Description = "显示运动控制卡及转接板信息,允许监控和调试";
            this.btnControlCardMonitor.Id = 22;
            this.btnControlCardMonitor.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnControlCardMonitor.ImageOptions.Image")));
            this.btnControlCardMonitor.Name = "btnControlCardMonitor";
            this.btnControlCardMonitor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnControlCardMonitor_ItemClick);
            // 
            // btnExtendIOMonitor
            // 
            this.btnExtendIOMonitor.Caption = "扩展IO板监控";
            this.btnExtendIOMonitor.Description = "显示扩展IO板的状态，允许监控和调试";
            this.btnExtendIOMonitor.Id = 23;
            this.btnExtendIOMonitor.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExtendIOMonitor.ImageOptions.Image")));
            this.btnExtendIOMonitor.Name = "btnExtendIOMonitor";
            this.btnExtendIOMonitor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExtendIOMonitor_ItemClick);
            // 
            // btnGasDACorrection
            // 
            this.btnGasDACorrection.Caption = "气体DA校正";
            this.btnGasDACorrection.Id = 24;
            this.btnGasDACorrection.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGasDACorrection.ImageOptions.Image")));
            this.btnGasDACorrection.Name = "btnGasDACorrection";
            this.btnGasDACorrection.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGasDACorrection_ItemClick);
            // 
            // btnBCS100Monitor
            // 
            this.btnBCS100Monitor.Caption = "NT100监控";
            this.btnBCS100Monitor.Id = 25;
            this.btnBCS100Monitor.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBCS100Monitor.ImageOptions.Image")));
            this.btnBCS100Monitor.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBCS100Monitor.ImageOptions.LargeImage")));
            this.btnBCS100Monitor.Name = "btnBCS100Monitor";
            this.btnBCS100Monitor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCS100Monitor_ItemClick);
            // 
            // btnBurnTest
            // 
            this.btnBurnTest.Caption = "拷机测试";
            this.btnBurnTest.Id = 26;
            this.btnBurnTest.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBurnTest.ImageOptions.Image")));
            this.btnBurnTest.Name = "btnBurnTest";
            this.btnBurnTest.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBurnTest_ItemClick);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonDropDownControl = this.appMenu;
            this.ribbonControl1.ApplicationButtonText = "文件";
            this.ribbonControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btnSelectItem,
            this.btnDisplayItem,
            this.btnFigureTransformItem,
            this.btnClearItem,
            this.btnStartPoint,
            this.btnDockPosition,
            this.btnGapCompensation,
            this.btnOuterCut,
            this.btnInnerCut,
            this.btnSurroundCut,
            this.btnMicroConnectItem,
            this.btnReverseItem,
            this.btnSealingItem,
            this.btnSortItem,
            this.btnArrayLayoutItem,
            this.btnFlyingCutItem,
            this.btnCommonSideItem,
            this.btnBridging,
            this.btnMeasure,
            this.btnMajorizationItem,
            this.btnCraftItem,
            this.btnRectangleItem,
            this.btnCircleItem,
            this.btnMultiLineItem,
            this.btnDot,
            this.btnWord,
            this.btnGroupItem,
            this.barEditItem1,
            this.btnFindEdgeItem,
            this.btnPLCProcessItem,
            this.btnGoOriginItem,
            this.btnBCS100Item,
            this.btnErrorMeasure,
            this.btnSelect,
            this.btnEditNode,
            this.btnTranslateView,
            this.btnSortMode,
            this.btnSelectAll,
            this.btnCancelSelect,
            this.btnBatchChange,
            this.ckBanFastDragCopy,
            this.btnSelectGapFigure,
            this.btnSelectSimilarFigure,
            this.btnSelectAllExternalModes,
            this.btnSelectAllInternalModes,
            this.btnSelectAllFigureSmaller,
            this.btnSelectFigureLayerItem,
            this.btnLockSelectBackground,
            this.btnSelectTypeItem,
            this.btnCut,
            this.btnCopy,
            this.btnCopyBasePoint,
            this.btnPaste,
            this.btnDelete,
            this.btnSelectAllMultiLines,
            this.btnSelectAllCircles,
            this.btnSelectAllBezierCurves,
            this.btnSelectLayerCraft0,
            this.btnSelectLayerCraft1,
            this.btnSelectLayerCraft2,
            this.ckDisplayGapFigureFrame,
            this.ckRedDisplayGapFigure,
            this.ckDisplaySerialNumber,
            this.ckDispalyPathStartPoint,
            this.ckDisplayProcessPath,
            this.ckDisplayEmptyMovePath,
            this.btnFigureCenter,
            this.btnSelectLayerCraft3,
            this.btnSelectLayerCraft4,
            this.btnSelectLayerCraft5,
            this.btnSelectLayerCraft6,
            this.btnSelectLayerCraft7,
            this.btnSelectLayerCraft8,
            this.btnSelectLayerCraft9,
            this.btnSelectLayerCraft10,
            this.btnSelectLayerCraft11,
            this.btnSelectLayerCraft12,
            this.btnSelectLayerCraft13,
            this.btnSelectLayerCraft14,
            this.btnSelectLayerCraft15,
            this.btnSelectLayerCraft16,
            this.btnShowFigureLayerItem,
            this.btnOnlyShowFigureLayerItem,
            this.btnLockFigureLayerItem,
            this.btnLanguageItem,
            this.btnBackFileBottomFigure,
            this.btnSystemUnitItem,
            this.ckMetricUnit,
            this.ckImperailUnit,
            this.ckEnglish,
            this.ckSimplifiedChinese,
            this.btnShowBackgroundCarft,
            this.ckShowCarft1,
            this.ckShowCarft2,
            this.ckShowCarft3,
            this.ckShowCarft4,
            this.ckShowCarft5,
            this.ckShowCarft6,
            this.ckShowCarft7,
            this.ckShowCarft8,
            this.ckShowCarft9,
            this.ckShowCarft10,
            this.ckShowCarft11,
            this.ckShowCarft12,
            this.ckShowCarft13,
            this.ckShowCarft14,
            this.ckShowCarft15,
            this.ckShowCarft16,
            this.btnOnlyShowCarftALL,
            this.btnOnlyShowCarft1,
            this.btnOnlyShowCarft2,
            this.btnOnlyShowCarft3,
            this.btnOnlyShowCarft4,
            this.btnOnlyShowCarft5,
            this.btnOnlyShowCarft6,
            this.btnOnlyShowCarft7,
            this.btnOnlyShowCarft8,
            this.btnOnlyShowCarft9,
            this.btnOnlyShowCarft10,
            this.btnOnlyShowCarft11,
            this.btnOnlyShowCarft12,
            this.btnOnlyShowCarft13,
            this.btnOnlyShowCarft14,
            this.btnOnlyShowCarft15,
            this.btnOnlyShowCarft16,
            this.ckLockCarftBackground,
            this.ckLockCarft1,
            this.ckLockCarft2,
            this.ckLockCarft3,
            this.ckLockCarft4,
            this.ckLockCarft5,
            this.ckLockCarft6,
            this.ckLockCarft7,
            this.ckLockCarft8,
            this.ckLockCarft9,
            this.ckLockCarft10,
            this.ckLockCarft11,
            this.ckLockCarft12,
            this.ckLockCarft13,
            this.ckLockCarft14,
            this.ckLockCarft15,
            this.ckLockCarft16,
            this.ckTraditionalChinese,
            this.btnPartItem,
            this.btnSheetMaterialItem,
            this.btnDeleteAllLayoutPart,
            this.btnSetSheetMaterial,
            this.btnDeleteAllSheetMaterial,
            this.btnNewFile,
            this.btnSaveFile,
            this.barDockingMenuItem1,
            this.btnSaveAsItem,
            this.btnSaveFileWXF,
            this.btnLayoutItem,
            this.btnImportItem,
            this.btnReportItem,
            this.btnUserSetItem,
            this.btnBackUpParam,
            this.btnDiagnoseToolsItem,
            this.btnSaveFileAutoCADDXF,
            this.btnImportPart,
            this.btnImportStandardPart,
            this.btnImportFileLXD,
            this.btnImportFileDXF,
            this.btnImportFilePLT,
            this.btnImportFileAI,
            this.btnImportFileGerBar,
            this.btnImportFileNC,
            this.btnProcessingReport,
            this.btnLayoutReport,
            this.btnUseReport,
            this.btnSetUserParam,
            this.btnControlCardMonitor,
            this.btnExtendIOMonitor,
            this.btnGasDACorrection,
            this.btnBCS100Monitor,
            this.btnBurnTest,
            this.barSubItem39,
            this.btnLeadInOrOutWireItem,
            this.btnClearLeadInOrOutWire,
            this.btnClearMicroConnect,
            this.btnCancelGapCompensation,
            this.barButtonItem65,
            this.barButtonItem66,
            this.btnFigureSizeItem,
            this.ckDisplayMicroHyphen,
            this.btnTranslation,
            this.btnInteractiveZoom,
            this.btnAlignItem,
            this.btnAlignLeft,
            this.btnAlignRight,
            this.btnAlignHorizontalCenter,
            this.btnAlignBottom,
            this.btnAlignVerticalCenter,
            this.btnAlignCenter,
            this.btnMirrorHorizontal,
            this.btnMirrorVertical,
            this.btnMirrorAnyAngle,
            this.btnAnticlockwise90,
            this.btnClockwise90,
            this.btnRotate180,
            this.btnRotateAnyAngle,
            this.btnReleaseAngle,
            this.btnRoundAngle,
            this.btnCoolingDotItem,
            this.btnFrontMost,
            this.btnBackMost,
            this.btnForward,
            this.btnBackward,
            this.btnPreviousTarget,
            this.btnNextTarget,
            this.btnStockLayoutItem,
            this.btnCurveSmoothing,
            this.btnCurveSegment,
            this.btnDeleteRepeatLine,
            this.btnDeleteSmallFigure,
            this.btnMergeConnectLine,
            this.btnCutUp,
            this.btnConnectKnife,
            this.btnDXFFigureMap,
            this.btnRectangle,
            this.btnFilletRectangle,
            this.btnTrackRectangle,
            this.btnCircle,
            this.btnThreePointArc,
            this.btnScanArc,
            this.btnNewEllipse,
            this.btnCircleReplaceToAcnode,
            this.btnReplaceToCircle,
            this.btnMultiLine,
            this.btnPolygon,
            this.btnStellate,
            this.btnGroupSelectAll,
            this.btnGroupScatterSelected,
            this.btnGroupScatterAll,
            this.btnGroupBlowOpen,
            this.btnMultiContourCotangent,
            this.btnBlowOpenMultiContourCotangent,
            this.btnMicroConnectionAuto,
            this.btnMicroConnectBlowOpen,
            this.btnReverse,
            this.btnClockwise,
            this.btnAnticlockwise,
            this.btnSealing,
            this.btnGaping,
            this.btnCutOver,
            this.btnMutilGaps,
            this.btnCoolingDotAuto,
            this.btnCoolingDotClear,
            this.ckSortGrid,
            this.ckSortShortestMove,
            this.ckSortKnife,
            this.ckSortSmallFigurePriority,
            this.ckSortInsideToOut,
            this.ckSortLeftToRight,
            this.ckSortRightToLeft,
            this.ckSortTopToBottom,
            this.ckSortBottomToTop,
            this.ckSortClockwise,
            this.ckSortAnticlockwise,
            this.ckSortProhibitChangDirection,
            this.ckSortDistinguishInsideOutside,
            this.ckSortShadeCutOutermost,
            this.btnArrayLayoutRectangle,
            this.btnArrayLayoutInteractive,
            this.btnArrayLayoutAnnular,
            this.btnArrayLayoutFull,
            this.btnFlyingCutLine,
            this.btnFlyingCutArc,
            this.btnCommonSideTypeC,
            this.btnFigureSize100mm,
            this.btnFigureSize200mm,
            this.btnFigureSize0_5,
            this.btnFigureSize2,
            this.btnFigureSize4,
            this.btnFigureSize8,
            this.btnFigureSize10,
            this.btnCheckLeadInOrOutWire,
            this.btnLeadInOrOutWireDifferentiateMode,
            this.btnStockLayoutClear,
            this.btnAlignTop,
            this.ckCapacitanceEdge,
            this.ckDotEdge,
            this.ckManualEdge,
            this.ckAutoClearEdgeProcessed,
            this.ckClearEdgeAngle,
            this.btnEditPLCProcess,
            this.btnExecutePLCProcess,
            this.btnChangeToWorkbenchA,
            this.btnChangeToWorkbenchB,
            this.btnCustomProcess1,
            this.btnCustomProcess2,
            this.btnCustomProcess3,
            this.btnCustomProcess4,
            this.btnCustomProcess5,
            this.btnCustomProcess6,
            this.btnCustomProcess7,
            this.btnCustomProcess8,
            this.btnCustomProcess9,
            this.btnCustomProcess10,
            this.btnCustomProcess11,
            this.btnCustomProcess12,
            this.btnCustomProcess13,
            this.btnCustomProcess14,
            this.btnCustomProcess15,
            this.btnCustomProcess16,
            this.btnCustomProcess17,
            this.btnCustomProcess18,
            this.btnCustomProcess19,
            this.btnCustomProcess20,
            this.btnProcessTaskItem,
            this.btnSaveProcessTask,
            this.btnLoadProcessTask,
            this.btnGoOriginAll,
            this.btnGoOriginX,
            this.btnGoOriginY,
            this.btnGantryInit,
            this.ckExecuteGantrySyncGoOrigin,
            this.ckAdjustHeightenGoOrigin,
            this.ckElectricFocusgoOrigin,
            this.btnLightPathDebug,
            this.btnBCSStop,
            this.btnBCSGoBerth,
            this.btnBCSGoOrigin,
            this.btnBCSFollowItem,
            this.btnBCSFollow1mm,
            this.btnBCSFollow2mm,
            this.btnBCSFollow3mm,
            this.btnBCSFollow4mm,
            this.btnBCSFollow5mm,
            this.btnBCSFollow6mm,
            this.btnBCSFollow7mm,
            this.btnBCSFollow8mm,
            this.btnBCSFollow9mm,
            this.btnBCSFollow10mm,
            this.barButtonItem266,
            this.btnBCSFollowStopItem,
            this.btnEmptyMoveRelativeItem,
            this.btnMoveAbsolutePositionItem,
            this.btnBCSKeyCalibration,
            this.btnBCSRegisteredEncrypt,
            this.btnBCSUpdate,
            this.btnBCSSaveParams,
            this.btnBCSRecordZCoordinate,
            this.btnBCSFollowStop1mm,
            this.btnBCSFollowStop2mm,
            this.barButtonItem275,
            this.btnBCSFollowStop3mm,
            this.btnBCSFollowStop4mm,
            this.btnBCSFollowStop5mm,
            this.barButtonItem279,
            this.btnBCSFollowStop6mm,
            this.btnBCSFollowStop7mm,
            this.btnBCSFollowStop8mm,
            this.btnBCSFollowStop9mm,
            this.btnBCSFollowStop10mm,
            this.btnBCSFollowStop15mm,
            this.btnBCSFollowStop20mm,
            this.btnBCSFollowStop25mm,
            this.btnBCSFollowStop30mm,
            this.btnEmptyMoveRelative1mm,
            this.btnEmptyMoveRelative2mm,
            this.btnEmptyMoveRelative3mm,
            this.btnEmptyMoveRelative4mm,
            this.btnEmptyMoveRelative5mm,
            this.btnEmptyMoveRelative6mm,
            this.btnEmptyMoveRelative7mm,
            this.btnEmptyMoveRelative8mm,
            this.btnEmptyMoveRelative9mm,
            this.btnEmptyMoveRelative10mm,
            this.btnEmptyMoveRelativeMinus1mm,
            this.btnEmptyMoveRelativeMinus2mm,
            this.btnEmptyMoveRelativeMinus3mm,
            this.btnEmptyMoveRelativeMinus4mm,
            this.btnEmptyMoveRelativeMinus5mm,
            this.btnEmptyMoveRelativeMinus6mm,
            this.btnEmptyMoveRelativeMinus7mm,
            this.btnEmptyMoveRelativeMinus8mm,
            this.btnEmptyMoveRelativeMinus9mm,
            this.btnEmptyMoveRelativeMinus10mm,
            this.btnMoveAbsolutePositionZ0,
            this.btnMoveAbsolutePositionZ1,
            this.btnMoveAbsolutePositionZ2,
            this.btnMoveAbsolutePositionZ3,
            this.btnMoveAbsolutePositionZ4,
            this.btnMoveAbsolutePositionZ5,
            this.btnMoveAbsolutePositionZ6,
            this.btnMoveAbsolutePositionZ7,
            this.btnMoveAbsolutePositionZ8,
            this.btnMoveAbsolutePositionZ9,
            this.btnMoveAbsolutePositionZ10,
            this.btnMoveAbsolutePositionZ15,
            this.btnMoveAbsolutePositionZ20,
            this.btnMoveAbsolutePositionZ25,
            this.btnMoveAbsolutePositionZ30,
            this.btnMoveAbsolutePositionZ40,
            this.btnMoveAbsolutePositionZ50,
            this.btnQCW,
            this.btnGantryErrorMonitor,
            this.btnInvertSelect,
            this.barButtonGroup4,
            this.barButtonGroup5,
            this.barButtonGroup6,
            this.barButtonGroup11,
            this.barButtonGroup7,
            this.barButtonGroup8,
            this.barButtonGroup9,
            this.btnSortRangeTagets,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.btnOpenFile});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 1;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribPageCommon,
            this.ribPageLayout,
            this.ribPageCNC});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPopupContainerEdit1,
            this.repositoryItemRangeTrackBar1,
            this.repositoryItemTrackBar1,
            this.repositoryItemTrackBar2,
            this.repositoryItemTrackBar3,
            this.repositoryItemTrackBar4,
            this.repositoryItemTrackBar5});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.True;
            this.ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowQatLocationSelector = false;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(1284, 147);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            // 
            // btnSelectItem
            // 
            this.btnSelectItem.Caption = "选择";
            this.btnSelectItem.Id = 10;
            this.btnSelectItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectItem.ImageOptions.Image")));
            this.btnSelectItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnInvertSelect),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelSelect),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBatchChange, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckBanFastDragCopy, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectGapFigure, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectSimilarFigure),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectAllExternalModes),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectAllInternalModes),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectAllFigureSmaller),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectFigureLayerItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLockSelectBackground),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectTypeItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCut, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCopyBasePoint),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPaste),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDelete)});
            this.btnSelectItem.Name = "btnSelectItem";
            this.btnSelectItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnSelectItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectItem_ItemClick);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Caption = "全选";
            this.btnSelectAll.Id = 1;
            this.btnSelectAll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.ImageOptions.Image")));
            this.btnSelectAll.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.ImageOptions.LargeImage")));
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectAll_ItemClick);
            // 
            // btnInvertSelect
            // 
            this.btnInvertSelect.Caption = "反选";
            this.btnInvertSelect.Id = 343;
            this.btnInvertSelect.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInvertSelect.ImageOptions.Image")));
            this.btnInvertSelect.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnInvertSelect.ImageOptions.LargeImage")));
            this.btnInvertSelect.Name = "btnInvertSelect";
            this.btnInvertSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInvertSelect_ItemClick);
            // 
            // btnCancelSelect
            // 
            this.btnCancelSelect.Caption = "取消选择";
            this.btnCancelSelect.Id = 3;
            this.btnCancelSelect.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelSelect.ImageOptions.Image")));
            this.btnCancelSelect.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCancelSelect.ImageOptions.LargeImage")));
            this.btnCancelSelect.Name = "btnCancelSelect";
            this.btnCancelSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancelSelect_ItemClick);
            // 
            // btnBatchChange
            // 
            this.btnBatchChange.Caption = "批量修改";
            this.btnBatchChange.Id = 4;
            this.btnBatchChange.Name = "btnBatchChange";
            this.btnBatchChange.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBatchChange_ItemClick);
            // 
            // ckBanFastDragCopy
            // 
            this.ckBanFastDragCopy.Caption = "禁止快速拖动和复制";
            this.ckBanFastDragCopy.Id = 5;
            this.ckBanFastDragCopy.Name = "ckBanFastDragCopy";
            this.ckBanFastDragCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ckBanFastDragCopy_ItemClick);
            // 
            // btnSelectGapFigure
            // 
            this.btnSelectGapFigure.Caption = "选择不封闭图形";
            this.btnSelectGapFigure.Id = 6;
            this.btnSelectGapFigure.Name = "btnSelectGapFigure";
            this.btnSelectGapFigure.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectGapFigure_ItemClick);
            // 
            // btnSelectSimilarFigure
            // 
            this.btnSelectSimilarFigure.Caption = "选择相似图形（区分角度）";
            this.btnSelectSimilarFigure.Id = 7;
            this.btnSelectSimilarFigure.Name = "btnSelectSimilarFigure";
            this.btnSelectSimilarFigure.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectSimilarFigure_ItemClick);
            // 
            // btnSelectAllExternalModes
            // 
            this.btnSelectAllExternalModes.Caption = "选择所有外模";
            this.btnSelectAllExternalModes.Id = 8;
            this.btnSelectAllExternalModes.Name = "btnSelectAllExternalModes";
            this.btnSelectAllExternalModes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectAllExternalModes_ItemClick);
            // 
            // btnSelectAllInternalModes
            // 
            this.btnSelectAllInternalModes.Caption = "选择所有内模";
            this.btnSelectAllInternalModes.Id = 9;
            this.btnSelectAllInternalModes.Name = "btnSelectAllInternalModes";
            this.btnSelectAllInternalModes.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectAllInternalModes_ItemClick);
            // 
            // btnSelectAllFigureSmaller
            // 
            this.btnSelectAllFigureSmaller.Caption = "选择所有小于指定尺寸的图形";
            this.btnSelectAllFigureSmaller.Id = 10;
            this.btnSelectAllFigureSmaller.Name = "btnSelectAllFigureSmaller";
            this.btnSelectAllFigureSmaller.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectAllFigureSmaller_ItemClick);
            // 
            // btnSelectFigureLayerItem
            // 
            this.btnSelectFigureLayerItem.Caption = "选择图层";
            this.btnSelectFigureLayerItem.Id = 16;
            this.btnSelectFigureLayerItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft0),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft1),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft2),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft3),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft4),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft5),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft6),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft7),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft8),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft9),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft10),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft11),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft12),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft13),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft14),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft15),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectLayerCraft16)});
            this.btnSelectFigureLayerItem.Name = "btnSelectFigureLayerItem";
            // 
            // btnSelectLayerCraft0
            // 
            this.btnSelectLayerCraft0.Caption = "工艺0";
            this.btnSelectLayerCraft0.Id = 28;
            this.btnSelectLayerCraft0.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer0;
            this.btnSelectLayerCraft0.Name = "btnSelectLayerCraft0";
            this.btnSelectLayerCraft0.Tag = ((short)(0));
            this.btnSelectLayerCraft0.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft1
            // 
            this.btnSelectLayerCraft1.Caption = "工艺1";
            this.btnSelectLayerCraft1.Id = 29;
            this.btnSelectLayerCraft1.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer1;
            this.btnSelectLayerCraft1.Name = "btnSelectLayerCraft1";
            this.btnSelectLayerCraft1.Tag = "1";
            this.btnSelectLayerCraft1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft2
            // 
            this.btnSelectLayerCraft2.Caption = "工艺2";
            this.btnSelectLayerCraft2.Id = 30;
            this.btnSelectLayerCraft2.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer2;
            this.btnSelectLayerCraft2.Name = "btnSelectLayerCraft2";
            this.btnSelectLayerCraft2.Tag = "2";
            this.btnSelectLayerCraft2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft3
            // 
            this.btnSelectLayerCraft3.Caption = "工艺3";
            this.btnSelectLayerCraft3.Id = 47;
            this.btnSelectLayerCraft3.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer3;
            this.btnSelectLayerCraft3.Name = "btnSelectLayerCraft3";
            this.btnSelectLayerCraft3.Tag = "3";
            this.btnSelectLayerCraft3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft4
            // 
            this.btnSelectLayerCraft4.Caption = "工艺4";
            this.btnSelectLayerCraft4.Id = 48;
            this.btnSelectLayerCraft4.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer4;
            this.btnSelectLayerCraft4.Name = "btnSelectLayerCraft4";
            this.btnSelectLayerCraft4.Tag = "4";
            this.btnSelectLayerCraft4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft5
            // 
            this.btnSelectLayerCraft5.Caption = "工艺5";
            this.btnSelectLayerCraft5.Id = 49;
            this.btnSelectLayerCraft5.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer5;
            this.btnSelectLayerCraft5.Name = "btnSelectLayerCraft5";
            this.btnSelectLayerCraft5.Tag = "5";
            this.btnSelectLayerCraft5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft6
            // 
            this.btnSelectLayerCraft6.Caption = "工艺6";
            this.btnSelectLayerCraft6.Id = 50;
            this.btnSelectLayerCraft6.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer6;
            this.btnSelectLayerCraft6.Name = "btnSelectLayerCraft6";
            this.btnSelectLayerCraft6.Tag = "6";
            this.btnSelectLayerCraft6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft7
            // 
            this.btnSelectLayerCraft7.Caption = "工艺7";
            this.btnSelectLayerCraft7.Id = 51;
            this.btnSelectLayerCraft7.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer7;
            this.btnSelectLayerCraft7.Name = "btnSelectLayerCraft7";
            this.btnSelectLayerCraft7.Tag = "7";
            this.btnSelectLayerCraft7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft8
            // 
            this.btnSelectLayerCraft8.Caption = "工艺8";
            this.btnSelectLayerCraft8.Id = 52;
            this.btnSelectLayerCraft8.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer8;
            this.btnSelectLayerCraft8.Name = "btnSelectLayerCraft8";
            this.btnSelectLayerCraft8.Tag = "8";
            this.btnSelectLayerCraft8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft9
            // 
            this.btnSelectLayerCraft9.Caption = "工艺9";
            this.btnSelectLayerCraft9.Id = 53;
            this.btnSelectLayerCraft9.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer9;
            this.btnSelectLayerCraft9.Name = "btnSelectLayerCraft9";
            this.btnSelectLayerCraft9.Tag = "9";
            this.btnSelectLayerCraft9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft10
            // 
            this.btnSelectLayerCraft10.Caption = "工艺10";
            this.btnSelectLayerCraft10.Id = 54;
            this.btnSelectLayerCraft10.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer10;
            this.btnSelectLayerCraft10.Name = "btnSelectLayerCraft10";
            this.btnSelectLayerCraft10.Tag = "10";
            this.btnSelectLayerCraft10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft11
            // 
            this.btnSelectLayerCraft11.Caption = "工艺11";
            this.btnSelectLayerCraft11.Id = 55;
            this.btnSelectLayerCraft11.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer11;
            this.btnSelectLayerCraft11.Name = "btnSelectLayerCraft11";
            this.btnSelectLayerCraft11.Tag = "11";
            this.btnSelectLayerCraft11.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft12
            // 
            this.btnSelectLayerCraft12.Caption = "工艺12";
            this.btnSelectLayerCraft12.Id = 56;
            this.btnSelectLayerCraft12.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer12;
            this.btnSelectLayerCraft12.Name = "btnSelectLayerCraft12";
            this.btnSelectLayerCraft12.Tag = "12";
            this.btnSelectLayerCraft12.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft13
            // 
            this.btnSelectLayerCraft13.Caption = "工艺13";
            this.btnSelectLayerCraft13.Id = 57;
            this.btnSelectLayerCraft13.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer13;
            this.btnSelectLayerCraft13.Name = "btnSelectLayerCraft13";
            this.btnSelectLayerCraft13.Tag = "13";
            this.btnSelectLayerCraft13.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft14
            // 
            this.btnSelectLayerCraft14.Caption = "工艺14";
            this.btnSelectLayerCraft14.Id = 58;
            this.btnSelectLayerCraft14.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer14;
            this.btnSelectLayerCraft14.Name = "btnSelectLayerCraft14";
            this.btnSelectLayerCraft14.Tag = "14";
            this.btnSelectLayerCraft14.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft15
            // 
            this.btnSelectLayerCraft15.Caption = "工艺15";
            this.btnSelectLayerCraft15.Id = 59;
            this.btnSelectLayerCraft15.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer15;
            this.btnSelectLayerCraft15.Name = "btnSelectLayerCraft15";
            this.btnSelectLayerCraft15.Tag = "15";
            this.btnSelectLayerCraft15.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnSelectLayerCraft16
            // 
            this.btnSelectLayerCraft16.Caption = "工艺16";
            this.btnSelectLayerCraft16.Id = 60;
            this.btnSelectLayerCraft16.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.layer16;
            this.btnSelectLayerCraft16.Name = "btnSelectLayerCraft16";
            this.btnSelectLayerCraft16.Tag = "16";
            this.btnSelectLayerCraft16.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectLayerCraft_ItemClick);
            // 
            // btnLockSelectBackground
            // 
            this.btnLockSelectBackground.Caption = "锁定背景";
            this.btnLockSelectBackground.Id = 17;
            this.btnLockSelectBackground.Name = "btnLockSelectBackground";
            this.btnLockSelectBackground.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockSelectBackground_ItemClick);
            // 
            // btnSelectTypeItem
            // 
            this.btnSelectTypeItem.Caption = "按类型选择";
            this.btnSelectTypeItem.Id = 19;
            this.btnSelectTypeItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectAllMultiLines),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectAllCircles),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSelectAllBezierCurves)});
            this.btnSelectTypeItem.Name = "btnSelectTypeItem";
            // 
            // btnSelectAllMultiLines
            // 
            this.btnSelectAllMultiLines.Caption = "选择所有多段线";
            this.btnSelectAllMultiLines.Id = 25;
            this.btnSelectAllMultiLines.Name = "btnSelectAllMultiLines";
            this.btnSelectAllMultiLines.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectAllMultiLines_ItemClick);
            // 
            // btnSelectAllCircles
            // 
            this.btnSelectAllCircles.Caption = "选择所有圆";
            this.btnSelectAllCircles.Id = 26;
            this.btnSelectAllCircles.Name = "btnSelectAllCircles";
            this.btnSelectAllCircles.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectAllCircles_ItemClick);
            // 
            // btnSelectAllBezierCurves
            // 
            this.btnSelectAllBezierCurves.Caption = "选择所有Bezier曲线";
            this.btnSelectAllBezierCurves.Id = 27;
            this.btnSelectAllBezierCurves.Name = "btnSelectAllBezierCurves";
            this.btnSelectAllBezierCurves.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelectAllBezierCurves_ItemClick);
            // 
            // btnCut
            // 
            this.btnCut.Caption = "剪切";
            this.btnCut.Id = 20;
            this.btnCut.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCut.ImageOptions.Image")));
            this.btnCut.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCut.ImageOptions.LargeImage")));
            this.btnCut.Name = "btnCut";
            this.btnCut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCut_ItemClick);
            // 
            // btnCopy
            // 
            this.btnCopy.Caption = "复制";
            this.btnCopy.Id = 21;
            this.btnCopy.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.ImageOptions.Image")));
            this.btnCopy.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCopy.ImageOptions.LargeImage")));
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCopy_ItemClick);
            // 
            // btnCopyBasePoint
            // 
            this.btnCopyBasePoint.Caption = "带基点复制";
            this.btnCopyBasePoint.Id = 22;
            this.btnCopyBasePoint.Name = "btnCopyBasePoint";
            this.btnCopyBasePoint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCopyBasePoint_ItemClick);
            // 
            // btnPaste
            // 
            this.btnPaste.Caption = "粘贴";
            this.btnPaste.Id = 23;
            this.btnPaste.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.ImageOptions.Image")));
            this.btnPaste.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnPaste.ImageOptions.LargeImage")));
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPaste_ItemClick);
            // 
            // btnDelete
            // 
            this.btnDelete.Caption = "删除";
            this.btnDelete.Id = 24;
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.LargeImage")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);
            // 
            // btnDisplayItem
            // 
            this.btnDisplayItem.Caption = "显示";
            this.btnDisplayItem.Id = 13;
            this.btnDisplayItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDisplayItem.ImageOptions.Image")));
            this.btnDisplayItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ckDisplayGapFigureFrame),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckRedDisplayGapFigure),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckDisplaySerialNumber),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckDispalyPathStartPoint),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckDisplayProcessPath),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckDisplayEmptyMovePath),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckDisplayMicroHyphen),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnFigureCenter, true)});
            this.btnDisplayItem.Name = "btnDisplayItem";
            this.btnDisplayItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // ckDisplayGapFigureFrame
            // 
            this.ckDisplayGapFigureFrame.Caption = "显示不封闭图形外框";
            this.ckDisplayGapFigureFrame.Id = 31;
            this.ckDisplayGapFigureFrame.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("ckDisplayGapFigureFrame.ImageOptions.LargeImage")));
            this.ckDisplayGapFigureFrame.Name = "ckDisplayGapFigureFrame";
            this.ckDisplayGapFigureFrame.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckDisplayGapFigureFrame_CheckedChanged);
            // 
            // ckRedDisplayGapFigure
            // 
            this.ckRedDisplayGapFigure.Caption = "红色显示不封闭图形";
            this.ckRedDisplayGapFigure.Id = 32;
            this.ckRedDisplayGapFigure.Name = "ckRedDisplayGapFigure";
            this.ckRedDisplayGapFigure.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckRedDisplayGapFigure_CheckedChanged);
            // 
            // ckDisplaySerialNumber
            // 
            this.ckDisplaySerialNumber.Caption = "显示序号";
            this.ckDisplaySerialNumber.Id = 33;
            this.ckDisplaySerialNumber.Name = "ckDisplaySerialNumber";
            this.ckDisplaySerialNumber.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckDisplaySerialNumber_CheckedChanged);
            // 
            // ckDispalyPathStartPoint
            // 
            this.ckDispalyPathStartPoint.Caption = "显示路径起点";
            this.ckDispalyPathStartPoint.Id = 35;
            this.ckDispalyPathStartPoint.Name = "ckDispalyPathStartPoint";
            this.ckDispalyPathStartPoint.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckDispalyPathStartPoint_CheckedChanged);
            // 
            // ckDisplayProcessPath
            // 
            this.ckDisplayProcessPath.Caption = "显示加工路径";
            this.ckDisplayProcessPath.Id = 36;
            this.ckDisplayProcessPath.Name = "ckDisplayProcessPath";
            this.ckDisplayProcessPath.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckDisplayProcessPath_CheckedChanged);
            // 
            // ckDisplayEmptyMovePath
            // 
            this.ckDisplayEmptyMovePath.Caption = "显示空移路径";
            this.ckDisplayEmptyMovePath.Id = 37;
            this.ckDisplayEmptyMovePath.Name = "ckDisplayEmptyMovePath";
            this.ckDisplayEmptyMovePath.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckDisplayEmptyMovePath_CheckedChanged);
            // 
            // ckDisplayMicroHyphen
            // 
            this.ckDisplayMicroHyphen.Caption = "显示微连标记";
            this.ckDisplayMicroHyphen.Id = 99;
            this.ckDisplayMicroHyphen.Name = "ckDisplayMicroHyphen";
            this.ckDisplayMicroHyphen.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckDisplayMicroHyphen_CheckedChanged);
            // 
            // btnFigureCenter
            // 
            this.btnFigureCenter.Caption = "图形居中";
            this.btnFigureCenter.Id = 38;
            this.btnFigureCenter.Name = "btnFigureCenter";
            this.btnFigureCenter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFigureCenter_ItemClick);
            // 
            // btnFigureTransformItem
            // 
            this.btnFigureTransformItem.Caption = "几何变换";
            this.btnFigureTransformItem.Id = 16;
            this.btnFigureTransformItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFigureTransformItem.ImageOptions.Image")));
            this.btnFigureTransformItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnTranslation),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnInteractiveZoom),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAlignItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMirrorHorizontal, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMirrorVertical),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMirrorAnyAngle),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAnticlockwise90, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnClockwise90),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRotate180),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRotateAnyAngle)});
            this.btnFigureTransformItem.Name = "btnFigureTransformItem";
            this.btnFigureTransformItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnTranslation
            // 
            this.btnTranslation.Caption = "平移";
            this.btnTranslation.Id = 100;
            this.btnTranslation.Name = "btnTranslation";
            this.btnTranslation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTranslation_ItemClick);
            // 
            // btnInteractiveZoom
            // 
            this.btnInteractiveZoom.Caption = "交互式缩放";
            this.btnInteractiveZoom.Id = 101;
            this.btnInteractiveZoom.Name = "btnInteractiveZoom";
            this.btnInteractiveZoom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInteractiveZoom_ItemClick);
            // 
            // btnAlignItem
            // 
            this.btnAlignItem.Caption = "对齐";
            this.btnAlignItem.Id = 102;
            this.btnAlignItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAlignLeft),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAlignRight),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAlignHorizontalCenter),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAlignTop, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAlignBottom),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAlignVerticalCenter),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAlignCenter, true)});
            this.btnAlignItem.Name = "btnAlignItem";
            // 
            // btnAlignLeft
            // 
            this.btnAlignLeft.Caption = "左对齐";
            this.btnAlignLeft.Id = 103;
            this.btnAlignLeft.Name = "btnAlignLeft";
            this.btnAlignLeft.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAlignLeft_ItemClick);
            // 
            // btnAlignRight
            // 
            this.btnAlignRight.Caption = "右对齐";
            this.btnAlignRight.Id = 104;
            this.btnAlignRight.Name = "btnAlignRight";
            this.btnAlignRight.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAlignRight_ItemClick);
            // 
            // btnAlignHorizontalCenter
            // 
            this.btnAlignHorizontalCenter.Caption = "水平居中";
            this.btnAlignHorizontalCenter.Id = 105;
            this.btnAlignHorizontalCenter.Name = "btnAlignHorizontalCenter";
            this.btnAlignHorizontalCenter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAlignHorizontalCenter_ItemClick);
            // 
            // btnAlignTop
            // 
            this.btnAlignTop.Caption = "顶部对齐";
            this.btnAlignTop.Id = 219;
            this.btnAlignTop.Name = "btnAlignTop";
            this.btnAlignTop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAlignTop_ItemClick);
            // 
            // btnAlignBottom
            // 
            this.btnAlignBottom.Caption = "底部对齐";
            this.btnAlignBottom.Id = 106;
            this.btnAlignBottom.Name = "btnAlignBottom";
            this.btnAlignBottom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAlignBottom_ItemClick);
            // 
            // btnAlignVerticalCenter
            // 
            this.btnAlignVerticalCenter.Caption = "垂直居中";
            this.btnAlignVerticalCenter.Id = 107;
            this.btnAlignVerticalCenter.Name = "btnAlignVerticalCenter";
            this.btnAlignVerticalCenter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAlignVerticalCenter_ItemClick);
            // 
            // btnAlignCenter
            // 
            this.btnAlignCenter.Caption = "居中对齐";
            this.btnAlignCenter.Id = 108;
            this.btnAlignCenter.Name = "btnAlignCenter";
            this.btnAlignCenter.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAlignCenter_ItemClick);
            // 
            // btnMirrorHorizontal
            // 
            this.btnMirrorHorizontal.Caption = "水平镜像";
            this.btnMirrorHorizontal.Id = 109;
            this.btnMirrorHorizontal.Name = "btnMirrorHorizontal";
            this.btnMirrorHorizontal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMirrorHorizontal_ItemClick);
            // 
            // btnMirrorVertical
            // 
            this.btnMirrorVertical.Caption = "垂直镜像";
            this.btnMirrorVertical.Id = 110;
            this.btnMirrorVertical.Name = "btnMirrorVertical";
            this.btnMirrorVertical.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMirrorVertical_ItemClick);
            // 
            // btnMirrorAnyAngle
            // 
            this.btnMirrorAnyAngle.Caption = "任意角度镜像";
            this.btnMirrorAnyAngle.Id = 111;
            this.btnMirrorAnyAngle.Name = "btnMirrorAnyAngle";
            this.btnMirrorAnyAngle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMirrorAnyAngle_ItemClick);
            // 
            // btnAnticlockwise90
            // 
            this.btnAnticlockwise90.Caption = "逆时针旋转90°";
            this.btnAnticlockwise90.Id = 112;
            this.btnAnticlockwise90.Name = "btnAnticlockwise90";
            this.btnAnticlockwise90.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAnticlockwise90_ItemClick);
            // 
            // btnClockwise90
            // 
            this.btnClockwise90.Caption = "顺时针旋转90°";
            this.btnClockwise90.Id = 113;
            this.btnClockwise90.Name = "btnClockwise90";
            this.btnClockwise90.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClockwise90_ItemClick);
            // 
            // btnRotate180
            // 
            this.btnRotate180.Caption = "旋转180°";
            this.btnRotate180.Id = 114;
            this.btnRotate180.Name = "btnRotate180";
            this.btnRotate180.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRotate180_ItemClick);
            // 
            // btnRotateAnyAngle
            // 
            this.btnRotateAnyAngle.Caption = "任意角度旋转";
            this.btnRotateAnyAngle.Id = 115;
            this.btnRotateAnyAngle.Name = "btnRotateAnyAngle";
            this.btnRotateAnyAngle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRotateAnyAngle_ItemClick);
            // 
            // btnClearItem
            // 
            this.btnClearItem.Caption = "清除";
            this.btnClearItem.Id = 18;
            this.btnClearItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClearItem.ImageOptions.Image")));
            this.btnClearItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnClearItem.ImageOptions.LargeImage")));
            this.btnClearItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnClearLeadInOrOutWire),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnClearMicroConnect),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancelGapCompensation)});
            this.btnClearItem.Name = "btnClearItem";
            this.btnClearItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnClearLeadInOrOutWire
            // 
            this.btnClearLeadInOrOutWire.Caption = "清除引入引出线";
            this.btnClearLeadInOrOutWire.Id = 94;
            this.btnClearLeadInOrOutWire.Name = "btnClearLeadInOrOutWire";
            this.btnClearLeadInOrOutWire.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClearLeadInOrOutWire_ItemClick);
            // 
            // btnClearMicroConnect
            // 
            this.btnClearMicroConnect.Caption = "清除微连";
            this.btnClearMicroConnect.Id = 95;
            this.btnClearMicroConnect.Name = "btnClearMicroConnect";
            this.btnClearMicroConnect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClearMicroConnect_ItemClick);
            // 
            // btnCancelGapCompensation
            // 
            this.btnCancelGapCompensation.Caption = "取消补偿";
            this.btnCancelGapCompensation.Id = 96;
            this.btnCancelGapCompensation.Name = "btnCancelGapCompensation";
            this.btnCancelGapCompensation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCancelGapCompensation_ItemClick);
            // 
            // btnStartPoint
            // 
            this.btnStartPoint.Caption = "起点";
            this.btnStartPoint.Id = 40;
            this.btnStartPoint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnStartPoint.ImageOptions.Image")));
            this.btnStartPoint.Name = "btnStartPoint";
            this.btnStartPoint.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnStartPoint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStartPoint_ItemClick);
            // 
            // btnDockPosition
            // 
            this.btnDockPosition.Caption = "停靠";
            this.btnDockPosition.Id = 41;
            this.btnDockPosition.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDockPosition.ImageOptions.Image")));
            this.btnDockPosition.Name = "btnDockPosition";
            this.btnDockPosition.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDockPosition_ItemClick);
            // 
            // btnGapCompensation
            // 
            this.btnGapCompensation.Caption = "补偿";
            this.btnGapCompensation.Id = 42;
            this.btnGapCompensation.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGapCompensation.ImageOptions.Image")));
            this.btnGapCompensation.Name = "btnGapCompensation";
            this.btnGapCompensation.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGapCompensation_ItemClick);
            // 
            // btnOuterCut
            // 
            this.btnOuterCut.Caption = "阳切";
            this.btnOuterCut.Id = 43;
            this.btnOuterCut.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOuterCut.ImageOptions.Image")));
            this.btnOuterCut.Name = "btnOuterCut";
            this.btnOuterCut.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnOuterCut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOuterCut_ItemClick);
            // 
            // btnInnerCut
            // 
            this.btnInnerCut.Caption = "阴切";
            this.btnInnerCut.Id = 44;
            this.btnInnerCut.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInnerCut.ImageOptions.Image")));
            this.btnInnerCut.Name = "btnInnerCut";
            this.btnInnerCut.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnInnerCut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInnerCut_ItemClick);
            // 
            // btnSurroundCut
            // 
            this.btnSurroundCut.Caption = "环切";
            this.btnSurroundCut.Id = 45;
            this.btnSurroundCut.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSurroundCut.ImageOptions.Image")));
            this.btnSurroundCut.Name = "btnSurroundCut";
            this.btnSurroundCut.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnSurroundCut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRingCut_ItemClick);
            // 
            // btnMicroConnectItem
            // 
            this.btnMicroConnectItem.Caption = "微连";
            this.btnMicroConnectItem.Id = 49;
            this.btnMicroConnectItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMicroConnectItem.ImageOptions.Image")));
            this.btnMicroConnectItem.ItemLinks.Add(this.btnMicroConnectionAuto);
            this.btnMicroConnectItem.ItemLinks.Add(this.btnMicroConnectBlowOpen);
            this.btnMicroConnectItem.Name = "btnMicroConnectItem";
            this.btnMicroConnectItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnMicroConnectItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMicroConnectItem_ItemClick);
            // 
            // btnMicroConnectionAuto
            // 
            this.btnMicroConnectionAuto.Caption = "自动微连";
            this.btnMicroConnectionAuto.Id = 174;
            this.btnMicroConnectionAuto.Name = "btnMicroConnectionAuto";
            this.btnMicroConnectionAuto.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMicroConnectionAuto_ItemClick);
            // 
            // btnMicroConnectBlowOpen
            // 
            this.btnMicroConnectBlowOpen.Caption = "炸开微连";
            this.btnMicroConnectBlowOpen.Id = 175;
            this.btnMicroConnectBlowOpen.Name = "btnMicroConnectBlowOpen";
            this.btnMicroConnectBlowOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMicroConnectBlowOpen_ItemClick);
            // 
            // btnReverseItem
            // 
            this.btnReverseItem.Caption = "反向";
            this.btnReverseItem.Id = 50;
            this.btnReverseItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnReverseItem.ImageOptions.Image")));
            this.btnReverseItem.ItemLinks.Add(this.btnReverse);
            this.btnReverseItem.ItemLinks.Add(this.btnClockwise);
            this.btnReverseItem.ItemLinks.Add(this.btnAnticlockwise);
            this.btnReverseItem.Name = "btnReverseItem";
            this.btnReverseItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnReverseItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReverseItem_ItemClick);
            // 
            // btnReverse
            // 
            this.btnReverse.Caption = "反向";
            this.btnReverse.Id = 177;
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReverse_ItemClick);
            // 
            // btnClockwise
            // 
            this.btnClockwise.Caption = "顺时针";
            this.btnClockwise.Id = 178;
            this.btnClockwise.Name = "btnClockwise";
            this.btnClockwise.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClockwise_ItemClick);
            // 
            // btnAnticlockwise
            // 
            this.btnAnticlockwise.Caption = "逆时针";
            this.btnAnticlockwise.Id = 179;
            this.btnAnticlockwise.Name = "btnAnticlockwise";
            this.btnAnticlockwise.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAnticlockwise_ItemClick);
            // 
            // btnSealingItem
            // 
            this.btnSealingItem.Caption = "封口";
            this.btnSealingItem.Id = 51;
            this.btnSealingItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSealingItem.ImageOptions.Image")));
            this.btnSealingItem.ItemLinks.Add(this.btnSealing);
            this.btnSealingItem.ItemLinks.Add(this.btnGaping);
            this.btnSealingItem.ItemLinks.Add(this.btnCutOver);
            this.btnSealingItem.ItemLinks.Add(this.btnMutilGaps);
            this.btnSealingItem.Name = "btnSealingItem";
            this.btnSealingItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnSealingItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSealingItem_ItemClick);
            // 
            // btnSealing
            // 
            this.btnSealing.Caption = "封口";
            this.btnSealing.Id = 181;
            this.btnSealing.Name = "btnSealing";
            this.btnSealing.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSealing_ItemClick);
            // 
            // btnGaping
            // 
            this.btnGaping.Caption = "缺口";
            this.btnGaping.Id = 182;
            this.btnGaping.Name = "btnGaping";
            this.btnGaping.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGaping_ItemClick);
            // 
            // btnCutOver
            // 
            this.btnCutOver.Caption = "过切";
            this.btnCutOver.Id = 183;
            this.btnCutOver.Name = "btnCutOver";
            this.btnCutOver.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOverCut_ItemClick);
            // 
            // btnMutilGaps
            // 
            this.btnMutilGaps.Caption = "多圈";
            this.btnMutilGaps.Id = 184;
            this.btnMutilGaps.Name = "btnMutilGaps";
            this.btnMutilGaps.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMutilGaps_ItemClick);
            // 
            // btnSortItem
            // 
            this.btnSortItem.Caption = "排序";
            this.btnSortItem.Id = 52;
            this.btnSortItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSortItem.ImageOptions.Image")));
            this.btnSortItem.ItemLinks.Add(this.ckSortGrid);
            this.btnSortItem.ItemLinks.Add(this.ckSortShortestMove);
            this.btnSortItem.ItemLinks.Add(this.ckSortKnife);
            this.btnSortItem.ItemLinks.Add(this.ckSortSmallFigurePriority);
            this.btnSortItem.ItemLinks.Add(this.ckSortInsideToOut);
            this.btnSortItem.ItemLinks.Add(this.ckSortLeftToRight);
            this.btnSortItem.ItemLinks.Add(this.ckSortRightToLeft);
            this.btnSortItem.ItemLinks.Add(this.ckSortTopToBottom);
            this.btnSortItem.ItemLinks.Add(this.ckSortBottomToTop);
            this.btnSortItem.ItemLinks.Add(this.ckSortClockwise);
            this.btnSortItem.ItemLinks.Add(this.ckSortAnticlockwise);
            this.btnSortItem.ItemLinks.Add(this.ckSortProhibitChangDirection);
            this.btnSortItem.ItemLinks.Add(this.ckSortDistinguishInsideOutside);
            this.btnSortItem.ItemLinks.Add(this.ckSortShadeCutOutermost);
            this.btnSortItem.Name = "btnSortItem";
            this.btnSortItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnSortItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSortItem_ItemClick);
            // 
            // ckSortGrid
            // 
            this.ckSortGrid.Caption = "网格排序";
            this.ckSortGrid.GroupIndex = 1;
            this.ckSortGrid.Id = 188;
            this.ckSortGrid.Name = "ckSortGrid";
            this.ckSortGrid.Tag = "SortGrid";
            this.ckSortGrid.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // ckSortShortestMove
            // 
            this.ckSortShortestMove.Caption = "局部最短空移";
            this.ckSortShortestMove.GroupIndex = 1;
            this.ckSortShortestMove.Id = 189;
            this.ckSortShortestMove.Name = "ckSortShortestMove";
            this.ckSortShortestMove.Tag = "SortShortestMove";
            this.ckSortShortestMove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // ckSortKnife
            // 
            this.ckSortKnife.Caption = "刀摸排序";
            this.ckSortKnife.GroupIndex = 1;
            this.ckSortKnife.Id = 190;
            this.ckSortKnife.Name = "ckSortKnife";
            this.ckSortKnife.Tag = "SortKnife";
            this.ckSortKnife.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // ckSortSmallFigurePriority
            // 
            this.ckSortSmallFigurePriority.Caption = "小图优先";
            this.ckSortSmallFigurePriority.GroupIndex = 1;
            this.ckSortSmallFigurePriority.Id = 191;
            this.ckSortSmallFigurePriority.Name = "ckSortSmallFigurePriority";
            this.ckSortSmallFigurePriority.Tag = "SortSmallFigurePriority";
            this.ckSortSmallFigurePriority.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // ckSortInsideToOut
            // 
            this.ckSortInsideToOut.Caption = "由内到外";
            this.ckSortInsideToOut.GroupIndex = 1;
            this.ckSortInsideToOut.Id = 192;
            this.ckSortInsideToOut.Name = "ckSortInsideToOut";
            this.ckSortInsideToOut.Tag = "SortInsideToOut";
            this.ckSortInsideToOut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // ckSortLeftToRight
            // 
            this.ckSortLeftToRight.Caption = "从左到右";
            this.ckSortLeftToRight.GroupIndex = 1;
            this.ckSortLeftToRight.Id = 193;
            this.ckSortLeftToRight.Name = "ckSortLeftToRight";
            this.ckSortLeftToRight.Tag = "SortLeftToRight";
            this.ckSortLeftToRight.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // ckSortRightToLeft
            // 
            this.ckSortRightToLeft.Caption = "从右到左";
            this.ckSortRightToLeft.GroupIndex = 1;
            this.ckSortRightToLeft.Id = 194;
            this.ckSortRightToLeft.Name = "ckSortRightToLeft";
            this.ckSortRightToLeft.Tag = "SortRightToLeft";
            this.ckSortRightToLeft.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // ckSortTopToBottom
            // 
            this.ckSortTopToBottom.Caption = "从上到下";
            this.ckSortTopToBottom.GroupIndex = 1;
            this.ckSortTopToBottom.Id = 195;
            this.ckSortTopToBottom.Name = "ckSortTopToBottom";
            this.ckSortTopToBottom.Tag = "SortTopToBottom";
            this.ckSortTopToBottom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // ckSortBottomToTop
            // 
            this.ckSortBottomToTop.Caption = "从下到上";
            this.ckSortBottomToTop.GroupIndex = 1;
            this.ckSortBottomToTop.Id = 196;
            this.ckSortBottomToTop.Name = "ckSortBottomToTop";
            this.ckSortBottomToTop.Tag = "SortBottomToTop";
            this.ckSortBottomToTop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // ckSortClockwise
            // 
            this.ckSortClockwise.Caption = "顺时针";
            this.ckSortClockwise.GroupIndex = 1;
            this.ckSortClockwise.Id = 197;
            this.ckSortClockwise.Name = "ckSortClockwise";
            this.ckSortClockwise.Tag = "SortClockwise";
            this.ckSortClockwise.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // ckSortAnticlockwise
            // 
            this.ckSortAnticlockwise.Caption = "逆时针";
            this.ckSortAnticlockwise.GroupIndex = 1;
            this.ckSortAnticlockwise.Id = 198;
            this.ckSortAnticlockwise.Name = "ckSortAnticlockwise";
            this.ckSortAnticlockwise.Tag = "SortAnticlockwise";
            this.ckSortAnticlockwise.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // ckSortProhibitChangDirection
            // 
            this.ckSortProhibitChangDirection.Caption = "禁止排序改变方向";
            this.ckSortProhibitChangDirection.Id = 199;
            this.ckSortProhibitChangDirection.Name = "ckSortProhibitChangDirection";
            this.ckSortProhibitChangDirection.Tag = "SortProhibitChangDirection";
            // 
            // ckSortDistinguishInsideOutside
            // 
            this.ckSortDistinguishInsideOutside.Caption = "排序时区分内外摸";
            this.ckSortDistinguishInsideOutside.Id = 200;
            this.ckSortDistinguishInsideOutside.Name = "ckSortDistinguishInsideOutside";
            this.ckSortDistinguishInsideOutside.Tag = "SortDistinguishInsideOutside";
            // 
            // ckSortShadeCutOutermost
            // 
            this.ckSortShadeCutOutermost.Caption = "最外层为阴切";
            this.ckSortShadeCutOutermost.Id = 201;
            this.ckSortShadeCutOutermost.Name = "ckSortShadeCutOutermost";
            this.ckSortShadeCutOutermost.Tag = "SortShadeCutOutermost";
            // 
            // btnArrayLayoutItem
            // 
            this.btnArrayLayoutItem.Caption = "阵列";
            this.btnArrayLayoutItem.Id = 53;
            this.btnArrayLayoutItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnArrayLayoutItem.ImageOptions.Image")));
            this.btnArrayLayoutItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnArrayLayoutItem.ImageOptions.LargeImage")));
            this.btnArrayLayoutItem.ItemLinks.Add(this.btnArrayLayoutRectangle);
            this.btnArrayLayoutItem.ItemLinks.Add(this.btnArrayLayoutInteractive);
            this.btnArrayLayoutItem.ItemLinks.Add(this.btnArrayLayoutAnnular);
            this.btnArrayLayoutItem.ItemLinks.Add(this.btnArrayLayoutFull);
            this.btnArrayLayoutItem.Name = "btnArrayLayoutItem";
            this.btnArrayLayoutItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnArrayLayoutItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnArrayLayoutItem_ItemClick);
            // 
            // btnArrayLayoutRectangle
            // 
            this.btnArrayLayoutRectangle.Caption = "矩形阵列";
            this.btnArrayLayoutRectangle.Id = 203;
            this.btnArrayLayoutRectangle.Name = "btnArrayLayoutRectangle";
            this.btnArrayLayoutRectangle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnArrayLayoutRectangle_ItemClick);
            // 
            // btnArrayLayoutInteractive
            // 
            this.btnArrayLayoutInteractive.Caption = "交互式阵列";
            this.btnArrayLayoutInteractive.Id = 204;
            this.btnArrayLayoutInteractive.Name = "btnArrayLayoutInteractive";
            this.btnArrayLayoutInteractive.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnArrayLayoutInteractive_ItemClick);
            // 
            // btnArrayLayoutAnnular
            // 
            this.btnArrayLayoutAnnular.Caption = "环形阵列";
            this.btnArrayLayoutAnnular.Id = 205;
            this.btnArrayLayoutAnnular.Name = "btnArrayLayoutAnnular";
            this.btnArrayLayoutAnnular.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnArrayLayoutAnnular_ItemClick);
            // 
            // btnArrayLayoutFull
            // 
            this.btnArrayLayoutFull.Caption = "布满";
            this.btnArrayLayoutFull.Id = 206;
            this.btnArrayLayoutFull.Name = "btnArrayLayoutFull";
            this.btnArrayLayoutFull.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnArrayLayoutFull_ItemClick);
            // 
            // btnFlyingCutItem
            // 
            this.btnFlyingCutItem.Caption = "飞切";
            this.btnFlyingCutItem.Id = 55;
            this.btnFlyingCutItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFlyingCutItem.ImageOptions.Image")));
            this.btnFlyingCutItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnFlyingCutItem.ImageOptions.LargeImage")));
            this.btnFlyingCutItem.ItemLinks.Add(this.btnFlyingCutLine);
            this.btnFlyingCutItem.ItemLinks.Add(this.btnFlyingCutArc);
            this.btnFlyingCutItem.Name = "btnFlyingCutItem";
            this.btnFlyingCutItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnFlyingCutItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFlyingCutItem_ItemClick);
            // 
            // btnFlyingCutLine
            // 
            this.btnFlyingCutLine.Caption = "直线飞行切割";
            this.btnFlyingCutLine.Id = 207;
            this.btnFlyingCutLine.Name = "btnFlyingCutLine";
            this.btnFlyingCutLine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFlyingCutLine_ItemClick);
            // 
            // btnFlyingCutArc
            // 
            this.btnFlyingCutArc.Caption = "圆弧飞行切割";
            this.btnFlyingCutArc.Id = 208;
            this.btnFlyingCutArc.Name = "btnFlyingCutArc";
            this.btnFlyingCutArc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFlyingCutArc_ItemClick);
            // 
            // btnCommonSideItem
            // 
            this.btnCommonSideItem.Caption = "共边";
            this.btnCommonSideItem.Id = 56;
            this.btnCommonSideItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCommonSideItem.ImageOptions.Image")));
            this.btnCommonSideItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCommonSideItem.ImageOptions.LargeImage")));
            this.btnCommonSideItem.ItemLinks.Add(this.btnCommonSideTypeC);
            this.btnCommonSideItem.Name = "btnCommonSideItem";
            this.btnCommonSideItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCommonSideItem_ItemClick);
            // 
            // btnCommonSideTypeC
            // 
            this.btnCommonSideTypeC.Caption = "C字形共边";
            this.btnCommonSideTypeC.Id = 209;
            this.btnCommonSideTypeC.Name = "btnCommonSideTypeC";
            this.btnCommonSideTypeC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCommonSideTypeC_ItemClick);
            // 
            // btnBridging
            // 
            this.btnBridging.Caption = "桥接";
            this.btnBridging.Id = 57;
            this.btnBridging.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBridging.ImageOptions.Image")));
            this.btnBridging.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBridging.ImageOptions.LargeImage")));
            this.btnBridging.Name = "btnBridging";
            this.btnBridging.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnBridging.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBridging_ItemClick);
            // 
            // btnMeasure
            // 
            this.btnMeasure.Caption = "测量";
            this.btnMeasure.Id = 58;
            this.btnMeasure.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMeasure.ImageOptions.Image")));
            this.btnMeasure.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnMeasure.ImageOptions.LargeImage")));
            this.btnMeasure.Name = "btnMeasure";
            this.btnMeasure.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnMeasure.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMeasure_ItemClick);
            // 
            // btnMajorizationItem
            // 
            this.btnMajorizationItem.Caption = "优化";
            this.btnMajorizationItem.Id = 59;
            this.btnMajorizationItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMajorizationItem.ImageOptions.Image")));
            this.btnMajorizationItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnMajorizationItem.ImageOptions.LargeImage")));
            this.btnMajorizationItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCurveSmoothing),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCurveSegment),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDeleteRepeatLine),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDeleteSmallFigure),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMergeConnectLine),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCutUp),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnConnectKnife)});
            this.btnMajorizationItem.Name = "btnMajorizationItem";
            this.btnMajorizationItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnCurveSmoothing
            // 
            this.btnCurveSmoothing.Caption = "曲线平滑";
            this.btnCurveSmoothing.Id = 137;
            this.btnCurveSmoothing.Name = "btnCurveSmoothing";
            this.btnCurveSmoothing.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCurveSmoothing_ItemClick);
            // 
            // btnCurveSegment
            // 
            this.btnCurveSegment.Caption = "曲线分割";
            this.btnCurveSegment.Id = 138;
            this.btnCurveSegment.Name = "btnCurveSegment";
            this.btnCurveSegment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCurveSegment_ItemClick);
            // 
            // btnDeleteRepeatLine
            // 
            this.btnDeleteRepeatLine.Caption = "去除重复线";
            this.btnDeleteRepeatLine.Id = 139;
            this.btnDeleteRepeatLine.Name = "btnDeleteRepeatLine";
            this.btnDeleteRepeatLine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteRepeatLine_ItemClick);
            // 
            // btnDeleteSmallFigure
            // 
            this.btnDeleteSmallFigure.Caption = "去除小图形";
            this.btnDeleteSmallFigure.Id = 140;
            this.btnDeleteSmallFigure.Name = "btnDeleteSmallFigure";
            this.btnDeleteSmallFigure.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteSmallFigure_ItemClick);
            // 
            // btnMergeConnectLine
            // 
            this.btnMergeConnectLine.Caption = "合并相连线";
            this.btnMergeConnectLine.Id = 141;
            this.btnMergeConnectLine.Name = "btnMergeConnectLine";
            this.btnMergeConnectLine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMergeConnectLine_ItemClick);
            // 
            // btnCutUp
            // 
            this.btnCutUp.Caption = "切碎";
            this.btnCutUp.Id = 142;
            this.btnCutUp.Name = "btnCutUp";
            this.btnCutUp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCutUp_ItemClick);
            // 
            // btnConnectKnife
            // 
            this.btnConnectKnife.Caption = "接刀";
            this.btnConnectKnife.Id = 143;
            this.btnConnectKnife.Name = "btnConnectKnife";
            this.btnConnectKnife.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnConnectKnife_ItemClick);
            // 
            // btnCraftItem
            // 
            this.btnCraftItem.Caption = "工艺";
            this.btnCraftItem.Id = 60;
            this.btnCraftItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCraftItem.ImageOptions.Image")));
            this.btnCraftItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCraftItem.ImageOptions.LargeImage")));
            this.btnCraftItem.ItemLinks.Add(this.btnShowFigureLayerItem);
            this.btnCraftItem.ItemLinks.Add(this.btnOnlyShowFigureLayerItem);
            this.btnCraftItem.ItemLinks.Add(this.btnLockFigureLayerItem);
            this.btnCraftItem.ItemLinks.Add(this.btnDXFFigureMap);
            this.btnCraftItem.Name = "btnCraftItem";
            this.btnCraftItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCraftItem_ItemClick);
            // 
            // btnShowFigureLayerItem
            // 
            this.btnShowFigureLayerItem.Caption = "显示图层";
            this.btnShowFigureLayerItem.Id = 88;
            this.btnShowFigureLayerItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnShowFigureLayerItem.ImageOptions.Image")));
            this.btnShowFigureLayerItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnShowFigureLayerItem.ImageOptions.LargeImage")));
            this.btnShowFigureLayerItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnShowBackgroundCarft),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft1),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft2),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft3),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft4),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft5),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft6),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft7),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft8),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft9),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft10),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft11),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft12),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.ckShowCarft13, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckShowCarft14),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.ckShowCarft15, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.ckShowCarft16, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.btnShowFigureLayerItem.Name = "btnShowFigureLayerItem";
            this.btnShowFigureLayerItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // btnShowBackgroundCarft
            // 
            this.btnShowBackgroundCarft.Caption = "显示背景";
            this.btnShowBackgroundCarft.Id = 99;
            this.btnShowBackgroundCarft.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnShowBackgroundCarft.ImageOptions.Image")));
            this.btnShowBackgroundCarft.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnShowBackgroundCarft.ImageOptions.LargeImage")));
            this.btnShowBackgroundCarft.Name = "btnShowBackgroundCarft";
            this.btnShowBackgroundCarft.Tag = "0";
            this.btnShowBackgroundCarft.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft1
            // 
            this.ckShowCarft1.BindableChecked = true;
            this.ckShowCarft1.Caption = "显示工艺1";
            this.ckShowCarft1.Checked = true;
            this.ckShowCarft1.Id = 100;
            this.ckShowCarft1.Name = "ckShowCarft1";
            this.ckShowCarft1.Tag = "1";
            this.ckShowCarft1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft2
            // 
            this.ckShowCarft2.BindableChecked = true;
            this.ckShowCarft2.Caption = "显示工艺2";
            this.ckShowCarft2.Checked = true;
            this.ckShowCarft2.Id = 101;
            this.ckShowCarft2.Name = "ckShowCarft2";
            this.ckShowCarft2.Tag = "2";
            this.ckShowCarft2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft3
            // 
            this.ckShowCarft3.BindableChecked = true;
            this.ckShowCarft3.Caption = "显示工艺3";
            this.ckShowCarft3.Checked = true;
            this.ckShowCarft3.Id = 102;
            this.ckShowCarft3.Name = "ckShowCarft3";
            this.ckShowCarft3.Tag = "3";
            this.ckShowCarft3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft4
            // 
            this.ckShowCarft4.BindableChecked = true;
            this.ckShowCarft4.Caption = "显示工艺4";
            this.ckShowCarft4.Checked = true;
            this.ckShowCarft4.Id = 103;
            this.ckShowCarft4.Name = "ckShowCarft4";
            this.ckShowCarft4.Tag = "4";
            this.ckShowCarft4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft5
            // 
            this.ckShowCarft5.BindableChecked = true;
            this.ckShowCarft5.Caption = "显示工艺5";
            this.ckShowCarft5.Checked = true;
            this.ckShowCarft5.Id = 104;
            this.ckShowCarft5.Name = "ckShowCarft5";
            this.ckShowCarft5.Tag = "5";
            this.ckShowCarft5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft6
            // 
            this.ckShowCarft6.BindableChecked = true;
            this.ckShowCarft6.Caption = "显示工艺6";
            this.ckShowCarft6.Checked = true;
            this.ckShowCarft6.Id = 105;
            this.ckShowCarft6.Name = "ckShowCarft6";
            this.ckShowCarft6.Tag = "6";
            this.ckShowCarft6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft7
            // 
            this.ckShowCarft7.BindableChecked = true;
            this.ckShowCarft7.Caption = "显示工艺7";
            this.ckShowCarft7.Checked = true;
            this.ckShowCarft7.Id = 106;
            this.ckShowCarft7.Name = "ckShowCarft7";
            this.ckShowCarft7.Tag = "7";
            this.ckShowCarft7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft8
            // 
            this.ckShowCarft8.BindableChecked = true;
            this.ckShowCarft8.Caption = "显示工艺8";
            this.ckShowCarft8.Checked = true;
            this.ckShowCarft8.Id = 107;
            this.ckShowCarft8.Name = "ckShowCarft8";
            this.ckShowCarft8.Tag = "8";
            this.ckShowCarft8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft9
            // 
            this.ckShowCarft9.BindableChecked = true;
            this.ckShowCarft9.Caption = "显示工艺9";
            this.ckShowCarft9.Checked = true;
            this.ckShowCarft9.Id = 108;
            this.ckShowCarft9.Name = "ckShowCarft9";
            this.ckShowCarft9.Tag = "9";
            this.ckShowCarft9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft10
            // 
            this.ckShowCarft10.BindableChecked = true;
            this.ckShowCarft10.Caption = "显示工艺10";
            this.ckShowCarft10.Checked = true;
            this.ckShowCarft10.Id = 109;
            this.ckShowCarft10.Name = "ckShowCarft10";
            this.ckShowCarft10.Tag = "10";
            this.ckShowCarft10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft11
            // 
            this.ckShowCarft11.BindableChecked = true;
            this.ckShowCarft11.Caption = "显示工艺11";
            this.ckShowCarft11.Checked = true;
            this.ckShowCarft11.Id = 110;
            this.ckShowCarft11.Name = "ckShowCarft11";
            this.ckShowCarft11.Tag = "11";
            this.ckShowCarft11.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft12
            // 
            this.ckShowCarft12.BindableChecked = true;
            this.ckShowCarft12.Caption = "显示工艺12";
            this.ckShowCarft12.Checked = true;
            this.ckShowCarft12.Id = 111;
            this.ckShowCarft12.Name = "ckShowCarft12";
            this.ckShowCarft12.Tag = "12";
            this.ckShowCarft12.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft13
            // 
            this.ckShowCarft13.BindableChecked = true;
            this.ckShowCarft13.Caption = "显示工艺13";
            this.ckShowCarft13.Checked = true;
            this.ckShowCarft13.Id = 112;
            this.ckShowCarft13.Name = "ckShowCarft13";
            this.ckShowCarft13.Tag = "13";
            this.ckShowCarft13.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft14
            // 
            this.ckShowCarft14.BindableChecked = true;
            this.ckShowCarft14.Caption = "显示工艺14";
            this.ckShowCarft14.Checked = true;
            this.ckShowCarft14.Id = 113;
            this.ckShowCarft14.Name = "ckShowCarft14";
            this.ckShowCarft14.Tag = "14";
            this.ckShowCarft14.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft15
            // 
            this.ckShowCarft15.BindableChecked = true;
            this.ckShowCarft15.Caption = "显示工艺15";
            this.ckShowCarft15.Checked = true;
            this.ckShowCarft15.Id = 114;
            this.ckShowCarft15.Name = "ckShowCarft15";
            this.ckShowCarft15.Tag = "15";
            this.ckShowCarft15.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // ckShowCarft16
            // 
            this.ckShowCarft16.BindableChecked = true;
            this.ckShowCarft16.Caption = "显示工艺16";
            this.ckShowCarft16.Checked = true;
            this.ckShowCarft16.Id = 115;
            this.ckShowCarft16.Name = "ckShowCarft16";
            this.ckShowCarft16.Tag = "16";
            this.ckShowCarft16.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShowCarft_ItemClick);
            // 
            // btnOnlyShowFigureLayerItem
            // 
            this.btnOnlyShowFigureLayerItem.AutoFillEditorWidth = false;
            this.btnOnlyShowFigureLayerItem.Caption = "只显示图层";
            this.btnOnlyShowFigureLayerItem.Id = 89;
            this.btnOnlyShowFigureLayerItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOnlyShowFigureLayerItem.ImageOptions.Image")));
            this.btnOnlyShowFigureLayerItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnOnlyShowFigureLayerItem.ImageOptions.LargeImage")));
            this.btnOnlyShowFigureLayerItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarftALL),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft1),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft2),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft3),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft4),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft5),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft6),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft7),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft8),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft9),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft10),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft11),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft12),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft13),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft14),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft15),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOnlyShowCarft16)});
            this.btnOnlyShowFigureLayerItem.Name = "btnOnlyShowFigureLayerItem";
            this.btnOnlyShowFigureLayerItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // btnOnlyShowCarftALL
            // 
            this.btnOnlyShowCarftALL.Caption = "显示全部";
            this.btnOnlyShowCarftALL.Id = 117;
            this.btnOnlyShowCarftALL.Name = "btnOnlyShowCarftALL";
            this.btnOnlyShowCarftALL.Tag = "0";
            this.btnOnlyShowCarftALL.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft1
            // 
            this.btnOnlyShowCarft1.Caption = "只显示工艺1";
            this.btnOnlyShowCarft1.Id = 118;
            this.btnOnlyShowCarft1.Name = "btnOnlyShowCarft1";
            this.btnOnlyShowCarft1.Tag = "1";
            this.btnOnlyShowCarft1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft2
            // 
            this.btnOnlyShowCarft2.Caption = "只显示工艺2";
            this.btnOnlyShowCarft2.Id = 119;
            this.btnOnlyShowCarft2.Name = "btnOnlyShowCarft2";
            this.btnOnlyShowCarft2.Tag = "2";
            this.btnOnlyShowCarft2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft3
            // 
            this.btnOnlyShowCarft3.Caption = "只显示工艺3";
            this.btnOnlyShowCarft3.Id = 120;
            this.btnOnlyShowCarft3.Name = "btnOnlyShowCarft3";
            this.btnOnlyShowCarft3.Tag = "3";
            this.btnOnlyShowCarft3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft4
            // 
            this.btnOnlyShowCarft4.Caption = "只显示工艺4";
            this.btnOnlyShowCarft4.Id = 121;
            this.btnOnlyShowCarft4.Name = "btnOnlyShowCarft4";
            this.btnOnlyShowCarft4.Tag = "4";
            this.btnOnlyShowCarft4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft5
            // 
            this.btnOnlyShowCarft5.Caption = "只显示工艺5";
            this.btnOnlyShowCarft5.Id = 122;
            this.btnOnlyShowCarft5.Name = "btnOnlyShowCarft5";
            this.btnOnlyShowCarft5.Tag = "5";
            this.btnOnlyShowCarft5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft6
            // 
            this.btnOnlyShowCarft6.Caption = "只显示工艺6";
            this.btnOnlyShowCarft6.Id = 123;
            this.btnOnlyShowCarft6.Name = "btnOnlyShowCarft6";
            this.btnOnlyShowCarft6.Tag = "6";
            this.btnOnlyShowCarft6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft7
            // 
            this.btnOnlyShowCarft7.Caption = "只显示工艺7";
            this.btnOnlyShowCarft7.Id = 124;
            this.btnOnlyShowCarft7.Name = "btnOnlyShowCarft7";
            this.btnOnlyShowCarft7.Tag = "7";
            this.btnOnlyShowCarft7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft8
            // 
            this.btnOnlyShowCarft8.Caption = "只显示工艺8";
            this.btnOnlyShowCarft8.Id = 125;
            this.btnOnlyShowCarft8.Name = "btnOnlyShowCarft8";
            this.btnOnlyShowCarft8.Tag = "8";
            this.btnOnlyShowCarft8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft9
            // 
            this.btnOnlyShowCarft9.Caption = "只显示工艺9";
            this.btnOnlyShowCarft9.Id = 126;
            this.btnOnlyShowCarft9.Name = "btnOnlyShowCarft9";
            this.btnOnlyShowCarft9.Tag = "9";
            this.btnOnlyShowCarft9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft10
            // 
            this.btnOnlyShowCarft10.Caption = "只显示工艺10";
            this.btnOnlyShowCarft10.Id = 127;
            this.btnOnlyShowCarft10.Name = "btnOnlyShowCarft10";
            this.btnOnlyShowCarft10.Tag = "10";
            this.btnOnlyShowCarft10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft11
            // 
            this.btnOnlyShowCarft11.Caption = "只显示工艺11";
            this.btnOnlyShowCarft11.Id = 128;
            this.btnOnlyShowCarft11.Name = "btnOnlyShowCarft11";
            this.btnOnlyShowCarft11.Tag = "11";
            this.btnOnlyShowCarft11.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft12
            // 
            this.btnOnlyShowCarft12.Caption = "只显示工艺12";
            this.btnOnlyShowCarft12.Id = 129;
            this.btnOnlyShowCarft12.Name = "btnOnlyShowCarft12";
            this.btnOnlyShowCarft12.Tag = "12";
            this.btnOnlyShowCarft12.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft13
            // 
            this.btnOnlyShowCarft13.Caption = "只显示工艺13";
            this.btnOnlyShowCarft13.Id = 130;
            this.btnOnlyShowCarft13.Name = "btnOnlyShowCarft13";
            this.btnOnlyShowCarft13.Tag = "13";
            this.btnOnlyShowCarft13.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft14
            // 
            this.btnOnlyShowCarft14.Caption = "只显示工艺14";
            this.btnOnlyShowCarft14.Id = 131;
            this.btnOnlyShowCarft14.Name = "btnOnlyShowCarft14";
            this.btnOnlyShowCarft14.Tag = "14";
            this.btnOnlyShowCarft14.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft15
            // 
            this.btnOnlyShowCarft15.Caption = "只显示工艺15";
            this.btnOnlyShowCarft15.Id = 132;
            this.btnOnlyShowCarft15.Name = "btnOnlyShowCarft15";
            this.btnOnlyShowCarft15.Tag = "15";
            this.btnOnlyShowCarft15.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnOnlyShowCarft16
            // 
            this.btnOnlyShowCarft16.Caption = "只显示工艺16";
            this.btnOnlyShowCarft16.Id = 133;
            this.btnOnlyShowCarft16.Name = "btnOnlyShowCarft16";
            this.btnOnlyShowCarft16.Tag = "16";
            this.btnOnlyShowCarft16.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOnlyShowCarft_ItemClick);
            // 
            // btnLockFigureLayerItem
            // 
            this.btnLockFigureLayerItem.Caption = "锁定图层";
            this.btnLockFigureLayerItem.Id = 90;
            this.btnLockFigureLayerItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLockFigureLayerItem.ImageOptions.Image")));
            this.btnLockFigureLayerItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnLockFigureLayerItem.ImageOptions.LargeImage")));
            this.btnLockFigureLayerItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarftBackground),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft1),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft2),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft3),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft4),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft5),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft6),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft7),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft8),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft9),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft10),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft11),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft12),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft13),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft14),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft15),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckLockCarft16)});
            this.btnLockFigureLayerItem.Name = "btnLockFigureLayerItem";
            this.btnLockFigureLayerItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // ckLockCarftBackground
            // 
            this.ckLockCarftBackground.Caption = "锁定背景";
            this.ckLockCarftBackground.Id = 134;
            this.ckLockCarftBackground.Name = "ckLockCarftBackground";
            // 
            // ckLockCarft1
            // 
            this.ckLockCarft1.Caption = "锁定工艺1";
            this.ckLockCarft1.Id = 135;
            this.ckLockCarft1.Name = "ckLockCarft1";
            this.ckLockCarft1.Tag = "1";
            this.ckLockCarft1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft2
            // 
            this.ckLockCarft2.Caption = "锁定工艺2";
            this.ckLockCarft2.Id = 136;
            this.ckLockCarft2.Name = "ckLockCarft2";
            this.ckLockCarft2.Tag = "2";
            this.ckLockCarft2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft3
            // 
            this.ckLockCarft3.Caption = "锁定工艺3";
            this.ckLockCarft3.Id = 137;
            this.ckLockCarft3.Name = "ckLockCarft3";
            this.ckLockCarft3.Tag = "3";
            this.ckLockCarft3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft4
            // 
            this.ckLockCarft4.Caption = "锁定工艺4";
            this.ckLockCarft4.Id = 138;
            this.ckLockCarft4.Name = "ckLockCarft4";
            this.ckLockCarft4.Tag = "4";
            this.ckLockCarft4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft5
            // 
            this.ckLockCarft5.Caption = "锁定工艺5";
            this.ckLockCarft5.Id = 139;
            this.ckLockCarft5.Name = "ckLockCarft5";
            this.ckLockCarft5.Tag = "5";
            this.ckLockCarft5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft6
            // 
            this.ckLockCarft6.Caption = "锁定工艺6";
            this.ckLockCarft6.Id = 140;
            this.ckLockCarft6.Name = "ckLockCarft6";
            this.ckLockCarft6.Tag = "6";
            this.ckLockCarft6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft7
            // 
            this.ckLockCarft7.Caption = "锁定工艺7";
            this.ckLockCarft7.Id = 141;
            this.ckLockCarft7.Name = "ckLockCarft7";
            this.ckLockCarft7.Tag = "7";
            this.ckLockCarft7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft8
            // 
            this.ckLockCarft8.Caption = "锁定工艺8";
            this.ckLockCarft8.Id = 142;
            this.ckLockCarft8.Name = "ckLockCarft8";
            this.ckLockCarft8.Tag = "8";
            this.ckLockCarft8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft9
            // 
            this.ckLockCarft9.Caption = "锁定工艺9";
            this.ckLockCarft9.Id = 143;
            this.ckLockCarft9.Name = "ckLockCarft9";
            this.ckLockCarft9.Tag = "9";
            this.ckLockCarft9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft10
            // 
            this.ckLockCarft10.Caption = "锁定工艺10";
            this.ckLockCarft10.Id = 144;
            this.ckLockCarft10.Name = "ckLockCarft10";
            this.ckLockCarft10.Tag = "10";
            this.ckLockCarft10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft11
            // 
            this.ckLockCarft11.Caption = "锁定工艺11";
            this.ckLockCarft11.Id = 145;
            this.ckLockCarft11.Name = "ckLockCarft11";
            this.ckLockCarft11.Tag = "11";
            this.ckLockCarft11.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft12
            // 
            this.ckLockCarft12.Caption = "锁定工艺12";
            this.ckLockCarft12.Id = 146;
            this.ckLockCarft12.Name = "ckLockCarft12";
            this.ckLockCarft12.Tag = "12";
            this.ckLockCarft12.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft13
            // 
            this.ckLockCarft13.Caption = "锁定工艺13";
            this.ckLockCarft13.Id = 147;
            this.ckLockCarft13.Name = "ckLockCarft13";
            this.ckLockCarft13.Tag = "13";
            this.ckLockCarft13.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft14
            // 
            this.ckLockCarft14.Caption = "锁定工艺14";
            this.ckLockCarft14.Id = 148;
            this.ckLockCarft14.Name = "ckLockCarft14";
            this.ckLockCarft14.Tag = "14";
            this.ckLockCarft14.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft15
            // 
            this.ckLockCarft15.Caption = "锁定工艺15";
            this.ckLockCarft15.Id = 149;
            this.ckLockCarft15.Name = "ckLockCarft15";
            this.ckLockCarft15.Tag = "15";
            this.ckLockCarft15.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // ckLockCarft16
            // 
            this.ckLockCarft16.Caption = "锁定工艺16";
            this.ckLockCarft16.Id = 150;
            this.ckLockCarft16.Name = "ckLockCarft16";
            this.ckLockCarft16.Tag = "16";
            this.ckLockCarft16.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLockCarft_ItemClick);
            // 
            // btnDXFFigureMap
            // 
            this.btnDXFFigureMap.Caption = "DXF图形映射";
            this.btnDXFFigureMap.Id = 144;
            this.btnDXFFigureMap.Name = "btnDXFFigureMap";
            this.btnDXFFigureMap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnDXFFigureMap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDXFShowCarft_ItemClick);
            // 
            // btnRectangleItem
            // 
            this.btnRectangleItem.Caption = "矩形";
            this.btnRectangleItem.Id = 63;
            this.btnRectangleItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRectangleItem.ImageOptions.Image")));
            this.btnRectangleItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRectangleItem.ImageOptions.LargeImage")));
            this.btnRectangleItem.ItemLinks.Add(this.btnRectangle);
            this.btnRectangleItem.ItemLinks.Add(this.btnFilletRectangle);
            this.btnRectangleItem.ItemLinks.Add(this.btnTrackRectangle);
            this.btnRectangleItem.Name = "btnRectangleItem";
            this.btnRectangleItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRectangleItem_ItemClick);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Caption = "矩形";
            this.btnRectangle.Id = 151;
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRectangle_ItemClick);
            // 
            // btnFilletRectangle
            // 
            this.btnFilletRectangle.Caption = "圆角矩形";
            this.btnFilletRectangle.Id = 152;
            this.btnFilletRectangle.Name = "btnFilletRectangle";
            this.btnFilletRectangle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFilletRectangle_ItemClick);
            // 
            // btnTrackRectangle
            // 
            this.btnTrackRectangle.Caption = "跑道形";
            this.btnTrackRectangle.Id = 153;
            this.btnTrackRectangle.Name = "btnTrackRectangle";
            this.btnTrackRectangle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTrackRectangle_ItemClick);
            // 
            // btnCircleItem
            // 
            this.btnCircleItem.Caption = "圆";
            this.btnCircleItem.Id = 64;
            this.btnCircleItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCircleItem.ImageOptions.Image")));
            this.btnCircleItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCircleItem.ImageOptions.LargeImage")));
            this.btnCircleItem.ItemLinks.Add(this.btnCircle);
            this.btnCircleItem.ItemLinks.Add(this.btnThreePointArc);
            this.btnCircleItem.ItemLinks.Add(this.btnScanArc);
            this.btnCircleItem.ItemLinks.Add(this.btnNewEllipse);
            this.btnCircleItem.ItemLinks.Add(this.btnCircleReplaceToAcnode);
            this.btnCircleItem.ItemLinks.Add(this.btnReplaceToCircle);
            this.btnCircleItem.Name = "btnCircleItem";
            this.btnCircleItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCircleItem_ItemClick);
            // 
            // btnCircle
            // 
            this.btnCircle.Caption = "整圆";
            this.btnCircle.Id = 154;
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCircle_ItemClick);
            // 
            // btnThreePointArc
            // 
            this.btnThreePointArc.Caption = "三点圆弧";
            this.btnThreePointArc.Id = 155;
            this.btnThreePointArc.Name = "btnThreePointArc";
            this.btnThreePointArc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThreePointArc_ItemClick);
            // 
            // btnScanArc
            // 
            this.btnScanArc.Caption = "扫描式圆弧";
            this.btnScanArc.Id = 156;
            this.btnScanArc.Name = "btnScanArc";
            this.btnScanArc.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnScanArc_ItemClick);
            // 
            // btnNewEllipse
            // 
            this.btnNewEllipse.Caption = "新椭圆";
            this.btnNewEllipse.Id = 157;
            this.btnNewEllipse.Name = "btnNewEllipse";
            this.btnNewEllipse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNewEllipse_ItemClick);
            // 
            // btnCircleReplaceToAcnode
            // 
            this.btnCircleReplaceToAcnode.Caption = "替换圆形定位孔为孤立点";
            this.btnCircleReplaceToAcnode.Id = 158;
            this.btnCircleReplaceToAcnode.Name = "btnCircleReplaceToAcnode";
            this.btnCircleReplaceToAcnode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCircleReplaceToAcnode_ItemClick);
            // 
            // btnReplaceToCircle
            // 
            this.btnReplaceToCircle.Caption = "替换为圆";
            this.btnReplaceToCircle.Id = 159;
            this.btnReplaceToCircle.Name = "btnReplaceToCircle";
            this.btnReplaceToCircle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReplaceToCircle_ItemClick);
            // 
            // btnMultiLineItem
            // 
            this.btnMultiLineItem.Caption = "多段线";
            this.btnMultiLineItem.Id = 65;
            this.btnMultiLineItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMultiLineItem.ImageOptions.Image")));
            this.btnMultiLineItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnMultiLineItem.ImageOptions.LargeImage")));
            this.btnMultiLineItem.ItemLinks.Add(this.btnMultiLine);
            this.btnMultiLineItem.ItemLinks.Add(this.btnPolygon);
            this.btnMultiLineItem.ItemLinks.Add(this.btnStellate);
            this.btnMultiLineItem.Name = "btnMultiLineItem";
            this.btnMultiLineItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMultiLineItem_ItemClick);
            // 
            // btnMultiLine
            // 
            this.btnMultiLine.Caption = "多线段";
            this.btnMultiLine.Id = 164;
            this.btnMultiLine.Name = "btnMultiLine";
            this.btnMultiLine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMultiLine_ItemClick);
            // 
            // btnPolygon
            // 
            this.btnPolygon.Caption = "多边形";
            this.btnPolygon.Id = 165;
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPolygon_ItemClick);
            // 
            // btnStellate
            // 
            this.btnStellate.Caption = "星形";
            this.btnStellate.Id = 166;
            this.btnStellate.Name = "btnStellate";
            this.btnStellate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStellate_ItemClick);
            // 
            // btnDot
            // 
            this.btnDot.Caption = "单点";
            this.btnDot.Id = 66;
            this.btnDot.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDot.ImageOptions.Image")));
            this.btnDot.Name = "btnDot";
            this.btnDot.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDot_ItemClick);
            // 
            // btnWord
            // 
            this.btnWord.Caption = "文字";
            this.btnWord.Id = 67;
            this.btnWord.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnWord.ImageOptions.Image")));
            this.btnWord.Name = "btnWord";
            this.btnWord.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnWord_ItemClick);
            // 
            // btnGroupItem
            // 
            this.btnGroupItem.Caption = "群组";
            this.btnGroupItem.Id = 68;
            this.btnGroupItem.ImageOptions.Image = global::WSX.WSXCut.Properties.Resources.group32;
            this.btnGroupItem.ImageOptions.LargeImage = global::WSX.WSXCut.Properties.Resources.group32;
            this.btnGroupItem.ItemLinks.Add(this.btnGroupSelectAll);
            this.btnGroupItem.ItemLinks.Add(this.btnGroupScatterSelected);
            this.btnGroupItem.ItemLinks.Add(this.btnGroupScatterAll);
            this.btnGroupItem.ItemLinks.Add(this.btnGroupBlowOpen);
            this.btnGroupItem.ItemLinks.Add(this.btnMultiContourCotangent);
            this.btnGroupItem.ItemLinks.Add(this.btnBlowOpenMultiContourCotangent);
            this.btnGroupItem.Name = "btnGroupItem";
            this.btnGroupItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnGroupItem.Tag = 1;
            this.btnGroupItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGroupItem_ItemClick);
            // 
            // btnGroupSelectAll
            // 
            this.btnGroupSelectAll.Caption = "选择所有群组";
            this.btnGroupSelectAll.Id = 167;
            this.btnGroupSelectAll.Name = "btnGroupSelectAll";
            this.btnGroupSelectAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGroupSelectAll_ItemClick);
            // 
            // btnGroupScatterSelected
            // 
            this.btnGroupScatterSelected.Caption = "打散选中群组";
            this.btnGroupScatterSelected.Id = 168;
            this.btnGroupScatterSelected.Name = "btnGroupScatterSelected";
            this.btnGroupScatterSelected.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGroupScatterSelected_ItemClick);
            // 
            // btnGroupScatterAll
            // 
            this.btnGroupScatterAll.Caption = "打散全部群组";
            this.btnGroupScatterAll.Id = 169;
            this.btnGroupScatterAll.Name = "btnGroupScatterAll";
            this.btnGroupScatterAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGroupScatterAll_ItemClick);
            // 
            // btnGroupBlowOpen
            // 
            this.btnGroupBlowOpen.Caption = "炸开图形";
            this.btnGroupBlowOpen.Id = 170;
            this.btnGroupBlowOpen.Name = "btnGroupBlowOpen";
            this.btnGroupBlowOpen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnGroupBlowOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGroupBlowOpen_ItemClick);
            // 
            // btnMultiContourCotangent
            // 
            this.btnMultiContourCotangent.Caption = "多轮廓共切群组";
            this.btnMultiContourCotangent.Id = 171;
            this.btnMultiContourCotangent.Name = "btnMultiContourCotangent";
            this.btnMultiContourCotangent.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnMultiContourCotangent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMultiContourCotangent_ItemClick);
            // 
            // btnBlowOpenMultiContourCotangent
            // 
            this.btnBlowOpenMultiContourCotangent.Caption = "炸开多轮廓共切群组";
            this.btnBlowOpenMultiContourCotangent.Id = 172;
            this.btnBlowOpenMultiContourCotangent.Name = "btnBlowOpenMultiContourCotangent";
            this.btnBlowOpenMultiContourCotangent.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnBlowOpenMultiContourCotangent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBlowOpenMultiContourCotangent_ItemClick);
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "模拟速度";
            this.barEditItem1.Edit = null;
            this.barEditItem1.Id = 78;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // btnFindEdgeItem
            // 
            this.btnFindEdgeItem.Caption = "寻边";
            this.btnFindEdgeItem.Id = 85;
            this.btnFindEdgeItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFindEdgeItem.ImageOptions.Image")));
            this.btnFindEdgeItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnFindEdgeItem.ImageOptions.LargeImage")));
            this.btnFindEdgeItem.ItemLinks.Add(this.ckCapacitanceEdge);
            this.btnFindEdgeItem.ItemLinks.Add(this.ckDotEdge);
            this.btnFindEdgeItem.ItemLinks.Add(this.ckManualEdge);
            this.btnFindEdgeItem.ItemLinks.Add(this.ckAutoClearEdgeProcessed);
            this.btnFindEdgeItem.ItemLinks.Add(this.ckClearEdgeAngle);
            this.btnFindEdgeItem.Name = "btnFindEdgeItem";
            this.btnFindEdgeItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnFindEdgeItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFindEdgeItem_ItemClick);
            // 
            // ckCapacitanceEdge
            // 
            this.ckCapacitanceEdge.Caption = "电容寻边";
            this.ckCapacitanceEdge.GroupIndex = 1;
            this.ckCapacitanceEdge.Id = 224;
            this.ckCapacitanceEdge.Name = "ckCapacitanceEdge";
            this.ckCapacitanceEdge.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ckCapacitanceEdge_ItemClick);
            // 
            // ckDotEdge
            // 
            this.ckDotEdge.Caption = "光电寻边";
            this.ckDotEdge.GroupIndex = 1;
            this.ckDotEdge.Id = 226;
            this.ckDotEdge.Name = "ckDotEdge";
            this.ckDotEdge.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ckDotEdge_ItemClick);
            // 
            // ckManualEdge
            // 
            this.ckManualEdge.Caption = "手动寻边";
            this.ckManualEdge.GroupIndex = 1;
            this.ckManualEdge.Id = 227;
            this.ckManualEdge.Name = "ckManualEdge";
            this.ckManualEdge.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ckManualEdge_ItemClick);
            // 
            // ckAutoClearEdgeProcessed
            // 
            this.ckAutoClearEdgeProcessed.Caption = "加工完自动清除寻边角度";
            this.ckAutoClearEdgeProcessed.Id = 228;
            this.ckAutoClearEdgeProcessed.Name = "ckAutoClearEdgeProcessed";
            this.ckAutoClearEdgeProcessed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ckAutoClearEdgeProcessed_ItemClick);
            // 
            // ckClearEdgeAngle
            // 
            this.ckClearEdgeAngle.Caption = "清除寻边角度";
            this.ckClearEdgeAngle.Id = 229;
            this.ckClearEdgeAngle.Name = "ckClearEdgeAngle";
            this.ckClearEdgeAngle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ckClearEdgeAngle_ItemClick);
            // 
            // btnPLCProcessItem
            // 
            this.btnPLCProcessItem.Caption = "PLC过程";
            this.btnPLCProcessItem.Id = 86;
            this.btnPLCProcessItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPLCProcessItem.ImageOptions.Image")));
            this.btnPLCProcessItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnPLCProcessItem.ImageOptions.LargeImage")));
            this.btnPLCProcessItem.ItemLinks.Add(this.btnEditPLCProcess);
            this.btnPLCProcessItem.ItemLinks.Add(this.btnExecutePLCProcess);
            this.btnPLCProcessItem.Name = "btnPLCProcessItem";
            this.btnPLCProcessItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnPLCProcessItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPLCProcessItem_ItemClick);
            // 
            // btnEditPLCProcess
            // 
            this.btnEditPLCProcess.Caption = "编辑PLC过程";
            this.btnEditPLCProcess.Id = 230;
            this.btnEditPLCProcess.Name = "btnEditPLCProcess";
            this.btnEditPLCProcess.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEditPLCProcess_ItemClick);
            // 
            // btnExecutePLCProcess
            // 
            this.btnExecutePLCProcess.Caption = "执行PLC过程";
            this.btnExecutePLCProcess.Id = 231;
            this.btnExecutePLCProcess.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnChangeToWorkbenchA),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnChangeToWorkbenchB),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess1),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess2),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess3),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess4),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess5),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess6),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess7),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess8),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess9),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess10),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess11),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess12),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess13),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess14),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess15),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess16),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess17),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess18),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess19),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCustomProcess20)});
            this.btnExecutePLCProcess.Name = "btnExecutePLCProcess";
            // 
            // btnChangeToWorkbenchA
            // 
            this.btnChangeToWorkbenchA.Caption = "切换到工作台A";
            this.btnChangeToWorkbenchA.Id = 233;
            this.btnChangeToWorkbenchA.Name = "btnChangeToWorkbenchA";
            this.btnChangeToWorkbenchA.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChangeToWorkbenchA_ItemClick);
            // 
            // btnChangeToWorkbenchB
            // 
            this.btnChangeToWorkbenchB.Caption = "切换到工作台B";
            this.btnChangeToWorkbenchB.Id = 234;
            this.btnChangeToWorkbenchB.Name = "btnChangeToWorkbenchB";
            this.btnChangeToWorkbenchB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChangeToWorkbenchB_ItemClick);
            // 
            // btnCustomProcess1
            // 
            this.btnCustomProcess1.Caption = "自定义过程1";
            this.btnCustomProcess1.Id = 235;
            this.btnCustomProcess1.Name = "btnCustomProcess1";
            this.btnCustomProcess1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess1_ItemClick);
            // 
            // btnCustomProcess2
            // 
            this.btnCustomProcess2.Caption = "自定义过程2";
            this.btnCustomProcess2.Id = 236;
            this.btnCustomProcess2.Name = "btnCustomProcess2";
            this.btnCustomProcess2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess2_ItemClick);
            // 
            // btnCustomProcess3
            // 
            this.btnCustomProcess3.Caption = "自定义过程3";
            this.btnCustomProcess3.Id = 237;
            this.btnCustomProcess3.Name = "btnCustomProcess3";
            this.btnCustomProcess3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess3_ItemClick);
            // 
            // btnCustomProcess4
            // 
            this.btnCustomProcess4.Caption = "自定义过程4";
            this.btnCustomProcess4.Id = 238;
            this.btnCustomProcess4.Name = "btnCustomProcess4";
            this.btnCustomProcess4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess4_ItemClick);
            // 
            // btnCustomProcess5
            // 
            this.btnCustomProcess5.Caption = "自定义过程5";
            this.btnCustomProcess5.Id = 239;
            this.btnCustomProcess5.Name = "btnCustomProcess5";
            this.btnCustomProcess5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess5_ItemClick);
            // 
            // btnCustomProcess6
            // 
            this.btnCustomProcess6.Caption = "自定义过程6";
            this.btnCustomProcess6.Id = 240;
            this.btnCustomProcess6.Name = "btnCustomProcess6";
            this.btnCustomProcess6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess6_ItemClick);
            // 
            // btnCustomProcess7
            // 
            this.btnCustomProcess7.Caption = "自定义过程7";
            this.btnCustomProcess7.Id = 241;
            this.btnCustomProcess7.Name = "btnCustomProcess7";
            this.btnCustomProcess7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess7_ItemClick);
            // 
            // btnCustomProcess8
            // 
            this.btnCustomProcess8.Caption = "自定义过程8";
            this.btnCustomProcess8.Id = 242;
            this.btnCustomProcess8.Name = "btnCustomProcess8";
            this.btnCustomProcess8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess8_ItemClick);
            // 
            // btnCustomProcess9
            // 
            this.btnCustomProcess9.Caption = "自定义过程9";
            this.btnCustomProcess9.Id = 243;
            this.btnCustomProcess9.Name = "btnCustomProcess9";
            this.btnCustomProcess9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess9_ItemClick);
            // 
            // btnCustomProcess10
            // 
            this.btnCustomProcess10.Caption = "自定义过程10";
            this.btnCustomProcess10.Id = 244;
            this.btnCustomProcess10.Name = "btnCustomProcess10";
            this.btnCustomProcess10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess10_ItemClick);
            // 
            // btnCustomProcess11
            // 
            this.btnCustomProcess11.Caption = "自定义过程11";
            this.btnCustomProcess11.Id = 245;
            this.btnCustomProcess11.Name = "btnCustomProcess11";
            this.btnCustomProcess11.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess11_ItemClick);
            // 
            // btnCustomProcess12
            // 
            this.btnCustomProcess12.Caption = "自定义过程12";
            this.btnCustomProcess12.Id = 246;
            this.btnCustomProcess12.Name = "btnCustomProcess12";
            this.btnCustomProcess12.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess12_ItemClick);
            // 
            // btnCustomProcess13
            // 
            this.btnCustomProcess13.Caption = "自定义过程13";
            this.btnCustomProcess13.Id = 247;
            this.btnCustomProcess13.Name = "btnCustomProcess13";
            this.btnCustomProcess13.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess13_ItemClick);
            // 
            // btnCustomProcess14
            // 
            this.btnCustomProcess14.Caption = "自定义过程14";
            this.btnCustomProcess14.Id = 248;
            this.btnCustomProcess14.Name = "btnCustomProcess14";
            this.btnCustomProcess14.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess14_ItemClick);
            // 
            // btnCustomProcess15
            // 
            this.btnCustomProcess15.Caption = "自定义过程15";
            this.btnCustomProcess15.Id = 249;
            this.btnCustomProcess15.Name = "btnCustomProcess15";
            this.btnCustomProcess15.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess15_ItemClick);
            // 
            // btnCustomProcess16
            // 
            this.btnCustomProcess16.Caption = "自定义过程16";
            this.btnCustomProcess16.Id = 250;
            this.btnCustomProcess16.Name = "btnCustomProcess16";
            this.btnCustomProcess16.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess16_ItemClick);
            // 
            // btnCustomProcess17
            // 
            this.btnCustomProcess17.Caption = "自定义过程17";
            this.btnCustomProcess17.Id = 251;
            this.btnCustomProcess17.Name = "btnCustomProcess17";
            this.btnCustomProcess17.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess17_ItemClick);
            // 
            // btnCustomProcess18
            // 
            this.btnCustomProcess18.Caption = "自定义过程18";
            this.btnCustomProcess18.Id = 252;
            this.btnCustomProcess18.Name = "btnCustomProcess18";
            this.btnCustomProcess18.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess18_ItemClick);
            // 
            // btnCustomProcess19
            // 
            this.btnCustomProcess19.Caption = "自定义过程19";
            this.btnCustomProcess19.Id = 253;
            this.btnCustomProcess19.Name = "btnCustomProcess19";
            this.btnCustomProcess19.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess19_ItemClick);
            // 
            // btnCustomProcess20
            // 
            this.btnCustomProcess20.Caption = "自定义过程20";
            this.btnCustomProcess20.Id = 254;
            this.btnCustomProcess20.Name = "btnCustomProcess20";
            this.btnCustomProcess20.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCustomProcess20_ItemClick);
            // 
            // btnGoOriginItem
            // 
            this.btnGoOriginItem.Caption = "回原点";
            this.btnGoOriginItem.Id = 88;
            this.btnGoOriginItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGoOriginItem.ImageOptions.Image")));
            this.btnGoOriginItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnGoOriginItem.ImageOptions.LargeImage")));
            this.btnGoOriginItem.ItemLinks.Add(this.btnGoOriginAll);
            this.btnGoOriginItem.ItemLinks.Add(this.btnGoOriginX);
            this.btnGoOriginItem.ItemLinks.Add(this.btnGoOriginY);
            this.btnGoOriginItem.ItemLinks.Add(this.btnGantryInit);
            this.btnGoOriginItem.ItemLinks.Add(this.ckExecuteGantrySyncGoOrigin);
            this.btnGoOriginItem.ItemLinks.Add(this.ckAdjustHeightenGoOrigin);
            this.btnGoOriginItem.ItemLinks.Add(this.ckElectricFocusgoOrigin);
            this.btnGoOriginItem.Name = "btnGoOriginItem";
            this.btnGoOriginItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGoOriginItem_ItemClick);
            // 
            // btnGoOriginAll
            // 
            this.btnGoOriginAll.Caption = "全部回原点";
            this.btnGoOriginAll.Id = 256;
            this.btnGoOriginAll.Name = "btnGoOriginAll";
            this.btnGoOriginAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGoOriginAll_ItemClick);
            // 
            // btnGoOriginX
            // 
            this.btnGoOriginX.Caption = "X轴回原点";
            this.btnGoOriginX.Id = 257;
            this.btnGoOriginX.Name = "btnGoOriginX";
            this.btnGoOriginX.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGoOriginX_ItemClick);
            // 
            // btnGoOriginY
            // 
            this.btnGoOriginY.Caption = "Y轴回原点";
            this.btnGoOriginY.Id = 258;
            this.btnGoOriginY.Name = "btnGoOriginY";
            this.btnGoOriginY.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGoOriginY_ItemClick);
            // 
            // btnGantryInit
            // 
            this.btnGantryInit.Caption = "龙门初始化";
            this.btnGantryInit.Id = 259;
            this.btnGantryInit.Name = "btnGantryInit";
            this.btnGantryInit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGantryInit_ItemClick);
            // 
            // ckExecuteGantrySyncGoOrigin
            // 
            this.ckExecuteGantrySyncGoOrigin.Caption = "回原点时执行龙门同步";
            this.ckExecuteGantrySyncGoOrigin.Id = 260;
            this.ckExecuteGantrySyncGoOrigin.Name = "ckExecuteGantrySyncGoOrigin";
            this.ckExecuteGantrySyncGoOrigin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ckExecuteGantrySyncGoOrigin_ItemClick);
            // 
            // ckAdjustHeightenGoOrigin
            // 
            this.ckAdjustHeightenGoOrigin.Caption = "调高器先回原点";
            this.ckAdjustHeightenGoOrigin.Id = 261;
            this.ckAdjustHeightenGoOrigin.Name = "ckAdjustHeightenGoOrigin";
            this.ckAdjustHeightenGoOrigin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ckAdjustHeightenGoOrigin_ItemClick);
            // 
            // ckElectricFocusgoOrigin
            // 
            this.ckElectricFocusgoOrigin.Caption = "电动调焦也回原点";
            this.ckElectricFocusgoOrigin.Id = 262;
            this.ckElectricFocusgoOrigin.Name = "ckElectricFocusgoOrigin";
            this.ckElectricFocusgoOrigin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ckElectricFocusgoOrigin_ItemClick);
            // 
            // btnBCS100Item
            // 
            this.btnBCS100Item.Caption = "NT100";
            this.btnBCS100Item.Id = 89;
            this.btnBCS100Item.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBCS100Item.ImageOptions.Image")));
            this.btnBCS100Item.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBCS100Item.ImageOptions.LargeImage")));
            this.btnBCS100Item.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSStop),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSGoBerth),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSGoOrigin),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStopItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelativeItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCS100Monitor),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSKeyCalibration),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSRegisteredEncrypt),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSUpdate),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSSaveParams),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSRecordZCoordinate)});
            this.btnBCS100Item.Name = "btnBCS100Item";
            this.btnBCS100Item.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnBCSStop
            // 
            this.btnBCSStop.Caption = "停止（Hold）";
            this.btnBCSStop.Id = 264;
            this.btnBCSStop.Name = "btnBCSStop";
            this.btnBCSStop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSStop_ItemClick);
            // 
            // btnBCSGoBerth
            // 
            this.btnBCSGoBerth.Caption = "回停靠";
            this.btnBCSGoBerth.Id = 265;
            this.btnBCSGoBerth.Name = "btnBCSGoBerth";
            this.btnBCSGoBerth.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSGoBerth_ItemClick);
            // 
            // btnBCSGoOrigin
            // 
            this.btnBCSGoOrigin.Caption = "回原点";
            this.btnBCSGoOrigin.Id = 266;
            this.btnBCSGoOrigin.Name = "btnBCSGoOrigin";
            this.btnBCSGoOrigin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSGoOrigin_ItemClick);
            // 
            // btnBCSFollowItem
            // 
            this.btnBCSFollowItem.Caption = "跟随到";
            this.btnBCSFollowItem.Id = 267;
            this.btnBCSFollowItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollow1mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollow2mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollow3mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollow4mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollow5mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollow6mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollow7mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollow8mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollow9mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollow10mm)});
            this.btnBCSFollowItem.Name = "btnBCSFollowItem";
            // 
            // btnBCSFollow1mm
            // 
            this.btnBCSFollow1mm.Caption = "1mm";
            this.btnBCSFollow1mm.Id = 268;
            this.btnBCSFollow1mm.Name = "btnBCSFollow1mm";
            this.btnBCSFollow1mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollow1mm_ItemClick);
            // 
            // btnBCSFollow2mm
            // 
            this.btnBCSFollow2mm.Caption = "2mm";
            this.btnBCSFollow2mm.Id = 269;
            this.btnBCSFollow2mm.Name = "btnBCSFollow2mm";
            this.btnBCSFollow2mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollow2mm_ItemClick);
            // 
            // btnBCSFollow3mm
            // 
            this.btnBCSFollow3mm.Caption = "3mm";
            this.btnBCSFollow3mm.Id = 270;
            this.btnBCSFollow3mm.Name = "btnBCSFollow3mm";
            // 
            // btnBCSFollow4mm
            // 
            this.btnBCSFollow4mm.Caption = "4mm";
            this.btnBCSFollow4mm.Id = 271;
            this.btnBCSFollow4mm.Name = "btnBCSFollow4mm";
            this.btnBCSFollow4mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollow4mm_ItemClick);
            // 
            // btnBCSFollow5mm
            // 
            this.btnBCSFollow5mm.Caption = "5mm";
            this.btnBCSFollow5mm.Id = 272;
            this.btnBCSFollow5mm.Name = "btnBCSFollow5mm";
            this.btnBCSFollow5mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollow5mm_ItemClick);
            // 
            // btnBCSFollow6mm
            // 
            this.btnBCSFollow6mm.Caption = "6mm";
            this.btnBCSFollow6mm.Id = 273;
            this.btnBCSFollow6mm.Name = "btnBCSFollow6mm";
            this.btnBCSFollow6mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollow6mm_ItemClick);
            // 
            // btnBCSFollow7mm
            // 
            this.btnBCSFollow7mm.Caption = "7mm";
            this.btnBCSFollow7mm.Id = 274;
            this.btnBCSFollow7mm.Name = "btnBCSFollow7mm";
            this.btnBCSFollow7mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollow7mm_ItemClick);
            // 
            // btnBCSFollow8mm
            // 
            this.btnBCSFollow8mm.Caption = "8mm";
            this.btnBCSFollow8mm.Id = 275;
            this.btnBCSFollow8mm.Name = "btnBCSFollow8mm";
            this.btnBCSFollow8mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollow8mm_ItemClick);
            // 
            // btnBCSFollow9mm
            // 
            this.btnBCSFollow9mm.Caption = "9mm";
            this.btnBCSFollow9mm.Id = 276;
            this.btnBCSFollow9mm.Name = "btnBCSFollow9mm";
            this.btnBCSFollow9mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollow9mm_ItemClick);
            // 
            // btnBCSFollow10mm
            // 
            this.btnBCSFollow10mm.Caption = "10mm";
            this.btnBCSFollow10mm.Id = 277;
            this.btnBCSFollow10mm.Name = "btnBCSFollow10mm";
            this.btnBCSFollow10mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollow10mm_ItemClick);
            // 
            // btnBCSFollowStopItem
            // 
            this.btnBCSFollowStopItem.Caption = "跟随然后停止";
            this.btnBCSFollowStopItem.Id = 279;
            this.btnBCSFollowStopItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop1mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop2mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop3mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop4mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop5mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop6mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop7mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop8mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop9mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop10mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop15mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop20mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop25mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBCSFollowStop30mm)});
            this.btnBCSFollowStopItem.Name = "btnBCSFollowStopItem";
            // 
            // btnBCSFollowStop1mm
            // 
            this.btnBCSFollowStop1mm.Caption = "1mm";
            this.btnBCSFollowStop1mm.Id = 288;
            this.btnBCSFollowStop1mm.Name = "btnBCSFollowStop1mm";
            this.btnBCSFollowStop1mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop1mm_ItemClick);
            // 
            // btnBCSFollowStop2mm
            // 
            this.btnBCSFollowStop2mm.Caption = "2mm";
            this.btnBCSFollowStop2mm.Id = 289;
            this.btnBCSFollowStop2mm.Name = "btnBCSFollowStop2mm";
            this.btnBCSFollowStop2mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop2mm_ItemClick);
            // 
            // btnBCSFollowStop3mm
            // 
            this.btnBCSFollowStop3mm.Caption = "3mm";
            this.btnBCSFollowStop3mm.Id = 291;
            this.btnBCSFollowStop3mm.Name = "btnBCSFollowStop3mm";
            this.btnBCSFollowStop3mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop3mm_ItemClick);
            // 
            // btnBCSFollowStop4mm
            // 
            this.btnBCSFollowStop4mm.Caption = "4mm";
            this.btnBCSFollowStop4mm.Id = 292;
            this.btnBCSFollowStop4mm.Name = "btnBCSFollowStop4mm";
            this.btnBCSFollowStop4mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop4mm_ItemClick);
            // 
            // btnBCSFollowStop5mm
            // 
            this.btnBCSFollowStop5mm.Caption = "5mm";
            this.btnBCSFollowStop5mm.Id = 293;
            this.btnBCSFollowStop5mm.Name = "btnBCSFollowStop5mm";
            this.btnBCSFollowStop5mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop5mm_ItemClick);
            // 
            // btnBCSFollowStop6mm
            // 
            this.btnBCSFollowStop6mm.Caption = "6mm";
            this.btnBCSFollowStop6mm.Id = 295;
            this.btnBCSFollowStop6mm.Name = "btnBCSFollowStop6mm";
            this.btnBCSFollowStop6mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop6mm_ItemClick);
            // 
            // btnBCSFollowStop7mm
            // 
            this.btnBCSFollowStop7mm.Caption = "7mm";
            this.btnBCSFollowStop7mm.Id = 296;
            this.btnBCSFollowStop7mm.Name = "btnBCSFollowStop7mm";
            this.btnBCSFollowStop7mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop7mm_ItemClick);
            // 
            // btnBCSFollowStop8mm
            // 
            this.btnBCSFollowStop8mm.Caption = "8mm";
            this.btnBCSFollowStop8mm.Id = 297;
            this.btnBCSFollowStop8mm.Name = "btnBCSFollowStop8mm";
            this.btnBCSFollowStop8mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop8mm_ItemClick);
            // 
            // btnBCSFollowStop9mm
            // 
            this.btnBCSFollowStop9mm.Caption = "9mm";
            this.btnBCSFollowStop9mm.Id = 298;
            this.btnBCSFollowStop9mm.Name = "btnBCSFollowStop9mm";
            this.btnBCSFollowStop9mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop9mm_ItemClick);
            // 
            // btnBCSFollowStop10mm
            // 
            this.btnBCSFollowStop10mm.Caption = "10mm";
            this.btnBCSFollowStop10mm.Id = 299;
            this.btnBCSFollowStop10mm.Name = "btnBCSFollowStop10mm";
            this.btnBCSFollowStop10mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop10mm_ItemClick);
            // 
            // btnBCSFollowStop15mm
            // 
            this.btnBCSFollowStop15mm.Caption = "15mm";
            this.btnBCSFollowStop15mm.Id = 300;
            this.btnBCSFollowStop15mm.Name = "btnBCSFollowStop15mm";
            this.btnBCSFollowStop15mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop15mm_ItemClick);
            // 
            // btnBCSFollowStop20mm
            // 
            this.btnBCSFollowStop20mm.Caption = "20mm";
            this.btnBCSFollowStop20mm.Id = 301;
            this.btnBCSFollowStop20mm.Name = "btnBCSFollowStop20mm";
            this.btnBCSFollowStop20mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop20mm_ItemClick);
            // 
            // btnBCSFollowStop25mm
            // 
            this.btnBCSFollowStop25mm.Caption = "25mm";
            this.btnBCSFollowStop25mm.Id = 302;
            this.btnBCSFollowStop25mm.Name = "btnBCSFollowStop25mm";
            this.btnBCSFollowStop25mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop25mm_ItemClick);
            // 
            // btnBCSFollowStop30mm
            // 
            this.btnBCSFollowStop30mm.Caption = "30mm";
            this.btnBCSFollowStop30mm.Id = 303;
            this.btnBCSFollowStop30mm.Name = "btnBCSFollowStop30mm";
            this.btnBCSFollowStop30mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSFollowStop30mm_ItemClick);
            // 
            // btnEmptyMoveRelativeItem
            // 
            this.btnEmptyMoveRelativeItem.Caption = "相对空移";
            this.btnEmptyMoveRelativeItem.Id = 280;
            this.btnEmptyMoveRelativeItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelative1mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelative2mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelative3mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelative4mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelative5mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelative6mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelative7mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelative8mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelative9mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelative10mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelativeMinus1mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelativeMinus2mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelativeMinus3mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelativeMinus4mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelativeMinus5mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelativeMinus6mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelativeMinus7mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelativeMinus8mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelativeMinus9mm),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEmptyMoveRelativeMinus10mm)});
            this.btnEmptyMoveRelativeItem.Name = "btnEmptyMoveRelativeItem";
            // 
            // btnEmptyMoveRelative1mm
            // 
            this.btnEmptyMoveRelative1mm.Caption = "1mm↓";
            this.btnEmptyMoveRelative1mm.Id = 304;
            this.btnEmptyMoveRelative1mm.Name = "btnEmptyMoveRelative1mm";
            this.btnEmptyMoveRelative1mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelative1mm_ItemClick);
            // 
            // btnEmptyMoveRelative2mm
            // 
            this.btnEmptyMoveRelative2mm.Caption = "2mm↓";
            this.btnEmptyMoveRelative2mm.Id = 305;
            this.btnEmptyMoveRelative2mm.Name = "btnEmptyMoveRelative2mm";
            this.btnEmptyMoveRelative2mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelative2mm_ItemClick);
            // 
            // btnEmptyMoveRelative3mm
            // 
            this.btnEmptyMoveRelative3mm.Caption = "3mm↓";
            this.btnEmptyMoveRelative3mm.Id = 306;
            this.btnEmptyMoveRelative3mm.Name = "btnEmptyMoveRelative3mm";
            this.btnEmptyMoveRelative3mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelative3mm_ItemClick);
            // 
            // btnEmptyMoveRelative4mm
            // 
            this.btnEmptyMoveRelative4mm.Caption = "4mm↓";
            this.btnEmptyMoveRelative4mm.Id = 307;
            this.btnEmptyMoveRelative4mm.Name = "btnEmptyMoveRelative4mm";
            this.btnEmptyMoveRelative4mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelative4mm_ItemClick);
            // 
            // btnEmptyMoveRelative5mm
            // 
            this.btnEmptyMoveRelative5mm.Caption = "5mm↓";
            this.btnEmptyMoveRelative5mm.Id = 308;
            this.btnEmptyMoveRelative5mm.Name = "btnEmptyMoveRelative5mm";
            this.btnEmptyMoveRelative5mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelative5mm_ItemClick);
            // 
            // btnEmptyMoveRelative6mm
            // 
            this.btnEmptyMoveRelative6mm.Caption = "6mm↓";
            this.btnEmptyMoveRelative6mm.Id = 309;
            this.btnEmptyMoveRelative6mm.Name = "btnEmptyMoveRelative6mm";
            this.btnEmptyMoveRelative6mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelative6mm_ItemClick);
            // 
            // btnEmptyMoveRelative7mm
            // 
            this.btnEmptyMoveRelative7mm.Caption = "7mm↓";
            this.btnEmptyMoveRelative7mm.Id = 310;
            this.btnEmptyMoveRelative7mm.Name = "btnEmptyMoveRelative7mm";
            this.btnEmptyMoveRelative7mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelative7mm_ItemClick);
            // 
            // btnEmptyMoveRelative8mm
            // 
            this.btnEmptyMoveRelative8mm.Caption = "8mm↓";
            this.btnEmptyMoveRelative8mm.Id = 311;
            this.btnEmptyMoveRelative8mm.Name = "btnEmptyMoveRelative8mm";
            this.btnEmptyMoveRelative8mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelative8mm_ItemClick);
            // 
            // btnEmptyMoveRelative9mm
            // 
            this.btnEmptyMoveRelative9mm.Caption = "9mm↓";
            this.btnEmptyMoveRelative9mm.Id = 312;
            this.btnEmptyMoveRelative9mm.Name = "btnEmptyMoveRelative9mm";
            this.btnEmptyMoveRelative9mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelative9mm_ItemClick);
            // 
            // btnEmptyMoveRelative10mm
            // 
            this.btnEmptyMoveRelative10mm.Caption = "10mm↓";
            this.btnEmptyMoveRelative10mm.Id = 313;
            this.btnEmptyMoveRelative10mm.Name = "btnEmptyMoveRelative10mm";
            this.btnEmptyMoveRelative10mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelative10mm_ItemClick);
            // 
            // btnEmptyMoveRelativeMinus1mm
            // 
            this.btnEmptyMoveRelativeMinus1mm.Caption = "-1mm↑";
            this.btnEmptyMoveRelativeMinus1mm.Id = 314;
            this.btnEmptyMoveRelativeMinus1mm.Name = "btnEmptyMoveRelativeMinus1mm";
            this.btnEmptyMoveRelativeMinus1mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelativeMinus1mm_ItemClick);
            // 
            // btnEmptyMoveRelativeMinus2mm
            // 
            this.btnEmptyMoveRelativeMinus2mm.Caption = "-2mm↑";
            this.btnEmptyMoveRelativeMinus2mm.Id = 315;
            this.btnEmptyMoveRelativeMinus2mm.Name = "btnEmptyMoveRelativeMinus2mm";
            this.btnEmptyMoveRelativeMinus2mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelativeMinus2mm_ItemClick);
            // 
            // btnEmptyMoveRelativeMinus3mm
            // 
            this.btnEmptyMoveRelativeMinus3mm.Caption = "-3mm↑";
            this.btnEmptyMoveRelativeMinus3mm.Id = 316;
            this.btnEmptyMoveRelativeMinus3mm.Name = "btnEmptyMoveRelativeMinus3mm";
            this.btnEmptyMoveRelativeMinus3mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelativeMinus3mm_ItemClick);
            // 
            // btnEmptyMoveRelativeMinus4mm
            // 
            this.btnEmptyMoveRelativeMinus4mm.Caption = "-4mm↑";
            this.btnEmptyMoveRelativeMinus4mm.Id = 317;
            this.btnEmptyMoveRelativeMinus4mm.Name = "btnEmptyMoveRelativeMinus4mm";
            this.btnEmptyMoveRelativeMinus4mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelativeMinus4mm_ItemClick);
            // 
            // btnEmptyMoveRelativeMinus5mm
            // 
            this.btnEmptyMoveRelativeMinus5mm.Caption = "-5mm↑";
            this.btnEmptyMoveRelativeMinus5mm.Id = 318;
            this.btnEmptyMoveRelativeMinus5mm.Name = "btnEmptyMoveRelativeMinus5mm";
            this.btnEmptyMoveRelativeMinus5mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelativeMinus5mm_ItemClick);
            // 
            // btnEmptyMoveRelativeMinus6mm
            // 
            this.btnEmptyMoveRelativeMinus6mm.Caption = "-6mm↑";
            this.btnEmptyMoveRelativeMinus6mm.Id = 319;
            this.btnEmptyMoveRelativeMinus6mm.Name = "btnEmptyMoveRelativeMinus6mm";
            this.btnEmptyMoveRelativeMinus6mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelativeMinus6mm_ItemClick);
            // 
            // btnEmptyMoveRelativeMinus7mm
            // 
            this.btnEmptyMoveRelativeMinus7mm.Caption = "-7mm↑";
            this.btnEmptyMoveRelativeMinus7mm.Id = 320;
            this.btnEmptyMoveRelativeMinus7mm.Name = "btnEmptyMoveRelativeMinus7mm";
            this.btnEmptyMoveRelativeMinus7mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelativeMinus7mm_ItemClick);
            // 
            // btnEmptyMoveRelativeMinus8mm
            // 
            this.btnEmptyMoveRelativeMinus8mm.Caption = "-8mm↑";
            this.btnEmptyMoveRelativeMinus8mm.Id = 321;
            this.btnEmptyMoveRelativeMinus8mm.Name = "btnEmptyMoveRelativeMinus8mm";
            this.btnEmptyMoveRelativeMinus8mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelativeMinus8mm_ItemClick);
            // 
            // btnEmptyMoveRelativeMinus9mm
            // 
            this.btnEmptyMoveRelativeMinus9mm.Caption = "-9mm↑";
            this.btnEmptyMoveRelativeMinus9mm.Id = 322;
            this.btnEmptyMoveRelativeMinus9mm.Name = "btnEmptyMoveRelativeMinus9mm";
            this.btnEmptyMoveRelativeMinus9mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelativeMinus9mm_ItemClick);
            // 
            // btnEmptyMoveRelativeMinus10mm
            // 
            this.btnEmptyMoveRelativeMinus10mm.Caption = "-10mm↑";
            this.btnEmptyMoveRelativeMinus10mm.Id = 323;
            this.btnEmptyMoveRelativeMinus10mm.Name = "btnEmptyMoveRelativeMinus10mm";
            this.btnEmptyMoveRelativeMinus10mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEmptyMoveRelativeMinus10mm_ItemClick);
            // 
            // btnMoveAbsolutePositionItem
            // 
            this.btnMoveAbsolutePositionItem.Caption = "绝对定位";
            this.btnMoveAbsolutePositionItem.Id = 281;
            this.btnMoveAbsolutePositionItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ0),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ1),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ2),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ3),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ4),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ5),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ6),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ7),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ8),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ9),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ10),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ15),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ20),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ25),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ30),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ40),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMoveAbsolutePositionZ50)});
            this.btnMoveAbsolutePositionItem.Name = "btnMoveAbsolutePositionItem";
            // 
            // btnMoveAbsolutePositionZ0
            // 
            this.btnMoveAbsolutePositionZ0.Caption = "Z=0";
            this.btnMoveAbsolutePositionZ0.Id = 324;
            this.btnMoveAbsolutePositionZ0.Name = "btnMoveAbsolutePositionZ0";
            this.btnMoveAbsolutePositionZ0.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ0_ItemClick);
            // 
            // btnMoveAbsolutePositionZ1
            // 
            this.btnMoveAbsolutePositionZ1.Caption = "Z=1";
            this.btnMoveAbsolutePositionZ1.Id = 325;
            this.btnMoveAbsolutePositionZ1.Name = "btnMoveAbsolutePositionZ1";
            this.btnMoveAbsolutePositionZ1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ1_ItemClick);
            // 
            // btnMoveAbsolutePositionZ2
            // 
            this.btnMoveAbsolutePositionZ2.Caption = "Z=2";
            this.btnMoveAbsolutePositionZ2.Id = 326;
            this.btnMoveAbsolutePositionZ2.Name = "btnMoveAbsolutePositionZ2";
            this.btnMoveAbsolutePositionZ2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ2_ItemClick);
            // 
            // btnMoveAbsolutePositionZ3
            // 
            this.btnMoveAbsolutePositionZ3.Caption = "Z=3";
            this.btnMoveAbsolutePositionZ3.Id = 327;
            this.btnMoveAbsolutePositionZ3.Name = "btnMoveAbsolutePositionZ3";
            this.btnMoveAbsolutePositionZ3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ3_ItemClick);
            // 
            // btnMoveAbsolutePositionZ4
            // 
            this.btnMoveAbsolutePositionZ4.Caption = "Z=4";
            this.btnMoveAbsolutePositionZ4.Id = 328;
            this.btnMoveAbsolutePositionZ4.Name = "btnMoveAbsolutePositionZ4";
            this.btnMoveAbsolutePositionZ4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ4_ItemClick);
            // 
            // btnMoveAbsolutePositionZ5
            // 
            this.btnMoveAbsolutePositionZ5.Caption = "Z=5";
            this.btnMoveAbsolutePositionZ5.Id = 329;
            this.btnMoveAbsolutePositionZ5.Name = "btnMoveAbsolutePositionZ5";
            this.btnMoveAbsolutePositionZ5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ5_ItemClick);
            // 
            // btnMoveAbsolutePositionZ6
            // 
            this.btnMoveAbsolutePositionZ6.Caption = "Z=6";
            this.btnMoveAbsolutePositionZ6.Id = 330;
            this.btnMoveAbsolutePositionZ6.Name = "btnMoveAbsolutePositionZ6";
            this.btnMoveAbsolutePositionZ6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ6_ItemClick);
            // 
            // btnMoveAbsolutePositionZ7
            // 
            this.btnMoveAbsolutePositionZ7.Caption = "Z=7";
            this.btnMoveAbsolutePositionZ7.Id = 331;
            this.btnMoveAbsolutePositionZ7.Name = "btnMoveAbsolutePositionZ7";
            this.btnMoveAbsolutePositionZ7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ7_ItemClick);
            // 
            // btnMoveAbsolutePositionZ8
            // 
            this.btnMoveAbsolutePositionZ8.Caption = "Z=8";
            this.btnMoveAbsolutePositionZ8.Id = 332;
            this.btnMoveAbsolutePositionZ8.Name = "btnMoveAbsolutePositionZ8";
            this.btnMoveAbsolutePositionZ8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ8_ItemClick);
            // 
            // btnMoveAbsolutePositionZ9
            // 
            this.btnMoveAbsolutePositionZ9.Caption = "Z=9";
            this.btnMoveAbsolutePositionZ9.Id = 333;
            this.btnMoveAbsolutePositionZ9.Name = "btnMoveAbsolutePositionZ9";
            this.btnMoveAbsolutePositionZ9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ9_ItemClick);
            // 
            // btnMoveAbsolutePositionZ10
            // 
            this.btnMoveAbsolutePositionZ10.Caption = "Z=10";
            this.btnMoveAbsolutePositionZ10.Id = 334;
            this.btnMoveAbsolutePositionZ10.Name = "btnMoveAbsolutePositionZ10";
            this.btnMoveAbsolutePositionZ10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ10_ItemClick);
            // 
            // btnMoveAbsolutePositionZ15
            // 
            this.btnMoveAbsolutePositionZ15.Caption = "Z=15";
            this.btnMoveAbsolutePositionZ15.Id = 335;
            this.btnMoveAbsolutePositionZ15.Name = "btnMoveAbsolutePositionZ15";
            this.btnMoveAbsolutePositionZ15.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ15_ItemClick);
            // 
            // btnMoveAbsolutePositionZ20
            // 
            this.btnMoveAbsolutePositionZ20.Caption = "Z=20";
            this.btnMoveAbsolutePositionZ20.Id = 336;
            this.btnMoveAbsolutePositionZ20.Name = "btnMoveAbsolutePositionZ20";
            this.btnMoveAbsolutePositionZ20.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ20_ItemClick);
            // 
            // btnMoveAbsolutePositionZ25
            // 
            this.btnMoveAbsolutePositionZ25.Caption = "Z=25";
            this.btnMoveAbsolutePositionZ25.Id = 337;
            this.btnMoveAbsolutePositionZ25.Name = "btnMoveAbsolutePositionZ25";
            this.btnMoveAbsolutePositionZ25.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ25_ItemClick);
            // 
            // btnMoveAbsolutePositionZ30
            // 
            this.btnMoveAbsolutePositionZ30.Caption = "Z=30";
            this.btnMoveAbsolutePositionZ30.Id = 338;
            this.btnMoveAbsolutePositionZ30.Name = "btnMoveAbsolutePositionZ30";
            this.btnMoveAbsolutePositionZ30.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ30_ItemClick);
            // 
            // btnMoveAbsolutePositionZ40
            // 
            this.btnMoveAbsolutePositionZ40.Caption = "Z=40";
            this.btnMoveAbsolutePositionZ40.Id = 339;
            this.btnMoveAbsolutePositionZ40.Name = "btnMoveAbsolutePositionZ40";
            this.btnMoveAbsolutePositionZ40.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ40_ItemClick);
            // 
            // btnMoveAbsolutePositionZ50
            // 
            this.btnMoveAbsolutePositionZ50.Caption = "Z=50";
            this.btnMoveAbsolutePositionZ50.Id = 340;
            this.btnMoveAbsolutePositionZ50.Name = "btnMoveAbsolutePositionZ50";
            this.btnMoveAbsolutePositionZ50.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMoveAbsolutePositionZ50_ItemClick);
            // 
            // btnBCSKeyCalibration
            // 
            this.btnBCSKeyCalibration.Caption = "一键标定";
            this.btnBCSKeyCalibration.Id = 283;
            this.btnBCSKeyCalibration.Name = "btnBCSKeyCalibration";
            this.btnBCSKeyCalibration.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSKeyCalibration_ItemClick);
            // 
            // btnBCSRegisteredEncrypt
            // 
            this.btnBCSRegisteredEncrypt.Caption = "注册加密";
            this.btnBCSRegisteredEncrypt.Id = 284;
            this.btnBCSRegisteredEncrypt.Name = "btnBCSRegisteredEncrypt";
            this.btnBCSRegisteredEncrypt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSRegisteredEncrypt_ItemClick);
            // 
            // btnBCSUpdate
            // 
            this.btnBCSUpdate.Caption = "升级";
            this.btnBCSUpdate.Id = 285;
            this.btnBCSUpdate.Name = "btnBCSUpdate";
            this.btnBCSUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSUpdate_ItemClick);
            // 
            // btnBCSSaveParams
            // 
            this.btnBCSSaveParams.Caption = "保存板外跟随参照高度";
            this.btnBCSSaveParams.Id = 286;
            this.btnBCSSaveParams.Name = "btnBCSSaveParams";
            this.btnBCSSaveParams.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSSaveParams_ItemClick);
            // 
            // btnBCSRecordZCoordinate
            // 
            this.btnBCSRecordZCoordinate.Caption = "记录板面Z坐标";
            this.btnBCSRecordZCoordinate.Id = 287;
            this.btnBCSRecordZCoordinate.Name = "btnBCSRecordZCoordinate";
            this.btnBCSRecordZCoordinate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBCSRecordZCoordinate_ItemClick);
            // 
            // btnErrorMeasure
            // 
            this.btnErrorMeasure.Caption = "误差测定";
            this.btnErrorMeasure.Id = 90;
            this.btnErrorMeasure.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnErrorMeasure.ImageOptions.Image")));
            this.btnErrorMeasure.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnErrorMeasure.ImageOptions.LargeImage")));
            this.btnErrorMeasure.Name = "btnErrorMeasure";
            this.btnErrorMeasure.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnErrorMeasure_ItemClick);
            // 
            // btnSelect
            // 
            this.btnSelect.Caption = "选择";
            this.btnSelect.Id = 91;
            this.btnSelect.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.ImageOptions.Image")));
            this.btnSelect.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSelect.ImageOptions.LargeImage")));
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSelect_ItemClick);
            // 
            // btnEditNode
            // 
            this.btnEditNode.Caption = "节点编辑";
            this.btnEditNode.Id = 92;
            this.btnEditNode.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEditNode.ImageOptions.Image")));
            this.btnEditNode.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEditNode.ImageOptions.LargeImage")));
            this.btnEditNode.Name = "btnEditNode";
            this.btnEditNode.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnEditNode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEditNode_ItemClick);
            // 
            // btnTranslateView
            // 
            this.btnTranslateView.Caption = "平移视图";
            this.btnTranslateView.Id = 93;
            this.btnTranslateView.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnTranslateView.ImageOptions.Image")));
            this.btnTranslateView.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnTranslateView.ImageOptions.LargeImage")));
            this.btnTranslateView.Name = "btnTranslateView";
            this.btnTranslateView.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnTranslateView.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnTranslateView_ItemClick);
            // 
            // btnSortMode
            // 
            this.btnSortMode.Caption = "排序模式";
            this.btnSortMode.Id = 94;
            this.btnSortMode.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSortMode.ImageOptions.Image")));
            this.btnSortMode.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSortMode.ImageOptions.LargeImage")));
            this.btnSortMode.Name = "btnSortMode";
            this.btnSortMode.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnSortMode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSortMode_ItemClick);
            // 
            // btnLanguageItem
            // 
            this.btnLanguageItem.Caption = "语言选择";
            this.btnLanguageItem.Id = 91;
            this.btnLanguageItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLanguageItem.ImageOptions.Image")));
            this.btnLanguageItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.ckEnglish, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckSimplifiedChinese),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckTraditionalChinese)});
            this.btnLanguageItem.Name = "btnLanguageItem";
            this.btnLanguageItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // ckEnglish
            // 
            this.ckEnglish.Caption = "English";
            this.ckEnglish.GroupIndex = 1;
            this.ckEnglish.Id = 97;
            this.ckEnglish.Name = "ckEnglish";
            this.ckEnglish.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckEnglish_CheckedChanged);
            // 
            // ckSimplifiedChinese
            // 
            this.ckSimplifiedChinese.Caption = "简体中文";
            this.ckSimplifiedChinese.GroupIndex = 1;
            this.ckSimplifiedChinese.Id = 98;
            this.ckSimplifiedChinese.Name = "ckSimplifiedChinese";
            this.ckSimplifiedChinese.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckSimplifiedChinese_CheckedChanged);
            // 
            // ckTraditionalChinese
            // 
            this.ckTraditionalChinese.Caption = "繁体中文";
            this.ckTraditionalChinese.GroupIndex = 1;
            this.ckTraditionalChinese.Id = 151;
            this.ckTraditionalChinese.Name = "ckTraditionalChinese";
            this.ckTraditionalChinese.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckTraditionalChinese_CheckedChanged);
            // 
            // btnBackFileBottomFigure
            // 
            this.btnBackFileBottomFigure.Caption = "返回文件底图";
            this.btnBackFileBottomFigure.Id = 92;
            this.btnBackFileBottomFigure.Name = "btnBackFileBottomFigure";
            this.btnBackFileBottomFigure.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBackFileBottomFigure_ItemClick);
            // 
            // btnSystemUnitItem
            // 
            this.btnSystemUnitItem.Caption = "系统单位选择";
            this.btnSystemUnitItem.Id = 93;
            this.btnSystemUnitItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSystemUnitItem.ImageOptions.Image")));
            this.btnSystemUnitItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ckMetricUnit),
            new DevExpress.XtraBars.LinkPersistInfo(this.ckImperailUnit)});
            this.btnSystemUnitItem.Name = "btnSystemUnitItem";
            this.btnSystemUnitItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // ckMetricUnit
            // 
            this.ckMetricUnit.BindableChecked = true;
            this.ckMetricUnit.Caption = "公制（Metric Unit）";
            this.ckMetricUnit.Checked = true;
            this.ckMetricUnit.GroupIndex = 1;
            this.ckMetricUnit.Id = 94;
            this.ckMetricUnit.Name = "ckMetricUnit";
            this.ckMetricUnit.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckMetricUnit_CheckedChanged);
            // 
            // ckImperailUnit
            // 
            this.ckImperailUnit.Caption = "英制（Imperail Unit）";
            this.ckImperailUnit.GroupIndex = 1;
            this.ckImperailUnit.Id = 96;
            this.ckImperailUnit.Name = "ckImperailUnit";
            this.ckImperailUnit.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.ckImperailUnit_CheckedChanged);
            // 
            // btnPartItem
            // 
            this.btnPartItem.Caption = "零件";
            this.btnPartItem.Id = 152;
            this.btnPartItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPartItem.ImageOptions.Image")));
            this.btnPartItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImportPart),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImportStandardPart),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDeleteAllLayoutPart)});
            this.btnPartItem.Name = "btnPartItem";
            this.btnPartItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnImportPart
            // 
            this.btnImportPart.Caption = "导入零件";
            this.btnImportPart.Description = "从外部文件导入零件到零件库";
            this.btnImportPart.Id = 10;
            this.btnImportPart.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportPart.ImageOptions.Image")));
            this.btnImportPart.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnImportPart.ImageOptions.LargeImage")));
            this.btnImportPart.Name = "btnImportPart";
            this.btnImportPart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportPart_ItemClick);
            // 
            // btnImportStandardPart
            // 
            this.btnImportStandardPart.Caption = "导入标准零件";
            this.btnImportStandardPart.Description = "从预定的标准零件库添加零件";
            this.btnImportStandardPart.Id = 11;
            this.btnImportStandardPart.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportStandardPart.ImageOptions.Image")));
            this.btnImportStandardPart.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnImportStandardPart.ImageOptions.LargeImage")));
            this.btnImportStandardPart.Name = "btnImportStandardPart";
            this.btnImportStandardPart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportStandardPart_ItemClick);
            // 
            // btnDeleteAllLayoutPart
            // 
            this.btnDeleteAllLayoutPart.Caption = "删除所有排样零件";
            this.btnDeleteAllLayoutPart.Id = 156;
            this.btnDeleteAllLayoutPart.Name = "btnDeleteAllLayoutPart";
            this.btnDeleteAllLayoutPart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteAllLayoutPart_ItemClick);
            // 
            // btnSheetMaterialItem
            // 
            this.btnSheetMaterialItem.Caption = "板材";
            this.btnSheetMaterialItem.Id = 153;
            this.btnSheetMaterialItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSheetMaterialItem.ImageOptions.Image")));
            this.btnSheetMaterialItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSetSheetMaterial),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDeleteAllSheetMaterial)});
            this.btnSheetMaterialItem.Name = "btnSheetMaterialItem";
            this.btnSheetMaterialItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnSetSheetMaterial
            // 
            this.btnSetSheetMaterial.Caption = "将选中图形设为板材";
            this.btnSetSheetMaterial.Id = 157;
            this.btnSetSheetMaterial.Name = "btnSetSheetMaterial";
            this.btnSetSheetMaterial.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSetSheetMaterial_ItemClick);
            // 
            // btnDeleteAllSheetMaterial
            // 
            this.btnDeleteAllSheetMaterial.Caption = "删除所有排样板材";
            this.btnDeleteAllSheetMaterial.Id = 158;
            this.btnDeleteAllSheetMaterial.Name = "btnDeleteAllSheetMaterial";
            this.btnDeleteAllSheetMaterial.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteAllSheetMaterial_ItemClick);
            // 
            // barDockingMenuItem1
            // 
            this.barDockingMenuItem1.Caption = "另存为";
            this.barDockingMenuItem1.Id = 161;
            this.barDockingMenuItem1.Name = "barDockingMenuItem1";
            // 
            // btnLayoutItem
            // 
            this.btnLayoutItem.Caption = "排样";
            this.btnLayoutItem.Id = 3;
            this.btnLayoutItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLayoutItem.ImageOptions.Image")));
            this.btnLayoutItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImportPart),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImportStandardPart)});
            this.btnLayoutItem.Name = "btnLayoutItem";
            // 
            // btnImportItem
            // 
            this.btnImportItem.Caption = "导入";
            this.btnImportItem.Id = 4;
            this.btnImportItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportItem.ImageOptions.Image")));
            this.btnImportItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImportFileLXD),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImportFileDXF),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImportFilePLT),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImportFileAI),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImportFileGerBar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImportFileNC)});
            this.btnImportItem.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.btnImportItem.Name = "btnImportItem";
            // 
            // btnImportFileLXD
            // 
            this.btnImportFileLXD.Caption = "导入LXD文件";
            this.btnImportFileLXD.Id = 12;
            this.btnImportFileLXD.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportFileLXD.ImageOptions.Image")));
            this.btnImportFileLXD.Name = "btnImportFileLXD";
            this.btnImportFileLXD.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportFileLXD_ItemClick);
            // 
            // btnImportFileDXF
            // 
            this.btnImportFileDXF.Caption = "导入DXF文件";
            this.btnImportFileDXF.Id = 13;
            this.btnImportFileDXF.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportFileDXF.ImageOptions.Image")));
            this.btnImportFileDXF.Name = "btnImportFileDXF";
            this.btnImportFileDXF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportFileDXF_ItemClick);
            // 
            // btnImportFilePLT
            // 
            this.btnImportFilePLT.Caption = "导入PLT格式文件";
            this.btnImportFilePLT.Id = 14;
            this.btnImportFilePLT.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportFilePLT.ImageOptions.Image")));
            this.btnImportFilePLT.Name = "btnImportFilePLT";
            this.btnImportFilePLT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportFilePLT_ItemClick);
            // 
            // btnImportFileAI
            // 
            this.btnImportFileAI.Caption = "导入AI路劲文件";
            this.btnImportFileAI.Id = 15;
            this.btnImportFileAI.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportFileAI.ImageOptions.Image")));
            this.btnImportFileAI.Name = "btnImportFileAI";
            this.btnImportFileAI.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportFileAI_ItemClick);
            // 
            // btnImportFileGerBar
            // 
            this.btnImportFileGerBar.Caption = "导入GerBer文件";
            this.btnImportFileGerBar.Id = 16;
            this.btnImportFileGerBar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportFileGerBar.ImageOptions.Image")));
            this.btnImportFileGerBar.Name = "btnImportFileGerBar";
            this.btnImportFileGerBar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportFileGerBer_ItemClick);
            // 
            // btnImportFileNC
            // 
            this.btnImportFileNC.Caption = "NC加工代码文件";
            this.btnImportFileNC.Id = 17;
            this.btnImportFileNC.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnImportFileNC.ImageOptions.Image")));
            this.btnImportFileNC.Name = "btnImportFileNC";
            this.btnImportFileNC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnImportFileNC_ItemClick);
            // 
            // barSubItem39
            // 
            this.barSubItem39.Caption = "尺寸";
            this.barSubItem39.Id = 76;
            this.barSubItem39.Name = "barSubItem39";
            // 
            // btnLeadInOrOutWireItem
            // 
            this.btnLeadInOrOutWireItem.Caption = "引线";
            this.btnLeadInOrOutWireItem.Id = 93;
            this.btnLeadInOrOutWireItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLeadInOrOutWireItem.ImageOptions.Image")));
            this.btnLeadInOrOutWireItem.ItemLinks.Add(this.btnCheckLeadInOrOutWire);
            this.btnLeadInOrOutWireItem.ItemLinks.Add(this.btnLeadInOrOutWireDifferentiateMode);
            this.btnLeadInOrOutWireItem.Name = "btnLeadInOrOutWireItem";
            this.btnLeadInOrOutWireItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnLeadInOrOutWireItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLeadInOrOutWireItem_ItemClick);
            // 
            // btnCheckLeadInOrOutWire
            // 
            this.btnCheckLeadInOrOutWire.Caption = "检查引入引出";
            this.btnCheckLeadInOrOutWire.Id = 216;
            this.btnCheckLeadInOrOutWire.Name = "btnCheckLeadInOrOutWire";
            this.btnCheckLeadInOrOutWire.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCheckLeadInOrOutWire_ItemClick);
            // 
            // btnLeadInOrOutWireDifferentiateMode
            // 
            this.btnLeadInOrOutWireDifferentiateMode.Caption = "区分内外模式";
            this.btnLeadInOrOutWireDifferentiateMode.Id = 217;
            this.btnLeadInOrOutWireDifferentiateMode.Name = "btnLeadInOrOutWireDifferentiateMode";
            this.btnLeadInOrOutWireDifferentiateMode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLeadInOrOutWireDifferentiateMode_ItemClick);
            // 
            // barButtonItem65
            // 
            this.barButtonItem65.Caption = "标记5";
            this.barButtonItem65.Id = 97;
            this.barButtonItem65.Name = "barButtonItem65";
            // 
            // barButtonItem66
            // 
            this.barButtonItem66.Caption = "标记6";
            this.barButtonItem66.Id = 98;
            this.barButtonItem66.Name = "barButtonItem66";
            // 
            // btnFigureSizeItem
            // 
            this.btnFigureSizeItem.Caption = "尺寸";
            this.btnFigureSizeItem.Id = 92;
            this.btnFigureSizeItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFigureSizeItem.ImageOptions.Image")));
            this.btnFigureSizeItem.ItemLinks.Add(this.btnFigureSize100mm);
            this.btnFigureSizeItem.ItemLinks.Add(this.btnFigureSize200mm);
            this.btnFigureSizeItem.ItemLinks.Add(this.btnFigureSize0_5);
            this.btnFigureSizeItem.ItemLinks.Add(this.btnFigureSize2);
            this.btnFigureSizeItem.ItemLinks.Add(this.btnFigureSize4);
            this.btnFigureSizeItem.ItemLinks.Add(this.btnFigureSize8);
            this.btnFigureSizeItem.ItemLinks.Add(this.btnFigureSize10);
            this.btnFigureSizeItem.ItemLinks.Add(this.btnInteractiveZoom);
            this.btnFigureSizeItem.Name = "btnFigureSizeItem";
            this.btnFigureSizeItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnFigureSizeItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFigureSizeItem_ItemClick);
            // 
            // btnFigureSize100mm
            // 
            this.btnFigureSize100mm.Caption = "100mm";
            this.btnFigureSize100mm.Id = 343;
            this.btnFigureSize100mm.Name = "btnFigureSize100mm";
            this.btnFigureSize100mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFigureSize100mm_ItemClick);
            // 
            // btnFigureSize200mm
            // 
            this.btnFigureSize200mm.Caption = "200mm";
            this.btnFigureSize200mm.Id = 344;
            this.btnFigureSize200mm.Name = "btnFigureSize200mm";
            this.btnFigureSize200mm.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFigureSize200mm_ItemClick);
            // 
            // btnFigureSize0_5
            // 
            this.btnFigureSize0_5.Caption = "0.5倍";
            this.btnFigureSize0_5.Id = 210;
            this.btnFigureSize0_5.Name = "btnFigureSize0_5";
            this.btnFigureSize0_5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFigureSize0_5_ItemClick);
            // 
            // btnFigureSize2
            // 
            this.btnFigureSize2.Caption = "2倍";
            this.btnFigureSize2.Id = 211;
            this.btnFigureSize2.Name = "btnFigureSize2";
            this.btnFigureSize2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFigureSize2_ItemClick);
            // 
            // btnFigureSize4
            // 
            this.btnFigureSize4.Caption = "4倍";
            this.btnFigureSize4.Id = 212;
            this.btnFigureSize4.Name = "btnFigureSize4";
            this.btnFigureSize4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFigureSize4_ItemClick);
            // 
            // btnFigureSize8
            // 
            this.btnFigureSize8.Caption = "8倍";
            this.btnFigureSize8.Id = 213;
            this.btnFigureSize8.Name = "btnFigureSize8";
            this.btnFigureSize8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFigureSize8_ItemClick);
            // 
            // btnFigureSize10
            // 
            this.btnFigureSize10.Caption = "10倍";
            this.btnFigureSize10.Id = 214;
            this.btnFigureSize10.Name = "btnFigureSize10";
            this.btnFigureSize10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFigureSize10_ItemClick);
            // 
            // btnReleaseAngle
            // 
            this.btnReleaseAngle.Caption = "释放角";
            this.btnReleaseAngle.Id = 119;
            this.btnReleaseAngle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnReleaseAngle.ImageOptions.Image")));
            this.btnReleaseAngle.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnReleaseAngle.ImageOptions.LargeImage")));
            this.btnReleaseAngle.Name = "btnReleaseAngle";
            this.btnReleaseAngle.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnReleaseAngle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReleaseAngle_ItemClick);
            // 
            // btnRoundAngle
            // 
            this.btnRoundAngle.Caption = "倒圆角";
            this.btnRoundAngle.Id = 120;
            this.btnRoundAngle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRoundAngle.ImageOptions.Image")));
            this.btnRoundAngle.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnRoundAngle.ImageOptions.LargeImage")));
            this.btnRoundAngle.Name = "btnRoundAngle";
            this.btnRoundAngle.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            this.btnRoundAngle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRoundAngle_ItemClick);
            // 
            // btnCoolingDotItem
            // 
            this.btnCoolingDotItem.Caption = "冷却点";
            this.btnCoolingDotItem.Id = 121;
            this.btnCoolingDotItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCoolingDotItem.ImageOptions.Image")));
            this.btnCoolingDotItem.ItemLinks.Add(this.btnCoolingDotAuto);
            this.btnCoolingDotItem.ItemLinks.Add(this.btnCoolingDotClear);
            this.btnCoolingDotItem.Name = "btnCoolingDotItem";
            this.btnCoolingDotItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnCoolingDotItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCoolingDotItem_ItemClick);
            // 
            // btnCoolingDotAuto
            // 
            this.btnCoolingDotAuto.Caption = "自动冷却点";
            this.btnCoolingDotAuto.Id = 186;
            this.btnCoolingDotAuto.Name = "btnCoolingDotAuto";
            this.btnCoolingDotAuto.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCoolingDotAuto_ItemClick);
            // 
            // btnCoolingDotClear
            // 
            this.btnCoolingDotClear.Caption = "清除冷却点";
            this.btnCoolingDotClear.Id = 187;
            this.btnCoolingDotClear.Name = "btnCoolingDotClear";
            this.btnCoolingDotClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCoolingDotClear_ItemClick);
            // 
            // btnFrontMost
            // 
            this.btnFrontMost.Caption = "最前";
            this.btnFrontMost.Id = 127;
            this.btnFrontMost.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFrontMost.ImageOptions.Image")));
            this.btnFrontMost.Name = "btnFrontMost";
            this.btnFrontMost.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            this.btnFrontMost.Tag = "FrontMost";
            this.btnFrontMost.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // btnBackMost
            // 
            this.btnBackMost.Caption = "最后";
            this.btnBackMost.Id = 128;
            this.btnBackMost.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBackMost.ImageOptions.Image")));
            this.btnBackMost.Name = "btnBackMost";
            this.btnBackMost.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            this.btnBackMost.Tag = "BackMost";
            this.btnBackMost.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // btnForward
            // 
            this.btnForward.Caption = "向前";
            this.btnForward.Id = 129;
            this.btnForward.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnForward.ImageOptions.Image")));
            this.btnForward.Name = "btnForward";
            this.btnForward.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            this.btnForward.Tag = "Forward";
            this.btnForward.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // btnBackward
            // 
            this.btnBackward.Caption = "向后";
            this.btnBackward.Id = 130;
            this.btnBackward.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBackward.ImageOptions.Image")));
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            this.btnBackward.Tag = "Backward";
            this.btnBackward.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // btnPreviousTarget
            // 
            this.btnPreviousTarget.Caption = "上一个";
            this.btnPreviousTarget.Id = 131;
            this.btnPreviousTarget.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousTarget.ImageOptions.Image")));
            this.btnPreviousTarget.Name = "btnPreviousTarget";
            this.btnPreviousTarget.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            this.btnPreviousTarget.Tag = "PreviousTarget";
            this.btnPreviousTarget.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // btnNextTarget
            // 
            this.btnNextTarget.Caption = "下一个";
            this.btnNextTarget.Id = 132;
            this.btnNextTarget.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNextTarget.ImageOptions.Image")));
            this.btnNextTarget.Name = "btnNextTarget";
            this.btnNextTarget.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            this.btnNextTarget.Tag = "NextTarget";
            this.btnNextTarget.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSort_ItemClick);
            // 
            // btnStockLayoutItem
            // 
            this.btnStockLayoutItem.Caption = "排样";
            this.btnStockLayoutItem.Id = 136;
            this.btnStockLayoutItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnStockLayoutItem.ImageOptions.Image")));
            this.btnStockLayoutItem.ItemLinks.Add(this.btnStockLayoutClear);
            this.btnStockLayoutItem.Name = "btnStockLayoutItem";
            this.btnStockLayoutItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnStockLayoutItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStockLayoutItem_ItemClick);
            // 
            // btnStockLayoutClear
            // 
            this.btnStockLayoutClear.Caption = "清除排样结果";
            this.btnStockLayoutClear.Id = 218;
            this.btnStockLayoutClear.Name = "btnStockLayoutClear";
            this.btnStockLayoutClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStockLayoutClear_ItemClick);
            // 
            // btnProcessTaskItem
            // 
            this.btnProcessTaskItem.Caption = "任务";
            this.btnProcessTaskItem.Id = 253;
            this.btnProcessTaskItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnProcessTaskItem.ImageOptions.Image")));
            this.btnProcessTaskItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveProcessTask),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLoadProcessTask)});
            this.btnProcessTaskItem.Name = "btnProcessTaskItem";
            this.btnProcessTaskItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // btnSaveProcessTask
            // 
            this.btnSaveProcessTask.Caption = "保存加工任务";
            this.btnSaveProcessTask.Id = 254;
            this.btnSaveProcessTask.Name = "btnSaveProcessTask";
            this.btnSaveProcessTask.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveProcessTask_ItemClick);
            // 
            // btnLoadProcessTask
            // 
            this.btnLoadProcessTask.Caption = "载入加工任务";
            this.btnLoadProcessTask.Id = 255;
            this.btnLoadProcessTask.Name = "btnLoadProcessTask";
            this.btnLoadProcessTask.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLoadProcessTask_ItemClick);
            // 
            // btnLightPathDebug
            // 
            this.btnLightPathDebug.Caption = "光路调试";
            this.btnLightPathDebug.Id = 263;
            this.btnLightPathDebug.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLightPathDebug.ImageOptions.Image")));
            this.btnLightPathDebug.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnLightPathDebug.ImageOptions.LargeImage")));
            this.btnLightPathDebug.Name = "btnLightPathDebug";
            this.btnLightPathDebug.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnLightPathDebug.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLightPathDebug_ItemClick);
            // 
            // barButtonItem266
            // 
            this.barButtonItem266.Caption = "跟随然后停止";
            this.barButtonItem266.Id = 278;
            this.barButtonItem266.Name = "barButtonItem266";
            // 
            // barButtonItem275
            // 
            this.barButtonItem275.Caption = "3mm.";
            this.barButtonItem275.Id = 290;
            this.barButtonItem275.Name = "barButtonItem275";
            // 
            // barButtonItem279
            // 
            this.barButtonItem279.Caption = "6";
            this.barButtonItem279.Id = 294;
            this.barButtonItem279.Name = "barButtonItem279";
            // 
            // btnQCW
            // 
            this.btnQCW.Caption = "QCW";
            this.btnQCW.Id = 341;
            this.btnQCW.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnQCW.ImageOptions.Image")));
            this.btnQCW.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnQCW.ImageOptions.LargeImage")));
            this.btnQCW.Name = "btnQCW";
            this.btnQCW.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnQCW.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnQCW_ItemClick);
            // 
            // btnGantryErrorMonitor
            // 
            this.btnGantryErrorMonitor.Caption = "龙门误差监控";
            this.btnGantryErrorMonitor.Id = 342;
            this.btnGantryErrorMonitor.Name = "btnGantryErrorMonitor";
            this.btnGantryErrorMonitor.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.btnGantryErrorMonitor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnGantryErrorMonitor_ItemClick);
            // 
            // barButtonGroup4
            // 
            this.barButtonGroup4.Caption = "barButtonGroup4";
            this.barButtonGroup4.Id = 103;
            this.barButtonGroup4.Name = "barButtonGroup4";
            // 
            // barButtonGroup5
            // 
            this.barButtonGroup5.Caption = "barButtonGroup5";
            this.barButtonGroup5.Id = 105;
            this.barButtonGroup5.Name = "barButtonGroup5";
            // 
            // barButtonGroup6
            // 
            this.barButtonGroup6.Caption = "barButtonGroup6";
            this.barButtonGroup6.Id = 108;
            this.barButtonGroup6.ItemLinks.Add(this.btnCut);
            this.barButtonGroup6.ItemLinks.Add(this.btnDelete);
            this.barButtonGroup6.ItemLinks.Add(this.btnPaste);
            this.barButtonGroup6.Name = "barButtonGroup6";
            // 
            // barButtonGroup11
            // 
            this.barButtonGroup11.Caption = "barButtonGroup11";
            this.barButtonGroup11.Id = 133;
            this.barButtonGroup11.ItemLinks.Add(this.btnSelectAll);
            this.barButtonGroup11.ItemLinks.Add(this.btnInvertSelect);
            this.barButtonGroup11.ItemLinks.Add(this.btnCancelSelect);
            this.barButtonGroup11.Name = "barButtonGroup11";
            // 
            // barButtonGroup7
            // 
            this.barButtonGroup7.Caption = "barButtonGroup7";
            this.barButtonGroup7.Id = 135;
            this.barButtonGroup7.ItemLinks.Add(this.btnSelectGapFigure);
            this.barButtonGroup7.Name = "barButtonGroup7";
            // 
            // barButtonGroup8
            // 
            this.barButtonGroup8.Caption = "barButtonGroup8";
            this.barButtonGroup8.Id = 137;
            this.barButtonGroup8.ItemLinks.Add(this.btnFrontMost);
            this.barButtonGroup8.ItemLinks.Add(this.btnBackMost);
            this.barButtonGroup8.ItemLinks.Add(this.btnForward);
            this.barButtonGroup8.ItemLinks.Add(this.btnBackward);
            this.barButtonGroup8.ItemLinks.Add(this.btnPreviousTarget);
            this.barButtonGroup8.ItemLinks.Add(this.btnNextTarget);
            this.barButtonGroup8.Name = "barButtonGroup8";
            // 
            // barButtonGroup9
            // 
            this.barButtonGroup9.Caption = "barButtonGroup9";
            this.barButtonGroup9.Id = 139;
            this.barButtonGroup9.ItemLinks.Add(this.btnSortRangeTagets);
            this.barButtonGroup9.Name = "barButtonGroup9";
            // 
            // btnSortRangeTagets
            // 
            this.btnSortRangeTagets.Caption = "图形索引范围";
            this.btnSortRangeTagets.Edit = this.repositoryItemTrackBar5;
            this.btnSortRangeTagets.EditWidth = 150;
            this.btnSortRangeTagets.Id = 140;
            this.btnSortRangeTagets.Name = "btnSortRangeTagets";
            this.btnSortRangeTagets.EditValueChanged += new System.EventHandler(this.btnSortRangeTagets_EditValueChanged);
            // 
            // repositoryItemTrackBar5
            // 
            this.repositoryItemTrackBar5.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemTrackBar5.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTrackBar5.Name = "repositoryItemTrackBar5";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "标记1";
            this.barButtonItem1.Id = 335;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "标记2";
            this.barButtonItem2.Id = 336;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "标记3";
            this.barButtonItem3.Id = 337;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // ribPageCommon
            // 
            this.ribPageCommon.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribPageGroupView,
            this.ribPageGroupTransform,
            this.ribPageGroupProcessSet,
            this.ribPageGroup,
            this.ribPageGroupTool1,
            this.ribPageGroupParam});
            this.ribPageCommon.Name = "ribPageCommon";
            this.ribPageCommon.Text = "常用";
            // 
            // ribPageGroupView
            // 
            this.ribPageGroupView.AllowMinimize = false;
            this.ribPageGroupView.ItemLinks.Add(this.btnDisplayItem);
            this.ribPageGroupView.ItemLinks.Add(this.btnSelectItem);
            this.ribPageGroupView.Name = "ribPageGroupView";
            this.ribPageGroupView.Text = "查看";
            // 
            // ribPageGroupTransform
            // 
            this.ribPageGroupTransform.AllowMinimize = false;
            this.ribPageGroupTransform.ItemLinks.Add(this.btnFigureSizeItem);
            this.ribPageGroupTransform.ItemLinks.Add(this.btnFigureTransformItem);
            this.ribPageGroupTransform.Name = "ribPageGroupTransform";
            this.ribPageGroupTransform.Text = "几何变换";
            // 
            // ribPageGroupProcessSet
            // 
            this.ribPageGroupProcessSet.AllowMinimize = false;
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnLeadInOrOutWireItem);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnClearItem);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnStartPoint);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnDockPosition);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnGapCompensation);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnOuterCut);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnInnerCut);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnSurroundCut);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnReleaseAngle);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnRoundAngle);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnMicroConnectItem, true);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnReverseItem);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnSealingItem);
            this.ribPageGroupProcessSet.ItemLinks.Add(this.btnCoolingDotItem);
            this.ribPageGroupProcessSet.Name = "ribPageGroupProcessSet";
            this.ribPageGroupProcessSet.Text = "工艺设置";
            // 
            // ribPageGroup
            // 
            this.ribPageGroup.AllowMinimize = false;
            this.ribPageGroup.ItemLinks.Add(this.btnSortItem);
            this.ribPageGroup.ItemLinks.Add(this.barButtonGroup8, true);
            this.ribPageGroup.ItemLinks.Add(this.barButtonGroup9);
            this.ribPageGroup.ItemsLayout = DevExpress.XtraBars.Ribbon.RibbonPageGroupItemsLayout.TwoRows;
            this.ribPageGroup.Name = "ribPageGroup";
            this.ribPageGroup.Text = "排序";
            // 
            // ribPageGroupTool1
            // 
            this.ribPageGroupTool1.AllowMinimize = false;
            this.ribPageGroupTool1.ItemLinks.Add(this.btnStockLayoutItem);
            this.ribPageGroupTool1.ItemLinks.Add(this.btnArrayLayoutItem);
            this.ribPageGroupTool1.ItemLinks.Add(this.btnGroupItem);
            this.ribPageGroupTool1.ItemLinks.Add(this.btnFlyingCutItem);
            this.ribPageGroupTool1.ItemLinks.Add(this.btnCommonSideItem);
            this.ribPageGroupTool1.ItemLinks.Add(this.btnBridging);
            this.ribPageGroupTool1.ItemLinks.Add(this.btnMeasure);
            this.ribPageGroupTool1.ItemLinks.Add(this.btnMajorizationItem);
            this.ribPageGroupTool1.Name = "ribPageGroupTool1";
            this.ribPageGroupTool1.Text = "工具";
            // 
            // ribPageGroupParam
            // 
            this.ribPageGroupParam.AllowMinimize = false;
            this.ribPageGroupParam.ItemLinks.Add(this.btnCraftItem);
            this.ribPageGroupParam.Name = "ribPageGroupParam";
            this.ribPageGroupParam.Text = "参数设置";
            // 
            // ribPageLayout
            // 
            this.ribPageLayout.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribPageGroupLayout});
            this.ribPageLayout.Name = "ribPageLayout";
            this.ribPageLayout.Text = "排样";
            // 
            // ribPageGroupLayout
            // 
            this.ribPageGroupLayout.ItemLinks.Add(this.btnPartItem);
            this.ribPageGroupLayout.ItemLinks.Add(this.btnSheetMaterialItem);
            this.ribPageGroupLayout.Name = "ribPageGroupLayout";
            this.ribPageGroupLayout.ShowCaptionButton = false;
            this.ribPageGroupLayout.Text = "排样";
            // 
            // ribPageCNC
            // 
            this.ribPageCNC.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribPageGroupProcess,
            this.ribPageGroupTool2});
            this.ribPageCNC.Name = "ribPageCNC";
            this.ribPageCNC.Text = "数控";
            // 
            // ribPageGroupProcess
            // 
            this.ribPageGroupProcess.AllowMinimize = false;
            this.ribPageGroupProcess.ItemLinks.Add(this.btnFindEdgeItem);
            this.ribPageGroupProcess.ItemLinks.Add(this.btnPLCProcessItem);
            this.ribPageGroupProcess.ItemLinks.Add(this.btnProcessTaskItem);
            this.ribPageGroupProcess.Name = "ribPageGroupProcess";
            this.ribPageGroupProcess.Text = "加工";
            // 
            // ribPageGroupTool2
            // 
            this.ribPageGroupTool2.AllowMinimize = false;
            this.ribPageGroupTool2.ItemLinks.Add(this.btnGoOriginItem);
            this.ribPageGroupTool2.ItemLinks.Add(this.btnLightPathDebug);
            this.ribPageGroupTool2.ItemLinks.Add(this.btnBCS100Item);
            this.ribPageGroupTool2.ItemLinks.Add(this.btnQCW);
            this.ribPageGroupTool2.ItemLinks.Add(this.btnErrorMeasure);
            this.ribPageGroupTool2.ItemLinks.Add(this.btnGantryErrorMonitor);
            this.ribPageGroupTool2.Name = "ribPageGroupTool2";
            this.ribPageGroupTool2.Text = "工具";
            // 
            // repositoryItemPopupContainerEdit1
            // 
            this.repositoryItemPopupContainerEdit1.AutoHeight = false;
            this.repositoryItemPopupContainerEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemPopupContainerEdit1.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.repositoryItemPopupContainerEdit1.Name = "repositoryItemPopupContainerEdit1";
            this.repositoryItemPopupContainerEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            // 
            // repositoryItemRangeTrackBar1
            // 
            this.repositoryItemRangeTrackBar1.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemRangeTrackBar1.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemRangeTrackBar1.Name = "repositoryItemRangeTrackBar1";
            // 
            // repositoryItemTrackBar1
            // 
            this.repositoryItemTrackBar1.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemTrackBar1.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTrackBar1.Name = "repositoryItemTrackBar1";
            // 
            // repositoryItemTrackBar2
            // 
            this.repositoryItemTrackBar2.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemTrackBar2.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTrackBar2.Name = "repositoryItemTrackBar2";
            // 
            // repositoryItemTrackBar3
            // 
            this.repositoryItemTrackBar3.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemTrackBar3.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTrackBar3.Name = "repositoryItemTrackBar3";
            // 
            // repositoryItemTrackBar4
            // 
            this.repositoryItemTrackBar4.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemTrackBar4.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemTrackBar4.Name = "repositoryItemTrackBar4";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 828);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1284, 31);
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.Ribbon = this.ribbonControl1;
            // 
            // splitMain
            // 
            this.splitMain.Appearance.BackColor = System.Drawing.Color.White;
            this.splitMain.Appearance.Options.UseBackColor = true;
            this.splitMain.Collapsed = true;
            this.splitMain.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(2, 2);
            this.splitMain.Name = "splitMain";
            this.splitMain.Panel1.MinSize = 100;
            this.splitMain.Panel1.Text = "Panel1";
            this.splitMain.Panel2.Controls.Add(this.splitMiddle);
            this.splitMain.Panel2.Text = "Panel2";
            this.splitMain.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            this.splitMain.Size = new System.Drawing.Size(990, 677);
            this.splitMain.TabIndex = 4;
            this.splitMain.Text = "splitContainerControl1";
            // 
            // splitMiddle
            // 
            this.splitMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMiddle.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitMiddle.Horizontal = false;
            this.splitMiddle.Location = new System.Drawing.Point(0, 0);
            this.splitMiddle.Name = "splitMiddle";
            this.splitMiddle.Panel1.AppearanceCaption.BackColor = System.Drawing.Color.White;
            this.splitMiddle.Panel1.AppearanceCaption.Options.UseBackColor = true;
            this.splitMiddle.Panel1.Controls.Add(this.canvasWrapper1);
            this.splitMiddle.Panel1.MinSize = 400;
            this.splitMiddle.Panel1.Text = "Panel1";
            this.splitMiddle.Panel2.Controls.Add(this.ucLogDetail1);
            this.splitMiddle.Panel2.MinSize = 100;
            this.splitMiddle.Panel2.Text = "Panel2";
            this.splitMiddle.Size = new System.Drawing.Size(990, 677);
            this.splitMiddle.SplitterPosition = 555;
            this.splitMiddle.TabIndex = 0;
            this.splitMiddle.Text = "splitContainerControl1";
            // 
            // canvasWrapper1
            // 
            this.canvasWrapper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasWrapper1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.canvasWrapper1.Location = new System.Drawing.Point(0, 0);
            this.canvasWrapper1.Name = "canvasWrapper1";
            this.canvasWrapper1.Size = new System.Drawing.Size(990, 555);
            this.canvasWrapper1.TabIndex = 0;
            // 
            // ucLogDetail1
            // 
            this.ucLogDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLogDetail1.Location = new System.Drawing.Point(0, 0);
            this.ucLogDetail1.Name = "ucLogDetail1";
            this.ucLogDetail1.Size = new System.Drawing.Size(990, 117);
            this.ucLogDetail1.TabIndex = 0;
            // 
            // barButtonItem26
            // 
            this.barButtonItem26.Caption = "降低速度";
            this.barButtonItem26.GroupIndex = 1;
            this.barButtonItem26.Id = 83;
            this.barButtonItem26.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem26.ImageOptions.Image")));
            this.barButtonItem26.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem26.ImageOptions.LargeImage")));
            this.barButtonItem26.Name = "barButtonItem26";
            this.barButtonItem26.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // barEditItem3
            // 
            this.barEditItem3.AutoFillWidthInMenu = DevExpress.Utils.DefaultBoolean.False;
            this.barEditItem3.Caption = "速度";
            this.barEditItem3.Edit = this.repositoryItemTrackBar3;
            this.barEditItem3.Id = 220;
            this.barEditItem3.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.barEditItem3.Name = "barEditItem3";
            this.barEditItem3.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.barEditItem3.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.True;
            // 
            // ucMachineControl1
            // 
            this.ucMachineControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ucMachineControl1.Location = new System.Drawing.Point(29, 299);
            this.ucMachineControl1.Name = "ucMachineControl1";
            this.ucMachineControl1.Size = new System.Drawing.Size(258, 266);
            this.ucMachineControl1.TabIndex = 52;
            // 
            // panelRightMain
            // 
            this.panelRightMain.AutoScroll = true;
            this.panelRightMain.Controls.Add(this.ucLayerParaBar1);
            this.panelRightMain.Controls.Add(this.ucMannual1);
            this.panelRightMain.Controls.Add(this.ucMachineCount1);
            this.panelRightMain.Controls.Add(this.ucMachineControl1);
            this.panelRightMain.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightMain.Location = new System.Drawing.Point(992, 2);
            this.panelRightMain.Name = "panelRightMain";
            this.panelRightMain.Size = new System.Drawing.Size(290, 677);
            this.panelRightMain.TabIndex = 5;
            // 
            // ucLayerParaBar1
            // 
            this.ucLayerParaBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(218)))), ((int)(((byte)(237)))));
            this.ucLayerParaBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucLayerParaBar1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ucLayerParaBar1.Location = new System.Drawing.Point(0, 0);
            this.ucLayerParaBar1.Name = "ucLayerParaBar1";
            this.ucLayerParaBar1.Size = new System.Drawing.Size(24, 677);
            this.ucLayerParaBar1.TabIndex = 57;
            // 
            // ucMannual1
            // 
            this.ucMannual1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ucMannual1.Location = new System.Drawing.Point(29, 6);
            this.ucMannual1.Name = "ucMannual1";
            this.ucMannual1.Size = new System.Drawing.Size(258, 292);
            this.ucMannual1.TabIndex = 56;
            // 
            // ucMachineCount1
            // 
            this.ucMachineCount1.Location = new System.Drawing.Point(29, 570);
            this.ucMachineCount1.Name = "ucMachineCount1";
            this.ucMachineCount1.Size = new System.Drawing.Size(258, 97);
            this.ucMachineCount1.TabIndex = 55;
            // 
            // panelMain
            // 
            this.panelMain.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelMain.Appearance.Options.UseBackColor = true;
            this.panelMain.Controls.Add(this.splitMain);
            this.panelMain.Controls.Add(this.panelRightMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 147);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1284, 681);
            this.panelMain.TabIndex = 7;
            // 
            // MainView
            // 
            this.AllowDraggingByPageCategory = DevExpress.Utils.DefaultBoolean.True;
            this.AllowDrop = true;
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseBorderColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(1284, 859);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MainView";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "万顺兴科技切割控制平台 V1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainView_FormClosing);
            this.Load += new System.EventHandler(this.MainView_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainView_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainView_DragEnter);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainView_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPopupContainerEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRangeTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMiddle)).EndInit();
            this.splitMiddle.ResumeLayout(false);
            this.panelRightMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu appMenu;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        private DevExpress.XtraEditors.SplitContainerControl splitMiddle;
        private Views.CustomControl.UCLogDetail ucLogDetail1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem26;
        private DevExpress.XtraBars.BarEditItem barEditItem3;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarSubItem btnSelectItem;
        private DevExpress.XtraBars.BarButtonItem btnSelectAll;
        private DevExpress.XtraBars.BarButtonItem btnInvertSelect;
        private DevExpress.XtraBars.BarButtonItem btnCancelSelect;
        private DevExpress.XtraBars.BarButtonItem btnBatchChange;
        private DevExpress.XtraBars.BarCheckItem ckBanFastDragCopy;
        private DevExpress.XtraBars.BarButtonItem btnSelectGapFigure;
        private DevExpress.XtraBars.BarButtonItem btnSelectSimilarFigure;
        private DevExpress.XtraBars.BarButtonItem btnSelectAllExternalModes;
        private DevExpress.XtraBars.BarButtonItem btnSelectAllInternalModes;
        private DevExpress.XtraBars.BarButtonItem btnSelectAllFigureSmaller;
        private DevExpress.XtraBars.BarSubItem btnSelectFigureLayerItem;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft0;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft1;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft2;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft3;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft4;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft5;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft6;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft7;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft8;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft9;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft10;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft11;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft12;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft13;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft14;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft15;
        private DevExpress.XtraBars.BarButtonItem btnSelectLayerCraft16;
        private DevExpress.XtraBars.BarButtonItem btnLockSelectBackground;
        private DevExpress.XtraBars.BarSubItem btnSelectTypeItem;
        private DevExpress.XtraBars.BarButtonItem btnSelectAllMultiLines;
        private DevExpress.XtraBars.BarButtonItem btnSelectAllCircles;
        private DevExpress.XtraBars.BarButtonItem btnSelectAllBezierCurves;
        private DevExpress.XtraBars.BarButtonItem btnCut;
        private DevExpress.XtraBars.BarButtonItem btnCopy;
        private DevExpress.XtraBars.BarButtonItem btnCopyBasePoint;
        private DevExpress.XtraBars.BarButtonItem btnPaste;
        private DevExpress.XtraBars.BarButtonItem btnDelete;
        private DevExpress.XtraBars.BarSubItem btnDisplayItem;
        private DevExpress.XtraBars.BarCheckItem ckDisplayGapFigureFrame;
        private DevExpress.XtraBars.BarCheckItem ckRedDisplayGapFigure;
        private DevExpress.XtraBars.BarCheckItem ckDisplaySerialNumber;
        private DevExpress.XtraBars.BarCheckItem ckDispalyPathStartPoint;
        private DevExpress.XtraBars.BarCheckItem ckDisplayProcessPath;
        private DevExpress.XtraBars.BarCheckItem ckDisplayEmptyMovePath;
        private DevExpress.XtraBars.BarCheckItem ckDisplayMicroHyphen;
        private DevExpress.XtraBars.BarButtonItem btnFigureCenter;
        private DevExpress.XtraBars.BarSubItem btnFigureTransformItem;
        private DevExpress.XtraBars.BarButtonItem btnTranslation;
        private DevExpress.XtraBars.BarButtonItem btnInteractiveZoom;
        private DevExpress.XtraBars.BarSubItem btnAlignItem;
        private DevExpress.XtraBars.BarButtonItem btnAlignLeft;
        private DevExpress.XtraBars.BarButtonItem btnAlignRight;
        private DevExpress.XtraBars.BarButtonItem btnAlignHorizontalCenter;
        private DevExpress.XtraBars.BarButtonItem btnAlignTop;
        private DevExpress.XtraBars.BarButtonItem btnAlignBottom;
        private DevExpress.XtraBars.BarButtonItem btnAlignVerticalCenter;
        private DevExpress.XtraBars.BarButtonItem btnAlignCenter;
        private DevExpress.XtraBars.BarButtonItem btnMirrorHorizontal;
        private DevExpress.XtraBars.BarButtonItem btnMirrorVertical;
        private DevExpress.XtraBars.BarButtonItem btnMirrorAnyAngle;
        private DevExpress.XtraBars.BarButtonItem btnAnticlockwise90;
        private DevExpress.XtraBars.BarButtonItem btnClockwise90;
        private DevExpress.XtraBars.BarButtonItem btnRotate180;
        private DevExpress.XtraBars.BarButtonItem btnRotateAnyAngle;
        private DevExpress.XtraBars.BarSubItem btnClearItem;
        private DevExpress.XtraBars.BarButtonItem btnClearLeadInOrOutWire;
        private DevExpress.XtraBars.BarButtonItem btnClearMicroConnect;
        private DevExpress.XtraBars.BarButtonItem btnCancelGapCompensation;
        private DevExpress.XtraBars.BarButtonItem btnStartPoint;
        private DevExpress.XtraBars.BarButtonItem btnDockPosition;
        private DevExpress.XtraBars.BarButtonItem btnGapCompensation;
        private DevExpress.XtraBars.BarButtonItem btnOuterCut;
        private DevExpress.XtraBars.BarButtonItem btnInnerCut;
        private DevExpress.XtraBars.BarButtonItem btnSurroundCut;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnMicroConnectItem;
        private DevExpress.XtraBars.BarButtonItem btnMicroConnectionAuto;
        private DevExpress.XtraBars.BarButtonItem btnMicroConnectBlowOpen;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnReverseItem;
        private DevExpress.XtraBars.BarButtonItem btnReverse;
        private DevExpress.XtraBars.BarButtonItem btnClockwise;
        private DevExpress.XtraBars.BarButtonItem btnAnticlockwise;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnSealingItem;
        private DevExpress.XtraBars.BarButtonItem btnSealing;
        private DevExpress.XtraBars.BarButtonItem btnGaping;
        private DevExpress.XtraBars.BarButtonItem btnCutOver;
        private DevExpress.XtraBars.BarButtonItem btnMutilGaps;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnSortItem;
        private DevExpress.XtraBars.BarCheckItem ckSortGrid;
        private DevExpress.XtraBars.BarCheckItem ckSortShortestMove;
        private DevExpress.XtraBars.BarCheckItem ckSortKnife;
        private DevExpress.XtraBars.BarCheckItem ckSortSmallFigurePriority;
        private DevExpress.XtraBars.BarCheckItem ckSortInsideToOut;
        private DevExpress.XtraBars.BarCheckItem ckSortLeftToRight;
        private DevExpress.XtraBars.BarCheckItem ckSortRightToLeft;
        private DevExpress.XtraBars.BarCheckItem ckSortTopToBottom;
        private DevExpress.XtraBars.BarCheckItem ckSortBottomToTop;
        private DevExpress.XtraBars.BarCheckItem ckSortClockwise;
        private DevExpress.XtraBars.BarCheckItem ckSortAnticlockwise;
        private DevExpress.XtraBars.BarCheckItem ckSortProhibitChangDirection;
        private DevExpress.XtraBars.BarCheckItem ckSortDistinguishInsideOutside;
        private DevExpress.XtraBars.BarCheckItem ckSortShadeCutOutermost;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnArrayLayoutItem;
        private DevExpress.XtraBars.BarButtonItem btnArrayLayoutRectangle;
        private DevExpress.XtraBars.BarButtonItem btnArrayLayoutInteractive;
        private DevExpress.XtraBars.BarButtonItem btnArrayLayoutAnnular;
        private DevExpress.XtraBars.BarButtonItem btnArrayLayoutFull;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnFlyingCutItem;
        private DevExpress.XtraBars.BarButtonItem btnFlyingCutLine;
        private DevExpress.XtraBars.BarButtonItem btnFlyingCutArc;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnCommonSideItem;
        private DevExpress.XtraBars.BarButtonItem btnCommonSideTypeC;
        private DevExpress.XtraBars.BarButtonItem btnBridging;
        private DevExpress.XtraBars.BarButtonItem btnMeasure;
        private DevExpress.XtraBars.BarSubItem btnMajorizationItem;
        private DevExpress.XtraBars.BarButtonItem btnCurveSmoothing;
        private DevExpress.XtraBars.BarButtonItem btnCurveSegment;
        private DevExpress.XtraBars.BarButtonItem btnDeleteRepeatLine;
        private DevExpress.XtraBars.BarButtonItem btnDeleteSmallFigure;
        private DevExpress.XtraBars.BarButtonItem btnMergeConnectLine;
        private DevExpress.XtraBars.BarButtonItem btnCutUp;
        private DevExpress.XtraBars.BarButtonItem btnConnectKnife;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnCraftItem;
        private DevExpress.XtraBars.BarSubItem btnShowFigureLayerItem;
        private DevExpress.XtraBars.BarButtonItem btnShowBackgroundCarft;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft1;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft2;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft3;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft4;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft5;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft6;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft7;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft8;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft9;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft10;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft11;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft12;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft13;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft14;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft15;
        private DevExpress.XtraBars.BarCheckItem ckShowCarft16;
        private DevExpress.XtraBars.BarSubItem btnOnlyShowFigureLayerItem;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarftALL;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft1;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft2;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft3;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft4;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft5;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft6;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft7;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft8;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft9;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft10;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft11;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft12;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft13;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft14;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft15;
        private DevExpress.XtraBars.BarButtonItem btnOnlyShowCarft16;
        private DevExpress.XtraBars.BarSubItem btnLockFigureLayerItem;
        private DevExpress.XtraBars.BarCheckItem ckLockCarftBackground;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft1;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft2;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft3;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft4;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft5;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft6;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft7;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft8;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft9;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft10;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft11;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft12;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft13;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft14;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft15;
        private DevExpress.XtraBars.BarCheckItem ckLockCarft16;
        private DevExpress.XtraBars.BarButtonItem btnDXFFigureMap;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnRectangleItem;
        private DevExpress.XtraBars.BarButtonItem btnRectangle;
        private DevExpress.XtraBars.BarButtonItem btnFilletRectangle;
        private DevExpress.XtraBars.BarButtonItem btnTrackRectangle;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnCircleItem;
        private DevExpress.XtraBars.BarButtonItem btnCircle;
        private DevExpress.XtraBars.BarButtonItem btnThreePointArc;
        private DevExpress.XtraBars.BarButtonItem btnScanArc;
        private DevExpress.XtraBars.BarButtonItem btnNewEllipse;
        private DevExpress.XtraBars.BarButtonItem btnCircleReplaceToAcnode;
        private DevExpress.XtraBars.BarButtonItem btnReplaceToCircle;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnMultiLineItem;
        private DevExpress.XtraBars.BarButtonItem btnMultiLine;
        private DevExpress.XtraBars.BarButtonItem btnPolygon;
        private DevExpress.XtraBars.BarButtonItem btnStellate;
        private DevExpress.XtraBars.BarButtonItem btnDot;
        private DevExpress.XtraBars.BarButtonItem btnWord;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnGroupItem;
        private DevExpress.XtraBars.BarButtonItem btnGroupSelectAll;
        private DevExpress.XtraBars.BarButtonItem btnGroupScatterSelected;
        private DevExpress.XtraBars.BarButtonItem btnGroupScatterAll;
        private DevExpress.XtraBars.BarButtonItem btnGroupBlowOpen;
        private DevExpress.XtraBars.BarButtonItem btnMultiContourCotangent;
        private DevExpress.XtraBars.BarButtonItem btnBlowOpenMultiContourCotangent;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnFindEdgeItem;
        private DevExpress.XtraBars.BarCheckItem ckCapacitanceEdge;
        private DevExpress.XtraBars.BarCheckItem ckDotEdge;
        private DevExpress.XtraBars.BarCheckItem ckManualEdge;
        private DevExpress.XtraBars.BarCheckItem ckAutoClearEdgeProcessed;
        private DevExpress.XtraBars.BarCheckItem ckClearEdgeAngle;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnPLCProcessItem;
        private DevExpress.XtraBars.BarButtonItem btnEditPLCProcess;
        private DevExpress.XtraBars.BarSubItem btnExecutePLCProcess;
        private DevExpress.XtraBars.BarButtonItem btnChangeToWorkbenchA;
        private DevExpress.XtraBars.BarButtonItem btnChangeToWorkbenchB;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess1;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess2;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess3;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess4;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess5;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess6;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess7;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess8;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess9;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess10;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess11;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess12;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess13;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess14;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess15;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess16;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess17;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess18;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess19;
        private DevExpress.XtraBars.BarButtonItem btnCustomProcess20;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnGoOriginItem;
        private DevExpress.XtraBars.BarButtonItem btnGoOriginAll;
        private DevExpress.XtraBars.BarButtonItem btnGoOriginX;
        private DevExpress.XtraBars.BarButtonItem btnGoOriginY;
        private DevExpress.XtraBars.BarButtonItem btnGantryInit;
        private DevExpress.XtraBars.BarCheckItem ckExecuteGantrySyncGoOrigin;
        private DevExpress.XtraBars.BarCheckItem ckAdjustHeightenGoOrigin;
        private DevExpress.XtraBars.BarCheckItem ckElectricFocusgoOrigin;
        private DevExpress.XtraBars.BarSubItem btnBCS100Item;
        private DevExpress.XtraBars.BarButtonItem btnBCSStop;
        private DevExpress.XtraBars.BarButtonItem btnBCSGoBerth;
        private DevExpress.XtraBars.BarButtonItem btnBCSGoOrigin;
        private DevExpress.XtraBars.BarSubItem btnBCSFollowItem;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollow1mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollow2mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollow3mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollow4mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollow5mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollow6mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollow7mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollow8mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollow9mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollow10mm;
        private DevExpress.XtraBars.BarSubItem btnBCSFollowStopItem;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop1mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop2mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop3mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop4mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop5mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop6mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop7mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop8mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop9mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop10mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop15mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop20mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop25mm;
        private DevExpress.XtraBars.BarButtonItem btnBCSFollowStop30mm;
        private DevExpress.XtraBars.BarSubItem btnEmptyMoveRelativeItem;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelative1mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelative2mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelative3mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelative4mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelative5mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelative6mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelative7mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelative8mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelative9mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelative10mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelativeMinus1mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelativeMinus2mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelativeMinus3mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelativeMinus4mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelativeMinus5mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelativeMinus6mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelativeMinus7mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelativeMinus8mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelativeMinus9mm;
        private DevExpress.XtraBars.BarButtonItem btnEmptyMoveRelativeMinus10mm;
        private DevExpress.XtraBars.BarSubItem btnMoveAbsolutePositionItem;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ0;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ1;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ2;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ3;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ4;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ5;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ6;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ7;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ8;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ9;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ10;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ15;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ20;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ25;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ30;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ40;
        private DevExpress.XtraBars.BarButtonItem btnMoveAbsolutePositionZ50;
        private DevExpress.XtraBars.BarButtonItem btnBCS100Monitor;
        private DevExpress.XtraBars.BarButtonItem btnBCSKeyCalibration;
        private DevExpress.XtraBars.BarButtonItem btnBCSRegisteredEncrypt;
        private DevExpress.XtraBars.BarButtonItem btnBCSUpdate;
        private DevExpress.XtraBars.BarButtonItem btnBCSSaveParams;
        private DevExpress.XtraBars.BarButtonItem btnBCSRecordZCoordinate;
        private DevExpress.XtraBars.BarButtonItem btnErrorMeasure;
        private DevExpress.XtraBars.BarButtonItem btnSelect;
        private DevExpress.XtraBars.BarButtonItem btnEditNode;
        private DevExpress.XtraBars.BarButtonItem btnTranslateView;
        private DevExpress.XtraBars.BarButtonItem btnSortMode;
        private DevExpress.XtraBars.BarSubItem btnLanguageItem;
        private DevExpress.XtraBars.BarCheckItem ckEnglish;
        private DevExpress.XtraBars.BarCheckItem ckSimplifiedChinese;
        private DevExpress.XtraBars.BarCheckItem ckTraditionalChinese;
        private DevExpress.XtraBars.BarButtonItem btnBackFileBottomFigure;
        private DevExpress.XtraBars.BarSubItem btnSystemUnitItem;
        private DevExpress.XtraBars.BarCheckItem ckMetricUnit;
        private DevExpress.XtraBars.BarCheckItem ckImperailUnit;
        private DevExpress.XtraBars.BarSubItem btnPartItem;
        private DevExpress.XtraBars.BarButtonItem btnImportPart;
        private DevExpress.XtraBars.BarButtonItem btnImportStandardPart;
        private DevExpress.XtraBars.BarButtonItem btnDeleteAllLayoutPart;
        private DevExpress.XtraBars.BarSubItem btnSheetMaterialItem;
        private DevExpress.XtraBars.BarButtonItem btnSetSheetMaterial;
        private DevExpress.XtraBars.BarButtonItem btnDeleteAllSheetMaterial;
        private DevExpress.XtraBars.BarButtonItem btnNewFile;
        private DevExpress.XtraBars.BarButtonItem btnSaveFile;
        private DevExpress.XtraBars.BarDockingMenuItem barDockingMenuItem1;
        private DevExpress.XtraBars.BarSubItem btnSaveAsItem;
        private DevExpress.XtraBars.BarButtonItem btnSaveFileWXF;
        private DevExpress.XtraBars.BarButtonItem btnSaveFileAutoCADDXF;
        private DevExpress.XtraBars.BarSubItem btnLayoutItem;
        private DevExpress.XtraBars.BarSubItem btnImportItem;
        private DevExpress.XtraBars.BarButtonItem btnImportFileLXD;
        private DevExpress.XtraBars.BarButtonItem btnImportFileDXF;
        private DevExpress.XtraBars.BarButtonItem btnImportFilePLT;
        private DevExpress.XtraBars.BarButtonItem btnImportFileAI;
        private DevExpress.XtraBars.BarButtonItem btnImportFileGerBar;
        private DevExpress.XtraBars.BarButtonItem btnImportFileNC;
        private DevExpress.XtraBars.BarSubItem btnReportItem;
        private DevExpress.XtraBars.BarButtonItem btnProcessingReport;
        private DevExpress.XtraBars.BarButtonItem btnLayoutReport;
        private DevExpress.XtraBars.BarButtonItem btnUseReport;
        private DevExpress.XtraBars.BarSubItem btnUserSetItem;
        private DevExpress.XtraBars.BarButtonItem btnSetUserParam;
        private DevExpress.XtraBars.BarButtonItem btnBackUpParam;
        private DevExpress.XtraBars.BarSubItem btnDiagnoseToolsItem;
        private DevExpress.XtraBars.BarButtonItem btnControlCardMonitor;
        private DevExpress.XtraBars.BarButtonItem btnExtendIOMonitor;
        private DevExpress.XtraBars.BarButtonItem btnGasDACorrection;
        private DevExpress.XtraBars.BarButtonItem btnBurnTest;
        private DevExpress.XtraBars.BarSubItem barSubItem39;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnLeadInOrOutWireItem;
        private DevExpress.XtraBars.BarButtonItem btnCheckLeadInOrOutWire;
        private DevExpress.XtraBars.BarButtonItem btnLeadInOrOutWireDifferentiateMode;
        private DevExpress.XtraBars.BarButtonItem barButtonItem65;
        private DevExpress.XtraBars.BarButtonItem barButtonItem66;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnFigureSizeItem;
        private DevExpress.XtraBars.BarButtonItem btnFigureSize100mm;
        private DevExpress.XtraBars.BarButtonItem btnFigureSize200mm;
        private DevExpress.XtraBars.BarButtonItem btnFigureSize0_5;
        private DevExpress.XtraBars.BarButtonItem btnFigureSize2;
        private DevExpress.XtraBars.BarButtonItem btnFigureSize4;
        private DevExpress.XtraBars.BarButtonItem btnFigureSize8;
        private DevExpress.XtraBars.BarButtonItem btnFigureSize10;
        private DevExpress.XtraBars.BarButtonItem btnReleaseAngle;
        private DevExpress.XtraBars.BarButtonItem btnRoundAngle;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnCoolingDotItem;
        private DevExpress.XtraBars.BarButtonItem btnCoolingDotAuto;
        private DevExpress.XtraBars.BarButtonItem btnCoolingDotClear;
        private DevExpress.XtraBars.BarButtonItem btnFrontMost;
        private DevExpress.XtraBars.BarButtonItem btnBackMost;
        private DevExpress.XtraBars.BarButtonItem btnForward;
        private DevExpress.XtraBars.BarButtonItem btnBackward;
        private DevExpress.XtraBars.BarButtonItem btnPreviousTarget;
        private DevExpress.XtraBars.BarButtonItem btnNextTarget;
        private Views.CustomControl.Menu.ComboHeadBarSubItem btnStockLayoutItem;
        private DevExpress.XtraBars.BarButtonItem btnStockLayoutClear;
        private DevExpress.XtraBars.BarSubItem btnProcessTaskItem;
        private DevExpress.XtraBars.BarButtonItem btnSaveProcessTask;
        private DevExpress.XtraBars.BarButtonItem btnLoadProcessTask;
        private DevExpress.XtraBars.BarButtonItem btnLightPathDebug;
        private DevExpress.XtraBars.BarButtonItem barButtonItem266;
        private DevExpress.XtraBars.BarButtonItem barButtonItem275;
        private DevExpress.XtraBars.BarButtonItem barButtonItem279;
        private DevExpress.XtraBars.BarButtonItem btnQCW;
        private DevExpress.XtraBars.BarButtonItem btnGantryErrorMonitor;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar4;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup5;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup6;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup11;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup7;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup8;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup9;
        private DevExpress.XtraBars.BarEditItem btnSortRangeTagets;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribPageCommon;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribPageGroupView;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribPageGroupTransform;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribPageGroupProcessSet;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribPageGroupTool1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribPageGroupParam;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribPageLayout;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribPageGroupLayout;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribPageCNC;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribPageGroupProcess;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribPageGroupTool2;
        private DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit repositoryItemPopupContainerEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemRangeTrackBar repositoryItemRangeTrackBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar3;
        private System.Windows.Forms.Panel panelRightMain;
        private Views.CustomControl.RightPanel.UCMachineControl ucMachineControl1;
        private Views.CustomControl.Draw.CanvasWrapper canvasWrapper1;
        private DevExpress.XtraEditors.PanelControl panelMain;
        private DevExpress.XtraBars.BarButtonItem btnOpenFile;
        private Views.CustomControl.RightPanel.UCMachineCount ucMachineCount1;
        private Views.CustomControl.RightPanel.UCMannual ucMannual1;
        private Views.CustomControl.Operation.UCLayerParaBar ucLayerParaBar1;
    }
}

