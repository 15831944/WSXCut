using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WSX.Engine.Models;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Engine.Utilities
{
    public static class MovementUtil
    {       
        private static double DISTANCE_RATIO = 0.5;
        private static double SPEED_STEP = 5;
        private static double MIN_CIRCLE_RADIUS = 2;
        private static double MIN_LINE_LENGTH = 2;   //unit: mm

        public static double DYNAMIC_SPEED_THRESHOLD = 60;
        public static double MIN_SPEED = 20;         //unit: mm/s

        public static List<LineSegment> GetArcData(List<PointF> points, double xySpeed, double acceleration)
        {
            List<LineSegment> segments = new List<LineSegment>();
            Action<double> handler = speed =>
            {
                segments.Clear();
                for (int i = 0; i < points.Count - 1; i++)
                {
                    var item = GetBasicLineSegmentPara(points[i], points[i + 1], speed);
                    segments.Add(item);
                }
            };

            if (xySpeed < DYNAMIC_SPEED_THRESHOLD)
            {
                handler.Invoke(xySpeed);
            }
            else
            {
                double len = MathEx.GetDistance(points);
                double radius = (len / points.Count) * 360 / (2 * Math.PI);
                int cnt = (int)((xySpeed - MIN_SPEED) / SPEED_STEP);

                bool condition1 = radius > MIN_CIRCLE_RADIUS;
                bool condition2 = points.Count > 2 * cnt;

                if (condition1)
                {
                    bool isValid = true;

                    if (!condition2)
                    {
                        if (points.Count > 5)
                        {
                            cnt = (points.Count - 1) / 2;
                            xySpeed = SPEED_STEP * cnt;
                        }
                        else
                        {
                            isValid = false;
                            xySpeed = MIN_SPEED;
                        }
                    }

                    handler.Invoke(xySpeed);

                    if (isValid)
                    {
                        for (int i = 0; i < cnt; i++)
                        {
                            var segment1 = segments[i];
                            var segment2 = segments[segments.Count - 1 - i];
                            double ratio = (MIN_SPEED + i * SPEED_STEP) / xySpeed;

                            segment1.XSpeed *= ratio;
                            segment1.YSpeed *= ratio;

                            segment2.XSpeed *= ratio;
                            segment2.YSpeed *= ratio;
                        }
                    }
                }
                else
                {
                    handler.Invoke(MIN_SPEED);
                }
            }

            return segments;
        }

        public static List<LineSegment> GetPolylineData(List<PointF> points, double xySpeed, double acceleration)
        {
            List<LineSegment> segments = new List<LineSegment>();
            for (int i = 0; i < points.Count - 1; i++)
            {
                var item = GetLineSegments(points[i], points[i + 1], xySpeed, acceleration);
                segments.AddRange(item);
            }
            return segments;
        }

        public static List<LineSegment> GetLineSegments(PointF p1, PointF p2, double xySpeed, double acceleration)
        {
            var lines = new List<LineSegment>();
            var line = GetBasicLineSegmentPara(p1, p2, xySpeed);
            double speed = new List<double> { line.XSpeed, line.YSpeed, line.ZSpeed, line.WSpeed }.Max();

            if (speed < DYNAMIC_SPEED_THRESHOLD)
            {
                lines.Add(line);
            }
            else
            {
                double time = (speed - MIN_SPEED) / acceleration;
                double distance = MIN_SPEED * time + 0.5 * acceleration * Math.Pow(time, 2);
                double maxDistance = new List<double> { Math.Abs(line.XDistance), Math.Abs(line.YDistance), Math.Abs(line.ZDistance), Math.Abs(line.WDistance) }.Max();

                distance *= DISTANCE_RATIO;

                bool isNeedToSplit = true;
                if (maxDistance < 2.2 * distance)
                {
                    double lowSpeed = 0;
                    if (maxDistance < 1.2 * MIN_LINE_LENGTH)
                    {
                        lowSpeed = MIN_SPEED;
                        isNeedToSplit = false;
                    }
                    else
                    {
                        double tmp = maxDistance - MIN_LINE_LENGTH;
                        lowSpeed = Math.Sqrt(tmp / acceleration) * acceleration;
                        lowSpeed = Math.Round(lowSpeed / 5.0) * 5.0;
                        if (lowSpeed <= 1.2 * MIN_SPEED)
                        {
                            isNeedToSplit = false;
                        }
                    }
                    line = GetBasicLineSegmentPara(p1, p2, lowSpeed);
                }

                if (isNeedToSplit)
                {
                    lines = SplitWithAcceleration(line, acceleration, true);
                    var lastLine = lines[lines.Count - 1];
                    lines.Remove(lastLine);
                    lines.AddRange(SplitWithAcceleration(lastLine, acceleration, false));

                    #region Make compensation
                    if (!SystemContext.IsDummyMode)
                    {
                        var motor = SystemContext.Hardware.Motor;

                        double xRatio = motor.GetRatio(AxisTypes.AxisX);
                        int xPulse1 = (int)Math.Round(line.XDistance * xRatio);
                        int xPulse2 = 0;
                        foreach (var m in lines)
                        {
                            xPulse2 += (int)Math.Round(m.XDistance * xRatio);
                        }
                        double xDiff = (xPulse1 - xPulse2) / xRatio;

                        double yRatio = motor.GetRatio(AxisTypes.AxisY);
                        int yPulse1 = (int)Math.Round(line.YDistance * yRatio);
                        int yPulse2 = 0;
                        foreach (var m in lines)
                        {
                            yPulse2 += (int)Math.Round(m.YDistance * yRatio);
                        }
                        double yDiff = (yPulse1 - yPulse2) / yRatio;

                        double zRatio = motor.GetRatio(AxisTypes.AxisZ);
                        int zPulse1 = (int)Math.Round(line.ZDistance * zRatio);
                        int zPulse2 = 0;
                        foreach (var m in lines)
                        {
                            zPulse2 += (int)Math.Round(m.ZDistance * zRatio);
                        }
                        double zDiff = (zPulse1 - zPulse2) / zRatio;

                        double wRatio = motor.GetRatio(AxisTypes.AxisW);
                        int wPulse1 = (int)Math.Round(line.WDistance * wRatio);
                        int wPulse2 = 0;
                        foreach (var m in lines)
                        {
                            wPulse2 += (int)Math.Round(m.WDistance * wRatio);
                        }
                        double wDiff = (wPulse1 - wPulse2) / wRatio;

                        lines[lines.Count / 2].XDistance += xDiff;
                        lines[lines.Count / 2].YDistance += yDiff;
                        lines[lines.Count / 2].ZDistance += zDiff;
                        lines[lines.Count / 2].WDistance += wDiff;
                    }
                    #endregion
                }
                else
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        private static List<LineSegment> SplitWithAcceleration(LineSegment segment, double acceleration, bool isSpeedUp)
        {
            double speed = new List<double> { segment.XSpeed, segment.YSpeed, segment.ZSpeed, segment.WSpeed }.Max();
            if (segment.ZSpeed == 0 && segment.WSpeed == 0)
            {
                speed = Math.Sqrt(Math.Pow(segment.XSpeed, 2) + Math.Pow(segment.YSpeed, 2));
            }
            double time = (speed - MIN_SPEED) / acceleration;
            double distance = MIN_SPEED * time + 0.5 * acceleration * Math.Pow(time, 2);           

            distance *= DISTANCE_RATIO;

            #region Calculate speed up or down distance
            double xDistance = (distance * segment.XSpeed / speed);
            double xLeft = Math.Abs(segment.XDistance) - xDistance;
            if (xLeft < 0)
            {
                xDistance = Math.Abs(segment.XDistance);
            }

            double yDistance = (distance * segment.YSpeed / speed);
            double yLeft = Math.Abs(segment.YDistance) - yDistance;
            if (yLeft < 0)
            {
                yDistance = Math.Abs(segment.YDistance);
            }

            double zDistance = (distance * segment.ZSpeed / speed);
            double zLeft = Math.Abs(segment.ZDistance) - zDistance;
            if (zLeft < 0)
            {
                zDistance = Math.Abs(segment.ZDistance);
            }

            double wDistance = (distance * segment.WSpeed / speed);
            double wLeft = Math.Abs(segment.WDistance) - wDistance;
            if (wLeft < 0)
            {
                wDistance = Math.Abs(segment.WDistance);
            }
            #endregion

            #region Split lineSegment
            int total = (int)((speed - MIN_SPEED) / SPEED_STEP);
            if (total < SPEED_STEP)
            {
                total = (int)SPEED_STEP;
            }

            var lines = new List<LineSegment>();
            for (int i = 0; i < total; i++)
            {
                int index = isSpeedUp ? i : total - 1 - i;
                double ratio = (MIN_SPEED + index * SPEED_STEP) / speed;
                var line = new LineSegment
                {
                    XDistance = xDistance / total,
                    XSpeed = ratio * segment.XSpeed,
                    YDistance = yDistance / total,
                    YSpeed = ratio * segment.YSpeed,
                    ZDistance = zDistance / total,
                    ZSpeed = ratio * segment.ZSpeed,
                    WDistance = wDistance / total,
                    WSpeed = ratio * segment.WSpeed,
                };
                lines.Add(line);
            }
            #endregion

            #region Insert final
            if (xLeft > 0 || yLeft > 0 || zLeft > 0 || wLeft > 0)
            {
                var line = new LineSegment
                {
                    XDistance = xLeft,
                    XSpeed = segment.XSpeed,
                    YDistance = yLeft,
                    YSpeed = segment.YSpeed,
                    ZDistance = zLeft,
                    ZSpeed = segment.ZSpeed,
                    WDistance = wLeft,
                    WSpeed = segment.WSpeed,
                };

                if (isSpeedUp)
                {
                    lines.Add(line);
                }
                else
                {
                    lines.Insert(0, line);
                }
            }
            #endregion

            #region Update direction
            double xFactor = segment.XDistance < 0 ? -1 : 1;
            double yFactor = segment.YDistance < 0 ? -1 : 1;
            double zFactor = segment.ZDistance < 0 ? -1 : 1;
            double wFactor = segment.WDistance < 0 ? -1 : 1;

            for (int i = 0; i < lines.Count; i++)
            {
                var m = lines[i];
                m.XDistance *= xFactor;
                m.YDistance *= yFactor;
                m.ZDistance *= zFactor;
                m.WDistance *= wFactor;
            }
            #endregion

            return lines;
        }

        public static LineSegment GetBasicLineSegmentPara(PointF p1, PointF p2, double xySpeed)
        {
            var lineSegment = new LineSegment
            {
                XDistance = p2.X - p1.X,
                YDistance = p2.Y - p1.Y
            };

            double xOffset = Math.Abs(p2.X - p1.X);
            double yOffset = Math.Abs(p2.Y - p1.Y);
            double maxOffset = MathEx.GetDistance(p1, p2);

            if (xOffset >= 0.001)
            {
                lineSegment.XSpeed = xySpeed * (xOffset / maxOffset);
            }
            if (yOffset >= 0.001)
            {
                lineSegment.YSpeed = xySpeed * (yOffset / maxOffset);
            }

            return lineSegment;
        }

        public static List<LineSegment> GetLineSegments(List<DataUnit> items, double speed, double acceleration)
        {
            var lines = new List<LineSegment>();
            foreach (var m in items)
            {               
                if (m.Id == DataUnitTypes.Polyline)
                {
                    var tmp = MovementUtil.GetPolylineData(m.Points, speed, acceleration);
                    lines.AddRange(tmp);
                }
                else
                {
                    var tmp = MovementUtil.GetArcData(m.Points, speed, acceleration);
                    lines.AddRange(tmp);
                }
            }
            return lines;
        }

        //public static List<LineSegment> GetLineSegments(List<DataUnit> items, double xSpeed, double ySpeed, double xAcceleration, double yAcceleration)
        //{
        //    return null;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="speed"></param>
        /// <param name="acceleration"></param>
        /// <returns>Item1: time, Item2: distance</returns>
        public static Tuple<double, double> GetMachineInfo(List<DataUnit> items, double speed, double acceleration)
        {
            double sumLen = 0;
            double sumTime = 0;

            foreach (var m in items)
            {
                double len = m.Length;
                double time = 0;
                var lines = new List<LineSegment>();
                if (m.Id == DataUnitTypes.Polyline)
                {
                    lines = MovementUtil.GetPolylineData(m.Points, speed, acceleration);
                }
                else
                {
                    lines = MovementUtil.GetArcData(m.Points, speed, acceleration);
                }
                foreach (var m1 in lines)
                {
                    double distance = Math.Sqrt(Math.Pow(m1.XDistance, 2) + Math.Pow(m1.YDistance, 2));
                    double speed1 = Math.Sqrt(Math.Pow(m1.XSpeed, 2) + Math.Pow(m1.YSpeed, 2));
                    double time1 = distance / speed1;
                    if (!double.IsNaN(time1) && !double.IsInfinity(time1))
                    {
                        time += time1;
                    }
                }
                sumLen += len;
                sumTime += time;
            }

            return Tuple.Create(sumTime, sumLen);
        }

        public static Tuple<double, double> GetMachineInfo(List<LineSegment> segments)
        {
            double sumTime = 0;
            double sumLen = 0;

            foreach (var m in segments)
            {
                double distance = Math.Sqrt(Math.Pow(m.XDistance, 2) + Math.Pow(m.YDistance, 2));
                double speed = Math.Sqrt(Math.Pow(m.XSpeed, 2) + Math.Pow(m.YSpeed, 2));             
                sumLen += distance;
                double time = (distance / speed);
                if (!double.IsNaN(time) && !double.IsInfinity(time))
                {
                    sumTime += time;
                }
            }

            return Tuple.Create(sumTime, sumLen);
        }
    }

    public class MachineInformation
    {
        public double CutLen { get; set; }
        public double EmptyMoveLen { get; set; }
        public int PiercingTimes { get; set; }
        public double CutTime { get; set; }
        public double EmptyMoveTime { get; set; }
        public double Delay { get; set; }     
        public double TotalTime
        {
            get
            {
                return this.CutTime + this.EmptyMoveTime + this.Delay;
            }
        }

        public static MachineInformation Combine(List<MachineInformation> machineInfos)
        {
            var info = new MachineInformation();
            foreach (var m in machineInfos)
            {
                info.CutLen += m.CutLen;
                info.EmptyMoveLen += m.EmptyMoveLen;
                info.PiercingTimes += m.PiercingTimes;
                info.CutTime += m.CutTime;
                info.EmptyMoveTime += m.EmptyMoveTime;
                info.Delay += m.Delay;
            }
            return info;
        }
    }
}
