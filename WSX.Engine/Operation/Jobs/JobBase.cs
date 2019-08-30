using System;
using System.Collections.Generic;
using System.Threading;
using WSX.Engine.Models;
using WSX.Engine.Utilities;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation.Jobs
{
    public abstract class JobBase : IJob, ILayerConfig
    {
        private readonly List<DataUnit> dataCollection;

        public JobBase(List<DataUnit> dataCollection)
        {
            this.dataCollection = dataCollection;
        }

        public abstract MachineInformation MachineInfo { get; }

        public abstract void Execute(CancellationToken token, Predicate<MotorInfoMap<double>> posInfoHandler, bool isFirst);

        public void AddData(DataUnit data)
        {
            this.dataCollection.Add(data);
        }

        public List<DataUnit> GetDataCollection()
        {
            return this.dataCollection;
        }

        #region ILayerConfig Implementation
        public virtual double LaserOnDelay => double.NaN;

        public virtual double LaserOffDelay => double.NaN;

        public virtual double NozzleHeight => double.NaN;

        public virtual double LiftHeight => double.NaN;

        public virtual LaserParameter LaserPara => null;

        public virtual Tuple<string, double> GasPara => null;

        public virtual bool NeedToLift => true;

        public virtual bool NeedToBlowingOff => true;

        public virtual bool NeedToLaserOff => true;
        #endregion
    }
}
