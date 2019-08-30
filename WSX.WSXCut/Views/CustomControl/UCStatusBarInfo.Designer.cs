namespace WSX.WSXCut.Views.CustomControl
{
    partial class UCStatusBarInfo
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCStatusBarInfo));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelCanvasPos = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelOperation = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.dropBtnCoordinate = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenuOper = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.checkBtnFine = new DevExpress.XtraEditors.CheckButton();
            this.ucInputDis = new WSX.ControlLibrary.Common.UCNumberInputer();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuOper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Image = global::WSX.WSXCut.Properties.Resources.line;
            this.labelControl1.Appearance.Options.UseImage = true;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(0, 1);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(7, 24);
            this.labelControl1.TabIndex = 0;
            // 
            // labelCanvasPos
            // 
            this.labelCanvasPos.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.labelCanvasPos.Appearance.Options.UseFont = true;
            this.labelCanvasPos.Appearance.Options.UseTextOptions = true;
            this.labelCanvasPos.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelCanvasPos.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelCanvasPos.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelCanvasPos.Location = new System.Drawing.Point(11, 4);
            this.labelCanvasPos.Name = "labelCanvasPos";
            this.labelCanvasPos.Size = new System.Drawing.Size(111, 18);
            this.labelCanvasPos.TabIndex = 1;
            this.labelCanvasPos.Text = "12.23, -123.34";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Image = global::WSX.WSXCut.Properties.Resources.line;
            this.labelControl3.Appearance.Options.UseImage = true;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(128, 1);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(7, 24);
            this.labelControl3.TabIndex = 2;
            // 
            // labelOperation
            // 
            this.labelOperation.Appearance.Options.UseTextOptions = true;
            this.labelOperation.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelOperation.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelOperation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelOperation.Location = new System.Drawing.Point(141, 4);
            this.labelOperation.Name = "labelOperation";
            this.labelOperation.Size = new System.Drawing.Size(51, 18);
            this.labelOperation.TabIndex = 3;
            this.labelOperation.Text = "停止";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Image = global::WSX.WSXCut.Properties.Resources.line;
            this.labelControl5.Appearance.Options.UseImage = true;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.Location = new System.Drawing.Point(198, 1);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(7, 24);
            this.labelControl5.TabIndex = 4;
            // 
            // dropBtnCoordinate
            // 
            this.dropBtnCoordinate.AllowFocus = false;
            this.dropBtnCoordinate.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.dropBtnCoordinate.Appearance.Options.UseFont = true;
            this.dropBtnCoordinate.DropDownArrowStyle = DevExpress.XtraEditors.DropDownArrowStyle.Show;
            this.dropBtnCoordinate.DropDownControl = this.popupMenuOper;
            this.dropBtnCoordinate.Location = new System.Drawing.Point(210, 2);
            this.dropBtnCoordinate.Name = "dropBtnCoordinate";
            this.dropBtnCoordinate.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.dropBtnCoordinate.Size = new System.Drawing.Size(153, 23);
            this.dropBtnCoordinate.TabIndex = 5;
            this.dropBtnCoordinate.Text = "(x:122.23, y:116.25)";
            // 
            // popupMenuOper
            // 
            this.popupMenuOper.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem5, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.popupMenuOper.Manager = this.barManager1;
            this.popupMenuOper.Name = "popupMenuOper";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "显示机械坐标";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ImageOptions.ImageIndex = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "显示程序坐标";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "设置机械坐标为零";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "设置当前点为程序零点";
            this.barButtonItem4.Id = 3;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "坐标定位";
            this.barButtonItem5.Id = 4;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockWindowTabFont = new System.Drawing.Font("Tahoma", 9F);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5});
            this.barManager1.MaxItemId = 5;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(512, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 27);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(512, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 27);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(512, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 27);
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertImage(global::WSX.WSXCut.Properties.Resources.apply, "apply", typeof(global::WSX.WSXCut.Properties.Resources), 0);
            this.imageCollection1.Images.SetKeyName(0, "apply");
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Image = global::WSX.WSXCut.Properties.Resources.line;
            this.labelControl6.Appearance.Options.UseImage = true;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.Location = new System.Drawing.Point(367, 1);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(7, 24);
            this.labelControl6.TabIndex = 6;
            // 
            // checkBtnFine
            // 
            this.checkBtnFine.AllowFocus = false;
            this.checkBtnFine.Location = new System.Drawing.Point(376, 2);
            this.checkBtnFine.Name = "checkBtnFine";
            this.checkBtnFine.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.checkBtnFine.Size = new System.Drawing.Size(49, 23);
            this.checkBtnFine.TabIndex = 7;
            this.checkBtnFine.Text = "微调";
            // 
            // ucInputDis
            // 
            this.ucInputDis.Format = null;
            this.ucInputDis.IsInterger = false;
            this.ucInputDis.Location = new System.Drawing.Point(429, 3);
            this.ucInputDis.Margin = new System.Windows.Forms.Padding(0);
            this.ucInputDis.Max = 100D;
            this.ucInputDis.Min = 0D;
            this.ucInputDis.Name = "ucInputDis";
            this.ucInputDis.Number = 10D;
            this.ucInputDis.ReadOnly = false;
            this.ucInputDis.Size = new System.Drawing.Size(77, 20);
            this.ucInputDis.Suffix = "mm";
            this.ucInputDis.TabIndex = 8;
            this.ucInputDis.TextSize = 8F;
            // 
            // UCStatusBarInfo
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucInputDis);
            this.Controls.Add(this.checkBtnFine);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.dropBtnCoordinate);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelOperation);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelCanvasPos);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCStatusBarInfo";
            this.Size = new System.Drawing.Size(512, 27);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuOper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelCanvasPos;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelOperation;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DropDownButton dropBtnCoordinate;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.CheckButton checkBtnFine;
        private ControlLibrary.Common.UCNumberInputer ucInputDis;
        private DevExpress.XtraBars.PopupMenu popupMenuOper;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
