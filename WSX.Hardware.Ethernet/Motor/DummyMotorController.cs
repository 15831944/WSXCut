using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WSX.CommomModel.Utilities;
using WSX.Hardware.Models;

namespace WSX.Hardware.Motor
{
    public class DummyMotorController : IMotorController
    {
        private const int INTERVAL_MS = 2;
        private const double MIN_DISTANCE = 1;

        private DelayHelper delayHelper = new DelayHelper();

        public MotorInfoMap<double> CurrentPosInfo { get; private set; }
      
        public MotorInfoMap<MotorStatus> CurrentStatusInfo { get; private set; }

        public MotorInfoMap<double> CurrentSpeedInfo { get; private set; }

        public MotorInfoMap<bool> CurrentAlarmInfo { get; private set; }

        public DummyMotorController()
        {
            this.CurrentPosInfo = new MotorInfoMap<double>();
            this.CurrentStatusInfo = new MotorInfoMap<MotorStatus>();
            this.CurrentSpeedInfo = new MotorInfoMap<double>();
            this.CurrentAlarmInfo = new MotorInfoMap<bool>();
        }
   
        public double GetZeroPos(AxisTypes axis)
        {
            throw new NotImplementedException();
        }

        public Task<MovementStatus> MoveAsync(List<LineSegment> segments, CancellationToken token, Action<MotorInfoMap<double>, MotorInfoMap<double>> positionHandler)
        {
            return Task<MovementStatus>.Factory.StartNew(() => 
            {
                if (token.IsCancellationRequested)
                {
                    return MovementStatus.Cancel;
                }

                Action<int> microLineHandler = delay =>
                {
                    this.OnReportPositionInfo?.Invoke(this.CurrentPosInfo);
                    positionHandler?.Invoke(this.CurrentPosInfo, this.CurrentSpeedInfo);
                    //token.WaitHandle.WaitOne(INTERVAL_MS);
                    this.delayHelper.Delay(delay);
                };
             
                foreach (var m in segments)
                {
                    this.CurrentSpeedInfo[AxisTypes.AxisX] = m.XSpeed;
                    this.CurrentSpeedInfo[AxisTypes.AxisY] = m.YSpeed;
                    this.CurrentSpeedInfo[AxisTypes.AxisZ] = m.ZSpeed;
                    this.CurrentSpeedInfo[AxisTypes.AxisW] = m.WSpeed;

                    if (m.ZDistance == 0 || m.WDistance == 0) //X or Y Movement
                    {
                        double xPos = this.CurrentPosInfo[AxisTypes.AxisX] + m.XDistance;
                        double yPos = this.CurrentPosInfo[AxisTypes.AxisY] + m.YDistance;
 
                        double speed = Math.Sqrt(Math.Pow(m.XSpeed, 2) + Math.Pow(m.YSpeed, 2));
                        double distance = Math.Sqrt(Math.Pow(m.XDistance, 2) + Math.Pow(m.YDistance, 2));
                        double timeMS = distance / speed * 1000;
                        double timeCnt = timeMS / INTERVAL_MS;

                        if (distance < MIN_DISTANCE || double.IsInfinity(timeCnt) || timeCnt == 0)
                        {
                            this.CurrentPosInfo[AxisTypes.AxisX] = xPos;
                            this.CurrentPosInfo[AxisTypes.AxisY] = yPos;
                            microLineHandler.Invoke((int)timeMS);
                            if (token.IsCancellationRequested)
                            {
                                return MovementStatus.Cancel;
                            }
                        }
                        else
                        {                          
                            double xStep = m.XDistance / timeCnt;
                            double yStep = m.YDistance / timeCnt;
                            for (int i = 0; i < timeCnt; i++)
                            {
                                if (!double.IsInfinity(xStep))
                                {
                                    this.CurrentPosInfo[AxisTypes.AxisX] += xStep;
                                }
                                if (!double.IsInfinity(yStep))
                                {
                                    this.CurrentPosInfo[AxisTypes.AxisY] += yStep;
                                }
                                this.OnReportPositionInfo?.Invoke(this.CurrentPosInfo);
                                positionHandler?.Invoke(this.CurrentPosInfo, this.CurrentSpeedInfo);
                                //Thread.Sleep(INTERVAL_MS);
                                this.delayHelper.Delay(INTERVAL_MS);
                                if (token.IsCancellationRequested)
                                {
                                    return MovementStatus.Cancel;
                                }
                                //if (token.WaitHandle.WaitOne(INTERVAL_MS))
                                //{
                                //    return MovementStatus.Cancel;
                                //}
                            }
                                                    
                            this.CurrentPosInfo[AxisTypes.AxisX] = xPos;
                            this.CurrentPosInfo[AxisTypes.AxisY] = yPos;                                  
                        }                        
                    }
                    else  //Z MovEment
                    {
                        double zPos = this.CurrentPosInfo[AxisTypes.AxisZ] + m.ZDistance;                

                        double speed = m.ZSpeed;
                        double distance = Math.Abs(m.ZDistance);
                        double timeMS = distance / speed * 1000;
                        double timeCnt = timeMS / INTERVAL_MS;

                        if (distance < MIN_DISTANCE || double.IsInfinity(timeCnt) || timeCnt == 0)
                        {
                            this.CurrentPosInfo[AxisTypes.AxisZ] = zPos;
                            microLineHandler.Invoke((int)timeMS);
                            if (token.IsCancellationRequested)
                            {
                                return MovementStatus.Cancel;
                            }
                        }
                        else
                        {
                            double zStep = m.ZDistance / timeCnt;
                            for (int i = 0; i < timeCnt; i++)
                            {
                                if (!double.IsInfinity(zStep))
                                {
                                    this.CurrentPosInfo[AxisTypes.AxisZ] += zStep;
                                }
                                this.OnReportPositionInfo?.Invoke(this.CurrentPosInfo);
                                positionHandler?.Invoke(this.CurrentPosInfo, this.CurrentSpeedInfo);
                                //Thread.Sleep(INTERVAL_MS);
                                this.delayHelper.Delay(INTERVAL_MS);
                                if (token.IsCancellationRequested)
                                {
                                    return MovementStatus.Cancel;
                                }
                                //if (token.WaitHandle.WaitOne(1))
                                //{
                                //    return MovementStatus.Cancel;
                                //}
                            }
                        }                

                        this.CurrentPosInfo[AxisTypes.AxisZ] = zPos;                      
                    }

                    this.OnReportPositionInfo?.Invoke(this.CurrentPosInfo);
                }

                return MovementStatus.Done;
            });                     
        }

        public void Reset(Dictionary<AxisTypes, ResetParameter> resetPara)
        {
           
        }

        public void SetZeroPos(AxisTypes axis, double pos)
        {
            
        }

        public void WriteMovingFlag(bool isMoving)
        {
           
        }

        public void Stop(bool isBlocking = false)
        {
            
        }

        public event Action<MotorInfoMap<double>> OnReportPositionInfo;
        public event Action<MotorInfoMap<MotorStatus>> OnReportStatusInfo;
        public event Action<MotorInfoMap<double>> OnReportSpeedInfo;
        public event Action<MotorInfoMap<bool>> OnReportAlarmInfo;

        public void SimulateMovement(Dictionary<AxisTypes, double> posInfos)
        {
            foreach (var m in posInfos)
            {
                this.CurrentPosInfo[m.Key] = m.Value;
                this.OnReportPositionInfo?.Invoke(this.CurrentPosInfo);                
            }
        }

        public int GetReversedFactor(AxisTypes axis)
        {
            throw new NotImplementedException();
        }

        public double GetRatio(AxisTypes axis)
        {
            return 501.0;
        }
    }
}
