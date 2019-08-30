using WSX.CommomModel.ParaModel;

namespace WSX.CommomModel.Physics.Converters
{
    public class PressureUnitConverter
    {
        public static string Convert(double pressure)
        {
            var pressureUnit = PressureUnit.FromBAR(pressure);
            var pressureUnitObserver = UnitObserverFacade.Instance.PressureUnitObserver;
            switch (pressureUnitObserver.UnitType)
            {
                case UnitPressureTypes.BAR:
                    pressure = pressureUnit.AsBAR;
                    break;
                case UnitPressureTypes.MPa:
                    pressure = pressureUnit.AsMPa;
                    break;
                case UnitPressureTypes.PSI:
                    pressure = pressureUnit.AsPSI;
                    break;
            }
            return pressure.ToString("0.###");
        }

        public static double ConvertBack(string str)
        {
            //double tmp = double.Parse(str);
            double.TryParse(str,out  double tmp);
            PressureUnit pressureUnit = PressureUnit.FromBAR(tmp);
            var unitType = UnitObserverFacade.Instance.PressureUnitObserver.UnitType;
            switch (unitType)
            {
                case UnitPressureTypes.BAR:
                    pressureUnit = PressureUnit.FromBAR(tmp);
                    break;
                case UnitPressureTypes.MPa:
                    pressureUnit = PressureUnit.FromMPa(tmp);
                    break;
                case UnitPressureTypes.PSI:
                    pressureUnit = PressureUnit.FromPSI(tmp);
                    break;
            }
            return pressureUnit.AsBAR;
        }
    }
}
