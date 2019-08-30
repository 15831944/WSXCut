using System;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.FigureModel
{
    /// <summary>
    /// 单点
    /// </summary>
    [Serializable]
    public class PointModel : FigureBaseModel
    {
        [XmlElement("Location")]
        public UnitPoint Point { get; set; }
        public PointModel()
        {
            Type = FigureTypes.Point;
        }
    }
}
