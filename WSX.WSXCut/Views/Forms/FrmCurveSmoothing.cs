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
    public partial class FrmCurveSmoothing : DevExpress.XtraEditors.XtraForm
    {
        public CurveSmoothingModel Model { get; internal set; }
        public FrmCurveSmoothing()
        {
            InitializeComponent();
        }

        public FrmCurveSmoothing(CurveSmoothingModel curveSmoothing) : this()
        {
            this.Model = CopyUtil.DeepCopy(curveSmoothing);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new CurveSmoothingModel();
                this.mvvmContext1.SetViewModel(typeof(CurveSmoothingModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<CurveSmoothingModel>();
            fluent.SetBinding(this.txtPrecisionSize.PopupEdit, c => c.Text, x => x.PrecisionSize);
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
