using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.RingCut;
using WSX.CommomModel.ParaModel;
using WSX.DrawService.Utils;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmRingCut : DevExpress.XtraEditors.XtraForm
    {
        public bool IsCancel { get; set; }
        public CornerRingModel Param
        {
            get
            {
                return new CornerRingModel()
                {
                    MaxAngle = HitUtil.DegreesToRadians(this.txtMaxAngle.Number),
                    MinLen = this.txtMinLen.Number,
                    Size = this.txtOutsideSize.Number,
                    IsScanline = this.ckIsScanline.Checked,
                    Style = this.GetStyle(),
                };
            }
        }

        public FrmRingCut()
        {
            InitializeComponent();
        }
        private  CornerRingType GetStyle()
        {
            if (this.rbtnInner.Checked) return CornerRingType.Inner;
            else if (this.rbtnOuter.Checked) return CornerRingType.Outer;
            else if (this.rbtnAll.Checked) return CornerRingType.All;
            else return CornerRingType.Auto; 
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
        private void btnCancelOperator_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.IsCancel = true;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void DrawPanel()
        {
            int width = pnlImage.Width;
            int height = pnlImage.Height;
            Bitmap image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.Black);
            float count = 4;
            float length = 75;
            UnitPoint p1 = new UnitPoint(20, 90);
            UnitPoint p2 = new UnitPoint(35, 65);
            UnitPoint p3 = new UnitPoint(50, 90);

            UnitPoint intersecte1 = HitUtil.GetLinePointByDistance(p2, p1, length, false);
            UnitPoint intersecte2 = HitUtil.GetLinePointByDistance(p2, p3, length, false);

            List<UnitPoint> pointBers = new List<UnitPoint>();
            pointBers.Add(p2);
            pointBers.Add(intersecte1);
            pointBers.Add(intersecte2);
            pointBers.Add(p2);

            List<UnitPoint> pointLines = new List<UnitPoint>();
            pointLines.Add(p1);
            pointLines.Add(p2);
            pointLines.Add(p3);

            for (int i = 0; i < count; i++)
            {
                var ps = pointBers.Select(e => new PointF((float)(e.X + Math.Abs(p1.X - p3.X) * i), (float)e.Y));
                g.DrawBeziers(Pens.LimeGreen, ps.ToArray());
            }
            for (int i = 0; i < count; i++)
            {
                var ps = pointLines.Select(e => new PointF((float)(e.X + Math.Abs(p1.X - p3.X) * i), (float)e.Y));
                g.DrawLines(Pens.LimeGreen, ps.ToArray());
            }
            using (Graphics tg = pnlImage.CreateGraphics())
            {
                tg.DrawImage(image, 0, 0);　　//把画布贴到画面上
            }
        }
    }
}
