using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WSX.CommomModel.Utilities;
using WSX.Hardware.Common;
using WSX.Hardware.Models;

namespace WSX.Hardware.Motor
{
    public class MotorController : IMotorController
    {
        private readonly TcpClientStream tcpClient;
        private readonly Dictionary<AxisTypes, MotorParameter> motorParaMap;
        private const int PACKAGE_SIZE = 90;
        private const int READ_REG_INTERVAL_MS = 2; //50 -> 1
        private bool reportEnabled = true;
        private MotorInfoManager motorInfo = MotorInfoManager.Instance;

        public event Action<MotorInfoMap<double>> OnReportPositionInfo;
        public event Action<MotorInfoMap<MotorStatus>> OnReportStatusInfo;
        public event Action<MotorInfoMap<double>> OnReportSpeedInfo;
        public event Action<MotorInfoMap<bool>> OnReportAlarmInfo;

        public MotorController(TcpClientStream tcpClient, Dictionary<AxisTypes, MotorParameter> motorPara)
        {
            this.tcpClient = tcpClient;

            this.motorParaMap = new Dictionary<AxisTypes, MotorParameter>();
            var axises = EnumHelper.GetAllEnumMembers<AxisTypes>();
            foreach (var m in axises)
            {
                this.motorParaMap[m] = motorPara.Keys.Contains(m) ? motorPara[m] : new MotorParameter();
            }

            this.LoadMotorInfo();
            this.EnableAllMotor();
        }

        #region IMotorController Implementation
        public void WriteMovingFlag(bool isMoving)
        {
            var info = isMoving ?  null : this.GetOriginalMotorInfo();
            foreach (var m in this.motorParaMap.Keys)
            {
                this.motorInfo.SetMovingStatus(m, isMoving);
                if (info != null)
                {
                    this.motorInfo.SetCurrentPos(m, info[m].Counter);
                    this.motorInfo.SetEncoder(m, info[m].Encoder);
                }
            }
        }
     
        public MotorInfoMap<double> CurrentPosInfo
        {
            get
            {
                return this.GetMotorInfo().PosInfo;
            }
        }

        public MotorInfoMap<MotorStatus> CurrentStatusInfo
        {
            get
            {
                return this.GetMotorInfo().StatusInfo;
            }
        }

        public MotorInfoMap<double> CurrentSpeedInfo
        {
            get
            {
                return this.GetMotorInfo().SpeedInfo;
            }
        }

        public MotorInfoMap<bool> CurrentAlarmInfo
        {
            get
            {
                return this.GetAlarmInfo();
            }
        }

        public Task<MovementStatus> MoveAsync(List<LineSegment> segments, CancellationToken token, Action<MotorInfoMap<double>, MotorInfoMap<double>> motorInfoHandler)
        {
            return Task<MovementStatus>.Factory.StartNew(() =>
            {
                if (token.IsCancellationRequested)
                {
                    return MovementStatus.Cancel;
                }

                MovementStatus status = MovementStatus.Done;
                segments = MotorUtil.SplitLines(this.motorParaMap, segments);
                var items = MotorUtil.Split(segments.Count, PACKAGE_SIZE);
                int index = 0;

                Action reportHandler = () =>
                {
                    var info = this.GetMotorInfo();
                  
                    if (this.reportEnabled)
                    {
                        this.OnReportPositionInfo?.Invoke(info.PosInfo);
                        this.OnReportStatusInfo?.Invoke(info.StatusInfo);
                        this.OnReportSpeedInfo?.Invoke(info.SpeedInfo);
                        this.OnReportAlarmInfo?.Invoke(this.CurrentAlarmInfo);
                    }

                    motorInfoHandler?.Invoke(info.PosInfo, info.SpeedInfo);
                };

                //while (!token.WaitHandle.WaitOne(READ_REG_INTERVAL_MS))
                while (true)
                {
                    if (this.IsCacheEmpty)
                    {
                        if (index < items.Count)
                        {
                            int startIndex = index * PACKAGE_SIZE;
                            int len = items[index];
                            this.WriteData(segments.GetRange(startIndex, len));
                            index++;
                        }
                        else
                        {
                            if (this.IsMovementDone)
                            {
                                break;
                            }
                        }
                    }

                    if (!token.IsCancellationRequested)
                    {
                        reportHandler.Invoke();
                    }
                    else
                    {
                        break;
                    }

                    Thread.Sleep(READ_REG_INTERVAL_MS);
                }

                if (token.IsCancellationRequested)
                {
                    this.Stop(true);
                    reportHandler.Invoke();
                    status = MovementStatus.Cancel;
                    //LogUtil.Instance.Info(string.Format("Stop time = {0}", DateTime.Now.ToString("HH:mm:ss.fff")));
                }

                return status;
            });
        }

        public void Reset(Dictionary<AxisTypes, ResetParameter> resetPara)
        {
            //var cts = new CancellationTokenSource();        
            //var factorMap = new Dictionary<AxisTypes, double>();

            //var segment1 = new LineSegment();
            //var segment2 = new LineSegment();
            //var segment3 = new LineSegment();
            //var segment4 = new LineSegment();
 
            //foreach (var m in resetPara.Keys)
            //{
            //    double factor = -1 * (int)resetPara[m].Direction * (this.motorParaMap[m].IsReversed ? -1 : 1);
            //    factorMap.Add(m, factor);
            //}

            //#region Calculate distance
            //if (resetPara.Keys.Contains(AxisTypes.AxisX))
            //{
            //    segment1.XSpeed = resetPara[AxisTypes.AxisX].RoughSpeed;
            //    segment1.XDistance = -1000 * factorMap[AxisTypes.AxisX];

            //    segment2.XSpeed = resetPara[AxisTypes.AxisX].RoughSpeed;
            //    segment2.XDistance = resetPara[AxisTypes.AxisX].BackOffset * factorMap[AxisTypes.AxisX];

            //    segment3.XSpeed = resetPara[AxisTypes.AxisX].PreciseSpeed;
            //    segment3.XDistance = -1000 * factorMap[AxisTypes.AxisX];

            //    segment4.XSpeed = resetPara[AxisTypes.AxisX].RoughSpeed;
            //    segment4.XDistance = resetPara[AxisTypes.AxisX].ZeroOffset * factorMap[AxisTypes.AxisX];
            //}

            //if (resetPara.Keys.Contains(AxisTypes.AxisY))
            //{
            //    segment1.YSpeed = resetPara[AxisTypes.AxisY].RoughSpeed;
            //    segment1.YDistance = -1000 * factorMap[AxisTypes.AxisY];

            //    segment2.YSpeed = resetPara[AxisTypes.AxisY].RoughSpeed;
            //    segment2.YDistance = resetPara[AxisTypes.AxisY].BackOffset * factorMap[AxisTypes.AxisY];

            //    segment3.YSpeed = resetPara[AxisTypes.AxisY].PreciseSpeed;
            //    segment3.YDistance = -1000 * factorMap[AxisTypes.AxisY];

            //    segment4.YSpeed = resetPara[AxisTypes.AxisY].RoughSpeed;
            //    segment4.YDistance = resetPara[AxisTypes.AxisY].ZeroOffset * factorMap[AxisTypes.AxisY];
            //}

            //if (resetPara.Keys.Contains(AxisTypes.AxisZ))
            //{
            //    segment1.ZSpeed = resetPara[AxisTypes.AxisZ].RoughSpeed;
            //    segment1.ZDistance = -1000 * factorMap[AxisTypes.AxisZ];

            //    segment2.ZSpeed = resetPara[AxisTypes.AxisZ].RoughSpeed;
            //    segment2.ZDistance = resetPara[AxisTypes.AxisZ].BackOffset * factorMap[AxisTypes.AxisZ];

            //    segment3.ZSpeed = resetPara[AxisTypes.AxisZ].PreciseSpeed;
            //    segment3.ZDistance = -1000 * factorMap[AxisTypes.AxisZ];

            //    segment4.ZSpeed = resetPara[AxisTypes.AxisZ].RoughSpeed;
            //    segment4.ZDistance = resetPara[AxisTypes.AxisZ].ZeroOffset * factorMap[AxisTypes.AxisZ];
            //}
            //#endregion

            //List<LineSegment> segments = new List<LineSegment> { segment1, segment2, segment3, segment4 };
            //this.reportEnabled = false;
            //this.WriteMovingFlag(true);
            //this.MoveAsync(segments, cts.Token, null).Wait();
            //foreach (var m in resetPara)
            //{
            //    double currentPos = m.Value.ZeroOffset;
            //    if (m.Value.Direction == ResetDirection.Positive)
            //    {
            //        currentPos = m.Value.MaxDistance - currentPos;
            //    }

            //    Int32 pos = (Int32)(currentPos * this.motorParaMap[m.Key].Ratio);
            //    this.pci.WriteIOData(this.posRegMap[m.Key], (uint)pos);

            //    this.motorInfo.SetCurrentPos(m.Key, pos);
            //    this.motorInfo.SetZeroPos(m.Key, pos);
            //}
            //this.WriteMovingFlag(false);
            //this.reportEnabled = true;
        }
       
        public void Stop(bool isBlocking = false)
        {
            this.MakeMovement(false);
            while (isBlocking && (!this.IsMovementDone))
            {

            }
        }

        public int GetReversedFactor(AxisTypes axis)
        {
            bool isReversed = this.motorParaMap[axis].IsReversed;
            return isReversed ? -1 : 1;
        }

        public double GetRatio(AxisTypes axis)
        {
            return this.motorParaMap[axis].Ratio;
        }
        #endregion

        #region Utility
        private void LoadMotorInfo()
        {
            if (!this.motorInfo.IsValid)
            {
                return;
            }

            var info = new MotorInfoMap<MotorInfoUnit>();
            var axises = EnumHelper.GetAllEnumMembers<AxisTypes>();
            foreach (var m in axises)
            {
                info[m].Counter = this.motorInfo.GetCurrentPos(m);
                info[m].Encoder = this.motorInfo.GetEncoder(m);
            }

            var bytes = Requestor.CreatePosInfoCmd(info);
            try
            {
                bytes = this.tcpClient.Query(bytes, x => Protocol.Verify(x), out bool valid);
                if (!valid)
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new Exception("Error occurred when loading motor information!");
            }       
        }

        private MotorInfoMap<MotorInfoUnit> GetOriginalMotorInfo()
        {
            try
            {
                var bytes = Requestor.CreateGetStatusCmd();
                bytes = this.tcpClient.Query(bytes, x => Protocol.Verify(x), out bool valid);
                if (valid)
                {
                    return Responder.GetMotorStatus(bytes);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new Exception("Error occurred when getting motor information!");
            }
        }

        private MotorInformation GetMotorInfo()
        {
            try
            {
                return MotorUtil.GetMotorInformation(this.motorParaMap, this.GetOriginalMotorInfo());
            }
            catch
            {
                throw new Exception("Error occurred when getting motor information!");
            }
        }

        private MotorInfoMap<bool> GetAlarmInfo()
        {
            try
            {
                var bytes = Requestor.CreateGetMotorAlarmCmd();
                bytes = this.tcpClient.Query(bytes, x => Protocol.Verify(x), out bool valid);
                if (valid)
                {
                    var data = Responder.GetMotorAlarmData(bytes);
                    return MotorUtil.ParseAlarmInfo(data);
                }
                else
                {
                    throw new Exception();
                }              
            }
            catch
            {
                throw new Exception("Error occurred when getting alarm information!");
            }
        }

        private void ClearAllAlarm()
        {        
            try
            {
                var bytes = Requestor.CreateSetMotorAlarmCmd(0xFFFFFFFF);
                bytes = this.tcpClient.Query(bytes, x => Protocol.Verify(x), out bool valid);
                if (!valid)
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new Exception("Error occurred when cleanup alarm information!");
            }
        }

        private void EnableAllMotor()
        {
            try
            {
                var bytes = Requestor.CreateSetMotorEnableCmd(0xFFFFFFFF);        
                bytes = this.tcpClient.Query(bytes, x => Protocol.Verify(x), out bool valid);
                if (!valid)
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new Exception("Error occurred when making motor enabled!");
            }
        }

        private bool IsCacheEmpty
        {
            get
            {       
                try
                {
                    var bytes = Requestor.CreateGetCacheStatusCmd();
                    bytes = this.tcpClient.Query(bytes, x => Protocol.Verify(x), out bool valid);
                    if (valid)
                    {
                        return Responder.GetCacheStatus(bytes);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    throw new Exception("Error occurred when getting cache status!");
                }
            }
        }

        private bool IsMovementDone
        {
            get
            {
                Func<bool> isMoving = () =>
                {
                    var statusInfo = this.CurrentStatusInfo;
                    bool flag = false;
                    foreach (var m in this.motorParaMap.Keys)
                    {
                        if (statusInfo[m].IsMoving)
                        {
                            flag = true;
                            break;
                        }
                    }
                    return flag;
                };

                bool isDone = false;

                if (!isMoving.Invoke())
                {
                    isDone = true;
                    for (int i = 0; i < 5; i++)
                    {
                        if (isMoving.Invoke())
                        {
                            isDone = false;
                            break;
                        }
                    }
                }

                return isDone;
            }
        }

        private void MakeMovement(bool running)
        {           
            try
            {
                var bytes = Requestor.CreateActionCmd(running);
                bytes = this.tcpClient.Query(bytes, x => Protocol.Verify(x), out bool valid);
                if (!valid)
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new Exception("Error occurred when sending movement command!");
            }
        }

        private void WriteData(List<LineSegment> segments)
        {         
            try
            {
                //var before = DateTime.Now;
                var bytes = Requestor.CreateMovementDataCmd(segments);
                bytes = this.tcpClient.Query(bytes, x => Protocol.Verify(x), out bool valid);
                if (!valid)
                {
                    throw new Exception();
                }
                this.MakeMovement(true);
                //var period = DateTime.Now - before;
                //Console.WriteLine("Time = {0}ms", period.TotalMilliseconds);
            }
            catch
            {
                throw new Exception("Error occurred when sending movement command!");
            }
        }
        #endregion
    }
}
