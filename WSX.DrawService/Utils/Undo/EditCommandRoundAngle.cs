using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.Utilities;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils.Undo
{
    public class EditCommandRoundAngle:EditCommandBase
    {
        private List<IDrawObject> curDrawObjects;
        private List<IDrawObject> oldDrawObjects;

        public EditCommandRoundAngle(List<IDrawObject> curDrawObjects, List<IDrawObject> oldDrawObjects)
        {
            this.oldDrawObjects = oldDrawObjects;
            this.curDrawObjects = curDrawObjects;
        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.DoRoundAngle(this.oldDrawObjects, this.curDrawObjects);
            return true;
        }

        public override bool DoRedo(IModel dataModel)
        {
            dataModel.DoRoundAngle(this.curDrawObjects, this.oldDrawObjects);
            return true;
        }
    }
}
