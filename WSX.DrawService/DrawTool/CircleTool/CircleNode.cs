using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool.CircleTool
{
    public class CircleNode : INodePoint
    {
        protected Circle owner;
        protected Circle clone;
        protected CircleNodeType circleNodeType;
        protected UnitPoint ownerCenter, cloneCenter;
        protected float ownerRadius, cloneRadius;

        public CircleNode(Circle owner,CircleNodeType circleNodeType)
        {
            this.owner = owner;
            this.clone = this.owner.Clone() as Circle;
            this.owner.IsSelected = false;
            this.clone.IsSelected = true;
            this.clone.IsCompleteDraw = false;
            this.ownerCenter = this.owner.Center;
            this.owner.Radius = this.owner.Radius;
            this.circleNodeType = circleNodeType;
        }

        #region INodePoint
        public void Cancel()
        {
            this.owner.IsSelected = false;
        }

        public void Finish()
        {
            this.cloneCenter = this.clone.Center;
            this.cloneRadius = this.clone.Radius;
            this.owner.Copy(this.clone);
            this.owner.Update();
            this.owner.IsSelected = true;
            this.owner.IsCompleteDraw = true;
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
            this.owner.Center = this.cloneCenter;
            this.owner.Radius = this.cloneRadius;
            this.owner.UpdateLeadwire();
        }

        public void SetPosition(UnitPoint unitPoint)
        {
            if(this.circleNodeType!=CircleNodeType.Center)
            {
                this.clone.Radius = (float)HitUtil.Distance(this.clone.Center, unitPoint);
            }
            else
            {
                UnitPoint offset = new UnitPoint(unitPoint.X - this.clone.Center.X, unitPoint.Y - this.clone.Center.Y);
                this.clone.Move(offset);
            }
        }

        public void Undo()
        {
            this.owner.Center = this.ownerCenter;
            this.owner.Radius = this.ownerRadius;
            this.owner.UpdateLeadwire();
        }
        #endregion
    }
}
