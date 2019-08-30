using System.Collections.Generic;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public  class EditCommandGroup:EditCommandBase
    {
        private List<List<IDrawObject>> drawObjects = new List<List<IDrawObject>>();

        public EditCommandGroup(List<List<IDrawObject>> drawObjects)
        {
            this.drawObjects = drawObjects;
        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.DoGroupScatter(this.drawObjects);
            return true;
        }
        public override bool DoRedo(IModel dataModel)
        {
            dataModel.DoGroup(this.drawObjects);
            return true;
        }
    }
}
