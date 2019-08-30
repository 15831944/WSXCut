using WSX.CommomModel.ParaModel;

namespace WSX.CommomModel.Physics.Converters
{
    public class AccelerationUnitConverter
    {
        public static string Convert(double acceleration)
        {
            var accelerationUnit = AccelerationUnit.FromMillimeterPerSecondSquared(acceleration);
            var accelerationUnitObserver = UnitObserverFacade.Instance.AccelerationUnitObserver;
            switch (accelerationUnitObserver.UnitType)
            {
                case UnitAcceleratedTypes.MillimeterPerSecondSquared:
                    acceleration = accelerationUnit.AsMillimeterPerSecondSquared;
                    break;
                case UnitAcceleratedTypes.G:
                    acceleration = accelerationUnit.AsG;
                    break;
                case UnitAcceleratedTypes.MeterPerMinuteSquared:
                    acceleration = accelerationUnit.AsMeterPerMinuteSquared;
                    break;
                case UnitAcceleratedTypes.MeterPerSecondSquared:
                    acceleration = accelerationUnit.AsMeterPerSecondSquared;
                    break;          
            }
            return acceleration.ToString("0.###");
        }

        public static double ConvertBack(string str)
        {
            //double tmp = double.Parse(str);
            double.TryParse(str, out double tmp);
            AccelerationUnit accelerationUnit = AccelerationUnit.FromMillimeterPerSecondSquared(tmp);
            var unitType = UnitObserverFacade.Instance.AccelerationUnitObserver.UnitType;
            switch (unitType)
            {
                case UnitAcceleratedTypes.MillimeterPerSecondSquared:
                    accelerationUnit = AccelerationUnit.FromMillimeterPerSecondSquared(tmp);
                    break;
                case UnitAcceleratedTypes.G:
                    accelerationUnit = AccelerationUnit.FromG(tmp);
                    break;
                case UnitAcceleratedTypes.MeterPerMinuteSquared:
                    accelerationUnit = AccelerationUnit.FromMeterPerMinuteSquared(tmp);
                    break;
                case UnitAcceleratedTypes.MeterPerSecondSquared:
                    accelerationUnit = AccelerationUnit.FromMeterPerSecondSquared(tmp);
                    break;
            }
            return accelerationUnit.AsMillimeterPerSecondSquared;
        }
    }
}
