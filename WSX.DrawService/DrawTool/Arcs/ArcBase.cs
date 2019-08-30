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
using WSX.DrawService.Utils;
using WSX.GlobalData.Model;

namespace WSX.DrawService.DrawTool.Arcs
{
    public class ArcBase : DrawObjectBase, IDrawObject, IDrawTranslation, IDrawData
    {
        public UnitPoint startPoint = UnitPoint.Empty, midPoint = UnitPoint.Empty, endPoint = UnitPoint.Empty;

        public List<ArcBase> ArcMicroConns;
        public List<ArcBase> ArcCompensations { get; set; }
        public List<MicroUnitPoint> MicroStartPoints { get; set; }
        #region 参数保存绘制圆弧必要参数
        public UnitPoint Center { get; set; }
        public float Radius { get; set; }
        public float StartAngle { get; set; }
        public float AngleSweep { get; set; }

        #endregion
        public float EndAngle
        {
            get
            {
                return this.StartAngle + this.AngleSweep;
            }
        }
        public bool IsClockwise
        {
            get
            {
                return HitUtil.IsClockwiseByCross(this.startPoint, this.midPoint, this.endPoint);
            }
        }
        public float MidAngle
        {
            get
            {
                return this.StartAngle + this.AngleSweep / 2;
            }
        }

        public float OverCutLen { get; set; } = 1;

        #region DrawObjectBase
        public override void InitializeFromModel(UnitPoint unitPoint, ISnapPoint snapPoint)
        {
            this.LayerId = (int)GlobalModel.CurrentLayerId;
            this.OnMouseDown(null, unitPoint, snapPoint, null);
            this.IsSelected = true;
        }
        public override void Copy(DrawObjectBase drawObjectBase)
        {
            ArcBase arcBase = drawObjectBase as ArcBase;
            base.Copy(arcBase);
            this.startPoint = arcBase.startPoint;
            this.midPoint = arcBase.midPoint;
            this.endPoint = arcBase.endPoint;
            this.UpdateArcFrom3Points();
            this.GroupParam = CopyUtil.DeepCopy(arcBase.GroupParam);
            this.IsCompleteDraw = arcBase.IsCompleteDraw;
            this.LeadIn = CopyUtil.DeepCopy(arcBase.LeadIn);
            this.LeadOut = CopyUtil.DeepCopy(arcBase.LeadOut);
            this.LeadInOutParams = CopyUtil.DeepCopy(arcBase.LeadInOutParams);
            this.LeadInWireParam = CopyUtil.DeepCopy(arcBase.LeadInWireParam);
            this.LeadOutWireParam = CopyUtil.DeepCopy(arcBase.LeadOutWireParam);
            this.MicroConnParams = CopyUtil.DeepCopy(arcBase.MicroConnParams);
            this.CompensationParam = CopyUtil.DeepCopy(arcBase.CompensationParam);
            this.IsInnerCut = arcBase.IsInnerCut;
            if (this.IsCompleteDraw)
            {
                this.Update();
            }
        }
        #endregion
        public ArcBase() { }
        public void UpdateArcFrom3Points()
        {
            ArcModelMini arcModel = HitUtil.GetArcParametersFromThreePoints(this.startPoint, this.midPoint, this.endPoint);
            this.Center = arcModel.Center;
            this.Radius = arcModel.Radius;
            this.StartAngle = arcModel.StartAngle; ;
            //this.EndAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(this.Center, this.endPoint, 0));
            this.AngleSweep = 359.99f;
            if (this.EndAngle == this.StartAngle) return;
            this.AngleSweep = arcModel.SweepAngle;
        }

        public void Update()
        {
            this.IsCompleteDraw = true;
            this.startPoint = HitUtil.PointOnCircle(this.Center, this.Radius, HitUtil.DegreesToRadians(this.StartAngle));
            this.midPoint = HitUtil.PointOnCircle(this.Center, this.Radius, HitUtil.DegreesToRadians(this.MidAngle));
            this.endPoint = HitUtil.PointOnCircle(this.Center, this.Radius, HitUtil.DegreesToRadians(this.EndAngle));
            this.UpdateOverCutting();
            this.UpdateByChangeMicroConnect();
            this.UpdateByChangeCompensation();
            this.UpdateLeadwire();
            this.UpdateStartEndMovePoint();
            this.UpdateCoolPointByLeadIn();
        }

        public bool IsEditMode { get; set; }

        public bool IsInnerCut { get; set; } = true;

        #region IDrawObject
        public FigureTypes FigureType { get { return FigureTypes.Arc; } }
        public string Id { get { return "Arc"; } }
        public virtual bool IsCompleteDraw { get; set; }
        public UnitPoint FirstDrawPoint { get { return this.startPoint; } }
        public double SizeLength
        {
            get
            {
                return Math.Abs(2 * Math.PI * this.Radius * this.AngleSweep / 360.0);
            }
        }
        public bool IsCloseFigure
        {
            get
            {
                return false;
            }
        }
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
        public UnitPoint StartMovePoint { get; set; }
        public UnitPoint EndMovePoint { get; set; }       
        public LineInOutParamsModel LeadInOutParams { get; set; } = new LineInOutParamsModel();
        public LeadInParam LeadInWireParam { get; set; }
        public LeadOutParam LeadOutWireParam { get; set; }
        public LeadInLine LeadIn { get; set; } = new LeadInLine();
        public LeadOutLine LeadOut { get; set; } = new LeadOutLine();
        public List<MicroConnectModel> MicroConnParams { get; set; }
        public CompensationModel CompensationParam { get; set; }
        public CornerRingModel CornerRingParam { get; set; }

        public UnitPoint RepeatStartingPoint { get { return UnitPoint.Empty; } }
        public virtual IDrawObject Clone()
        {
            ArcBase arcBase = new ArcBase();
            arcBase.Copy(this);
            return arcBase;
        }
        public virtual void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            int penIndex = this.LayerId;
            if (AdditionalInfo.Instance.IsShowNotClosedFigureInRed)
            {
                penIndex = 0;
            }
            if (IsVisiblity == false) return;

            var items = new List<IDrawLite>();
            items.AddRange(this.GetBasic());
            items.AddRange(this.GetCompensation());
            items.AddRange(this.GetCoolingPoints());
            items.AddRange(this.GetLeadIn());
            items.AddRange(this.GetLeadOut());

            float scale= HitUtil.IsDrawExtraProperties(unitRectangle, this.GetBoundingRectangle(BoundingRectangleType.All));
            bool isDrawExtraProperties = scale < 1250;
            bool isShowMachinePath = AdditionalInfo.Instance.IsShowMachinePath;
            AdditionalInfo.Instance.IsShowMachinePath = AdditionalInfo.Instance.IsShowMachinePath && this.IsCompleteDraw && (this.LayerId != (int)WSX.GlobalData.Model.LayerId.White) && isDrawExtraProperties;
            DrawUtils.Draw(canvas, items,scale);
            AdditionalInfo.Instance.IsShowMachinePath = isShowMachinePath;

            if (this.IsCompleteDraw && isDrawExtraProperties)
            {
                this.DrawNodes(canvas);
                this.ShowBoundRect(canvas, penIndex);
                this.ShowFigureSN(canvas);
                this.ShowStartMovePoint(canvas);
                this.ShowMicroConnectFlag(canvas);
            }        
        }

        private void DrawNodes(ICanvas canvas)
        {
            if (this.IsSelected && this.IsCompleteDraw && canvas.DataModel.DrawingPattern == DrawingPattern.NodeEditPattern && this.GroupParam.GroupSN.Count == 1)
            {
                DrawUtils.DrawNode(canvas, this.startPoint);
                DrawUtils.DrawNode(canvas, this.endPoint);
                DrawUtils.DrawTriangleNode(canvas, midPoint);
                DrawUtils.DrawCircleNode(canvas, this.Center);
            }
        }

        private UnitPoint GetLeadLineIntersectPoint()
        {
            double angle = HitUtil.LineAngleR(this.Center, this.startPoint, 0);
            this.LeadIn.Position = (float)(angle / (Math.PI * 2));
            return HitUtil.PointOnCircle(this.Center, this.Radius, angle);
        }

        private UnitPoint GetLeadLineEndPoint()
        {
            UnitPoint intersectPoint = this.GetLeadLineIntersectPoint();
            float lineAngle = (float)HitUtil.LineAngleR(intersectPoint, this.Center, 0);
            return DrawingOperationHelper.GetLeadLineEndPoint(intersectPoint, lineAngle, this.LeadIn.Angle, this.LeadIn.Length);
        }
        public virtual RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            return this.GetBoundingRectCommon(thresholdWidth);
        }

        protected RectangleF GetBoundingRectCommon(float thresholdWidth)
        {
            RectangleF rectangleF = DrawingOperationHelper.GetArcBounds(this, thresholdWidth);
            if (this.ArcCompensations != null && this.ArcCompensations.Count > 0)
            {
                foreach (var arc in this.ArcCompensations)
                {
                    RectangleF bounds = DrawingOperationHelper.GetArcBounds(arc, thresholdWidth);
                    rectangleF = RectangleF.Union(rectangleF, bounds);
                }
            }
            if (this.LeadIn != null)
            {
                RectangleF rectangleLine = ScreenUtils.GetRectangleF(this.GetLeadLineEndPoint(), this.GetLeadLineIntersectPoint(), thresholdWidth / 2);
                rectangleF = RectangleF.Union(rectangleF, rectangleLine);
            }
            return rectangleF;
        }


        public string GetInfoAsString()
        {
            throw new NotImplementedException();
        }

        public void Move(UnitPoint offset)
        {
            this.startPoint += offset;
            this.midPoint += offset;
            this.endPoint += offset;

            this.UpdateArcFrom3Points();
            this.Update();
        }

        public INodePoint NodePoint(UnitPoint unitPoint)
        {
            float thWidth = UCCanvas.GetThresholdWidth();
            if (HitUtil.PointInPoint(this.startPoint, unitPoint, thWidth))
            {
                return new NodeArcPoint(this, ArcNodeType.StartPoint);
            }
            if (HitUtil.PointInPoint(this.midPoint, unitPoint, thWidth))
            {
                return new NodeArcPoint(this, ArcNodeType.MidPoint);
            }
            if (HitUtil.PointInPoint(this.endPoint, unitPoint, thWidth))
            {
                return new NodeArcPoint(this, ArcNodeType.EndPoint);
            }
            if (HitUtil.PointInPoint(this.Center, unitPoint, thWidth))
            {
                return new NodeArcPoint(this, ArcNodeType.Center);
            }
            return null;
        }

        public bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            float thWidth = UCCanvas.GetThresholdWidth();
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
                    if (HitUtil.IsArcIntersectWithRect(this, rectangle, thWidth))
                    {
                        return true;
                    }
                }
            }
            float thresholdWidth = UCCanvas.GetThresholdWidth() / 2;
            RectangleF rectangleF = this.GetBoundingRectCommon(thresholdWidth);
            return rectangle.Contains(rectangleF);
        }

        public void OnKeyDown(ICanvas canvas, KeyEventArgs e)
        {

        }

        public virtual DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint, MouseEventArgs e)
        {
            return DrawObjectMouseDown.Done;
        }

        public virtual void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {

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
                //1.排除起始点，起始点不能设置微连
                if (HitUtil.PointInPoint(this.startPoint, unitPoint, UCCanvas.GetThresholdWidth())) return;
                if (this.MicroConnParams == null)
                {
                    this.MicroConnParams = new List<MicroConnectModel>();
                }
                MicroConnectModel microConnectModel = new MicroConnectModel() { Flags = MicroConnectFlags.MicroConnect, Size = microConnLen };
                //2.计算位置
                double angle = HitUtil.LineAngleR(this.Center, unitPoint, 0);
                unitPoint = HitUtil.PointOnCircle(this.Center, this.Radius, angle);
                angle = HitUtil.CalAngleSweep(this.StartAngle, HitUtil.RadiansToDegrees(angle), this.IsClockwise);
                microConnectModel.Position = (float)(angle / this.AngleSweep);
                this.MicroConnParams.Add(microConnectModel);
                this.MicroConnParams = this.MicroConnParams.OrderBy(r => r.Position).ToList();
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
                this.ArcMicroConns = MicroConnectHelper.CalMicroconnsByArc(this, this.MicroConnParams, out microStartPoints);
                microStartPoints.ForEach(e => { e.Point.IsBasePoint = true; });
                if (this.MicroStartPoints == null)
                {
                    this.MicroStartPoints = new List<MicroUnitPoint>();
                }
                this.MicroStartPoints.RemoveAll(e => e.Point.IsBasePoint == true);
                this.MicroStartPoints.AddRange(microStartPoints);
            }
        }
        #endregion

        #region 补偿
        public void DoCompensation(CompensationModel param)
        {
            this.CompensationParam = param;
            if (param != null) this.IsInnerCut = this.CompensationParam.Style == CompensationType.AllInner;
            this.Update();
        }
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
                //1.计算补偿
                double r = this.CompensationParam.Style == CompensationType.AllInner ? this.Radius - this.CompensationParam.Size : this.Radius + this.CompensationParam.Size;
                if (r > 0)
                {
                    ArcBase arc = new ArcBase()
                    {
                        Center = this.Center,
                        AngleSweep = this.AngleSweep,
                        StartAngle = this.StartAngle,
                        Radius = (float)r,
                    };
                    arc.startPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.StartAngle));
                    arc.midPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.MidAngle));
                    arc.endPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.EndAngle));
                    if (this.MicroConnParams != null)
                    {
                        List<MicroUnitPoint> microStartPoints;
                        this.ArcCompensations = MicroConnectHelper.CalMicroconnsByArc(arc, this.MicroConnParams, out microStartPoints);
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
            if (unitPoint.IsEmpty)
            {
                //自动添加冷却点，计算冷却点参数
                this.MicroConnParams?.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingPoint);
                //引入点是否需要冷却
                this.LeadIn.LeadInBreak = isLeadIn;
            }
            else
            {
                //1.排除起始点，起始点不能设置微连
                if (HitUtil.PointInPoint(this.startPoint, unitPoint, UCCanvas.GetThresholdWidth())) return;
                if (this.MicroConnParams == null)
                {
                    this.MicroConnParams = new List<MicroConnectModel>();
                }
                MicroConnectModel microConnectModel = new MicroConnectModel() { Flags = MicroConnectFlags.CoolingPoint, Size = 0 };
                //2.计算位置
                double angleLine = HitUtil.LineAngleR(this.Center, unitPoint, 0);
                unitPoint = HitUtil.PointOnCircle(this.Center, this.Radius, angleLine);
                angleLine = HitUtil.CalAngleSweep(this.StartAngle, HitUtil.RadiansToDegrees(angleLine), this.IsClockwise);
                microConnectModel.Position = (float)(angleLine / this.AngleSweep);
                this.MicroConnParams.Add(microConnectModel);
                this.MicroConnParams = this.MicroConnParams.OrderBy(r => r.Position).ToList();
            }
            this.Update();
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

        #region 显示附加信息
        public void ShowBoundRect(ICanvas canvas, int penIndex)
        {
            if (AdditionalInfo.Instance.IsShowBoundRect)
            {
                penIndex = (this.ArcCompensations != null && this.ArcCompensations.Count > 0) ? (int)WSX.GlobalData.Model.LayerId.White : this.LayerId;
                penIndex = (AdditionalInfo.Instance.IsShowNotClosedFigureInRed && !this.IsCloseFigure) ? 0 : penIndex;
                RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.All);
                canvas.DrawBoundRectangle(canvas, DrawUtils.CustomPens[penIndex], new PointF(rectangleF.Location.X, rectangleF.Bottom), rectangleF.Width, rectangleF.Height);
            }
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
            if (AdditionalInfo.Instance.IsShowMicroConnectFlag)
            {
                if (this.MicroStartPoints != null)
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

        #endregion

        #region IDrawTranslation
        public UnitPoint MaxValue
        {
            get
            {
                RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.All);
                UnitPoint max = new UnitPoint(rectangleF.Right, rectangleF.Bottom);
                return max;
            }
        }

        public UnitPoint MinValue
        {
            get
            {
                RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.All);
                UnitPoint min = new UnitPoint(rectangleF.Left, rectangleF.Top);
                return min;
            }
        }
        public GroupParam GroupParam { get; set; } = new GroupParam() { GroupSN = new List<int>() { 0 } };

        public void Mirroring(double A, double B, double C)
        {
            if (B == 0)//水平镜像
            {
                double centerAxis = -C / A;
                this.startPoint = new UnitPoint(2 * centerAxis - this.startPoint.X, this.startPoint.Y);
                this.midPoint = new UnitPoint(2 * centerAxis - this.midPoint.X, this.midPoint.Y);
                this.endPoint = new UnitPoint(2 * centerAxis - this.endPoint.X, this.endPoint.Y);
                this.Center = new UnitPoint(2 * centerAxis - this.Center.X, this.Center.Y);
                //this.StartMovePoint = new UnitPoint(2 * centerAxis - this.StartMovePoint.X, this.StartMovePoint.Y);
            }
            else if (A == 0)
            {
                double centerAxis = -C / B;
                this.startPoint = new UnitPoint(this.startPoint.X, 2 * centerAxis - this.startPoint.Y);
                this.midPoint = new UnitPoint(this.midPoint.X, 2 * centerAxis - this.midPoint.Y);
                this.endPoint = new UnitPoint(this.endPoint.X, 2 * centerAxis - this.endPoint.Y);
                this.Center = new UnitPoint(this.Center.X, 2 * centerAxis - this.Center.Y);
                //this.StartMovePoint = new UnitPoint(this.StartMovePoint.X, 2 * centerAxis - this.StartMovePoint.Y);
            }
            else
            {
                this.startPoint = Utils.Utils.MirrorAnyAlgorithm(this.startPoint, A, B, C);
                this.midPoint = Utils.Utils.MirrorAnyAlgorithm(this.midPoint, A, B, C);
                this.endPoint = Utils.Utils.MirrorAnyAlgorithm(this.endPoint, A, B, C);
                this.Center = Utils.Utils.MirrorAnyAlgorithm(this.Center, A, B, C);
                //this.StartMovePoint = Utils.Utils.MirrorAnyAlgorithm(this.StartMovePoint, A, B, C);
            }
            this.UpdateArcFrom3Points();
            this.Update();
        }

        #region 封口、缺口、过切、多圈操作
        public void OverCutting(float pos, bool roundCut)
        {
            if (this.LeadOut == null) this.LeadOut = new LeadOutLine();
            this.LeadOut.Pos = pos;
            //this.UpdateOverCutting();
            this.Update();
        }

        public void UpdateOverCutting()
        {
            if (this.MicroConnParams != null)
            {
                this.MicroConnParams.RemoveAll(r => r.Flags == MicroConnectFlags.GapPoint);
            }
            float pos = this.LeadOut.Pos;
            if (Math.Abs(pos) < 0.000001)//封口
            {
                //if (this.MicroConnParams != null)
                //{
                //    this.MicroConnParams.RemoveAll(r => r.Flags == MicroConnectFlags.GapPoint);
                //}
            }
            else if (pos < 0)//缺口
            {
                this.LeadOut.Pos = pos;
                //float percent = (float)(1 + pos / this.SizeLength);
                MicroConnectModel microConnectModel = new MicroConnectModel() { Flags = MicroConnectFlags.GapPoint, Size = pos, Position = 1 };
                if (this.MicroConnParams == null) this.MicroConnParams = new List<MicroConnectModel>();
                //this.MicroConnParams.RemoveAll(r => r.Flags == MicroConnectFlags.GapPoint);
                this.MicroConnParams.Add(microConnectModel);
            }
            else//过切/多圈
            {
                //if (this.LeadOut.RoundCut)
                //{
                //    this.OverCutLen = this.LeadOut.Pos;
                //}
                //else
                //{
                //    this.OverCutLen = (float)(this.LeadOut.Pos / this.SizeLength);
                //}
            }
        }

        #endregion

        public void ReverseDirection()
        {
            UnitPoint temp = this.startPoint;
            this.startPoint = this.endPoint;
            this.endPoint = temp;
            this.UpdateArcFrom3Points();
            if (this.MicroConnParams != null)
            {
                MicroConnectHelper.ReverseMicroConnParams(this.MicroConnParams, false);
            }
            
            this.Update();
        }

        public void DoSizeChange(double centerX, double centerY, double coffX, double coffY)
        {
            double coff = coffX > coffY ? coffX : coffY;
            this.Radius = (float)(this.Radius * coff);
            this.Center = Utils.Utils.ScaleAlgorithm(this.Center, centerX, centerY, coff, coff);
            //this.StartMovePoint = Utils.Utils.ScaleAlgorithm(this.StartMovePoint, centerX, centerY, coff, coff);
            this.Update();
        }

        public void DoRotate(UnitPoint rotateCenter, double rotateAngle, bool isClockwise)
        {
            rotateAngle = HitUtil.DegreesToRadians(rotateAngle);
            if (!isClockwise) rotateAngle = -rotateAngle;
            this.startPoint = Utils.Utils.RotateAlgorithm(this.startPoint, rotateCenter, rotateAngle);
            this.midPoint = Utils.Utils.RotateAlgorithm(this.midPoint, rotateCenter, rotateAngle);
            this.endPoint = Utils.Utils.RotateAlgorithm(this.endPoint, rotateCenter, rotateAngle);
            this.UpdateArcFrom3Points();
            //this.StartMovePoint = Utils.Utils.RotateAlgorithm(this.StartMovePoint, rotateCenter, rotateAngle);
            this.Update();
        }

        #region 设置引入引出线
        private UnitPoint LeadInIntersectPoint = UnitPoint.Empty;
        private UnitPoint LeadOutIntersectPoint = UnitPoint.Empty;
        public void DoSetLeadwire(LineInOutParamsModel leadwireModel)
        {
            this.LeadInOutParams = CopyUtil.DeepCopy(leadwireModel);
            this.SetLeadInParam();
            this.SetLeadOutParam();
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
            this.LeadIn.Position = 0;
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
        }

        public void UpdateLeadwire()
        {
            if (this.LeadIn == null) return;
            
            if (this.ArcCompensations != null && this.ArcCompensations.Count > 0)
            {
                LeadInIntersectPoint = this.ArcCompensations[0].startPoint;
                LeadOutIntersectPoint = this.ArcCompensations[this.ArcCompensations.Count - 1].endPoint;
            }
            else if (this.ArcMicroConns != null && this.ArcMicroConns.Count > 0)
            {
                LeadInIntersectPoint = this.ArcMicroConns[0].startPoint;
                LeadOutIntersectPoint = this.ArcMicroConns[this.ArcMicroConns.Count - 1].endPoint;
            }
            else
            {
                LeadInIntersectPoint = this.startPoint;
                LeadOutIntersectPoint = this.endPoint;
            }
            this.UpdateLeadInLine(LeadInIntersectPoint);
            this.UpdateLeadOutLine(LeadOutIntersectPoint);
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
            #region 算法1
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
            #endregion
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
            #region 算法1
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
            #endregion
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
            if (this.LeadIn != null)
            {
                switch (this.LeadIn.LeadType)
                {
                    case LeadLineType.None:
                        this.StartMovePoint = this.startPoint;
                        break;
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
                this.StartMovePoint = this.startPoint;
            }
        }
        private void UpdateStartEndMovePoint()
        {
            //起点
            if(this.LeadIn.LeadType !=  LeadLineType.None)
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
            if(this.LeadOut.LeadType != LeadLineType.None)
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
            int penIndex = (AdditionalInfo.Instance.IsShowNotClosedFigureInRed && !this.IsCloseFigure) ? 0 : this.LayerId;
            bool isShowMachinePath = AdditionalInfo.Instance.IsShowMachinePath && this.IsCompleteDraw&&(this.LayerId!=(int)WSX.GlobalData.Model.LayerId.White);
            Pen pen = this.IsSelected ? DrawUtils.CustomSelectedPens[penIndex] : DrawUtils.CustomPens[penIndex];
            if (this.ArcMicroConns != null)
            {
                //this.DrawArcBases(canvas, pen, unitRectangle, this.ArcMicroConns, isShowMachinePath);
                items.AddRange(this.GetArcBases(pen, this.ArcMicroConns, isShowMachinePath));
            }
            else
            {
                //canvas.DrawArc(canvas, pen, this.Center, this.Radius, this.StartAngle, this.AngleSweep, isShowMachinePath);
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
                int penIndex = (AdditionalInfo.Instance.IsShowNotClosedFigureInRed && !this.IsCloseFigure) ? 0 : (int)WSX.GlobalData.Model.LayerId.White;
                var pen = this.IsSelected ? DrawUtils.CustomSelectedPens[penIndex] : DrawUtils.CustomPens[penIndex];
                bool isShowMachinePath = AdditionalInfo.Instance.IsShowMachinePath && this.IsCompleteDraw;
                items.AddRange(this.GetArcBases(pen, this.ArcCompensations, isShowMachinePath));
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
        #endregion

        #region IDrawData utils
        public List<IDrawLite> GetArcBases(Pen pen, List<ArcBase> arcBases, bool isShowMachinePath)
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
    }
}
