using System.ComponentModel;

namespace WSX.CommomModel.Physics
{
    public struct TimeUnit
    {
        private TimeUnit(double value)
        {
            AsMilliSecond = value;
        }

        public double AsMilliSecond { get; }

        public double AsSecond
        {
            get
            {
                return AsMilliSecond / 1000.0;
            }
        }

        public double AsMinute
        {
            get
            {
                return AsMilliSecond / (1000.0 * 60);
            }
        }

        public static TimeUnit Zero { get => FromMillisecond(0); }

        public static TimeUnit FromMillisecond(double ms)
        {
            return new TimeUnit(ms);
        }

        public static TimeUnit FromSecond(double second)
        {
            return new TimeUnit(second * 1000);
        }

        public static TimeUnit FromMinute(double minute)
        {
            return new TimeUnit(minute * 60 * 1000);
        }

        public static TimeUnit operator +(TimeUnit t1, TimeUnit t2)
        {
            return FromMillisecond(t1.AsMilliSecond + t2.AsMilliSecond);
        }

        public static TimeUnit operator -(TimeUnit t1, TimeUnit t2)
        {
            return FromMillisecond(t1.AsMilliSecond - t2.AsMilliSecond);
        }

        public static TimeUnit operator *(TimeUnit t1, TimeUnit t2)
        {
            return FromMillisecond(t1.AsMilliSecond * t2.AsMilliSecond);
        }

        public static TimeUnit operator /(TimeUnit t1, TimeUnit t2)
        {
            return FromMillisecond(t1.AsMilliSecond / t2.AsMilliSecond);
        }
    }
}
