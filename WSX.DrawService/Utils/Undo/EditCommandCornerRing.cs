using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel.RingCut;
using WSX.CommomModel.Utilities;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils.Undo
{
    public class EditCommandCornerRing : EditCommandBase
    {
        private List<IDrawObject> drawObjects;
        private List<CornerRingModel> oldParams;
        private List<CornerRingModel> curParams;
        public EditCommandCornerRing(List<IDrawObject> drawObjects, List<CornerRingModel> oldParams)
        {
            this.drawObjects = drawObjects;
            this.curParams = CopyUtil.DeepCopy(drawObjects.Select(e => e.CornerRingParam).ToList());
            this.oldParams = CopyUtil.DeepCopy(oldParams);
        }

        public override bool DoRedo(IModel dataModel)
        {
            var temp = CopyUtil.DeepCopy(this.curParams);
            this.curParams = CopyUtil.DeepCopy(this.oldParams);
            this.oldParams = temp;
            dataModel.DoCornerRing(this.drawObjects, this.curParams);
            return true;
        }
        public override bool DoUndo(IModel dataModel)
        {
            var temp = CopyUtil.DeepCopy(this.curParams);
            this.curParams = CopyUtil.DeepCopy(this.oldParams);
            this.oldParams = temp;
            dataModel.DoCornerRing(this.drawObjects, this.curParams);
            return true;
        }
    }
}
