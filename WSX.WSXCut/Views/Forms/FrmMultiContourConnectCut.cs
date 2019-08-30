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
    public partial class FrmMultiContourConnectCut : DevExpress.XtraEditors.XtraForm
    {
        public MultiContourConnectCutModel Model { get; internal set; }
        public FrmMultiContourConnectCut()
        {
            InitializeComponent();
        }

        public FrmMultiContourConnectCut(MultiContourConnectCutModel multiContourConnectCut) : this()
        {
            this.Model = CopyUtil.DeepCopy(multiContourConnectCut);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new MultiContourConnectCutModel();
                this.mvvmContext1.SetViewModel(typeof(MultiContourConnectCutModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<MultiContourConnectCutModel>();
            fluent.SetBinding(this.txtMicroConnectLength.PopupEdit, c => c.Text, x => x.MicroConnectLength);
            fluent.SetBinding(this.txtMaxConnectLength.PopupEdit, c => c.Text, x => x.MaxConnectLength);
            fluent.SetBinding(this.ckBanModifyCutPath, c => c.Checked, x => x.IsBanModifyCutPath);
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
