using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawModel;
using WSX.DrawService.Utils;
using WSX.GlobalData;
using WSX.GlobalData.Messenger;
using WSX.GlobalData.Model;

namespace WSX.DrawService.DrawTool.Arcs
{
    public class ThreePointsArc : ArcBase
    {
        private ArcCurrentPointType arcCurrentPointType = ArcCurrentPointType.StartPoint;

        #region IDrawObject
        public override IDrawObject Clone()
        {
            ThreePointsArc threePointsArc = new ThreePointsArc();
            threePointsArc.Copy(this);
            return threePointsArc;
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
                canvas.DrawArc(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.Center, this.Radius, this.StartAngle, this.AngleSweep, 0);
                if (this.arcCurrentPointType == ArcCurrentPointType.MidPoint && this.midPoint != UnitPoint.Empty)
                {
                    canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.startPoint, this.midPoint);
                }
                if (this.arcCurrentPointType == ArcCurrentPointType.EndPoint && this.endPoint != UnitPoint.Empty)
                {
                    canvas.DrawLine(canvas, DrawUtils.CustomSelectedPens[this.LayerId], this.midPoint, this.endPoint);
                }
            }
        }

        public override RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            if (this.arcCurrentPointType == ArcCurrentPointType.MidPoint)
            {
                return ScreenUtils.GetRectangleF(this.startPoint, this.midPoint, thresholdWidth);
            }
            else
            {
                return this.GetBoundingRectCommon(thresholdWidth);
            }
        }

        public override DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint, MouseEventArgs e)
        {
            OnMouseMove(canvas, point);
            if (this.arcCurrentPointType == ArcCurrentPointType.StartPoint)
            {
                this.arcCurrentPointType = ArcCurrentPointType.MidPoint;
                this.StartMovePoint = point;
                return DrawObjectMouseDown.Continue;
            }
            if (this.arcCurrentPointType == ArcCurrentPointType.MidPoint)
            {
                if (this.startPoint != point)
                {
                    this.arcCurrentPointType = ArcCurrentPointType.EndPoint;
                }

                return DrawObjectMouseDown.Continue;
            }
            if (this.arcCurrentPointType == ArcCurrentPointType.EndPoint)
            {
                this.arcCurrentPointType = ArcCurrentPointType.Done;
                this.IsSelected = false;
                this.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
                this.GroupParam.ShowSN = this.GroupParam.FigureSN;
                this.IsCompleteDraw = true;
                this.midPoint = HitUtil.PointOnCircle(this.Center, this.Radius, HitUtil.DegreesToRadians(this.MidAngle));
                this.EndMovePoint = this.endPoint;
                return DrawObjectMouseDown.Done;
            }
            return DrawObjectMouseDown.Done;
        }

        public override void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {
            if (this.arcCurrentPointType == ArcCurrentPointType.StartPoint)
            {
                this.startPoint = unitPoint;
                return;
            }
            if (this.arcCurrentPointType == ArcCurrentPointType.MidPoint)
            {
                this.midPoint = unitPoint;
                return;
            }
            if (this.arcCurrentPointType == ArcCurrentPointType.EndPoint)
            {
                this.endPoint = unitPoint;
                this.UpdateArcFrom3Points();
            }
        }
        #endregion       
    }
}
