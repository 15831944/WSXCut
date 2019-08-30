using DevExpress.XtraBars;
using System.Drawing;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.WSXCut.Views.Forms;

namespace WSX.WSXCut
{
	//常用-->几何变换-->尺寸
    public partial class MainView
    {
        private FigureSizeSetModel figureSizeSetModel = new FigureSizeSetModel() { ScaleCenterPoint = FigureSizeSetModel.ScaleCenter.LeftTop };
        private void btnFigureSizeItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            //1.读取选中图形外框尺寸
            RectangleF rectangleF = this.canvasWrapper1.GetDrawingObjectSize();
            this.figureSizeSetModel.Height = rectangleF.Height;
            this.figureSizeSetModel.Width = rectangleF.Width;
            //2.传入图形外框尺寸，打开尺寸缩放设置窗体
            FrmFigureSizeSet frmFigureSizeSet = new FrmFigureSizeSet(this.figureSizeSetModel);
            if (frmFigureSizeSet.ShowDialog() == DialogResult.OK)
            {
                PointF scaleCenter = this.GetScaleCenterBasePoint(rectangleF, this.figureSizeSetModel.ScaleCenterPoint);
                double coffWidth = this.figureSizeSetModel.Width / rectangleF.Width;
                double coffHeight = this.figureSizeSetModel.Height / rectangleF.Height;
                this.canvasWrapper1.DoSizeChange(scaleCenter.X,scaleCenter.Y, coffWidth,coffHeight);
            }
        }

        private void DoSizeChangeBySzie(double width)
        {
            //1.读取选中图形外框尺寸
            RectangleF rectangleF = this.canvasWrapper1.GetDrawingObjectSize();
            this.figureSizeSetModel.Height = rectangleF.Height;
            this.figureSizeSetModel.Width = rectangleF.Width;
            PointF scaleCenter = this.GetScaleCenterBasePoint(rectangleF, this.figureSizeSetModel.ScaleCenterPoint);
            double coffWidth = width / rectangleF.Width;
            this.canvasWrapper1.DoSizeChange(scaleCenter.X, scaleCenter.Y, coffWidth, coffWidth);
        }

        private void DoSizeChangeByCoff(double coff)
        {
            //1.读取选中图形外框尺寸
            RectangleF rectangleF = this.canvasWrapper1.GetDrawingObjectSize();
            this.figureSizeSetModel.Height = rectangleF.Height;
            this.figureSizeSetModel.Width = rectangleF.Width;
            PointF scaleCenter = this.GetScaleCenterBasePoint(rectangleF, this.figureSizeSetModel.ScaleCenterPoint);
            this.canvasWrapper1.DoSizeChange(scaleCenter.X, scaleCenter.Y, coff, coff);
        }

        private void btnFigureSize100mm_ItemClick(object sender,ItemClickEventArgs e)
        {
            this.DoSizeChangeBySzie(100);
        }

        private void btnFigureSize200mm_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DoSizeChangeBySzie(200);
        }

        private void btnFigureSize0_5_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DoSizeChangeByCoff(0.5);
        }
        private void btnFigureSize2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DoSizeChangeByCoff(2);
        }
        private void btnFigureSize4_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DoSizeChangeByCoff(4);
        }
        private void btnFigureSize8_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DoSizeChangeByCoff(8);
        }
        private void btnFigureSize10_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.DoSizeChangeByCoff(10);
        }

        private void btnInteractiveZoom_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.InteractScale();
        }

        private PointF GetScaleCenterBasePoint(RectangleF rect, FigureSizeSetModel.ScaleCenter scaleCenter)
        {
            switch (scaleCenter)
            {
                case FigureSizeSetModel.ScaleCenter.LeftTop:
                    return new PointF(rect.Location.X, rect.Location.Y);
                case FigureSizeSetModel.ScaleCenter.Top:
                    return new PointF(rect.Location.X + rect.Width / 2, rect.Location.Y);
                case FigureSizeSetModel.ScaleCenter.RightTop:
                    return new PointF(rect.Location.X + rect.Width, rect.Location.Y);
                case FigureSizeSetModel.ScaleCenter.Left:
                    return new PointF(rect.Location.X, rect.Location.Y - rect.Height / 2);
                case FigureSizeSetModel.ScaleCenter.Middle:
                    return new PointF(rect.Location.X + rect.Width / 2, rect.Location.Y - rect.Height / 2);
                case FigureSizeSetModel.ScaleCenter.Right:
                    return new PointF(rect.Location.X + rect.Width, rect.Location.Y - rect.Height / 2);
                case FigureSizeSetModel.ScaleCenter.LeftBottom:
                    return new PointF(rect.Location.X, rect.Location.Y - rect.Height);
                case FigureSizeSetModel.ScaleCenter.Bottom:
                    return new PointF(rect.Location.X + rect.Width / 2, rect.Location.Y - rect.Height);
                case FigureSizeSetModel.ScaleCenter.RightBottom:
                    return new PointF(rect.Location.X + rect.Width, rect.Location.Y - rect.Height);
            }
            return new PointF(rect.Location.X, rect.Location.Y);
        }
    }
}
