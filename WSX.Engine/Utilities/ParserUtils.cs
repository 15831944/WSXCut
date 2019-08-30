using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.DrawModel;
using WSX.Engine.Models;

namespace WSX.Engine.Utilities
{
    public static class ParserUtils
    {
        private const int DEFAULT_BEZIER_POINTS_COUNT = 100;
        private const double ARC_SPILT_STEP = 0.2;
     
        public static PointF ToPointF(this UnitPoint p)
        {
            float x = (float)p.X;
            float y = (float)p.Y;
            return new PointF(x, y);
        }

        public static UnitPoint Offset(this UnitPoint p, double x, double y)
        {
            return new UnitPoint(p.X + x, p.Y + y);
        }

        public static PointF Offset(this PointF p, float x, float y)
        {
            return new PointF(p.X + x, p.Y + y);
        }

        public static List<DataUnit> Convert(List<IDrawLite> items, int layerId)
        {
            var res = new List<DataUnit>();
            items.ForEach(m =>
            {
                if (m is DotLite)
                {
                    var dot = m as DotLite;
                    res.Add(CreateDot(dot, layerId));
                }
                else if (m is LineLite)
                {
                    var line = m as LineLite;
                    res.Add(CreatePolyLine(line, layerId));
                }
                else if (m is ArcLite)
                {
                    var arc = m as ArcLite;
                    res.Add(CreateArc(arc, layerId));
                }
                else if (m is BezierLite)
                {
                    var bezier = m as BezierLite;
                    res.Add(CreateBezier(bezier, layerId));
                }
            });
            return res;
        }

        private static DataUnit CreateDot(DotLite dot, int layerId)
        {
            var p1 = dot.Point.ToPointF();
            var p2 = dot.Point.Offset(0.0001, 0.0001).ToPointF();
            return new DataUnit(DataUnitTypes.PointCut, layerId, new List<PointF> { p1, p2 });
        }

        private static DataUnit CreatePolyLine(LineLite line, int layerId)
        {
            return new DataUnit(DataUnitTypes.Polyline, layerId, line.Points.Select(x => x.ToPointF()).ToList());
        }

        private static DataUnit CreateArc(ArcLite arc, int layerId)
        {
            double step = 0;
            double factor = arc.SweepAngle < 0 ? -1 : 1;
            if (arc.Radius < 30)
            {
                //step = 1;
                step = GetArcSpiltStep(arc.Radius);
            }
            else if (arc.Radius < 60)
            {
                step = 0.5;
            }
            else
            {
                step = 0.25;
            }
            
            Func<double, double> handler = angle =>
            {
                if (angle > 360)
                {
                    angle -= 360;
                }
                if (angle < 0)
                {
                    angle += 360;
                }
                return angle;
            };

            var points = new List<PointF>();
            for (double i = 0; i < Math.Abs(arc.SweepAngle); i += step)
            {
                double angle = handler(arc.StartAngle + factor * i);
                var p = MathEx.GetArcPoint(arc.Center.ToPointF(), arc.Radius, angle);
                points.Add(p);
            }

            double endAngle = handler(arc.StartAngle + arc.SweepAngle);
            var finalPoint = MathEx.GetArcPoint(arc.Center.ToPointF(), arc.Radius, endAngle);
            points.Add(finalPoint);

            return new DataUnit(DataUnitTypes.Arc, layerId, points);
        }

        private static DataUnit CreateBezier(BezierLite bezier, int layerId)
        {
            BezierHelper helper = new BezierHelper();
            //2019-04-22: The precision of bezier maybe be changed in the future.
            var points = helper.GetBezierPoints(bezier.Points.Select(x => x.ToPointF()).ToArray(), DEFAULT_BEZIER_POINTS_COUNT);
            return new DataUnit(DataUnitTypes.Arc, layerId, points);
        }

        private static double GetArcSpiltStep(double radius)
        {
            double len = 2 * Math.PI * radius;
            return 360.0 * (ARC_SPILT_STEP / len);
        }
    }
}
