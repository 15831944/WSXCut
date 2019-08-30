using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.LeadLine;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.DrawModel.RingCut;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawModel;
using WSX.DrawService.DrawTool.Arcs;
using WSX.DrawService.DrawTool.MultiSegmentLine;
using WSX.DrawService.Utils;
using WSX.GlobalData.Model;

namespace WSX.DrawService.DrawTool.CircleTool
{
    public class Circle : DrawObjectBase, IDrawObject, IDrawTranslation, IDrawData
    {
        public UnitPoint Center { get; set; }
        public float Radius { get; set; }
        public float StartAngle { get; set; }
        public bool IsClockwise { get; set; }
        public float AngleSweep
        {
            get
            {
                if (this.IsClockwise)
                {
                    return -360;
                }
                else
                {
                    return 360;
                }
            }
        }

        public float OverCutLen { get; set; } = 1;
        public List<ArcBase> ArcMicroConns { get; set; }
        public List<ArcBase> ArcCompensations { get; set; }
        public List<MicroUnitPoint> MicroStartPoints { get; set; }

        public bool IsInnerCut { get; set; }
        private enum CircleCurrentPoint { Center, Radius, Done }
        private CircleCurrentPoint circleCurrentPoint;
        public void Update()
        {
            this.IsCompleteDraw = true;
            this.UpdateOverCutting();
            this.UpdateByChangeMicroConnect();
            this.UpdateByChangeCompensation();
            this.UpdateLeadwire();
            this.UpdateStartEndMovePoint();
            this.UpdateCoolPointByLeadIn();
        }
        private List<UnitPoint> GetNodePoints()
        {
            List<UnitPoint> nodePoints = new List<UnitPoint>();
            UnitPoint degree45 = new UnitPoint(this.Radius * Math.Cos(HitUtil.DegreesToRadians(45)) + this.Center.X, this.Radius * Math.Sin(HitUtil.DegreesToRadians(45)) + this.Center.Y);
            nodePoints.Add(degree45);
            for (int i = 0; i < 360; i += 90)
            {
                UnitPoint tempPoint = new UnitPoint(this.Radius * Math.Cos(HitUtil.DegreesToRadians(i)) + this.Center.X, this.Radius * Math.Sin(HitUtil.DegreesToRadians(i)) + this.Center.Y);
                nodePoints.Add(tempPoint);
            }
            return nodePoints;
        }

        #region DrawObjectBase
        public override void InitializeFromModel(UnitPoint unitPoint, ISnapPoint snapPoint)
        {
            this.LayerId = (int)GlobalModel.CurrentLayerId;
            this.OnMouseDown(null, unitPoint, snapPoint, null);
            this.IsSelected = true;
        }
        #endregion
        public Circle()
        {

        }
        #region IDrawObject
        public FigureTypes FigureType { get { return FigureTypes.Circle; } }
        public bool IsCompleteDraw { get; set; }
        public List<MicroConnectModel> MicroConnParams { get; set; }
        public CompensationModel CompensationParam { get; set; }
        public CornerRingModel CornerRingParam { get; set; }
        public LineInOutParamsModel LeadInOutParams { get; set; } = new LineInOutParamsModel();
        public LeadInParam LeadInWireParam { get; set; }
        public LeadOutParam LeadOutWireParam { get; set; }
        public LeadInLine LeadIn { get; set; } = new LeadInLine();
        public LeadOutLine LeadOut { get; set; } = new LeadOutLine();

        private ArcFlyingLeadOut flyingCutLeadOut = null;
        /// <summary>
        /// 圆弧飞切连接线（显示虚线）
        /// </summary>
        public ArcFlyingLeadOut FlyingCutLeadOut {
            set {
                flyingCutLeadOut = value;
                IsFlyingCutScatter = (value != null ? false : IsFlyingCutScatter);
            }
            get {
                return IsFlyingCutScatter ? null : flyingCutLeadOut;
            } //打散后隐藏飞切参数 FlyingCutLeadOut 返回空
        }

        /// <summary>
        /// 是否飞切被打散(打散后隐藏飞切参数 FlyingCutLeadOut)
        /// </summary>
        public bool IsFlyingCutScatter { set; get; } = false;
        /// <summary>
        /// 是否飞切了
        /// </summary>
        public bool IsFlyingCut { set; get; } = false;

        public String Name { set; get; }//飞切调试方便确认是哪个圆

        public UnitPoint FirstDrawPoint { get { return this.StartMovePoint; } }
        public void Move(UnitPoint offset)
        {
            this.Center = new UnitPoint(this.Center.X + offset.X, this.Center.Y + offset.Y);
            this.StartMovePoint += offset;
            if(this.FlyingCutLeadOut != null)
            {
                if(this.FlyingCutLeadOut.LeadEndPoint != UnitPoint.Empty)
                    this.FlyingCutLeadOut.LeadEndPoint += offset;
                if (this.FlyingCutLeadOut.PreviousEndPoint != UnitPoint.Empty) 
                    this.FlyingCutLeadOut.PreviousEndPoint += offset;
                if (this.FlyingCutLeadOut.NextNextPoint != UnitPoint.Empty)
                    this.FlyingCutLeadOut.NextNextPoint += offset;
            }
            this.Update();
        }
      
        public INodePoint NodePoint(UnitPoint unitPoint)
        {
            float thWidth = UCCanvas.GetThresholdWidth();
            List<UnitPoint> nodePoints = this.GetNodePoints();
            for (int i = 0; i < nodePoints.Count; i++)
            {
                if (HitUtil.PointInPoint(nodePoints[i], unitPoint, thWidth))
                {
                    return new CircleNode(this, CircleNodeType.StartPoint);
                }
            }
            if (HitUtil.PointInPoint(this.Center, unitPoint, thWidth))
            {
                return new CircleNode(this, CircleNodeType.Center);
            }
            return null;
        }

        public bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            float thWidth = UCCanvas.GetThresholdWidth() / 2;
            float r = this.Radius;
            RectangleF rectangleF = HitUtil.CircleBoundingRectangle(this.Center, r);
            if (anyPoint)
            {
                if (this.ArcMicroConns != null && this.ArcMicroConns.Count > 0)
                {
                    foreach (var item in this.ArcMicroConns)
                    {
                        if (HitUtil.IsArcIntersectWithRect(item, rectangle, thWidth))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    UnitPoint p1 = new UnitPoint(rectangle.Left, rectangle.Top);
                    UnitPoint p2 = new UnitPoint(rectangle.Left, rectangle.Bottom);
                    if (HitUtil.CircleIntersectWithLine(this.Center, this.Radius, p1, p2)) return true;
                    p1 = new UnitPoint(rectangle.Left, rectangle.Bottom);
                    p2 = new UnitPoint(rectangle.Right, rectangle.Bottom);
                    if (HitUtil.CircleIntersectWithLine(this.Center, this.Radius, p1, p2)) return true;
                    p1 = new UnitPoint(rectangle.Left, rectangle.Top);
                    p2 = new UnitPoint(rectangle.Right, rectangle.Top);
                    if (HitUtil.CircleIntersectWithLine(this.Center, this.Radius, p1, p2)) return true;
                    p1 = new UnitPoint(rectangle.Right, rectangle.Top);
                    p2 = new UnitPoint(rectangle.Right, rectangle.Bottom);
                    if (HitUtil.CircleIntersectWithLine(this.Center, this.Radius, p1, p2)) return true;
                }
            }
            return rectangle.Contains(rectangleF);
        }

        public void OnKeyDown(ICanvas canvas, KeyEventArgs e)
        {
        }

        public DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint, MouseEventArgs e)
        {
            this.OnMouseMove(canvas, point);
            if (this.circleCurrentPoint == CircleCurrentPoint.Center)
            {
                this.circleCurrentPoint = CircleCurrentPoint.Radius;

                return DrawObjectMouseDown.Continue;
            }
            if (this.circleCurrentPoint == CircleCurrentPoint.Radius)
            {
                this.circleCurrentPoint = CircleCurrentPoint.Done;
                this.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
                this.GroupParam.ShowSN = this.GroupParam.FigureSN;
                this.StartMovePoint = DrawingOperationHelper.GetPointOnArcByAngle(this.Center, this.Radius, this.StartAngle);
                this.LeadInIntersectPoint = this.LeadOutIntersectPoint = this.EndMovePoint = this.StartMovePoint;
                this.IsCompleteDraw = true;

            }
            return DrawObjectMouseDown.Done;
        }


        public void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {
            if (this.circleCurrentPoint == CircleCurrentPoint.Center)
            {
                this.Center = unitPoint;
                return;
            }
            if (this.circleCurrentPoint == CircleCurrentPoint.Radius)
            {
                this.Radius = (float)HitUtil.Distance(this.Center, unitPoint);
                return;
            }
        }

        public void OnMouseUp(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint)
        {
        }

        public bool PointInObject(UnitPoint unitPoint)
        {
            RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.All);
            if (rectangleF.Contains(unitPoint.Point) == false)
            {
                return false;
            }
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            if (HitUtil.PointInPoint(this.Center, unitPoint, thresholdWidth))
            {
                return true;
            }
            if (this.ArcMicroConns != null && this.ArcMicroConns.Count > 0)
            {
                foreach (var item in this.ArcMicroConns)
                {
                    bool result = HitUtil.IsPointOnArc(unitPoint, thresholdWidth / 2, new DrawModel.ArcModelMini()
                    {
                        Center = item.Center,
                        Radius = item.Radius,
                        StartAngle = item.StartAngle,
                        SweepAngle = item.AngleSweep,
                        Clockwise = this.IsClockwise
                    });
                    if (result) return result;
                }
                return false;
            }
            else
            {
                return HitUtil.IsPointInCircle(this.Center, this.Radius, unitPoint, thresholdWidth / 2);
            }
        }

        #region 图形附加操作

        #region 微连
        public void SetMicroConnectParams(bool isCancel, UnitPoint unitPoint, float microConnLen)
        {
            if (isCancel)
            {
                if (this.MicroConnParams != null)
                {
                    this.MicroConnParams.RemoveAll(e => e.Flags == MicroConnectFlags.MicroConnect);
                }
            }
            else
            {
                if (this.MicroConnParams == null)
                {
                    this.MicroConnParams = new List<MicroConnectModel>();
                }
                MicroConnectModel microConnectModel = new MicroConnectModel() { Flags = MicroConnectFlags.MicroConnect, Size = microConnLen };
                double angle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(this.Center, unitPoint, 0));
                angle = this.GetRelativeAngleByStartAngle(angle);
                microConnectModel.Position = (float)(angle / 360);
                this.MicroConnParams.Add(microConnectModel);
            }
            this.Update();
        }
        public void SetAutoMicroConnect(AutoMicroConParam param)
        {
            var micros = MicroConnectHelper.CalAutoMicroConnsParams(this.SizeLength, param);
            if (micros == null || micros.Count == 0) return;
            if (this.MicroConnParams != null)
            {
                this.MicroConnParams.RemoveAll(e => e.Flags == MicroConnectFlags.MicroConnect);
            }
            else
            {
                this.MicroConnParams = new List<MicroConnectModel>();
            }
            this.MicroConnParams.AddRange(micros);
            this.IsSelected = true;
            this.Update();
        }
        /// <summary>
        /// 根据微连参数集合更新绘图数据
        /// </summary>
        public void UpdateByChangeMicroConnect()
        {
            if (this.MicroConnParams == null || this.MicroConnParams.Count == 0)
            {
                //清除微连
                if (this.ArcMicroConns != null)
                {
                    this.ArcMicroConns.Clear();
                    this.ArcMicroConns = null;
                }
                if (this.MicroStartPoints != null)
                {
                    this.MicroStartPoints.Clear();
                    this.MicroStartPoints = null;
                }
            }
            else
            {
                List<MicroUnitPoint> microStartPoints;
                this.ArcMicroConns = MicroConnectHelper.CalMicroconnsByCircle(this, this.MicroConnParams, out microStartPoints);
                if (microStartPoints != null)
                {
                    microStartPoints.ForEach(e => { e.Point.IsBasePoint = true; });
                    if (this.MicroStartPoints == null)
                    {
                        this.MicroStartPoints = new List<MicroUnitPoint>();
                    }
                    this.MicroStartPoints.RemoveAll(e => e.Point.IsBasePoint == true);
                    this.MicroStartPoints.AddRange(microStartPoints);
                }
            }
        }

        #endregion

        #region 补偿
        public void DoCompensation(CompensationModel param)
        {
            //飞切后不允许补偿(飞切打散后允许)
            if (IsFlyingCut && !IsFlyingCutScatter)
                return;
            this.CompensationParam = param;
            if (param != null) this.IsInnerCut = this.CompensationParam.Style == CompensationType.AllInner;
            this.Update();
        }

        /// <summary>
        /// 更新补偿
        /// </summary>
        public void UpdateByChangeCompensation()
        {
            if (this.CompensationParam == null)
            {
                //清除补偿
                if (this.ArcCompensations != null)
                {
                    this.ArcCompensations.Clear();
                    this.ArcCompensations = null;
                }
                if (this.MicroStartPoints != null)
                {
                    this.MicroStartPoints.RemoveAll(e => e.Point.IsBasePoint == false);
                }
            }
            else
            {
                this.CompensationParam.Style = this.IsInnerCut ? CompensationType.AllInner : CompensationType.AllOuter;
                //计算补偿
                double r = this.CompensationParam.Style == CompensationType.AllInner ? this.Radius - this.CompensationParam.Size : this.Radius + this.CompensationParam.Size;
                if (r > 0)
                {
                    ArcBase arc = new ArcBase()
                    {
                        Center = this.Center,
                        AngleSweep = this.AngleSweep,
                        StartAngle = this.StartAngle,
                        Radius = (float)r
                    };
                    
                    if (this.MicroConnParams != null && this.MicroConnParams.Count > 0)
                    {
                        //补偿图形上计算微连
                        var circle = new Circle()
                        {
                            Radius = arc.Radius,
                            Center = arc.Center,
                            StartAngle = arc.StartAngle,
                            IsClockwise = this.IsClockwise
                        };
                        List<MicroUnitPoint> microStartPoints;
                        this.ArcCompensations = MicroConnectHelper.CalMicroconnsByCircle(circle, this.MicroConnParams, out microStartPoints);
                        if (microStartPoints != null)
                        {
                            microStartPoints.ForEach(e => { e.Point.IsBasePoint = false; });
                            if (this.MicroStartPoints == null)
                            {
                                this.MicroStartPoints = new List<MicroUnitPoint>();
                            }
                            this.MicroStartPoints.RemoveAll(e => e.Point.IsBasePoint == false);
                            this.MicroStartPoints.AddRange(microStartPoints);
                        }
                    }
                    else
                    {
                        arc.startPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.StartAngle));
                        arc.midPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.MidAngle));
                        arc.endPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.EndAngle));
                        this.ArcCompensations = new List<ArcBase>();
                        this.ArcCompensations.Add(arc);
                    }
                }
            }
        }

        #endregion

        #region 阳切
        public void SetOuterCut()
        {
            this.IsInnerCut = false;
            this.Update();
        }
        #endregion

        #region 阴切

        public void SetInnerCut()
        {
            this.IsInnerCut = true;
            this.Update();
        }
        #endregion

        #region 环切
        public void DoCornerRing(CornerRingModel param)
        {
            this.CornerRingParam = param;
            this.UpdateByChangeCornerRing();
        }

        public void UpdateByChangeCornerRing()
        {

        }
        #endregion

        #region 冷却点
       
        /// <summary>
        /// 引入点是否需要冷却
        /// </summary>
        private void UpdateCoolPointByLeadIn()
        {
            if (this.LeadIn.LeadInBreak && this.LeadIn.LeadType != LeadLineType.None)
            {
                bool isbase = !(this.ArcCompensations != null && this.ArcCompensations.Count > 0);
                if (this.MicroStartPoints == null)
                {
                    this.MicroStartPoints = new List<MicroUnitPoint>();
                    this.MicroStartPoints.Add(new MicroUnitPoint()
                    {
                        Flags = MicroConnectFlags.CoolingLeadIn,
                        Point = new UnitPointBulge() { Point = this.LeadInIntersectPoint, IsBasePoint = isbase },
                    });
                }
                else
                {
                    var point = this.MicroStartPoints.FirstOrDefault(e => e.Flags == MicroConnectFlags.CoolingLeadIn && HitUtil.PointInPoint(e.Point.Point, this.LeadInIntersectPoint, 0.0001f));
                    if (point == null)
                    {
                        this.MicroStartPoints.Add(new MicroUnitPoint()
                        {
                            Flags = MicroConnectFlags.CoolingLeadIn,
                            Point = new UnitPointBulge() { Point = this.LeadInIntersectPoint, IsBasePoint = isbase },
                        });
                    }
                    else
                    {
                        point.Point = new UnitPointBulge() { Point = this.LeadInIntersectPoint, IsBasePoint = isbase };
                    }
                }
            }
            else
            {
                this.MicroStartPoints?.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingLeadIn);
            }
        }
        public void SetCoolPoint(UnitPoint unitPoint, bool isLeadIn = false, bool isCorner = false, double maxAngle = 0)
        {
            //飞切后不允许设置冷却点(飞切打散后可以)
            if (IsFlyingCut && !IsFlyingCutScatter)
                return;

            if (unitPoint.IsEmpty)
            {
                //自动添加冷却点，计算冷却点参数
                this.MicroConnParams?.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingPoint);
                //引入点是否需要冷却
                this.LeadIn.LeadInBreak = isLeadIn;
            }
            else
            {
                if (this.MicroConnParams == null)
                {
                    this.MicroConnParams = new List<MicroConnectModel>();
                }
                MicroConnectModel microConnectModel = new MicroConnectModel() { Flags = MicroConnectFlags.CoolingPoint, Size = 0 };
                double angle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(this.Center, unitPoint, 0));
                angle = this.GetRelativeAngleByStartAngle(angle);
                microConnectModel.Position = (float)(angle / 360);
                this.MicroConnParams.Add(microConnectModel);
            }
            this.Update();
        }

        /// <summary>
        /// 根据当前角度转为相对于起始角的度数
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private double GetRelativeAngleByStartAngle(double angle)
        {
            if (this.IsClockwise)
            {
                if (this.StartAngle > angle)
                {
                    angle = this.StartAngle - angle;
                }
                else
                {
                    angle = 360 - angle + this.StartAngle;
                }
            }
            else
            {
                if (this.StartAngle > angle)
                {
                    angle = 360 - this.StartAngle + angle;
                }
                else
                {
                    angle = angle - this.StartAngle;
                }
            }

            return angle;
        }

        public void ClearCoolPoint()
        {
            if (this.MicroConnParams != null)
            {
                this.MicroConnParams.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingPoint);
            }
            this.LeadIn.LeadInBreak = false;
            this.Update();
        }
        #endregion


        #endregion

        #region 显示图形附加信息
        public void ShowBoundRect(ICanvas canvas, int penIndex)
        {
            throw new NotImplementedException();
        }

        public void ShowFigureSN(ICanvas canvas)
        {
            if (AdditionalInfo.Instance.IsShowFigureSN)
            {
                if (this.GroupParam.ShowSN != 0)
                {
                    canvas.DrawSN(canvas, this.GroupParam.ShowSN.ToString(), this.StartMovePoint);
                }
            }
        }

        public void ShowMicroConnectFlag(ICanvas canvas)
        {
            if (this.MicroStartPoints != null)
            {
                if (AdditionalInfo.Instance.IsShowMicroConnectFlag)
                {
                    DrawUtils.DrawMicroFlag(canvas, this.MicroStartPoints.Where(e => e.Flags == MicroConnectFlags.MicroConnect && e.Point.IsBasePoint).Select(e => e.Point.Point).ToList());
                }
            }
        }

        public void ShowStartMovePoint(ICanvas canvas)
        {
            if (AdditionalInfo.Instance.IsShowStartMovePoint)
            {
                canvas.DrawStartDot(canvas, Brushes.Yellow, this.StartMovePoint);
            }
        }

        #endregion
        public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint unitPoint, List<IDrawObject> drawObjects, Type[] runningSnapTypes, Type userSnapType)
        {
            return null;
        }

        public string Id { get { return "Circle"; } }

        public double SizeLength
        {
            get
            {
                return 2 * Math.PI * this.Radius;
            }
        }

        public bool IsCloseFigure { get { return true; } }

        public bool IsSelected { get; set; }
        private bool isvisiblity = true;
        public bool IsVisiblity
        {
            get { return isvisiblity; }
            set { isvisiblity = value; }
        }
        private bool islocked = false;
        public bool IsLocked
        {
            get { return islocked; }
            set { islocked = value; }
        }

        public UnitPoint StartMovePoint { set; get; }
        public UnitPoint EndMovePoint { get; set; }
        public float PathStartParm { get; set; }

        public UnitPoint RepeatStartingPoint { get { return UnitPoint.Empty; } }

        public IDrawObject Clone()
        {
            Circle circle = new Circle();
            circle.Copy(this);
            return circle;
        }

        public void Copy(Circle circle)
        {
            base.Copy(circle);
            this.Center = circle.Center;
            this.Radius = circle.Radius;
            this.GroupParam = CopyUtil.DeepCopy(circle.GroupParam);
            this.IsClockwise = circle.IsClockwise;
            this.StartAngle = circle.StartAngle;
            this.IsCompleteDraw = circle.IsCompleteDraw;
            this.StartMovePoint = circle.StartMovePoint;
            this.LeadIn = CopyUtil.DeepCopy(circle.LeadIn);
            this.LeadOut = CopyUtil.DeepCopy(circle.LeadOut);
            this.LeadInOutParams = CopyUtil.DeepCopy(circle.LeadInOutParams);
            this.LeadInWireParam = CopyUtil.DeepCopy(circle.LeadInWireParam);
            this.LeadOutWireParam = CopyUtil.DeepCopy(circle.LeadOutWireParam);
            this.MicroConnParams = CopyUtil.DeepCopy(circle.MicroConnParams);
            this.CompensationParam = CopyUtil.DeepCopy(circle.CompensationParam);
            this.LeadInIntersectPoint = circle.LeadInIntersectPoint;
            this.LeadOutIntersectPoint = circle.LeadOutIntersectPoint;
            this.StartMovePoint = circle.StartMovePoint;
            this.EndMovePoint = circle.EndMovePoint;
            this.IsInnerCut = circle.IsInnerCut;
            this.flyingCutLeadOut = CopyUtil.DeepCopy(circle.FlyingCutLeadOut);//最好用首字母小写的 flyingCutLeadOut
            this.IsFlyingCut = circle.IsFlyingCut;
            this.IsFlyingCutScatter = circle.IsFlyingCutScatter;
            if (this.IsCompleteDraw)
            {
                this.Update();
            }
        }

        public void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            if (IsVisiblity == false) return;
            var items = new List<IDrawLite>();
            items.AddRange(this.GetBasic());
            items.AddRange(this.GetCompensation());
            items.AddRange(this.GetCoolingPoints());
            items.AddRange(this.GetLeadIn());
            items.AddRange(this.GetLeadOut());

            //飞切虚线 先过切(也可没有过切)然后直线空移到下一个圆的起点虚线
            items.AddRange(this.GetFlyingCutLeadOut());

            float scale=HitUtil.IsDrawExtraProperties(unitRectangle, this.GetBoundingRectangle(BoundingRectangleType.All));
            bool isDrawExtraProperties = scale < 1000;
            bool isShowMachinePath = AdditionalInfo.Instance.IsShowMachinePath;
            AdditionalInfo.Instance.IsShowMachinePath = AdditionalInfo.Instance.IsShowMachinePath && this.IsCompleteDraw && (this.LayerId != (int)WSX.GlobalData.Model.LayerId.White) && isDrawExtraProperties;
            DrawUtils.Draw(canvas, items, scale);
            AdditionalInfo.Instance.IsShowMachinePath = isShowMachinePath;

            if (this.IsCompleteDraw && isDrawExtraProperties)
            {
                this.DrawNodes(canvas);
                this.ShowStartMovePoint(canvas);
                this.ShowFigureSN(canvas);
                this.ShowMicroConnectFlag(canvas);
                this.ShowMultiLoopFlag(canvas);
            }
        }

        private void DrawNodes(ICanvas canvas)
        {
            if (this.IsSelected && this.IsCompleteDraw && canvas.DataModel.DrawingPattern == DrawingPattern.NodeEditPattern && this.GroupParam.GroupSN.Count == 1)
            {
                List<UnitPoint> nodePoints = this.GetNodePoints();
                for (int i = 0; i < nodePoints.Count; i++)
                {
                    DrawUtils.DrawNode(canvas, nodePoints[i]);
                }
                DrawUtils.DrawCircleNode(canvas, this.Center);
            }
        }
        private void ShowMultiLoopFlag(ICanvas canvas)
        {
            if (this.OverCutLen >= 2)
            {
                canvas.DrawMultiLoopFlag(canvas, Brushes.PeachPuff, this.LeadInIntersectPoint, ((int)this.OverCutLen).ToString());
            }
        }
      
        private UnitPoint GetLeadLineIntersectPoint()
        {
            double angle = this.LeadIn.Position * 360.0;
            return HitUtil.PointOnCircle(this.Center, this.Radius, HitUtil.DegreesToRadians(angle));
        }

        private UnitPoint GetLeadLineEndPoint()
        {
            UnitPoint intersectPoint = this.GetLeadLineIntersectPoint();
            float lineAngle = (float)HitUtil.LineAngleR(intersectPoint, this.Center, 0);
            return DrawingOperationHelper.GetLeadLineEndPoint(intersectPoint, lineAngle, this.LeadIn.Angle, this.LeadIn.Length);
        }
        
        //这里是使用引线的点作为依据点，故在有引线或是补偿时会对原图的判断有差异性(例：由于引线的长度很大时，本来被包含的圆，被误判为相交--xiesheng)
        public RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            float thWidth = UCCanvas.GetThresholdWidth();
            RectangleF rectangleF = HitUtil.CircleBoundingRectangle(this.Center, this.Radius + thWidth / 2);
            if (this.LeadIn != null)
            {
                RectangleF rectangleLine = ScreenUtils.GetRectangleF(this.GetLeadLineEndPoint(), this.GetLeadLineIntersectPoint(), thWidth / 2);
                rectangleF = RectangleF.Union(rectangleF, rectangleLine);
            }
            return rectangleF;
        }

        public string GetInfoAsString()
        {
            return $"圆，圆心：({this.Center.X},{this.Center.Y}),半径:{this.Radius} 图形总长: {this.SizeLength}";
        }
        #endregion

        #region IDrawTranslation
        public UnitPoint MaxValue
        {
            get
            {
                RectangleF rectangleF = this.GetBoundingRectangle(0);
                UnitPoint max = new UnitPoint(rectangleF.Right, rectangleF.Bottom);
                return max;
            }
        }

        public UnitPoint MinValue
        {
            get
            {
                RectangleF rectangleF = this.GetBoundingRectangle(0);
                UnitPoint min = new UnitPoint(rectangleF.Left, rectangleF.Top);
                return min;
            }
        }
        public GroupParam GroupParam { get; set; } = new GroupParam() { GroupSN=new List<int>() { 0} };

        public void Mirroring(double A, double B, double C)
        {
            UnitPoint startPoint = HitUtil.PointOnCircle(this.Center, this.Radius, HitUtil.DegreesToRadians(this.StartAngle));
            if (B == 0)//水平镜像
            {
                double centerAxis = -C / A;
                startPoint = new UnitPoint(2 * centerAxis - startPoint.X, startPoint.Y);
                this.Center = new UnitPoint(2 * centerAxis - this.Center.X, this.Center.Y);

                if (flyingCutLeadOut != null)
                {
                    flyingCutLeadOut.LeadEndPoint = new UnitPoint(2 * centerAxis - flyingCutLeadOut.LeadEndPoint.X, flyingCutLeadOut.LeadEndPoint.Y);
                    flyingCutLeadOut.PreviousEndPoint = new UnitPoint(2 * centerAxis - flyingCutLeadOut.PreviousEndPoint.X, flyingCutLeadOut.PreviousEndPoint.Y);
                    flyingCutLeadOut.NextNextPoint = new UnitPoint(2 * centerAxis - flyingCutLeadOut.NextNextPoint.X, flyingCutLeadOut.NextNextPoint.Y);
                }
            }
            else if (A == 0)
            {
                double centerAxis = -C / B;
                startPoint = new UnitPoint(startPoint.X, 2 * centerAxis - startPoint.Y);
                this.Center = new UnitPoint(this.Center.X, 2 * centerAxis - this.Center.Y);

                if (flyingCutLeadOut != null)
                {
                    flyingCutLeadOut.LeadEndPoint = new UnitPoint(flyingCutLeadOut.LeadEndPoint.X, 2 * centerAxis - flyingCutLeadOut.LeadEndPoint.Y);
                    flyingCutLeadOut.PreviousEndPoint = new UnitPoint(flyingCutLeadOut.PreviousEndPoint.X, 2 * centerAxis - flyingCutLeadOut.PreviousEndPoint.Y);
                    flyingCutLeadOut.NextNextPoint = new UnitPoint(flyingCutLeadOut.NextNextPoint.X, 2 * centerAxis - flyingCutLeadOut.NextNextPoint.Y);
                }
            }
            else
            {
                startPoint = Utils.Utils.MirrorAnyAlgorithm(startPoint, A, B, C);
                this.Center = Utils.Utils.MirrorAnyAlgorithm(this.Center, A, B, C);

                if (flyingCutLeadOut != null)
                {
                    flyingCutLeadOut.LeadEndPoint = Utils.Utils.MirrorAnyAlgorithm(flyingCutLeadOut.LeadEndPoint, A, B, C);
                    flyingCutLeadOut.PreviousEndPoint = Utils.Utils.MirrorAnyAlgorithm(flyingCutLeadOut.PreviousEndPoint, A, B, C);
                    flyingCutLeadOut.NextNextPoint = Utils.Utils.MirrorAnyAlgorithm(flyingCutLeadOut.NextNextPoint, A, B, C);
                }
            }

            this.StartAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(this.Center, startPoint, 0));
            this.LeadIn.Position = (Math.Abs(this.StartAngle) / 360) % 1;
            this.IsClockwise = !this.IsClockwise;

            this.Update();
        }

        #region 封口、缺口、过切、多圈操作
        public void OverCutting(float pos, bool roundCut)
        {
            if (!roundCut)//清除多圈
            {
                this.OverCutLen = 1;
            }
            this.LeadOut.Pos = pos;
            this.LeadOut.RoundCut = roundCut;
            this.Update();
        }

        public void UpdateOverCutting()
        {
            this.MicroConnParams?.RemoveAll(r => r.Flags == MicroConnectFlags.GapPoint);
            float pos = this.LeadOut.Pos;
            this.LeadOut.Position = this.LeadIn.Position;
            if (Math.Abs(pos) < 0.000001)//封口
            {
                this.LeadOut.Position = this.LeadIn.Position;
            }
            else if (pos < 0)//缺口
            {
                //this.LeadOut.Pos = pos;
                MicroConnectModel microConnectModel = new MicroConnectModel() { Flags = MicroConnectFlags.GapPoint, Size = pos, Position = 0 };
                if (this.MicroConnParams == null) this.MicroConnParams = new List<MicroConnectModel>();
                this.MicroConnParams.Add(microConnectModel);
            }
            else//过切/多圈
            {
                if (this.LeadOut.RoundCut)
                {
                    this.OverCutLen = this.LeadOut.Pos;
                    this.LeadOut.Position = this.LeadIn.Position + (this.LeadOut.Pos % 1) * (this.IsClockwise ? -1 : 1);
                }
                else
                {
                    this.OverCutLen = (float)(this.LeadOut.Pos / this.SizeLength) + 1;
                    this.LeadOut.Position = this.LeadIn.Position + (float)(Math.Abs(this.LeadOut.Pos) / this.SizeLength) * (this.IsClockwise ? -1 : 1);
                }

                //if (this.LeadOut.RoundCut)
                //{
                //    this.OverCutLen = (float)((this.LeadOut.Pos % 1) * this.SizeLength);
                //    this.LeadOut.Position = this.LeadIn.Position + (this.LeadOut.Pos % 1) * (this.IsClockwise ? -1 : 1);
                //}
                //else
                //{
                //    this.OverCutLen = this.LeadOut.Pos;
                //    this.LeadOut.Position = this.LeadIn.Position + (float)(Math.Abs(this.LeadOut.Pos) / this.SizeLength) * (this.IsClockwise ? -1 : 1);
                //}
            }
        }

        #endregion

        public void ReverseDirection()
        {
            this.IsClockwise = !this.IsClockwise;
            this.MicroConnParams?.ForEach(e => e.Position = 1 - e.Position);
            this.Update();
        }

        public void DoSizeChange(double centerX, double centerY, double coffX, double coffY)
        {
            //1.判断X,Y的系数是否相等，相等则为等比压缩，不等则要进行圆与多段贝塞尔曲线之间的简化
            double coff = coffX > coffY ? coffX : coffY;
            this.Radius = (float)(this.Radius * coff);
            this.Center = Utils.Utils.ScaleAlgorithm(this.Center, centerX, centerY, coff, coff);
            this.StartMovePoint = Utils.Utils.ScaleAlgorithm(this.StartMovePoint, centerX, centerY, coff, coff);
            if (flyingCutLeadOut != null)
            {
                flyingCutLeadOut.LeadEndPoint = Utils.Utils.ScaleAlgorithm(flyingCutLeadOut.LeadEndPoint, centerX, centerY, coff, coff);
                flyingCutLeadOut.PreviousEndPoint = Utils.Utils.ScaleAlgorithm(flyingCutLeadOut.PreviousEndPoint, centerX, centerY, coff, coff);
                flyingCutLeadOut.NextNextPoint = Utils.Utils.ScaleAlgorithm(flyingCutLeadOut.NextNextPoint, centerX, centerY, coff, coff);
            }
            this.Update();
        }

        public void DoRotate(UnitPoint rotateCenter, double rotateAngle, bool isClockwise)
        {
            rotateAngle = HitUtil.DegreesToRadians(rotateAngle);
            if (!isClockwise) rotateAngle = -rotateAngle;
            UnitPoint point = HitUtil.PointOnCircle(this.Center, this.Radius, HitUtil.DegreesToRadians(this.StartAngle));
            this.Center = Utils.Utils.RotateAlgorithm(this.Center, rotateCenter, rotateAngle);
            //this.StartMovePoint = Utils.Utils.RotateAlgorithm(this.StartMovePoint, rotateCenter, rotateAngle);
            this.StartAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(this.Center, Utils.Utils.RotateAlgorithm(point, rotateCenter, rotateAngle), 0));
            this.LeadIn.Position = (float)(this.StartAngle / 360);
            if(flyingCutLeadOut != null)
            {
                flyingCutLeadOut.LeadEndPoint = Utils.Utils.RotateAlgorithm(flyingCutLeadOut.LeadEndPoint, rotateCenter, rotateAngle);
                flyingCutLeadOut.PreviousEndPoint = Utils.Utils.RotateAlgorithm(flyingCutLeadOut.PreviousEndPoint, rotateCenter, rotateAngle);
                flyingCutLeadOut.NextNextPoint = Utils.Utils.RotateAlgorithm(flyingCutLeadOut.NextNextPoint, rotateCenter, rotateAngle);
            }
            this.Update();
        }

        #region 引入引出线设置

        private UnitPoint LeadInIntersectPoint;
        private UnitPoint LeadOutIntersectPoint;
        public void DoSetLeadwire(LineInOutParamsModel leadwireModel)
        {
            this.LeadInOutParams = CopyUtil.DeepCopy(leadwireModel);
            this.SetLeadInParam();
            this.SetLeadOutParam();
            //this.UpdateLeadwire();
            this.Update();
        }

        private void SetLeadInParam()
        {
            if (this.LeadIn == null)
            {
                this.LeadIn = new LeadInLine();
            }
            this.LeadIn.LeadType = this.LeadInOutParams.LineInType;
            this.LeadIn.LeadByHole = this.LeadInOutParams.IsAddCircularHole;
            if (this.LeadInOutParams.LinePosition == LinePositions.AutoSelectSuitable)
            {
                this.LeadIn.Position = 0;
            }
            else if(this.LeadInOutParams.LinePosition == LinePositions.FigureTotalLength)
            {
                this.LeadIn.Position = this.LeadInOutParams.FigureTotalLength;
            }
            else
            {

            }
            this.LeadIn.Angle = this.LeadInOutParams.LineInAngle;
            this.LeadIn.Length = this.LeadInOutParams.LineInLength;
            this.LeadIn.ArcRadius = this.LeadInOutParams.LineInRadius;
            this.LeadIn.LeadHoleRadius = this.LeadInOutParams.CircularHoleRadius;
        }

        private void SetLeadOutParam()
        {
            if (this.LeadOut == null)
            {
                this.LeadOut = new LeadOutLine();
            }
            this.LeadOut.LeadType = this.LeadInOutParams.LineOutType;
            this.LeadOut.Angle = this.LeadInOutParams.LineOutAngle;
            this.LeadOut.Length = this.LeadInOutParams.LineOutLength;
            this.LeadOut.ArcRadius = this.LeadInOutParams.LineOutRadius;
            this.LeadOut.Position = this.LeadIn.Position;
        }

        public void UpdateLeadwire()
        {
            if (this.LeadIn == null) return;
            if (this.ArcCompensations != null && this.ArcCompensations.Count > 0)
            {
                MicroConnectHelper.GetLeadLinePositionByCircle(this.ArcCompensations, this.LeadIn.Position, this.LeadOut.Position, out this.LeadInIntersectPoint, out this.LeadOutIntersectPoint);
            }
            else if (this.ArcMicroConns != null && this.ArcMicroConns.Count > 0)
            {
                MicroConnectHelper.GetLeadLinePositionByCircle(this.ArcMicroConns, this.LeadIn.Position, this.LeadOut.Position, out this.LeadInIntersectPoint, out this.LeadOutIntersectPoint);
            }
            else
            {
                this.LeadInIntersectPoint = HitUtil.LineEndPoint(this.Center, this.LeadIn.Position * Math.PI * 2, this.Radius);
                this.LeadOutIntersectPoint = HitUtil.LineEndPoint(this.Center, this.LeadOut.Position * Math.PI * 2, this.Radius);
            }
            this.UpdateLeadInLine(this.LeadInIntersectPoint);
            this.UpdateLeadOutLine(this.LeadOutIntersectPoint);
            //this.UpdateStartMovePointWhenSetLeadwire();
        }

        private void UpdateLeadInLine(UnitPoint intersectPoint)
        {
            if (this.LeadInWireParam == null)
            {
                this.LeadInWireParam = new LeadInParam();
            }
            switch (this.LeadIn.LeadType)
            {
                case LeadLineType.None:
                    this.CalNoneLeadIn();
                    break;
                case LeadLineType.Line:
                    this.CalLineLeadIn(intersectPoint);
                    break;
                case LeadLineType.Arc:
                    this.CalArcLeadIn(intersectPoint);
                    break;
                case LeadLineType.LineArc:
                    this.CalLineArcLeadIn(intersectPoint);
                    break;
            }
        }

        private void UpdateLeadOutLine(UnitPoint intersectPoint)
        {
            if (this.LeadOut == null) return;
            if (this.LeadOutWireParam == null)
            {
                this.LeadOutWireParam = new LeadOutParam();
            }
            switch (this.LeadOut.LeadType)
            {
                case LeadLineType.None:
                    this.CalNoneLeadOut();
                    break;
                case LeadLineType.Line:
                    this.CalLineLeadOut(intersectPoint);
                    break;
                case LeadLineType.Arc:
                    this.CalArcLeadOut(intersectPoint);
                    break;
                case LeadLineType.LineArc:
                    this.CalLineArcLeadOut(intersectPoint);
                    break;
            }
        }

        private void CalNoneLeadIn()
        {
            this.LeadIn.LeadType = LeadLineType.None;

        }

        private void CalLineLeadIn(UnitPoint intersectPoint)
        {
            //double angle = HitUtil.LineAngleR(this.Center, intersectPoint, 0) + HitUtil.DegreesToRadians(90) * (this.IsClockwise ? 1 : -1);
            //angle += this.LeadIn.Angle * (this.IsClockwise ? -1 : 1);
            //UnitPoint startPoint = HitUtil.PointOnCircle(intersectPoint, this.LeadIn.Length, angle);
            //this.LeadInWireParam.StartPoint = startPoint;
            //this.LeadInWireParam.EndPoint = intersectPoint;
            //if (this.LeadIn.LeadByHole)
            //{
            //    this.LeadInWireParam.HoleCenter = this.LeadInWireParam.StartPoint;
            //    this.LeadInWireParam.HoleRadius = this.LeadIn.LeadHoleRadius;
            //}
            //1.求取圆弧起始点处的在切线上的距离为引入线长度的点
            UnitPoint zeroAnglePoint = DrawingOperationHelper.GetRightCircleCenter(intersectPoint, this.Center, this.LeadIn.Length, this.IsClockwise);
            //2.阴切：将零度引线按照与原图相反的方向进行旋转；阳切：将零度引线按照与原图相同的方向进行旋转
            double angle;
            if (this.IsInnerCut)
            {
                angle = this.IsClockwise ? -this.LeadIn.Angle : this.LeadIn.Angle;
            }
            else
            {
                angle = this.IsClockwise ? this.LeadIn.Angle : -this.LeadIn.Angle;
            }
            this.LeadInWireParam.StartPoint = Utils.Utils.RotateAlgorithm(zeroAnglePoint, intersectPoint, angle);
            this.LeadInWireParam.EndPoint = intersectPoint;
            if (this.LeadIn.LeadByHole)
            {
                this.LeadInWireParam.HoleCenter = this.LeadInWireParam.StartPoint;
                this.LeadInWireParam.HoleRadius = this.LeadIn.LeadHoleRadius;
            }
        }

        private void CalArcLeadIn(UnitPoint intersectPoint)
        {
            //double angle = HitUtil.LineAngleR(this.Center, intersectPoint, 0) + HitUtil.DegreesToRadians(90) * (this.IsClockwise ? 1 : -1);
            //angle += this.LeadIn.Angle * (this.IsClockwise ? -1 : 1);
            //UnitPoint startPoint = HitUtil.PointOnCircle(intersectPoint, this.LeadIn.Length, angle);

            //double radius = (this.LeadIn.Length / 2) / Math.Sin(this.LeadIn.Angle);
            //double length = Math.Sqrt(radius * radius - (this.LeadIn.Length / 2) * (this.LeadIn.Length / 2));
            //var centers = DrawingOperationHelper.GetLinePointByVerticalLine(HitUtil.LineMidpoint(intersectPoint, startPoint), startPoint, length);
            //UnitPoint center = centers.Item1;
            //if ((this.LeadIn.Angle < Math.PI / 2 && this.IsClockwise) || (this.LeadIn.Angle > Math.PI / 2 && !this.IsClockwise))
            //{
            //    center = centers.Item2;
            //}
            //float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, startPoint, 0));
            //float endAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, intersectPoint, 0));
            //float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, endAngle, !this.IsClockwise);
            //this.LeadInWireParam.Center = center;
            //this.LeadInWireParam.Radius = (float)radius;
            //this.LeadInWireParam.SweepAngle = sweepAngle;
            //this.LeadInWireParam.StartAngle = startAngle;
            //this.LeadInWireParam.EndAngle = endAngle;

            double radius = (this.LeadIn.Length / 2) / Math.Sin(this.LeadIn.Angle);
            UnitPoint zeroAnglePoint = DrawingOperationHelper.GetRightCircleCenter(intersectPoint, this.Center, this.LeadIn.Length, this.IsClockwise);
            double angle, lineAngle;
            bool clockwise;
            if (this.IsInnerCut)
            {
                clockwise = this.IsClockwise;
                angle = this.IsClockwise ? -this.LeadIn.Angle : this.LeadIn.Angle;
                lineAngle = HitUtil.LineAngleR(intersectPoint, this.Center, 0);
            }
            else
            {
                clockwise = !this.IsClockwise;
                angle = this.IsClockwise ? this.LeadIn.Angle : -this.LeadIn.Angle;
                lineAngle = HitUtil.LineAngleR(this.Center, intersectPoint, 0);
            }
            UnitPoint startPoint = Utils.Utils.RotateAlgorithm(zeroAnglePoint, intersectPoint, angle);
            //求圆心           
            UnitPoint center = HitUtil.LineEndPoint(intersectPoint, lineAngle, radius);

            float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, startPoint, 0));
            float endAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, intersectPoint, 0));
            float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, endAngle, clockwise);
            this.LeadInWireParam.Center = center;
            this.LeadInWireParam.Radius = (float)radius;
            this.LeadInWireParam.SweepAngle = sweepAngle;
            this.LeadInWireParam.StartAngle = startAngle;
            this.LeadInWireParam.EndAngle = endAngle;
            this.LeadInWireParam.StartPoint = startPoint;
            this.LeadInWireParam.EndPoint = intersectPoint;
        }

        private void CalLineArcLeadIn(UnitPoint intersectPoint)
        {
            UnitPoint zeroAnglePoint = DrawingOperationHelper.GetRightCircleCenter(intersectPoint, this.Center, this.LeadIn.Length, this.IsClockwise);
            double angle, lineAngle;
            bool clockwise;
            if (this.IsInnerCut)
            {
                clockwise = this.IsClockwise;
                angle = this.IsClockwise ? -this.LeadIn.Angle : this.LeadIn.Angle;
                lineAngle = HitUtil.LineAngleR(intersectPoint, this.Center, 0);
            }
            else
            {
                clockwise = !this.IsClockwise;
                angle = this.IsClockwise ? this.LeadIn.Angle : -this.LeadIn.Angle;
                lineAngle = HitUtil.LineAngleR(this.Center, intersectPoint, 0);
            }
            //求圆心           
            UnitPoint center = HitUtil.LineEndPoint(intersectPoint, lineAngle, this.LeadIn.ArcRadius);
            UnitPoint startPoint = Utils.Utils.RotateAlgorithm(intersectPoint, center, angle);

            //2.求直线起点          
            this.LeadInWireParam.StartPoint = DrawingOperationHelper.GetRightCircleCenter(startPoint, center, this.LeadIn.Length, clockwise);
            this.LeadInWireParam.EndPoint = startPoint;

            float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, startPoint, 0));
            float endAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, intersectPoint, 0));
            float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, endAngle, clockwise);
            this.LeadInWireParam.Center = center;
            this.LeadInWireParam.Radius = (float)this.LeadIn.ArcRadius;
            this.LeadInWireParam.SweepAngle = sweepAngle;
            this.LeadInWireParam.StartAngle = startAngle;
            this.LeadInWireParam.EndAngle = endAngle;
        }

        private void CalNoneLeadOut()
        {
            this.LeadOut.LeadType = LeadLineType.None;
        }

        private void CalLineLeadOut(UnitPoint intersectPoint)
        {
            //double angle = HitUtil.LineAngleR(this.Center, intersectPoint, 0) + HitUtil.DegreesToRadians(90) * (this.IsClockwise ? -1 : 1);
            //angle += this.LeadOut.Angle * (this.IsClockwise ? 1 : -1);
            //UnitPoint endPoint = HitUtil.PointOnCircle(intersectPoint, this.LeadOut.Length, angle);
            //this.LeadOutWireParam.StartPoint = intersectPoint;
            //this.LeadOutWireParam.EndPoint = endPoint;
            //1.求取圆弧起始点处的在切线上的距离为引入线长度的点
            UnitPoint zeroAnglePoint = DrawingOperationHelper.GetRightCircleCenter(intersectPoint, this.Center, this.LeadOut.Length, !this.IsClockwise);
            //2.阴切：将零度引线按照与原图相同的方向进行旋转；阳切：将零度引线按照与原图相反的方向进行旋转
            double angle;
            if (this.IsInnerCut)
            {
                angle = this.IsClockwise ? this.LeadOut.Angle : -this.LeadOut.Angle;
            }
            else
            {
                angle = this.IsClockwise ? -this.LeadOut.Angle : this.LeadOut.Angle;
            }
            this.LeadOutWireParam.StartPoint = intersectPoint;
            this.LeadOutWireParam.EndPoint = Utils.Utils.RotateAlgorithm(zeroAnglePoint, intersectPoint, angle);
        }

        private void CalArcLeadOut(UnitPoint intersectPoint)
        {
            //double angle = HitUtil.LineAngleR(this.Center, intersectPoint, 0) + HitUtil.DegreesToRadians(90) * (this.IsClockwise ? -1 : 1);
            //angle += this.LeadOut.Angle * (this.IsClockwise ? 1 : -1);
            //UnitPoint endPoint = HitUtil.PointOnCircle(intersectPoint, this.LeadOut.Length, angle);

            //double radius = (this.LeadOut.Length / 2) / Math.Sin(this.LeadOut.Angle);
            //double length = Math.Sqrt(radius * radius - (this.LeadOut.Length / 2) * (this.LeadOut.Length / 2));
            //var centers = DrawingOperationHelper.GetLinePointByVerticalLine(HitUtil.LineMidpoint(endPoint, intersectPoint), intersectPoint, length);
            //UnitPoint center = centers.Item1;
            //if ((this.LeadOut.Angle < Math.PI / 2 && this.IsClockwise) ||
            //   (this.LeadOut.Angle > Math.PI / 2 && !this.IsClockwise))
            //{
            //    center = centers.Item2;
            //}
            //float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, intersectPoint, 0));
            //float endAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, endPoint, 0));
            //float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, endAngle, !this.IsClockwise);
            //this.LeadOutWireParam.Center = center;
            //this.LeadOutWireParam.Radius = (float)radius;
            //this.LeadOutWireParam.SweepAngle = sweepAngle;
            //this.LeadOutWireParam.StartAngle = startAngle;
            //this.LeadOutWireParam.EndAngle = endAngle;
            double radius = (this.LeadOut.Length / 2) / Math.Sin(this.LeadOut.Angle);
            UnitPoint zeroAnglePoint = DrawingOperationHelper.GetRightCircleCenter(intersectPoint, this.Center, this.LeadOut.Length, !this.IsClockwise);
            double angle, lineAngle;
            bool clockwise;
            if (this.IsInnerCut)
            {
                clockwise = this.IsClockwise;
                angle = this.IsClockwise ? this.LeadOut.Angle : -this.LeadOut.Angle;
                lineAngle = HitUtil.LineAngleR(intersectPoint, this.Center, 0);
            }
            else
            {
                clockwise = !this.IsClockwise;
                angle = this.IsClockwise ? -this.LeadOut.Angle : this.LeadOut.Angle;
                lineAngle = HitUtil.LineAngleR(this.Center, intersectPoint, 0);
            }
            UnitPoint endPoint = Utils.Utils.RotateAlgorithm(zeroAnglePoint, intersectPoint, angle);
            //求圆心           
            UnitPoint center = HitUtil.LineEndPoint(intersectPoint, lineAngle, radius);

            float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, intersectPoint, 0));
            float endAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, endPoint, 0));
            float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, endAngle, clockwise);
            this.LeadOutWireParam.Center = center;
            this.LeadOutWireParam.Radius = (float)radius;
            this.LeadOutWireParam.SweepAngle = sweepAngle;
            this.LeadOutWireParam.StartAngle = startAngle;
            this.LeadOutWireParam.EndAngle = endAngle;
            this.LeadOutWireParam.StartPoint = intersectPoint;
            this.LeadOutWireParam.EndPoint = endPoint;
        }

        private void CalLineArcLeadOut(UnitPoint intersectPoint)
        {
            UnitPoint zeroAnglePoint = DrawingOperationHelper.GetRightCircleCenter(intersectPoint, this.Center, this.LeadOut.Length, !this.IsClockwise);
            double angle, lineAngle;
            bool clockwise;
            if (this.IsInnerCut)
            {
                clockwise = this.IsClockwise;
                angle = this.IsClockwise ? this.LeadOut.Angle : -this.LeadOut.Angle;
                lineAngle = HitUtil.LineAngleR(intersectPoint, this.Center, 0);
            }
            else
            {
                clockwise = !this.IsClockwise;
                angle = this.IsClockwise ? -this.LeadOut.Angle : this.LeadOut.Angle;
                lineAngle = HitUtil.LineAngleR(this.Center, intersectPoint, 0);
            }
            //求圆心           
            UnitPoint center = HitUtil.LineEndPoint(intersectPoint, lineAngle, this.LeadOut.ArcRadius);
            UnitPoint endPoint = Utils.Utils.RotateAlgorithm(intersectPoint, center, angle);

            //2.求直线起点          
            this.LeadOutWireParam.StartPoint = endPoint;
            this.LeadOutWireParam.EndPoint = DrawingOperationHelper.GetRightCircleCenter(endPoint, center, this.LeadOut.Length, !clockwise); ;

            float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, intersectPoint, 0));
            float endAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, endPoint, 0));
            float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, endAngle, clockwise);
            this.LeadOutWireParam.Center = center;
            this.LeadOutWireParam.Radius = (float)this.LeadOut.ArcRadius;
            this.LeadOutWireParam.SweepAngle = sweepAngle;
            this.LeadOutWireParam.StartAngle = startAngle;
            this.LeadOutWireParam.EndAngle = endAngle;
        }

        private void UpdateStartMovePointWhenSetLeadwire()
        {
            if (this.LeadIn != null && this.LeadIn.LeadType != LeadLineType.None)
            {
                switch (this.LeadIn.LeadType)
                {
                    case LeadLineType.Line:
                    case LeadLineType.LineArc:
                        this.StartMovePoint = this.LeadInWireParam.StartPoint;
                        break;
                    case LeadLineType.Arc:
                        this.StartMovePoint = DrawingOperationHelper.GetPointOnArcByAngle(this.LeadInWireParam.Center, this.LeadInWireParam.Radius, this.LeadInWireParam.StartAngle);
                        break;
                }
            }
            else if (this.LeadOut != null)
            {
                switch (this.LeadOut.LeadType)
                {
                    case LeadLineType.Line:
                    case LeadLineType.LineArc:
                        this.StartMovePoint = this.LeadOutWireParam.StartPoint;
                        break;
                    case LeadLineType.Arc:
                        this.StartMovePoint = DrawingOperationHelper.GetPointOnArcByAngle(this.LeadOutWireParam.Center, this.LeadOutWireParam.Radius, this.LeadOutWireParam.StartAngle);
                        break;
                }
            }
        }
        private void UpdateStartEndMovePoint()
        {
            //起点
            if (this.LeadIn.LeadType != LeadLineType.None)
            {
                switch (this.LeadIn.LeadType)
                {
                    case LeadLineType.Line:
                    case LeadLineType.LineArc:
                        this.StartMovePoint = this.LeadInWireParam.StartPoint;
                        break;
                    case LeadLineType.Arc:
                        this.StartMovePoint = DrawingOperationHelper.GetPointOnArcByAngle(this.LeadInWireParam.Center, this.LeadInWireParam.Radius, this.LeadInWireParam.StartAngle);
                        break;
                }
            }
            else
            {
                this.StartMovePoint = this.LeadInIntersectPoint;
            }
            //终点
            if (this.LeadOut.LeadType != LeadLineType.None)
            {
                switch (this.LeadIn.LeadType)
                {
                    case LeadLineType.Line:
                    case LeadLineType.LineArc:
                        this.EndMovePoint = this.LeadOutWireParam.EndPoint;
                        break;
                    case LeadLineType.Arc:
                        this.EndMovePoint = DrawingOperationHelper.GetPointOnArcByAngle(this.LeadOutWireParam.Center, this.LeadOutWireParam.Radius, this.LeadOutWireParam.EndAngle);
                        break;
                }
            }
            else
            {
                this.EndMovePoint = this.LeadOutIntersectPoint;
            }
        }
        #endregion

        #endregion

        #region IDrawData
        public List<IDrawLite> GetBasic()
        {
            var items = new List<IDrawLite>();          
            Pen pen = this.IsSelected ? DrawUtils.CustomSelectedPens[this.LayerId] : DrawUtils.CustomPens[this.LayerId];
            if (this.ArcMicroConns != null)
            {
                items.AddRange(this.GetArcBases(pen, this.ArcMicroConns));
            }
            else
            {
                items.Add(new ArcLite
                {
                    DrawPen = pen,
                    Center = this.Center,
                    Radius = this.Radius,
                    StartAngle = this.StartAngle,
                    SweepAngle = this.AngleSweep,
                });
            }
            return items;
        }

        public List<IDrawLite> GetCompensation()
        {
            var items = new List<IDrawLite>();
            if (this.ArcCompensations != null)
            {
                int index = (int)WSX.GlobalData.Model.LayerId.White;
                var pen = this.IsSelected ? DrawUtils.CustomSelectedPens[index] : DrawUtils.CustomPens[index];
                items.AddRange(this.GetArcBases(pen, this.ArcCompensations));
            }
            return items;
        }

        public List<IDrawLite> GetCornerRing()
        {
            return null;
        }

        public List<IDrawLite> GetCoolingPoints()
        {
            var items = new List<IDrawLite>();
            if (this.MicroStartPoints != null)
            {
                Pen pen = (AdditionalInfo.Instance.IsShowNotClosedFigureInRed && !this.IsCloseFigure) ? DrawUtils.CustomPens[0] : Pens.White;
                var part1 = DrawUtils.GetCoolPoints(this.IsSelected, pen, this.MicroStartPoints.Where(e => e.Flags == MicroConnectFlags.CoolingPoint || (e.Flags == MicroConnectFlags.CoolingLeadIn && this.LeadIn.LeadInBreak)).Select(e => e.Point).ToList());
                items.AddRange(part1);
            }
            return items;
        }

        public List<IDrawLite> GetLeadIn()
        {
            var items = new List<IDrawLite>();
            if (this.LeadIn != null)
            {
                Pen pen = this.IsSelected ? DrawUtils.SelectedLeadLinePen : DrawUtils.LeadLinePen;
                if (this.LeadIn.LeadByHole)
                {
                    var arc = new ArcLite
                    {
                        DrawPen = pen,
                        Center = this.LeadInWireParam.HoleCenter,
                        Radius = this.LeadInWireParam.HoleRadius,
                        StartAngle = 0,
                        SweepAngle = 360,
                    };
                    items.Add(arc);
                }

                switch (this.LeadIn.LeadType)
                {
                    case LeadLineType.Line:
                        var line = new LineLite
                        {
                            DrawPen = pen,
                            Points = new List<UnitPoint> { this.LeadInWireParam.StartPoint, this.LeadInWireParam.EndPoint },
                            IsLeadLine = true,
                        };
                        items.Add(line);
                        break;
                    case LeadLineType.Arc:
                        var arc = new ArcLite
                        {
                            DrawPen = pen,
                            Center = this.LeadInWireParam.Center,
                            Radius = this.LeadInWireParam.Radius,
                            StartAngle = this.LeadInWireParam.StartAngle,
                            SweepAngle = this.LeadInWireParam.SweepAngle,
                        };
                        items.Add(arc);
                        break;
                    case LeadLineType.LineArc:
                        var line1 = new LineLite
                        {
                            DrawPen = pen,
                            Points = new List<UnitPoint> { this.LeadInWireParam.StartPoint, this.LeadInWireParam.EndPoint },
                            IsLeadLine = true,
                        };
                        items.Add(line1);
                        var arc1 = new ArcLite
                        {
                            DrawPen = pen,
                            Center = this.LeadInWireParam.Center,
                            Radius = this.LeadInWireParam.Radius,
                            StartAngle = this.LeadInWireParam.StartAngle,
                            SweepAngle = this.LeadInWireParam.SweepAngle,
                        };
                        items.Add(arc1);
                        break;
                }
            }
            return items;
        }

        public List<IDrawLite> GetLeadOut()
        {
            var items = new List<IDrawLite>();
            if (this.LeadOut != null)
            {
                Pen pen = this.IsSelected ? DrawUtils.SelectedLeadLinePen : DrawUtils.LeadLinePen;
                switch (this.LeadOut.LeadType)
                {
                    case LeadLineType.Line:
                        var line = new LineLite
                        {
                            DrawPen = pen,
                            Points = new List<UnitPoint> { this.LeadOutWireParam.StartPoint, this.LeadOutWireParam.EndPoint },
                            IsLeadLine = true,
                        };
                        items.Add(line);
                        break;
                    case LeadLineType.Arc:
                        var arc = new ArcLite
                        {
                            DrawPen = pen,
                            Center = this.LeadOutWireParam.Center,
                            Radius = this.LeadOutWireParam.Radius,
                            StartAngle = this.LeadOutWireParam.StartAngle,
                            SweepAngle = this.LeadOutWireParam.SweepAngle,
                        };
                        items.Add(arc);
                        break;
                    case LeadLineType.LineArc:
                        var arc1 = new ArcLite
                        {
                            DrawPen = pen,
                            Center = this.LeadOutWireParam.Center,
                            Radius = this.LeadOutWireParam.Radius,
                            StartAngle = this.LeadOutWireParam.StartAngle,
                            SweepAngle = this.LeadOutWireParam.SweepAngle,
                        };
                        items.Add(arc1);
                        var line1 = new LineLite
                        {
                            DrawPen = pen,
                            Points = new List<UnitPoint> { this.LeadOutWireParam.StartPoint, this.LeadOutWireParam.EndPoint },
                            IsLeadLine = true,
                        };
                        items.Add(line1);
                        break;
                }
            }
            return items;
        }

        /// <summary>
        /// 获取圆弧飞切的连接线
        /// </summary>
        /// <returns></returns>
        public List<IDrawLite> GetFlyingCutLeadOut()
        {
            var items = new List<IDrawLite>();
            int penIndex = (int)WSX.GlobalData.Model.LayerId.White;
            if (this.FlyingCutLeadOut != null && this.FlyingCutLeadOut.HasLeadLine)
            {
                Pen pen = DrawUtils.CornerRingPens[penIndex]; //this.IsSelected ? DrawUtils.SelectedLeadLinePen : DrawUtils.LeadLinePen;


                if(this.FlyingCutLeadOut != null && this.FlyingCutLeadOut.HasBezierCurve)
                {
                    UnitPoint myStartPoint = DrawingOperationHelper.GetPointOnArcByAngle(Center, Radius, StartAngle);
                    UnitPoint pointA = myStartPoint;
                    UnitPoint pointD = FlyingCutLeadOut.LeadEndPoint;
                    if (this.FlyingCutLeadOut.LeadlineDistance > 0)
                    {
                        pointA = HitUtil.GetLinePointByDistance(myStartPoint, FlyingCutLeadOut.PreviousEndPoint, FlyingCutLeadOut.LeadlineDistance, false);
                        pointD = HitUtil.GetLinePointByDistance(FlyingCutLeadOut.LeadEndPoint, FlyingCutLeadOut.NextNextPoint, FlyingCutLeadOut.LeadlineDistance, false);
                        IDrawLite line = new LineLite
                        {
                            DrawPen = pen,
                            Points = new List<UnitPoint> { myStartPoint, pointA },
                        };
                        items.Add(line);
                    }

                    UnitPoint pointB = HitUtil.GetLinePointByDistance(
                        myStartPoint, FlyingCutLeadOut.PreviousEndPoint, FlyingCutLeadOut.LeadlineDistance + FlyingCutLeadOut.BezierWide, false);
                    UnitPoint pointC = HitUtil.GetLinePointByDistance(
                        FlyingCutLeadOut.LeadEndPoint, FlyingCutLeadOut.NextNextPoint, FlyingCutLeadOut.LeadlineDistance + FlyingCutLeadOut.BezierWide, false);

                    #region 调试时 去掉注释 可显示确定贝塞尔曲线的ABCD点 
                    //Pen aPen = new Pen(Color.Red, 1);
                    //Pen bPen = new Pen(Color.Green, 1);

                    //DotLite da = new DotLite();
                    //da.Point = pointA;
                    //da.Radius = 2;
                    //da.DrawPen = aPen;
                    //items.Add(da);
                    //DotLite db = new DotLite();
                    //db.Point = pointB;
                    //db.Radius = 2;
                    //db.DrawPen = bPen;
                    //items.Add(db);
                    //DotLite dc = new DotLite();
                    //dc.Point = pointC;
                    //dc.Radius = 2;
                    //dc.DrawPen = pen;
                    //items.Add(dc);
                    //DotLite dd = new DotLite();
                    //dd.Point = pointD;
                    //dd.Radius = 2;
                    //dd.DrawPen = pen;
                    //items.Add(dd);
                    #endregion

                    var bezier1 = new BezierLite
                    {
                        DrawPen = pen,
                        Points = new List<UnitPoint>()
                         {
                             pointA,
                             pointB,
                             pointC,
                             pointD
                         },
                    };
                    items.Add(bezier1);

                    if (this.FlyingCutLeadOut.LeadlineDistance > 0)
                    {
                        IDrawLite line = new LineLite
                        {
                            DrawPen = pen,
                            Points = new List<UnitPoint> { pointD, FlyingCutLeadOut.LeadEndPoint },
                        };
                        items.Add(line);
                    }

                    //有贝塞尔曲线 可以return 了
                    return items;
                }



                UnitPoint lineStartPoint = UnitPoint.Empty;
                if (this.FlyingCutLeadOut.HasLeadArc)
                {
                    var arc1 = new ArcLite
                    {
                        DrawPen = pen,
                        Center = this.Center,
                        Radius = this.Radius,
                        StartAngle = this.StartAngle,
                        SweepAngle = this.FlyingCutLeadOut.LeadEndAngle,
                    };
                    items.Add(arc1);

                    lineStartPoint = DrawingOperationHelper.GetPointOnArcByAngle(this.Center, this.Radius, this.StartAngle + this.FlyingCutLeadOut.LeadEndAngle);
                }else
                {
                    lineStartPoint = DrawingOperationHelper.GetPointOnArcByAngle(this.Center, this.Radius, this.StartAngle);
                }

                var line1 = new LineLite
                {
                    DrawPen = pen,
                    Points = new List<UnitPoint> { lineStartPoint, this.FlyingCutLeadOut.LeadEndPoint },
                    IsLeadLine = true,
                };
                items.Add(line1);


            }
            return items;
        }
        #endregion

        #region IDrawData utils
        public List<IDrawLite> GetArcBases(Pen pen, List<ArcBase> arcBases)
        {
            var items = new List<IDrawLite>();
            if (arcBases != null && arcBases.Count > 0)
            {
                arcBases.ForEach(arcBase =>
                {
                    //canvas.DrawArc(canvas, pen, arcBase.Center, arcBase.Radius, arcBase.StartAngle, arcBase.AngleSweep, isShowMachinePath);
                    items.Add(new ArcLite
                    {
                        DrawPen = pen,
                        Center = arcBase.Center,
                        Radius = arcBase.Radius,
                        StartAngle = arcBase.StartAngle,
                        SweepAngle = arcBase.AngleSweep,
                    });
                });
            }
            return items;
        }
        #endregion

        #region Event

        #endregion
    }
}
