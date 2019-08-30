using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.DrawModel.RingCut;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawTool;
using WSX.DrawService.DrawTool.CircleTool;
using WSX.DrawService.DrawTool.MultiSegmentLine;
using WSX.DrawService.DrawTool.MultiSegmentLine.MultiLine;
using WSX.DrawService.Layers;
using WSX.DrawService.Utils;
using WSX.GlobalData.Messenger;
using WSX.GlobalData.Model;
using System.Windows.Media;
using WSX.CommomModel.DrawModel.LeadLine;

namespace WSX.DrawService.Wrapper
{
    public partial class DrawingComponent : UserControl, ICanvasOwner
    {
        private UCCanvas myCanvas;
        private DataModel dataModel;
        private PointF startPointRuler = new PointF();
        private PointF endPointRuler = new Point();
        private Dictionary<DrawObjectBase, int> backupMap = new Dictionary<DrawObjectBase, int>();

        #region Public Properties

        public UCCanvas UCCanvas { get { return myCanvas; } }

        public bool HRulerVisible
        {
            get
            {
                return this.panelHRuler.Visible;
            }
            set
            {
                this.panelHRuler.Visible = value;
            }
        }

        public bool VRulerVisible
        {
            get
            {
                return this.panelVRuler.Visible;
            }
            set
            {
                this.panelVRuler.Visible = value;
            }
        }

        public bool CanvasEnabled
        {
            get
            {
                return this.myCanvas.CanvasEnabled;
            }
            set
            {
                this.myCanvas.CanvasEnabled = value;
            }
        }

        #endregion Public Properties

        #region Events

        public event Action<object, PositionChangedEventArgs> OnPositionChanged;

        #endregion Events

        #region Public Methods

        public void ExcuteCommand(CanvasCommands cmd)
        {
            this.myCanvas.CommandEscape();
            if (cmd == CanvasCommands.SelectMode)
            {
                this.btnSelectPattern.CheckState = CheckState.Checked;
                this.btnSortMode.CheckState = CheckState.Unchecked;
                this.btnTranslationView.CheckState = CheckState.Unchecked;
                this.btnNodeEditMode.CheckState = CheckState.Unchecked;
                this.myCanvas.CommandSelectPattern();
            }
            else if (cmd == CanvasCommands.Pan)
            {
                this.btnSelectPattern.CheckState = CheckState.Unchecked;
                this.btnSortMode.CheckState = CheckState.Unchecked;
                this.btnTranslationView.CheckState = CheckState.Checked;
                this.btnNodeEditMode.CheckState = CheckState.Unchecked;
                this.myCanvas.CommandTranslationView();
            }
        }

        public void InvidateCanvas(bool doStatic = true)
        {
            this.myCanvas.DoInvalidate(doStatic);
        }

        public void UpdateMarkPoint(PointF point)
        {
            this.myCanvas.UpdateMarkPoint(point);
        }

        public void AddMarkPathPoint(string id, PointF point)
        {
            this.myCanvas.AddMarkPathPoint(id, point);
        }

        public void ClearMark()
        {
            this.myCanvas.ClearMark();
        }

        public bool StaticRefreshMark
        {
            get
            {
                return this.myCanvas.StaticRefreshMark;
            }
            set
            {
                this.myCanvas.StaticRefreshMark = value;
                this.InvidateCanvas(true);
            }
        }

        public RectangleF GetRegion()
        {
            return this.myCanvas.GetRegion();
        }

        public void UpdateMarkFlag(PointF p, System.Drawing.Color color)
        {
            this.myCanvas.UpdataMarkFlag(p, color);
            this.InvidateCanvas(true);
        }

        public void UpdateRelativePos(PointF p)
        {
            this.myCanvas.UpdateRelativePos(p);
            this.InvidateCanvas(true);
        }

        public List<IDrawObject> GetDrawObjects()
        {
            return this.dataModel.DrawingLayer.Objects;
        }

        /// <summary>
        /// 对刀
        /// </summary>
        /// <param name="offset"></param>
        public void MoveAll(PointF offset)
        {
            var drawObjects = this.GetDrawObjects();
            foreach (var m in drawObjects)
            {
                m.Move(new UnitPoint(offset));
            }
            this.myCanvas.DoInvalidate(true);
        }

        public void UpdateOutline(RectangleF rect)
        {
            var gridLayer = this.dataModel.GridLayer as GridLayer;
            gridLayer.UpdateOutline(rect);
            this.InvidateCanvas(true);
        }

        public void SetCanvasView(RectangleF rect)
        {
            if (rect.Width == 0 || rect.Height == 0)
            {
                return;
            }

            var p1 = this.myCanvas.ScreenTopLeftToUnitPoint();
            var p2 = this.myCanvas.ScreenBottomRightToUnitPoint();

            double width = p2.X - p1.X;
            double height = p1.Y - p2.Y;

            double xRatio = width / rect.Width;
            double yRatio = height / rect.Height;
         
            this.dataModel.Zoom *= (float)(xRatio > yRatio ? yRatio : xRatio);
            this.myCanvas.SetCenter(new UnitPoint(rect.X + rect.Width / 2.0, rect.Y + rect.Height / 2.0));

            this.myCanvas.RaiseScaleChangedEvent();
        }

        #endregion Public Methods

        #region Private methods

        private void UpdateData()
        {
            // update any additional properties of data which is not part of the interface
            this.dataModel.CenterPoint = this.myCanvas.GetCenter();
        }

        #endregion Private methods

        public DrawingComponent()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.dataModel = new DataModel();
            this.myCanvas = new UCCanvas(this, this.dataModel);
            this.myCanvas.Dock = DockStyle.Fill;
            this.panelDrawing.Controls.Add(this.myCanvas);
            this.myCanvas.SetCenter(new UnitPoint(0, 0));
            this.myCanvas.RunningSnaps = new Type[]
            {
                typeof(VertextSnapPoint),typeof(MidpointSnapPoint),typeof(IntersectSnapPoint),
                typeof(QuadrantSnapPoint),typeof(CenterSnapPoint),typeof(DivisionSnapPoint)
            };
            this.myCanvas.AddQuickSnapType(Keys.N, typeof(NearestSnapPoint));
            this.myCanvas.AddQuickSnapType(Keys.M, typeof(MidpointSnapPoint));
            this.myCanvas.AddQuickSnapType(Keys.I, typeof(IntersectSnapPoint));
            this.myCanvas.AddQuickSnapType(Keys.V, typeof(VertextSnapPoint));
            this.myCanvas.AddQuickSnapType(Keys.P, typeof(PerpendicularSnapPoint));
            this.myCanvas.AddQuickSnapType(Keys.Q, typeof(QuadrantSnapPoint));
            this.myCanvas.AddQuickSnapType(Keys.C, typeof(CenterSnapPoint));
            //this.myCanvas.AddQuickSnapType(Keys.T, typeof(TangentSnapPoint));
            this.myCanvas.AddQuickSnapType(Keys.D, typeof(DivisionSnapPoint));

            this.myCanvas.KeyDown += OnCanvasKeyDown;
            this.myCanvas.OnScaleChanged += MyCanvas_OnScaleChanged;
            this.CanvasEnabled = true;
        }

        private void DrawingComponent_Load(object sender, EventArgs e)
        {
            this.dataModel.AddDrawTool(CanvasCommands.Lines.ToString(), new LineCommon());
            this.dataModel.AddDrawTool(CanvasCommands.CircleCR.ToString(), new DrawTool.CircleTool.Circle());
            this.dataModel.AddDrawTool(CanvasCommands.ArcCR.ToString(), new DrawTool.Arcs.SweepArc());
            this.dataModel.AddDrawTool(CanvasCommands.Arc3P.ToString(), new DrawTool.Arcs.ThreePointsArc());
            this.dataModel.AddDrawTool(CanvasCommands.SingleDot.ToString(), new SingleDot());
            this.dataModel.AddDrawTool(CanvasCommands.SingleRectangle.ToString(), new RectangleCommon());
            this.dataModel.AddDrawTool(CanvasCommands.Hexagon.ToString(), new PolygonCommon());
            this.dataModel.AddDrawTool(CanvasCommands.StarCommon.ToString(), new StarCommon());
            this.dataModel.AddDrawTool(CanvasCommands.RoundRect.ToString(), new RectangleRound());
            this.dataModel.AddDrawTool(CanvasCommands.MultiLine.ToString(), new PolyLine());
            //this.dataModel.AddDrawTool(CanvasCommands.Group.ToString(), new Group());
            this.myCanvas.DoInvalidate(true);
        }

        #region Canvas and DataModel Callback

        private void OnCanvasKeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys != Keys.None)
            {
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.myCanvas.CommandEscape();
            }
        }

        private void MyCanvas_OnScaleChanged(object sender, ScaleChangedEventArgs e)
        {
            bool flag = false;
            if (e.StartPoint != this.startPointRuler)
            {
                flag = true;
                this.startPointRuler = e.StartPoint;
            }
            if (e.EndPoint != this.endPointRuler)
            {
                flag = true;
                this.endPointRuler = e.EndPoint;
            }
            if (flag)
            {
                this.rulerControlH.Max = this.endPointRuler.X;
                this.rulerControlH.Min = this.startPointRuler.X;
                this.panelHRuler.Invalidate(true);
                this.rulerControlV.Max = this.startPointRuler.Y;
                this.rulerControlV.Min = this.endPointRuler.Y;
                this.panelVRuler.Invalidate(true);
            }
        }

        public void SetPositionInfo(UnitPoint unitPoint)
        {
            this.OnPositionChanged?.Invoke(this, new PositionChangedEventArgs { CurrentPoint = unitPoint });
            this.rulerControlH.Current = unitPoint.X;
            this.rulerControlH.Invalidate();
            this.rulerControlV.Current = unitPoint.Y;
            this.rulerControlV.Invalidate();
        }

        public void SetSnapInfo(ISnapPoint snap)
        {
        }

        public void OnLayerIdChanged()
        {
            foreach (DrawObjectBase item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                if (item.LayerId == (int)LayerId.White)
                {
                    if (item.IsBoard == false)
                    {
                        continue;
                    }
                }
                item.LayerId = (int)GlobalModel.CurrentLayerId;
            }
            this.DoRefresh();
        }

        public void OnMachineEnabledChanged(bool enabled)
        {
            if (this.backupMap.Any())
            {
                var items = this.dataModel.DrawingLayer.Objects.OfType<DrawObjectBase>();
                var tmp = new Dictionary<DrawObjectBase, int>();
                this.backupMap.Where(x => items.Contains(x.Key)).ToList()
                              .ForEach(x => tmp.Add(x.Key, x.Value));
                this.backupMap = tmp;
            }

            foreach (DrawObjectBase item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                if (enabled)
                {
                    if (this.backupMap.Keys.Contains(item))
                    {
                        item.LayerId = this.backupMap[item];
                        this.backupMap.Remove(item);
                    }
                }
                else
                {
                    this.backupMap[item] = item.LayerId;
                    item.LayerId = (int)LayerId.White;
                }
            }

            this.DoRefresh();
        }

        #endregion Canvas and DataModel Callback

        #region 工具条

        private void btnSelectPattern_Click(object sender, EventArgs e)
        {
            if (this.CanvasEnabled)//&& this.btnSelectPattern.CheckState != CheckState.Checked)
            {
                this.btnSelectPattern.CheckState = CheckState.Checked;
                this.btnSortMode.CheckState = CheckState.Unchecked;
                this.btnTranslationView.CheckState = CheckState.Unchecked;
                this.btnNodeEditMode.CheckState = CheckState.Unchecked;
                this.myCanvas.CommandSelectPattern();
            }
        }

        private void btnTranslationView_Click(object sender, EventArgs e)
        {
            if (this.CanvasEnabled)// && this.btnTranslationView.CheckState != CheckState.Checked)
            {
                this.btnSelectPattern.CheckState = CheckState.Unchecked;
                this.btnSortMode.CheckState = CheckState.Unchecked;
                this.btnTranslationView.CheckState = CheckState.Checked;
                this.btnNodeEditMode.CheckState = CheckState.Unchecked;
                this.myCanvas.CommandTranslationView();
            }
        }

        private void btnSortMode_Click(object sender, EventArgs e)
        {
            if (this.CanvasEnabled)// && this.btnSortMode.CheckState != CheckState.Checked)
            {
                this.btnSelectPattern.CheckState = CheckState.Unchecked;
                this.btnSortMode.CheckState = CheckState.Checked;
                this.btnTranslationView.CheckState = CheckState.Unchecked;
                this.btnNodeEditMode.CheckState = CheckState.Unchecked;
                this.myCanvas.CommandSortPattern();
            }
        }

        private void btnNodeEditMode_Click(object sender, EventArgs e)
        {
            if (this.CanvasEnabled)// && this.btnNodeEditMode.CheckState != CheckState.Checked)
            {
                this.btnSelectPattern.CheckState = CheckState.Unchecked;
                this.btnSortMode.CheckState = CheckState.Unchecked;
                this.btnTranslationView.CheckState = CheckState.Unchecked;
                this.btnNodeEditMode.CheckState = CheckState.Checked;
                this.myCanvas.CommandNodeEidtPattern();
            }
        }

        private void toolBtnRedo_Click(object sender, EventArgs e)
        {
            if (!this.CanvasEnabled)
            {
                return;
            }
            if (this.dataModel.DoRedo())
            {
                this.myCanvas.DoInvalidate(true);
            }
        }

        private void toolBtnUndo_Click(object sender, EventArgs e)
        {
            if (!this.CanvasEnabled)
            {
                return;
            }
            if (this.dataModel.DoUndo())
            {
                this.myCanvas.DoInvalidate(true);
            }
        }

        private void toolBtnDrawingTool_Click(object sender, EventArgs e)
        {
            if (this.CanvasEnabled)
            {
                string toolId = string.Empty;
                if (sender is ToolStripButton)
                {
                    var btn = sender as ToolStripButton;
                    toolId = (string)btn.Tag;
                }
                else if (sender is ToolStripDropDownItem)
                {
                    var btn = sender as ToolStripDropDownItem;
                    toolId = (string)btn.Tag;
                }
                if (toolId != string.Empty)
                {
                    this.myCanvas.CommandSelectDrawTool(toolId);
                }
            }
        }

        private void toolBtnAutoChamfer_Click(object sender, EventArgs e)
        {
            if (this.dataModel.DrawingLayer.SelectedObjects.Count > 0)
            {
                Messenger.Instance.Send("OnAutoRoundAngle", null);
            }
        }

        #endregion 工具条

        #region Utility

        private bool SelectEventEnabled
        {
            set
            {
                ((DataModel)this.dataModel).SelectEventEnabled = value;
            }
        }

        #endregion Utility

        #region 图形变换

        #region 平移

        public void MovePosition()
        {
            this.myCanvas.CommandMovePosition();
        }

        public void InteractScale()
        {
            this.myCanvas.CommandInteractScale();
        }

        public RectangleF GetDrawingObjectSize()
        {
            UnitPoint maxPoint, minPoint;
            this.GetAllDrawingsMaxMinPoint(out maxPoint, out minPoint);
            RectangleF rectangleF = new RectangleF((float)(minPoint.X), (float)(maxPoint.Y), (float)(maxPoint.X - minPoint.X), (float)(maxPoint.Y - minPoint.Y));
            float th = (float)UCCanvas.GetThresholdWidth() / 2;
            rectangleF.Inflate(-th, -th);
            return rectangleF;
        }

        public void DoSizeChange(double centerX, double centerY, double width, double height)
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                drawObjects.Add(item);
            }
            this.dataModel.DoSizeChange(drawObjects, centerX, centerY, width, height);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnRotate(double rotateAngle, bool isClockwise)
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            UnitPoint centerAxis = this.GetBoundingRectangleCenterPoint();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                drawObjects.Add(item);
            }
            this.dataModel.DoRotate(drawObjects, centerAxis, rotateAngle, isClockwise);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnRotateAny()
        {
            this.myCanvas.CommandRotateAny();
        }

        public void OnMirrorAny()
        {
            this.myCanvas.CommandMirrorAny();
        }
      
        public void DoClearLeadWire()
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            LineInOutParamsModel leadwireModel = new LineInOutParamsModel();
            List<LineInOutParamsModel> leadwireModels = new List<LineInOutParamsModel>();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                if (item.FigureType != FigureTypes.Point)
                {
                    drawObjects.Add(item);
                    leadwireModels.Add(leadwireModel);//给空的引入引出线对象给图形
                }
            }
            this.dataModel.DoSetLeadwire(drawObjects, leadwireModels);
            this.myCanvas.DoInvalidate(true);
        }

        public void DoSetLeadwire(LineInOutParamsModel leadwireModel)
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            List<LineInOutParamsModel> leadwireModels = new List<LineInOutParamsModel>();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                if (item.FigureType != FigureTypes.Point)
                {
                    if (leadwireModel.IsOnlyApplyClosedFigure)
                    {
                        if (item.IsCloseFigure)
                        {
                            drawObjects.Add(item);
                            leadwireModels.Add(leadwireModel);
                        }
                    }
                    else
                    {
                        drawObjects.Add(item);
                        leadwireModels.Add(leadwireModel);
                    }
                }
            }
            this.dataModel.DoSetLeadwire(drawObjects, leadwireModels);
            this.myCanvas.DoInvalidate(true);
        }

        /// <summary>
        /// 检查引线(只做了圆弧与多段线两种封闭图形)
        /// </summary>
        /// <param name="InterOrExterModel">是否区分内外模</param>
        public void DoCheckLeadInOrOutWire(bool InterOrExterModel)
        {
            this.myCanvas.CommandEscape();
            bool checkFlag = true;
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            Dictionary<int, LeadInParam> leadInLineParamsDic = new Dictionary<int, LeadInParam>();
            Dictionary<int, LeadOutParam> leadOutLineParamsDic = new Dictionary<int, LeadOutParam>();
            List<IDrawObject> originalListObjects = this.dataModel.DrawingLayer.Objects.ConvertAll(x => x.Clone());
            List<Tuple<IDrawObject, IDrawObject>> listIDrawObject = new List<Tuple<IDrawObject, IDrawObject>>();
            List<List<IDrawObject>> sortListDrawObjects = new List<List<IDrawObject>>();
            foreach (var item in this.dataModel.DrawingLayer.Objects)
            {
                if (item.FigureType != FigureTypes.Point)
                {
                    if (item.IsCloseFigure)//不封闭图形图像的考虑
                    {
                        drawObjects.Add(item);
                        switch (item.Id)
                        {
                            case "Circle":
                                var tempObject1 = item as Circle;
                                leadInLineParamsDic.Add(tempObject1.GroupParam.FigureSN, tempObject1.LeadInWireParam);
                                leadOutLineParamsDic.Add(tempObject1.GroupParam.FigureSN, tempObject1.LeadOutWireParam);
                                break;
                            case "MultiLineBase":
                                var tempObject2 = item as MultiSegmentLineBase;
                                leadInLineParamsDic.Add(tempObject2.GroupParam.FigureSN, tempObject2.LeadInWireParam);
                                leadOutLineParamsDic.Add(tempObject2.GroupParam.FigureSN, tempObject2.LeadOutWireParam);
                                break;
                            case "Arc":
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (InterOrExterModel)
            {
                listIDrawObject = this.GetDrawObjectsContainDic(drawObjects);//Item2被包含图形，Item1包含图形
                List<IDrawObject> containDrawObjects = listIDrawObject.ConvertAll(x => x.Item1).Union(listIDrawObject.ConvertAll(x => x.Item2)).ToList();
                if (drawObjects.Count >= containDrawObjects.Count)
                {
                    drawObjects.RemoveAll(x => containDrawObjects.Contains(x));
                }
                sortListDrawObjects = this.SortContainDrawObjects(listIDrawObject);
                foreach (var item in sortListDrawObjects)
                {
                    if (!drawObjects.Contains(item.Last()))
                    {
                        drawObjects.Add(item.Last());
                    }
                }
            }
            //不具包含关系的图形(即没有包含任何其他图形也没有被任何其他图形包含,有包含图形的选其最外层的那个)检查引线是否与其他图形边界有干涉
            //一个图形的引线与每一个图形的边界去检查是否有干涉
            foreach (var item in drawObjects)
            {
                LeadInParam tempLeadinParam = leadInLineParamsDic[item.GroupParam.FigureSN];//在IDrawObject中没有此参数不能直接调用
                LeadOutParam tempLeadOutParam = leadOutLineParamsDic[item.GroupParam.FigureSN];
                if (tempLeadinParam != null && item.LeadIn.LeadType != LeadLineType.None)
                {
                    if (tempLeadinParam.StartPoint != new UnitPoint(0, 0))
                    {
                        //引线图形为圆弧或者直线或者直线与圆弧(加焊洞LeadHole)与画布中的图形的边界是否有干涉
                        foreach (var item1 in drawObjects)
                        {
                            if (InterOrExterModel)
                            {
                                item.SetOuterCut();  //区分内外模时，单个的图形均为阳切
                            }
                            //不区分内外模时,不能相交，为空或者完全包含均可以   
                            checkFlag = CheckLeadInLineRecursive(tempLeadinParam, item, item1, IntersectionDetail.Intersects);
                        }
                    }
                }
                if (tempLeadOutParam != null && item.LeadOut.LeadType != LeadLineType.None)
                {
                    if (tempLeadOutParam.EndPoint != new UnitPoint(0, 0))
                    {
                        foreach (var item1 in drawObjects)
                        {
                            if (InterOrExterModel)
                            {
                                item.SetOuterCut();
                            }
                            checkFlag = CheckLeadOutLineRecursive(tempLeadOutParam, item, item1, IntersectionDetail.Intersects);
                        }
                    }
                }
            }
            #region 内外模判断
            if (InterOrExterModel)
            {
                foreach (var item in listIDrawObject) //具有包含关系的图形的引线的判断(内外模判断(根据包含关系最外层为阳切，次层为阴切，再次层为阳切以此类推))
                {
                    LeadInParam tempLeadInParam1 = leadInLineParamsDic[item.Item1.GroupParam.FigureSN];
                    LeadOutParam tempLeadOutParam1 = leadOutLineParamsDic[item.Item1.GroupParam.FigureSN];
                    LeadInParam tempLeadInParam2 = leadInLineParamsDic[item.Item2.GroupParam.FigureSN];
                    LeadOutParam tempLeadOutParam2 = leadOutLineParamsDic[item.Item2.GroupParam.FigureSN];
                    bool interOrExterFlag = this.DistinguishInterOrExterModel(sortListDrawObjects, item);
                    if (tempLeadInParam2 != null && item.Item2.LeadIn.LeadType != LeadLineType.None)
                    {
                        if (tempLeadInParam2.StartPoint != new UnitPoint(0, 0))//Item2引入线判断
                        {
                            if (interOrExterFlag)//true----->Item1:外 Item2:内   //Item2被包含图形，Item1包含图形
                            {
                                item.Item2.SetInnerCut();
                                checkFlag = CheckLeadInLineRecursive(tempLeadInParam2, item.Item2, item.Item2, IntersectionDetail.Intersects);
                            }
                            else//false  Item1:内模，Item2:外膜   //Item2被包含图形，Item1包含图形
                            {
                                item.Item2.SetOuterCut();
                                checkFlag = CheckLeadInLineRecursive(tempLeadInParam2, item.Item2, item.Item1, IntersectionDetail.Intersects);
                                checkFlag = CheckLeadInLineRecursive(tempLeadInParam2, item.Item2, item.Item2, IntersectionDetail.Intersects);
                            }
                        }
                    }
                    if (tempLeadOutParam2 != null && item.Item2.LeadOut.LeadType != LeadLineType.None)
                    {
                        if (tempLeadOutParam2.EndPoint != new UnitPoint(0, 0))//Item2引出线判断
                        {
                            if (interOrExterFlag)
                            {
                                item.Item2.SetInnerCut();
                                checkFlag = CheckLeadOutLineRecursive(tempLeadOutParam2, item.Item2, item.Item2, IntersectionDetail.Intersects);
                            }
                            else
                            {
                                item.Item2.SetOuterCut();
                                checkFlag = CheckLeadOutLineRecursive(tempLeadOutParam2, item.Item2, item.Item1, IntersectionDetail.Intersects);
                                checkFlag = CheckLeadOutLineRecursive(tempLeadOutParam2, item.Item2, item.Item2, IntersectionDetail.Intersects);
                            }
                        }
                    }
                    if (tempLeadInParam1 != null && item.Item1.LeadIn.LeadType != LeadLineType.None)
                    {
                        if (tempLeadInParam1.StartPoint != new UnitPoint(0, 0))//Item1引入线判断
                        {
                            if (interOrExterFlag)
                            {
                                item.Item1.SetOuterCut();
                                checkFlag = CheckLeadInLineRecursive(tempLeadInParam1, item.Item1, item.Item1, IntersectionDetail.Intersects);
                            }
                            else
                            {
                                item.Item1.SetInnerCut();
                                checkFlag = CheckLeadInLineRecursive(tempLeadInParam1, item.Item1, item.Item1, IntersectionDetail.Intersects);
                                checkFlag = CheckLeadInLineRecursive(tempLeadInParam1, item.Item1, item.Item2, IntersectionDetail.Intersects);
                            }
                        }
                    }
                    if (tempLeadOutParam1 != null && item.Item1.LeadOut.LeadType != LeadLineType.None)
                    {
                        if (tempLeadOutParam1.EndPoint != new UnitPoint(0, 0))//Item1引出线判断
                        {
                            if (interOrExterFlag)
                            {
                                item.Item1.SetOuterCut();
                                checkFlag = CheckLeadOutLineRecursive(tempLeadOutParam1, item.Item1, item.Item1, IntersectionDetail.Intersects);
                            }
                            else
                            {
                                item.Item1.SetInnerCut();
                                checkFlag = CheckLeadOutLineRecursive(tempLeadOutParam1, item.Item1, item.Item1, IntersectionDetail.Intersects);
                                checkFlag = CheckLeadOutLineRecursive(tempLeadOutParam1, item.Item1, item.Item2, IntersectionDetail.Intersects);
                            }
                        }
                    }
                }
            }
            #endregion
            this.dataModel.DoRoundAngle(this.dataModel.DrawingLayer.Objects, originalListObjects);
            this.myCanvas.DoInvalidate(true);
            this.myCanvas.CommandEscape();
        }

        /// <summary>
        /// 检查引入线与其他图形是否有干涉(递归)
        /// </summary>
        /// <param name="leadInParam">引入线参数</param>
        /// <param name="IeadLinedrawObject">引入线所属图形</param>
        /// <param name="actDrawObject">判断与引入线是否有干涉的图形</param>
        /// <param name="intersectionDetail">图形与图形的关系</param>
        /// <returns></returns>
        private bool CheckLeadInLineRecursive(LeadInParam leadInParam, IDrawObject IeadLinedrawObject, IDrawObject actDrawObject, IntersectionDetail intersectionDetail)
        {
            if (IeadLinedrawObject.LeadIn.LeadType != LeadLineType.None)
            {
                bool checkFlag = false;
                MultiSegmentLineBase multiSegmentLineBase = this.LeadInLineDrawObjects(IeadLinedrawObject, leadInParam);//把引线组合成一个图形
                if (IeadLinedrawObject.LeadIn.LeadByHole)
                {
                     checkFlag = !CheckLeadCircleRecursive(leadInParam, IeadLinedrawObject, actDrawObject, IntersectionDetail.Intersects);//先判断焊洞的半径，后判断起始点
                }
                if (DrawingOperationHelper.TwoDrawObjectsIntersectByGeometry(multiSegmentLineBase, actDrawObject, false, intersectionDetail)|| checkFlag)
                {
                    if (IeadLinedrawObject.LeadIn.LeadType == LeadLineType.Arc)
                    {
                        //同时改变引线圆的圆心角(2倍引线角)以及引线长度(起始点与引线在图形上的交点(不是圆弧长度)),可以保证计算的图形与实际的图形一致
                        IeadLinedrawObject.LeadIn.Angle /= 2;
                        float newAngle = (float)Math.Sin(IeadLinedrawObject.LeadIn.Angle);
                        IeadLinedrawObject.LeadIn.Length = 2 * leadInParam.Radius * newAngle;
                        if (IeadLinedrawObject.LeadIn.Length < 0.5 || IeadLinedrawObject.LeadIn.Angle < 0.02)//一度是0.01745°
                        {
                            return false;
                        }
                    }
                    if (IeadLinedrawObject.LeadIn.LeadType == LeadLineType.Line)
                    {
                        IeadLinedrawObject.LeadIn.Length /= 2;
                        if (IeadLinedrawObject.LeadIn.Length < 0.5)// 目前设置的最小的自动引入线长度是0.5
                        {
                            return false;
                        }
                    }
                    if (IeadLinedrawObject.LeadIn.LeadType == LeadLineType.LineArc)
                    {
                        if (IeadLinedrawObject.LeadIn.Length < 0.5)
                        {
                            IeadLinedrawObject.LeadIn.Angle /= 2;
                            if (IeadLinedrawObject.LeadIn.Angle < 0.02)// 目前设置的最小的自动引入线角度是0.02   一度是0.01745°
                            {
                                return false;
                            }
                        }
                        else
                        {
                            IeadLinedrawObject.LeadIn.Length /= 2;
                        }
                    }
                    IeadLinedrawObject.Update();
                    return CheckLeadInLineRecursive(leadInParam, IeadLinedrawObject, actDrawObject, intersectionDetail);
                }
            }
            return true;
        }

        /// <summary>
        /// 检查引出线与其他图形是否有干涉(递归)
        /// </summary>
        /// <param name="leadOutParam">引出线参数</param>
        /// <param name="IeadLinedrawObject">引出线所属图形</param>
        /// <param name="actDrawObject">判断与引出线是否有干涉的图形</param>
        /// <param name="intersectionDetail">图形与图形的关系</param>
        /// <returns></returns>
        private bool CheckLeadOutLineRecursive(LeadOutParam leadOutParam, IDrawObject IeadLinedrawObject, IDrawObject actDrawObject, IntersectionDetail intersectionDetail)
        {
            if (IeadLinedrawObject.LeadOut.LeadType != LeadLineType.None)
            {
                MultiSegmentLineBase multiSegmentLineBase = this.LeadOutLineDrawObjects(IeadLinedrawObject, leadOutParam);
                if (DrawingOperationHelper.TwoDrawObjectsIntersectByGeometry(multiSegmentLineBase, actDrawObject, false, intersectionDetail))
                {
                    if (IeadLinedrawObject.LeadOut.LeadType == LeadLineType.Arc)
                    {
                        IeadLinedrawObject.LeadOut.Angle /= 2;
                        float newAngle = (float)Math.Sin(IeadLinedrawObject.LeadOut.Angle);
                        IeadLinedrawObject.LeadOut.Length = 2 * leadOutParam.Radius * newAngle;
                        if (IeadLinedrawObject.LeadOut.Angle < 0.02|| IeadLinedrawObject.LeadOut.Length < 0.5)//一度是0.01745°
                        {
                            return false;
                        }
                    }
                    if (IeadLinedrawObject.LeadOut.LeadType == LeadLineType.Line)
                    {
                        IeadLinedrawObject.LeadOut.Length /= 2;
                        if (IeadLinedrawObject.LeadOut.Length < 0.5)
                        {
                            return false;
                        }
                    }
                    if (IeadLinedrawObject.LeadOut.LeadType == LeadLineType.LineArc)
                    {
                       
                        if (IeadLinedrawObject.LeadOut.Length < 0.5)
                        {
                            IeadLinedrawObject.LeadOut.Angle /= 2;
                            if (IeadLinedrawObject.LeadOut.Angle < 0.02)//一度是0.01745°
                            {
                                return false;
                            }
                        }
                        else
                        {
                            IeadLinedrawObject.LeadOut.Length /= 2;
                        }
                    }
                    IeadLinedrawObject.Update();
                    return CheckLeadOutLineRecursive(leadOutParam, IeadLinedrawObject, actDrawObject, intersectionDetail);
                }
            }
            return true;
        }

        /// <summary>
        /// 检查焊洞与其他图形是否有干涉(递归)
        /// </summary>
        /// <param name="leadInParam">引入线参数</param>
        /// <param name="IeadLinedrawObject">引入线所属图形</param>
        /// <param name="actDrawObject">判断与引入线是否有干涉的图形</param>
        /// <param name="intersectionDetail">图形与图形的关系</param>
        /// <returns></returns>
        private bool CheckLeadCircleRecursive(LeadInParam leadInParam, IDrawObject IeadLinedrawObject,IDrawObject actDrawObject, IntersectionDetail intersectionDetail)
        {
            if (IeadLinedrawObject.LeadIn.LeadType != LeadLineType.None)
            {
                Circle circle = new Circle();
                circle.Radius = IeadLinedrawObject.LeadIn.LeadHoleRadius;
                circle.Center = leadInParam.HoleCenter;
                if (IeadLinedrawObject.LeadIn.LeadByHole)
                {
                    if (DrawingOperationHelper.TwoDrawObjectsIntersectByGeometry(circle, actDrawObject, false, intersectionDetail))
                    {
                        IeadLinedrawObject.LeadIn.LeadHoleRadius /= 3;
                        if (leadInParam.HoleRadius < 0.1)//目前设置的最小的自动引入线焊洞半径是0.2
                        {
                            return false;
                        }
                        IeadLinedrawObject.Update();
                        return CheckLeadCircleRecursive(leadInParam, IeadLinedrawObject, actDrawObject, intersectionDetail);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 通过引出线参数推算出引出线图形
        /// </summary>
        /// <param name="item">引出线所属图形</param>
        /// <param name="tempLeadOutParam">引出线参数</param>
        /// <returns></returns>
        private MultiSegmentLineBase LeadOutLineDrawObjects(IDrawObject item, LeadOutParam tempLeadOutParam)
        {
            double bulge = item.LeadOut.LeadType == LeadLineType.Line ? double.NaN : BulgeHelper.GetBulgFromRadian(HitUtil.DegreesToRadians(tempLeadOutParam.SweepAngle));
            UnitPoint leadOutInterestctPoint = HitUtil.PointOnCircle(tempLeadOutParam.Center, tempLeadOutParam.Radius, tempLeadOutParam.StartAngle);//在父类IDrawObject中没有这个点(在各子类中才有)，只能通过参数反推           
            MultiSegmentLineBase leadLine = new MultiSegmentLineBase();
            leadLine.Points = new List<UnitPointBulge>();
            if (item.LeadOut.LeadType == LeadLineType.LineArc)
            {
                leadLine.Points.Add(new UnitPointBulge(leadOutInterestctPoint, bulge));
                leadLine.Points.Add(new UnitPointBulge(tempLeadOutParam.StartPoint));
            }
            else
            {
                leadLine.Points.Add(new UnitPointBulge(tempLeadOutParam.StartPoint, item.LeadOut.LeadType == LeadLineType.Arc ? bulge : double.NaN));
            }
            leadLine.Points.Add(new UnitPointBulge(tempLeadOutParam.EndPoint));
            return leadLine;
        }

        /// <summary>
        /// 通过引出线参数推算出引入线图形
        /// </summary>
        /// <param name="item">引入线所属图形</param>
        /// <param name="tempLeadinParam">引入线参数</param>
        /// <returns></returns>
        private MultiSegmentLineBase LeadInLineDrawObjects(IDrawObject item, LeadInParam tempLeadinParam)
        {
            double bulge = item.LeadIn.LeadType == LeadLineType.Line ? double.NaN : BulgeHelper.GetBulgFromRadian(HitUtil.DegreesToRadians(tempLeadinParam.SweepAngle));
            UnitPoint leadInInterestctPoint = HitUtil.PointOnCircle(tempLeadinParam.Center, tempLeadinParam.Radius, HitUtil.DegreesToRadians(tempLeadinParam.EndAngle));//在父类IDrawObject中没有这个点(在各子类中才有)，只能通过参数反推
            MultiSegmentLineBase leadLine = new MultiSegmentLineBase();
            leadLine.Points = new List<UnitPointBulge>();
            leadLine.Points.Add(new UnitPointBulge(tempLeadinParam.StartPoint, item.LeadIn.LeadType == LeadLineType.Arc ? bulge : double.NaN));
            if (item.LeadIn.LeadType == LeadLineType.LineArc)
            {
                leadLine.Points.Add(new UnitPointBulge(tempLeadinParam.EndPoint, bulge));
                leadLine.Points.Add(new UnitPointBulge(leadInInterestctPoint));
            }
            else
            {
                leadLine.Points.Add(new UnitPointBulge(tempLeadinParam.EndPoint, double.NaN));
            }
            return leadLine;
        }

        /// <summary>
        /// 区分内外模(元组图形内Item1/Item2根据在排序List中顺序(最外层为外膜,次外层为内膜依此类推)决定)
        /// </summary>
        /// <param name="sortContainDrawObjects">排序List</param>
        /// <param name="aPairDrawObjects">元组图形 (其关系为Item1包含Item2)</param>
        /// <returns></returns>
        public bool DistinguishInterOrExterModel(List<List<IDrawObject>> sortContainDrawObjects, Tuple<IDrawObject, IDrawObject> aPairDrawObjects)
        {
            bool flag = false;
            foreach (var item in sortContainDrawObjects)
            {
                int index1 = item.IndexOf(aPairDrawObjects.Item1) + 1;
                int index2 = item.IndexOf(aPairDrawObjects.Item2) + 1;
                if (index1 != 0 && index2 != 0)
                {
                    if (index1 % 2 == 0)//偶奇  1：内模，2：外膜
                    {
                        flag = item.Count % 2 == 0 ? true : false;
                        break;
                    }
                    if (index2 % 2 == 0)//奇偶  1：外模，2：内模
                    {
                        flag = item.Count % 2 == 0 ? false : true;
                        break;
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// 根据包含关系的元组List,得出具有排序关系的包含List(可根据此List计算最外层为外膜,次外层为内膜依此类推)
        /// </summary>
        /// <param name="listIDrawObject">具有包含关系的元组图形List</param>
        /// <returns></returns>
        public List<List<IDrawObject>> SortContainDrawObjects(List<Tuple<IDrawObject, IDrawObject>> listIDrawObject)
        {
            List<List<IDrawObject>> sortDrawObjects = new List<List<IDrawObject>>();
            var tempDrawObjects = listIDrawObject.ConvertAll(y => y.Item1);
            var endListIDrawObject = listIDrawObject.FindAll(x => tempDrawObjects.Contains(x.Item2) == false);
            foreach (var item in endListIDrawObject)
            {
                sortDrawObjects.Add(new List<IDrawObject>() { item.Item2, item.Item1 });
            }

            for (int i = 0; i < sortDrawObjects.Count; i++)
            {
                for (int j = 0; j < listIDrawObject.Count; j++)
                {
                    if (listIDrawObject[j].Item2 == sortDrawObjects[i].Last())
                    {
                        sortDrawObjects[i].Add(listIDrawObject[j].Item1);
                        j = -1;
                    }
                }
            }

            return sortDrawObjects;
        }

        /// <summary>
        /// 根据输入的图形对其进行所有具有包含关系的筛选(保持一对一一包含关系,元组中Item1包含Item2）
        /// </summary>
        /// <param name="drawObjects">画布中的图形</param>
        /// <returns></returns>
        public List<Tuple<IDrawObject, IDrawObject>> GetDrawObjectsContainDic(List<IDrawObject> drawObjects)
        {
            List<Tuple<IDrawObject, IDrawObject>> listIDrawObject = new List<Tuple<IDrawObject, IDrawObject>>();
            if (drawObjects == null || drawObjects.Count <= 1) return listIDrawObject;   //???

            for (int i = 0; i < drawObjects.Count; i++)
            {
                for (int j = 0; j < drawObjects.Count; j++)
                {
                    if (i != j)
                    {
                        if (DrawingOperationHelper.TwoDrawObjectsIntersectByGeometry(drawObjects[i], drawObjects[j], true, IntersectionDetail.FullyContains))
                        {
                            listIDrawObject.Add(Tuple.Create(drawObjects[j], drawObjects[i]));
                        }
                    }
                }
            }
            var backListIDrawObject = listIDrawObject.ToList();
            for (int i = 0; i < backListIDrawObject.Count; i++)//去除重复包含的情况
            {
                var reListIDrawObject = backListIDrawObject.FindAll(x => x.Item2 == backListIDrawObject[i].Item1);
                foreach (var item in reListIDrawObject)
                {
                    listIDrawObject.RemoveAll(x => x.Item1 == item.Item1 && x.Item2 == backListIDrawObject[i].Item2);
                }
            }
            return listIDrawObject;
        }

        #endregion

        #region 镜像

        public void OnHorizonalMirror(object args)
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            UnitPoint centerAxis = this.GetBoundingRectangleCenterPoint();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                drawObjects.Add(item);
            }
            this.dataModel.DoMirror(drawObjects, 1, 0, -centerAxis.X);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnVerticalMirror(object args)
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            UnitPoint centerAxis = this.GetBoundingRectangleCenterPoint();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                drawObjects.Add(item);
            }
            this.dataModel.DoMirror(drawObjects, 0, 1, -centerAxis.Y);
            this.myCanvas.DoInvalidate(true);
        }

        private UnitPoint GetBoundingRectangleCenterPoint()
        {
            UnitPoint maxPoint, minPoint;
            this.GetAllDrawingsMaxMinPoint(out maxPoint, out minPoint);
            return new UnitPoint((maxPoint.X + minPoint.X) / 2, (maxPoint.Y + minPoint.Y) / 2);
        }

        public void GetAllDrawingsMaxMinPoint(out UnitPoint maxPoint, out UnitPoint minPoint)
        {
            List<IDrawObject> drawObjects = this.dataModel.DrawingLayer.SelectedObjects.ToList();
            maxPoint = ((IDrawTranslation)drawObjects[0]).MaxValue;
            minPoint = ((IDrawTranslation)drawObjects[0]).MinValue;
            for (int i = 1; i < drawObjects.Count; i++)
            {
                UnitPoint tempMax = ((IDrawTranslation)drawObjects[i]).MaxValue;
                UnitPoint tempMin = ((IDrawTranslation)drawObjects[i]).MinValue;
                if (tempMax.X > maxPoint.X)
                {
                    maxPoint.X = tempMax.X;
                }
                if (tempMax.Y > maxPoint.Y)
                {
                    maxPoint.Y = tempMax.Y;
                }
                if (tempMin.X < minPoint.X)
                {
                    minPoint.X = tempMin.X;
                }
                if (tempMin.Y < minPoint.Y)
                {
                    minPoint.Y = tempMin.Y;
                }
            }
        }

        #endregion 镜像

        #region 阵列

        public void OnArray(object args)
        {
            this.myCanvas.CommandEscape();

            ArrayRectangleModel arrayRectangleModel = args as ArrayRectangleModel;
            if (arrayRectangleModel != null)
            {
                ArrayRectangle(arrayRectangleModel);
            }
            ArrayInteractiveModel arrayInteractiveModel = args as ArrayInteractiveModel;
            if (arrayInteractiveModel != null)
            {
                this.UCCanvas.ArrayInteractive();
            }
            ArrayAnnularModel arrayAnnularModel = args as ArrayAnnularModel;
            if (arrayAnnularModel != null)
            {
                this.UCCanvas.ArrayAnnular(arrayAnnularModel);
            }
            ArrayFullModel arrayFullModel = args as ArrayFullModel;
            if (arrayFullModel != null)
            {
                ArrayFullHelper arrayFullHelper = new ArrayFullHelper(this.UCCanvas, this.dataModel.DrawingLayer.Objects.ToList(), this.dataModel.DrawingLayer.SelectedObjects.ToList(), arrayFullModel);
                arrayFullHelper.ArrayRectangle();
            }

            this.myCanvas.DoInvalidate(true);
        }

        private void ArrayRectangle(ArrayRectangleModel arrayRectangleModel)
        {
            int rowCoeff = 1;
            int colCoeff = 1;
            UnitPoint offset = this.GetArrayRectangleOffset(arrayRectangleModel, ref rowCoeff, ref colCoeff);
            List<IDrawObject> drawObjects = this.dataModel.DrawingLayer.SelectedObjects.ToList();
            List<IDrawObject> arrayObjects = new List<IDrawObject>();

            //计算偏移距离
            float thWidth = UCCanvas.GetThresholdWidth();
            GetAllDrawingsMaxMinPoint(out UnitPoint maxPoint, out UnitPoint minPoint);

            double offsetx = maxPoint.X - minPoint.X - thWidth;
            double offsety = maxPoint.Y - minPoint.Y - thWidth;

            for (int i = 0; i < arrayRectangleModel.RowCount; i++)
            {
                int index = 0;
                for (int j = 0; j < arrayRectangleModel.ColumnCount; j++)
                {
                    if (i % 2 != 0)
                    {
                        index = arrayRectangleModel.ColumnCount - 1 - j;
                    }
                    else
                    {
                        index = j;
                    }
                    for (int k = 0; k < drawObjects.Count; k++)
                    {
                        if (i == 0 && j == 0)
                            break;
                        IDrawObject drawObject = drawObjects[k].Clone();
                        drawObject.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
                        if (arrayRectangleModel.OffsetType == ArrayOffsetTypes.Spacing)
                        {
                            drawObject.Move(new UnitPoint(index * (offset.X + offsetx * colCoeff), i * (offset.Y + offsety * rowCoeff)));
                        }
                        else
                        {
                            drawObject.Move(new UnitPoint(index * offset.X, i * offset.Y));
                        }
                        arrayObjects.Add(drawObject);
                    }
                }
            }
            this.dataModel.AddObjectOnDrawing(arrayObjects);
            this.dataModel.DrawingLayer.UpdateSN();
        }

        /// <summary>
        /// 偏移方向确认
        /// </summary>
        /// <param name="arrayModel"></param>
        /// <param name="rowCoeff"></param>
        /// <param name="colCoeff"></param>
        /// <returns></returns>
        private UnitPoint GetArrayRectangleOffset(ArrayRectangleModel arrayModel, ref int rowCoeff, ref int colCoeff)
        {
            UnitPoint offset = new UnitPoint();
            if (arrayModel != null)
            {
                if (arrayModel.ArrayColumnDirection == ArrayColumnDirections.Left)
                {
                    offset.X = -arrayModel.OffsetColumn;
                    colCoeff = -1;
                }
                else
                {
                    offset.X = arrayModel.OffsetColumn;
                }

                if (arrayModel.ArrayRowDirection == ArrayRowDirections.Top)
                {
                    offset.Y = arrayModel.OffsetRow;
                }
                else
                {
                    offset.Y = -arrayModel.OffsetRow;
                    rowCoeff = -1;
                }
            }
            return offset;
        }

        #endregion 阵列

        #region 对齐

        public void OnLeftAligment(object args)
        {
            this.myCanvas.CommandEscape();
            UnitPoint maxPoint, minPoint;
            List<UnitPoint> offsets = new List<UnitPoint>();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            this.GetAllDrawingsMaxMinPoint(out maxPoint, out minPoint);
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                double offsetX = minPoint.X - ((IDrawTranslation)item).MinValue.X;
                offsets.Add(new UnitPoint(offsetX, 0));
                drawObjects.Add(item);
            }
            this.dataModel.DoAligment(offsets, drawObjects);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnRightAligment(object args)
        {
            this.myCanvas.CommandEscape();
            UnitPoint maxPoint, minPoint;
            List<UnitPoint> offsets = new List<UnitPoint>();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            this.GetAllDrawingsMaxMinPoint(out maxPoint, out minPoint);
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                double offsetX = maxPoint.X - ((IDrawTranslation)item).MaxValue.X;
                offsets.Add(new UnitPoint(offsetX, 0));
                drawObjects.Add(item);
            }
            this.dataModel.DoAligment(offsets, drawObjects);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnTopAligment(object args)
        {
            this.myCanvas.CommandEscape();
            UnitPoint maxPoint, minPoint;
            List<UnitPoint> offsets = new List<UnitPoint>();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            this.GetAllDrawingsMaxMinPoint(out maxPoint, out minPoint);
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                double offsetY = maxPoint.Y - ((IDrawTranslation)item).MaxValue.Y;
                offsets.Add(new UnitPoint(0, offsetY));
                drawObjects.Add(item);
            }
            this.dataModel.DoAligment(offsets, drawObjects);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnBottomAligment(object args)
        {
            this.myCanvas.CommandEscape();
            UnitPoint maxPoint, minPoint;
            List<UnitPoint> offsets = new List<UnitPoint>();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            this.GetAllDrawingsMaxMinPoint(out maxPoint, out minPoint);
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                double offsetY = minPoint.Y - ((IDrawTranslation)item).MinValue.Y;
                offsets.Add(new UnitPoint(0, offsetY));
                drawObjects.Add(item);
            }
            this.dataModel.DoAligment(offsets, drawObjects);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnCenterAligment(object args)
        {
            this.myCanvas.CommandEscape();
            List<UnitPoint> offsets = new List<UnitPoint>();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            UnitPoint rectCenter = this.GetBoundingRectangleCenterPoint();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                double offsetX = rectCenter.X - (((IDrawTranslation)item).MinValue.X + ((IDrawTranslation)item).MaxValue.X) / 2;
                double offsetY = rectCenter.Y - (((IDrawTranslation)item).MinValue.Y + ((IDrawTranslation)item).MaxValue.Y) / 2;
                offsets.Add(new UnitPoint(offsetX, offsetY));
                drawObjects.Add(item);
            }
            this.dataModel.DoAligment(offsets, drawObjects);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnHorizonalCenter(object args)
        {
            this.myCanvas.CommandEscape();
            List<UnitPoint> offsets = new List<UnitPoint>();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            UnitPoint rectCenter = this.GetBoundingRectangleCenterPoint();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                double offsetX = rectCenter.X - (((IDrawTranslation)item).MinValue.X + ((IDrawTranslation)item).MaxValue.X) / 2;
                offsets.Add(new UnitPoint(offsetX, 0));
                drawObjects.Add(item);
            }
            this.dataModel.DoAligment(offsets, drawObjects);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnVerticalCenter(object args)
        {
            this.myCanvas.CommandEscape();
            List<UnitPoint> offsets = new List<UnitPoint>();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            UnitPoint rectCenter = this.GetBoundingRectangleCenterPoint();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                double offsetY = rectCenter.Y - (((IDrawTranslation)item).MinValue.Y + ((IDrawTranslation)item).MaxValue.Y) / 2;
                offsets.Add(new UnitPoint(0, offsetY));
                drawObjects.Add(item);
            }
            this.dataModel.DoAligment(offsets, drawObjects);
            this.myCanvas.DoInvalidate(true);
        }

        #endregion 对齐

        #region 工艺

        public void OnSettingStartPoint(object arg)
        {
            this.myCanvas.CommandSetStartPoint();
        }

        public void OnReverseDirction(object arg)
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                drawObjects.Add(item);
            }
            this.dataModel.ReverseDirection(drawObjects);
            this.myCanvas.DoInvalidate(true);
        }

        #region 封口、缺口、过切、多圈

        public void OnOverCutting(float pos, bool roundCut)
        {
            this.myCanvas.CommandEscape();
            float epsilon = 0.00001f;
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            if (Math.Abs(pos) < epsilon)//封口
            {
                foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
                {
                    if (item.FigureType == FigureTypes.Point) continue;
                    drawObjects.Add(item);
                }
            }
            else if (pos < 0)//缺口
            {
                foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
                {
                    if (item.FigureType == FigureTypes.Point) continue;
                    if (Math.Abs(pos + item.SizeLength) > epsilon)
                    {
                        drawObjects.Add(item);
                    }
                }
            }
            else//过切/多圈
            {
                foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
                {
                    if (item.FigureType == FigureTypes.Point) continue;
                    drawObjects.Add(item);
                }
            }
            this.dataModel.SetOverCutting(drawObjects, pos, roundCut);
            this.myCanvas.DoInvalidate(true);
        }

        #endregion 封口、缺口、过切、多圈

        public void OnRoundAngle(float radius)
        {
            this.myCanvas.CommandRoundAngle(0, radius);
        }

        public void OnReleaseAngle(float radius)
        {
            this.myCanvas.CommandRoundAngle(1, radius);
        }

        public void OnAutoRoundAngle(double radius)
        {
            List<IDrawObject> curDrawObjects = new List<IDrawObject>();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                MultiSegmentLineBase multi = item.Clone() as MultiSegmentLineBase;
                if (multi != null && multi.PointCount > 2)
                {
                    MultiSegmentLineBase old1 = (MultiSegmentLineBase)multi.Clone();
                    if (multi.IsCloseFigure)
                    {
                        for (int i = 0; i < multi.PointCount; i++)
                        {
                            if (multi.DoRoundAngle(true, radius, multi.Points[i].Point))
                            {
                                multi.Update();
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < multi.PointCount - 1; i++)
                        {
                            if (multi.DoRoundAngle(true, radius, multi.Points[i].Point))
                            {
                                multi.Update();
                            }
                        }
                    }
                    curDrawObjects.Add(multi);
                }
                else
                {
                    curDrawObjects.Add(item);
                }
            }
            if (curDrawObjects.Count != 0)
            {
                this.dataModel.DoRoundAngle(curDrawObjects, this.dataModel.DrawingLayer.SelectedObjects);
                this.myCanvas.DoInvalidate(true);
                this.myCanvas.CommandEscape();
            }
        }

        public void OnMicroConncect(float microConnLen, bool isApplyingToSimilarGraphics)
        {
            this.myCanvas.OnMicroConncect(microConnLen, isApplyingToSimilarGraphics);
        }

        public void OnAutoMicroConnect(bool isAllFigure, AutoMicroConParam param)
        {
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            List<List<MicroConnectModel>> prams = new List<List<MicroConnectModel>>();
            var figures = isAllFigure ? this.dataModel.DrawingLayer.Objects : this.dataModel.DrawingLayer.SelectedObjects;
            foreach (var drawObject in figures)
            {
                if (drawObject.FigureType != FigureTypes.Point)
                {
                    if ((!param.IsMaxSize || drawObject.SizeLength <= param.MaxSize) &&
                        (!param.IsMinSize || drawObject.SizeLength >= param.MinSize))
                    {
                        prams.Add(CopyUtil.DeepCopy(drawObject.MicroConnParams));
                        drawObject.SetAutoMicroConnect(param);
                        drawObjects.Add(drawObject);
                    }
                }
            }
            this.dataModel.DoMicroConnect(drawObjects, prams, false);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnMicroConnectBlowOpen()
        {
            return;
            //List<IDrawObject> drawObjects = new List<IDrawObject>();
            //foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            //{
            //    var objects = item.GetMicroConnectBlowOpen();
            //    drawObjects.AddRange(objects);
            //}
            //this.dataModel.AddObjectOnDrawing(drawObjects);
            //this.myCanvas.DoInvalidate(true);
        }

        public void OnCompensation(bool allFigure, CompensationModel param)
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            List<CompensationModel> prams = new List<CompensationModel>();
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                if (item.FigureType != FigureTypes.Point)
                {
                    if (param == null)
                    {
                        drawObjects.Add(item);
                        prams.Add(param);
                    }
                    else
                    {
                        if (allFigure)
                        {
                            drawObjects.Add(item);
                            prams.Add(param);
                        }
                        else if (item.IsCloseFigure)
                        {
                            drawObjects.Add(item);
                            prams.Add(param);
                        }
                    }
                }
            }
            this.dataModel.DoCompensation(drawObjects, prams);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnOuterCut()
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            foreach (IDrawObject drawObject in this.dataModel.DrawingLayer.SelectedObjects)
            {
                if (drawObject.FigureType != FigureTypes.Point)
                {
                    drawObjects.Add(drawObject);
                }
            }
            this.dataModel.DoOuterCut(drawObjects);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnInnerCut()
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            foreach (IDrawObject drawObject in this.dataModel.DrawingLayer.SelectedObjects)
            {
                if (drawObject.FigureType != FigureTypes.Point)
                {
                    drawObjects.Add(drawObject);
                }
            }
            this.dataModel.DoInnerCut(drawObjects);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnCornerRing(CornerRingModel param)
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            List<CornerRingModel> prams = new List<CornerRingModel>();
            foreach (IDrawObject drawObject in this.dataModel.DrawingLayer.SelectedObjects)
            {
                if (drawObject.FigureType != FigureTypes.Point)
                {
                    drawObjects.Add(drawObject);
                    prams.Add(param);
                }
            }
            this.dataModel.DoCornerRing(drawObjects, prams);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnClearMicroConnect()
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            List<List<MicroConnectModel>> prams = new List<List<MicroConnectModel>>();
            foreach (IDrawObject drawObject in this.dataModel.DrawingLayer.SelectedObjects)
            {
                if (drawObject.FigureType != FigureTypes.Point)
                {
                    prams.Add(CopyUtil.DeepCopy(drawObject.MicroConnParams));
                    drawObject.SetMicroConnectParams(true, UnitPoint.Empty, 0);
                    drawObjects.Add(drawObject);
                }
            }
            this.dataModel.DoMicroConnect(drawObjects, prams, false);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnSetCoolPoint()
        {
            this.myCanvas.OnSetCoolPoint();
        }

        public void OnSetAutoCoolPoint(bool isLeadIn, bool isCorner, double maxAngle)
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            List<List<MicroConnectModel>> prams = new List<List<MicroConnectModel>>();
            foreach (IDrawObject drawObject in this.dataModel.DrawingLayer.SelectedObjects)
            {
                if (drawObject.FigureType != FigureTypes.Point)
                {
                    prams.Add(CopyUtil.DeepCopy(drawObject.MicroConnParams));
                    drawObject.SetCoolPoint(UnitPoint.Empty, isLeadIn, isCorner, maxAngle);
                    drawObjects.Add(drawObject);
                }
            }
            this.dataModel.DoMicroConnect(drawObjects, prams, false);
            this.myCanvas.DoInvalidate(true);
        }

        public void OnClearCoolPoint()
        {
            this.myCanvas.CommandEscape();
            List<IDrawObject> drawObjects = new List<IDrawObject>();
            List<List<MicroConnectModel>> prams = new List<List<MicroConnectModel>>();
            foreach (IDrawObject drawObject in this.dataModel.DrawingLayer.SelectedObjects)
            {
                if (drawObject.FigureType != FigureTypes.Point)
                {
                    prams.Add(CopyUtil.DeepCopy(drawObject.MicroConnParams));
                    drawObject.ClearCoolPoint();
                    drawObjects.Add(drawObject);
                }
            }
            this.dataModel.DoMicroConnect(drawObjects, prams, false);
            this.myCanvas.DoInvalidate(true);
        }

        /// <summary>
        /// 显示工艺
        /// </summary>
        /// <param name="layerId"></param>
        public void DoShowLayerCraft(int layerId, bool isShow)
        {
            this.myCanvas.CommandEscape();
            this.dataModel.DrawingLayer.Objects.ForEach(s =>
            {
                ///显示全部
                if (layerId == 0) { s.IsVisiblity = isShow; }
                else
                {
                    DrawObjectBase drawObjectBase = s as DrawObjectBase;
                    if (drawObjectBase != null && drawObjectBase.LayerId == layerId) { s.IsVisiblity = isShow; }
                }
            });
            DoRefresh();
        }

        public void DoOnlyShowLayerCraft(int layerId)
        {
            this.myCanvas.CommandEscape();
            this.dataModel.DrawingLayer.Objects.ForEach(s =>
            {
                ///显示全部
                if (layerId == 0) { s.IsVisiblity = true; }
                else
                {
                    DrawObjectBase drawObjectBase = s as DrawObjectBase;
                    if (drawObjectBase != null && drawObjectBase.LayerId == layerId) { s.IsVisiblity = true; }
                    else { s.IsVisiblity = false; }
                }
            });
            DoRefresh();
        }

        /// <summary>
        /// 锁定工艺
        /// </summary>
        /// <param name="layerId"></param>
        public void DoLockLayerCraft(int layerId, bool isLock)
        {
            this.myCanvas.CommandEscape();
            this.dataModel.DrawingLayer.Objects.ForEach(s =>
            {
                DrawObjectBase drawObjectBase = s as DrawObjectBase;
                if (drawObjectBase != null && drawObjectBase.LayerId == layerId) { s.IsLocked = isLock; }
            });
            DoRefresh();
        }

        #endregion 工艺

        #endregion 图形变换

        public void DoRefresh()
        {
            this.myCanvas.DoInvalidate(true);
        }

        #region 排序

        /// <param name="sortType"></param>
        /// <param name="unitPointZero"></param>
        /// <param name="sortProhibitChangDirection">禁止排序改变方向</param>
        /// <param name="sortDistinguishInsideOutside">排序时区分内外摸</param>
        /// <param name="SortShadeCutOutermost">最外层为阴切</param>
        /// <param name="groupInside">是否为群组内排序</param>
        public void DoSort(string sortType, UnitPoint unitPointZero, bool sortProhibitChangDirection, bool sortDistinguishInsideOutside, bool SortShadeCutOutermost, bool groupInside)
        {
            this.myCanvas.CommandEscape();
            Dictionary<int, int> indexs = new Dictionary<int, int>();
            List<IDrawObject> selectedObjs = this.dataModel.DrawingLayer.SelectedObjects;
            List<IDrawObject> objs;
            if (groupInside)
            {
                objs = selectedObjs;
                int index = selectedObjs.Min(x => x.GroupParam.FigureSN);
                if (index != 1)
                {
                    index -= 2;
                }
                else
                {
                    index--;//当群组内第一个图形就是所有图形的第一个图形即以本身为起点
                }
                //当时群组内部最短空移排序时应该选择群组中最小ShowSN的上一个图形为起点而不是固定的坐标零点为起点
                unitPointZero = this.dataModel.DrawingLayer.Objects[index].EndMovePoint;
            }
            else
            {
                objs = this.dataModel.DrawingLayer.Objects;
            }
            bool canSort = false;
            switch (sortType)
            {
                case "FrontMost": //最前
                    {
                        //TODO:还需要判断多个图形的时候根据某种默认方式进行排序
                        canSort = selectedObjs != null && selectedObjs.Count > 0;
                        if (canSort)
                        {
                            foreach (var item in objs)
                            {
                                if (item.GroupParam.FigureSN < selectedObjs[0].GroupParam.FigureSN)
                                {
                                    indexs[item.GroupParam.FigureSN] = item.GroupParam.FigureSN + 1;
                                }
                                else if (item.GroupParam.FigureSN == selectedObjs[0].GroupParam.FigureSN)
                                {
                                    indexs[item.GroupParam.FigureSN] = 1;
                                }
                                else
                                {
                                    indexs[item.GroupParam.FigureSN] = item.GroupParam.FigureSN;
                                }
                            }
                        }
                    }
                    break;

                case "BackMost"://最后
                    {
                        //TODO:还需要判断多个图形的时候根据某种默认方式进行排序
                        canSort = selectedObjs != null && selectedObjs.Count > 0;
                        if (canSort)
                        {
                            foreach (var item in objs)
                            {
                                if (item.GroupParam.FigureSN > selectedObjs[0].GroupParam.FigureSN)
                                {
                                    indexs[item.GroupParam.FigureSN] = item.GroupParam.FigureSN - 1;
                                }
                                else if (item.GroupParam.FigureSN == selectedObjs[0].GroupParam.FigureSN)
                                {
                                    indexs[item.GroupParam.FigureSN] = objs.Count;
                                }
                                else
                                {
                                    indexs[item.GroupParam.FigureSN] = item.GroupParam.FigureSN;
                                }
                            }
                        }
                    }
                    break;

                case "Forward"://向前
                    {
                        //如果选中多个则默认只会变序号最小的那个
                        canSort = selectedObjs != null && selectedObjs.Count > 0 && selectedObjs[0].GroupParam.FigureSN > 1;
                        if (canSort)
                        {
                            foreach (var item in objs)
                            {
                                if (item.GroupParam.FigureSN == selectedObjs[0].GroupParam.FigureSN - 1)
                                {
                                    indexs[item.GroupParam.FigureSN] = item.GroupParam.FigureSN + 1;
                                }
                                else if (item.GroupParam.FigureSN == selectedObjs[0].GroupParam.FigureSN)
                                {
                                    indexs[item.GroupParam.FigureSN] = item.GroupParam.FigureSN - 1;
                                }
                                else
                                {
                                    indexs[item.GroupParam.FigureSN] = item.GroupParam.FigureSN;
                                }
                            }
                        }
                    }
                    break;

                case "Backward"://向后
                    {
                        //如果选中多个则默认只会变序号最大的那个
                        canSort = selectedObjs != null && selectedObjs.Count > 0 && selectedObjs[0].GroupParam.FigureSN < objs.Count;
                        if (canSort)
                        {
                            foreach (var item in objs)
                            {
                                if (item.GroupParam.FigureSN == selectedObjs[0].GroupParam.FigureSN)
                                {
                                    indexs[item.GroupParam.FigureSN] = item.GroupParam.FigureSN + 1;
                                }
                                else if (item.GroupParam.FigureSN == selectedObjs[0].GroupParam.FigureSN + 1)
                                {
                                    indexs[item.GroupParam.FigureSN] = item.GroupParam.FigureSN - 1;
                                }
                                else
                                {
                                    indexs[item.GroupParam.FigureSN] = item.GroupParam.FigureSN;
                                }
                            }
                        }
                    }
                    break;

                case "PreviousTarget"://上一个
                    {
                    }
                    break;

                case "NextTarget"://下一个
                    {
                    }
                    break;

                case "SortShortestMove"://局部最短空移
                    {
                        canSort = objs != null && objs.Count > 1;
                        if (canSort)
                        {
                            indexs = SortWithShortestMove(unitPointZero, objs);
                        }
                    }
                    break;

                case "SortKnife"://刀摸排序
                    {
                    }
                    break;

                case "SortSmallFigurePriority"://小图优先
                    {
                        canSort = objs != null && objs.Count > 1;
                        if (canSort)
                        {
                            indexs = SortWithSmallFigurePriority(objs);
                        }
                    }
                    break;

                case "SortInsideToOut"://由内到外
                    {
                        canSort = objs != null && objs.Count > 1;
                        if (canSort)
                        {
                            indexs = this.SortWithInsideOutside(objs);
                        }
                    }
                    break;

                case "SortLeftToRight"://从左到右
                    {
                        canSort = objs != null && objs.Count > 1;
                        if (canSort)
                        {
                            indexs = this.GetSortIndexByDirection(objs, true, true);
                        }
                    }
                    break;

                case "SortRightToLeft"://从右到左
                    {
                        canSort = objs != null && objs.Count > 1;
                        if (canSort)
                        {
                            indexs = this.GetSortIndexByDirection(objs, false, true);
                        }
                    }
                    break;

                case "SortGrid":   //网格排序！！！！！！！！！！！！！！！！！！！！！！！！！！！！
                case "SortTopToBottom"://从上到下
                    {
                        canSort = objs != null && objs.Count > 1;
                        if (canSort)
                        {
                            indexs = this.GetSortIndexByDirection(objs, false, false);
                        }
                    }
                    break;

                case "SortBottomToTop"://从下到上
                    {
                        canSort = objs != null && objs.Count > 1;
                        if (canSort)
                        {
                            indexs = this.GetSortIndexByDirection(objs, true, false);
                        }
                    }
                    break;

                case "SortClockwise"://顺时针
                    {
                        canSort = objs != null && objs.Count > 1;
                        if (canSort)
                        {
                            indexs = SortWithClockwise(objs, true);
                        }
                    }
                    break;

                case "SortAnticlockwise"://逆时针
                    {
                        canSort = objs != null && objs.Count > 1;
                        if (canSort)
                        {
                            indexs = SortWithClockwise(objs, false);
                        }
                    }
                    break;

                default:
                    break;
            }
            if (canSort)
            {
                this.dataModel.DoSort(indexs);
                this.myCanvas.DoInvalidate(true);
            }
        }

        private Dictionary<int, int> SortWithSmallFigurePriority(List<IDrawObject> drawObjects)
        {
            List<IDrawObject> allDrawObjects = this.dataModel.DrawingLayer.Objects;
            List<IDrawObject> sortDrawObjects = drawObjects.ToList();
            Dictionary<int, int> indexs = new Dictionary<int, int>();
            sortDrawObjects.Sort((a, b) =>
            {
                double x1 = a.SizeLength;
                double x2 = b.SizeLength;
                return x1 >= x2 ? 1 : -1;
            });
            if (drawObjects.Count != allDrawObjects.Count)
            {
                this.SortedGroupShowSN(drawObjects, sortDrawObjects);
                int j = 0;
                for (int i = 0; i < allDrawObjects.Count; i++)
                {
                    if (sortDrawObjects.Contains(allDrawObjects[i]))
                    {
                        indexs[sortDrawObjects[j++].GroupParam.FigureSN] = i + 1;
                    }
                    else
                    {
                        indexs[i + 1] = i + 1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < sortDrawObjects.Count; i++)
                {
                    indexs[sortDrawObjects[i].GroupParam.FigureSN] = i + 1;
                }
            }
            indexs = indexs.OrderBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value);
            return indexs;
        }

        private void SortedGroupShowSN(List<IDrawObject> drawObjects, List<IDrawObject> sortDrawObjects)
        {
            int temp = drawObjects[0].GroupParam.GroupSN[0];
            drawObjects[0].GroupParam.GroupSN[0] = 0;
            drawObjects.Find(x => x == sortDrawObjects[0]).GroupParam.GroupSN[0] = temp;
        }

        private Dictionary<int, int> SortWithShortestMove(UnitPoint unitPointZero, List<IDrawObject> drawObjects)
        {
            Dictionary<int, int> indexs = new Dictionary<int, int>();
            List<IDrawObject> sortDrawObjects = drawObjects.ToList();
            List<IDrawObject> allDrawObjects = this.dataModel.DrawingLayer.Objects;
            //1.求出第一个图形(停靠点到起始点距离最近原则)
            int index = this.GetNextShortestPathObjectIndex(unitPointZero, sortDrawObjects);
            unitPointZero = drawObjects[index].EndMovePoint;
            if (drawObjects.Count != allDrawObjects.Count)
            {
                var temp = sortDrawObjects.Min(x => x.GroupParam.FigureSN);
                indexs[drawObjects[index].GroupParam.FigureSN] = temp;
                drawObjects[0].GroupParam.GroupSN[0] = 0;
                drawObjects[index].GroupParam.GroupSN[0] = temp;
                sortDrawObjects.Remove(drawObjects[index]);
                int j = 1;
                for (int i = 0; i < sortDrawObjects.Count; j++)
                {
                    index = this.GetNextShortestPathObjectIndex(unitPointZero, sortDrawObjects);
                    unitPointZero = sortDrawObjects[index].EndMovePoint;
                    indexs[sortDrawObjects[index].GroupParam.FigureSN] = temp + j;
                    sortDrawObjects.Remove(sortDrawObjects[index]);
                    i = 0;
                }
                for (int i = 1; i < temp; i++)
                {
                    indexs[i] = i;
                }
                for (int i = drawObjects.Max(x => x.GroupParam.FigureSN) + 1; i <= allDrawObjects.Count; i++)
                {
                    indexs[i] = i;
                }
            }
            else
            {
                indexs[drawObjects[index].GroupParam.FigureSN] = 1;
                sortDrawObjects.Remove(drawObjects[index]);
                //2.依次求出其他图形(前一图形终止点到后一图形起始点距离最近原则)
                int j = 0;
                for (int i = 0; i < sortDrawObjects.Count; j++)
                {
                    index = this.GetNextShortestPathObjectIndex(unitPointZero, sortDrawObjects);
                    unitPointZero = sortDrawObjects[index].EndMovePoint;
                    indexs[sortDrawObjects[index].GroupParam.FigureSN] = j + 2;
                    sortDrawObjects.Remove(sortDrawObjects[index]);
                    i = 0;
                }
            }
            indexs = indexs.OrderBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value);
            return indexs;
        }

        private int GetNextShortestPathObjectIndex(UnitPoint point, List<IDrawObject> drawObjects)
        {
            int index = 0;
            List<double> distances = new List<double>();
            foreach (var item in drawObjects)
            {
                distances.Add(HitUtil.Distance(point, item.StartMovePoint));
            }
            index = distances.IndexOf(distances.Min());
            index = drawObjects.IndexOf(drawObjects[index]);
            return index;
        }

        private Dictionary<int, int> GetSortIndexByDirection(List<IDrawObject> drawObjects, bool minToMax, bool byX)
        {
            Dictionary<int, int> indexs = new Dictionary<int, int>();
            List<IDrawObject> allDrawObjects = this.dataModel.DrawingLayer.Objects;
            List<IDrawObject> sortDrawObjects = drawObjects.ToList();
            if (byX)
            {
                sortDrawObjects.Sort((a, b) =>
                {
                    double x1 = ((IDrawTranslation)a).MinValue.X;
                    double x2 = ((IDrawTranslation)b).MinValue.X;
                    if (minToMax)
                    {
                        return x1 >= x2 ? 1 : -1;
                    }
                    return x1 <= x2 ? 1 : -1;
                });
            }
            else
            {
                sortDrawObjects.Sort((a, b) =>
                {
                    double y1 = ((IDrawTranslation)a).MinValue.Y;
                    double y2 = ((IDrawTranslation)b).MinValue.Y;
                    if (minToMax)
                    {
                        return y1 >= y2 ? 1 : -1;
                    }
                    return y1 <= y2 ? 1 : -1;
                });
            }
            if (drawObjects.Count != allDrawObjects.Count)
            {
                this.SortedGroupShowSN(drawObjects, sortDrawObjects);
                int j = 0;
                for (int i = 0; i < allDrawObjects.Count; i++)
                {
                    if (sortDrawObjects.Contains(allDrawObjects[i]))
                    {
                        indexs[sortDrawObjects[j++].GroupParam.FigureSN] = i + 1;
                    }
                    else
                    {
                        indexs[i + 1] = i + 1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < sortDrawObjects.Count; i++)
                {
                    indexs[sortDrawObjects[i].GroupParam.FigureSN] = i + 1;
                }
            }
            indexs = indexs.OrderBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value);
            return indexs;
        }

        #endregion 排序

        /// <summary>
        ///
        /// </summary>
        /// <param name="drawObjects">前面key位外膜，后面value内模</param>
        /// <returns></returns>
        public Dictionary<IDrawObject, IDrawObject> GetInsideOutsideDic(List<IDrawObject> drawObjects)
        {
            Dictionary<IDrawObject, IDrawObject> dicIdrawObject = new Dictionary<IDrawObject, IDrawObject>();
            if (drawObjects == null && drawObjects.Count <= 1) return dicIdrawObject;
            for (int i = 0; i < drawObjects.Count - 1; i++)
            {
                RectangleF rectangleF = drawObjects[i].GetBoundingRectangle(BoundingRectangleType.All);
                RectangleF rectangleF1 = drawObjects[i + 1].GetBoundingRectangle(BoundingRectangleType.All);

                for (int j = 1; j < drawObjects.Count; j++)
                {
                    rectangleF1 = drawObjects[j].GetBoundingRectangle(BoundingRectangleType.All);

                    if ((rectangleF.X < rectangleF1.X) && (rectangleF.Y < rectangleF1.Y))
                    {
                        if (
                            (((rectangleF1.X - rectangleF.X) + rectangleF1.Width) < rectangleF.Width)
                            &&
                            (((rectangleF1.Y - rectangleF.Y) + rectangleF1.Height) < rectangleF.Height)
                                )
                        {
                            if (dicIdrawObject.ContainsKey(drawObjects[i]) == false)
                                dicIdrawObject.Add(drawObjects[i], drawObjects[j]);
                            continue;
                        }
                    }
                    else if ((rectangleF1.X < rectangleF.X) && (rectangleF1.Y < rectangleF.Y))
                    {
                        if (
                            (((rectangleF.X - rectangleF1.X) + rectangleF.Width) < rectangleF1.Width)
                            &&
                            (((rectangleF.Y - rectangleF1.Y) + rectangleF.Height) < rectangleF1.Height)
                                )
                        {
                            if (dicIdrawObject.ContainsKey(drawObjects[j]) == false)
                                dicIdrawObject.Add(drawObjects[j], drawObjects[i]);
                            continue;
                        }
                    }
                }
            }
            return dicIdrawObject;
        }

        private bool CompareDrawObject(IDrawObject drawObject1, IDrawObject drawObject2)
        {
            RectangleF rectangleF1 = drawObject1.GetBoundingRectangle(BoundingRectangleType.All);
            RectangleF rectangleF2 = drawObject2.GetBoundingRectangle(BoundingRectangleType.All);
            if (rectangleF1.Contains(rectangleF2))
            {
                return true;
            }
            return false;
        }

        private List<IDrawObject> ClockwiseRecursive(List<IDrawObject> drawObjects, bool isClockWise)
        {
            if (drawObjects.Count < 2) return drawObjects;
            List<IDrawObject> listDrawObjects = new List<IDrawObject>();
            List<RectangleF> drawObjectsRF = new List<RectangleF>();
            List<IDrawObject> unContainsDrawObjects = new List<IDrawObject>();
            List<IDrawObject> containsDrawObjects = new List<IDrawObject>();
            drawObjects.ForEach(x => drawObjectsRF.Add(x.GetBoundingRectangle(BoundingRectangleType.All)));
            int index = drawObjectsRF.IndexOf(drawObjectsRF.OrderByDescending(y => y.Width * y.Height).ToList()[0]);
            UnitPoint drawObjectsCenter = this.CalAllDrawObjectCenter(drawObjects);
            foreach (var item in drawObjects)
            {
                if (drawObjectsRF[index].Contains(item.GetBoundingRectangle(BoundingRectangleType.All)))
                {
                    containsDrawObjects.Add(item);
                }
                else
                {
                    unContainsDrawObjects.Add(item);
                }
            }
            if (containsDrawObjects.Count > 0)
            {
                containsDrawObjects.Remove(drawObjects[index]);
            }
            if (unContainsDrawObjects.Count != 0 && containsDrawObjects.Count != 0)
            {
                if (unContainsDrawObjects.Count != 1)
                {
                    listDrawObjects.AddRange(this.ClockWiseBubbleSort(unContainsDrawObjects, drawObjectsCenter, drawObjects[index].StartMovePoint, isClockWise));
                    listDrawObjects.Add(drawObjects[index]);
                    drawObjects.Remove(drawObjects[index]);
                    drawObjects.RemoveAll(x => unContainsDrawObjects.Contains(x) == true);
                }
                else
                {
                    listDrawObjects.Add(unContainsDrawObjects[0]);
                    listDrawObjects.Add(drawObjects[index]);
                    drawObjects.Remove(drawObjects[index]);
                    drawObjects.Remove(unContainsDrawObjects[0]);
                    if (containsDrawObjects.Count == 1)
                    {
                        listDrawObjects.Add(containsDrawObjects[0]);
                        drawObjects.Remove(containsDrawObjects[0]);
                    }
                }
            }
            if (unContainsDrawObjects.Count == 0 && containsDrawObjects.Count != 0)
            {
                listDrawObjects.Add(drawObjects[index]);
                drawObjects.Remove(drawObjects[index]);
            }
            if (unContainsDrawObjects.Count != 0 && containsDrawObjects.Count == 0)
            {
                if (unContainsDrawObjects.Count != 1)
                {
                    if (!drawObjects[index].GetBoundingRectangle(BoundingRectangleType.All).Contains((float)drawObjectsCenter.X, (float)drawObjectsCenter.Y))
                    {
                        unContainsDrawObjects.Add(drawObjects[index]);
                    }
                    var temp = unContainsDrawObjects.FindAll(x => x.GetBoundingRectangle(BoundingRectangleType.All).Contains((float)drawObjectsCenter.X, (float)drawObjectsCenter.Y) == true);
                    if (temp.Count > 0)
                    {
                        unContainsDrawObjects.RemoveAll(x => temp.Contains(x) == true);
                    }
                    listDrawObjects.AddRange(this.ClockWiseBubbleSort(unContainsDrawObjects, drawObjectsCenter, drawObjects[index].StartMovePoint, isClockWise));
                    drawObjects.RemoveAll(x => unContainsDrawObjects.Contains(x) == true);
                }
                else
                {
                    listDrawObjects.Add(drawObjects[index]);
                    drawObjects.Remove(drawObjects[index]);
                    listDrawObjects.Add(unContainsDrawObjects[0]);
                    drawObjects.Remove(unContainsDrawObjects[0]);
                }
            }
            if (unContainsDrawObjects.Count == 0 && containsDrawObjects.Count == 0)
            {
                listDrawObjects.Add(drawObjects[0]);
                drawObjects.Remove(drawObjects[0]);
            }
            if (drawObjects.Count != 0)
            {
                listDrawObjects.AddRange(this.ClockwiseRecursive(drawObjects, isClockWise));
            }
            return listDrawObjects;
        }

        private Dictionary<int, int> SortWithClockwise(List<IDrawObject> drawObjects, bool isClockWise)
        {
            Dictionary<int, int> indexs = new Dictionary<int, int>();
            List<IDrawObject> allDrawObjects = this.dataModel.DrawingLayer.Objects;
            List<IDrawObject> sortDrawObjects = this.ClockwiseRecursive(drawObjects.ToList(), isClockWise);
            if (drawObjects.Count != allDrawObjects.Count)
            {
                sortDrawObjects.Reverse();
                this.SortedGroupShowSN(drawObjects, sortDrawObjects);
                int j = 0;
                for (int i = 0; i < allDrawObjects.Count; i++)
                {
                    if (sortDrawObjects.Contains(allDrawObjects[i]))
                    {
                        indexs[sortDrawObjects[j++].GroupParam.FigureSN] = i + 1;
                    }
                    else
                    {
                        indexs[i + 1] = i + 1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < sortDrawObjects.Count; i++)
                {
                    indexs[sortDrawObjects[i].GroupParam.FigureSN] = sortDrawObjects.Count - i;
                }
            }
            indexs = indexs.OrderBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value);
            return indexs;
        }

        private bool LineLineRoundAngle(UnitPoint point1, UnitPoint point2, UnitPoint centerPoint, UnitPoint startPoint)
        {
            double rotateAngle = HitUtil.LineAngleR(centerPoint, startPoint, 0);
            UnitPoint rotatePoint1 = Utils.Utils.RotateAlgorithm(point1, centerPoint, rotateAngle);
            UnitPoint rotatePoint2 = Utils.Utils.RotateAlgorithm(point2, centerPoint, rotateAngle);
            double temp1 = HitUtil.LineAngleR(centerPoint, rotatePoint1, 0);
            double temp2 = HitUtil.LineAngleR(centerPoint, rotatePoint2, 0);
            return temp1 < temp2;
        }

        private List<IDrawObject> ClockWiseBubbleSort(List<IDrawObject> drawObjects, UnitPoint drawObjectsCenter, UnitPoint startPoint, bool isClockWise)
        {
            for (int i = 0; i < drawObjects.Count; i++)
            {
                for (int j = 0; j < drawObjects.Count - i - 1; j++)
                {
                    if (isClockWise ^ (this.LineLineRoundAngle(drawObjects[j].StartMovePoint, drawObjects[j + 1].StartMovePoint, drawObjectsCenter, startPoint)))
                    {
                        var temp = drawObjects[j];
                        drawObjects[j] = drawObjects[j + 1];
                        drawObjects[j + 1] = temp;
                    }
                }
            }
            return drawObjects;
        }

        private bool CalClockWise(UnitPoint point1, UnitPoint point2, UnitPoint centerPoint)
        {
            if (point1.X >= 0 && point2.X < 0) return true;
            if (point1.X == 0 && point2.X == 0) return point1.Y > point2.Y;
            double temp = (point1.X - centerPoint.X) * (point2.Y - centerPoint.Y) - (point2.X - centerPoint.X) * (point1.Y - centerPoint.Y);
            if (temp < 0) return true;
            if (temp > 0) return false;
            double d1 = (point1.X - centerPoint.X) * (point1.X - centerPoint.X) + (point1.Y - centerPoint.Y) * (point1.Y - centerPoint.Y);
            double d2 = (point2.X - centerPoint.X) * (point2.X - centerPoint.Y) + (point2.Y - centerPoint.Y) * (point2.Y - centerPoint.Y);
            return d1 > d2;
        }

        private UnitPoint CalAllDrawObjectCenter(List<IDrawObject> drawObjects)
        {
            //int index = 0;
            float x1 = 0f, y1 = 0f;
            List<float> xList, yList;
            List<RectangleF> drawObjectsRF = new List<RectangleF>();
            List<RectangleF> finalDrawObjectsRF = new List<RectangleF>();
            drawObjects.ForEach(x => drawObjectsRF.Add(x.GetBoundingRectangle(BoundingRectangleType.All)));
            xList = drawObjectsRF.ConvertAll(s => s.X + (s.Width / 2));
            yList = drawObjectsRF.ConvertAll(s => s.Y + (s.Height / 2));
            x1 = xList.Average();
            y1 = yList.Average();
            UnitPoint point = new UnitPoint(x1, y1);
            List<IDrawObject> drawObjects1 = new List<IDrawObject>();
            //drawObjects.Find(x=>x)
            //找最大的图形的中心点为起点(也可以作为取中心点的方法，目前使用的是取所有图形在XY轴上的平均值作为中心点)
            //finalDrawObjectsRF = drawObjectsRF.FindAll(x => x.Contains(x1, y1));
            // if (finalDrawObjectsRF.Count == 0 || finalDrawObjectsRF.Count > 1)
            // {
            //所有图形(x,y)的平均值
            //index = this.GetNextShortestPathObjectIndex(point, drawObjects);
            //}
            //return index;
            return point;
        }

        private Dictionary<int, int> SortWithInsideOutside(List<IDrawObject> drawObjects)
        {
            Dictionary<int, int> indexs = new Dictionary<int, int>();
            List<IDrawObject> sortDrawObjects = drawObjects.ToList();
            List<IDrawObject> allDrawObjects = this.dataModel.DrawingLayer.Objects;
            for (int i = 0; i < sortDrawObjects.Count; i++)
            {
                for (int j = 0; j < sortDrawObjects.Count - 1 - i; j++)
                {
                    if (this.CompareDrawObject(sortDrawObjects[j], sortDrawObjects[j + 1]))
                    {
                        var temp = sortDrawObjects[j];
                        sortDrawObjects[j] = sortDrawObjects[j + 1];
                        sortDrawObjects[j + 1] = temp;
                    }
                }
            }
            if (drawObjects.Count != allDrawObjects.Count)
            {
                this.SortedGroupShowSN(drawObjects, sortDrawObjects);
                int j = 0;
                for (int i = 0; i < allDrawObjects.Count; i++)
                {
                    if (sortDrawObjects.Contains(allDrawObjects[i]))
                    {
                        indexs[sortDrawObjects[j++].GroupParam.FigureSN] = i + 1;
                    }
                    else
                    {
                        indexs[i + 1] = i + 1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < sortDrawObjects.Count; i++)
                {
                    indexs[sortDrawObjects[i].GroupParam.FigureSN] = i + 1;
                }
            }
            indexs = indexs.OrderBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value);
            return indexs;
        }

        #region 全选 反选 取消选择

        public void DoSelectAll()
        {
            this.dataModel.DrawingLayer.Objects.ForEach(s => s.IsSelected = true);
            DoRefresh();
        }

        public void DoCancelSelectAll()
        {
            this.dataModel.DrawingLayer.Objects.ForEach(s => s.IsSelected = false);
            DoRefresh();
        }

        /// <summary>
        /// 反选
        /// </summary>
        public void DoInvertSelect()
        {
            this.myCanvas.CommandEscape();
            this.dataModel.DrawingLayer.Objects.ForEach(s => s.IsSelected = !s.IsSelected);
            DoRefresh();
        }

        public void DoBanFastDragCopy()
        {
            UCCanvas.SelectCanMove = !UCCanvas.SelectCanMove;
        }

        public void DoSelectGapFigurey()
        {
            this.dataModel.DrawingLayer.Objects.ForEach(s =>
            {
                if (s.IsCloseFigure == false) { s.IsSelected = true; }
                else { s.IsSelected = false; }
            });
            DoRefresh();
        }

        /// <summary>
        /// 选择所有内膜
        /// </summary>
        public void DoSelectAllInternalModes()
        {
            List<IDrawObject> drawObjects = this.dataModel.DrawingLayer.Objects.ToList();
            Dictionary<IDrawObject, IDrawObject> dicIdrawObject = GetInsideOutsideDic(drawObjects);

            this.dataModel.DrawingLayer.Objects.ForEach(s =>
            {
                if (dicIdrawObject.ContainsValue(s)) { s.IsSelected = true; }
                else { s.IsSelected = false; }
            });
            DoRefresh();
        }

        /// <summary>
        /// 选择所有外模
        /// </summary>
        public void DoSelectAllExternalModes()
        {
            List<IDrawObject> drawObjects = this.dataModel.DrawingLayer.Objects.ToList();
            Dictionary<IDrawObject, IDrawObject> dicIdrawObject = GetInsideOutsideDic(drawObjects);
            this.dataModel.DrawingLayer.Objects.ForEach(s =>
            {
                if (dicIdrawObject.ContainsValue(s) == false) { s.IsSelected = true; }
                else { s.IsSelected = false; }
            });
            DoRefresh();
        }

        public void DoSelectLayerCraft(int layerId)
        {
            this.dataModel.DrawingLayer.Objects.ForEach(s =>
            {
                DrawObjectBase drawObjectBase = s as DrawObjectBase;
                if (drawObjectBase != null && drawObjectBase.LayerId == layerId) { s.IsSelected = true; }
                else { s.IsSelected = false; }
            });
            DoRefresh();
        }

        public void DoSelectAllMultiLines()
        {
            this.dataModel.DrawingLayer.Objects.ForEach(s =>
            {
                MultiSegmentLineBase multiSegmentLineBase = s as MultiSegmentLineBase;
                if (s.GroupParam.GroupSN.Count < 2 && multiSegmentLineBase != null) { s.IsSelected = true; }
                else { s.IsSelected = false; }
            });
            DoRefresh();
        }

        public void DoSelectAllCircles()
        {
            this.dataModel.DrawingLayer.Objects.ForEach(s =>
            {
                Circle circle = s as Circle;
                if (circle != null) { s.IsSelected = true; }
                else { s.IsSelected = false; }
            });
            DoRefresh();
        }

        public void DoSelectAllBezierCurves()
        {
        }

        public DrawingLayer GetDrawingLayer()
        {
            return this.dataModel.DrawingLayer;
        }

        public DataModel GetDataModel()
        {
            return this.dataModel;
        }

        public void DoCopy()
        {
            this.UCCanvas.Copy_Click(null, null);
        }

        public void DoPaste()
        {
            this.UCCanvas.Paste_Click(null, null);
        }

        public void DoCut()
        {
            this.UCCanvas.tsCut_Click(null, null);
        }

        public void DoDelete()
        {
            this.UCCanvas.Delete_Click(null, null);
        }

        #endregion 全选 反选 取消选择

        #region 群组

        public void DoGroup()
        {
            //1.确保有选中图形
            List<IDrawObject> selectedObjects = this.dataModel.DrawingLayer.SelectedObjects;
            bool hasSelectedObj = selectedObjects.Count > 0;
            if (hasSelectedObj)
            {
                //2.确保选中图形中尚含有尚未是群组的图形
                bool hasCanGroupObj = selectedObjects.Any(r => r.GroupParam.GroupSN.Count == 1);
                //3.两个不同群组
                if (hasCanGroupObj == false)
                {
                    var groupObject = selectedObjects.FindAll(s => s.GroupParam.GroupSN.Count > 1);
                    if (groupObject.Count > 0)
                    {
                        int gsn = groupObject[0].GroupParam.GroupSN[1];
                        hasCanGroupObj = groupObject.Any(s => s.GroupParam.GroupSN[1] != gsn);
                    }
                }
                if (hasCanGroupObj)
                {
                    this.dataModel.DoGroup(new List<List<IDrawObject>>() { selectedObjects });
                    this.myCanvas.DoInvalidate(true);
                }
            }
        }

        public void DoGroupSelectAll()
        {
            this.dataModel.DrawingLayer.Objects.ForEach(r =>
            {
                if (r.GroupParam.GroupSN.Count != 1)
                {
                    r.IsSelected = true;
                }
            });
            DoRefresh();
        }

        public void DoGroupScatterAll()
        {
            this.ScatterGroup(this.dataModel.DrawingLayer.Objects);
        }

        public void DoGroupScatterSelected()
        {
            List<IDrawObject> selectedObjects = this.dataModel.DrawingLayer.SelectedObjects;
            if (selectedObjects.Count > 0)//1.保证有选中图形
            {
                this.ScatterGroup(selectedObjects);
            }
        }

        private void ScatterGroup(List<IDrawObject> drawObjects)
        {
            List<IDrawObject> groupObjects = drawObjects.FindAll(r => r.GroupParam.GroupSN[0] != 0).ToList();
            if (groupObjects.Count != 0)//2.保证存在群组
            {
                List<List<IDrawObject>> result = new List<List<IDrawObject>>();
                for (int i = 0; i < groupObjects.Count; i++)
                {
                    int index = groupObjects[i].GroupParam.GroupSN[1];
                    List<IDrawObject> tempObjs = drawObjects.FindAll(r =>
                      {
                          if (r.GroupParam.GroupSN.Count != 1 && r.GroupParam.GroupSN[1] == index)
                          {
                              return true;
                          }
                          return false;
                      });
                    result.Add(tempObjs);
                }
                this.dataModel.DoGroupScatter(result);
                this.myCanvas.DoInvalidate(true);
            }
        }

        #endregion 群组

        #region 桥接

        public void OnBridge(Func<BridgingModel> func)
        {
            this.myCanvas.OnBridge(func);
        }

        #endregion 桥接

        #region 测量

        public void DoMeasure()
        {
            this.UCCanvas.Measure();
        }

        #endregion 测量

        #region 飞切

        public void DoLineFlyingCut(LineFlyingCutModel lineFlyingCutModel)
        {
            List<IDrawObject> selectedLines = new List<IDrawObject>();
            //获取源对象节点
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                var obj = item as WSX.DrawService.DrawTool.MultiSegmentLine.MultiSegmentLineBase;
                if (obj == null || obj.Points == null || obj.Points.Count <= 0)
                    continue;

                selectedLines.Add(item);
            }

            FlyCutHelper helper = new FlyCutHelper();
            List<List<LineFlyCut>> flyingCutGoups = helper.DoLineFlyingCut(selectedLines, lineFlyingCutModel);
            if (flyingCutGoups != null && flyingCutGoups.Count > 0)
            {
                this.dataModel.DoLineFlyingCut(selectedLines, flyingCutGoups);
                this.myCanvas.DoInvalidate(true);
            }
        }

        public void DoArcFlyingCut(ArcFlyingCutModel arcFlyingCutModel)
        {
            List<IDrawObject> selectedCircles = new List<IDrawObject>();

            FlyCutHelper helper = new FlyCutHelper();
            if (arcFlyingCutModel.IsFlyingByPart)
            {
                List<IDrawObject> list1= helper.DoArcFlyingCutForPart(this.dataModel.DrawingLayer.SelectedObjects, arcFlyingCutModel, ref selectedCircles);

                if (list1 != null && list1.Count > 0)
                {
                    this.dataModel.DoArcFlyingCut(selectedCircles, list1);
                    this.myCanvas.DoInvalidate(true);
                }
                return;
            }

            //获取源对象节点
            foreach (var item in this.dataModel.DrawingLayer.SelectedObjects)
            {
                var obj = item as WSX.DrawService.DrawTool.CircleTool.Circle;
                if (obj == null || obj.Center == null || obj.Radius <= 0 || obj.GroupParam.GroupSN.Count > 1)
                    continue;

                selectedCircles.Add(item);
            }

            selectedCircles = helper.DeleteOutterCircle(selectedCircles);
            if (selectedCircles.Count < 2)
            {
                return;
            }
            List<IDrawObject> list = helper.DoArcFlyingCut(selectedCircles, arcFlyingCutModel).ToList<IDrawObject>();

            if (list != null && list.Count > 0)
            {
                this.dataModel.DoArcFlyingCut(selectedCircles, list);
                this.myCanvas.DoInvalidate(true);
            }
        }

        #endregion 飞切

        #region 共边

        public bool IsCommonSide()
        {
            List<IDrawObject> selectedObjs = this.dataModel.DrawingLayer.SelectedObjects;
            int objectCount = selectedObjs.Count;
            //保证选中图形大于等于2个
            if (objectCount <= 1) { return false; } 
            for (int i = 0; i < objectCount; i++)
            {
                IDrawObject drawObject = selectedObjs[i];
                var temp = drawObject as WSX.DrawService.DrawTool.MultiSegmentLine.MultiSegmentLineBase;
                //不是群组 必须四边形
                if (drawObject.GroupParam.GroupSN.Count > 1 || (temp == null) || (temp != null && temp.PointCount != 4))
                {
                    return false;
                }
            }
            //保证图形之间的间距足够小，满足共边的条件
            double offset = UCCanvas.GetThresholdWidth() / 2;
            List<IDrawTranslation> trans_drawObjects = Array.ConvertAll(selectedObjs.ToArray(), s => (IDrawTranslation)s).ToList();
            double Figure0center_x = (trans_drawObjects[0].MinValue.X + offset + trans_drawObjects[0].MaxValue.X - offset) / 2;
            double Figure0center_y = (trans_drawObjects[0].MinValue.Y + offset + trans_drawObjects[0].MaxValue.Y - offset) / 2;
            double Figure1center_x = (trans_drawObjects[1].MinValue.X + offset + trans_drawObjects[1].MaxValue.X - offset) / 2;
            double Figure1center_y = (trans_drawObjects[1].MinValue.Y + offset + trans_drawObjects[1].MaxValue.Y - offset) / 2;

            double spacing0 = Math.Abs(Figure1center_y - Figure0center_y)
                - (trans_drawObjects[1].MaxValue.Y - offset - Figure1center_y)
                - (trans_drawObjects[0].MaxValue.Y - offset - Figure0center_y);
            double spacing1 = (Math.Abs(Figure1center_x - Figure0center_x)
                - (trans_drawObjects[1].MaxValue.X - offset - Figure1center_x)
                - (trans_drawObjects[0].MaxValue.X - offset - Figure0center_x));
            if ((spacing0 > 0) && (spacing0 < CommonSideHelper.CommonSideBorderDistance))
            {

            }
            else if ((spacing1 > 0) && (spacing1 < CommonSideHelper.CommonSideBorderDistance))
            {

            }
            else
            {
                return false;
            }
            return true;
        }

        public void DoCommonSide(CommonSideRectangleModel commonSideRectangleModel)
        {
            var objs = this.dataModel.DrawingLayer.SelectedObjects;
            List<UnitPoint> unitPoints = new List<UnitPoint>();
            //获取共边的源对象节点
            //foreach (var item in objs)
            //{
            //    if (item is WSX.DrawService.DrawTool.MultiSegmentLine.MultiSegmentLineBase obj)
            //    {
            //        if (obj.Points != null && obj.Points.Count > 0)
            //        {
            //            foreach (var itemPoint in obj.Points)
            //            {
            //                unitPoints.Add(itemPoint.Point);
            //            }
            //        }
            //    }
            //}
            List<List<UnitPoint>> temp = new List<List<UnitPoint>>();
            CommonSideHelper commonSideHelper = new CommonSideHelper();
            //横平竖直 默认
            if (commonSideRectangleModel.CommonSideStyle == CommonSideStyles.HorizontalsAndVerticals)
            {
                temp = commonSideHelper.GetHorizontalsAndVerticals(objs, unitPoints, commonSideRectangleModel);
            }
            //外框优先  以及
            //外框最后
            else if (commonSideRectangleModel.CommonSideStyle == CommonSideStyles.FramedPriority || commonSideRectangleModel.CommonSideStyle == CommonSideStyles.FrameFinal)
            {
                temp = commonSideHelper.GetFramedPriority(objs, unitPoints, commonSideRectangleModel);
            }
            //逐个蛇形
            else if (commonSideRectangleModel.CommonSideStyle == CommonSideStyles.Serpentine)
            {
                temp = commonSideHelper.GetSerpentine(objs, unitPoints, commonSideRectangleModel);
            }
            //逐个阶梯图
            else if (commonSideRectangleModel.CommonSideStyle == CommonSideStyles.Stairs)
            {
                if (objs.Count == 2) { temp = commonSideHelper.GetHorizontalsAndVerticals2(objs, unitPoints, commonSideRectangleModel); }
                temp = commonSideHelper.GetStairs(objs, unitPoints, commonSideRectangleModel);
            }
            else
            {
                return;
            }
            if (temp.Count > 0)
            {
                //抹掉选中图形，重画共边之后的图形
                this.dataModel.DoCommonSide(this.dataModel.DrawingLayer.SelectedObjects.ToList(), temp);
                this.myCanvas.DoInvalidate(true);
            }
        }

        #endregion 共边
    }
}