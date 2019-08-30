using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.DrawModel
{
    public class LineLite : IDrawLite
    {
        public Pen DrawPen { get; set; }
        public List<UnitPoint> Points { get; set; }
        public bool IsLeadLine { get; set; }

        public void Draw(ICanvas canvas, float scale)
        {
            if (this.IsLeadLine)
            {
                canvas.DrawLeadLine(canvas, this.DrawPen, this.Points[0], this.Points[1], scale);
            }
            else
            {
                canvas.DrawLine(canvas, this.DrawPen, this.Points[0], this.Points[1], scale);
            }
        }
    }
}
