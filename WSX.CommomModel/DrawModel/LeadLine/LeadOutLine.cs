using System;
using System.Xml.Serialization;

namespace WSX.CommomModel.DrawModel.LeadLine
{
    [Serializable]
    public class LeadOutLine
    {
        [XmlAttribute("LeadType")]
        public LeadLineType LeadType { get; set; }
        [XmlAttribute("Angle")]
        public float Angle { get; set; }
        [XmlAttribute("Length")]
        public float Length { get; set; }
        [XmlAttribute("ArcRadius")]
        public float ArcRadius { get; set; }
        [XmlAttribute("Position")]
        /// <summary>
        /// 引出线交点所在图形上的百分比
        /// </summary>
        public float Position { get; set; }

        [XmlAttribute("RoundCut")]
        public bool RoundCut { get; set; }

        /// <summary>
        /// 图形过切、多圈、封口、缺口时的长度参数
        /// </summary>
        [XmlAttribute("Pos")]
        public float Pos { get; set; }
    }
}
