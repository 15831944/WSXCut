using System;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmFigureSizeSet : DevExpress.XtraEditors.XtraForm
    {
        public FigureSizeSetModel Model { get; internal set; }
        private double WHRatio = 1.0;
        public FrmFigureSizeSet()
        {
            InitializeComponent();
            this.cmbCommonSize.SelectedIndex = 0;
            this.chkLockHWRatio.Enabled = true;
            this.txtNewWidth.OnInputComplete += NewWidthInputCompete;
            this.txtNewHeight.OnInputComplete += NewHeightInputComplete;
        }
        public FrmFigureSizeSet(FigureSizeSetModel model) : this()
        {
            this.Model = model;
            this.txtCurrentHeight.Text = this.Model.Height.ToString("#0.000");
            this.txtCurrentWidth.Text = this.Model.Width.ToString("#0.000");
            this.txtNewHeight.Text = this.txtCurrentHeight.Text;
            this.txtNewWidth.Text = this.txtCurrentWidth.Text;
            string centerPoint = ((int)this.Model.ScaleCenterPoint).ToString();
            foreach (var item in this.gbScaleCenter.Controls)
            {
                if (((RadioButton)item).Tag.ToString() == centerPoint)
                {
                    ((RadioButton)item).Checked = true;
                    break;
                }
            }
            this.WHRatio = this.Model.Width/ this.Model.Height ;
        }
       

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Focus();
            this.Model.Height = double.Parse(this.txtNewHeight.Text.Trim());
            this.Model.Width = double.Parse(this.txtNewWidth.Text.Trim());
            foreach (var item in this.gbScaleCenter.Controls)
            {
                if (((RadioButton)item).Checked)
                {
                    this.Model.ScaleCenterPoint = (FigureSizeSetModel.ScaleCenter)Convert.ToInt32(((RadioButton)item).Tag);
                    break;
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {          
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void checkEdit16_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbCommonSize.Enabled = this.chkLockHWRatio.Checked;
        }

        public void NewWidthInputCompete(double newValue)
        {
            this.AdjustHeightWidth(newValue);
        }

        private void NewHeightInputComplete(double newValue)
        {
            if (this.chkLockHWRatio.Checked)
            {
                this.txtNewWidth.Text = (this.WHRatio * newValue).ToString("#0.000");
            }
        }

        private void AdjustHeightWidth(double width)
        {
            this.txtNewWidth.Text = width.ToString("#0.000");
            if (this.chkLockHWRatio.Checked)
            {
                this.txtNewHeight.Text = (width/ this.WHRatio).ToString("#0.000");
            }
        }

        private void cmbCommonSize_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            var item = cmbCommonSize.EditValue + "";
            switch (item)
            {
                case "20mm":
                case "50mm":
                case "100mm":
                case "200mm":
                    {
                        double width = double.Parse(item.Replace("mm", ""));
                        this.AdjustHeightWidth(width);
                    }
                    break;
                case "0.5倍":
                case "2倍":
                case "4倍":
                case "8倍":
                case "10倍":
                case "20倍":
                    {
                        double width = double.Parse(item.Replace("倍", "")) * this.Model.Width;
                        this.AdjustHeightWidth(width);
                    }
                    break;
            }
        }
    }
}
