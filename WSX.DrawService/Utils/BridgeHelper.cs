using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawModel;
using WSX.DrawService.DrawTool;
using WSX.DrawService.DrawTool.Arcs;
using WSX.DrawService.DrawTool.CircleTool;
using WSX.DrawService.DrawTool.MultiSegmentLine;

namespace WSX.DrawService.Utils
{
    /// <summary>
    /// 桥接计算
    /// </summary>
    public class BridgeHelper
    {
        public static List<IDrawObject> GetBridgeObjects(List<IDrawObject> drawObjects, UnitPoint p1, UnitPoint p2, BridgingModel param, out bool isChanged)
        {
            isChanged = false;//桥接是否改变了图形
            List<BridgePoints> bridgePoints = new List<BridgePoints>();

            #region 计算桥接点在图形中的点的坐标
            drawObjects?.ForEach(drawObject =>
            {
                if ((drawObject.FigureType == FigureTypes.Circle))
                {
                    Circle circle = drawObject as Circle;
                    var bridges = BridgeHelper.GetBridgeByCircle(circle, p1, p2, param);
                    bridgePoints.AddRange(bridges);
                }
                else if (drawObject.FigureType == FigureTypes.Arc)
                {
                    ArcBase arc = drawObject as ArcBase;
                    var bridges = BridgeHelper.GetBridgeByArc(arc, p1, p2, param);
                    bridgePoints.AddRange(bridges);
                }
                else if (drawObject.FigureType == FigureTypes.LwPolyline)
                {
                    MultiSegmentLineBase multiSegLine = drawObject as MultiSegmentLineBase;
                    var bridges = BridgeHelper.GetBridgeByMultiSegLine(multiSegLine, p1, p2, param);
                    bridgePoints.AddRange(bridges);
                }
            });
            #endregion

            #region 根据桥接点坐标，按直线的起点距离排序,如果奇数个将移除最后一个，保证偶数个可以连接
            bridgePoints.Sort((x, y) => { if (x.Distance > y.Distance) return 1; return -1; });
            if (bridgePoints.Count % 2 == 1)
            {
                bridgePoints.RemoveAt(bridgePoints.Count - 1);
            }
            #endregion

            #region 移除间距大于最大条件的点
            List<BridgePoints> removes = new List<BridgePoints>();
            for (int i = 0; i < bridgePoints.Count - 1; i += 2)
            {
                var bridge1 = bridgePoints[i];
                var bridge2 = bridgePoints[i + 1];
                if (Math.Abs(bridge1.Distance - bridge2.Distance) > param.MaxDistance)
                {
                    removes.Add(bridge1);
                    removes.Add(bridge2);
                }
            }
            removes.ForEach(e => bridgePoints.Remove(e)); 
            #endregion

            if (bridgePoints.Count > 0)
            {
                List<IDrawObject> temps = new List<IDrawObject>();//计算后的图形
                List<IDrawObject> oldDraws = new List<IDrawObject>();//不用计算的原图

                #region 根据桥接点的位置把原图形拆为多段线
                foreach (IDrawObject drawObject in drawObjects)
                {
                    var points = bridgePoints.FindAll(b => b.Owner == drawObject);
                    if (points.Count > 0)
                    {
                        List<IDrawObject> draws = null;
                        if (drawObject.FigureType == FigureTypes.Circle)
                        {
                            draws = BridgeHelper.ConvertToMultiSegLine(drawObject as Circle, points);
                        }
                        else if (drawObject.FigureType == FigureTypes.Arc)
                        {
                            draws = BridgeHelper.ConvertToMultiSegLine(drawObject as ArcBase, points);
                        }
                        else if (drawObject.FigureType == FigureTypes.LwPolyline)
                        {
                            draws = BridgeHelper.ConvertToMultiSegLine(drawObject as MultiSegmentLineBase, points);
                        }
                        if (draws != null)
                        {
                            draws.RemoveAll(p => (p as MultiSegmentLineBase).Points.Count < 2);
                            temps.AddRange(draws);
                        }
                    }
                    else
                    {
                        oldDraws.Add(drawObject);
                    }
                }
                #endregion

                #region 连接最新的桥接图形
                for (int i = 0; i < bridgePoints.Count - 1; i += 2)
                {
                    var bridge1 = bridgePoints[i];
                    var bridge2 = bridgePoints[i + 1];
                    bool clockwise1 = HitUtil.IsClockwiseByCross(p1, p2, bridge1.Point1.Point);
                    bool clockwise2 = HitUtil.IsClockwiseByCross(p1, p2, bridge2.Point1.Point);
                    UnitPointBulge point11 = !clockwise1 ? bridge1.Point1 : bridge1.Point2;
                    UnitPointBulge point12 = clockwise1 ? bridge1.Point1 : bridge1.Point2;
                    UnitPointBulge point21 = !clockwise2 ? bridge2.Point1 : bridge2.Point2;
                    UnitPointBulge point22 = clockwise2 ? bridge2.Point1 : bridge2.Point2;
                    MultiSegmentLineBase draw11 = temps.Find(d => (d as MultiSegmentLineBase).Points.Contains(point11)) as MultiSegmentLineBase;
                    MultiSegmentLineBase draw12 = temps.Find(d => (d as MultiSegmentLineBase).Points.Contains(point12)) as MultiSegmentLineBase;
                    MultiSegmentLineBase draw21 = temps.Find(d => (d as MultiSegmentLineBase).Points.Contains(point21)) as MultiSegmentLineBase;
                    MultiSegmentLineBase draw22 = temps.Find(d => (d as MultiSegmentLineBase).Points.Contains(point22)) as MultiSegmentLineBase;
                    if (draw11 == null) { draw11 = new MultiSegmentLineBase() { LayerId = (bridge1.Owner as DrawObjectBase).LayerId, GroupParam = CopyUtil.DeepCopy(bridge1.Owner.GroupParam), Points = new List<UnitPointBulge>() }; }
                    if (draw12 == null) { draw12 = new MultiSegmentLineBase() { LayerId = (bridge1.Owner as DrawObjectBase).LayerId, GroupParam = CopyUtil.DeepCopy(bridge1.Owner.GroupParam), Points = new List<UnitPointBulge>() }; }
                    if (draw21 == null) { draw21 = new MultiSegmentLineBase() { LayerId = (bridge2.Owner as DrawObjectBase).LayerId, GroupParam = CopyUtil.DeepCopy(bridge2.Owner.GroupParam), Points = new List<UnitPointBulge>() }; }
                    if (draw22 == null) { draw22 = new MultiSegmentLineBase() { LayerId = (bridge2.Owner as DrawObjectBase).LayerId, GroupParam = CopyUtil.DeepCopy(bridge2.Owner.GroupParam), Points = new List<UnitPointBulge>() }; }
                    #region 组合多段线
                    if (draw11 == draw12 && draw21 == draw22)
                    {
                        if (draw11.Points.Count > 0 && draw11.Points[0] == point11) { draw11.ReverseDirection(); }
                        if (draw21.Points.Count > 0 && draw21.Points[0] != point21) { draw11.ReverseDirection(); }
                        if (draw11.Points.Count > 0) { draw11.Points[draw11.Points.Count - 1].Bulge = double.NaN; }
                        if (draw21.Points.Count > 0) { draw21.Points[draw21.Points.Count - 1].Bulge = double.NaN; }
                        draw11.Points.AddRange(draw21.Points);
                        draw11.IsCloseFigure = true;
                        temps.Remove(draw21);
                    }
                    else if (draw11 == draw12 && draw21 != draw22)
                    {
                        if (draw21.Points.Count > 0 && draw21.Points[0] == point21) { draw21.ReverseDirection(); }
                        if (draw22.Points.Count > 0 && draw22.Points[0] != point22) { draw22.ReverseDirection(); }
                        if (draw11.Points.Count > 0 && draw11.Points[0] != point11) { draw11.ReverseDirection(); }
                        if (draw21.Points.Count > 0) { draw21.Points[draw21.Points.Count - 1].Bulge = double.NaN; }
                        if (draw11.Points.Count > 0) { draw11.Points[draw11.Points.Count - 1].Bulge = double.NaN; }
                        draw21.Points.AddRange(draw11.Points);
                        draw21.Points.AddRange(draw22.Points);
                        temps.Remove(draw11);
                        temps.Remove(draw22);
                    }
                    else if (draw11 != draw12 && draw21 == draw22)
                    {
                        if (draw11.Points.Count > 0 && draw11.Points[0] == point11) { draw11.ReverseDirection(); }
                        if (draw21.Points.Count > 0 && draw21.Points[0] != point21) { draw21.ReverseDirection(); }
                        if (draw12.Points.Count > 0 && draw12.Points[0] != point12) { draw12.ReverseDirection(); }
                        if (draw11.Points.Count > 0) { draw11.Points[draw11.Points.Count - 1].Bulge = double.NaN; }
                        if (draw21.Points.Count > 0) { draw21.Points[draw21.Points.Count - 1].Bulge = double.NaN; }
                        draw11.Points.AddRange(draw21.Points);
                        draw11.Points.AddRange(draw12.Points);
                        temps.Remove(draw21);
                        temps.Remove(draw12);
                    }
                    else if (draw11 == draw21 && draw12 == draw22)
                    {
                        if (draw11.Points.Count > 0 && draw11.Points[0] != point11) { draw11.ReverseDirection(); }
                        if (draw12.Points.Count > 0 && draw12.Points[0] != point12) { draw12.ReverseDirection(); }
                        if (draw11.Points.Count > 0) { draw11.Points[draw11.Points.Count - 1].Bulge = double.NaN; }
                        if (draw12.Points.Count > 0) { draw12.Points[draw12.Points.Count - 1].Bulge = double.NaN; }
                        draw11.IsCloseFigure = true;
                        draw12.IsCloseFigure = true;
                    }
                    else if (draw11 == draw21 && draw12 != draw22)
                    {
                        if (draw11.Points.Count > 0 && draw11.Points[0] != point11) { draw11.ReverseDirection(); }
                        if (draw12.Points.Count > 0 && draw12.Points[0] == point12) { draw12.ReverseDirection(); }
                        if (draw22.Points.Count > 0 && draw22.Points[0] != point22) { draw22.ReverseDirection(); }
                        if (draw11.Points.Count > 0) { draw11.Points[draw11.Points.Count - 1].Bulge = double.NaN; }
                        if (draw12.Points.Count > 0) { draw12.Points[draw12.Points.Count - 1].Bulge = double.NaN; }
                        draw11.IsCloseFigure = true;
                        draw12.Points.AddRange(draw22.Points);
                        temps.Remove(draw22);
                    }
                    else if (draw11 != draw21 && draw12 == draw22)
                    {
                        if (draw12.Points.Count > 0 && draw12.Points[0] != point12) { draw12.ReverseDirection(); }
                        if (draw11.Points.Count > 0 && draw11.Points[0] == point11) { draw11.ReverseDirection(); }
                        if (draw21.Points.Count > 0 && draw21.Points[0] != point21) { draw21.ReverseDirection(); }
                        if (draw12.Points.Count > 0) { draw12.Points[draw12.Points.Count - 1].Bulge = double.NaN; }
                        if (draw11.Points.Count > 0) { draw11.Points[draw11.Points.Count - 1].Bulge = double.NaN; }
                        draw12.IsCloseFigure = true;
                        draw11.Points.AddRange(draw21.Points);
                        temps.Remove(draw21);
                    }
                    else
                    {
                        if (draw11.Points.Count > 0 && draw11.Points[0] == point11) { draw11.ReverseDirection(); }
                        if (draw21.Points.Count > 0 && draw21.Points[0] != point21) { draw21.ReverseDirection(); }
                        if (draw12.Points.Count > 0 && draw12.Points[0] == point12) { draw12.ReverseDirection(); }
                        if (draw22.Points.Count > 0 && draw22.Points[0] != point22) { draw22.ReverseDirection(); }
                        if (draw11.Points.Count > 0) { draw11.Points[draw11.Points.Count - 1].Bulge = double.NaN; }
                        if (draw12.Points.Count > 0) { draw12.Points[draw12.Points.Count - 1].Bulge = double.NaN; }
                        draw11.Points.AddRange(draw21.Points);
                        draw12.Points.AddRange(draw22.Points);
                        temps.Remove(draw21);
                        temps.Remove(draw22);
                    }
                    #endregion
                }
                #endregion

                isChanged = true;
                temps.ForEach(d => d.Update());
                temps.AddRange(oldDraws);
                return temps;
            }
            return drawObjects;
        }
        private static List<IDrawObject> ConvertToMultiSegLine(Circle circle, List<BridgePoints> bridges)
        {
            List<IDrawObject> retObjects = new List<IDrawObject>();
            if (bridges.Count == 2)
            {
                bridges[0].Point2.Bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(circle.Center, bridges[0].Point2.Point, bridges[1].Point1.Point, circle.IsClockwise); ;
                retObjects.Add(new MultiSegmentLineBase()
                {
                    IsCloseFigure = false,
                    LayerId = circle.LayerId,
                    GroupParam = CopyUtil.DeepCopy(circle.GroupParam),
                    Points = new List<UnitPointBulge>() { bridges[0].Point2, bridges[1].Point1 }
                });

                bridges[1].Point2.Bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(circle.Center, bridges[1].Point2.Point, bridges[0].Point1.Point, circle.IsClockwise); ;
                retObjects.Add(new MultiSegmentLineBase()
                {
                    IsCloseFigure = false,
                    LayerId = circle.LayerId,
                    GroupParam = CopyUtil.DeepCopy(circle.GroupParam),
                    Points = new List<UnitPointBulge>() { bridges[1].Point2, bridges[0].Point1 }
                });
            }
            else if (bridges.Count == 1)
            {
                bridges[0].Point2.Bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(circle.Center, bridges[0].Point2.Point, bridges[0].Point1.Point, circle.IsClockwise); ;
                retObjects.Add(new MultiSegmentLineBase()
                {
                    IsCloseFigure = false,
                    LayerId = circle.LayerId,
                    GroupParam = CopyUtil.DeepCopy(circle.GroupParam),
                    Points = new List<UnitPointBulge>() { bridges[0].Point2, bridges[0].Point1 }
                });
            }
            return retObjects;
        }
        private static List<IDrawObject> ConvertToMultiSegLine(ArcBase arc, List<BridgePoints> bridges)
        {
            List<IDrawObject> retObjects = new List<IDrawObject>();
            if (bridges.Count == 2)
            {
                double angle1 = HitUtil.LineAngleR(arc.Center, bridges[0].Point1.Point, 0);
                double angle2 = HitUtil.LineAngleR(arc.Center, bridges[1].Point1.Point, 0);
                angle1 = HitUtil.CalAngleSweep(arc.StartAngle, HitUtil.RadiansToDegrees(angle1), arc.IsClockwise);
                angle2 = HitUtil.CalAngleSweep(arc.StartAngle, HitUtil.RadiansToDegrees(angle2), arc.IsClockwise);
                if (angle1 > angle2) { bridges.Reverse(); }

                double bulge1 = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arc.Center, arc.startPoint, bridges[0].Point1.Point, arc.IsClockwise);
                retObjects.Add(new MultiSegmentLineBase()
                {
                    IsCloseFigure = false,
                    LayerId = arc.LayerId,
                    GroupParam = CopyUtil.DeepCopy(arc.GroupParam),
                    Points = new List<UnitPointBulge>() { new UnitPointBulge(arc.startPoint, bulge1), bridges[0].Point1 }
                });

                bridges[0].Point2.Bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arc.Center, bridges[0].Point2.Point, bridges[1].Point1.Point, arc.IsClockwise); ;
                retObjects.Add(new MultiSegmentLineBase()
                {
                    IsCloseFigure = false,
                    LayerId = arc.LayerId,
                    GroupParam = CopyUtil.DeepCopy(arc.GroupParam),
                    Points = new List<UnitPointBulge>() { bridges[0].Point2, bridges[1].Point1 }
                });

                bridges[1].Point2.Bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arc.Center, bridges[1].Point2.Point, arc.endPoint, arc.IsClockwise); ;
                retObjects.Add(new MultiSegmentLineBase()
                {
                    IsCloseFigure = false,
                    LayerId = arc.LayerId,
                    GroupParam = CopyUtil.DeepCopy(arc.GroupParam),
                    Points = new List<UnitPointBulge>() { bridges[1].Point2, new UnitPointBulge(arc.endPoint, bridges[1].Point2.Bulge) }
                });
            }
            else if (bridges.Count == 1)
            {
                double bulge1 = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arc.Center, arc.startPoint, bridges[0].Point1.Point, arc.IsClockwise);
                retObjects.Add(new MultiSegmentLineBase()
                {
                    IsCloseFigure = false,
                    LayerId = arc.LayerId,
                    GroupParam = CopyUtil.DeepCopy(arc.GroupParam),
                    Points = new List<UnitPointBulge>() { new UnitPointBulge(arc.startPoint, bulge1), bridges[0].Point1 }
                });

                bridges[0].Point2.Bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arc.Center, bridges[0].Point2.Point, arc.endPoint, arc.IsClockwise); ;
                retObjects.Add(new MultiSegmentLineBase()
                {
                    IsCloseFigure = false,
                    LayerId = arc.LayerId,
                    GroupParam = CopyUtil.DeepCopy(arc.GroupParam),
                    Points = new List<UnitPointBulge>() { bridges[0].Point2, new UnitPointBulge(arc.endPoint, bridges[0].Point2.Bulge) }
                });
            }
            return retObjects;
        }
        private static List<IDrawObject> ConvertToMultiSegLine(MultiSegmentLineBase multiSegLine, List<BridgePoints> bridges)
        {
            List<IDrawObject> retObjects = new List<IDrawObject>();
            double totalLength = 0;
            List<MicroUnitPoint> microTemps = MicroConnectHelper.InitMicroUnitPoint(multiSegLine.Points, multiSegLine.IsCloseFigure, out totalLength);
            List<MicroUnitPoint> microBridges = new List<MicroUnitPoint>();
            microTemps.ForEach(e => e.Point.IsBasePoint = true);

            #region 位置点计算长度
            foreach (BridgePoints point in bridges)
            {
                //第一个位置点
                MicroUnitPoint startPoint = microTemps[(int)point.Point1.Position];
                MicroUnitPoint endPoint = microTemps[(int)point.Point1.Position + 1 > microTemps.Count - 1 ? 0 : (int)point.Point1.Position + 1];
                double length = 0;
                if (!double.IsNaN(startPoint.Point.Bulge))
                {
                    var arcMini = DrawingOperationHelper.GetArcParametersFromBulge(startPoint.Point.Point, endPoint.Point.Point, (float)startPoint.Point.Bulge);
                    double bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arcMini.Center, startPoint.Point.Point, point.Point1.Point, arcMini.Clockwise);
                    var arcNew = DrawingOperationHelper.GetArcParametersFromBulge(startPoint.Point.Point, point.Point1.Point, (float)bulge);
                    length = DrawingOperationHelper.GetArcLength(arcNew.Radius, arcNew.SweepAngle);
                }
                else
                {
                    length = HitUtil.Distance(startPoint.Point.Point, point.Point1.Point);
                }
                microBridges.Add(new MicroUnitPoint()
                {
                    Point = point.Point1,
                    OwerPos = (int)point.Point1.Position,
                    StartLength = startPoint.StartLength + length,
                    Flags = MicroConnectFlags.None
                });
                //第二个位置点
                startPoint = microTemps[(int)point.Point2.Position];
                endPoint = microTemps[(int)point.Point2.Position + 1 > microTemps.Count - 1 ? 0 : (int)point.Point2.Position + 1];
                if (!double.IsNaN(startPoint.Point.Bulge))
                {
                    var arcMini = DrawingOperationHelper.GetArcParametersFromBulge(startPoint.Point.Point, endPoint.Point.Point, (float)startPoint.Point.Bulge);
                    double bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arcMini.Center, startPoint.Point.Point, point.Point2.Point, arcMini.Clockwise);
                    var arcNew = DrawingOperationHelper.GetArcParametersFromBulge(startPoint.Point.Point, point.Point2.Point, (float)bulge);
                    length = DrawingOperationHelper.GetArcLength(arcMini.Radius, arcNew.SweepAngle);
                }
                else
                {
                    length = HitUtil.Distance(startPoint.Point.Point, point.Point2.Point);
                }
                microBridges.Add(new MicroUnitPoint()
                {
                    Point = point.Point2,
                    OwerPos = (int)point.Point2.Position,
                    StartLength = startPoint.StartLength + length,
                    Flags = MicroConnectFlags.None
                });
            }
            #endregion

            #region 排序，相当于在合适的位置插入位置点
            microTemps.AddRange(microBridges);
            microTemps.Sort((x, y) => { if (x.StartLength > y.StartLength) return 1; return -1; });
            #endregion

            #region 更新bulge
            for (int i = 0; i < microTemps.Count; i++)
            {
                int iNext = i + 1 >= microTemps.Count ? 0 : i + 1;
                if (!double.IsNaN(microTemps[i].Point.Bulge) &&
                   (!microTemps[i].Point.IsBasePoint || !microTemps[iNext].Point.IsBasePoint))
                {
                    int mIndex = microTemps[i].Point.IsBasePoint ? microTemps[i].OwerPos : (int)microTemps[i].Point.Position;
                    int mNext = mIndex + 1 >= multiSegLine.Points.Count ? 0 : mIndex + 1;
                    ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(multiSegLine.Points[mIndex].Point, multiSegLine.Points[mNext].Point, (float)multiSegLine.Points[mIndex].Bulge);
                    microTemps[i].Point.Bulge = BulgeHelper.GetBulgeFromTwoPointsAndCenter(arc.Center, microTemps[i].Point.Point, microTemps[iNext].Point.Point, arc.Clockwise);
                }
            }
            #endregion

            #region 更新断开的位置标志
            bridges.ForEach(e =>
            {
                int startIndex = microTemps.FindIndex(m => m.Point == e.Point1);
                int endIndex = microTemps.FindIndex(m => m.Point == e.Point2);
                if (startIndex > endIndex)
                {
                    var points1 = microTemps.GetRange(startIndex, microTemps.Count - startIndex);
                    var points2 = microTemps.GetRange(0, endIndex);
                    points1.ForEach(p => p.Point.HasMicroConn = true);
                    points2.ForEach(p => p.Point.HasMicroConn = true);
                }
                else
                {
                    var points = microTemps.GetRange(startIndex, endIndex - startIndex);
                    points.ForEach(p => p.Point.HasMicroConn = true);
                }
            });
            #endregion

            #region 转换为多段线
            MultiSegmentLineBase lwPolyLine = null;
            for (int i = 0; i < microTemps.Count; i++)
            {
                if (microTemps[i].Point.HasMicroConn)
                {
                    if (lwPolyLine != null)
                    {
                        microTemps[i].Point.HasMicroConn = false;
                        lwPolyLine.Points.Add(microTemps[i].Point);
                        retObjects.Add(lwPolyLine);
                        lwPolyLine = null;
                    }
                }
                else
                {
                    if (lwPolyLine == null)
                    {
                        lwPolyLine = new MultiSegmentLineBase()
                        {
                            IsCloseFigure = false,
                            LayerId = multiSegLine.LayerId,
                            GroupParam = CopyUtil.DeepCopy(multiSegLine.GroupParam),
                            Points = new List<UnitPointBulge>()
                        };
                    }
                    lwPolyLine.Points.Add(microTemps[i].Point);
                }
            }
            if (lwPolyLine != null)
            {
                if (multiSegLine.IsCloseFigure)
                {
                    (retObjects[0] as MultiSegmentLineBase).Points.InsertRange(0, lwPolyLine.Points);
                }
                else
                {
                    retObjects.Add(lwPolyLine);
                }
            }
            #endregion

            return retObjects;
        }

        private static List<BridgePoints> GetBridgeByCircle(Circle circle, UnitPoint p1, UnitPoint p2, BridgingModel param)
        {
            List<BridgePoints> retPoints = new List<BridgePoints>();
            var line = DrawingOperationHelper.GetLineEquation(p1, p2);
            double width = param.Width / 2;
            List<UnitPoint> points = DrawingOperationHelper.GetIntersectPointByLineAndCircle(line.Item1, line.Item2, line.Item3, circle.Center, circle.Radius);
            var hasPoints = points?.Where(p => !p.IsEmpty && HitUtil.IsPointInLine(p1, p2, p, 0.000001f)).ToList();
            if (hasPoints != null && hasPoints.Count > 0)
            {
                //根据宽度求点
                double angle = width / circle.Radius * (circle.IsClockwise ? 1 : -1);
                List<UnitPointBulge> temps = new List<UnitPointBulge>();
                hasPoints?.ForEach(p =>
                {
                    double lineAngle = HitUtil.LineAngleR(circle.Center, p, 0);
                    double angle1 = lineAngle + angle;
                    double angle2 = lineAngle - angle;
                    //第一个位置点
                    UnitPoint retPoint1 = HitUtil.PointOnCircle(circle.Center, circle.Radius, angle1);
                    //第二个位置点
                    UnitPoint retPoint2 = HitUtil.PointOnCircle(circle.Center, circle.Radius, angle2);

                    BridgePoints bridges = new BridgePoints(circle,
                                                            new UnitPointBulge(retPoint1),
                                                            new UnitPointBulge(retPoint2),
                                                            HitUtil.Distance(p1, p));
                    retPoints.Add(bridges);
                });
            }
            return retPoints;
        }
        private static List<BridgePoints> GetBridgeByArc(ArcBase arc, UnitPoint p1, UnitPoint p2, BridgingModel param)
        {
            List<BridgePoints> retPoints = new List<BridgePoints>();
            var line = DrawingOperationHelper.GetLineEquation(p1, p2);
            double width = param.Width / 2;
            ArcModelMini arcMini = new DrawModel.ArcModelMini()
            {
                Center = arc.Center,
                Radius = arc.Radius,
                StartAngle = arc.StartAngle,
                EndAngle = arc.EndAngle,
                SweepAngle = arc.AngleSweep,
                Clockwise = arc.IsClockwise
            };

            List<UnitPoint> points = DrawingOperationHelper.GetIntersectPointByLineAndCircle(line.Item1, line.Item2, line.Item3, arc.Center, arc.Radius);
            var hasPoints = points?.Where(p => !p.IsEmpty && (HitUtil.IsPointOnArc(p, 0.000001f, arcMini) && HitUtil.IsPointInLine(p1, p2, p, 0.000001f))).ToList();
            if (hasPoints != null && hasPoints.Count > 0)
            {
                //根据宽度求点
                double angle = width / arc.Radius * (arc.IsClockwise ? 1 : -1);
                hasPoints?.ForEach(p =>
                {
                    double lineAngle = HitUtil.LineAngleR(arcMini.Center, p, 0);
                    double angle1 = lineAngle + angle;
                    double angle2 = lineAngle - angle;
                    UnitPoint retPoint1 = UnitPoint.Empty;
                    UnitPoint retPoint2 = UnitPoint.Empty;
                    if (HitUtil.IsPointInArc(HitUtil.RadiansToDegrees(angle1), arcMini.StartAngle, arcMini.EndAngle, arcMini.Clockwise))
                    {
                        //第一个位置点
                        retPoint1 = HitUtil.PointOnCircle(arcMini.Center, arcMini.Radius, angle1);
                    }
                    if (HitUtil.IsPointInArc(HitUtil.RadiansToDegrees(angle2), arcMini.StartAngle, arcMini.EndAngle, arcMini.Clockwise))
                    {
                        //第二个位置点
                        retPoint2 = HitUtil.PointOnCircle(arcMini.Center, arcMini.Radius, angle2);
                    }
                    BridgePoints bridges = new BridgePoints(arc,
                                                            new UnitPointBulge(retPoint1),
                                                            new UnitPointBulge(retPoint2),
                                                            HitUtil.Distance(p1, p));
                    retPoints.Add(bridges);
                });
            }
            return retPoints;
        }
        private static List<BridgePoints> GetBridgeByMultiSegLine(MultiSegmentLineBase multiSegLine, UnitPoint p1, UnitPoint p2, BridgingModel param)
        {
            List<BridgePoints> retPoints = new List<BridgePoints>();
            var line = DrawingOperationHelper.GetLineEquation(p1, p2);
            double width = param.Width / 2;
            var oriPoints = multiSegLine.Points;
            for (int index = 0; index < oriPoints.Count; index++)
            {
                int next = (index + 1 == oriPoints.Count) ? 0 : index + 1;
                UnitPointBulge point1 = oriPoints[index];
                UnitPointBulge point2 = oriPoints[next];
                if (!multiSegLine.IsCloseFigure && index == oriPoints.Count - 1) break;
                if (!double.IsNaN(point1.Bulge))
                {
                    //圆弧
                    ArcModelMini arcMini = DrawingOperationHelper.GetArcParametersFromBulge(point1.Point, point2.Point, (float)point1.Bulge);
                    arcMini.StartAngle = (float)HitUtil.RadiansToDegrees(arcMini.StartAngle);
                    arcMini.EndAngle = (float)HitUtil.RadiansToDegrees(arcMini.EndAngle);
                    List<UnitPoint> points = DrawingOperationHelper.GetIntersectPointByLineAndCircle(line.Item1, line.Item2, line.Item3, arcMini.Center, arcMini.Radius);
                    var hasPoints = points?.Where(p => !p.IsEmpty && HitUtil.IsPointOnArc(p, 0.000001f, arcMini) && HitUtil.IsPointInLine(p1, p2, p, 0.000001f)).ToList();
                    if (hasPoints != null && hasPoints.Count > 0)
                    {
                        arcMini.StartAngle = (float)HitUtil.DegreesToRadians(arcMini.StartAngle);
                        arcMini.EndAngle = (float)HitUtil.DegreesToRadians(arcMini.EndAngle);
                        //根据宽度求点
                        double arcLength = DrawingOperationHelper.GetArcLength(arcMini.Radius, arcMini.SweepAngle);
                        hasPoints?.ForEach(p =>
                        {
                            double percent = DrawingOperationHelper.GetPercentInArcByPoint(arcMini, p);
                            //第一个位置点
                            double length1 = arcLength * (1 - percent) + width;
                            UnitPointBulge retP1 = GetEndPointByLength(oriPoints, index, length1, false);

                            //第二个位置点
                            double length2 = arcLength * percent + width;
                            UnitPointBulge retP2 = GetEndPointByLength(oriPoints, index, length2, true);

                            var bridge = new BridgePoints(multiSegLine, retP1, retP2, HitUtil.Distance(p1, p));
                            retPoints.Add(bridge);
                        });
                    }
                }
                else
                {
                    //直线
                    var lineABC = DrawingOperationHelper.GetLineEquation(point1.Point, point2.Point);
                    UnitPoint point = DrawingOperationHelper.GetIntersectionPointBy2Line(line.Item1, line.Item2, line.Item3, lineABC.Item1, lineABC.Item2, lineABC.Item3);
                    if (!point.IsEmpty && HitUtil.IsPointInLine(point1.Point, point2.Point, point, 0.000001f) && HitUtil.IsPointInLine(p1, p2, point, 0.000001f))
                    {
                        //第一个位置点
                        double lenght1 = HitUtil.Distance(point2.Point, point) + width;
                        UnitPointBulge retP1 = GetEndPointByLength(oriPoints, index, lenght1, false);


                        //第二个位置点
                        double lenght2 = HitUtil.Distance(point1.Point, point) + width;
                        UnitPointBulge retP2 = GetEndPointByLength(oriPoints, index, lenght2, true);

                        var bridge = new BridgePoints(multiSegLine, retP1, retP2, HitUtil.Distance(p1, point));
                        retPoints.Add(bridge);
                    }
                }
            }
            return retPoints;
        }
        /// <summary>
        /// 根据长度获取多段线的点坐标
        /// </summary>
        /// <param name="points">多段线的点</param>
        /// <param name="index">当前开始的索引位置</param>
        /// <param name="length">长度</param>
        /// <param name="isPositive">是否正向计算长度，否则从反向计算长度</param>
        /// <returns></returns>
        private static UnitPointBulge GetEndPointByLength(List<UnitPointBulge> points, int index, double length, bool isPositive)
        {
            int next = index + 1 >= points.Count ? 0 : index + 1;
            var p1 = points[index];
            var p2 = points[next];
            UnitPoint end = UnitPoint.Empty;
            if (!double.IsNaN(p1.Bulge))
            {
                ArcModelMini arc = DrawingOperationHelper.GetArcParametersFromBulge(p1.Point, p2.Point, (float)p1.Bulge);
                double curLength = DrawingOperationHelper.GetArcLength(arc.Radius, arc.SweepAngle);
                if (length < curLength)
                {
                    float angle = (float)((isPositive ? length : (curLength - length)) / curLength) * arc.SweepAngle;
                    angle = arc.StartAngle + (float)HitUtil.DegreesToRadians(angle);
                    end = HitUtil.PointOnCircle(arc.Center, arc.Radius, angle);
                }
                else if (!double.IsNaN(length - curLength))
                {
                    int nextIndex = isPositive ? next : (index - 1) < 0 ? points.Count - 1 : (index - 1);
                    return GetEndPointByLength(points, nextIndex, length - curLength, isPositive);
                }
            }
            else
            {
                double curLength = HitUtil.Distance(p1.Point, p2.Point);
                if (length < curLength)
                {
                    if (isPositive)
                    {
                        end = HitUtil.GetLinePointByDistance(p1.Point, p2.Point, length);
                    }
                    else
                    {
                        end = HitUtil.GetLinePointByDistance(p2.Point, p1.Point, length);
                    }
                }
                else if (!double.IsNaN(length - curLength))
                {
                    int nextIndex = isPositive ? next : (index - 1) < 0 ? points.Count - 1 : (index - 1);
                    return GetEndPointByLength(points, nextIndex, length - curLength, isPositive);
                }
            }
            return new UnitPointBulge(end, bulge: p1.Bulge, position: index);
        }

    }

    internal class BridgePoints
    {
        public BridgePoints(IDrawObject owner, UnitPointBulge point1, UnitPointBulge point2, double distance)
        {
            Owner = owner;
            Point1 = point1;
            Point2 = point2;
            Distance = distance;
        }
        public IDrawObject Owner { get; private set; }
        /// <summary>
        /// 与桥接直线的的起点的距离
        /// </summary>
        public double Distance { get; set; }
        public UnitPointBulge Point1 { get; set; }
        public UnitPointBulge Point2 { get; set; }
    }

}
