using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils.Undo
{
    //class 
    public class EditCommandArcFlyingCut : EditCommandBase
    {
        private List<IDrawObject> drawObjectsOld = new List<IDrawObject>();
        private List<IDrawObject> drawObjectsNew = new List<IDrawObject>();

        public EditCommandArcFlyingCut(List<IDrawObject> drawObjectsOld, List<IDrawObject> drawObjectsNew)
        {
            this.drawObjectsOld = drawObjectsOld;
            this.drawObjectsNew = drawObjectsNew;

        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.DoArcFlyingCut(drawObjectsOld, null, drawObjectsNew, null);
            return true;
        }
        public override bool DoRedo(IModel dataModel)
        {
            dataModel.DoArcFlyingCut(drawObjectsNew, null, null, drawObjectsOld);
            return true;
        }
    }
}

