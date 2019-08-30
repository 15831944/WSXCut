using System;
using WSX.CommomModel.DrawModel;

namespace WSX.DrawService.DrawModel
{
    [Serializable]
    public class ArcModelMini
    {
        public float StartAngle { get; set; }
        public float SweepAngle { get; set; }
        public float EndAngle { get; set; }
        public float Radius { get; set; }
        public UnitPoint Center { get; set; }
        public bool Clockwise { get; set; }
    }
}
