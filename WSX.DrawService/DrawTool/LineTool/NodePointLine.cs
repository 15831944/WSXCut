using System.Windows.Forms;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool
{
    public class NodePointLine : INodePoint
    {
        public enum LinePoint { P1, P2 }
        private bool angleLocked = false;
        private Line owner;
        private Line clone;
        private UnitPoint originalPoint;
        private UnitPoint endPoint;
        private LinePoint pointId;

        public NodePointLine(Line owner,LinePoint id)
        {
            this.owner = owner;
            this.clone = owner.Clone() as Line;
            this.pointId = id;
            this.originalPoint = this.GetPoint(id);
        }

        protected UnitPoint GetPoint(LinePoint pointId)
        {
            if(pointId==LinePoint.P1)
            {
                return this.clone.P1;
            }
            if(pointId==LinePoint.P2)
            {
                return this.clone.P2;
            }
            return this.owner.P1;
        }

        protected UnitPoint OtherPoint(LinePoint currentPointId)
        {
            if(currentPointId==LinePoint.P1)
            {
                return this.GetPoint(LinePoint.P2);
            }
            return this.GetPoint(LinePoint.P1);
        }

        protected void SetPoint(LinePoint pointId,UnitPoint unitPoint,Line line)
        {
            UnitPoint offset = new UnitPoint(0,0);
            if(pointId==LinePoint.P1)
            {
                if (line.P1 == line.StartMovePoint)
                {
                    offset.X = line.P1.X - unitPoint.X;
                    offset.Y = line.P1.Y - unitPoint.Y;
                }
                line.P1 = unitPoint;
            }
            else if(pointId==LinePoint.P2)
            {
                if (line.P2 == line.StartMovePoint)
                {
                    offset.X = line.P2.X - unitPoint.X;
                    offset.Y = line.P2.Y - unitPoint.Y;
                }
                line.P2 = unitPoint;
            }
            line.StartMovePoint =new UnitPoint(line.StartMovePoint.X-offset.X,line.StartMovePoint.Y-offset.Y);
        }

        #region INodePoint
        public void Cancel()
        { }

        public void Finish()
        {
            this.endPoint = this.GetPoint(this.pointId);
            this.owner.P1 = this.clone.P1;
            this.owner.P2 = this.clone.P2;
            this.owner.StartMovePoint = this.clone.StartMovePoint;
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
            if(e.KeyCode==Keys.L)
            {
                this.angleLocked = !this.angleLocked;
                e.Handled = true;
            }
        }

        public void Redo()
        {
            this.SetPoint(this.pointId, this.endPoint, this.owner);
        }

        public void SetPosition(UnitPoint position)
        {
            if(Control.ModifierKeys==Keys.Control)
            {
                position = HitUtil.OrthoPointD(this.OtherPoint(this.pointId), position, 45);
            }
            if(this.angleLocked||Control.ModifierKeys==(Keys)(Keys.Control|Keys.Shift))
            {
                position = HitUtil.NearestPointOnLine(this.owner.P1, this.owner.P2, position, true);
            }
            this.SetPoint(this.pointId, position, this.clone);
        }

        public void Undo()
        {
            this.SetPoint(this.pointId, this.endPoint, this.owner);
        }

        #endregion
    }
}
