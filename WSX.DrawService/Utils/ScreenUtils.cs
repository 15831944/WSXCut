using System;
using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    /// <summary>
    /// 屏幕坐标和世界坐标转换工具类
    /// </summary>
    public class ScreenUtils
    {
        /// <summary>
        /// 获取单位化矩形的右上角坐标
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="unitRectangle"></param>
        /// <returns></returns>
        public static PointF RightPoint(ICanvas canvas,RectangleF unitRectangle)
        {
            PointF leftPoint = unitRectangle.Location;
            float x = leftPoint.X + unitRectangle.Width;
            float y = leftPoint.Y + unitRectangle.Height;
            return new PointF(x, y);
        }

        /// <summary>
        /// 获取单位化矩形为屏幕图形坐标
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="unitRectangle"></param>
        /// <returns></returns>
        public static RectangleF ToScreen(ICanvas canvas,RectangleF unitRectangle)
        {
            RectangleF rectangleF = new RectangleF();
            rectangleF.Location = canvas.ToScreen(new UnitPoint(unitRectangle.Location));
            rectangleF.Width = (float)Math.Round(canvas.ToScreen(unitRectangle.Width));
            rectangleF.Height = (float)Math.Round(canvas.ToScreen(unitRectangle.Height));
            return rectangleF;
        }

        /// <summary>
        /// 屏幕坐标矩形转换为单位坐标矩形
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="screenRectangle"></param>
        /// <returns></returns>
        public static RectangleF ToUnit(ICanvas canvas,Rectangle screenRectangle)
        {
            UnitPoint unitPoint = canvas.ToUnit(screenRectangle.Location);
            SizeF sizeF = new SizeF((float)canvas.ToUnit(screenRectangle.Width), (float)canvas.ToUnit(screenRectangle.Height));
            RectangleF unitRectangle = new RectangleF(unitPoint.Point, sizeF);
            return unitRectangle;
        }

        public static RectangleF ToScreenNormalized(ICanvas canvas,RectangleF unitRectangle)
        {
            RectangleF rectangleF = ToScreen(canvas, unitRectangle);
            rectangleF.Y = rectangleF.Y - rectangleF.Height;
            return rectangleF;
        }

        public static RectangleF ToUnitNormalized(ICanvas canvas,Rectangle screenRectangle)
        {
            UnitPoint unitPoint = canvas.ToUnit(screenRectangle.Location);
            SizeF sizeF = new SizeF((float)canvas.ToUnit(screenRectangle.Width), (float)canvas.ToUnit(screenRectangle.Height));
            RectangleF unitRectangle = new RectangleF(unitPoint.Point, sizeF);
            unitRectangle.Y -= unitRectangle.Height;
            return unitRectangle;
        }

        public static Rectangle ConvertRectangle(RectangleF rectangleF)
        {
            return new Rectangle((int)rectangleF.Left, (int)rectangleF.Top, (int)rectangleF.Width, (int)rectangleF.Height);
        }

        public static RectangleF GetRectangleF(UnitPoint p1,UnitPoint p2,double width)
        {
            double x = Math.Min(p1.X, p2.X);
            double y = Math.Min(p1.Y, p2.Y);
            double w = Math.Abs(p1.X - p2.X);
            double height = Math.Abs(p1.Y - p2.Y);
            RectangleF rectangleF = GetRectangleF(x, y, w, height);
            rectangleF.Inflate((float)width, (float)width);
            return rectangleF;
        }

        public static RectangleF GetRectangleF(double x,double y,double width,double height)
        {
            return new RectangleF((float)x, (float)y, (float)width, (float)height);
        }

        public static Point ConvertPointF(PointF pointF)
        {
            return new Point((int)pointF.X, (int)pointF.Y);
        }
    }
}
