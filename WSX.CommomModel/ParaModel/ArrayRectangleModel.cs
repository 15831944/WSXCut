using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 矩形阵列参数
    /// </summary>
    [Serializable]
    public class ArrayRectangleModel
    {
        /// <summary>
        /// 阵列行数
        /// </summary>
        public int RowCount { get; set; }
        /// <summary>
        /// 阵列列数
        /// </summary>
        public int ColumnCount { get; set; }
        /// <summary>
        /// 偏移方式
        /// </summary>
        public ArrayOffsetTypes OffsetType { get; set; }
        /// <summary>
        /// 行间距/行偏移
        /// </summary>
        public double OffsetRow { get; set; }
        /// <summary>
        /// 列间距/列偏移
        /// </summary>
        public double OffsetColumn { get; set; }
        /// <summary>
        /// 行方向
        /// </summary>
        public ArrayRowDirections ArrayRowDirection { get; set; }
        /// <summary>
        /// 列方向
        /// </summary>
        public ArrayColumnDirections ArrayColumnDirection { get; set; }
    }
    /// <summary>
    /// 偏移方式
    /// </summary>
    [Serializable]
    public enum ArrayOffsetTypes
    {
        /// <summary>
        /// 偏移
        /// </summary>
        RowCount,
        /// <summary>
        /// 间距
        /// </summary>
        Spacing
    }
    /// <summary>
    /// 行方向
    /// </summary>
    [Serializable]
    public enum ArrayRowDirections
    {
        /// <summary>
        /// 向上
        /// </summary>
        Top,
        /// <summary>
        /// 向下
        /// </summary>
        Bottom
    }
    /// <summary>
    /// 列方向
    /// </summary>
    [Serializable]
    public enum ArrayColumnDirections
    {
        /// <summary>
        /// 向左
        /// </summary>
        Left,
        /// <summary>
        /// 向右
        /// </summary>
        Right
    }
}
