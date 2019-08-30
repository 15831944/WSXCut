using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmArrayFull : DevExpress.XtraEditors.XtraForm
    {
        public ArrayFullModel Model { get; internal set; }
        public FrmArrayFull()
        {
            InitializeComponent();
        }

        public FrmArrayFull(ArrayFullModel arrayFull) : this()
        {
            this.Model = CopyUtil.DeepCopy(arrayFull);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new ArrayFullModel();
                this.mvvmContext1.SetViewModel(typeof(ArrayFullModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<ArrayFullModel>();
            fluent.SetBinding(this.txtPlateWidth.PopupEdit, c => c.Text, x => x.PlateWidth);
            fluent.SetBinding(this.txtPlateHeight.PopupEdit, c => c.Text, x => x.PlateHeight);
            fluent.SetBinding(this.txtPartsSpacing.PopupEdit, c => c.Text, x => x.PartsSpacing);
            fluent.SetBinding(this.txtPlateRetainEdge.PopupEdit, c => c.Text, x => x.PlateRetainEdge);
            fluent.SetBinding(this.ckAutoCombination, c => c.Checked, x => x.IsAutoCombination);
            fluent.SetBinding(this.ckBanRotation, c => c.Checked, x => x.IsBanRotation);
            fluent.SetBinding(this.ckAutoCommonEdge, c => c.Checked, x => x.IsAutoCommonEdge);
            fluent.SetBinding(this.ckClearOriginalCompleted, c => c.Checked, x => x.IsClearOriginalCompleted);

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

    }
}
