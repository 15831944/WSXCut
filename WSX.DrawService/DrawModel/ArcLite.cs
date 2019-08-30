using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.DrawModel
{
    public class ArcLite: IDrawLite
    {
        public Pen DrawPen { get; set; }
        public UnitPoint Center { get; set; }
        public float Radius { get; set; }
        public float StartAngle { get; set; }
        public float SweepAngle { get; set; }

        public void Draw(ICanvas canvas, float scale)
        {
            canvas.DrawArc(canvas, this.DrawPen, this.Center, this.Radius, this.StartAngle, this.SweepAngle, scale);
        }
    }
}
