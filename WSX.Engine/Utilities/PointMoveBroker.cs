using System;
using System.Drawing;
using System.Threading;
using WSX.CommomModel.ParaModel;
using WSX.Engine.Models;
using WSX.Engine.Operation;
using WSX.GlobalData.Model;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Engine.Utilities
{
    public class PointMoveBroker
    {
        private readonly ManualParameter manualPara;
        private readonly PointMoveCutModel pointMovePara;
        private CancellationToken token;

        public PointMoveBroker(ManualParameter para)
        {
            this.manualPara = para;
            this.pointMovePara = GlobalModel.Params.LayerConfig.PointMoveCut;
        }

        public void Execute(CancellationToken token)
        {
            this.token = token;
            this.MakeMovementBefore();
            this.MakeMovement();
            this.MakeMovementAfter();
        }

        private void MakeMovementBefore()
        {
            if (!manualPara.IsPointMoveCut)
            {
                return;
            }
          
            bool pierceNeeded = this.pointMovePara.PierceLevel != PierceLevels.None;
            if (pierceNeeded)
            {              
                var config = new PierceConfig
                {
                    Level = this.pointMovePara.PierceLevel,
                    Level1Para = this.pointMovePara.PierceLevel1,
                    Level2Para = this.pointMovePara.PierceLevel2,
                    Level3Para = this.pointMovePara.PierceLevel3
                };
                var broker = new PierceBroker(config);
                broker.OnLaserEnabledChanged += x => OperationEngine.Instance.NotifyLaserEnabled(x);
                broker.OnLaserParaChanged += x=> OperationEngine.Instance.NotifyLaserParaChanged(x);
                broker.OnBlowingEnabledChanged += x => OperationEngine.Instance.NotifyBlowingEnabled(x);
                broker.OnFollowChanged += x => OperationEngine.Instance.NotifyFollowEnabled(x);
                broker.Execute(this.token);              
            }

            this.MoveZ(this.pointMovePara.NozzleHeight);
            this.BlowingOn();
            this.LaserOn();          
        }

        private void MakeMovement()
        {
            SystemContext.Hardware.Motor.WriteMovingFlag(true);
            if (!double.IsNaN(manualPara.Step))
            {
                SystemContext.Hardware?.MoveTo(manualPara.Axis, manualPara.Step, manualPara.Speed, token, this.HandleMotorInfo);
            }
            else
            {
                SystemContext.Hardware?.MoveTo(manualPara.TargetPoint, manualPara.Speed, manualPara.Acceleration, token, this.HandleMotorInfo);
            }
            SystemContext.Hardware.Motor.WriteMovingFlag(false);
        }

        private void MakeMovementAfter()
        {
            if (!manualPara.IsPointMoveCut)
            {
                return;
            }
       
            this.LaserOff();        
            this.BlowingOff();
            this.MoveZ(this.pointMovePara.LiftHeight);
        }

        private void HandleMotorInfo(MotorInfoMap<double> posInfo, MotorInfoMap<double> speedInfo)
        {
            OperationEngine.Instance.ReportMotorPos(posInfo);
            OperationEngine.Instance.ReportMotorSpeed(speedInfo);
            OperationEngine.Instance.NotifyMarkPointChanged(new PointF((float)posInfo[AxisTypes.AxisX], (float)posInfo[AxisTypes.AxisY]));
        }

        private LaserParameter GetLaserPara()
        {
            return new LaserParameter
            {
                VoltagePercentage = this.pointMovePara.PowerPercent,
                FrequencyHz = this.pointMovePara.PulseFrequency,
                DutyCircle = this.pointMovePara.PulseDutyFactorPercent
            };
        }

        private void LaserOn()
        {
            //if (this.token.IsCancellationRequested)
            //{
            //    return;
            //}
            var laserPara = this.GetLaserPara();
            SystemContext.Hardware?.LaserOn(laserPara);
            OperationEngine.Instance.NotifyLaserEnabled(true);
            OperationEngine.Instance.NotifyLaserParaChanged(laserPara);         
            this.token.WaitHandle.WaitOne((int)this.pointMovePara.DelayTime);           
        }

        private void LaserOff()
        {
            //if (this.token.IsCancellationRequested)
            //{
            //    return;
            //}
            this.token.WaitHandle.WaitOne((int)this.pointMovePara.LaserOffDelay);
            SystemContext.Hardware?.LaserOff();
            OperationEngine.Instance.NotifyLaserEnabled(false);
        }

        private void BlowingOn()
        {
            //if (this.token.IsCancellationRequested)
            //{
            //    return;
            //}
            SystemContext.Hardware?.BlowingOn();
            OperationEngine.Instance.NotifyBlowingEnabled(true);
        }

        private void BlowingOff()
        {
            //if (this.token.IsCancellationRequested)
            //{
            //    return;
            //}
            SystemContext.Hardware?.BlowingOff();
            OperationEngine.Instance.NotifyBlowingEnabled(false);
        }

        private void MoveZ(double height)
        {

        }
    }
}
