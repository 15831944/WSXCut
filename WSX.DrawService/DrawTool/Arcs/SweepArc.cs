using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;
using WSX.GlobalData;
using WSX.GlobalData.Messenger;
using WSX.GlobalData.Model;

namespace WSX.DrawService.DrawTool.Arcs
{
    public class SweepArc : ArcBase
    {
        private ArcCurrentPointType arcCurrentPointType = ArcCurrentPointType.Center;
        #region IDrawObject
        /// <summary>
        /// 传入的半径参数
        /// </summary>
        public float InRadiusValue { get; set; } = float.NaN;
        public override IDrawObject Clone()
        {
            SweepArc sweepArc = new SweepArc();
            sweepArc.Copy(this);
            return sweepArc;
        }

        public override void Copy(DrawObjectBase drawObjectBase)
        {
            base.Copy(drawObjectBase);
        }
        public override void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            if (this.IsCompleteDraw || this.IsEditMode)
            {
                base.Draw(canvas, unitRectangle);
            }
            else
            {
                canvas.DrawArc(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.Center, this.Radius, this.StartAngle, this.AngleSweep);
                if (this.arcCurrentPointType == ArcCurrentPointType.StartPoint && this.startPoint != UnitPoint.Empty)
                {
                    canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.Center, this.startPoint);
                }
                if (this.arcCurrentPointType == ArcCurrentPointType.EndPoint && this.endPoint != UnitPoint.Empty)
                {
                    canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.Center, this.startPoint);
                    canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.Center, this.endPoint);
                }
            }
        }

        public override RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            if (this.IsCompleteDraw || this.IsEditMode)
            {
                return this.GetBoundingRectCommon(thresholdWidth);
            }
            else
            {
                return HitUtil.CircleBoundingRectangle(this.Center, this.Radius + thresholdWidth / 2);
            }
        }

        public override DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint, MouseEventArgs e)
        {
            OnMouseMove(canvas, point);
            if (this.arcCurrentPointType == ArcCurrentPointType.Center)
            {
                this.arcCurrentPointType = ArcCurrentPointType.StartPoint;


                return DrawObjectMouseDown.Continue;
            }
            if (this.arcCurrentPointType == ArcCurrentPointType.StartPoint)
            {
                this.arcCurrentPointType = ArcCurrentPointType.EndPoint;
                this.StartMovePoint = startPoint;
                return DrawObjectMouseDown.Continue;
            }
            if (this.arcCurrentPointType == ArcCurrentPointType.EndPoint)
            {
                this.arcCurrentPointType = ArcCurrentPointType.Done;
                this.midPoint = new UnitPoint(this.Radius * Math.Cos(HitUtil.DegreesToRadians(this.StartAngle + AngleSweep / 2)) + this.Center.X, this.Radius * Math.Sin(HitUtil.DegreesToRadians(this.StartAngle + AngleSweep / 2)) + this.Center.Y);
                this.IsSelected = false;
                this.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
                this.GroupParam.ShowSN = this.GroupParam.FigureSN;
                this.IsCompleteDraw = true;
                this.EndMovePoint = this.endPoint;
                return DrawObjectMouseDown.Done;
            }
            return DrawObjectMouseDown.Done;
        }

        public override void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {
            if (this.arcCurrentPointType == ArcCurrentPointType.Center)
            {
                this.Center = unitPoint;
                return;
            }
            if (this.arcCurrentPointType == ArcCurrentPointType.StartPoint)
            {
                this.StartAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(this.Center, unitPoint, 0));
                this.AngleSweep = 360;
                if (float.IsNaN(this.InRadiusValue))
                {
                    this.Radius = (float)HitUtil.Distance(this.Center, unitPoint);
                    this.startPoint = unitPoint;
                }
                else
                {
                    this.Radius = InRadiusValue;
                    this.startPoint = new UnitPoint(this.Radius * Math.Cos(HitUtil.DegreesToRadians(StartAngle)) + this.Center.X, this.Radius * Math.Sin(HitUtil.DegreesToRadians(StartAngle)) + this.Center.Y);
                }
                return;
            }
            if (this.arcCurrentPointType == ArcCurrentPointType.EndPoint)
            {
                float endAngle = (float)HitUtil.RadiansToDegrees(HitUtil.LineAngleR(this.Center, unitPoint, 0));
                this.AngleSweep = endAngle - this.StartAngle;
                this.AngleSweep = this.AngleSweep > 0 ? this.AngleSweep : this.AngleSweep + 360;
                this.endPoint = new UnitPoint(this.Radius * Math.Cos(HitUtil.DegreesToRadians(endAngle)) + this.Center.X, this.Radius * Math.Sin(HitUtil.DegreesToRadians(endAngle)) + this.Center.Y);
                return;
            }
        }



        #endregion
    }
}
