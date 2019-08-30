using WSX.CommomModel.SystemConfig;
using WSX.Engine.Operation;
using WSX.Hardware.IO;
using WSX.Hardware.Laser;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Engine.Models
{
    public class SystemContext
    {
        public static bool IsDummyMode { get; set; } = true;
        public static MachineParameter MachineControlPara { get; set; } = new MachineParameter();
        public static SystemConfig SystemPara { get; set; } = new SystemConfig();
        public static Coordinate CoordinatePara { get; set; } = new Coordinate();
        public static HardwareProxy Hardware { get; set; }
    }
}
