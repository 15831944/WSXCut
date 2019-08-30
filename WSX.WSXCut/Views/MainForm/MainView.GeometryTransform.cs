using DevExpress.XtraBars;

namespace WSX.WSXCut
{
    //常用-->几何变换-->几何变换
    public partial class MainView
    {
		/// <summary>
        /// 平移操作
        /// </summary>
        private void btnTranslation_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.MovePosition();
        }

        #region 对齐
        private void btnAlignLeft_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnLeftAligment(null);
        }
        private void btnAlignRight_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnRightAligment(null);
        }
        private void btnAlignHorizontalCenter_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnHorizonalCenter(null);
        }
        private void btnAlignTop_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnTopAligment(null);
        }
        private void btnAlignBottom_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnBottomAligment(null);
        }
        private void btnAlignVerticalCenter_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnVerticalCenter(null);
        }
        private void btnAlignCenter_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnCenterAligment(null);
        }
        #endregion

        #region 镜像
        private void btnMirrorAnyAngle_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnMirrorAny();
        }
        private void btnMirrorHorizontal_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnHorizonalMirror(null);
        }
        private void btnMirrorVertical_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnVerticalMirror(null);
        }
        #endregion

        #region 旋转
        private void btnAnticlockwise90_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnRotate(90, false);
        }
        private void btnClockwise90_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnRotate(90, true);
        }
        private void btnRotate180_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnRotate(180, false);
        }
        private void btnRotateAnyAngle_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.canvasWrapper1.OnRotateAny();
        }
        #endregion

    }
}
