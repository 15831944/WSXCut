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
    /// <summary>
    /// 矩形
    /// </summary>
    public class RectangleCommon : MultiSegmentLineBase
    {
        private UnitPoint p1, p2;
        protected override MultiSegementLineCurrentPoint CurrentPoint { get; set; } = MultiSegementLineCurrentPoint.StartPoint;

        public override IDrawObject Clone()
        {
            RectangleCommon rectangleCommon = new RectangleCommon();
            rectangleCommon.Copy(this);
            return rectangleCommon;
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
            if (this.CurrentPoint == MultiSegementLineCurrentPoint.EndPoint)
            {
                this.p2 = unitPoint;
                this.Points.Clear();
                this.Points.Add(new UnitPointBulge(this.p1));
                this.Points.Add(new UnitPointBulge(new UnitPoint(this.p2.X, this.p1.Y)));
                this.Points.Add(new UnitPointBulge(this.p2));
                this.Points.Add(new UnitPointBulge(new UnitPoint(this.p1.X, this.p2.Y)));
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
