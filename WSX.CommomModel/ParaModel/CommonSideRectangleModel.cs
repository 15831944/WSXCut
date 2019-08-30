using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 矩形共边
    /// </summary>
    [Serializable]
    public  class CommonSideRectangleModel
    {
        /// <summary>
        /// 共边样式
        /// </summary>
        public CommonSideStyles CommonSideStyle { get; set; }
        /// <summary>
        /// 起始位置
        /// </summary>
        public StartPositions StartPostion { get; set; }
        /// <summary>
        /// 是否启用过切
        /// </summary>
        public bool IsOutCut { get; set; }
        /// <summary>
        /// 过切值
        /// </summary>
        public double OutCutValue { get; set; }
    }
    /// <summary>
    /// 共边样式
    /// </summary>
    [Serializable]
    public enum CommonSideStyles
    {
        /// <summary>
        /// 横平竖直
        /// </summary>
        HorizontalsAndVerticals,//
        /// <summary>
        /// 外框优先
        /// </summary>
        FramedPriority,
        /// <summary>
        /// 外框最后
        /// </summary>
        FrameFinal,
        /// <summary>
        /// 逐个蛇形
        /// </summary>
        Serpentine,
        /// <summary>
        /// 逐个阶梯图
        /// </summary>
        Stairs,
        /// <summary>
        /// C字形
        /// </summary>        
        Cglyph,
        /// <summary>
        /// 不规则图形
        /// </summary>
        Irregular,
    }
    /// <summary>
    /// 起始位置
    /// </summary>
    [Serializable]
    public enum StartPositions
    {
        LeftTop,
        LeftBotton,
        RightTop,
        RightBotton,
    }
}
