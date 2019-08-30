using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WSX.CommomModel.ParaModel;
using WSX.Engine.Operation;
using WSX.GlobalData.Model;
using WSX.ViewModels.Common;

namespace WSX.ViewModels.CustomControl.RightPanel
{
    public class MachineCountViewModel : ViewModelExtensions
    {
        private readonly Timer timer;
        private int timerCnt = 0;
        private DateTime? startTime;
        private OperationStatus operStatus;

        public virtual TimeSpan RunningPeroid { get; set; }
        public virtual TimeSpan? CountdownPeriod { get; set; }
        public virtual int TotalCount { get; set; }
        public virtual int CurrentCount { get; set; }

        public MachineCountViewModel()
        {
            this.timer = new Timer(1000);
            this.timer.Elapsed += Timer_Elapsed;
            this.timer.Start();

            Messenger.Default.Register<object>(this, "OnMachineOnce", this.OnMachineOnce);
            Messenger.Default.Register<object>(this, "OnCountdownChanged", this.OnCountdownChanged);
            Messenger.Default.Register<object>(this, "OperStatusChanged", status => this.operStatus = (OperationStatus)status);
        }

        public void ConfigParameter()
        {
            this.ExecuteCmd("ConfigPara", null);
            //GlobalModel.Params.MachineCount.TotalSecond = 5;
            this.UpdatePara();
            if (this.operStatus == OperationStatus.Running && this.startTime == null)
            {
                this.OnCountdownChanged("Start");
            }
        }

        public void UpdatePara()
        {
            this.CurrentCount = GlobalModel.Params.MachineCount.FinishedCount;
            this.TotalCount = GlobalModel.Params.MachineCount.PlanTotalCount;
            bool autoSuspend = GlobalModel.Params.MachineCount.IsAutoSuspend;
            if (autoSuspend)
            {
                if (this.startTime == null)
                {
                    var seconds = GlobalModel.Params.MachineCount.TotalSecond;
                    this.CountdownPeriod = TimeSpan.FromSeconds(seconds);
                }
            }
            else
            {
                this.startTime = null;
                this.CountdownPeriod = null;
            }
        }

        private void OnMachineOnce(object arg)
        {
            DispatcherService.BeginInvoke(() => 
            {
                this.CurrentCount = ++GlobalModel.Params.MachineCount.FinishedCount;
                int total = GlobalModel.Params.MachineCount.PlanTotalCount;
                if (this.CurrentCount == total)
                {
                    var operType = GlobalModel.Params.MachineCount.MachineFinishedType;
                    switch (operType)
                    {
                        case MachineFinishedTypes.None:
                            break;
                        case MachineFinishedTypes.BanMachine:                           
                            break;
                        case MachineFinishedTypes.ShowTips:
                            this.MsgBoxService.ShowMessage($"计划加工{total}次，已完成！", "消息", MessageButton.OK, MessageIcon.Information);
                            break;
                    }
                }
            });
            
          
        }

        private void OnCountdownChanged(object arg)
        {
            if (!GlobalModel.Params.MachineCount.IsAutoSuspend)
            {
                return;
            }

            string status = (string)arg;
            if (status == "Start")
            {
                this.startTime = DateTime.Now;
            }
            else
            {
                if (this.startTime != null)
                {
                    this.startTime = null;
                    this.DispatcherService.BeginInvoke(() => this.CountdownPeriod = null);
                    GlobalModel.Params.MachineCount.IsAutoSuspend = false;
                }
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.timerCnt++;
            this.DispatcherService.BeginInvoke(() => this.RunningPeroid = TimeSpan.FromSeconds(this.timerCnt));
            if (this.startTime != null)
            {
                var peroid = DateTime.Now - this.startTime.Value;
                var refPeroid = TimeSpan.FromSeconds(GlobalModel.Params.MachineCount.TotalSecond);
                var diff = refPeroid - peroid;            
                //if (peroid >= refPeroid)
                if(diff < TimeSpan.FromSeconds(1))
                {
                    this.startTime = null;
                    this.DispatcherService.BeginInvoke(() => this.CountdownPeriod = null);
                    GlobalModel.Params.MachineCount.IsAutoSuspend = false;
                    Messenger.Default.Send<object>(null, "AutoSuspend");
                }
                else
                {
                    this.DispatcherService.BeginInvoke(() => this.CountdownPeriod = refPeroid - peroid);
                }
            }
        }

        public void Dispose()
        {
            this.timer.Stop();
            this.timer.Dispose();
        }
    }
}
