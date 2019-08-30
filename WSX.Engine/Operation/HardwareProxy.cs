using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using WSX.Engine.Utilities;
using WSX.Hardware.IO;
using WSX.Hardware.Laser;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation
{
    public class HardwareProxy
    {
        private uint blowing_pin_1 = IOPinDefinition.Pin_0;
        private uint blowing_pin_2 = IOPinDefinition.Pin_1;
     
        public IMotorController Motor { get; private set; }
        public ILaser Laser { get; private set; }
        public IAssistantIO Blowing { get; private set; }
        public bool IsLaserOn { get; private set; }
        public bool IsBlowingOn { get; private set; }
        public bool IsFollowOn { get; set; }

        public HardwareProxy(IMotorController motor, ILaser laser, IAssistantIO blowing)
        {
            this.Motor = motor;
            this.Laser = laser;
            this.Blowing = blowing;
        }

        public void UpdateBlowingPins(uint pin1, uint pin2)
        {
            this.blowing_pin_1 = pin1;
            this.blowing_pin_2 = pin2;
        }

        public void MoveTo(PointF point, double speed, double acceleration, CancellationToken token, Action<MotorInfoMap<double>, MotorInfoMap<double>> infoHandler)
        {
            var posInfo = this.Motor.CurrentPosInfo;
            var p1 = new PointF((float)posInfo[AxisTypes.AxisX], (float)posInfo[AxisTypes.AxisY]);
            var p2 = point;
            var lines = MovementUtil.GetLineSegments(p1, p2, speed, acceleration);
            this.Motor.MoveAsync(lines, token, infoHandler).Wait();
        }

        public void MoveTo(AxisTypes axis, double step, double speed, CancellationToken token, Action<MotorInfoMap<double>, MotorInfoMap<double>> infoHandler)
        {
            var line = new LineSegment();
            if (axis == AxisTypes.AxisX)
            {
                line.XDistance = step;
                line.XSpeed = speed;
            }
            else if (axis == AxisTypes.AxisY)
            {
                line.YDistance = step;
                line.YSpeed = speed;
            }
            else if (axis == AxisTypes.AxisZ)
            {
                line.ZDistance = step;
                line.ZSpeed = speed;
            }

            var lines = new List<LineSegment> { line };
            this.Motor.MoveAsync(lines, token, infoHandler).Wait();
        }

        public Task<MovementStatus> MoveAsync(List<LineSegment> segments, CancellationToken token, Action<MotorInfoMap<double>, MotorInfoMap<double>> infoHandler)
        {
            return this.Motor.MoveAsync(segments, token, infoHandler);
        }

        public PointF GetCurrentPosition()
        {
            var posInfo = this.Motor.CurrentPosInfo;
            return new PointF((float)posInfo[AxisTypes.AxisX], (float)posInfo[AxisTypes.AxisY]);
        }

        public void SetLaserPara(LaserParameter laserPara)
        {
            this.Laser.SetParameter(laserPara);
        }

        public void LaserOn()
        {
            this.Laser.LaserOn();
            this.IsLaserOn = true;
        }

        public void LaserOn(LaserParameter laserPara)
        {           
            this.SetLaserPara(laserPara);
            this.LaserOn();
        }

        public void LaserOff()
        {
            this.Laser.LaserOff();
            this.IsLaserOn = false;
        }

        public void SetBlowingPara(string gasType, double pressure)
        {

        }

        public void BlowingOn()
        {
            this.Blowing.ResetBits(blowing_pin_1 | blowing_pin_2);
            this.IsBlowingOn = true;
        }

        public void BlowingOff()
        {
            this.Blowing.SetBits(blowing_pin_1 | blowing_pin_2);
            this.IsBlowingOn = false;
        }

        public void FollowOn(double height = double.NaN)
        {
            this.IsFollowOn = true;
        }

        public void FollowOff()
        {
            this.IsFollowOn = false;
        }

        public void MoveZ(double pos, bool relativeMove, bool blocking, CancellationToken token)
        {



            if (blocking)
            {
                //Wait for motion end
            }
        }

        public void MoveZ(double followHeight)
        {


        }
    }
}
