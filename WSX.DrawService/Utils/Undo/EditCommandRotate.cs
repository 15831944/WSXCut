using System.Collections.Generic;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandRotate : EditCommandBase
    {
        private List<IDrawObject> drawObjects;
        private UnitPoint rotateCenter;
        private double rotateAngle;
        private bool isClockwise;

        public EditCommandRotate(List<IDrawObject> drawObjects, UnitPoint rotateCenter, double rotateAngle, bool isClockwise)
        {
            this.drawObjects = new List<IDrawObject>(drawObjects);
            this.rotateCenter = rotateCenter;
            this.rotateAngle = rotateAngle;
            this.isClockwise = isClockwise;
        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.DoRotate(this.drawObjects, this.rotateCenter, this.rotateAngle, !this.isClockwise);
            return true;
        }

        public override bool DoRedo(IModel dataModel)
        {
            dataModel.DoRotate(this.drawObjects, this.rotateCenter, this.rotateAngle, this.isClockwise);
            return true;
        }
    }
}
