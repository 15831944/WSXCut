using System.Collections.Generic;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandMirror:EditCommandBase
    {
        private List<IDrawObject> drawObjects = new List<IDrawObject>();
        private double A, B, C;

        public EditCommandMirror(List<IDrawObject> drawObjects, double A,double B,double C)
        {
            this.drawObjects = new List<IDrawObject>(drawObjects);
            this.A = A;
            this.B = B;
            this.C = C;
        }

        public override bool DoUndo(IModel dataModel)
        {
            dataModel.DoMirror(this.drawObjects, this.A, this.B, this.C);
            return true;
        }

        public override bool DoRedo(IModel dataModel)
        {
            dataModel.DoMirror(this.drawObjects, this.A, this.B, this.C);
            return true;
        }
    }
}
