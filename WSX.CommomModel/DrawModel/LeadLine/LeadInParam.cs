using System;

namespace WSX.CommomModel.DrawModel.LeadLine
{
    [Serializable]
    public class LeadInParam
    {
        public UnitPoint StartPoint { get; set; }
        public UnitPoint EndPoint { get; set; }

        public UnitPoint Center { get; set; }
        public float StartAngle { get; set; }
        public float SweepAngle { get; set; }
        public float EndAngle { get; set; }
        public float Radius { get; set; }

        public UnitPoint HoleCenter { get; set; }
        public float HoleRadius { get; set; }
    }
}
