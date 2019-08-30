using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.FigureModel;
using WSX.DXF;
using WSX.DXF.Entities;
using WSX.DXF.Header;
using WSX.DXF.Tables;

namespace WSX.DataCollection.Utilities
{
    /// <summary>
    /// dxf图形操作
    /// </summary>
    public static class DxfHelper
    {
        private static int SN = 1;
        private static int precision = 10;
        /// <summary>
        /// 获取dxf文件中的图形数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<FigureBaseModel> LoadDXF(string path)
        {
            List<FigureBaseModel> values = new List<FigureBaseModel>();

            DxfDocument dxfdoc = DxfDocument.Load(path);
            var points = dxfdoc.Points.ToConverter();
            var lines = dxfdoc.Lines.ToConverter();
            var lwPolylines = dxfdoc.LwPolylines.ToConverter();
            var arcs = dxfdoc.Arcs.ToConverter();
            var circles = dxfdoc.Circles.ToConverter();
            var ellipses = dxfdoc.Ellipses.ToConverter();
            var leaders = dxfdoc.Leaders.ToConverter();
            var splines = dxfdoc.Splines.ToConverter();
            var xlines = dxfdoc.XLines.ToConverter();
            var mtexts = dxfdoc.MTexts.ToConverter();

            values.AddRange(points);
            values.AddRange(lines);
            values.AddRange(lwPolylines);
            values.AddRange(arcs);
            values.AddRange(circles);
            values.AddRange(ellipses);
            values.AddRange(splines);
            values.AddRange(xlines);
            values.AddRange(mtexts);

            SN = 1;
            return values;
        }
        /// <summary>
        /// 保存为DXF格式
        /// </summary>
        /// <param name="figures"></param>
        /// <param name="path"></param>
        public static void WriteDXF(List<FigureBaseModel> figures, string path)
        {
            DxfDocument dxf = new DxfDocument();
            foreach (FigureBaseModel figure in figures)
            {
                switch (figure.Type)
                {
                    case FigureTypes.Arc:
                        {
                            var fg = figure as ArcModel;
                            double startAngle = double.IsNaN(fg.StartAngle) ? 0 : fg.StartAngle;
                            double endAngle = double.IsNaN(fg.EndAngle) ? 0 : fg.EndAngle;
                            double sweepAngle = double.IsNaN(fg.AngleSweep) ? 0 : fg.AngleSweep;
                            if (fg.IsClockwise) endAngle = startAngle - sweepAngle;
                            else endAngle = startAngle + sweepAngle;
                            if (endAngle > 360) endAngle = endAngle - 360;
                            if (sweepAngle < 0)
                            {
                                double temp = startAngle;
                                startAngle = endAngle;
                                endAngle = temp;
                            }

                            Arc arc = new Arc(new Vector3(fg.Center.X, fg.Center.Y, 0),
                                fg.Radius,
                                startAngle,
                                endAngle
                                );
                            arc.Layer = new Layer("arc");
                            arc.Layer.Color.Index = (short)(figure.LayerId);
                            arc.Layer.Name = (figure.LayerId).ToString();
                            dxf.AddEntity(arc);
                        }
                        break;

                    case FigureTypes.Circle:
                        {
                            var fg = figure as CircleModel;
                            Circle circle = new Circle(new Vector3(fg.Center.X, fg.Center.Y, 0), fg.Radius);
                            circle.Layer = new Layer("circle with spaces");
                            circle.Layer.Color.Index = (short)(figure.LayerId);
                            circle.Layer.Name = (figure.LayerId).ToString();
                            dxf.AddEntity(circle);
                        }
                        break;
                    case FigureTypes.Point:
                        {
                            var fg = figure as PointModel;
                            Point point1 = new Point(new Vector3(fg.Point.X, fg.Point.Y, 0));
                            point1.Layer = new Layer("point");
                            point1.Layer.Color.Index = (short)(figure.LayerId);
                            point1.Layer.Name = (figure.LayerId).ToString();
                            dxf.AddEntity(point1);
                        }
                        break;
                    case FigureTypes.LwPolyline:
                        {
                            var fg = figure as LwPolylineModel;
                            List<LwPolylineVertex> lwVertexes = new List<LwPolylineVertex>();
                            foreach (UnitPointBulge pt in fg.Points)
                            {
                                LwPolylineVertex lwVertex = new LwPolylineVertex(pt.Point.X, pt.Point.Y, double.IsNaN(pt.Bulge) ? 0 : pt.Bulge);
                                lwVertexes.Add(lwVertex);
                            }
                            LwPolyline lwPolyline = new LwPolyline(lwVertexes, true);
                            lwPolyline.IsClosed = fg.IsFill;
                            lwPolyline.Layer.Color.Index = (short)(figure.LayerId);
                            lwPolyline.Layer = new Layer("lwpolyline");
                            lwPolyline.Layer.Name = (figure.LayerId).ToString();
                            dxf.AddEntity(lwPolyline);
                        }
                        break;
                    case FigureTypes.PolyBezier:
                        {

                        }
                        break;
                    default:
                        break;
                }
            }
            dxf.DrawingVariables.AcadVer = DxfVersion.AutoCad2010;
            dxf.Save(path);
        }

        #region 图形数据类型转换
        /// <summary>
        /// 单点转换
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static List<PointModel> ToConverter(this IEnumerable<WSX.DXF.Entities.Point> paras)
        {
            List<PointModel> values = new List<PointModel>();
            int channelPort = 1;//所在图层，默认给第一层
            foreach (var p in paras)
            {
                int.TryParse(p.Layer.Name, out channelPort);
                channelPort = (channelPort <= 0 || channelPort > 15) ? 1 : channelPort;
                values.Add(new PointModel()
                {
                    GroupParam = new GroupParam() { FigureSN = SN++, GroupSN = new List<int>() { 0 } },
                    LayerId = channelPort,
                    Point = new UnitPoint(p.Position.X, p.Position.Y),
                });
            }
            return values;
        }
        /// <summary>
        /// 直线转换
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static List<LwPolylineModel> ToConverter(this IEnumerable<Line> paras)
        {
            List<LwPolylineModel> values = new List<LwPolylineModel>();
            int channelPort = 1;//所在图层，默认给第一层
            foreach (var p in paras)
            {
                int.TryParse(p.Layer.Name, out channelPort);
                channelPort = (channelPort <= 0 || channelPort > 15) ? 1 : channelPort;
                values.Add(new LwPolylineModel()
                {
                    GroupParam = new GroupParam() { FigureSN = SN++, GroupSN = new List<int>() { 0 } },
                    LayerId = channelPort,
                    Points = new List<UnitPointBulge>() {
                         new UnitPointBulge(new UnitPoint(p.StartPoint.X,p.StartPoint.Y)),
                         new UnitPointBulge(new UnitPoint(p.EndPoint.X,p.EndPoint.Y))
                    }
                });
            }
            return values;
        }

        /// <summary>
        /// 多线段转换
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static List<LwPolylineModel> ToConverter(this IEnumerable<LwPolyline> paras)
        {
            List<LwPolylineModel> values = new List<LwPolylineModel>();
            int channelPort = 1;//所在图层，默认给第一层
            foreach (var p in paras)
            {
                int.TryParse(p.Layer.Name, out channelPort);
                channelPort = (channelPort <= 0 || channelPort > 15) ? 1 : channelPort;
                var lwPolyline = new LwPolylineModel()
                {
                    GroupParam = new GroupParam() { FigureSN = SN++, GroupSN = new List<int>() { 0 } },
                    LayerId = channelPort,
                    IsFill = p.IsClosed
                };
                foreach (var vertex in p.Vertexes)
                {
                    lwPolyline.Points.Add(new UnitPointBulge(new UnitPoint(vertex.Position.X, vertex.Position.Y))
                    {
                        Bulge = vertex.Bulge == 0 ? double.NaN : vertex.Bulge
                    });
                }
                System.Diagnostics.Debug.WriteLine("lwPolyline.Count:" + lwPolyline.PointCount);
                values.Add(lwPolyline);
            }


            return values;
        }
        /// <summary>
        /// 圆弧转换
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static List<ArcModel> ToConverter(this IEnumerable<Arc> paras)
        {
            List<ArcModel> values = new List<ArcModel>();
            int channelPort = 0;//所在图层，默认给第一层
            foreach (var p in paras)
            {
                int.TryParse(p.Layer.Name, out channelPort);
                channelPort = (channelPort <= 0 || channelPort > 15) ? 1 : channelPort;
                double startAngle = p.StartAngle;
                double endAngle = p.EndAngle;
                double sweepAngle = endAngle - startAngle;
                if (sweepAngle < 0)
                {
                    double temp = startAngle;
                    startAngle = endAngle;
                    endAngle = temp;
                    sweepAngle = -(360 + sweepAngle);
                }
                values.Add(new ArcModel()
                {
                    GroupParam = new GroupParam() { FigureSN = SN++, GroupSN = new List<int>() { 0 } },
                    LayerId = channelPort,
                    StartAngle = startAngle,
                    EndAngle = endAngle,
                    AngleSweep = (float)sweepAngle,
                    Radius = p.Radius,
                    Center = new UnitPoint() { X = p.Center.X, Y = p.Center.Y }
                });
            }
            return values;
        }
        /// <summary>
        /// 圆转换
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static List<CircleModel> ToConverter(this IEnumerable<Circle> paras)
        {
            List<CircleModel> values = new List<CircleModel>();
            int channelPort = 1;//所在图层，默认给第一层
            foreach (var p in paras)
            {
                int.TryParse(p.Layer.Name, out channelPort);
                channelPort = (channelPort <= 0 || channelPort > 15) ? 1 : channelPort;
                values.Add(new CircleModel()
                {
                    GroupParam = new GroupParam() { FigureSN = SN++, GroupSN = new List<int>() { 0 } },
                    LayerId = channelPort,
                    Radius = p.Radius,
                    Center = new UnitPoint() { X = p.Center.X, Y = p.Center.Y }
                });
            }
            return values;
        }
        /// <summary>
        /// 椭圆转换
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static List<LwPolylineModel> ToConverter(this IEnumerable<Ellipse> paras)
        {
            //List<EllipseModel> values = new List<EllipseModel>();
            //int channelPort = 1;//所在图层，默认给第一层
            //foreach (var p in paras)
            //{
            //    int.TryParse(p.Layer.Name, out channelPort);
            //    channelPort = (channelPort <= 0 || channelPort > 15) ? 1 : channelPort;
            //    values.Add(new EllipseModel()
            //    {
            //        SN = SN++,//p.Handle,
            //        LayerId = channelPort == 0 ? 1 : channelPort,
            //        StartAngle = p.StartAngle,
            //        EndAngle = p.EndAngle,
            //        IsFill = p.IsFullEllipse,
            //        MajorAxis = p.MajorAxis,
            //        MinorAxis = p.MinorAxis,
            //        Rotation = p.Rotation,
            //        Center = new UnitPoint() { X = p.Center.X, Y = p.Center.Y }
            //    });
            //}
            //return values;
            List<LwPolylineModel> values = new List<LwPolylineModel>();
            int channelPort = 1;//所在图层，默认给第一层
            foreach (var p in paras)
            {
                int.TryParse(p.Layer.Name, out channelPort);
                channelPort = (channelPort <= 0 || channelPort > 15) ? 1 : channelPort;
                LwPolyline lwpolyline = p.ToPolyline(precision * 5);
                values.Add(new LwPolylineModel()
                {
                    GroupParam = new GroupParam() { FigureSN = SN++, GroupSN = new List<int>() { 0 } },
                    LayerId = channelPort,
                    IsFill = lwpolyline.IsClosed,
                    Points = lwpolyline.Vertexes.Select(e => new UnitPointBulge(new UnitPoint(e.Position.X, e.Position.Y), e.Bulge == 0 ? double.NaN : e.Bulge)).ToList()
                });
            }
            return values;
        }

        private static List<FigureBaseModel> ToConverter(this IEnumerable<Leader> paras)
        {
            //List<FigureBaseModel> values = new List<EllipseModel>();
            //int channelPort = 1;//所在图层，默认给第一层
            //foreach (var p in paras)
            //{
            //int.TryParse(p.Layer.Name, out channelPort);
            //channelPort = (channelPort <= 0 || channelPort > 15) ? 1 : channelPort;
            //values.Add(new EllipseModel()
            //{
            //    SN = SN++,//p.Handle,
            //    LayerId = channelPort,
            //    StartAngle = p.StartAngle,
            //    EndAngle = p.EndAngle,
            //    IsFill = p.IsFullEllipse,
            //    MajorAxis = p.MajorAxis,
            //    MinorAxis = p.MinorAxis,
            //    Rotation = p.Rotation,
            //    Center = new UnitPoint() { X = p.Center.X, Y = p.Center.Y }
            //});
            //}
            //return values;
            return null;
        }
        /// <summary>
        /// 曲线转换
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        private static List<LwPolylineModel> ToConverter(this IEnumerable<Spline> paras)
        {
            List<LwPolylineModel> values = new List<LwPolylineModel>();
            int channelPort = 1;//所在图层，默认给第一层
            foreach (var p in paras)
            {
                int.TryParse(p.Layer.Name, out channelPort);
                channelPort = (channelPort <= 0 || channelPort > 15) ? 1 : channelPort;
                int count = (int)(p.Knots.Count * precision);
                count = count >= 10000 ? 10000 : count;
                Polyline polyline = p.ToPolyline(count);
                values.Add(new LwPolylineModel()
                {
                    GroupParam = new GroupParam() { FigureSN = SN++, GroupSN = new List<int>() { 0 } },
                    LayerId = channelPort,
                    IsFill = polyline.IsClosed,
                    Points = polyline.Vertexes.Select(e => new UnitPointBulge(new UnitPoint(e.Position.X, e.Position.Y))).ToList()
                });
            }
            return values;
        }
        private static List<LwPolylineModel> ToConverter(this IEnumerable<XLine> paras)
        {
            List<LwPolylineModel> values = new List<LwPolylineModel>();
            int channelPort = 1;//所在图层，默认给第一层
            //foreach (var p in paras)
            //{
            //    int.TryParse(p.Layer.Name, out channelPort);
            //    channelPort = (channelPort <= 0 || channelPort > 15) ? 1 : channelPort;
            //    values.Add(new LwPolylineModel()
            //    {
            //        SN = SN++,//p.Handle,
            //        LayerId = channelPort,
            //        Points = new List<UnitPointBulge>() {
            //             new UnitPointBulge() { Point=new UnitPoint(p.Origin.X, p.Origin.Y), Bulge = double.NaN },
            //             new UnitPointBulge() { Point=new UnitPoint(p.Direction.X, p.Direction.Y), Bulge = double.NaN }
            //        }
            //    });
            //}
            return values;
        }
        private static List<LwPolylineModel> ToConverter(this IEnumerable<MText> paras)
        {
            List<LwPolylineModel> values = new List<LwPolylineModel>();
            int channelPort = 1;//所在图层，默认给第一层
            foreach (var p in paras)
            {
                int.TryParse(p.Layer.Name, out channelPort);
                channelPort = (channelPort <= 0 || channelPort > 15) ? 1 : channelPort;
                var lines = MTextHelper.BuildTextPath(p, new System.Windows.Media.Typeface(
                        new System.Windows.Media.FontFamily(p.Style.FontFamilyName),
                        System.Windows.FontStyles.Normal,
                        System.Windows.FontWeights.Bold,
                        System.Windows.FontStretches.Normal), (int)p.Height);
                foreach (var line in lines)
                {
                    values.Add(new LwPolylineModel()
                    {
                        GroupParam = new GroupParam() { FigureSN = SN++, GroupSN = new List<int>() { 0 } },
                        LayerId = channelPort,
                        IsFill = line.IsFill,
                        Points = line.Points,
                    });
                }
            }

            return values;
        }
        #endregion


    }

    public class MTextHelper
    {
        public static List<LwPolylineModel> BuildTextPath(MText mText, System.Windows.Media.Typeface typeface, int fontSize)
        {
            string text = mText.PlainText();
            var positon = new System.Windows.Point(mText.Position.X, -mText.Position.Y);
            var data = GetTextGeometry(text, positon, 360 - mText.Rotation, typeface, fontSize);
            var figures = data.GetOutlinedPathGeometry().Figures;
            SortedList<double, LwPolylineModel> sortLines = new SortedList<double, LwPolylineModel>();
            foreach (var fig in figures)
            {
                LwPolylineModel line = new LwPolylineModel();
                line.IsFill = fig.IsClosed;
                foreach (var seg in fig.Segments)
                {
                    if (seg is PolyLineSegment)
                    {
                        var segs = seg as PolyLineSegment;
                        var points = segs.Points.Select(e =>
                            {
                                var point = GetSymmetryPointByLine(new UnitPoint(e.X, e.Y), 0, 1, 0);
                                return new UnitPointBulge(point);
                            }).ToList();
                        line.Points.AddRange(points);
                    }
                    else if (seg is ArcSegment)
                    { }
                    else if (seg is BezierSegment)
                    { }
                    else if (seg is LineSegment)
                    { }
                    else if (seg is PolyBezierSegment)
                    { }
                    else if (seg is PolyQuadraticBezierSegment)
                    { }
                    else if (seg is QuadraticBezierSegment)
                    { }
                    else
                    { }
                }
                sortLines.Add(Distance(new UnitPoint(mText.Position.X, -mText.Position.Y), new UnitPoint(fig.StartPoint.X, fig.StartPoint.Y), true), line);
            }
            return sortLines.Values.ToList();
        }
        /// <summary>
        /// 创建文本路径
        /// </summary>
        /// <param name="word">文本字符串</param>
        /// <param name="point">显示位置</param>
        /// <param name="typeface">字体信息</param>
        /// <param name="fontSize">字体大小</param>
        /// <returns></returns>
        public static Geometry GetTextGeometry(string word, System.Windows.Point point, double angle, Typeface typeface, int fontSize)
        {
            FormattedText text = new FormattedText(word, new System.Globalization.CultureInfo("zh-cn"), System.Windows.FlowDirection.LeftToRight, typeface, fontSize, System.Windows.Media.Brushes.Black);
            Geometry geo = text.BuildGeometry(point);
            geo.SetValue(Geometry.TransformProperty, new System.Windows.Media.RotateTransform(angle, point.X, point.Y));
            PathGeometry path = geo.GetFlattenedPathGeometry(0.0001, ToleranceType.Relative);
            return path;
        }
        /// <summary>
        /// 获取点p1与直线Ax+By+C=0的对称点
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <returns></returns>
        public static UnitPoint GetSymmetryPointByLine(UnitPoint p1, double A, double B, double C)
        {
            double x = ((B * B - A * A) * p1.X - 2 * A * B * p1.Y - 2 * A * C) / (A * A + B * B);
            double y = (-2 * A * B * p1.X + (A * A - B * B) * p1.Y - 2 * B * C) / (A * A + B * B);
            return new UnitPoint(x, y);
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
    }
}
