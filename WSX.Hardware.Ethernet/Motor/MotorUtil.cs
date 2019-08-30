using System;
using System.Collections.Generic;
using System.Linq;
using WSX.CommomModel.Utilities;
using WSX.Hardware.Models;

namespace WSX.Hardware.Motor
{
    public class MotorUtil
    {
        private const int PULSE_SPLIT_STEP = 1000;
        private const int MIN_SPLIT_PULSE = 100;
        private const int FPGA_FREQUENCY_HZ = 100000000;  //330000000 -- Legacy

        public static uint CalSpeed(uint speed)
        {
            uint content = 0;
            if (speed != 0)
            {
                content = FPGA_FREQUENCY_HZ / speed;
            }
            return content;
        }

        public static List<LineSegment> SplitLines(Dictionary<AxisTypes, MotorParameter> motorParaMap, List<LineSegment> lines)
        {
            var list = new List<LineSegment>();
            foreach (var m in lines)
            {
                if (motorParaMap[AxisTypes.AxisY].IsDualDriven)
                {
                    list.AddRange(SplitLine(ToLineSegment(motorParaMap, m), PULSE_SPLIT_STEP));
                }
                else
                {
                    list.Add(ToLineSegment(motorParaMap, m));
                }
            }
            return list;
        }

        public static LineSegment ToLineSegment(Dictionary<AxisTypes, MotorParameter> motorParaMap, LineSegment line)
        {
            var list = LineSegmentToList(motorParaMap, line);
            return new LineSegment
            {
                XDistance = list[0],
                XSpeed = list[1],
                YDistance = list[2],
                YSpeed = list[3],
                ZDistance = list[4],
                ZSpeed = list[5],
                ADistance = list[6],
                ASpeed = list[7],
                BDistance = list[8],
                BSpeed = list[9],
                CDistance = list[10],
                CSpeed = list[11],
                UDistance = list[12],
                USpeed = list[13],
                VDistance = list[14],
                VSpeed = list[15],
                WDistance = list[16],
                WSpeed = list[17],
            };
        }

        public static List<LineSegment> SplitLine(LineSegment line, int pulseStep)
        {
            var list = new List<LineSegment>();

            uint tmp = GetPositive((uint)line.YDistance);
            if (tmp <= pulseStep)
            {
                list.Add(line);
            }
            else
            {
                #region Step1: 获取每个轴距离的符号
                var symbolList = new List<bool>();
                symbolList.Add(IsNegative((uint)line.XDistance));
                symbolList.Add(IsNegative((uint)line.YDistance));
                symbolList.Add(IsNegative((uint)line.ZDistance));
                symbolList.Add(IsNegative((uint)line.ADistance));
                symbolList.Add(IsNegative((uint)line.BDistance));
                symbolList.Add(IsNegative((uint)line.CDistance));
                symbolList.Add(IsNegative((uint)line.UDistance));
                symbolList.Add(IsNegative((uint)line.VDistance));
                symbolList.Add(IsNegative((uint)line.WDistance));
                #endregion

                #region Step2: 获取轴距离的绝对值
                var newLine = new LineSegment
                {
                    XDistance = GetPositive((uint)line.XDistance),
                    XSpeed = line.XSpeed,
                    YDistance = GetPositive((uint)line.YDistance),
                    YSpeed = line.YSpeed,
                    ZDistance = GetPositive((uint)line.ZDistance),
                    ZSpeed = line.ZSpeed,

                    ADistance = GetPositive((uint)line.ADistance),
                    ASpeed = line.ASpeed,
                    BDistance = GetPositive((uint)line.BDistance),
                    BSpeed = line.BSpeed,
                    CDistance = GetPositive((uint)line.CDistance),
                    CSpeed = line.CSpeed,

                    UDistance = GetPositive((uint)line.UDistance),
                    USpeed = line.USpeed,
                    VDistance = GetPositive((uint)line.VDistance),
                    VSpeed = line.VSpeed,
                    WDistance = GetPositive((uint)line.WDistance),
                    WSpeed = line.WSpeed
                };
                #endregion

                #region Step3: 分割线段               
                var stepList = MotorUtil.Split((int)newLine.YDistance, pulseStep);              
                int last = stepList.Last();
                if (last < MIN_SPLIT_PULSE)
                {
                    stepList.RemoveAt(stepList.Count - 1);
                    stepList[stepList.Count - 1] += last;
                }

                foreach (var m in stepList)
                {
                    double ratio = m / newLine.YDistance;
                    var subLine = new LineSegment
                    {
                        XDistance = (uint)(newLine.XDistance * ratio),
                        XSpeed = newLine.XSpeed,
                        YDistance = m,
                        YSpeed = newLine.YSpeed,
                        ZDistance = (uint)(newLine.ZDistance * ratio),
                        ZSpeed = newLine.ZSpeed,

                        ADistance = (uint)(newLine.ADistance * ratio),
                        ASpeed = newLine.ASpeed,
                        BDistance = (uint)(newLine.BDistance * ratio),
                        BSpeed = newLine.BSpeed,
                        CDistance = (uint)(newLine.CDistance * ratio),
                        CSpeed = newLine.CSpeed,

                        UDistance = (uint)(newLine.UDistance * ratio),
                        USpeed = newLine.USpeed,
                        VDistance = (uint)(newLine.VDistance * ratio),
                        VSpeed = newLine.VSpeed,
                        WDistance = (uint)(newLine.WDistance * ratio),
                        WSpeed = newLine.WSpeed,
                    };
                    list.Add(subLine);
                }
                #endregion

                #region Step4: 补偿误差
                var tmp1 = new LineSegment();
                foreach (var m in list)
                {
                    tmp1.XDistance += m.XDistance;
                    tmp1.YDistance += m.YDistance;
                    tmp1.ZDistance += m.ZDistance;
                    tmp1.ADistance += m.ADistance;
                    tmp1.BDistance += m.BDistance;
                    tmp1.CDistance += m.CDistance;
                    tmp1.UDistance += m.UDistance;
                    tmp1.VDistance += m.VDistance;
                    tmp1.WDistance += m.WDistance;
                }
                var lastLine = list.Last();
                lastLine.XDistance += newLine.XDistance - tmp1.XDistance;
                lastLine.YDistance += newLine.YDistance - tmp1.YDistance;
                lastLine.ZDistance += newLine.ZDistance - tmp1.ZDistance;
                lastLine.ADistance += newLine.ADistance - tmp1.ADistance;
                lastLine.BDistance += newLine.BDistance - tmp1.BDistance;
                lastLine.CDistance += newLine.CDistance - tmp1.CDistance;
                lastLine.UDistance += newLine.UDistance - tmp1.UDistance;
                lastLine.VDistance += newLine.VDistance - tmp1.VDistance;
                lastLine.WDistance += newLine.WDistance - tmp1.WDistance;
                #endregion

                #region Step5: 更新符号
                foreach (var m in list)
                {
                    if (symbolList[0])
                    {
                        m.XDistance = ToNegative((uint)m.XDistance);
                    }
                    if (symbolList[1])
                    {
                        m.YDistance = ToNegative((uint)m.YDistance);
                    }
                    if (symbolList[2])
                    {
                        m.ZDistance = ToNegative((uint)m.ZDistance);
                    }

                    if (symbolList[3])
                    {
                        m.ADistance = ToNegative((uint)m.ADistance);
                    }
                    if (symbolList[4])
                    {
                        m.BDistance = ToNegative((uint)m.BDistance);
                    }
                    if (symbolList[5])
                    {
                        m.CDistance = ToNegative((uint)m.CDistance);
                    }

                    if (symbolList[6])
                    {
                        m.UDistance = ToNegative((uint)m.UDistance);
                    }
                    if (symbolList[7])
                    {
                        m.VDistance = ToNegative((uint)m.VDistance);
                    }
                    if (symbolList[8])
                    {
                        m.WDistance = ToNegative((uint)m.WDistance);
                    }
                }
                #endregion
            }

            return list;
        }

        private static bool IsNegative(uint num)
        {
            return (num & 0x80000000) != 0;
        }

        private static uint GetPositive(uint num)
        {
            return num & 0x7FFFFFFF;
        }

        private static uint ToNegative(uint num)
        {
            return num | 0x80000000;
        }

        public static List<uint> LineSegmentToList(Dictionary<AxisTypes, MotorParameter> motorParaMap, LineSegment line)
        {
            var list = new List<uint>();

            //Axis X
            uint distance = ConvertDistance(motorParaMap[AxisTypes.AxisX], line.XDistance);
            uint speed = ConvertSpeed(motorParaMap[AxisTypes.AxisX], line.XSpeed);
            list.Add(distance);
            list.Add(speed);
            //Axis Y
            distance = ConvertDistance(motorParaMap[AxisTypes.AxisY], line.YDistance);
            speed = ConvertSpeed(motorParaMap[AxisTypes.AxisY], line.YSpeed);
            list.Add(distance);
            list.Add(speed);
            //Axis Z
            distance = ConvertDistance(motorParaMap[AxisTypes.AxisZ], line.ZDistance);
            speed = ConvertSpeed(motorParaMap[AxisTypes.AxisZ], line.ZSpeed);
            list.Add(distance);
            list.Add(speed);
            //Axis A
            distance = ConvertDistance(motorParaMap[AxisTypes.AxisA], line.ADistance);
            speed = ConvertSpeed(motorParaMap[AxisTypes.AxisA], line.ASpeed);
            list.Add(distance);
            list.Add(speed);
            //Axis B
            distance = ConvertDistance(motorParaMap[AxisTypes.AxisB], line.BDistance);
            speed = ConvertSpeed(motorParaMap[AxisTypes.AxisB], line.BSpeed);
            list.Add(distance);
            list.Add(speed);
            //Axis C
            distance = ConvertDistance(motorParaMap[AxisTypes.AxisC], line.CDistance);
            speed = ConvertSpeed(motorParaMap[AxisTypes.AxisC], line.CSpeed);
            list.Add(distance);
            list.Add(speed);
            //Axis U
            distance = ConvertDistance(motorParaMap[AxisTypes.AxisU], line.UDistance);
            speed = ConvertSpeed(motorParaMap[AxisTypes.AxisU], line.USpeed);
            list.Add(distance);
            list.Add(speed);
            //Axis V
            distance = ConvertDistance(motorParaMap[AxisTypes.AxisV], line.VDistance);
            speed = ConvertSpeed(motorParaMap[AxisTypes.AxisV], line.VSpeed);
            list.Add(distance);
            list.Add(speed);
            //Axis W
            distance = ConvertDistance(motorParaMap[AxisTypes.AxisW], line.WDistance);
            speed = ConvertSpeed(motorParaMap[AxisTypes.AxisW], line.WSpeed);
            list.Add(distance);
            list.Add(speed);

            return list;
        }

        public static MotorInformation GetMotorInformation(Dictionary<AxisTypes, MotorParameter> motorParaMap, MotorInfoMap<MotorInfoUnit> motorInfoMap)
        {
            var res = new MotorInformation();
            foreach (var m in motorParaMap.Keys)
            {
                res.PosInfo[m] = ConvertBackDistance(motorParaMap[m], motorInfoMap[m].Counter);
                res.EncoderInfo[m] = ConvertBackDistance(motorParaMap[m], motorInfoMap[m].Encoder);
                res.SpeedInfo[m] = ConvertBackSpeed(motorParaMap[m], motorInfoMap[m].Speed);
                res.StatusInfo[m] = MotorStatus.FromRegContent((uint)motorInfoMap[m].Status);
            }
            return res;
        }

        public static uint ConvertDistance(MotorParameter motorPara, double distance)
        {
            distance *= motorPara.IsReversed ? -1 : 1;
            //uint content = (uint)(Math.Abs(distance) * this.motorParaMap[axis].Ratio);
            uint content = (uint)Math.Round((Math.Abs(distance) * motorPara.Ratio));
            if (distance < 0)
            {
                content |= 0x80000000;
            }
            return content;
        }

        public static double ConvertBackDistance(MotorParameter motorPara, int pulseDistance)
        {       
            double factor = motorPara.IsReversed ? -1 : 1;
            return Math.Round(pulseDistance / motorPara.Ratio * factor, 2);
        }

        public static uint ConvertSpeed(MotorParameter motorPara, double speed)
        {
            uint content = (uint)(speed * motorPara.Ratio);
            return CalSpeed(content);
        }

        public static double ConvertBackSpeed(MotorParameter motorPara, int pulseSpeed)
        {
            return CalSpeed((uint)pulseSpeed) / motorPara.Ratio;
        }

        public static MotorInfoMap<bool> ParseAlarmInfo(uint data)
        {
            var info = new MotorInfoMap<bool>();
            var axises = EnumHelper.GetAllEnumMembers<AxisTypes>();
            foreach (var m in axises)
            {
                info[m] = (data & (1 << (int)m)) != 0;
            }
            return info;
        }

        public static List<int> Split(int total, int step)
        {
            List<int> items = new List<int>();
            int cnt = total / step;

            for (int i = 0; i < cnt; i++)
            {
                items.Add(step);
            }

            int leftPart = total % step;
            if (leftPart != 0)
            {
                items.Add(leftPart);
            }

            return items;
        }
    }
}
