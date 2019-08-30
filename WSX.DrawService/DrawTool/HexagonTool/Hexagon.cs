using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Layers;
using WSX.DrawService.Utils;
using WSX.GlobalData.Model;

namespace WSX.DrawService.DrawTool
{
    public class Hexagon : DrawObjectBase, IDrawObject, IDrawTranslation
    {
        private UnitPoint centerPoint, startMovePoint = UnitPoint.Empty;
        private UnitPoint[] hexagonPoints;
        public enum CurrentPoint { CenterPoint, StartPoint };
        public CurrentPoint currentPoint = CurrentPoint.CenterPoint;
        public int StartMovePointIndex { get; set; }
        public double OverCuttingLength { get; set; }
        public List<UnitPoint> OverCuttingPoints { get; set; }
        private DrawObjectMouseDown drawStatus = DrawObjectMouseDown.Unknown;
        public static string ObjectType
        {
            get
            {
                return "Hexagon";
            }
        }

        public List<UnitPoint> MachinePoints
        {
            get
            {
                List<UnitPoint> machinePoints = new List<UnitPoint>();
                machinePoints.Add(this.startMovePoint);
                for (int i = 0; i < this.hexagonPoints.Length; i++)
                {
                    if (this.StartMovePointIndex + i + 1 < this.hexagonPoints.Length)
                    {
                        machinePoints.Add(this.hexagonPoints[this.StartMovePointIndex + i + 1]);
                    }
                    else
                    {
                        machinePoints.Add(this.hexagonPoints[this.StartMovePointIndex + i + 1 - this.hexagonPoints.Length]);
                    }
                }
                return machinePoints;
            }
        }

        public double GetRadius(UnitPoint otherPoint)
        {
            return HitUtil.Distance(this.centerPoint, otherPoint, true);
        }

        public bool IsCloseFigure
        {
            get
            {
                return true;
            }
        }
        public UnitPoint StartMovePoint
        {
            get { return this.startMovePoint; }
            set { this.startMovePoint = value; }
        }
        /// <summary>
        /// 多边形中心点坐标
        /// </summary>
        
        public UnitPoint CenterPoint
        {
            get
            {
                return this.centerPoint;
            }
            set
            {
                this.centerPoint = value;
            }
        }

        public int SideCount { get; set; } = 6;

        public float stepAngle
        {
            get
            {
                return 360.0f / this.SideCount;
            }
        }
        
        public UnitPoint[] HexagonPoints
        {
            get
            {
                return this.hexagonPoints;
            }
            set
            {
                this.hexagonPoints = value;
            }
        }

        public Hexagon()
        {
            this.FigureId = Guid.NewGuid().ToString();
            hexagonPoints = new UnitPoint[this.SideCount];
        }

        public Hexagon(UnitPoint[] hexagonPoints, float width, Color color)
        {
            this.hexagonPoints = hexagonPoints;
            this.IsSelected = false;
            this.FigureId = Guid.NewGuid().ToString();
        }

        public virtual void Copy(Hexagon hexagon)
        {
            base.Copy(hexagon);
            this.SideCount = hexagon.SideCount;
            this.centerPoint = hexagon.centerPoint;
            this.hexagonPoints = new UnitPoint[this.SideCount];
            Array.Copy(hexagon.hexagonPoints, this.hexagonPoints, this.hexagonPoints.Length);
            this.centerPoint = hexagon.centerPoint;
            this.IsSelected = hexagon.IsSelected;
            this.startMovePoint = hexagon.startMovePoint;
            hexagon.StartMovePointIndex = hexagon.StartMovePointIndex;
            this.OverCuttingLength = hexagon.OverCuttingLength;
            this.OverCuttingPoints = hexagon.OverCuttingPoints;
            this.LayerId = hexagon.LayerId;
            this.drawStatus = hexagon.drawStatus;
            this.SN = hexagon.SN;
        }

        #region DrawObjectBase

        public override void InitializeFromModel(UnitPoint unitPoint, ISnapPoint snapPoint)
        {
            this.LayerId = (int)GlobalModel.CurrentLayerId;
            this.centerPoint = this.hexagonPoints[0] = unitPoint;
            this.CalculateKeyPoints();
            this.OnMouseDown(null, unitPoint, snapPoint, null);
            this.IsSelected = true;
        }

        private void CalculateKeyPoints()
        {
            double startAngle = HitUtil.LineAngleR(this.centerPoint, this.hexagonPoints[0], 0);
            double radius = this.GetRadius(this.hexagonPoints[0]);
            for (int i = 1; i < this.SideCount; i++)
            {
                UnitPoint vertexPoint = HitUtil.PointOnCircle(this.centerPoint, radius, startAngle + this.stepAngle * Math.PI / 180 * i);
                this.hexagonPoints[i] = vertexPoint;
            }
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
        public double SizeLength
        {
            get
            {
                return this.SideCount * HitUtil.Distance(this.hexagonPoints[0], this.hexagonPoints[1]);
            }
        }

        public UnitPoint RepeatStartingPoint
        {
            get
            {
                return UnitPoint.Empty;
            }
        }

        public virtual IDrawObject Clone()
        {
            Hexagon hexagon = new Hexagon();
            hexagon.Copy(this);
            return hexagon;
        }

        public void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            if (this.IsSelected)
            {
                canvas.DrawHexagon(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.hexagonPoints);
                if (this.OverCuttingLength != 0)
                {
                    for (int i = 0; i < this.OverCuttingPoints.Count - 1; i++)
                    {
                        canvas.DrawLine(canvas, DrawUtils.SelectecedOverCuttingPen, this.OverCuttingPoints[i], this.OverCuttingPoints[i + 1]);
                    }
                }
                if (canvas.DataModel.DrawingPattern == DrawingPattern.NodeEditPattern && this.drawStatus == DrawObjectMouseDown.Done)
                {
                    for (int i = 0; i < this.SideCount; i++)
                    {
                        DrawUtils.DrawNode(canvas, this.hexagonPoints[i]);
                    }
                }
            }
            else
            {
                canvas.DrawHexagon(canvas, DrawUtils.CustomPens[this.LayerId], this.hexagonPoints);
                Pen pen1 = new Pen(this.Color, 3f);
                if (this.OverCuttingLength != 0)
                {
                    for (int i = 0; i < this.OverCuttingPoints.Count - 1; i++)
                    {
                        canvas.DrawLine(canvas, pen1, this.OverCuttingPoints[i], this.OverCuttingPoints[i + 1]);
                    }
                }
            }
            this.ShowStartMovePoint(canvas);
            this.ShowFigureSN(canvas);
        }

        public RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            RectangleF rectangleF = this.GetBoundingRectangle();
            return rectangleF;
        }

        private RectangleF GetBoundingRectangle()
        {
            float maxX = (float)this.hexagonPoints.Max<UnitPoint>(maxx => maxx.X);
            float maxY = (float)this.hexagonPoints.Max<UnitPoint>(maxy => maxy.Y);
            float minX = (float)this.hexagonPoints.Min<UnitPoint>(minx => minx.X);
            float minY = (float)this.hexagonPoints.Min<UnitPoint>(miny => miny.Y);
            RectangleF rectangleF = ScreenUtils.GetRectangleF(new UnitPoint(minX, minY), new UnitPoint(maxX, maxY), UCCanvas.GetThresholdWidth());
            return rectangleF;
        }

        public virtual string GetInfoAsString()
        {
            return string.Format("Hexagon@{0},r={1:f4},P1={2:f3}", this.centerPoint.PosAsString(), this.GetRadius(this.hexagonPoints[0]), this.hexagonPoints[0]);
        }

        public virtual void Move(UnitPoint offset)
        {
            this.centerPoint += offset;
            this.startMovePoint += offset;
            for (int i = 0; i < this.SideCount; i++)
            {
                this.hexagonPoints[i].X += offset.X;
                this.hexagonPoints[i].Y += offset.Y;
            }
            this.OverCutting();
        }

        public INodePoint NodePoint(UnitPoint unitPoint)
        {
            float thWidth = UCCanvas.GetThresholdWidth();
            for (int i = 0; i < this.SideCount; i++)
            {
                if (HitUtil.PointInPoint(this.hexagonPoints[i], unitPoint, thWidth))
                {
                    return new NodeHexagon(this, i);
                }
            }
            return null;
        }

        public bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.All);
            if (anyPoint)//包含图形的任一点即可
            {
                if (HitUtil.LineIntersectWithRect(this.hexagonPoints[this.SideCount - 1], this.hexagonPoints[0], rectangle))
                    return true;
                for (int i = 0; i < this.hexagonPoints.Length - 1; i++)
                {
                    if (HitUtil.LineIntersectWithRect(this.hexagonPoints[i], this.hexagonPoints[i + 1], rectangle))
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
            if (this.currentPoint == CurrentPoint.CenterPoint)
            {
                this.currentPoint = CurrentPoint.StartPoint;
                return DrawObjectMouseDown.Continue;
            }
            if (this.currentPoint == CurrentPoint.StartPoint)
            {
                //OnMouseMove(canvas, point);
                this.IsSelected = false;
                this.startMovePoint = this.hexagonPoints[0];
                this.drawStatus = DrawObjectMouseDown.Done;
                this.SN = ++GlobalModel.TotalDrawObjectCount;
                return DrawObjectMouseDown.Done;
            }
            return DrawObjectMouseDown.Done;
        }

        public void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {
            if (this.currentPoint == CurrentPoint.CenterPoint)
            {
                this.centerPoint = unitPoint;
                return;
            }
            if (this.currentPoint == CurrentPoint.StartPoint)
            {
                this.hexagonPoints[0] = unitPoint;
                this.CalculateKeyPoints();
            }
        }

        public void OnMouseUp(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint)
        {

        }

        public bool PointInObject(UnitPoint unitPoint)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            for (int i = 0; i < this.SideCount - 1; i++)
            {
                if (HitUtil.IsPointInLine(this.hexagonPoints[i], this.hexagonPoints[i + 1], unitPoint, thresholdWidth))
                {
                    return true;
                }
            }
            if (HitUtil.IsPointInLine(this.hexagonPoints[0], this.hexagonPoints[SideCount - 1], unitPoint, thresholdWidth))
            {
                return true;
            }
            return false;
        }

        public bool PointInObject2(ref UnitPoint unitPoint)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            for (int i = 0; i < this.SideCount - 1; i++)
            {
                if (HitUtil.IsPointInLine(this.hexagonPoints[i], this.hexagonPoints[i + 1], unitPoint, thresholdWidth))
                {
                    this.StartMovePointIndex = i;
                    unitPoint = HandleSideBoundError(unitPoint, this.hexagonPoints[i], this.hexagonPoints[i + 1],thresholdWidth);
                    return true;
                }
            }
            if (HitUtil.IsPointInLine(this.hexagonPoints[0], this.hexagonPoints[SideCount - 1], unitPoint, thresholdWidth))
            {
                this.StartMovePointIndex = this.SideCount - 1;
                unitPoint = HandleSideBoundError(unitPoint, this.hexagonPoints[0], this.hexagonPoints[SideCount - 1], thresholdWidth);
                return true;
            }
            return false;
        }

        private UnitPoint HandleSideBoundError(UnitPoint mousePoint, UnitPoint unitPoint1,UnitPoint unitPoint2, float threshWidth)
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

        public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint unitPoint, List<IDrawObject> drawObjects, Type[] runningSnapTypes, Type userSnapType)
        {
            return null;
        }

        public bool SetStartMovePoint(UnitPoint unitPoint)
        {
            if (this.PointInObject2(ref unitPoint))
            {
                if (this.StartMovePointIndex != this.SideCount - 1)
                {
                    this.startMovePoint = HitUtil.FindApparentIntersectPoint(this.centerPoint, unitPoint, this.hexagonPoints[this.StartMovePointIndex], this.hexagonPoints[this.StartMovePointIndex + 1]);
                }
                else
                {
                    this.startMovePoint = HitUtil.FindApparentIntersectPoint(this.centerPoint, unitPoint, this.hexagonPoints[this.StartMovePointIndex], this.hexagonPoints[0]);
                }
                this.OverCutting();
                return true;
            }
            return false;
        }

        #region 显示附加信息

        public void ShowBoundRect(ICanvas canvas,int penIndex)
        {
            throw new NotImplementedException();
        }

        public void ShowNotClosedFigure()
        {
            throw new NotImplementedException();
        }

        public void ShowFigureSN(ICanvas canvas)
        {
            if(AdditionalInfo.Instance.IsShowFigureSN&& this.drawStatus == DrawObjectMouseDown.Done)
            {
                canvas.DrawSN(canvas,this.SN.ToString(),this.startMovePoint);
            }
        }

        public void ShowStartMovePoint(ICanvas canvas)
        {
            if (this.drawStatus==DrawObjectMouseDown.Done&&AdditionalInfo.Instance.IsShowStartMovePoint)
            {
                canvas.DrawStartDot(canvas, Brushes.Yellow, this.startMovePoint);
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
                this.centerPoint = new UnitPoint(2 * centerAxis - this.centerPoint.X, this.centerPoint.Y);
                for (int i = 0; i < this.SideCount; i++)
                {
                    this.hexagonPoints[i] = new UnitPoint(2 * centerAxis - this.hexagonPoints[i].X, this.hexagonPoints[i].Y);
                }
                this.startMovePoint = new UnitPoint(2 * centerAxis - this.startMovePoint.X, this.startMovePoint.Y);
            }
            else
            {
                this.centerPoint = new UnitPoint(this.centerPoint.X, 2 * centerAxis - this.centerPoint.Y);
                for (int i = 0; i < this.SideCount; i++)
                {
                    this.hexagonPoints[i] = new UnitPoint(this.hexagonPoints[i].X, 2 * centerAxis - this.hexagonPoints[i].Y);
                }
                this.startMovePoint = new UnitPoint(this.startMovePoint.X, 2 * centerAxis - this.startMovePoint.Y);
            }
            this.OverCutting();
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

        public void ReverseDirection()
        {
            this.StartMovePointIndex = this.hexagonPoints.Length - 1 - this.StartMovePointIndex;
            UnitPoint[] temp = new UnitPoint[this.hexagonPoints.Length];
            temp[0] = this.hexagonPoints[0];
            for (int i = 1; i < this.hexagonPoints.Length; i++)
            {
                temp[i] = this.hexagonPoints[this.hexagonPoints.Length - i];
            }
            this.hexagonPoints = temp;
            this.OverCutting();
        }

        public void OverCutting()
        {
            this.OverCuttingPoints = new List<UnitPoint>();
            if (this.OverCuttingLength > 0 && this.OverCuttingLength < this.SizeLength / 3)
            {
                this.OverCuttingPoints.Add(this.startMovePoint);
                this.CalOverCuttingPoints(this.startMovePoint, this.MachinePoints[0], this.OverCuttingLength, 0);
            }
            else
            {
                this.ClearOverCutting();
            }
        }

        public void CalOverCuttingPoints(UnitPoint startPoint, UnitPoint p2, double length, int index)
        {
            double distance = HitUtil.Distance(startPoint, p2);
            if (length > distance)
            {
                this.OverCuttingPoints.Add(p2);
                length -= distance;
                index += 1;
                this.CalOverCuttingPoints(p2, this.MachinePoints[index], length, index);
            }
            else
            {
                double angle = HitUtil.LineAngleR(startPoint, p2, 0);
                UnitPoint unitPoint = HitUtil.PointOnCircle(startPoint, length, angle);
                this.OverCuttingPoints.Add(unitPoint);
            }
        }

        private void ClearOverCutting()
        {
            this.OverCuttingPoints.Clear();
            this.OverCuttingLength = 0;
        }
        #endregion
    }
}
