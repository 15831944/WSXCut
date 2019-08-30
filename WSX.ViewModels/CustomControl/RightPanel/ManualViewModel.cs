using DevExpress.Mvvm;
using DevExpress.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.Drawing;
using WSX.Engine.Models;
using WSX.Engine.Operation;
using WSX.GlobalData.Model;
using WSX.Hardware.Models;
using WSX.ViewModels.Common;
using WSX.ViewModels.Model;

namespace WSX.ViewModels.RightPanel
{

    public class ManualViewModel: ViewModelExtensions
    {
        private bool ligthSwitchEnabled;
        //private bool followEnabled;
        private bool blowingEnabled;
        private bool redLightEnabled;
        private double lowSpeed = 10;
        private double highSpeed = 50;
        private bool running = false;
        private const double MAX_DISTANCE = 10000;    //10m
        private readonly List<Color> colorMap = new List<Color>
        {
            Color.FromArgb(102, 153, 255),
            Color.FromArgb(0, 204, 0),
            Color.FromArgb(204, 0, 0),
            Color.FromArgb(255, 153, 0),
            Color.FromArgb(204, 204, 0),
            Color.FromArgb(0, 0, 204)
        };

        public const int SET_ZERO = 0;
        public const int SET_FLOATING_COORDINATE = 1;
        public const int SET_MARK_START_INDEX = 12;

        public event Action<string, bool> StatusChanged;

        #region Properties
        public bool IsFloating { get; set; }
        public bool IsStepMove { get; set; }
        public virtual double Speed { get; set; } = 10;    
        public virtual double Step { get; set; } = 1;
        public virtual bool IsPointMoveCut { get; set; }
        public virtual bool IsFast { get; set; }
        public double Power { get; set; } = 10;
        #endregion

        public ManualViewModel()
        {
            MVVMContext.RegisterXtraMessageBoxService();
            OperationEngine.Instance.OnLaserEnabled += Engine_OnLaserEnabled;
            OperationEngine.Instance.OnBlowingEnabled += Engine_OnBlowingEnabled;
            OperationEngine.Instance.OnFollowEnabled += Engine_OnFollowEnabled;
            Messenger.Default.Register<object>(this, "OperStatusChanged", this.OnOperStatusChanged);
            Messenger.Default.Register<object>(this, "SetCoordinate", x => this.SwichContext(0));

            var coordinatePara = SystemContext.CoordinatePara;
            var p = coordinatePara.MarkSeries[0];
            var color = this.colorMap[0];
            Messenger.Default.Send<object>(Tuple.Create(p, color), "MarkFlagChanged");
        }
     
        #region Engine Callback
        private void Engine_OnBlowingEnabled(bool enabled)
        {
            this.NotityStatusChanged("Blowing", enabled);
        }

        private void Engine_OnLaserEnabled(bool enabled)
        {
            this.NotityStatusChanged("Laser", enabled);
        }

        private void Engine_OnFollowEnabled(bool enabled)
        {
            this.NotityStatusChanged("Follow", enabled);
        }
        #endregion

        #region Commands
        public void SwichContext(int index)
        {
            var coordinatePara = SystemContext.CoordinatePara;
            if (coordinatePara == null)
            {
                return;
            }

            bool flag = true;
            if (index == SET_ZERO)                      //Set current point to zero point
            {
                int currentIndex = coordinatePara.RefZeroIndex;
                string subMsg = currentIndex == 0 ? "浮动坐标系" : $"工作坐标系{currentIndex}";
                if (MsgBoxService.ShowMessage($"确定将当前位置设置为[{subMsg}]零点?", "消息", MessageButton.YesNo, MessageIcon.Question) == MessageResult.Yes)
                {
                    var p = SystemContext.Hardware.GetCurrentPosition();
                    coordinatePara.RefZeroSeries[currentIndex] = p;

                    string msg = $"设置程序坐标系{currentIndex}的零点为({p.X.ToString("0.###")},{p.Y.ToString("0.###")})mm";
                    this.AddSysLog(msg, Color.FromArgb(0, 128, 128));
                }
                else
                {
                    flag = false;
                }
            }
            else if (index == SET_FLOATING_COORDINATE)   //Set floating coordinate
            {
                coordinatePara.RefZeroIndex = 0;
                var p = SystemContext.Hardware.GetCurrentPosition();
                coordinatePara.RefZeroSeries[0] = p;

                string msg = $"选择浮动坐标系,其零点在({p.X.ToString("0.###")},{p.Y.ToString("0.###")})mm";
                this.AddSysLog(msg, Color.FromArgb(0, 128, 128));
            }
            else if (index < SET_MARK_START_INDEX)       //Switch coordinate
            {     
                int index1 = index - SET_FLOATING_COORDINATE;
                coordinatePara.RefZeroIndex = index1;

                var zeroPoint = coordinatePara.RefZeroSeries[index1];               
                string msg = $"选择程序坐标系{index1},其零点在({zeroPoint.X.ToString("0.###")},{zeroPoint.Y.ToString("0.###")})mm";
                this.AddSysLog(msg, Color.FromArgb(0, 128, 128));
            }
            else                    //Switch mark            
            {
                coordinatePara.MarkIndex = index - SET_MARK_START_INDEX;
                //this => CanvasWrapper ViewModel
                var p = coordinatePara.MarkSeries[coordinatePara.MarkIndex];
                var color = this.colorMap[coordinatePara.MarkIndex];
                Messenger.Default.Send<object>(Tuple.Create(p, color), "MarkFlagChanged");
                flag = false;

                int index1 = index - SET_MARK_START_INDEX + 1;
                string msg = $"选择标记点{index1},坐标为({p.X.ToString("0.###")},{p.Y.ToString("0.###")})mm";
                this.AddSysLog(msg, Color.FromArgb(0, 128, 0));
            }

            if (flag)
            {
                var zeroPoint = coordinatePara.RefZeroSeries[coordinatePara.RefZeroIndex];
                //this => CanvasWrapper ViewModel               
                Messenger.Default.Send<object>(zeroPoint, "RelativePosChanged");
                //Messenger.Default.Send<object>(null, "OnMoveFigures");
                //Messenger.Default.Send<object>(null, "OnPreview");
                var para = new CanvasOperParameter
                {
                    OperType = CanvasOperTypes.Move | CanvasOperTypes.Preview,
                };
                Messenger.Default.Send<object>(para, "OnOperation");
            }
        }

        public void MakeMovement(Tuple<AxisTypes, double> info)
        {
            var para = new ManualParameter
            {
                Speed = this.Speed,
                Acceleration = GlobalModel.Params.LayerConfig.EmptyMoveAcceleratedSpeed,
                Step = this.IsStepMove ? this.Step * info.Item2 : MAX_DISTANCE * info.Item2,
                Axis = info.Item1,
                IsPointMoveCut = this.IsPointMoveCut
            };

            this.UpdateStep(para);
            if (para.Step == 0)
            {
                return;
            }
         
            //Update UI before movement
            if (this.IsStepMove)
            {
                this.running = true;
                this.NotityStatusChanged("Running");
            }
            Messenger.Default.Send<bool>(true, "OnManualMovement");       //this => Machining ViewModel
            this.AddSysLog("停止-->点动", Color.Black);
            Messenger.Default.Send<object>("PointMove", "UpdateOperation");
            OperationEngine.Instance.MakeManualMovementAsync(para).ContinueWith(x => 
            {
                Messenger.Default.Send<bool>(false, "OnManualMovement");  //this => Machining ViewModel
                if (this.IsStepMove)
                {
                    this.running = false;
                    this.NotityStatusChanged("Idle");
                }
                if (this.IsPointMoveCut)
                {
                    DispatcherService.BeginInvoke(() => this.IsPointMoveCut = false);
                }              
                this.AddSysLog("点动-->停止", Color.Black);
                Messenger.Default.Send<object>("Idle", "UpdateOperation");
            });
        }

        public void ConfigParameter()
        {
            this.ExecuteCmd("ConfigPara", null);
        }

        public void Cancel()
        {
            if (!this.IsStepMove)
            {
                OperationEngine.Instance.Cancel();
            }           
        }

        public void CaptureMarkPoint()
        {
            var p = SystemContext.Hardware.GetCurrentPosition();
            var coordinatePara = SystemContext.CoordinatePara;
            coordinatePara.MarkSeries[coordinatePara.MarkIndex] = p;
            Messenger.Default.Send<object>(Tuple.Create(p, this.colorMap[coordinatePara.MarkIndex]), "MarkFlagChanged");

            int index = coordinatePara.MarkIndex + 1;
            string msg = $"保存标记{index}的绝对坐标为({p.X.ToString("0.###")},{p.Y.ToString("0.###")})mm";
            this.AddSysLog(msg, Color.FromArgb(0, 128, 128));
        }

        public void MoveToMarkPoint()
        {
            var coordinatePara = SystemContext.CoordinatePara;
            var p = coordinatePara.MarkSeries[coordinatePara.MarkIndex];

            var para = new ManualParameter
            {
                Speed = this.Speed,
                Acceleration = GlobalModel.Params.LayerConfig.EmptyMoveAcceleratedSpeed,
                Step = double.NaN,
                TargetPoint = p      
            };

            Messenger.Default.Send<bool>(true, "OnManualMovement");       //this => Machining ViewModel
            this.running = true;
            this.NotityStatusChanged("Running");
            string msg = $"会标记点{coordinatePara.MarkIndex + 1},坐标({p.X.ToString("0.###")},{p.Y.ToString("0.###")})mm";
            this.AddSysLog(msg, Color.FromArgb(0, 128, 0));
            Messenger.Default.Send<object>("ReturnMark", "UpdateOperation");
            OperationEngine.Instance.MakeManualMovementAsync(para).ContinueWith(x =>
            {
                Messenger.Default.Send<bool>(false, "OnManualMovement");  //this => Machining ViewModel        
                this.running = false;
                this.NotityStatusChanged("Idle");
                this.AddSysLog("回标记点-->停止", Color.Black);
                Messenger.Default.Send<object>("Idle", "UpdateOperation");
            });
        }

        public void Preview()
        {
            int index = SystemContext.CoordinatePara.RefZeroIndex;
            if (index == 0)    //Floating coordinate
            {
                var p = SystemContext.Hardware.GetCurrentPosition();
                SystemContext.CoordinatePara.RefZeroSeries[0] = p;
            }
            //if (!this.running)
            //{
            //    Messenger.Default.Send<object>(null, "OnMoveFigures");
            //}
            //Messenger.Default.Send<object>(null, "OnPreview");
            var para = new CanvasOperParameter
            {
                OperType = CanvasOperTypes.Preview,
            };
            if (!this.running)
            {
                para.OperType |= CanvasOperTypes.Move;
            }
            Messenger.Default.Send<object>(para, "OnOperation");
        }

        public void ControlLigthSwitch()
        {
            if (this.running)
            {
                return;
            }

            this.ligthSwitchEnabled = !this.ligthSwitchEnabled;
            this.NotityStatusChanged("LigthSwitch", this.ligthSwitchEnabled);
        }

        public void ControlRedLight()
        {
            if (this.running)
            {
                return;
            }

            this.redLightEnabled = !this.redLightEnabled;
            this.NotityStatusChanged("RedLight", this.redLightEnabled);
        }

        public void LaserOn()
        {
            if (this.running)
            {
                return;
            }

            this.NotityStatusChanged("Laser", true);
            double freq = GlobalModel.Params.LayerConfig.DotBurstPulseFrequency;
            var tmp = new LaserParameter
            {
                FrequencyHz = freq,
                DutyCircle = 50,
                VoltagePercentage = this.Power
            };
            SystemContext.Hardware.LaserOn(tmp);
        }

        public void LaserOff()
        {
            if (this.running)
            {
                return;
            }

            this.NotityStatusChanged("Laser", false);
            SystemContext.Hardware.LaserOff();
        }

        public void ControlHeightDevice()
        {
            if (this.running)
            {
                return;
            }

            //this.followEnabled = !this.followEnabled;
            //this.NotityStatusChanged("Follow", this.followEnabled);

            //TODO: Control height device
            bool flag = SystemContext.Hardware?.IsFollowOn == true;
            if (flag)
            {
                SystemContext.Hardware?.FollowOff();
            }
            else
            {
                SystemContext.Hardware?.FollowOn();
            }
            this.NotityStatusChanged("Follow", !flag);
        }

        public void ControlBlowing()
        {
            if (this.running)
            {
                return;
            }

            this.blowingEnabled = !this.blowingEnabled;
            this.NotityStatusChanged("Blowing", this.blowingEnabled);
            if (this.blowingEnabled)
            {
                SystemContext.Hardware.BlowingOn();
            }
            else
            {
                SystemContext.Hardware.BlowingOff();
            }
        }
        #endregion

        #region Property changed callback
        protected void OnIsFastChanged()
        {
            if (this.IsFast)
            {
                this.Speed = this.highSpeed;
            }
            else
            {
                this.Speed = this.lowSpeed;
            }
        }

        protected void OnSpeedChanged()
        {
            if (this.Speed < this.lowSpeed)
            {
                if (this.IsFast)
                {
                    //this.Speed = this.highSpeed;
                    this.lowSpeed = this.Speed;
                    this.IsFast = false;
                }
                else
                {
                    this.lowSpeed = this.Speed;
                }
            }
            else if (this.Speed < this.highSpeed)
            {
                if (this.IsFast)
                {
                    this.highSpeed = this.Speed;
                }
                else
                {
                    this.lowSpeed = this.Speed;
                }
            }
            else
            {
                if (this.IsFast)
                {
                    this.highSpeed = this.Speed;
                }
                else
                {
                    //this.Speed = this.lowSpeed;
                    this.highSpeed = this.Speed;
                    this.IsFast = true;
                }
            }
        }
        #endregion

        #region Message handler
        private void OnOperStatusChanged(object arg)
        {           
            var status = (OperationStatus)arg;
            this.running = status != OperationStatus.Idle;
            this.NotityStatusChanged(status.ToString(), false);
        }
        #endregion

        #region Utility
        private void NotityStatusChanged(string item, bool enabled = true)
        {
            this.StatusChanged?.Invoke(item, enabled);
        }

        private void UpdateStep(ManualParameter para)
        {
            if (SystemContext.MachineControlPara.SoftwareLimitEnabled && (para.Axis == AxisTypes.AxisX || para.Axis == AxisTypes.AxisY))
            {
                var current = SystemContext.Hardware.GetCurrentPosition();
                var rect = SystemContext.SystemPara.GetMachiningRegion();
                if (para.Axis == AxisTypes.AxisX)
                {
                    #region AxisX soft limit
                    if (para.Step > 0)
                    {
                        if (current.X < rect.Right)
                        {
                            float xDiff = rect.Right - current.X;
                            if (para.Step > xDiff)
                            {
                                para.Step = xDiff;
                            }
                        }
                        else
                        {
                            para.Step = 0;
                        }
                    }
                    else
                    {
                        if (current.X > rect.Left)
                        {
                            float xDiff = rect.Left - current.X;
                            if (para.Step < xDiff)
                            {
                                para.Step = xDiff;
                            }
                        }
                        else
                        {
                            para.Step = 0;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region AxisY soft limit
                    if (para.Step > 0)
                    {
                        if (current.Y < rect.Bottom)
                        {
                            float yDiff = rect.Bottom - current.Y;
                            if (para.Step > yDiff)
                            {
                                para.Step = yDiff;
                            }
                        }
                        else
                        {
                            para.Step = 0;
                        }
                    }
                    else
                    {
                        if (current.Y > rect.Top)
                        {
                            float yDiff = rect.Top - current.Y;
                            if (para.Step < yDiff)
                            {
                                para.Step = yDiff;
                            }
                        }
                        else
                        {
                            para.Step = 0;
                        }
                    }
                    #endregion
                }
            }
        }

        private void AddSysLog(string msg, Color color)
        {
            Messenger.Default.Send<object>(Tuple.Create(msg, color), "AddSysLog");
        }
        #endregion
    }
}
