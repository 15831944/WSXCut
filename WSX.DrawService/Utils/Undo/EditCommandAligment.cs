using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandAligment:EditCommandBase
    {
        private List<IDrawObject> drawObjects = new List<IDrawObject>();
        private List<UnitPoint> offsets;

        public EditCommandAligment(List<UnitPoint> offsets, IEnumerable<IDrawObject> drawObjects)
        {
            this.drawObjects = new List<IDrawObject>(drawObjects);
            this.offsets = offsets;
        }

        public override bool DoUndo(IModel dataModel)
        {
            if (this.drawObjects != null)
            {
                for (int i = 0; i < this.offsets.Count; i++)
                {
                    this.offsets[i] = new UnitPoint(-this.offsets[i].X, -this.offsets[i].Y);
                }
                dataModel.DoAligment(this.offsets, this.drawObjects);
            }
            return true;
        }

        public override bool DoRedo(IModel dataModel)
        {
            if (this.drawObjects != null)
            {
                for (int i = 0; i < this.offsets.Count; i++)
                {
                    this.offsets[i] = new UnitPoint(-this.offsets[i].X, -this.offsets[i].Y);
                }
                dataModel.DoAligment(this.offsets, this.drawObjects);
            }
            return true;
        }
    }
}
