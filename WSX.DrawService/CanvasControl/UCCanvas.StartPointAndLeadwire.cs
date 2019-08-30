using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.DrawService.DrawTool;
using WSX.DrawService.DrawTool.Arcs;
using WSX.DrawService.DrawTool.CircleTool;
using WSX.DrawService.DrawTool.LeadLine;
using WSX.DrawService.DrawTool.MultiSegmentLine;
using WSX.DrawService.Utils;

namespace WSX.DrawService.CanvasControl
{
    //起点和引线设置
    public partial class UCCanvas
    {
        private bool leadLineDone = true;//当前引线是否设置完成
        private LeadLine leadLine;

        private void MouseDownSetStartMovePoint(MouseEventArgs e)
        {
            UnitPoint mousePoint = this.ToUnit(this.mouseDownPoint);
            IDrawObject hitObject = this.Model.GetHitObject(mousePoint);
            if (hitObject != null && leadLineDone && hitObject.IsCloseFigure)
            {
                LineInOutParamsModel leadInOutParamsModel = CopyUtil.DeepCopy(hitObject.LeadInOutParams);
                leadInOutParamsModel.LinePosition = LinePositions.FigureTotalLength;
                leadInOutParamsModel.FigureTotalLength = this.CalPercentByPoint(hitObject, mousePoint);
                this.Model.DoSetLeadwire(new List<IDrawObject>() { hitObject }, new List<LineInOutParamsModel>() { leadInOutParamsModel });
                this.DoInvalidate(true);
            }
            else//绘制引入线
            {
                if (this.leadLine == null)
                {
                    this.leadLine = new LeadLine(mousePoint);
                    this.leadLineDone = false;
                }
                else
                {
                    if (hitObject != null)
                    {
                        LineInOutParamsModel leadInOutParamsModel = CopyUtil.DeepCopy(hitObject.LeadInOutParams);
                        leadInOutParamsModel.LinePosition = LinePositions.FigureTotalLength;
                        leadInOutParamsModel.LineInType = CommomModel.DrawModel.LeadLine.LeadLineType.Line;
                        if (hitObject.IsCloseFigure)
                        {
                            DrawObjectMouseDown result = this.leadLine.OnMouseDown(mousePoint);
                            this.leadLine.CalLeadLineParams(hitObject, leadInOutParamsModel);
                            this.Model.DoSetLeadwire(new List<IDrawObject>() { hitObject }, new List<LineInOutParamsModel>() { leadInOutParamsModel });
                        }
                        else if (HitUtil.PointInPoint(mousePoint, hitObject.FirstDrawPoint, GetThresholdWidth()))
                        {
                            DrawObjectMouseDown result = this.leadLine.OnMouseDown(hitObject.FirstDrawPoint);
                            this.leadLine.CalLeadLineParams(hitObject, leadInOutParamsModel);
                            this.Model.DoSetLeadwire(new List<IDrawObject>() { hitObject }, new List<LineInOutParamsModel>() { leadInOutParamsModel });
                        }
                        this.leadLine = null;
                        this.leadLineDone = true;
                    }
                    else
                    {
                        this.leadLine = new LeadLine(mousePoint);
                    }
                    this.DoInvalidate(true);
                }
            }
        }

        private void MouseMoveSetStartMovePoint(MouseEventArgs e)
        {
            if (this.leadLine != null)
            {
                UnitPoint mousePoint = this.GetMousePoint();
                Rectangle invalidaterect = ScreenUtils.ConvertRectangle(ScreenUtils.ToScreenNormalized(this.canvasWrapper, this.leadLine.GetBoundingRectangle(BoundingRectangleType.Drawing)));
                invalidaterect.Inflate(2, 2);
                RepaintStatic(invalidaterect);
                this.leadLine.OnMouseMove(mousePoint);

                CanvasWrapper canvasWrapper = new CanvasWrapper(this, Graphics.FromHwnd(this.Handle), this.ClientRectangle);
                RectangleF invalidateRect = ScreenUtils.ConvertRectangle(ScreenUtils.ToScreenNormalized(this.canvasWrapper, this.leadLine.GetBoundingRectangle(BoundingRectangleType.Drawing)));
                this.leadLine.Draw(canvasWrapper, invalidateRect);
                canvasWrapper.Graphics.Dispose();
                canvasWrapper.Dispose();
            }
        }

        #region 求取点在图形上的百分比
        private float CalPercentByPoint(IDrawObject drawObject,UnitPoint unitPoint)
        {
            switch (drawObject.Id)
            {
                case "Circle":
                    return this.GetPercentInCircleByPoint(drawObject, unitPoint);
                case "MultiLineBase":
                    return this.GetPercentInPolylineByPoint(drawObject, unitPoint);
                case "Arc":
                    return this.GetPercentInArcByPoint(drawObject, unitPoint);
                default:
                    return 0;
            }
        }

        private float GetPercentInCircleByPoint(IDrawObject drawObject,UnitPoint unitPoint)
        {
            Circle circle = (Circle)drawObject;
            double angle = HitUtil.LineAngleR(circle.Center, unitPoint, 0);
           return (float)(angle / (Math.PI * 2));
        }
        private float GetPercentInPolylineByPoint(IDrawObject drawObject, UnitPoint unitPoint)
        {
            MultiSegmentLineBase item = (MultiSegmentLineBase)drawObject;
            int segment = DrawingOperationHelper.GetPointInLineIndex(item, unitPoint);
            int nextIndex = (segment + 1 >= item.PointCount) ? 0 : segment + 1;
            float percent;
            if (double.IsNaN(item.Points[segment].Bulge))
            {
                double partLen = HitUtil.Distance(item.Points[segment].Point, unitPoint);
                double allLen = HitUtil.Distance(item.Points[segment].Point, item.Points[nextIndex].Point);
                percent = (float)(segment + partLen / allLen);
            }
            else
            {
                DrawModel.ArcModelMini arcModelMini = DrawingOperationHelper.GetArcParametersFromBulge(item.Points[segment].Point, item.Points[nextIndex].Point, (float)item.Points[segment].Bulge);
                percent = segment + DrawingOperationHelper.GetPercentInArcByPoint(arcModelMini, unitPoint);
            }
            return (float)(DrawingOperationHelper.GetLengthByPositionInPolyLine(item.Points, item.IsCloseFigure, percent) / item.SizeLength);
        }
        private float GetPercentInArcByPoint(IDrawObject drawObject, UnitPoint unitPoint)
        {
            ArcBase arcBase = (ArcBase)drawObject;
            double angle = HitUtil.LineAngleR(arcBase.Center, unitPoint, 0);
            return (float)(angle / (Math.PI * 2));
        }
        #endregion
    }
}
