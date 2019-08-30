using System;
using System.ComponentModel;

namespace WSX.Hardware.Models
{
    public enum LaserTypes
    {
        [Description("锐科")]
        Raycus,
        [Description("其它")]
        Other
    }

    public enum LaserControlTypes
    {
        [Description("IO")]
        Extern,
        [Description("串口")]
        RS232
    }

    [Serializable]
    public class LaserParameter
    {
        public double VoltagePercentage { get; set; } = 30;
        public double FrequencyHz { get; set; } = 5000;
        public double DutyCircle { get; set; } = 30;
        public bool Enabled { get; set; } = false;

        public LaserParameter DeepCopy()
        {
            return new LaserParameter
            {
                VoltagePercentage = this.VoltagePercentage,
                FrequencyHz = this.FrequencyHz,
                DutyCircle = this.DutyCircle,
                Enabled  = this.Enabled
            };
        }

        public static LaserParameter Empty
        {
            get => new LaserParameter
            {
                VoltagePercentage = double.NaN,
                FrequencyHz = double.NaN,
                DutyCircle = double.NaN
            };
        }
    }

    public class LaserConfig
    {
        public LaserTypes LaserType { get; set; }
        public LaserControlTypes ControlType { get; set; }
        public double MaxVoltage { get; set; }
        public string PortName { get; set; }
    }
}
