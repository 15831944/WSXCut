using System;
using System.Windows.Forms;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmGapOrOverCutSizeSet : DevExpress.XtraEditors.XtraForm
    {
        public float Pos { get; internal set; }
        public FrmGapOrOverCutSizeSet(int actionType,float pos)
        {
            InitializeComponent();
            this.InitFormContent(actionType, pos);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Focus();
            this.DialogResult = DialogResult.OK;
            this.Pos = Convert.ToSingle(this.txtSize.Text.Trim());
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 初始化窗体文字
        /// </summary>
        /// <param name="actionType">0.缺口，1.过切，2.多圈</param>
        /// <param name="pos"></param>
        private void InitFormContent(int actionType, float pos)
        {
            switch (actionType)
            {
                case 0:
                    this.Text = "缺口大小设置";
                    this.lblDescription.Text = "本功能用于设置缺口大小。";
                    this.lblSubTitle.Text = "缺口大小";
                    this.lblUnit.Visible = true;
                    break;
                case 1:
                    this.Text = "过切大小设置";
                    this.lblDescription.Text = "本功能用于设置过切大小。";
                    this.lblSubTitle.Text = "过切大小";
                    this.lblUnit.Visible = true;
                    break;
                case 2:
                    this.Text = "多圈圈数设置";
                    this.lblDescription.Text = "本功能用于设置多圈圈数。";
                    this.lblSubTitle.Text = "多圈圈数";
                    this.lblUnit.Visible = false;
                    break;
            }
            this.lblMainTitle.Text = this.Text;
            this.txtSize.Text = pos.ToString();
        }
    }
}
