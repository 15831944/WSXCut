using System;
using WSX.Hardware.Common;
using WSX.Hardware.Models;

namespace WSX.Hardware.Laser
{

    public class LaserControllerFactory
    {
        public static ILaser CreateLaserController(LaserConfig laserPara, TcpClientStream tcpClient)
        {
            ILaser laser = null;
            string typeStr = laserPara.LaserType.ToString();
           
            switch (typeStr)
            {
                case "Raycus":
                    if (laserPara.ControlType == LaserControlTypes.Extern)
                    {
                        laser = new LaserController(tcpClient, laserPara.MaxVoltage);
                    }
                    else
                    {
                        var serialPortStream = new SerialPortStream(laserPara.PortName, 9600);
                        laser = new RaycusLaserController(serialPortStream);
                    }
                    break;

                case "Other":
                    laser = new LaserController(tcpClient, laserPara.MaxVoltage);
                    break;

                default:
                    throw new Exception("LaserFactory: not support!");
            }

            return laser;
        }
    }
}
