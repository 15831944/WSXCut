using System;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;

namespace WSX.CommomModel.FigureModel
{
    /// <summary>
    /// 圆
    /// </summary>
    [Serializable]
    public class CircleModel  : FigureBaseModel
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
        /// 圆心坐标
        /// </summary>
        public UnitPoint Center { get; set; }
        public CircleModel()
        {
            Type = FigureTypes.Circle;
        }

        /// <summary>
        /// 飞切参数
        /// </summary>
        public ArcFlyingLeadOut FlyingCutLeadOut { get; set; }

        /// <summary>
        /// 是否打散了飞切
        /// </summary>
        [XmlAttribute("IsFlyingCutScatter")]
        public bool IsFlyingCutScatter { get; set; }

        /// <summary>
        /// 是否做了飞切
        /// </summary>
        [XmlAttribute("IsFlyingCut")]
        public bool IsFlyingCut { get; set; }
    }
}
