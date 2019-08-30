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
    public partial class FrmArrayInteractive : DevExpress.XtraEditors.XtraForm
    {
        public ArrayInteractiveModel Model { get; internal set; }
        public FrmArrayInteractive()
        {
            InitializeComponent();
        }

        public FrmArrayInteractive(ArrayInteractiveModel arrayInteractive) : this()
        {
            this.Model = CopyUtil.DeepCopy(arrayInteractive);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new ArrayInteractiveModel();
                this.mvvmContext1.SetViewModel(typeof(ArrayInteractiveModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<ArrayInteractiveModel>();
            fluent.SetBinding(this.txtRowSpacing.PopupEdit, c => c.Text, x => x.RowSpacing);
            fluent.SetBinding(this.txtColumnSpacing.PopupEdit, c => c.Text, x => x.ColumnSpacing);
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
