using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils.Undo
{
    public class EditCommandLineFlyingCut : EditCommandBase
    {
        private List<IDrawObject> drawObjectsOld = new List<IDrawObject>();
        private List<IDrawObject> drawObjectsNew = new List<IDrawObject>();

        public EditCommandLineFlyingCut(List<IDrawObject> drawObjectsOld, List<IDrawObject> drawObjectsNew)
        {
            this.drawObjectsOld = drawObjectsOld;
            this.drawObjectsNew = drawObjectsNew;

        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.DoLineFlyingCut(drawObjectsOld, null, drawObjectsNew[0].GroupParam.GroupSN[1], null);
            return true;
        }
        public override bool DoRedo(IModel dataModel)
        {
            dataModel.DoLineFlyingCut(drawObjectsNew, null, 0, drawObjectsOld);
            return true;
        }
    }
}
