using System.Collections.Generic;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandOverCutting:EditCommandBase
    {
        private List<IDrawObject> drawObjects;
        private float pos,lastPos;
        private bool roundCut,lastRoundCut;

        public EditCommandOverCutting(List<IDrawObject> drawObjects,float pos,bool roundCut)
        {
            this.pos = pos;
            this.roundCut = roundCut;
            this.drawObjects = drawObjects;
        }

        public override bool DoUndo(IModel dataModel)
        {
            this.Swap();
            dataModel.SetOverCutting(this.drawObjects, this.pos,roundCut);
            return true;
        }

        public override bool DoRedo(IModel dataModel)
        {
            this.Swap();
            dataModel.SetOverCutting(this.drawObjects, this.pos,roundCut);
            return true;
        }

        private void Swap()
        {
            float temp = this.pos;
            this.pos = this.lastPos;
            this.lastPos = temp;
            bool tempRoundCut = this.roundCut;
            this.roundCut = this.lastRoundCut;
            this.lastRoundCut = tempRoundCut;
        }
    }
}
