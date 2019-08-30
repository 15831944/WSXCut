using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawModel;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool.MultiSegmentLine
{
    public class NodeMultisegment : INodePoint
    {
        protected MultiSegmentLineBase owner,clone;
        protected List<UnitPointBulge> ownerPoints;
        protected List<UnitPointBulge> clonePoints;
        protected MultiSegementNodeType nodeType;
        protected int nodeIndex;

        public NodeMultisegment(MultiSegmentLineBase owner,MultiSegementNodeType nodeType,int nodeIndex)
        {
            this.owner = owner;
            this.clone = this.owner.Clone() as MultiSegmentLineBase;
            this.nodeType = nodeType;
            this.nodeIndex = nodeIndex;
            this.ownerPoints = new List<UnitPointBulge>();
            for (int i = 0; i < this.owner.PointCount; i++)
            {
                this.ownerPoints.Add(this.owner.Points[i]);
            }
            this.clonePoints = new List<UnitPointBulge>();
            this.clone.IsCompleteDraw = false;
            this.clone.IsEditMode = true;
            this.owner.IsSelected = false;
            this.clone.IsSelected = true;
        }

        #region INodePoint
        public void Cancel()
        {
            this.owner.IsSelected = false;
        }

        public void Finish()
        {
            foreach (UnitPointBulge point in this.clone.Points)
            {
                this.clonePoints.Add(point);
            }
            this.clone.IsCompleteDraw = true;
            this.clone.IsEditMode = false;
            this.owner.Copy(this.clone);
            this.owner.Update();
            this.owner.IsSelected = true;
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
            this.owner.Points.Clear();
            foreach (UnitPointBulge point in this.clonePoints)
            {
                this.owner.Points.Add(point);
            }
            this.owner.UpdateLeadwire();
        }

        public void SetPosition(UnitPoint postion)
        {
            if(this.nodeType==MultiSegementNodeType.CommonNode)
            {
                this.clone.Points[this.nodeIndex] = new UnitPointBulge(postion, this.clone.Points[this.nodeIndex].Bulge);
                //if (this.owner.IsCloseFigure)
                //{
                //    this.clone.Points[this.clone.Points.Count - 1] = this.clone.Points[0];
                //}
            }
            else
            {
                int index = this.nodeIndex+1;
                if (this.nodeIndex + 1 > this.owner.PointCount - 1) index = 0;
                ArcModelMini arcModel = HitUtil.GetArcParametersFromThreePoints(this.clone.Points[this.nodeIndex].Point, postion, this.clone.Points[index].Point);
                float angle=Math.Abs(arcModel.SweepAngle)/4;
                double bulge = Math.Tan(angle * Math.PI / 180);
                bulge = arcModel.Clockwise ? -bulge : bulge;
                this.clone.Points[this.nodeIndex] = new UnitPointBulge(this.clone.Points[this.nodeIndex].Point, bulge);
            }          
        }



        public void Undo()
        {
            this.owner.Points.Clear();
            foreach (UnitPointBulge point in this.ownerPoints)
            {
                this.owner.Points.Add(point);
            }
            this.owner.UpdateLeadwire();
        }
        #endregion
    }
}
