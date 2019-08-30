using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.DrawModel
{
    public enum FigureTypes
    {
        /// <summary>
        /// 单点
        /// </summary>
        Point,
        /// <summary>
        /// 直线
        /// </summary>
        //Line,
        /// <summary>
        /// 圆弧
        /// </summary>
        Arc,
        /// <summary>
        /// 圆
        /// </summary>
        Circle,
        /// <summary>
        /// 多线段
        /// </summary>
        LwPolyline,
        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        PolyBezier,
        /// <summary>
        /// 椭圆
        /// </summary>
        Ellipse,
        /// <summary>
        /// 群组
        /// </summary>
        Group,
    }
}
