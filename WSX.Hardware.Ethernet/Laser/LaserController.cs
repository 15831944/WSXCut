using WSX.Hardware.Common;
using WSX.Hardware.Models;

namespace WSX.Hardware.Laser
{
    public class LaserController : ILaser
    {
        private readonly TcpClientStream tcpClient;
        private const int SEND_DELAY_MS = 5;
        private readonly double maxVoltage;

        public LaserController(TcpClientStream tcpClient, double maxVoltage)
        {
            this.tcpClient = tcpClient;
            this.maxVoltage = maxVoltage;

            //Initialize hardware
            //this.SetVoltage(1);
            //this.SetVoltage(2);
        }
    
        public void LaserOn()
        {
            //this.tcpClient.WriteIOData(RegisterDefinition.LaserEnalbed, LaserCommands.LaserOn);
        }

        public void LaserOff()
        {
            //this.tcpClient.WriteIOData(RegisterDefinition.LaserEnalbed, LaserCommands.LaserOff);
        }

        public void SetParameter(LaserParameter para)
        {
            ////uint freq = (uint)(1000000000 / (para.FrequencyHz * 1000 * 30.30303030));
            //uint freq = (uint)(1000000000 / (para.FrequencyHz * 30.30303030));
            //uint duty = (uint)(freq * para.DutyCircle / 100.0);

            //this.tcpClient.WriteIOData(RegisterDefinition.LaserFrequency, freq);
            //this.tcpClient.WriteIOData(RegisterDefinition.LaserDutyCycle, duty);

            //double voltage = this.maxVoltage * para.VoltagePercentage / 100.0;
            //uint data = ((uint)(voltage * 3276.7) | 0x8000) + 170;
            //if (voltage > 9.9)
            //{
            //    data = 0xFFFF;
            //}
            //var para1 = PciProtocol.CreateExtCommand(RegisterDefinition.AnalogOut1, data);
            //foreach (var pair in para1)
            //{
            //    this.pci.WriteMemoryData(pair.Key, pair.Value);
            //    Thread.Sleep(SEND_DELAY_MS);
            //}
        }

        public void SetVoltage(double voltagePercentage)
        {
   //         double voltage = this.maxVoltage * voltagePercentage / 100.0;
   //         uint data = ((uint)(voltage * 3276.7) | 0x8000) + 170;
			//if (voltage > 9.9)
   //         {
   //             data = 0xFFFF;
   //         }
   //         //var para1 = PciProtocol.CreateExtCommand(RegisterDefinition.AnalogOut1, data);
   //         //foreach (var pair in para1)
   //         //{
   //         //    this.pci.WriteMemoryData(pair.Key, pair.Value);
   //         //    //Thread.Sleep(SEND_DELAY_MS);
   //         //}
        }

        public void SetFrequency(double frequency)
        {
            //uint freq = (uint)(1000000000 / (frequency * 30.30303030));
            //this.tcpClient.WriteIOData(RegisterDefinition.LaserFrequency, freq);
        }
    }
}
