using System;
using System.Collections.Generic;
using System.Threading;
using WSX.Engine.Operation.Jobs;
using WSX.Engine.Utilities;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation.Process
{
    public interface IProcess
    {
        void Execute(CancellationToken token, Predicate<MotorInfoMap<double>> posInfoHandler);
        MachineInformation MachineInfo { get; }
        bool? IsEmptyMoveShort { get; set; }
        bool IsFirst { get; set; }
        bool IsNeedToLaserOff { get; set; }
        bool IsNeedToBlowingOff { get; set; }
        bool IsNeedToFollowOff { get; set; }
        void AddJob(IJob job);
        void Optimize();
    }
}
