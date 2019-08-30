using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool
{
    public class QuadrantSnapPoint:SnapPointBase
    {
        public QuadrantSnapPoint(ICanvas canvas,IDrawObject drawObject,UnitPoint snapPoint) : base(canvas, drawObject, snapPoint) { }

        public override void Draw(ICanvas canvas)
        {
            base.DrawPoint(canvas, Pens.White, Brushes.YellowGreen);
        }
    }
}
