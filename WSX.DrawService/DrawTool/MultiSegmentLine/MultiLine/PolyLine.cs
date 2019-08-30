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
    public class PolyLine : MultiSegmentLineBase
    {
        private UnitPoint p1, p2;
        protected override MultiSegementLineCurrentPoint CurrentPoint { get; set; } = MultiSegementLineCurrentPoint.StartPoint;

        public override string Id
        {
            get
            {
                if (this.IsCompleteDraw)
                {
                    return base.Id;
                }
                else
                {
                    return "PolyLine";
                }
            }
        }

        public override IDrawObject Clone()
        {
            PolyLine polyLine = new PolyLine();
            polyLine.Copy(this);
            return polyLine;
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
                this.DrawPolyLine(canvas, unitRectangle);
            }
        }

        private void DrawPolyLine(ICanvas canvas, RectangleF unitRectangle)
        {
            for (int i = 0; i < this.PointCount - 1; i++)
            {
                canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.Points[i].Point, this.Points[i + 1].Point);
            }
            if (!this.p2.IsEmpty)
            {
                canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.Points[this.Points.Count - 1].Point, this.p2);
            }
        }

        public override bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.SelectRange);
            if (anyPoint)//包含图形的任一点即可
            {
                for (int i = 0; i < this.PointCount - 1; i++)
                {
                    if (HitUtil.LineIntersectWithRect(this.Points[i].Point, this.Points[i + 1].Point, rectangle))
                    {
                        return true;
                    }
                }
            }
            return rectangle.Contains(rectangleF);
        }

        public override RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            if (this.IsCompleteDraw || this.IsEditMode)
            {
                return base.GetBoundingRectangle(rectangleType);
            }
            float thWidth = UCCanvas.GetThresholdWidth();
            return ScreenUtils.GetRectangleF(this.Points[this.Points.Count - 1].Point, this.p2, thWidth / 2);
        }

        public override DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint unitPoint, ISnapPoint snapPoint, MouseEventArgs e)
        {
            OnMouseMove(canvas, unitPoint);
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.StartPoint)
            {
                this.CurrentPoint = MultiSegementLineCurrentPoint.EndPoint;
                this.p1 = unitPoint;
                this.Points = new List<UnitPointBulge>();
                this.Points.Add(new UnitPointBulge(this.p1));

                return DrawObjectMouseDown.Continue;
            }
            else
            {
                this.Points.Add(new UnitPointBulge(unitPoint));

                if (this.Points.Count == 2)
                {
                    this.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
                    this.GroupParam.ShowSN = this.GroupParam.FigureSN;
                    this.StartMovePoint = this.Points[0].Point;
                    return DrawObjectMouseDown.Change;
                }
                this.EndMovePoint = this.IsCloseFigure ? this.Points[0].Point : this.Points[this.Points.Count - 1].Point;
                if (!this.IsCloseFigure) this.LeadOut.Position = this.Points.Count - 1;
            }
            return DrawObjectMouseDown.Continue;
        }

        public override void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {
            base.OnMouseMove(canvas, unitPoint);
            this.p2 = unitPoint;
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
