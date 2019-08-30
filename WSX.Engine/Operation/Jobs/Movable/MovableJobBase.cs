using System;
using System.Collections.Generic;
using System.Threading;
using WSX.Engine.Models;
using WSX.Engine.Utilities;
using WSX.GlobalData.Model;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation.Jobs.Movable
{
    public abstract class MovableJobBase : JobBase
    { 
        public MovableJobBase(List<DataUnit> dataCollection) : base(dataCollection)
        {

        }

        public override MachineInformation MachineInfo { get; }

        public override void Execute(CancellationToken token, Predicate<MotorInfoMap<double>> posInfoHandler, bool isFirst)
        {       
            this.Intialize(out List<LineSegment> segments, this.Speed, token);
            SystemContext.Hardware?.MoveAsync(segments, token, (pos, speed) =>
            {            
                if (posInfoHandler.Invoke(pos))
                {
                    this.HandleSpeedInfo(speed);
                }
            }).Wait();              
        }

        public virtual void HandleSpeedInfo(MotorInfoMap<double> speedInfo)
        {
            OperationEngine.Instance.ReportMotorSpeed(speedInfo);
        }

        public abstract void Intialize(out List<LineSegment> segments, double speed, CancellationToken token);

        private double Speed
        {
            get
            {
                double speed = double.NaN;
                var operType = SystemContext.MachineControlPara.OperationType;
                if (operType == OperationTypes.Step)
                {
                    speed = SystemContext.MachineControlPara.StepSpeed;
                }
                else if (operType == OperationTypes.Outline)
                {
                    speed = GlobalModel.Params.LayerConfig.CheckEdgeSpeed;
                }
                return speed;
            }
        }
    }
}
