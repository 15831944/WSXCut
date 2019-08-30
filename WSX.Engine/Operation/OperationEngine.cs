using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using WSX.Engine.Models;
using WSX.Engine.Operation.Batch;
using WSX.Engine.Utilities;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation
{
    public class OperationEngine
    {
        private static OperationEngine instance;
        private static object SyncRoot = new object();
        private CancellationTokenSource cancellation = new CancellationTokenSource();
        private DataProvider provider;
        private EngineStatus status = EngineStatus.Idle;
        private Task machiningTask;
        //private bool disposing = false;
        public int MachiningCnt { get; set; } = 0;

        public event Action<EngineStatus> OnStatusChanged;
        public event Action<PointF> OnMarkPointChanged;
        public event Action<string, PointF> OnMarkPathAdd;

        public event Action<bool> OnLaserEnabled;
        public event Action<bool> OnBlowingEnabled;
        public event Action<bool> OnFollowEnabled;
        public event Action<LaserParameter> OnLaserParaChanged;
        public event Action<double> OnTargetFollowHeightChanged;
        public event Action<double> OnActualFollowHeightChanged;

        public event Action<string> OnLog;
        public event Action<string> OnErrorLog;
        public event Action OnFinishOnce;

        //public event Action<MotorInfoMap<double>, MotorInfoMap<double>> OnMotorInfoReport;
        public event Action<double> OnProgressChanged;
        public event Action<MotorInfoMap<double>> OnMotorPosReport;
        public event Action<MotorInfoMap<double>> OnMotorSpeedReport;

        private OperationEngine()
        {

        }

        //public bool Intialized { get; private set; } = false;

        public bool Done { get; private set; } = false;

        public EngineStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if (this.status != value)
                {
                    this.OnStatusChanged?.Invoke(value);
                }
                this.status = value;
            }
        }

        public void Intialize(DataProvider data)
        {
            this.provider = data;
            this.MachiningCnt = 0;
        }

        public static OperationEngine Instance
        {
            get
            {
                return instance ?? (instance = new OperationEngine());
            }
        }

        public Task MakeMachiningAsync()
        {
            return this.machiningTask = Task.Factory.StartNew(() =>
            {
                try
                {
                    var para = SystemContext.MachineControlPara;
                    int total = para.MachiningCnt;
                    int delayMS = (int)para.MachiningIntervalSeconds * 1000;
                    this.cancellation = new CancellationTokenSource();
                    this.Done = false;
                    //this.disposing = true;
                    this.Status = EngineStatus.Running;
                    SystemContext.Hardware.Motor.WriteMovingFlag(true);

                    for (int i = this.MachiningCnt; i < total; i++)
                    {
                        IBatch batch = null;
                        if (para.OperationType == OperationTypes.Simulation)
                        {
                            batch = new SimulationBatch();
                        }
                        else
                        {
                            batch = new MachiningBatch();
                        }
                        batch.MakeMachining(this.provider, this.cancellation.Token);
                        if (para.OperationType != OperationTypes.Step)
                        {
                            this.MachiningCnt++;
                            /*********** Update circle para ************/
                            this.provider.CurrentLen = 0;
                            total = para.MachiningCnt;
                            delayMS = (int)para.MachiningIntervalSeconds * 1000;
                            /*******************************************/
                            if (total != 1)
                            {
                                this.Log($"循环加工第{this.MachiningCnt}次完成，计划{total}次，延时等待{para.MachiningIntervalSeconds}s……");
                            }
                            this.OnFinishOnce?.Invoke();
                            this.cancellation.Token.WaitHandle.WaitOne(delayMS);
                            this.cancellation.Token.ThrowIfCancellationRequested();
                        }
                    }

                    this.MachiningCnt = 0;
                    this.Done = true;
                }
                catch (OperationCanceledException)
                {
                    //if (this.disposing)
                    //{
                    //    this.Log("操作被取消");
                    //}
                    //else
                    //{
                    //    this.Log("暂停加工过程");
                    //}
                }
                catch (Exception ex)
                {
                    this.Log($"加工过程中发现异常：{ex.Message}", true);
                }
                finally
                {
                    //if (this.disposing)
                    //{
                    //    this.MoveAfterMachining();
                    //}
                    if (SystemContext.Hardware?.IsLaserOn == true)
                    {
                        SystemContext.Hardware?.LaserOff();
                        this.NotifyLaserEnabled(false);
                    }
                    if (SystemContext.Hardware?.IsBlowingOn == true)
                    {
                        SystemContext.Hardware?.BlowingOff();
                        this.NotifyBlowingEnabled(false);
                    }
                    if (SystemContext.Hardware?.IsFollowOn == true)
                    {
                        SystemContext.Hardware?.FollowOff();
                        this.NotifyFollowEnabled(false);
                    }
                    this.Status = EngineStatus.Idle;
                    SystemContext.Hardware.Motor.WriteMovingFlag(false);
                }
            });
        }

        public Task MakeManualMovementAsync(ManualParameter para)
        {
            return this.machiningTask = Task.Factory.StartNew(() =>
            {
                this.cancellation = new CancellationTokenSource();
                this.Status = EngineStatus.Running;
                var broker = new PointMoveBroker(para);
                broker.Execute(this.cancellation.Token);
                this.Status = EngineStatus.Idle;
            });   
        }

        public void Cancel()
        {
            if (this.Status == EngineStatus.Idle)
            {
                return;
            }

            this.cancellation.Cancel();
            this.machiningTask.Wait();
            //this.cancellation.Dispose();
            //this.cancellation = null;
            //this.machiningTask = null;
        }

        public void NotifyMarkPointChanged(PointF point)
        {
            this.OnMarkPointChanged?.Invoke(point);
        }

        public void NotityMarkPathAdd(string id, PointF point)
        {
            this.OnMarkPathAdd?.Invoke(id, point);
        }

        public void NotifyLaserEnabled(bool enabled)
        {
            this.OnLaserEnabled?.Invoke(enabled);
        }

        public void NotifyBlowingEnabled(bool enabled)
        {
            this.OnBlowingEnabled?.Invoke(enabled);
        }

        public void NotifyFollowEnabled(bool enabled)
        {
            this.OnFollowEnabled?.Invoke(enabled);
        }

        public void Log(string msg, bool error = false)
        {
            if (error)
            {
                this.OnErrorLog?.Invoke(msg);
            }
            else
            {
                this.OnLog?.Invoke(msg);
            }
        }

        public void ReportMotorPos(MotorInfoMap<double> posInfo)
        {
            this.OnMotorPosReport?.Invoke(posInfo);
        }

        public void ReportMotorSpeed(MotorInfoMap<double> speedInfo)
        {
            this.OnMotorSpeedReport?.Invoke(speedInfo);
        }

        public void ReportProgress(double progress)
        {
            this.OnProgressChanged?.Invoke(progress);
        }

        public void NotifyLaserParaChanged(LaserParameter para)
        {
            this.OnLaserParaChanged?.Invoke(para);
        }

        public void NotifyTargetFollowHeightChanged(double height)
        {
            this.OnTargetFollowHeightChanged?.Invoke(height);
        }

        public void NotifyActualFollowHeightChanged(double height)
        {
            this.OnActualFollowHeightChanged?.Invoke(height);
        }
    }
}
