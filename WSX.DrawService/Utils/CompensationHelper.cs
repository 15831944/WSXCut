using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.DrawService.DrawModel;
using WSX.DrawService.DrawTool.MultiSegmentLine;

namespace WSX.DrawService.Utils
{
    public class CompensationHelper
    {
        private static float errorRange = 0.0001f;

        /// <summary>
        ///  计算补偿点
        /// </summary>
        /// <param name="points"></param>
        /// <param name="isCloseFigure"></param>
        /// <param name="compensationParam"></param>
        /// <param name="isReverseDirection"></param>
        /// <param name="isOutside"></param>
        /// <returns></returns>
        public static List<UnitPointBulge> CalCompensationPoints(List<UnitPointBulge> points, bool isCloseFigure, CompensationModel compensationParam, bool isReverseDirection, bool isOutside)
        {
            var rets = new List<UnitPointBulge>();
            if (points == null && points.Count <= 0) return rets;

            #region 计算补偿点坐标
            if (points.Count == 2)
            {
                var ps = GetCompensationsPoint(points[0], points[1], compensationParam.Size, isOutside);
                ps.Item1.IsBasePoint = true;
                ps.Item1.Position = 0;
                ps.Item2.IsBasePoint = true;
                ps.Item2.Position = 1;
                rets.Add(ps.Item1);
                rets.Add(ps.Item2);
            }
            else if (points.Count >= 3)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (!isCloseFigure && i == 0)
                    {
                        var ps = GetCompensationsPoint(points[0], points[1], compensationParam.Size, isOutside);
                        ps.Item1.IsBasePoint = true;
                        ps.Item1.Position = i;
                        rets.Add(ps.Item1);
                    }
                    else if (!isCloseFigure && i == points.Count - 1)
                    {
                        var ps = GetCompensationsPoint(points[i - 1], points[points.Count - 1], compensationParam.Size, isOutside);
                        ps.Item2.IsBasePoint = true;
                        ps.Item2.Position = i;
                        rets.Add(ps.Item2);
                    }
                    else
                    {
                        int indexPer = (i - 1 < 0) ? points.Count - 1 : i - 1;//前一段
                        int indexCur = i;//当前段
                        int indexNext = (i + 1 > points.Count - 1) ? 0 : i + 1;//后一段

                        UnitPointBulge p1 = points[indexPer];
                        UnitPointBulge p2 = points[indexCur];
                        UnitPointBulge p3 = points[indexNext];

                        var ps = new List<UnitPointBulge>();
                        if (!double.IsNaN(p1.Bulge) && !double.IsNaN(p2.Bulge))
                        {
                            //圆弧到圆弧,求交点
                            ps = CalCompensationsBy2Arc(p1, p2, p3, indexCur, compensationParam, isOutside);
                        }
                        else if (double.IsNaN(p1.Bulge) && !double.IsNaN(p2.Bulge))
                        {
                            //直线到圆弧，求交点
                            ps = CalCompensationsByLineArc(p1, p2, p3, indexCur, compensationParam, isOutside);
                        }
                        else if (!double.IsNaN(p1.Bulge) && double.IsNaN(p2.Bulge))
                        {
                            //圆弧到直线，求交点
                            ps = CalCompensationsByArcLine(p1, p2, p3, indexCur, compensationParam, isOutside);
                        }
                        else
                        {
                            //直线到直线，求交点
                            ps = CalCompensationsBy2Line(p1, p2, p3, indexCur, compensationParam, isOutside);
                        }
                        if (ps.Count > 0)
                        {
                            ps[ps.Count - 1].IsBasePoint = true;
                        }
                        rets.AddRange(ps);
                    }
                }
            }
            #endregion

            #region 更新凸度值，和所在段的标记
            for (int i = 0; i < rets.Count - 1; i++)
            {
                if (!double.IsNaN(rets[i].Bulge) && (int)rets[i].Position >= 0)
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(points[(int)rets[i].Position].Point, points[(int)rets[i].Position + 1].Point, (float)points[(int)rets[i].Position].Bulge);
                    double bugle = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arc.Center, rets[i].Point, rets[i + 1].Point, arc.Clockwise);
                    rets[i].Bulge = bugle;
                }
                if ((int)rets[i].Position < 0)
                {
                    rets[i].Position = -(rets[i].Position + 1);
                }
            }
            if (isCloseFigure && rets.Count > 0 && !double.IsNaN(rets[rets.Count - 1].Bulge) && rets[rets.Count - 1].Position >= 0)
            {
                ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(points[points.Count - 1].Point, points[0].Point, (float)points[points.Count - 1].Bulge);
                double bugle = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arc.Center, rets[rets.Count - 1].Point, rets[0].Point, arc.Clockwise);
                rets[rets.Count - 1].Bulge = bugle;

            }
            if (rets.Count > 0 && (int)rets[rets.Count - 1].Position < 0)
            {
                rets[rets.Count - 1].Position = -(rets[rets.Count - 1].Position + 1);
            }
            #endregion

            #region 根据情况调整起点
            if (isCloseFigure)
            {
                int index = rets.FindLastIndex(e => (int)e.Position == 0);
                if ((index == 1 && !isReverseDirection) || index == 2)
                {
                    var temp = rets[0];
                    rets.Remove(temp);
                    rets.Add(temp);
                }
            }
            #endregion

            return rets;
        }
        /// <summary>
        /// 圆弧到圆弧,补偿求交点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="indexCur"></param>
        /// <returns></returns>
        private static List<UnitPointBulge> CalCompensationsBy2Arc(UnitPointBulge p1, UnitPointBulge p2, UnitPointBulge p3, int indexCur, CompensationModel compensationParam, bool isOutside)
        {
            var retPoints = new List<UnitPointBulge>();
            ArcModelMini arc1 = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
            ArcModelMini arc2 = DrawingOperationHelper.GetArcParametersFromBulge(p2.Point, p3.Point, (float)p2.Bulge);
            double r1 = arc1.Clockwise ? arc1.Radius - compensationParam.Size : arc1.Radius + compensationParam.Size;
            double r2 = arc2.Clockwise ? arc2.Radius - compensationParam.Size : arc2.Radius + compensationParam.Size;
            if (isOutside)
            {
                r1 = arc1.Clockwise ? arc1.Radius + compensationParam.Size : arc1.Radius - compensationParam.Size;
                r2 = arc2.Clockwise ? arc2.Radius + compensationParam.Size : arc2.Radius - compensationParam.Size;
            }
            UnitPoint point1 = HitUtil.GetLinePointByDistance(arc1.Center, p2.Point, r1, true);
            UnitPoint point2 = HitUtil.GetLinePointByDistance(arc2.Center, p2.Point, r2, true);
            if (HitUtil.Distance(point1, point2) < errorRange)
            {
                retPoints.Add(new UnitPointBulge(point2, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                return retPoints;
            }

            var intersects = DrawingOperationHelper.GetIntersectPointBy2Circle(arc1.Center, r1, arc2.Center, r2);
            var validPoint = new UnitPoint(double.NaN, double.NaN);
            UnitPoint intersecteArcPoint = new UnitPoint(double.NaN, double.NaN);
            //筛选有交点,如果有两个点就选距离p2近的点
            if (intersects.Count == 1) { validPoint = intersects[0]; }
            else if (intersects.Count == 2)
            {
                validPoint = HitUtil.Distance(intersects[0], p2.Point) < HitUtil.Distance(intersects[1], p2.Point) ? intersects[0] : intersects[1];
            }

            bool closewise = HitUtil.IsClockwiseByCross(p2.Point, point1, point2);
            if (validPoint.IsEmpty)
            {
                if (arc1.Clockwise != closewise || arc2.Clockwise != closewise)//求两个圆弧的切线的交点
                {
                    var line1 = DrawingOperationHelper.GetLineEquationByVerticalLine(point1, arc1.Center);
                    var line2 = DrawingOperationHelper.GetLineEquationByVerticalLine(point2, arc2.Center);
                    intersecteArcPoint = DrawingOperationHelper.GetIntersectionPointBy2Line(line1.Item1, line1.Item2, line1.Item3, line2.Item1, line2.Item2, line2.Item3);
                }
            }
            bool completedSmooth = false;//是否完成圆角处理
            if (compensationParam.IsSmooth)//圆角处理
            {
                bool needSmooth = false;
                if (!validPoint.IsEmpty)
                {
                    double lineAngle = HitUtil.LineAngleR(arc1.Center, validPoint, 0);
                    bool pointInArc = HitUtil.IsPointInArc(HitUtil.RadiansToDegrees(lineAngle), HitUtil.RadiansToDegrees(arc1.StartAngle), HitUtil.RadiansToDegrees(arc1.EndAngle), arc1.Clockwise);
                    needSmooth = !pointInArc;
                }
                if (!intersecteArcPoint.IsEmpty || needSmooth)
                {
                    double bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(p2.Point, point1, point2, closewise);
                    retPoints.Add(new UnitPointBulge(point1, bulge, hasMicroConn: p2.HasMicroConn, position: -1 - indexCur));
                    retPoints.Add(new UnitPointBulge(point2, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                    completedSmooth = true;
                }
            }
            if (!completedSmooth)
            {
                if (!validPoint.IsEmpty)
                {
                    retPoints.Add(new UnitPointBulge(validPoint, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                    return retPoints;
                }
                else if (!intersecteArcPoint.IsEmpty)
                {
                    retPoints.Add(new UnitPointBulge(point1, position: indexCur));
                    retPoints.Add(new UnitPointBulge(intersecteArcPoint, position: indexCur));
                    retPoints.Add(new UnitPointBulge(point2, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                }
                else
                {
                    retPoints.Add(new UnitPointBulge(point1, position: indexCur));
                    retPoints.Add(new UnitPointBulge(p2.Point, position: indexCur));
                    retPoints.Add(new UnitPointBulge(point2, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                }
            }
            return retPoints;
        }
        /// <summary>
        /// 直线到圆弧，补偿求交点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="indexCur"></param>
        /// <returns></returns>
        private static List<UnitPointBulge> CalCompensationsByLineArc(UnitPointBulge p1, UnitPointBulge p2, UnitPointBulge p3, int indexCur, CompensationModel compensationParam, bool isOutside)
        {
            var retPoints = new List<UnitPointBulge>();
            ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p2.Point, p3.Point, (float)p2.Bulge);
            var cPoints = GetCompensationsPoint(p1, p2, compensationParam.Size, isOutside);
            double r = arc.Clockwise ? arc.Radius - compensationParam.Size : arc.Radius + compensationParam.Size;
            if (isOutside) r = arc.Clockwise ? arc.Radius + compensationParam.Size : arc.Radius - compensationParam.Size;
            UnitPoint point1 = HitUtil.GetLinePointByDistance(arc.Center, p2.Point, r, true);
            //如果两点距离在误差范围内，当做同一个点处理
            if (HitUtil.Distance(point1, cPoints.Item2.Point) < errorRange)
            {
                retPoints.Add(new UnitPointBulge(point1, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                return retPoints;
            }
            var line = DrawingOperationHelper.GetLineEquation(cPoints.Item1.Point, cPoints.Item2.Point);
            var intersects = DrawingOperationHelper.GetIntersectPointByLineAndCircle(line.Item1, line.Item2, line.Item3, arc.Center, r);
            var validPoint = new UnitPoint(double.NaN, double.NaN);
            UnitPoint intersecteArcPoint = new UnitPoint(double.NaN, double.NaN);
            //筛选有交点,如果有两个点就选距离p2近的点
            if (intersects.Count == 1)
            {
                validPoint = intersects[0];
            }
            else if (intersects.Count == 2)
            {
                validPoint = HitUtil.Distance(intersects[0], p2.Point) < HitUtil.Distance(intersects[1], p2.Point) ? intersects[0] : intersects[1];
            }
            if (validPoint.IsEmpty)//|| !HitUtil.IsPointInLine(cPoints.Item1.Point, cPoints.Item2.Point, validPoint, errorRange))
            {
                //求圆弧的切线与直线的交点
                if (arc.Clockwise != HitUtil.IsClockwiseByCross(p2.Point, cPoints.Item2.Point, point1))
                {
                    var l1 = DrawingOperationHelper.GetLineEquationByVerticalLine(point1, p2.Point);
                    intersecteArcPoint = DrawingOperationHelper.GetIntersectionPointBy2Line(l1.Item1, l1.Item2, l1.Item3, line.Item1, line.Item2, line.Item3);
                }
            }
            bool completedSmooth = false;//是否完成圆角处理
            if (compensationParam.IsSmooth)//圆角处理
            {
                if (!intersecteArcPoint.IsEmpty ||
                    (!validPoint.IsEmpty && !HitUtil.IsPointInLine(cPoints.Item1.Point, cPoints.Item2.Point, validPoint, errorRange)))
                {
                    bool closewise = HitUtil.IsClockwiseByCross(cPoints.Item1.Point, cPoints.Item2.Point, p2.Point);
                    double bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(p2.Point, cPoints.Item2.Point, point1, closewise);
                    retPoints.Add(new UnitPointBulge(cPoints.Item2.Point, bulge, hasMicroConn: p2.HasMicroConn, position: -1 - indexCur));
                    retPoints.Add(new UnitPointBulge(point1, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                    completedSmooth = true;
                }
            }
            if (!completedSmooth)
            {
                if (!intersecteArcPoint.IsEmpty)
                {
                    retPoints.Add(new UnitPointBulge(intersecteArcPoint, position: indexCur));
                    retPoints.Add(new UnitPointBulge(point1, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                }
                else if (!validPoint.IsEmpty)
                {
                    retPoints.Add(new UnitPointBulge(validPoint, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                }
                else
                {
                    retPoints.Add(new UnitPointBulge(cPoints.Item2.Point, position: indexCur));
                    retPoints.Add(new UnitPointBulge(p2.Point, position: indexCur));
                    retPoints.Add(new UnitPointBulge(point1, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                }
            }
            return retPoints;
        }
        /// <summary>
        /// 圆弧到直线，补偿求交点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="indexCur"></param>
        /// <returns></returns>
        private static List<UnitPointBulge> CalCompensationsByArcLine(UnitPointBulge p1, UnitPointBulge p2, UnitPointBulge p3, int indexCur, CompensationModel compensationParam, bool isOutside)
        {
            var retPoints = new List<UnitPointBulge>();
            ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
            var cPoints = GetCompensationsPoint(p2, p3, compensationParam.Size, isOutside);
            double r = arc.Clockwise ? arc.Radius - compensationParam.Size : arc.Radius + compensationParam.Size;
            if (isOutside) r = arc.Clockwise ? arc.Radius + compensationParam.Size : arc.Radius - compensationParam.Size;
            UnitPoint point1 = HitUtil.GetLinePointByDistance(arc.Center, p2.Point, r, true);
            //如果两点距离在误差范围内，当做同一个点处理
            if (HitUtil.Distance(point1, cPoints.Item1.Point) < errorRange)
            {
                retPoints.Add(new UnitPointBulge(point1, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                return retPoints;
            }
            var line = DrawingOperationHelper.GetLineEquation(cPoints.Item1.Point, cPoints.Item2.Point);
            var intersects = DrawingOperationHelper.GetIntersectPointByLineAndCircle(line.Item1, line.Item2, line.Item3, arc.Center, r);
            var validPoint = new UnitPoint(double.NaN, double.NaN);
            UnitPoint intersecteArcPoint = new UnitPoint(double.NaN, double.NaN);
            //筛选有交点,如果有两个点就选距离p2近的点
            if (intersects.Count == 1)
            {
                validPoint = intersects[0];
            }
            else if (intersects.Count == 2)
            {
                validPoint = HitUtil.Distance(intersects[0], p2.Point) < HitUtil.Distance(intersects[1], p2.Point) ? intersects[0] : intersects[1];
            }

            if (validPoint.IsEmpty) //|| !HitUtil.IsPointInLine(cPoints.Item1.Point, cPoints.Item2.Point, validPoint, errorRange))
            {
                //求圆弧的切线的交点
                if (arc.Clockwise != HitUtil.IsClockwiseByCross(p2.Point, point1, cPoints.Item1.Point))
                {
                    var l1 = DrawingOperationHelper.GetLineEquationByVerticalLine(point1, p2.Point);
                    intersecteArcPoint = DrawingOperationHelper.GetIntersectionPointBy2Line(l1.Item1, l1.Item2, l1.Item3, line.Item1, line.Item2, line.Item3);
                }
            }
            bool completedSmooth = false;//是否完成圆角处理
            if (compensationParam.IsSmooth)//圆角处理
            {
                if (!intersecteArcPoint.IsEmpty ||
                   (!validPoint.IsEmpty && !HitUtil.IsPointInLine(cPoints.Item1.Point, cPoints.Item2.Point, validPoint, errorRange)))
                {
                    bool closewise = HitUtil.IsClockwiseByCross(p2.Point, cPoints.Item1.Point, cPoints.Item2.Point);
                    double bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(p2.Point, point1, cPoints.Item1.Point, closewise);
                    retPoints.Add(new UnitPointBulge(point1, bulge, hasMicroConn: p2.HasMicroConn, position: -1 - indexCur));
                    retPoints.Add(new UnitPointBulge(cPoints.Item1.Point, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                    completedSmooth = true;
                }
            }
            if (!completedSmooth)
            {
                if (!intersecteArcPoint.IsEmpty)
                {
                    retPoints.Add(new UnitPointBulge(point1, position: indexCur));
                    retPoints.Add(new UnitPointBulge(intersecteArcPoint, position: indexCur));
                }
                else if (!validPoint.IsEmpty)
                {
                    retPoints.Add(new UnitPointBulge(validPoint, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                    return retPoints;
                }
                else
                {
                    retPoints.Add(new UnitPointBulge(point1, position: indexCur));
                    retPoints.Add(new UnitPointBulge(p2.Point, position: indexCur));
                    retPoints.Add(new UnitPointBulge(cPoints.Item1.Point, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                }
            }
            return retPoints;
        }
        /// <summary>
        /// 直线与直线，补偿求交点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="indexCur"></param>
        /// <returns></returns>
        private static List<UnitPointBulge> CalCompensationsBy2Line(UnitPointBulge p1, UnitPointBulge p2, UnitPointBulge p3, int indexCur, CompensationModel compensationParam, bool isOutside)
        {
            var retPoints = new List<UnitPointBulge>();
            var cPoints1 = GetCompensationsPoint(p1, p2, compensationParam.Size, isOutside);
            var cPoints2 = GetCompensationsPoint(p2, p3, compensationParam.Size, isOutside);
            //如果两点距离在误差范围内，当做同一个点处理
            if (HitUtil.Distance(cPoints1.Item2.Point, cPoints2.Item1.Point) < errorRange)
            {
                retPoints.Add(new UnitPointBulge(cPoints1.Item2.Point, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                return retPoints;
            }
            var line1 = DrawingOperationHelper.GetLineEquation(cPoints1.Item1.Point, cPoints1.Item2.Point);
            var line2 = DrawingOperationHelper.GetLineEquation(cPoints2.Item1.Point, cPoints2.Item2.Point);
            UnitPoint point = DrawingOperationHelper.GetIntersectionPointBy2Line(line1.Item1, line1.Item2, line1.Item3, line2.Item1, line2.Item2, line2.Item3);
            if (double.IsNaN(point.X) || double.IsNaN(point.Y))
            {
                //平行
                retPoints.Add(new UnitPointBulge(cPoints1.Item2.Point, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                return retPoints;
            }
            else
            {
                bool pointInLine = HitUtil.IsPointInLine(cPoints1.Item1.Point, cPoints1.Item2.Point, point, errorRange);
                if (compensationParam.IsSmooth && !pointInLine)
                {
                    //圆角处理，转换为带弧度的点
                    bool closewise = HitUtil.IsClockwiseByCross(cPoints1.Item1.Point, cPoints1.Item2.Point, p2.Point);
                    double bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(p2.Point, cPoints1.Item2.Point, cPoints2.Item1.Point, closewise);
                    retPoints.Add(new UnitPointBulge(cPoints1.Item2.Point, bulge, hasMicroConn: p2.HasMicroConn, position: -1 - indexCur));
                    retPoints.Add(new UnitPointBulge(cPoints2.Item1.Point, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                    return retPoints;
                }
                else
                {
                    retPoints.Add(new UnitPointBulge(point, p2.Bulge, hasMicroConn: p2.HasMicroConn, position: indexCur));
                    return retPoints;
                }
            }
        }
        /// <summary>
        /// 得到直线或者圆弧的标准补偿点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="isOutside"></param>
        /// <returns></returns>
        public static Tuple<UnitPointBulge, UnitPointBulge> GetCompensationsPoint(UnitPointBulge p1, UnitPointBulge p2, double size, bool isOutside, bool isUpdateBulge = false)
        {
            UnitPointBulge up1 = new UnitPointBulge();
            UnitPointBulge up2 = new UnitPointBulge();
            if (!double.IsNaN(p1.Bulge))//圆弧
            {
                var arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                double r = arc.Clockwise ? arc.Radius - size : arc.Radius + size;
                if (isOutside) r = arc.Clockwise ? arc.Radius + size : arc.Radius - size;
                if (r > 0)
                {
                    UnitPoint point1 = HitUtil.GetLinePointByDistance(arc.Center, p1.Point, r, true);
                    UnitPoint point2 = HitUtil.GetLinePointByDistance(arc.Center, p2.Point, r, true);
                    up1 = new UnitPointBulge() { Point = point1, Bulge = p1.Bulge, HasMicroConn = p1.HasMicroConn };
                    up2 = new UnitPointBulge() { Point = point2, Bulge = p2.Bulge, HasMicroConn = p2.HasMicroConn };
                    if (isUpdateBulge)
                    {
                        up1.Bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arc.Center, up1.Point, up2.Point, arc.Clockwise);
                    }
                }
            }
            else //直线
            {
                var line1d1 = DrawingOperationHelper.GetLinePointByVerticalLine(p1.Point, p2.Point, size);
                var line1d2 = DrawingOperationHelper.GetLinePointByVerticalLine(p2.Point, p1.Point, size);
                UnitPoint point1 = !isOutside ? line1d1.Item2 : line1d1.Item1;
                UnitPoint point2 = !isOutside ? line1d2.Item1 : line1d2.Item2;
                up1 = new UnitPointBulge() { Point = point1, Bulge = p1.Bulge, HasMicroConn = p1.HasMicroConn };
                up2 = new UnitPointBulge() { Point = point2, Bulge = p2.Bulge, HasMicroConn = p2.HasMicroConn };
            }
            
            return Tuple.Create(up1, up2);
        }


    }
}
