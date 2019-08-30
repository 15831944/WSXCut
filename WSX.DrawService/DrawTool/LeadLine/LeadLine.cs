using System;
using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.LeadLine;
using WSX.CommomModel.ParaModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawModel;
using WSX.DrawService.DrawTool.Arcs;
using WSX.DrawService.DrawTool.CircleTool;
using WSX.DrawService.DrawTool.MultiSegmentLine;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool.LeadLine
{
    public class LeadLine
    {
        private UnitPoint p1, p2;
        private enum CurrentPoint { FirstPoint, EndPoint }
        private CurrentPoint CurrentMousePoint;

        public LeadLine(UnitPoint mousePoint)
        {
            this.p2 = UnitPoint.Empty;
            this.CurrentMousePoint = CurrentPoint.FirstPoint;
            this.OnMouseDown(mousePoint);
        }

        public DrawObjectMouseDown OnMouseDown(UnitPoint point)
        {
            this.OnMouseMove(point);
            if (CurrentMousePoint == CurrentPoint.FirstPoint)
            {
                CurrentMousePoint = CurrentPoint.EndPoint;
                return DrawObjectMouseDown.Continue;
            }
            return DrawObjectMouseDown.Done;
        }

        public void OnMouseMove(UnitPoint unitPoint)
        {
            if (CurrentMousePoint == CurrentPoint.FirstPoint)
            {
                this.p1 = unitPoint;
            }
            else
            {
                this.p2 = unitPoint;
            }
        }

        public void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            RectangleF rectangleF = new RectangleF(canvas.ToScreen(this.p1), new SizeF(0, 0));
            rectangleF.Inflate(4, 4);
            canvas.Graphics.DrawArc(DrawUtils.LeadLinePen, rectangleF, 0, 360);
            canvas.DrawLine(canvas, DrawUtils.SelectedLeadLinePen, this.p1, this.p2);
        }

        public RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            float thWidth = UCCanvas.GetThresholdWidth();
            return ScreenUtils.GetRectangleF(this.p1, this.p2, thWidth / 2);
        }

        public void CalLeadLineParams(IDrawObject drawObject, LineInOutParamsModel leadInOutParamsModel)
        {
            //位置(1.计算当前鼠标点位置在图形的第几段线上，2.计算当前鼠标点在当前线段的相对百分位置)
            switch (drawObject.Id)
            {
                case "Circle":
                    this.GetCircleLeadLineParams(drawObject, leadInOutParamsModel);
                    break;
                case "MultiLineBase":
                    this.GetMultiLineLeadLineParams(drawObject, leadInOutParamsModel);
                    break;
                case "Arc":
                    this.GetArcLeadLineParams(drawObject, leadInOutParamsModel);
                    break;
                default: break;
            }
        }

        private void GetCircleLeadLineParams(IDrawObject drawObject, LineInOutParamsModel leadInOutParamsModel)
        {
            Circle circle = (Circle)drawObject;
            double angle = HitUtil.LineAngleR(circle.Center, this.p2, 0);
            leadInOutParamsModel.FigureTotalLength = (float)(angle / (Math.PI * 2));
            this.p2 = HitUtil.PointOnCircle(circle.Center, circle.Radius, angle);
            bool isInner;
            leadInOutParamsModel.LineInAngle = DrawingOperationHelper.GetLeadLineAngleArc(this.p1, this.p2, circle.Center,circle.IsClockwise,out isInner);
            leadInOutParamsModel.LineInLength = (float)HitUtil.Distance(this.p1, this.p2);
            circle.IsInnerCut = isInner;
        }
        private void GetMultiLineLeadLineParams(IDrawObject drawObject, LineInOutParamsModel leadInOutParamsModel)
        {
            MultiSegmentLineBase multiSegmentLine = drawObject as MultiSegmentLineBase;
            int segment = 0;
            if (multiSegmentLine != null)
            {
                //所在段数必须合法
                segment = DrawingOperationHelper.GetPointInLineIndex(multiSegmentLine, this.p2);
                int nextIndex = (segment + 1 >= multiSegmentLine.PointCount && multiSegmentLine.IsCloseFigure) ? 0 : segment + 1;
                if (double.IsNaN(multiSegmentLine.Points[segment].Bulge))
                {
                    double partLen = HitUtil.Distance(multiSegmentLine.Points[segment].Point, this.p2);
                    double allLen = HitUtil.Distance(multiSegmentLine.Points[segment].Point, multiSegmentLine.Points[nextIndex].Point);
                    float percent = (float)(segment + partLen / allLen);
                    leadInOutParamsModel.FigureTotalLength=(float)(DrawingOperationHelper.GetLengthByPositionInPolyLine(multiSegmentLine.Points, multiSegmentLine.IsCloseFigure, percent) / multiSegmentLine.SizeLength);
                    //角度(p1p2与当前线段所成夹角或与圆弧在当前鼠标点位置的切点出的切线所成夹角)
                    this.p2 = HitUtil.FindApparentIntersectPoint(this.p1, this.p2, multiSegmentLine.Points[segment].Point, multiSegmentLine.Points[nextIndex].Point);
                    double lineAngle = HitUtil.LineAngleR(multiSegmentLine.Points[nextIndex].Point, multiSegmentLine.Points[segment].Point, 0);
                    UnitPoint extensionCordPoint = HitUtil.LineEndPoint(multiSegmentLine.Points[segment].Point, lineAngle, 5);
                    leadInOutParamsModel.LineInAngle = (float)BulgeHelper.CalTwoLinesAngleFromThreePoints(extensionCordPoint, this.p2, this.p1);
                    multiSegmentLine.IsInnerCut=HitUtil.IsClockwiseByCross(this.p1, this.p2, extensionCordPoint) == multiSegmentLine.Clockwise ? false : true;
                }
                else
                {
                    ArcModelMini arcModelMini = DrawingOperationHelper.GetArcParametersFromBulge(multiSegmentLine.Points[segment].Point, multiSegmentLine.Points[nextIndex].Point, (float)multiSegmentLine.Points[segment].Bulge);
                    float percent = segment + DrawingOperationHelper.GetPercentInArcByPoint(arcModelMini, this.p2);
                    leadInOutParamsModel.FigureTotalLength = (float)(DrawingOperationHelper.GetLengthByPositionInPolyLine(multiSegmentLine.Points, multiSegmentLine.IsCloseFigure, percent) / multiSegmentLine.SizeLength);
                    bool isInner;
                    leadInOutParamsModel.LineInAngle = DrawingOperationHelper.GetLeadLineAngleArc(this.p1, this.p2, arcModelMini.Center,arcModelMini.Clockwise,out isInner);
                    multiSegmentLine.IsInnerCut = isInner;
                }
            }
            //长度(p1p2的长度)
            leadInOutParamsModel.LineInLength = (float)HitUtil.Distance(p1, p2);
        }

        private void GetArcLeadLineParams(IDrawObject drawObject, LineInOutParamsModel leadInOutParamsModel)
        {
            ArcBase arcBase = (ArcBase)drawObject;
            double angle = HitUtil.LineAngleR(arcBase.Center, this.p2, 0);
            leadInOutParamsModel.FigureTotalLength = (float)(angle / (Math.PI * 2));//TODO:验证
            this.p2 = HitUtil.PointOnCircle(arcBase.Center, arcBase.Radius, angle);
            bool isInner;
            leadInOutParamsModel.LineInAngle = DrawingOperationHelper.GetLeadLineAngleArc(this.p1, this.p2, arcBase.Center,arcBase.IsClockwise,out isInner);
            leadInOutParamsModel.LineInLength = (float)HitUtil.Distance(this.p1, this.p2);
            arcBase.IsInnerCut = isInner;
        }      

    }
}
