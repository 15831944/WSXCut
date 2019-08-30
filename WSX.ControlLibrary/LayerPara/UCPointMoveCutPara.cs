using DevExpress.Mvvm.POCO;
using System;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Physics.Converters;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.Utilities;

namespace WSX.ControlLibrary.LayerPara
{
    /// <summary>
    /// 图层工艺参数
    /// </summary>
    public partial class UCPointMoveCutPara : DevExpress.XtraEditors.XtraUserControl
    {       
        public PointMoveCutModel Model { get; internal set; }
      
        public UCPointMoveCutPara()
        {
            InitializeComponent();            
        }

        public UCPointMoveCutPara(PointMoveCutModel model) : this()
        {         
            this.Model = this.mvvmContext1.GetViewModel<PointMoveCutModel>();          
            CopyUtil.CopyModel(this.Model, model ?? DefaultParaHelper.GetDefaultPointMoveModel());          
            this.InitializeBindings();
            var monitor = new UnitMonitor(this, () => this.Model.RaisePropertiesChanged());
            monitor.Listen();
        }

        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<PointMoveCutModel>();

            fluent.SetBinding(this.cmbMaterialTypes, c => c.EditValue, x => x.MaterialType);
            fluent.SetBinding(this.cmbThickness, c => c.EditValue, x => x.Thickness);
            fluent.SetBinding(this.cmbNozzle, c => c.EditValue, x => x.Nozzle);         
            //切割
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
            this.txtStepTime1.DataBindings.Add("Enabled", this.ckStepTime1, "Checked");
            this.txtExtraPuffing1.DataBindings.Add("Enabled", this.ckExtraPuffing1, "Checked");
            this.txtStepTime2.DataBindings.Add("Enabled", this.ckStepTime2, "Checked");
            this.txtExtraPuffing2.DataBindings.Add("Enabled", this.ckExtraPuffing2, "Checked");
            this.txtStepTime3.DataBindings.Add("Enabled", this.ckStepTime3, "Checked");
            this.txtExtraPuffing3.DataBindings.Add("Enabled", this.ckExtraPuffing3, "Checked");
        }

        private void rbtnPierceLevelNone_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnPierceLevelNone.Checked)
            {    
                this.plLevel1.Enabled = false;
                this.plLevel2.Enabled = false;
                this.plLevel3.Enabled = false;
            }
        }

        private void rbtnPierceLevel1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnPierceLevel1.Checked)
            {
                this.plLevel1.Enabled = true;
                this.plLevel2.Enabled = false;
                this.plLevel3.Enabled = false;
            }
        }

        private void rbtnPierceLevel2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnPierceLevel2.Checked)
            {
                this.plLevel1.Enabled = true;
                this.plLevel2.Enabled = true;
                this.plLevel3.Enabled = false;
            }
        }

        private void rbtnPierceLevel3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnPierceLevel3.Checked)
            {
                this.plLevel1.Enabled = true;
                this.plLevel2.Enabled = true;
                this.plLevel3.Enabled = true;
            }
        }     
    }
}
