using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.Utils;

namespace WSX.DrawService.CanvasControl
{
    /// <summary>
    /// 点捕获接口
    /// </summary>
    public interface ISnapPoint
    {
        IDrawObject Owner { get; }
        UnitPoint SnapPoint { get; }
        RectangleF BoundingRect { get; }
        void Draw(ICanvas canvas);
    }
}
