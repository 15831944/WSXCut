using System;
using System.Diagnostics;

namespace WSX.CommomModel.Utilities
{
    public class DelayHelper
    {
        private readonly Stopwatch watch = new Stopwatch();

        public void Delay(int milliseconds)
        {
            this.watch.Restart();
            while (this.watch.ElapsedMilliseconds < milliseconds) ;
            this.watch.Stop();
        }

        public static string GetTimeString(double seconds)
        {
            string str = null;
            var period = TimeSpan.FromSeconds(seconds);
            if (period.Days != 0)
            {
                str += period.Days + "天";
            }
            if (period.Hours != 0)
            {
                str += period.Hours + "小时";
            }
            if (period.Minutes != 0)
            {
                str += period.Minutes + "分";
            }
            if (period.Seconds != 0 || str == null)
            {
                str += (period.Seconds + period.Milliseconds / 1000.0).ToString("0.###") + "秒";
            }
            return str;
        }
    }
}
