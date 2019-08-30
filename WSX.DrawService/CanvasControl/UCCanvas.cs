using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WSX.DrawService.Resources;
using WSX.DrawService.Utils;
using System.Drawing.Drawing2D;
using WSX.DrawService.Wrapper;
using WSX.DrawService.Layers;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.DrawTool.MultiSegmentLine;
using System.Linq;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.Utilities;
using WSX.GlobalData.Messenger;
using WSX.GlobalData;
using WSX.CommomModel.ParaModel;
using WSX.GlobalData.Model;
using WSX.DrawService.DrawTool;

namespace WSX.DrawService.CanvasControl
{
    public partial class UCCanvas : UserControl
    {
        /// <summary>
        /// 命令类型
        /// </summary>
        private enum CommandType
        {
            None,//无操作
            Draw,//绘图
            SetStartPoint,//设置起点
            SetCoolPoint,//设置冷却点
            Smothing,//倒圆角
            UnSmothing,//释圆角
            SetMicroConnect,//设置微连
            MovePosition,//平移图形
            AnyMirror,//任意角度镜像
            AnyRotate,//任意角度旋转
            InteractScale,//交互式缩放
            AbsAnchor,//绝对停靠
            Measure,//测量
            ArrayInteractive,//交互式阵列
            ArrayAnnular,//环形阵列
            Bridge,//桥接
        }

        #region 成员变量
        private ICanvasOwner canvasOwner;
        private CursorCollection cursorCollection = new CursorCollection();
        private MoveHelper moveHelper = null;
        private NodeMoveHelper nodeMoveHelper = null;
        private RotateAnyHelper rotateAnyHelper = null;
        private MirrorHelper mirrorHelper = null;
        private ScaleHelper scaleHelper = null;
        private MeasureHelper measureHelper = null;
        private ArrayInteractiveHelper interactiveHelper = null;
        private ArrayAnnularHelper annularHelper = null;
        public CanvasWrapper canvasWrapper;
        private CommandType commandType = CommandType.None;
        private PointF mouseDownPoint;
        private SelectionRectangle selectRect = null;
        private string drawObjectId = string.Empty;
        private Bitmap bitmap = null;
        private bool dirty = true;
        private ISnapPoint snapPoint = null;
        private UnitPoint lastCenterPoint;
        private PointF panOffset = new PointF(25, -25);
        private PointF dragOffset = new PointF(0, 0);
        private float screenResolution = 10;
        private Dictionary<Keys, Type> quickSnap = new Dictionary<Keys, Type>();

        bool rasiseEventFirst = false;
        public event Action<object, ScaleChangedEventArgs> OnScaleChanged;
        private readonly MarkLayer markLayer;
        private Font snFont = new Font("Verdana", 10);
        private Brush snBrush = new SolidBrush(Color.White);

        #region 图形操作(主窗体传递过来)的参数
        private bool isLeftDown = false;
        private bool canMove = false;//图形是否能进行移动操作

        private double roundRadius;//倒圆角或释放角半径值
        private float microConnectLength;
        private bool isApplyingToSimilarGraphics;
        #endregion

        #endregion
        public Type[] RunningSnaps { get; set; }
        public bool RunningSnapsEnable { get; set; } = true;
        public SmoothingMode SmoothingMode { get; set; } = SmoothingMode.HighSpeed;
        public IModel Model { get; set; }
        public IDrawObject NewObject { get; private set; }
        public bool CanvasEnabled { get; set; } = true;

        public UCCanvas(ICanvasOwner canvasOwner, IModel model)
        {
            this.canvasWrapper = new CanvasWrapper(this);
            this.canvasOwner = canvasOwner;
            this.Model = model;
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.Model.DrawingPattern = DrawingPattern.SelectPattern;
            this.cursorCollection.AddCursor(DrawingPattern.SelectPattern, Cursors.Arrow);
            this.cursorCollection.AddCursor(DrawingPattern.NodeEditPattern, Cursors.Cross);
            this.cursorCollection.AddCursor(DrawingPattern.TranslationViewPattern, "hmove.cur");
            this.cursorCollection.AddCursor(DrawingPattern.SortPattern, Cursors.SizeAll);
            this.cursorCollection.AddCursor(CommandType.SetCoolPoint, "coolpoint.cur");
            this.cursorCollection.AddCursor(CommandType.SetMicroConnect, "micorconn.cur");
            this.UpdateCursor(this.Model.DrawingPattern);
            this.moveHelper = new MoveHelper(this);
            this.rotateAnyHelper = new RotateAnyHelper(this);
            this.mirrorHelper = new MirrorHelper(this);
            this.scaleHelper = new ScaleHelper(this);
            this.measureHelper = new MeasureHelper(this);
            this.interactiveHelper = new ArrayInteractiveHelper(this);
            this.annularHelper = new ArrayAnnularHelper(this);
            this.nodeMoveHelper = new NodeMoveHelper(this.canvasWrapper);
            this.SizeChanged += new EventHandler(this.UCCanvas_SizeChanged);
            RegisterEvent();
            this.markLayer = new MarkLayer(this);
        }

        public void RegisterEvent()
        {
            Messenger.Instance.Register(MainEvent.OnInDataDrawCommand, OnInDrawCommand);
        }
        public UnitPoint GetMousePoint()
        {
            Point point = this.PointToClient(Control.MousePosition);
            return this.ToUnit(point);
        }
        public void SetCenter(UnitPoint unitPoint)
        {
            PointF point = this.ToScreen(unitPoint);
            this.lastCenterPoint = unitPoint;
            this.SetCenterScreen(point, false);
        }
        public void SetCenter()
        {
            Point point = this.PointToClient(Control.MousePosition);
            this.SetCenterScreen(point, true);
        }
        protected void SetCenterScreen(PointF screenPoint, bool setCursor)
        {
            float centerX = this.ClientRectangle.Width / 2;
            this.panOffset.X += centerX - screenPoint.X;
            float centerY = this.ClientRectangle.Height / 2;
            this.panOffset.Y += centerY - screenPoint.Y;
            if (setCursor)
            {
                Cursor.Position = this.PointToScreen(new Point((int)centerX, (int)centerY));
            }
            this.DoInvalidate(true);
        }
        public void DoInvalidate(bool doStatic, RectangleF rectangleF)
        {
            this.dirty = doStatic;
            this.Invalidate(ScreenUtils.ConvertRectangle(rectangleF));
        }
        public void DoInvalidate(bool doStatic)
        {
            //this.dirty = doStatic;
            if (!this.dirty)
            {
                this.dirty = doStatic;
            }
            this.Invalidate();
        }
        public UnitPoint GetCenter()
        {
            return this.ToUnit(new PointF(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            CanvasWrapper canvasWrapper = new CanvasWrapper(this, e.Graphics, this.ClientRectangle);
            Rectangle clipRectangle = e.ClipRectangle;
            if (this.bitmap == null)
            {
                clipRectangle = this.ClientRectangle;
                this.bitmap = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
                this.dirty = true;
            }
            RectangleF rectangleF = ScreenUtils.ToUnitNormalized(canvasWrapper, clipRectangle);
            if (float.IsNaN(rectangleF.Width) || float.IsInfinity(rectangleF.Width))
            {
                rectangleF = ScreenUtils.ToUnitNormalized(canvasWrapper, clipRectangle);
            }
            if (this.dirty)
            {
                this.dirty = false;
                CanvasWrapper dcStatic = new CanvasWrapper(this, Graphics.FromImage(this.bitmap), ClientRectangle);
                dcStatic.Graphics.SmoothingMode = this.SmoothingMode;
                this.Model.BackgroundLayer.Draw(dcStatic, rectangleF);
                if (!this.Model.GridLayer.Locked)
                {
                    this.Model.GridLayer.Draw(dcStatic, rectangleF);
                }
                //PointF nullPoint = this.ToScreen(new UnitPoint(0, 0));
                //dcStatic.Graphics.DrawLine(Pens.Blue, nullPoint.X - 10, nullPoint.Y, nullPoint.X + 10, nullPoint.Y);
                //dcStatic.Graphics.DrawLine(Pens.Blue, nullPoint.X, nullPoint.Y - 10, nullPoint.X, nullPoint.Y + 10);

                this.Model.DrawingLayer.Draw(dcStatic, rectangleF);
                this.markLayer.UpdateSceenPoints();
                if (this.StaticRefreshMark)
                {
                    this.markLayer.Draw(dcStatic);
                }
                if (this.leadLine != null)
                {
                    this.leadLine.Draw(dcStatic, rectangleF);
                }
                dcStatic.Dispose();
            }
            e.Graphics.DrawImage(this.bitmap, clipRectangle, clipRectangle, GraphicsUnit.Pixel);
            if (this.NewObject != null)
            {
                this.NewObject.Draw(canvasWrapper, rectangleF);
            }
            if (this.snapPoint != null)
            {
                this.snapPoint.Draw(canvasWrapper);
            }
            if (this.selectRect != null)
            {
                //this.selectRect.Reset();
                //this.selectRect.SetMousePoint(e.Graphics, this.PointToClient(Control.MousePosition));
            }
            if (this.moveHelper.IsEmpty == false)
            {
                this.moveHelper.DrawObjects(canvasWrapper, rectangleF);
            }
            if (this.nodeMoveHelper.IsEmpty == false)
            {
                //this.nodeMoveHelper.DrawObjects(canvasWrapper, rectangleF);
            }
            switch (commandType)
            {
                case CommandType.AnyRotate:
                    this.rotateAnyHelper.DrawObjects(canvasWrapper, rectangleF);
                    break;
                case CommandType.InteractScale:
                    this.scaleHelper.DrawObject(canvasWrapper, rectangleF);
                    break;
                case CommandType.AnyMirror:
                    this.mirrorHelper.DrawObjects(canvasWrapper, rectangleF);
                    break;
                case CommandType.Measure:
                    this.measureHelper.DrawObjects(canvasWrapper, rectangleF);
                    break;
                case CommandType.ArrayInteractive:
                    this.interactiveHelper.DrawObjects(canvasWrapper, rectangleF);
                    break;
            }
            if (!this.StaticRefreshMark)
            {
               this.markLayer.Draw(canvasWrapper);
            }
            if (AdditionalInfo.Instance.IsShowVacantPath)
            {
                this.DrawVacantPath(canvasWrapper);
            }
            canvasWrapper.Dispose();
            if (!this.rasiseEventFirst)
            {
                this.rasiseEventFirst = true;
                this.RaiseScaleChangedEvent();
            }

            Messenger.Instance.Send("DrawObjectSelectedChange", this.Model.SelectedCount);

            List<IDrawObject> listDW = this.Model.DrawingLayer.SelectedObjects;
            bool IsSigleGroup = true;
            int gsn=0;
            for (int i = 0; i < listDW.Count; i++)
            {
                if(i==0&&listDW[i].GroupParam.GroupSN.Count>1)
                {
                    gsn = listDW[i].GroupParam.GroupSN[1];
                }
                if (listDW[i].GroupParam.GroupSN.Count < 2 || listDW[i].GroupParam.GroupSN[1] != gsn)
                {                   
                    IsSigleGroup = false;
                    break;
                }
            }
            if (listDW.Count>1&& IsSigleGroup )
            {
                Messenger.Instance.Send("DrawObjectSelectedIsSigleGroupChange", true);
                this.tsGroupSort.Visible = true;
            }
            else
            {
                Messenger.Instance.Send("DrawObjectSelectedIsSigleGroupChange", false);
                this.tsGroupSort.Visible = false;
            }
            
        }
        private void DrawVacantPath(CanvasWrapper canvasWrapper)
        {
            List<UnitPoint> vacantPoints = new List<UnitPoint>(); 
            //不加工图形不显示空移路线           
            List<IDrawObject> objs = this.Model.DrawingLayer.Objects.FindAll(s=> {
                var v = s as DrawObjectBase;
                if(v!=null)
                {
                    if(v.LayerId!=(int)LayerId.White)
                    { return true; }
                }
                return false;
            });
            if (objs == null || objs.Count < 2)
                return;
            DrawTool.CircleTool.Circle circle = null;
            DrawTool.CircleTool.Circle previousCircle = objs[0] as DrawTool.CircleTool.Circle;

            vacantPoints.Add(objs[0].EndMovePoint);
            for (int i = 1; i < objs.Count; i++)
            {
                circle = objs[i] as DrawTool.CircleTool.Circle;
                if (circle != null && circle.IsFlyingCut && !circle.IsFlyingCutScatter)
                {
                    if(previousCircle == null || previousCircle.GroupParam.GroupSN.Count<2 || previousCircle.GroupParam.GroupSN[1] != circle.GroupParam.GroupSN[1])
                    {
                        vacantPoints.Add(objs[i].StartMovePoint);
                        vacantPoints.Add(objs[i].EndMovePoint);
                    }

                    if (vacantPoints.Count > 1)
                    {
                        for (int j = 0; j < vacantPoints.Count - 1; j += 2)
                        {
                            this.DrawLine(canvasWrapper, DrawTool.DrawUtils.VancantPen, vacantPoints[j], vacantPoints[j + 1], 1);
                        }
                    }
                    vacantPoints.Clear();
                    vacantPoints.Add(objs[i].EndMovePoint);
                    previousCircle = circle;
                    continue;
                }
                vacantPoints.Add(objs[i].StartMovePoint);
                vacantPoints.Add(objs[i].EndMovePoint);
                previousCircle = circle;
            }
            for (int i = 0; i < vacantPoints.Count - 1; i += 2)
            {         
                this.DrawLine(canvasWrapper, DrawTool.DrawUtils.VancantPen, vacantPoints[i], vacantPoints[i + 1],1);
            }
        }

        private void DrawVacantPath1(CanvasWrapper canvasWrapper)
        {
            List<UnitPoint> vacantPoints = new List<UnitPoint>();
            //不加工图形不显示空移路线           
            List<IDrawObject> objs = this.Model.DrawingLayer.Objects.FindAll(s =>
            {
                var v = s as DrawObjectBase;
                if (v != null)
                {
                    if (v.LayerId != (int)LayerId.White)
                    { return true; }
                }
                return false;
            });
            if (objs == null || objs.Count < 2) return;

            DrawTool.CircleTool.Circle circle = null;

            int previousGroup = objs[0].GroupParam.GroupSN.Count > 1 ? objs[0].GroupParam.GroupSN[1] : int.MaxValue;

            int currentGroup = 0;
            vacantPoints.Add(objs[0].EndMovePoint);

            DrawTool.CircleTool.Circle previousCircle = objs[0] as DrawTool.CircleTool.Circle;
            for (int i = 1; i < objs.Count; i++)
            {
                currentGroup = objs[i].GroupParam.GroupSN.Count > 1 ? objs[i].GroupParam.GroupSN[1] : -1;
                circle = objs[i] as DrawTool.CircleTool.Circle;
                if (circle != null && previousCircle != null && previousCircle.FlyingCutLeadOut != null && previousCircle.FlyingCutLeadOut.HasLeadLine && previousGroup == objs[i].GroupParam.GroupSN[1])
                {
                    if (vacantPoints.Count > 1)
                    {
                        for (int j = 0; j < vacantPoints.Count - 1; j += 2)
                        {
                            this.DrawLine(canvasWrapper, DrawTool.DrawUtils.VancantPen, vacantPoints[j], vacantPoints[j + 1], 1);
                        }
                    }
                    vacantPoints.Clear();
                    vacantPoints.Add(objs[i].EndMovePoint);
                    previousGroup = currentGroup == -1 ? int.MaxValue : currentGroup;
                    previousCircle = circle;
                    continue;
                }

                vacantPoints.Add(objs[i].StartMovePoint);
                vacantPoints.Add(objs[i].EndMovePoint);

                //previousCircle = circle;
                //previousGroup = currentGroup == -1 ? int.MaxValue : currentGroup;
            }

            if (vacantPoints.Count < 2)
                return;

            for (int i = 0; i < vacantPoints.Count - 1; i += 2)
            {
                this.DrawLine(canvasWrapper, DrawTool.DrawUtils.VancantPen, vacantPoints[i], vacantPoints[i + 1], 1);
            }

        }

        //private List<UnitPoint> GetGroupMovePoint(IDrawObject drawObject)
        //{
        //    List<UnitPoint> unitPoints = new List<UnitPoint>();
        //    if (drawObject.Id.Equals(CanvasCommands.Group.ToString()))
        //    {
        //        Group group = (Group)drawObject;
        //        foreach (var item in group.GroupMembers)
        //        {
        //            if (item.Id.Equals(CanvasCommands.Group.ToString()))
        //            {

        //                unitPoints.AddRange(GetGroupMovePoint(item));
        //            }
        //            else
        //            {
        //                unitPoints.Add(item.StartMovePoint);
        //                unitPoints.Add(item.EndMovePoint);
        //            }
        //        }

        //    }
        //    return unitPoints;
        //}
        protected void HandleSelection(List<IDrawObject> selectedObjects)
        {
            bool add = Control.ModifierKeys == Keys.Shift;
            bool toggle = Control.ModifierKeys == Keys.Control;
            bool invalidate = false;
            bool anyOldSelected = false;
            int selectedCount = 0;
            if (selectedObjects != null)
            {
                selectedCount = selectedObjects.Count;
            }
            foreach (IDrawObject drawObject in this.Model.DrawingLayer.SelectedObjects)
            {
                anyOldSelected = true;
                break;
            }
            if (toggle && selectedCount > 0)
            {
                invalidate = true;
                foreach (IDrawObject drawObject in selectedObjects)
                {
                    if (drawObject.IsSelected)
                    {
                        this.Model.RemoveSelectedObject(drawObject);
                    }
                    else
                    {
                        this.Model.AddSelectedObject(drawObject);
                    }
                }
            }
            if (add && selectedCount > 0)
            {
                invalidate = true;
                foreach (IDrawObject drawObject in selectedObjects)
                {
                    this.Model.AddSelectedObject(drawObject);
                }
            }
            if (add == false && toggle == false && selectedCount > 0)
            {
                invalidate = true;
                this.Model.ClearSelectedObjects();
                foreach (IDrawObject drawObject in selectedObjects)
                {
                    this.Model.AddSelectedObject(drawObject);
                }
            }
            if (add == false && toggle == false && selectedCount == 0 && anyOldSelected)
            {
                invalidate = true;
                this.Model.ClearSelectedObjects();
            }
            if (invalidate)
            {
                this.DoInvalidate(false);
            }
        }

        private bool selectCanMove = true;
        public bool SelectCanMove
        {
            get { return selectCanMove; }
            set { selectCanMove = value; }
        }
        #region 鼠标按下事件处理

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.isLeftDown = (e.Button == MouseButtons.Left);
            if (!this.isLeftDown)
            {
                this.HandleRightMenu(e.Location);
            }
            else
            {
                this.mouseDownPoint = new PointF(e.X, e.Y);
                switch (this.commandType)
                {
                    case CommandType.Draw:
                        this.MouseDownDraw(e);
                        break;
                    case CommandType.SetStartPoint:
                        this.MouseDownSetStartMovePoint(e);
                        break;
                    case CommandType.SetCoolPoint:
                        this.MouseDownSetCoolPoint(e);
                        break;
                    case CommandType.Smothing:
                        this.MouseDownSmoothing(e);
                        break;
                    case CommandType.UnSmothing:
                        this.MouseDownUnSmoothing(e);
                        break;
                    case CommandType.SetMicroConnect:
                        this.MouseDownSetMicroConnect(e);
                        break;
                    case CommandType.MovePosition:
                        this.MouseDownMovePosition(e);
                        break;
                    case CommandType.Bridge:
                        this.MouseDownSetBridge(e);
                        break;
                    case CommandType.InteractScale:
                        if (this.scaleHelper.HandleMouseDownForScale(this.ToUnit(e.Location)))
                        {
                            this.CommandEscape();
                        }
                        break;
                    case CommandType.AnyRotate:
                        if (this.rotateAnyHelper.HandleMouseDownForRotate(this.ToUnit(e.Location)))
                        {
                            this.CommandEscape();
                        }
                        break;
                    case CommandType.AnyMirror:
                        if (this.mirrorHelper.HandleMouseDownForMirror(this.ToUnit(e.Location)))
                        {
                            this.CommandEscape();
                        }
                        break;
                    case CommandType.None:
                        this.MouseDownNone(e);
                        break;
                    case CommandType.Measure:
                        if (this.measureHelper.HandleMouseDownForMeasure(this.ToUnit(e.Location)))
                        {
                            this.CommandEscape();
                        }
                        break;
                    case CommandType.ArrayInteractive:
                        this.interactiveHelper.HandleMouseDownForArrayInteractive(e);
                        break;
                    case CommandType.ArrayAnnular:
                        {
                            if (this.annularHelper.HandleMouseDownForArrayAnnular(e))
                            {
                                this.CommandEscape();
                            }
                        }
                        break;
                }
            }
            base.OnMouseDown(e);
        }
        private void MouseDownDraw(MouseEventArgs e)
        {
            this.dragOffset = new PointF(0, 0);
            UnitPoint mousePoint = this.ToUnit(this.mouseDownPoint);
            this.HandleMouseDownWhenDrawing(mousePoint, null, e);
            this.DoInvalidate(true);
        }

        private void MouseDownSetCoolPoint(MouseEventArgs e)
        {
            UnitPoint mousePoint = this.ToUnit(this.mouseDownPoint);
            IDrawObject hitObject = this.Model.GetHitObject(mousePoint);
            if (hitObject != null && hitObject.FigureType != FigureTypes.Point)
            {
                List<IDrawObject> drawObjects = new List<IDrawObject>();
                List<List<MicroConnectModel>> prams = new List<List<MicroConnectModel>>();
                prams.Add(CopyUtil.DeepCopy(hitObject.MicroConnParams));
                hitObject.SetCoolPoint(mousePoint);
                drawObjects.Add(hitObject);
                this.Model.DoMicroConnect(drawObjects, prams, false);
                this.DoInvalidate(true);
            }
        }

        private void MouseDownSmoothing(MouseEventArgs e)
        {
            UnitPoint mousePoint = this.ToUnit(this.mouseDownPoint);
            List<IDrawObject> hitLists = this.Model.GetHitObjects(this.canvasWrapper, mousePoint);
            List<IDrawObject> curDrawObjects = new List<IDrawObject>();
            if (hitLists != null && hitLists.Count > 0)
            {
                if (hitLists[0].GroupParam.GroupSN.Count != 1) return;
                MultiSegmentLineBase multiSegmentLineBase;
                foreach (IDrawObject item in hitLists)
                {
                    multiSegmentLineBase = item.Clone() as MultiSegmentLineBase;
                    if (multiSegmentLineBase != null && multiSegmentLineBase.PointCount > 2)
                    {
                        bool result = multiSegmentLineBase.DoRoundAngle(true, this.roundRadius, mousePoint);
                        if (result)
                        {
                            multiSegmentLineBase.Update();
                            curDrawObjects.Add(multiSegmentLineBase);
                            this.Model.DoRoundAngle(curDrawObjects, hitLists);
                            this.DoInvalidate(true);
                        }
                        else
                        {
                            this.CommandEscape();
                        }
                        break;
                    }
                }
            }
            else
            {
                this.CommandEscape();
            }
        }
        private void MouseDownUnSmoothing(MouseEventArgs e)
        {
            UnitPoint mousePoint = this.ToUnit(this.mouseDownPoint);
            List<IDrawObject> hitLists = this.Model.GetHitObjects(this.canvasWrapper, mousePoint);
            List<IDrawObject> curDrawObjects = new List<IDrawObject>();
            if (hitLists != null && hitLists.Count > 0)
            {
                if (hitLists[0].GroupParam.GroupSN.Count != 1) return;
                MultiSegmentLineBase multiSegmentLineBase;
                foreach (IDrawObject item in hitLists)
                {
                    multiSegmentLineBase = item.Clone() as MultiSegmentLineBase;
                    if (multiSegmentLineBase != null && multiSegmentLineBase.PointCount > 2)
                    {
                        bool result = multiSegmentLineBase.DoRoundAngle(false, this.roundRadius, mousePoint);
                        if (result)
                        {
                            multiSegmentLineBase.Update();
                            curDrawObjects.Add(multiSegmentLineBase);
                            this.Model.DoRoundAngle(curDrawObjects, hitLists);
                            this.DoInvalidate(true);
                        }
                        else
                        {
                            this.CommandEscape();
                        }
                        break;
                    }
                }
            }
            else
            {
                this.CommandEscape();
            }
        }

        private void MouseDownSetMicroConnect(MouseEventArgs e)
        {
            UnitPoint mousePoint = this.ToUnit(this.mouseDownPoint);
            IDrawObject hitObject = this.Model.GetHitObject(mousePoint);
            if (hitObject != null && hitObject.FigureType != FigureTypes.Point)
            {
                List<IDrawObject> drawObjects = new List<IDrawObject>();
                List<List<MicroConnectModel>> prams = new List<List<MicroConnectModel>>();
                if (this.microConnectLength > hitObject.SizeLength)
                {
                    this.microConnectLength = (float)((this.microConnectLength * 100) % (hitObject.SizeLength * 100)) / 100;
                }
                prams.Add(CopyUtil.DeepCopy(hitObject.MicroConnParams));
                hitObject.SetMicroConnectParams(false, mousePoint, this.microConnectLength);
                drawObjects.Add(hitObject);
                this.Model.DoMicroConnect(drawObjects, prams, false);
                this.DoInvalidate(true);
            }
        }
        private void MouseDownNone(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.HandleRightMenu(e.Location);
            }
            else
            {
                UnitPoint mousePoint = this.ToUnit(this.mouseDownPoint);
                switch (this.Model.DrawingPattern)
                {
                    case DrawingPattern.SelectPattern:
                        {
                            if (this.snapPoint != null)
                            {
                                mousePoint = this.snapPoint.SnapPoint;
                            }
                            this.selectRect = new SelectionRectangle(this.mouseDownPoint);
                            if (this.Model.SelectedCount > 0)
                            {
                                foreach (var item in this.Model.DrawingLayer.SelectedObjects)
                                {
                                    if (item.IsLocked == true)
                                    {
                                        this.canMove = false;
                                        continue;
                                    }
                                    if (item.PointInObject(mousePoint))
                                    {
                                        this.canMove = true;
                                        break;
                                    }
                                }
                                if (this.canMove && SelectCanMove)
                                {
                                    this.moveHelper.HandleMouseDownForMove(mousePoint, snapPoint);
                                }
                            }
                            break;
                        }
                    case DrawingPattern.NodeEditPattern:
                        {
                            bool handled = false;
                            if (this.nodeMoveHelper.HandleMouseDown(mousePoint, ref handled))
                            {
                                this.snapPoint = null;
                            }
                            break;
                        }
                    case DrawingPattern.SortPattern:
                        break;
                    case DrawingPattern.TranslationViewPattern:
                        break;
                }
            }
        }
        protected virtual void HandleMouseDownWhenDrawing(UnitPoint mouseUnitPoint, ISnapPoint snapPoint, MouseEventArgs e)
        {
            if (this.commandType == CommandType.Draw)
            {
                SendDrawMsg(drawObjectId, $"{mouseUnitPoint.X},{mouseUnitPoint.Y}");

                if (this.NewObject == null)
                {
                    this.NewObject = this.Model.CreateObject(this.drawObjectId, mouseUnitPoint, snapPoint);
                    SetDrawComponetAttribute();
                    if (this.drawObjectId == "SingleDot")
                    {
                        this.Model.AddObjectOnDrawing(this.NewObject);
                        this.NewObject = null;
                        this.DoInvalidate(true);
                        return;
                    }
                    if (this.drawObjectId == "Group") return;
                    this.DoInvalidate(false, this.NewObject.GetBoundingRectangle(BoundingRectangleType.Drawing));
                }
                else
                {
                    DrawObjectMouseDown result = this.NewObject.OnMouseDown(this.canvasWrapper, mouseUnitPoint, snapPoint, e);
                    switch (result)
                    {
                        case DrawObjectMouseDown.Done:
                            this.Model.AddObjectOnDrawing(this.NewObject);
                            this.NewObject = null;
                            this.DoInvalidate(true);
                            break;
                        case DrawObjectMouseDown.DoneRepeat:
                            SendDrawMsg(this.drawObjectId, $"{this.NewObject.RepeatStartingPoint.X},{this.NewObject.RepeatStartingPoint.Y}");
                            this.Model.AddObjectOnDrawing(this.NewObject);
                            this.NewObject = this.Model.CreateObject(this.drawObjectId, this.NewObject.RepeatStartingPoint, null);
                            this.DoInvalidate(true);

                            break;
                        case DrawObjectMouseDown.Continue:
                            break;
                        case DrawObjectMouseDown.Change:
                            this.Model.AddObjectOnDrawing(this.NewObject);
                            break;
                    }
                }
            }
        }
       
        #endregion

        #region 鼠标移动事件处理
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            switch (this.commandType)
            {
                case CommandType.Draw:
                    this.MouseMoveDraw(e);
                    break;
                case CommandType.SetStartPoint:
                    this.MouseMoveSetStartMovePoint(e);
                    break;
                case CommandType.SetCoolPoint:
                    this.MouseMoveSetCoolPoint(e);
                    break;
                case CommandType.Smothing:
                    this.MouseMoveSmoothing(e);
                    break;
                case CommandType.UnSmothing:
                    this.MouseMoveUnSmoothing(e);
                    break;
                case CommandType.MovePosition:
                    this.MouseMoveMovePosition(e);
                    break;
                case CommandType.Bridge:
                    this.MouseMoveSetBridge(e);
                    break;
                case CommandType.InteractScale:
                    //this.MouseMoveInteractScale(e);
                    this.scaleHelper.HandleMouseMoveForScale(this.ToUnit(e.Location));
                    break;
                case CommandType.AnyRotate:
                    this.rotateAnyHelper.HandleMouseMoveForRotate(this.ToUnit(e.Location));
                    Refresh();
                    break;
                case CommandType.AnyMirror:
                    this.mirrorHelper.HandleMouseMoveForMirror(this.ToUnit(e.Location));
                    break;
                case CommandType.None:
                    this.MouseMoveNone(e);
                    break;
                case CommandType.Measure:
                    this.measureHelper.HandleMouseMoveForMeasure(this.ToUnit(e.Location));
                    break;
                case CommandType.ArrayInteractive:
                    this.interactiveHelper.HandleMouseMoveForArrayInteractive(e);
                    break;
            }

            this.canvasOwner.SetPositionInfo(this.ToUnit(new PointF(e.X, e.Y)));
        }
        private void MouseMoveDraw(MouseEventArgs e)
        {
            UnitPoint mousePoint;
            UnitPoint unitPoint = this.ToUnit(new PointF(e.X, e.Y));
            Rectangle invalidateRect = Rectangle.Empty;
            ISnapPoint newSnap = null;
            mousePoint = this.GetMousePoint();
            if (this.RunningSnapsEnable)
            {
                newSnap = this.Model.SnapPoint(this.canvasWrapper, mousePoint, this.RunningSnaps, null);
            }
            if (newSnap == null)
            {
                newSnap = this.Model.GridLayer.SnapPoint(this.canvasWrapper, mousePoint, null);
            }
            if ((this.snapPoint != null) && ((newSnap == null) || (newSnap.SnapPoint != this.snapPoint.SnapPoint) || this.snapPoint.GetType() != newSnap.GetType()))
            {
                invalidateRect = ScreenUtils.ConvertRectangle(ScreenUtils.ToScreenNormalized(this.canvasWrapper, this.snapPoint.BoundingRect));
                invalidateRect.Inflate(2, 2);
                RepaintStatic(invalidateRect); // 移除旧的捕获栅格
                this.snapPoint = newSnap;
            }
            if (this.snapPoint == null)
            {
                this.snapPoint = newSnap;
            }
            //RepaintMarkLayer();
            //this.canvasOwner.SetPositionInfo(unitPoint);
            this.canvasOwner.SetSnapInfo(this.snapPoint);
            if (this.snapPoint != null)
            {
                mousePoint = this.snapPoint.SnapPoint;
            }
            else
            {
                mousePoint = GetMousePoint();
            }
            if (this.NewObject != null)
            {
                Rectangle invalidaterect = ScreenUtils.ConvertRectangle(ScreenUtils.ToScreenNormalized(this.canvasWrapper, this.NewObject.GetBoundingRectangle(BoundingRectangleType.Drawing)));
                invalidaterect.Inflate(2, 2);
                RepaintStatic(invalidaterect);
                this.NewObject.OnMouseMove(this.canvasWrapper, mousePoint);
                RepaintObject(this.NewObject);
                //RepaintMarkLayer();
            }
            if (this.snapPoint != null)
            {
                RepaintSnappoint(this.snapPoint);
            }
            switch (this.Model.DrawingPattern)
            {
                case DrawingPattern.SelectPattern:
                    break;
                case DrawingPattern.NodeEditPattern:
                    break;
                case DrawingPattern.SortPattern:
                    break;
                case DrawingPattern.TranslationViewPattern:
                    break;
            }
        }

        private void MouseMoveSetCoolPoint(MouseEventArgs e)
        {

        }
        private void MouseMoveSmoothing(MouseEventArgs e)
        {

        }
        private void MouseMoveUnSmoothing(MouseEventArgs e)
        {

        }

        private void MouseMoveNone(MouseEventArgs e)
        {
            UnitPoint mousePoint = this.ToUnit(e.Location);
            switch (this.Model.DrawingPattern)
            {
                case DrawingPattern.SelectPattern:
                    if (this.canMove && SelectCanMove)
                    {
                        if (isLeftDown && this.Model.SelectedCount > 0)
                        {
                            if (this.moveHelper.HandleMouseMoveForMove(mousePoint))
                            {
                                Refresh();
                            }
                        }
                    }
                    else
                    {
                        if (this.selectRect != null)
                        {
                            Graphics graphics = Graphics.FromHwnd(Handle);
                            this.selectRect.SetMousePoint(graphics, new PointF(e.X, e.Y));
                            graphics.Dispose();
                        }
                    }
                    break;
                case DrawingPattern.NodeEditPattern:
                    {
                        if (this.isLeftDown && !this.nodeMoveHelper.IsEmpty)
                        {
                            RectangleF rNoderect = this.nodeMoveHelper.HandleMouseMoveForNode(mousePoint);
                            if (rNoderect != RectangleF.Empty)
                            {
                                Rectangle invalidaterect = ScreenUtils.ConvertRectangle(ScreenUtils.ToScreenNormalized(this.canvasWrapper, rNoderect));
                                RepaintStatic(invalidaterect);
                                CanvasWrapper dc = new CanvasWrapper(this, Graphics.FromHwnd(Handle), ClientRectangle);
                                dc.Graphics.Clip = new Region(ClientRectangle);
                                this.nodeMoveHelper.DrawObjects(dc, rNoderect);
                                if (this.snapPoint != null)
                                {
                                    RepaintSnappoint(this.snapPoint);
                                }
                                dc.Graphics.Dispose();
                                dc.Dispose();
                            }
                        }
                        break;
                    }
                case DrawingPattern.SortPattern:
                    break;
                case DrawingPattern.TranslationViewPattern:
                    {
                        if (this.isLeftDown)
                        {
                            this.dragOffset.X = -(this.mouseDownPoint.X - e.X);
                            this.dragOffset.Y = -(this.mouseDownPoint.Y - e.Y);
                            this.lastCenterPoint = CenterPointUnit();
                            DoInvalidate(true);
                            this.RaiseScaleChangedEvent();
                        }
                        break;
                    }
            }
        }
        #endregion

        #region 鼠标弹起事件处理
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.isLeftDown = false;
            base.OnMouseUp(e);
            switch (this.commandType)
            {
                case CommandType.Draw:
                    this.MouseUpDraw(e);
                    break;
                case CommandType.SetCoolPoint:
                    this.MouseUpSetCoolPoint(e);
                    break;
                case CommandType.Smothing:
                    this.MouseUpSmoothing(e);
                    break;
                case CommandType.UnSmothing:
                    this.MouseUpUnSmoothing(e);
                    break;
                case CommandType.None:
                    this.MouseUpNone(e);
                    break;
            }
        }
        private void MouseUpDraw(MouseEventArgs e)
        {
            if (this.NewObject != null)
            {
                UnitPoint mousePoint = this.ToUnit(this.mouseDownPoint);
                if (this.snapPoint != null)
                {
                    mousePoint = this.snapPoint.SnapPoint;
                }
                this.NewObject.OnMouseUp(this.canvasWrapper, mousePoint, this.snapPoint);
            }
            this.DoInvalidate(true);
        }

        private void MouseUpSetCoolPoint(MouseEventArgs e)
        {

        }
        private void MouseUpSmoothing(MouseEventArgs e)
        {

        }
        private void MouseUpUnSmoothing(MouseEventArgs e)
        {

        }
        private void MouseUpNone(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.HandleRightMenu(e.Location);
            }
            else
            {
                UnitPoint mousePoint = this.ToUnit(this.mouseDownPoint);
                switch (this.Model.DrawingPattern)
                {
                    case DrawingPattern.SelectPattern:
                        {
                            List<IDrawObject> hitList = null;
                            Rectangle screentSelectedRect = Rectangle.Empty;
                            if (this.selectRect != null)
                            {
                                screentSelectedRect = this.selectRect.ScreenRect();
                                RectangleF selectionRect = this.selectRect.Selection(this.canvasWrapper);
                                if (selectionRect != RectangleF.Empty)
                                {
                                    hitList = this.Model.GetHitObjects(this.canvasWrapper, selectionRect, this.selectRect.AnyPoint());
                                    this.DoInvalidate(true);
                                }
                                else
                                {
                                    hitList = this.Model.GetHitObjects(this.canvasWrapper, mousePoint);
                                }
                                this.selectRect = null;
                            }
                            if (hitList != null)
                            {
                                this.HandleSelection(hitList);
                                if (this.canMove && SelectCanMove)
                                {
                                    UnitPoint mouseUpPoint = this.ToUnit(e.Location);
                                    this.moveHelper.HandleMouseDownForMove(mouseUpPoint, snapPoint);
                                    this.canMove = false;

                                }
                            }
                            this.DoInvalidate(true);
                            //this.moveHelper.HandleCancelMove();
                            break;
                        }
                    case DrawingPattern.NodeEditPattern:
                        {
                            List<IDrawObject> hitList = this.Model.GetHitObjects(this.canvasWrapper, mousePoint);
                            if (hitList != null)
                            {
                                this.HandleSelection(hitList);
                            }
                            bool handled = false;
                            if (!this.nodeMoveHelper.IsEmpty)
                            {
                                this.nodeMoveHelper.HandleMouseDown(this.ToUnit(e.Location), ref handled);
                            }
                            this.DoInvalidate(true);
                            break;
                        }
                    case DrawingPattern.SortPattern:
                        break;
                    case DrawingPattern.TranslationViewPattern:
                        {
                            this.panOffset.X += this.dragOffset.X;
                            this.panOffset.Y += this.dragOffset.Y;
                            this.dragOffset = new PointF(0, 0);
                            break;
                        }
                }
            }
        }      
        #endregion

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            UnitPoint p = GetMousePoint();
            float wheeldeltatick = 120;
            float zoomdelta = (1.25f * (Math.Abs(e.Delta) / wheeldeltatick));
            if (e.Delta < 0)
                this.Model.Zoom = this.Model.Zoom / zoomdelta;
            else
                this.Model.Zoom = this.Model.Zoom * zoomdelta;
            SetCenterScreen(ToScreen(p), true);
            DoInvalidate(true);
            base.OnMouseWheel(e);
            this.RaiseScaleChangedEvent();
        }
        private void RepaintStatic(Rectangle rectangle)
        {
            if (this.bitmap != null)
            {
                Graphics graphics = Graphics.FromHwnd(this.Handle);
                if (rectangle.Width > this.bitmap.Width || rectangle.Width < 0)
                {
                    rectangle.X = 0;
                    rectangle.Width = this.bitmap.Width;
                }
                if (rectangle.Y > this.bitmap.Height || rectangle.Y < 0)
                {
                    rectangle.Y = 0;
                    rectangle.Height = this.bitmap.Height;
                }
                graphics.DrawImage(this.bitmap, rectangle, rectangle, GraphicsUnit.Pixel);
                graphics.Dispose();
            }
        }
        private void RepaintSnappoint(ISnapPoint snapPoint)
        {
            if (this.snapPoint == null) return;
            CanvasWrapper canvasWrapper = new CanvasWrapper(this, Graphics.FromHwnd(this.Handle), this.ClientRectangle);
            snapPoint.Draw(canvasWrapper);
            canvasWrapper.Graphics.Dispose();
            canvasWrapper.Dispose();
        }
        private void RepaintObject(IDrawObject drawObject)
        {
            if (drawObject != null)
            {
                CanvasWrapper canvasWrapper = new CanvasWrapper(this, Graphics.FromHwnd(this.Handle), this.ClientRectangle);
                RectangleF invalidateRect = ScreenUtils.ConvertRectangle(ScreenUtils.ToScreenNormalized(this.canvasWrapper, drawObject.GetBoundingRectangle(BoundingRectangleType.Drawing)));
                drawObject.Draw(canvasWrapper, invalidateRect);
                canvasWrapper.Graphics.Dispose();
                canvasWrapper.Dispose();
            }
        }
        public void RaiseScaleChangedEvent()
        {
            var point1 = ToUnit(new PointF(0, 0));
            var point2 = ToUnit(new PointF(this.Width, this.Height));

            PointF startPoint = new PointF((float)point1.X, (float)point1.Y);
            PointF endPoint = new PointF((float)point2.X, (float)point2.Y);

            this.OnScaleChanged?.Invoke(this, new ScaleChangedEventArgs { StartPoint = startPoint, EndPoint = endPoint });
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.lastCenterPoint != UnitPoint.Empty && Width != 0)
                SetCenterScreen(ToScreen(this.lastCenterPoint), false);
            this.lastCenterPoint = CenterPointUnit();
            this.bitmap = null;
            DoInvalidate(true);
        }
        private void UCCanvas_SizeChanged(object sender, EventArgs e)
        {
            this.RaiseScaleChangedEvent();
        }
        private void UpdateCursor(object key)
        {
            this.Cursor = this.cursorCollection.GetCursor(key);
        }
        private float ScreenHeight()
        {
            return (float)(ToUnit(this.ClientRectangle.Height) / this.Model.Zoom);
        }

        #region ICanvas
        public UnitPoint CenterPointUnit()
        {
            UnitPoint p1 = ScreenTopLeftToUnitPoint();
            UnitPoint p2 = ScreenBottomRightToUnitPoint();
            UnitPoint center = new UnitPoint();
            center.X = (p1.X + p2.X) / 2;
            center.Y = (p1.Y + p2.Y) / 2;
            return center;
        }
        public UnitPoint ScreenTopLeftToUnitPoint()
        {
            return this.ToUnit(new PointF(0, 0));
        }
        public UnitPoint ScreenBottomRightToUnitPoint()
        {
            return this.ToUnit(new PointF(this.ClientRectangle.Width, this.ClientRectangle.Height));
        }
        public PointF ToScreen(UnitPoint point)
        {
            PointF transformedPoint = point.Point;
            transformedPoint.Y = ScreenHeight() - transformedPoint.Y;
            transformedPoint.Y *= this.screenResolution * this.Model.Zoom;
            transformedPoint.X *= this.screenResolution * this.Model.Zoom;

            transformedPoint.X += this.panOffset.X + this.dragOffset.X;
            transformedPoint.Y += this.panOffset.Y + this.dragOffset.Y;
            return transformedPoint;
        }
        public float ToScreen(double value)
        {
            return (float)(value * this.screenResolution * this.Model.Zoom);
        }
        public UnitPoint ToUnit(PointF screenPoint)
        {
            float panOffsetX = this.panOffset.X + this.dragOffset.X;
            float panOffsetY = this.panOffset.Y + this.dragOffset.Y;
            float xPos = (screenPoint.X - panOffsetX) / (this.screenResolution * this.Model.Zoom);
            float yPos = this.ScreenHeight() - (screenPoint.Y - panOffsetY) / (this.screenResolution * this.Model.Zoom);
            return new UnitPoint(xPos, yPos);
        }
        public double ToUnit(float screenValue)
        {
            return (double)screenValue / (double)(this.screenResolution * this.Model.Zoom);
        }

        #region 图形绘制新方法
       
        public void DrawDot(ICanvas canvas, Brush brush, UnitPoint center, float radius)
        {
            RectangleF rectangleF = new RectangleF(this.ToScreen(center), new SizeF(0, 0));
            radius = (float)Math.Round(this.ToScreen(radius));
            rectangleF.Inflate(radius, radius);
            canvas.Graphics.FillEllipse(brush, rectangleF);
        }
        public void DrawDot(ICanvas canvas, Pen pen, UnitPoint center, float radius)
        {
            RectangleF rectangleF = new RectangleF(this.ToScreen(center), new SizeF(0, 0));
            radius = (float)Math.Round(this.ToScreen(radius));
            rectangleF.Inflate(radius, radius);
            canvas.Graphics.DrawEllipse(pen, rectangleF);
        }
        public void DrawLine(ICanvas canvas, Pen pen, UnitPoint p1, UnitPoint p2, float scale=0)
        {
            PointF tempP1 = this.ToScreen(p1);
            PointF tempP2 = this.ToScreen(p2);
            pen = this.GetDrawPen(pen,scale);
            canvas.Graphics.DrawLine(pen, tempP1, tempP2);
        }
        public void DrawArc(ICanvas canvas, Pen pen, UnitPoint center, float radius, float startAngle, float sweepAngle, float scale=0)
        {
            PointF circleCenter = this.ToScreen(center);
            radius = (float)Math.Round(this.ToScreen(radius));
            RectangleF rectangleF = new RectangleF(circleCenter, new SizeF());
            rectangleF.Inflate(radius, radius);
            pen = this.GetDrawPen(pen,scale);
            int step=360;
            if (scale != 0)
            {
                if(scale<100)
                {
                    step = 120;
                }
                else if(scale<200)
                {
                    step = 180;
                }
            }
            
            if (radius > 0 && radius < 1e8f)
            {
                if (sweepAngle >= 0)
                {
                    float angle = sweepAngle;
                    float sa = startAngle;
                    for (; angle > step + 10; angle -= step, sa += step)
                    {
                        canvas.Graphics.DrawArc(pen, rectangleF, -sa, -step);
                    }
                    if (angle != 0)
                    {
                        double length = DrawingOperationHelper.GetArcLength(radius, angle);
                        if (Math.Abs(length) > 0.1)
                        {
                            canvas.Graphics.DrawArc(pen, rectangleF, -sa, -angle);
                        }
                    }
                }
                else
                {
                    step = -step;
                    float angle = sweepAngle;
                    float sa = startAngle;
                    for (; angle < step - 10; angle -= step, sa += step)
                    {
                        canvas.Graphics.DrawArc(pen, rectangleF, -sa, -step);
                    }
                    if (angle != 0)
                    {
                        double length = DrawingOperationHelper.GetArcLength(radius, angle);
                        if (Math.Abs(length) > 0.1)
                        {
                            canvas.Graphics.DrawArc(pen, rectangleF, -sa, -angle);
                        }
                    }
                }
            }
        }
        public void DrawBeziers(ICanvas canvas, Pen pen, List<UnitPoint> points, float scale=0)
        {
            if (points == null || points.Count < 4) return;
            pen = this.GetDrawPen(pen,scale);
            PointF[] temp = points.Select(p => this.ToScreen(p)).ToArray();
            bool isValid = true;
            for (int i = 1; i < temp.Length; i++)
            {
                if (HitUtil.Distance(new UnitPoint(temp[i - 1].X, temp[i - 1].Y), new UnitPoint(temp[i].X, temp[i].Y)) < 1f)
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid)
            {
                canvas.Graphics.DrawBeziers(pen, temp);
            }
        }


        public void DrawStartDot(ICanvas canvas, Brush brush, UnitPoint unitPoint)
        {
            RectangleF rectangleF = new RectangleF(this.ToScreen(unitPoint), new SizeF(0, 0));
            rectangleF.Inflate(4, 4);
            canvas.Graphics.FillEllipse(brush, rectangleF);
        }
        public void DrawSN(ICanvas canvas, string sn, UnitPoint position)
        {
            PointF pos = this.ToScreen(position);
            canvas.Graphics.DrawString(sn, this.snFont, this.snBrush, pos);
        }
        public void DrawLeadLine(ICanvas canvas, Pen pen, UnitPoint p1, UnitPoint p2, float scale=0)
        {
            PointF tempP1 = this.ToScreen(p1);
            PointF tempP2 = this.ToScreen(p2);
            PointF p3 = this.ToScreen(HitUtil.LineMidpoint(p1, p2));
            pen = this.GetDrawPen(pen,scale);
            canvas.Graphics.DrawLine(pen, tempP1, p3);
            pen = this.GetDrawPen(pen,scale);
            canvas.Graphics.DrawLine(pen, p3, tempP2);
        }
        public void DrawBoundRectangle(ICanvas canvas, Pen pen, PointF leftTopPoint, float width, float height)
        {
            PointF temp = this.ToScreen(new UnitPoint(leftTopPoint.X, leftTopPoint.Y));
            width = (float)Math.Round(this.ToScreen(width));
            height = (float)Math.Round(this.ToScreen(height));
            canvas.Graphics.DrawRectangle(pen, (float)temp.X, (float)temp.Y, width, height);
        }
        public void DrawMultiLoopFlag(ICanvas canvas, Brush brush, UnitPoint startPoint, string loops)
        {
            PointF point = this.ToScreen(startPoint);
            RectangleF rectangleF = new RectangleF(point, new SizeF(0, 0));
            rectangleF.Inflate(10, 10);
            canvas.Graphics.FillEllipse(brush, rectangleF);
            Font font = new Font("Times New Roman", 12, FontStyle.Regular);
            SizeF size = canvas.Graphics.MeasureString(loops, font);
            Single posX = (rectangleF.Width - Convert.ToInt16(size.Width)) / 2;
            Single posY = (rectangleF.Height - Convert.ToInt16(size.Height)) / 2;
            canvas.Graphics.DrawString(loops, font, Brushes.Blue, rectangleF.X + posX, rectangleF.Y + posY);
        }

        private Pen GetDrawPen(Pen pen,float scale)
        {
            if (AdditionalInfo.Instance.IsShowMachinePath && scale != 0)
            {
                pen.CustomEndCap = new AdjustableArrowCap(10, 10, false);
            }
            else
            {
                pen.EndCap = LineCap.Flat;
            }
            return pen;
        }
        #endregion
        #endregion

        #region 绘图模式和命令类型切换逻辑处理
        private void HandlCancelSetStartMovePoint()
        {
            this.leadLine = null;
            this.leadLineDone = true;
            this.DoInvalidate(true);
        }
        public void CommandSelectPattern()
        {
            this.CommandEscape();
            this.Model.DrawingPattern = DrawingPattern.SelectPattern;
            this.UpdateCursor(this.Model.DrawingPattern);
        }
        public void CommandEscape()
        {
            bool dirty = (this.NewObject != null) || (this.snapPoint != null);
            this.NewObject = null;
            this.snapPoint = null;
            this.commandType = CommandType.None;
            this.moveHelper.HandleCancelMove();
            this.nodeMoveHelper.HandleCancelMove();
            this.rotateAnyHelper.HandleCancelRotate();
            this.mirrorHelper.HandleCancelMirror();
            this.scaleHelper.HandleCancelScale();
            this.measureHelper.HandleCancelMeasure();
            this.interactiveHelper.HandleCancelArrayInteractive();
            this.annularHelper.HandleCancelArrayAnnular();
            if (this.leadLine != null)
            {
                this.HandlCancelSetStartMovePoint();
            }
            this.DoInvalidate(dirty);
            this.UpdateCursor(this.Model.DrawingPattern);
        }
        public void CommandTranslationView()
        {
            this.CommandEscape();
            this.Model.DrawingPattern = DrawingPattern.TranslationViewPattern;
            this.UpdateCursor(this.Model.DrawingPattern);
        }
        public void CommandSortPattern()
        {
            this.CommandEscape();
            this.Model.DrawingPattern = DrawingPattern.SortPattern;
            this.UpdateCursor(this.Model.DrawingPattern);
        }

        public void CommandNodeEidtPattern()
        {
            this.CommandEscape();
            this.Model.DrawingPattern = DrawingPattern.NodeEditPattern;
            this.UpdateCursor(this.Model.DrawingPattern);
            this.DoInvalidate(true);
        }
        public void CommandSelectDrawTool(string drawObjectId)
        {
            this.CommandEscape();
            this.Model.ClearSelectedObjects();
            this.commandType = CommandType.Draw;
            this.drawObjectId = drawObjectId;
            this.UpdateCursor(this.commandType);
            SendCommandMSG(drawObjectId);
        }

        public void CommandSetStartPoint()
        {
            this.CommandEscape();
            this.commandType = CommandType.SetStartPoint;
            this.DoInvalidate(true);
        }

        #region 几何变化
        public void CommandMovePosition()
        {
            this.CommandEscape();
            this.commandType = CommandType.MovePosition;
            this.DoInvalidate(true);
        }

        public void CommandInteractScale()
        {
            this.CommandEscape();
            this.commandType = CommandType.InteractScale;
            this.DoInvalidate(true);
        }

        public void CommandRotateAny()
        {
            this.CommandEscape();
            this.commandType = CommandType.AnyRotate;
            this.DoInvalidate(true);
        }

        public void CommandMirrorAny()
        {
            this.CommandEscape();
            this.commandType = CommandType.AnyMirror;
            this.DoInvalidate(true);
        }
        #endregion
        public void CommandRoundAngle(int type, double radius)
        {
            this.roundRadius = radius;
            this.CommandEscape();
            if (type == 0)
            {
                this.commandType = CommandType.Smothing;
            }
            else
            {
                this.commandType = CommandType.UnSmothing;
            }
            this.DoInvalidate(true);
        }

        public void OnMicroConncect(float microConnLen, bool isApplyingToSimilarGraphics)
        {
            this.CommandEscape();
            this.commandType = CommandType.SetMicroConnect;
            this.microConnectLength = microConnLen;
            this.isApplyingToSimilarGraphics = isApplyingToSimilarGraphics;
            this.UpdateCursor(CommandType.SetMicroConnect);
            this.DoInvalidate(true);
        }

        public void OnSetCoolPoint()
        {
            this.CommandEscape();
            this.commandType = CommandType.SetCoolPoint;
            this.UpdateCursor(CommandType.SetCoolPoint);
            this.DoInvalidate(true);
        }
        #endregion
        public void AddQuickSnapType(Keys key, Type snapType)
        {
            this.quickSnap.Add(key, snapType);
        }
        public void CommandDeleteSelected()
        {
            this.Model.DeleteObjects(this.Model.DrawingLayer.SelectedObjects);
            this.Model.ClearSelectedObjects();
            this.DoInvalidate(true);
            this.UpdateCursor(this.Model.DrawingPattern);
        }
        protected void HandleQuickSnap(KeyEventArgs e)
        {
            if (this.Model.DrawingPattern == DrawingPattern.SelectPattern || this.Model.DrawingPattern == DrawingPattern.TranslationViewPattern)
            {
                return;
            }
            ISnapPoint snapPoint = null;
            UnitPoint mousePoint = this.GetMousePoint();
            if (this.quickSnap.ContainsKey(e.KeyCode))
            {
                snapPoint = this.Model.SnapPoint(this.canvasWrapper, mousePoint, null, this.quickSnap[e.KeyCode]);
            }
            if (snapPoint != null)
            {
                if (this.commandType == CommandType.Draw)
                {
                    this.HandleMouseDownWhenDrawing(snapPoint.SnapPoint, snapPoint, null);
                    if (this.NewObject != null)
                    {
                        this.NewObject.OnMouseMove(this.canvasWrapper, this.GetMousePoint());
                    }
                    this.DoInvalidate(true);
                    e.Handled = true;
                }
                if (this.Model.DrawingPattern == DrawingPattern.TranslationViewPattern)
                {
                    this.moveHelper.HandleMouseDownForMove(snapPoint.SnapPoint, snapPoint);
                    e.Handled = true;
                }
                if (this.nodeMoveHelper.IsEmpty == false)
                {
                    bool handled = false;
                    this.nodeMoveHelper.HandleMouseDown(snapPoint.SnapPoint, ref handled);
                    e.Handled = true;
                }
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!this.CanvasEnabled)
            {
                return;
            }

            this.HandleQuickSnap(e);
            if (this.nodeMoveHelper.IsEmpty == false)
            {
                this.nodeMoveHelper.OnKeyDown(this.canvasWrapper, e);
                if (e.Handled == true) return;
            }
            base.OnKeyDown(e);
            if (e.Handled)
            {
                this.UpdateCursor(this.Model.DrawingPattern);
                return;
            }
            if (this.NewObject != null)
            {
                this.NewObject.OnKeyDown(this.canvasWrapper, e);
                if (e.Handled) return;
            }
            foreach (IDrawObject drawObject in this.Model.DrawingLayer.SelectedObjects)
            {
                drawObject.OnKeyDown(this.canvasWrapper, e);
                if (e.Handled) return;
            }
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (e.KeyCode == Keys.G)
                {
                    this.Model.GridLayer.Locked = !this.Model.GridLayer.Locked;
                    this.DoInvalidate(true);
                }
                else if (e.KeyCode == Keys.S)
                {
                    this.RunningSnapsEnable = !this.RunningSnapsEnable;
                    if (!RunningSnapsEnable)
                    {
                        this.snapPoint = null;
                    }
                    this.DoInvalidate(false);
                }
                else if (e.KeyCode == Keys.C)
                {
                    Copy_Click(null, null);
                }
                else if (e.KeyCode == Keys.V)
                {
                    Paste_Click(null, null);
                }
                else if (e.KeyCode == Keys.X)
                {
                    tsCut_Click(null, null);
                }
                else if (e.KeyCode == Keys.R)
                {
                    Redo_Click(null, null);
                }
                else if (e.KeyCode == Keys.Z)
                {
                    Undo_Click(null, null);
                }
                else if(e.KeyCode==Keys.A)
                {
                    this.Model.DrawingLayer.Objects.ForEach(s => s.IsSelected = true);
                    this.DoInvalidate(true);
                }
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.CommandEscape();
            }
            else if (e.KeyCode == Keys.T)
            {
                this.CommandTranslationView();
            }
            else if (e.KeyCode == Keys.S)
            {
                this.RunningSnapsEnable = !this.RunningSnapsEnable;
                if (!RunningSnapsEnable)
                {
                    this.snapPoint = null;
                }
                this.DoInvalidate(false);
            }
            else if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
            {
                int layerIndex = (int)e.KeyCode - (int)Keys.D1;
                this.DoInvalidate(true);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                this.CommandDeleteSelected();
            }
            this.UpdateCursor(this.Model.DrawingPattern);
        }

        #region 右键菜单

        private void HandleRightMenu(Point point)
        {
            if (!this.CanvasEnabled)
            {
                return;
            }
            if (this.Model.SelectedCount == 0)
            {
                this.tsCopy.Visible = false;
                this.tsCut.Visible = false;
                this.tsDelete.Visible = false;
            }
            else
            {
                this.tsCopy.Visible = true;
                this.tsCut.Visible = true;
                this.tsDelete.Visible = true;
            }
            this.tsRedo.Visible = this.Model.CanRedo();
            this.tsUndo.Visible = this.Model.CanUndo();
            this.tsEndDrawing.Visible = this.commandType == CommandType.Draw;
            this.tsPaste.Visible = !(this.Model.CopyObjectInClipBoard.Count == 0);
            this.tsEndSetStartPoint.Visible = this.commandType == CommandType.SetStartPoint;
            this.tsEndSetCoolPoint.Visible = this.commandType == CommandType.SetCoolPoint;
            this.tsEndSetMicroConn.Visible = this.commandType == CommandType.SetMicroConnect;
            this.tsEndSetRelease.Visible = this.commandType == CommandType.UnSmothing;
            this.tsEndSetSmooth.Visible = this.commandType == CommandType.Smothing;
            this.tsEndSetBridge.Visible = this.commandType == CommandType.Bridge;
            this.contextMenuStrip1.Show(this, point);
        }
        public void Copy_Click(object sender, EventArgs e)
        {
            this.Model.Copy();
        }
        public void Paste_Click(object sender, EventArgs e)
        {
            this.Model.PasteObjects(this.ToUnit(this.mouseDownPoint));
            this.DoInvalidate(true);
            this.UpdateCursor(this.Model.DrawingPattern);
        }
        public void Delete_Click(object sender, EventArgs e)
        {
            this.Model.DeleteObjects(this.Model.DrawingLayer.SelectedObjects);
            this.Model.ClearSelectedObjects();
            this.DoInvalidate(true);
            this.UpdateCursor(this.Model.DrawingPattern);
        }
        private void Undo_Click(object sender, EventArgs e)
        {
            this.Model.DoUndo();
            this.DoInvalidate(true);
            this.UpdateCursor(this.Model.DrawingPattern);
        }
        private void Redo_Click(object sender, EventArgs e)
        {
            this.Model.DoRedo();
            this.DoInvalidate(true);
            this.UpdateCursor(this.Model.DrawingPattern);
        }
        public void tsCut_Click(object sender, EventArgs e)
        {
            this.Copy_Click(sender, e);
            this.Delete_Click(sender, e);
        }
        private void tsEndDrawing_Click(object sender, EventArgs e)
        {
            if (this.NewObject != null && this.NewObject.Id == "PolyLine")
            {
                this.NewObject.IsCompleteDraw = true;
                DrawButtonStatus(true);
            }
            else
            {
                DrawButtonStatus(false);
            }
            this.CommandEscape();
        }
        private void tsEndCommandEscape_Click(object sender, EventArgs e)
        {
            this.CommandEscape();
        }
        private void tsGroupSort_Click(object sender, EventArgs e)
        {
            Messenger.Instance.Send("GroupInsideSortAction", sender);
         }
        #endregion

        #region 标记层
        public bool StaticRefreshMark { get; set; } = true;

        public void ClearMark()
        {
            this.markLayer.ClearMark();
        }

        public void UpdateMarkPoint(PointF point)
        {
            this.markLayer.UpdateMarkPoint(point);
        }

        public void AddMarkPathPoint(string figureId, PointF point)
        {
            this.markLayer.AddMarkPathPoint(figureId, point);
        }

        public void UpdataMarkFlag(PointF p, Color color)
        {
            this.markLayer.UpdateMarkFlag(p, color);
        }

        public void UpdateRelativePos(PointF p)
        {
            this.markLayer.UpdateRelativePos(p);
        }

        public RectangleF GetRegion()
        {
            var p1 = this.ToUnit(new PointF(0, this.ClientRectangle.Height));
            var p2 = this.ToUnit(new PointF(this.ClientRectangle.Width, 0));
            return new RectangleF((float)p1.X, (float)p1.Y, (float)(p2.X - p1.X), (float)(p2.Y - p1.Y));
        }
        #endregion

        public static float GetThresholdWidth(float screenValue = 6)
        {
            return screenValue / GlobalData.Model.GlobalModel.ThresholdWidth;
        }

        #region 测量
        public void Measure()
        {
            this.CommandEscape();
            this.commandType = CommandType.Measure;
            this.DoInvalidate(true);
            SendCommandMSG(CanvasCommands.Measure.ToString());
        }
        #endregion
        #region  阵列
        /// <summary>
        /// 交互式阵列
        /// </summary>
        public void ArrayInteractive()
        {
            this.CommandEscape();
            this.commandType = CommandType.ArrayInteractive;
            this.DoInvalidate(true);
        }
        /// <summary>
        /// 环形阵列
        /// </summary>
        public void ArrayAnnular(ArrayAnnularModel arrayAnnularModel)
        {
            if (arrayAnnularModel.IsSetArrayCenterScope)//
            {
                //ArrayAnnularHelper arrayAnnularHelper = new ArrayAnnularHelper(this);
                annularHelper.ArrayAnnular();
            }
            else
            {
                this.CommandEscape();
                this.commandType = CommandType.ArrayAnnular;                
            }             
            DoInvalidate(true);            
        }
        #endregion
    }
}
