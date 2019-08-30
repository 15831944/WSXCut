using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.FigureModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawTool;
using WSX.DrawService.DrawTool.Arcs;
using WSX.DrawService.DrawTool.CircleTool;
using WSX.DrawService.DrawTool.MultiSegmentLine;
using WSX.DrawService.DrawTool.MultiSegmentLine.MultiLine;

namespace WSX.WSXCut.Utils
{
    public class FigureHelper
    {
        /// <summary>
        /// 转换为可以保存的类型
        /// </summary>
        /// <param name="drawObjects"></param>
        /// <returns></returns>
        public static List<FigureBaseModel> ToFigureBaseModel(List<DrawService.CanvasControl.IDrawObject> drawObjects)
        {
            if (drawObjects == null) return null;
            List<FigureBaseModel> rets = new List<FigureBaseModel>();
            foreach (DrawService.CanvasControl.IDrawObject drawObj in drawObjects)
            {
                switch (drawObj.FigureType)
                {
                    case FigureTypes.Point:
                        {
                            var figure = drawObj as SingleDot;
                            rets.Add(new PointModel()
                            {
                                LayerId = figure.LayerId,
                                Point = figure.P1,
                                GroupParam = figure.GroupParam,
                                IsFill = figure.IsCloseFigure,
                                IsSelected = figure.IsSelected,
                                LeadIn = figure.LeadIn,
                                LeadOut = figure.LeadOut,
                                MicroConnects = figure.MicroConnParams,
                                CornerRingParam = figure.CornerRingParam,
                                IsInnerCut = figure.IsInnerCut
                            });
                        }
                        break;
                    case FigureTypes.Arc:
                        {
                            var figure = drawObj as ArcBase;
                            rets.Add(new ArcModel()
                            {
                                LayerId = figure.LayerId,
                                Center = figure.Center,
                                EndAngle = figure.EndAngle,
                                StartAngle = figure.StartAngle,
                                Radius = figure.Radius,
                                GroupParam = figure.GroupParam,
                                IsFill = figure.IsCloseFigure,
                                AngleSweep = figure.AngleSweep,
                                IsSelected = figure.IsSelected,
                                LeadIn = figure.LeadIn,
                                LeadOut = figure.LeadOut,
                                MicroConnects = figure.MicroConnParams,
                                CompensationParam = figure.CompensationParam,
                                CornerRingParam = figure.CornerRingParam,
                                IsInnerCut = figure.IsInnerCut
                            });
                        }
                        break;
                    case FigureTypes.Circle:
                        {
                            var figure = drawObj as Circle;
                            rets.Add(new CircleModel()
                            {
                                LayerId = figure.LayerId,
                                Center = figure.Center,
                                StartAngle = figure.StartAngle,
                                Radius = figure.Radius,
                                GroupParam = figure.GroupParam,
                                IsSelected = figure.IsSelected,
                                IsClockwise = figure.IsClockwise,
                                IsFill = figure.IsCloseFigure,
                                LeadIn = figure.LeadIn,
                                LeadOut = figure.LeadOut,
                                MicroConnects = figure.MicroConnParams,
                                CompensationParam = figure.CompensationParam,
                                CornerRingParam = figure.CornerRingParam,
                                IsInnerCut = figure.IsInnerCut,
                                IsFlyingCut = figure.IsFlyingCut,
                                IsFlyingCutScatter = figure.IsFlyingCutScatter,
                                FlyingCutLeadOut = figure.FlyingCutLeadOut,
                            });
                        }
                        break;
                    case FigureTypes.LwPolyline:
                        {
                            var figure = drawObj as MultiSegmentLineBase;
                            rets.Add(new LwPolylineModel()
                            {
                                LayerId = figure.LayerId,
                                IsFill = figure.IsCloseFigure,
                                Points = figure.Points,
                                GroupParam = figure.GroupParam,
                                IsSelected = figure.IsSelected,
                                PathStartParm = figure.PathStartParm,
                                LeadIn = figure.LeadIn,
                                LeadOut = figure.LeadOut,
                                MicroConnects = figure.MicroConnParams,
                                CompensationParam = figure.CompensationParam,
                                CornerRingParam = figure.CornerRingParam,
                                IsInnerCut = figure.IsInnerCut,
                                BezierParam = figure.BezierParam
                            });
                        }
                        break;
                    case FigureTypes.Ellipse:
                        { }
                        break;
                    case FigureTypes.PolyBezier:
                        { }
                        break;

                    default:
                        break;
                }
            }
            return rets;
        }

        /// <summary>
        /// 添加到显示画布
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="figures"></param>
        public static void AddToDrawObject(UCCanvas canvas, List<FigureBaseModel> figures, bool isClear = false)
        {
            if (canvas == null) return;
            if (figures != null && figures.Count > 0)
            {
                if (isClear) canvas.Model.Clear();
                List<IDrawObject> objects = new List<IDrawObject>();
                foreach (FigureBaseModel figure in figures)
                {
                    switch (figure.Type)
                    {
                        case FigureTypes.Point:
                            {
                                var fig = figure as PointModel;
                                SingleDot singleDot = new SingleDot()
                                {
                                    LayerId = fig.LayerId,
                                    IsSelected = fig.IsSelected,
                                    P1 = fig.Point,
                                    GroupParam = fig.GroupParam,
                                    LeadIn = fig.LeadIn,
                                    LeadOut = fig.LeadOut,
                                    MicroConnParams = figure.MicroConnects,
                                    CornerRingParam = figure.CornerRingParam,
                                    IsInnerCut = figure.IsInnerCut
                                };
                                singleDot.Update();
                                objects.Add(singleDot);
                            }
                            break;
                        case FigureTypes.Arc:
                            {
                                var fig = figure as ArcModel;
                                ArcBase arcBase = new ArcBase()
                                {
                                    LayerId = fig.LayerId,
                                    IsSelected = fig.IsSelected,
                                    AngleSweep = fig.AngleSweep,
                                    Center = fig.Center,
                                    Radius = (float)fig.Radius,
                                    StartAngle = (float)fig.StartAngle,
                                    GroupParam = fig.GroupParam,
                                    LeadIn = fig.LeadIn,
                                    LeadOut = fig.LeadOut,
                                    MicroConnParams = figure.MicroConnects,
                                    CompensationParam = figure.CompensationParam,
                                    CornerRingParam = figure.CornerRingParam,
                                    IsInnerCut = figure.IsInnerCut
                                };
                                arcBase.Update();
                                objects.Add(arcBase);
                            }
                            break;
                        case FigureTypes.Circle:
                            {
                                var fig = figure as CircleModel;
                                Circle circle = new Circle()
                                {
                                    LayerId = fig.LayerId,
                                    IsSelected = fig.IsSelected,
                                    IsClockwise = fig.IsClockwise,
                                    Center = fig.Center,
                                    Radius = (float)fig.Radius,
                                    StartAngle = (float)fig.StartAngle,
                                    GroupParam = fig.GroupParam,
                                    LeadIn = fig.LeadIn,
                                    LeadOut = fig.LeadOut,
                                    MicroConnParams = figure.MicroConnects,
                                    CompensationParam = figure.CompensationParam,
                                    CornerRingParam = figure.CornerRingParam,
                                    IsInnerCut = figure.IsInnerCut,
                                    IsFlyingCut = fig.IsFlyingCut,
                                    IsFlyingCutScatter = fig.IsFlyingCutScatter,
                                    FlyingCutLeadOut = fig.FlyingCutLeadOut,
                                };
                                circle.Update();
                                objects.Add(circle);
                            }
                            break;
                        case FigureTypes.LwPolyline:
                            {
                                var fig = figure as LwPolylineModel;
                                MultiSegmentLineBase multiSegmentLineBase = new MultiSegmentLineBase()
                                {
                                    LayerId = fig.LayerId,
                                    IsSelected = fig.IsSelected,
                                    IsCloseFigure = fig.IsFill,
                                    Points = fig.Points,
                                    GroupParam = fig.GroupParam,
                                    LeadIn = fig.LeadIn,
                                    LeadOut = fig.LeadOut,
                                    PathStartParm = fig.PathStartParm,
                                    MicroConnParams = figure.MicroConnects,
                                    CompensationParam = figure.CompensationParam,
                                    CornerRingParam = figure.CornerRingParam,
                                    IsInnerCut = figure.IsInnerCut,
                                    BezierParam = fig.BezierParam
                                };
                                multiSegmentLineBase.Update();
                                objects.Add(multiSegmentLineBase);
                            }
                            break;
                        case FigureTypes.Ellipse:
                            { }
                            break;
                        case FigureTypes.PolyBezier:
                            { }
                            break;

                        default:
                            break;
                    }
                }
                canvas.Model.DrawingLayer.AddObjectOnDrawing(objects);
                canvas.Model.DrawingLayer.UpdateSN();
                GlobalData.Model.GlobalModel.TotalDrawObjectCount = canvas.Model.DrawingLayer.Objects.Count;
                canvas.DoInvalidate(true);
            }
        }
    }
}
