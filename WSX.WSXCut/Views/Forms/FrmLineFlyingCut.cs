using System;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmLineFlyingCut : DevExpress.XtraEditors.XtraForm
    {
        public LineFlyingCutModel Model { get; internal set; }
        public FrmLineFlyingCut()
        {
            InitializeComponent();
        }

        public FrmLineFlyingCut(LineFlyingCutModel lineFlyingCut):this()
        {
            this.Model = CopyUtil.DeepCopy(lineFlyingCut);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model != null)
                {
                    this.mvvmContext1.SetViewModel(typeof(LineFlyingCutModel), this.Model);
                }
                else
                {
                    this.Model = this.mvvmContext1.GetViewModel<LineFlyingCutModel>();
                }
                InitializeBindings();
            }
        }

        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<LineFlyingCutModel>();
            fluent.SetBinding(this.rbtnLeftTop, e => e.Checked, x => x.StartKnifePostion,
                m => { return StartKnifePostions.LeftTop == m; },
                r => { return StartKnifePostions.LeftTop; });
            fluent.SetBinding(this.rbtnLeftBottom, e => e.Checked, x => x.StartKnifePostion,
                m => { return StartKnifePostions.LeftBottom == m; },
                r => { return StartKnifePostions.LeftBottom; });
            fluent.SetBinding(this.rbtnRightTop, e => e.Checked, x => x.StartKnifePostion,
                 m => { return StartKnifePostions.RightTop == m; },
                 r => { return StartKnifePostions.RightTop; });
            fluent.SetBinding(this.rbtnRightBottom, e => e.Checked, x => x.StartKnifePostion,
                 m => { return StartKnifePostions.RightBottom == m; },
                 r => { return StartKnifePostions.RightBottom; });
            fluent.SetBinding(this.txtDistanceDeviation.PopupEdit, c => c.Text, x => x.DistanceDeviation);
            fluent.SetBinding(this.txtMaxConnectDistance.PopupEdit, c => c.Text, x => x.MaxConnectDistance);
            
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
