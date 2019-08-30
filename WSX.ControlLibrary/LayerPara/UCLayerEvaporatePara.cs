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
    public partial class UCLayerEvaporatePara : DevExpress.XtraEditors.XtraUserControl
    {      
        public LayerEvaporateModel Model { get; set; }
        public UCLayerEvaporatePara()
        {
            InitializeComponent();
        }

        public UCLayerEvaporatePara(LayerEvaporateModel layerEvaporate) :this()
        {       
            this.Model = this.mvvmContext1.GetViewModel<LayerEvaporateModel>();
            CopyUtil.CopyModel(this.Model, layerEvaporate ?? DefaultParaHelper.GetDefaultLayerEvaporateModel());        
            this.InitializeBindings();
            this.InitializeCurve();
            var monitor = new UnitMonitor(this, () => this.Model.RaisePropertiesChanged());
            monitor.Listen();
            UnitObserverFacade.Instance.SpeedUnitObserver.UnitChanged += x => this.InitializeCurve();
        }
      
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<LayerEvaporateModel>();
            //切割
            fluent.SetBinding(this.txtCutSpeed.PopupEdit, c => c.Text, x => x.CutSpeed, m => SpeedUnitConverter.Convert(m), r => SpeedUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtLiftHeight.PopupEdit, c => c.Text, x => x.LiftHeight);
            fluent.SetBinding(this.txtNozzleHeight.PopupEdit, c => c.Text, x => x.NozzleHeight);
            fluent.SetBinding(this.cmbGasKind, c => c.Text, x => x.GasKind);
            fluent.SetBinding(this.txtGasPressure.PopupEdit, c => c.Text, x => x.GasPressure, m => PressureUnitConverter.Convert(m), r => PressureUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtPowerPercent.PopupEdit, c => c.Text, x => x.PowerPercent);
            fluent.SetBinding(this.txtPulseFrequency.PopupEdit, c => c.Text, x => x.PulseFrequency);
            fluent.SetBinding(this.txtPulseDutyFactorPercent.PopupEdit, c => c.Text, x => x.PulseDutyFactorPercent);
            fluent.SetBinding(this.txtBeamSize.PopupEdit, c => c.Text, x => x.BeamSize);
            fluent.SetBinding(this.txtFocusPosition.PopupEdit, c => c.Text, x => x.FocusPosition);
            fluent.SetBinding(this.txtLaserOpenDelay.PopupEdit, c => c.Text, x => x.LaserOpenDelay);
            //fluent.SetBinding(this.ckDynamicAdjustPower, c => c.Checked, x => x.IsDynamicAdjustPower);
            //fluent.SetBinding(this.ckDynamicAdjustFrequency, c => c.Checked, x => x.IsDynamicAdjustFrequency);
            //fluent.SetBinding(this.ckShowAbs, c => c.Checked, x => x.IsShowAbs);
            fluent.SetBinding(this.ckDynamicAdjustPower, c => c.Checked, x => x.PwrCtrlPara.PowerAdjustmentEnabled);
            fluent.SetBinding(this.ckDynamicAdjustFrequency, c => c.Checked, x => x.PwrCtrlPara.FreqAdjustmentEnabled);
            fluent.SetBinding(this.ckShowAbs, c => c.Checked, x => x.PwrCtrlPara.ShowAbs);
            fluent.SetBinding(this.txtUserNotes, c => c.Text, x => x.UserNotes);
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

        private void ckShowAbs_CheckedChanged(object sender, EventArgs e)
        {
            this.Model.PwrCtrlPara.ShowAbs = this.ckShowAbs.Checked;
            this.InitializeCurve();
        }

        private void txtCutSpeed_NumberChanged(object arg1, EventArgs arg2)
        {
            //this.Model.CutSpeed = this.txtCutSpeed.Number;
            this.Model.CutSpeed = SpeedUnitConverter.ConvertBack(this.txtCutSpeed.Number.ToString());
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
