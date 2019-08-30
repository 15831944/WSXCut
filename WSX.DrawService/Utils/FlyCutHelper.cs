using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
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
    public class FlyCutHelper
    {
        #region 直线飞切
        public List<List<LineFlyCut>> DoLineFlyingCut(List<IDrawObject> selectedObjects, LineFlyingCutModel lineFlyingCutModel)
        {
            List<List<LineFlyCut>> flyingCutGoups = new List<List<LineFlyCut>>();

            var objs = selectedObjects;
            //List<UnitPoint> unitPoints = new List<UnitPoint>();
            List<LineFlyCut> lineFlyCuts = new List<LineFlyCut>();

            int count = 0;

            //获取源对象节点
            foreach (var item in objs)
            {
                var obj = item as WSX.DrawService.DrawTool.MultiSegmentLine.MultiSegmentLineBase;
                if (obj == null || obj.Points == null || obj.Points.Count <= 0)
                    continue;

                for (int idx = 0; idx < obj.Points.Count; idx++)
                {
                    if (double.IsNaN(obj.Points[idx].Bulge))
                    {
                        if (idx < obj.Points.Count - 1)
                        {
                            //unitPoints.Add(obj.Points[idx].Point);
                            lineFlyCuts.Add(new LineFlyCut((++count).ToString(), obj.Points[idx].Point, obj.Points[idx + 1].Point));
                        }
                        else if (obj.IsCloseFigure)
                        {
                            //最后一个点 如果是封闭图形则 最后一个点到第一个点有线段 否则没有(非封闭图形)
                            //unitPoints.Add(obj.Points[idx].Point);
                            lineFlyCuts.Add(new LineFlyCut((++count).ToString(), obj.Points[idx].Point, obj.Points[0].Point));
                        }
                        else
                        {
                            //unitPoints.Add(obj.Points[idx].Point);
                        }
                    }
                }
            }

            UnitPoint flyEndPoint = new UnitPoint();
            LineFlyCut startLine = GetStartlineAndPoint(lineFlyCuts, lineFlyingCutModel, ref flyEndPoint);

            #region 0723 已注释 根据包围图形的矩形 判断起刀位置 修改前的
            //LineFlyCut startLine = GetStartPoint(lineFlyCuts,lineFlyingCutModel);

            //if (lineFlyingCutModel.StartKnifePostion == StartKnifePostions.LeftBottom )
            //{
            //    if (Math.Abs(startLine.StartPoint.X - startLine.EndPoint.X) <= LineFlyCut.THRESHOLD)
            //    {
            //        flyEndPoint = startLine.StartPoint.Y > startLine.EndPoint.Y ? startLine.EndPoint : startLine.StartPoint;
            //    }
            //    else
            //    {
            //        flyEndPoint = startLine.StartPoint.X > startLine.EndPoint.X ? startLine.EndPoint : startLine.StartPoint;
            //    }
            //}
            //else if (lineFlyingCutModel.StartKnifePostion == StartKnifePostions.RightBottom)
            //{
            //    if (Math.Abs(startLine.StartPoint.X - startLine.EndPoint.X) <= LineFlyCut.THRESHOLD)
            //    {
            //        flyEndPoint = startLine.StartPoint.Y > startLine.EndPoint.Y ? startLine.EndPoint : startLine.StartPoint;
            //    }
            //    else
            //    {
            //        flyEndPoint = startLine.StartPoint.X > startLine.EndPoint.X ? startLine.StartPoint : startLine.EndPoint;
            //    }
            //}
            //else if (lineFlyingCutModel.StartKnifePostion == StartKnifePostions.LeftTop)
            //{
            //    if (Math.Abs(startLine.StartPoint.X - startLine.EndPoint.X) <= LineFlyCut.THRESHOLD)
            //    {
            //        flyEndPoint = startLine.StartPoint.Y > startLine.EndPoint.Y ? startLine.StartPoint : startLine.EndPoint;
            //    }
            //    else
            //    {
            //        flyEndPoint = startLine.StartPoint.X > startLine.EndPoint.X ? startLine.EndPoint : startLine.StartPoint;
            //    }
            //}
            //else if (lineFlyingCutModel.StartKnifePostion == StartKnifePostions.RightTop)
            //{
            //    if (Math.Abs(startLine.StartPoint.X - startLine.EndPoint.X) <= LineFlyCut.THRESHOLD)
            //    {
            //        flyEndPoint = startLine.StartPoint.Y > startLine.EndPoint.Y ? startLine.StartPoint : startLine.EndPoint;
            //    }
            //    else
            //    {
            //        flyEndPoint = startLine.StartPoint.X > startLine.EndPoint.X ? startLine.StartPoint : startLine.EndPoint;
            //    }
            //}
            #endregion

            List<LineFlyCut> tempLines = null;
            List<LineFlyCut> flyingLines = new List<LineFlyCut>();

            bool hasSmoothConnect = false;
            LineFlyCut lastLine = null;
            double distanceOfPointPoint = 0.0d;

            //查找跟startLine平行且垂直距离(0最优[一条直线上])在指定范围内的线段
            while (lineFlyCuts.Count > 0)
            {
                #region 
                tempLines = lineFlyCuts.Where(l =>
                {
                    return l.IsParallel(startLine);//LineFlyCut.THRESHOLD
                }).ToList<LineFlyCut>();

                Tuple<bool, LineFlyCut> checkResult = CheckLineOnCommonSide(tempLines, startLine);

                if (!checkResult.Item1)
                {
                    startLine = checkResult.Item2;
                    continue;
                }
                #endregion

                tempLines = lineFlyCuts.Where(l =>
                {
                    return l.IsParallel(startLine) && startLine.DistanctPointToLine(l.StartPoint) <= lineFlyingCutModel.DistanceDeviation + 0.0001d;//LineFlyCut.THRESHOLD
                }).ToList<LineFlyCut>();

                if (tempLines != null && tempLines.Count > 0)
                {
                    //排序                                                                                                                                                                                                                                  
                    flyingLines = this.Order(tempLines, flyEndPoint);
                    flyingCutGoups.Add(flyingLines);

                    if (lastLine != null)
                    {
                        if (HitUtil.PointInPoint(lastLine.EndPoint, flyingLines[0].StartPoint, (float)LineFlyCut.THRESHOLD) && hasSmoothConnect)
                        {
                            lastLine.ConnectLineStartPoint = flyingLines[0].StartPoint;
                            lastLine.ConnectLineEndPoint = flyingLines[0].EndPoint;
                            hasSmoothConnect = true;
                        }
                        else if (!HitUtil.PointInPoint(lastLine.EndPoint, flyingLines[0].StartPoint, (float)LineFlyCut.THRESHOLD))
                        {
                            //判断是否需要光滑连接
                            distanceOfPointPoint = HitUtil.Distance(lastLine.EndPoint, flyingLines[0].StartPoint);//本段起点到上一段的终点2点距离
                                                                                                                  //小于最大光滑连接距离
                            if (distanceOfPointPoint + LineFlyCut.THRESHOLD < lineFlyingCutModel.MaxConnectDistance)
                            {
                                lastLine.ConnectLineStartPoint = flyingLines[0].StartPoint;
                                lastLine.ConnectLineEndPoint = flyingLines[0].EndPoint;
                                hasSmoothConnect = true;
                            }
                        }
                    }

                    lastLine = flyingLines[flyingLines.Count - 1];
                    flyEndPoint = flyingLines[flyingLines.Count - 1].EndPoint;
                    foreach (var item in tempLines)
                    {
                        lineFlyCuts.Remove(item);
                    }
                }

                if (lineFlyCuts.Count == 0)
                    break;

                //先找平行的
                tempLines = lineFlyCuts.Where(l =>
                {
                    return l.IsParallel(startLine);
                }).ToList<LineFlyCut>();
                while (tempLines.Count > 0)
                {
                    double minDistance1 = tempLines.Min(l => { return startLine.DistanctPointToLine(l.StartPoint); });
                    tempLines = tempLines.Where(l =>
                    {
                        return Math.Abs(startLine.DistanctPointToLine(l.StartPoint) - minDistance1) <= lineFlyingCutModel.DistanceDeviation + 0.0001d;//LineFlyCut.THRESHOLD
                    }).ToList<LineFlyCut>();

                    flyingLines = this.Order(tempLines, flyEndPoint);
                    flyingCutGoups.Add(flyingLines);

                    flyEndPoint = flyingLines[flyingLines.Count - 1].EndPoint;

                    //判断是否需要光滑连接
                    distanceOfPointPoint = HitUtil.Distance(lastLine.EndPoint, flyingLines[0].StartPoint);//本段起点到上一段的终点2点距离
                                                                                                          //小于最大光滑连接距离
                    if (distanceOfPointPoint + LineFlyCut.THRESHOLD < lineFlyingCutModel.MaxConnectDistance)
                    {
                        lastLine.ConnectLineStartPoint = flyingLines[0].StartPoint;
                        lastLine.ConnectLineEndPoint = flyingLines[0].EndPoint;
                        hasSmoothConnect = true;
                    }

                    lastLine = flyingLines[flyingLines.Count - 1];

                    foreach (var item in tempLines)
                    {
                        lineFlyCuts.Remove(item);
                    }

                    tempLines = lineFlyCuts.Where(l =>
                    {
                        return l.IsParallel(startLine);
                    }).ToList<LineFlyCut>();
                }

                if (lineFlyCuts.Count == 0)
                {
                    //String s = LineFlyCut.testS(flyingCutGoups);//查看文字版节点顺序
                    break;
                }
                else
                {
                    // flyEndPoint 为起点或者终点的线段
                    tempLines = lineFlyCuts.Where(l => {
                        return (l.StartPoint.X == flyEndPoint.X && l.StartPoint.Y == flyEndPoint.Y)
                        || l.EndPoint.X == flyEndPoint.X && l.EndPoint.Y == flyEndPoint.Y;
                    }
                    ).ToList<LineFlyCut>();
                    if (tempLines == null || tempLines.Count == 0)
                    {
                        startLine = lineFlyCuts[0];
                    }
                    else
                    {
                        startLine = tempLines[0];
                    }
                }
            }

            return flyingCutGoups;
        }

        /// <summary>
        /// 检查一组平行线是否都在startLine的同一侧
        /// </summary>
        /// <param name="lines">一组平行线</param>
        /// <param name="testLine"></param>
        public Tuple<bool,LineFlyCut> CheckLineOnCommonSide(List<LineFlyCut> lines, LineFlyCut testLine)
        {
            if (lines == null && lines.Count == 0)
                return new Tuple<bool, LineFlyCut>(true,null);
            LineFlyCut firstLineA = null;//A
            foreach(var item in lines)
            {
                if (item.StartPoint.X == testLine.StartPoint.X && item.StartPoint.Y == testLine.StartPoint.Y &&
                   item.EndPoint.X == testLine.EndPoint.X && item.EndPoint.Y == testLine.EndPoint.Y)
                    continue;
                if (HitUtil.DistanctPointToLine(testLine.StartPoint, testLine.EndPoint, item.StartPoint) <= LineFlyCut.THRESHOLD)
                {
                    continue;
                }
                firstLineA = item;
                break;
            }

            if(firstLineA == null)
            {
                return new Tuple<bool, LineFlyCut>(true, null);
            }

            bool isCommonSide = true;//true 都在同一侧 false 分布在两侧
            double maxDistanceCommonSide = 0.0d, maxDistanceDifferentSide = 0.0d;
            LineFlyCut farComonSide = null, farDifferentSide = null;
            
            double distanceAB = 0.0d;
            double distanceTestA = 0.0d;
            double distanceTestB = 0.0d;

            distanceTestA = HitUtil.DistanctPointToLine(firstLineA.StartPoint, firstLineA.EndPoint, testLine.StartPoint);
            foreach (var itemB in lines)
            {
                if (itemB.StartPoint.X == testLine.StartPoint.X && itemB.StartPoint.Y == testLine.StartPoint.Y &&
                   itemB.EndPoint.X == testLine.EndPoint.X && itemB.EndPoint.Y == testLine.EndPoint.Y)
                    continue;
                if (itemB.StartPoint.X == firstLineA.StartPoint.X && itemB.StartPoint.Y == firstLineA.StartPoint.Y &&
                   itemB.EndPoint.X == firstLineA.EndPoint.X && itemB.EndPoint.Y == firstLineA.EndPoint.Y)
                    continue;

                if(HitUtil.DistanctPointToLine(testLine.StartPoint,testLine.EndPoint,itemB.StartPoint) <= LineFlyCut.THRESHOLD){
                    continue;
                }
                //item B
                distanceAB = HitUtil.DistanctPointToLine(firstLineA.StartPoint, firstLineA.EndPoint, itemB.StartPoint);
                distanceTestB = HitUtil.DistanctPointToLine(itemB.StartPoint, itemB.EndPoint, testLine.StartPoint);

                //String x = testLine.Name.PadLeft(2, ' ') + firstLine.Name.PadLeft(2, ' ') + item.Name.PadLeft(2, ' ');

                // distanceAB = distanceTestA +distanceTestB 则itemB与firstLineA都在testLine的同一侧，否则分布在testLine的两侧
                if (Math.Abs(distanceAB- distanceTestA- distanceTestB) < LineFlyCut.THRESHOLD)
                {
                    //不同侧
                    isCommonSide = false;
                    if (maxDistanceDifferentSide < distanceTestB)
                    {
                        maxDistanceDifferentSide = distanceTestB;
                        farDifferentSide = itemB;
                    }
                }
                else
                {
                    //同一侧
                    if (maxDistanceCommonSide < distanceTestB)
                    {
                        maxDistanceCommonSide = distanceTestB;
                        farComonSide = itemB;
                    }
                }
            }

            if (isCommonSide)
            {
                return new Tuple<bool, LineFlyCut>(isCommonSide, null);
            }else
            {
                LineFlyCut startLine = null;

                //两边都有线段，两边分别找到最远的，在找二者较近的(空移短)
                if (maxDistanceCommonSide > 0 && maxDistanceDifferentSide > 0)
                {
                    startLine = maxDistanceCommonSide > maxDistanceDifferentSide ? farDifferentSide : farComonSide;
                }
                else if (maxDistanceCommonSide > 0)
                {
                    startLine = farComonSide;
                }
                else if (maxDistanceDifferentSide > 0)
                {
                    startLine = farDifferentSide;
                }
                return new Tuple<bool, LineFlyCut>(isCommonSide, startLine);
            }
        }

        private LineFlyCut GetStartPoint(List<LineFlyCut> lineFlyCuts, LineFlyingCutModel lineFlyingCutModel)
        {
            LineFlyCut startLine = null;
            //double slopePlusOrMinus = 0;//斜率正负
            if (lineFlyingCutModel.StartKnifePostion == StartKnifePostions.LeftBottom)
            {
                #region LeftBottom
                //与X抽平行(斜率等于0) 
                List<LineFlyCut> listStartLines = lineFlyCuts.Where(l =>
                {
                    return Math.Abs(l.Slope - 0) <= LineFlyCut.THRESHOLD;
                }).ToList<LineFlyCut>();

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope > 0 && l.Slope <= 1;
                    }).ToList<LineFlyCut>();
                }

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope >= -1 && l.Slope < 0;
                    }).ToList<LineFlyCut>();
                }

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope != double.NaN;//与Y轴平行的
                    }).ToList<LineFlyCut>();
                }
                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts;
                }

                //IntersectX
                double minIntersectX = listStartLines.Min(l =>
                {
                    return l.IntersectX.X;
                });

                //与X轴无交点即与X轴平行
                if (double.IsNaN(minIntersectX))
                {
                    //取StartPoint.Y最小的(EndPoint也可以)， 再取最右侧的点
                    double minY = listStartLines.Min(l =>
                    {
                        return l.StartPoint.Y;
                    });

                    listStartLines = listStartLines.Where(l => { return Math.Abs(l.StartPoint.Y - minY) <= LineFlyCut.THRESHOLD; }).ToList<LineFlyCut>();

                    //再取最靠左的 StartPoint或者EndPoint
                    double minX = listStartLines.Min(l =>
                    {
                        return Math.Min(l.StartPoint.X, l.EndPoint.X);
                    });
                    listStartLines = listStartLines.Where(l => {
                        return Math.Abs(Math.Min(l.StartPoint.X, l.EndPoint.X) - minX) <= LineFlyCut.THRESHOLD;
                    }).ToList<LineFlyCut>();
                }
                else
                {
                    //取与X轴交点最靠左的
                    listStartLines = listStartLines.Where(l =>
                    {
                        return Math.Abs(l.IntersectX.X - minIntersectX) <= LineFlyCut.THRESHOLD;
                    }).ToList<LineFlyCut>();

                    //在同一条直线上的多条，取X轴最小的
                    if (listStartLines != null && listStartLines.Count > 1)
                    {
                        double minX = listStartLines.Min(line => Math.Min(line.StartPoint.X, line.EndPoint.X));
                        listStartLines = listStartLines.Where(line => Math.Min(line.StartPoint.X, line.EndPoint.X) == minX).ToList<LineFlyCut>();
                    }
                }

                if (listStartLines != null && listStartLines.Count > 0)
                {
                    startLine = listStartLines[0];
                }
                #endregion
            }
            else if (lineFlyingCutModel.StartKnifePostion == StartKnifePostions.RightBottom)
            {
                #region RightBottom
                //与X抽平行(斜率等于0) 
                List<LineFlyCut> listStartLines = lineFlyCuts.Where(l =>
                {
                    return Math.Abs(l.Slope - 0) <= LineFlyCut.THRESHOLD;
                }).ToList<LineFlyCut>();

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope > 0 && l.Slope <= 1;
                    }).ToList<LineFlyCut>();

                    //if (listStartLines != null && listStartLines.Count > 0)
                    //    slopePlusOrMinus = 1;
                }

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope >= -1 && l.Slope < 0;
                    }).ToList<LineFlyCut>();

                    //if (listStartLines != null && listStartLines.Count > 0)
                    //    slopePlusOrMinus = -1;
                }

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope != double.NaN;//与Y轴平行的
                    }).ToList<LineFlyCut>();

                    //if (listStartLines.Count > 0)
                    //    slopePlusOrMinus = double.NaN;
                }

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts;
                }

                //IntersectX
                double maxIntersectX = listStartLines.Max(l =>
                {
                    return l.IntersectX.X;
                });

                //与X轴无交点即与X轴平行
                if (double.IsNaN(maxIntersectX))
                {
                    //取StartPoint.Y最小的(EndPoint也可以)， 再取最右侧的点
                    double minY = listStartLines.Min(l =>
                    {
                        return l.StartPoint.Y;
                    });

                    listStartLines = listStartLines.Where(l => {
                        return Math.Abs(l.StartPoint.Y - minY) <= LineFlyCut.THRESHOLD;
                    }).ToList<LineFlyCut>();

                    //再取最靠右的 StartPoint或者EndPoint
                    double maxX = listStartLines.Max(l =>
                    {
                        return Math.Max(l.StartPoint.X, l.EndPoint.X);
                    });
                    listStartLines = listStartLines.Where(l => {
                        return Math.Abs(Math.Max(l.StartPoint.X, l.EndPoint.X) - maxX) <= LineFlyCut.THRESHOLD;
                    }).ToList<LineFlyCut>();
                }
                else
                {
                    listStartLines = listStartLines.Where(l =>
                    {
                        return Math.Abs(l.IntersectX.X - maxIntersectX) <= LineFlyCut.THRESHOLD;
                    }).ToList<LineFlyCut>();

                    if (listStartLines != null && listStartLines.Count > 1)
                    {
                        double maxX = listStartLines.Max(line => Math.Max(line.StartPoint.X, line.EndPoint.X));

                        listStartLines = listStartLines.Where(line => Math.Max(line.StartPoint.X, line.EndPoint.X) == maxX).ToList<LineFlyCut>();
                    }
                }
                if (listStartLines != null && listStartLines.Count > 0)
                {
                    startLine = listStartLines[0];
                }
                #endregion
            }
            else if (lineFlyingCutModel.StartKnifePostion == StartKnifePostions.LeftTop)
            {
                #region LeftTop
                //与X抽平行(斜率等于0) 
                List<LineFlyCut> listStartLines = lineFlyCuts.Where(l =>
                {
                    return Math.Abs(l.Slope - 0) <= LineFlyCut.THRESHOLD;
                }).ToList<LineFlyCut>();

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope > 0 && l.Slope <= 1;
                    }).ToList<LineFlyCut>();
                }

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope >= -1 && l.Slope < 0;
                    }).ToList<LineFlyCut>();
                }

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope != double.NaN;//与Y轴平行的
                    }).ToList<LineFlyCut>();
                }

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts;
                }

                //IntersectX
                double minIntersectX = listStartLines.Min(l =>
                {
                    return l.IntersectX.X;
                });

                //与X轴无交点即与X轴平行
                if (double.IsNaN(minIntersectX))
                {
                    //取StartPoint.Y最大的(EndPoint也可以)， 再取最右侧的点
                    double maxY = listStartLines.Max(l =>
                    {
                        return l.StartPoint.Y;
                    });

                    listStartLines = listStartLines.Where(l => { return Math.Abs(l.StartPoint.Y - maxY) <= LineFlyCut.THRESHOLD; }).ToList<LineFlyCut>();

                    //再取最靠左的 StartPoint或者EndPoint
                    double minX = listStartLines.Min(l =>
                    {
                        return Math.Min(l.StartPoint.X, l.EndPoint.X);
                    });
                    listStartLines = listStartLines.Where(l => {
                        return Math.Abs(Math.Min(l.StartPoint.X, l.EndPoint.X) - minX) <= LineFlyCut.THRESHOLD;
                    }).ToList<LineFlyCut>();
                }
                else
                {
                    listStartLines = listStartLines.Where(l =>
                    {
                        return Math.Abs(l.IntersectX.X - minIntersectX) <= LineFlyCut.THRESHOLD;
                    }).ToList<LineFlyCut>();

                    if (listStartLines != null && listStartLines.Count > 1)
                    {
                        double minX = listStartLines.Min(line => Math.Min(line.StartPoint.X, line.EndPoint.X));

                        listStartLines = listStartLines.Where(line => Math.Min(line.StartPoint.X, line.EndPoint.X) == minX).ToList<LineFlyCut>();
                    }
                }

                if (listStartLines != null && listStartLines.Count > 0)
                {
                    startLine = listStartLines[0];
                }
                #endregion
            }
            else if (lineFlyingCutModel.StartKnifePostion == StartKnifePostions.RightTop)
            {
                #region RightTop
                //与X抽平行(斜率等于0) 
                List<LineFlyCut> listStartLines = lineFlyCuts.Where(l =>
                {
                    return Math.Abs(l.Slope - 0) <= LineFlyCut.THRESHOLD;
                }).ToList<LineFlyCut>();

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope >= -1 && l.Slope < 0;
                    }).ToList<LineFlyCut>();
                }

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope > 0 && l.Slope <= 1;
                    }).ToList<LineFlyCut>();
                }

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts.Where(l =>
                    {
                        return l.Slope != double.NaN;//与Y轴平行的
                    }).ToList<LineFlyCut>();
                }

                if (listStartLines == null || listStartLines.Count == 0)
                {
                    listStartLines = lineFlyCuts;
                }

                //IntersectX
                double maxIntersectX = listStartLines.Max(l =>
                {
                    return l.IntersectX.X;
                });

                //与X轴无交点即与X轴平行
                if (double.IsNaN(maxIntersectX))
                {
                    //取StartPoint.Y最大的(EndPoint也可以)， 再取最右侧的点
                    double maxY = listStartLines.Max(l =>
                    {
                        return l.StartPoint.Y;
                    });

                    listStartLines = listStartLines.Where(l => { return Math.Abs(l.StartPoint.Y - maxY) <= LineFlyCut.THRESHOLD; }).ToList<LineFlyCut>();

                    //再取最靠右的 StartPoint或者EndPoint
                    double maxX = listStartLines.Max(l =>
                    {
                        return Math.Max(l.StartPoint.X, l.EndPoint.X);
                    });
                    listStartLines = listStartLines.Where(l => {
                        return Math.Abs(Math.Max(l.StartPoint.X, l.EndPoint.X) - maxX) <= LineFlyCut.THRESHOLD;
                    }).ToList<LineFlyCut>();
                }
                else
                {
                    //取与X轴交点最靠右的
                    listStartLines = listStartLines.Where(l =>
                    {
                        return Math.Abs(l.IntersectX.X - maxIntersectX) <= LineFlyCut.THRESHOLD;
                    }).ToList<LineFlyCut>();

                    //多条线段
                    if (listStartLines != null && listStartLines.Count > 1)
                    {
                        double maxX = listStartLines.Max(line => Math.Max(line.StartPoint.X, line.EndPoint.X));

                        listStartLines = listStartLines.Where(line => Math.Max(line.StartPoint.X, line.EndPoint.X) == maxX).ToList<LineFlyCut>();
                    }
                }

                if (listStartLines != null && listStartLines.Count > 0)
                {
                    startLine = listStartLines[0];
                }
                #endregion
            }
            return startLine;
        }

        private bool Equals(double value1,double value2)
        {
            return Math.Abs(value1 - value2) <= LineFlyCut.THRESHOLD;
        }

        private LineFlyCut GetStartlineAndPoint(List<LineFlyCut> lineFlyCuts, LineFlyingCutModel model, ref UnitPoint startPoint)
        {
            LineFlyCut startLine = null;
            //找到包围 获取包围这些线段的矩形的四个点之一 左下角、右下角、左上角、右上角
            UnitPoint boundingAngle = this.GetBoundingRectanglePoint(lineFlyCuts, model);

            double minX = lineFlyCuts.Min(line => Math.Min(line.StartPoint.X, line.EndPoint.X));
            double maxX = lineFlyCuts.Max(line => Math.Max(line.StartPoint.X, line.EndPoint.X));
            double minY = lineFlyCuts.Min(line => Math.Min(line.StartPoint.Y, line.EndPoint.Y));
            double maxY = lineFlyCuts.Max(line => Math.Max(line.StartPoint.Y, line.EndPoint.Y));

            //与包围这些线段的矩形边框接触的点
            List<UnitPoint> borderPoints = new List<UnitPoint>();
            foreach (var line in lineFlyCuts)
            {
                if (Equals(line.StartPoint.X ,minX) || Equals(line.StartPoint.X, maxX) || Equals(line.StartPoint.Y,minY) || Equals(line.StartPoint.Y, maxY))
                    borderPoints.Add(line.StartPoint);

                if (Equals(line.EndPoint.X, minX) || Equals(line.EndPoint.X, maxX) || Equals(line.EndPoint.Y, minY) || Equals(line.EndPoint.Y, maxY))
                    borderPoints.Add(line.EndPoint);
            }

            double minDistance = borderPoints.Min(p => HitUtil.Distance(p, boundingAngle));

            List<UnitPoint> nearPoints = borderPoints.Where(p => HitUtil.TwoFloatNumberIsEqual(HitUtil.Distance(p, boundingAngle), minDistance, LineFlyCut.THRESHOLD)).ToList<UnitPoint>();
            startPoint = nearPoints[0];
            UnitPoint startPoint1 = startPoint;

            #region 已注释
            //找到离boundingAngle最近的距离
            //double minDistance = lineFlyCuts.Min(line => 
            //Math.Min(HitUtil.Distance(line.StartPoint, boundingAngle), HitUtil.Distance(line.EndPoint, boundingAngle)));

            ////最近距离的线段
            //List<LineFlyCut> startLines = lineFlyCuts.Where(line =>
            //{
            //    return HitUtil.TwoFloatNumberIsEqual(Math.Min(HitUtil.Distance(line.StartPoint, boundingAngle), HitUtil.Distance(line.EndPoint, boundingAngle)),minDistance,LineFlyCut.THRESHOLD);
            //}).ToList<LineFlyCut>();
            #endregion

            ////最近距离的线段
            List<LineFlyCut> startLines = lineFlyCuts.Where(line =>
            {
                return (line.StartPoint.X == startPoint1.X && line.StartPoint.Y == startPoint1.Y) || (line.EndPoint.X == startPoint1.X && line.EndPoint.Y == startPoint1.Y);
            }).ToList<LineFlyCut>();

            if (startLines != null && startLines.Count == 1)
            {
                startLine = startLines[0];
                return startLine;
            }

            //优先找平行于X轴的
            List<LineFlyCut> parallelX = startLines.Where(line => Math.Abs(line.Slope - 0) <= LineFlyCut.THRESHOLD).ToList<LineFlyCut>();
            if (parallelX != null && parallelX.Count > 0)
            {
                startLine = parallelX[0];
                //startPoint = GetStartPoint(startLine, boundingAngle);
                return startLine;
            }

            //优先找平行于Y轴的
            List<LineFlyCut> parallelY = startLines.Where(line => double.IsNaN(line.Slope)).ToList<LineFlyCut>();
            if (parallelY != null && parallelY.Count > 0)
            {
                startLine = parallelY[0];
                //startPoint = GetStartPoint(startLine, boundingAngle);
                return startLine;
            }

            #region

            //起点有两条线段，线段A对应一组平行线，与A平行的线段都分布在A的同一侧
            // 线段B对应一组平行线，与B平行的线段分布在B的两侧 优先切割A
            List<LineFlyCut> paralelLines = null;
            List<LineFlyCut> removeLines = new List<LineFlyCut>();
            foreach (var item in startLines)
            {
                paralelLines = lineFlyCuts.Where(line => line.IsParallel(item)).ToList<LineFlyCut>();
                if (paralelLines.Count < 2)
                {
                    removeLines.Add(item);
                }
                else
                {
                    //检查是否平行线都在item的同一侧
                    Tuple<bool, LineFlyCut> check = this.CheckLineOnCommonSide(paralelLines, item);
                    if (!check.Item1)
                    {
                        removeLines.Add(item);
                    }
                }
            }
            if (removeLines.Count > 0)
            {
                foreach (var item in removeLines)
                {
                    startLines.Remove(item);
                }
            }

            if (startLines.Count == 1)
            {
                startLine = startLines[0];
                return startLine;
            }
            else if (startLines.Count == 0)
            {
                //如果都删了， 加回去
                foreach (var item in removeLines)
                {
                    startLines.Add(item);
                }
            }

            #endregion

            List<LineFlyCut> tempLines = null;

            if (model.StartKnifePostion == StartKnifePostions.LeftBottom)
            {
                tempLines = startLines.Where(line => line.Slope > 0 && line.Slope < 1).ToList<LineFlyCut>();
                if (tempLines != null && tempLines.Count > 0)
                {
                    startLine = tempLines[0];
                    //startPoint = GetStartPoint(startLine, boundingAngle);
                    return startLine;
                }

                tempLines = startLines.Where(line => line.Slope > -1 && line.Slope < 0).ToList<LineFlyCut>();
                if (tempLines != null && tempLines.Count > 0)
                {
                    startLine = tempLines[0];
                    //startPoint = GetStartPoint(startLine, boundingAngle);
                    return startLine;
                }

                startLine = startLines[0];
                //startPoint = GetStartPoint(startLine, boundingAngle);
                return startLine;
            }
            else if (model.StartKnifePostion == StartKnifePostions.RightBottom)
            {
                tempLines = startLines.Where(line => line.Slope > -1 && line.Slope < 0).ToList<LineFlyCut>();
                if (tempLines != null && tempLines.Count > 0)
                {
                    startLine = tempLines[0];
                    //startPoint = GetStartPoint(startLine, boundingAngle);
                    return startLine;
                }

                tempLines = startLines.Where(line => line.Slope > 0 && line.Slope < 1).ToList<LineFlyCut>();
                if (tempLines != null && tempLines.Count > 0)
                {
                    startLine = tempLines[0];
                    //startPoint = GetStartPoint(startLine, boundingAngle);
                    return startLine;
                }
                startLine = startLines[0];
                //startPoint = GetStartPoint(startLine, boundingAngle);
                return startLine;
            }
            else if (model.StartKnifePostion == StartKnifePostions.LeftTop)
            {
                //同 RightBottom
                tempLines = startLines.Where(line => line.Slope > -1 && line.Slope < 0).ToList<LineFlyCut>();
                if (tempLines != null && tempLines.Count > 0)
                {
                    startLine = tempLines[0];
                    //startPoint = GetStartPoint(startLine, boundingAngle);
                    return startLine;
                }

                tempLines = startLines.Where(line => line.Slope > 0 && line.Slope < 1).ToList<LineFlyCut>();
                if (tempLines != null && tempLines.Count > 0)
                {
                    startLine = tempLines[0];
                    //startPoint = GetStartPoint(startLine, boundingAngle);
                    return startLine;
                }
                startLine = startLines[0];
                //startPoint = GetStartPoint(startLine, boundingAngle);
                return startLine;
            }
            else if (model.StartKnifePostion == StartKnifePostions.RightTop)
            {
                //同LeftBottom
                tempLines = startLines.Where(line => line.Slope > 0 && line.Slope < 1).ToList<LineFlyCut>();
                if (tempLines != null && tempLines.Count > 0)
                {
                    startLine = tempLines[0];
                    // startPoint = GetStartPoint(startLine, boundingAngle);
                    return startLine;
                }

                tempLines = startLines.Where(line => line.Slope > -1 && line.Slope < 0).ToList<LineFlyCut>();
                if (tempLines != null && tempLines.Count > 0)
                {
                    startLine = tempLines[0];
                    //startPoint = GetStartPoint(startLine, boundingAngle);
                    return startLine;
                }

                startLine = startLines[0];
                //startPoint = GetStartPoint(startLine, boundingAngle);
                return startLine;
            }
            return startLine;

        }

        private UnitPoint GetStartPoint(LineFlyCut startLine, UnitPoint anglePoint)
        {
            if (HitUtil.Distance(anglePoint, startLine.StartPoint) < HitUtil.Distance(anglePoint, startLine.EndPoint))
            {
                return startLine.StartPoint;
            }
            else
            {
                return startLine.EndPoint;
            }
        }

        /// <summary>
        /// 获取包围这些线段的矩形的四个点之一
        /// </summary>
        /// <param name="lineFlyCuts"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private UnitPoint GetBoundingRectanglePoint(List<LineFlyCut> lineFlyCuts, LineFlyingCutModel model)
        {
            if (model.StartKnifePostion == StartKnifePostions.LeftBottom)
            {
                double minX = lineFlyCuts.Min(line => Math.Min(line.StartPoint.X, line.EndPoint.X));
                double minY = lineFlyCuts.Min(line => Math.Min(line.StartPoint.Y, line.EndPoint.Y));
                return new UnitPoint(minX, minY);
            }
            else if (model.StartKnifePostion == StartKnifePostions.RightBottom)
            {
                double maxX = lineFlyCuts.Max(line => Math.Max(line.StartPoint.X, line.EndPoint.X));
                double minY = lineFlyCuts.Min(line => Math.Min(line.StartPoint.Y, line.EndPoint.Y));
                return new UnitPoint(maxX, minY);
            }
            else if (model.StartKnifePostion == StartKnifePostions.LeftTop)
            {
                double minX = lineFlyCuts.Min(line => Math.Min(line.StartPoint.X, line.EndPoint.X));
                double maxY = lineFlyCuts.Max(line => Math.Max(line.StartPoint.Y, line.EndPoint.Y));
                return new UnitPoint(minX, maxY);
            }
            else if (model.StartKnifePostion == StartKnifePostions.RightTop)
            {
                double maxX = lineFlyCuts.Max(line => Math.Max(line.StartPoint.X, line.EndPoint.X));
                double maxY = lineFlyCuts.Max(line => Math.Max(line.StartPoint.Y, line.EndPoint.Y));
                return new UnitPoint(maxX, maxY);
            }
            return new UnitPoint();
        }

        private List<LineFlyCut> Order(List<LineFlyCut> source, UnitPoint point)
        {
            List<LineFlyCut> flyCuts = new List<LineFlyCut>();
            foreach (var item in source)
            {
                flyCuts.Add(new LineFlyCut(item.Name, item.StartPoint, item.EndPoint));
            }

            if (Math.Abs(flyCuts[0].StartPoint.X - flyCuts[0].EndPoint.X) <= LineFlyCut.THRESHOLD)
            {
                for (int idx = 0; idx < flyCuts.Count; idx++)
                {
                    if (flyCuts[idx].StartPoint.Y > flyCuts[idx].EndPoint.Y)
                    {
                        flyCuts[idx] = new LineFlyCut(flyCuts[idx].Name, flyCuts[idx].EndPoint, flyCuts[idx].StartPoint);//start end 对调--
                    }
                }

                flyCuts = flyCuts.OrderBy(l => { return l.StartPoint.Y; }).ToList<LineFlyCut>();

                double distanceStart = HitUtil.Distance(flyCuts[0].StartPoint, point);
                double distanceEnd = HitUtil.Distance(flyCuts[flyCuts.Count - 1].EndPoint, point);
                if (distanceEnd < distanceStart)
                {
                    for (int idx = 0; idx < flyCuts.Count; idx++)
                    {
                        flyCuts[idx] = new LineFlyCut(flyCuts[idx].Name, flyCuts[idx].EndPoint, flyCuts[idx].StartPoint);//start end 对调--
                    }
                    flyCuts = flyCuts.OrderByDescending(l => { return l.StartPoint.Y; }).ToList<LineFlyCut>();
                }
            }
            else
            {
                for (int idx = 0; idx < flyCuts.Count; idx++)
                {
                    if (flyCuts[idx].StartPoint.X > flyCuts[idx].EndPoint.X)
                    {
                        flyCuts[idx] = new LineFlyCut(flyCuts[idx].Name, flyCuts[idx].EndPoint, flyCuts[idx].StartPoint);//start end 对调--
                    }
                }

                flyCuts = flyCuts.OrderBy(l => { return l.StartPoint.X; }).ToList<LineFlyCut>();

                double distanceStart = HitUtil.Distance(flyCuts[0].StartPoint, point);
                double distanceEnd = HitUtil.Distance(flyCuts[flyCuts.Count - 1].EndPoint, point);
                if (distanceEnd < distanceStart)
                {
                    for (int idx = 0; idx < flyCuts.Count; idx++)
                    {
                        flyCuts[idx] = new LineFlyCut(flyCuts[idx].Name, flyCuts[idx].EndPoint, flyCuts[idx].StartPoint);//start end 对调--
                    }
                    flyCuts = flyCuts.OrderByDescending(l => { return l.StartPoint.X; }).ToList<LineFlyCut>();
                }
            }

            return flyCuts;
        }
        #endregion

        #region 圆弧飞切

        public List<Circle> DoArcFlyingCut(List<IDrawObject> selectedObjects, ArcFlyingCutModel arcFlyingCutModel)
        {
            List<Circle> ret = null;
            List<List<Circle>> orderArc = null;

            List<Circle> source = new List<Circle>();

            //获取源对象节点
            int idx = 0;
            foreach (var item in selectedObjects)
            {
                var circle = item as WSX.DrawService.DrawTool.CircleTool.Circle;
                if (circle == null)
                    continue;
                source.Add((Circle)circle.Clone());
                source[source.Count - 1].Name = (++idx).ToString();
            }

            //不排序
            if (!arcFlyingCutModel.IsFirstSort)
            {
                ret = CalulateFlyingParam(source);
                return ret;
            }

            switch (arcFlyingCutModel.SortType)
            {
                case ArcFlyingCutSortTypes.LocalShortestEmptyMove:
                    ret = CalulateFlyingParam(source);
                    break;
                case ArcFlyingCutSortTypes.LeftToRight:
                    orderArc = this.CalCutGrouping(source, arcFlyingCutModel);
                    ret = OrderLeftToRight(orderArc);
                    ret = CalulateFlyingParam(ret);
                    break;
                case ArcFlyingCutSortTypes.RightToLeft:
                    orderArc = this.CalCutGrouping(source, arcFlyingCutModel);
                    ret = OrderRightToLeft(orderArc);
                    ret = CalulateFlyingParam(ret);
                    break;
                case ArcFlyingCutSortTypes.TopToBottom:
                    orderArc = this.CalCutGrouping(source, arcFlyingCutModel);
                    ret = OrderTopToBottom(orderArc);
                    ret = CalulateFlyingParam(ret);
                    break;
                case ArcFlyingCutSortTypes.BottomToTop:
                    orderArc = this.CalCutGrouping(source, arcFlyingCutModel);
                    ret = OrderBottomToTop(orderArc);
                    ret = CalulateFlyingParam(ret);
                    break;
                case ArcFlyingCutSortTypes.Clockwise:
                case ArcFlyingCutSortTypes.Anticlockwise:
                    ret = OrderClockwiseOrAnticlocksise(source, arcFlyingCutModel);
                    ret = CalulateFlyingParam(ret);
                    break;
                default:
                    break;
            }
            return ret;
        }

        #region 左到右

        public List<Circle> OrderLeftToRight(List<List<Circle>> circleGroups)
        {
            List<Circle> circles = new List<Circle>();

            Circle currentCircle = null; // 当前圆
            List<Circle> currentGroup = null; //当前分组
            List<Circle> previousGroup = null;//上一个分组

            for (int groupIdx = 0; groupIdx < circleGroups.Count; groupIdx++)
            {
                #region group
                currentGroup = circleGroups[groupIdx];
                currentGroup = currentGroup.OrderBy(item => item.Center.Y).ToList<Circle>();
                if (previousGroup != null && NeedDescendingLeftRight(previousGroup, currentGroup))
                {
                    currentGroup = currentGroup.OrderByDescending(item => item.Center.Y).ToList<Circle>();
                }

                for (int idx = 0; idx < currentGroup.Count; idx++)
                {
                    currentCircle = currentGroup[idx];
                    circles.Add(currentCircle);
                }

                previousGroup = currentGroup;
                #endregion group
            }

            return circles;
        }
        #endregion 左到右

        #region 右到左

        public List<Circle> OrderRightToLeft(List<List<Circle>> circleGroups)
        {
            List<Circle> circles = new List<Circle>();

            Circle currentCircle = null; // 当前圆
            List<Circle> currentGroup = null; //当前分组
            List<Circle> previousGroup = null;//上一个分组

            for (int groupIdx = 0; groupIdx < circleGroups.Count; groupIdx++)
            {
                #region group
                currentGroup = circleGroups[groupIdx];
                currentGroup = currentGroup.OrderBy(item => item.Center.Y).ToList<Circle>();
                if (previousGroup != null && NeedDescendingLeftRight(previousGroup, currentGroup))
                {
                    currentGroup = currentGroup.OrderByDescending(item => item.Center.Y).ToList<Circle>();
                }

                for (int idx = 0; idx < currentGroup.Count; idx++)
                {
                    currentCircle = currentGroup[idx];
                    circles.Add(currentCircle);
                }

                previousGroup = currentGroup;// 前提 本次循环previousGroup已使用完 
                #endregion group
            }

            return circles;
        }
        #endregion

        #region 上到下

        public List<Circle> OrderTopToBottom(List<List<Circle>> circleGroups)
        {
            List<Circle> circles = new List<Circle>();

            Circle currentCircle = null; // 当前圆
            List<Circle> currentGroup = null; //当前分组
            List<Circle> previousGroup = null;//上一个分组

            for (int groupIdx = 0; groupIdx < circleGroups.Count; groupIdx++)
            {
                currentGroup = circleGroups[groupIdx];
                currentGroup = currentGroup.OrderBy(item => item.Center.X).ToList<Circle>();
                if (previousGroup != null && NeedDescendingTopBottom(previousGroup, currentGroup))
                {
                    currentGroup = currentGroup.OrderByDescending(item => item.Center.X).ToList<Circle>();
                }

                for (int idx = 0; idx < currentGroup.Count; idx++)
                {
                    currentCircle = currentGroup[idx];
                    circles.Add(currentCircle);
                }

                previousGroup = currentGroup;
            }

            return circles;
        }
        #endregion

        #region 下到上

        public List<Circle> OrderBottomToTop(List<List<Circle>> circleGroups)
        {
            List<Circle> circles = new List<Circle>();

            Circle currentCircle = null; // 当前圆
            List<Circle> currentGroup = null; //当前分组
            List<Circle> previousGroup = null;//上一个分组

            for (int groupIdx = 0; groupIdx < circleGroups.Count; groupIdx++)
            {
                currentGroup = circleGroups[groupIdx];
                currentGroup = currentGroup.OrderBy(item => item.Center.X).ToList<Circle>();
                if (previousGroup != null && NeedDescendingTopBottom(previousGroup, currentGroup))
                {
                    currentGroup = currentGroup.OrderByDescending(item => item.Center.X).ToList<Circle>();
                }

                for (int idx = 0; idx < currentGroup.Count; idx++)
                {
                    currentCircle = currentGroup[idx];
                    circles.Add(currentCircle);
                }

                previousGroup = currentGroup;
            }

            return circles;
        }
        #endregion

        #region 顺时针&逆时针
        /// <summary>
        /// 顺时针&逆时针
        /// </summary>
        /// <param name="circleBases"></param>
        /// <param name="arcFlyingCutModel"></param>
        /// <returns></returns>
        public List<Circle> OrderClockwiseOrAnticlocksise(List<Circle> circleBases, ArcFlyingCutModel arcFlyingCutModel)
        {
            if(circleBases == null || circleBases.Count <= 1)
            {
                return circleBases;
            }
            double minX = circleBases.Min(circle => circle.Center.X);
            double maxX = circleBases.Max(circle => circle.Center.X);
            double minY = circleBases.Min(circle => circle.Center.Y);
            double maxY = circleBases.Max(circle => circle.Center.Y);

            //图形中心
            double avgX = (minX + maxX) / 2;
            double avgY = (minY + maxY) / 2;

            UnitPoint figureCenter = new UnitPoint(avgX, avgY);

            //按照到中心的距离排序
            circleBases = circleBases.OrderBy(circle => HitUtil.Distance(figureCenter, circle.Center)).ToList() ;

            List<List<Circle>> groups = new List<List<Circle>>();
            List<Circle> group = null;
            double distance = double.NaN;
            double previousDistance = double.NaN;

            double avgDistance = 0;
            double sumDistance = 0;
            //根据距离分组
            foreach(var item in circleBases)
            {
                distance = HitUtil.Distance(figureCenter, item.Center);
                if (double.IsNaN(previousDistance) || Math.Abs(distance - previousDistance) > LineFlyCut.THRESHOLD)
                {
                    group = new List<Circle>();
                    group.Add(item);
                    groups.Add(group);
                    previousDistance = distance;
                }
                else
                {
                    group.Add(item);
                }
            }

            List<Circle> listSum = null;
            foreach (var item in circleBases)
            {
                listSum = circleBases.ToList();
                listSum.Remove(item);
                sumDistance += listSum.Min(circle =>  HitUtil.Distance(item.Center, circle.Center));
            }
            avgDistance = sumDistance / circleBases.Count;

            List<Circle> currentGroup = null;
            List<Circle> previousGroup = null;
            List<int> mergedIdx = new List<int>();
            int previousIndex = 0;

            //分组优化
            for (int idx = 0; idx < groups.Count; idx ++)
            {
                if (previousGroup == null)
                {
                    previousGroup = groups[idx];
                    previousIndex = idx;
                    continue;
                }

                currentGroup = groups[idx];
                distance = HitUtil.Distance(figureCenter, currentGroup[0].Center);
                previousDistance = HitUtil.Distance(figureCenter, previousGroup[0].Center);

                double threshold =  avgDistance/2;

                if ((distance - previousDistance) < threshold && previousGroup.Count <=2 && currentGroup.Count >= 2)
                {
                    currentGroup.AddRange(previousGroup);
                    //mergedIdx.Add(idx-1);
                    mergedIdx.Add(previousIndex);
                    previousGroup = currentGroup;
                    previousIndex = idx;
                }
                else if ((distance - previousDistance) < threshold && currentGroup.Count <= 2 &&  previousGroup.Count >= 2)
                {
                    previousGroup.AddRange(currentGroup);
                    mergedIdx.Add(idx);
                }else if ((distance - previousDistance) < threshold)
                {
                    previousGroup.AddRange(currentGroup);
                    mergedIdx.Add(idx);
                }
                else
                {
                    previousGroup = currentGroup;
                    previousIndex = idx;
                }
            }

            //移除被合并的分组
            mergedIdx = mergedIdx.OrderByDescending(i => i).ToList();
            foreach (var item in mergedIdx)
            {
                groups.RemoveAt(item);
            }

            //分组内顺时针/逆时针排序
            List<Circle> groupsNew = OrderForRing(groups, figureCenter, arcFlyingCutModel);

            return groupsNew;
        }

        /// <summary>
        /// 对每个分组(圆环)顺时针/逆时针排序
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="center"></param>
        /// <param name="arcFlyingCutModel"></param>
        /// <returns></returns>
        private List<Circle> OrderForRing(List<List<Circle>> groups, UnitPoint center, ArcFlyingCutModel arcFlyingCutModel)
        {
            double endAngel = 0;
            List<Circle> orderdCircle = new List<Circle>();
            List<Circle> orderdGroup = null;
            if (arcFlyingCutModel.SortType == ArcFlyingCutSortTypes.Clockwise)
            {
                foreach (var group in groups)
                {
                    orderdGroup = group.OrderByDescending(circle =>
                        CalculateAngel(GetAngel(center, circle.Center, false), endAngel)
                    ).ToList<Circle>();

                    foreach (var circle in orderdGroup)
                    {
                        orderdCircle.Add(circle);
                    }

                    endAngel = GetAngel(center, orderdGroup[orderdGroup.Count - 1].Center, false);
                }

            }
            else if (arcFlyingCutModel.SortType == ArcFlyingCutSortTypes.Anticlockwise)
            {
                foreach (var group in groups)
                {
                    orderdGroup = group.OrderBy(circle =>
                        CalculateAngel(GetAngel(center, circle.Center, false), endAngel)
                    ).ToList<Circle>();
                    foreach (var circle in orderdGroup)
                    {
                        orderdCircle.Add(circle);
                    }

                    endAngel = GetAngel(center, orderdGroup[orderdGroup.Count - 1].Center, false);
                }
            }
            return orderdCircle;
        }
        #endregion

        #region 局部最短空移以及其他所有类型排序后调用
        /// <summary>
        /// 飞切后参数计算/记录
        /// </summary>
        /// <param name="selectedObjects"></param>
        /// <returns></returns>
        public List<Circle> CalulateFlyingParam(List<Circle> circleBases)
        {
            foreach (var itemCircle in circleBases)
            {
                itemCircle.IsFlyingCut = true;
                itemCircle.StartMovePoint = UnitPoint.Empty;
            }

            int group = 1;
            Circle previousCircle = null, currentCircle = null,  nextCircle = null;
            for (int idx=0; idx < circleBases.Count; idx ++)
            {
                currentCircle = circleBases[idx];
                currentCircle.GroupParam.GroupSN.Insert(1, group);

                if( idx + 1 < circleBases.Count)
                {
                    nextCircle = circleBases[idx + 1];
                }
                else
                    nextCircle = null;

                if (previousCircle == null)
                {
                    previousCircle = currentCircle;
                    continue; 
                }else if (DistanceOfCircles(previousCircle,currentCircle) > GlobalData.Model.GlobalModel.Params.ArcFlyingCut.MaxConnectSpace + LineFlyCut.THRESHOLD)
                {
                    //与前一个圆的距离超过设定的飞行连接距离
                    group++;
                    currentCircle.GroupParam.GroupSN.RemoveAt(1);
                    currentCircle.GroupParam.GroupSN.Insert(1, group);
                    previousCircle = currentCircle;
                    continue;
                }

                SetStartPointAndLeadLine(previousCircle, currentCircle, nextCircle);

                previousCircle = currentCircle;
            }

            //相同 group 只有一个圆 不能当做一个group 重置为0 ，其他group要重算
            int groupSn = 0;
            Dictionary<int, int> oldAndNew = new Dictionary<int, int>();
            foreach(var circle in circleBases)
            {
                if (oldAndNew.ContainsKey(circle.GroupParam.GroupSN[1]))
                {
                    circle.GroupParam.GroupSN[1] = oldAndNew[circle.GroupParam.GroupSN[1]];
                }
                else if (circleBases.Count(o => o.GroupParam.GroupSN[1] == circle.GroupParam.GroupSN[1]) < 2)
                {
                    circle.GroupParam.GroupSN[1] = 0;
                }
                else
                {
                    groupSn++;
                    oldAndNew.Add(circle.GroupParam.GroupSN[1], groupSn);
                    circle.GroupParam.GroupSN[1] = groupSn;
                }
            }

            this.ResetBezierParam(circleBases);

            return circleBases;
        }

        #endregion

        #region 按零件飞切

        public List<IDrawObject> DoArcFlyingCutForPart(List<IDrawObject> selectedObjects, ArcFlyingCutModel arcFlyingCutModel,ref List<IDrawObject> before)
        {
            List<IDrawObject> ret = new List<IDrawObject>();

            List<IDrawObject> listObj = new List<IDrawObject>();
            foreach(var item in selectedObjects)
            {
                listObj.Add(item);
            }

            List<List<IDrawObject>> groups = DoPartGroup(listObj);

            groups = SplitNonCircle(groups);

            //List<List<IDrawObject>> circleGroups = RemoveNonCircle(groups);

            before = new List<IDrawObject>();
            List<Circle> flycutGroup = null;
            int previousGroupSN = 0, currentGroupSN = 0;
            foreach (var group in groups)
            {
                bool noneCircle = false;
                foreach(var item in group)
                {
                    before.Add(item);
                    if(!(item is Circle))
                    {
                        noneCircle = true;
                        ret.Add(item);
                    }
                }

                if (noneCircle)
                {
                    continue;
                }

                flycutGroup = DoArcFlyingCut(group, arcFlyingCutModel);
                foreach (var circle in flycutGroup)
                {
                    if(circle.GroupParam.GroupSN.Count > 1 && circle.GroupParam.GroupSN[1] > 0)
                    {
                        //DoArcFlyingCut每次运算的group从1开始，多次运算的结果 groupSn要重算，加上前面的分组数
                        currentGroupSN = previousGroupSN + circle.GroupParam.GroupSN[1];
                        circle.GroupParam.GroupSN[1] = currentGroupSN;
                    }
                    
                    ret.Add(circle);
                }
                previousGroupSN = currentGroupSN;
            }
            return ret;
        }

        /// <summary>
        /// 如果一个分组包含了圆和非圆，非圆独立成一个分组
        /// </summary>
        /// <param name="drawGroups"></param>
        /// <returns></returns>
        private List<List<IDrawObject>> SplitNonCircle(List<List<IDrawObject>> drawGroups)
        {
            List<List<IDrawObject>> ret = new List<List<IDrawObject>>();

            List<IDrawObject> splitGroup = null;

            String firstType = string.Empty;
            foreach (var drawGroup in drawGroups)
            {
                firstType = string.Empty;
                splitGroup = new List<IDrawObject>();
                foreach (var draw in drawGroup)
                {
                    if (!(draw is Circle))
                    {
                        firstType = string.IsNullOrEmpty(firstType) ? "1" : firstType;//标记第一个是非圆
                        if ("1".Equals(firstType))
                            continue;
                        else
                        {
                            splitGroup.Add(draw);
                        }
                    }
                    else
                    {
                        firstType = string.IsNullOrEmpty(firstType) ? "0" : firstType;//标记第一个是圆
                        if ("0".Equals(firstType))
                            continue;
                        else
                        {
                            splitGroup.Add(draw);
                        }
                    }
                }

                if (splitGroup.Count > 0)
                {
                    foreach(var item in splitGroup)
                    {
                        drawGroup.Remove(item);
                    }
                }

                if (drawGroup.Count > 0)
                    ret.Add(drawGroup);
                if (splitGroup.Count > 0)
                {
                    ret.Add(splitGroup);
                }
            }
            return ret;
        }

        /// <summary>
        /// 按各图形零件分组(大图包含小图判断"零件")
        /// </summary>
        /// <param name="listObj"></param>
        /// <returns></returns>
        private List<List<IDrawObject>> DoPartGroup(List<IDrawObject> listObj)
        {
            IDrawObject current = null;
            IDrawObject outObject = null;

            List<List<IDrawObject>> ret = new List<List<IDrawObject>>();
            List<IDrawObject> innerObject = null;
            List <List<IDrawObject>> innerGroup = null;

            int idx = listObj.Count;
            List<IDrawObject> currentGroup = new List<IDrawObject>();
            List<IDrawObject> noCirclePart = null;
            while (listObj.Count > 0)
            {
                current = listObj[0];
                outObject = GetBoundingFiger(listObj, current, true);//包围current的最外的图形

                if (outObject == null)
                    outObject = current;
                else
                    current = outObject;// ***
                
                //找出 被outObject包含的图形
                innerObject = GetInnerFigers(listObj, outObject);
                if(innerObject.Count > 0)
                {
                    //递归前Remove(否则递归后 innerObject 已被清空)
                    foreach (var item in innerObject)
                    {
                        listObj.Remove(item);
                    }
                    innerGroup = DoPartGroup(innerObject);
                    ret.AddRange(innerGroup);
                }
                listObj.Remove(current);

                //包含有其他图形的非圆单独排序(新实例化一个List/Group,立刻放到ret)
                if (!(current is Circle) && innerObject != null && innerObject.Count > 0)
                {
                    noCirclePart = new List<IDrawObject>();
                    noCirclePart.Add(current);
                    ret.Add(noCirclePart);
                }
                else
                {
                    currentGroup.Add(current);
                }
            }
            if (currentGroup.Count > 0)
            {
                ret.Add(currentGroup);
            }
            return ret;
        }

        /// <summary>
        /// 检索figer包含了哪些图形
        /// </summary>
        /// <param name="selectedObjects"></param>
        /// <param name="figer"></param>
        /// <returns></returns>
        private List<IDrawObject> GetInnerFigers(List<IDrawObject> selectedObjects, IDrawObject figer)
        {
            List<IDrawObject> boundings = new List<IDrawObject>();

            //获取最小的包裹figer的图形
            foreach (var item in selectedObjects)
            {
                if (item is SingleDot || item == figer)
                    continue;
                if (DrawingOperationHelper.IsInsideOf(item,figer))
                {
                    boundings.Add(item);
                }
            }

            return boundings;
        }

        /// <summary>
        /// 检索包含figer的最外层哪个图形或者最内层哪个图形
        /// </summary>
        /// <param name="selectedObjects"></param>
        /// <param name="figer"></param>
        /// <param name="outermost"></param>
        /// <returns></returns>
        private IDrawObject GetBoundingFiger(List<IDrawObject> selectedObjects, IDrawObject figer, bool outermost)
        {
            List<IDrawObject> boundings = new List<IDrawObject>();

            //获取最小的包裹figer的图形
            foreach(var item in selectedObjects)
            {
                if (item is SingleDot || item == figer)
                    continue;
                if (DrawingOperationHelper.IsInsideOf(figer, item))
                {
                    boundings.Add(item);
                }
            }

            if (boundings.Count == 0)
                return null;
            if(boundings.Count == 1)
            {
                return boundings[0];
            }

            if (outermost)
            {
                //被多个图形包裹 从外到里排序
                boundings.Sort(new ComparerInOut(true));
            }
            else
            {
                //被多个图形包裹 从里到外排序
                boundings.Sort(new ComparerInOut(false));
            }

            return boundings[0];
        }

        /// <summary>
        /// 比较图形(包含关系)
        /// </summary>
        private class ComparerInOut : IComparer<IDrawObject>
        {
            private bool reverse = false;
            public ComparerInOut(bool reverse)
            {
                this.reverse = reverse;
            }
            public int Compare(IDrawObject x, IDrawObject y)
            {
                if (reverse)
                {
                    if (DrawingOperationHelper.IsInsideOf(x, y))
                    {
                        return 1;
                    }
                    return -1;
                }
                else
                {
                    if (DrawingOperationHelper.IsInsideOf(x, y))
                    {
                        return -1;
                    }
                    return 1;
                }
            }
        }
        #endregion

        #region 圆弧切割 公共方法分组

        /// <summary>
        /// 内部有包含的其他圆，则去掉外部的圆
        /// </summary>
        /// <param name="circles"></param>
        /// <returns></returns>
        public List<IDrawObject> DeleteOutterCircle(List<IDrawObject> circles)
        {
            int idx = circles.Count - 1;
            Circle currentCircle;
            bool hasIncludedCircle = false;
            while (idx >= 0)
            {
                currentCircle = circles[idx] as Circle;
                hasIncludedCircle = false;
                foreach (var item in circles)
                {
                    if (currentCircle == item)
                        continue;
                    if (IsOutSide(currentCircle, item as Circle))
                    {
                        hasIncludedCircle = true;
                        break;
                    }
                }
                if (hasIncludedCircle)
                {
                    circles.Remove(currentCircle);
                }
                idx--;
            }

            return circles;
        }

        /// <summary>
        /// 判断circle1是否包含circle2
        /// </summary>
        /// <param name="circle1"></param>
        /// <param name="circle2"></param>
        /// <returns></returns>
        public bool IsOutSide(Circle circle1, Circle circle2)
        {
            // 判断定理 设两个圆的半径为R和r，圆心距为d，
            // 则⑴d > R + r两圆外离； ⑵d = R + r 两圆外切； ⑶R - r < d < R + r(Rr) 两圆相交； ⑷d = R - r（R > r） 两圆内切； ⑸d < R - r(R > r)两圆内含．
            double o1o2 = HitUtil.Distance(circle1.Center, circle2.Center);
            bool isOutSide = (o1o2 <= Math.Abs(circle1.Radius - circle2.Radius) - LineFlyCut.THRESHOLD && circle1.Radius > circle2.Radius) ||
            (Math.Abs(circle1.Radius - circle2.Radius) <= LineFlyCut.THRESHOLD &&
             Math.Abs(circle1.Center.X - circle2.Center.X) <= LineFlyCut.THRESHOLD &&
             Math.Abs(circle1.Center.Y - circle2.Center.Y) <= LineFlyCut.THRESHOLD);
            return isOutSide;
        }

        /// <summary>
        /// 是否需要变换排序方向
        /// </summary>
        /// <param name="orderdPreviousGroup"></param>
        /// <param name="currentGroup"></param>
        /// <returns></returns>
        private bool NeedDescendingTopBottom(List<Circle> orderdPreviousGroup, List<Circle> currentGroup)
        {
            if (orderdPreviousGroup == null || currentGroup == null)
                return false;
            double previousEndX = orderdPreviousGroup[orderdPreviousGroup.Count - 1].Center.X;
            double currentMaxX = currentGroup.Max(circle => circle.Center.X);
            double currentMinX = currentGroup.Min(circle => circle.Center.X);

            bool isMaxEqual = Math.Abs(previousEndX - currentMaxX) <= LineFlyCut.THRESHOLD;

            if (previousEndX > currentMaxX || (isMaxEqual && currentGroup.Count > 1))
            {
                return true;
            }
            else if ((currentMaxX - previousEndX) < previousEndX - currentMinX)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否需要变换排序方向
        /// </summary>
        /// <param name="orderdPreviousGroup"></param>
        /// <param name="currentGroup"></param>
        /// <returns></returns>
        private bool NeedDescendingLeftRight(List<Circle> orderdPreviousGroup, List<Circle> currentGroup)
        {
            if (orderdPreviousGroup == null || currentGroup == null)
                return false;
            double previousEndY = orderdPreviousGroup[orderdPreviousGroup.Count - 1].Center.Y;
            double currentMaxY = currentGroup.Max(circle => circle.Center.Y);
            double currentMinY = currentGroup.Min(circle => circle.Center.Y);
            bool isMaxEqual = Math.Abs(previousEndY - currentMaxY) <= LineFlyCut.THRESHOLD;
            if (previousEndY > currentMaxY || (isMaxEqual && currentGroup.Count > 1))
            {
                return true;
            }
            else if ((currentMaxY - previousEndY) < previousEndY - currentMinY)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// circle1的终点到tangent1的圆弧与tangent1到tangent2的线段方向是否一致
        /// </summary>
        /// <param name="circle1"></param>
        /// <param name="tangent1">circle1与circle2的切线与 circle1 的切点</param>
        /// <param name="tangent2">circle1与circle2的切线与 circle2 的切点</param>
        /// <returns></returns>
        public bool IsTangantAndArcTheSameDirection(Circle circle1, UnitPoint tangent1, UnitPoint tangent2)
        {
            //经过tangent1的直径的另一个点 pointDiam到tangent1为直径
            UnitPoint pointDiam = HitUtil.GetLinePointByDistance(circle1.Center, tangent1, circle1.Radius, false);
            if (HitUtil.LinesIntersect(pointDiam, tangent1, circle1.StartMovePoint, tangent2))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取圆上面的点的角度
        /// </summary>
        /// <param name="center">圆心</param>
        /// <param name="testPoint">圆上面的点</param>
        /// <param name="change">0度是否转换成360度</param>
        /// <returns></returns>
        private float GetAngel(UnitPoint center, UnitPoint testPoint, bool change)
        {
            float startAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(center, testPoint, 0));
            if (change && Math.Abs(startAngle) <= LineFlyCut.THRESHOLD)
            {
                startAngle = 360;// true 0 转换成 360
            }
            else if (!change && Math.Abs(360 - startAngle) <= LineFlyCut.THRESHOLD)
            {
                startAngle = 0;// false 360 转换 0
            }
            return startAngle;
        }

        /// <summary>
        /// 确定圆的顺序后依次调用本方法设置起始切割位置、切线连接线
        /// </summary>
        /// <param name="previousCircle"></param>
        /// <param name="currentCircle"></param>
        /// <param name="nextCircle"></param>
        /// <param name="isCloseWise"></param>
        private void SetStartPointAndLeadLine(Circle previousCircle, Circle currentCircle, Circle nextCircle, bool isCloseWise = false)
        {
            //当前圆为第一个圆
            if (previousCircle == null)
                return;

            #region 上一步确定了
            if (currentCircle.StartMovePoint != UnitPoint.Empty)
            {
                previousCircle.FlyingCutLeadOut = new ArcFlyingLeadOut()
                {
                    HasLeadLine = true,
                    HasLeadArc = false,
                    LeadEndAngle = 0,
                    LeadEndPoint = currentCircle.StartMovePoint,
                };
                return;
            }
            #endregion

            Tuple<UnitPoint, UnitPoint> tangentLine = null;
            Tuple<UnitPoint, UnitPoint>[] tangentLines =
                DrawingOperationHelper.GetTangentLineOfCircle(previousCircle.Center, previousCircle.Radius, currentCircle.Center, currentCircle.Radius);

            #region 当前圆为第二个圆：与第一个圆的切线
            if (previousCircle.StartMovePoint == UnitPoint.Empty)
            {
                tangentLine = SelectTangent(previousCircle, currentCircle, tangentLines);

                previousCircle.StartMovePoint = tangentLine.Item1;
                currentCircle.StartMovePoint = tangentLine.Item2;
                previousCircle.FlyingCutLeadOut = new ArcFlyingLeadOut()
                {
                    HasLeadLine = true,
                    HasLeadArc = false,
                    LeadEndAngle = 0,
                    LeadEndPoint = currentCircle.StartMovePoint,
                };

                return;
            }
            #endregion

            #region 与前2圆切线在同一条直线上
            if (HitUtil.DistanctPointToLine(tangentLines[0].Item1, tangentLines[0].Item2, previousCircle.StartMovePoint) <= LineFlyCut.THRESHOLD)
            {
                tangentLine = tangentLines[0];
            }
            else if (HitUtil.DistanctPointToLine(tangentLines[1].Item1, tangentLines[1].Item2, previousCircle.StartMovePoint) <= LineFlyCut.THRESHOLD)
            {
                tangentLine = tangentLines[1];
            }

            if (tangentLine != null)
            {
                currentCircle.StartMovePoint = tangentLine.Item2;
                previousCircle.FlyingCutLeadOut = new ArcFlyingLeadOut()
                {
                    HasLeadLine = true,
                    HasLeadArc = false,
                    LeadEndAngle = 0,
                    LeadEndPoint = currentCircle.StartMovePoint,
                };
                return;
            }
            #endregion

            #region 上一个圆切割完继续一段圆弧在与本圆相切
            double endAngel = GetAngel(previousCircle.Center, previousCircle.StartMovePoint, false);
            double emptyMoveAngel1 = GetAngel(previousCircle.Center, tangentLines[0].Item1, false);
            double emptyMoveAngel2 = GetAngel(previousCircle.Center, tangentLines[1].Item1, false);

            emptyMoveAngel1 = emptyMoveAngel1 < endAngel ? (emptyMoveAngel1 + 360) - endAngel : emptyMoveAngel1 - endAngel;
            emptyMoveAngel2 = emptyMoveAngel2 < endAngel ? (emptyMoveAngel2 + 360) - endAngel : emptyMoveAngel2 - endAngel;

            if (emptyMoveAngel1 <= 180 + LineFlyCut.THRESHOLD && IsTangantAndArcTheSameDirection(previousCircle, tangentLines[0].Item1, tangentLines[0].Item2))
            {
                //圆弧+切线1
                currentCircle.StartMovePoint = tangentLines[0].Item2;
                previousCircle.FlyingCutLeadOut = new ArcFlyingLeadOut()
                {
                    HasLeadLine = true,
                    HasLeadArc = true,
                    LeadEndAngle = (float)emptyMoveAngel1,
                    LeadEndPoint = currentCircle.StartMovePoint,
                };
                return;
            }
            else if (emptyMoveAngel2 <= 180 + LineFlyCut.THRESHOLD && IsTangantAndArcTheSameDirection(previousCircle, tangentLines[1].Item1, tangentLines[1].Item2))
            {
                //圆弧+切线2
                currentCircle.StartMovePoint = tangentLines[1].Item2;
                previousCircle.FlyingCutLeadOut = new ArcFlyingLeadOut()
                {
                    HasLeadLine = true,
                    HasLeadArc = true,
                    LeadEndAngle = (float)emptyMoveAngel2,
                    LeadEndPoint = currentCircle.StartMovePoint,
                };
                return;
            }
            #endregion

            #region 当前圆与下一个圆的切线
            if (nextCircle != null)
            {
                //currentCircle 和 nextCircle的切线
                tangentLines =
                    DrawingOperationHelper.GetTangentLineOfCircle(currentCircle.Center, currentCircle.Radius, nextCircle.Center, nextCircle.Radius);
                
                //if (isCloseWise)
                //    tangentLine = SelectTangent(currentCircle, nextCircle, tangentLines, true);
                //else
                //    tangentLine = SelectTangent(currentCircle, nextCircle, tangentLines, false);
                tangentLine = SelectTangent(currentCircle, nextCircle, tangentLines);

                currentCircle.StartMovePoint = tangentLine.Item1;
                nextCircle.StartMovePoint = tangentLine.Item2;

                previousCircle.FlyingCutLeadOut = new ArcFlyingLeadOut()
                {
                    HasLeadLine = true,
                    HasLeadArc = false,
                    HasBezierCurve = true,
                    LeadEndAngle = 0,
                    PreviousEndPoint = UnitPoint.Empty,// 当前圆的前2圆的结束点或者切割后圆弧结束点(待定)
                    LeadEndPoint = currentCircle.StartMovePoint,
                    NextNextPoint = tangentLine.Item2
                };

                return;
            }
            #endregion

            #region 已经是最后一个圆 上一个圆终点和当前圆的切线
            currentCircle.StartMovePoint = TangentPoint(currentCircle, previousCircle.StartMovePoint);
            previousCircle.FlyingCutLeadOut = new ArcFlyingLeadOut()
            {
                HasLeadLine = true,
                HasLeadArc = false,
                LeadEndAngle = 0,
                LeadEndPoint = currentCircle.StartMovePoint,
            };
            #endregion
        }

        /// <summary>
        /// 点和圆的切点(只返回判断后选择的一个切点)
        /// </summary>
        /// <param name="circle"></param>
        /// <param name="testPoint"></param>
        /// <returns></returns>
        private UnitPoint TangentPoint(Circle circle, UnitPoint testPoint)
        {
            if (Math.Abs(HitUtil.Distance(circle.Center, testPoint) - circle.Radius) <= LineFlyCut.THRESHOLD)
            {
                //点不在圆外
                return UnitPoint.Empty;
            }

            //x1 x2为 经过testPoint、圆心的直线与圆的2个交点，其中x1为testPoint到圆心的线段内的交点
            UnitPoint x1 = HitUtil.GetLinePointByDistance(circle.Center, testPoint, circle.Radius, true);
            UnitPoint x2 = HitUtil.GetLinePointByDistance(circle.Center, testPoint, circle.Radius, false);

            UnitPoint p1 = HitUtil.TangentPointOnCircle(circle.Center, circle.Radius, testPoint, true);
            UnitPoint p2 = HitUtil.TangentPointOnCircle(circle.Center, circle.Radius, testPoint, false);

            if (IsAntiClockWise(x1, p1, x2))
            {
                return p1;
            }
            else if (IsAntiClockWise(x1, p2, x2))
            {
                return p2;
            }
            return UnitPoint.Empty;
        }

        /// <summary>
        /// 切线选择
        /// </summary>
        /// <param name="tangentLines"></param>
        /// <returns></returns>
        private Tuple<UnitPoint, UnitPoint> SelectTangent(Circle circle1, Circle circle2, Tuple<UnitPoint, UnitPoint>[] tangentLines)
        {
            //设两条切线为 A1B1 A2B2 circle1 circle2的圆心分别为O1 O2,半径分别为R1 R2
            //O1 O2的直线与circle1相交于X1、X2点，其中X2点为在O1O2线段内的点

            UnitPoint pointX1 = HitUtil.GetLinePointByDistance(circle1.Center, circle2.Center, circle1.Radius, false);
            UnitPoint pointX2 = HitUtil.GetLinePointByDistance(circle1.Center, circle2.Center, circle1.Radius, true);

            if (IsAntiClockWise(pointX1, tangentLines[0].Item1, pointX2))
            {
                return tangentLines[0];
            }
            else if (IsAntiClockWise(pointX1, tangentLines[1].Item1, pointX2))
            {
                return tangentLines[1];
            }

            return null;
        }

        /// <summary>
        /// 判断圆上的3点 P1 到 P2 到 P3方向是否为逆时针 true  逆时针 false 顺时针
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns>true  逆时针 false 顺时针</returns>
        private bool IsAntiClockWise(UnitPoint p1, UnitPoint p2, UnitPoint p3)
        {
            /*
            判断三点是顺时针还是逆时针方向
            设 p1=(x1,y1)， p2=(x2,y2)， p3=(x3,y3) 
            求向量 
            p12=(x2-x1,y2-y1) 
            p23=(x3-x2,y3-y2) 
            则当 p12 与 p23 的叉乘（向量积） 
            p12 x p23 = (x2-x1)*(y3-y2)-(y2-y1)*(x3-x2) 
            为正时，p1-p2-p3 路径的走向为逆时针， 
            为负时，p1-p2-p3 走向为顺时针， 
            为零时，p1-p2-p3 所走的方向不变，亦即三点在一直线上。
             **/

            double x = (p2.X - p1.X) * (p3.Y - p2.Y) - (p2.Y - p1.Y) * (p3.X - p2.X);

            if (x > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            throw new Exception("3点在一条直线上，无法判断顺时针还是逆时针");
        }

        /// <summary>
        /// 两个圆的距离(最近处的距离)
        /// </summary>
        /// <param name="circle1"></param>
        /// <param name="circle2"></param>
        /// <returns></returns>
        private double DistanceOfCircles(Circle circle1, Circle circle2)
        {
            //两个圆心的距离减2个圆的半径
            return Math.Abs(HitUtil.Distance(circle1.Center, circle2.Center) - circle1.Radius - circle2.Radius);
        }

        private double CalculateAngel(double angel, double endAngel)
        {
            return endAngel > 0 && angel < endAngel ? 360 + angel : angel;
        }

        /// <summary>
        /// 更新贝塞尔曲线曲线前的直线起点
        /// </summary>
        /// <param name="circles"></param>
        private void ResetBezierParam(List<Circle> circles)
        {
            for (int i = 0; i < circles.Count; i++)
            {
                if (i >= 1 && circles[i].FlyingCutLeadOut != null && circles[i].FlyingCutLeadOut.HasBezierCurve)
                {
                    UnitPoint pointPreviousTangentStart = UnitPoint.Empty;
                    Circle prePreviousCircle = circles[i - 1];
                    if (circles[i - 1].FlyingCutLeadOut != null && circles[i - 1].FlyingCutLeadOut.HasLeadArc)
                    {
                        pointPreviousTangentStart = DrawingOperationHelper.GetPointOnArcByAngle(
                            prePreviousCircle.Center, prePreviousCircle.Radius, prePreviousCircle.StartAngle + prePreviousCircle.FlyingCutLeadOut.LeadEndAngle);
                    }
                    else
                    {
                        pointPreviousTangentStart = prePreviousCircle.StartMovePoint;
                    }

                    circles[i].FlyingCutLeadOut.PreviousEndPoint = pointPreviousTangentStart;

                    //两根切线的交点
                    UnitPoint intersectPoint = HitUtil.FindApparentIntersectPoint(
                        circles[i].FlyingCutLeadOut.PreviousEndPoint, circles[i].StartMovePoint, circles[i].FlyingCutLeadOut.LeadEndPoint, circles[i].FlyingCutLeadOut.NextNextPoint);

                    //圆的起点到交点小于1.5  去掉0.5直线
                    double toIntersectPoint = HitUtil.Distance(circles[i].StartMovePoint, intersectPoint);
                    //if (toIntersectPoint < 1.5 - LineFlyCut.THRESHOLD)
                    //{
                    //    circles[i].FlyingCutLeadOut.LeadlineDistance = 0;//去掉0.5直线
                    //}

                    //if (toIntersectPoint < 1.0*3/2 +LineFlyCut.THRESHOLD)
                    //{
                    //    circles[i].FlyingCutLeadOut.BezierWide = 0.63333333 * toIntersectPoint;//只延长到交点
                    //}

                    if (toIntersectPoint < 1.0 * 3 / 2 + LineFlyCut.THRESHOLD)
                    {
                        circles[i].FlyingCutLeadOut.LeadlineDistance = 0;//去掉0.5直线
                        circles[i].FlyingCutLeadOut.BezierWide = 0.63333333 * toIntersectPoint;//只延长到交点
                    }

                }
            }
        }

        /// <summary>
        /// 求圆的最左、最右、最上、最下的点
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <param name="circlePointType"></param>
        /// <returns></returns>
        private UnitPoint GetCirclePoint(UnitPoint center, double radius, CirclePointType circlePointType)
        {
            List<UnitPoint> pointLeftRight = HitUtil.GetIntersectPointLineWithCircle(
                        new UnitPoint(center.X - radius - 1, center.Y), new UnitPoint(center.X + radius + 1, center.Y), center, (float)radius, UCCanvas.GetThresholdWidth());
            List<UnitPoint> pointTopBottom = HitUtil.GetIntersectPointLineWithCircle(
                        new UnitPoint(center.X, center.Y - radius - 1), new UnitPoint(center.X, center.Y + radius + 1), center, (float)radius, UCCanvas.GetThresholdWidth());

            switch (circlePointType)
            {
                case CirclePointType.LEFT:
                    return pointLeftRight[0].X < pointLeftRight[1].X ? pointLeftRight[0] : pointLeftRight[1];
                case CirclePointType.RIGHT:
                    return pointLeftRight[0].X > pointLeftRight[1].X ? pointLeftRight[0] : pointLeftRight[1];
                case CirclePointType.TOP:
                    return pointTopBottom[0].Y > pointTopBottom[1].Y ? pointTopBottom[0] : pointTopBottom[1];
                case CirclePointType.BOTTOM:
                    return pointTopBottom[0].Y < pointTopBottom[1].Y ? pointTopBottom[0] : pointTopBottom[1];
                default:
                    return UnitPoint.Empty;

            }
        }

        /// <summary>
        /// 根据左右/上下分组（不是圆心是按最左、最右、最上、最下的点）
        /// </summary>
        /// <param name="circleBases"></param>
        /// <param name="arcFlyingCutModel"></param>
        /// <returns></returns>
        private List<List<Circle>> CalCutGrouping(List<Circle> circleBases, ArcFlyingCutModel arcFlyingCutModel)
        {
            List<Circle> orderedCircles = null;
            List<List<Circle>> ret = new List<List<Circle>>();

            switch (arcFlyingCutModel.SortType)
            {
                case ArcFlyingCutSortTypes.LeftToRight:
                    orderedCircles = circleBases.OrderBy(circle => this.GetCirclePoint(circle.Center, circle.Radius, CirclePointType.LEFT).X).ToList<Circle>();

                   double preLeftX = double.NaN,leftX = double.NaN;

                    List<Circle> tempCommonLeftX = null;

                    int idx = 0;
                    foreach (var circle in orderedCircles)
                    {
                        idx++;
                        leftX = this.GetCirclePoint(circle.Center, circle.Radius, CirclePointType.LEFT).X;

                        if (double.IsNaN(preLeftX))
                        {
                            preLeftX = leftX;
                            tempCommonLeftX = new List<Circle>();
                            tempCommonLeftX.Add(circle);
                            ret.Add(tempCommonLeftX);
                        }
                        else if (preLeftX + LineFlyCut.THRESHOLD >= leftX)
                        {
                            //误差范围内与前一个分到同一组(最小到最大，两倍误差)
                            tempCommonLeftX.Add(circle);
                        }
                        else
                        {
                            preLeftX = leftX;
                            tempCommonLeftX = new List<Circle>();
                            tempCommonLeftX.Add(circle);
                            ret.Add(tempCommonLeftX);
                        }
                    }
                    break;
                    
                case ArcFlyingCutSortTypes.RightToLeft:
                    //X轴 大到小
                    orderedCircles = circleBases.OrderByDescending(circle => this.GetCirclePoint(circle.Center, circle.Radius, CirclePointType.RIGHT).X).ToList<Circle>();

                    double preRightX = double.NaN, rightX = double.NaN;

                    List<Circle> tempCommonRightX = null;

                    foreach (var circle in orderedCircles)
                    {
                        rightX = this.GetCirclePoint(circle.Center, circle.Radius, CirclePointType.RIGHT).X;

                        if (double.IsNaN(preRightX))
                        {
                            preRightX = rightX;
                            tempCommonRightX = new List<Circle>();
                            tempCommonRightX.Add(circle);
                            ret.Add(tempCommonRightX);
                        }
                        else if (rightX + LineFlyCut.THRESHOLD >= preRightX)
                        {
                            //误差范围内与前一个分到同一组(最小到最大，两倍误差)
                            tempCommonRightX.Add(circle);
                        }
                        else
                        {
                            preRightX = rightX;
                            tempCommonRightX = new List<Circle>();
                            tempCommonRightX.Add(circle);
                            ret.Add(tempCommonRightX);
                        }
                    }
                    break;
                case ArcFlyingCutSortTypes.TopToBottom:
                    //Y轴 大到小
                    orderedCircles = circleBases.OrderByDescending(circle => this.GetCirclePoint(circle.Center, circle.Radius, CirclePointType.TOP).Y).ToList<Circle>();

                    double preTopY = double.NaN, topY = double.NaN;

                    List<Circle> tempCommonTopY = null;

                    foreach (var circle in orderedCircles)
                    {
                        topY = this.GetCirclePoint(circle.Center, circle.Radius, CirclePointType.TOP).Y;

                        if (double.IsNaN(preTopY))
                        {
                            preTopY = topY;
                            tempCommonTopY = new List<Circle>();
                            tempCommonTopY.Add(circle);
                            ret.Add(tempCommonTopY);
                        }
                        else if (topY >= preTopY - LineFlyCut.THRESHOLD)
                        {
                            //误差范围内与前一个分到同一组
                            tempCommonTopY.Add(circle);
                        }
                        else
                        {
                            preTopY = topY;
                            tempCommonTopY = new List<Circle>();
                            tempCommonTopY.Add(circle);
                            ret.Add(tempCommonTopY);
                        }
                    }
                    break;
                case ArcFlyingCutSortTypes.BottomToTop:
                    //Y轴 小到大
                    orderedCircles = circleBases.OrderBy(circle => this.GetCirclePoint(circle.Center, circle.Radius, CirclePointType.BOTTOM).Y).ToList<Circle>();

                    double preBottomY = double.NaN, bottomY = double.NaN;

                    List<Circle> tempCommonBottomY = null;

                    foreach (var circle in orderedCircles)
                    {
                        bottomY = this.GetCirclePoint(circle.Center, circle.Radius, CirclePointType.BOTTOM).Y;

                        if (double.IsNaN(preBottomY))
                        {
                            preBottomY = bottomY;
                            tempCommonBottomY = new List<Circle>();
                            tempCommonBottomY.Add(circle);
                            ret.Add(tempCommonBottomY);
                        }
                        else if (bottomY <= preBottomY + LineFlyCut.THRESHOLD)
                        {
                            //误差范围内与前一个分到同一组
                            tempCommonBottomY.Add(circle);
                        }
                        else
                        {
                            preBottomY = bottomY;
                            tempCommonBottomY = new List<Circle>();
                            tempCommonBottomY.Add(circle);
                            ret.Add(tempCommonBottomY);
                        }
                    }
                    break;
                case ArcFlyingCutSortTypes.Clockwise:
                    //顺时针
                    break;
                case ArcFlyingCutSortTypes.Anticlockwise:
                    //逆时针
                    break;
                case ArcFlyingCutSortTypes.InsideToOut:
                    //里到外
                    break;
                case ArcFlyingCutSortTypes.LocalShortestEmptyMove:
                    //局部最短空移
                    break;
            }

            return ret;
        }
        #endregion

        #endregion
    }

    #region LineFlyCut类
    /// <summary>
    /// 飞切逻辑计算使用的线段
    /// </summary>
    public class LineFlyCut
    {

        //public static string testS(List<List<LineFlyCut>> x)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach(var iteml in x)
        //    {
        //        foreach(var item in iteml)
        //        {
        //            sb.Append(Convert.ToInt16(item.StartPoint.X).ToString().PadLeft(3, '0')).Append(",")
        //                .Append(Convert.ToInt16(item.StartPoint.Y).ToString().PadLeft(3, '0')).Append(" -> ")
        //                .Append(Convert.ToInt16(item.EndPoint.X).ToString().PadLeft(3, '0')).Append(",")
        //                .Append(Convert.ToInt16(item.EndPoint.Y).ToString().PadLeft(3, '0')).Append(Environment.NewLine);
        //        }
        //    }

        //    return sb.ToString();
        //}

        /// <summary>
        /// 可以给个序号方便调试时知道是哪个线段
        /// </summary>
        public string Name { set; get; }

        public UnitPoint StartPoint { set; get; }
        public UnitPoint EndPoint { set; get; }

        //public int Direction(bool reverse)
        //{
        //    if (StartPoint.X == StartPoint.X)
        //    {
        //        if (StartPoint.Y < EndPoint.Y)
        //            return 3;
        //        else
        //            return 4;
        //    }
        //    else
        //    {
        //        bool x = StartPoint.X < EndPoint.X;
        //        if (x)
        //            return 1;
        //        else
        //            return 2;
        //    }
        //}

        /// <summary>
        /// threshold 计算精度偏差
        /// </summary>
        public const double THRESHOLD = 0.00002d; // threshold 

        public LineFlyCut(String name, UnitPoint startPoint, UnitPoint endPoint)
        {
            this.Name = name;
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
        }

        /// <summary>
        /// 光滑连接的线段的起点
        /// </summary>
        public UnitPoint ConnectLineStartPoint { set; get; } = new UnitPoint(double.NaN, double.NaN);

        /// <summary>
        /// 光滑连接的线段的终点
        /// </summary>
        public UnitPoint ConnectLineEndPoint { set; get; } = new UnitPoint(double.NaN, double.NaN);

        /// <summary>
        /// 线段/直线的斜率
        /// </summary>
        public double Slope
        {
            get
            {
                return Math.Abs(StartPoint.X - EndPoint.X) <= LineFlyCut.THRESHOLD ? double.NaN : (StartPoint.Y - EndPoint.Y) / (StartPoint.X - EndPoint.X);
            }
        }

        /// <summary>
        /// 线段/直线是否平行
        /// </summary>
        /// <param name="testLine"></param>
        /// <returns></returns>
        public bool IsParallel(LineFlyCut testLine)
        {
            if (double.IsNaN(this.Slope) && double.IsNaN(testLine.Slope))
                return true;
            else
                return Math.Abs(this.Slope - testLine.Slope) < LineFlyCut.THRESHOLD;
        }

        /// <summary>
        /// 求点到线段/直线的垂直距离
        /// </summary>
        /// <param name="testPoint"></param>
        /// <returns></returns>
        public double DistanctPointToLine(UnitPoint testPoint)
        {
            //三角形边长
            double distanceStartTest = HitUtil.Distance(StartPoint, testPoint);
            double distanceEndTest = HitUtil.Distance(EndPoint, testPoint);
            double distanceStartEnd = HitUtil.Distance(StartPoint, EndPoint);

            //半周长
            double p = (distanceStartTest + distanceEndTest + distanceStartEnd) / 2;

            //海伦公式求三角形面积
            double s = Math.Sqrt(p * Math.Abs(p - distanceStartTest) * Math.Abs(p - distanceEndTest) * Math.Abs(p - distanceStartEnd));

            return 2 * s / distanceStartEnd;
        }

        /// <summary>
        /// 直线与X轴的交点
        /// </summary>
        public UnitPoint IntersectX
        {
            get
            {
                if (Math.Abs(StartPoint.Y - EndPoint.Y) <= LineFlyCut.THRESHOLD)
                {
                    UnitPoint intersectPoint = new UnitPoint();
                    intersectPoint.X = double.NaN;
                    intersectPoint.Y = double.NaN;
                    return intersectPoint;//与X轴平行 无交点 返回交点的X,Y为double.NaN
                }

                UnitPoint intercectPoint = HitUtil.FindApparentIntersectPoint(StartPoint, EndPoint, new UnitPoint(0d, 0d), new UnitPoint(1d, 0d), true, true);

                return intercectPoint;
            }
        }

        /// <summary>
        /// 直线与Y轴的交点
        /// </summary>
        public UnitPoint IntersectY
        {
            get
            {
                if (Math.Abs(StartPoint.X - EndPoint.X) <= LineFlyCut.THRESHOLD)
                {
                    return new UnitPoint();//与Y轴平行 无交点 返回交点的X,Y为double.NaN
                }
                UnitPoint intercectPoint = HitUtil.FindApparentIntersectPoint(StartPoint, EndPoint, new UnitPoint(0d, 0d), new UnitPoint(0d, 1d), true, true);

                return intercectPoint;
            }
        }

    }
    #endregion

    /// <summary>
    /// 圆的最左、最右、最上、最下点的参数
    /// </summary>
    enum CirclePointType
    {
        LEFT = 1,
        RIGHT = 2,
        TOP = 3,
        BOTTOM = 4
    }
}