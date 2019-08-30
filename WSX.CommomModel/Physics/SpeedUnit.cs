using System.Collections.Generic;
using System.ComponentModel;
using WSX.CommomModel.ParaModel;

namespace WSX.CommomModel.Physics
{
    public struct SpeedUnit
    {
        private SpeedUnit(double value)
        {
            this.AsMillimeterPerSecond = value;
        }

        public double AsMillimeterPerSecond { get; }

        public double AsMeterPerSecond
        {
            get
            {
                return AsMillimeterPerSecond / 1000.0;
            }
        }

        public double AsMeterPerMinute
        {
            get
            {
                return AsMillimeterPerSecond * 0.06;
            }
        }

        public double AsMillimeterPerMinute
        {
            get
            {
                return AsMillimeterPerSecond * 60;
            }
        }

        public double AsInchPerMinute
        {
            get
            {
                return AsMillimeterPerSecond * (60 / 25.4);
            }
        }

        public double AsInchPerSecond
        {
            get
            {
                return AsMillimeterPerSecond / 25.4;
            }
        }

        public static SpeedUnit Zero { get => FromMillimeterPerSecond(0); }
       
        public static SpeedUnit FromMillimeterPerSecond(double mmPerSecond)
        {
            return new SpeedUnit(mmPerSecond);
        }

        public static SpeedUnit FromMeterPerSecond(double mPerSecond)
        {
            return new SpeedUnit(mPerSecond * 1000.0);
        }

        public static SpeedUnit FromMeterPerMinute(double mPerMinute)
        {
            return new SpeedUnit(mPerMinute / 0.06);
        }

        public static SpeedUnit FromMillimeterPerMinute(double mmPerMinute)
        {
            return new SpeedUnit(mmPerMinute / 60.0);
        }

        public static SpeedUnit FromInchPerMinute(double inchPerMinute)
        {
            return new SpeedUnit(inchPerMinute * (25.4 / 60));
        }

        public static SpeedUnit FromInchPerSecond(double inchPerSecond)
        {
            return new SpeedUnit(inchPerSecond * 25.4);
        }

        public static SpeedUnit operator +(SpeedUnit t1, SpeedUnit t2)
        {
            return FromMillimeterPerSecond(t1.AsMillimeterPerSecond + t2.AsMillimeterPerSecond);
        }

        public static SpeedUnit operator -(SpeedUnit t1, SpeedUnit t2)
        {
            return FromMillimeterPerSecond(t1.AsMillimeterPerSecond - t2.AsMillimeterPerSecond);
        }

        public static SpeedUnit operator *(SpeedUnit t1, SpeedUnit t2)
        {
            return FromMillimeterPerSecond(t1.AsMillimeterPerSecond * t2.AsMillimeterPerSecond);
        }

        public static SpeedUnit operator /(SpeedUnit t1, SpeedUnit t2)
        {
            return FromMillimeterPerSecond(t1.AsMillimeterPerSecond / t2.AsMillimeterPerSecond);
        }
    }
}
