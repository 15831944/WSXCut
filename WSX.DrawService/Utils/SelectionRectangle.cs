using System;
using System.Drawing;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class SelectionRectangle
    {
        private PointF p1;
        private PointF p2;

        public SelectionRectangle(PointF mouseDownPoint)
        {
            this.p1 = mouseDownPoint;
            this.p2 = PointF.Empty;
        }

        public void Reset()
        {
            this.p2 = Point.Empty;
        }

        public void SetMousePoint(Graphics graphics,PointF mousePoint)
        {
            if(this.p2!=Point.Empty)
            {
                XorGDI.DrawRectangle(graphics, PenStyles.PS_DOT, 1, this.GetColor(), this.p1, this.p2);
            }
            this.p2 = mousePoint;
            XorGDI.DrawRectangle(graphics, PenStyles.PS_DOT, 1, this.GetColor(), this.p1, this.p2);
        }

        public Rectangle ScreenRect()
        {
            float x = Math.Min(this.p1.X, this.p2.X);
            float y = Math.Min(this.p1.Y, this.p2.Y);
            float w = Math.Abs(this.p1.X - this.p2.X);
            float h = Math.Abs(this.p1.Y - this.p2.Y);
            if (this.p2 == PointF.Empty)
                return Rectangle.Empty;
            if (w < 4 || h < 4) // if no selection was made return empty rectangle (giving a 4 pixel threshold)
                return Rectangle.Empty;
            return new Rectangle((int)x, (int)y, (int)w, (int)h);
        }

        public RectangleF Selection(ICanvas canvas)
        {
            Rectangle screenRect = ScreenRect();
            if (screenRect.IsEmpty)
                return RectangleF.Empty;
            return ScreenUtils.ToUnitNormalized(canvas, screenRect);
        }

        public bool AnyPoint()
        {
            return (this.p1.X > this.p2.X);
        }

        private Color GetColor()
        {
            if(this.AnyPoint())
            {
                return Color.Blue;
            }
            return Color.Green;
        }
    }
}
