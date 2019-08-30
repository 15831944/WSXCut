using WSX.CommomModel.ParaModel;

namespace WSX.CommomModel.Physics.Converters
{
    public class SpeedUnitConverter
    {
        public static string Convert(double speed)
        {
            var speedUnit = SpeedUnit.FromMillimeterPerSecond(speed);
            var speedUnitObserver = UnitObserverFacade.Instance.SpeedUnitObserver;
            switch (speedUnitObserver.UnitType)
            {
                case UnitSpeedTypes.Millimeter_Second:
                    speed = speedUnit.AsMillimeterPerSecond;
                    break;
                case UnitSpeedTypes.Meter_Second:
                    speed = speedUnit.AsMeterPerSecond;
                    break;
                case UnitSpeedTypes.Meter_Minute:
                    speed = speedUnit.AsMeterPerMinute;
                    break;
                case UnitSpeedTypes.Millimeter_Minute:
                    speed = speedUnit.AsMillimeterPerMinute;
                    break;
            }
            return speed.ToString("0.###");
        }
  
        public static double ConvertBack(string str)
        {
            //double tmp = double.TryParse(str);
            double.TryParse(str, out double tmp);
            SpeedUnit speedUnit = SpeedUnit.FromMillimeterPerSecond(tmp);
            var unitType = UnitObserverFacade.Instance.SpeedUnitObserver.UnitType;
            switch (unitType)
            {
                case UnitSpeedTypes.Millimeter_Second:
                    speedUnit = SpeedUnit.FromMillimeterPerSecond(tmp);
                    break;
                case UnitSpeedTypes.Meter_Second:
                    speedUnit = SpeedUnit.FromMeterPerSecond(tmp);
                    break;
                case UnitSpeedTypes.Meter_Minute:
                    speedUnit = SpeedUnit.FromMeterPerMinute(tmp);
                    break;
                case UnitSpeedTypes.Millimeter_Minute:
                    speedUnit = SpeedUnit.FromMillimeterPerMinute(tmp);
                    break;
            }
            return speedUnit.AsMillimeterPerSecond;
        }
    }
}
