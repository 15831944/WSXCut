using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandReverseDirection:EditCommandBase
    {
        private List<IDrawObject> drawObjects;

        public EditCommandReverseDirection(List<IDrawObject> drawObjects)
        {
            this.drawObjects = drawObjects;
        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.ReverseDirection(this.drawObjects);
            return true;
        }

        public override bool DoRedo(IModel dataModel)
        {
            dataModel.ReverseDirection(this.drawObjects);
            return true;
        }
    }
}
