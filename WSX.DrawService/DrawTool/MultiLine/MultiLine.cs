using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;
using WSX.GlobalData.Model;

namespace WSX.DrawService.DrawTool
{
    public class MultiLine : DrawObjectBase, IDrawObject,IDrawTranslation
    {
        public UnitPoint currentPoint=UnitPoint.Empty;
        public int currentNodeIndex = 0;
        public DrawObjectMouseDown drawStatus = DrawObjectMouseDown.Unknown;
        public enum CurrentPoint { P1,EndPoint}
        public CurrentPoint current = CurrentPoint.P1;

        public static string ObjectType
        {
            get
            {
                return "MultiLine";
            }
        }
        public bool IsCloseFigure
        {
            get
            {
                return false;
            }
        }
        public UnitPoint StartMovePoint
        {
            get
            {
                if(this.MultiLinePoints!=null&&this.MultiLinePoints.Count>=1)
                {
                    return this.MultiLinePoints[0];
                }
                return UnitPoint.Empty;
            }
            set { }
        }

        public List<UnitPoint> MultiLinePoints { get; set; }

        public MultiLine()
        {
            this.FigureId = Guid.NewGuid().ToString();
            this.MultiLinePoints = new List<UnitPoint>();
        }

        public MultiLine(List<UnitPoint> multiLinePoints,float width,Color color)
        {
            this.MultiLinePoints = multiLinePoints;
            this.IsSelected = false;
            this.FigureId = Guid.NewGuid().ToString();
        }

        public virtual void Copy(MultiLine multiLine)
        {
            base.Copy(multiLine);
            this.MultiLinePoints = new List<UnitPoint>(multiLine.MultiLinePoints);
            this.IsSelected = multiLine.IsSelected;
            this.drawStatus = multiLine.drawStatus;
            this.SN = multiLine.SN;
        }

        #region DrawObjectBase

        public override void InitializeFromModel(UnitPoint unitPoint,ISnapPoint snapPoint)
        {
            this.LayerId = (int)GlobalModel.CurrentLayerId;
            this.IsSelected = true;
        }
        #endregion

        #region IDrawObject
        public string FigureId { get; set; }
        public bool IsSelected { get; set; }
        public virtual string Id
        {
            get
            {
                return ObjectType;
            }
        }
        public int SN { get; set; }
        public UnitPoint RepeatStartingPoint
        {
            get
            {
                return this.currentPoint;
            }
        }
        public double SizeLength
        {
            get
            {
                double length = 0.0;
                for (int i = 0; i < this.MultiLinePoints.Count-1; i++)
                {
                    length += HitUtil.Distance(this.MultiLinePoints[i], this.MultiLinePoints[i + 1]);
                }
                return length;
            }
        }
        public IDrawObject Clone()
        {
            MultiLine multiLine = new MultiLine();
            multiLine.Copy(this);
            return multiLine;
        }
        public void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            int penIndex = this.LayerId;
            if (AdditionalInfo.Instance.IsShowNotClosedFigureInRed)
            {
                penIndex = 0;
            }
            if (this.IsSelected)
            {
                if (this.currentPoint != UnitPoint.Empty)
                {
                    this.MultiLinePoints.Add(this.currentPoint);
                }
                canvas.DrawMultiLine(canvas, DrawUtils.CustomSelectedPens[penIndex], this.MultiLinePoints);
                if (this.currentPoint != UnitPoint.Empty)
                {
                    this.MultiLinePoints.RemoveAt(this.MultiLinePoints.Count - 1);
                }
                if (canvas.DataModel.DrawingPattern == DrawingPattern.NodeEditPattern && this.drawStatus == DrawObjectMouseDown.Done)
                {
                    for (int i = 0; i < this.MultiLinePoints.Count; i++)
                    {
                        DrawUtils.DrawNode(canvas, this.MultiLinePoints[i]);
                    }
                }
            }
            else
            {
                canvas.DrawMultiLine(canvas, DrawUtils.CustomPens[penIndex], this.MultiLinePoints);
            }        
            this.ShowBoundRect(canvas,penIndex);
            this.ShowStartMovePoint(canvas);
            this.ShowFigureSN(canvas);
        }

        public RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            if (thresholdWidth < this.Width)
            {
                thresholdWidth = this.Width;
            }
            RectangleF rectangleF;
            switch(rectangleType)
            {
                case BoundingRectangleType.Drawing:
                    rectangleF = this.GetDrawingRectangle();
                    break;
                case BoundingRectangleType.NodeEditing:
                    rectangleF = this.GetNodeEditRectangle();
                    break;
                case BoundingRectangleType.SelectRange:
                    rectangleF = this.GetSelectionRectangle();
                    break;
                default:
                    rectangleF = new RectangleF();
                    break;
            }           
            return rectangleF;
        }

        public RectangleF GetBoundingRectangle()
        {
            return this.GetSelectionRectangle();
        }

        private RectangleF GetDrawingRectangle()
        {
            RectangleF rectangleF;
            if (this.MultiLinePoints.Count > 0)
            {
                rectangleF = ScreenUtils.GetRectangleF(this.MultiLinePoints[this.MultiLinePoints.Count - 1], this.currentPoint, UCCanvas.GetThresholdWidth());
            }
            else
            {
                rectangleF = new RectangleF();
            }
            return rectangleF;
        }

        private RectangleF GetNodeEditRectangle()
        {
            RectangleF rectangleF;
            float thWidth = UCCanvas.GetThresholdWidth();
            if (this.currentNodeIndex == 0)
            {
                rectangleF = ScreenUtils.GetRectangleF(this.MultiLinePoints[0], this.MultiLinePoints[1], thWidth);
            }
            else if (this.currentNodeIndex == this.MultiLinePoints.Count - 1)
            {
                rectangleF = ScreenUtils.GetRectangleF(this.MultiLinePoints[this.MultiLinePoints.Count - 2], this.MultiLinePoints[this.MultiLinePoints.Count - 1], thWidth);
            }
            else
            {
                List<UnitPoint> temp = new List<UnitPoint>();
                temp.Add(this.MultiLinePoints[this.currentNodeIndex - 1]);
                temp.Add(this.MultiLinePoints[this.currentNodeIndex]);
                temp.Add(this.MultiLinePoints[this.currentNodeIndex + 1]);
                float maxX = (float)temp.Max<UnitPoint>(maxx => maxx.X);
                float maxY = (float)temp.Max<UnitPoint>(maxy => maxy.Y);
                float minX = (float)temp.Min<UnitPoint>(minx => minx.X);
                float minY = (float)temp.Min<UnitPoint>(miny => miny.Y);
                rectangleF = ScreenUtils.GetRectangleF(new UnitPoint(minX, minY), new UnitPoint(maxX, maxY), thWidth);            
            }
            return rectangleF;
        }

        private RectangleF GetSelectionRectangle()
        {
            RectangleF rectangleF ;
            float maxX = (float)this.MultiLinePoints.Max<UnitPoint>(maxx => maxx.X);
            float maxY = (float)this.MultiLinePoints.Max<UnitPoint>(maxy => maxy.Y);
            float minX = (float)this.MultiLinePoints.Min<UnitPoint>(minx => minx.X);
            float minY = (float)this.MultiLinePoints.Min<UnitPoint>(miny => miny.Y);
            rectangleF = ScreenUtils.GetRectangleF(new UnitPoint(minX, minY), new UnitPoint(maxX, maxY), UCCanvas.GetThresholdWidth());
            return rectangleF;
        }

        public string GetInfoAsString()
        {
            return string.Format("MultiLine@{0}", this.MultiLinePoints[0]);
        }

        public void Move(UnitPoint offset)
        {
            for (int i = 0; i < this.MultiLinePoints.Count; i++)
            {
                this.MultiLinePoints[i] = new UnitPoint(this.MultiLinePoints[i].X + offset.X, this.MultiLinePoints[i].Y + offset.Y);
            }
        }

        public INodePoint NodePoint(UnitPoint unitPoint)
        {
            float thWidth = UCCanvas.GetThresholdWidth();
            for (int i = 0; i < this.MultiLinePoints.Count; i++)
            {
                if (HitUtil.PointInPoint(this.MultiLinePoints[i], unitPoint, thWidth))
                {                   
                    INodePoint nodePoint= new NodeMultiLine(this, i);
                    this.currentNodeIndex = i;
                    return nodePoint;
                }
            }
            return null;
        }

        public bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.SelectRange);
            if (anyPoint)//包含图形的任一点即可
            {
                if (HitUtil.LineIntersectWithRect(this.MultiLinePoints[this.MultiLinePoints.Count - 1], this.MultiLinePoints[0], rectangle))
                    return true;
                for (int i = 0; i < this.MultiLinePoints.Count - 1; i++)
                {
                    if (HitUtil.LineIntersectWithRect(this.MultiLinePoints[i], this.MultiLinePoints[i + 1], rectangle))
                    {
                        return true;
                    }
                }
            }
            return rectangle.Contains(rectangleF);
        }

        public void OnKeyDown(ICanvas canvas, KeyEventArgs e)
        {
            
        }

        public DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint, MouseEventArgs e)
        {
            OnMouseMove(canvas, point);
            this.MultiLinePoints.Add(point);
            return DrawObjectMouseDown.Continue;
        }

        public void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {
            if(this.current==CurrentPoint.P1)
            {
                this.MultiLinePoints.Add(unitPoint);
                this.current = CurrentPoint.EndPoint;
                return;
            }
            else
            {
                this.currentPoint = unitPoint;
            }
        }

        public void OnMouseUp(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint)
        {
            
        }

        public bool PointInObject(UnitPoint unitPoint)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            for (int i = 0; i < this.MultiLinePoints.Count - 1; i++)
            {
                if (HitUtil.IsPointInLine(this.MultiLinePoints[i],this.MultiLinePoints[i + 1],unitPoint, thresholdWidth))
                {
                    return true;
                }
            }
            if (HitUtil.IsPointInLine(this.MultiLinePoints[0],this.MultiLinePoints[this.MultiLinePoints.Count-1], unitPoint, thresholdWidth))
            {
                return true;
            }
            return false;
        }

        public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint unitPoint, List<IDrawObject> drawObjects, Type[] runningSnapTypes, Type userSnapType)
        {
            return null;
        }

        public bool SetStartMovePoint(UnitPoint unitPoint)
        {
            if(this.PointInObject(unitPoint))
            {
                return true;
            }
            return false;
        }

        #region 显示附加信息

        public void ShowBoundRect(ICanvas canvas,int penIndex)
        {
            if (AdditionalInfo.Instance.IsShowBoundRect&&this.drawStatus==DrawObjectMouseDown.Done)
            {
                RectangleF rectangleF = this.GetBoundingRectangle();
                canvas.DrawBoundRectangle(canvas, DrawUtils.CustomPens[penIndex], new PointF(rectangleF.Location.X, rectangleF.Bottom), rectangleF.Width, rectangleF.Height);
            }
        }

        public void ShowNotClosedFigure()
        {
            throw new NotImplementedException();
        }

        public void ShowFigureSN(ICanvas canvas)
        {
            if(AdditionalInfo.Instance.IsShowFigureSN && this.drawStatus == DrawObjectMouseDown.Done)
            {
                canvas.DrawSN(canvas, this.SN.ToString(), this.StartMovePoint);
            }
        }

        public void ShowStartMovePoint(ICanvas canvas)
        {
            if (this.drawStatus == DrawObjectMouseDown.Done && AdditionalInfo.Instance.IsShowStartMovePoint)
            {
                canvas.DrawStartDot(canvas, Brushes.Yellow, this.StartMovePoint);
            }
        }

        public void ShowMachinePath()
        {
            throw new NotImplementedException();
        }
        public void ShowMicroConnectFlag()
        {
            throw new NotImplementedException();
        }
        public void ShowVacantPath()
        {
            throw new NotImplementedException();
        }

        #endregion
        #endregion

        #region IDrawTranslate

        public void Mirroring(double centerAxis, int direction)
        {
            if (direction == 0)//水平镜像
            {
                for (int i = 0; i < this.MultiLinePoints.Count; i++)
                {
                    this.MultiLinePoints[i] = new UnitPoint(2 * centerAxis - this.MultiLinePoints[i].X, this.MultiLinePoints[i].Y);
                }
            }
            else
            {
                for (int i = 0; i < this.MultiLinePoints.Count; i++)
                {
                    this.MultiLinePoints[i] = new UnitPoint(this.MultiLinePoints[i].X, 2 * centerAxis - this.MultiLinePoints[i].Y);
                }
            }
        }

        public UnitPoint MaxValue
        {
            get
            {
                RectangleF rectangleF = this.GetBoundingRectangle();
                UnitPoint max = new UnitPoint(rectangleF.Right, rectangleF.Bottom);
                return max;
            }
        }

        public UnitPoint MinValue
        {
            get
            {
                RectangleF rectangleF = this.GetBoundingRectangle();
                UnitPoint min = new UnitPoint(rectangleF.Left, rectangleF.Top);
                return min;
            }
        }

        public double OverCuttingLength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void ReverseDirection()
        {
            this.MultiLinePoints.Reverse();
        }

        public void OverCutting()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
