using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;
using WSX.GlobalData;
using WSX.GlobalData.Messenger;
using WSX.GlobalData.Model;
using static WSX.DrawService.DrawCommand;

namespace WSX.DrawService.DrawTool.MultiSegmentLine.MultiLine
{
    public class LineCommon : MultiSegmentLineBase
    {
        private UnitPoint p1, p2;
        protected override MultiSegementLineCurrentPoint CurrentPoint { get; set; } = MultiSegementLineCurrentPoint.StartPoint;

        public override UnitPoint RepeatStartingPoint
        {
            get
            {
                return this.p2;
            }
        }
        public override IDrawObject Clone()
        {
            LineCommon lineCommon = new LineCommon();
            lineCommon.Copy(this);
            return lineCommon;
        }

        public override void Copy(DrawObjectBase drawObjectBase)
        {
            base.Copy(drawObjectBase);
        }

        public override void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            if (this.IsCompleteDraw || this.IsEditMode)
            {
                base.Draw(canvas, unitRectangle);
            }
            else
            {
                canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.p1, this.p2);
            }
        }

        public override bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            if (anyPoint)//包含图形的任一点即可
            {
                return HitUtil.LineIntersectWithRect(base.Points[0].Point, base.Points[1].Point, rectangle);
            }
            RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.SelectRange);
            return rectangle.Contains(rectangleF);
        }

        public override RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            if (this.IsCompleteDraw || this.IsEditMode)
            {
                return base.GetBoundingRectangle(rectangleType);
            }
            float thWidth = UCCanvas.GetThresholdWidth();
            return ScreenUtils.GetRectangleF(this.p1, this.p2, thWidth / 2);
        }

        public override DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint unitPoint, ISnapPoint snapPoint, MouseEventArgs e)
        {
            OnMouseMove(canvas, unitPoint);
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.StartPoint)
            {
                this.CurrentPoint = MultiSegementLineCurrentPoint.EndPoint;
                return DrawObjectMouseDown.Continue;
            }
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.EndPoint)
            {
                this.CurrentPoint = MultiSegementLineCurrentPoint.Done;
                this.IsSelected = false;
                this.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
                this.GroupParam.ShowSN = this.GroupParam.FigureSN;
                this.StartMovePoint = this.Points[0].Point;
                this.EndMovePoint = this.Points[1].Point;
                this.LeadOut.Position = this.Points.Count - 1;
                this.IsCompleteDraw = true;
                return DrawObjectMouseDown.DoneRepeat;
            }
            return DrawObjectMouseDown.Done;
        }

        public override void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {
            base.OnMouseMove(canvas, unitPoint);
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.StartPoint)
            {
                this.p1 = unitPoint;
                this.p2 = p1;
                this.Points = new List<UnitPointBulge>();
                Points.Add(new UnitPointBulge(this.p1));
            }
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.EndPoint)
            {
                this.p2 = unitPoint;
                this.Points.Clear();
                this.Points.Add(new UnitPointBulge(this.p1));
                this.Points.Add(new UnitPointBulge(this.p2));
            }
        }

        public override bool IsCloseFigure
        {
            get
            {
                return false;
            }
        }


    }
}
