using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class RotateAnyHelper
    {
        private enum Status { BasePoint,EndPoint,OtherEndPoint}
        private Status CurrentStatus;
        private List<IDrawObject> originals = new List<IDrawObject>();
        private UCCanvas uCCanvas;
        private UnitPoint FirstPoint,SecondPoint,ThirdPoint;
        private double rotateAngle;
        private bool clockwise;
        private UnitPoint LastPoint { get; set; }

        public List<IDrawObject> Copies { get; } = new List<IDrawObject>();

        public RotateAnyHelper(UCCanvas uCCanvas)
        {
            this.uCCanvas = uCCanvas;
        }

        public bool IsEmpty
        {
            get
            {
                return this.Copies.Count == 0;
            }
        }

        public void HandleMouseMoveForRotate(UnitPoint mousePoint)
        {
            if (this.CurrentStatus == Status.EndPoint)
            {
                this.SecondPoint =this.LastPoint= mousePoint;
            }
            else if (this.CurrentStatus == Status.OtherEndPoint)
            {
                this.ThirdPoint = mousePoint;
                this.CalAngleAndDirection(this.LastPoint);
                foreach (IDrawObject obj in this.Copies)
                {
                    ((IDrawTranslation)obj).DoRotate(this.FirstPoint, this.rotateAngle, this.clockwise);
                }
            }
            this.uCCanvas.DoInvalidate(true);
        }

        private void CalAngleAndDirection(UnitPoint lastPoint)
        {
            double angle = HitUtil.LineAngleR(this.FirstPoint, lastPoint, 0);
            double angle2 = HitUtil.LineAngleR(this.FirstPoint, this.ThirdPoint, 0);
            this.LastPoint = this.ThirdPoint;
            double theta = angle2 - angle;
            if (theta > Math.PI) theta -= Math.PI * 2;
            else if (theta < -Math.PI) theta += Math.PI * 2;
            clockwise = theta >= 0 ? false : true;
            this.rotateAngle = Math.Abs(HitUtil.RadiansToDegrees(theta));  
        }
        public void HandleCancelRotate()
        {
            this.CurrentStatus = Status.BasePoint;
            this.originals.Clear();
            this.Copies.Clear();
            this.uCCanvas.DoInvalidate(true);
        }

        public bool HandleMouseDownForRotate(UnitPoint mousePoint)
        {
            bool isComplete = false;
            if(this.CurrentStatus==Status.BasePoint)
            {
                this.FirstPoint =this.SecondPoint= mousePoint;
                this.CurrentStatus = Status.EndPoint;
                foreach (IDrawObject obj in this.uCCanvas.Model.DrawingLayer.SelectedObjects)
                {
                    this.originals.Add(obj);
                    this.Copies.Add(obj.Clone());
                }
            }
            else if(this.CurrentStatus==Status.EndPoint)
            {
                this.SecondPoint = this.ThirdPoint = mousePoint;
                this.CurrentStatus = Status.OtherEndPoint;
            }
            else
            {
                this.ThirdPoint = mousePoint;
                this.CurrentStatus = Status.BasePoint;
                this.CalAngleAndDirection(this.SecondPoint);
                this.uCCanvas.Model.DoRotate(this.originals, this.FirstPoint,this.rotateAngle, this.clockwise);
                isComplete = true;
            }
            this.uCCanvas.DoInvalidate(true);
            return isComplete;
        }

        public void DrawObjects(ICanvas canvas,RectangleF rectangleF)
        {
            if(this.CurrentStatus==Status.EndPoint)
            {
                canvas.DrawLine(canvas, new Pen(Color.White, 1), this.FirstPoint, this.SecondPoint);
            }
            else if(this.CurrentStatus==Status.OtherEndPoint)
            {
                canvas.DrawLine(canvas, new Pen(Color.White, 1), this.FirstPoint, this.SecondPoint);
                canvas.DrawLine(canvas, new Pen(Color.White, 1), this.FirstPoint, this.ThirdPoint);
            }
            if(this.Copies.Count>0)
            {
                foreach (IDrawObject obj in Copies)
                {
                    obj.Draw(canvas, rectangleF);
                }
            }
        }

    }
}
