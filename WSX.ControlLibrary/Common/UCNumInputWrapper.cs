using System;
using System.Collections.Generic;
using System.ComponentModel;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Physics;

namespace WSX.ControlLibrary.Common
{
    public class UCInputerWrapper<T1, T2> : UCNumberInputer
        where T2 : struct
    {
        protected Dictionary<T1, Func<T2, double>> ActionMap { get; set; }

        [Browsable(false)]
        public T2 MaxValue { get; set; }
        [Browsable(false)]
        public T2 MinValue { get; set; }

        public UCInputerWrapper()
        {
           
        }

        public void UpdateUnitType(T1 speedType)
        {
            base.Max = ActionMap[speedType].Invoke(this.MaxValue);
            base.Min = ActionMap[speedType].Invoke(this.MinValue);
        }
    }

    public class SpeedInputer : UCInputerWrapper<UnitSpeedTypes, SpeedUnit>
    {
        public SpeedInputer()
        {
            base.ActionMap = new Dictionary<UnitSpeedTypes, Func<SpeedUnit, double>>
            {
                { UnitSpeedTypes.Millimeter_Second, x => x.AsMillimeterPerSecond },
                { UnitSpeedTypes.Meter_Second, x => x.AsMeterPerSecond },
                { UnitSpeedTypes.Meter_Minute, x => x.AsMeterPerMinute },
                { UnitSpeedTypes.Millimeter_Minute, x => x.AsMillimeterPerMinute }
            };
        }
    }

    public class TimeInputer : UCInputerWrapper<UnitTimeTypes, TimeUnit>
    {
        public TimeInputer()
        {
            base.ActionMap = new Dictionary<UnitTimeTypes, Func<TimeUnit, double>>
            {
                { UnitTimeTypes.Millisecond, x => x.AsMilliSecond },
                { UnitTimeTypes.Second, x => x.AsSecond },
                { UnitTimeTypes.Minute, x => x.AsMinute }
            };
        }
    }

    public class PressureInputer : UCInputerWrapper<UnitPressureTypes, PressureUnit>
    {
        public PressureInputer()
        {
            base.ActionMap = new Dictionary<UnitPressureTypes, Func<PressureUnit, double>>
            {
                { UnitPressureTypes.BAR, x => x.AsBAR },
                { UnitPressureTypes.MPa, x => x.AsMPa },
                { UnitPressureTypes.PSI, x => x.AsPSI }
            };
        }
    }

    public class AccelerationInputer: UCInputerWrapper<UnitAcceleratedTypes, AccelerationUnit>
    {
        public AccelerationInputer()
        {
            base.ActionMap = new Dictionary<UnitAcceleratedTypes, Func<AccelerationUnit, double>>
            {
                { UnitAcceleratedTypes.MillimeterPerSecondSquared, x => x.AsMillimeterPerSecondSquared},
                { UnitAcceleratedTypes.G, x => x.AsG},
                { UnitAcceleratedTypes.MeterPerSecondSquared, x => x.AsMeterPerSecondSquared},
                { UnitAcceleratedTypes.MeterPerMinuteSquared, x => x.AsMeterPerMinuteSquared}
            };
        }
    }
}
