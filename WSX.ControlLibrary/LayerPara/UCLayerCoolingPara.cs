using DevExpress.Mvvm.POCO;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Physics.Converters;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.Utilities;

namespace WSX.ControlLibrary.LayerPara
{
    public partial class UCLayerCoolingPara : DevExpress.XtraEditors.XtraUserControl
    {     
        public LayerCoolingModel Model { get; set; }

        public UCLayerCoolingPara()
        {
            InitializeComponent();
        }

        public UCLayerCoolingPara(LayerCoolingModel layerCooling) : this()
        {         
            this.Model = this.mvvmContext1.GetViewModel<LayerCoolingModel>();
            CopyUtil.CopyModel(this.Model, layerCooling ?? DefaultParaHelper.GetDefaultLayerCoolingModel());           
            this.InitializeBindings();
            var monitor = new UnitMonitor(this, () => this.Model.RaisePropertiesChanged());
            monitor.Listen();
        }

        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<LayerCoolingModel>();
            fluent.SetBinding(this.txtCoolingSpeed.PopupEdit, c => c.Text, x => x.CoolingSpeed, m => SpeedUnitConverter.Convert(m), r => SpeedUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtLiftHeight.PopupEdit, c => c.Text, x => x.LiftHeight);
            fluent.SetBinding(this.txtNozzleHeight.PopupEdit, c => c.Text, x => x.NozzleHeight);
            fluent.SetBinding(this.cmbGasKind, c => c.Text, x => x.GasKind);
            fluent.SetBinding(this.txtGasPressure.PopupEdit, c => c.Text, x => x.GasPressure, m => PressureUnitConverter.Convert(m), r => PressureUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtUserNotes, c => c.Text, x => x.UserNotes);
        }     
    }
}
