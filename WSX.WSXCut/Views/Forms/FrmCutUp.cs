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
    public partial class FrmCutUp : DevExpress.XtraEditors.XtraForm
    {
        public CutUpModel Model { get; internal set; }
        public FrmCutUp()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public FrmCutUp(CutUpModel cutUp):this()
        {
            this.Model = CopyUtil.DeepCopy(cutUp);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new CutUpModel();
                this.mvvmContext1.SetViewModel(typeof(CutUpModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<CutUpModel>();
            fluent.SetBinding(this.rbtnLineSpacing, c => c.Checked, x => x.CutUpType,
                m => { return CutUpTypes.LineSpacing == m; },
                r => { return CutUpTypes.LineSpacing; });
            fluent.SetBinding(this.rbtnLineCount, c => c.Checked, x => x.CutUpType,
                m => { return CutUpTypes.LineCount == m; },
                r => { return CutUpTypes.LineCount; });
            fluent.SetBinding(this.txtSpacing.PopupEdit, c => c.Text, x => x.Spacing);
            fluent.SetBinding(this.txtLongitudeCount.PopupEdit, c => c.Text, x => x.LongitudeCount);
            fluent.SetBinding(this.txtLatitudeCount.PopupEdit, c => c.Text, x => x.LatitudeCount);
            fluent.SetBinding(this.ckDifferInsideOutside, c => c.Checked, x => x.IsDifferInsideOutside);

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

        private void FrmCutUp_Load(object sender, EventArgs e)
        {
            var fluent = mvvmContext1.OfType<CutUpModel>();
            fluent.SetBinding(this.labelControl26, c => c.Visible, x => x.CutUpType,
                m => { return CutUpTypes.LineSpacing == m; });
            fluent.SetBinding(this.labelControl4, c => c.Visible, x => x.CutUpType,
                m => { return CutUpTypes.LineCount == m; });
            fluent.SetBinding(this.labelControl3, c => c.Visible, x => x.CutUpType,
                m => { return CutUpTypes.LineCount == m; });
            fluent.SetBinding(this.txtSpacing, c => c.Visible, x => x.CutUpType,
                m => { return CutUpTypes.LineSpacing == m; });
            fluent.SetBinding(this.txtLatitudeCount, c => c.Visible, x => x.CutUpType,
                m => { return CutUpTypes.LineCount == m; });
            fluent.SetBinding(this.txtLongitudeCount, c => c.Visible, x => x.CutUpType,
                m => { return CutUpTypes.LineCount == m; });
        }

        private void rbtnLineSpacing_CheckedChanged(object sender, EventArgs e)
        {
            this.labelControl4.Visible = !this.rbtnLineSpacing.Checked;
            this.labelControl3.Visible = !this.rbtnLineSpacing.Checked;
            this.txtLatitudeCount.Visible = !this.rbtnLineSpacing.Checked;
            this.txtLongitudeCount.Visible = !this.rbtnLineSpacing.Checked;
            this.labelControl26.Visible = this.rbtnLineSpacing.Checked;
            this.txtSpacing.Visible = this.rbtnLineSpacing.Checked;
        }
    }
}
