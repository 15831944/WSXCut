using System.Windows.Forms;
using WSX.CommomModel.DrawModel;

namespace WSX.DrawService.CanvasControl
{
    public partial class UCCanvas
    {
        private bool IsMovePositionComplete = false;
        private void MouseDownMovePosition(MouseEventArgs e)
        {
            this.IsMovePositionComplete = !this.IsMovePositionComplete;
            if (!this.IsMovePositionComplete)
            {
                this.CommandEscape();
            }
            else
            {
                UnitPoint mousePoint = this.ToUnit(this.mouseDownPoint);
                this.moveHelper.HandleMouseDownForMove(mousePoint, null);
            }
        }

        private void MouseMoveMovePosition(MouseEventArgs e)
        {
            UnitPoint mousePoint = this.ToUnit(e.Location);
            if (this.moveHelper.HandleMouseMoveForMove(mousePoint))
            {
                Refresh();
            }
        }

    }
}
