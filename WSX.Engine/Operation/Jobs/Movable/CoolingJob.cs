using System;
using System.Collections.Generic;
using System.Threading;
using WSX.CommomModel.ParaModel;
using WSX.Engine.Models;
using WSX.Engine.Utilities;
using WSX.GlobalData.Model;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation.Jobs.Movable
{
    public class CoolingJob : MovableJobBase
    {
        private readonly LayerCoolingModel layerPara;
        private readonly LayerCraftModel subLayerPara;

        public CoolingJob(List<DataUnit> dataCollection) : base(dataCollection)
        {
            this.layerPara = GlobalModel.Params.LayerConfig.LayerCooling;
            int id = dataCollection[0].AttachedLayerId.Value;
            this.subLayerPara = GlobalModel.Params.LayerConfig.LayerCrafts[id];
            this.NeedToBlowingOff = !this.subLayerPara.IsKeepPuffing;
        }
        
        public override MachineInformation MachineInfo
        {
            get
            {
                double acceleration = GlobalModel.Params.LayerConfig.ProcessAcceleratedSpeed;
                double speed = this.layerPara.CoolingSpeed;
                var items = base.GetDataCollection();
                var info = MovementUtil.GetMachineInfo(items, speed, acceleration);
                return new MachineInformation
                {
                    CutTime = info.Item1,
                    CutLen = info.Item2
                };
            }
        }

        public override void Intialize(out List<LineSegment> segments, double speed, CancellationToken token)
        {
            var items = base.GetDataCollection();
            double accelebration = GlobalModel.Params.LayerConfig.ProcessAcceleratedSpeed;
            if (double.IsNaN(speed))
            {
                speed = this.layerPara.CoolingSpeed;
            }
            segments = MovementUtil.GetLineSegments(items, speed, accelebration);    
            
            //Fllow logic, case: EmptyMove
        }
  
        public override double NozzleHeight
        {
            get
            {
                double height = double.NaN;
                if (!this.subLayerPara.IsNoFollow)
                {
                    height = this.layerPara.NozzleHeight;
                    double max = GlobalModel.Params.LayerConfig.FollowMaxHeight;
                    if (height > max)
                    {
                        height = max;
                    }
                }
                return height;
            }
        }

        public override double LiftHeight => this.subLayerPara.IsNoFollow ? double.NaN : this.layerPara.LiftHeight;

        public override bool NeedToBlowingOff { get; }

        public override Tuple<string, double> GasPara => Tuple.Create(this.layerPara.GasKind, this.layerPara.GasPressure);
    }
}
