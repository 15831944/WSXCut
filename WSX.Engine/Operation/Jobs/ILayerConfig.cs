using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.Hardware.Models;

namespace WSX.Engine.Operation.Jobs
{
    public interface ILayerConfig
    {
        double LaserOnDelay { get; }
        double LaserOffDelay { get; }
        double NozzleHeight { get; }
        double LiftHeight { get; }
        bool NeedToLift { get; }
        bool NeedToBlowingOff { get; }
        bool NeedToLaserOff { get; }
        LaserParameter LaserPara { get; }
        Tuple<string, double> GasPara { get; }   
    }
}
