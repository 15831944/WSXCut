using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class ScaleHelper
    {
        private enum Status { BasePoint,EndPoint,OtherEndPoint}
        private Status CurrentStatus;
        private List<IDrawObject> originals = new List<IDrawObject>();
        private UCCanvas uCCanvas;
        private UnitPoint FirstPoint, SecondPoint, ThridPoint;
        private double scaleCoff=1.0,lastLength,baseLength;

        public List<IDrawObject> Copies { get; } = new List<IDrawObject>();

        public ScaleHelper(UCCanvas uCCanvas)
        {
            this.uCCanvas = uCCanvas;
        }

        public void HandleMouseMoveForScale(UnitPoint mousePoint)
        {
            if(this.CurrentStatus==Status.EndPoint)
            {
                this.SecondPoint =  mousePoint;
            }
            else if(this.CurrentStatus==Status.OtherEndPoint)
            {
                this.ThridPoint = mousePoint;
                this.CalScaleCoff();
                foreach (IDrawObject obj in this.Copies)
                {
                    ((IDrawTranslation)obj).DoSizeChange(this.FirstPoint.X, this.FirstPoint.Y, this.scaleCoff, this.scaleCoff);
                }
                this.uCCanvas.DoInvalidate(true);
            }
        }

        private void CalScaleCoff()
        {
            double currentLength = HitUtil.Distance(this.FirstPoint, this.ThridPoint); ;
            if (currentLength > 0.05*this.baseLength)
            {
                this.scaleCoff = currentLength / this.lastLength;
                this.lastLength = currentLength;
            }
            else
            {
                this.scaleCoff = 1;
            }           
        }

        public void HandleCancelScale()
        {
            this.CurrentStatus = Status.BasePoint;
            this.originals.Clear();
            this.Copies.Clear();
            this.uCCanvas.DoInvalidate(true);
        }

        public bool HandleMouseDownForScale(UnitPoint mousePoint)
        {
            bool isComplete = false;
            if(this.CurrentStatus==Status.BasePoint)
            {
                this.FirstPoint = this.SecondPoint = mousePoint;
                this.CurrentStatus = Status.EndPoint;
                foreach (IDrawObject obj in this.uCCanvas.Model.DrawingLayer.SelectedObjects)
                {
                    this.originals.Add(obj);
                    this.Copies.Add(obj.Clone());
                }
            }
            else if(this.CurrentStatus==Status.EndPoint)
            {
                this.SecondPoint = this.ThridPoint = mousePoint;
                this.CurrentStatus = Status.OtherEndPoint;
                this.lastLength =this.baseLength= HitUtil.Distance(this.FirstPoint, this.SecondPoint);
            }
            else
            {
                this.ThridPoint = mousePoint;
                this.CurrentStatus = Status.BasePoint;
                this.scaleCoff = HitUtil.Distance(this.FirstPoint, this.ThridPoint) / this.baseLength;
                this.uCCanvas.Model.DoSizeChange(this.originals, this.FirstPoint.X, this.FirstPoint.Y, this.scaleCoff, this.scaleCoff);
                isComplete = true;
            }
            this.uCCanvas.DoInvalidate(true);
            return isComplete;
        }

        public void DrawObject(ICanvas canvas,RectangleF rectangleF)
        {
            if(this.CurrentStatus==Status.EndPoint)
            {
                canvas.DrawLine(canvas, new Pen(Color.White, 1), this.FirstPoint, this.SecondPoint);
            }
            else if(this.CurrentStatus==Status.OtherEndPoint)
            {
                canvas.DrawLine(canvas, new Pen(Color.White, 1), this.FirstPoint, this.ThridPoint);
            }
            if(this.Copies.Count>0)
            {
                foreach (IDrawObject obj in this.Copies)
                {
                    obj.Draw(canvas, rectangleF);
                }
            }
        }
    }
}
