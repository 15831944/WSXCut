using DevExpress.Utils.MVVM;
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
    public partial class FrmArcFlyingCut : DevExpress.XtraEditors.XtraForm
    {
        public ArcFlyingCutModel Model { get; internal set; }
        public FrmArcFlyingCut()
        {
            InitializeComponent();

        }

        public FrmArcFlyingCut(ArcFlyingCutModel arcFlyingCut) : this()
        {
            this.Model = CopyUtil.DeepCopy(arcFlyingCut);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new ArcFlyingCutModel();
                this.mvvmContext1.SetViewModel(typeof(ArcFlyingCutModel), this.Model);
                InitializeBindings();
            }
        }

        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<ArcFlyingCutModel>();
            fluent.SetBinding(this.txtMaxConnectSpace.PopupEdit, c => c.Text, x => x.MaxConnectSpace);
            fluent.SetBinding(this.ckFirstSort, c => c.Checked, x => x.IsFirstSort);
            fluent.SetBinding(this.ckFlyingByPart, c => c.Checked, x => x.IsFlyingByPart);
            fluent.SetBinding(this.cmbSortTypes, c => c.SelectedIndex, x => x.SortType,
                m => { return (int)m; },
                r => { return (ArcFlyingCutSortTypes)r; });
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
