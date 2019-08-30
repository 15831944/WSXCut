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
    public partial class FrmCommonSide : DevExpress.XtraEditors.XtraForm
    {
        public CommonSideRectangleModel Model { get; internal set; }
        public FrmCommonSide()
        {
            InitializeComponent();
            this.rbtnHorizontalsAndVerticals.Checked = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        public FrmCommonSide(CommonSideRectangleModel commonSideRectangleModel) : this()
        {
            this.Model = CopyUtil.DeepCopy(commonSideRectangleModel);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new CommonSideRectangleModel();
                this.mvvmContext1.SetViewModel(typeof(CommonSideRectangleModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<CommonSideRectangleModel>();
            fluent.SetBinding(this.rbtnHorizontalsAndVerticals, c => c.Checked, x => x.CommonSideStyle,
                m => { return CommonSideStyles.HorizontalsAndVerticals == m; },
                r => { return CommonSideStyles.HorizontalsAndVerticals; });
            fluent.SetBinding(this.rbtFramedPriority, c => c.Checked, x => x.CommonSideStyle,
                m => { return CommonSideStyles.FramedPriority == m; },
                r => { return CommonSideStyles.FramedPriority; });
            fluent.SetBinding(this.rbtSerpentine, c => c.Checked, x => x.CommonSideStyle,
               m => { return CommonSideStyles.Serpentine == m; },
               r => { return CommonSideStyles.Serpentine; });
            fluent.SetBinding(this.rbtFrameFinal, c => c.Checked, x => x.CommonSideStyle,
               m => { return CommonSideStyles.FrameFinal == m; },
               r => { return CommonSideStyles.FrameFinal; });
            fluent.SetBinding(this.rbtStairs, c => c.Checked, x => x.CommonSideStyle,
               m => { return CommonSideStyles.Stairs == m; },
               r => { return CommonSideStyles.Stairs; });
            fluent.SetBinding(this.rbtLeftTop, c => c.Checked, x => x.StartPostion,
               m => { return StartPositions.LeftTop == m; },
               r => { return StartPositions.LeftTop; });
            fluent.SetBinding(this.rbtLeftBotton, c => c.Checked, x => x.StartPostion,
             m => { return StartPositions.LeftBotton == m; },
             r => { return StartPositions.LeftBotton; });
            fluent.SetBinding(this.rbtRightBotton, c => c.Checked, x => x.StartPostion,
            m => { return StartPositions.RightBotton == m; },
            r => { return StartPositions.RightBotton; });
            fluent.SetBinding(this.rbtRightTop, c => c.Checked, x => x.StartPostion,
            m => { return StartPositions.RightTop == m; },
            r => { return StartPositions.RightTop; });
            fluent.SetBinding(this.ckIsCutOut, c => c.Checked, x => x.IsOutCut);
            fluent.SetBinding(this.txtCutOutValue.PopupEdit, c => c.Text, x => x.OutCutValue);
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

        private void rbtnHorizontalsAndVerticals_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnHorizontalsAndVerticals.Checked)
            {
                //groupBox2.Enabled = false;
                ckIsCutOut.Enabled = false;
                txtCutOutValue.Enabled = false;
            }
            else
            {
                //groupBox2.Enabled = true;
                ckIsCutOut.Enabled = true;
                txtCutOutValue.Enabled = true;
            }
        }

        private void rbtFramedPriority_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtFramedPriority.Checked||rbtFrameFinal.Checked)
            {
                ckIsCutOut.Enabled = false;
                txtCutOutValue.Enabled = false;
            }
            else
            {
                ckIsCutOut.Enabled = true;
                txtCutOutValue.Enabled = true;
            }
        }
    }
}
