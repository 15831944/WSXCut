using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils.Undo
{
    public  class EditCommandCommonSide:EditCommandBase
    {
        private List<IDrawObject> drawObjectsold = new List<IDrawObject>();
        private List<IDrawObject> drawObjectsnew = new List<IDrawObject>();
       

        public EditCommandCommonSide(List<IDrawObject> drawObjectsold, List<IDrawObject> drawObjectsnew)
        {
            this.drawObjectsold = drawObjectsold;
            this.drawObjectsnew = drawObjectsnew;
          
        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.DoCommonSide(drawObjectsold,null,drawObjectsnew[0].GroupParam.GroupSN[1] ,null);
            return true;
        }
        public override bool DoRedo(IModel dataModel)
        {
            dataModel.DoCommonSide(drawObjectsnew,null,0,drawObjectsold);
            return true;
        }
    }
}
