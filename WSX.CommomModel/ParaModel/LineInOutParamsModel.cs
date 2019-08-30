using System;
using WSX.CommomModel.DrawModel.LeadLine;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 引入引出线参数设置
    /// </summary>
    [Serializable]
    public class LineInOutParamsModel
    {
        /// <summary>
        /// 引入线类型
        /// </summary>
        public LeadLineType LineInType { get; set; }
        /// <summary>
        /// 引入线长度
        /// </summary>
        public float LineInLength { get; set; }
        /// <summary>
        /// 引入线角度(弧度)
        /// </summary>
        public float LineInAngle { get; set; }
        /// <summary>
        /// 引入线半径
        /// </summary>
        public float LineInRadius { get; set; }
        /// <summary>
        /// 引入线是否添加小圆孔
        /// </summary>
        public bool IsAddCircularHole { get; set; }
        /// <summary>
        /// 圆孔半径
        /// </summary>
        public float CircularHoleRadius { get; set; }
        /// <summary>
        /// 
        /// 引出线类型
        /// </summary>
        public LeadLineType LineOutType { get; set; }
        /// <summary>
        /// 引出线长度
        /// </summary>
        public float LineOutLength { get; set; }
        /// <summary>
        /// 引出线角度(弧度)
        /// </summary>
        public float LineOutAngle { get; set; }
        /// <summary>
        /// 引出线半径
        /// </summary>
        public float LineOutRadius { get; set; }
        /// <summary>
        /// 引线位置
        /// </summary>
        public LinePositions LinePosition { get; set; }
        /// <summary>
        /// 图形总长位置设定统一的位置（0-1）
        /// </summary>
        public float FigureTotalLength { get; set; }
        /// <summary>
        /// 优先顶点引入
        /// </summary>
        public bool IsVertexLeadin { get; set; }
        /// <summary>
        /// 优先长边引入
        /// </summary>
        public bool IsLongSideLeadin { get; set; }
        /// <summary>
        /// 仅作用于封闭图形
        /// </summary>
        public bool IsOnlyApplyClosedFigure { get; set; }
        /// <summary>
        /// 仅作用于外膜图形
        /// </summary>
        public bool IsOnlyApplyOutFigure { get; set; }
        /// <summary>
        /// 仅作用于内膜图形
        /// </summary>
        public bool IsOnlyApplyInFigure { get; set; }
    }  
    /// <summary>
    /// 引线位置
    /// </summary>
    [Serializable]
    public enum LinePositions
    {
        /// <summary>
        /// 自动选择合适的位置
        /// </summary>
        AutoSelectSuitable,
        /// <summary>
        /// 按照图形总长设定统一位置
        /// </summary>
        FigureTotalLength,
        /// <summary>
        /// 不改变引线位置，只改变类型
        /// </summary>
        OnlyChangeType,
    }
}
