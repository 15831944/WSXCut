using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.DrawModel
{
    public class BezierLite: IDrawLite
    {
        public Pen DrawPen { get; set; }
        public List<UnitPoint> Points { get; set; }

        public void Draw(ICanvas canvas,float scale)
        {
            canvas.DrawBeziers(canvas, this.DrawPen, this.Points, scale);
        }
    }
}
