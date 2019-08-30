using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;

namespace WSX.CommomModel.FigureModel
{
    [Serializable]
    public class LwPolylineModel : FigureBaseModel
    {
        /// <summary>
        /// 点数
        /// </summary>
        [XmlAttribute("PointCount")]
        public int PointCount { get { return Points.Count; } set {  } }
        /// <summary>
        /// 起始点
        /// </summary>
        [XmlAttribute("PathStartParm")]
        public float PathStartParm { get; set; }
        /// <summary>
        /// 点集合
        /// </summary>
        public List<UnitPointBulge> Points { get; set; } = new List<UnitPointBulge>();

        public BezierParamModel BezierParam { get; set; }

        public LwPolylineModel()
        {
            Type = FigureTypes.LwPolyline;
        }
    }

}
