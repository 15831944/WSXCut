using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public  class EditCommandArray:EditCommandBase
    {
        private List<IDrawObject> delObjects = new List<IDrawObject>();
         private List<IDrawObject> addObjects = new List<IDrawObject>();

        public EditCommandArray(List<IDrawObject>delObjects,List<IDrawObject>addObjects)
        {
            this.delObjects = delObjects;
            this.addObjects = addObjects;
        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.ArrayObjects(addObjects,delObjects);
            return true;
        }
        public override bool DoRedo(IModel dataModel)
        {
           dataModel.ArrayObjects(delObjects,addObjects);
            return true;
        }
    }
}
