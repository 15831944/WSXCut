using System.ComponentModel;

namespace WSX.CommomModel.Physics
{  
    public struct PressureUnit
    {
        private PressureUnit(double value)
        {
            AsBAR = value;
        }

        public double AsBAR { get; }

        public double AsPSI
        {
            get
            {
                return AsBAR * (14.22 / 0.98);
            }
        }

        public double AsMPa
        {
            get
            {
                return AsBAR / 10.0;
            }
        }

        public static PressureUnit Zero { get => FromBAR(0); }

        public static PressureUnit FromBAR(double value)
        {
            return new PressureUnit(value);
        }

        public static PressureUnit FromPSI(double value)
        {
            return new PressureUnit(value * (0.98 / 14.22));
        }

        public static PressureUnit FromMPa(double value)
        {
            return new PressureUnit(value * 10);
        }

        public static PressureUnit operator +(PressureUnit t1, PressureUnit t2)
        {
            return FromBAR(t1.AsBAR + t2.AsBAR);
        }

        public static PressureUnit operator -(PressureUnit t1, PressureUnit t2)
        {
            return FromBAR(t1.AsBAR - t2.AsBAR);
        }

        public static PressureUnit operator *(PressureUnit t1, PressureUnit t2)
        {
            return FromBAR(t1.AsBAR * t2.AsBAR);
        }

        public static PressureUnit operator /(PressureUnit t1, PressureUnit t2)
        {
            return FromBAR(t1.AsBAR / t2.AsBAR);
        }
    }
}
