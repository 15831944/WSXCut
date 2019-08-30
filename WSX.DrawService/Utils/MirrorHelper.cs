using System;
using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    /*定理：已知点M(x0, y0)和直线 l：Ax+By+C=0（A≠0，B≠0）,点M关于直线l对称的对称点M′的坐标(x，y)，则
     x = x0 - 2Af(x0, y0)/(A2+B2),
     y=y0-2Bf(x0, y0)/(A2+B2).
    其中f(x, y)=Ax+By+C

        已知直线上两点求直线的一般式方程
       已知直线上的两点P1(X1,Y1) P2(X2,Y2)， P1 P2两点不重合。则直线的一般式方程AX+BY+C=0中，A B C分别等于：
        A = Y2 - Y1
        B = X1 - X2
        C = X2*Y1 - X1*Y2
    */
    public class MirrorHelper
    {
        private enum Status { BasePoint, EndPoint }
        private Status CurrentStatus;
        private List<IDrawObject> originals = new List<IDrawObject>();
        private UCCanvas uCCanvas;
        private UnitPoint FirstPoint, SecondPoint;
        public List<IDrawObject> Copies { get; } = new List<IDrawObject>();
        private double A, B, C;

        public MirrorHelper(UCCanvas uCCanvas)
        {
            this.uCCanvas = uCCanvas;
        }

        public void HandleMouseMoveForMirror(UnitPoint mousePoint)
        {
            if (this.CurrentStatus == Status.EndPoint)
            {
                this.SecondPoint = mousePoint;
                if (this.CalLineCoff())
                {
                    this.Copies.Clear();
                    foreach (IDrawObject obj in this.originals)
                    {
                        IDrawObject temp = obj.Clone();
                        ((IDrawTranslation)temp).Mirroring(this.A, this.B, this.C);
                        this.Copies.Add(temp);
                    }
                }
                this.uCCanvas.DoInvalidate(true);
            }
        }

        private bool CalLineCoff()
        {
            if (this.FirstPoint.X == this.SecondPoint.X && this.FirstPoint.Y == this.SecondPoint.Y)
            {
                return false;
            }
            this.A = this.SecondPoint.Y - this.FirstPoint.Y;
            this.B = this.FirstPoint.X - this.SecondPoint.X;
            this.C = this.SecondPoint.X * this.FirstPoint.Y - this.FirstPoint.X * this.SecondPoint.Y;
            return true;
        }

        public void HandleCancelMirror()
        {
            this.CurrentStatus = Status.BasePoint;
            this.originals.Clear();
            this.Copies.Clear();
            this.uCCanvas.DoInvalidate(true);
        }

        public bool HandleMouseDownForMirror(UnitPoint mousePoint)
        {
            bool isComplete = false;
            if (this.CurrentStatus == Status.BasePoint)
            {
                this.CurrentStatus = Status.EndPoint;
                this.FirstPoint = mousePoint;
                foreach (IDrawObject obj in this.uCCanvas.Model.DrawingLayer.SelectedObjects)
                {
                    this.originals.Add(obj);
                    this.Copies.Add(obj.Clone());
                }
            }
            else if (this.CurrentStatus == Status.EndPoint)
            {
                this.CurrentStatus = Status.BasePoint;
                this.SecondPoint = mousePoint;
                this.CalLineCoff();
                this.uCCanvas.Model.DoMirror(this.originals, this.A, this.B, this.C);
                this.originals.Clear();
                this.Copies.Clear();
                isComplete = true;
            }
            this.uCCanvas.DoInvalidate(true);
            return isComplete;
        }

        public void DrawObjects(ICanvas canvas, RectangleF rectangleF)
        {
            if (this.CurrentStatus == Status.EndPoint)
            {
                canvas.DrawLine(canvas, new Pen(Color.White, 1), this.FirstPoint, this.SecondPoint);
                if (this.Copies.Count > 0)
                {
                    foreach (IDrawObject obj in this.Copies)
                    {
                        obj.Draw(canvas, rectangleF);
                    }
                }
            }
        }
    }
}
