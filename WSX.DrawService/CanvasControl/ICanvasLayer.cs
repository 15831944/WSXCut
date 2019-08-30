using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.DrawModel;

namespace WSX.DrawService.CanvasControl
{
    /// <summary>
    /// 画布分层
    /// </summary>
    public interface ICanvasLayer
    {
        string LayerName { get; }//图层名称
        void Draw(ICanvas canvas, RectangleF unitRectangle);
        ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, List<IDrawObject> otherObject);
        List<IDrawObject> Objects { get; }
        bool Locked { get; set; }//锁定图层
        bool Visible { get; }//隐藏或者显示图层
    }
}
