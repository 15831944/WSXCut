using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool.MultiSegmentLine
{
    public class BulgeHelper
    {
        private static readonly double epsilon = 0.000001;
        public static List<UnitPoint> GetCufoffPoint(UnitPoint p1, UnitPoint p2, UnitPoint p3, double radius)
        {
            List<UnitPoint> cutoffPoints = new List<UnitPoint>();
            double lineAngle = CalTwoLinesAngleFromThreePoints(p1, p2, p3);
            double length = radius / Math.Tan(lineAngle / 2);
            double preAngle = HitUtil.LineAngleR(p2, p1, 0);
            UnitPoint prePoint = HitUtil.LineEndPoint(p2, preAngle, length);
            //UnitPoint prePoint = HitUtil.PointOnCircle(p2, length, preAngle);
            cutoffPoints.Add(prePoint);
            double nextAngle = HitUtil.LineAngleR(p2, p3, 0);
            UnitPoint nextPoint = HitUtil.LineEndPoint(p2, nextAngle, length);
            //UnitPoint nextPoint = HitUtil.PointOnCircle(p2, length, nextAngle);
            cutoffPoints.Add(nextPoint);
            return cutoffPoints;
        }



        /// <summary>
        /// 获取离交点距离一定的点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static List<UnitPoint> GetCufPoint(UnitPoint p1, UnitPoint p2, UnitPoint p3, double radius)
        {
            List<UnitPoint> cutPoints = new List<UnitPoint>();
            double angle = HitUtil.LineAngleR(p2, p1, 0);
            UnitPoint cutPoint = HitUtil.LineEndPoint(p2, angle, radius);
            cutPoints.Add(cutPoint);
            angle = HitUtil.LineAngleR(p2, p3, 0);
            cutPoint = HitUtil.LineEndPoint(p2, angle, radius);
            cutPoints.Add(cutPoint);
            return cutPoints;
        }

        public static double GetBulgeFromThreePoints(UnitPoint p1, UnitPoint p2, UnitPoint p3, bool isRoundAngle = true)
        {
            double bulge = Math.PI - CalTwoLinesAngleFromThreePoints(p1, p2, p3);
            bulge = isRoundAngle ? bulge : 2 * Math.PI - bulge;
            bool clockwise = HitUtil.IsClockwiseByCross(p1, p2, p3);
            bulge = GetBulgFromRadian(bulge);
            bulge = clockwise ? -bulge : bulge;
            return bulge;
        }

        /// <summary>
        /// 根据圆心，圆上起始点，结束点及圆的时针方向计算圆弧凸度
        /// </summary>
        /// <param name="center">圆心</param>
        /// <param name="startPoint">起始点</param>
        /// <param name="endPoint">结束点</param>
        /// <param name="clockwise">时针方向</param>
        /// <returns></returns>
        public static double GetBulgeFromTwoPointsAndCenter(UnitPoint center, UnitPoint startPoint, UnitPoint endPoint, bool clockwise)
        {
            double startAngle = HitUtil.LineAngleR(center, startPoint, 0);
            double endAngle = HitUtil.LineAngleR(center, endPoint, 0);
            double sweepAngle = HitUtil.CalAngleSweep(HitUtil.RadiansToDegrees(startAngle), HitUtil.RadiansToDegrees(endAngle), clockwise);
            sweepAngle = Math.Abs(HitUtil.DegreesToRadians(sweepAngle) / 4);
            double bulge = Math.Tan(sweepAngle);
            bulge = clockwise ? -bulge : bulge;
            return bulge;
        }

        public static UnitPoint GetBulgeCenter(UnitPoint vertex, UnitPoint cutoffPoint, UnitPoint cutoffPoint2, double radius)
        {
            UnitPoint midPoint = HitUtil.LineMidpoint(cutoffPoint, cutoffPoint2);
            double midAngle = HitUtil.LineAngleR(vertex, midPoint, 0);
            double sideLen = GetDistanceBetweenVertexAndCutoffPoint(radius, midAngle * 2);
            double vertexToCenter = radius * radius + sideLen * sideLen;
            UnitPoint center = HitUtil.LineEndPoint(vertex, midAngle, vertexToCenter);
            return center;
        }

        /// <summary>
        /// 计算夹角顶点到所夹圆与直线的切点之间的距离
        /// </summary>
        /// <param name="radius">圆的半径</param>
        /// <param name="angle">两条线段之间夹角</param>
        /// <returns></returns>
        public static double GetDistanceBetweenVertexAndCutoffPoint(double radius, double angle)
        {
            return radius / Math.Tan(angle / 2);
        }

        public static double GetBulgFromRadian(double radian)
        {
            return Math.Tan(radian / 4);
        }

        public static double CalTwoLinesAngleFromThreePoints(UnitPoint p1, UnitPoint p2, UnitPoint p3)
        {
            double p12 = HitUtil.Distance(p1, p2);
            double p23 = HitUtil.Distance(p2, p3);
            double p13 = HitUtil.Distance(p1, p3);
            double cosA = (p12 * p12 + p23 * p23 - p13 * p13) / (2 * p12 * p23);
            double angle = Math.Acos(cosA);
            return angle;
        }

        public static UnitPoint GetCenterByBulgeAndTwoPoints(UnitPoint p1, UnitPoint p2, double bulge)
        {

            //UnitPoint centerPoints;
            //double chord = HitUtil.Distance(p1, p2);
            //double radius = chord / (2 * Math.Sin(2 * Math.Atan(Math.Abs(bulge))));
            //double c1 = (p2.X * p2.X - p1.X * p1.X + p2.Y * p2.Y - p1.Y * p1.Y) / (2 * (p2.X - p1.X));
            //double c2 = (p2.Y - p1.Y) / (p2.X - p1.X);
            //double A = (c2 * c2 + 1);
            //double B = (2 * p1.X * c2 - 2 * c1 * c2 - 2 * p1.Y);
            //double C = p1.X * p1.X - 2 * p1.X * c1 + c1 * c1 + p1.Y * p1.Y - radius * radius;
            //double y1 = (-B + Math.Sqrt(B * B - 4 * A * C)) / (2 * A);
            //double y2 = (-B - Math.Sqrt(B * B - 4 * A * C)) / (2 * A);
            //double x1 = c1 - c2 * y1;
            //double x2 = c1 - c2 * y2;
            //UnitPoint centerPoints1 = new UnitPoint(x1, y1);
            //UnitPoint centerPoints2 = new UnitPoint(x2, y2);
            //double err1 = IsCorrectCenter(centerPoints1, radius, bulge, p1, p2);
            //double err2 = IsCorrectCenter(centerPoints2, radius, bulge, p1, p2);
            //centerPoints = err1 < err2 ? centerPoints1 : centerPoints2;
            //return centerPoints;


            UnitPoint centerPoints = new UnitPoint();
            double chord = HitUtil.Distance(p1, p2);
            double radius = chord / (2 * Math.Sin(2 * Math.Atan(Math.Abs(bulge))));
            //1.求p1,p2的中间点
            UnitPoint midPoint = HitUtil.GetLinePointByDistance(p1, p2, chord / 2, true);
            double length = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(chord / 2, 2));
            //2.求等距离的垂线点
            var centers = DrawingOperationHelper.GetLinePointByVerticalLine(midPoint, p1, length);
            //3.筛选圆心坐标
            double err1 = IsCorrectCenter(centers.Item1, radius, bulge, p1, p2);
            double err2 = IsCorrectCenter(centers.Item2, radius, bulge, p1, p2);
            centerPoints = err1 < err2 ? centers.Item1 : centers.Item2;
            return centerPoints;
        }
        private static double IsCorrectCenter(UnitPoint center, double radius, double bulge, UnitPoint p1, UnitPoint p2)
        {
            double startAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, p1, 0));
            double endAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, p2, 0));
            bool clockwise = bulge >= 0 ? false : true;
            double sweepAngle = HitUtil.CalAngleSweep(startAngle, endAngle, clockwise);
            double midAngle = startAngle + sweepAngle / 2;
            UnitPoint midPoint = new UnitPoint(radius * Math.Cos(HitUtil.DegreesToRadians(midAngle)) + center.X, radius * Math.Sin(HitUtil.DegreesToRadians(midAngle)) + center.Y);
            double temp = BulgeToDegree(bulge);
            return Math.Abs(temp - Math.Abs(sweepAngle));
        }

        private static double BulgeToDegree(double bulge)
        {
            double angle = Math.Abs(bulge);
            angle = 4 * Math.Atan(angle) * 180 / Math.PI;
            return angle;
        }

        public static UnitPoint GetBulgeMidPoint(UnitPoint p1, UnitPoint p2, double bulge)
        {
            bool clockwise = bulge >= 0 ? false : true;
            UnitPoint center = GetCenterByBulgeAndTwoPoints(p1, p2, bulge);
            float radius = (float)HitUtil.Distance(center, p1);
            float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, p1, 0));
            float EndAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, p2, 0));
            float sweepAngle = (float)HitUtil.CalAngleSweep(startAngle, EndAngle, clockwise);
            float midAngle = startAngle + sweepAngle / 2;
            UnitPoint midPoint = new UnitPoint(radius * Math.Cos(HitUtil.DegreesToRadians(midAngle)) + center.X, radius * Math.Sin(HitUtil.DegreesToRadians(midAngle)) + center.Y);
            return midPoint;
        }

    }
}
