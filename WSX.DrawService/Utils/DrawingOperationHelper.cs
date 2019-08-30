using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Media;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawModel;
using WSX.DrawService.DrawTool;
using WSX.DrawService.DrawTool.Arcs;
using WSX.DrawService.DrawTool.CircleTool;
using WSX.DrawService.DrawTool.MultiSegmentLine;

namespace WSX.DrawService.Utils
{
    public class DrawingOperationHelper
    {
        /// <summary>
        /// 计算当前鼠标点在多段线的第几段线之上
        /// </summary>
        /// <param name="mousePoint">当前鼠标点</param>
        /// <returns>当前鼠标点所做当段线段数</returns>
        public static int GetPointInLineIndex(MultiSegmentLineBase multiSegmentLineBase, UnitPoint mousePoint)
        {
            float thWidth = UCCanvas.GetThresholdWidth();
            for (int i = 0; i < multiSegmentLineBase.PointCount - 1; i++)
            {
                if (double.IsNaN(multiSegmentLineBase.Points[i].Bulge))
                {
                    if (HitUtil.IsPointInLine(multiSegmentLineBase.Points[i].Point, multiSegmentLineBase.Points[i + 1].Point, mousePoint, thWidth))
                        return i;
                }
                else
                {
                    UnitPoint midPoint = BulgeHelper.GetBulgeMidPoint(multiSegmentLineBase.Points[i].Point, multiSegmentLineBase.Points[i + 1].Point, multiSegmentLineBase.Points[i].Bulge);
                    ArcModelMini arcModel = HitUtil.GetArcParametersFromThreePoints(multiSegmentLineBase.Points[i].Point, midPoint, multiSegmentLineBase.Points[i + 1].Point);
                    if (HitUtil.IsPointOnArc(mousePoint, thWidth, arcModel))
                        return i;
                }
            }
            if (multiSegmentLineBase.IsCloseFigure)
            {
                if (double.IsNaN(multiSegmentLineBase.Points[multiSegmentLineBase.PointCount - 1].Bulge))
                {
                    if (HitUtil.IsPointInLine(multiSegmentLineBase.Points[multiSegmentLineBase.PointCount - 1].Point, multiSegmentLineBase.Points[0].Point, mousePoint, thWidth))
                        return multiSegmentLineBase.PointCount - 1;
                }
                else
                {
                    UnitPoint midPoint = BulgeHelper.GetBulgeMidPoint(multiSegmentLineBase.Points[multiSegmentLineBase.PointCount - 1].Point, multiSegmentLineBase.Points[0].Point, multiSegmentLineBase.Points[multiSegmentLineBase.PointCount - 1].Bulge);
                    ArcModelMini arcModel = HitUtil.GetArcParametersFromThreePoints(multiSegmentLineBase.Points[multiSegmentLineBase.PointCount - 1].Point, midPoint, multiSegmentLineBase.Points[0].Point);
                    if (HitUtil.IsPointOnArc(mousePoint, thWidth, arcModel))
                        return multiSegmentLineBase.PointCount - 1;
                }
            }
            return -1;
        }

        public static float GetPercentOnLineByLength(List<UnitPointBulge> points, bool isCloseFigure, float len)
        {
            float result = 0;
            double distance = 0, sumLen = 0;
            int nextIndex = 0;
            for (int i = 0; i < points.Count; i++)
            {
                nextIndex = (isCloseFigure && i + 1 == points.Count) ? 0 : i + 1;
                if (double.IsNaN(points[i].Bulge))
                {
                    distance = HitUtil.Distance(points[i].Point, points[nextIndex].Point);
                }
                else
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(points[i].Point, points[nextIndex].Point, (float)points[i].Bulge);
                    distance = Math.Abs(2 * Math.PI * arc.Radius * arc.SweepAngle / 360.0);
                }
                sumLen += distance;
                if (sumLen >= len)
                {
                    result = (float)((len - (sumLen - distance)) / distance + i);
                    break;
                }
            }
            return result;
        }

        public static float GetLengthByPositionInPolyLine(List<UnitPointBulge> points, bool isCloseFigure, float percent)
        {
            double distance = 0;
            int nextIndex = 0, i = 0;
            Tuple<int, float> temp = HitUtil.GetSegmentAndPercent(percent);
            for (; i < temp.Item1; i++)
            {
                nextIndex = (isCloseFigure && i + 1 == points.Count) ? 0 : i + 1;
                if (double.IsNaN(points[i].Bulge))
                {
                    distance += HitUtil.Distance(points[i].Point, points[nextIndex].Point);
                }
                else
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(points[i].Point, points[nextIndex].Point, (float)points[i].Bulge);
                    distance += Math.Abs(2 * Math.PI * arc.Radius * arc.SweepAngle / 360.0);
                }
            }
            nextIndex = (isCloseFigure && i + 1 == points.Count) ? 0 : i + 1;
            if (nextIndex < points.Count)
            {
                if (double.IsNaN(points[i].Bulge))
                {
                    distance += HitUtil.Distance(points[i].Point, points[nextIndex].Point) * temp.Item2;
                }
                else
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(points[i].Point, points[nextIndex].Point, (float)points[i].Bulge);
                    distance += Math.Abs(2 * Math.PI * arc.Radius * arc.SweepAngle / 360.0) * temp.Item2;
                }
            }
            return (float)distance;
        }

        public static UnitPoint GetLeadLineEndPoint(UnitPoint intersectPoint, float lineAngle, float leadLineAngle, float length)
        {
            UnitPoint result = new UnitPoint();
            leadLineAngle = (float)lineAngle - leadLineAngle;
            leadLineAngle = leadLineAngle < 0 ? (float)(leadLineAngle + Math.PI) : leadLineAngle;
            result = HitUtil.PointOnCircle(intersectPoint, length, leadLineAngle);
            return result;
        }

        /// <summary>
        /// 计算引线的角度
        /// </summary>
        /// <param name="p1">直线的起始端点</param>
        /// <param name="p2">直线的结束端点</param>
        /// <param name="intersectPoint">直线与引线的交点</param>
        /// <param name="endPoint">引线的起始端点</param>
        /// <returns>引线与直线的夹角</returns>
        public static float GetLeadLineAngle(UnitPoint p1, UnitPoint p2, UnitPoint intersectPoint, UnitPoint endPoint)
        {
            double angle = 0;
            double epsilon = 0.000001;
            double length = 10;
            double angleR = HitUtil.LineAngleR(p1, p2, 0);
            bool direction = false;
            if (Math.Abs(p1.X - intersectPoint.X) < epsilon && Math.Abs(p1.Y - intersectPoint.Y) < epsilon)
            {
                p1 = HitUtil.LineEndPoint(p1, angleR, length);
                direction = HitUtil.IsClockwiseByCross(endPoint, intersectPoint, p1);
                angle = BulgeHelper.CalTwoLinesAngleFromThreePoints(endPoint, intersectPoint, p1);
            }
            else if (Math.Abs(p2.X - intersectPoint.X) < epsilon && Math.Abs(p2.Y - intersectPoint.Y) < epsilon)
            {
                p2 = HitUtil.LineEndPoint(p2, angleR, length);
                direction = HitUtil.IsClockwiseByCross(endPoint, intersectPoint, p2);
                angle = BulgeHelper.CalTwoLinesAngleFromThreePoints(endPoint, intersectPoint, p2);
            }
            else
            {
                direction = HitUtil.IsClockwiseByCross(endPoint, intersectPoint, p2);
                angle = BulgeHelper.CalTwoLinesAngleFromThreePoints(endPoint, intersectPoint, p2);
            }
            if (!direction)
            {
                angle = Math.PI - angle;
            }
            return (float)angle;
        }

        public static float GetLeadLineAngleArc(UnitPoint p1, UnitPoint intersectPoint, UnitPoint center, bool arcDirection, out bool isInnerCut)
        {
            Tuple<UnitPoint, UnitPoint> result = GetLinePointByVerticalLine(intersectPoint, center, 1);
            bool directionA = HitUtil.IsClockwiseByCross(center, intersectPoint, result.Item1);
            bool directionB = HitUtil.IsClockwiseByCross(center, intersectPoint, result.Item2);
            UnitPoint rightPoint = directionA != arcDirection ? result.Item1 : result.Item2;
            double angle = BulgeHelper.CalTwoLinesAngleFromThreePoints(p1, intersectPoint, rightPoint);
            bool direction = HitUtil.IsClockwiseByCross(p1, intersectPoint, rightPoint);
            isInnerCut = direction == arcDirection ? false : true;
            //if (direction)
            //{
            //    angle = Math.PI * 2 - angle;
            //}
            return (float)angle;
        }

        /// <summary>
        /// 计算圆弧上一点在圆弧上位置的百分比值
        /// </summary>
        /// <param name="arcModelMini">圆弧参数</param>
        /// <param name="pointOnArc">圆弧上一点</param>
        /// <returns></returns>
        public static float GetPercentInArcByPoint(ArcModelMini arcModelMini, UnitPoint pointOnArc)
        {
            float pos = 0f;
            double angle = HitUtil.LineAngleR(arcModelMini.Center, pointOnArc, 0);
            angle = HitUtil.CalAngleSweep(HitUtil.RadiansToDegrees(arcModelMini.StartAngle), HitUtil.RadiansToDegrees(angle), arcModelMini.Clockwise);
            pos = (float)(angle / arcModelMini.SweepAngle);
            return pos;
        }

        /// <summary>
        /// 计算圆弧参数，根据圆弧上面2点及凸度角
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="bulge"></param>
        /// <returns></returns>
        public static ArcModelMini GetArcParametersFromBulge(UnitPoint p1, UnitPoint p2, float bulge)
        {
            ArcModelMini arcModel = new ArcModelMini();
            arcModel.Center = BulgeHelper.GetCenterByBulgeAndTwoPoints(p1, p2, bulge);
            arcModel.Radius = (float)HitUtil.Distance(arcModel.Center, p1);
            arcModel.StartAngle = (float)HitUtil.LineAngleR(arcModel.Center, p1, 0);
            float endAngle = (float)HitUtil.LineAngleR(arcModel.Center, p2, 0);
            arcModel.EndAngle = endAngle;
            arcModel.Clockwise = bulge >= 0 ? false : true;
            endAngle = (float)HitUtil.RadiansToDegrees(endAngle);
            arcModel.SweepAngle = (float)HitUtil.CalAngleSweep((float)HitUtil.RadiansToDegrees(arcModel.StartAngle), endAngle, arcModel.Clockwise);
            return arcModel;
        }

        /// <summary>
        /// 根据百分比计算其在直线上百分比处点的坐标值
        /// </summary>
        /// <param name="percent">百分比</param>
        /// <param name="p1">起点</param>
        /// <param name="p2">终点</param>
        /// <returns>百分比点的坐标</returns>
        public static UnitPoint GetPositionByPercentInLine(float percent, UnitPoint p1, UnitPoint p2)
        {
            UnitPoint result = new UnitPoint();
            double angle = HitUtil.LineAngleR(p1, p2, 0);
            double length = HitUtil.Distance(p1, p2);
            result = HitUtil.LineEndPoint(p1, angle, length * percent);
            return result;
        }

        public static UnitPoint GetPositionByPercentInArc(float percent, UnitPoint p1, UnitPoint p2, float bulge)
        {
            ArcModelMini arcModelMini = DrawingOperationHelper.GetArcParametersFromBulge(p1, p2, bulge);
            float angle = percent * arcModelMini.SweepAngle;
            angle = arcModelMini.StartAngle + (float)HitUtil.DegreesToRadians(angle);
            return HitUtil.PointOnCircle(arcModelMini.Center, arcModelMini.Radius, angle);
        }

        public static UnitPoint GetPostitonByPercentInPolyLine(float percent, UnitPointBulge p1, UnitPointBulge p2)
        {
            percent = HitUtil.GetSegmentAndPercent(percent).Item2;
            if (double.IsNaN(p1.Bulge))
            {
                return GetPositionByPercentInLine(percent, p1.Point, p2.Point);
            }
            else
            {
                return GetPositionByPercentInArc(percent, p1.Point, p2.Point, (float)p1.Bulge);
            }
        }

        #region 求圆弧与直线的倒角中心点坐标
        /// <summary>
        /// 根据直线方程的ax+by+c=0,和圆的标准方程(x-c)^2 + (y-d)^2 = r^2，求交点坐标p1,p2
        /// </summary>
        /// <param name="la">直线系数a</param>
        /// <param name="lb">直线系数b</param>
        /// <param name="lc">直线系数c</param>
        /// <param name="center">圆心坐标</param>
        /// <param name="radius">半径</param>
        /// <returns></returns>
        public static List<UnitPoint> GetIntersectPointByLineAndCircle(double la, double lb, double lc, UnitPoint center, double r)
        {
            //求解过程:
            //直线与圆求交点坐标公式,解方程组
            //直线：y = kx + b;ax+by+c=0 => y = -a/b*x - c/b;
            //圆：(x - c) ^ 2 + (y - d) ^ 2 = r ^ 2  => c为圆心x坐标，d为圆心的y坐标，r为半径
            //有两组解
            //x1= -((√[(k * k + 1)*r*r - c*c*k*k + (2*c* d + 2*b* c)*k - d* d -2b* d -b* b]+(d+b)*k + c ) / (k* k + 1))
            //y1= -((k*(√[(k*k*r*r +r*r-c*c*k*k+2*c*d*k +2*b*ck-d*d -2b*d-b*b)]+c) + d*k*k - b)/(k*k+1))
            //x2= ([(k * k + 1) * r * r - c * c * k * k + (2 * c * d + 2 * b * c) * k - d * d - 2 * b * d - b * b)] + (-d - b) * k - c) / (k * k + 1);
            //y2= -((k * (c - √[k * k * r * r + r * r - c * c * k * k + 2 * c * d * k + 2 * b * c * k - d * d - 2 * b * d - b * b)]) + d * k * k - b) / (k * k + 1))

            List<UnitPoint> rets = new List<UnitPoint>();
            double k, b, tx, ty, c = -center.X, d = -center.Y;
            if (lb == 0)
            {

                double F = d * d - r * r + (-lc / la - center.X) * (-lc / la - center.X);
                double x1 = -lc / la;
                double x2 = -lc / la;
                double y1 = Math.Sqrt(r * r - (-lc / la - center.X) * (-lc / la - center.X)) + center.Y;
                double y2 = -Math.Sqrt(r * r - (-lc / la - center.X) * (-lc / la - center.X)) + center.Y;
                if (!double.IsNaN(y1) && !double.IsNaN(y2))
                {
                    UnitPoint p1 = new UnitPoint(x1, y1);
                    UnitPoint p2 = new UnitPoint(x2, y2);
                    rets.Add(p1);
                    if (!p2.Equals(p1))
                    {
                        rets.Add(p2);
                    }
                }
            }
            else
            {
                k = -la / lb;
                b = -lc / lb;
                tx = Math.Sqrt(((k * k + 1) * r * r - c * c * k * k + (2 * c * d + 2 * b * c) * k - d * d - 2 * b * d - b * b));
                ty = Math.Sqrt((k * k * r * r + r * r - c * c * k * k + 2 * c * d * k + 2 * b * c * k - d * d - 2 * b * d - b * b));
                if (!double.IsNaN(tx) && !double.IsNaN(ty))
                {
                    //第一组解
                    double x1 = -((tx + (d + b) * k + c) / (k * k + 1));
                    double y1 = -((k * (ty + c) + d * k * k - b) / (k * k + 1));

                    //第二组解
                    double x2 = (tx + (-d - b) * k - c) / (k * k + 1);
                    double y2 = -((k * (c - ty) + d * k * k - b) / (k * k + 1));

                    UnitPoint p1 = new UnitPoint(x1, y1);
                    UnitPoint p2 = new UnitPoint(x2, y2);
                    rets.Add(p1);
                    if (!p2.Equals(p1))
                    {
                        rets.Add(p2);
                    }
                }
            }

            return rets;
        }
        /// <summary>
        /// 获取圆弧与直线的倒角圆心坐标
        /// </summary>
        /// <param name="A">直线A点</param>
        /// <param name="B">直线B点（为圆弧与直线的交点）</param>
        /// <param name="center">圆弧中心点</param>
        /// <param name="r">圆弧半径</param>
        /// <param name="d">倒角距离（半径）</param>
        /// <returns>返回4个距离点</returns>
        //public static Tuple<UnitPoint, UnitPoint, UnitPoint> GetCenterChamferingByLineArc(UnitPoint A, UnitPoint B, ArcModelMini arcModelMini, double d)
        public static List<UnitPoint> GetCenterChamferingByLineArc(UnitPoint A, UnitPoint B, ArcModelMini arcModelMini, double d)
        {
            //已知：两点A(Ax, Ay),B(Bx, By)，,圆弧C1(Cx, Cy)坐标，倒角半径d,圆弧C1半径r,求倒角圆心坐标。
            //1.求平行与直线AB,且距离为r的直线方程,得到两条直线方程L1,L2
            //2.求圆心为C1(Cx, Cy)的圆弧,半径为R + r,的一般方程公式C
            //3.求L1,L2与C交点，默认可能有p1,p2,p3,p4四个交点，可能不存在（不考虑）
            //4.求p1,p2,p3,p4分别与圆弧C1的交点坐标，也就是圆弧的倒角切点坐标，判断是否在圆弧上，如果在，则P有效,否则丢弃。
            //5.最后p1,p2,p3,p4中有效的点就是倒角圆心坐标。

            //直线AB的系数a,b,c
            double a = B.Y - A.Y;
            double b = A.X - B.X;
            double c = B.X * A.Y - A.X * B.Y;

            //距离d到直线AB的系数c1,c2
            double c1 = c + d * Math.Sqrt(a * a + b * b);
            double c2 = c - d * Math.Sqrt(a * a + b * b);
            List<UnitPoint> centerPoints = new List<UnitPoint>();
            //两条线与圆1的交点
            double R1 = arcModelMini.Radius + d;
            var L1p = GetIntersectPointByLineAndCircle(a, b, c1, arcModelMini.Center, R1);
            var L2p = GetIntersectPointByLineAndCircle(a, b, c2, arcModelMini.Center, R1);
            centerPoints.AddRange(L1p);
            centerPoints.AddRange(L2p);
            //两条线与圆2的交点
            double R2 = arcModelMini.Radius - d;
            if (R2 > 0)
            {
                var L3p = GetIntersectPointByLineAndCircle(a, b, c1, arcModelMini.Center, R2);
                var L4p = GetIntersectPointByLineAndCircle(a, b, c2, arcModelMini.Center, R2);
                centerPoints.AddRange(L3p);
                centerPoints.AddRange(L4p);
            }
            List<UnitPoint> arcInfo = ExcludeInvalidCenter(centerPoints, A, B, arcModelMini, d);
            //return centerPoints;
            return arcInfo;
        }

        private static List<UnitPoint> ExcludeInvalidCenter(List<UnitPoint> originals, UnitPoint startPoint, UnitPoint endPoint, ArcModelMini arcModelMini, double distance)
        {
            List<UnitPoint> result = new List<UnitPoint>();
            //1.通过直线和圆的切点排除切点不在线段之上的圆
            //(1).求直线和圆的切点坐标   
            List<List<UnitPoint>> tempCuts = new List<List<UnitPoint>>();
            for (int i = 0; i < originals.Count; i++)
            {
                tempCuts.Add(new List<UnitPoint> { HitUtil.NearestPointOnLine(startPoint, endPoint, originals[i]), originals[i] });
            }
            for (int i = tempCuts.Count - 1; i >= 0; i--)
            {
                if (!HitUtil.IsPointInLine(startPoint, endPoint, tempCuts[i][0], UCCanvas.GetThresholdWidth()) ||
                    HitUtil.TwoFloatNumberIsEqual(tempCuts[i][0].X, endPoint.X, 0.01f) && HitUtil.TwoFloatNumberIsEqual(tempCuts[i][0].Y, endPoint.Y, 0.01f)) //此处参数设置为0.01f  避免已经倒角后，再次点击时还能进行倒角
                {
                    tempCuts.RemoveAt(i);
                }
            }
            //2.通过时针方向(直线上切点->圆心->圆与圆切点->直线上切点)，如果与原曲线时针方向不一致则排除或通过切点是否在圆弧上进行排除            
            for (int i = tempCuts.Count - 1; i >= 0; i--)
            {
                UnitPoint circleCutPoint = HitUtil.GetLinePointByDistance(arcModelMini.Center, tempCuts[i][1], arcModelMini.Radius);
                double testAngle = HitUtil.LineAngleR(arcModelMini.Center, circleCutPoint, 0);
                if (!HitUtil.IsPointInArc((float)HitUtil.RadiansToDegrees(testAngle), (float)HitUtil.RadiansToDegrees(arcModelMini.StartAngle), (float)HitUtil.RadiansToDegrees(arcModelMini.StartAngle) + arcModelMini.SweepAngle, arcModelMini.Clockwise))
                {
                    tempCuts.RemoveAt(i);
                }
                else
                {
                    tempCuts[i].Add(circleCutPoint);
                }
            }
            //3.如果上一步排除的结果还多余1个则需要进一步排除,根据直线与圆弧的切点距离直线的端点距离近的保留
            if (tempCuts.Count > 1)
            {
                //double d1 = HitUtil.Distance(tempCuts[0][0], endPoint);
                //double d2 = HitUtil.Distance(tempCuts[1][0], endPoint);
                //if (d1 < d2)
                //{
                //    result.Add(tempCuts[0][0]);
                //    result.Add(tempCuts[0][1]);
                //    result.Add(tempCuts[0][2]);
                //}
                //else
                //{
                //    result.Add(tempCuts[1][0]);
                //    result.Add(tempCuts[1][1]);
                //    result.Add(tempCuts[1][2]);
                //}
                List<UnitPoint> intersectPoints = HitUtil.GetIntersectPointLineWithCircle(startPoint, endPoint, arcModelMini.Center, arcModelMini.Radius, UCCanvas.GetThresholdWidth());
                if (intersectPoints.Count >= 1)
                {
                    double min = double.MaxValue;
                    int index = 0;
                    for (int i = 0; i < tempCuts.Count; i++)
                    {
                        double d = HitUtil.Distance(tempCuts[i][0], startPoint);
                        if (d < min)
                        {
                            min = d;
                            index = i;
                        }
                    }
                    result.Add(tempCuts[index][0]);
                    result.Add(tempCuts[index][1]);
                    result.Add(tempCuts[index][2]);
                }
            }
            else if (tempCuts.Count == 1)
            {
                result.Add(tempCuts[0][0]);
                result.Add(tempCuts[0][1]);
                result.Add(tempCuts[0][2]);
            }
            return result;
        }

        /// <summary>
        /// 获取圆弧与圆弧的倒角圆心坐标
        /// </summary>
        /// <param name="center1">圆弧1的圆心坐标</param>
        /// <param name="r1">圆弧1的半径</param>
        /// <param name="center2">圆弧2的圆心坐标</param>
        /// <param name="r2">圆弧2的半径</param>
        /// <param name="d">倒角距离</param>
        /// <returns></returns>
        public static List<UnitPoint> GetCenterChamferingBy2Arc(UnitPoint center1, double r1, UnitPoint center2, double r2, double d)
        {
            List<UnitPoint> points = new List<UnitPoint>();

            double x1R1 = r1 + d;
            double x1R2 = r2 + d;

            var ret1 = GetIntersectPointBy2Circle(center1, x1R1, center2, x1R2);
            points.AddRange(ret1);

            double x2R1 = r1 - d;
            double x2R2 = r2 + d;

            var ret2 = GetIntersectPointBy2Circle(center1, x2R1, center2, x2R2);
            points.AddRange(ret2);

            double x3R1 = r1 + d;
            double x3R2 = r2 - d;

            var ret3 = GetIntersectPointBy2Circle(center1, x3R1, center2, x3R2);
            points.AddRange(ret3);

            double x4R1 = r1 - d;
            double x4R2 = r2 - d;

            var ret4 = GetIntersectPointBy2Circle(center1, x4R1, center2, x4R2);
            points.AddRange(ret4);

            return points;
        }
        /// <summary>
        /// 求圆与圆的交点坐标
        /// </summary>
        /// <param name="center1">圆1圆心坐标</param>
        /// <param name="r1">圆1半径</param>
        /// <param name="center2">圆2圆心坐标</param>
        /// <param name="r2">圆2半径</param>
        /// <returns></returns>
        public static List<UnitPoint> GetIntersectPointBy2Circle(UnitPoint center1, double r1, UnitPoint center2, double r2)
        {
            List<UnitPoint> retPoints = new List<UnitPoint>();
            UnitPoint[] points = new UnitPoint[2];
            double d, a, b, c, p, q, r, dr;
            double[] cos_value = new double[2];
            double[] sin_value = new double[2];
            if (HitUtil.TwoFloatNumberIsEqual(center1.X, center2.X)
                && HitUtil.TwoFloatNumberIsEqual(center1.Y, center2.Y)
                && HitUtil.TwoFloatNumberIsEqual(r1, r2))
            {
                //两圆相同
                return retPoints;
            }

            d = HitUtil.Distance(center1, center2);
            if (d > r1 + r2 || d < Math.Abs(r1 - r2))
            {
                //不相交
                return retPoints;
            }
            dr = (center1.X - center2.X) * (center1.X - center2.X) + (center1.Y - center2.Y) * (center1.Y - center2.Y);
            a = 2.0 * r1 * (center1.X - center2.X);
            b = 2.0 * r1 * (center1.Y - center2.Y);
            c = r2 * r2 - r1 * r1 - dr;
            p = a * a + b * b;
            q = -2.0 * a * c;
            if (HitUtil.TwoFloatNumberIsEqual(d, r1 + r2) || HitUtil.TwoFloatNumberIsEqual(d, Math.Abs(r1 - r2)))
            {
                cos_value[0] = -q / p / 2.0;
                sin_value[0] = Math.Sqrt(1 - cos_value[0] * cos_value[0]);

                points[0].X = r1 * cos_value[0] + center1.X;
                points[0].Y = r1 * sin_value[0] + center1.Y;

                if (!HitUtil.TwoFloatNumberIsEqual(dr, r2 * r2))
                {
                    points[0].Y = center1.Y - r1 * sin_value[0];
                }
                //两圆相切
                retPoints.Add(points[0]);
                return retPoints;
            }

            r = c * c - b * b;
            cos_value[0] = (Math.Sqrt(q * q - 4.0 * p * r) - q) / p / 2.0;
            cos_value[1] = (-Math.Sqrt(q * q - 4.0 * p * r) - q) / p / 2.0;
            sin_value[0] = Math.Sqrt(1 - cos_value[0] * cos_value[0]);
            sin_value[1] = Math.Sqrt(1 - cos_value[1] * cos_value[1]);

            points[0].X = r1 * cos_value[0] + center1.X;
            points[1].X = r1 * cos_value[1] + center1.X;
            points[0].Y = r1 * sin_value[0] + center1.Y;
            points[1].Y = r1 * sin_value[1] + center1.Y;

            double dp0r2 = (points[0].X - center2.X) * (points[0].X - center2.X) + (points[0].Y - center2.Y) * (points[0].Y - center2.Y);
            if (!HitUtil.TwoFloatNumberIsEqual(dp0r2, r2 * r2))
            {
                points[0].Y = center1.Y - r1 * sin_value[0];
            }
            double dp1r2 = (points[1].X - center2.X) * (points[1].X - center2.X) + (points[1].Y - center2.Y) * (points[1].Y - center2.Y);
            if (!HitUtil.TwoFloatNumberIsEqual(dp1r2, r2 * r2))
            {
                points[1].Y = center1.Y - r1 * sin_value[1];
            }
            if (HitUtil.TwoFloatNumberIsEqual(points[0].Y, points[1].Y) && HitUtil.TwoFloatNumberIsEqual(points[0].X, points[1].X))
            {
                if (points[0].Y > 0)
                {
                    points[1].Y = -points[1].Y;
                }
                else
                {
                    points[0].Y = -points[0].Y;
                }
            }
            retPoints.AddRange(points);
            return retPoints;
        }
        #endregion

        /// <summary>
        ///  求circle1 与 circle2 的外公切线
        /// </summary>
        public static Tuple<UnitPoint,UnitPoint>[] GetTangentLineOfCircle(UnitPoint center1, double radius1, UnitPoint center2, double radius2)
        {
            Tuple<UnitPoint, UnitPoint>[] cantactLines = new Tuple<UnitPoint, UnitPoint>[2];
            UnitPoint[] pointsOfCircle1 = GetTangentPointOfCircle(center1, radius1, center2, radius2);
            UnitPoint[] pointsOfCircle2 = GetTangentPointOfCircle(center2, radius2, center1, radius1);

            if (double.IsNaN(pointsOfCircle1[0].X) && double.IsNaN(pointsOfCircle1[1].X))
            {
                //大圆包含小圆切不相切 无切点
                return null;
            }else if (HitUtil.PointInPoint(pointsOfCircle1[0], pointsOfCircle1[1], (float)LineFlyCut.THRESHOLD) && //UCCanvas.GetThresholdWidth()
                HitUtil.PointInPoint(pointsOfCircle1[0], pointsOfCircle2[0], (float)LineFlyCut.THRESHOLD) &&
                HitUtil.PointInPoint(pointsOfCircle2[0], pointsOfCircle2[1], (float)LineFlyCut.THRESHOLD))
            {
                //几个切点相同 两个圆内切时只有一个切点，没有外公切线
                return null;
            }

            // pointsOfCircle1[0] pointsOfCircle2[0] 与 center1 center2 线段是否相交
            if (HitUtil.LinesIntersect(pointsOfCircle1[0], pointsOfCircle2[0], pointsOfCircle1[1], pointsOfCircle2[1]))
            {
                cantactLines[0] = new Tuple<UnitPoint, UnitPoint>(pointsOfCircle1[0], pointsOfCircle2[1]);
                cantactLines[1] = new Tuple<UnitPoint, UnitPoint>(pointsOfCircle1[1], pointsOfCircle2[0]);
            }
            else
            {
                cantactLines[0] = new Tuple<UnitPoint, UnitPoint>(pointsOfCircle1[0], pointsOfCircle2[0]);
                cantactLines[1] = new Tuple<UnitPoint, UnitPoint>(pointsOfCircle1[1], pointsOfCircle2[1]);
            }
            if (cantactLines == null)
            {
            }
                return cantactLines;
        }

        /// <summary>
        ///  求circle1 与 circle2 的外公切线与circle1的交点坐标
        /// </summary>
        /// <param name="center1">circle1 圆心</param>
        /// <param name="radius1">circle1 半径</param>
        /// <param name="center2">circle2 圆心</param>
        /// <param name="radius2">circle2 半径</param>
        /// <returns></returns>
        public static UnitPoint[] GetTangentPointOfCircle(UnitPoint center1, double radius1, UnitPoint center2, double radius2)
        {
            if(radius1 != radius2)
            {
                return GetTangentPointOfCircleDiffrentRadius(center1, radius1, center2, radius2);
            }else
            {
                Tuple<UnitPoint, UnitPoint> tuplePoints = GetLinePointByVerticalLine(center1, center2, radius2);
                UnitPoint[] points = new UnitPoint[2];
                points[0] = tuplePoints.Item1;
                points[1] = tuplePoints.Item2;
                return points;
            }
        }

        /// <summary>
        ///  (两圆半径不相等的情况下) 求circle1 与 circle2 的外公切线与circle1的交点坐标
        /// </summary>
        /// <param name="center1">circle1 圆心</param>
        /// <param name="radius1">circle1 半径</param>
        /// <param name="center2">circle2 圆心</param>
        /// <param name="radius2">circle2 半径</param>
        /// <returns></returns>
        private static UnitPoint[] GetTangentPointOfCircleDiffrentRadius(UnitPoint center1, double radius1, UnitPoint center2, double radius2)
        {
            //原理参考 https://blog.csdn.net/wangyue4/article/details/5606437
            //令circle1，circle2的外公切线交于点P
            //circle1的圆心为O1,circle2的圆心为O2
            //切线于circle1的两个焦点分别记作A1,A2
            //切线于circle2的两个焦点分别记作B1,B2
            //过点 B1 引 O1O2 的垂线，垂足为 M
            // O1O2 的斜率为k
            //切点

            UnitPoint[] CutPoints = new UnitPoint[2];
            double deltaX = center2.X - center1.X;
            double deltaY = center2.Y - center1.Y;

            double distance_d = 0; // O1 与 O2 距离
            double length_i;       // P 与 O2 距离 
            double length_s;       // P 与 B1 或 B2点的距离
            double length_b1o1;    // B1 到 O1O2的距离
            double length_o2m;     // O2 到 M 的距离
            UnitPoint M = new UnitPoint(); //用于记录 M 点的坐标
            double k; //O1O2 直线方程的斜率k
            double b; //O1O2 直线方程中的常数

            distance_d = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));  //求2点距离
            length_i = distance_d * radius1 / (radius2 - radius1);              //相似三角形特征 对应边等比例
            length_s = Math.Sqrt(Math.Pow(length_i, 2) - Math.Pow(radius1, 2)); //直角三角形
            length_b1o1 = length_s * radius1 / length_i; //相似三角形特征 对应边等比例
            length_o2m = radius1 * radius1 / length_i;   //相似三角形特征 对应边等比例

            M.X = center1.X + length_o2m * -deltaX / distance_d;
            M.Y = center1.Y + length_o2m * -deltaY / distance_d;
            k = (center1.Y - center2.Y) / (center1.X - center2.X);
            b = center1.Y - k * center1.X;

            double Y = 0;
            double X = 0;

            Y = (float)((k * M.X + M.Y * k * k + b + Math.Abs(length_b1o1) * Math.Sqrt(k * k + 1)) / (k * k + 1));
            X = M.X - (Y - M.Y) * k;
            CutPoints[0].X = X;
            CutPoints[0].Y = Y;
            Y = (float)((k * M.X + M.Y * k * k + b - Math.Abs(length_b1o1) * Math.Sqrt(k * k + 1)) / (k * k + 1));
            X = M.X - (Y - M.Y) * k;

            CutPoints[1].X = X;
            CutPoints[1].Y = Y;
            return CutPoints;
        }

        /// <summary>
        /// 求circle1 与 circle2 的内公切线与circle1的交点坐标
        /// </summary>
        /// <param name="center1">circle1 圆心</param>
        /// <param name="radius1">circle1 半径</param>
        /// <param name="center2">circle2 圆心</param>
        /// <param name="radius2">circle2 半径</param>
        /// <returns></returns>
        public static Tuple<UnitPoint, UnitPoint>[] GetInnerTangentLineOfCircle(UnitPoint center1, double radius1, UnitPoint center2, double radius2)
        {
            Tuple<UnitPoint, UnitPoint>[] tangentLines = null;
            Tuple<UnitPoint, UnitPoint> points1 = GetInnerTangentPointOfCircle(center1, radius1, center2, radius2);//与center1的两个切点
            Tuple<UnitPoint, UnitPoint> points2 = GetInnerTangentPointOfCircle(center2, radius2, center1, radius1);//与center2的两个切点

            //两公切线的交点在两圆心O1O2之间  O1P 小于O1O2距离
            UnitPoint intersectPoint = HitUtil.LinesIntersectPoint(points1.Item1, points2.Item1, points1.Item2, points2.Item2);
            if(intersectPoint != UnitPoint.Empty)
            {
                double distanceO1P = HitUtil.Distance(center1, intersectPoint);
                double distanceO1O2 = HitUtil.Distance(center1, center2);
                if(distanceO1P < distanceO1O2)
                {
                    tangentLines = new Tuple<UnitPoint, UnitPoint>[2];
                    tangentLines[0] = new Tuple<UnitPoint, UnitPoint>(points1.Item1, points2.Item1);
                    tangentLines[1] = new Tuple<UnitPoint, UnitPoint>(points1.Item2, points2.Item2);
                    return tangentLines;
                }
            }

            intersectPoint = HitUtil.LinesIntersectPoint(points1.Item1, points2.Item2, points1.Item2, points2.Item1);
            if (intersectPoint != UnitPoint.Empty)
            {
                double distanceO1P = HitUtil.Distance(center1, intersectPoint);
                double distanceO1O2 = HitUtil.Distance(center1, center2);
                if (distanceO1P < distanceO1O2)
                {
                    tangentLines = new Tuple<UnitPoint, UnitPoint>[2];
                    tangentLines[0] = new Tuple<UnitPoint, UnitPoint>(points1.Item1, points2.Item2);
                    tangentLines[1] = new Tuple<UnitPoint, UnitPoint>(points1.Item2, points2.Item1);
                    return tangentLines;
                }
            }

            return tangentLines;
        }

        /// <summary>
        /// 求circle1 与 circle2 的内公切线与circle1的交点(切点)坐标
        /// </summary>
        /// <param name="center1">circle1 圆心</param>
        /// <param name="radius1">circle1 半径</param>
        /// <param name="center2">circle2 圆心</param>
        /// <param name="radius2">circle2 半径</param>
        /// <returns></returns>
        public static Tuple<UnitPoint, UnitPoint> GetInnerTangentPointOfCircle(UnitPoint center1, double radius1, UnitPoint center2, double radius2)
        {
            /*
             设circle1的圆心为O1,circle2的圆心为O2 (也是center1 center2)
             circle1 circle2 的内切线相交于P点
             circle1 circle2 的半径为R r (radius1 radius2)
             第1个圆的内切点为A,A1 第2个圆的内切点为B,B1
             过A点做垂直于O1O2的垂线，垂足为M
            */

            double distanceO1O2 = HitUtil.Distance(center1, center2);//求2点距离
            double distanceO1P = distanceO1O2 * (radius1 / (radius1 + radius2));// 相似三角形
            //double distanceO2P = distanceO1O2 * (radius2 / (radius1 + radius2));//
            UnitPoint pointP = HitUtil.GetLinePointByDistance(center1, center2, distanceO1P, true);
            double distanceO1M = radius1 * radius1 / distanceO1P;//相似三角形
            UnitPoint pointM = HitUtil.GetLinePointByDistance(center1, center2, distanceO1M, true);
            double distanceA1M = Math.Sqrt(radius1 * radius1 - distanceO1M * distanceO1M);// 直角三角形

            //过pointM点垂直pointM pointP 距离distanceA1M求2点
            Tuple<UnitPoint, UnitPoint> tangentPoints = DrawingOperationHelper.GetLinePointByVerticalLine(pointM, pointP, distanceA1M);
            return tangentPoints;
        }

        public static double GetArcLength(double radius, double sweepAngle)
        {
            return Math.Abs(2 * Math.PI * radius * sweepAngle / 360.0);
        }

        /// <summary>
        /// p1,p2,为直线，过p1点的距离为d的垂线上的点的坐标。
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Tuple<UnitPoint, UnitPoint> GetLinePointByVerticalLine(UnitPoint p1, UnitPoint p2, double d)
        {
            //1、已知直线上两点求直线的一般式方程
            //已知直线上的两点P1(X1, Y1) P2(X2, Y2)， P1 P2两点不重合。则直线的一般式方程AX + BY + C = 0中，A B C分别等于：
            //A = Y2 - Y1
            //B = X1 - X2
            //C = X2 * Y1 - X1 * Y2
            //2、过直线外一点P0(x0, y0）的垂线方程：y = (B / A) * (x - x0) + y0=> y =(B / A) * x - x0 * (B / A) + y0
            //3、求直线与垂线的交点
            //x = ((B ^ 2) * x0 - A * B * y0 - A * C) / (A ^ 2 + B ^ 2)
            //y = -(A * x + C) / B

            var lx = DrawingOperationHelper.GetLineEquation(p1, p2);
            double A = lx.Item1;
            double B = lx.Item2;
            double C = lx.Item3;
            if (A == 0)
            {
                double y1 = (Math.Abs(p1.Y) + d) * (p1.Y >= 0 ? 1 : -1);//(Math.Abs(p1.Y) / p1.Y);
                double y2 = (Math.Abs(p1.Y) - d) * (p1.Y >= 0 ? 1 : -1);//(Math.Abs(p1.Y) / p1.Y);
                var point1 = new UnitPoint(p1.X, y1);
                var point2 = new UnitPoint(p1.X, y2);
                if (HitUtil.IsClockwiseByCross(p2, p1, point1))
                {
                    return Tuple.Create(point1, point2);
                }
                else
                {
                    return Tuple.Create(point2, point1);
                }

                //if (C >= 0)
                //{
                //    return Tuple.Create(new UnitPoint(p1.X, y1), new UnitPoint(p1.X, y2));
                //}
                //else
                //{
                //    return Tuple.Create(new UnitPoint(p1.X, y2), new UnitPoint(p1.X, y1));
                //}
            }
            // -(B / A) * x + y + x0 * (B / A) - y0 =0
            double x0 = p1.X;
            double y0 = p1.Y;
            double la = -(B / A);
            double lb = 1;
            double lc = x0 * (B / A) - y0;

            //double 
            double c1 = C + d * Math.Sqrt(A * A + B * B);
            double c2 = C - d * Math.Sqrt(A * A + B * B);

            UnitPoint up1 = DrawingOperationHelper.GetIntersectionPointBy2Line(A, B, c1, la, lb, lc);
            UnitPoint up2 = DrawingOperationHelper.GetIntersectionPointBy2Line(A, B, c2, la, lb, lc);

            return Tuple.Create(up1, up2);
        }

        /// <summary>
        ///求直线方程的一般方程系数AX+BY+C=0，a,b,c.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>AX+BY+C=0的A,B,C系数</returns>
        public static Tuple<double, double, double> GetLineEquation(UnitPoint p1, UnitPoint p2)
        {
            //求解过程：
            //已知直线上的两点P1(X1, Y1) P2(X2, Y2)， P1 P2两点不重合。
            //对于AX+BY+C=0：
            //当x1=x2时，直线方程为x-x1=0
            //当y1=y2时，直线方程为y-y1=0
            //当x1≠x2，y1≠y2时，直线的斜率k=(y2-y1)/(x2-x1)
            //故直线方程为y-y1=(y2-y1)/(x2-x1)×(x-x1)
            //即x2y-x1y-x2y1+x1y1=(y2-y1)x-x1(y2-y1)
            //即(y2-y1)x-(x2-x1)y-x1(y2-y1)+(x2-x1)y1=0
            //即(y2-y1)x+(x1-x2)y+x2y1-x1y2=0 ①
            //可以发现，当x1=x2或y1=y2时，①式仍然成立。所以直线AX+BY+C=0的一般式方程就是：
            //A = Y2 - Y1
            //B = X1 - X2
            //C = X2* Y1 - X1* Y2
            double a = 0, b = 0, c = 0;
            a = p2.Y - p1.Y;
            b = p1.X - p2.X;
            c = p2.X * p1.Y - p1.X * p2.Y;
            return Tuple.Create(a, b, c);
        }
        /// <summary>
        /// 求直线方程的一般方程系数AX+BY+C=0，a,b,c.
        /// </summary>
        /// <param name="p1">过点</param>
        /// <param name="angle">斜角</param>
        /// <returns></returns>
        public static Tuple<double, double, double> GetLineEquation(UnitPoint p1, double angle)
        {
            //点坐标（a，b）
            //y = tan（夹角）*（x - a）+b

            double a = 0, b = 0, c = 0;
            double tanA = Math.Tan(angle);
            if (double.IsInfinity(tanA) || double.IsNaN(tanA))
            {

            }
            else
            {
                a = tanA;
                b = -1;
                c = p1.Y - tanA * p1.X;
            }
            return Tuple.Create(a, b, c);
        }
        /// <summary>
        /// 获的两条直线的交点
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="b1"></param>
        /// <param name="c1"></param>
        /// <param name="a2"></param>
        /// <param name="b2"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static UnitPoint GetIntersectionPointBy2Line(double a1, double b1, double c1, double a2, double b2, double c2)
        {
            double d = (a1 * b2 - a2 * b1);
            if (Math.Abs(d) < 0.000001)
            {
                return new UnitPoint(double.NaN, double.NaN);
            }
            double x = (b1 * c2 - b2 * c1) / d;
            double y = (a2 * c1 - a1 * c2) / d;
            return new UnitPoint(x, y);
        }
        /// <summary>
        /// 求过p1点的垂线方程，系数AX+BY+C=0，a,b,c.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Tuple<double, double, double> GetLineEquationByVerticalLine(UnitPoint p1, UnitPoint p2)
        {
            var lx = DrawingOperationHelper.GetLineEquation(p1, p2);
            double A = lx.Item1;
            double B = lx.Item2;
            double C = lx.Item3;
            double d = 0;
            if (A == 0)
            {
                return Tuple.Create(1d, 0d, -p1.X); ;
            }
            // -(B / A) * x + y + x0 * (B / A) - y0 =0
            double x0 = p1.X;
            double y0 = p1.Y;
            double la = -(B / A);
            double lb = 1;
            double lc = x0 * (B / A) - y0;
            return Tuple.Create(la, lb, lc);
        }
        /// <summary>
        /// 可以用于求线段之间的夹角,center为中间点
        /// </summary>
        /// <param name="first"></param>
        /// <param name="center"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static double Angle(UnitPoint first, UnitPoint center, UnitPoint second)
        {

            double ma_x = first.X - center.X;
            double ma_y = first.Y - center.Y;
            double mb_x = second.X - center.X;
            double mb_y = second.Y - center.Y;
            double v1 = (ma_x * mb_x) + (ma_y * mb_y);
            double ma_val = Math.Sqrt(ma_x * ma_x + ma_y * ma_y);
            double mb_val = Math.Sqrt(mb_x * mb_x + mb_y * mb_y);
            double cosM = v1 / (ma_val * mb_val);
            double angleAMB = Math.Acos(cosM) * 180 / Math.PI;

            return angleAMB;
        }

        /// <summary>
        /// 图形innerObj是否outsideObj全包含
        /// 可处理点、圆、圆弧、多段线(含星型、带角度的矩形)
        /// </summary>
        /// <param name="innerObj"></param>
        /// <param name="outsideObj"></param>
        /// <returns></returns>
        public static bool IsInsideOf(IDrawObject innerObj, IDrawObject outsideObj)
        {
            PathGeometry inner = BuildPathGeometry(innerObj);
            PathGeometry outside = BuildPathGeometry(outsideObj);
            if (inner == null || outside == null)
                return false;
            if (innerObj is SingleDot)
            {
                //点是否被图形包含 用图形包含图形判断不出 改为判断图形是否包含点
                UnitPoint p1 = (innerObj as SingleDot).P1;
                return outside.FillContains(new System.Windows.Point(p1.X, p1.Y), 0.00001, ToleranceType.Relative);
            }
            return outside.FillContains(inner, 0.00001, ToleranceType.Relative);
        }

        public static PathGeometry BuildPathGeometry(IDrawObject draw)
        {
            if (draw == null)
                return null;
            if(draw is SingleDot)
            {
                List<UnitPointBulge> points = new List<UnitPointBulge>()
                {
                    new UnitPointBulge((draw as SingleDot).P1),
                    new UnitPointBulge((draw as SingleDot).P1)
                };
                return BuildPathGeometry(points, true);
            }
            else if (draw is MultiSegmentLineBase)
            {
                MultiSegmentLineBase multiSegment = draw as MultiSegmentLineBase;
                return BuildPathGeometry(multiSegment.Points, true);
            }else if (draw is ArcBase)
            {
                ArcBase arc = draw as ArcBase;
                var radians = HitUtil.DegreesToRadians(arc.AngleSweep);
                double bulge = Math.Sin(radians / 4.0d) / Math.Cos(radians / 4.0d);

                List<UnitPointBulge> points = new List<UnitPointBulge>()
                {
                    new UnitPointBulge(arc.StartMovePoint,bulge),
                    new UnitPointBulge(arc.EndMovePoint)
                };
                return BuildPathGeometry(points, true);
            }
            else if (draw is Circle)
            {
                Circle circle = draw as Circle;

                //相当于2个半圆 取过圆心的水平线与圆的两个交点
                List<UnitPointBulge> points = new List<UnitPointBulge>()
                {
                    new UnitPointBulge(new UnitPoint(circle.Center.X-circle.Radius,circle.Center.Y),1),
                    new UnitPointBulge(new UnitPoint(circle.Center.X+circle.Radius,circle.Center.Y),1),
                    new UnitPointBulge(new UnitPoint(circle.Center.X-circle.Radius,circle.Center.Y)),
                };
                return BuildPathGeometry(points, true);
            }

            //椭圆及其他图形？？

            return null;
        }

        public static bool IsPointInPathGeometry(UnitPoint p1, List<UnitPointBulge> points, bool isClosed = true)
        {
            PathGeometry path = BuildPathGeometry(points, isClosed);
            return path.FillContains(new System.Windows.Point(p1.X, p1.Y), 0.00001, ToleranceType.Relative);
        }

        private static PathGeometry BuildPathGeometry(List<UnitPointBulge> points, bool isClosed)
        {
            PathGeometry path = new PathGeometry();
            PathFigure figures = new PathFigure();
            figures.StartPoint = new System.Windows.Point(points[0].Point.X, points[0].Point.Y);
            figures.IsClosed = isClosed;
            for (int i = 1; i < points.Count; i++)
            {
                UnitPointBulge startPoint = points[i - 1];
                UnitPointBulge endPoint = points[i];
                if (double.IsNaN(startPoint.Bulge))
                {
                    figures.Segments.Add(new LineSegment(new System.Windows.Point(endPoint.Point.X, endPoint.Point.Y), true));
                }
                else
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(startPoint.Point, endPoint.Point, (float)startPoint.Bulge);
                    ArcSegment arcSeg = new ArcSegment();
                    arcSeg.Point = new System.Windows.Point(endPoint.Point.X, endPoint.Point.Y);
                    arcSeg.Size = new System.Windows.Size(arc.Radius, arc.Radius);
                    arcSeg.SweepDirection = !arc.Clockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise;
                    arcSeg.RotationAngle = Math.Abs(arc.SweepAngle);
                    arcSeg.IsLargeArc = Math.Abs(arc.SweepAngle) >= 180;
                    figures.Segments.Add(arcSeg);
                }
            }
            if (isClosed)
            {
                UnitPointBulge startPoint = points[points.Count - 1];
                UnitPointBulge endPoint = points[0];
                if (double.IsNaN(startPoint.Bulge))
                {
                    figures.Segments.Add(new LineSegment(new System.Windows.Point(endPoint.Point.X, endPoint.Point.Y), true));
                }
                else
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(startPoint.Point, endPoint.Point, (float)startPoint.Bulge);
                    ArcSegment arcSeg = new ArcSegment();
                    arcSeg.Point = new System.Windows.Point(endPoint.Point.X, endPoint.Point.Y);
                    arcSeg.Size = new System.Windows.Size(arc.Radius, arc.Radius);
                    arcSeg.SweepDirection = !arc.Clockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise;
                    arcSeg.RotationAngle = Math.Abs(arc.SweepAngle);
                    arcSeg.IsLargeArc = Math.Abs(arc.SweepAngle) >= 180;
                    figures.Segments.Add(arcSeg);
                }
            }

            path.Figures.Add(figures);
            return path;
        }
        /// <summary>
        /// 判断图形与图形是否相交/包含/被包含/空集(true：不相交，目前只实现圆与多段线的封闭图形)
        /// </summary>
        /// <param name="drawObject1">被包含图形</param>
        /// <param name="drawObject2">包含图形</param>
        /// <param name="isClosed">图形是否是封闭的</param>
        /// <param name="intersectionDetail">图形与图形的关系(相交/包含/被包含/空集)</param>
        /// <returns></returns>
        public static bool TwoDrawObjectsIntersectByGeometry(IDrawObject drawObject1, IDrawObject drawObject2, bool isClosed, IntersectionDetail intersectionDetail)
        {
            PathGeometry path1 =null;
            PathGeometry path2 =null;
            if (drawObject1 is Circle)
            {
                var tempDrawObject1 = drawObject1 as Circle;
                 path1 = CircleBulidPathGeometry(tempDrawObject1);
            }
            else if (drawObject1 is MultiSegmentLineBase)
            {
                var tempDrawObject1 = drawObject1 as MultiSegmentLineBase;
                 path1 = BuildPathGeometry(tempDrawObject1.Points, isClosed);
            }
            if (drawObject2 is Circle)
            {
                var tempDrawObject2 = drawObject2 as Circle;
                 path2 = CircleBulidPathGeometry(tempDrawObject2);
            }
            else if (drawObject2 is MultiSegmentLineBase)
            {
                var tempDrawObject2 = drawObject2 as MultiSegmentLineBase;
                 path2 = BuildPathGeometry(tempDrawObject2.Points, isClosed);
            }
            if (!path1.IsEmpty()&& !path2.IsEmpty())
            {
                if (path2.FillContainsWithDetail(path1) == intersectionDetail)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 圆弧的几何图形（由两个圆心角为180°的弧组合而成）
        /// </summary>
        /// <param name="drawObject"></param>
        /// <returns></returns>
        private static PathGeometry CircleBulidPathGeometry(IDrawObject drawObject)
        {
            Circle tempCircle = drawObject as Circle;
            if (tempCircle == null) return null;
            UnitPoint startPoint = HitUtil.PointOnCircle(tempCircle.Center, tempCircle.Radius, 0);
            UnitPoint endPoint = HitUtil.PointOnCircle(tempCircle.Center, tempCircle.Radius, Math.PI);
           var arcToCirclePoints = new List<UnitPointBulge>()
            {
                 new UnitPointBulge(startPoint,1f),
                new UnitPointBulge(endPoint,1f)
            };
            return BuildPathGeometry(arcToCirclePoints, true);
        }

        public static RectangleF GetBoundsPathGeometry(List<UnitPointBulge> points, bool isClosed)
        {
            PathGeometry path = BuildPathGeometry(points, isClosed);
            return new RectangleF((float)path.Bounds.X, (float)path.Bounds.Y, (float)path.Bounds.Width, (float)path.Bounds.Height);
        }
        public static RectangleF GetArcBounds(ArcBase arc, float thresholdWidth)
        {
            //float r = arc.Radius + thresholdWidth / 2;
            List<UnitPoint> points = new List<UnitPoint>() { arc.startPoint, arc.endPoint };
            float angle1 = arc.StartAngle;
            if (arc.IsClockwise)
            {
                float tempAngle = angle1 % 90;
                for (float i = arc.AngleSweep; i <= 0; i += tempAngle)
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
                        UnitPoint point = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(angle1)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(angle1)) + arc.Center.Y);
                        points.Add(point);
                    }
                }
            }
            else
            {
                float tempAngle = 90 - angle1 % 90;
                for (float i = arc.AngleSweep; i >= 0; i -= tempAngle)
                {
                    tempAngle = 90 - angle1 % 90;
                    if (tempAngle <= i)
                    {
                        angle1 = (angle1 + tempAngle) % 360;
                        UnitPoint point = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(angle1)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(angle1)) + arc.Center.Y);
                        points.Add(point);
                    }
                }
            }
            float maxX = (float)points.Max<UnitPoint>(x => x.X);
            float maxY = (float)points.Max<UnitPoint>(x => x.Y);
            float minX = (float)points.Min<UnitPoint>(x => x.X);
            float minY = (float)points.Min<UnitPoint>(x => x.Y);
            return ScreenUtils.GetRectangleF(new UnitPoint(minX, minY), new UnitPoint(maxX, maxY), thresholdWidth / 2);
        }

        public static UnitPoint GetPointOnArcByAngle(UnitPoint center, double radius, double angle)
        {
            UnitPoint unitPoint = new UnitPoint(radius * Math.Cos(HitUtil.DegreesToRadians(angle)) + center.X, radius * Math.Sin(HitUtil.DegreesToRadians(angle)) + center.Y);
            return unitPoint;
        }

        /// <summary>
        /// 计算与已知直线垂直的线的两个距离一定的端点坐标
        /// </summary>
        /// <param name="intersectPoint">已知直线与另一垂线的交点</param>
        /// <param name="endPoint">已知直线的另一点</param>
        /// <param name="length">垂直线一端长度</param>
        /// <param name="clockWise">原图形的时针方向</param>e
        /// <returns></returns>
        public static UnitPoint GetRightCircleCenter(UnitPoint intersectPoint, UnitPoint endPoint, double length, bool clockWise)
        {
            Tuple<UnitPoint, UnitPoint> result = DrawingOperationHelper.GetLinePointByVerticalLine(intersectPoint, endPoint, length);
            bool directionA = HitUtil.IsClockwiseByCross(result.Item1, intersectPoint, endPoint);
            UnitPoint arcCenter = directionA == clockWise ? result.Item1 : result.Item2;
            return arcCenter;
        }
    }
}
