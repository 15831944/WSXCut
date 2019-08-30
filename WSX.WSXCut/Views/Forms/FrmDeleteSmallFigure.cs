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
    public partial class FrmDeleteSmallFigure : DevExpress.XtraEditors.XtraForm
    {
        public DeleteSmallFigureModel Model { get; internal set; }
        public FrmDeleteSmallFigure()
        {
            InitializeComponent();
        }

        public FrmDeleteSmallFigure(DeleteSmallFigureModel deleteSmallFigure) : this()
        {
            this.Model = CopyUtil.DeepCopy(deleteSmallFigure);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new DeleteSmallFigureModel();
                this.mvvmContext1.SetViewModel(typeof(DeleteSmallFigureModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<DeleteSmallFigureModel>();
            fluent.SetBinding(this.txtFigureLength.PopupEdit, c => c.Text, x => x.FigureLength);
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
