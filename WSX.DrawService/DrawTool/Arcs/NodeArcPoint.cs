using System;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool.Arcs
{
    public class NodeArcPoint : INodePoint
    {
        protected ArcBase owner;
        protected ArcBase clone;
        protected ArcNodeType arcNodeType;
        protected UnitPoint[] ownerPoints = new UnitPoint[3];
        protected UnitPoint[] clonePoints = new UnitPoint[3];

        public NodeArcPoint(ArcBase owner,ArcNodeType arcNodeType)
        {
            this.owner = owner;
            this.clone = this.owner.Clone() as ArcBase;
            this.owner.IsSelected = false;
            this.clone.IsSelected = true;
            this.clone.IsCompleteDraw = false;
            this.clone.IsEditMode = true;
            this.ownerPoints[0] = this.owner.startPoint;
            this.ownerPoints[1] = this.owner.midPoint;
            this.ownerPoints[2] = this.owner.endPoint;
            this.arcNodeType = arcNodeType;
        }
        #region INodePoint
        public void Cancel()
        {
            this.owner.IsSelected = false;
        }

        public void Finish()
        {
            this.clonePoints[0] = this.clone.startPoint;
            this.clonePoints[1] = this.clone.midPoint;
            this.clonePoints[2] = this.clone.endPoint;
            this.clone.IsCompleteDraw = true;
            this.owner.Copy(this.clone);
            this.owner.Update();
            this.owner.IsSelected = true;
            this.clone.IsEditMode = false;
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

        public virtual void Redo()
        {
            this.owner.startPoint = this.clonePoints[0];
            this.owner.midPoint = this.clonePoints[1];
            this.owner.endPoint = this.clonePoints[2];
            this.owner.UpdateArcFrom3Points();
            this.owner.UpdateLeadwire();
        }

        public virtual void SetPosition(UnitPoint unitPoint)
        {
            if (this.arcNodeType == ArcNodeType.StartPoint)
            {
                this.clone.startPoint = unitPoint;
            }
            else if (this.arcNodeType == ArcNodeType.MidPoint)
            {
                this.clone.midPoint = unitPoint;
            }
            else if (this.arcNodeType == ArcNodeType.EndPoint)
            {
                this.clone.endPoint = unitPoint;
            }
            else if(this.arcNodeType==ArcNodeType.Center)
            {
                UnitPoint offset = new UnitPoint(unitPoint.X - this.clone.Center.X, unitPoint.Y - this.clone.Center.Y);
                this.clone.Move(offset);
            }
            this.clone.UpdateArcFrom3Points();
        }

        public virtual void Undo()
        {
            this.owner.startPoint = this.ownerPoints[0];
            this.owner.midPoint = this.ownerPoints[1];
            this.owner.endPoint = this.ownerPoints[2];
            this.owner.UpdateArcFrom3Points();
            this.owner.UpdateLeadwire();
        }
        #endregion
    }
}
