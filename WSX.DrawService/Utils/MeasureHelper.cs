using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Wrapper;

namespace WSX.DrawService.Utils
{
    public class MeasureHelper
    {
        private enum Status { BasePoint, EndPoint }
        private Status CurrentStatus;
        private UnitPoint FirstPoint, SecondPoint;
        private UCCanvas uCCanvas;
        public MeasureHelper(UCCanvas uCCanvas)
        {
            this.uCCanvas = uCCanvas;
        }
        public bool HandleMouseDownForMeasure(UnitPoint unitPoint)
        {
            bool isComplete = false;
            string msg = "";
            List<Patameter> SubPatameter = new List<Patameter>();
            if (this.CurrentStatus == Status.BasePoint)
            {
                this.CurrentStatus = Status.EndPoint;
                FirstPoint = unitPoint;
                msg = $"{unitPoint.X},{unitPoint.Y}";
            }
            else if (this.CurrentStatus == Status.EndPoint)
            {
                this.CurrentStatus = Status.BasePoint;
                SecondPoint = unitPoint;
                msg = $"{unitPoint.X},{unitPoint.Y}";
                SubPatameter = new List<Patameter>()
                {
                     new Patameter(){ Explain="长度：", ParameterValue=HitUtil.Distance(FirstPoint, SecondPoint).ToString()},
                     new Patameter(){ Explain="X方向：", ParameterValue=(SecondPoint.X - FirstPoint.X).ToString()},
                     new Patameter(){ Explain="Y方向：", ParameterValue=(SecondPoint.Y - FirstPoint.Y).ToString()},
                };
                this.uCCanvas.DoInvalidate(true);
                isComplete = true;
            }
            uCCanvas.SendDrawMsg(CanvasCommands.Measure.ToString(), msg,SubPatameter);
            return isComplete;
        }
        public void HandleMouseMoveForMeasure(UnitPoint mousePoint)
        {
            if (this.CurrentStatus == Status.EndPoint)
            {
                SecondPoint = mousePoint;
                this.uCCanvas.DoInvalidate(true);
            }
        }
        public void DrawObjects(ICanvas canvas, RectangleF rectangleF)
        {
            if (this.CurrentStatus == Status.EndPoint)
            {
                canvas.DrawLine(canvas, DrawTool.DrawUtils.VancantPen, FirstPoint, SecondPoint);
            }
        }
        public void HandleCancelMeasure()
        {
            this.CurrentStatus = Status.BasePoint;
            this.uCCanvas.DoInvalidate(true);
            this.uCCanvas.DrawButtonStatus(false);
        }
    }


}
