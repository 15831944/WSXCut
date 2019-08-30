using System;
using System.Collections.Generic;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandScale : EditCommandBase
    {
        private List<IDrawObject> drawObjects=new List<IDrawObject>();
        private double centerX;
        private double centerY;
        private double coffX;
        private double coffY;

        public EditCommandScale(List<IDrawObject> drawObjects, double centerX, double centerY, double coffX, double coffY)
        {
            this.drawObjects = new List<IDrawObject>(drawObjects);
            this.centerX = centerX;
            this.centerY = centerY;
            this.coffX = coffX;
            this.coffY = coffY;
        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.DoSizeChange(this.drawObjects, centerX, centerY, 1 / coffX, 1 / coffY);
            return true;
        }

        public override bool DoRedo(IModel dataModel)
        {
            dataModel.DoSizeChange(this.drawObjects, centerX, centerY, coffX, coffY);
            return true;
        }
    }
}
