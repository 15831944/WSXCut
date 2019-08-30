using System;
using System.Windows.Forms;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool
{
    public class NodeHexagon : INodePoint
    {
        private Hexagon owner;
        private Hexagon clone;
        private UnitPoint[] originalPoints;
        private UnitPoint[] endPoints;
        private int nodeIndex;

        public NodeHexagon(Hexagon owner,int nodeIndex)
        {
            this.owner = owner;
            this.clone = this.owner.Clone() as Hexagon;
            this.clone.IsSelected = true;
            this.originalPoints = new UnitPoint[owner.SideCount];
            this.endPoints = new UnitPoint[owner.SideCount];
            for (int i = 0; i < owner.SideCount; i++)
            {
                this.originalPoints[i] = owner.HexagonPoints[i];
                this.endPoints[i] = owner.HexagonPoints[i];
            }
            this.nodeIndex = nodeIndex;
        }

        #region INodePoint
        public void Cancel()
        {
            this.owner.IsSelected = true;
        }

        public void Finish()
        {
            this.SetNewStartMovePoint();
            this.clone.OverCutting();
            for (int i = 0; i < this.owner.SideCount; i++)
            {
                this.endPoints[i] = this.clone.HexagonPoints[i];
            }
            this.owner.Copy(this.clone);           
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
            for (int i = 0; i < this.owner.SideCount; i++)
            {
                this.owner.HexagonPoints[i] = this.endPoints[i];
            }
        }

        public void SetPosition(UnitPoint postion)
        {
            this.SetPoint(this.clone, postion);
        }

        public void Undo()
        {
            for (int i = 0; i < this.owner.SideCount; i++)
            {
                this.owner.HexagonPoints[i] = this.originalPoints[i];
            }
        }

        #endregion

        private void SetPoint(Hexagon hexagon,UnitPoint unitPoint)
        {
            #region 变化单个节点
            //hexagon.HexagonPoints[nodeIndex] = unitPoint;

            //double radius0 = hexagon.GetRadius(hexagon.HexagonPoints[0]);
            //double radius = hexagon.GetRadius(unitPoint);
            //double k = radius / radius0;
            //for (int i = 0; i < hexagon.HexagonPoints.Length; i++)
            //{
            //    hexagon.HexagonPoints[i].X = hexagon.HexagonPoints[i].X + (k - 1) * (hexagon.HexagonPoints[i].X - hexagon.CenterPoint.X);
            //    hexagon.HexagonPoints[i].Y = hexagon.HexagonPoints[i].Y + (k - 1) * (hexagon.HexagonPoints[i].Y - hexagon.CenterPoint.Y);
            //}
            #endregion

            double startAngle = HitUtil.LineAngleR(hexagon.CenterPoint, unitPoint, 0);
            double radius = hexagon.GetRadius(unitPoint);
            hexagon.HexagonPoints[0] = unitPoint;
            for (int i = 1; i < hexagon.SideCount; i++)
            {
                UnitPoint vertexPoint = HitUtil.PointOnCircle(hexagon.CenterPoint, radius, startAngle + hexagon.stepAngle * Math.PI / 180 * i);
                hexagon.HexagonPoints[i] = vertexPoint;
            }

            //double currentNodeAngle = HitUtil.LineAngleR(hexagon.CenterPoint, unitPoint, 0);
            //double startAngle = currentNodeAngle - (nodeIndex * hexagon.stepAngle);
            //double radius = hexagon.GetRadius(unitPoint);
            //for (int i = 0; i < hexagon.SideCount; i++)
            //{
            //    UnitPoint vertexPoint = HitUtil.PointOnCircle(hexagon.CenterPoint, radius, startAngle + hexagon.stepAngle * Math.PI / 180 * i);
            //    hexagon.HexagonPoints[i] = vertexPoint;
            //}
        }

        private void SetNewStartMovePoint()
        {
            double cloneStartAngle= HitUtil.LineAngleR(this.clone.CenterPoint, this.clone.HexagonPoints[0], 0);
            double offsetAngle = cloneStartAngle - HitUtil.LineAngleR(this.owner.CenterPoint, this.owner.HexagonPoints[0], 0);
            double cloneStartMoveAngle = HitUtil.LineAngleR(this.owner.CenterPoint, this.owner.StartMovePoint, 0) + offsetAngle;
            if (cloneStartMoveAngle > 2 * Math.PI)
            {
                cloneStartMoveAngle -= 2 * Math.PI;
            }
            else if (cloneStartMoveAngle < 0)
            {
                cloneStartMoveAngle += 2 * Math.PI;
            }
            this.clone.StartMovePoint = HitUtil.PointOnCircle(this.clone.CenterPoint, this.clone.GetRadius(this.clone.HexagonPoints[0]), cloneStartMoveAngle);
            if (this.owner.StartMovePointIndex != this.owner.HexagonPoints.Length - 1)
            {
                this.clone.StartMovePoint = HitUtil.FindApparentIntersectPoint(this.clone.CenterPoint, this.clone.StartMovePoint, this.clone.HexagonPoints[this.owner.StartMovePointIndex], this.clone.HexagonPoints[this.owner.StartMovePointIndex + 1]);
            }
            else
            {
                this.clone.StartMovePoint = HitUtil.FindApparentIntersectPoint(this.clone.CenterPoint, this.clone.StartMovePoint, this.clone.HexagonPoints[this.owner.StartMovePointIndex], this.clone.HexagonPoints[0]);
            }
        }
    }
}
