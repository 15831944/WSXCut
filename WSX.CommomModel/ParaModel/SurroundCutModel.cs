using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 尖角环切
    /// </summary>
    [Serializable]
    public class SurroundCutModel
    {
        /// <summary>
        /// 最大夹角
        /// </summary>
        public double MaxAngle { get; set; }
        /// <summary>
        /// 最短边长
        /// </summary>
        public double MinSideLength { get; set; }
        /// <summary>
        /// 外引长度
        /// </summary>
        public double IntroducedLength { get; set; }
        /// <summary>
        /// 环切线使用扫描方式
        /// </summary>
        public bool IsUseScanType { get; set; }
        /// <summary>
        /// 环切方式
        /// </summary>
        public SurroundCutTypes SurroundCutType { get; set; }
    }
    /// <summary>
    /// 环切方式
    /// </summary>
    [Serializable]
    public enum SurroundCutTypes
    {
        /// <summary>
        /// 自动，阳切外部环切，阴切内部环切
        /// </summary>
        Auto,
        /// <summary>
        /// 外部环切
        /// </summary>
        Outside,
        /// <summary>
        /// 内部环切
        /// </summary>
        Inside,
        /// <summary>
        /// 内外都环切
        /// </summary>
        OutSideAndInside
    }
}
