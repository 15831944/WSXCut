using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandBase
    {
        public virtual bool DoUndo(IModel dataModel)
        {
            return false;
        }

        public virtual bool DoRedo(IModel dataModel)
        {
            return false;
        }
    }
}
