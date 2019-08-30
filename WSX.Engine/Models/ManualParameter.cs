using System.Drawing;
using WSX.Hardware.Models;

namespace WSX.Engine.Models
{
    public class ManualParameter
    {
        public double Speed { get; set; }
        public double Acceleration { get; set; }
        public PointF TargetPoint { get; set; }
        public double Step { get; set; } = double.NaN;
        public AxisTypes Axis { get; set; }
        public bool IsPointMoveCut { get; set; }
    }
}
