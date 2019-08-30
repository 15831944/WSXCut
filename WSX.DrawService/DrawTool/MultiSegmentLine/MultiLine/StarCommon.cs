using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;
using WSX.GlobalData.Model;

namespace WSX.DrawService.DrawTool.MultiSegmentLine.MultiLine
{
    public class StarCommon : MultiSegmentLineBase
    {
        private UnitPoint p1, p2;
        public int SideCount = 8;
        protected override MultiSegementLineCurrentPoint CurrentPoint { get; set; } = MultiSegementLineCurrentPoint.StartPoint;

        public override IDrawObject Clone()
        {
            StarCommon starCommon = new StarCommon();
            starCommon.Copy(this);
            return starCommon;
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
                for (int i = 0; i < this.PointCount - 1; i++)
                {
                    canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.Points[i].Point, this.Points[i + 1].Point);
                }
                canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.Points[this.Points.Count - 1].Point, this.Points[0].Point);
            }
        }

        public override bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.SelectRange);
            if (anyPoint)//包含图形的任一点即可
            {
                for (int i = 0; i < this.Points.Count - 1; i++)
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
            float maxX = (float)this.Points.Max<UnitPointBulge>(x => x.Point.X);
            float maxY = (float)this.Points.Max<UnitPointBulge>(x => x.Point.Y);
            float minX = (float)this.Points.Min<UnitPointBulge>(x => x.Point.X);
            float minY = (float)this.Points.Min<UnitPointBulge>(x => x.Point.Y);
            return ScreenUtils.GetRectangleF(new UnitPoint(minX, minY), new UnitPoint(maxX, maxY), thWidth / 2);
        }

        public override DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint unitPoint, ISnapPoint snapPoint, MouseEventArgs e)
        {
            OnMouseMove(canvas, unitPoint);
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.StartPoint)
            {
                this.CurrentPoint = MultiSegementLineCurrentPoint.MidPoint;
                return DrawObjectMouseDown.Continue;
            }
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.MidPoint)
            {
                this.CurrentPoint = MultiSegementLineCurrentPoint.EndPoint;
                Point positionCursor = Cursor.Position;
                PointF point0 = canvas.ToScreen(this.Points[0].Point);
                Point offset0 = new Point((int)point0.X - positionCursor.X, (int)point0.Y - positionCursor.Y);
                PointF point = canvas.ToScreen(this.Points[1].Point);
                Cursor.Position = new Point((int)point.X - offset0.X, (int)point.Y - offset0.Y);
                return DrawObjectMouseDown.Continue;
            }
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.EndPoint)
            {
                this.CurrentPoint = MultiSegementLineCurrentPoint.Done;
                this.IsSelected = false;
                this.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
                this.GroupParam.ShowSN = this.GroupParam.FigureSN;
                this.StartMovePoint = this.Points[0].Point;
                this.EndMovePoint = this.Points[0].Point;
                this.IsCompleteDraw = true;
                return DrawObjectMouseDown.Done;
            }
            return DrawObjectMouseDown.Done;
        }

        public override void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {
            base.OnMouseMove(canvas, unitPoint);
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.StartPoint)
            {
                this.p1 = unitPoint;
                this.Points = new List<UnitPointBulge>();
                Points.Add(new UnitPointBulge(this.p1));
            }
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.MidPoint)
            {
                this.p2 = unitPoint;
                this.Points.Clear();
                this.CalculateVertexPoints(this.p1, this.p2);
            }
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.EndPoint)
            {
                this.p2 = unitPoint;
                this.CalculateVertexPoints2(this.p1, this.p2);
            }
        }

        private void CalculateVertexPoints(UnitPoint centerPoint, UnitPoint vertexPoint)
        {
            double startAngle = HitUtil.LineAngleR(centerPoint, vertexPoint, 0);
            double radius = HitUtil.Distance(centerPoint, vertexPoint, true);
            double stepAngle = 360 / (this.SideCount * 2);
            for (int i = 0; i < this.SideCount * 2; i++)
            {
                double r = i % 2 == 0 ? radius : radius * 0.3817;
                UnitPoint point = HitUtil.PointOnCircle(centerPoint, r, startAngle + stepAngle * Math.PI / 180 * i);
                this.Points.Add(new UnitPointBulge(point));
            }
        }

        private void CalculateVertexPoints2(UnitPoint centerPoint, UnitPoint vertexPoint)
        {
            double startAngle = HitUtil.LineAngleR(centerPoint, this.Points[1].Point, 0);
            double r = HitUtil.Distance(centerPoint, vertexPoint, true);
            double stepAngle = 360 / this.SideCount;
            for (int i = 0; i < this.Points.Count; i++)
            {
                if (i % 2 != 0)
                {
                    UnitPoint point = HitUtil.PointOnCircle(centerPoint, r, startAngle + stepAngle * Math.PI / 180 * (i / 2));
                    this.Points[i] = new UnitPointBulge(point);
                }
            }
        }

        public override bool IsCloseFigure
        {
            get
            {
                return true;
            }
        }

    }
}
