using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 直线飞切
    /// </summary>
    [Serializable]
    public class LineFlyingCutModel
    {
        /// <summary>
        /// 起刀位置
        /// </summary>
        public StartKnifePostions StartKnifePostion { get; set; }
        /// <summary>
        /// 允许距离偏差
        /// </summary>
        public double DistanceDeviation { get; set; }
        /// <summary>
        /// 光滑连接最大距离
        /// </summary>
        public double MaxConnectDistance { get; set; }
    }
    /// <summary>
    /// 起刀位置
    /// </summary>
    [Serializable]
    public enum StartKnifePostions
    {
        /// <summary>
        /// 左上
        /// </summary>
        LeftTop,
        /// <summary>
        /// 左下
        /// </summary>
        LeftBottom,
        /// <summary>
        /// 右上
        /// </summary>
        RightTop,
        /// <summary>
        /// 右下
        /// </summary>
        RightBottom
    }

}
