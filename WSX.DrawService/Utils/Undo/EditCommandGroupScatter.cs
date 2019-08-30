using System.Collections.Generic;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public  class EditCommandGroupScatter:EditCommandBase
    {
        private List<List<IDrawObject>> drawObjects = new List<List<IDrawObject>>();

        public EditCommandGroupScatter(List<List<IDrawObject>> drawObjects)
        {
            this.drawObjects = drawObjects;
        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.DoGroup(drawObjects,true);
            return true;
        }
        public override bool DoRedo(IModel dataModel)
        {
            dataModel.DoGroupScatter(drawObjects);
            return true;
        }
    }
}
