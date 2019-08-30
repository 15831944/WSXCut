using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 合并相连线
    /// </summary>
    [Serializable]
    public class MergeConnectLineModel
    {
        /// <summary>
        /// 合并精度（最大距离）
        /// </summary>
        public double MaxMergePrecision { get; set; }
        /// <summary>
        /// 合并有效方式
        /// </summary>
        public MergeValidFigureTypes MergeValidFigureType { get; set; }
    }
    /// <summary>
    /// 合并有效方式
    /// </summary>
    [Serializable]
    public enum MergeValidFigureTypes
    {
        /// <summary>
        /// 所有图形
        /// </summary>
        All,
        /// <summary>
        /// 选中图形
        /// </summary>
        Selected,
    }
}
