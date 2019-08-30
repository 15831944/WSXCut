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
    public partial class FrmArrayAnnular : DevExpress.XtraEditors.XtraForm
    {
        public ArrayAnnularModel Model { get; internal set; }
        public FrmArrayAnnular()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public FrmArrayAnnular(ArrayAnnularModel arrayAnnular):this()
        {
            this.Model = CopyUtil.DeepCopy(arrayAnnular);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new ArrayAnnularModel();
                this.mvvmContext1.SetViewModel(typeof(ArrayAnnularModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<ArrayAnnularModel>();
            fluent.SetBinding(this.rbtnByAngleSpacing, c => c.Checked, x => x.ArrayStandardType,
                m => { return ArrayStandardTypes.ByAngleSpacing == m; },
                r => { return ArrayStandardTypes.ByAngleSpacing; });
            fluent.SetBinding(this.rbtnByArrayScope, c => c.Checked, x => x.ArrayStandardType,
                m => { return ArrayStandardTypes.ByArrayScope == m; },
                r => { return ArrayStandardTypes.ByArrayScope; });
            fluent.SetBinding(this.txtAngleSpace.PopupEdit, c => c.Text, x => x.AngleSpace);
            fluent.SetBinding(this.txtArrayScope.PopupEdit, c => c.Text, x => x.ArrayScope);
            fluent.SetBinding(this.txtFigureCount.PopupEdit, c => c.Text, x => x.FigureCount);
            fluent.SetBinding(this.ckSetArrayCenterScope, c => c.Checked, x => x.IsSetArrayCenterScope);
            fluent.SetBinding(this.txtCenterCricleRadius.PopupEdit, c => c.Text, x => x.CenterCricleRadius);
            fluent.SetBinding(this.txtCenterStartAngle.PopupEdit, c => c.Text, x => x.CenterStartAngle);
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

        private void FrmArrayAnnular_Load(object sender, EventArgs e)
        {
            var fluent = mvvmContext1.OfType<ArrayAnnularModel>();
            fluent.SetBinding(this.labelControl7, c => c.Visible, x => x.ArrayStandardType,
                m => { return ArrayStandardTypes.ByAngleSpacing == m; });
            fluent.SetBinding(this.labelControl3, c => c.Visible, x => x.ArrayStandardType,
                m => { return ArrayStandardTypes.ByArrayScope == m; });
            fluent.SetBinding(this.txtAngleSpace, c => c.Visible, x => x.ArrayStandardType,
                m => { return ArrayStandardTypes.ByAngleSpacing == m; });
            fluent.SetBinding(this.txtArrayScope, c => c.Visible, x => x.ArrayStandardType,
                m => { return ArrayStandardTypes.ByArrayScope == m; });
        }

        private void rbtnByAngleSpacing_CheckedChanged(object sender, EventArgs e)
        {
            this.labelControl3.Visible = !this.rbtnByAngleSpacing.Checked;
            this.labelControl7.Visible = this.rbtnByAngleSpacing.Checked;
            this.txtArrayScope.Visible = !this.rbtnByAngleSpacing.Checked;
            this.txtAngleSpace.Visible = this.rbtnByAngleSpacing.Checked;
        }
    }
}
