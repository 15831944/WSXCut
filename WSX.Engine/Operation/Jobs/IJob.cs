using System;
using System.Collections.Generic;
using System.Threading;
using WSX.Engine.Models;
using WSX.Engine.Utilities;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation.Jobs
{
    public interface IJob
    {
        void Execute(CancellationToken token, Predicate<MotorInfoMap<double>> posInfoHandler, bool isFirst);
        MachineInformation MachineInfo { get; }
        void AddData(DataUnit data);
        List<DataUnit> GetDataCollection();
    }
}
