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
    public class EvaporationJob : MovableJobBase
    {
        private readonly LayerEvaporateModel layerPara;
        private readonly LayerCraftModel subLayerPara;
        private readonly LaserControllerImp laserController;

        public EvaporationJob(List<DataUnit> dataCollection) : base(dataCollection)
        {
            this.layerPara = GlobalModel.Params.LayerConfig.LayerEvaporate;
            int id = dataCollection[0].AttachedLayerId.Value;
            this.subLayerPara = GlobalModel.Params.LayerConfig.LayerCrafts[id];
            this.laserController = new LaserControllerImp(this.layerPara.PwrCtrlPara, this.layerPara.PowerPercent, this.layerPara.PulseFrequency);
            this.laserController.LaserParaChanged += x => OperationEngine.Instance.NotifyLaserParaChanged(x);
        }

        public override MachineInformation MachineInfo
        {
            get
            {
                double acceleration = GlobalModel.Params.LayerConfig.ProcessAcceleratedSpeed;
                double speed = this.layerPara.CutSpeed;
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
                speed = this.layerPara.CutSpeed;
            }
            segments = MovementUtil.GetLineSegments(items, speed, accelebration);

            if (SystemContext.Hardware?.IsLaserOn == true)
            {
                int delay = (int)this.layerPara.LaserOpenDelay;
                token.WaitHandle.WaitOne(delay);
            }

            //Fllow logic, case: EmptyMove
        }

        public override void HandleSpeedInfo(MotorInfoMap<double> speedInfo)
        {
            base.HandleSpeedInfo(speedInfo);
            bool need = SystemContext.Hardware.IsLaserOn;
            if (need)
            {
                //TODO: Adjust power base on speed
                double speed = Math.Sqrt(Math.Pow(speedInfo[AxisTypes.AxisX], 2) + Math.Pow(speedInfo[AxisTypes.AxisY], 2));
                this.laserController.Adjust(speed / this.layerPara.CutSpeed * 100.0);
            }
        }

        public override double LaserOnDelay => this.subLayerPara.IsNoFollow ? double.NaN : this.layerPara.LaserOpenDelay;

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

        public override LaserParameter LaserPara => new LaserParameter
        {
            VoltagePercentage = this.layerPara.PowerPercent,
            FrequencyHz = this.layerPara.PulseFrequency,
            DutyCircle = this.layerPara.PulseDutyFactorPercent
        };

        public override Tuple<string, double> GasPara => Tuple.Create(this.layerPara.GasKind, this.layerPara.GasPressure);

        public override bool NeedToLaserOff => false;

        public override bool NeedToBlowingOff => false;
    }
}
