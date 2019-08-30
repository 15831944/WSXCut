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
    public class EmptyMoveJob : MovableJobBase
    {
        public EmptyMoveJob(List<DataUnit> dataCollection) : base(dataCollection)
        {

        }

        public override MachineInformation MachineInfo
        {
            get
            {
                double acceleration = GlobalModel.Params.LayerConfig.EmptyMoveAcceleratedSpeed;
                double speed = GlobalModel.Params.LayerConfig.EmptyMoveSpeed;
                var items = base.GetDataCollection();
                var info = MovementUtil.GetMachineInfo(items, speed, acceleration);
                return new MachineInformation
                {
                    EmptyMoveTime = info.Item1,
                    EmptyMoveLen = info.Item2
                };
            }
        }

        public override void Intialize(out List<LineSegment> segments, double speed, CancellationToken token)
        {
            var items = base.GetDataCollection();
            double accelebration = GlobalModel.Params.LayerConfig.EmptyMoveAcceleratedSpeed;
            if (double.IsNaN(speed))
            {
                speed = GlobalModel.Params.LayerConfig.EmptyMoveSpeed;
            }
            segments = MovementUtil.GetLineSegments(items, speed, accelebration);
        }

        public override void Execute(CancellationToken token, Predicate<MotorInfoMap<double>> posInfoHandler, bool isFirst)
        {
            base.Execute(token, posInfoHandler, isFirst);
            //TODO: Wait for Z moving end
        }
    }
}
