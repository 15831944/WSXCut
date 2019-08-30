using WSX.Hardware.Models;

namespace WSX.Hardware.Laser
{
    public interface ILaser
    {
        void LaserOn();
        void LaserOff();
        void SetVoltage(double voltagePercentage);
        void SetFrequency(double frequency);
        void SetParameter(LaserParameter para);
    }
}
