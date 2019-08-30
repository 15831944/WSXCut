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
    public partial class FrmMergeConnectLine : DevExpress.XtraEditors.XtraForm
    {
        public MergeConnectLineModel Model { get; internal set; }
        public FrmMergeConnectLine()
        {
            InitializeComponent();
        }

        public FrmMergeConnectLine(MergeConnectLineModel mergeConnectLine) : this()
        {
            this.Model = CopyUtil.DeepCopy(mergeConnectLine);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new MergeConnectLineModel();
                this.mvvmContext1.SetViewModel(typeof(MergeConnectLineModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<MergeConnectLineModel>();
            fluent.SetBinding(this.txtMaxMergePrecision.PopupEdit, c => c.Text, x => x.MaxMergePrecision);
            fluent.SetBinding(this.cmbMergeValidFigureTypes, c => c.SelectedIndex, x => x.MergeValidFigureType,
                m => { return (int)m; },
                r => { return (MergeValidFigureTypes)r; });
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
