using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool
{
    class IntersectSnapPoint:SnapPointBase
    {
        public IntersectSnapPoint(ICanvas canvas, IDrawObject drawObject, UnitPoint unitPoint) : base(canvas, drawObject, unitPoint) { }

        public override void Draw(ICanvas canvas)
        {
            base.DrawPoint(canvas, System.Drawing.Pens.White, System.Drawing.Brushes.YellowGreen);
        }
    }
}
