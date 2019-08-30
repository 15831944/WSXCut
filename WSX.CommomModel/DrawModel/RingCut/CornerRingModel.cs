using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WSX.CommomModel.DrawModel.RingCut
{
    /// <summary>
    /// 拐角环切参数
    /// </summary>
    [Serializable]
    public class CornerRingModel
    {
        /// <summary>
        /// 环切最大夹角
        /// </summary>
        [XmlAttribute("MaxAngle")]
        public double MaxAngle { get; set; }
        /// <summary>
        /// 最短边长,小于该长度的边不做环切处理
        /// </summary>
        [XmlAttribute("MinLen")]
        public double MinLen { get; set; }
        /// <summary>
        /// 外引长度
        /// </summary>
        [XmlAttribute("Size")]
        public double Size { get; set; }
        /// <summary>
        /// 环切线使用扫描方式
        /// </summary>
        [XmlAttribute("IsScanline")]
        public bool IsScanline { get; set; }
        /// <summary>
        /// 样式,0-自动，根据补偿的方式来确定，没有则使用外部，1-内部，2-外部，3-内外都要
        /// </summary>
        [XmlAttribute("Style")]
        public CornerRingType Style { get; set; }
    }
}
