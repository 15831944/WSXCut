using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;
using WSX.GlobalData.Model;

namespace WSX.DrawService.DrawTool
{
    public class SingleRectangle: DrawObjectBase, IDrawObject, IDrawTranslation
    {
        private DrawObjectMouseDown drawStatus = DrawObjectMouseDown.Unknown;
        public UnitPoint FirstPoint { get; set; }
        public UnitPoint SecondPoint { get; set; }
        public UnitPoint  LeftTopPoint { get; set; }
        public UnitPoint RightTopPoint { get; set; }
        public UnitPoint LeftBottomPoint { get; set; }
        public UnitPoint RightBottomPoint { get; set; }
        public UnitPoint CenterPoint
        {
            get
            {
                return new UnitPoint((MinValue.X + MaxValue.X) / 2, (MinValue.Y + MaxValue.Y) / 2);
            }
        }
        public NodePosition StartMovePosition { get; set; }

        public CurrentDrawPoint DrawPoint = CurrentDrawPoint.First;

        public List<UnitPoint> OverCuttingPoints { get; set; }

        public double WidthRect
        {
            get
            {
                return this.RightTopPoint.X - this.LeftTopPoint.X;
            }           
        }
        public double HeightRect
        {
            get
            {
                return this.LeftTopPoint.Y - this.LeftBottomPoint.Y;
            }
        }

        public List<UnitPoint> MachinePoints
        {
            get
            {
                List<UnitPoint> machinePoints = new List<UnitPoint>();
                UnitPoint[] unitPoints = new UnitPoint[4] { LeftTopPoint, RightTopPoint, RightBottomPoint, LeftBottomPoint };
                machinePoints.Add(this.StartMovePoint);
                int startPointIndex = (int)this.StartMovePosition;
                if (this.IsClockWiseDirection)
                {
                    for (int i = 0; i < unitPoints.Length; i++)
                    {
                        if (startPointIndex + i + 1 <= 3)
                        {
                            machinePoints.Add(unitPoints[startPointIndex + i + 1]);
                        }
                        else
                        {
                            machinePoints.Add(unitPoints[startPointIndex + i + 1 - unitPoints.Length]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < unitPoints.Length; i++)
                    {
                        if (startPointIndex - i >= 0)
                        {
                            machinePoints.Add(unitPoints[startPointIndex - i]);
                        }
                        else
                        {
                            machinePoints.Add(unitPoints[startPointIndex - i + unitPoints.Length]);
                        }
                    }
                }
                machinePoints.Add(this.StartMovePoint);
                return machinePoints;
            }
        }

        public double OverCuttingLength { get; set; }

        public bool IsClockWiseDirection { get; set; }

        public static string ObjectType
        {
            get
            {
                return "SingleRectangle";
            }
        }

        public enum CurrentDrawPoint { First,Second}
        public enum NodePosition { LeftTop, RightTop, RightBottom, LeftBottom }

        public SingleRectangle()
        {
            this.FigureId = Guid.NewGuid().ToString();
            this.SecondPoint = UnitPoint.Empty;
            this.IsClockWiseDirection = true;
        }
              
        #region DrawObjectBase

        public override void InitializeFromModel(UnitPoint unitPoint, ISnapPoint snapPoint)
        {
            this.LayerId = (int)GlobalModel.CurrentLayerId;
            this.LeftTopPoint = this.RightBottomPoint = this.StartMovePoint = unitPoint;
            this.OnMouseDown(null, unitPoint, snapPoint, null);
            this.IsSelected = true;
        }
        #endregion

        #region IDrawObject
        public string FigureId { get; private set; }
        public bool IsSelected { get; set; }
        public virtual string Id
        {
            get
            {
                return ObjectType;
            }
        }
        public int SN { get; set; }
        public double SizeLength
        {
            get
            {
                return (this.WidthRect + this.HeightRect) * 2;
            }
        }
        public bool IsCloseFigure
        {
            get { return true; }
        }
        public UnitPoint StartMovePoint { get; set; }

        public UnitPoint RepeatStartingPoint
        {
            get { return UnitPoint.Empty; }
        }

        public virtual IDrawObject Clone()
        {
            SingleRectangle singleRectangle = new SingleRectangle();
            singleRectangle.Copy(this);
            return singleRectangle;
        }

        public virtual void Copy(SingleRectangle singleRectangle)
        {
            base.Copy(singleRectangle);
            this.FirstPoint = singleRectangle.FirstPoint;
            this.SecondPoint = singleRectangle.SecondPoint;
            this.LeftTopPoint = singleRectangle.LeftTopPoint;
            this.LeftBottomPoint = singleRectangle.LeftBottomPoint;
            this.RightTopPoint = singleRectangle.RightTopPoint;
            this.RightBottomPoint = singleRectangle.RightBottomPoint;
            this.IsSelected = singleRectangle.IsSelected;
            this.StartMovePoint = singleRectangle.StartMovePoint;
            this.IsClockWiseDirection = singleRectangle.IsClockWiseDirection;
            this.OverCuttingLength = singleRectangle.OverCuttingLength;
            this.OverCuttingPoints = singleRectangle.OverCuttingPoints;
            this.StartMovePosition = singleRectangle.StartMovePosition;
            this.drawStatus = singleRectangle.drawStatus;
            this.SN = singleRectangle.SN;
        }

        public void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            if (this.SecondPoint == UnitPoint.Empty) return;
            if(this.IsSelected)
            {
                canvas.DrawSingleRectangle(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.LeftTopPoint, this.RightTopPoint, this.RightBottomPoint, this.LeftBottomPoint,this.IsClockWiseDirection);
                if(this.OverCuttingLength!=0)
                {
                    for (int i = 0; i < this.OverCuttingPoints.Count-1; i++)
                    {
                        canvas.DrawLine(canvas, DrawUtils.SelectecedOverCuttingPen, this.OverCuttingPoints[i], this.OverCuttingPoints[i + 1]);
                    }
                }
                //绘制节点   
                if (canvas.DataModel.DrawingPattern == DrawingPattern.NodeEditPattern&&this.drawStatus==DrawObjectMouseDown.Done)
                {
                        DrawUtils.DrawNode(canvas, this.LeftTopPoint);
                        DrawUtils.DrawNode(canvas, this.RightTopPoint);
                        DrawUtils.DrawNode(canvas, this.RightBottomPoint);
                        DrawUtils.DrawNode(canvas, this.LeftBottomPoint);
                }
            }
            else
            {
                canvas.DrawSingleRectangle(canvas, DrawUtils.CustomPens[this.LayerId], this.LeftTopPoint, this.RightTopPoint, this.RightBottomPoint, this.LeftBottomPoint,this.IsClockWiseDirection);
                if (this.OverCuttingLength != 0)
                {
                    for (int i = 0; i < this.OverCuttingPoints.Count - 1; i++)
                    {
                        canvas.DrawLine(canvas, DrawUtils.SelectecedOverCuttingPen, this.OverCuttingPoints[i], this.OverCuttingPoints[i + 1]);
                    }
                }
            }
            this.ShowStartMovePoint(canvas);
            this.ShowFigureSN(canvas);
            //this.ShowBoundRect(canvas,penIndex);
        }

        public RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            return this.GetBoundingRectangle();
        }

        private RectangleF GetBoundingRectangle()
        {
            RectangleF rectangleF =ScreenUtils.GetRectangleF(LeftTopPoint, RightBottomPoint, UCCanvas.GetThresholdWidth());
            return rectangleF;
        }

        public string GetInfoAsString()
        {
            return string.Format("SingleRectangle@{0}, width={1:f4}, A1={2:f4},", this.LeftTopPoint.ToString(), this.WidthRect, this.HeightRect);
        }

        public void Move(UnitPoint offset)
        {
            this.FirstPoint += offset;
            this.SecondPoint += offset;
            this.StartMovePoint += offset;
            this.UpdateSingleRectangleFromTwoPoints();
            this.OverCutting();
        }

        public INodePoint NodePoint(UnitPoint unitPoint)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            if (HitUtil.PointInPoint(this.LeftTopPoint, unitPoint, thresholdWidth))
            {
                return new NodePointRectangle(this, NodePosition.LeftTop);
            }
            if (HitUtil.PointInPoint(this.RightTopPoint, unitPoint, thresholdWidth))
            {
                return new NodePointRectangle(this,NodePosition.RightTop);
            }
            if (HitUtil.PointInPoint(this.RightBottomPoint, unitPoint, thresholdWidth))
            {
                return new NodePointRectangle(this, NodePosition.RightBottom);
            }
            if (HitUtil.PointInPoint(this.LeftBottomPoint, unitPoint, thresholdWidth))
            {
                return new NodePointRectangle(this,NodePosition.LeftBottom);
            }
            return null;
        }

        public bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.All);
            if (anyPoint)
            {
                return HitUtil.RectangleIntersect(rectangle, rectangleF);
            }
            return rectangle.Contains(rectangleF);
        }

        public void OnKeyDown(ICanvas canvas, KeyEventArgs e)
        {
            
        }

        public DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint, MouseEventArgs e)
        {
            OnMouseMove(canvas, point);
            if (this.DrawPoint==CurrentDrawPoint.First)
            {
                this.DrawPoint = CurrentDrawPoint.Second;
                return DrawObjectMouseDown.Continue;
            }
            else
            {
                this.IsSelected = false;
                this.SN = ++GlobalModel.TotalDrawObjectCount;
                this.drawStatus = DrawObjectMouseDown.Done;
                return DrawObjectMouseDown.Done;
            }
        }

        public void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {
            if(this.DrawPoint==CurrentDrawPoint.First)
            {
                this.FirstPoint = unitPoint;
                return;
            }
            else
            {
                this.SecondPoint = unitPoint;
                this.UpdateSingleRectangleFromTwoPoints();
                this.SetStartMovePoint(FirstPoint);
            }
        }

        public void UpdateSingleRectangleFromTwoPoints()
        {
            double maxX, maxY, minX, minY;
            maxX = FirstPoint.X > SecondPoint.X ? FirstPoint.X : SecondPoint.X;
            maxY = FirstPoint.Y > SecondPoint.Y ? FirstPoint.Y : SecondPoint.Y;
            minX = FirstPoint.X < SecondPoint.X ? FirstPoint.X : SecondPoint.X;
            minY = FirstPoint.Y < SecondPoint.Y ? FirstPoint.Y : SecondPoint.Y;
            this.LeftTopPoint = new UnitPoint(minX, maxY);
            this.RightTopPoint = new UnitPoint(maxX, maxY);
            this.RightBottomPoint = new UnitPoint(maxX, minY);
            this.LeftBottomPoint = new UnitPoint(minX, minY);
        }

        public void OnMouseUp(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint)
        {
           
        }

        public bool PointInObject(UnitPoint unitPoint)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            if (HitUtil.IsPointInLine(this.LeftTopPoint, this.RightTopPoint, unitPoint, thresholdWidth))
            {
                return true;
            }
            if (HitUtil.IsPointInLine(this.RightTopPoint, this.RightBottomPoint, unitPoint, thresholdWidth))
            {
                return true;
            }
            if (HitUtil.IsPointInLine(this.RightBottomPoint, this.LeftBottomPoint, unitPoint, thresholdWidth))
            {
                return true;
            }
            if (HitUtil.IsPointInLine(this.LeftBottomPoint, this.LeftTopPoint, unitPoint, thresholdWidth))
            {
                return true;
            }
            return false;
        }    

        private UnitPoint HandleSideBoundError(UnitPoint mousePoint, UnitPoint unitPoint1, UnitPoint unitPoint2, float threshWidth)
        {
            if (HitUtil.Distance(mousePoint, unitPoint1) <= threshWidth)
            {
                return unitPoint1;
            }
            if (HitUtil.Distance(mousePoint, unitPoint2) <= threshWidth)
            {
                return unitPoint2;
            }
            return mousePoint;
        }

        public bool SetStartMovePoint(UnitPoint unitPoint)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            if (this.PointInObject(unitPoint))
            {
                if (HitUtil.IsPointInLine(this.LeftTopPoint, this.RightTopPoint, unitPoint, thresholdWidth))
                {
                    unitPoint = this.HandleSideBoundError(unitPoint, LeftTopPoint, RightTopPoint, thresholdWidth);
                    this.StartMovePoint = HitUtil.FindApparentIntersectPoint(this.CenterPoint, unitPoint, this.LeftTopPoint, this.RightTopPoint);
                    this.StartMovePosition = NodePosition.LeftTop;                  
                }
                else if (HitUtil.IsPointInLine(this.RightTopPoint, this.RightBottomPoint, unitPoint, thresholdWidth))
                {
                    unitPoint = this.HandleSideBoundError(unitPoint, RightTopPoint, RightBottomPoint, thresholdWidth);
                    this.StartMovePoint = HitUtil.FindApparentIntersectPoint(this.CenterPoint, unitPoint, this.RightTopPoint, this.RightBottomPoint);
                    this.StartMovePosition = NodePosition.RightTop;                   
                }
                else if (HitUtil.IsPointInLine(this.RightBottomPoint, this.LeftBottomPoint, unitPoint, thresholdWidth))
                {
                    unitPoint = this.HandleSideBoundError(unitPoint, RightBottomPoint, LeftBottomPoint, thresholdWidth);
                    this.StartMovePoint = HitUtil.FindApparentIntersectPoint(this.CenterPoint, unitPoint, this.RightBottomPoint, this.LeftBottomPoint);
                    this.StartMovePosition = NodePosition.RightBottom;
                }
                else if (HitUtil.IsPointInLine(this.LeftBottomPoint, this.LeftTopPoint, unitPoint, thresholdWidth))
                {
                    unitPoint = this.HandleSideBoundError(unitPoint, LeftBottomPoint, LeftTopPoint, thresholdWidth);
                    this.StartMovePoint = HitUtil.FindApparentIntersectPoint(this.CenterPoint, unitPoint, this.LeftBottomPoint, this.LeftTopPoint);
                    this.StartMovePosition = NodePosition.LeftBottom;
                }
                this.OverCutting();
                return true;
            }
            return false;
        }

        public void UpdateStartMovePoint(UnitPoint unitPoint)
        {
            float threshWidth = UCCanvas.GetThresholdWidth();
            if (HitUtil.IsPointInLine(this.LeftTopPoint, this.RightTopPoint, unitPoint, threshWidth))
            {
                this.StartMovePosition = NodePosition.LeftTop;
            }
            if (HitUtil.IsPointInLine(this.RightTopPoint, this.RightBottomPoint, unitPoint, threshWidth))
            {
                this.StartMovePosition = NodePosition.RightTop;
            }
            if (HitUtil.IsPointInLine(this.RightBottomPoint, this.LeftBottomPoint, unitPoint, threshWidth))
            {
                this.StartMovePosition = NodePosition.RightBottom;
            }
            if (HitUtil.IsPointInLine(this.LeftBottomPoint, this.LeftTopPoint, unitPoint, threshWidth))
            {
                this.StartMovePosition = NodePosition.LeftBottom;
            }
        }

        public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint unitPoint, List<IDrawObject> drawObjects, Type[] runningSnapTypes, Type userSnapType)
        {
            float thWidth = UCCanvas.GetThresholdWidth();
            if (runningSnapTypes != null)
            {
                foreach (Type snaptype in runningSnapTypes)
                {
                    if (snaptype == typeof(VertextSnapPoint))
                    {
                        if (HitUtil.CircleHitPoint(this.LeftTopPoint, thWidth, unitPoint))
                            return new VertextSnapPoint(canvas, this, this.LeftTopPoint);
                        if (HitUtil.CircleHitPoint(this.RightTopPoint, thWidth, unitPoint))
                            return new VertextSnapPoint(canvas, this, this.RightTopPoint);
                        if (HitUtil.CircleHitPoint(this.RightBottomPoint, thWidth, unitPoint))
                            return new VertextSnapPoint(canvas, this, this.RightBottomPoint);
                        if (HitUtil.CircleHitPoint(this.LeftBottomPoint, thWidth, unitPoint))
                            return new VertextSnapPoint(canvas, this, this.LeftBottomPoint);
                    }
                    if (snaptype == typeof(MidpointSnapPoint))
                    {
                        UnitPoint p = MidPoint(this.LeftTopPoint, this.RightTopPoint, unitPoint);
                        if (p != UnitPoint.Empty)
                            return new MidpointSnapPoint(canvas, this, p);
                    }
                }
                return null;
            }
            return null;
        }

        private UnitPoint MidPoint(UnitPoint p1, UnitPoint p2, UnitPoint hitPoint)
        {
            UnitPoint midPoint = HitUtil.LineMidpoint(p1, p2);
            if (HitUtil.CircleHitPoint(midPoint, UCCanvas.GetThresholdWidth(), hitPoint))
            {
                return midPoint;
            }
            return UnitPoint.Empty;
        }

        #region 显示附加信息

        public void ShowBoundRect(ICanvas canvas,int penIndex)
        {
            if (GlobalData.Model.AdditionalInfo.Instance.IsShowBoundRect && this.drawStatus == DrawObjectMouseDown.Done)
            {
                RectangleF rectF = this.GetBoundingRectangle();
                canvas.DrawBoundRectangle(canvas, new Pen(Color.Blue), new PointF(rectF.X, rectF.Y+rectF.Height), rectF.Width, rectF.Height);
            }
        }

        public void ShowNotClosedFigure()
        {
            throw new NotImplementedException();
        }

        public void ShowFigureSN(ICanvas canvas)
        {
            if(AdditionalInfo.Instance.IsShowFigureSN&&this.drawStatus==DrawObjectMouseDown.Done)
            {
                canvas.DrawSN(canvas, this.SN.ToString(), this.StartMovePoint);
            }
        }

        public void ShowStartMovePoint(ICanvas canvas)
        {
            if(AdditionalInfo.Instance.IsShowStartMovePoint&& this.drawStatus == DrawObjectMouseDown.Done)
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

        #region IDrawTranslation
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

        public void Mirroring(double centerAxis, int direction)
        {
            if (direction == 0)//水平镜像
            {
                this.FirstPoint = new UnitPoint(2 * centerAxis - this.FirstPoint.X, this.FirstPoint.Y);
                this.SecondPoint = new UnitPoint(2 * centerAxis - this.SecondPoint.X, this.SecondPoint.Y);
                this.StartMovePoint = new UnitPoint(2 * centerAxis - this.StartMovePoint.X, this.StartMovePoint.Y);
            }
            else
            {
                this.FirstPoint = new UnitPoint(this.FirstPoint.X, 2 * centerAxis - this.FirstPoint.Y);
                this.SecondPoint = new UnitPoint(this.SecondPoint.X, 2 * centerAxis - this.SecondPoint.Y);
                this.StartMovePoint = new UnitPoint(this.StartMovePoint.X, 2 * centerAxis - this.StartMovePoint.Y);
            }
            this.UpdateSingleRectangleFromTwoPoints();
            this.UpdateStartMovePoint(this.StartMovePoint);
            this.ReverseDirection();
        }

        public void OverCutting()
        {
            this.OverCuttingPoints = new List<UnitPoint>();
            if (this.OverCuttingLength > 0 && this.OverCuttingLength < this.SizeLength / 3)
            {
                this.OverCuttingPoints.Add(this.StartMovePoint);
                this.CalOverCuttingPoints(this.StartMovePoint, this.MachinePoints[0], this.OverCuttingLength, 0);
            }
            else
            {
                this.ClearOverCutting();
            }
        }

        private void CalOverCuttingPoints(UnitPoint startPoint,UnitPoint p2,double length,int index)
        {
            double distance = HitUtil.Distance(startPoint, p2);
            if (length > distance)//仍需继续计算
            {
                this.OverCuttingPoints.Add(p2);
                length -= distance;
                index += 1;
                this.CalOverCuttingPoints(p2, this.MachinePoints[index], length,index);
            }
            else
            {
                UnitPoint offset = new UnitPoint(p2.X - startPoint.X, p2.Y - startPoint.Y);
                if(offset.X==0)
                {
                    if(p2.Y> startPoint.Y)
                    {
                        this.OverCuttingPoints.Add(new UnitPoint(startPoint.X, startPoint.Y + length));
                    }
                    else
                    {
                        this.OverCuttingPoints.Add(new UnitPoint(startPoint.X, startPoint.Y - length));
                    }
                }
                else
                {
                    if (p2.X > startPoint.X)
                    {
                        this.OverCuttingPoints.Add(new UnitPoint(startPoint.X+length, startPoint.Y ));
                    }
                    else
                    {
                        this.OverCuttingPoints.Add(new UnitPoint(startPoint.X-length, startPoint.Y));
                    }
                }
            }
        }

        private void ClearOverCutting()
        {
            this.OverCuttingPoints.Clear();
            this.OverCuttingLength = 0;
        }

        public void ReverseDirection()
        {
            this.IsClockWiseDirection = !IsClockWiseDirection;
            this.OverCutting();
        }
        #endregion
    }
}
