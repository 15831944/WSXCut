using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.Utils;

namespace WSX.DrawService.CanvasControl
{
    public interface INodePoint
    {
        IDrawObject GetClone();
        IDrawObject GetOriginal();
        void Cancel();
        void Finish();
        void SetPosition(UnitPoint postion);
        void Undo();
        void Redo();
        void OnKeyDown(ICanvas canvas, KeyEventArgs e);
    }
}
