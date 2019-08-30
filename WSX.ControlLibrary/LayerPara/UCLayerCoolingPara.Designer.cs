namespace WSX.ControlLibrary.LayerPara
{
    partial class UCLayerCoolingPara
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCLayerCoolingPara));
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.txtGasPressure = new WSX.ControlLibrary.Common.PressureInputer();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtUserNotes = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbGasKind = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtLiftHeight = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtCoolingSpeed = new WSX.ControlLibrary.Common.SpeedInputer();
            this.txtNozzleHeight = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGasKind.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(679, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 439);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3});
            this.barManager1.MaxItemId = 3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.HideWhenMerging = DevExpress.Utils.DefaultBoolean.False;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.Text = "Tools";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "从文件读取";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "保存到文件";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.Image")));
            this.barButtonItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.LargeImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "note";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.Image")));
            this.barButtonItem3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.LargeImage")));
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(679, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 470);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(679, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 439);
            // 
            // txtGasPressure
            // 
            this.txtGasPressure.Format = null;
            this.txtGasPressure.IsInterger = false;
            this.txtGasPressure.Location = new System.Drawing.Point(117, 123);
            this.txtGasPressure.Margin = new System.Windows.Forms.Padding(0);
            this.txtGasPressure.Max = 100D;
            this.txtGasPressure.Min = 0D;
            this.txtGasPressure.Name = "txtGasPressure";
            this.txtGasPressure.Number = 0D;
            this.txtGasPressure.Size = new System.Drawing.Size(89, 20);
            this.txtGasPressure.Suffix = null;
            this.txtGasPressure.TabIndex = 4;
            this.txtGasPressure.Tag = "Pressure";
            this.txtGasPressure.TextSize = 9F;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(43, 102);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.TabIndex = 3;
            this.labelControl6.Text = "气体种类：";
            // 
            // txtUserNotes
            // 
            this.txtUserNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            this.txtUserNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserNotes.Location = new System.Drawing.Point(3, 18);
            this.txtUserNotes.Multiline = true;
            this.txtUserNotes.Name = "txtUserNotes";
            this.txtUserNotes.Size = new System.Drawing.Size(673, 249);
            this.txtUserNotes.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbGasKind);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.txtGasPressure);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.txtLiftHeight);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.labelControl13);
            this.groupBox1.Controls.Add(this.labelControl9);
            this.groupBox1.Controls.Add(this.labelControl8);
            this.groupBox1.Controls.Add(this.txtCoolingSpeed);
            this.groupBox1.Controls.Add(this.txtNozzleHeight);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(0, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(679, 169);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "工艺参数";
            // 
            // cmbGasKind
            // 
            this.cmbGasKind.EditValue = "标准";
            this.cmbGasKind.Location = new System.Drawing.Point(117, 99);
            this.cmbGasKind.Margin = new System.Windows.Forms.Padding(0);
            this.cmbGasKind.MenuManager = this.barManager1;
            this.cmbGasKind.Name = "cmbGasKind";
            this.cmbGasKind.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGasKind.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbGasKind.Size = new System.Drawing.Size(89, 20);
            this.cmbGasKind.TabIndex = 16;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(43, 30);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "冷却速度：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(43, 54);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "喷嘴高度：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(208, 54);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "毫米";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(43, 78);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "上抬高度：";
            // 
            // txtLiftHeight
            // 
            this.txtLiftHeight.Format = null;
            this.txtLiftHeight.IsInterger = false;
            this.txtLiftHeight.Location = new System.Drawing.Point(117, 75);
            this.txtLiftHeight.Margin = new System.Windows.Forms.Padding(0);
            this.txtLiftHeight.Max = 100D;
            this.txtLiftHeight.Min = 0D;
            this.txtLiftHeight.Name = "txtLiftHeight";
            this.txtLiftHeight.Number = 0D;
            this.txtLiftHeight.Size = new System.Drawing.Size(89, 20);
            this.txtLiftHeight.Suffix = null;
            this.txtLiftHeight.TabIndex = 4;
            this.txtLiftHeight.TextSize = 9F;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(208, 78);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 2;
            this.labelControl5.Text = "毫米";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(208, 30);
            this.labelControl13.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(41, 14);
            this.labelControl13.TabIndex = 2;
            this.labelControl13.Tag = "Speed";
            this.labelControl13.Text = "毫米/秒";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(208, 126);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(23, 14);
            this.labelControl9.TabIndex = 2;
            this.labelControl9.Tag = "Pressure";
            this.labelControl9.Text = "BAR";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(43, 126);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(60, 14);
            this.labelControl8.TabIndex = 3;
            this.labelControl8.Text = "气      压：";
            // 
            // txtCoolingSpeed
            // 
            this.txtCoolingSpeed.Format = null;
            this.txtCoolingSpeed.IsInterger = false;
            this.txtCoolingSpeed.Location = new System.Drawing.Point(117, 27);
            this.txtCoolingSpeed.Margin = new System.Windows.Forms.Padding(0);
            this.txtCoolingSpeed.Max = 100D;
            this.txtCoolingSpeed.Min = 0D;
            this.txtCoolingSpeed.Name = "txtCoolingSpeed";
            this.txtCoolingSpeed.Number = 0D;
            this.txtCoolingSpeed.Size = new System.Drawing.Size(89, 20);
            this.txtCoolingSpeed.Suffix = null;
            this.txtCoolingSpeed.TabIndex = 4;
            this.txtCoolingSpeed.Tag = "Speed";
            this.txtCoolingSpeed.TextSize = 9F;
            // 
            // txtNozzleHeight
            // 
            this.txtNozzleHeight.Format = null;
            this.txtNozzleHeight.IsInterger = false;
            this.txtNozzleHeight.Location = new System.Drawing.Point(117, 51);
            this.txtNozzleHeight.Margin = new System.Windows.Forms.Padding(0);
            this.txtNozzleHeight.Max = 100D;
            this.txtNozzleHeight.Min = 0D;
            this.txtNozzleHeight.Name = "txtNozzleHeight";
            this.txtNozzleHeight.Number = 0D;
            this.txtNozzleHeight.Size = new System.Drawing.Size(89, 20);
            this.txtNozzleHeight.Suffix = null;
            this.txtNozzleHeight.TabIndex = 4;
            this.txtNozzleHeight.TextSize = 9F;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtUserNotes);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(679, 270);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "用户备注";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(WSX.CommomModel.ParaModel.LayerCoolingModel);
            // 
            // UCLayerCoolingPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCLayerCoolingPara";
            this.Size = new System.Drawing.Size(679, 470);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGasKind.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Common.UCNumberInputer txtGasPressure;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Common.UCNumberInputer txtLiftHeight;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private Common.UCNumberInputer txtCoolingSpeed;
        private Common.UCNumberInputer txtNozzleHeight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtUserNotes;
        private DevExpress.XtraEditors.ComboBoxEdit cmbGasKind;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}
