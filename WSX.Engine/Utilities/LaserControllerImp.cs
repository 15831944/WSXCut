using System;
using WSX.CommomModel.ParaModel;
using WSX.Engine.Models;
using WSX.Hardware.Models;

namespace WSX.Engine.Utilities
{
    public class LaserControllerImp
    {
        private readonly PowerControlModel pwrCtrlPara;
        private readonly double maxPower;
        private readonly double maxFreq;   

        public event Action<LaserParameter> LaserParaChanged;

        public LaserControllerImp(PowerControlModel para, double maxPower, double maxFreq)
        {
            this.pwrCtrlPara = para;
            this.maxPower = maxPower;
            this.maxFreq = maxFreq;
        }

        public void Adjust(double speedPercentage)
        {
            if (speedPercentage < 0)
            {
                speedPercentage = 0;
            }
            if (speedPercentage > 100)
            {
                speedPercentage = 100;
            }

            if (this.pwrCtrlPara.PowerAdjustmentEnabled)
            {
                this.AdjustPower(speedPercentage);
            }      
            if (this.pwrCtrlPara.FreqAdjustmentEnabled)
            {
                this.AdjustFrequency(speedPercentage);
            }          
        }

        private void AdjustPower(double speedPercentage)
        {
            double ratio = this.pwrCtrlPara.PowerCurve.GetY(speedPercentage) / 100.0;
            double voltage = this.maxPower * ratio;
            SystemContext.Hardware.Laser.SetVoltage(voltage);
            var para = LaserParameter.Empty;
            para.VoltagePercentage = voltage;
            this.LaserParaChanged?.Invoke(para);
        }

        private void AdjustFrequency(double speedPercentage)
        {
            double ratio = this.pwrCtrlPara.FreqCurve.GetY(speedPercentage) / 100.0;
            double freq = this.maxFreq * ratio;
            SystemContext.Hardware.Laser.SetFrequency(freq);
            var para = LaserParameter.Empty;
            para.FrequencyHz = freq;
            this.LaserParaChanged?.Invoke(para);
        }  
    }
}
