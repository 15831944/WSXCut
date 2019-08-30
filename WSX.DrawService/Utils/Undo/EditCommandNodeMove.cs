using System.Collections.Generic;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandNodeMove:EditCommandBase
    {
        private List<INodePoint> nodePoints = new List<INodePoint>();
        public EditCommandNodeMove(IEnumerable<INodePoint> nodePoints)
        {
            this.nodePoints = new List<INodePoint>(nodePoints);
        }

        public override bool DoUndo(IModel dataModel)
        {
            foreach (INodePoint nodePoint in this.nodePoints)
            {
                nodePoint.Undo();
            }
            return true;
        }

        public override bool DoRedo(IModel dataMode)
        {
            foreach (INodePoint nodePoint in this.nodePoints)
            {
                nodePoint.Redo();
            }
            return true;
        }
    }
}
