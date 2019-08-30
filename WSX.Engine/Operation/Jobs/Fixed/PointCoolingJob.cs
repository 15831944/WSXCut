using System;
using System.Collections.Generic;
using System.Threading;
using WSX.Engine.Models;
using WSX.Engine.Utilities;
using WSX.GlobalData.Model;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation.Jobs.Fixed
{
    public class PointCoolingJob : JobBase
    {
        private readonly int delayMS;
        
        public PointCoolingJob(List<DataUnit> dataCollection): base(dataCollection)
        {
            this.delayMS = (int)GlobalModel.Params.LayerConfig.CoolingDotDelay;          
        }

        public override MachineInformation MachineInfo
        {
            get
            {
                return new MachineInformation
                {
                    Delay = this.delayMS / 1000.0
                };
            }
        }

        public override void Execute(CancellationToken token, Predicate<MotorInfoMap<double>> posInfoHandler, bool isFirst)
        {
            SystemContext.Hardware?.LaserOff();
            OperationEngine.Instance.NotifyLaserEnabled(false);
            token.WaitHandle.WaitOne(this.delayMS);
        } 
    }
}
