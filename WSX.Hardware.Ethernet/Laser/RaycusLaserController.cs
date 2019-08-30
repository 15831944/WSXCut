using System;
using System.Collections.Generic;
using WSX.Hardware.Models;

namespace WSX.Hardware.Laser
{
    public class RaycusLaserController : ILaser
    {
        private LaserParameter para = new LaserParameter();
        private readonly SerialPortStream serial;
        private const string LASER_ON_CMD = "1B4F0D";
        private const string LASER_OFF_CMD = "1B530D";
        private const string LASER_PARA_CMD = "1B46{0}44{1}50{2}0D";

        public RaycusLaserController(SerialPortStream serialPortStream)
        {
            this.serial = serialPortStream;
        }

        public void LaserOff()
        {
            byte[] cmd = this.CovertToBytes(LASER_OFF_CMD);
            this.serial.WriteData(cmd);
        }

        public void LaserOn()
        {
            byte[] cmd = this.CovertToBytes(LASER_ON_CMD);
            this.serial.WriteData(cmd);
        }

        public void SetFrequency(double frequency)
        {
            this.para.FrequencyHz = frequency;
            byte[] cmd = this.CovertToBytes(this.CreateParaCmd());
            this.serial.WriteData(cmd);
        }

        public void SetParameter(LaserParameter para)
        {
            this.para = para;
            byte[] cmd = this.CovertToBytes(this.CreateParaCmd());
            this.serial.WriteData(cmd);
        }

        public void SetVoltage(double voltagePercentage)
        {
            this.para.VoltagePercentage = voltagePercentage;
            byte[] cmd = this.CovertToBytes(this.CreateParaCmd());
            this.serial.WriteData(cmd);
        }

        private byte[] CovertToBytes(string cmd)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < cmd.Length / 2; i += 2)
            {
                string subStr = cmd.Substring(i, 2);
                bytes.Add(Convert.ToByte(subStr, 2));
            }
            return bytes.ToArray();
        }

        private string CreateParaCmd()
        {
            return string.Format(LASER_PARA_CMD, this.para.FrequencyHz.ToString("X4"), this.para.DutyCircle.ToString("X2"), this.para.VoltagePercentage.ToString("X2"));
        }
    }
}
