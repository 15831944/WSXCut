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
    public partial class FrmConnectKnife : DevExpress.XtraEditors.XtraForm
    {
        public ConnectKnifeModel Model { get; internal set; }
        public FrmConnectKnife()
        {
            InitializeComponent();
        }

        public FrmConnectKnife(ConnectKnifeModel connectKnife):this()
        {
            this.Model = CopyUtil.DeepCopy(connectKnife);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new ConnectKnifeModel();
                this.mvvmContext1.SetViewModel(typeof(ConnectKnifeModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<ConnectKnifeModel>();
            fluent.SetBinding(this.txtFormatLength.PopupEdit, c => c.Text, x => x.FormatLength);
            fluent.SetBinding(this.txtDriveDistance.PopupEdit, c => c.Text, x => x.DriveDistance);
            fluent.SetBinding(this.rbtnX, c => c.Checked, x => x.SeparationDirection,
               m => { return SeparationDirections.X == m; },
               r => { return SeparationDirections.X; });
            fluent.SetBinding(this.rbtnY, c => c.Checked, x => x.SeparationDirection,
                m => { return SeparationDirections.Y == m; },
                r => { return SeparationDirections.Y; });
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
