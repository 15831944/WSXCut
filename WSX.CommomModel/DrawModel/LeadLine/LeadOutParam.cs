using System;

namespace WSX.CommomModel.DrawModel.LeadLine
{
    [Serializable]
    public class LeadOutParam
    {
        public UnitPoint StartPoint { get; set; }
        public UnitPoint EndPoint { get; set; }

        public UnitPoint Center { get; set; }
        public float StartAngle { get; set; }
        public float SweepAngle { get; set; }
        public float EndAngle { get; set; }
        public float Radius { get; set; }
    }
}
