using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;
using WSX.GlobalData.Model;

namespace WSX.DrawService.DrawTool
{
    public class Line : DrawObjectBase, IDrawObject,IDrawTranslation
    {
        private DrawObjectMouseDown drawStatus = DrawObjectMouseDown.Unknown;

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
                return this.P1;
            }
            set
            {                
            }
        }
       
        public UnitPoint P1 { get; set; }      
        public UnitPoint P2 { get; set; }

        public Line() { this.FigureId = Guid.NewGuid().ToString(); }

        public Line(UnitPoint unitPoint,UnitPoint endPoint, float width,int layerId)
        {
            this.P1 = unitPoint;
            this.P2 = endPoint;
            this.LayerId = layerId;
            this.Width = width;
            this.IsSelected = false;
            this.FigureId = Guid.NewGuid().ToString();
        }
        public virtual void Copy(Line line)
        {
            base.Copy(line);
            this.P1 = line.P1;
            this.P2 = line.P2;
            this.LayerId=line.LayerId;
            this.drawStatus = line.drawStatus;
            this.IsSelected = line.IsSelected;
            this.SN = line.SN;
        }

        public void ExtendLineToPoint(UnitPoint unitPoint)
        {
            UnitPoint newLinePoint = HitUtil.NearestPointOnLine(this.P1, this.P2, unitPoint, true);
            if (HitUtil.Distance(newLinePoint, this.P1) < HitUtil.Distance(newLinePoint, this.P2))
            {
                this.P1 = newLinePoint;
            }
            else
            {
                this.P2 = newLinePoint;
            }
        }
        
        #region DrawObjectBase

        public override void InitializeFromModel(UnitPoint unitPoint,ISnapPoint snapPoint)
        {
            this.P1 = P2 = unitPoint;
            this.LayerId = (int)GlobalModel.CurrentLayerId;
            this.IsSelected = true;
        }

        #endregion

        #region IDrawObject
        public virtual string Id
        {
            get
            {
                return "Line";
            }
        }
        public bool IsSelected { get; set; }
        public int SN { get; set; }
        public UnitPoint RepeatStartingPoint
        {
            get
            {
                return this.P2;
            }
        }

        public string FigureId { get; private set; }

        public double SizeLength
        {
            get
            {
                return HitUtil.Distance(this.P1, this.P2);
            }
        }

        public virtual IDrawObject Clone()
        {
            Line line = new Line();
            line.Copy(this);
            return line;
        }

        public virtual void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            int penIndex= this.LayerId;
            if(AdditionalInfo.Instance.IsShowNotClosedFigureInRed)
            {
                penIndex = 0;
            }
            if (this.IsSelected)
            {
                canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[penIndex], this.P1, this.P2,AdditionalInfo.Instance.IsShowMachinePath);
                if (canvas.DataModel.DrawingPattern == DrawingPattern.NodeEditPattern && this.drawStatus == DrawObjectMouseDown.DoneRepeat)
                {
                    DrawUtils.DrawNode(canvas, this.P1);
                    DrawUtils.DrawNode(canvas, this.P2);
                }
            }
            else
            {
                    canvas.DrawLine(canvas, DrawUtils.CustomPens[penIndex], this.P1, this.P2, AdditionalInfo.Instance.IsShowMachinePath);
            }
            this.ShowBoundRect(canvas,penIndex);
            this.ShowStartMovePoint(canvas);
            this.ShowFigureSN(canvas);
        }

        public RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            return this.GetBoundingRectangle();
        }

        private RectangleF GetBoundingRectangle()
        {
            return ScreenUtils.GetRectangleF(this.P1, this.P2, UCCanvas.GetThresholdWidth());
        }

        public string GetInfoAsString()
        {
            return string.Format("Line@{0},{1} - L={2:f4}>{3:f4}", this.P1.PosAsString(), this.P2.PosAsString(), HitUtil.Distance(this.P1, this.P2), HitUtil.RadiansToDegrees(HitUtil.LineAngleR(this.P1, this.P2, 0)));
        }

        public void Move(UnitPoint offset)
        {
            this.P1 += offset;
            this.P2 += offset;
        }

        public INodePoint NodePoint(UnitPoint unitPoint)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            if(HitUtil.CircleHitPoint(this.P1,thresholdWidth,unitPoint))
            {
                return new NodePointLine(this, NodePointLine.LinePoint.P1);
            }
            if(HitUtil.CircleHitPoint(this.P2,thresholdWidth,unitPoint))
            {
                return new NodePointLine(this, NodePointLine.LinePoint.P2);
            }
            return null;
        }

        public bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            RectangleF boundingRectangleF = this.GetBoundingRectangle(BoundingRectangleType.All);
            if(anyPoint)
            {
                return HitUtil.LineIntersectWithRect(this.P1, this.P2, rectangle);
            }
            return rectangle.Contains(boundingRectangleF);
        }

        public virtual void OnKeyDown(ICanvas canvas, KeyEventArgs e)
        {
            
        }

        public virtual DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint, MouseEventArgs e)
        {
            this.IsSelected = false;
            this.drawStatus = DrawObjectMouseDown.DoneRepeat;
            this.SN = ++GlobalModel.TotalDrawObjectCount;
            if (snapPoint is PerpendicularSnapPoint && snapPoint.Owner is Line)
            {
                Line source = snapPoint.Owner as Line;
                this.P2 = HitUtil.NearestPointOnLine(source.P1, source.P2, this.P1, true);
                return DrawObjectMouseDown.DoneRepeat;
            }
            //if(snapPoint is PerpendicularSnapPoint && snapPoint.Owner is Arc)
            //{
            //    Arc source = snapPoint.Owner as Arc;
            //    this.P2 = HitUtil.NearestPointOnCircle(source.Center, source.Radius, this.P1, 0);
            //    return DrawObjectMouseDown.DoneRepeat;
            //}
            if(Control.ModifierKeys==Keys.Control)
            {
                point = HitUtil.OrthoPointD(this.P1, point, 45);
            }
            this.P2 = point;
            return DrawObjectMouseDown.DoneRepeat;
        }

        public virtual void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {
            if(Control.ModifierKeys==Keys.Control)
            {
                unitPoint = HitUtil.OrthoPointD(this.P1, unitPoint, 45);
            }
            this.P2 = unitPoint;
        }

        public virtual void OnMouseUp(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint)
        {
            
        }

        public bool PointInObject(UnitPoint unitPoint)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            return HitUtil.IsPointInLine(this.P1, this.P2,unitPoint, thresholdWidth);
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
                        if (HitUtil.CircleHitPoint(this.P1, thWidth, unitPoint))
                            return new VertextSnapPoint(canvas, this, this.P1);
                        if (HitUtil.CircleHitPoint(this.P2, thWidth, unitPoint))
                            return new VertextSnapPoint(canvas, this, this.P2);
                    }
                    if (snaptype == typeof(MidpointSnapPoint))
                    {
                        UnitPoint p = MidPoint(canvas, this.P1, this.P2, unitPoint);
                        if (p != UnitPoint.Empty)
                            return new MidpointSnapPoint(canvas, this, p);
                    }
                    if (snaptype == typeof(IntersectSnapPoint))
                    {
                        Line otherline =Utils.Utils.FindObjectTypeInList(this, drawObjects, typeof(Line)) as Line;
                        if (otherline == null)
                            continue;
                        UnitPoint p = HitUtil.LinesIntersectPoint(this.P1, this.P2, otherline.P1, otherline.P2);
                        if (p != UnitPoint.Empty)
                            return new IntersectSnapPoint(canvas, this, p);
                    }
                }
                return null;
            }

            if (userSnapType == typeof(MidpointSnapPoint))
                return new MidpointSnapPoint(canvas, this, HitUtil.LineMidpoint(this.P1, this.P2));
            if (userSnapType == typeof(IntersectSnapPoint))
            {
                Line otherline = Utils.Utils.FindObjectTypeInList(this, drawObjects, typeof(Line)) as Line;
                if (otherline == null)
                    return null;
                UnitPoint p = HitUtil.LinesIntersectPoint(this.P1, this.P2, otherline.P1, otherline.P2);
                if (p != UnitPoint.Empty)
                    return new IntersectSnapPoint(canvas, this, p);
            }
            if (userSnapType == typeof(VertextSnapPoint))
            {
                double d1 = HitUtil.Distance(unitPoint, this.P1);
                double d2 = HitUtil.Distance(unitPoint, this.P2);
                if (d1 <= d2)
                    return new VertextSnapPoint(canvas, this, this.P1);
                return new VertextSnapPoint(canvas, this, this.P2);
            }
            if (userSnapType == typeof(NearestSnapPoint))
            {
                UnitPoint p = HitUtil.NearestPointOnLine(this.P1, this.P2, unitPoint);
                if (p != UnitPoint.Empty)
                    return new NearestSnapPoint(canvas, this, p);
            }
            if (userSnapType == typeof(PerpendicularSnapPoint))
            {
                UnitPoint p = HitUtil.NearestPointOnLine(this.P1, this.P2, unitPoint);
                if (p != UnitPoint.Empty)
                    return new PerpendicularSnapPoint(canvas, this, p);
            }
            return null;
        }

        private UnitPoint MidPoint(ICanvas canvas,UnitPoint P1,UnitPoint P2,UnitPoint hitPoint)
        {
            UnitPoint midPoint = HitUtil.LineMidpoint(P1, P2);
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            if(HitUtil.CircleHitPoint(midPoint,thresholdWidth,hitPoint))
            {
                return midPoint;
            }
            return UnitPoint.Empty;
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
            if (AdditionalInfo.Instance.IsShowBoundRect && drawStatus == DrawObjectMouseDown.DoneRepeat)
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
            if(AdditionalInfo.Instance.IsShowFigureSN && drawStatus == DrawObjectMouseDown.DoneRepeat)
            {
                canvas.DrawSN(canvas, this.SN.ToString(), this.StartMovePoint);
            }
        }

        public void ShowStartMovePoint(ICanvas canvas)
        {
            if(AdditionalInfo.Instance.IsShowStartMovePoint && drawStatus == DrawObjectMouseDown.DoneRepeat)
            {
                canvas.DrawStartDot(canvas, Brushes.Yellow, this.P1);
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
            if(direction==0)//水平镜像
            {
                this.P1 = new UnitPoint(2 * centerAxis - this.P1.X, this.P1.Y);
                this.P2 = new UnitPoint(2 * centerAxis - this.P2.X, this.P2.Y);
            }
            else
            {
                this.P1 = new UnitPoint(this.P1.X, 2 * centerAxis - this.P1.Y);
                this.P2 = new UnitPoint(this.P2.X, 2 * centerAxis - this.P2.Y);
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
            UnitPoint unitPoint = this.P1;
            this.P1 = this.P2;
            this.P2 = unitPoint;
        }

        public void OverCutting()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
