using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Hardware.Common
{
    public class Requestor
    {
        public static byte[] CreateMovementDataCmd(List<LineSegment> lines, Dictionary<AxisTypes, MotorParameter> motorParaMap)
        {
            var list = new List<uint>();
            foreach (var m in lines)
            {
                list.AddRange(MotorUtil.LineSegmentToList(motorParaMap, m));
            }
            return Protocol.CreateRequest(Commands.MovementData, list.ToArray());
        }

        public static byte[] CreateMovementDataCmd(List<LineSegment> lines)
        {
            var list = new List<uint>();
            foreach (var m in lines)
            {
                list.AddRange(m.ToList());
            }
            return Protocol.CreateRequest(Commands.MovementData, list.ToArray());
        }
      
        public static byte[] CreatePosInfoCmd(MotorInfoMap<MotorInfoUnit> posInfo)
        {
            var part1 = new List<uint>();
            var part2 = new List<uint>();
            foreach (var m in Protocol.Axises)
            {
                part1.Add((uint)posInfo[m].Counter);
                part2.Add((uint)posInfo[m].Encoder);
            }
            part1.AddRange(part2);
            return Protocol.CreateRequest(Commands.SetMotorStatus, part1.ToArray());
        }

        public static byte[] CreateLaserCmd(LaserParameter laserPara, int index = 1)
        {
            var list = new List<uint>();
            list.Add((uint)laserPara.FrequencyHz);
            list.Add((uint)laserPara.DutyCircle);
            list.Add(laserPara.Enabled ? 0xA5A5A5A5 : 0x00);
            list.Add((uint)laserPara.VoltagePercentage);
            return Protocol.CreateRequest(index == 1 ? Commands.Laser1 : Commands.Laser2, list.ToArray());
        }

        public static byte[] CreateActionCmd(bool running)
        {
            var list = new List<uint>();
            //list.Add(running ? 0x00 : 0xA5A5A5A5);
            list.Add(running ? 0x00 : 0xFFFFFFFF);
            return Protocol.CreateRequest(Commands.Action, list.ToArray());
        }

        public static byte[] CreateSetOutputCmd(uint data)
        {
            return Protocol.CreateRequest(Commands.SetOutput, new uint[] { data });
        }

        public static byte[] CreateSetMotorEnableCmd(uint data)
        {
            return Protocol.CreateRequest(Commands.SetMotorEnable, new uint[] { data });
        }

        public static byte[] CreateSetMotorAlarmCmd(uint data)
        {
            return Protocol.CreateRequest(Commands.SetMotorAlarm, new uint[] { data });
        }

        public static byte[] CreateGetStatusCmd()
        {
            return Protocol.CreateRequest(Commands.GetMotorStatus, null);
        }

        public static byte[] CreateGetVersionCmd()
        {
            return Protocol.CreateRequest(Commands.GetVersion, null);
        }

        public static byte[] CreateGetHeightCmd()
        {
            return Protocol.CreateRequest(Commands.GetHeight, null);
        }

        public static byte[] CreateGetInputCmd()
        {
            return Protocol.CreateRequest(Commands.GetInput, null);
        }

        public static byte[] CreateGetAnalogCmd()
        {
            return Protocol.CreateRequest(Commands.GetAnalog, null);
        }

        public static byte[] CreateGetCacheStatusCmd()
        {
            return Protocol.CreateRequest(Commands.GetCacheStatus, null);
        }

        public static byte[] CreateGetMotorAlarmCmd()
        {
            return Protocol.CreateRequest(Commands.GetMotorAlarm, null);
        }
    }
}
