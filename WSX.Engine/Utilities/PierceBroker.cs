using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WSX.CommomModel.ParaModel;
using WSX.Engine.Models;
using WSX.GlobalData.Model;
using WSX.Hardware.Models;

namespace WSX.Engine.Utilities
{
    public class PierceConfig
    {
        public PierceLevels Level { get; set; }
        public PierceParameters Level1Para { get; set; }
        public PierceParameters Level2Para { get; set; }
        public PierceParameters Level3Para { get; set; }
        public bool FollowEnabled { get; set; } = true;
        public bool LaserEnabled { get; set; } = true;
    }

    public class PierceBroker
    {
        private readonly PierceConfig config;
        private bool isFirst;

        public List<PierceParameters> Pierces { get; private set; }
        public MachineInformation MachineInfo { get; private set; }

        public event Action<double> FollowHeightChanged;
        public event Action<double> OnFollowHeightSet;
        public event Action<bool> OnFollowChanged;
        public event Action<LaserParameter> OnLaserParaChanged;
        public event Action<bool> OnLaserEnabledChanged;
        public event Action<bool> OnBlowingEnabledChanged;

        public PierceBroker(PierceConfig config)
        {
            this.config = config;
            this.Pierces = new List<PierceParameters>();
            switch (config.Level)
            {
                case PierceLevels.Level3:
                    this.Pierces.Add(config.Level3Para);
                    this.Pierces.Add(config.Level2Para);
                    this.Pierces.Add(config.Level1Para);
                    break;
                case PierceLevels.Level2:
                    this.Pierces.Add(config.Level2Para);
                    this.Pierces.Add(config.Level1Para);
                    break;
                case PierceLevels.Level1:
                    this.Pierces.Add(config.Level1Para);
                    break;
            }
            this.MachineInfo = new MachineInformation();
            if (this.Pierces.Any())
            {
                this.MachineInfo.PiercingTimes = 1;
            }
            foreach (var m in this.Pierces)
            {
                this.UpdateMachineInfo(this.MachineInfo, m);
            }
        }

        public void Execute(CancellationToken token, bool isFirst = false)
        {          
            if (token.IsCancellationRequested)
            {
                return;
            }

            this.isFirst = isFirst;
            foreach (var m in this.Pierces)
            {
                this.PierceCore(m, token);
                this.isFirst = false;
            }
        }

        private void UpdateMachineInfo(MachineInformation info, PierceParameters piercePara)
        {         
            info.Delay += piercePara.DelayTime / 1000.0;
            if (piercePara.IsStepTime)
            {
                info.Delay += piercePara.StepTime / 1000.0;
            }
            if (piercePara.IsExtraPuffing)
            {
                info.Delay += piercePara.ExtraPuffing / 1000.0;
            }
        }

        private void PierceCore(PierceParameters para, CancellationToken token)
        {
            //Control Axis Z
            this.MoveZ(this.GetFollowHeight(para), token);
            //Control gas
            if (SystemContext.Hardware?.IsBlowingOn == false)
            {
                SystemContext.Hardware?.SetBlowingPara(para.GasKind, para.GasPressure);
                SystemContext.Hardware?.BlowingOn();
                this.OnBlowingEnabledChanged?.Invoke(true);
                double delay = GlobalModel.Params.LayerConfig.OpenAirDelay;
                if (this.isFirst)
                {
                    delay += GlobalModel.Params.LayerConfig.FirstOpenAirDelay;
                }
                token.WaitHandle.WaitOne((int)delay);
            }
            //Control focus
            //Control laser
            if (this.config.LaserEnabled)
            {
                var laserPara = new LaserParameter
                {
                    FrequencyHz = para.PulseFrequency,
                    DutyCircle = para.PulseDutyFactorPercent,
                    VoltagePercentage = para.PowerPercent
                };
                SystemContext.Hardware?.LaserOn(laserPara);
                this.OnLaserEnabledChanged?.Invoke(true);
                this.OnLaserParaChanged?.Invoke(laserPara);
                //Delay -> (Laser off -> Delay -> Laser on)
                token.WaitHandle.WaitOne((int)para.DelayTime);
                if (para.IsExtraPuffing)
                {
                    SystemContext.Hardware?.LaserOff();
                    this.OnLaserEnabledChanged?.Invoke(false);
                    token.WaitHandle.WaitOne((int)para.ExtraPuffing);
                }
            }        
        }

        private void MoveZ(double followHeight, CancellationToken token)
        {
            if (!this.config.FollowEnabled)
            {
                return;
            }
            this.OnFollowHeightSet?.Invoke(followHeight);

            //TODO：Control Z
            double height = followHeight;
            bool isOnlyLocate = GlobalModel.Params.LayerConfig.IsOnlyPositionZCoordinate;
            if (isOnlyLocate)
            {
                height = GlobalModel.Params.LayerConfig.PositionZCoordinate - height;
                SystemContext.Hardware?.MoveZ(height, false, true, token);
            }
            else
            {
                SystemContext.Hardware?.MoveZ(height);
                SystemContext.Hardware?.FollowOn();
                this.OnFollowChanged?.Invoke(true);
            }         
        }

        private double GetFollowHeight(PierceParameters para)
        {
            double height = para.NozzleHeight;
            double max = GlobalModel.Params.LayerConfig.FollowMaxHeight;
            if (height > max)
            {
                height = max;
            }
            return height;
        }
    }
}
