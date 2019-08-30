using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 停靠
    /// </summary>
    [Serializable]
    public class DockPositionModel
    {
        /// <summary>
        /// 停靠方式
        /// </summary>
        public DockTypes DockType { get; set; }
        /// <summary>
        /// 相对位置
        /// </summary>
        public RelativePositions RelativePosition { get; set; }
        /// <summary>
        /// 排除不加工图形
        /// </summary>
        public bool IsExcludeUnprocessedFigure { get; set; }
        /// <summary>
        /// 应用于所有已排板材
        /// </summary>
        public bool IsApplyToAllPlates { get; set; }
    }
    /// <summary>
    /// 停靠方式
    /// </summary>
    [Serializable]
    public enum DockTypes
    {
        /// <summary>
        /// 相对
        /// </summary>
        Relative,
        /// <summary>
        /// 绝对
        /// </summary>
        Absolute
    }
    /// <summary>
    /// 相对位置
    /// </summary>
    [Serializable]
    public enum RelativePositions
    {
        /// <summary>
        /// 左上
        /// </summary>
        LeftTop,
        /// <summary>
        /// 左
        /// </summary>
        Left,
        /// <summary>
        /// 左下
        /// </summary>
        LeftBottom,
        /// <summary>
        /// 上
        /// </summary>
        Top,
        /// <summary>
        /// 中
        /// </summary>
        Middle,
        /// <summary>
        /// 下
        /// </summary>
        Bottom,
        /// <summary>
        /// 右上
        /// </summary>
        RightTop,
        /// <summary>
        /// 右
        /// </summary>
        Right,
        /// <summary>
        /// 右下
        /// </summary>
        RightBottom
    }
}
