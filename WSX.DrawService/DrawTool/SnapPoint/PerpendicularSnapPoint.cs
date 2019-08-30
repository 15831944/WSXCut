using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool
{
    public class PerpendicularSnapPoint:SnapPointBase
    {
        public PerpendicularSnapPoint(ICanvas canvas,IDrawObject owner,UnitPoint snapPoint) : base(canvas, owner, snapPoint) { }

        public override void Draw(ICanvas canvas)
        {
            base.DrawPoint(canvas, Pens.White, Brushes.YellowGreen);
        }
    }
}
