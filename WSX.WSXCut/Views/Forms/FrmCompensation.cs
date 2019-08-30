using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.ParaModel;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmCompensation : DevExpress.XtraEditors.XtraForm
    {
        public bool IsCancel { get; set; }
        public CompensationModel Param
        {
            get
            {
                return new CompensationModel()
                {
                    Size = this.txtInSideWidth.Number,
                    IsSmooth = this.rbtnFillet.Checked,
                    Style = this.GetStyle(),
                };
            }
        }
        public bool IsAllFigure { get { return this.ckCompensationNotClosedFigure.Checked; } }
        private CompensationType GetStyle()
        {
            if (this.rbtnAllInner.Checked) return CompensationType.AllInner;
            else if (this.rbtnAllOuter.Checked) return CompensationType.AllOuter;
            else return CompensationType.Auto;
        }
        public FrmCompensation()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.DrawPanel();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Focus();
            this.DialogResult = DialogResult.OK;
            this.IsCancel = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 取消补偿操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelOperator_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.IsCancel = true;
            this.Close();
        }

        private void rbtnClick_CheckedChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void DrawPanel()
        {
            int width = pnlImage.Width;
            int height = pnlImage.Height;
            Bitmap image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.Black);
            float detal = 15;
            if (this.rbtnFillet.Checked)
            {
                g.DrawLine(Pens.White, new PointF(0, height / 2 - detal), new PointF((float)(width / 1.5), height / 2 - detal));
                RectangleF rectangleF = new RectangleF(new PointF((float)(width / 1.5), height / 2), new SizeF());
                rectangleF.Inflate(detal, detal);
                g.DrawArc(Pens.White, rectangleF, 0, -90);
                g.DrawLine(Pens.White, new PointF((float)(width / 1.5) + detal, height / 2), new PointF((float)(width / 1.5) + detal, height));
            }
            else
            {
                g.DrawLines(Pens.White, new PointF[] { new PointF(0, height / 2 - detal), new PointF((float)(width / 1.5) + detal, height / 2 - detal), new PointF((float)(width / 1.5) + detal, height) });
            }
            g.DrawLines(Pens.LimeGreen, new PointF[] { new PointF(0, height / 2), new PointF((float)(width / 1.5), height / 2), new PointF((float)(width / 1.5), height) });
            //绘制箭头
            Pen p = new Pen(Color.White, 1);
            p.CustomEndCap = new AdjustableArrowCap(5, 5);
            g.DrawLine(p, new PointF((float)(width / 2.25), height / 2 - detal - 30), new PointF((float)(width / 2.25), height / 2 - detal));
            g.DrawLine(p, new PointF((float)(width / 2.25), height / 2 - detal + 50), new PointF((float)(width / 2.25), height / 2));

            using (Graphics tg = pnlImage.CreateGraphics())
            {
                tg.DrawImage(image, 0, 0);　　//把画布贴到画面上
            }
        }
    }
}
