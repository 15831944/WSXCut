using System.Collections.Generic;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandRemove:EditCommandBase
    {
        private List<IDrawObject> removeDrawObjects = new List<IDrawObject>();

        public void AddLayerObjects(ICanvasLayer canvasLayer,List<IDrawObject> drawObjects)
        {
            this.removeDrawObjects.AddRange(drawObjects);
        }

        public override bool DoUndo(IModel dataModel)
        {
            foreach (IDrawObject drawObject in removeDrawObjects)
            {
                //dataModel.AddObject(drawObject);
                dataModel.AddObjectOnRedoUndo(drawObject);
            }
            return true;
        }

        public override bool DoRedo(IModel dataMode)
        {
            dataMode.DeleteObjects(this.removeDrawObjects);
            return true;
        }
    }
}
