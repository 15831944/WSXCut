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
    public class RectangleRound : MultiSegmentLineBase
    {
        private UnitPoint p1, p2, p3 = UnitPoint.Empty;
        private double maxRadius;
        private List<UnitPoint> rectPoint = new List<UnitPoint>();
        protected override MultiSegementLineCurrentPoint CurrentPoint { get; set; } = MultiSegementLineCurrentPoint.StartPoint;
        public override IDrawObject Clone()
        {
            RectangleRound rectangleRound = new RectangleRound();
            rectangleRound.Copy(this);
            return rectangleRound;
        }

        public override void Copy(DrawObjectBase drawObjectBase)
        {
            base.Copy(drawObjectBase);
        }

        public override void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            if (this.IsEditMode || this.IsCompleteDraw)
            {
                base.Draw(canvas, unitRectangle);
            }
            else
            {
                this.DrawRectRound(canvas, unitRectangle);
            }
        }

        private void DrawRectRound(ICanvas canvas, RectangleF unitRectangle)
        {
            if (this.PointCount > 1)
            {
                if (this.CurrentPoint == MultiSegementLineCurrentPoint.EndPoint)
                {
                    canvas.DrawLine(canvas, DrawUtils.SelectecedPen, this.p2, this.p3);
                }
                for (int i = 0; i < this.PointCount - 1; i++)
                {
                    if (double.IsNaN(this.Points[i].Bulge))
                    {
                        canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[LayerId], this.Points[i].Point, this.Points[i + 1].Point);
                    }
                    else
                    {
                        this.DrawBulgeArc(canvas, unitRectangle, DrawUtils.CustomSelectedPens[LayerId], this.Points[i], this.Points[i + 1]);
                    }
                }
                if (double.IsNaN(this.Points[PointCount - 1].Bulge))
                {
                    canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.Points[this.Points.Count - 1].Point, this.Points[0].Point);
                }
                else
                {
                    this.DrawBulgeArc(canvas, unitRectangle, DrawUtils.CustomSelectedPens[this.LayerId], this.Points[this.PointCount - 1], this.Points[0]);
                }
            }
        }

        public override bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            if (this.IsEditMode || this.IsCompleteDraw)
            {
                return base.ObjectInRectangle(rectangle, anyPoint);
            }
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
            if (this.IsEditMode || this.IsCompleteDraw)
            {
                return base.GetBoundingRectangle(rectangleType);
            }
            float thWidth = UCCanvas.GetThresholdWidth();
            float maxX = (float)this.Points.Max<UnitPointBulge>(x => x.Point.X);
            float maxY = (float)this.Points.Max<UnitPointBulge>(x => x.Point.Y);
            float minX = (float)this.Points.Min<UnitPointBulge>(x => x.Point.X);
            float minY = (float)this.Points.Min<UnitPointBulge>(x => x.Point.Y);
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.EndPoint)
            {
                maxX = maxX > this.p3.X ? maxX : (float)p3.X;
                maxY = maxY > this.p3.Y ? maxY : (float)p3.Y;
                minX = minX < this.p3.X ? minX : (float)p3.X;
                minY = minY < this.p3.Y ? minY : (float)p3.Y;
            }
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
                return DrawObjectMouseDown.Continue;
            }
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.EndPoint)
            {
                this.CurrentPoint = MultiSegementLineCurrentPoint.Done;
                this.IsSelected = false;
                this.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
                this.GroupParam.ShowSN = this.GroupParam.FigureSN;
                this.IsCompleteDraw = true;
                this.StartMovePoint = this.Points[0].Point;
                this.EndMovePoint = this.Points[0].Point;
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
            else if (this.CurrentPoint == MultiSegementLineCurrentPoint.MidPoint)
            {
                this.p2 = unitPoint;
                this.Points.Clear();
                this.rectPoint.Clear();
                this.Points.Add(new UnitPointBulge(this.p1));
                this.Points.Add(new UnitPointBulge(new UnitPoint(this.p2.X, this.p1.Y)));
                this.Points.Add(new UnitPointBulge(this.p2));
                this.Points.Add(new UnitPointBulge(new UnitPoint(this.p1.X, this.p2.Y)));
                this.maxRadius = this.GetMinSideLen() / 2;
                for (int i = 0; i < this.PointCount; i++)
                {
                    this.rectPoint.Add(this.Points[i].Point);
                }
                return;
            }
            else if (this.CurrentPoint == MultiSegementLineCurrentPoint.EndPoint)
            {
                this.p3 = unitPoint;
                double radius = HitUtil.Distance(unitPoint, this.p2);
                this.CalculateRoundParams(radius);
            }
        }

        private void CalculateRoundParams(double radius)
        {
            if (radius > this.maxRadius) return;
            List<UnitPointBulge> temp = new List<UnitPointBulge>();
            double bulge = BulgeHelper.GetBulgFromRadian(Math.PI / 2);
            bulge = this.Clockwise ? -bulge : bulge;
            List<UnitPoint> unitPoints = BulgeHelper.GetCufoffPoint(this.rectPoint[0], this.rectPoint[1], this.rectPoint[2], radius);
            temp.Add(new UnitPointBulge(unitPoints[0], bulge));
            temp.Add(new UnitPointBulge(unitPoints[1]));
            unitPoints = BulgeHelper.GetCufoffPoint(this.rectPoint[1], this.rectPoint[2], this.rectPoint[3], radius);
            temp.Add(new UnitPointBulge(unitPoints[0], bulge));
            temp.Add(new UnitPointBulge(unitPoints[1]));
            unitPoints = BulgeHelper.GetCufoffPoint(this.rectPoint[2], this.rectPoint[3], this.rectPoint[0], radius);
            temp.Add(new UnitPointBulge(unitPoints[0], bulge));
            temp.Add(new UnitPointBulge(unitPoints[1]));
            unitPoints = BulgeHelper.GetCufoffPoint(this.rectPoint[3], this.rectPoint[0], this.rectPoint[1], radius);
            temp.Add(new UnitPointBulge(unitPoints[0], bulge));
            temp.Add(new UnitPointBulge(unitPoints[1]));
            this.Points = temp;
            this.Points.Insert(0, this.Points[this.PointCount - 1]);
            this.Points.RemoveAt(this.PointCount - 1);
        }

        private double GetMinSideLen()
        {
            double sideLen = HitUtil.Distance(this.Points[0].Point, this.Points[1].Point);
            double sideLen2 = HitUtil.Distance(this.Points[1].Point, this.Points[2].Point);
            double minSide = sideLen > sideLen2 ? sideLen2 : sideLen;
            return minSide;
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
