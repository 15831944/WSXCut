using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Diagnostics;
using System.Drawing;
using WSX.CommomModel.Utilities;
using WSX.Engine.Operation;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;
using WSX.ViewModels.Common;

namespace WSX.ViewModels.CustomControl.Menu
{
    public enum TimerStatus
    {
        Restart,
        Start,
        Stop
    }

    public class RibPageMachineViewModel: ViewModelExtensions
    {
        private readonly System.Timers.Timer timer;
        private Stopwatch watch = new Stopwatch();
        private const int TIMER_INTERVAL_MS = 100;

        #region Properties
        public TimeSpan MachineTime { get; set; } = TimeSpan.Zero;
        public double Progress { get; set; } = 0;
        public double XPos { get; set; }
        public double YPos { get; set; }
        public double ZPos { get; set; }
        public double FollowHeight { get; set; }
        public double ActualFollowHeight { get; set; } 
        public double XSpeed { get; set; }
        public double YSpeed { get; set; }
        public double Speed { get; set; }
        public double Frequency { get; set; } = 5000;
        public double Power { get; set; } = 100;
        public double DutyCircle { get; set; } = 100;
        #endregion

        public RibPageMachineViewModel()
        {
            timer = new System.Timers.Timer(TIMER_INTERVAL_MS);
            timer.Elapsed += Timer_Elapsed;

            OperationEngine.Instance.OnMotorPosReport += Engine_OnMotorPosReport;
            OperationEngine.Instance.OnMotorSpeedReport += Engine_OnMotorSpeedReport;
            OperationEngine.Instance.OnProgressChanged += Engine_OnProgressChanged;
            OperationEngine.Instance.OnLaserParaChanged += Engine_OnLaserParaChanged;

            Messenger.Default.Register<object>(this, "MenuInfoDisChanged", this.OnDisplayChanged);
            Messenger.Default.Register<object>(this, "TimerStatusChanged", this.OnTimerStatusChanged);
            Messenger.Default.Register<object>(this, "UpdateOperation", this.OnUpdateOperation);
            Messenger.Default.Register<object>(this, "LogMachineTime", this.OnLogMachineTime);
        }

        #region Engine callback
        private void Engine_OnLaserParaChanged(LaserParameter para)
        {
            if (!double.IsNaN(para.FrequencyHz))
            {
                this.Frequency = para.FrequencyHz;
            }
            if (!double.IsNaN(para.VoltagePercentage))
            {
                this.Power = para.VoltagePercentage;
            }
            if (!double.IsNaN(para.DutyCircle))
            {
                this.DutyCircle = para.DutyCircle;
            }
        }

        private void Engine_OnProgressChanged(double progress)
        {
            this.Progress = progress * 100;
        }

        private void Engine_OnMotorPosReport(MotorInfoMap<double> posInfo)
        {          
            this.XPos = posInfo[AxisTypes.AxisX];
            this.YPos = posInfo[AxisTypes.AxisY];
            this.ZPos = posInfo[AxisTypes.AxisZ];
        }

        private void Engine_OnMotorSpeedReport(MotorInfoMap<double> speedInfo)
        {                  
            this.XSpeed = speedInfo[AxisTypes.AxisX];
            this.YSpeed = speedInfo[AxisTypes.AxisY];
            this.Speed = Math.Sqrt(Math.Pow(this.XSpeed, 2) + Math.Pow(this.YSpeed, 2));
        }
        #endregion

        #region Message handler
        private void OnDisplayChanged(object arg)
        {
            bool flag = (bool)arg;
            if (flag)
            {
                this.ExecuteCmd("AttachToMenu", null);
                this.timer.Start();
            }
            else
            {
                this.ExecuteCmd("RemoveFromMenu", null);
                this.timer.Stop();

                this.Progress = 0;
                this.MachineTime = TimeSpan.Zero;
                DispatcherService.BeginInvoke(() =>
                {
                    this.RaisePropertyChanged(x => x.Progress);
                    this.RaisePropertyChanged(x => x.MachineTime);
                });
            }
        }

        private void OnTimerStatusChanged(object arg)
        {
            var status = (TimerStatus)arg;
            switch (status)
            {
                case TimerStatus.Restart:
                    this.watch.Restart();
                    break;
                case TimerStatus.Start:
                    this.watch.Start();
                    break;
                case TimerStatus.Stop:
                    this.watch.Stop();
                    break;
            }
        }

        private void OnUpdateOperation(object arg)
        {
            this.ExecuteCmd("UpdateOperation", arg);
        }

        private void OnLogMachineTime(object arg)
        {
            //this.AddSysLog($"加工结束！花费时间：{this.watch.Elapsed.TotalSeconds.ToString("0.###")}s", Color.FromArgb(0, 128, 0));
            this.AddSysLog($"加工结束！花费时间：{DelayHelper.GetTimeString(this.watch.Elapsed.TotalSeconds)}", Color.FromArgb(0, 128, 0));
        }
        #endregion

        #region Commands
        public void Resume()
        {
            Messenger.Default.Send<object>("Start", "OnOperationTrigger");
        }

        public void Stop()
        {
            Messenger.Default.Send<object>("Stop", "OnOperationTrigger");
        }

        public void Pause()
        {
            Messenger.Default.Send<object>("Pause", "OnOperationTrigger");
        }
        #endregion

        #region Utilities
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DispatcherService.BeginInvoke(() =>
            {             
                this.MachineTime = this.watch.Elapsed;
                this.RaisePropertiesChanged();
            });
        }
        
        private void AddSysLog(string msg, Color color)
        {
            Messenger.Default.Send<object>(Tuple.Create(msg, color), "AddSysLog");
        }

        public void Dispose()
        {
            this.timer.Stop();
            this.watch.Stop();
        }
        #endregion
    }
}
