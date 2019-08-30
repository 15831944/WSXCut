using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils.Undo
{
    public class EditCommandBridge : EditCommandBase
    {
        private List<IDrawObject> newObjects = new List<IDrawObject>();
        private List<IDrawObject> oldObjects = new List<IDrawObject>();
        public EditCommandBridge(List<IDrawObject> newObjects, List<IDrawObject> oldObjects)
        {
            this.newObjects = newObjects;
            this.oldObjects = oldObjects;
        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.DoBridge(oldObjects, newObjects,false);
            return true;
        }
        public override bool DoRedo(IModel dataModel)
        {
            dataModel.DoBridge(newObjects, oldObjects,false);
            return true;
        }
    }
}
