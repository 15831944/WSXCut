using DevExpress.Mvvm;
using DevExpress.Utils.MVVM;
using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.GlobalData.Model;
using WSX.ViewModels.CustomControl.RightPanel;
using WSX.WSXCut.Views.Forms;

namespace WSX.WSXCut.Views.CustomControl.RightPanel
{
    public partial class UCMachineCount : DevExpress.XtraEditors.XtraUserControl
    {     
        public UCMachineCount()
        {
            InitializeComponent();
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            var context = new MVVMContext();
            context.ContainerControl = this;
            context.ViewModelType = typeof(MachineCountViewModel);

            var fluent = context.OfType<MachineCountViewModel>();
            var viewModel = context.GetViewModel<MachineCountViewModel>();

            fluent.SetBinding(this.lblFinishCount, e => e.Text, x => x.CurrentCount);
            fluent.SetBinding(this.lblPlanTotalCount, e => e.Text, x => x.TotalCount);
            fluent.SetBinding(this.lblCurTime, e => e.Text, x => x.RunningPeroid, period =>
            {
                string timeStr = null;
                if (period.Days != 0)
                {
                    timeStr += period.Days + "天";
                }
                if (period.Hours != 0)
                {
                    timeStr += period.Hours + "小时";
                }
                if (period.Minutes != 0)
                {
                    timeStr += period.Minutes + "分";
                }
                if (period.Seconds != 0)
                {
                    timeStr += period.Seconds + "秒";
                }
                return timeStr;
            });
            fluent.SetBinding(this.lblTotalTime, e => e.Text, x => x.CountdownPeriod, period =>
            {
                string timeStr = null;
                if (period == null)
                {
                    this.lblTotalTime.Visible = false;
                }
                else
                {
                    this.lblTotalTime.Visible = true;
                    timeStr = "-" + period.Value.ToString(@"hh\:mm\:ss");
                }
                return timeStr;
            });
            fluent.BindCommand(this.btnManager, x => x.ConfigParameter());
            viewModel.Register("ConfigPara", x =>
            {
                FrmMachineCount frm = new FrmMachineCount(GlobalModel.Params.MachineCount);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    GlobalModel.Params.MachineCount = frm.Model;
                }
            });
            this.Load += (sender, e) =>
            {
                GlobalModel.Params.MachineCount.IsAutoSuspend = false;
                viewModel.UpdatePara();
            };
        }

        private void OnDisposing()
        {
            var viewModel = MVVMContext.GetViewModel<MachineCountViewModel>(this);
            viewModel?.Dispose();
        }  
    }
}
