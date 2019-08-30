using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool
{
    public class NodePointSingleDot : INodePoint
    {
        private SingleDot owner;
        private SingleDot clone;
        private UnitPoint p1;

        public NodePointSingleDot(SingleDot owner)
        {
            this.owner = owner;
            this.clone = this.owner.Clone() as SingleDot;
            this.p1 = this.owner.P1;
        }

        protected void SetPoint(UnitPoint unitPoint,SingleDot singleDot)
        {
            singleDot.P1 = singleDot.P1;
        }
        #region INodePoint
        public void Cancel()
        {
            
        }

        public void Finish()
        {
            this.owner.P1 = this.clone.P1;
            this.clone = null;
        }

        public IDrawObject GetClone()
        {
            return this.clone;
        }

        public IDrawObject GetOriginal()
        {
            return this.owner;
        }

        public void OnKeyDown(ICanvas canvas, KeyEventArgs e)
        {
            
        }

        public void Redo()
        {
            this.SetPoint(this.p1, this.owner);
        }

        public void SetPosition(UnitPoint postion)
        {
            this.SetPoint(postion, this.clone);
        }

        public void Undo()
        {
            this.SetPoint(this.p1, this.clone);
        }

        #endregion
    }
}
