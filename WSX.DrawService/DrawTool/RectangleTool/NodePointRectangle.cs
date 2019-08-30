using System.Windows.Forms;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool
{
    public class NodePointRectangle : INodePoint
    {
        protected SingleRectangle owner, clone;
        protected UnitPoint[] originalPoints=new UnitPoint[2];
        protected UnitPoint[] clonePoints = new UnitPoint[2];

        public NodePointRectangle(SingleRectangle owner, SingleRectangle.NodePosition nodePosition)
        {
            this.owner = owner;
            this.clone = this.owner.Clone() as SingleRectangle;
            this.owner.IsSelected = false;
            this.clone.IsSelected = true;
        }

        #region INodePoint

        public void Cancel()
        {
            this.owner.IsSelected = true;
        }

        public void Finish()
        {
            this.UpdateStartMovePoint();
            this.clone.OverCutting();
            this.owner.Copy(this.clone);
            this.owner.IsSelected = true;
            this.clone = null;
        }

        private void UpdateStartMovePoint()
        {
            double kx, ky, lengthOwner, lengthClone;//系数
            switch (this.owner.StartMovePosition)
            {
                case SingleRectangle.NodePosition.LeftTop:
                    {
                        lengthOwner = this.owner.RightTopPoint.X - this.owner.LeftTopPoint.X;
                        lengthClone = this.clone.RightTopPoint.X - this.clone.LeftTopPoint.X;
                        kx = (this.owner.StartMovePoint.X - this.owner.LeftTopPoint.X) / lengthOwner;
                        ky = this.clone.LeftTopPoint.Y - this.owner.LeftTopPoint.Y;
                        this.clone.StartMovePoint = new UnitPoint(this.clone.LeftTopPoint.X + kx * lengthClone, this.owner.StartMovePoint.Y + ky);
                        break;
                    }
                case SingleRectangle.NodePosition.RightTop:
                    {
                        lengthOwner = this.owner.RightTopPoint.Y - this.owner.RightBottomPoint.Y;
                        lengthClone = this.clone.RightTopPoint.Y - this.clone.RightBottomPoint.Y;
                        ky = (this.owner.StartMovePoint.Y - this.owner.RightBottomPoint.Y) / lengthOwner;
                        kx = this.clone.RightTopPoint.X - this.owner.RightTopPoint.X;
                        this.clone.StartMovePoint = new UnitPoint(this.owner.StartMovePoint.X + kx, this.clone.RightBottomPoint.Y + ky * lengthClone);
                        break;
                    }
                case SingleRectangle.NodePosition.RightBottom:
                    {
                        lengthOwner = this.owner.RightBottomPoint.X - this.owner.LeftBottomPoint.X;
                        lengthClone = this.clone.RightBottomPoint.X - this.clone.LeftBottomPoint.X;
                        kx = (this.owner.StartMovePoint.X - this.owner.LeftBottomPoint.X) / lengthOwner;
                        ky = this.clone.LeftBottomPoint.Y - this.owner.RightBottomPoint.Y;
                        this.clone.StartMovePoint = new UnitPoint(this.clone.LeftTopPoint.X + kx * lengthClone, this.owner.StartMovePoint.Y + ky);
                        break;
                    }
                case SingleRectangle.NodePosition.LeftBottom:
                    {
                        lengthOwner = this.owner.LeftTopPoint.Y - this.owner.LeftBottomPoint.Y;
                        lengthClone = this.clone.LeftTopPoint.Y - this.clone.LeftBottomPoint.Y;
                        ky = (this.owner.StartMovePoint.Y - this.owner.LeftBottomPoint.Y) / lengthOwner;
                        kx = this.clone.LeftTopPoint.X - this.owner.LeftTopPoint.X;
                        this.clone.StartMovePoint = new UnitPoint(this.owner.StartMovePoint.X + kx, this.clone.LeftBottomPoint.Y + ky * lengthClone);
                        break;
                    }
            }
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
            this.owner.FirstPoint = this.clonePoints[0];
            this.owner.SecondPoint = this.clonePoints[1];
        }

        public void SetPosition(UnitPoint postion)
        {
            this.clone.FirstPoint = postion;
            this.clone.UpdateSingleRectangleFromTwoPoints();           
        }

        public void Undo()
        {
            this.owner.FirstPoint = this.originalPoints[0];
            this.owner.SecondPoint = this.originalPoints[1];
        }

        #endregion
    }
}
