using System;
using System.Collections.Generic;
using System.Linq;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.LeadLine;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.DrawService.DrawModel;
using WSX.DrawService.DrawTool.Arcs;
using WSX.DrawService.DrawTool.CircleTool;
using WSX.DrawService.DrawTool.MultiSegmentLine;

namespace WSX.DrawService.Utils
{
    public class MicroConnectHelper
    {
        private static float errorRange = 0.000001f;

        #region 微连参数计算
        /// <summary>
        /// 反向微连参数
        /// </summary>
        /// <param name="microConnects"></param>
        /// <param name="isCloseFigure"></param>
        /// <param name="count">圆和圆弧默认2个点，多段线对应点个数</param>
        public static void ReverseMicroConnParams(List<MicroConnectModel> microConnects, bool isCloseFigure, int count = 2)
        {
            if (microConnects != null)
            {
                for (int i = 0; i < microConnects.Count; i++)
                {
                    if (microConnects[i].Position == 0) continue;
                    int seg = (int)Math.Truncate(microConnects[i].Position);
                    if (count > 2) seg = count - seg - (isCloseFigure ? 1 : 2);
                    float dec = microConnects[i].Position % 1;
                    microConnects[i].Position = seg + (1 - dec);
                }
            }
        }

        /// <summary>
        /// 计算自动冷却点的参数
        /// </summary>
        /// <param name="points"></param>
        /// <param name="isCloseFigure"></param>
        /// <param name="maxAngle"></param>
        /// <returns></returns>
        public static List<MicroConnectModel> CalAutoCoolPointParams(List<UnitPointBulge> points, bool isCloseFigure, double maxAngle, double leadinPosition)
        {
            List<MicroConnectModel> mParams = new List<MicroConnectModel>();
            if (points == null || points.Count <= 2) return mParams;
            for (int i = 0; i < points.Count; i++)
            {
                //if (i == 0 || (!isCloseFigure && i == points.Count - 1)) continue;
                if (i == leadinPosition) continue;
                if (!isCloseFigure && (i == 0 || i == points.Count - 1)) continue;
                int indexPer = (i - 1 < 0) ? points.Count - 1 : i - 1;//前一段
                int indexCur = i;//当前段
                int indexNext = (i + 1 > points.Count - 1) ? 0 : i + 1;//后一段
                if (points[indexPer].HasMicroConn || points[indexCur].HasMicroConn) continue;

                UnitPointBulge p1 = points[indexPer];
                UnitPointBulge p2 = points[indexCur];
                UnitPointBulge p3 = points[indexNext];

                UnitPoint point1, point3;

                #region 获取要计算的点，或者切线点
                if (!double.IsNaN(p1.Bulge) && !double.IsNaN(p2.Bulge))
                {
                    //圆弧到圆弧
                    ArcModelMini arc1 = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                    ArcModelMini arc2 = DrawingOperationHelper.GetArcParametersFromBulge(p2.Point, p3.Point, (float)p2.Bulge);
                    var temps1 = DrawingOperationHelper.GetLinePointByVerticalLine(p2.Point, arc1.Center, 10);
                    var temps2 = DrawingOperationHelper.GetLinePointByVerticalLine(p2.Point, arc2.Center, 10);
                    point1 = !arc1.Clockwise ? temps1.Item1 : temps1.Item2;
                    point3 = arc2.Clockwise ? temps2.Item1 : temps2.Item2;

                }
                else if (double.IsNaN(p1.Bulge) && !double.IsNaN(p2.Bulge))
                {
                    //直线到圆弧
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p2.Point, p3.Point, (float)p2.Bulge);
                    var temps = DrawingOperationHelper.GetLinePointByVerticalLine(p2.Point, arc.Center, 10);
                    point1 = p1.Point;
                    point3 = arc.Clockwise ? temps.Item1 : temps.Item2;

                }
                else if (!double.IsNaN(p1.Bulge) && double.IsNaN(p2.Bulge))
                {
                    //圆弧到直线
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                    var temps = DrawingOperationHelper.GetLinePointByVerticalLine(p2.Point, arc.Center, 10);
                    point1 = !arc.Clockwise ? temps.Item1 : temps.Item2;
                    point3 = p3.Point;

                }
                else
                {
                    //直线到直线
                    point1 = p1.Point;
                    point3 = p3.Point;
                }
                #endregion

                double angle = DrawingOperationHelper.Angle(point1, p2.Point, point3);
                if (Math.Round(angle, 2) <= maxAngle)
                {
                    mParams.Add(new MicroConnectModel() { Flags = MicroConnectFlags.CoolingPoint, Size = 0, Position = i });
                }
            }
            return mParams;
        }

        /// <summary>
        /// 计算多段线的自动微连参数
        /// </summary>
        /// <param name="points"></param>
        /// <param name="isCloseFigure"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<MicroConnectModel> CalAutoMicroConnsParams(List<UnitPointBulge> points, double totalLength, bool isCloseFigure, AutoMicroConParam param)
        {
            List<MicroConnectModel> micros = new List<MicroConnectModel>();
            double spacing = 0;
            if (param.IsTypeCount)
            {
                int count = param.IsNotApplyStartPoint ? ((int)param.TypeValue + 1) : (int)param.TypeValue;
                spacing = totalLength / count;
            }
            else
            {
                spacing = param.TypeValue;
            }
            if (spacing <= 0 || spacing < param.MicroSize) return micros;
            List<KeyValuePair<double, double>> keyPos = new List<KeyValuePair<double, double>>();
            double remLength = 0;
            int pointCount = isCloseFigure ? points.Count : points.Count - 1;
            for (int index = 0; index < pointCount; index++)
            {
                int next = index + 1 >= points.Count ? 0 : index + 1;
                var p1 = points[index];
                var p2 = points[next];
                double curLength = 0;
                if (!double.IsNaN(p1.Bulge))
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                    curLength = DrawingOperationHelper.GetArcLength(arc.Radius, arc.SweepAngle);
                }
                else
                {
                    curLength = HitUtil.Distance(p1.Point, p2.Point);
                }
                if (curLength > remLength)
                {
                    double hasLength = curLength - remLength;
                    double firstPos = index + remLength / curLength;
                    keyPos.Add(new KeyValuePair<double, double>(firstPos, curLength));
                    if (hasLength > spacing)
                    {
                        int count = (int)(hasLength / spacing);
                        double perPos = spacing / curLength;
                        for (int i = 1; i <= count; i++)
                        {
                            keyPos.Add(new KeyValuePair<double, double>(firstPos + perPos * i, curLength));
                        }
                        remLength = spacing - (hasLength - spacing * (count));
                    }
                    else
                    {
                        remLength = spacing - hasLength;
                    }
                }
                else
                {
                    remLength = remLength - curLength;
                }
            }
            keyPos.RemoveAll(e => (e.Key + errorRange) >= pointCount);//清除不合法的pos
            if (param.IsNotApplyStartPoint) //过滤起点
            {
                keyPos.RemoveAll(e => e.Key == 0);
            }
            if (param.IsNotApplyCorner) //过滤拐角
            {
                if (param.IsTypeCount)
                {
                    for (int index = 0; index < pointCount; index++)
                    {
                        int count = keyPos.Count(e => Math.Truncate(e.Key) == index);
                        for (int i = 1; i <= count; i++)
                        {
                            micros.Add(new MicroConnectModel()
                            {
                                Flags = MicroConnectFlags.MicroConnect,
                                Position = index + i * ((float)1 / (count + 1)),
                                Size = (float)param.MicroSize
                            });
                        }
                    }
                }
                else
                {
                    double maxLength = param.MicroSize * 5;
                    keyPos.RemoveAll(e =>
                    {
                        double perLength = (e.Key % 1) * e.Value;
                        double lastLength = e.Value - (perLength + param.MicroSize);
                        return (perLength < maxLength || lastLength < maxLength);
                    });
                    micros.AddRange(keyPos.Select(e => new MicroConnectModel()
                    {
                        Flags = MicroConnectFlags.MicroConnect,
                        Position = (float)e.Key,
                        Size = (float)param.MicroSize
                    }));
                }
            }
            else
            {
                micros.AddRange(keyPos.Select(e => new MicroConnectModel()
                {
                    Flags = MicroConnectFlags.MicroConnect,
                    Position = (float)e.Key,
                    Size = (float)param.MicroSize
                }));
            }

            return micros;
        }

        /// <summary>
        /// 圆或者圆弧计算自动微连参数
        /// </summary>
        /// <param name="totalLength"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<MicroConnectModel> CalAutoMicroConnsParams(double totalLength, AutoMicroConParam param)
        {
            List<MicroConnectModel> micros = new List<MicroConnectModel>();
            int count = (int)(param.IsTypeCount ? param.TypeValue : (totalLength / param.TypeValue));
            if (param.IsNotApplyStartPoint && param.IsTypeCount)
            {
                count += 1;
            }
            if (!param.IsNotApplyStartPoint)
            {
                micros.Add(new MicroConnectModel()
                {
                    Flags = MicroConnectFlags.MicroConnect,
                    Position = 0,
                    Size = (float)param.MicroSize
                });
            }
            double segPos = 1.0 / count;
            for (int i = 1; i < count; i++)
            {
                micros.Add(new MicroConnectModel()
                {
                    Flags = MicroConnectFlags.MicroConnect,
                    Position = (float)(segPos * i),
                    Size = (float)param.MicroSize
                });
            }
            return micros;
        }
        #endregion

        #region 微连点计算
        /// <summary>
        /// 计算圆微连
        /// </summary>
        /// <param name="circle">要计算的圆</param>
        /// <param name="microStartPoints">微连起点</param>
        /// <returns></returns>
        public static List<ArcBase> CalMicroconnsByCircle(Circle circle, List<MicroConnectModel> microConnParams, out List<MicroUnitPoint> microStartPoints)
        {
            List<ArcBase> arcMicroConns = new List<ArcBase>();
            List<MicroUnitPoint> micros = new List<MicroUnitPoint>();
            microStartPoints = new List<MicroUnitPoint>();
            double totalLength = circle.SizeLength;

            #region 1.计算出微连起点和终点坐标
            foreach (MicroConnectModel m in microConnParams)
            {
                if (m.Size >= totalLength) continue;
                double startAngle = circle.StartAngle + m.Position * circle.AngleSweep;//起点
                UnitPoint startPoint = HitUtil.PointOnCircle(circle.Center, circle.Radius, HitUtil.DegreesToRadians(startAngle));
                double endAngle = startAngle + m.Size / totalLength * circle.AngleSweep;//终点
                UnitPoint endPoint = HitUtil.PointOnCircle(circle.Center, circle.Radius, HitUtil.DegreesToRadians(endAngle));
                var microStart = new MicroUnitPoint()
                {
                    Point = new UnitPointBulge(startPoint) { HasMicroConn = true },
                    StartLength = m.Position * totalLength,
                    Flags = m.Flags,
                    SizeLength = m.Size,
                };
                if (m.Flags == MicroConnectFlags.MicroConnect)
                {
                    micros.Add(microStart);
                    double endSize = m.Position * totalLength + m.Size;
                    micros.Add(new MicroUnitPoint()
                    {
                        Point = new UnitPointBulge(endPoint),
                        StartLength = endSize > totalLength ? endSize - totalLength : endSize,
                        SizeLength = 0,
                    });
                    microStartPoints.Add(microStart);
                }
                else if (m.Flags == MicroConnectFlags.GapPoint)
                {
                    if (!micros.Exists(p => { return HitUtil.Distance(microStart.Point.Point, p.Point.Point) < 0.00001f; }))
                    {
                        microStart.Point.HasMicroConn = false;
                        microStart.SizeLength = 0;
                        micros.Add(microStart);
                    }
                    micros.Add(new MicroUnitPoint()
                    {
                        Point = new UnitPointBulge(endPoint, hasMicroConn: true),
                        StartLength = totalLength + m.Size,
                        SizeLength = Math.Abs(m.Size),
                    });
                }
                else if (m.Flags == MicroConnectFlags.CoolingPoint)
                {
                    microStart.Point.HasMicroConn = false;
                    micros.Add(microStart);
                    microStartPoints.Add(microStart);
                }
            }
            #endregion

            #region 2.根据距离起点的距离排序，更新微连起点和终点区间没有微连标记的点，标记为微连标志
            micros = micros.OrderBy(e => e.StartLength).ToList();
            for (int i = 0; i < micros.Count; i++)
            {
                MicroUnitPoint m = micros[i];
                if (m.Point.HasMicroConn)
                {
                    List<MicroUnitPoint> points = null;
                    if (m.StartLength + m.SizeLength > totalLength)//区间跨过了分界点(起点->分界点->终点)
                    {
                        points = micros.Where(e => ((e.StartLength > m.StartLength && e.StartLength <= totalLength) ||
                                                    (e.StartLength >= 0 && e.StartLength < (m.StartLength + m.SizeLength - totalLength - errorRange))) &&
                                                    !e.Point.HasMicroConn).ToList();
                    }
                    else
                    {
                        points = micros.Where(e => (e.StartLength > m.StartLength && e.StartLength < (m.StartLength + m.SizeLength - errorRange)) &&
                                 !e.Point.HasMicroConn).ToList();
                    }
                    if (points != null)
                    {
                        points.ForEach(e => { e.Point.HasMicroConn = true; });
                    }
                }
            }
            #endregion

            #region 3.排除冷却点
            micros.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingPoint);
            microStartPoints.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingPoint && e.Point.HasMicroConn);
            #endregion

            #region 4.获取最后有效的Arc
            for (int i = 0; i < micros.Count - 1; i++)
            {
                if (!micros[i].Point.HasMicroConn)//有效图形,更新为Arc
                {
                    double startAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(circle.Center, micros[i].Point.Point, 0));
                    double endAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(circle.Center, micros[i + 1].Point.Point, 0));
                    ArcBase arc = new ArcBase()
                    {
                        Center = circle.Center,
                        Radius = circle.Radius,
                        StartAngle = (float)startAngle,
                        AngleSweep = (float)HitUtil.CalAngleSweep(startAngle, endAngle, circle.IsClockwise)
                    };
                    //arc.startPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.StartAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.StartAngle)) + arc.Center.Y);
                    //arc.midPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.MidAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.MidAngle)) + arc.Center.Y);
                    //arc.endPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.EndAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.EndAngle)) + arc.Center.Y);
                    arc.startPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.StartAngle));
                    arc.midPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.MidAngle));
                    arc.endPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.EndAngle));

                    arcMicroConns.Add(arc);
                }
            }
            if (micros.Count >= 2 && !micros[micros.Count - 1].Point.HasMicroConn)//最后一个有效图形,更新为Arc
            {
                double startAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(circle.Center, micros[micros.Count - 1].Point.Point, 0));
                double endAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(circle.Center, micros[0].Point.Point, 0));
                ArcBase arc = new ArcBase()
                {
                    Center = circle.Center,
                    Radius = circle.Radius,
                    StartAngle = (float)startAngle,
                    AngleSweep = (float)HitUtil.CalAngleSweep(startAngle, endAngle, circle.IsClockwise)
                };
                //arc.startPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.StartAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.StartAngle)) + arc.Center.Y);
                //arc.midPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.MidAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.MidAngle)) + arc.Center.Y);
                //arc.endPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.EndAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.EndAngle)) + arc.Center.Y);
                arc.startPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.StartAngle));
                arc.midPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.MidAngle));
                arc.endPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.EndAngle));
                arcMicroConns.Add(arc);
            }
            if (arcMicroConns.Count == 0)
            {
                arcMicroConns.Add(new ArcBase()
                {
                    Center = circle.Center,
                    Radius = circle.Radius,
                    StartAngle = circle.StartAngle,
                    AngleSweep = circle.AngleSweep
                });
            }
            #endregion

            return arcMicroConns;
        }

        /// <summary>
        /// 计算圆弧微连
        /// </summary>
        /// <param name="arcBase">要计算的圆弧</param>
        /// <param name="microStartPoints">微连起点</param>
        /// <returns></returns>
        public static List<ArcBase> CalMicroconnsByArc(ArcBase arcBase, List<MicroConnectModel> microConnParams, out List<MicroUnitPoint> microStartPoints)
        {
            double totalLength = arcBase.SizeLength;
            List<ArcBase> arcMicroConns = new List<ArcBase>();
            List<MicroUnitPoint> micros = new List<MicroUnitPoint>();
            microStartPoints = new List<MicroUnitPoint>();
            micros.Add(new MicroUnitPoint() { Point = new UnitPointBulge() { Point = arcBase.startPoint }, StartLength = 0 });
            micros.Add(new MicroUnitPoint() { Point = new UnitPointBulge() { Point = arcBase.endPoint, HasMicroConn = true }, StartLength = totalLength });

            #region 1.计算出微连起点和终点坐标
            foreach (MicroConnectModel m in microConnParams)
            {
                if (m.Size >= totalLength) continue;
                double startAngle = arcBase.StartAngle + m.Position * arcBase.AngleSweep;//起点
                UnitPoint startPoint = m.Position == 0 ? arcBase.startPoint : HitUtil.PointOnCircle(arcBase.Center, arcBase.Radius, HitUtil.DegreesToRadians(startAngle));
                var microStart = new MicroUnitPoint()
                {
                    Point = new UnitPointBulge(startPoint) { HasMicroConn = true },
                    StartLength = m.Position * totalLength,
                    Flags = m.Flags,
                    SizeLength = m.Size,
                };
                if (m.Flags == MicroConnectFlags.MicroConnect)
                {
                    micros.RemoveAll(e => e.Point.Point == startPoint);
                    micros.Add(microStart);
                    microStartPoints.Add(microStart);

                    if (microStart.StartLength + microStart.SizeLength < totalLength)
                    {
                        double endAngle = startAngle + m.Size / totalLength * arcBase.AngleSweep;//终点
                        UnitPoint endPoint = HitUtil.PointOnCircle(arcBase.Center, arcBase.Radius, HitUtil.DegreesToRadians(endAngle));
                        double endSize = m.Position * totalLength + m.Size;
                        micros.Add(new MicroUnitPoint()
                        {
                            Point = new UnitPointBulge(endPoint),
                            StartLength = endSize > totalLength ? endSize - totalLength : endSize,
                            SizeLength = 0,
                        });
                    }
                }
                else if (m.Flags == MicroConnectFlags.CoolingPoint)
                {
                    microStart.Point.HasMicroConn = false;
                    micros.Add(microStart);
                    microStartPoints.Add(microStart);
                }
                else if (m.Flags == MicroConnectFlags.GapPoint)
                {
                    microStart.SizeLength = 0;//totalLength;
                    micros.Add(microStart);

                    double endAngle = startAngle + m.Size / totalLength * arcBase.AngleSweep;//终点
                    UnitPoint endPoint = HitUtil.PointOnCircle(arcBase.Center, arcBase.Radius, HitUtil.DegreesToRadians(endAngle));
                    double endSize = m.Position * totalLength + m.Size;
                    micros.Add(new MicroUnitPoint()
                    {
                        Point = new UnitPointBulge(endPoint, hasMicroConn: true),
                        StartLength = endSize > totalLength ? endSize - totalLength : endSize,
                        SizeLength = Math.Abs(m.Size),
                    });
                }
            }
            #endregion

            #region 2.根据距离起点的距离排序，更新微连起点和终点区间没有微连标记的点，标记为微连标志
            micros = micros.OrderBy(e => e.StartLength).ToList();
            for (int i = 0; i < micros.Count; i++)
            {
                MicroUnitPoint m = micros[i];
                if (m.Point.HasMicroConn)
                {
                    var points = micros.Where(e => (e.StartLength > m.StartLength && e.StartLength < m.StartLength + m.SizeLength - errorRange) && !e.Point.HasMicroConn).ToList();
                    if (points != null)
                    {
                        points.ForEach(e => { e.Point.HasMicroConn = true; });
                    }
                }
            }
            #endregion

            #region 3.排除冷却点
            micros.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingPoint);
            microStartPoints.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingPoint && e.Point.HasMicroConn);
            #endregion

            #region 4.获取最后有效的Arc
            for (int i = 0; i < micros.Count - 1; i++)
            {
                if (!micros[i].Point.HasMicroConn)//有效图形,更新为Arc
                {
                    double startAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(arcBase.Center, micros[i].Point.Point, 0));
                    double endAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(arcBase.Center, micros[i + 1].Point.Point, 0));
                    ArcBase arc = new ArcBase()
                    {
                        Center = arcBase.Center,
                        Radius = arcBase.Radius,
                        StartAngle = (float)startAngle,
                        AngleSweep = (float)HitUtil.CalAngleSweep(startAngle, endAngle, arcBase.IsClockwise)
                    };
                    //arc.startPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.StartAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.StartAngle)) + arc.Center.Y);
                    //arc.midPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.MidAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.MidAngle)) + arc.Center.Y);
                    //arc.endPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.EndAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.EndAngle)) + arc.Center.Y);
                    arc.startPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.StartAngle));
                    arc.midPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.MidAngle));
                    arc.endPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.EndAngle));
                    arcMicroConns.Add(arc);
                }
            }
            if (micros.Count >= 2 && !micros[micros.Count - 1].Point.HasMicroConn)//最后一个有效图形,更新为Arc
            {
                double startAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(arcBase.Center, micros[micros.Count - 1].Point.Point, 0));
                double endAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(arcBase.Center, micros[0].Point.Point, 0));
                ArcBase arc = new ArcBase()
                {
                    Center = arcBase.Center,
                    Radius = arcBase.Radius,
                    StartAngle = (float)startAngle,
                    AngleSweep = (float)HitUtil.CalAngleSweep(startAngle, endAngle, arcBase.IsClockwise)
                };
                //arc.startPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.StartAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.StartAngle)) + arc.Center.Y);
                //arc.midPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.MidAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.MidAngle)) + arc.Center.Y);
                //arc.endPoint = new UnitPoint(arc.Radius * Math.Cos(HitUtil.DegreesToRadians(arc.EndAngle)) + arc.Center.X, arc.Radius * Math.Sin(HitUtil.DegreesToRadians(arc.EndAngle)) + arc.Center.Y);
                arc.startPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.StartAngle));
                arc.midPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.MidAngle));
                arc.endPoint = HitUtil.PointOnCircle(arc.Center, arc.Radius, HitUtil.DegreesToRadians(arc.EndAngle));
                arcMicroConns.Add(arc);
            }
            #endregion

            return arcMicroConns;
        }

        /// <summary>
        /// 多段线微连
        /// </summary>
        /// <returns></returns>
        public static List<UnitPointBulge> CalMicroconnsByMultiSegLine(List<UnitPointBulge> points, bool isCloseFigure, List<MicroConnectModel> microConnParams, out List<MicroUnitPoint> microStartPoints, bool isCompensation = false, double size = 0, bool isOutside = false, List<UnitPointBulge> basePoints = null)
        {
            microStartPoints = new List<MicroUnitPoint>();
            if (microConnParams == null || microConnParams.Count == 0) return points;
            double totalLength = 0;
            List<UnitPointBulge> rets = new List<UnitPointBulge>();
            List<MicroUnitPoint> temps = MicroConnectHelper.InitMicroUnitPoint(points, isCloseFigure, out totalLength);
            List<MicroUnitPoint> micros = new List<MicroUnitPoint>(temps);

            #region 1.计算起点和终点
            foreach (MicroConnectModel m in microConnParams)
            {
                if (m.Flags == MicroConnectFlags.MicroConnect)
                {
                    MicroUnitPoint startPoint = GetMicroStartPoint(points, m, temps, isCompensation, size, isOutside, basePoints);
                    microStartPoints.Add(startPoint);
                    micros.Add(startPoint);
                    MicroUnitPoint endPoint = GetMicroEndPointByPositive(points, startPoint, startPoint.OwerPos, startPoint.SizeLength, temps);
                    micros.Add(endPoint);
                }
                else if (m.Flags == MicroConnectFlags.GapPoint)
                {
                    MicroUnitPoint startPoint = GetMicroStartPoint(points, m, temps, isCompensation, size, isOutside, basePoints);
                    startPoint.SizeLength = 0;
                    startPoint.Point.HasMicroConn = false;
                    micros.Add(startPoint);
                    MicroUnitPoint endPoint = GetMicroEndPointByNegative(points, startPoint, startPoint.OwerPos, Math.Abs(m.Size), temps);
                    endPoint.SizeLength = Math.Abs(m.Size);
                    endPoint.Flags = MicroConnectFlags.GapPoint;
                    micros.Add(endPoint);
                }
                else if (m.Flags == MicroConnectFlags.CoolingPoint ||
                         m.Flags == MicroConnectFlags.LeadInPoint ||
                         m.Flags == MicroConnectFlags.LeadOutPoint)
                {
                    MicroUnitPoint startPoint = GetMicroStartPoint(points, m, temps, isCompensation, size, isOutside, basePoints);
                    startPoint.Point.HasMicroConn = false;
                    micros.Add(startPoint);
                    microStartPoints.Add(startPoint);
                }
            }
            #endregion

            #region 2.排序更新微连之间的点
            micros = micros.OrderBy(e => e.StartLength).ToList();
            List<MicroUnitPoint> dels = new List<MicroUnitPoint>();//需要去除间距小于0.000001F的点
            for (int i = 0; i < micros.Count - 1; i++)
            {
                int next = i + 1 == points.Count ? 0 : i + 1;
                if ((micros[i].Flags == MicroConnectFlags.GapPoint || (micros[i].Flags == MicroConnectFlags.MicroConnect)) &&
                   (micros[next].Flags == MicroConnectFlags.MicroConnect || (micros[next].Flags == MicroConnectFlags.GapPoint)))
                {

                    if (Math.Abs(micros[i].StartLength - micros[next].StartLength) < 0.000001f)
                    {
                        if (micros[i].SizeLength >= micros[next].SizeLength)
                        {
                            if (micros[next].Point.HasMicroConn)
                            { micros[i].Point.HasMicroConn = micros[next].Point.HasMicroConn; }
                            dels.Add(micros[next]);
                        }
                        else
                        {
                            if (micros[i].Point.HasMicroConn)
                            { micros[next].Point.HasMicroConn = micros[i].Point.HasMicroConn; }
                            dels.Add(micros[i]);
                        }
                    }
                }
            }
            dels.ForEach(e => { micros.Remove(e); });
            for (int i = 0; i < micros.Count; i++)
            {
                MicroUnitPoint m = micros[i];
                if (m.Point.HasMicroConn)
                {
                    List<MicroUnitPoint> hasMicros = null;
                    if (m.StartLength + m.SizeLength > totalLength && isCloseFigure)
                    {
                        hasMicros = micros.Where(e => ((e.StartLength > m.StartLength && e.StartLength <= totalLength) ||
                                                (e.StartLength >= 0 && e.StartLength < (m.StartLength + m.SizeLength - totalLength - errorRange))) &&
                                                !e.Point.HasMicroConn).ToList();
                    }
                    else
                    {
                        hasMicros = micros.Where(e => (e.StartLength > m.StartLength && e.StartLength < (m.StartLength + m.SizeLength - errorRange)) && !e.Point.HasMicroConn).ToList();
                    }
                    if (hasMicros != null && hasMicros.Count > 0)
                    {
                        hasMicros.ForEach(e => { e.Point.HasMicroConn = true; });
                    }
                }
            }
            #endregion

            #region 3.排除冷却点
            micros.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingPoint ||
                                  e.Flags == MicroConnectFlags.LeadInPoint ||
                                  e.Flags == MicroConnectFlags.LeadOutPoint);
            microStartPoints.RemoveAll(e => e.Flags == MicroConnectFlags.CoolingPoint && e.Point.HasMicroConn);
            #endregion

            #region 4.更新有效的弧
            for (int i = 0; i < micros.Count - 1; i++)
            {
                if (!micros[i].Point.HasMicroConn && !double.IsNaN(micros[i].Point.Bulge))
                {
                    int index = micros[i].OwerPos;
                    int next = index + 1 >= points.Count ? 0 : index + 1;
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(points[index].Point, points[next].Point, (float)points[index].Bulge);
                    double bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arc.Center, micros[i].Point.Point, micros[i + 1].Point.Point, arc.Clockwise);
                    micros[i].Point.Bulge = bulge;
                }
            }
            if (isCloseFigure && micros.Count > 0 && !micros[micros.Count - 1].Point.HasMicroConn && !double.IsNaN(micros[micros.Count - 1].Point.Bulge))//最后一个有效图形,更新为Arc
            {
                int index = micros[micros.Count - 1].OwerPos;
                int next = index + 1 >= points.Count ? 0 : index + 1;
                ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(points[index].Point, points[next].Point, (float)points[index].Bulge);
                double bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arc.Center, micros[micros.Count - 1].Point.Point, micros[0].Point.Point, arc.Clockwise);
                micros[micros.Count - 1].Point.Bulge = bulge;
            }
            if (!isCompensation) { micros.ForEach(e => e.Point.Position = e.OwerPos); }
            rets = micros.Select(e => e.Point).ToList();
            #endregion

            return (!isCompensation && rets.Count == temps.Count) ? null : rets;
        }
        public static List<MicroUnitPoint> InitMicroUnitPoint(List<UnitPointBulge> points, bool isCloseFigure, out double totalLength)
        {
            List<MicroUnitPoint> micros = new List<MicroUnitPoint>();
            double sum = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                var p1 = points[i];
                var p2 = points[i + 1];
                micros.Add(new MicroUnitPoint()
                {
                    Point = new UnitPointBulge(p1.Point, p1.Bulge, isBasePoint: p1.IsBasePoint),
                    OwerPos = i,
                    StartLength = sum,
                    SizeLength = 0,
                    Flags = MicroConnectFlags.MicroConnect
                });
                if (!double.IsNaN(p1.Bulge))// && p1.Bulge != 0)
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                    sum += DrawingOperationHelper.GetArcLength(arc.Radius, arc.SweepAngle);
                }
                else
                {
                    sum += HitUtil.Distance(p1.Point, p2.Point);
                }
            }
            micros.Add(new MicroUnitPoint()
            {
                Point = new UnitPointBulge(points[points.Count - 1].Point, points[points.Count - 1].Bulge, isBasePoint: points[points.Count - 1].IsBasePoint),
                OwerPos = points.Count - 1,
                StartLength = sum,
                SizeLength = 0,
                Flags = MicroConnectFlags.MicroConnect
            });
            if (isCloseFigure)
            {
                if (!double.IsNaN(points[points.Count - 1].Bulge))
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(points[points.Count - 1].Point, points[0].Point, (float)points[points.Count - 1].Bulge);
                    sum += DrawingOperationHelper.GetArcLength(arc.Radius, arc.SweepAngle);
                }
                else
                {
                    sum += HitUtil.Distance(points[points.Count - 1].Point, points[0].Point);
                }
            }
            totalLength = sum;
            return micros;
        }
        private static MicroUnitPoint GetMicroStartPoint(List<UnitPointBulge> points, MicroConnectModel param, List<MicroUnitPoint> temp, bool isCompensation = false, double size = 0, bool isOutside = false, List<UnitPointBulge> basePoints = null)
        {
            UnitPoint start = UnitPoint.Empty;
            double percent = param.Position % 1;
            double length = 0;
            int index = (int)Math.Truncate(param.Position);
            int next = index + 1 >= points.Count ? 0 : index + 1;
            UnitPointBulge p1 = points[index];
            UnitPointBulge p2 = points[next];
            if (isCompensation && basePoints != null)
            {
                index = points.FindIndex(e => e.IsBasePoint && e.Position == (int)Math.Truncate(param.Position));
                next = index + 1 >= points.Count ? 0 : index + 1;
                p1 = points[index];
                p2 = points[next];

                UnitPointBulge basep1 = basePoints[(int)p1.Position];
                UnitPointBulge basep2 = basePoints[(int)p1.Position + 1 >= basePoints.Count ? 0 : (int)p1.Position + 1];
                var cpoints = CompensationHelper.GetCompensationsPoint(basep1, basep2, size, isOutside);
                if (!double.IsNaN(p1.Bulge))
                {
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(basep1.Point, basep2.Point, (float)basep1.Bulge);
                    float angle = arc.StartAngle + (float)HitUtil.DegreesToRadians((float)percent * arc.SweepAngle);
                    double radius = HitUtil.Distance(cpoints.Item1.Point, arc.Center);
                    start = percent == 0 ? p1.Point /*cpoints.Item1.Point*/ : HitUtil.PointOnCircle(arc.Center, radius, angle);
                    if (percent == 0 && HitUtil.Distance(start, p1.Point) < 0.000001)
                    {
                        start = p1.Point;
                        length = 0;
                    }
                    else
                    {
                        double startAngle = HitUtil.RadiansToDegrees(arc.StartAngle);
                        double endAngle = HitUtil.RadiansToDegrees(arc.EndAngle);
                        double testAngle = percent == 0 ? startAngle : HitUtil.RadiansToDegrees(HitUtil.LineAngleR(arc.Center, start, 0));
                        double p1Angle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(arc.Center, p1.Point, 0));
                        double p2Angle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(arc.Center, p2.Point, 0));

                        if (!HitUtil.IsPointInArc(testAngle, p1Angle, p2Angle, arc.Clockwise))
                        {
                            bool isabove1 = HitUtil.IsPointInArc(p1Angle, startAngle, endAngle, arc.Clockwise);
                            bool isabove2 = HitUtil.IsPointInArc(p2Angle, startAngle, endAngle, arc.Clockwise);
                            if (HitUtil.IsPointInArc(testAngle, isabove1 ? startAngle : p1Angle, !isabove1 ? startAngle : p1Angle, arc.Clockwise))
                            {
                                start = p1.Point;
                                length = 0;
                            }
                            else if (HitUtil.IsPointInArc(testAngle, isabove2 ? p2Angle : endAngle, !isabove2 ? p2Angle : endAngle, arc.Clockwise))
                            {
                                start = p2.Point;
                                double sweepAngle = (float)HitUtil.CalAngleSweep((float)p1Angle, (float)p2Angle, arc.Clockwise);
                                length = DrawingOperationHelper.GetArcLength(radius, sweepAngle);
                            }
                        }
                        else
                        {
                            double sweepAngle = (float)HitUtil.CalAngleSweep((float)p1Angle, (float)testAngle, arc.Clockwise);
                            length = DrawingOperationHelper.GetArcLength(radius, sweepAngle);
                        }
                    }
                }
                else
                {
                    double baseLength = HitUtil.Distance(cpoints.Item1.Point, cpoints.Item2.Point) * (float)percent;
                    start = percent == 0 ? p1.Point : HitUtil.GetLinePointByDistance(cpoints.Item1.Point, cpoints.Item2.Point, baseLength);
                    if (!HitUtil.IsPointInLine(p1.Point, p2.Point, start, 0.001f))
                    {
                        if (HitUtil.IsPointInLine(cpoints.Item1.Point, p1.Point, start, 0.001f))
                        {
                            start = p1.Point;
                            length = 0;
                        }
                        else if (HitUtil.IsPointInLine(p2.Point, cpoints.Item2.Point, start, 0.001f))
                        {
                            start = p2.Point;
                            length = HitUtil.Distance(p1.Point, p2.Point);
                        }
                        else
                        {
                            length = HitUtil.Distance(p1.Point, start);
                        }
                    }
                    else
                    {
                        length = HitUtil.Distance(p1.Point, start);
                    }
                }
            }
            else
            {
                if (percent != 0)
                {
                    if (!double.IsNaN(p1.Bulge))
                    {
                        ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                        float angle = (float)percent * arc.SweepAngle;
                        angle = arc.StartAngle + (float)HitUtil.DegreesToRadians(angle);
                        length = DrawingOperationHelper.GetArcLength(arc.Radius, arc.SweepAngle) * (float)percent;
                        start = HitUtil.PointOnCircle(arc.Center, arc.Radius, angle);
                    }
                    else
                    {

                        length = HitUtil.Distance(p1.Point, p2.Point) * (float)percent;
                        start = HitUtil.GetLinePointByDistance(p1.Point, p2.Point, length);
                    }
                }
                else
                {
                    length = 0;
                    start = p1.Point;
                }

            }
            double startLength = temp[index].StartLength + length;
            return new MicroUnitPoint()
            {
                Point = new UnitPointBulge(start, bulge: p1.Bulge, hasMicroConn: true, isBasePoint: p1.IsBasePoint),
                OwerPos = index,
                Flags = param.Flags,
                StartLength = startLength,
                SizeLength = param.Size
            };
        }
        /// <summary>
        /// 从正方向根据距离计算结束点
        /// </summary>
        private static MicroUnitPoint GetMicroEndPointByPositive(List<UnitPointBulge> points, MicroUnitPoint startPoint, int index, double length, List<MicroUnitPoint> temp)
        {
            int next = index + 1 >= points.Count ? 0 : index + 1;
            var p1 = points[index];
            var p2 = points[next];
            UnitPoint end = UnitPoint.Empty;
            MicroUnitPoint indexPoint = temp[index];
            double remLength = 0;
            if (!double.IsNaN(p1.Bulge))
            {
                ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                double curLength = DrawingOperationHelper.GetArcLength(arc.Radius, arc.SweepAngle);
                remLength = (startPoint.StartLength - indexPoint.StartLength + length);
                if (remLength < curLength)
                {
                    float angle = (float)(remLength / curLength) * arc.SweepAngle;
                    angle = arc.StartAngle + (float)HitUtil.DegreesToRadians(angle);
                    end = HitUtil.PointOnCircle(arc.Center, arc.Radius, angle);
                }
                else if (!double.IsNaN(remLength - curLength))
                {
                    return GetMicroEndPointByPositive(points, temp[next], next, remLength - curLength, temp);
                }
            }
            else
            {
                double curLength = HitUtil.Distance(p1.Point, p2.Point);
                remLength = (startPoint.StartLength - indexPoint.StartLength + length);
                if (remLength < curLength)
                {
                    end = HitUtil.GetLinePointByDistance(p1.Point, p2.Point, remLength);
                }
                else if (!double.IsNaN(remLength - curLength))
                {
                    return GetMicroEndPointByPositive(points, temp[next], next, remLength - curLength, temp);
                }
            }
            return new MicroUnitPoint()
            {
                Point = new UnitPointBulge(end, bulge: p1.Bulge, hasMicroConn: false, isBasePoint: p1.IsBasePoint),
                OwerPos = index,
                Flags = startPoint.Flags,
                StartLength = indexPoint.StartLength + remLength,
            };
        }
        /// <summary>
        /// 从反方向根据距离计算结束点
        /// </summary>
        private static MicroUnitPoint GetMicroEndPointByNegative(List<UnitPointBulge> points, MicroUnitPoint startPoint, int nextIndex, double length, List<MicroUnitPoint> temp)
        {
            int next = nextIndex + 1 >= points.Count ? 0 : nextIndex + 1;
            var p1 = points[nextIndex];
            var p2 = points[next];
            UnitPoint end = UnitPoint.Empty;
            MicroUnitPoint indexPoint = temp[nextIndex];
            double remLength = 0;
            if (!double.IsNaN(p1.Bulge))
            {
                ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                double curLength = DrawingOperationHelper.GetArcLength(arc.Radius, arc.SweepAngle);
                if ((startPoint.StartLength - indexPoint.StartLength) < 0)
                {
                    remLength = curLength - length;
                }
                else
                {
                    remLength = (startPoint.StartLength - indexPoint.StartLength) - length;
                }
                if (remLength > 0)
                {
                    float angle = (float)(remLength / curLength) * arc.SweepAngle;
                    angle = arc.StartAngle + (float)HitUtil.DegreesToRadians(angle);
                    end = HitUtil.PointOnCircle(arc.Center, arc.Radius, angle);
                }
                else if (!double.IsNaN(remLength))
                {
                    int index = (nextIndex - 1) < 0 ? points.Count - 1 : (nextIndex - 1);
                    return GetMicroEndPointByNegative(points, temp[nextIndex], index, Math.Abs(remLength), temp);
                }
            }
            else
            {
                double curLength = HitUtil.Distance(p1.Point, p2.Point);
                if ((startPoint.StartLength - indexPoint.StartLength) < 0)
                {
                    remLength = curLength - length;
                }
                else
                {
                    remLength = (startPoint.StartLength - indexPoint.StartLength) - length;
                }

                if (remLength > 0)
                {
                    end = HitUtil.GetLinePointByDistance(p1.Point, p2.Point, remLength);
                }
                else if (!double.IsNaN(remLength))
                {
                    int index = (nextIndex - 1) < 0 ? points.Count - 1 : (nextIndex - 1);
                    return GetMicroEndPointByNegative(points, temp[nextIndex], index, Math.Abs(remLength), temp);
                }
            }
            return new MicroUnitPoint()
            {
                Point = new UnitPointBulge(end, bulge: p1.Bulge, hasMicroConn: true, isBasePoint: p1.IsBasePoint),
                OwerPos = nextIndex,
                Flags = startPoint.Flags,
                StartLength = indexPoint.StartLength + Math.Abs(remLength),
            };
        }

        #endregion

        #region 引入引出点的位置计算
        /// <summary>
        /// 计算圆有效的引入引出点
        /// </summary>
        public static void GetLeadLinePositionByCircle(List<ArcBase> arcBases, double leadInPosition, double leadOutPosition, out UnitPoint leadInIntersectPoint, out UnitPoint leadOutIntersectPoint)
        {
            leadInIntersectPoint = UnitPoint.Empty;
            leadOutIntersectPoint = UnitPoint.Empty;
            if (arcBases != null && arcBases.Count > 0)
            {
                UnitPoint leadInPoint = HitUtil.LineEndPoint(arcBases[0].Center, leadInPosition * Math.PI * 2, arcBases[0].Radius);
                UnitPoint leadOutPoint = HitUtil.LineEndPoint(arcBases[0].Center, leadOutPosition * Math.PI * 2, arcBases[0].Radius);

                double leadInAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(arcBases[0].Center, leadInPoint, 0));
                double leadOutAngle = HitUtil.RadiansToDegrees(HitUtil.LineAngleR(arcBases[0].Center, leadOutPoint, 0));
                bool isLeadInFind = false;
                bool isLeadOutFind = false;
                for (int i = 0; i < arcBases.Count; i++)
                {
                    int nextIndex = i + 1 == arcBases.Count ? 0 : i + 1;
                    if (!isLeadInFind)
                    {
                        if (HitUtil.PointInPoint(leadInPoint, arcBases[nextIndex].startPoint, 0.0001f) ||
                            HitUtil.PointInPoint(leadInPoint, arcBases[i].endPoint, 0.0001f) ||
                            HitUtil.IsPointInArc(leadInAngle, arcBases[i].EndAngle, arcBases[nextIndex].StartAngle, arcBases[i].IsClockwise))
                        {
                            leadInIntersectPoint = arcBases[nextIndex].startPoint;
                            isLeadInFind = true;
                        }
                    }
                    if (!isLeadOutFind)
                    {
                        if (HitUtil.PointInPoint(leadOutPoint, arcBases[nextIndex].startPoint, 0.0001f) ||
                            HitUtil.PointInPoint(leadOutPoint, arcBases[i].endPoint, 0.0001f) ||
                            HitUtil.IsPointInArc(leadOutAngle, arcBases[i].EndAngle, arcBases[nextIndex].StartAngle, arcBases[i].IsClockwise))
                        {
                            leadOutIntersectPoint = arcBases[i].endPoint;
                            isLeadOutFind = true;
                        }
                    }

                    if (isLeadInFind && isLeadOutFind) break;
                }
                if (!isLeadInFind) leadInIntersectPoint = leadInPoint;
                if (!isLeadOutFind) leadOutIntersectPoint = leadOutPoint;
            }
        }
        /// <summary>
        /// 计算多段线有效的引入引出点
        /// </summary>
        public static void GetLeadLinePointByMultiSegLine(List<UnitPointBulge> points, bool isCloseFigure, UnitPoint point, bool isLeadin, out UnitPoint intersectPoint, out Tuple<UnitPointBulge, UnitPointBulge> leadPoints)
        {
            leadPoints = null;
            intersectPoint = UnitPoint.Empty;
            int next;
            for (int i = 0; i < points.Count; i++)
            {
                next = i + 1 == points.Count ? 0 : i + 1;
                if (HitUtil.PointInPoint(points[i].Point, point, 0.00001f))
                {
                    UnitPointBulge validPoint = GetMicroValidPoint(points, ref i, isLeadin, isCloseFigure);
                    intersectPoint = validPoint == null ? point : validPoint.Point;
                    if (isLeadin)
                    {
                        next = i + 1 == points.Count ? 0 : i + 1;
                        leadPoints = Tuple.Create(points[i], points[next]);
                    }
                    else
                    {
                        i = i - 1 == -1 ? points.Count - 1 : i - 1;
                        next = i + 1 == points.Count ? 0 : i + 1;
                        leadPoints = Tuple.Create(points[i], points[next]);
                    }
                    break;
                }
                else if (HitUtil.PointInPoint(points[next].Point, point, 0.00001f))
                {
                    UnitPointBulge validPoint = GetMicroValidPoint(points, ref next, isLeadin, isCloseFigure);
                    intersectPoint = validPoint == null ? point : validPoint.Point;
                    i = next - 1 == -1 ? points.Count : next - 1;
                    leadPoints = Tuple.Create(points[i], points[next]);
                    break;
                }
                else
                {
                    bool isFind = false;
                    if (double.IsNaN(points[i].Bulge))
                    {
                        isFind = HitUtil.IsPointInLine(points[i].Point, points[next].Point, point, 0.00001f);
                    }
                    else
                    {
                        var arc = DrawingOperationHelper.GetArcParametersFromBulge(points[i].Point, points[next].Point, (float)points[i].Bulge);
                        double angle = HitUtil.LineAngleR(arc.Center, point, 0);
                        isFind = HitUtil.IsPointInArc(HitUtil.RadiansToDegrees(angle), HitUtil.RadiansToDegrees(arc.StartAngle), HitUtil.RadiansToDegrees(arc.EndAngle), arc.Clockwise);
                    }
                    if (isFind)
                    {
                        if (points[i].HasMicroConn)
                        {
                            UnitPointBulge validPoint = GetMicroValidPoint(points, ref i, isLeadin, isCloseFigure);
                            intersectPoint = validPoint == null ? point : validPoint.Point;
                        }
                        else
                        {
                            intersectPoint = point;
                        }
                        next = i + 1 == points.Count ? 0 : i + 1;
                        leadPoints = Tuple.Create(points[i], points[next]);
                        break;
                    }
                }
            }
        }

        private static UnitPointBulge GetMicroValidPoint(List<UnitPointBulge> points, ref int curIndex, bool isLeadIn, bool isCloseFigure)
        {
            if (isLeadIn)
            {
                for (int i = curIndex; i < points.Count; i++)
                {
                    if (!points[i].HasMicroConn)
                    {
                        curIndex = i;
                        return points[i];
                    }
                }
                if (isCloseFigure)
                {
                    for (int i = 0; i < curIndex; i++)
                    {
                        if (!points[i].HasMicroConn)
                        {
                            curIndex = i;
                            return points[i];
                        }
                    }
                }
            }
            else
            {
                for (int i = curIndex - 1; i >= 0; i--)
                {
                    if (!points[i].HasMicroConn)
                    {
                        curIndex = (i + 1) == points.Count ? 0 : (i + 1);
                        return points[curIndex];
                    }
                }
                if (isCloseFigure)
                {
                    for (int i = points.Count - 1; i >= curIndex; i--)
                    {
                        if (!points[i].HasMicroConn)
                        {
                            curIndex = (i + 1) == points.Count ? 0 : (i + 1);
                            return points[curIndex];
                        }
                    }
                }
            }
            return null;
        }
        #endregion
    }

}
