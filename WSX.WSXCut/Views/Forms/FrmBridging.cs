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
    public partial class FrmBridging : DevExpress.XtraEditors.XtraForm
    {
        public BridgingModel Model { get; internal set; }
        public FrmBridging()
        {
            InitializeComponent();
        }

        public FrmBridging(BridgingModel bridging) : this()
        {
            this.Model = CopyUtil.DeepCopy(bridging);
            if (this.Model.MaxDistance == 0) this.Model.MaxDistance = 100;
            if (this.Model.Width == 0) this.Model.Width = 10;
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new BridgingModel();
                this.mvvmContext1.SetViewModel(typeof(BridgingModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<BridgingModel>();
            fluent.SetBinding(this.txtMaxDistance, c => c.Number, x => x.MaxDistance);
            fluent.SetBinding(this.txtWidth, c => c.Number, x => x.Width);
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
