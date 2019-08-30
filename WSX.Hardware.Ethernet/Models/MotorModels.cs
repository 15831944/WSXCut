using System.Collections.Generic;
using WSX.Hardware.Motor;

namespace WSX.Hardware.Models
{
    public class LineSegment
    {
        public double XDistance { get; set; }
        public double XSpeed { get; set; }
        public double YDistance { get; set; }
        public double YSpeed { get; set; }
        public double ZDistance { get; set; }
        public double ZSpeed { get; set; }
        public double ADistance { get; set; }
        public double ASpeed { get; set; }
        public double BDistance { get; set; }
        public double BSpeed { get; set; }
        public double CDistance { get; set; }
        public double CSpeed { get; set; }
        public double UDistance { get; set; }
        public double USpeed { get; set; }
        public double VDistance { get; set; }
        public double VSpeed { get; set; }
        public double WDistance { get; set; }
        public double WSpeed { get; set; }

        public List<uint> ToList()
        {
            var list = new List<uint>();
            list.Add((uint)this.XDistance);
            list.Add((uint)this.XSpeed);
            list.Add((uint)this.YDistance);
            list.Add((uint)this.YSpeed);
            list.Add((uint)this.ZDistance);
            list.Add((uint)this.ZSpeed);
            list.Add((uint)this.ADistance);
            list.Add((uint)this.ASpeed);
            list.Add((uint)this.BDistance);
            list.Add((uint)this.BSpeed);
            list.Add((uint)this.CDistance);
            list.Add((uint)this.CSpeed);
            list.Add((uint)this.UDistance);
            list.Add((uint)this.USpeed);
            list.Add((uint)this.VDistance);
            list.Add((uint)this.VSpeed);
            list.Add((uint)this.WDistance);
            list.Add((uint)this.WSpeed);
            return list;
        }
    }

    public class MotorInfoUnit
    {
        public int Counter { get; set; }
        public int Encoder { get; set; }
        public int Speed { get; set; }
        public int Status { get; set; }
    }

    public class MotorInformation
    {
        public MotorInfoMap<double> PosInfo { get; set; } = new MotorInfoMap<double>();
        public MotorInfoMap<double> EncoderInfo { get; set; } = new MotorInfoMap<double>();
        public MotorInfoMap<double> SpeedInfo { get; set; } = new MotorInfoMap<double>();
        public MotorInfoMap<MotorStatus> StatusInfo { get; set; } = new MotorInfoMap<MotorStatus>();     
    }

    public class MotorParameter
    {
        public double Ratio { get; set; } = 1;
        //public double ZeroPos { get; set; }
        public bool IsDualDriven { get; set; }
        public bool IsReversed { get; set; }
    }

    public class MotorStatus
    {
        public bool IsMoving { get; set; }    
        public bool IsNegativeLimited { get; set; }
        public bool IsPositiveLimited { get; set; }
        public bool IsHome { get; set; }

        public static MotorStatus FromRegContent(uint content)
        {        
            return new MotorStatus
            {                
                IsMoving = (content & (1 << 0)) != 0,
                IsPositiveLimited = (content & (1 << 1)) != 0,
                IsNegativeLimited = (content & (1 << 2)) != 0,
                IsHome = (content & (1 << 3)) != 0,
            };
        }
    }
}
