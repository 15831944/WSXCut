using System;
using System.Xml.Serialization;

namespace WSX.CommomModel.DrawModel.LeadLine
{
    [Serializable]
    public class LeadInLine
    {
        [XmlAttribute("LeadType")]
        public LeadLineType LeadType { get; set; }

        /// <summary>
        /// 在引入线起点添加小圆孔
        /// </summary>
        [XmlAttribute("LeadByHole")]
        public bool LeadByHole { get; set; }
        [XmlAttribute("Position")]
        public float Position { get; set; }
        [XmlAttribute("Angle")]
        public float Angle { get; set; }
        [XmlAttribute("Length")]
        public float Length { get; set; }
        [XmlAttribute("ArcRadius")]
        public float ArcRadius { get; set; }
        [XmlAttribute("LeadHoleRadius")]
        public float LeadHoleRadius { get; set; }
        /// <summary>
        /// 引入点是否冷却
        /// </summary>
        [XmlAttribute("LeadInBreak")]
        public bool LeadInBreak { get; set; }


    }
}
