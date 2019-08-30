using System;
using System.Collections.Generic;
using System.Drawing;

namespace WSX.Engine.Utilities
{
    public static class MathEx
    {
        public static double GetDistance(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public static PointF GetPointBetweenTwoPoints(PointF p1, PointF p2, double len)
        {
            double xDiff = p2.X - p1.X;
            double yDiff = p2.Y - p1.Y;
            PointF point = new PointF();

            if (Math.Abs(xDiff) < 0.001 && Math.Abs(yDiff) < 0.001)
            {
                point.X = p2.X;
                point.Y = p2.Y;
            }
            else if (Math.Abs(xDiff) < 0.001)  //Vertical line
            {
                point.X = p2.X;
                point.Y = (float)(yDiff > 0 ? p1.Y + len : p1.Y - len);
            }
            else if (Math.Abs(yDiff) < 0.001)  //Horizontal line
            {
                point.X = (float)(xDiff > 0 ? p1.X + len : p1.X - len);
                point.Y = p2.Y;
            }
            else
            {
                double k = (p2.Y - p1.Y) / (p2.X - p1.X);
                double b = p1.Y - k * p1.X;

                double xDiff1 = Math.Abs(len * Math.Cos(Math.Atan(k)));
                double x1 = xDiff > 0 ? p1.X + xDiff1 : p1.X - xDiff1;

                point.X = (float)x1;
                point.Y = (float)(k * x1 + b);
            }

            return point;
        }

        public static double GetDistance(List<PointF> points)
        {
            double len = 0;
            if (points.Count > 1)
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    len += GetDistance(points[i], points[i + 1]);
                }
            }
            return len;
        }

        public static double GetSlope(PointF p1, PointF p2)
        {
            return (double)((p1.Y - p2.Y) / (p1.X - p2.X));                 
        }

        public static List<PointF> GetLinePoints(PointF p1, PointF p2, float step)
        {
            float xOffset = p1.X - p2.X;
            float yOffset = p1.Y - p2.Y;
            List<PointF> list = new List<PointF>();

            double distance = Math.Sqrt(Math.Pow(xOffset, 2) + Math.Pow(yOffset, 2));
            if (distance < step)
            {
                list.Add(p1);
                list.Add(p2);
            }
            else
            {
                double angle = 0;
                if (Math.Abs(xOffset) < 0.001)
                {
                    angle = 90 * Math.PI / 180.0;
                }
                else
                {
                    angle = Math.Atan(Math.Abs(yOffset / xOffset));
                }

                double xFactor = xOffset < 0 ? 1 : -1;
                double yFactor = yOffset < 0 ? 1 : -1;

                for (double i = 0; i < distance; i += step)
                {
                    double x = p1.X + xFactor * i * Math.Cos(angle);
                    double y = p1.Y + yFactor * i * Math.Sin(angle);

                    list.Add(new PointF((float)x, (float)y));
                }

                list.Add(p2);
            }
      
            return list;
        }

        public static PointF GetArcPoint(PointF center, double radius, double angle)
        {
            float xDiff = (float)(radius * Math.Cos(angle * Math.PI / 180));
            float yDiff = (float)(radius * Math.Sin(angle * Math.PI / 180));
            return new PointF(center.X + xDiff, center.Y + yDiff);
        }

        public static bool Equals(PointF p1, PointF p2)
        {
            bool condition1 = Math.Abs(p1.X - p2.X) < 0.001;
            bool condition2 = Math.Abs(p1.Y - p2.Y) < 0.001;
            return condition1 && condition2;
        }

        public static bool IsInLine(PointF p, PointF p1, PointF p2)
        {
            double len1 = GetDistance(p1, p);
            double len2 = GetDistance(p, p2);
            double len3 = GetDistance(p1, p2);
            return Math.Abs(len1 + len2 - len3) < 0.0001;

            //float x = p1.X < p2.X ? p1.X : p2.X;
            //float y = p1.Y < p2.Y ? p1.Y : p2.Y;

            //float width = Math.Abs(p1.X - p2.X);
            //if (width <= 0.1)
            //{
            //    width = 0.1f;
            //}
            //float height = Math.Abs(p1.Y - p2.Y);
            //if (height <= 0.1)
            //{
            //    height = 0.1f;
            //}

            //var rect = new RectangleF(x, y, width, height);
            //rect.Inflate(1.1f, 1.1f);

            //return rect.Contains(p);
        }

        public static int GetInteger(this float num)
        {
            return (int)num;
        }

        public static float GetDecimal(this float num)
        {
            return num - num.GetInteger();
        }
    }
}

