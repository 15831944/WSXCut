using System;
using System.Collections.Generic;
using System.Threading;
using WSX.CommomModel.ParaModel;
using WSX.Engine.Models;
using WSX.Engine.Utilities;
using WSX.GlobalData.Model;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation.Jobs.Fixed
{
    public class PointCutJob : JobBase
    {
        private readonly LayerCraftModel layerPara;

        public PointCutJob(List<DataUnit> dataCollection) : base(dataCollection)
        {
            this.layerPara = GlobalModel.Params.LayerConfig.LayerCrafts[dataCollection[0].LayerId];
        }

        public override MachineInformation MachineInfo
        {
            get
            {
                return new MachineInformation
                {
                    Delay = (this.layerPara.DelayTime + this.layerPara.LaserOffDelay) / 1000.0
                };
            }
        }

        public override void Execute(CancellationToken token, Predicate<MotorInfoMap<double>> posInfoHandler, bool isFirst)
        {
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
            }
            double delay = this.layerPara.DelayTime + this.layerPara.LaserOffDelay;
            token.WaitHandle.WaitOne((int)delay);
        }

        public override double LaserOnDelay => this.layerPara.DelayTime;

        public override double LaserOffDelay => this.layerPara.LaserOffDelay;

        public override double NozzleHeight => this.layerPara.NozzleHeight;

        public override double LiftHeight => this.layerPara.LiftHeight;

        public override LaserParameter LaserPara => new LaserParameter
        {
            VoltagePercentage = this.layerPara.PowerPercent,
            FrequencyHz = this.layerPara.PulseFrequency,
            DutyCircle = this.layerPara.PulseDutyFactorPercent
        };

        public override Tuple<string, double> GasPara => Tuple.Create(this.layerPara.GasKind, this.layerPara.GasPressure);
    }
}