using System;
using System.Windows.Forms;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool
{
    public class NodeMultiLine : INodePoint
    {
        private MultiLine owner;
        private MultiLine clone;
        private UnitPoint[] originalPoints;
        private UnitPoint[] endPoints;
        private int nodeIndex;

        public NodeMultiLine(MultiLine owner,int nodeIndex)
        {
            this.owner = owner;
            this.clone = this.owner.Clone() as MultiLine;
            this.clone.IsSelected = true;
            this.originalPoints = new UnitPoint[owner.MultiLinePoints.Count];
            this.endPoints = new UnitPoint[owner.MultiLinePoints.Count];
            for (int i = 0; i < owner.MultiLinePoints.Count; i++)
            {
                this.originalPoints[i] = owner.MultiLinePoints[i];
                this.endPoints[i] = owner.MultiLinePoints[i];
            }
            this.clone.currentNodeIndex = nodeIndex;
            this.nodeIndex = nodeIndex;
        }

        #region INodePoint
        public void Cancel()
        {
            this.owner.IsSelected = true;
        }

        public void Finish()
        {
            int points = this.owner.MultiLinePoints.Count;
            UnitPoint offset = new UnitPoint(this.clone.MultiLinePoints[0].X - this.owner.MultiLinePoints[0].X, this.clone.MultiLinePoints[0].Y - this.owner.MultiLinePoints[0].Y);
            for (int i = 0; i < this.owner.MultiLinePoints.Count; i++)
            {
                this.endPoints[i] = this.clone.MultiLinePoints[i];
            }
            this.owner.Copy(this.clone);
            this.owner.IsSelected = true;
            this.owner.StartMovePoint = new UnitPoint(this.owner.StartMovePoint.X + offset.X, this.owner.StartMovePoint.Y + offset.Y);
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
            for (int i = 0; i < this.owner.MultiLinePoints.Count; i++)
            {
                this.owner.MultiLinePoints[i] = this.endPoints[i];
            }
        }

        public void SetPosition(UnitPoint postion)
        {
            this.SetPoint(this.clone, postion);
        }

        public void Undo()
        {
            for (int i = 0; i < this.owner.MultiLinePoints.Count; i++)
            {
                this.owner.MultiLinePoints[i] = this.originalPoints[i];
            }
        }

        #endregion

        private void SetPoint(MultiLine multiLine, UnitPoint unitPoint)
        {
            multiLine.MultiLinePoints[nodeIndex] = unitPoint;
        }
    }
}
