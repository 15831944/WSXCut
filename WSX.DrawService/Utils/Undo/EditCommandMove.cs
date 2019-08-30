using System.Collections.Generic;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandMove:EditCommandBase
    {
        private List<IDrawObject> drawObjects = new List<IDrawObject>();
        private UnitPoint offset;
        
        public EditCommandMove(UnitPoint offset,IEnumerable<IDrawObject> drawObjects)
        {
            this.drawObjects = new List<IDrawObject>(drawObjects);
            this.offset = offset;
        }

        public override bool DoUndo(IModel dataModel)
        {
            if (this.drawObjects != null)
            {
                dataModel.MoveObjects(new UnitPoint(-this.offset.X, -offset.Y), this.drawObjects);
            }
            return true;
        }

        public override bool DoRedo(IModel dataModel)
        {
            if (this.drawObjects != null)
            {
                dataModel.MoveObjects(this.offset, this.drawObjects);
            }
            return true;
        }
    }
}
