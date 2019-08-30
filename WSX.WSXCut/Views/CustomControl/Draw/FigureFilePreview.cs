using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawTool;
using WSX.DrawService.DrawTool.MultiSegmentLine.MultiLine;
using WSX.DrawService.Wrapper;
using WSX.Engine.Utilities;
using WSX.WSXCut.Utils;

namespace WSX.WSXCut.Views.CustomControl.Draw
{
    /// <summary>
    /// 图形文件预览
    /// </summary>
    public partial class FigureFilePreview : UserControl, ICanvasOwner
    {
        private UCCanvas myCanvas;
        private DataModel dataModel;
        private List<FigureBaseModel> figures = null;
        private static bool isPreView = true;
        public bool IsPreView { get { return isPreView; } }
        public FigureFilePreview()
        {
            InitializeComponent();
            this.ckPreView.Checked = this.IsPreView;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.dataModel = new DataModel();
            this.myCanvas = new UCCanvas(this, this.dataModel);
            this.myCanvas.Dock = DockStyle.Fill;
            this.panelDrawing.Controls.Add(this.myCanvas);
            this.myCanvas.SetCenter(new UnitPoint(0, 0));
            this.dataModel.SelectEventEnabled = false;
            this.myCanvas.CanvasEnabled = false;
        }

        public void SetPositionInfo(UnitPoint unitPoint)
        {
            //throw new NotImplementedException();
        }

        public void SetSnapInfo(ISnapPoint snap)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 图形预览
        /// </summary>
        /// <param name="figures"></param>
        public void FigurePreview(List<FigureBaseModel> figures)
        {
            this.figures = figures;
            this.lblCount.Text = string.Format("Entities Count:{0}", this.figures == null ? 0 : this.figures.Count);
            FigureHelper.AddToDrawObject(this.myCanvas, figures, true);
            this.SetCanvasView();
        }


        public void SetCanvasView()
        {
            var rect = GetRect();
            var p1 = this.myCanvas.ScreenTopLeftToUnitPoint();
            var p2 = this.myCanvas.ScreenBottomRightToUnitPoint();

            double width = p2.X - p1.X;
            double height = p1.Y - p2.Y;

            double xRatio = width / rect.Width;
            double yRatio = height / rect.Height;

            this.dataModel.Zoom *= (float)(xRatio > yRatio ? yRatio : xRatio);
            this.myCanvas.SetCenter(new UnitPoint(rect.X + rect.Width / 2.0, rect.Y + rect.Height / 2.0));

            this.myCanvas.RaiseScaleChangedEvent();
        }
        private RectangleF GetRect()
        {
            var data = new DataProvider();
            var builder = new DataBuilder(data, this.dataModel.DrawingLayer.Objects);
            DataDirector.Bulid(builder);
            if (data == null || data.IsEmpty)
            {
                return new RectangleF();
            }

            var points = data.OutLine.Points;
            var rect = new RectangleF(points[0], new SizeF(points[2].X - points[1].X, points[1].Y - points[0].Y));
            float x = (float)((1.2 * rect.Width - rect.Width) / 2.0);
            float y = (float)((1.2 * rect.Height - rect.Height) / 2.0);
            rect.Inflate(x, y);
            return rect;
        }
        private void FigureFilePreview_Load(object sender, EventArgs e)
        {
            this.dataModel.AddDrawTool(CanvasCommands.Lines.ToString(), new LineCommon());
            this.dataModel.AddDrawTool(CanvasCommands.CircleCR.ToString(), new DrawService.DrawTool.CircleTool.Circle());
            this.dataModel.AddDrawTool(CanvasCommands.ArcCR.ToString(), new DrawService.DrawTool.Arcs.SweepArc());
            this.dataModel.AddDrawTool(CanvasCommands.Arc3P.ToString(), new DrawService.DrawTool.Arcs.ThreePointsArc());
            this.dataModel.AddDrawTool(CanvasCommands.SingleDot.ToString(), new SingleDot());
            this.dataModel.AddDrawTool(CanvasCommands.SingleRectangle.ToString(), new RectangleCommon());
            this.dataModel.AddDrawTool(CanvasCommands.Hexagon.ToString(), new PolygonCommon());
            this.dataModel.AddDrawTool(CanvasCommands.StarCommon.ToString(), new StarCommon());
            this.dataModel.AddDrawTool(CanvasCommands.RoundRect.ToString(), new RectangleRound());
            this.dataModel.AddDrawTool(CanvasCommands.MultiLine.ToString(), new PolyLine());
            this.myCanvas.DoInvalidate(true);
        }

        private void ckPreView_CheckedChanged(object sender, EventArgs e)
        {
            isPreView = this.ckPreView.Checked;
        }
    }
}
