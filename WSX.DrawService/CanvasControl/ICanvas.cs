using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.DrawModel;

namespace WSX.DrawService.CanvasControl
{
    public interface ICanvas
    {
        IModel DataModel { get; }

        /// <summary>
        /// 屏幕左上角转换位单位点坐标
        /// </summary>
        /// <returns></returns> 
        UnitPoint ScreenTopLeftToUnitPoint();

        /// <summary>
        /// 屏幕右下角转换位单位点坐标
        /// </summary>
        /// <returns></returns>
        UnitPoint ScreenBottomRightToUnitPoint();

        /// <summary>
        /// 单位点转换位屏幕上点坐标
        /// </summary>
        /// <param name="unitPoint"></param>
        /// <returns></returns>
        PointF ToScreen(UnitPoint unitPoint);

        /// <summary>
        /// 单位值转换为屏幕值
        /// </summary>
        /// <param name="unitValue"></param>
        /// <returns></returns>
        float ToScreen(double unitValue);

        /// <summary>
        /// 屏幕坐标转换为单位坐标
        /// </summary>
        /// <param name="screenValue"></param>
        /// <returns></returns>
        double ToUnit(float screenValue);

        UnitPoint ToUnit(PointF screenPoint);

        /// <summary>
        /// 使绘图区域无效
        /// </summary>
        void Invalidate();

        /// <summary>
        /// 获取当前图形对象
        /// </summary>
        IDrawObject CurrentObject { get; }

        /// <summary>
        /// 绘图矩形区域
        /// </summary>
        Rectangle ClientRectangle { get; }

        Graphics Graphics { get; }       

        #region 图形绘制新方法

        void DrawDot(ICanvas canvas, Brush brush, UnitPoint center, float radius);
        void DrawDot(ICanvas canvas, Pen pen, UnitPoint center, float radius);
        void DrawLine(ICanvas canvas, Pen pen, UnitPoint p1, UnitPoint p2, float scale = 0);
        void DrawArc(ICanvas canvas, Pen pen, UnitPoint center, float radius, float startAngle, float sweepAngle, float scale = 0);
        void DrawBeziers(ICanvas canvas, Pen pen, List<UnitPoint> points, float scale = 0);


        void DrawStartDot(ICanvas canvas, Brush brush, UnitPoint startPoint);
        void DrawSN(ICanvas canvas, string sn, UnitPoint unitPoint);
        void DrawBoundRectangle(ICanvas canvas, Pen pen, PointF leftTopPoint, float width, float height);
        void DrawLeadLine(ICanvas canvas, Pen pen, UnitPoint p1, UnitPoint p2, float scale = 0);
        void DrawMultiLoopFlag(ICanvas canvas, Brush brush, UnitPoint startPoint, string loops);

        #endregion
    }
}
