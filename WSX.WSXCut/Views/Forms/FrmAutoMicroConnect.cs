using System;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel.MicroConn;
using DevExpress.XtraEditors;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmAutoMicroConnect : DevExpress.XtraEditors.XtraForm
    {
        public bool IsAllFigure { get { return this.cmbApplySelectedTypes.SelectedIndex == 0 ? false : true; } }
        public AutoMicroConParam Param
        {
            get
            {
                return new AutoMicroConParam()
                {
                    IsTypeCount = this.rbtnCount.Checked,
                    TypeValue = this.rbtnCount.Checked ? this.txtCount.Number : this.txtDistance.Number,
                    MicroSize = this.txtMicroSize.Number,
                    IsNotApplyCorner = this.ckNotApplyCorner.Checked,
                    IsNotApplyStartPoint = this.ckNotApplyStartPoint.Checked,
                    IsMaxSize = this.ckMaxSize.Checked,
                    IsMinSize = this.ckMinSize.Checked,
                    MaxSize = this.txtMaxSize.Number,
                    MinSize = this.txtMinSize.Number
                };
            }
        }
        public FrmAutoMicroConnect()
        {
            InitializeComponent();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Focus();
            if (this.rbtnSpacing.Checked && this.txtMicroSize.Number >= this.txtDistance.Number)
            {
                XtraMessageBox.Show("间隔距离不得小于微连长度！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.txtMicroSize.Number > 0)
            {
                this.DialogResult = DialogResult.OK;
            }           
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbtnNumber_CheckedChanged(object sender, EventArgs e)
        {
            this.labelControl4.Visible = !this.rbtnCount.Checked;
            this.txtDistance.Visible = !this.rbtnCount.Checked;
            this.labelControl26.Visible = this.rbtnCount.Checked;
            this.txtCount.Visible = this.rbtnCount.Checked;
        }

        private void ckFilterMin_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMinSize.Enabled = this.ckMinSize.Checked;
        }

        private void ckFilterMax_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMaxSize.Enabled = this.ckMaxSize.Checked;
        }

        private void FrmAutoMicroConnect_Load(object sender, EventArgs e)
        {
            this.txtMaxSize.Enabled = this.ckMaxSize.Checked;
            this.txtMinSize.Enabled = this.ckMinSize.Checked;
        }
    }


}
