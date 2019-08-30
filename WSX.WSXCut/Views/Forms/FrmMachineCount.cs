using System;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmMachineCount : DevExpress.XtraEditors.XtraForm
    {
        public MachineCountModel Model { get; internal set; }
        public FrmMachineCount()
        {
            InitializeComponent();

        }

        public FrmMachineCount(MachineCountModel machineCount) : this()
        {
            this.Model = CopyUtil.DeepCopy(machineCount);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new MachineCountModel();
                this.mvvmContext1.SetViewModel(typeof(MachineCountModel), this.Model);
                InitializeBindings();
            }
        }

        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<MachineCountModel>();
            fluent.SetBinding(this.txtPlanTotalCount, c => c.Number, x => x.PlanTotalCount);
            fluent.SetBinding(this.spFinishedCount, c => c.EditValue, x => x.FinishedCount);
            fluent.SetBinding(this.cmbSortTypes, c => c.SelectedIndex, x => x.MachineFinishedType,
                m => { return (int)m; },
                r => { return (MachineFinishedTypes)r; });
            fluent.SetBinding(this.cmbPlanTotalCount, c => c.Text, x => x.TotalSecond,
                m =>
                {
                    long hours = m / (60 * 60);
                    long minute = m % (60 * 60) / (60);
                    if (hours != 0 && minute != 0) return hours + "小时" + minute + "分钟";
                    else if (hours != 0) return hours + "小时";
                    else if (minute != 0) return minute + "分钟";
                    else
                    {
                        this.Model.TotalSecond = 10 * 60;
                        return "10分钟";
                    }
                },
                r =>
                {
                    //10小时34分钟，34分钟10小时
                    try
                    {
                        long totalSecond = 0;
                        int indexm = r.IndexOf("分钟");
                        int indexh = r.IndexOf("小时");
                        if (indexh != -1 && indexm != -1)
                        {
                            if (indexh < indexm)
                            {
                                totalSecond += long.Parse(r.Substring(0, indexh)) * 60 * 60;
                                totalSecond += long.Parse(r.Substring(indexh + 2, indexm - (indexh + 2))) * 60;
                            }
                            else
                            {
                                totalSecond += long.Parse(r.Substring(0, indexm)) * 60;
                                totalSecond += long.Parse(r.Substring(indexm + 2, indexh - (indexm + 2))) * 60 * 60;
                            }
                        }
                        else if (indexh != -1)
                        {
                            totalSecond += long.Parse(r.Substring(0, indexh)) * 60 * 60;
                        }
                        else if (indexm != -1)
                        {
                            totalSecond += long.Parse(r.Substring(0, indexm)) * 60;
                        }
                        return totalSecond;
                    }
                    catch (Exception ex)
                    {
                        return this.Model.TotalSecond;
                    }
                });
            fluent.SetBinding(this.ckAutoSuspend, c => c.Checked, x => x.IsAutoSuspend);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Focus();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.spFinishedCount.EditValue = 0.0;
        }
    }
}
