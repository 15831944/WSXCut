using WSX.CommomModel.Physics;

namespace WSX.CommomModel.Physics
{
    public class Constants
    {
        public static readonly SpeedUnit MinSpeed = SpeedUnit.Zero;
        public static readonly SpeedUnit MaxSpeed = SpeedUnit.FromMillimeterPerSecond(500);
        public static readonly AccelerationUnit MinAcceleration = AccelerationUnit.Zero;
        public static readonly AccelerationUnit MaxAcceleration = AccelerationUnit.FromMillimeterPerSecondSquared(10000);
        public static readonly TimeUnit MinTime = TimeUnit.Zero;
        public static readonly TimeUnit MaxTime = TimeUnit.FromMinute(10);
        public static readonly PressureUnit MinPressure = PressureUnit.Zero;
        public static readonly PressureUnit MaxPressure = PressureUnit.FromBAR(10);
        public static readonly double MaxPower = 1000;   //1000W
    }
}
