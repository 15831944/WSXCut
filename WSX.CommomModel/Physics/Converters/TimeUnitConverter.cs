using WSX.CommomModel.ParaModel;

namespace WSX.CommomModel.Physics.Converters
{
    public class TimeUnitConverter
    {
        public static string Convert(double time)
        {
            var timeUnit = TimeUnit.FromMillisecond(time);
            var timeUnitObserver = UnitObserverFacade.Instance.TimeUnitObserver;
            switch (timeUnitObserver.UnitType)
            {
                case UnitTimeTypes.Millisecond:
                    time = timeUnit.AsMilliSecond;
                    break;
                case UnitTimeTypes.Second:
                    time = timeUnit.AsSecond;
                    break;
                case UnitTimeTypes.Minute:
                    time = timeUnit.AsMinute;
                    break;            
            }
            return time.ToString("0.###");
        }

        public static double ConvertBack(string str)
        {
            //double tmp = double.Parse(str);
            double.TryParse(str, out double tmp);
            TimeUnit timeUnit = TimeUnit.FromMillisecond(tmp);
            var unitType = UnitObserverFacade.Instance.TimeUnitObserver.UnitType;
            switch (unitType)
            {
                case UnitTimeTypes.Millisecond:
                    timeUnit = TimeUnit.FromMillisecond(tmp);
                    break;
                case UnitTimeTypes.Second:
                    timeUnit = TimeUnit.FromSecond(tmp);
                    break;
                case UnitTimeTypes.Minute:
                    timeUnit = TimeUnit.FromMinute(tmp);
                    break;
            }
            return timeUnit.AsMilliSecond;
        }
    }
}
