using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WSX.CommomModel.DrawModel.Compensation
{
    /// <summary>
    /// 补偿参数
    /// </summary>
    [Serializable]
    public class CompensationModel
    {
        /// <summary>
        /// 补偿样式
        /// </summary>
        [XmlAttribute("Style")]
        public CompensationType Style { get; set; }
        /// <summary>
        /// 补偿宽度
        /// </summary>
        [XmlAttribute("Size")]
        public double Size { get; set; }
        /// <summary>
        /// 拐角处理方式，直角-false,圆角-true
        /// </summary>
        [XmlAttribute("Smooth")]
        public bool IsSmooth { get; set; }
    }
}
