using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 圆形孔替换为孤立点
    /// </summary>
    [Serializable]
    public class CircleReplaceToAcnodeModel
    {
        /// <summary>
        /// 圆形孔尺寸
        /// </summary>
        public double CircleSize { get; set; }
        /// <summary>
        /// 圆形孔尺寸类型
        /// </summary>
        public CircleSizeTypes CircleSizeType { get; set; }
        /// <summary>
        /// 替换所有小于给定尺寸的圆孔
        /// </summary>
        public bool IsReplaceAll { get; set; }
        /// <summary>
        /// 在选中的图形中查找和替换
        /// </summary>
        public bool IsReplaceSelected { get; set; }
    }
    /// <summary>
    /// 圆形孔尺寸类型
    /// </summary>
    [Serializable]
    public enum CircleSizeTypes
    {
        /// <summary>
        /// 半径
        /// </summary>
        Radius,
        /// <summary>
        /// 直径
        /// </summary>
        Diameter,
    }
}
