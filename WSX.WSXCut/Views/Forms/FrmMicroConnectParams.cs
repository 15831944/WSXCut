using System;
using System.Windows.Forms;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmMicroConnectParams : DevExpress.XtraEditors.XtraForm
    {
        public float MicroConnLength { get { return (float)this.txtMicroConnLen.Number; } }
        public bool IsApplyingToSimilarGraphics { get { return this.chkApplyingToSimilarGraphics.Checked; } }
        public FrmMicroConnectParams()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.btnOk.Focus();
            this.DialogResult = DialogResult.Yes;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
