using System.Collections.Generic;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandSetLeadwire:EditCommandBase
    {
        private List<IDrawObject> drawObjects;
        private List<LineInOutParamsModel> leadwireModels=new List<LineInOutParamsModel>();
        private List<LineInOutParamsModel> leadwireModelOlds;

        public EditCommandSetLeadwire(List<IDrawObject> drawObjects, List<LineInOutParamsModel> leadwireModels, List<LineInOutParamsModel> leadwireModelOlds)
        {
            this.drawObjects = drawObjects;
            for (int i = 0; i < leadwireModels.Count; i++)
            {
                this.leadwireModels.Add(CopyUtil.DeepCopy(leadwireModels[i]));
            }
             this.leadwireModelOlds = leadwireModelOlds;
        }

        public override bool DoUndo(IModel dataModel)
        {
            List<LineInOutParamsModel> temp = this.leadwireModels;
            this.leadwireModels = this.leadwireModelOlds;
            this.leadwireModelOlds = temp;
            dataModel.DoSetLeadwire(this.drawObjects, this.leadwireModels);          
            return true;
        }

        public override bool DoRedo(IModel dataModel)
        {
            List<LineInOutParamsModel> temp = this.leadwireModels;
            this.leadwireModels = this.leadwireModelOlds;
            this.leadwireModelOlds = temp;
            dataModel.DoSetLeadwire(this.drawObjects, this.leadwireModels);
            return true;
        }
    }
}
