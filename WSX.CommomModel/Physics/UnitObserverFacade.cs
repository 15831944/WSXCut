using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.ParaModel;

namespace WSX.CommomModel.Physics
{
    public class UnitObserverFacade
    {
        private static UnitObserverFacade instance;

        public UnitObserver<UnitAcceleratedTypes> AccelerationUnitObserver { get; }
        public UnitObserver<UnitTimeTypes> TimeUnitObserver { get; }
        public UnitObserver<UnitSpeedTypes> SpeedUnitObserver { get; }
        public UnitObserver<UnitPressureTypes> PressureUnitObserver { get; }

        public static UnitObserverFacade Instance
        {
            get
            {
                return instance ?? (instance = new UnitObserverFacade());
            }
        }

        private UnitObserverFacade()
        {
            AccelerationUnitObserver = new UnitObserver<UnitAcceleratedTypes>();
            TimeUnitObserver = new UnitObserver<UnitTimeTypes>();
            SpeedUnitObserver = new UnitObserver<UnitSpeedTypes>();
            PressureUnitObserver = new UnitObserver<UnitPressureTypes>();
        }    
    }
}
