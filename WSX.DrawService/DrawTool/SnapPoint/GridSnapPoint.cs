using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool
{
    public class GridSnapPoint:SnapPointBase
    {
        public GridSnapPoint(ICanvas canvas,UnitPoint snapPoint):base(canvas,null,snapPoint)
        {

        }

        public override void Draw(ICanvas canvas)
        {
            base.DrawPoint(canvas, Pens.Gray, null);
        }
    }
}
