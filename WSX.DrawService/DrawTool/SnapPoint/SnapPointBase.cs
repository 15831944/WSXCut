using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool
{
    public class SnapPointBase : ISnapPoint
    {
        protected UnitPoint snapPoint;
        protected RectangleF boundingRectangle;
        protected IDrawObject owner;

        public SnapPointBase(ICanvas canvas,IDrawObject drawObject,UnitPoint snapPoint)
        {
            this.owner = drawObject;
            this.snapPoint = snapPoint;
            float size = (float)canvas.ToUnit(14);
            this.boundingRectangle.X = (float)(this.snapPoint.X - size / 2);
            this.boundingRectangle.Y = (float)(this.snapPoint.Y - size / 2);
            this.boundingRectangle.Width = size;
            this.boundingRectangle.Height = size;
        }

        protected void DrawPoint(ICanvas canvas,Pen pen,Brush brush)
        {
            Rectangle screenRectangle = ScreenUtils.ConvertRectangle(ScreenUtils.ToScreenNormalized(canvas, this.boundingRectangle));
            canvas.Graphics.DrawRectangle(pen, screenRectangle);
            screenRectangle.X++;
            screenRectangle.Y++;
            screenRectangle.Width--;
            screenRectangle.Height--;
            if(brush!=null)
            {
                canvas.Graphics.FillRectangle(brush, screenRectangle);
            }
        }

        #region ISnapPoint
        public IDrawObject Owner
        {
            get
            {
                return this.owner;
            }
        }

        public virtual UnitPoint SnapPoint
        {
            get
            {
                return this.snapPoint;
            }
        }

        public virtual RectangleF BoundingRect
        {
            get
            {
                return this.boundingRectangle;
            }
        }

        public virtual void Draw(ICanvas canvas)
        {
            
        }

        #endregion
    }
}
