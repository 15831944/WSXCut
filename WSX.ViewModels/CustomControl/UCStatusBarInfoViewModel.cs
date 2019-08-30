using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WSX.Engine;
using WSX.Engine.Models;
using WSX.Engine.Operation;
using WSX.ViewModels.Common;

namespace WSX.ViewModels.CustomControl
{
    public class UCStatusBarInfoViewModel: ViewModelExtensions
    {
        private System.Timers.Timer timer;
        private PointF currentPoint;
        private bool isDisplayRelative = false;
        private readonly Dictionary<string, string> operMap = new Dictionary<string, string>
        {
            { "Idle","停止" },
            { "Start","正在加工" },
            { "Outline","检边" },
            { "Simulate","模拟" },
            { "Empty","空走" },
            { "Zero","回零" },
            { "Forward","前进" },
            { "Backward", "后退"},
            { "BreakPointLocate","定位" },
            { "BreakPointStart","正在加工" },
            { "Fast","正在加工" },
            { "Stopping","正在停止"},
            { "Pause","暂停" },
            { "Stop","停止" },
            { "PointMove","点动" },
            { "ReturnMark", "回标记点"}
        };
       

        public virtual PointF CanvasPos { get; set; }
        public virtual string Operation { get; set; }
        public virtual PointF CurrentPos { get; set; }
        public virtual bool FineEnabled { get; set; }
        public virtual double Distance { get; set; }
        public virtual bool OperEnabled { get; set; }

        public UCStatusBarInfoViewModel()
        {
            this.Operation = this.operMap["Idle"];
            this.FineEnabled = false;
            this.OperEnabled = true;
            this.Distance = 10;
            Messenger.Default.Register<object>(this, "UpdateCanvasPos", this.OnUpdateCanvasPos);
            Messenger.Default.Register<object>(this, "UpdateOperation", this.OnUpdateOperation);
            Messenger.Default.Register<object>(this, "OperStatusChanged", this.OnOperStatusChanged);
            OperationEngine.Instance.OnMarkPointChanged += Engine_OnMarkPointChanged;
            OperationEngine.Instance.OnStatusChanged += Engine_OnStatusChanged;
            this.timer = new System.Timers.Timer(100);
            this.timer.Elapsed += Timer_Elapsed;
        }

        public void MakeOperation(int index)
        {
            switch (index)
            {
                case 0:
                    this.isDisplayRelative = false;
                    this.UpdateCurrentPos();
                    break;
                case 1:
                    this.isDisplayRelative = true;
                    this.UpdateCurrentPos();
                    break;
                case 2:
                    if (this.MsgBoxService.ShowMessage("确定将当前位置设置为机械坐标零点？") == true)
                    {

                    }
                    break;
                case 3:
                    Messenger.Default.Send<object>(null, "SetCoordinate");
                    break;
                case 4:
                    break;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.UpdateCurrentPos();
        }

        private void UpdateCurrentPos()
        {
            var p = this.currentPoint;
            if (this.isDisplayRelative)
            {
                var zero = SystemContext.CoordinatePara.RefZeroSeries[SystemContext.CoordinatePara.RefZeroIndex];
                p = new PointF(p.X - zero.X, p.Y - zero.Y);
            }        
            this.DispatcherService.BeginInvoke(() => this.CurrentPos = p);
        }

        private void Engine_OnStatusChanged(EngineStatus status)
        {
            if (status == EngineStatus.Running)
            {
                this.timer.Start();              
            }
            else
            {
                this.timer.Stop();
                this.UpdateCurrentPos();
            }
        }

        private void Engine_OnMarkPointChanged(PointF p)
        {
            this.currentPoint = p;
        }

        #region Property changed callback
        protected void OnFineEnabledChanged()
        {
            double dis = this.FineEnabled ? this.Distance : double.NaN;        
            Messenger.Default.Send<double>(dis, "FineDistanceChanged");          
        }

        protected void OnDistanceChanged()
        {
            double dis = this.FineEnabled ? this.Distance : double.NaN;
            Messenger.Default.Send<double>(dis, "FineDistanceChanged");
        }
        #endregion

        #region Message handler
        private void OnUpdateCanvasPos(object arg)
        {
            this.DispatcherService.BeginInvoke(() => this.CanvasPos = (PointF)arg);
        }

        private void OnUpdateOperation(object arg)
        {
            this.DispatcherService.BeginInvoke(() => this.Operation = this.operMap[(string)arg]);
        }

        private void OnOperStatusChanged(object arg)
        {
            bool flag = (OperationStatus)arg == OperationStatus.Idle;
            this.DispatcherService.BeginInvoke(() => this.OperEnabled = flag);
        }
        #endregion
    }
}
