using System;
using System.Windows.Forms;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmReleaseOrSmoothing : DevExpress.XtraEditors.XtraForm
    {
        public float RoundRadius { get; private set; }
        public FrmReleaseOrSmoothing()
        {
            InitializeComponent();
            this.txtRadius.Text = "1";
        }

        public void SetHeaderParams(string header)
        {
            this.Text = header;
            this.labelControl1.Text = header;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Focus();
            this.RoundRadius = Convert.ToSingle(this.txtRadius.Text.Trim());
            this.DialogResult = DialogResult.Yes;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
