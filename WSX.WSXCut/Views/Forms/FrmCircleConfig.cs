using DevExpress.Utils.MVVM;
using System.Windows.Forms;
using WSX.ViewModels.Forms;


namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmCircleConfig : DevExpress.XtraEditors.XtraForm
    {
        public FrmCircleConfig()
        {
            InitializeComponent();
        }

        public void SetDataContext(FrmCircleViewModel viewModel)
        {
            var context = new MVVMContext();
            context.ContainerControl = this;
            context.SetViewModel(typeof(FrmCircleViewModel), viewModel);

            var fluent = context.OfType<FrmCircleViewModel>();

            //this.ucInputCnt.Number = viewModel.Count;
            //this.ucInputCnt.NumberChanged += (sender, e) => viewModel.Count = (int)this.ucInputCnt.Number;
            //fluent.SetTrigger(x => x.Count, cnt => this.ucInputCnt.Number = cnt);

            fluent.SetBinding(this.ucInputCnt, e => e.Number, x => x.Count);

            this.ucInputInterval.Number = viewModel.Interval;
            this.ucInputInterval.NumberChanged += (sender, e) => viewModel.Interval = (int)this.ucInputInterval.Number;
            fluent.SetTrigger(x => x.Interval, interval => this.ucInputInterval.Number = interval);

            this.radioGroupModel.SelectedIndex = viewModel.Normal ? 0 : 1;
            this.radioGroupModel.SelectedIndexChanged += (sender, e) => viewModel.Normal = this.radioGroupModel.SelectedIndex == 0;
            fluent.SetTrigger(x => x.Normal, normal => this.radioGroupModel.SelectedIndex = normal ? 0 : 1);

            fluent.SetBinding(this.checkEditClear, e => e.Checked, x => x.ClearMachineCount);
            fluent.SetBinding(this.checkEditStart, e => e.Checked, x => x.MachineImmediately);

            this.btnAbort.Click += (sender, e) => { viewModel.Result = DialogResult.Abort; this.Close(); };
            this.btnOK.Click += (sender, e) => { this.btnOK.Focus(); viewModel.Result = DialogResult.OK; this.Close(); };
            this.btnCancel.Click += (sender, e) => { viewModel.Result = DialogResult.Cancel; this.Close(); };

            fluent.SetBinding(this.radioGroupModel, e => e.Enabled, x => x.Enabled);
            fluent.SetBinding(this.checkEditStart, e => e.Enabled, x => x.Enabled);
        }
    }
}
