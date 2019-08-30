using System.Collections.Generic;
using System.Linq;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public  class EditCommandSort:EditCommandBase
    {
        private Dictionary<int,int> indexs = new Dictionary<int, int>();

        public EditCommandSort(Dictionary<int, int> indexs)
        {
            this.indexs = indexs;
        }

        public override bool DoUndo(IModel dataModel)
        {
            this.indexs = indexs.OrderBy(r => r.Value).ToDictionary(r => r.Value, r => r.Key);
            dataModel.DoSort(this.indexs);
            return true;
        }
        public override bool DoRedo(IModel dataModel)
        {
            this.indexs = indexs.OrderBy(r => r.Value).ToDictionary(r => r.Value, r => r.Key);
            dataModel.DoSort(this.indexs);
            return true;
        }
       
    }
}
