using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.Utilities;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils.Undo
{
    public class EditCommandMicroConnect: EditCommandBase
    {
        private List<IDrawObject> drawObjects;
        private List<List<MicroConnectModel>> oldParams;
        private List<List<MicroConnectModel>> curParams;
        public EditCommandMicroConnect(List<IDrawObject> drawObjects, List<List<MicroConnectModel>> oldParams)
        {
            this.drawObjects = drawObjects;
            this.curParams = CopyUtil.DeepCopy(drawObjects.Select(e => e.MicroConnParams).ToList());
            this.oldParams = CopyUtil.DeepCopy(oldParams);
        }

        public override bool DoRedo(IModel dataModel)
        {
            var temp = CopyUtil.DeepCopy(this.curParams);
            this.curParams = CopyUtil.DeepCopy(this.oldParams);
            this.oldParams = temp;
            dataModel.DoMicroConnect(this.drawObjects, this.curParams, true);
            return true;
        }
        public override bool DoUndo(IModel dataModel)
        {
            var temp = CopyUtil.DeepCopy(this.curParams);
            this.curParams = CopyUtil.DeepCopy(this.oldParams);
            this.oldParams = temp;
            dataModel.DoMicroConnect(this.drawObjects, this.curParams, true);
            return true;
        }
    }
}
