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
    public partial class FrmAutoCoolingPoint : DevExpress.XtraEditors.XtraForm
    {
        public bool IsLeadIn { get { return this.ckLeadinPointCooling.Checked; } }
        public bool IsCorner { get { return this.ckSharpAngleCooling.Checked; } }
        public double MaxAngle { get { return this.txtMaxAngle.Number; } }
        public FrmAutoCoolingPoint()
        {
            InitializeComponent();
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
