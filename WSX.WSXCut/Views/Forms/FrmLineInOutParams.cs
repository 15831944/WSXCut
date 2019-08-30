using System;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel.LeadLine;
using WSX.CommomModel.ParaModel;
using WSX.DrawService.Utils;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmLineInOutParams :DevExpress.XtraEditors.XtraForm
    {
        public LineInOutParamsModel LeadwireParam { get; set; }
        public FrmLineInOutParams()
        {
            InitializeComponent();
            this.LeadwireParam = new LineInOutParamsModel();
            this.cmbLineInTypes.SelectedIndex = 0;
            this.cmbLineOutTypes.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Focus();
            this.LeadwireParam.LineInType = (LeadLineType)this.cmbLineInTypes.SelectedIndex;
            this.LeadwireParam.LineInLength = float.Parse(this.txtLineInLength.Text.Trim());
            this.LeadwireParam.LineInAngle =(float)HitUtil.DegreesToRadians(float.Parse(this.txtLineInAngle.Text.Trim()));
            this.LeadwireParam.LineInRadius = float.Parse(this.txtLineInRadius.Text.Trim());
            this.LeadwireParam.IsAddCircularHole = this.ckAddCircularHole.Checked;
            this.LeadwireParam.CircularHoleRadius = float.Parse(this.txtCircularHoleRadius.Text.Trim());

            this.LeadwireParam.LineOutType = (LeadLineType)this.cmbLineOutTypes.SelectedIndex;
            this.LeadwireParam.LineOutLength = float.Parse(this.txtLineOutLength.Text.Trim());
            this.LeadwireParam.LineOutAngle = (float)HitUtil.DegreesToRadians(float.Parse(this.txtLineOutAngle.Text.Trim()));
            this.LeadwireParam.LineOutRadius = float.Parse(this.txtLineOutRadius.Text.Trim());

            if(this.rbtnAutoSelectSuitable.Checked)
            {
                this.LeadwireParam.LinePosition = LinePositions.AutoSelectSuitable;
            }
            else if(this.rbtnFigureTotalLength.Checked)
            {
                this.LeadwireParam.LinePosition = LinePositions.FigureTotalLength;
            }
            else
            {
                this.LeadwireParam.LinePosition = LinePositions.OnlyChangeType;
            }
            this.LeadwireParam.IsVertexLeadin = this.chkVertexLeadIn.Checked;
            this.LeadwireParam.IsLongSideLeadin = this.chkSideLeadin.Checked;
            this.LeadwireParam.FigureTotalLength = float.Parse(this.txtFigureTotalLength.Text.Trim());
            this.LeadwireParam.IsOnlyApplyClosedFigure = this.ckOnlyApplyClosedFigure.Checked;
            this.LeadwireParam.IsOnlyApplyOutFigure = this.chkOnlyApplyOutFigure.Checked;
            this.LeadwireParam.IsOnlyApplyInFigure = this.chkOnlyApplyInFigure.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbtnAutoSelectSuitable_CheckedChanged(object sender, EventArgs e)
        {
            this.chkVertexLeadIn.Enabled = this.rbtnAutoSelectSuitable.Checked;
            this.chkSideLeadin.Enabled = this.rbtnAutoSelectSuitable.Checked;
        }
        private void cmbLineInTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cmbLineInTypes.SelectedIndex)
            {
                case 0:
                    this.txtLineInLength.Enabled = false;
                    this.txtLineInAngle.Enabled = false;
                    this.txtLineInRadius.Enabled = false;
                    this.ckAddCircularHole.Checked = false;
                    this.ckAddCircularHole.Enabled = false;
                    this.txtCircularHoleRadius.Enabled = false;
                    break;
                case 1:
                    this.txtLineInLength.Enabled = true;
                    this.txtLineInAngle.Enabled = true;
                    this.txtLineInRadius.Enabled = false;
                    this.ckAddCircularHole.Enabled = true;
                    this.txtCircularHoleRadius.Enabled = this.ckAddCircularHole.Checked;
                    break;
                case 2:
                    this.txtLineInLength.Enabled = true;
                    this.txtLineInAngle.Enabled = true;
                    this.txtLineInRadius.Enabled = false;
                    this.ckAddCircularHole.Checked = false;
                    this.ckAddCircularHole.Enabled = false;
                    this.txtCircularHoleRadius.Enabled = false;
                    break;
                case 3:
                    this.txtLineInLength.Enabled = true;
                    this.txtLineInAngle.Enabled = true;
                    this.txtLineInRadius.Enabled = true;
                    this.ckAddCircularHole.Enabled = true;
                    this.txtCircularHoleRadius.Enabled = this.ckAddCircularHole.Checked;
                    break;
            }
        }
        private void cmbLineOutTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.cmbLineOutTypes.SelectedIndex)
            {
                case 0:
                    this.txtLineOutLength.Enabled = false;
                    this.txtLineOutAngle.Enabled = false;
                    this.txtLineOutRadius.Enabled = false;
                    break;
                case 1:
                    this.txtLineOutLength.Enabled = true;
                    this.txtLineOutAngle.Enabled = true;
                    this.txtLineOutRadius.Enabled = false;
                    break;
                case 2:
                    this.txtLineOutLength.Enabled = true;
                    this.txtLineOutAngle.Enabled = true;
                    this.txtLineOutRadius.Enabled = false;
                    break;
                case 3:
                    this.txtLineOutLength.Enabled = true;
                    this.txtLineOutAngle.Enabled = true;
                    this.txtLineOutRadius.Enabled = true;
                    break;
            }
        }
        private void ckAddCircularHole_CheckedChanged(object sender, EventArgs e)
        {
            this.txtCircularHoleRadius.Enabled = this.ckAddCircularHole.Checked;
        }

        private void chkVertexLeadIn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkVertexLeadIn.Checked) this.chkSideLeadin.Checked = false;
        }

        private void chkSideLeadin_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSideLeadin.Checked) this.chkVertexLeadIn.Checked = false;
        }

        private void chkOnlyApplyOutFigure_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkOnlyApplyOutFigure.Checked) this.chkOnlyApplyInFigure.Checked = false;
        }

        private void chkOnlyApplyInFigure_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkOnlyApplyInFigure.Checked) this.chkOnlyApplyOutFigure.Checked = false;
        }
    }
}
