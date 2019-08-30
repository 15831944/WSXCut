using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 圆弧飞行切割
    /// </summary>
    [Serializable]
    public class ArcFlyingCutModel
    {
        /// <summary>
        /// 飞行连接两圆最大间距
        /// </summary>
        public double MaxConnectSpace { get; set; }
        /// <summary>
        /// 圆弧先排序再飞切
        /// </summary>
        public bool IsFirstSort { get; set; }
        /// <summary>
        /// 按零件飞切
        /// </summary>
        public bool IsFlyingByPart { get; set; }
        /// <summary>
        /// 圆弧飞切排序方式
        /// </summary>
        public ArcFlyingCutSortTypes SortType { get; set; }
    }
    /// <summary>
    /// 圆弧飞切排序
    /// </summary>
    [Serializable]
    public enum ArcFlyingCutSortTypes
    {
        /// <summary>
        /// 局部最短空移
        /// </summary>
        LocalShortestEmptyMove,
        /// <summary>
        /// 从内到外
        /// </summary>
        InsideToOut,
        /// <summary>
        /// 从左到右
        /// </summary>
        LeftToRight,
        /// <summary>
        /// 从右到左
        /// </summary>
        RightToLeft,
        /// <summary>
        /// 从上到下
        /// </summary>
        TopToBottom,
        /// <summary>
        /// 从下到上
        /// </summary>
        BottomToTop,
        /// <summary>
        /// 顺时针
        /// </summary>
        Clockwise,
        /// <summary>
        /// 逆时针
        /// </summary>
        Anticlockwise
    }
}
