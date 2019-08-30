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
using WSX.GlobalData;
using WSX.GlobalData.Messenger;
using WSX.GlobalData.Model;

namespace WSX.DrawService.DrawTool.MultiSegmentLine
{
    public class MultiSegmentLineBase : DrawObjectBase, IDrawObject, IDrawTranslation, IDrawData
    {
        private RectangleF unitRect;

        public List<UnitPointBulge> Points { get; set; }
        public List<UnitPointBulge> MicroConnPoints { get; set; }
        public List<MicroUnitPoint> MicroStartPoints { get; set; }
        public List<UnitPointBulge> CompensationPoints { get; set; }
        public Dictionary<int, List<UnitPoint>> CornerRingPoints { get; set; }

        public const float ROUNDANGLE_PRECISION = 0.001f;//倒圆角精度：在图形重叠时需要判断是否可以进行倒角，当精度过高会把切点识别为两个点，导致倒角异常。
        /// <summary>
        /// 贝塞尔曲线参数 飞切
        /// </summary>
        public BezierParamModel BezierParam { set; get; }

        public float OverCutLen { get; set; } = 1;
        public int PointCount
        {
            get
            {
                if (this.Points != null)
                {
                    return this.Points.Count;
                }
                return 0;
            }
        }

        protected virtual MultiSegementLineCurrentPoint CurrentPoint { get; set; }
        public bool IsEditMode { get; set; }

        #region DrawObjectBase
        public override void InitializeFromModel(UnitPoint unitPoint, ISnapPoint snapPoint)
        {
            this.LayerId = (int)GlobalModel.CurrentLayerId;
            this.OnMouseDown(null, unitPoint, snapPoint, null);
            this.IsSelected = false;
        }
        #endregion

        #region IDrawObject
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
        public FigureTypes FigureType { get { return FigureTypes.LwPolyline; } }
        public virtual string Id { get { return "MultiLineBase"; } }
        public virtual bool IsCompleteDraw { get; set; }
        public virtual bool Clockwise
        {
            get
            {
                if (!this.IsCloseFigure)
                {
                    return false;
                }
                return HitUtil.IsClockwiseByCross(this.Points[0].Point, this.Points[1].Point, this.Points[2].Point);
            }
            set { }
        }
        public bool IsReverseDirection { get; set; }
        public UnitPoint FirstDrawPoint { get { return this.Points[0].Point; } }
        public double SizeLength
        {
            get
            {
                //TODO:计算多段线的总长度
                return GetLength(this.Points, this.IsCloseFigure);
            }
        }
        public virtual bool IsCloseFigure { get; set; }
        public bool IsInnerCut { get; set; }
        public virtual bool IsSelected { get; set; }
        public UnitPoint StartMovePoint { get; set; }
        public UnitPoint EndMovePoint { get; set; }
        public float PathStartParm { get; set; }
        public List<MicroConnectModel> MicroConnParams { get; set; }
        public LineInOutParamsModel LeadInOutParams { get; set; } = new LineInOutParamsModel();
        public LeadInParam LeadInWireParam { get; set; }
        public LeadOutParam LeadOutWireParam { get; set; }
        public LeadInLine LeadIn { get; set; } = new LeadInLine();
        public LeadOutLine LeadOut { get; set; } = new LeadOutLine();
        public virtual UnitPoint RepeatStartingPoint { get; }
        public CompensationModel CompensationParam { get; set; }
        public CornerRingModel CornerRingParam { get; set; }
        public MultiSegmentLineBase()
        {
        }

        public void Update()
        {
            this.IsCompleteDraw = true;
            this.UpdateOverCutting();
            this.UpdateByChangeMicroConnect();
            this.UpdateByChangeCompensation();
            this.UpdateLeadwire();
            this.UpdateStartEndMovePoint();
            this.UpdateByChangeCornerRing();
            this.UpdateCoolPointByLeadIn();
        }
        public virtual IDrawObject Clone()
        {
            MultiSegmentLineBase multiSegmentLineBase = new MultiSegmentLineBase();
            multiSegmentLineBase.Copy(this);
            return multiSegmentLineBase;
        }

        public override void Copy(DrawObjectBase drawObjectBase)
        {
            MultiSegmentLineBase multiSegmentLineBase = drawObjectBase as MultiSegmentLineBase;
            base.Copy(multiSegmentLineBase);
            this.Points = new List<UnitPointBulge>();
            if (multiSegmentLineBase.Points != null)
            {
                for (int i = 0; i < multiSegmentLineBase.PointCount; i++)
                {
                    this.Points.Add(new UnitPointBulge()
                    {
                        Point = multiSegmentLineBase.Points[i].Point,
                        Bulge = multiSegmentLineBase.Points[i].Bulge,
                        Dotted = multiSegmentLineBase.Points[i].Dotted,
                    });
                }
            }
            this.GroupParam = CopyUtil.DeepCopy(multiSegmentLineBase.GroupParam);
            this.StartMovePoint = multiSegmentLineBase.StartMovePoint;
            this.EndMovePoint = multiSegmentLineBase.EndMovePoint;
            this.IsCompleteDraw = multiSegmentLineBase.IsCompleteDraw;
            this.IsCloseFigure = multiSegmentLineBase.IsCloseFigure;
            this.LeadIn = CopyUtil.DeepCopy(multiSegmentLineBase.LeadIn);
            this.LeadOut = CopyUtil.DeepCopy(multiSegmentLineBase.LeadOut);
            this.LeadInOutParams = CopyUtil.DeepCopy(multiSegmentLineBase.LeadInOutParams);
            this.LeadInWireParam = CopyUtil.DeepCopy(multiSegmentLineBase.LeadInWireParam);
            this.LeadOutWireParam = CopyUtil.DeepCopy(multiSegmentLineBase.LeadOutWireParam);
            this.MicroConnParams = CopyUtil.DeepCopy(multiSegmentLineBase.MicroConnParams);
            this.CompensationParam = CopyUtil.DeepCopy(multiSegmentLineBase.CompensationParam);
            this.CornerRingParam = CopyUtil.DeepCopy(multiSegmentLineBase.CornerRingParam);
            this.BezierParam = CopyUtil.DeepCopy(multiSegmentLineBase.BezierParam);
            this.IsInnerCut = multiSegmentLineBase.IsInnerCut;
            if (this.IsCompleteDraw)
            {
                this.Update();
            }
        }
        private bool drawTest = false;
        private float radius;
        private List<UnitPoint> centers = new List<UnitPoint>();
        public virtual void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            if (IsVisiblity == false) return;
            int penIndex = this.LayerId;
            List<int> indexs = this.DrawingIndexs;
            if (AdditionalInfo.Instance.IsShowNotClosedFigureInRed && !this.IsCloseFigure)
            {
                penIndex = 0;
            }
            if (drawTest)
            {
                for (int i = 0; i < this.centers.Count; i++)
                {
                    //canvas.DrawArc(canvas, DrawUtils.CustomPens[2], this.centers[i], radius, 0, 360);
                    canvas.DrawDot(canvas, DrawUtils.CustomPens[2], this.centers[i], 0.2f);
                }
            }
            this.unitRect = unitRectangle;
            var items = new List<IDrawLite>();
            items.AddRange(this.GetBasic());
            items.AddRange(this.GetCompensation());
            items.AddRange(this.GetCornerRing());
            items.AddRange(this.GetCoolingPoints());
            items.AddRange(this.GetLeadIn());
            items.AddRange(this.GetLeadOut());

            //飞切 jiang
            items.AddRange(this.GetBezierLite());

            float scale = HitUtil.IsDrawExtraProperties(unitRectangle, this.GetBoundingRectangle(BoundingRectangleType.All));
            bool isDrawExtraProperties = scale < 1250;
            bool isShowMachinePath = AdditionalInfo.Instance.IsShowMachinePath;
            AdditionalInfo.Instance.IsShowMachinePath = AdditionalInfo.Instance.IsShowMachinePath && this.IsCompleteDraw && (this.LayerId != (int)WSX.GlobalData.Model.LayerId.White) && isDrawExtraProperties;
            DrawUtils.Draw(canvas, items, scale);
            AdditionalInfo.Instance.IsShowMachinePath = isShowMachinePath;

            if (this.IsCompleteDraw && isDrawExtraProperties)
            {
                this.DrawNodes(canvas);
                this.ShowBoundRect(canvas, penIndex);
                this.ShowFigureSN(canvas);
                this.ShowStartMovePoint(canvas);
                this.ShowMicroConnectFlag(canvas);
                this.ShowMultiLoopFlag(canvas);
            }
        }

        protected void DrawBulgeArc(ICanvas canvas, RectangleF unitRectangle, Pen pen, UnitPointBulge p1, UnitPointBulge p2)
        {
            UnitPoint center = BulgeHelper.GetCenterByBulgeAndTwoPoints(p1.Point, p2.Point, p1.Bulge);
            double radius = HitUtil.Distance(center, p1.Point);
            float startAngle = (float)HitUtil.LineAngleR(center, p1.Point, 0);
            float endAngle = (float)HitUtil.LineAngleR(center, p2.Point, 0);
            bool clockwise = p1.Bulge >= 0 ? false : true;
            startAngle = (float)HitUtil.RadiansToDegrees(startAngle);
            endAngle = (float)HitUtil.RadiansToDegrees(endAngle);
            float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, endAngle, clockwise);
            canvas.DrawArc(canvas, pen, center, (float)radius, startAngle, sweepAngle, 0);
        }

        protected void DrawNodes(ICanvas canvas)
        {
            if (this.IsSelected && this.IsCompleteDraw && canvas.DataModel.DrawingPattern == DrawingPattern.NodeEditPattern && this.GroupParam.GroupSN.Count == 1)
            {
                for (int i = 0; i < this.PointCount - 1; i++)
                {
                    if (!double.IsNaN(this.Points[i].Bulge))
                    {
                        ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(this.Points[i].Point, this.Points[i + 1].Point, (float)this.Points[i].Bulge);
                        float midAngle = (float)HitUtil.RadiansToDegrees((float)arc.StartAngle) + arc.SweepAngle / 2;
                        UnitPoint midPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(midAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(midAngle)) + arc.Center.Y);
                        DrawUtils.DrawTriangleNode(canvas, midPoint);
                    }
                    DrawUtils.DrawNode(canvas, this.Points[i].Point);
                }
                if (!double.IsNaN(this.Points[this.PointCount - 1].Bulge))
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(this.Points[this.PointCount - 1].Point, this.Points[0].Point, (float)this.Points[this.PointCount - 1].Bulge);
                    float midAngle = (float)HitUtil.RadiansToDegrees((float)arc.StartAngle) + arc.SweepAngle / 2;
                    UnitPoint midPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(midAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(midAngle)) + arc.Center.Y);
                    DrawUtils.DrawTriangleNode(canvas, midPoint);
                }
                DrawUtils.DrawNode(canvas, this.Points[this.PointCount - 1].Point);
            }
        }

        /// <summary>
        /// 构造绘图索引集合
        /// </summary>
        /// <param name="maxIndex">最大索引</param>
        /// <returns>绘图索引集合</returns>
        public List<int> DrawingIndexs
        {
            get
            {
                int maxIndex = 0;
                if (this.MicroConnPoints != null && this.MicroConnPoints.Count > 0)
                {
                    maxIndex = this.MicroConnPoints.Count;
                }
                else
                {
                    maxIndex = this.Points.Count;
                }
                List<int> result = new List<int>();
                for (int i = 0; i < maxIndex; i++)
                {
                    result.Add(i);
                }
                if (this.IsCloseFigure)
                {
                    result.Add(0);
                }
                return result;
            }
        }

        /// <summary>
        /// 绘制环切图形
        /// </summary>
        private void DrawCornerRing(ICanvas canvas, RectangleF unitRectangle, int penIndex)
        {
            if (this.CornerRingPoints == null || this.CornerRingPoints.Keys.Count <= 0) return;
            Pen pen = this.IsSelected ? DrawUtils.CustomSelectedPens[penIndex] : DrawUtils.CustomPens[penIndex];
            if (this.CornerRingParam.IsScanline)
            {
                pen = DrawUtils.CornerRingPens[penIndex];
            }
            foreach (int key in this.CornerRingPoints.Keys)
            {
                canvas.DrawBeziers(canvas, pen, this.CornerRingPoints[key]);
            }
        }

        public virtual RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            float thWidth = UCCanvas.GetThresholdWidth();
            List<UnitPoint> boundPoints = new List<UnitPoint>();
            float maxX = (float)this.Points.Max<UnitPointBulge>(x => x.Point.X);
            float maxY = (float)this.Points.Max<UnitPointBulge>(x => x.Point.Y);
            float minX = (float)this.Points.Min<UnitPointBulge>(x => x.Point.X);
            float minY = (float)this.Points.Min<UnitPointBulge>(x => x.Point.Y);
            if (this.LeadIn != null)
            {
                //UnitPoint leadLineEndPoint = this.LeadInWireParam.StartPoint;
                //maxX = maxX > leadLineEndPoint.X ? maxX : (float)leadLineEndPoint.X;
                //maxY = maxY > leadLineEndPoint.Y ? maxY : (float)leadLineEndPoint.Y;
                //minX = minX < leadLineEndPoint.X ? minX : (float)leadLineEndPoint.X;
                //minY = minY < leadLineEndPoint.Y ? minY : (float)leadLineEndPoint.Y;
            }
            for (int i = 0; i < this.PointCount; i++)
            {
                if (!double.IsNaN(this.Points[i].Bulge))
                {
                    boundPoints.AddRange(this.GetArcBoundPoints(thWidth, i));
                }
            }
            if (boundPoints.Count > 0)
            {
                float maxX2 = (float)boundPoints.Max<UnitPoint>(x => x.X);
                float maxY2 = (float)boundPoints.Max<UnitPoint>(x => x.Y);
                float minX2 = (float)boundPoints.Min<UnitPoint>(x => x.X);
                float minY2 = (float)boundPoints.Min<UnitPoint>(x => x.Y);
                maxX = maxX > maxX2 ? maxX : maxX2;
                maxY = maxY > maxY2 ? maxY : maxY2;
                minX = minX < minX2 ? minX : minX2;
                minY = minY < minY2 ? minY : minY2;
            }
            if (this.CompensationPoints != null)
            {
                RectangleF rect = DrawingOperationHelper.GetBoundsPathGeometry(this.CompensationPoints, true);
                float maxX2 = (rect.X + rect.Width);
                float maxY2 = (rect.Y + rect.Height);
                float minX2 = rect.X;
                float minY2 = rect.Y;
                maxX = maxX > maxX2 ? maxX : maxX2;
                maxY = maxY > maxY2 ? maxY : maxY2;
                minX = minX < minX2 ? minX : minX2;
                minY = minY < minY2 ? minY : minY2;
            }
            if (CornerRingPoints != null)
            {
                foreach (var beziers in CornerRingPoints.Values)
                {
                    float maxX2 = (float)beziers.Max(x => x.X);
                    float maxY2 = (float)beziers.Max(x => x.Y);
                    float minX2 = (float)beziers.Min(x => x.X);
                    float minY2 = (float)beziers.Min(x => x.Y);
                    maxX = maxX > maxX2 ? maxX : maxX2;
                    maxY = maxY > maxY2 ? maxY : maxY2;
                    minX = minX < minX2 ? minX : minX2;
                    minY = minY < minY2 ? minY : minY2;
                }
            }

            return ScreenUtils.GetRectangleF(new UnitPoint(minX, minY), new UnitPoint(maxX, maxY), thWidth / 2);
        }

        private List<UnitPoint> GetArcBoundPoints(float thresholdWidth, int index)
        {
            int index2 = index + 1 > this.PointCount - 1 ? 0 : index + 1;
            UnitPoint midPoint = BulgeHelper.GetBulgeMidPoint(this.Points[index].Point, this.Points[index2].Point, this.Points[index].Bulge);
            ArcModelMini arcModel = HitUtil.GetArcParametersFromThreePoints(this.Points[index].Point, midPoint, this.Points[index2].Point);
            float r = arcModel.Radius + thresholdWidth / 2;
            List<UnitPoint> points = new List<UnitPoint>();
            float angle1 = arcModel.StartAngle;
            if (arcModel.Clockwise)
            {
                float tempAngle = angle1 % 90;
                for (float i = arcModel.SweepAngle; i <= 0; i += tempAngle)
                {
                    if (tempAngle != 0)
                    {
                        tempAngle = angle1 % 90;
                    }
                    else
                    {
                        tempAngle = 90;
                    }
                    if (tempAngle <= -i)
                    {
                        angle1 = (angle1 - tempAngle) % 360;
                        UnitPoint point = new UnitPoint(arcModel.Radius * Math.Cos(HitUtil.DegreesToRadians(angle1)) + arcModel.Center.X, arcModel.Radius * Math.Sin(HitUtil.DegreesToRadians(angle1)) + arcModel.Center.Y);
                        points.Add(point);
                    }
                }
            }
            else
            {
                float tempAngle = 90 - angle1 % 90;
                for (float i = arcModel.SweepAngle; i >= 0; i -= tempAngle)
                {
                    tempAngle = 90 - angle1 % 90;
                    if (tempAngle <= i)
                    {
                        angle1 = (angle1 + tempAngle) % 360;
                        UnitPoint point = new UnitPoint(arcModel.Radius * Math.Cos(HitUtil.DegreesToRadians(angle1)) + arcModel.Center.X, arcModel.Radius * Math.Sin(HitUtil.DegreesToRadians(angle1)) + arcModel.Center.Y);
                        points.Add(point);
                    }
                }
            }
            return points;
        }

        public string GetInfoAsString()
        {
            throw new NotImplementedException();
        }

        public void Move(UnitPoint offset)
        {
            for (int i = 0; i < this.Points.Count; i++)
            {
                this.Points[i] = new UnitPointBulge()
                {
                    Point = new UnitPoint(this.Points[i].Point.X + offset.X, this.Points[i].Point.Y + offset.Y),
                    Bulge = this.Points[i].Bulge,
                    Dotted = this.Points[i].Dotted
                };
            }
            this.StartMovePoint = new UnitPoint(this.StartMovePoint.X + offset.X, this.StartMovePoint.Y + offset.Y);
            if (this.BezierParam != null)
            {
                BezierParam.ConnectStartPoint = new UnitPoint(BezierParam.ConnectStartPoint.X + offset.X, BezierParam.ConnectStartPoint.Y + offset.Y);
                BezierParam.ConnectEndPoint = new UnitPoint(BezierParam.ConnectEndPoint.X + offset.X, BezierParam.ConnectEndPoint.Y + offset.Y);
            }
            this.Update();
        }
        public INodePoint NodePoint(UnitPoint unitPoint)
        {
            float thWidth = UCCanvas.GetThresholdWidth();
            for (int i = 0; i < this.PointCount; i++)
            {
                if (HitUtil.PointInPoint(this.Points[i].Point, unitPoint, thWidth))
                {
                    INodePoint nodePoint = new NodeMultisegment(this, MultiSegementNodeType.CommonNode, i);
                    return nodePoint;
                }
                if (!double.IsNaN(this.Points[i].Bulge))
                {
                    int index = i + 1;
                    if (i + 1 > this.PointCount - 1) index = 0;
                    UnitPoint midPoint = BulgeHelper.GetBulgeMidPoint(this.Points[i].Point, this.Points[index].Point, this.Points[i].Bulge);
                    if (HitUtil.PointInPoint(midPoint, unitPoint, thWidth))
                    {
                        INodePoint nodePoint = new NodeMultisegment(this, MultiSegementNodeType.ArcNode, i);
                        return nodePoint;
                    }
                }
            }
            return null;
        }

        public virtual bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            if (anyPoint)//包含图形的任一点即可
            {
                List<int> indexs = this.DrawingIndexs;
                int index, nextIndex;
                if (this.MicroConnPoints != null && this.MicroConnPoints.Count > 0)
                {
                    for (int i = 0; i < indexs.Count - 1; i++)
                    {
                        index = indexs[i];
                        nextIndex = indexs[i + 1];
                        if (!this.MicroConnPoints[i].HasMicroConn)
                        {
                            if (double.IsNaN(this.MicroConnPoints[index].Bulge))
                            {
                                if (HitUtil.LineIntersectWithRect(this.MicroConnPoints[index].Point, this.MicroConnPoints[nextIndex].Point, rectangle))
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                UnitPoint midPoint = BulgeHelper.GetBulgeMidPoint(this.MicroConnPoints[index].Point, this.MicroConnPoints[nextIndex].Point, this.MicroConnPoints[index].Bulge);
                                ArcModelMini arcModel = HitUtil.GetArcParametersFromThreePoints(this.MicroConnPoints[index].Point, midPoint, this.MicroConnPoints[nextIndex].Point);
                                bool result = this.RectangleIntersectWithArc(rectangle, arcModel);
                                if (result) return result;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < indexs.Count - 1; i++)
                    {
                        index = indexs[i];
                        nextIndex = indexs[i + 1];
                        if (double.IsNaN(this.Points[index].Bulge))
                        {
                            if (HitUtil.LineIntersectWithRect(this.Points[index].Point, this.Points[nextIndex].Point, rectangle))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            UnitPoint midPoint = BulgeHelper.GetBulgeMidPoint(this.Points[index].Point, this.Points[nextIndex].Point, this.Points[index].Bulge);
                            ArcModelMini arcModel = HitUtil.GetArcParametersFromThreePoints(this.Points[index].Point, midPoint, this.Points[nextIndex].Point);
                            bool result = this.RectangleIntersectWithArc(rectangle, arcModel);
                            if (result) return result;
                        }
                    }
                }
            }
            RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.SelectRange);
            return rectangle.Contains(rectangleF);
        }

        private bool RectangleIntersectWithArc(RectangleF rectangle, ArcModelMini arcModel)
        {
            bool result = false;
            UnitPoint p1 = new UnitPoint(rectangle.Left, rectangle.Top);
            UnitPoint p2 = new UnitPoint(rectangle.Left, rectangle.Bottom);
            float thWidth = UCCanvas.GetThresholdWidth();
            if (HitUtil.CircleIntersectWithLine(arcModel.Center, arcModel.Radius, p1, p2))
            {
                result = HitUtil.IsLineOnArc(p1, p2, arcModel.Center, arcModel.Radius, arcModel.StartAngle, arcModel.SweepAngle, arcModel.Clockwise, thWidth);
                if (result) return result;
            }
            p1 = new UnitPoint(rectangle.Left, rectangle.Bottom);
            p2 = new UnitPoint(rectangle.Right, rectangle.Bottom);
            if (HitUtil.CircleIntersectWithLine(arcModel.Center, arcModel.Radius, p1, p2))
            {
                result = HitUtil.IsLineOnArc(p1, p2, arcModel.Center, arcModel.Radius, arcModel.StartAngle, arcModel.SweepAngle, arcModel.Clockwise, thWidth);
                if (result) return result;
            }
            p1 = new UnitPoint(rectangle.Left, rectangle.Top);
            p2 = new UnitPoint(rectangle.Right, rectangle.Top);
            if (HitUtil.CircleIntersectWithLine(arcModel.Center, arcModel.Radius, p1, p2))
            {
                result = HitUtil.IsLineOnArc(p1, p2, arcModel.Center, arcModel.Radius, arcModel.StartAngle, arcModel.SweepAngle, arcModel.Clockwise, thWidth);
                if (result) return result;
            }
            p1 = new UnitPoint(rectangle.Right, rectangle.Top);
            p2 = new UnitPoint(rectangle.Right, rectangle.Bottom);
            if (HitUtil.CircleIntersectWithLine(arcModel.Center, arcModel.Radius, p1, p2))
            {
                result = HitUtil.IsLineOnArc(p1, p2, arcModel.Center, arcModel.Radius, arcModel.StartAngle, arcModel.SweepAngle, arcModel.Clockwise, thWidth);
                if (result) return result;
            }
            return false;
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
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            List<int> indexs = this.DrawingIndexs;
            int index, nextIndex;
            if (this.MicroConnPoints != null && this.MicroConnPoints.Count > 0)
            {
                for (int i = 0; i < indexs.Count - 1; i++)
                {
                    index = indexs[i];
                    nextIndex = indexs[i + 1];
                    if (!this.MicroConnPoints[i].HasMicroConn)
                    {
                        if (double.IsNaN(this.MicroConnPoints[index].Bulge))
                        {
                            if (HitUtil.IsPointInLine(this.MicroConnPoints[index].Point, this.MicroConnPoints[nextIndex].Point, unitPoint, thresholdWidth))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            UnitPoint midPoint = BulgeHelper.GetBulgeMidPoint(this.MicroConnPoints[index].Point, this.MicroConnPoints[nextIndex].Point, this.MicroConnPoints[index].Bulge);
                            ArcModelMini arcModel = HitUtil.GetArcParametersFromThreePoints(this.MicroConnPoints[index].Point, midPoint, this.MicroConnPoints[nextIndex].Point);
                            bool result = HitUtil.IsPointOnArc(unitPoint, thresholdWidth, arcModel);
                            if (result) return result;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < indexs.Count - 1; i++)
                {
                    index = indexs[i];
                    nextIndex = indexs[i + 1];
                    if (double.IsNaN(this.Points[index].Bulge))
                    {
                        if (HitUtil.IsPointInLine(this.Points[index].Point, this.Points[nextIndex].Point, unitPoint, thresholdWidth))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        UnitPoint midPoint = BulgeHelper.GetBulgeMidPoint(this.Points[index].Point, this.Points[nextIndex].Point, this.Points[index].Bulge);
                        ArcModelMini arcModel = HitUtil.GetArcParametersFromThreePoints(this.Points[index].Point, midPoint, this.Points[nextIndex].Point);
                        bool result = HitUtil.IsPointOnArc(unitPoint, thresholdWidth, arcModel);
                        if (result) return result;
                    }
                }
            }
            return false;
        }

        public void ShowBoundRect(ICanvas canvas, int penIndex)
        {
            if (AdditionalInfo.Instance.IsShowBoundRect && !this.IsCloseFigure)
            {
                penIndex = (this.CompensationPoints != null && this.CompensationPoints.Count > 0) ? (int)WSX.GlobalData.Model.LayerId.White : this.LayerId;
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

        private void ShowMultiLoopFlag(ICanvas canvas)
        {
            if (this.OverCutLen >= 2)
            {
                canvas.DrawMultiLoopFlag(canvas, Brushes.PeachPuff, this.LeadInIntersectPoint, ((int)this.OverCutLen).ToString());
            }
        }

        public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint unitPoint, List<IDrawObject> drawObjects, Type[] runningSnapTypes, Type userSnapType)
        {
            return null;
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
                MicroConnectModel microConnectModel = new MicroConnectModel() { Flags = MicroConnectFlags.MicroConnect };
                microConnectModel.Size = microConnLen;
                //1.计算当前点所在线段数   
                int segment = 0;
                if (this.Points.Count > 2)
                {
                    segment = DrawingOperationHelper.GetPointInLineIndex(this, unitPoint);
                }
                if (segment < 0) return;
                int nextIndex = (segment + 1 >= this.PointCount && this.IsCloseFigure) ? 0 : segment + 1;
                //2.计算位置
                if (double.IsNaN(this.Points[segment].Bulge))
                {
                    double partLen = HitUtil.Distance(this.Points[segment].Point, unitPoint);
                    double allLen = HitUtil.Distance(this.Points[segment].Point, this.Points[nextIndex].Point);
                    microConnectModel.Position = (float)(segment + partLen / allLen);
                }
                else
                {
                    ArcModelMini arcModelMini = DrawingOperationHelper.GetArcParametersFromBulge(this.Points[segment].Point, this.Points[nextIndex].Point, (float)this.Points[segment].Bulge);
                    microConnectModel.Position = segment + DrawingOperationHelper.GetPercentInArcByPoint(arcModelMini, unitPoint);
                }
                this.MicroConnParams.Add(microConnectModel);
                this.MicroConnParams = this.MicroConnParams.OrderBy(r => r.Position).ToList();
            }
            this.Update();
        }
        public void SetAutoMicroConnect(AutoMicroConParam param)
        {
            var micros = MicroConnectHelper.CalAutoMicroConnsParams(this.Points, this.SizeLength, this.IsCloseFigure, param);
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
        public void UpdateByChangeMicroConnect()
        {
            if (this.MicroConnParams == null || this.MicroConnParams.Count == 0)
            {
                //清除微连
                if (this.MicroConnPoints != null)
                {
                    this.MicroConnPoints.Clear();
                    this.MicroConnPoints = null;
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
                this.MicroConnPoints = MicroConnectHelper.CalMicroconnsByMultiSegLine(this.Points, this.IsCloseFigure, this.MicroConnParams, out microStartPoints);
                if (microStartPoints != null)
                {
                    microStartPoints.ForEach(e => { e.Point.IsBasePoint = true; });
                    if (this.MicroStartPoints == null)
                    {
                        this.MicroStartPoints = new List<MicroUnitPoint>();
                    }
                    this.MicroStartPoints.RemoveAll(e => e.Point.IsBasePoint == true);
                    this.MicroStartPoints.RemoveAll(e => e.Flags == MicroConnectFlags.LeadInPoint || e.Flags == MicroConnectFlags.LeadOutPoint);
                    this.MicroStartPoints.AddRange(microStartPoints);
                }
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
        private bool isOutside = true;
        /// <summary>
        /// 更新补偿
        /// </summary>
        public void UpdateByChangeCompensation()
        {
            if (this.CompensationParam == null)
            {
                //清除补偿
                if (this.CompensationPoints != null)
                {
                    this.CompensationPoints.Clear();
                    this.CompensationPoints = null;
                }
                if (this.MicroStartPoints != null)
                {
                    this.MicroStartPoints.RemoveAll(e => e.Point.IsBasePoint == false);
                }
            }
            else
            {
                this.CompensationParam.Style = this.IsInnerCut ? CompensationType.AllInner : CompensationType.AllOuter;
                this.isOutside = true;
                var points1 = CompensationHelper.CalCompensationPoints(this.Points, this.IsCloseFigure, this.CompensationParam, this.IsReverseDirection, isOutside);
                var points2 = CompensationHelper.CalCompensationPoints(this.Points, this.IsCloseFigure, this.CompensationParam, this.IsReverseDirection, !isOutside);

                #region 根据内外膜筛选补偿点
                if (this.IsCloseFigure)
                {
                    #region 封闭图形，根据点所在的区域内外来区分内外膜
                    if (points1.Count >= 3 && points2.Count >= 3)
                    {
                        bool isInner = true;
                        for (int i = 0; i < 3; i++)
                        {
                            bool inside1 = DrawingOperationHelper.IsPointInPathGeometry(points1[i].Point, this.Points);
                            bool inside2 = DrawingOperationHelper.IsPointInPathGeometry(points2[i].Point, this.Points);
                            if (inside1 != inside2)
                            {
                                isInner = inside1;
                                break;
                            }
                            else
                            {
                                inside1 = DrawingOperationHelper.IsPointInPathGeometry(points1[i].Point, this.Points);
                                inside2 = DrawingOperationHelper.IsPointInPathGeometry(points2[i].Point, this.Points);
                            }
                        }
                        if ((this.CompensationParam.Style == CompensationType.Auto ||
                            (this.CompensationParam.Style == CompensationType.AllOuter)))
                        {
                            this.CompensationPoints = isInner ? points2 : points1;
                            isOutside = isInner ? !isOutside : isOutside;
                        }
                        else
                        {
                            this.CompensationPoints = isInner ? points1 : points2;
                            isOutside = isInner ? isOutside : !isOutside;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 不封闭图形，根据边框的区域大小来区分内外膜
                    var bounds1 = DrawingOperationHelper.GetBoundsPathGeometry(points1, this.IsCloseFigure);
                    var bounds2 = DrawingOperationHelper.GetBoundsPathGeometry(points2, this.IsCloseFigure);

                    double area1 = bounds1.Width + bounds1.Height;
                    double area2 = bounds2.Width + bounds2.Height;
                    if ((area1 <= area2 && this.CompensationParam.Style == CompensationType.AllInner) ||
                        (area1 > area2 && this.CompensationParam.Style != CompensationType.AllInner))
                    {
                        this.CompensationPoints = points1;
                    }
                    else
                    {
                        this.CompensationPoints = points2;
                        isOutside = !isOutside;
                    }
                    #endregion
                }
                #endregion

                if (this.MicroConnParams != null && this.MicroConnParams.Count > 0)
                {
                    List<MicroUnitPoint> microStartPoints;
                    this.CompensationPoints = MicroConnectHelper.CalMicroconnsByMultiSegLine(this.CompensationPoints, this.IsCloseFigure, this.MicroConnParams, out microStartPoints, true, this.CompensationParam.Size, isOutside, this.Points);
                    if (microStartPoints != null)
                    {
                        microStartPoints.ForEach(e => { e.Point.IsBasePoint = false; });
                        if (this.MicroStartPoints == null)
                        {
                            this.MicroStartPoints = new List<MicroUnitPoint>();
                        }
                        this.MicroStartPoints.RemoveAll(e => e.Point.IsBasePoint == false);
                        this.MicroStartPoints.RemoveAll(e => e.Flags == MicroConnectFlags.LeadInPoint || e.Flags == MicroConnectFlags.LeadOutPoint);
                        this.MicroStartPoints.AddRange(microStartPoints);
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
            if (this.CornerRingParam == null)
            {
                //清除环切
                if (this.CornerRingPoints != null)
                {
                    this.CornerRingPoints.Clear();
                    this.CornerRingPoints = null;
                }
            }
            else
            {
                if (this.CompensationPoints != null)
                {
                    this.CornerRingPoints = CalCornerRingPoints(this.CompensationPoints, false);
                }
                else if (this.MicroConnPoints != null)
                {
                    this.CornerRingPoints = CalCornerRingPoints(this.MicroConnPoints, false);
                }
                else
                {
                    this.CornerRingPoints = CalCornerRingPoints(this.Points, true);
                }

            }
        }
        /// <summary>
        /// 计算环切点的bezier曲线，返回key，和对应的曲线点，key为所在的段的索引
        /// </summary>
        /// <param name="points"></param>
        /// <param name="isBasePoint">是否原数据点</param>
        /// <returns></returns>
        private Dictionary<int, List<UnitPoint>> CalCornerRingPoints(List<UnitPointBulge> points, bool isBasePoint)
        {
            if (points == null || points.Count <= 2) return null;
            var rets = new Dictionary<int, List<UnitPoint>>();
            double maxAngle = HitUtil.RadiansToDegrees(this.CornerRingParam.MaxAngle);
            for (int i = 0; i < points.Count; i++)
            {
                if (!this.IsCloseFigure && (i == 0 || i == points.Count - 1)) continue;
                int indexPer = (i - 1 < 0) ? points.Count - 1 : i - 1;//前一段
                int indexCur = i;//当前段
                int indexNext = (i + 1 > points.Count - 1) ? 0 : i + 1;//后一段
                if (points[indexPer].HasMicroConn || points[indexCur].HasMicroConn) continue;

                UnitPointBulge p1 = points[indexPer];
                UnitPointBulge p2 = points[indexCur];
                UnitPointBulge p3 = points[indexNext];
                if (this.LeadInIntersectPoint != null && HitUtil.PointInPoint(p2.Point, this.LeadInIntersectPoint, ROUNDANGLE_PRECISION)) continue;

                double angle = double.NaN;
                double len1 = 0;
                double len2 = 0;
                UnitPoint point1, point3;

                #region 计算最短边长
                if (!double.IsNaN(p1.Bulge) && !double.IsNaN(p2.Bulge))
                {
                    //圆弧到圆弧
                    ArcModelMini arc1 = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                    ArcModelMini arc2 = DrawingOperationHelper.GetArcParametersFromBulge(p2.Point, p3.Point, (float)p2.Bulge);
                    var temps1 = DrawingOperationHelper.GetLinePointByVerticalLine(p2.Point, arc1.Center, 10);
                    var temps2 = DrawingOperationHelper.GetLinePointByVerticalLine(p2.Point, arc2.Center, 10);
                    point1 = !arc1.Clockwise ? temps1.Item1 : temps1.Item2;
                    point3 = arc2.Clockwise ? temps2.Item1 : temps2.Item2;
                    if (!isBasePoint)
                    {
                        UnitPointBulge originP1 = this.Points[((int)p2.Position - 1 < 0) ? this.Points.Count - 1 : (int)p2.Position - 1];
                        UnitPointBulge originP2 = this.Points[(int)p2.Position];
                        UnitPointBulge originP3 = this.Points[((int)p2.Position + 1 > this.Points.Count - 1) ? 0 : (int)p2.Position + 1];
                        ArcModelMini arcOrigin1 = DrawingOperationHelper.GetArcParametersFromBulge(originP1.Point, originP2.Point, (float)originP1.Bulge);
                        ArcModelMini arcOrigin2 = DrawingOperationHelper.GetArcParametersFromBulge(originP2.Point, originP3.Point, (float)originP2.Bulge);
                        len1 = Math.Abs(arcOrigin1.SweepAngle) / 180 * Math.PI * arcOrigin1.Radius;
                        len2 = Math.Abs(arcOrigin2.SweepAngle) / 180 * Math.PI * arcOrigin2.Radius;
                    }
                    else
                    {
                        len1 = Math.Abs(arc1.SweepAngle) / 180 * Math.PI * arc1.Radius;
                        len2 = Math.Abs(arc2.SweepAngle) / 180 * Math.PI * arc2.Radius;
                    }
                }
                else if (double.IsNaN(p1.Bulge) && !double.IsNaN(p2.Bulge))
                {
                    //直线到圆弧
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p2.Point, p3.Point, (float)p2.Bulge);
                    var temps = DrawingOperationHelper.GetLinePointByVerticalLine(p2.Point, arc.Center, 10);
                    point1 = p1.Point;
                    point3 = arc.Clockwise ? temps.Item1 : temps.Item2;
                    if (!isBasePoint)
                    {
                        UnitPointBulge originP1 = this.Points[((int)p2.Position - 1 < 0) ? this.Points.Count - 1 : (int)p2.Position - 1];
                        UnitPointBulge originP2 = this.Points[(int)p2.Position];
                        UnitPointBulge originP3 = this.Points[((int)p2.Position + 1 > this.Points.Count - 1) ? 0 : (int)p2.Position + 1];
                        ArcModelMini arcOrigin = DrawingOperationHelper.GetArcParametersFromBulge(originP2.Point, originP3.Point, (float)originP2.Bulge);
                        len1 = HitUtil.Distance(originP1.Point, originP2.Point);
                        len2 = (Math.Abs(arcOrigin.SweepAngle) / 180 * Math.PI * arcOrigin.Radius);
                    }
                    else
                    {
                        len1 = HitUtil.Distance(point1, p2.Point);
                        len2 = Math.Abs(arc.SweepAngle) / 180 * Math.PI * arc.Radius;
                    }
                }
                else if (!double.IsNaN(p1.Bulge) && double.IsNaN(p2.Bulge))
                {
                    //圆弧到直线
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                    var temps = DrawingOperationHelper.GetLinePointByVerticalLine(p2.Point, arc.Center, 10);
                    point1 = !arc.Clockwise ? temps.Item1 : temps.Item2;
                    point3 = p3.Point;
                    if (!isBasePoint)
                    {
                        UnitPointBulge originP1 = this.Points[((int)p2.Position - 1 < 0) ? this.Points.Count - 1 : (int)p2.Position - 1];
                        UnitPointBulge originP2 = this.Points[(int)p2.Position];
                        UnitPointBulge originP3 = this.Points[((int)p2.Position + 1 > this.Points.Count - 1) ? 0 : (int)p2.Position + 1];
                        ArcModelMini arcOrigin = DrawingOperationHelper.GetArcParametersFromBulge(originP1.Point, originP2.Point, (float)originP1.Bulge);
                        len1 = (Math.Abs(arcOrigin.SweepAngle) / 180 * Math.PI * arcOrigin.Radius);
                        len2 = HitUtil.Distance(originP2.Point, originP3.Point);
                    }
                    else
                    {
                        len1 = (Math.Abs(arc.SweepAngle) / 180 * Math.PI * arc.Radius);
                        len2 = HitUtil.Distance(p2.Point, point3);
                    }
                }
                else
                {
                    //直线到直线
                    point1 = p1.Point;
                    point3 = p3.Point;
                    if (!isBasePoint)
                    {
                        UnitPointBulge originP1 = this.Points[((int)p2.Position - 1 < 0) ? this.Points.Count - 1 : (int)p2.Position - 1];
                        UnitPointBulge originP2 = this.Points[(int)p2.Position];
                        UnitPointBulge originP3 = this.Points[((int)p2.Position + 1 > this.Points.Count - 1) ? 0 : (int)p2.Position + 1];

                        len1 = HitUtil.Distance(originP1.Point, originP2.Point);
                        len2 = HitUtil.Distance(originP2.Point, originP3.Point);
                    }
                    else
                    {
                        len1 = HitUtil.Distance(p1.Point, p2.Point);
                        len2 = HitUtil.Distance(p2.Point, p3.Point);
                    }
                }
                #endregion

                #region 计算夹角角度
                if (this.CornerRingParam.Style == CornerRingType.All)
                {
                    angle = DrawingOperationHelper.Angle(point1, p2.Point, point3);
                }
                else
                {
                    bool isInner = (this.CornerRingParam.Style == CornerRingType.Inner ||
                                   (this.CornerRingParam.Style == CornerRingType.Auto && !isBasePoint &&
                                    this.CompensationParam != null && this.CompensationParam.Style == CompensationType.AllInner));
                    if (this.IsCloseFigure)
                    {
                        UnitPoint temp = HitUtil.GetLinePointByDistance(p2.Point, point1, 0.1, false);
                        bool isInside = DrawingOperationHelper.IsPointInPathGeometry(temp, points, true);
                        isInside = isInner ? isInside : !isInside;
                        if (isInside)
                        {
                            angle = DrawingOperationHelper.Angle(point1, p2.Point, point3);
                        }
                    }
                    else
                    {
                        bool clockwise = this.IsReverseDirection ? HitUtil.IsClockwiseByCross(point3, p2.Point, point1) : HitUtil.IsClockwiseByCross(point1, p2.Point, point3);
                        clockwise = isInner ? !clockwise : clockwise;
                        if (clockwise)
                        {
                            angle = DrawingOperationHelper.Angle(point1, p2.Point, point3);
                        }
                    }
                }
                #endregion

                if (Math.Round(angle, 2) <= maxAngle &&
                    Math.Round(len1, 2) >= this.CornerRingParam.MinLen &&
                    Math.Round(len2, 2) >= this.CornerRingParam.MinLen)
                {
                    UnitPoint intersecte1 = HitUtil.GetLinePointByDistance(p2.Point, point1, this.CornerRingParam.Size, false);
                    UnitPoint intersecte2 = HitUtil.GetLinePointByDistance(p2.Point, point3, this.CornerRingParam.Size, false);
                    List<UnitPoint> pointBezs = new List<UnitPoint>();
                    pointBezs.Add(p2.Point);
                    pointBezs.Add(intersecte1);
                    pointBezs.Add(intersecte2);
                    pointBezs.Add(p2.Point);
                    rets.Add(indexCur, pointBezs);
                }
            }
            return rets;
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
                bool isbase = !(this.CompensationPoints != null && this.CompensationPoints.Count > 0);
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
            if (this.MicroConnParams == null)
            {
                this.MicroConnParams = new List<MicroConnectModel>();
            }
            if (unitPoint.IsEmpty)
            {
                //自动添加冷却点，计算冷却点参数
                this.MicroConnParams?.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingPoint);
                if (isCorner)
                {
                    var coolParams = MicroConnectHelper.CalAutoCoolPointParams(this.Points, this.IsCloseFigure, maxAngle, this.LeadIn.Position);
                    this.MicroConnParams.AddRange(coolParams);
                }
                //引入点是否需要冷却
                this.LeadIn.LeadInBreak = isLeadIn;
            }
            else
            {
                MicroConnectModel microConnectModel = new MicroConnectModel() { Flags = MicroConnectFlags.CoolingPoint, Size = 0 };
                //1.计算当前点所在线段数   
                int segment = 0;
                if (this.Points.Count > 2)
                {
                    segment = DrawingOperationHelper.GetPointInLineIndex(this, unitPoint);
                }
                if (segment < 0) return;
                int nextIndex = (segment + 1 >= this.PointCount && this.IsCloseFigure) ? 0 : segment + 1;
                //2.计算位置
                if (double.IsNaN(this.Points[segment].Bulge))
                {
                    double partLen = HitUtil.Distance(this.Points[segment].Point, unitPoint);
                    double allLen = HitUtil.Distance(this.Points[segment].Point, this.Points[nextIndex].Point);
                    microConnectModel.Position = (float)(segment + partLen / allLen);
                }
                else
                {
                    ArcModelMini arcModelMini = DrawingOperationHelper.GetArcParametersFromBulge(this.Points[segment].Point, this.Points[nextIndex].Point, (float)this.Points[segment].Bulge);
                    microConnectModel.Position = segment + DrawingOperationHelper.GetPercentInArcByPoint(arcModelMini, unitPoint);
                }
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

        #endregion

        #region IDrawData

        public List<IDrawLite> GetBasic()
        {
            var points = this.MicroConnPoints == null ? this.Points : this.MicroConnPoints;
            if (points == null || points.Count <= 0) return null;
            int penIndex = (AdditionalInfo.Instance.IsShowNotClosedFigureInRed && !this.IsCloseFigure) ? 0 : this.LayerId;
            return this.GetBasicCore(this.unitRect, points, penIndex);
        }

        public List<IDrawLite> GetCompensation()
        {
            int penIndex = (AdditionalInfo.Instance.IsShowNotClosedFigureInRed && !this.IsCloseFigure) ? 0 : (int)WSX.GlobalData.Model.LayerId.White;
            return this.GetBasicCore(this.unitRect, this.CompensationPoints, penIndex);
        }

        public List<IDrawLite> GetCornerRing()
        {
            var items = new List<IDrawLite>();
            if (this.CornerRingPoints == null || this.CornerRingPoints.Keys.Count <= 0) return items;
            int penIndex = (this.CompensationPoints != null && this.CompensationPoints.Count > 0) ? (int)WSX.GlobalData.Model.LayerId.White : this.LayerId;
            penIndex = (AdditionalInfo.Instance.IsShowNotClosedFigureInRed && !this.IsCloseFigure) ? 0 : penIndex;

            Pen pen = this.IsSelected ? DrawUtils.CustomSelectedPens[penIndex] : DrawUtils.CustomPens[penIndex];
            if (this.CornerRingParam.IsScanline)
            {
                pen = DrawUtils.CornerRingPens[penIndex];
            }
            foreach (int key in this.CornerRingPoints.Keys)
            {
                var bezier = new BezierLite
                {
                    DrawPen = pen,
                    Points = this.CornerRingPoints[key],
                };
                items.Add(bezier);
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
        #endregion

        #region IDrawData util
        private IDrawLite GetBulgeArc(RectangleF unitRectangle, Pen pen, UnitPointBulge p1, UnitPointBulge p2, bool isShowMachinePath = false)
        {
            UnitPoint center = BulgeHelper.GetCenterByBulgeAndTwoPoints(p1.Point, p2.Point, p1.Bulge);
            double radius = HitUtil.Distance(center, p1.Point);
            float startAngle = (float)HitUtil.LineAngleR(center, p1.Point, 0);
            float endAngle = (float)HitUtil.LineAngleR(center, p2.Point, 0);
            bool clockwise = p1.Bulge >= 0 ? false : true;
            startAngle = (float)HitUtil.RadiansToDegrees(startAngle);
            endAngle = (float)HitUtil.RadiansToDegrees(endAngle);
            float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, endAngle, clockwise);
            return new ArcLite
            {
                DrawPen = pen,
                Center = center,
                Radius = (float)radius,
                StartAngle = startAngle,
                SweepAngle = sweepAngle,
            };
        }

        private List<IDrawLite> GetBasicCore(RectangleF unitRectangle, List<UnitPointBulge> points, int penIndex)
        {
            var items = new List<IDrawLite>();
            if (points == null || points.Count <= 0) return items;
            Pen pen = this.IsSelected ? DrawUtils.CustomSelectedPens[penIndex] : DrawUtils.CustomPens[penIndex];
            Pen dottedPen = DrawUtils.DottedPens[penIndex];//虚线

            for (int i = 0; i < points.Count - 1; i++)
            {
                if (points[i].HasMicroConn) continue;
                if (double.IsNaN(points[i].Bulge))
                {
                    IDrawLite line = new LineLite
                    {
                        DrawPen = points[i].Dotted ? dottedPen : pen, //  DrawPen = pen,
                        Points = new List<UnitPoint> { points[i].Point, points[i + 1].Point },
                    };
                    items.Add(line);
                }
                else
                {
                    items.Add(this.GetBulgeArc(this.unitRect, pen, points[i], points[i + 1], AdditionalInfo.Instance.IsShowMachinePath && (this.LayerId != (int)WSX.GlobalData.Model.LayerId.White)));
                }
            }
            if (this.IsCloseFigure && !points[points.Count - 1].HasMicroConn)
            {
                if (double.IsNaN(points[points.Count - 1].Bulge))
                {
                    IDrawLite line = new LineLite
                    {
                        DrawPen = points[points.Count - 1].Dotted ? dottedPen : pen, //  DrawPen = pen,
                        Points = new List<UnitPoint> { points[points.Count - 1].Point, points[0].Point },
                    };
                    items.Add(line);
                }
                else
                {
                    items.Add(this.GetBulgeArc(this.unitRect, pen, points[points.Count - 1], points[0], AdditionalInfo.Instance.IsShowMachinePath && (this.LayerId != (int)WSX.GlobalData.Model.LayerId.White)));
                }
            }
            return items;
        }

        //测试贝塞尔曲线
        private List<IDrawLite> GetBezierLite()
        {

            var items = new List<IDrawLite>();

            if (this.IsCloseFigure)
                return items;

            Dictionary<int, List<UnitPoint>> bezierPoints = this.CalFlyConnectBezier();

            if (bezierPoints == null || bezierPoints.Keys.Count <= 0)
                return items;

            int penIndex = (this.CompensationPoints != null && this.CompensationPoints.Count > 0) ? (int)WSX.GlobalData.Model.LayerId.White : this.LayerId;
            penIndex = (AdditionalInfo.Instance.IsShowNotClosedFigureInRed && !this.IsCloseFigure) ? 0 : penIndex;

            Pen pen = this.IsSelected ? DrawUtils.CustomSelectedPens[penIndex] : DrawUtils.CustomPens[penIndex];
            pen = DrawUtils.CornerRingPens[penIndex];

            foreach (int key in bezierPoints.Keys)
            {
                if (BezierParam.LeadlineDistance > 0)
                {
                    IDrawLite line = new LineLite
                    {
                        DrawPen = pen,
                        Points = new List<UnitPoint> { Points[Points.Count - 1].Point, bezierPoints[key][0] },
                    };
                    items.Add(line);
                }

                var bezier = new BezierLite
                {
                    DrawPen = pen,
                    Points = bezierPoints[key],
                };
                items.Add(bezier);

                if (BezierParam.LeadlineDistance > 0)
                {
                    IDrawLite line = new LineLite
                    {
                        DrawPen = pen,
                        Points = new List<UnitPoint> { bezierPoints[key][3], BezierParam.ConnectStartPoint },
                    };
                    items.Add(line);
                }
            }
            return items;
        }

        /// <summary>
        /// 计算飞切的光滑连接曲线(贝塞尔曲线部分)
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, List<UnitPoint>> CalFlyConnectBezier()
        {
            if (BezierParam == null || this.Points == null || this.Points.Count < 2)
                return null;
            Dictionary<int, List<UnitPoint>> flyConnectBezier = new Dictionary<int, List<UnitPoint>>();

            UnitPoint startPoint = this.Points[this.Points.Count - 2].Point;
            UnitPoint endPoint = this.Points[this.Points.Count - 1].Point;

            UnitPoint p1 = endPoint;
            if (BezierParam.LeadlineDistance > 0)
            {
                p1 = HitUtil.GetLinePointByDistance(endPoint, startPoint, BezierParam.LeadlineDistance, false);
            }

            UnitPoint p2 = HitUtil.GetLinePointByDistance(p1, startPoint, BezierParam.BezierWide, false);
            UnitPoint p3 = new UnitPoint();
            UnitPoint p4 = new UnitPoint();

            p3 = HitUtil.GetLinePointByDistance(BezierParam.ConnectStartPoint, BezierParam.ConnectEndPoint, BezierParam.LeadlineDistance + BezierParam.BezierWide, false);
            p4 = HitUtil.GetLinePointByDistance(BezierParam.ConnectStartPoint, BezierParam.ConnectEndPoint, BezierParam.LeadlineDistance, false);

            List<UnitPoint> bezierPoints = new List<UnitPoint>();
            bezierPoints.Add(p1);
            bezierPoints.Add(p2);
            bezierPoints.Add(p3);
            bezierPoints.Add(p4);
            flyConnectBezier.Add(this.Points.Count - 1, bezierPoints);

            return flyConnectBezier;
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
                for (int i = 0; i < this.Points.Count; i++)
                {
                    this.Points[i].Point = new UnitPoint(2 * centerAxis - this.Points[i].Point.X, this.Points[i].Point.Y);
                    if (!double.IsNaN(this.Points[i].Bulge))
                    {
                        this.Points[i].Bulge = -this.Points[i].Bulge;
                    }
                }
                this.StartMovePoint = new UnitPoint(2 * centerAxis - this.StartMovePoint.X, this.StartMovePoint.Y);
            }
            else if (A == 0)
            {
                double centerAxis = -C / B;
                for (int i = 0; i < this.Points.Count; i++)
                {
                    this.Points[i].Point = new UnitPoint(this.Points[i].Point.X, 2 * centerAxis - this.Points[i].Point.Y);
                    if (!double.IsNaN(this.Points[i].Bulge))
                    {
                        this.Points[i].Bulge = -this.Points[i].Bulge;
                    }
                }
                this.StartMovePoint = new UnitPoint(this.StartMovePoint.X, 2 * centerAxis - this.StartMovePoint.Y);
            }
            else
            {
                for (int i = 0; i < this.Points.Count; i++)
                {
                    this.Points[i].Point = Utils.Utils.MirrorAnyAlgorithm(this.Points[i].Point, A, B, C);
                    if (!double.IsNaN(this.Points[i].Bulge))
                    {
                        this.Points[i].Bulge = -this.Points[i].Bulge;
                    }
                }
                this.StartMovePoint = Utils.Utils.MirrorAnyAlgorithm(this.StartMovePoint, A, B, C);
            }
            this.Update();
        }
        public void DoRotate(UnitPoint rotateCenter, double rotateAngle, bool isClockwise)
        {
            rotateAngle = HitUtil.DegreesToRadians(rotateAngle);
            if (!isClockwise) rotateAngle = -rotateAngle;
            for (int i = 0; i < this.Points.Count; i++)
            {
                this.Points[i].Point = Utils.Utils.RotateAlgorithm(this.Points[i].Point, rotateCenter, rotateAngle);
            }
            this.StartMovePoint = Utils.Utils.RotateAlgorithm(this.StartMovePoint, rotateCenter, rotateAngle);
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
            if (!this.IsCloseFigure)
            {
                this.LeadIn.Position = 0;
                this.LeadOut.Position = this.PointCount - 1;
            }
            this.Update();
        }

        public void UpdateOverCutting()
        {
            if (Math.Abs(this.LeadOut.Pos) < 0.000001)//封口
            {
                this.MicroConnParams?.RemoveAll(r => r.Flags == MicroConnectFlags.GapPoint);
                this.LeadOut.Position = this.IsCloseFigure ? this.LeadIn.Position : this.PointCount - 1;
            }
            else if (this.LeadOut.Pos < 0)//缺口
            {
                this.MicroConnParams?.RemoveAll(r => r.Flags == MicroConnectFlags.GapPoint);
                float position = this.IsCloseFigure ? this.LeadIn.Position : this.Points.Count - 1;
                MicroConnectModel microConnectModel = new MicroConnectModel() { Flags = MicroConnectFlags.GapPoint, Size = this.LeadOut.Pos, Position = position };
                if (this.MicroConnParams == null) this.MicroConnParams = new List<MicroConnectModel>();
                this.MicroConnParams.Add(microConnectModel);
            }
            else if (this.IsCloseFigure)
            {
                this.MicroConnParams?.RemoveAll(r => r.Flags == MicroConnectFlags.GapPoint);
                if (this.LeadOut.RoundCut)//多圈
                {
                    this.OverCutLen = this.LeadOut.Pos;
                    this.LeadOut.Position = this.ConvertLengthToPercentInPolyLine((float)((this.LeadOut.Pos % 1) * this.SizeLength));
                }
                else
                {
                    this.OverCutLen = (float)(this.LeadOut.Pos / this.SizeLength) + 1;
                    this.LeadOut.Position = this.ConvertLengthToPercentInPolyLine(this.LeadOut.Pos);
                }
            }
            this.MicroConnParams?.RemoveAll(r => r.Flags == MicroConnectFlags.LeadInPoint || r.Flags == MicroConnectFlags.LeadOutPoint);
            if (this.MicroConnParams == null) this.MicroConnParams = new List<MicroConnectModel>();
            this.MicroConnParams.Add(new MicroConnectModel() { Flags = MicroConnectFlags.LeadInPoint, Position = this.LeadIn.Position, Size = 0 });
            this.MicroConnParams.Add(new MicroConnectModel() { Flags = MicroConnectFlags.LeadOutPoint, Position = this.LeadOut.Position, Size = 0 });

        }

        /// <summary>
        /// 将一定长度转化为在多段线上的百分比(带段数)
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private float ConvertLengthToPercentInPolyLine(float length)
        {
            float percent;
            float startLength = DrawingOperationHelper.GetLengthByPositionInPolyLine(this.Points, this.IsCloseFigure, this.LeadIn.Position);
            if (length < 0)
            {
                percent = (float)(-length / this.SizeLength);
                float startPercent = (float)(startLength / this.SizeLength);
                if (startPercent < percent)
                {
                    percent = 1 + (startPercent - percent);
                }
                else
                {
                    percent = startPercent - percent;
                }
                percent = (float)(percent * this.SizeLength);
                percent = DrawingOperationHelper.GetPercentOnLineByLength(this.Points, this.IsCloseFigure, percent);
            }
            else
            {
                length = length + startLength;
                length = length > this.SizeLength ? (float)(length - this.SizeLength) : length;
                percent = DrawingOperationHelper.GetPercentOnLineByLength(this.Points, this.IsCloseFigure, length);
                //percent = percent > this.PointCount ? percent - this.PointCount : percent;
            }
            return percent;
        }

        #endregion

        public void ReverseDirection()
        {
            this.IsReverseDirection = !this.IsReverseDirection;
            var startPoint = this.Points[0];
            this.Points.Reverse();
            List<double> bulges = this.Points.Select(e => -e.Bulge).ToList();
            if (this.IsCloseFigure)
            {
                this.Points.Insert(0, startPoint);
                this.Points.RemoveAt(this.Points.Count - 1);
            }
            else
            {
                bulges.Add(bulges[0]);
                bulges.RemoveAt(0);
            }
            for (int i = 0; i < this.Points.Count && i < bulges.Count; i++)
            {
                this.Points[i].Bulge = bulges[i];
            }
            if (this.MicroConnParams != null)
            {
                MicroConnectHelper.ReverseMicroConnParams(this.MicroConnParams, this.IsCloseFigure, this.Points.Count);
            }
            this.Update();
        }

        public void DoSizeChange(double centerX, double centerY, double coffX, double coffY)
        {
            for (int i = 0; i < this.PointCount; i++)
            {
                double x1 = this.Points[i].Point.X + (coffX - 1) * (this.Points[i].Point.X - centerX);
                double y1 = this.Points[i].Point.Y + (coffY - 1) * (this.Points[i].Point.Y - centerY);
                this.Points[i] = new UnitPointBulge(new UnitPoint(x1, y1), this.Points[i].Bulge, this.Points[i].HasMicroConn, this.Points[i].Position, this.Points[i].IsBasePoint);
            }
            this.StartMovePoint = Utils.Utils.ScaleAlgorithm(this.StartMovePoint, centerX, centerY, coffX, coffY);
            this.Update();
        }

        #region 设置引入引出线
        private UnitPoint LeadInIntersectPoint;
        private UnitPoint LeadOutIntersectPoint;
        private Tuple<UnitPointBulge, UnitPointBulge> leadInPoints;
        private Tuple<UnitPointBulge, UnitPointBulge> leadOutPoints;
        //private int SPIndex, SPNextIndex;//起始点位置索引和下一索引
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
            if (this.IsCloseFigure)
            {
                if (this.LeadInOutParams.LinePosition == LinePositions.AutoSelectSuitable)
                {
                    if (this.LeadInOutParams.IsLongSideLeadin)
                    {
                        this.LeadIn.Position = this.GetMaxSideIndex() + 0.5f;
                    }
                    else
                    {
                        this.LeadIn.Position = 0;
                    }
                }
                else if (this.LeadInOutParams.LinePosition == LinePositions.FigureTotalLength)
                {
                    this.LeadIn.Position = DrawingOperationHelper.GetPercentOnLineByLength(this.Points, this.IsCloseFigure, (float)(this.LeadInOutParams.FigureTotalLength * this.SizeLength));
                }
                else
                {

                }
            }
            else
            {
                this.LeadIn.Position = 0;
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
            this.LeadOut.Position = this.IsCloseFigure ? this.LeadIn.Position : this.PointCount - 1;
        }

        public void UpdateLeadwire()
        {
            if (this.CompensationPoints != null && this.CompensationPoints.Count > 0)
            {
                MicroUnitPoint leadinPoint = this.MicroStartPoints?.Find(e => e.Flags == MicroConnectFlags.LeadInPoint);
                MicroConnectHelper.GetLeadLinePointByMultiSegLine(this.CompensationPoints, this.IsCloseFigure, leadinPoint.Point.Point, true, out this.LeadInIntersectPoint, out this.leadInPoints);
                MicroUnitPoint leadoutPoint = this.MicroStartPoints?.Find(e => e.Flags == MicroConnectFlags.LeadOutPoint);
                MicroConnectHelper.GetLeadLinePointByMultiSegLine(this.CompensationPoints, this.IsCloseFigure, leadoutPoint.Point.Point, false, out this.LeadOutIntersectPoint, out this.leadOutPoints);
            }
            else if (this.MicroConnPoints != null && this.MicroConnPoints.Count > 0)
            {
                MicroUnitPoint leadinPoint = this.MicroStartPoints?.Find(e => e.Flags == MicroConnectFlags.LeadInPoint);
                MicroConnectHelper.GetLeadLinePointByMultiSegLine(this.MicroConnPoints, this.IsCloseFigure, leadinPoint.Point.Point, true, out this.LeadInIntersectPoint, out this.leadInPoints);
                MicroUnitPoint leadoutPoint = this.MicroStartPoints?.Find(e => e.Flags == MicroConnectFlags.LeadOutPoint);
                MicroConnectHelper.GetLeadLinePointByMultiSegLine(this.MicroConnPoints, this.IsCloseFigure, leadoutPoint.Point.Point, false, out this.LeadOutIntersectPoint, out this.leadOutPoints);
            }
            else if (this.Points != null && this.Points.Count > 0)
            {
                if (this.IsCloseFigure)
                {
                    int sPIndex = (int)this.LeadIn.Position;
                    int sPNextIndex = (sPIndex == this.Points.Count - 1 && this.IsCloseFigure) ? 0 : sPIndex + 1;
                    leadInPoints = Tuple.Create(this.Points[sPIndex], this.Points[sPNextIndex]);
                    this.GetLeadInIntersectPoint(leadInPoints);

                    sPIndex = (int)this.LeadOut.Position;
                    sPNextIndex = (sPIndex == this.Points.Count - 1 && this.IsCloseFigure) ? 0 : sPIndex + 1;
                    leadOutPoints = Tuple.Create(this.Points[sPIndex], this.Points[sPNextIndex]);
                    this.GetLeadOutInetersectPoint(leadOutPoints);
                    if (this.LeadOut.Position % 1 == 0)
                    {
                        sPIndex = (int)this.LeadOut.Position;
                        sPIndex = sPIndex - 1 == -1 ? Points.Count - 1 : sPIndex - 1;
                        sPNextIndex = (sPIndex == this.Points.Count - 1 && this.IsCloseFigure) ? 0 : sPIndex + 1;
                        leadOutPoints = Tuple.Create(this.Points[sPIndex], this.Points[sPNextIndex]);
                    }
                }
                else
                {
                    this.leadInPoints = Tuple.Create(this.Points[0], this.Points[1]);
                    this.leadOutPoints = Tuple.Create(this.Points[this.PointCount - 2], this.Points[this.PointCount - 1]);
                    this.LeadInIntersectPoint = this.leadInPoints.Item1.Point;
                    this.LeadOutIntersectPoint = this.leadOutPoints.Item2.Point;
                }
            }
            this.UpdateLeadInLine(leadInPoints);
            this.UpdateLeadOutLine(leadOutPoints);
            //this.UpdateStartMovePointWhenSetLeadwire();
        }

        private void UpdateLeadInLine(Tuple<UnitPointBulge, UnitPointBulge> points)
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
                    this.CalLineLeadIn(points);
                    break;
                case LeadLineType.Arc:
                    this.CalArcLeadIn(points);
                    break;
                case LeadLineType.LineArc:
                    this.CalLineArcLeadIn(points);
                    break;
            }
        }

        private void UpdateLeadOutLine(Tuple<UnitPointBulge, UnitPointBulge> points)
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
                    this.CalLineLeadOut(points);
                    break;
                case LeadLineType.Arc:
                    this.CalArcLeadOut(points);
                    break;
                case LeadLineType.LineArc:
                    this.CalLineArcLeadOut(points);
                    break;
            }
        }

        private void GetLeadInIntersectPoint(Tuple<UnitPointBulge, UnitPointBulge> points)
        {
            //if (this.IsCloseFigure)
            //{
            if (double.IsNaN(points.Item1.Bulge))
            {
                this.LeadInIntersectPoint = DrawingOperationHelper.GetPositionByPercentInLine(this.LeadIn.Position % 1, points.Item1.Point, points.Item2.Point);
            }
            else
            {
                this.LeadInIntersectPoint = DrawingOperationHelper.GetPositionByPercentInArc(this.LeadIn.Position % 1, points.Item1.Point, points.Item2.Point, (float)points.Item1.Bulge);
            }
            //}
        }

        private void GetLeadOutInetersectPoint(Tuple<UnitPointBulge, UnitPointBulge> points)
        {
            //if (this.IsCloseFigure)
            //{
            if (double.IsNaN(points.Item1.Bulge))
            {
                this.LeadOutIntersectPoint = DrawingOperationHelper.GetPositionByPercentInLine(this.LeadOut.Position % 1, points.Item1.Point, points.Item2.Point);
            }
            else
            {
                this.LeadOutIntersectPoint = DrawingOperationHelper.GetPositionByPercentInArc(this.LeadOut.Position % 1, points.Item1.Point, points.Item2.Point, (float)points.Item1.Bulge);
            }
            //}
        }


        private void CalNoneLeadIn()
        {
            this.LeadIn.LeadType = LeadLineType.None;
        }
        private void CalLineLeadIn(Tuple<UnitPointBulge, UnitPointBulge> points)
        {
            double lineAngle, angle;
            if (this.IsCloseFigure)
            {
                lineAngle = HitUtil.LineAngleR(points.Item2.Point, points.Item1.Point, 0);
                if (this.IsInnerCut)
                {
                    angle = this.Clockwise ? -this.LeadIn.Angle : this.LeadIn.Angle;
                }
                else
                {
                    angle = this.Clockwise ? this.LeadIn.Angle : -this.LeadIn.Angle;
                }
            }
            else
            {
                //this.LeadInIntersectPoint = points.Item1.Point;
                lineAngle = HitUtil.LineAngleR(points.Item2.Point, points.Item1.Point, 0);
                if (this.IsInnerCut)
                {
                    angle = this.LeadIn.Angle;
                }
                else
                {
                    angle = -this.LeadIn.Angle;
                }
            }
            this.LeadInWireParam.StartPoint = HitUtil.LineEndPoint(this.LeadInIntersectPoint, lineAngle, this.LeadIn.Length);
            this.LeadInWireParam.StartPoint = Utils.Utils.RotateAlgorithm(this.LeadInWireParam.StartPoint, this.LeadInIntersectPoint, angle);
            this.LeadInWireParam.EndPoint = this.LeadInIntersectPoint;

            if (this.LeadIn.LeadByHole)
            {
                this.LeadInWireParam.HoleCenter = this.LeadInWireParam.StartPoint;
                this.LeadInWireParam.HoleRadius = this.LeadIn.LeadHoleRadius;
            }
        }

        private void CalArcLeadIn(Tuple<UnitPointBulge, UnitPointBulge> points)
        {
            double radius = (this.LeadIn.Length / 2) / Math.Sin(this.LeadIn.Angle);
            double lineAngle, angle;
            UnitPoint zeroPoint, center;
            bool clockwise;//圆弧引线方向
            if (this.IsCloseFigure)
            {
                lineAngle = HitUtil.LineAngleR(points.Item2.Point, points.Item1.Point, 0);
                zeroPoint = HitUtil.LineEndPoint(this.LeadInIntersectPoint, lineAngle, this.LeadIn.Length);
                if (this.IsInnerCut)
                {
                    clockwise = this.Clockwise;
                    angle = this.Clockwise ? -this.LeadIn.Angle : this.LeadIn.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadInIntersectPoint, zeroPoint, radius, !this.Clockwise);
                }
                else
                {
                    clockwise = !this.Clockwise;
                    angle = this.Clockwise ? this.LeadIn.Angle : -this.LeadIn.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadInIntersectPoint, zeroPoint, radius, this.Clockwise);
                }
            }
            else
            {
                //this.LeadInIntersectPoint = points.Item1.Point;
                lineAngle = HitUtil.LineAngleR(points.Item2.Point, points.Item1.Point, 0);
                zeroPoint = HitUtil.LineEndPoint(this.LeadInIntersectPoint, lineAngle, this.LeadIn.Length);
                if (this.IsInnerCut)
                {
                    clockwise = false;
                    angle = this.LeadIn.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadInIntersectPoint, zeroPoint, radius, !this.Clockwise);
                }
                else
                {
                    clockwise = true;
                    angle = -this.LeadIn.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadInIntersectPoint, zeroPoint, radius, this.Clockwise);
                }
            }
            UnitPoint startPoint = Utils.Utils.RotateAlgorithm(zeroPoint, this.LeadInIntersectPoint, angle);
            float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, startPoint, 0));
            float endAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, this.LeadInIntersectPoint, 0));
            float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, endAngle, clockwise);

            this.LeadInWireParam.Center = center;
            this.LeadInWireParam.Radius = (float)radius;
            this.LeadInWireParam.SweepAngle = sweepAngle;
            this.LeadInWireParam.StartAngle = startAngle;
            this.LeadInWireParam.EndAngle = endAngle;
            this.LeadInWireParam.StartPoint = startPoint;
            this.LeadInWireParam.EndPoint = LeadInIntersectPoint;
        }
        private void CalLineArcLeadIn(Tuple<UnitPointBulge, UnitPointBulge> points)
        {
            double lineAngle, angle;
            UnitPoint zeroPoint, center;
            bool clockwise;//圆弧引线方向
            if (this.IsCloseFigure)
            {
                lineAngle = HitUtil.LineAngleR(points.Item2.Point, points.Item1.Point, 0);
                zeroPoint = HitUtil.LineEndPoint(this.LeadInIntersectPoint, lineAngle, this.LeadIn.Length);
                if (this.IsInnerCut)
                {
                    clockwise = this.Clockwise;
                    angle = this.Clockwise ? -this.LeadIn.Angle : this.LeadIn.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadInIntersectPoint, zeroPoint, this.LeadIn.ArcRadius, !this.Clockwise);
                }
                else
                {
                    clockwise = !this.Clockwise;
                    angle = this.Clockwise ? this.LeadIn.Angle : -this.LeadIn.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadInIntersectPoint, zeroPoint, this.LeadIn.ArcRadius, this.Clockwise);
                }
            }
            else
            {
                //this.LeadInIntersectPoint = points.Item1.Point;
                lineAngle = HitUtil.LineAngleR(points.Item2.Point, points.Item1.Point, 0);
                zeroPoint = HitUtil.LineEndPoint(this.LeadInIntersectPoint, lineAngle, this.LeadIn.Length);
                if (this.IsInnerCut)
                {
                    clockwise = false;
                    angle = this.LeadIn.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadInIntersectPoint, zeroPoint, this.LeadIn.ArcRadius, !this.Clockwise);
                }
                else
                {
                    clockwise = true;
                    angle = -this.LeadIn.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadInIntersectPoint, zeroPoint, this.LeadIn.ArcRadius, this.Clockwise);
                }
            }

            UnitPoint startPoint = Utils.Utils.RotateAlgorithm(this.LeadInIntersectPoint, center, angle);
            //2.求直线起点          
            this.LeadInWireParam.StartPoint = DrawingOperationHelper.GetRightCircleCenter(startPoint, center, this.LeadIn.Length, clockwise);
            this.LeadInWireParam.EndPoint = startPoint;

            float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, startPoint, 0));
            float endAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, this.LeadInIntersectPoint, 0));
            float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, endAngle, clockwise);

            this.LeadInWireParam.Center = center;
            this.LeadInWireParam.Radius = (float)this.LeadIn.ArcRadius;
            this.LeadInWireParam.SweepAngle = sweepAngle;
            this.LeadInWireParam.StartAngle = startAngle;
            this.LeadInWireParam.EndAngle = endAngle;
            if (this.LeadIn.LeadByHole)
            {
                this.LeadInWireParam.HoleCenter = this.LeadInWireParam.StartPoint;
                this.LeadInWireParam.HoleRadius = this.LeadIn.LeadHoleRadius;
            }
        }

        private void CalNoneLeadOut()
        {
            this.LeadOut.LeadType = LeadLineType.None;
        }
        private void CalLineLeadOut(Tuple<UnitPointBulge, UnitPointBulge> points)
        {
            double lineAngle, angle;
            if (this.IsCloseFigure)
            {
                lineAngle = HitUtil.LineAngleR(points.Item1.Point, points.Item2.Point, 0);
                if (this.IsInnerCut)
                {
                    angle = this.Clockwise ? this.LeadOut.Angle : -this.LeadOut.Angle;
                }
                else
                {
                    angle = this.Clockwise ? -this.LeadOut.Angle : this.LeadOut.Angle;
                }
            }
            else
            {
                //this.LeadOutIntersectPoint = points[this.PointCount - 1].Point;
                lineAngle = HitUtil.LineAngleR(points.Item1.Point, points.Item2.Point, 0);
                if (this.IsInnerCut)
                {
                    angle = -this.LeadOut.Angle;
                }
                else
                {
                    angle = this.LeadOut.Angle;
                }
            }
            this.LeadOutWireParam.EndPoint = HitUtil.LineEndPoint(this.LeadOutIntersectPoint, lineAngle, this.LeadOut.Length);
            this.LeadOutWireParam.EndPoint = Utils.Utils.RotateAlgorithm(this.LeadOutWireParam.EndPoint, this.LeadOutIntersectPoint, angle);
            this.LeadOutWireParam.StartPoint = this.LeadOutIntersectPoint;
        }

        private void CalArcLeadOut(Tuple<UnitPointBulge, UnitPointBulge> points)
        {
            double radius = (this.LeadOut.Length / 2) / Math.Sin(this.LeadOut.Angle);
            double lineAngle, angle;
            UnitPoint zeroPoint, center;
            bool clockwise;
            if (this.IsCloseFigure)
            {
                lineAngle = HitUtil.LineAngleR(points.Item1.Point, points.Item2.Point, 0);
                zeroPoint = HitUtil.LineEndPoint(this.LeadOutIntersectPoint, lineAngle, this.LeadOut.Length);
                if (this.IsInnerCut)
                {
                    clockwise = this.Clockwise;
                    angle = this.Clockwise ? this.LeadOut.Angle : -this.LeadOut.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadOutIntersectPoint, zeroPoint, radius, this.Clockwise);
                }
                else
                {
                    clockwise = !this.Clockwise;
                    angle = this.Clockwise ? -this.LeadOut.Angle : this.LeadOut.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadOutIntersectPoint, zeroPoint, radius, !this.Clockwise);
                }
            }
            else
            {
                //this.LeadOutIntersectPoint = points.Item2.Point;
                lineAngle = HitUtil.LineAngleR(points.Item1.Point, points.Item2.Point, 0);
                zeroPoint = HitUtil.LineEndPoint(this.LeadOutIntersectPoint, lineAngle, this.LeadOut.Length);
                if (this.IsInnerCut)
                {
                    clockwise = true;
                    angle = this.LeadOut.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadOutIntersectPoint, zeroPoint, radius, !this.Clockwise);
                }
                else
                {
                    clockwise = false;
                    angle = -this.LeadOut.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadOutIntersectPoint, zeroPoint, radius, this.Clockwise);
                }
            }
            UnitPoint endpoint = Utils.Utils.RotateAlgorithm(zeroPoint, this.LeadOutIntersectPoint, angle);
            float endAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, endpoint, 0));
            float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, this.LeadOutIntersectPoint, 0));
            float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, endAngle, clockwise);

            this.LeadOutWireParam.Center = center;
            this.LeadOutWireParam.Radius = (float)radius;
            this.LeadOutWireParam.SweepAngle = sweepAngle;
            this.LeadOutWireParam.StartAngle = startAngle;
            this.LeadOutWireParam.EndAngle = endAngle;
            this.LeadOutWireParam.StartPoint = LeadOutIntersectPoint;
            this.LeadOutWireParam.EndPoint = endpoint;
        }
        private void CalLineArcLeadOut(Tuple<UnitPointBulge, UnitPointBulge> points)
        {
            double lineAngle, angle;
            UnitPoint zeroPoint, center;
            bool clockwise;//圆弧引线方向
            if (this.IsCloseFigure)
            {
                lineAngle = HitUtil.LineAngleR(points.Item1.Point, points.Item2.Point, 0);
                zeroPoint = HitUtil.LineEndPoint(this.LeadOutIntersectPoint, lineAngle, this.LeadOut.Length);
                if (this.IsInnerCut)
                {
                    clockwise = this.Clockwise;
                    angle = this.Clockwise ? this.LeadOut.Angle : -this.LeadOut.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadOutIntersectPoint, zeroPoint, this.LeadOut.ArcRadius, this.Clockwise);
                }
                else
                {
                    clockwise = !this.Clockwise;
                    angle = this.Clockwise ? -this.LeadOut.Angle : this.LeadOut.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadOutIntersectPoint, zeroPoint, this.LeadOut.ArcRadius, !this.Clockwise);
                }
            }
            else
            {
                //this.LeadOutIntersectPoint = points.Item2.Point;
                lineAngle = HitUtil.LineAngleR(points.Item1.Point, points.Item2.Point, 0);
                zeroPoint = HitUtil.LineEndPoint(this.LeadOutIntersectPoint, lineAngle, this.LeadOut.Length);
                if (this.IsInnerCut)
                {
                    clockwise = true;
                    angle = this.LeadOut.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadOutIntersectPoint, zeroPoint, this.LeadOut.ArcRadius, !this.Clockwise);
                }
                else
                {
                    clockwise = false;
                    angle = -this.LeadOut.Angle;
                    center = DrawingOperationHelper.GetRightCircleCenter(this.LeadOutIntersectPoint, zeroPoint, this.LeadOut.ArcRadius, this.Clockwise);
                }
            }

            UnitPoint endpoint = Utils.Utils.RotateAlgorithm(this.LeadOutIntersectPoint, center, angle);
            //2.求直线起点          
            this.LeadOutWireParam.StartPoint = endpoint;
            this.LeadOutWireParam.EndPoint = DrawingOperationHelper.GetRightCircleCenter(endpoint, center, this.LeadOut.Length, !clockwise);

            float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, this.LeadOutIntersectPoint, 0));
            float endAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, endpoint, 0));
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

        #region 倒角
        public bool DoRoundAngle(bool isRoundAngle, double radius, UnitPoint mousePoint)
        {
            List<int> roundAngleIndexs = this.FindRoundAngleIndexs(mousePoint);
            if (roundAngleIndexs != null)
            {
                if (double.IsNaN(this.Points[roundAngleIndexs[0]].Bulge) && double.IsNaN(this.Points[roundAngleIndexs[1]].Bulge))
                {
                    return this.LineLineBulge(isRoundAngle, radius, roundAngleIndexs);
                }//圆弧和圆弧
                else if (!double.IsNaN(this.Points[roundAngleIndexs[0]].Bulge) && !double.IsNaN(this.Points[roundAngleIndexs[1]].Bulge))
                {
                    return this.ArcArcBulge(isRoundAngle,radius,roundAngleIndexs);
                }//直线和圆弧
                else
                {
                    return this.LineArcBulge(isRoundAngle, radius, roundAngleIndexs);
                }
            }
            return false;
        }

        private bool LineLineBulge(bool isRoundAngle, double radius, List<int> roundAngleIndexs)
        {
            double maxRadius = this.GetLineLineMaxRoundRadius(this.Points[roundAngleIndexs[0]].Point, this.Points[roundAngleIndexs[1]].Point, this.Points[roundAngleIndexs[2]].Point);
            if (radius <= maxRadius)
            {
                List<UnitPoint> unitPoints = new List<UnitPoint>();
                double bugle = double.NaN;
                if (isRoundAngle)
                {
                    unitPoints = BulgeHelper.GetCufoffPoint(this.Points[roundAngleIndexs[0]].Point, this.Points[roundAngleIndexs[1]].Point, this.Points[roundAngleIndexs[2]].Point, radius);
                }
                else
                {
                    unitPoints = BulgeHelper.GetCufPoint(this.Points[roundAngleIndexs[0]].Point, this.Points[roundAngleIndexs[1]].Point, this.Points[roundAngleIndexs[2]].Point, radius);
                }
                bugle = BulgeHelper.GetBulgeFromThreePoints(this.Points[roundAngleIndexs[0]].Point, this.Points[roundAngleIndexs[1]].Point, this.Points[roundAngleIndexs[2]].Point, isRoundAngle);
                this.Points.RemoveAt(roundAngleIndexs[1]);
                if (roundAngleIndexs[1] == 0)
                {
                    this.Points.Add(new UnitPointBulge(unitPoints[0], bugle));
                    this.Points.Insert(0, new UnitPointBulge(unitPoints[1]));
                }
                else if (roundAngleIndexs[2] == 0)
                {
                    this.Points.Add(new UnitPointBulge(unitPoints[0], bugle));
                    this.Points.Add(new UnitPointBulge(unitPoints[1]));
                }
                else
                {
                    this.Points.Insert(roundAngleIndexs[1], new UnitPointBulge(unitPoints[0], bugle));
                    this.Points.Insert(roundAngleIndexs[2], new UnitPointBulge(unitPoints[1]));
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 直线和圆弧倒角
        /// </summary>
        /// <param name="isRoundAngle"></param>
        /// <param name="radius"></param>
        /// <param name="roundAngleIndexs"></param>
        /// <returns></returns>
        private bool LineArcBulge(bool isRoundAngle, double radius, List<int> roundAngleIndexs)
        {
            if (!isRoundAngle) return false;
            double maxRadius = this.GetLineArcMaxRoundRadius(roundAngleIndexs);
            if (maxRadius < 0 || radius > maxRadius)
            {
                return false;
            }
            if (double.IsNaN(this.Points[roundAngleIndexs[0]].Bulge))//第一段是直线
            {
                ArcModelMini arcModelMini = DrawingOperationHelper.GetArcParametersFromBulge(this.Points[roundAngleIndexs[1]].Point, this.Points[roundAngleIndexs[2]].Point, (float)this.Points[roundAngleIndexs[1]].Bulge);
                //当圆弧与直线相交时有两处交点，需要选出最优点进行倒角
                double a = this.Points[roundAngleIndexs[1]].Point.Y - this.Points[roundAngleIndexs[0]].Point.Y;
                double b = this.Points[roundAngleIndexs[0]].Point.X - this.Points[roundAngleIndexs[1]].Point.X;
                double c = this.Points[roundAngleIndexs[1]].Point.X * this.Points[roundAngleIndexs[0]].Point.Y - this.Points[roundAngleIndexs[0]].Point.X * this.Points[roundAngleIndexs[1]].Point.Y;
                List<UnitPoint> intersectionPoints = DrawingOperationHelper.GetIntersectPointByLineAndCircle(a, b, c, arcModelMini.Center, arcModelMini.Radius);
                var validIntersectionPoints = intersectionPoints.FindAll(x => HitUtil.IsPointInLine(this.Points[roundAngleIndexs[0]].Point, this.Points[roundAngleIndexs[1]].Point, x, ROUNDANGLE_PRECISION) ==true);
                if (validIntersectionPoints.Count == 2)
                {
                    validIntersectionPoints.RemoveAll((x) => HitUtil.TwoFloatNumberIsEqual(x.X, this.Points[roundAngleIndexs[1]].Point.X, ROUNDANGLE_PRECISION) && HitUtil.TwoFloatNumberIsEqual(x.Y, this.Points[roundAngleIndexs[1]].Point.Y, ROUNDANGLE_PRECISION) == true);
                    if (validIntersectionPoints.Count==1)
                    {
                        double bulgeArcNew = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arcModelMini.Center, validIntersectionPoints[0], this.Points[roundAngleIndexs[2]].Point, arcModelMini.Clockwise);
                        arcModelMini = DrawingOperationHelper.GetArcParametersFromBulge(validIntersectionPoints[0], this.Points[roundAngleIndexs[2]].Point, (float)bulgeArcNew);
                        this.Points[roundAngleIndexs[1]].Point = validIntersectionPoints[0];
                    }
                }

                var result = DrawingOperationHelper.GetCenterChamferingByLineArc(this.Points[roundAngleIndexs[0]].Point, this.Points[roundAngleIndexs[1]].Point, arcModelMini, radius);
                if (result.Count > 0)
                {
                    this.centers.Clear();
                    this.centers.Add(result[1]);
                    //this.centers.AddRange(result);
                    this.radius = (float)radius;
                    this.drawTest = false;
                    //return false;
                    double bulgeNew = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arcModelMini.Center, result[2], this.Points[roundAngleIndexs[2]].Point, arcModelMini.Clockwise);
                    bool clockWise = HitUtil.IsClockwiseByCross(result[0], this.Points[roundAngleIndexs[1]].Point, result[2]);
                    double bulgeNew2 = BulgeHelper.GetBulgeFromTwoPointsAndCenter(result[1], result[0], result[2], clockWise);
                    this.Points[roundAngleIndexs[1]] = new UnitPointBulge(result[0], bulgeNew2);
                    this.Points.Insert(roundAngleIndexs[2], new UnitPointBulge(result[2], bulgeNew));
                    return true;
                }
            }
            else//第一段是圆弧
            {
                ArcModelMini arcModelMini = DrawingOperationHelper.GetArcParametersFromBulge(this.Points[roundAngleIndexs[0]].Point, this.Points[roundAngleIndexs[1]].Point, (float)this.Points[roundAngleIndexs[0]].Bulge);
                //当圆弧与直线相交时有两处交点，需要选出最优点进行倒角
                double a = this.Points[roundAngleIndexs[1]].Point.Y - this.Points[roundAngleIndexs[2]].Point.Y;
                double b = this.Points[roundAngleIndexs[2]].Point.X - this.Points[roundAngleIndexs[1]].Point.X;
                double c = this.Points[roundAngleIndexs[1]].Point.X * this.Points[roundAngleIndexs[2]].Point.Y - this.Points[roundAngleIndexs[2]].Point.X * this.Points[roundAngleIndexs[1]].Point.Y;
                List<UnitPoint> intersectionPoints = DrawingOperationHelper.GetIntersectPointByLineAndCircle(a, b, c, arcModelMini.Center, arcModelMini.Radius);
                var validIntersectionPoints = intersectionPoints.FindAll(x => HitUtil.IsPointInLine(this.Points[roundAngleIndexs[1]].Point, this.Points[roundAngleIndexs[2]].Point, x, ROUNDANGLE_PRECISION) == true);
                if (validIntersectionPoints.Count == 2)
                {
                    validIntersectionPoints.RemoveAll(x => HitUtil.TwoFloatNumberIsEqual(x.X, this.Points[roundAngleIndexs[1]].Point.X, ROUNDANGLE_PRECISION) && HitUtil.TwoFloatNumberIsEqual(x.Y, this.Points[roundAngleIndexs[1]].Point.Y, ROUNDANGLE_PRECISION) == true);
                    if (validIntersectionPoints.Count == 1)
                    {
                        double bulgeArcNew = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arcModelMini.Center, this.Points[roundAngleIndexs[0]].Point, validIntersectionPoints[0], arcModelMini.Clockwise);
                        arcModelMini = DrawingOperationHelper.GetArcParametersFromBulge(this.Points[roundAngleIndexs[0]].Point, validIntersectionPoints[0], (float)bulgeArcNew);
                        this.Points[roundAngleIndexs[1]].Point = validIntersectionPoints[0];
                    }
                }

                var result = DrawingOperationHelper.GetCenterChamferingByLineArc(this.Points[roundAngleIndexs[2]].Point, this.Points[roundAngleIndexs[1]].Point, arcModelMini, radius);
                if (result.Count > 0)
                {
                    this.centers.Clear();
                    this.centers.Add(result[1]);
                    //this.centers.AddRange(result);
                    this.radius = (float)radius;
                    this.drawTest = false;
                    //return false;

                    double bulgeNew = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arcModelMini.Center, this.Points[roundAngleIndexs[0]].Point, result[2], arcModelMini.Clockwise);
                    bool clockWise = HitUtil.IsClockwiseByCross(result[2], this.Points[roundAngleIndexs[1]].Point, result[0]);
                    double bulgeNew2 = BulgeHelper.GetBulgeFromTwoPointsAndCenter(result[1], result[2], result[0], clockWise);
                    this.Points[roundAngleIndexs[0]] = new UnitPointBulge(this.Points[roundAngleIndexs[0]].Point, bulgeNew);
                    this.Points[roundAngleIndexs[1]] = new UnitPointBulge(result[2], bulgeNew2);
                    this.Points.Insert(roundAngleIndexs[2], new UnitPointBulge(result[0]));
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 圆弧与圆弧倒角
        /// </summary>
        /// <returns></returns>
        private bool ArcArcBulge(bool isRoundAngle,double radius,List<int> roundAngleIndexs)
        {
            //1.两段弧扩展为两个圆，2.判断两段弧是否重复相交，假如重复相交则舍弃重叠部分，以最优点组建新圆弧,3.求倒角的圆心，4.找倒角圆心与两个圆(两段弧)的最近点，5.筛选出两个最近点均在两段弧上即为切点
            //两段弧的首尾点重合的情况没有考虑和验证！！！(多端线不能设置弧，无法验证充分)
            //圆与圆的释放角没有做！
            if (!isRoundAngle) return false;
            if (radius <= 0) return false;  
            var arcModelMini1 = DrawingOperationHelper.GetArcParametersFromBulge(this.Points[roundAngleIndexs[0]].Point, this.Points[roundAngleIndexs[1]].Point, (float)this.Points[roundAngleIndexs[0]].Bulge);
            var arcModelMini2 = DrawingOperationHelper.GetArcParametersFromBulge(this.Points[roundAngleIndexs[1]].Point, this.Points[roundAngleIndexs[2]].Point, (float)this.Points[roundAngleIndexs[1]].Bulge);
            UnitPoint backupPoint = new UnitPoint(this.Points[roundAngleIndexs[1]].Point.X, this.Points[roundAngleIndexs[1]].Point.Y);
            arcModelMini1.StartAngle = (float)HitUtil.RadiansToDegrees(arcModelMini1.StartAngle);
            arcModelMini1.EndAngle = (float)HitUtil.RadiansToDegrees(arcModelMini1.EndAngle);
            arcModelMini2.StartAngle = (float)HitUtil.RadiansToDegrees(arcModelMini2.StartAngle);
            arcModelMini2.EndAngle = (float)HitUtil.RadiansToDegrees(arcModelMini2.EndAngle);
            var twoArcIntersectPoint = DrawingOperationHelper.GetIntersectPointBy2Circle(arcModelMini1.Center, arcModelMini1.Radius, arcModelMini2.Center, arcModelMini2.Radius);
            if (twoArcIntersectPoint.Count == 1&& arcModelMini2.Clockwise != arcModelMini1.Clockwise) return false;//两段弧相切且时钟方向相反即认为不可倒角
            var validTwoArcIntersectPoint = twoArcIntersectPoint.FindAll(x => HitUtil.IsPointOnArc(x, ROUNDANGLE_PRECISION, arcModelMini1) && HitUtil.IsPointOnArc(x, ROUNDANGLE_PRECISION, arcModelMini2) == true);
            if (validTwoArcIntersectPoint.Count == 0) return false;
            if (validTwoArcIntersectPoint.Count ==2)
            {
                validTwoArcIntersectPoint.RemoveAll(x => HitUtil.TwoFloatNumberIsEqual(x.X, this.Points[roundAngleIndexs[1]].Point.X, ROUNDANGLE_PRECISION) && HitUtil.TwoFloatNumberIsEqual(x.Y, this.Points[roundAngleIndexs[1]].Point.Y, ROUNDANGLE_PRECISION) == true);
                if (validTwoArcIntersectPoint.Count == 1)
                {
                    double bulgeArcNew1 = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arcModelMini1.Center, this.Points[roundAngleIndexs[0]].Point, validTwoArcIntersectPoint[0], arcModelMini1.Clockwise);
                    double bulgeArcNew2 = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arcModelMini2.Center, validTwoArcIntersectPoint[0], this.Points[roundAngleIndexs[2]].Point, arcModelMini2.Clockwise);
                    arcModelMini1 = DrawingOperationHelper.GetArcParametersFromBulge(this.Points[roundAngleIndexs[0]].Point, validTwoArcIntersectPoint[0], (float)bulgeArcNew1);
                    arcModelMini2 = DrawingOperationHelper.GetArcParametersFromBulge(validTwoArcIntersectPoint[0], this.Points[roundAngleIndexs[2]].Point, (float)bulgeArcNew2);
                    arcModelMini1.StartAngle = (float)HitUtil.RadiansToDegrees(arcModelMini1.StartAngle);
                    arcModelMini1.EndAngle = (float)HitUtil.RadiansToDegrees(arcModelMini1.EndAngle);
                    arcModelMini2.StartAngle = (float)HitUtil.RadiansToDegrees(arcModelMini2.StartAngle);
                    arcModelMini2.EndAngle = (float)HitUtil.RadiansToDegrees(arcModelMini2.EndAngle);//调用HitUtil.IsPointOnArc方法时，其内部需要把弧度转换为角度
                    this.Points[roundAngleIndexs[1]].Point = validTwoArcIntersectPoint[0];
                }
            }
            List<UnitPoint> getArcArcBulgecoordinate = DrawingOperationHelper.GetCenterChamferingBy2Arc(arcModelMini1.Center, arcModelMini1.Radius, arcModelMini2.Center, arcModelMini2.Radius, radius);
            UnitPoint cutoffpoint1 = new UnitPoint();
            UnitPoint cutoffpoint2 = new UnitPoint();
            UnitPoint finalCenter = new UnitPoint();
            int i = 0;
            foreach (var item in getArcArcBulgecoordinate)
            {
                 var cutoffPointTest1 = HitUtil.NearestPointOnCircle(arcModelMini1.Center,  arcModelMini1.Radius, item, 0);
                 var cutoffPointTest2 = HitUtil.NearestPointOnCircle(arcModelMini2.Center, arcModelMini2.Radius, item, 0);
                if (HitUtil.IsPointOnArc(cutoffPointTest1, ROUNDANGLE_PRECISION, arcModelMini1) && HitUtil.IsPointOnArc(cutoffPointTest2, ROUNDANGLE_PRECISION, arcModelMini2))
                {
                    cutoffpoint1 = cutoffPointTest1;
                    cutoffpoint2 = cutoffPointTest2;
                    finalCenter = item;
                    i++;
                }
            }
            if (i==1)
            {
                double bulgeNew0 = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arcModelMini1.Center, this.Points[roundAngleIndexs[0]].Point, cutoffpoint1, arcModelMini1.Clockwise);
                bool clockWise = HitUtil.IsClockwiseByCross(cutoffpoint1, this.Points[roundAngleIndexs[1]].Point, cutoffpoint2);
                double bulgeNew1 = BulgeHelper.GetBulgeFromTwoPointsAndCenter(finalCenter, cutoffpoint1, cutoffpoint2, clockWise);
                double bugleNew2 = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arcModelMini2.Center, cutoffpoint2, this.Points[roundAngleIndexs[2]].Point, arcModelMini2.Clockwise);
                this.Points[roundAngleIndexs[0]] = new UnitPointBulge(this.Points[roundAngleIndexs[0]].Point, bulgeNew0);
                this.Points[roundAngleIndexs[1]] = new UnitPointBulge(cutoffpoint1, bulgeNew1);
                this.Points.Insert(roundAngleIndexs[2], new UnitPointBulge(cutoffpoint2, bugleNew2));
                return true;
            }
            if (this.Points[roundAngleIndexs[1]].Point != backupPoint)
            {
                this.Points[roundAngleIndexs[1]].Point = backupPoint;
            }
            return false;
        }

        /// <summary>
        /// 计算能倒角的两条线段的索引
        /// </summary>
        /// <param name="mousePoint"></param>
        /// <returns></returns>
        private List<int> FindRoundAngleIndexs(UnitPoint mousePoint)
        {
            int index = DrawingOperationHelper.GetPointInLineIndex(this, mousePoint);
            if (index != -1)
            {
                int tempIndex = (this.IsCloseFigure && index == this.PointCount - 1) ? 0 : index + 1;
                double d1 = HitUtil.Distance(this.Points[index].Point, mousePoint);
                double d2 = HitUtil.Distance(this.Points[tempIndex].Point, mousePoint);
                index = d1 <= d2 ? index : tempIndex;
                if (!this.IsCloseFigure)
                {
                    if (index == 0 || index == this.PointCount - 1)
                    {
                        return null;
                    }
                    else
                    {
                        return new List<int>() { index - 1, index, index + 1 };
                    }
                }
                else
                {
                    if (index == 0)
                    {
                        return new List<int>() { this.PointCount - 1, 0, 1 };
                    }
                    else if (index == this.PointCount - 1)
                    {
                        return new List<int>() { index - 1, index, 0 };
                    }
                    else
                    {
                        return new List<int>() { index - 1, index, index + 1 };
                    }
                }
            }
            return null;
        }

        private double GetLineLineMaxRoundRadius(UnitPoint p1, UnitPoint p2, UnitPoint p3)
        {
            double lineAngle = BulgeHelper.CalTwoLinesAngleFromThreePoints(p1, p2, p3);
            double l1 = HitUtil.Distance(p1, p2);
            double l2 = HitUtil.Distance(p2, p3);
            double length = l1 > l2 ? l2 : l1;
            double distance = 0;
            if (l1 > l2)
            {
                double preAngle = HitUtil.LineAngleR(p2, p1, 0);
                UnitPoint prePoint = HitUtil.LineEndPoint(p2, preAngle, length);
                distance = HitUtil.Distance(prePoint, p3);
            }
            else if (l1 < l2)
            {
                double preAngle = HitUtil.LineAngleR(p2, p3, 0);
                UnitPoint prePoint = HitUtil.LineEndPoint(p2, preAngle, length);
                distance = HitUtil.Distance(prePoint, p1);
            }
            else
            {
                distance = HitUtil.Distance(p1, p3);
            }
            distance = (distance / 2) / Math.Cos(lineAngle / 2);
            return distance;
        }

        /// <summary>
        /// 计算直线和圆弧做倒角时能允许倒角的最大半径值
        /// </summary>
        /// <param name="roundAngleIndexs">倒角的三个端点坐标</param>
        /// <returns>最大允许倒角半径</returns>
        private double GetLineArcMaxRoundRadius(List<int> roundAngleIndexs)
        {
            UnitPoint center, lineEndPoint;
            double radius, bulge;
            if (!double.IsNaN(this.Points[roundAngleIndexs[0]].Bulge))
            {
                bulge = this.Points[roundAngleIndexs[0]].Bulge;
                center = BulgeHelper.GetCenterByBulgeAndTwoPoints(this.Points[roundAngleIndexs[0]].Point, this.Points[roundAngleIndexs[1]].Point, bulge);
                radius = HitUtil.Distance(center, this.Points[roundAngleIndexs[0]].Point);
                lineEndPoint = this.Points[roundAngleIndexs[2]].Point;
            }
            else
            {
                bulge = this.Points[roundAngleIndexs[1]].Bulge;
                center = BulgeHelper.GetCenterByBulgeAndTwoPoints(this.Points[roundAngleIndexs[1]].Point, this.Points[roundAngleIndexs[2]].Point, bulge);
                radius = HitUtil.Distance(center, this.Points[roundAngleIndexs[1]].Point);
                lineEndPoint = this.Points[roundAngleIndexs[0]].Point;
            }
            double slope1 = HitUtil.LineSlope(lineEndPoint, this.Points[roundAngleIndexs[1]].Point);
            double slope2 = HitUtil.LineSlope(center, this.Points[roundAngleIndexs[1]].Point);
            if (HitUtil.TwoFloatNumberIsEqual( slope1 * slope2 ,-1, ROUNDANGLE_PRECISION))
            {
                bool clockwise = HitUtil.IsClockwiseByCross(lineEndPoint, this.Points[roundAngleIndexs[1]].Point, center);
                if (clockwise && bulge < 0 || !clockwise && bulge > 0)
                {
                    return -1;
                }
            }
            double c1p3 = HitUtil.Distance(center, lineEndPoint);
            double p2p3 = HitUtil.Distance(lineEndPoint, this.Points[roundAngleIndexs[1]].Point);
            double cosTheta = (Math.Pow(c1p3, 2) + Math.Pow(p2p3, 2) - Math.Pow(radius, 2)) / (2 * c1p3 * p2p3);
            double theta = Math.Acos(cosTheta);
            double alpha = Math.PI / 2 - theta;
            double maxR = (c1p3 * c1p3 - radius * radius) / (2 * (c1p3 * Math.Cos(alpha) + radius));
            return maxR;
        }
        #endregion

        /// <summary>
        /// 得到总长度
        /// </summary>
        /// <param name="points"></param>
        /// <param name="isCloseFigure"></param>
        /// <returns></returns>
        private double GetLength(List<UnitPointBulge> points, bool isCloseFigure)
        {
            double sum = 0;
            double length = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                var p1 = points[i];
                var p2 = points[i + 1];
                if (!double.IsNaN(p1.Bulge))
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                    length = Math.Abs(2 * Math.PI * arc.Radius * arc.SweepAngle / 360.0);
                }
                else
                {
                    length = HitUtil.Distance(p1.Point, p2.Point);
                }
                sum += length;
            }
            if (isCloseFigure && points.Count > 1)
            {
                var p1 = points[points.Count - 1];
                var p2 = points[0];
                if (!double.IsNaN(p1.Bulge))
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                    length = Math.Abs(2 * Math.PI * arc.Radius * arc.SweepAngle / 360.0);
                }
                else
                {
                    length = HitUtil.Distance(p1.Point, p2.Point);
                }
                sum += length;
            }
            return sum;
        }

        private int GetMaxSideIndex()
        {
            int index = 0;
            double maxLen = 0;
            if (this.IsCloseFigure)
            {
                int nextIndex = 0;
                for (int i = 0; i < this.Points.Count; i++)
                {
                    nextIndex = i == this.PointCount - 1 ? 0 : i + 1;
                    if (double.IsNaN(this.Points[i].Bulge))
                    {
                        double len = HitUtil.Distance(this.Points[i].Point, this.Points[nextIndex].Point);
                        if (len > maxLen)
                        {
                            maxLen = len;
                            index = i;
                        }
                    }
                }
            }
            return index;
        }

    }
}
