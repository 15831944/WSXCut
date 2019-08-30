using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.Utilities;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils.Undo
{
    public class EditCommandCompensation : EditCommandBase
    {
        private List<IDrawObject> drawObjects;
        private List<CompensationModel> oldParams;
        private List<CompensationModel> curParams;
        public EditCommandCompensation(List<IDrawObject> drawObjects, List<CompensationModel> oldParams)
        {
            this.drawObjects = drawObjects;
            this.curParams = CopyUtil.DeepCopy(drawObjects.Select(e => e.CompensationParam).ToList());
            this.oldParams = CopyUtil.DeepCopy(oldParams);
        }

        public override bool DoRedo(IModel dataModel)
        {
            var temp = CopyUtil.DeepCopy(this.curParams);
            this.curParams = CopyUtil.DeepCopy(this.oldParams);
            this.oldParams = temp;
            dataModel.DoCompensation(this.drawObjects, this.curParams);
            return true;
        }
        public override bool DoUndo(IModel dataModel)
        {
            var temp = CopyUtil.DeepCopy(this.curParams);
            this.curParams = CopyUtil.DeepCopy(this.oldParams);
            this.oldParams = temp;
            dataModel.DoCompensation(this.drawObjects, this.curParams);
            return true;
        }
    }
}
