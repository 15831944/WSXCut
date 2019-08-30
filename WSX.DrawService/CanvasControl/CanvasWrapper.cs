using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.DrawModel;

namespace WSX.DrawService.CanvasControl
{
    public struct CanvasWrapper : ICanvas
    {
        public UCCanvas UCCanvas { get; }
        public CanvasWrapper(UCCanvas ucCanvas)
        {
            this.UCCanvas = ucCanvas;
            this.Graphics = null;
            this.ClientRectangle = new Rectangle();
        }
        public CanvasWrapper(UCCanvas ucCanvas, Graphics graphics, Rectangle rectangle)
        {
            this.UCCanvas = ucCanvas;
            this.Graphics = graphics;
            this.ClientRectangle = rectangle;
        }
        public void Dispose()
        {
            this.Graphics = null;
        }

        #region ICanvas

        public IModel DataModel
        {
            get
            {
                return this.UCCanvas.Model;
            }
        }
        public IDrawObject CurrentObject
        {
            get
            {
                return this.UCCanvas.NewObject;
            }
        }
        public Rectangle ClientRectangle { get; set; }
        public Graphics Graphics { get; private set; }
        
        public void Invalidate()
        {
            this.UCCanvas.DoInvalidate(false);
        }
        public UnitPoint ScreenBottomRightToUnitPoint()
        {
            return this.UCCanvas.ScreenBottomRightToUnitPoint();
        }
        public UnitPoint ScreenTopLeftToUnitPoint()
        {
            return this.UCCanvas.ScreenTopLeftToUnitPoint();
        }
        public PointF ToScreen(UnitPoint unitPoint)
        {
            return this.UCCanvas.ToScreen(unitPoint);
        }
        public float ToScreen(double unitValue)
        {
            return this.UCCanvas.ToScreen(unitValue);
        }
        public double ToUnit(float screenValue)
        {
            return this.UCCanvas.ToUnit(screenValue);
        }
        public UnitPoint ToUnit(PointF screenPoint)
        {
            return this.UCCanvas.ToUnit(screenPoint);
        }

        #region 图形绘制新方法
        public void DrawDot(ICanvas canvas, Brush brush, UnitPoint center, float radius)
        {
            this.UCCanvas.DrawDot(canvas, brush, center, radius);
        }
        public void DrawDot(ICanvas canvas, Pen pen, UnitPoint center, float radius)
        {
            this.UCCanvas.DrawDot(canvas, pen, center, radius);
        }
        public void DrawLine(ICanvas canvas, Pen pen, UnitPoint p1, UnitPoint p2, float scale=0)
        {
            this.UCCanvas.DrawLine(canvas, pen, p1, p2, scale);
        }
        public void DrawArc(ICanvas canvas, Pen pen, UnitPoint center, float radius, float beginAngle, float angle, float scale)
        {
            this.UCCanvas.DrawArc(canvas, pen, center, radius, beginAngle, angle,scale);
        }
        public void DrawBeziers(ICanvas canvas, Pen pen, List<UnitPoint> points, float scale)
        {
            this.UCCanvas.DrawBeziers(canvas, pen, points, scale);
        }


        public void DrawStartDot(ICanvas canvas, Brush brush, UnitPoint startPoint)
        {
            this.UCCanvas.DrawStartDot(canvas, brush, startPoint);
        }
        public void DrawSN(ICanvas canvas, string sn, UnitPoint unitPoint)
        {
            this.UCCanvas.DrawSN(canvas, sn, unitPoint);
        }
        public void DrawBoundRectangle(ICanvas canvas, Pen pen, PointF leftTopPoint, float width, float height)
        {
            this.UCCanvas.DrawBoundRectangle(canvas, pen, leftTopPoint, width, height);
        }
        public void DrawLeadLine(ICanvas canvas, Pen pen, UnitPoint p1, UnitPoint p2, float scale = 0)
        {
            this.UCCanvas.DrawLeadLine(canvas, pen, p1, p2, scale);
        }
        public void DrawMultiLoopFlag(ICanvas canvas, Brush brush, UnitPoint startPoint, string loops)
        {
            this.UCCanvas.DrawMultiLoopFlag(canvas, brush, startPoint, loops);
        }

        #endregion

        #endregion
    }
}
