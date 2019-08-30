using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawTool;
using WSX.DrawService.Wrapper;
using WSX.GlobalData.Model;

namespace WSX.DrawService.Utils
{
    public class ArrayInteractiveHelper
    {
        private enum Status { BasePoint, EndPoint }
        private Status CurrentStatus;
        private UnitPoint FirstPoint, SecondPoint;
        private UCCanvas uCCanvas;
        private double OffsetColumn, OffsetRow;
        private List<IDrawObject> drawObjects = new List<IDrawObject>();
        private List<IDrawObject> marginDrawObjects = new List<IDrawObject>();
        private SelectionRectangle selectionRectangle;
        private bool isComplete = false;
        private PointF p1, p2;
        public ArrayInteractiveHelper(UCCanvas uCCanvas)
        {
            this.uCCanvas = uCCanvas;

        }
        public bool HandleMouseDownForArrayInteractive(MouseEventArgs e)
        {
            string msg = "";
            List<Patameter> SubPatameter = new List<Patameter>();
            if (this.CurrentStatus == Status.BasePoint)
            {
                isComplete = false;
                drawObjects = uCCanvas.Model.DrawingLayer.SelectedObjects.ToList();
                this.CurrentStatus = Status.EndPoint;
                FirstPoint = uCCanvas.ToUnit(e.Location);
                selectionRectangle = new SelectionRectangle(new PointF(e.X, e.Y));
                //uCCanvas.selectRect = new SelectionRectangle(new PointF(e.X, e.Y));
                p1 = new PointF(e.X, e.Y);
            }
            else if (this.CurrentStatus == Status.EndPoint)
            {
                this.CurrentStatus = Status.BasePoint;
                SecondPoint = uCCanvas.ToUnit(e.Location);
                isComplete = true;
                this.uCCanvas.DoInvalidate(true);


            }
            return isComplete;
        }
        public void HandleMouseMoveForArrayInteractive(MouseEventArgs e)
        {
            if (this.CurrentStatus == Status.EndPoint)
            {
                p2 = new PointF(e.X, e.Y);
                SecondPoint = uCCanvas.ToUnit(e.Location);

                this.uCCanvas.DoInvalidate(true);
            }
        }
        public void DrawObjects(ICanvas canvas, RectangleF rectangleF)
        {
            if (this.CurrentStatus == Status.EndPoint || isComplete == true)
            {
                ArrayRectangle();
                foreach (var item in marginDrawObjects)
                {
                    item.Draw(canvas, rectangleF);
                }
                if (isComplete == false)
                {
                    var vacantPen = new Pen(Color.White, 1)
                    {
                        DashStyle = DashStyle.Custom,
                        DashPattern = new float[2] { 3, 4 },
                        EndCap = LineCap.Flat
                    };
                    List<UnitPoint> rectPoint = new List<UnitPoint>() { FirstPoint, new UnitPoint(SecondPoint.X, FirstPoint.Y), SecondPoint, new UnitPoint(FirstPoint.X, SecondPoint.Y) };
                    for (int i = 0; i < rectPoint.Count-1; i++)
                    {
                        uCCanvas.DrawLine(canvas, vacantPen, rectPoint[i], rectPoint[i + 1], 0);
                    }
                    uCCanvas.DrawLine(canvas, vacantPen, rectPoint[3], rectPoint[0], 0);
                }
            }
        }
        public void HandleCancelArrayInteractive()
        {
            this.CurrentStatus = Status.BasePoint;
            this.isComplete = false;
            this.uCCanvas.DoInvalidate(true);

            //this.uCCanvas.DrawButtonStatus(false);
        }
        /// <summary>
        /// 获取偏移距离的正负
        /// </summary>
        /// <param name="rowCoeff"></param>
        /// <param name="colCoeff"></param>
        /// <returns></returns>
        private UnitPoint GetArrayRectangleOffset(ref int rowCoeff, ref int colCoeff)
        {
            UnitPoint offset = new UnitPoint();

            if (SecondPoint.X > FirstPoint.X)
            {
                offset.X = OffsetColumn;
                colCoeff = 1;
            }
            else
            {
                offset.X = -OffsetColumn;
                colCoeff = -1;
            }
            if (SecondPoint.Y > FirstPoint.Y)
            {
                offset.Y = OffsetRow;
                rowCoeff = 1;
            }
            else
            {
                offset.Y = -OffsetRow;
                rowCoeff = -1;
            }

            return offset;
        }
        /// <summary>
        /// 计算放置的对象数量
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        private void CalRowAndColCount(out int rowCount, out int colCount)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            GetAllDrawingsMaxMinPoint(out UnitPoint maxpoint, out UnitPoint minpoint);
            double width = maxpoint.X - minpoint.X - thresholdWidth;
            double height = maxpoint.Y - minpoint.Y - thresholdWidth;

            double totalwidth = Math.Abs(SecondPoint.X - FirstPoint.X - thresholdWidth);
            double totalheight = Math.Abs(SecondPoint.Y - FirstPoint.Y - thresholdWidth);
            colCount = Math.Abs((int)Math.Floor((totalwidth + OffsetColumn) / (width + OffsetColumn)));
            rowCount = Math.Abs((int)Math.Floor((totalheight + OffsetRow) / (height + OffsetRow)));
        }


        private void GetAllDrawingsMaxMinPoint(out UnitPoint maxPoint, out UnitPoint minPoint)
        {
            maxPoint = ((IDrawTranslation)drawObjects[0]).MaxValue;
            minPoint = ((IDrawTranslation)drawObjects[0]).MinValue;
            for (int i = 1; i < drawObjects.Count; i++)
            {
                UnitPoint tempMax = ((IDrawTranslation)drawObjects[i]).MaxValue;
                UnitPoint tempMin = ((IDrawTranslation)drawObjects[i]).MinValue;
                if (tempMax.X > maxPoint.X)
                {
                    maxPoint.X = tempMax.X;
                }
                if (tempMax.Y > maxPoint.Y)
                {
                    maxPoint.Y = tempMax.Y;
                }
                if (tempMin.X < minPoint.X)
                {
                    minPoint.X = tempMin.X;
                }
                if (tempMin.Y < minPoint.Y)
                {
                    minPoint.Y = tempMin.Y;
                }
            }
        }
        private UnitPoint GetMovePoint(int rowcoeff, int colcoeff)
        {
            GetAllDrawingsMaxMinPoint(out UnitPoint maxpoint, out UnitPoint minpoint);
            UnitPoint unitPoint = new UnitPoint();

            if (rowcoeff == -1 && colcoeff == 1)
            {
                unitPoint = new UnitPoint(minpoint.X, maxpoint.Y);
            }
            else if (rowcoeff == -1 && colcoeff == -1)
            { unitPoint = new UnitPoint(maxpoint.X, maxpoint.Y); }
            else if (rowcoeff == 1 && colcoeff == 1)
            {
                unitPoint = new UnitPoint(minpoint.X, minpoint.Y);
            }
            else if (rowcoeff == 1 && colcoeff == -1)
            {
                unitPoint = new UnitPoint(maxpoint.X, minpoint.Y);
            }

            return unitPoint;
        }

        private void ArrayRectangle()
        {
            marginDrawObjects = new List<IDrawObject>();
            int rowCoeff = 1;
            int colCoeff = 1;
            //获取偏移正负
            OffsetColumn = GlobalModel.Params.ArrayInteractive.ColumnSpacing;
            OffsetRow = GlobalModel.Params.ArrayInteractive.RowSpacing;
            UnitPoint offset = this.GetArrayRectangleOffset(ref rowCoeff, ref colCoeff);

            //计算拖动范围内可放置的数量
            CalRowAndColCount(out int rowCount, out int colCount);
            //
            if (rowCount <= 0 || colCount <= 0) return;

            //计算偏移距离 
            UnitPoint unitPoint = GetMovePoint(rowCoeff, colCoeff);

            GetAllDrawingsMaxMinPoint(out UnitPoint maxpoint, out UnitPoint minpoint);
            float thWidth = UCCanvas.GetThresholdWidth();
            double ofx = maxpoint.X - minpoint.X - thWidth;
            double ofy = maxpoint.Y - minpoint.Y - thWidth;

            for (int i = 0; i < colCount; i++)
            {                
                for (int j = 0; j < rowCount; j++)
                {
                    // index = j;                  
                    for (int k = 0; k < drawObjects.Count; k++)
                    {
                        IDrawObject drawObject = drawObjects[k].Clone();
                        if (isComplete)
                        {
                            drawObject.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
                            drawObject.IsCompleteDraw = isComplete;
                        }

                        if (i == 0 && j == 0)
                        {
                            drawObject.Move(new UnitPoint((FirstPoint.X - unitPoint.X - thWidth / 2), (FirstPoint.Y - unitPoint.Y)));
                        }
                        else
                        {
                            drawObject.Move(
                                new UnitPoint(
                                    ((FirstPoint.X - unitPoint.X - thWidth / 2) + (i * (offset.X + ofx * colCoeff))),
                                    ((FirstPoint.Y - unitPoint.Y) + (j * (offset.Y + ofy * rowCoeff)))
                                    )
                                    );
                        }
                        marginDrawObjects.Add(drawObject);
                    }
                }
            }
            

            if (isComplete)
            {
                if (GlobalModel.Params.ArrayInteractive.IsClearOriginalCompleted)
                {
                    marginDrawObjects.ForEach(s => s.GroupParam.FigureSN = s.GroupParam.FigureSN - drawObjects.Count);

                    uCCanvas.Model.ArrayObjects(drawObjects, marginDrawObjects);
                }
                else
                {
                    uCCanvas.Model.AddObjectOnDrawing(marginDrawObjects);
                }
                uCCanvas.Model.DrawingLayer.UpdateSN();

                this.uCCanvas.DoInvalidate(true);
                uCCanvas.CommandEscape();
            } 
        }
    }
}
