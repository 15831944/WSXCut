using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 环形阵列
    /// </summary>
    [Serializable]
    public class ArrayAnnularModel
    {
        /// <summary>
        /// 阵列标准
        /// </summary>
        public ArrayStandardTypes ArrayStandardType { get; set; }
        /// <summary>
        /// 角度间距
        /// </summary>
        public double AngleSpace { get; set; }
        /// <summary>
        /// 阵列范围
        /// </summary>
        public double ArrayScope { get; set; }
        /// <summary>
        /// 图形数量
        /// </summary>
        public int FigureCount { get; set; }
        /// <summary>
        /// 是否设置阵列中心范围
        /// </summary>
        public bool IsSetArrayCenterScope { get; set; }
        /// <summary>
        /// 中心圆半径
        /// </summary>
        public double CenterCricleRadius { get; set; }
        /// <summary>
        /// 图形相对中心起始角
        /// </summary>
        public double CenterStartAngle { get; set; }
    }
    [Serializable]
    public enum ArrayStandardTypes
    {
        /// <summary>
        /// 按角度间距
        /// </summary>
        ByAngleSpacing,
        /// <summary>
        /// 按阵列范围
        /// </summary>
        ByArrayScope,
    }
}
