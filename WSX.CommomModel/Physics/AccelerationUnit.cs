using System.ComponentModel;

namespace WSX.CommomModel.Physics
{   
    public struct AccelerationUnit
    {
        private AccelerationUnit(double value)
        {
            AsMillimeterPerSecondSquared = value;
        }

        public double AsMillimeterPerSecondSquared { get; }

        public double AsG
        {
            get
            {
                return AsMillimeterPerSecondSquared * 0.0001;
            }
        }

        public double AsMeterPerMinuteSquared
        {
            get
            {
                return AsMillimeterPerSecondSquared * 3.6;
            }
        }

        public double AsMeterPerSecondSquared
        {
            get
            {
                return AsMillimeterPerSecondSquared * 0.001;
            }
        }

        public static AccelerationUnit Zero { get => FromMillimeterPerSecondSquared(0); }

        public static AccelerationUnit FromMillimeterPerSecondSquared(double value)
        {
            return new AccelerationUnit(value);
        }

        public static AccelerationUnit FromG(double value)
        {
            return new AccelerationUnit(value / 0.0001);
        }

        public static AccelerationUnit FromMeterPerMinuteSquared(double value)
        {
            return new AccelerationUnit(value / 3.6);
        }

        public static AccelerationUnit FromMeterPerSecondSquared(double value)
        {
            return new AccelerationUnit(value / 0.001);
        }

        public static AccelerationUnit operator +(AccelerationUnit t1, AccelerationUnit t2)
        {
            return FromMillimeterPerSecondSquared(t1.AsMillimeterPerSecondSquared + t2.AsMillimeterPerSecondSquared);
        }

        public static AccelerationUnit operator -(AccelerationUnit t1, AccelerationUnit t2)
        {
            return FromMillimeterPerSecondSquared(t1.AsMillimeterPerSecondSquared - t2.AsMillimeterPerSecondSquared);
        }

        public static AccelerationUnit operator *(AccelerationUnit t1, AccelerationUnit t2)
        {
            return FromMillimeterPerSecondSquared(t1.AsMillimeterPerSecondSquared * t2.AsMillimeterPerSecondSquared);
        }

        public static AccelerationUnit operator /(AccelerationUnit t1, AccelerationUnit t2)
        {
            return FromMillimeterPerSecondSquared(t1.AsMillimeterPerSecondSquared / t2.AsMillimeterPerSecondSquared);
        }
    }
}
