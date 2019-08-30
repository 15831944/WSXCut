using DevExpress.Mvvm.POCO;
using System;
using System.Drawing;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Physics;
using WSX.CommomModel.Physics.Converters;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.Common;
using WSX.ControlLibrary.Utilities;

namespace WSX.ControlLibrary.LayerPara
{
    /// <summary>
    /// 图层工艺参数
    /// </summary>
    public partial class UCLayerCraftPara : DevExpress.XtraEditors.XtraUserControl
    {        
        public LayerCraftModel Model { get; internal set; }
        public string key { get; set; }
        /// <summary>
        /// 蒸发选择改变
        /// </summary>
        public event Action<bool, string> OnEvaporationFilmChanged;
        /// <summary>
        /// 冷却选择改变
        /// </summary>
        public event Action<bool, string> OnPathRecoolingChanged;
        public UCLayerCraftPara()
        {
            InitializeComponent();            
        }

        public UCLayerCraftPara(LayerCraftModel model, string key) : this()
        {         
            this.Model = this.mvvmContext1.GetViewModel<LayerCraftModel>();
            this.key = key;
            CopyUtil.CopyModel(this.Model, model ?? DefaultParaHelper.GetDefaultLayerCraftModel());          
            this.InitializeBindings();
            this.InitializeCurve();
            var monitor = new UnitMonitor(this, () => this.Model.RaisePropertiesChanged());
            monitor.Listen();
            UnitObserverFacade.Instance.SpeedUnitObserver.UnitChanged += x => this.InitializeCurve();
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<LayerCraftModel>();

            fluent.SetBinding(this.cmbMaterialTypes, c => c.EditValue, x => x.MaterialType);
            fluent.SetBinding(this.cmbThickness, c => c.EditValue, x => x.Thickness);
            fluent.SetBinding(this.cmbNozzle, c => c.EditValue, x => x.Nozzle);
            fluent.SetBinding(this.ckShortUnLift, c => c.Checked, x => x.IsShortUnLift);
            fluent.SetBinding(this.ckPrePierce, c => c.Checked, x => x.IsPrePierce);
            fluent.SetBinding(this.ckEvaporationFilm, c => c.Checked, x => x.IsEvaporationFilm);
            fluent.SetBinding(this.ckPathRecooling, c => c.Checked, x => x.IsPathRecooling);
            fluent.SetBinding(this.ckMultiTime, c => c.Checked, x => x.IsMultiTime);
            fluent.SetBinding(this.txtMultiTime.PopupEdit, c => c.Text, x => x.MultiTime);
            fluent.SetBinding(this.ckKeepPuffing, c => c.Checked, x => x.IsKeepPuffing);
            fluent.SetBinding(this.ckUnprocessed, c => c.Checked, x => x.IsUnprocessed);
            fluent.SetBinding(this.ckNoFollow, c => c.Checked, x => x.IsNoFollow);
            fluent.SetBinding(this.cmbProcessedTypes, c => c.SelectedIndex, x => x.ProcessedType,
                m => { return (int)m; },
                r => { return (ProcessedTypes)r; });
            //切割
            fluent.SetBinding(this.txtCutSpeed.PopupEdit, c => c.Text, x => x.CutSpeed, m => SpeedUnitConverter.Convert(m), r => SpeedUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtLiftHeight.PopupEdit, c => c.Text, x => x.LiftHeight);
            fluent.SetBinding(this.txtNozzleHeight.PopupEdit, c => c.Text, x => x.NozzleHeight);
            fluent.SetBinding(this.cmbGasKind, c => c.Text, x => x.GasKind);
            fluent.SetBinding(this.txtGasPressure.PopupEdit, c => c.Text, x => x.GasPressure, m => PressureUnitConverter.Convert(m), r => PressureUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtPowerPercent.PopupEdit, c => c.Text, x => x.PowerPercent);
            fluent.SetBinding(this.txtPowerValue.PopupEdit, c => c.Text, x => x.PowerValue);
            fluent.SetBinding(this.txtPulseDutyFactorPercent.PopupEdit, c => c.Text, x => x.PulseDutyFactorPercent);
            fluent.SetBinding(this.txtPulseFrequency.PopupEdit, c => c.Text, x => x.PulseFrequency);
            fluent.SetBinding(this.txtBeamSize.PopupEdit, c => c.Text, x => x.BeamSize);
            fluent.SetBinding(this.txtFocusPosition.PopupEdit, c => c.Text, x => x.FocusPosition);
            fluent.SetBinding(this.txtDelayTime.PopupEdit, c => c.Text, x => x.DelayTime, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtLaserOffDelay.PopupEdit, c => c.Text, x => x.LaserOffDelay, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));

            fluent.SetBinding(this.ckSlowStart, c => c.Checked, x => x.IsSlowStart);
            fluent.SetBinding(this.ckSlowStop, c => c.Checked, x => x.IsSlowStop);
            fluent.SetBinding(this.txtSlowStartDistance.PopupEdit, c => c.Text, x => x.SlowStartDistance);
            fluent.SetBinding(this.txtSlowStopDistance.PopupEdit, c => c.Text, x => x.SlowStopDistance);
            fluent.SetBinding(this.txtSlowStartSpeed.PopupEdit, c => c.Text, x => x.SlowStartSpeed, m => SpeedUnitConverter.Convert(m), r => SpeedUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtSlowStopSpeed.PopupEdit, c => c.Text, x => x.SlowStopSpeed, m => SpeedUnitConverter.Convert(m), r => SpeedUnitConverter.ConvertBack(r));
            //fluent.SetBinding(this.ckDynamicAdjustPower, c => c.Checked, x => x.IsDynamicAdjustPower);
            //fluent.SetBinding(this.ckDynamicAdjustFrequency, c => c.Checked, x => x.IsDynamicAdjustFrequency);
            //fluent.SetBinding(this.ckIsShowAbs, c => c.Checked, x => x.IsShowAbs);
            fluent.SetBinding(this.ckDynamicAdjustPower, c => c.Checked, x => x.PwrCtrlPara.PowerAdjustmentEnabled);
            fluent.SetBinding(this.ckDynamicAdjustFrequency, c => c.Checked, x => x.PwrCtrlPara.FreqAdjustmentEnabled);
            fluent.SetBinding(this.ckIsShowAbs, c => c.Checked, x => x.PwrCtrlPara.ShowAbs);
            fluent.SetBinding(this.txtUserNotes, c => c.Text, x => x.UserNotes);

            //穿孔
            fluent.SetBinding(this.rbtnPierceLevelNone, c => c.Checked, x => x.PierceLevel,
                m => { return PierceLevels.None == m; },
                r => { return PierceLevels.None; });
            fluent.SetBinding(this.rbtnPierceLevel1, c => c.Checked, x => x.PierceLevel,
                m => { return PierceLevels.Level1 == m; },
                r => { return PierceLevels.Level1; });
            fluent.SetBinding(this.rbtnPierceLevel2, c => c.Checked, x => x.PierceLevel,
                m => { return PierceLevels.Level2 == m; },
                r => { return PierceLevels.Level2; });
            fluent.SetBinding(this.rbtnPierceLevel3, c => c.Checked, x => x.PierceLevel,
                m => { return PierceLevels.Level3 == m; },
                r => { return PierceLevels.Level3; });
            //等级一
            fluent.SetBinding(this.ckStepTime1, c => c.Checked, x => x.PierceLevel1.IsStepTime);
            fluent.SetBinding(this.txtStepTime1.PopupEdit, c => c.Text, x => x.PierceLevel1.StepTime, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtNozzleHeight1.PopupEdit, c => c.Text, x => x.PierceLevel1.NozzleHeight);
            fluent.SetBinding(this.cmbGasKind1, c => c.Text, x => x.PierceLevel1.GasKind);
            fluent.SetBinding(this.txtGasPressure1.PopupEdit, c => c.Text, x => x.PierceLevel1.GasPressure, m => PressureUnitConverter.Convert(m), r => PressureUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtPowerPercent1.PopupEdit, c => c.Text, x => x.PierceLevel1.PowerPercent);
            fluent.SetBinding(this.txtPowerValue1.PopupEdit, c => c.Text, x => x.PierceLevel1.PowerValue);
            fluent.SetBinding(this.txtPulseDutyFactorPercent1.PopupEdit, c => c.Text, x => x.PierceLevel1.PulseDutyFactorPercent);
            fluent.SetBinding(this.txtPulseFrequency1.PopupEdit, c => c.Text, x => x.PierceLevel1.PulseFrequency);
            fluent.SetBinding(this.txtBeamSize1.PopupEdit, c => c.Text, x => x.PierceLevel1.BeamSize);
            fluent.SetBinding(this.txtFocusPosition1.PopupEdit, c => c.Text, x => x.PierceLevel1.FocusPosition);
            fluent.SetBinding(this.txtDelayTime1.PopupEdit, c => c.Text, x => x.PierceLevel1.DelayTime, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.ckExtraPuffing1, c => c.Checked, x => x.PierceLevel1.IsExtraPuffing);
            fluent.SetBinding(this.txtExtraPuffing1.PopupEdit, c => c.Text, x => x.PierceLevel1.ExtraPuffing, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            //等级二
            fluent.SetBinding(this.ckStepTime2, c => c.Checked, x => x.PierceLevel2.IsStepTime);
            fluent.SetBinding(this.txtStepTime2.PopupEdit, c => c.Text, x => x.PierceLevel2.StepTime, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtNozzleHeight2.PopupEdit, c => c.Text, x => x.PierceLevel2.NozzleHeight);
            fluent.SetBinding(this.cmbGasKind2, c => c.Text, x => x.PierceLevel2.GasKind);
            fluent.SetBinding(this.txtGasPressure2.PopupEdit, c => c.Text, x => x.PierceLevel2.GasPressure, m => PressureUnitConverter.Convert(m), r => PressureUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtPowerPercent2.PopupEdit, c => c.Text, x => x.PierceLevel2.PowerPercent);
            fluent.SetBinding(this.txtPowerValue2.PopupEdit, c => c.Text, x => x.PierceLevel2.PowerValue);
            fluent.SetBinding(this.txtPulseDutyFactorPercent2.PopupEdit, c => c.Text, x => x.PierceLevel2.PulseDutyFactorPercent);
            fluent.SetBinding(this.txtPulseFrequency2.PopupEdit, c => c.Text, x => x.PierceLevel2.PulseFrequency);
            fluent.SetBinding(this.txtBeamSize2.PopupEdit, c => c.Text, x => x.PierceLevel2.BeamSize);
            fluent.SetBinding(this.txtFocusPosition2.PopupEdit, c => c.Text, x => x.PierceLevel2.FocusPosition);
            fluent.SetBinding(this.txtDelayTime2.PopupEdit, c => c.Text, x => x.PierceLevel2.DelayTime, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.ckExtraPuffing2, c => c.Checked, x => x.PierceLevel2.IsExtraPuffing);
            fluent.SetBinding(this.txtExtraPuffing2.PopupEdit, c => c.Text, x => x.PierceLevel2.ExtraPuffing, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            //等级三
            fluent.SetBinding(this.ckStepTime3, c => c.Checked, x => x.PierceLevel3.IsStepTime);
            fluent.SetBinding(this.txtStepTime3.PopupEdit, c => c.Text, x => x.PierceLevel3.StepTime, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtNozzleHeight3.PopupEdit, c => c.Text, x => x.PierceLevel3.NozzleHeight);
            fluent.SetBinding(this.cmbGasKind3, c => c.Text, x => x.PierceLevel3.GasKind);
            fluent.SetBinding(this.txtGasPressure3.PopupEdit, c => c.Text, x => x.PierceLevel3.GasPressure, m => PressureUnitConverter.Convert(m), r => PressureUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtPowerPercent3.PopupEdit, c => c.Text, x => x.PierceLevel3.PowerPercent);
            fluent.SetBinding(this.txtPowerValue3.PopupEdit, c => c.Text, x => x.PierceLevel3.PowerValue);
            fluent.SetBinding(this.txtPulseDutyFactorPercent3.PopupEdit, c => c.Text, x => x.PierceLevel3.PulseDutyFactorPercent);
            fluent.SetBinding(this.txtPulseFrequency3.PopupEdit, c => c.Text, x => x.PierceLevel3.PulseFrequency);
            fluent.SetBinding(this.txtBeamSize3.PopupEdit, c => c.Text, x => x.PierceLevel3.BeamSize);
            fluent.SetBinding(this.txtFocusPosition3.PopupEdit, c => c.Text, x => x.PierceLevel3.FocusPosition);
            fluent.SetBinding(this.txtDelayTime3.PopupEdit, c => c.Text, x => x.PierceLevel3.DelayTime, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.ckExtraPuffing3, c => c.Checked, x => x.PierceLevel3.IsExtraPuffing);
            fluent.SetBinding(this.txtExtraPuffing3.PopupEdit, c => c.Text, x => x.PierceLevel3.ExtraPuffing, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            //UI上的绑定
            this.txtMultiTime.DataBindings.Add("Enabled", this.ckMultiTime, "Checked");
            this.txtStepTime1.DataBindings.Add("Enabled", this.ckStepTime1, "Checked");
            this.txtExtraPuffing1.DataBindings.Add("Enabled", this.ckExtraPuffing1, "Checked");
            this.txtStepTime2.DataBindings.Add("Enabled", this.ckStepTime2, "Checked");
            this.txtExtraPuffing2.DataBindings.Add("Enabled", this.ckExtraPuffing2, "Checked");
            this.txtStepTime3.DataBindings.Add("Enabled", this.ckStepTime3, "Checked");
            this.txtExtraPuffing3.DataBindings.Add("Enabled", this.ckExtraPuffing3, "Checked");
            //UI上的关联行为
            this.txtPowerPercent.NumberChanged += (sender, e) => this.txtPowerValue.Number = Constants.MaxPower * this.txtPowerPercent.Number / 100.0;
            this.txtPowerPercent1.NumberChanged += (sender, e) => this.txtPowerValue1.Number = Constants.MaxPower * this.txtPowerPercent1.Number / 100.0;
            this.txtPowerPercent2.NumberChanged += (sender, e) => this.txtPowerValue2.Number = Constants.MaxPower * this.txtPowerPercent2.Number / 100.0;
            this.txtPowerPercent3.NumberChanged += (sender, e) => this.txtPowerValue3.Number = Constants.MaxPower * this.txtPowerPercent3.Number / 100.0;
        }

        private void InitializeCurve()
        {        
            this.chartControl1.Clear();

            bool enabled1 = this.Model.PwrCtrlPara.PowerAdjustmentEnabled;
            if (enabled1)
            {
                var data = this.Model.PwrCtrlPara.PowerCurve;
                this.chartControl1.AddCurve(new Curve
                {
                    Id = "PwrCurve",
                    DisColor = Color.Red,
                    Points = data.Points,
                    Pattern = data.Pattern
                });
            }

            bool enabled2 = this.Model.PwrCtrlPara.FreqAdjustmentEnabled;
            if (enabled2)
            {
                var data = this.Model.PwrCtrlPara.FreqCurve;
                this.chartControl1.AddCurve(new Curve
                {
                    Id = "FreqCurve",
                    DisColor = Color.Blue,
                    Points = data.Points,
                    Pattern = data.Pattern
                });
            }

            double xMax;
            double yMax;
            string xLegend = null;
            string yLegend = null;
            bool showAbs = this.Model.PwrCtrlPara.ShowAbs;
            if (showAbs)
            {
                var speedUnit = UnitObserverFacade.Instance.SpeedUnitObserver.UnitType;
                xLegend = $"Speed({speedUnit.GetDescription()})";

                if (enabled2)
                {
                    yLegend = "Frequency(Hz)\r\n";
                }
                if (enabled1)
                {
                    yLegend += "Power(W)";
                }

                xMax = double.Parse(SpeedUnitConverter.Convert(this.Model.CutSpeed));
                yMax = Constants.MaxPower * this.Model.PowerPercent / 100.0;
            }
            else
            {
                xLegend = "Speed(%)";

                if (enabled2)
                {
                    yLegend = "Frequency(%)\r\n";
                }
                if (enabled1)
                {
                    yLegend += "Power(%)";
                }

                xMax = 100;
                yMax = 100;
            }

            this.chartControl1.XLengend = xLegend;
            this.chartControl1.YLengend = yLegend;
            this.chartControl1.XMax = xMax;
            this.chartControl1.YMax = yMax;
        }
      
        private void rbtnPierceLevelNone_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnPierceLevelNone.Checked)
            {
                this.ckPrePierce.Enabled = false;
                this.plLevel1.Enabled = false;
                this.plLevel2.Enabled = false;
                this.plLevel3.Enabled = false;
            }
        }

        private void rbtnPierceLevel1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnPierceLevel1.Checked)
            {
                this.ckPrePierce.Enabled = true;
                this.plLevel1.Enabled = true;
                this.plLevel2.Enabled = false;
                this.plLevel3.Enabled = false;
            }
        }

        private void rbtnPierceLevel2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnPierceLevel2.Checked)
            {
                this.ckPrePierce.Enabled = true;
                this.plLevel1.Enabled = true;
                this.plLevel2.Enabled = true;
                this.plLevel3.Enabled = false;
            }
        }

        private void rbtnPierceLevel3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnPierceLevel3.Checked)
            {
                this.ckPrePierce.Enabled = true;
                this.plLevel1.Enabled = true;
                this.plLevel2.Enabled = true;
                this.plLevel3.Enabled = true;
            }
        }

        private void ckPrePierce_CheckedChanged(object sender, EventArgs e)
        {
            this.rbtnPierceLevelNone.Enabled = !this.ckPrePierce.Checked;
        }

        private void ckEvaporationFilm_CheckedChanged(object sender, EventArgs e)
        {
            OnEvaporationFilmChanged?.Invoke(this.ckEvaporationFilm.Checked, this.key);
        }

        private void ckPathRecooling_CheckedChanged(object sender, EventArgs e)
        {
            OnPathRecoolingChanged?.Invoke(this.ckPathRecooling.Checked, this.key);
        }

        private void ckDynamicAdjustPower_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckDynamicAdjustFrequency.Checked && !this.ckDynamicAdjustPower.Checked)
            {
                this.ckDynamicAdjustFrequency.Checked = false;
            }          
            this.Model.PwrCtrlPara.PowerAdjustmentEnabled = this.ckDynamicAdjustPower.Checked;
            this.InitializeCurve();           
        }

        private void ckDynamicAdjustFrequency_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckDynamicAdjustFrequency.Checked && !this.ckDynamicAdjustPower.Checked)
            {
                this.ckDynamicAdjustPower.Checked = true;
            }
            else
            {
                this.Model.PwrCtrlPara.FreqAdjustmentEnabled = this.ckDynamicAdjustFrequency.Checked;
                this.InitializeCurve();
            }          
        }

        private void ckIsShowAbs_CheckedChanged(object sender, EventArgs e)
        {
            this.Model.PwrCtrlPara.ShowAbs = this.ckIsShowAbs.Checked;
            this.InitializeCurve();
        }

        private void txtCutSpeed_NumberChanged(object arg1, EventArgs arg2)
        {
            //this.Model.CutSpeed = this.txtCutSpeed.Number;
            this.Model.CutSpeed = SpeedUnitConverter.ConvertBack(this.txtCutSpeed.Number.ToString());
            this.InitializeCurve();
        }

        private void txtPowerPercent_NumberChanged(object sender, EventArgs e)
        {
            this.Model.PowerPercent = this.txtPowerPercent.Number;
            this.InitializeCurve();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {        
            var pwrData = new DataCurveInfo
            {
                Data = CopyUtil.DeepCopy(this.Model.PwrCtrlPara.PowerCurve),
                ShowAbs = this.Model.PwrCtrlPara.ShowAbs,
                SpeedUnit = UnitObserverFacade.Instance.SpeedUnitObserver.UnitType,
                Speed = this.Model.CutSpeed,
                Freq = this.Model.PulseFrequency,
                PowerPercentage = this.Model.PowerPercent
            };
            var freData = new DataCurveInfo
            {
                Data = CopyUtil.DeepCopy(this.Model.PwrCtrlPara.FreqCurve),
                ShowAbs = this.Model.PwrCtrlPara.ShowAbs,
                SpeedUnit = UnitObserverFacade.Instance.SpeedUnitObserver.UnitType,
                Speed = this.Model.CutSpeed,
                Freq = this.Model.PulseFrequency,
                PowerPercentage = this.Model.PowerPercent
            };

            var frm = new FrmPowerEdit(pwrData, freData);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                this.Model.PwrCtrlPara.PowerCurve.Points = CopyUtil.DeepCopy(frm.PwrData.Data.Points);
                this.Model.PwrCtrlPara.FreqCurve.Points = CopyUtil.DeepCopy(frm.FreqData.Data.Points);
                this.InitializeCurve();
            }
        }
    }
}
