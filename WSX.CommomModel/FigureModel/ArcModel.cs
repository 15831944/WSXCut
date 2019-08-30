using System;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.FigureModel
{
    /// <summary>
    /// 圆弧
    /// </summary>
    [Serializable]
    public class ArcModel : FigureBaseModel
    {
        /// <summary>
        /// 半径
        /// </summary>
        [XmlAttribute("Radius")]
        public double Radius { get; set; }
        /// <summary>
        /// 起始角
        /// </summary>
        [XmlAttribute("StartAngle")]
        public double StartAngle { get; set; }
        /// <summary>
        /// 扫描角
        /// </summary>
        [XmlAttribute("AngleSweep")]
        public float AngleSweep { get; set; }
        /// <summary>
        /// 结束角
        /// </summary>
        [XmlAttribute("EndAngle")]
        public double EndAngle { get; set; }
        /// <summary>
        /// 圆心坐标
        /// </summary>
        public UnitPoint Center { get; set; }
        public ArcModel()
        {
            Type = FigureTypes.Arc;
        }
    }
}
