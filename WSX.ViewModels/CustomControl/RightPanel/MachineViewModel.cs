using DevExpress.Mvvm;
using DevExpress.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.Utilities;
using WSX.Engine;
using WSX.Engine.Models;
using WSX.Engine.Operation;
using WSX.Engine.Operation.Process;
using WSX.Engine.Utilities;
using WSX.GlobalData.Model;
using WSX.ViewModels.Common;
using WSX.ViewModels.CustomControl.Menu;
using WSX.ViewModels.Forms;
using WSX.ViewModels.Model;

namespace WSX.ViewModels.RightPanel
{
    public class MachineViewModel: ViewModelExtensions
    {
        private ManualResetEvent waitEvent = new ManualResetEvent(false);
        private DataProvider provider;
        private int drawObjectsCnt;
        private string lastOperation = "Idle";
        private double currentLen = 0;
        private string snapShot;

        public event Action<string> StatusChanged;
        public event Action DisableLocateStatus;

        #region Properties
        private FrmCircleViewModel CircleConfig { get; set; } = new FrmCircleViewModel();
        //[BindableProperty(OnPropertyChangedMethodName = "NotifyReturnPointChanged")]
        public virtual bool IsReturnAfterMachine { get; set; }
        //[BindableProperty(OnPropertyChangedMethodName = "NotifyReturnPointChanged")]
        public virtual int ReturnPointIndex { get; set; }

        public virtual bool IsReturnZeroWhenStop
        {
            get
            {
                return SystemContext.MachineControlPara.IsReturnZeroWhenStop;
            }
            set
            {
                SystemContext.MachineControlPara.IsReturnZeroWhenStop = value;
            }
        }

        public virtual bool IsOnlyMachineSelected
        {
            get
            {
                return SystemContext.MachineControlPara.IsOnlyMachineSelected;
            }
            set
            {
                SystemContext.MachineControlPara.IsOnlyMachineSelected = value;
            }

        }

        //[BindableProperty(OnPropertyChangedMethodName = "NotifySoftLimitChanged")]
        public virtual bool SoftwareLimitEnalbed
        {
            get
            {
                return SystemContext.MachineControlPara.SoftwareLimitEnabled;
            }
            set
            {
                SystemContext.MachineControlPara.SoftwareLimitEnabled = value;
            }
        }

        public virtual bool EdgeDetectoinEnabled
        {
            get
            {
                return SystemContext.MachineControlPara.EdgeDetectionEnabled;
            }
            set
            {
                SystemContext.MachineControlPara.EdgeDetectionEnabled = value;
            }
        }

        public virtual double Step
        {
            get
            {
                return SystemContext.MachineControlPara.Step;
            }
            set
            {
                SystemContext.MachineControlPara.Step = value;
            }
        }

        public virtual double StepSpeed
        {
            get
            {
                return SystemContext.MachineControlPara.StepSpeed;
            }
            set
            {
                SystemContext.MachineControlPara.StepSpeed = value;
            }
        }
        #endregion

        public MachineViewModel()
        {
            MVVMContext.RegisterXtraMessageBoxService();
            Messenger.Default.Register<object>(this, "ReceiveDataProvider", this.OnDataProviderReceive);
            Messenger.Default.Register<object>(this, "ReceiveDrawObjectsCnt", this.OnDrawObjectsCntReceive);
            Messenger.Default.Register<object>(this, "DataProviderChanged", this.OnDataProviderChanged);
            Messenger.Default.Register<bool>(this, "OnManualMovement", this.OnManualMovement);
            Messenger.Default.Register<object>(this, "OnOperationTrigger", this.OnOperationTrigger);
            Messenger.Default.Register<object>(this, "AutoSuspend", this.OnAutoSuspend);
            OperationEngine.Instance.OnFinishOnce += () => 
            {
                Messenger.Default.Send<object>(null, "ClearMark");
                Messenger.Default.Send<object>(null, "OnMachineOnce");
            };
            OperationEngine.Instance.OnLog += Engine_OnLog;
        }

        #region Engine callback
        private void Engine_OnLog(string msg)
        {
            this.AddSysLog(msg, Color.FromArgb(0, 128, 0));
        }
        #endregion

        #region Commands
        public void RunOperation(string item)
        {
            if (!this.Initialize(item))
            {
                return;
            }       
            this.OutputLog(item);
            this.UpdateMachiningPara(item);
            this.MachineCore(item); 
            this.NotifyStatusChanged(item);
            this.lastOperation = item;
        }

        public void ConfigCirclePara()
        {
            this.CircleConfig.ClearMachineCount = false;
            bool idle = this.lastOperation == "Idle" || this.lastOperation == "Stop";
            this.CircleConfig.Enabled = idle;
            this.CircleConfig.MachineImmediately = idle; 
            this.ExecuteCmd("UpdateCirclePara", this.CircleConfig);
            var result = this.CircleConfig.Result;
            var para = SystemContext.MachineControlPara;
            if (result == DialogResult.OK)
            {
                para.MachiningCnt = this.CircleConfig.Count;
                para.MachiningIntervalSeconds = this.CircleConfig.Interval;
                if (this.CircleConfig.ClearMachineCount)
                {
                    OperationEngine.Instance.MachiningCnt = 0;
                    //TODO: Notify record control
                }
                if (this.CircleConfig.MachineImmediately)
                {
                    string item = this.CircleConfig.Normal ? "Start" : "Empty";
                    this.RunOperation(item);
                }
            }
            else if(result == DialogResult.Abort)
            {
                para.MachiningCnt = 1;
                para.MachiningIntervalSeconds = 0;
            }
        }

        public void LoacateBreakPoint()
        {
            var point = this.provider.Locate(ref this.currentLen);
            this.provider.CurrentLen = this.currentLen;
            if (point == null)
            {
                return;
            }
            
            var para = new ManualParameter
            {
                Speed = GlobalModel.Params.LayerConfig.EmptyMoveSpeed,
                Acceleration = GlobalModel.Params.LayerConfig.EmptyMoveAcceleratedSpeed,
                TargetPoint = point.Value
            };
            this.NotifyStatusChanged("BreakPointLocate");
            this.lastOperation = "BreakPointLocate";
            Messenger.Default.Send<object>(OperationStatus.Running, "OperStatusChanged");
            this.AddSysLog("定位", Color.FromArgb(128, 0, 0));
            this.AddSysLog("停止-->定位", Color.Black);
            OperationEngine.Instance.MakeManualMovementAsync(para).ContinueWith(x =>
            {
                Messenger.Default.Send<object>(OperationStatus.Idle, "OperStatusChanged");
                this.AddSysLog("定位-->暂停", Color.Black);
                this.NotifyStatusChanged("Pause");
                this.lastOperation = "Pause";
                this.provider.CurrentLen = this.currentLen;
            });
        }

        public void MoveToZero()
        {         
            this.NotifyStatusChanged("Zero");
            this.lastOperation = "Pause";
            Messenger.Default.Send<object>(OperationStatus.Running, "OperStatusChanged");
            this.AddSysLog("回零点", Color.FromArgb(128, 0, 0));
            this.AddSysLog("停止-->回零点", Color.Black);
            this.MoveToZeroAsync().ContinueWith(x =>
            {
                this.NotifyStatusChanged("Idle");
                this.lastOperation = "Idle";
                Messenger.Default.Send<object>(OperationStatus.Idle, "OperStatusChanged");
                this.AddSysLog("回零点-->停止", Color.Black);
                Messenger.Default.Send<object>(null, "ClearMark");
                Messenger.Default.Send<object>(true, "CanvasStatusChanged");
                Messenger.Default.Send<object>(false, "MenuInfoDisChanged");
            });
        }

        public void StopOperation()
        {
            if (this.lastOperation == "Idle" || this.lastOperation == "Stop")
            {
                //Cancel manual movement
                OperationEngine.Instance.Cancel();  
                return;
            }

            this.AddSysLog("用户停止", Color.Red);
            this.lastOperation = "Stopping";
            this.NotifyStatusChanged("Stopping");
            if (OperationEngine.Instance.Status == EngineStatus.Running)
            {
                OperationEngine.Instance.Cancel();
            }
            else
            {
                //Take an empty movement if paused
                Task.Run(() => 
                {
                    Messenger.Default.Send<object>(OperationStatus.Running, "OperStatusChanged");
                    this.ReturnZeroWhenStop();
                    Messenger.Default.Send<object>(OperationStatus.Idle, "OperStatusChanged");
                });
            }
        }

        public void Pause()
        {
            this.lastOperation = "Pause";
            this.NotifyStatusChanged("Pause");
            this.AddSysLog("暂停", Color.FromArgb(255, 128, 0));
            OperationEngine.Instance.Cancel();                       
        }
        #endregion

        #region Message handler
        private void OnDataProviderReceive(object args)
        {
            this.provider = (DataProvider)args;
            this.waitEvent.Set();
        }

        private void OnDrawObjectsCntReceive(object args)
        {
            this.drawObjectsCnt = (int)args;
            this.waitEvent.Set();
        }

        private void OnDataProviderChanged(object args)
        {
           System.Diagnostics.Debug.WriteLine("********** DataProvider Changed **********");
            //DispatcherService.BeginInvoke(() => this.DisableLocateStatus?.Invoke());
            this.DisableLocateStatus?.Invoke();
            Messenger.Default.Send<object>(false, "CanvasMonitorChanged");
        }

        private void OnManualMovement(bool running)
        {
            if (running)
            {
                this.snapShot = this.lastOperation;
                //Only update buttons's enabled status
                this.NotifyStatusChanged("Zero", false);
            }
            else
            {
                this.NotifyStatusChanged(this.snapShot);
            }
        }

        private void OnOperationTrigger(object arg)
        {
            string operation = (string)arg;
            if (operation == "Start")
            {
                this.RunOperation("Start");
            }
            else if (operation == "Pause")
            {
                this.Pause();
            }
            else
            {
                this.StopOperation();
            }
        }

        private void OnAutoSuspend(object arg)
        {
            this.Pause();
        }
        #endregion

        #region Machine process
        private void MachineCore(string status)
        {
            var validConditions = new List<string> { "Start", "Fast", "Outline", "Empty", "BreakPointStart" };
            bool flag = validConditions.Contains(status);
            if (flag)
            {
                Messenger.Default.Send<object>(TimerStatus.Start, "TimerStatusChanged");
            }
            if (status == "Start" || status == "Empty")
            {
                Messenger.Default.Send<object>("Start", "OnCountdownChanged");
            }
            Messenger.Default.Send<object>(OperationStatus.Running, "OperStatusChanged");

            var engine = OperationEngine.Instance;
            engine.MakeMachiningAsync().ContinueWith(x =>
            {
                bool done = engine.Done;
                bool isSuspend = false;
                if (done)
                {
                    // Machining finished without cancellation
                    if (this.lastOperation == "Forward" || this.lastOperation == "Backward")
                    {
                        if (this.lastOperation == "Forward")
                        {
                            this.AddSysLog("前进-->暂停", Color.Black);
                        }
                        else
                        {
                            this.AddSysLog("后退-->暂停", Color.Black);
                        }
                        this.lastOperation = "Pause";
                        this.NotifyStatusChanged("Pause");
                        isSuspend = true;
                    }
                    else
                    {
                        this.ReturnMark();
                        Messenger.Default.Send<object>(null, "LogMachineTime");
                    }
                }
                else
                { 
                    // Back to a certain distance after pause
                    double backDis = GlobalModel.Params.LayerConfig.PauseBackspaceDistance;
                    this.provider.CurrentLen -= backDis;
                    if (this.provider.CurrentLen < 0)
                    {
                        this.provider.CurrentLen = 0;
                    }

                    // Cancel operation
                    if (this.lastOperation == "Stopping")
                    {
                        this.ReturnZeroWhenStop();
                    }
                    else
                    {
                        isSuspend = true;
                        this.AddSysLog("正在加工-->暂停", Color.Black);                      
                    }               
                }

                Messenger.Default.Send<object>(isSuspend ? OperationStatus.Suspend : OperationStatus.Idle, "OperStatusChanged");
                Messenger.Default.Send<object>("Stop", "OnCountdownChanged");
                if (flag)
                {
                    Messenger.Default.Send<object>(TimerStatus.Stop, "TimerStatusChanged");
                }             
            });
        }

        private bool Initialize(string item)
        {
            bool valid = true;
            if (item != "BreakPointStart" && (this.lastOperation == "Idle" || this.lastOperation == "Stop"))
            {
                Messenger.Default.Send<object>(false, "CanvasMonitorChanged");
                //this.UpdateDataProvider();
                this.UpdateDrawObjectsCnt();
               
                #region Step1: Check if empty
                //if (this.provider.IsEmpty)
                if(this.drawObjectsCnt == 0)
                {
                    MsgBoxService.ShowMessage("没有可加工的图形！", "消息", MessageButton.OK, MessageIcon.Information);
                    return false;
                }
                #endregion

                #region Step2: Check if need to sort, then move figures and get dataprovider again
                //bool needToSort = false;
                //if (this.provider.DrawObjectCount > 1)
                if (this.drawObjectsCnt > 1)
                {
                    //var res = this.MsgBoxService.ShowMessage("加工之前是否先进行排序?", "消息", MessageButton.YesNoCancel, MessageIcon.Question);
                    //if (res == MessageResult.Cancel)
                    //{
                    //    return false;
                    //}
                    //needToSort = res == MessageResult.Yes;
                }
                int index = SystemContext.CoordinatePara.RefZeroIndex;
                if (index == 0)   //Floating coordinate
                {
                    var p = SystemContext.Hardware.GetCurrentPosition();
                    SystemContext.CoordinatePara.RefZeroSeries[0] = p;
                }
                var zeroPoint = SystemContext.CoordinatePara.RefZeroSeries[index];
                //Messenger.Default.Send<object>(null, "OnMoveFigures");
                //Messenger.Default.Send<object>(null, "OnPreview");
                //Messenger.Default.Send<object>(zeroPoint, "RelativePosChanged");
                //this.UpdateDataProvider(needToSort);
                var para = new CanvasOperParameter
                {
                    OperType = CanvasOperTypes.Move | CanvasOperTypes.Preview,
                    Selected = SystemContext.MachineControlPara.IsOnlyMachineSelected
                };
                Messenger.Default.Send<object>(para, "OnOperation");
                Messenger.Default.Send<object>(zeroPoint, "RelativePosChanged");
                #endregion

                #region Step3: check if out of range
                if (SystemContext.MachineControlPara.SoftwareLimitEnabled)
                {
                    var rect1 = this.provider.GetOutlineRect();
                    var rect2 = SystemContext.SystemPara.GetMachiningRegion();
                    if (!rect2.Contains(rect1.Value))
                    {
                        MsgBoxService.ShowMessage("加工图形已经超出行程范围！", "消息", MessageButton.OK, MessageIcon.Error);
                        return false;
                    }
                }
                #endregion

                #region Step4: Initialize
                OperationEngine.Instance.Intialize(this.provider);
                Messenger.Default.Send<object>(null, "ClearMark");            
                Messenger.Default.Send<object>(false, "CanvasStatusChanged");
                //Messenger.Default.Send<object>(false, "CanvasMonitorChanged");
                if (item != "Simulate")
                {
                    Messenger.Default.Send<object>(true, "MenuInfoDisChanged");
                    Messenger.Default.Send<object>(TimerStatus.Restart, "TimerStatusChanged");
                }               
                #endregion

                #region Step5: Log
                string msg;
                if (index == 0)
                {
                    msg = $"设置浮动坐标系的零点为({zeroPoint.X.ToString("0.###")},{zeroPoint.Y.ToString("0.###")})mm";
                }
                else
                {
                    msg = $"选择程序坐标系{index},其零点位于({zeroPoint.X.ToString("0.###")},{zeroPoint.Y.ToString("0.###")})mm";
                }

                if (item == "Start")
                {
                    this.AddSysLog("开始", Color.FromArgb(128, 0, 0));
                    this.AddSysLog(msg, Color.FromArgb(0, 128, 128));
                    this.AddSysLog("开始加工", Color.FromArgb(0, 0, 255));
                    if (this.drawObjectsCnt < 5000)
                    {
                        var info = this.GetMachineInfo();
                        msg = $"切割总长：{info.CutLen.ToString("0.##")}mm, 空移总长：{info.EmptyMoveLen.ToString("0.##")}mm, 穿孔次数：{info.PiercingTimes}次";
                        this.AddSysLog(msg, Color.FromArgb(0, 128, 0));
                        //msg = $"切割时间(估计)：{info.CutTime.ToString("0.###")}s, 空移时间(估计)：{info.EmptyMoveTime.ToString("0.###")}s, 延时：{info.Delay.ToString("0.###")}s, 总耗时(估计)：{info.TotalTime.ToString("0.###")}s";
                        msg = $"切割时间(估计)：{DelayHelper.GetTimeString(info.CutTime)}, 空移时间(估计)：{DelayHelper.GetTimeString(info.EmptyMoveTime)}, 延时：{DelayHelper.GetTimeString(info.Delay)}, 总耗时(估计)：{DelayHelper.GetTimeString(info.TotalTime)}";
                        this.AddSysLog(msg, Color.FromArgb(0, 128, 0));
                    }
                    this.AddSysLog("停止-->正在加工", Color.Black);
                }
                if (item == "Outline")
                {
                    this.AddSysLog("检边", Color.FromArgb(128, 0, 0));
                    this.AddSysLog(msg, Color.FromArgb(0, 128, 128));
                    this.AddSysLog("停止-->检边", Color.Black);
                }
                if (item == "Simulate")
                {
                    this.AddSysLog("模拟", Color.FromArgb(128, 0, 0));
                    this.AddSysLog(msg, Color.FromArgb(0, 128, 128));
                    this.AddSysLog("停止-->模拟", Color.Black);
                }
                if (item == "Empty")
                {
                    this.AddSysLog("空走", Color.FromArgb(128, 0, 0));
                    this.AddSysLog(msg, Color.FromArgb(0, 128, 128));
                    this.AddSysLog("停止-->空走", Color.Black);
                }
                #endregion
            }            
            return valid;
        }

        private void OutputLog(string item)
        {
            if ((item == "Start" || item == "Empty" || item == "Fast") && this.lastOperation == "Pause")
            {
                this.AddSysLog("继续", Color.FromArgb(128, 0, 0));
                this.AddSysLog("暂停-->继续", Color.Black);
                this.AddSysLog("继续-->正在加工", Color.Black);
            }
            if (item == "Forward" && this.lastOperation == "Pause")
            {
                this.AddSysLog("前进", Color.FromArgb(128, 0, 0));
                this.AddSysLog("暂停-->前进", Color.Black);
            }
            if (item == "Backward" && this.lastOperation == "Pause")
            {
                this.AddSysLog("后退", Color.FromArgb(128, 0, 0));
                this.AddSysLog("暂停-->后退", Color.Black);
            }
            if (item == "BreakpointStart")
            {
                this.AddSysLog("继续", Color.FromArgb(128, 0, 0));
                this.AddSysLog("停止-->正在加工", Color.Black);
            }  
        }

        private void ReturnMark()
        {
            this.UpdateReturnPoint();
            var retPoint = SystemContext.MachineControlPara.ReturnPoint;
            if (retPoint != null)
            {
                var para = new ManualParameter
                {
                    Speed = GlobalModel.Params.LayerConfig.EmptyMoveSpeed,
                    Acceleration = GlobalModel.Params.LayerConfig.EmptyMoveAcceleratedSpeed,
                    TargetPoint = retPoint.Value
                };

                this.lastOperation = "Stopping";
                this.NotifyStatusChanged("Stopping");
                this.AddSysLog("正在加工-->回停靠", Color.Black);
                OperationEngine.Instance.MakeManualMovementAsync(para).Wait();
                this.AddSysLog("回停靠-->停止", Color.Black);
            }

            this.NotifyStatusChanged("Idle");
            this.lastOperation = "Idle";
            SystemContext.MachineControlPara.MachiningCnt = 1;
            SystemContext.MachineControlPara.MachiningIntervalSeconds = 0;
            Messenger.Default.Send<object>(true, "CanvasStatusChanged");
            Messenger.Default.Send<object>(false, "MenuInfoDisChanged");
            Messenger.Default.Send<object>(null, "ClearMark");
        }

        private void ReturnZeroWhenStop()
        {
            if (this.IsReturnZeroWhenStop)
            {
                this.MoveToZeroAsync().Wait();
            }

            this.lastOperation = "Stop";
            this.NotifyStatusChanged("Stop");
            this.currentLen = this.provider.CurrentLen;
            SystemContext.MachineControlPara.MachiningCnt = 1;
            SystemContext.MachineControlPara.MachiningIntervalSeconds = 0;
            Messenger.Default.Send<object>(true, "CanvasStatusChanged");
            Messenger.Default.Send<object>(false, "MenuInfoDisChanged");
            Messenger.Default.Send<object>(null, "ClearMark");
            Messenger.Default.Send<object>(true, "CanvasMonitorChanged");
        }

        private Task MoveToZeroAsync()
        {
            var zeroIndex = SystemContext.CoordinatePara.RefZeroIndex;
            var zeroPoint = SystemContext.CoordinatePara.RefZeroSeries[zeroIndex];
            var para = new ManualParameter
            {     
                Speed = GlobalModel.Params.LayerConfig.EmptyMoveSpeed,
                Acceleration = GlobalModel.Params.LayerConfig.EmptyMoveAcceleratedSpeed,
                TargetPoint = zeroPoint
            };
            return OperationEngine.Instance.MakeManualMovementAsync(para);
        }
        #endregion

        #region Utilities     
        private void UpdateDataProvider(bool needToSort = false)
        {
            bool selected = SystemContext.MachineControlPara.IsOnlyMachineSelected;
            Messenger.Default.Send<object>(Tuple.Create(needToSort, selected), "GetDataProvider");
            this.waitEvent.WaitOne();
            this.waitEvent.Reset();
        }

        private void UpdateDrawObjectsCnt(bool needToSort = false)
        {
            bool selected = SystemContext.MachineControlPara.IsOnlyMachineSelected;
            Messenger.Default.Send<object>(Tuple.Create(needToSort, selected), "GetDrawObjectsCnt");
            this.waitEvent.WaitOne();
            this.waitEvent.Reset();
        }

        private void NotifyStatusChanged(string status, bool notify = true)
        {
            //DispatcherService.BeginInvoke(() => this.StatusChanged?.Invoke(status));
            this.StatusChanged?.Invoke(status);
            if (notify)
            {
                Messenger.Default.Send<object>(status, "UpdateOperation");
            }
        }

        private void UpdateMachiningPara(string item)
        {
            var para = SystemContext.MachineControlPara;
            if (item == "Start")
            {
                para.OperationType = OperationTypes.Machining;
            }
            else if (item == "Fast")
            {
                para.OperationType = OperationTypes.FastMachinig;
            }
            else if (item == "Simulate")
            {
                para.OperationType = OperationTypes.Simulation;
            }
            else if (item == "Empty")
            {
                para.OperationType = OperationTypes.EmptyMove;
            }
            else if (item == "Outline")
            {
                para.OperationType = OperationTypes.Outline;
            }
            else if (item == "Forward" || item == "Backward")
            {
                para.OperationType = OperationTypes.Step;
                double factor = (item == "Forward") ? 1 : -1;
                para.Step = Math.Abs(this.Step) * factor;
                para.StepSpeed = this.StepSpeed;
            }

            //if (this.lastOperation == "Pause")
            //{
            //    this.provider.CurrentLen = this.currentLen;
            //}

            if (item == "BreakPointStart")
            {
                this.provider.Locate(ref this.currentLen);
                this.provider.CurrentLen = this.currentLen;
            }
        }

        protected void UpdateReturnPoint()
        {
            if (!this.IsReturnAfterMachine)
            {
                SystemContext.MachineControlPara.ReturnPoint = null;
            }
            else
            {
                int index = this.ReturnPointIndex;
                if (index == 0)      //Relative Zero Point
                {
                    int index1 = SystemContext.CoordinatePara.RefZeroIndex;
                    SystemContext.MachineControlPara.ReturnPoint = SystemContext.CoordinatePara.RefZeroSeries[index1];
                }
                else if (index == 1)  //Start Point  
                {
                    if (this.provider != null && (!this.provider.IsEmpty))
                    {
                        var items = this.provider.DataCollection[0].Points;
                        SystemContext.MachineControlPara.ReturnPoint = items[0];
                    }
                }
                else if (index == 2)  //End Point
                {
                    if (this.provider != null && (!this.provider.IsEmpty))
                    {
                        var items = this.provider.DataCollection[this.provider.DataCollection.Count - 1].Points;
                        SystemContext.MachineControlPara.ReturnPoint = items[0];
                    }
                }
                else if (index == 3)   //Absolute Point
                {
                    SystemContext.MachineControlPara.ReturnPoint = new PointF(0, 0);
                }
                else     //Mark Point
                {
                    int index1 = index - 4;
                    SystemContext.MachineControlPara.ReturnPoint = SystemContext.CoordinatePara.MarkSeries[index1];
                }
            }
        }

        private MachineInformation GetMachineInfo()
        {
            //var items = CopyUtil.DeepCopyBaseOnJSon(this.provider.DataCollection);
            var items = this.provider.DataCollection;
            var p1 = SystemContext.Hardware.GetCurrentPosition();
            var p2 = items[0].Points[0];
            var data = new DataUnit(DataUnitTypes.Polyline, 0, new List<PointF> { p1, p2 });
            items.Insert(0, data);
            this.UpdateReturnPoint();
            var p3 = SystemContext.MachineControlPara.ReturnPoint;
            if (p3 != null)
            {
                var tmp = items[items.Count - 1].Points;
                var p4 = tmp[tmp.Count - 1];
                data = new DataUnit(DataUnitTypes.Polyline, 0, new List<PointF> { p4, p3.Value });
                items.Add(data);
            }
            var process = ProcessFactory.CreateNewProcess(items);
            process.IsFirst = true;
            items.Remove(data);
            return process.MachineInfo;
        }

        private void AddSysLog(string msg, Color color)
        {
            Messenger.Default.Send<object>(Tuple.Create(msg, color), "AddSysLog");
        }
        #endregion
    }
}
