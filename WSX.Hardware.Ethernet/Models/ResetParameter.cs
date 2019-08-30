using System;

namespace WSX.Hardware.Models
{
   
    [Serializable]
    public class ResetParameter
    {
        public ResetDirection Direction { get; set; }
        public double MaxDistance { get; set; }
        public double BackOffset { get; set; }
        public double ZeroOffset { get; set; }
        public double RoughSpeed { get; set; }
        public double PreciseSpeed { get; set; }
    }
}
