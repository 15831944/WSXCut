using System;
using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.DrawModel;
using WSX.DrawService.DrawTool.Arcs;

namespace WSX.DrawService.Utils
{
    public static class PointFExtensions
    {
        public static Point ToPoint(this PointF p)
        {
            return new Point((int)p.X, (int)p.Y);
        }
    }

    public class HitUtil
    {
        /// <summary>
        /// 判断p2是否在p1上
        /// </summary>
        /// <param name="p1">目标点</param>
        /// <param name="p2">被判断点</param>
        /// <param name="threshold">宽度阈值</param>
        /// <returns></returns>
        public static bool PointInPoint(UnitPoint p1, UnitPoint p2, float threshold)
        {
            if (p1.IsEmpty || p2.IsEmpty) return false;
            if (p1.X < p2.X - threshold || p1.X > p2.X + threshold) return false;
            if (p1.Y < p2.Y - threshold || p1.Y > p2.Y + threshold) return false;
            return true;
        }

        /// <summary>
        /// 求2点之间距离
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="abs"></param>
        /// <returns></returns>
        public static double Distance(UnitPoint p1, UnitPoint p2, bool abs)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            if (abs)
            {
                dx = Math.Abs(dx);
                dy = Math.Abs(dy);
            }
            if (dx == 0) return dy;
            if (dy == 0) return dx;
            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }

        public static double Distance(UnitPoint p1, UnitPoint p2)
        {
            return Distance(p1, p2, true);
        }

        public static double RadiansToDegrees(double radians)
        {
            return radians * (180 / Math.PI);
        }

        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        /// <summary>
        /// 获取圆的绑定矩形
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static RectangleF CircleBoundingRectangle(UnitPoint center, float radius)
        {
            RectangleF rectangleF = new RectangleF(center.Point, new SizeF(0, 0));
            rectangleF.Inflate(radius, radius);
            return rectangleF;
        }

        public static bool CircleHitPoint(UnitPoint center, float radius, UnitPoint hitPoint)
        {
            //检查所绑定的矩形，这样比创建一个新的矩形并调用其Contains()方法更快
            double leftPoint = center.X - radius;
            double rightPoint = center.X + radius;
            if (hitPoint.X < leftPoint || hitPoint.X > rightPoint) return false;
            double bottomPoint = center.Y - radius;
            double topPoint = center.Y + radius;
            if (hitPoint.Y < bottomPoint || hitPoint.Y > topPoint) return false;
            return true;
        }

        /// <summary>
        /// 判断圆是否与直线相交
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool CircleIntersectWithLine(UnitPoint center, float radius, UnitPoint p1, UnitPoint p2)
        {
            //判断2个点是否都在圆内，此种情况直线和圆没有交点
            if (Distance(center, p1) < radius && Distance(center, p2) < radius) return false;
            //寻找中心点到直线段上最近的点，
            UnitPoint nearestPoint = NearestPointOnLine(p1, p2, center);
            double distance = Distance(center, nearestPoint);
            if (distance <= radius)//如果圆心和圆心到直线段上最近点的距离小于半径则相交
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 求取点到线段上最近的点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="point"></param>
        /// <param name="beyondSegment"></param>
        /// <returns></returns>
        public static UnitPoint NearestPointOnLine(UnitPoint p1, UnitPoint p2, UnitPoint point, bool beyondSegment)
        {
            if (p1.X == p2.X)
            {
                //如果两个点均在下方，那么选择最靠近point的Y坐标
                if (beyondSegment == false && p1.Y < point.Y && p2.Y < point.Y)
                {
                    return new UnitPoint(p1.X, Math.Max(p1.Y, p2.Y));
                }
                //如果两个点均在上方，那么选择最靠近point的Y坐标
                if (beyondSegment == false && p1.Y > point.Y && p2.Y > point.Y)
                {
                    return new UnitPoint(p1.X, Math.Min(p1.Y, p2.Y));
                }
                //否则点在线段一侧，选择point的X坐标
                return new UnitPoint(p1.X, point.Y);
            }
            if (p1.Y == p2.Y)
            {
                //如果两个点均在右侧，那么选择最最靠近Point的点的X坐标作为最近点的x坐标
                if (beyondSegment == false && p1.X < point.X && p2.X < point.X)
                {
                    return new UnitPoint(Math.Max(p1.X, p2.X), p1.Y);
                }
                if (beyondSegment == false && p1.X > point.X && p2.X > point.X)
                {
                    return new UnitPoint(Math.Min(p1.X, p2.X), p1.Y);
                }
                return new UnitPoint(point.X, p1.Y);
            }
            return NearestPointOnLine(p1, p2, point, 0);
        }

        public static UnitPoint NearestPointOnLine(UnitPoint p1, UnitPoint p2, UnitPoint testPoint, double roundToAngleR)
        {
            if (p1.X == testPoint.X)
            {
                UnitPoint unitPoint = p1;
                p1 = p2;
                p2 = unitPoint;
            }
            double A1 = LineAngleR(p1, testPoint, 0);
            double A2 = LineAngleR(p1, p2, roundToAngleR);
            double A = A1 - A2;
            double h1 = (p1.X - testPoint.X) / Math.Cos(A1);
            double h2 = Math.Cos(A) * h1;
            double x = Math.Cos(A2) * h2;
            double y = Math.Sin(A2) * h2;
            x = p1.X - x;
            y = p1.Y - y;
            return new UnitPoint((float)x, (float)y);
        }

        public static UnitPoint NearestPointOnLine(UnitPoint p1, UnitPoint p2, UnitPoint testPoint)
        {
            return NearestPointOnLine(p1, p2, testPoint, false);
        }

        public static double LineAngleR(UnitPoint p1, UnitPoint p2, double roundToAngleR)
        {
            if (p1.X == p2.X)
            {
                if (p1.Y > p2.Y)
                {
                    return Math.PI * 6 / 4;
                }
                if (p1.Y < p2.Y)
                {
                    return Math.PI / 2;
                }
                return 0;
            }
            double adjacent = p2.X - p1.X;
            double oppsite = p2.Y - p1.Y;
            double A = Math.Atan(oppsite / adjacent);
            if (adjacent < 0)//在第二第三象限
            {
                A += Math.PI;
            }
            if (adjacent > 0 && oppsite < 0)//第4象限
            {
                A += Math.PI * 2;
            }
            if (roundToAngleR != 0)
            {
                double roundUnit = Math.Round(A / roundToAngleR);
                A = roundToAngleR * roundUnit;
            }
            return A;
        }

        /// <summary>
        /// 判断点是否在圆上
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="testPoint"></param>
        /// <param name="halfLineWidth"></param>
        /// <returns></returns>
        public static bool IsPointInCircle(UnitPoint center, float radius, UnitPoint testPoint, float halfLineWidth)
        {
            double distance = Distance(center, testPoint);
            if (distance >= radius - halfLineWidth && distance <= radius + halfLineWidth)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 求取某个点离圆上最近的点
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="testPoint"></param>
        /// <param name="roundToAngleD"></param>
        /// <returns></returns>
        public static UnitPoint NearestPointOnCircle(UnitPoint center, float radius, UnitPoint testPoint, double roundToAngleD)
        {
            double A = LineAngleR(center, testPoint, DegreesToRadians(roundToAngleD));
            double dx = Math.Cos(A) * radius;
            double dy = Math.Sin(A) * radius;
            double distance = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
            return new UnitPoint((float)(center.X + dx), (float)(center.Y + dy));
        }

        /// <summary>
        /// 求取点与圆的切点坐标
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="testPoint"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        public static UnitPoint TangentPointOnCircle(UnitPoint center, float radius, UnitPoint testPoint, bool reverse)
        {
            double h = Math.Sqrt(Math.Pow(center.X - testPoint.X, 2) + Math.Pow(center.Y - testPoint.Y, 2));
            if (Math.Abs(h) < radius)//点在圆内，则没有切点
            {
                return UnitPoint.Empty;
            }
            double A1 = LineAngleR(center, testPoint, 0);//圆心角
            double A2 = Math.Asin(radius / h);
            double A3 = Math.Acos(radius / h);
            double A = Math.PI / 2 + (A1 - A2);
            if (reverse)
            {
                A = A1 - A3;
            }
            return LineEndPoint(center, A, radius);
        }

        /// <summary>
        /// 求取线段终点坐标
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="angleR"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static UnitPoint LineEndPoint(UnitPoint p1, double angleR, double length)
        {
            double x = Math.Cos(angleR) * length;
            double y = Math.Sin(angleR) * length;
            return new UnitPoint(p1.X + (float)x, p1.Y + (float)y);
        }

        /// <summary>
        /// 得到两点连成的直线中的距离p1为d的点坐标
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="d"></param>
        /// <param name="inside">线段内的点-ture,线段外的点-false</param>
        /// <returns></returns>
        public static UnitPoint GetLinePointByDistance(UnitPoint p1, UnitPoint p2, double d, bool inside = true)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;
            double r = Math.Sqrt((dx * dx + dy * dy));

            if (!inside)
            {
                double x = p1.X - (d * (p2.X - p1.X)) / r;
                double y = p1.Y - (d * (p2.Y - p1.Y)) / r;

                return new UnitPoint(x, y);
            }
            else
            {
                double x = (d * (p2.X - p1.X)) / r + p1.X;
                double y = (d * (p2.Y - p1.Y)) / r + p1.Y;

                return new UnitPoint(x, y);
            }
        }

        /// <summary>
        /// 求对应圆心角在圆上的点的坐标
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="angleR"></param>
        /// <returns></returns>
        public static UnitPoint PointOnCircle(UnitPoint center, double radius, double angleR)
        {
            double y = center.Y + Math.Sin(angleR) * radius;
            double x = center.X + Math.Cos(angleR) * radius;
            return new UnitPoint((float)x, (float)y);
        }

        public static RectangleF LineBoundingRectangle(UnitPoint p1, UnitPoint p2, float halfLineWidth)
        {
            double x = Math.Min(p1.X, p2.X);
            double y = Math.Min(p1.Y, p2.Y);
            double w = Math.Abs(p1.X - p2.X);
            double h = Math.Abs(p1.Y - p2.Y);
            RectangleF boundingRectangle = ScreenUtils.GetRectangleF(x, y, w, h);
            boundingRectangle.Inflate(halfLineWidth, halfLineWidth);
            return boundingRectangle;
        }


        public static bool IsPointInLine(UnitPoint p1, UnitPoint p2, UnitPoint testPoint, float halfLineWidth)
        {
            //range 判断的的误差，不需要误差则赋值0
            //点在线段首尾两端之外则return false

            double cross = (p2.X - p1.X) * (testPoint.X - p1.X) + (p2.Y - p1.Y) * (testPoint.Y - p1.Y);
            //检查是否命中端点
            if (CircleHitPoint(p1, halfLineWidth, testPoint)) return true;
            if (CircleHitPoint(p2, halfLineWidth, testPoint)) return true;
            //double cross = (p2.X - p1.X) * (testPoint.Y - p1.Y) - (testPoint.X - p1.X) * (p2.Y - p1.Y);
            if (cross < 0) return false;
            double d2 = (p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y);
            if (cross >= d2 + halfLineWidth / 2)
                return false;
            double r = cross / d2;
            double px = p1.X + (p2.X - p1.X) * r;
            double py = p1.Y + (p2.Y - p1.Y) * r;
            //判断距离是否小于误差
            double errorValue = Math.Sqrt((testPoint.X - px) * (testPoint.X - px) + (py - testPoint.Y) * (py - testPoint.Y));
            return errorValue <= halfLineWidth / 2;
        }

        /// <summary>
        /// 判断点是否在线上
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="testPoint"></param>
        /// <param name="halfLineWidth"></param>
        /// <returns></returns>
        //public static bool IsPointInLine(UnitPoint p1, UnitPoint p2, UnitPoint testPoint, float halfLineWidth)
        //{
        //    double lineLeftPoint = Math.Min(p1.X, p2.X) - halfLineWidth;
        //    double lineRightPoint = Math.Max(p1.X, p2.X) + halfLineWidth;
        //    if (testPoint.X < lineLeftPoint || testPoint.X > lineRightPoint) return false;
        //    double lineBottomPoint = Math.Min(p1.Y, p2.Y) - halfLineWidth;
        //    double lineTopPoint = Math.Max(p1.Y, p2.Y) + halfLineWidth;
        //    if (testPoint.Y < lineBottomPoint || testPoint.Y > lineTopPoint) return false;
        //    //检查是否命中端点
        //    if (CircleHitPoint(p1, halfLineWidth, testPoint)) return true;
        //    if (CircleHitPoint(p2, halfLineWidth, testPoint)) return true;
        //    if (p1.Y == p2.Y)//水平线
        //    {
        //        double min = Math.Min(p1.X, p2.X) - halfLineWidth;
        //        double max = Math.Max(p1.X, p2.X) + halfLineWidth;
        //        if (testPoint.X >= min && testPoint.X <= max)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    if (p1.X == p2.X)//垂直线
        //    {
        //        double min = Math.Min(p1.Y, p2.Y) - halfLineWidth;
        //        double max = Math.Max(p1.Y, p2.Y) + halfLineWidth;
        //        if (testPoint.Y >= min && testPoint.Y <= max)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    //使用余弦规则 a^2=b^2+c^2-2*b*c*Cos(A)
        //    //A=A*Cos((a^2-b^2-c^2)/(-2bc))
        //    double xDiff = Math.Abs(p2.X - testPoint.X);
        //    double yDiff = Math.Abs(p2.Y - testPoint.Y);
        //    double square = Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2);
        //    double a = Math.Sqrt(square);
        //    xDiff = Math.Abs(p1.X - p2.X);
        //    yDiff = Math.Abs(p1.Y - p2.Y);
        //    double bSquare = Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2);
        //    double b = Math.Sqrt(bSquare);

        //    xDiff = Math.Abs(p1.X - testPoint.X);
        //    yDiff = Math.Abs(p1.Y - testPoint.Y);
        //    double cSquare = Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2);
        //    double c = Math.Sqrt(cSquare);
        //    double A = Math.Acos(((square - bSquare - cSquare) / (-2 * b * c)));

        //    // 求出角度A就可以找到高 (到直线的距离)
        //    // SIN(A) = (h / c)
        //    // h = SIN(A) * c;
        //    double h = Math.Sin(A) * c;

        //    // now if height is smaller than half linewidth, the hitpoint is within the line
        //    return h <= halfLineWidth;
        //}

        /// <summary>
        /// 判断两条线段是否相交
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="returnpoint"></param>
        /// <param name="extendA"></param>
        /// <param name="extendB"></param>
        /// <returns></returns>
        private static bool LinesIntersect(UnitPoint p1, UnitPoint p2, UnitPoint p3, UnitPoint p4, ref double x, ref double y,
            bool returnpoint,
            bool extendA,
            bool extendB)
        {
            double x1 = p1.X;
            double x2 = p2.X;
            double x3 = p3.X;
            double x4 = p4.X;
            double y1 = p1.Y;
            double y2 = p2.Y;
            double y3 = p3.Y;
            double y4 = p4.Y;

            double denominator = ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            if (denominator == 0) // lines are parallel
                return false;
            double numerator_ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3));
            double numerator_ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3));
            double ua = numerator_ua / denominator;
            double ub = numerator_ub / denominator;
            // if a line is not extended then ua (or ub) must be between 0 and 1
            if (extendA == false)
            {
                if (ua < 0 || ua > 1)
                    return false;
            }
            if (extendB == false)
            {
                if (ub < 0 || ub > 1)
                    return false;
            }
            if (extendA || extendB) // no need to chck range of ua and ub if check is one on lines 
            {
                x = x1 + ua * (x2 - x1);
                y = y1 + ua * (y2 - y1);
                return true;
            }
            if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
            {
                if (returnpoint)
                {
                    x = x1 + ua * (x2 - x1);
                    y = y1 + ua * (y2 - y1);
                }
                return true;
            }
            return false;
        }

        public static bool LinesIntersect(UnitPoint p1, UnitPoint p2, UnitPoint p3, UnitPoint p4)
        {
            double x = 0;
            double y = 0;
            return LinesIntersect(p1, p2, p3, p4, ref x, ref y, false, false, false);
        }

        /// <summary>
        /// 求取两条直线段的交点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        public static UnitPoint LinesIntersectPoint(UnitPoint p1, UnitPoint p2, UnitPoint p3, UnitPoint p4)
        {
            double x = 0;
            double y = 0;
            if (LinesIntersect(p1, p2, p3, p4, ref x, ref y, true, false, false))
                return new UnitPoint(x, y);
            return UnitPoint.Empty;
        }

        public static UnitPoint FindApparentIntersectPoint(UnitPoint p1, UnitPoint p2, UnitPoint p3, UnitPoint p4)
        {
            double x = 0;
            double y = 0;
            if (LinesIntersect(p1, p2, p3, p4, ref x, ref y, true, true, true))
                return new UnitPoint(x, y);
            return UnitPoint.Empty;
        }
        public static UnitPoint FindApparentIntersectPoint(UnitPoint p1, UnitPoint lp2, UnitPoint p3, UnitPoint p4, bool extendA, bool extendB)
        {
            double x = 0;
            double y = 0;
            if (LinesIntersect(p1, lp2, p3, p4, ref x, ref y, true, extendA, extendB))
                return new UnitPoint(x, y);
            return UnitPoint.Empty;
        }

        /// <summary>
        /// 判断线段是否与矩形相交
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool LineIntersectWithRect(UnitPoint p1, UnitPoint p2, RectangleF r)
        {
            if (r.Contains(p1.Point))
                return true;
            if (r.Contains(p2.Point))
                return true;

            // the rectangle bottom is top in world units and top is bottom!, confused?
            // check left
            UnitPoint p3 = new UnitPoint(r.Left, r.Top);
            UnitPoint p4 = new UnitPoint(r.Left, r.Bottom);
            if (LinesIntersect(p1, p2, p3, p4))
                return true;
            // check bottom
            p4.Y = r.Top;
            p4.X = r.Right;
            if (LinesIntersect(p1, p2, p3, p4))
                return true;
            // check right
            p3.X = r.Right;
            p3.Y = r.Top;
            p4.X = r.Right;
            p4.Y = r.Bottom;
            if (LinesIntersect(p1, p2, p3, p4))
                return true;
            return false;
        }

        /// <summary>
        /// 求取线段的中点坐标
        /// </summary>
        /// <param name="lp1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static UnitPoint LineMidpoint(UnitPoint p1, UnitPoint p2)
        {
            UnitPoint mid = new UnitPoint();
            mid.X = (p1.X + p2.X) / 2;
            mid.Y = (p1.Y + p2.Y) / 2;
            return mid;
        }

        public static UnitPoint OrthoPointD(UnitPoint p1, UnitPoint p2, double roundToAngleR)
        {
            return OrthoPointR(p1, p2, DegreesToRadians(roundToAngleR));
        }
        public static UnitPoint OrthoPointR(UnitPoint p1, UnitPoint p2, double roundToAngleR)
        {
            return NearestPointOnLine(p1, p2, p2, roundToAngleR);
        }

        public static double LineSlope(UnitPoint p1, UnitPoint p2)
        {
            return (p2.Y - p1.Y) / (p2.X - p1.X);
        }
        public static UnitPoint CenterPointFrom3Points(UnitPoint p1, UnitPoint p2, UnitPoint p3)
        {
            // 3 points forms 2 lines a and b, slope of a is ma, b is mb
            // ma = (y2-y1) / (x2-x1)
            // mb = (y3-y2) / (x3-x2)
            double ma = (p2.Y - p1.Y) / (p2.X - p1.X);
            double mb = (p3.Y - p2.Y) / (p3.X - p2.X);

            // this is just a hacky work around for when p1-p2 are either horizontal or vertical.
            // A real solution should be found, but for now this will work
            if (double.IsInfinity(ma))
                ma = 1000000000000;
            if (double.IsInfinity(mb))
                mb = 1000000000000;
            if (ma == 0)
                ma = 0.000000000001;
            if (mb == 0f)
                mb = 0.000000000001;

            // centerpoint x = mamb(y1-y3) + mb(x1+x2) - ma(x2+x3) / 2(mb-ma)
            double center_x = (ma * mb * (p1.Y - p3.Y) + mb * (p1.X + p2.X) - ma * (p2.X + p3.X)) / (2 * (mb - ma));
            double center_y = (-1 * (center_x - (p1.X + p2.X) / 2) / ma) + ((p1.Y + p2.Y) / 2);

            //m_Center.m_y = -1*(m_Center.x() - (pt1->x()+pt2->x())/2)/aSlope +  (pt1->y()+pt2->y())/2;
            return new UnitPoint((float)center_x, (float)center_y);
        }

        /// <summary>
        /// 判断矩形是否和矩形相交
        /// </summary>
        /// <param name="rectangleA"></param>
        /// <param name="rectangleB"></param>
        /// <returns></returns>
        public static bool RectangleIntersect(RectangleF rectangleA, RectangleF rectangleB)
        {
            UnitPoint A1 = new UnitPoint(rectangleA.Left, rectangleA.Bottom);
            UnitPoint A2 = new UnitPoint(rectangleA.Right, rectangleA.Top);
            UnitPoint B1 = new UnitPoint(rectangleB.Left, rectangleB.Bottom);
            UnitPoint B2 = new UnitPoint(rectangleB.Right, rectangleB.Top);
            try
            {
                double zx = Math.Abs(A1.X + A2.X - B1.X - B2.X);
                double x = Math.Abs(A1.X - A2.X) + Math.Abs(B1.X - B2.X);
                double zy = Math.Abs(A1.Y + A2.Y - B1.Y - B2.Y);
                double y = Math.Abs(A1.Y - A2.Y) + Math.Abs(B1.Y - B2.Y);
                if (zx <= x && zy <= y)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 求点到线段/直线的垂直距离
        /// </summary>
        /// <param name="p1">线段的第一个点</param>
        /// <param name="p2">线段的第二个点</param>
        /// <param name="testPoint"></param>
        /// <returns></returns>
        public static double DistanctPointToLine(UnitPoint p1, UnitPoint p2, UnitPoint testPoint)
        {
            //三角形边长
            double distanceStartTest = HitUtil.Distance(p1, testPoint);
            double distanceEndTest = HitUtil.Distance(p2, testPoint);
            double distanceStartEnd = HitUtil.Distance(p1, p2);

            //半周长
            double p = (distanceStartTest + distanceEndTest + distanceStartEnd) / 2;

            //海伦公式求三角形面积
            double s = Math.Sqrt(p * Math.Abs(p - distanceStartTest) * Math.Abs(p - distanceEndTest) * Math.Abs(p - distanceStartEnd));

            return 2 * s / distanceStartEnd;
        }

        #region 新增 LXZ
        public static bool IsClockwiseByCross(UnitPoint startPoint, UnitPoint midPoint, UnitPoint endPoint)
        {
            double result = (midPoint.X - startPoint.X) * (endPoint.Y - midPoint.Y) - (midPoint.Y - startPoint.Y) * (endPoint.X - midPoint.X);
            return result < 0;//顺时针方向
        }

        public static ArcModelMini GetArcParametersFromThreePoints(UnitPoint startPoint, UnitPoint midPoint, UnitPoint endPoint)
        {
            ArcModelMini arcModel = new ArcModelMini();
            arcModel.Clockwise = HitUtil.IsClockwiseByCross(startPoint, midPoint, endPoint);
            arcModel.Center = HitUtil.CenterPointFrom3Points(startPoint, midPoint, endPoint);
            arcModel.Radius = (float)HitUtil.Distance(arcModel.Center, startPoint);
            arcModel.StartAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(arcModel.Center, startPoint, 0));
            float EndAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(arcModel.Center, endPoint, 0));
            arcModel.SweepAngle = (float)HitUtil.CalAngleSweep(arcModel.StartAngle, EndAngle, arcModel.Clockwise);
            return arcModel;
        }

        public static double CalAngleSweep(double startAngle, double endAngle, bool clockwise)
        {
            double sweepAngle = 0;
            if (clockwise)
            {
                if (endAngle > startAngle)
                {
                    sweepAngle = -(360 - (endAngle - startAngle));
                }
                else
                {
                    sweepAngle = -(startAngle - endAngle);
                }
            }
            else
            {
                if (endAngle > startAngle)
                {
                    sweepAngle = endAngle - startAngle;
                }
                else
                {
                    sweepAngle = 360 - (startAngle - endAngle);
                }
            }
            return sweepAngle;
        }


        public static bool IsPointOnArc(UnitPoint testPoint, float thWidth, ArcModelMini arcModel)
        {
            List<UnitPoint> temp = new List<UnitPoint>();
            temp.Add(new UnitPoint(testPoint.X + thWidth, testPoint.Y + thWidth));
            temp.Add(new UnitPoint(testPoint.X + thWidth, testPoint.Y - thWidth));
            temp.Add(new UnitPoint(testPoint.X - thWidth, testPoint.Y + thWidth));
            temp.Add(new UnitPoint(testPoint.X - thWidth, testPoint.Y - thWidth));
            for (int i = 0; i < temp.Count; i++)
            {
                if (HitUtil.IsLineOnArc(arcModel.Center, temp[i], arcModel.Center, arcModel.Radius, arcModel.StartAngle, arcModel.SweepAngle, arcModel.Clockwise, thWidth))
                {
                    double distance = HitUtil.Distance(arcModel.Center, testPoint);
                    if (distance <= arcModel.Radius + thWidth && distance >= arcModel.Radius - thWidth)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsPointInArc(double testAngle, double startAngle, double endAngle, bool clockWise)
        {
            double angle1 = HitUtil.CalAngleSweep(startAngle, endAngle, clockWise);
            double angle2 = HitUtil.CalAngleSweep(startAngle, testAngle, clockWise);
            if (Math.Abs(angle1 % 360) > Math.Abs(angle2 % 360)) return true;
            return false;
        }

        //TODO:此方法可能存在问题
        public static bool IsLineOnArc(UnitPoint p1, UnitPoint p2, UnitPoint center, float radius, double startAngle, double angleSweep, bool clockwise, float thWidth)
        {
            List<UnitPoint> unitPoints = HitUtil.GetIntersectPointLineWithCircle(p1, p2, center, radius, thWidth);
            for (int i = 0; i < unitPoints.Count; i++)
            {
                double angle = HitUtil.LineAngleR(center, unitPoints[i], 0);
                angle = HitUtil.RadiansToDegrees(angle);
                double sweepAngle = HitUtil.CalAngleSweep(startAngle, angle, clockwise);
                if (Math.Abs(angleSweep) > Math.Abs(sweepAngle))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取直线和圆的交点坐标
        /// </summary>
        /// <param name="p1">线段起点</param>
        /// <param name="p2">线段终点</param>
        /// <param name="center">圆心坐标</param>
        /// <param name="radius">半径</param>
        /// <param name="thWidth">阈值宽度</param>
        /// <returns>交点坐标</returns>
        public static List<UnitPoint> GetIntersectPointLineWithCircle(UnitPoint p1, UnitPoint p2, UnitPoint center, float radius, float thWidth)
        {
            List<UnitPoint> unitPoints = new List<UnitPoint>();
            UnitPoint intersectOne;
            UnitPoint intersectTwo;
            if (Math.Abs(p1.X - p2.X) < 0.000001)
            {
                intersectOne = new UnitPoint(p1.X, Math.Sqrt(radius * radius - (p1.X - center.X) * (p1.X - center.X)) + center.Y);
                intersectTwo = new UnitPoint(p1.X, -Math.Sqrt(radius * radius - (p1.X - center.X) * (p1.X - center.X)) + center.Y);
            }
            else if (Math.Abs(p1.Y - p2.Y) < 0.000001)
            {
                intersectOne = new UnitPoint(Math.Sqrt(radius * radius - (p1.Y - center.Y) * (p1.Y - center.Y)) + center.X, p1.Y);
                intersectTwo = new UnitPoint(-Math.Sqrt(radius * radius - (p1.Y - center.Y) * (p1.Y - center.Y)) + center.X, p1.Y);
            }
            else
            {
                double A = p1.Y - p2.Y;
                double B = p1.X - p2.X;
                double C = (p2.Y * B - p2.X * A) / B;
                double D = 1 + (A * A) / (B * B);
                double E = 2 * A * (C - center.Y) / B - 2 * center.X;
                double F = center.X * center.X + C * C + center.Y * center.Y - 2 * center.Y * C - radius * radius;
                double delta = Math.Sqrt(E * E - 4 * D * F);
                double x1 = (-E + delta) / (2 * D);
                double x2 = (-E - delta) / (2 * D);
                double y1 = (A / B) * x1 + C;
                double y2 = (A / B) * x2 + C;
                intersectOne = new UnitPoint(x1, y1);
                intersectTwo = new UnitPoint(x2, y2);
            }
            if (IsPointInLine(p1, p2, intersectOne, thWidth)) unitPoints.Add(intersectOne);
            if (IsPointInLine(p1, p2, intersectTwo, thWidth)) unitPoints.Add(intersectTwo);
            return unitPoints;
        }

        /// <summary>
        /// 获取浮点数的整数部分和小数部分
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Tuple<int, float> GetSegmentAndPercent(float value)
        {
            int index = (int)value;
            float percent = value - index;
            Tuple<int, float> result = new Tuple<int, float>(index, percent);
            return result;
        }

        /// <summary>
        /// 判断2个浮点数在精度允许范围内是否相等
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <param name="epsilon">精度</param>
        /// <returns></returns>
        public static bool TwoFloatNumberIsEqual(double number1, double number2, double epsilon = 0.000001)
        {
            if (Math.Abs(number1 - number2) < epsilon) return true;
            return false;
        }

        public static bool IsArcIntersectWithRect(ArcBase arcBase, RectangleF rectangle, float thWidth)
        {
            bool result = false;
            UnitPoint p1 = new UnitPoint(rectangle.Left, rectangle.Top);
            UnitPoint p2 = new UnitPoint(rectangle.Left, rectangle.Bottom);
            if (HitUtil.CircleIntersectWithLine(arcBase.Center, arcBase.Radius, p1, p2))
            {
                result = HitUtil.IsLineOnArc(p1, p2, arcBase.Center, arcBase.Radius, arcBase.StartAngle, arcBase.AngleSweep, arcBase.IsClockwise, thWidth);
                if (result) return result;
            }
            p1 = new UnitPoint(rectangle.Left, rectangle.Bottom);
            p2 = new UnitPoint(rectangle.Right, rectangle.Bottom);
            if (HitUtil.CircleIntersectWithLine(arcBase.Center, arcBase.Radius, p1, p2))
            {
                result = HitUtil.IsLineOnArc(p1, p2, arcBase.Center, arcBase.Radius, arcBase.StartAngle, arcBase.AngleSweep, arcBase.IsClockwise, thWidth);
                if (result) return result;
            }
            p1 = new UnitPoint(rectangle.Left, rectangle.Top);
            p2 = new UnitPoint(rectangle.Right, rectangle.Top);
            if (HitUtil.CircleIntersectWithLine(arcBase.Center, arcBase.Radius, p1, p2))
            {
                result = HitUtil.IsLineOnArc(p1, p2, arcBase.Center, arcBase.Radius, arcBase.StartAngle, arcBase.AngleSweep, arcBase.IsClockwise, thWidth);
                if (result) return result;
            }
            p1 = new UnitPoint(rectangle.Right, rectangle.Top);
            p2 = new UnitPoint(rectangle.Right, rectangle.Bottom);
            if (HitUtil.CircleIntersectWithLine(arcBase.Center, arcBase.Radius, p1, p2))
            {
                result = HitUtil.IsLineOnArc(p1, p2, arcBase.Center, arcBase.Radius, arcBase.StartAngle, arcBase.AngleSweep, arcBase.IsClockwise, thWidth);
                if (result) return result;
            }
            return result;
        }

        public static float IsDrawExtraProperties(RectangleF bigRectangleF, RectangleF smallRectangleF)
        {
            float zoom = GlobalData.Model.GlobalModel.ThresholdWidth / 10;
            float bigSize = bigRectangleF.Width * bigRectangleF.Height;
            float smallSize = smallRectangleF.Width * smallRectangleF.Height * zoom;
            return bigSize / smallSize;
        }
        #endregion

    }
}
