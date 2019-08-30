using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.DrawModel
{
    public class DotLite: IDrawLite
    {
        public Pen DrawPen { get; set; }
        public Brush DrawBrush { get; set; }
        public UnitPoint Point { get; set; }
        public float Radius { get; set; }
        public bool IsInCompensation { get; set; }

        public void Draw(ICanvas canvas,float scale)
        {
            if (this.DrawPen != null)
            {
                canvas.DrawDot(canvas, this.DrawPen, this.Point, (float)canvas.ToUnit(this.Radius));
            }
            else
            {
                canvas.DrawDot(canvas, this.DrawBrush, this.Point, (float)canvas.ToUnit(this.Radius));
            }
        }
    }
}
