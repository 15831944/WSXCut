using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Physics;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.Common;

namespace WSX.ControlLibrary.Utilities
{
    public class UnitMonitor
    {
        private readonly Control container;
        private readonly Action unitChangedHandler;
       
        public UnitMonitor(Control control, Action handler)
        {
            this.container = control;
            this.unitChangedHandler = handler;
        }

        public void Listen()
        {
            this.InitializeLimit();
            this.InitializeUnitDisplay();
            this.UpdateUnitInfo();
        }


        private void OnTimeUnitChanged(UnitTimeTypes timeUnit)
        {
            UITreeHelper.ChangeLabelText(UITreeHelper.GetAll<LabelControl>(this.container, "Time"), timeUnit.GetDescription());
            unitChangedHandler.Invoke();
            foreach (var m in UITreeHelper.GetAll<TimeInputer>(this.container, "Time"))
            {
                m.UpdateUnitType(timeUnit);
                if (!string.IsNullOrEmpty(m.Suffix))
                {
                    m.Suffix = timeUnit.GetDescription();
                }
            }
        }

        private void OnSpeedUnitChanged(UnitSpeedTypes speedUnit)
        {
            UITreeHelper.ChangeLabelText(UITreeHelper.GetAll<LabelControl>(this.container, "Speed"), speedUnit.GetDescription());
            unitChangedHandler.Invoke();
            foreach (var m in UITreeHelper.GetAll<SpeedInputer>(this.container, "Speed"))
            {
                m.UpdateUnitType(speedUnit);
                if (!string.IsNullOrEmpty(m.Suffix))
                {
                    m.Suffix = speedUnit.GetDescription();
                }
            }
        }

        private void OnAccelerationUnitChanged(UnitAcceleratedTypes accelerationUnit)
        {
            UITreeHelper.ChangeLabelText(UITreeHelper.GetAll<LabelControl>(this.container, "Acceleration"), accelerationUnit.GetDescription());
            unitChangedHandler.Invoke();
            foreach (var m in UITreeHelper.GetAll<AccelerationInputer>(this.container, "Acceleration"))
            {
                m.UpdateUnitType(accelerationUnit);
                if (!string.IsNullOrEmpty(m.Suffix))
                {
                    m.Suffix = accelerationUnit.GetDescription();
                }
            }
        }

        private void OnPressureUnitChanged(UnitPressureTypes pressureUnit)
        {
            UITreeHelper.ChangeLabelText(UITreeHelper.GetAll<LabelControl>(this.container, "Pressure"), pressureUnit.GetDescription());
            unitChangedHandler.Invoke();
            foreach (var m in UITreeHelper.GetAll<PressureInputer>(this.container, "Pressure"))
            {
                m.UpdateUnitType(pressureUnit);
                if (!string.IsNullOrEmpty(m.Suffix))
                {
                    m.Suffix = pressureUnit.GetDescription();
                }
            }
        }

        private void InitializeUnitDisplay()
        {
            var monitor = UnitObserverFacade.Instance;          
            monitor.TimeUnitObserver.UnitChanged += this.OnTimeUnitChanged;
            monitor.SpeedUnitObserver.UnitChanged += this.OnSpeedUnitChanged;
            monitor.AccelerationUnitObserver.UnitChanged += this.OnAccelerationUnitChanged;
            monitor.PressureUnitObserver.UnitChanged += this.OnPressureUnitChanged;
        }

        private void InitializeLimit()
        {
            var item1 = UITreeHelper.GetAll<SpeedInputer>(this.container, "Speed");
            foreach (var m in item1)
            {
                m.MaxValue = Constants.MaxSpeed;
                m.MinValue = Constants.MinSpeed;
            }

            var item2 = UITreeHelper.GetAll<AccelerationInputer>(this.container, "Acceleration");
            foreach (var m in item2)
            {
                m.MaxValue = Constants.MaxAcceleration;
                m.MinValue = Constants.MinAcceleration;
            }

            var item3 = UITreeHelper.GetAll<TimeInputer>(this.container, "Time");
            foreach (var m in item3)
            {
                m.MaxValue = Constants.MaxTime;
                m.MinValue = Constants.MinTime;
            }

            var item4 = UITreeHelper.GetAll<PressureInputer>(this.container, "Pressure");
            foreach (var m in item4)
            {
                m.MaxValue = Constants.MaxPressure;
                m.MinValue = Constants.MinPressure;
            }
        }

        private void UpdateUnitInfo()
        {
            var tmp1 = UnitObserverFacade.Instance.TimeUnitObserver.UnitType;
            var tmp2 = UnitObserverFacade.Instance.SpeedUnitObserver.UnitType;
            var tmp3 = UnitObserverFacade.Instance.AccelerationUnitObserver.UnitType;
            var tmp4 = UnitObserverFacade.Instance.PressureUnitObserver.UnitType;

            this.OnTimeUnitChanged(tmp1);
            this.OnSpeedUnitChanged(tmp2);
            this.OnAccelerationUnitChanged(tmp3);
            this.OnPressureUnitChanged(tmp4);
        }
    }
}
