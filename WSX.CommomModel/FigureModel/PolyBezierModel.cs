using System;
using System.Collections.Generic;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.FigureModel
{
    /// <summary>
    /// 贝塞尔曲线
    /// </summary>
    [Serializable]
    public class PolyBezierModel : FigureBaseModel
    {
        /// <summary>
        /// 曲线点集合
        /// </summary>
        public List<UnitPointBulge> Points { get; set; } = new List<UnitPointBulge>();
        public PolyBezierModel()
        {
            Type = FigureTypes.PolyBezier;
        }
    }
}
