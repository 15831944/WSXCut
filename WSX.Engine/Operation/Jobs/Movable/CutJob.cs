using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WSX.CommomModel.ParaModel;
using WSX.Engine.Models;
using WSX.Engine.Utilities;
using WSX.GlobalData.Model;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation.Jobs.Movable
{
    public class CutJob : MovableJobBase
    {
        private readonly LayerCraftModel layerPara;
        private readonly LaserControllerImp laserController;

        public CutJob(List<DataUnit> dataCollection) : base(dataCollection)
        {
            int layerId = base.GetDataCollection()[0].LayerId;
            this.layerPara = GlobalModel.Params.LayerConfig.LayerCrafts[layerId];
            this.laserController = new LaserControllerImp(this.layerPara.PwrCtrlPara, this.layerPara.PowerPercent, this.layerPara.PulseFrequency);
            this.laserController.LaserParaChanged += x => OperationEngine.Instance.NotifyLaserParaChanged(x);
        }

        public override MachineInformation MachineInfo
        {
            get
            {
                double delay = this.layerPara.DelayTime + layerPara.LaserOffDelay;
                var info = MovementUtil.GetMachineInfo(this.GetCuttingSegments());
                return new MachineInformation
                {
                    CutTime = info.Item1,
                    CutLen = info.Item2,
                    Delay = delay / 1000.0
                };
            }
        }

        public override void Intialize(out List<LineSegment> segments, double speed, CancellationToken token)
        {
            if (double.IsNaN(speed))
            {
                segments = this.GetCuttingSegments();
            }
            else
            {
                segments = this.GetEmptySegments();
            }

            if (SystemContext.Hardware?.IsLaserOn == true)
            {
                var laserPara = new LaserParameter
                {
                    VoltagePercentage = this.layerPara.PowerPercent,
                    FrequencyHz = this.layerPara.PulseFrequency,
                    DutyCircle = this.layerPara.PulseDutyFactorPercent
                };
                SystemContext.Hardware?.SetLaserPara(laserPara);
                OperationEngine.Instance.NotifyLaserParaChanged(laserPara);
                token.WaitHandle.WaitOne((int)layerPara.DelayTime);
            }

            //Fllow logic, case: EmptyMove, Cut->Config
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

        private List<LineSegment> GetCuttingSegments()
        {
            double startLen = 0;
            double startSpeed = 0;
            double stopLen = 0;
            double stopSpeed = 0;
            double normalSpeed = this.layerPara.CutSpeed;

            if (this.layerPara.IsSlowStart)
            {
                startSpeed = this.layerPara.SlowStartSpeed;
                if (startSpeed > 0)
                {
                    startLen = this.layerPara.SlowStartDistance;
                }
            }
            if (this.layerPara.IsSlowStop)
            {
                stopSpeed = this.layerPara.SlowStopSpeed;
                if (stopSpeed > 0)
                {
                    stopLen = this.layerPara.SlowStopDistance;
                }
            }

            var items = base.GetDataCollection();
            var segments = new List<LineSegment>();
            var spiltter = new DataSplitter(items, startLen, stopLen);
            double accelebration = GlobalModel.Params.LayerConfig.ProcessAcceleratedSpeed;
            if (spiltter.IsValid)
            {
                var part1 = spiltter.GetStartData();
                if (part1.Any())
                {
                    var tmp = MovementUtil.GetLineSegments(part1, startSpeed, accelebration);
                    if (tmp.Any())
                    {
                        segments.AddRange(tmp);
                    }
                }
                var part2 = spiltter.GetMiddleData();
                if (part2.Any())
                {
                    segments.AddRange(MovementUtil.GetLineSegments(part2, normalSpeed, accelebration));
                }
                var part3 = spiltter.GetEndData();
                if (part3.Any())
                {
                    var tmp = MovementUtil.GetLineSegments(part3, stopSpeed, accelebration);
                    if (tmp.Any())
                    {
                        segments.AddRange(tmp);
                    }
                }
            }
            else
            {
                segments.AddRange(MovementUtil.GetLineSegments(items, normalSpeed, accelebration));
            }

            return segments;
        }

        /// <summary>
        /// For step or outline movement
        /// </summary>
        /// <returns></returns>
        private List<LineSegment> GetEmptySegments()
        {
            var items = base.GetDataCollection();
            double accelebration = GlobalModel.Params.LayerConfig.ProcessAcceleratedSpeed;
            return MovementUtil.GetLineSegments(items, this.layerPara.CutSpeed, accelebration);
        }

        public override double LaserOnDelay => this.layerPara.DelayTime;

        public override double LaserOffDelay => this.layerPara.LaserOffDelay;

        public override double NozzleHeight
        {
            get
            {
                double height = double.NaN;
                if (!this.layerPara.IsNoFollow)
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

        public override double LiftHeight => this.layerPara.IsNoFollow ? double.NaN : this.layerPara.LiftHeight;

        public override LaserParameter LaserPara => new LaserParameter
        {
            VoltagePercentage = this.layerPara.PowerPercent,
            FrequencyHz = this.layerPara.PulseFrequency,
            DutyCircle = this.layerPara.PulseDutyFactorPercent
        };

        public override Tuple<string, double> GasPara => Tuple.Create(this.layerPara.GasKind, this.layerPara.GasPressure);

        public override bool NeedToBlowingOff => !this.layerPara.IsPathRecooling || !this.layerPara.IsKeepPuffing;
    }
}
