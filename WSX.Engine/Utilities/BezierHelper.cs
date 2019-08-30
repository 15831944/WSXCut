using System.Collections.Generic;
using System.Drawing;

namespace WSX.Engine.Utilities
{
    public class BezierHelper
    {
        private PointF GetBezierInterpolationPoint(float t, PointF[] points, int count)
        {
            PointF[] tmp = new PointF[count];
            for (int i = 1; i < count; ++i)
            {
                for (int j = 0; j < count - i; ++j)
                {
                    if (i == 1)
                    {
                        tmp[j].X = (float)(points[j].X * (1 - t) + points[j + 1].X * t);
                        tmp[j].Y = (float)(points[j].Y * (1 - t) + points[j + 1].Y * t);
                        continue;
                    }
                    tmp[j].X = (float)(tmp[j].X * (1 - t) + tmp[j + 1].X * t);
                    tmp[j].Y = (float)(tmp[j].Y * (1 - t) + tmp[j + 1].Y * t);
                }
            }
            return tmp[0];
        }

        public List<PointF> GetBezierPoints(PointF[] points, int outCount)
        {
            List<PointF> res = new List<PointF>();
            float step = 1.0f / outCount;
            float t = 0;
            for (int i = 0; i <= outCount; i++)
            {
                PointF tmp = GetBezierInterpolationPoint(t, points, points.Length);    // 计算插值点
                t += step;
                res.Add(tmp);
            }
            return res;
        }
    }
}
