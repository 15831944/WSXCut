namespace WSX.WSXCut.Views.Forms
{
    partial class FrmFigureSizeSet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFigureSizeSet));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmbCommonSize = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gbScaleCenter = new System.Windows.Forms.GroupBox();
            this.rbtnRightBottom = new System.Windows.Forms.RadioButton();
            this.rbtnRight = new System.Windows.Forms.RadioButton();
            this.rbtnRightTop = new System.Windows.Forms.RadioButton();
            this.rbtnBottom = new System.Windows.Forms.RadioButton();
            this.rbtnLeftBottom = new System.Windows.Forms.RadioButton();
            this.rbtnMiddle = new System.Windows.Forms.RadioButton();
            this.rbtnLeft = new System.Windows.Forms.RadioButton();
            this.rbtnTop = new System.Windows.Forms.RadioButton();
            this.rbtnLeftTop = new System.Windows.Forms.RadioButton();
            this.chkLockHWRatio = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtNewHeight = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtNewWidth = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtCurrentHeight = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtCurrentWidth = new WSX.ControlLibrary.Common.UCNumberInputer();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCommonSize.Properties)).BeginInit();
            this.gbScaleCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkLockHWRatio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(395, 57);
            this.panelControl1.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 39);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(180, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "本功能用于修改选中图形的尺寸。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(21, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(120, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "设置图形尺寸";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 295);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(395, 48);
            this.panelControl2.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(307, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnOK.Location = new System.Drawing.Point(209, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 27);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确  定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(20, 75);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(84, 14);
            this.labelControl26.TabIndex = 10;
            this.labelControl26.Text = "图形当前尺寸：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(20, 101);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(84, 14);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "请输入新尺寸：";
            // 
            // cmbCommonSize
            // 
            this.cmbCommonSize.EditValue = "";
            this.cmbCommonSize.Location = new System.Drawing.Point(107, 148);
            this.cmbCommonSize.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.cmbCommonSize.Name = "cmbCommonSize";
            this.cmbCommonSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCommonSize.Properties.Items.AddRange(new object[] {
            "请选择",
            "20mm",
            "50mm",
            "100mm",
            "200mm",
            "0.5倍",
            "2倍",
            "4倍",
            "8倍",
            "10倍",
            "20倍"});
            this.cmbCommonSize.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbCommonSize.Size = new System.Drawing.Size(223, 20);
            this.cmbCommonSize.TabIndex = 5;
            this.cmbCommonSize.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.cmbCommonSize_Closed);
            // 
            // gbScaleCenter
            // 
            this.gbScaleCenter.Controls.Add(this.rbtnRightBottom);
            this.gbScaleCenter.Controls.Add(this.rbtnRight);
            this.gbScaleCenter.Controls.Add(this.rbtnRightTop);
            this.gbScaleCenter.Controls.Add(this.rbtnBottom);
            this.gbScaleCenter.Controls.Add(this.rbtnLeftBottom);
            this.gbScaleCenter.Controls.Add(this.rbtnMiddle);
            this.gbScaleCenter.Controls.Add(this.rbtnLeft);
            this.gbScaleCenter.Controls.Add(this.rbtnTop);
            this.gbScaleCenter.Controls.Add(this.rbtnLeftTop);
            this.gbScaleCenter.Location = new System.Drawing.Point(20, 178);
            this.gbScaleCenter.Name = "gbScaleCenter";
            this.gbScaleCenter.Size = new System.Drawing.Size(335, 109);
            this.gbScaleCenter.TabIndex = 23;
            this.gbScaleCenter.TabStop = false;
            this.gbScaleCenter.Text = "缩放中心";
            // 
            // rbtnRightBottom
            // 
            this.rbtnRightBottom.AutoSize = true;
            this.rbtnRightBottom.Location = new System.Drawing.Point(250, 78);
            this.rbtnRightBottom.Name = "rbtnRightBottom";
            this.rbtnRightBottom.Size = new System.Drawing.Size(49, 18);
            this.rbtnRightBottom.TabIndex = 8;
            this.rbtnRightBottom.Tag = "8";
            this.rbtnRightBottom.Text = "右下";
            this.rbtnRightBottom.UseVisualStyleBackColor = true;
            // 
            // rbtnRight
            // 
            this.rbtnRight.AutoSize = true;
            this.rbtnRight.Location = new System.Drawing.Point(250, 54);
            this.rbtnRight.Name = "rbtnRight";
            this.rbtnRight.Size = new System.Drawing.Size(37, 18);
            this.rbtnRight.TabIndex = 5;
            this.rbtnRight.Tag = "5";
            this.rbtnRight.Text = "右";
            this.rbtnRight.UseVisualStyleBackColor = true;
            // 
            // rbtnRightTop
            // 
            this.rbtnRightTop.AutoSize = true;
            this.rbtnRightTop.Location = new System.Drawing.Point(250, 30);
            this.rbtnRightTop.Name = "rbtnRightTop";
            this.rbtnRightTop.Size = new System.Drawing.Size(49, 18);
            this.rbtnRightTop.TabIndex = 2;
            this.rbtnRightTop.Tag = "2";
            this.rbtnRightTop.Text = "右上";
            this.rbtnRightTop.UseVisualStyleBackColor = true;
            // 
            // rbtnBottom
            // 
            this.rbtnBottom.AutoSize = true;
            this.rbtnBottom.Location = new System.Drawing.Point(140, 78);
            this.rbtnBottom.Name = "rbtnBottom";
            this.rbtnBottom.Size = new System.Drawing.Size(37, 18);
            this.rbtnBottom.TabIndex = 7;
            this.rbtnBottom.Tag = "7";
            this.rbtnBottom.Text = "下";
            this.rbtnBottom.UseVisualStyleBackColor = true;
            // 
            // rbtnLeftBottom
            // 
            this.rbtnLeftBottom.AutoSize = true;
            this.rbtnLeftBottom.Location = new System.Drawing.Point(18, 78);
            this.rbtnLeftBottom.Name = "rbtnLeftBottom";
            this.rbtnLeftBottom.Size = new System.Drawing.Size(49, 18);
            this.rbtnLeftBottom.TabIndex = 6;
            this.rbtnLeftBottom.Tag = "6";
            this.rbtnLeftBottom.Text = "左下";
            this.rbtnLeftBottom.UseVisualStyleBackColor = true;
            // 
            // rbtnMiddle
            // 
            this.rbtnMiddle.AutoSize = true;
            this.rbtnMiddle.Location = new System.Drawing.Point(140, 54);
            this.rbtnMiddle.Name = "rbtnMiddle";
            this.rbtnMiddle.Size = new System.Drawing.Size(37, 18);
            this.rbtnMiddle.TabIndex = 4;
            this.rbtnMiddle.Tag = "4";
            this.rbtnMiddle.Text = "中";
            this.rbtnMiddle.UseVisualStyleBackColor = true;
            // 
            // rbtnLeft
            // 
            this.rbtnLeft.AutoSize = true;
            this.rbtnLeft.Location = new System.Drawing.Point(18, 54);
            this.rbtnLeft.Name = "rbtnLeft";
            this.rbtnLeft.Size = new System.Drawing.Size(37, 18);
            this.rbtnLeft.TabIndex = 3;
            this.rbtnLeft.Tag = "3";
            this.rbtnLeft.Text = "左";
            this.rbtnLeft.UseVisualStyleBackColor = true;
            // 
            // rbtnTop
            // 
            this.rbtnTop.AutoSize = true;
            this.rbtnTop.Location = new System.Drawing.Point(140, 30);
            this.rbtnTop.Name = "rbtnTop";
            this.rbtnTop.Size = new System.Drawing.Size(37, 18);
            this.rbtnTop.TabIndex = 1;
            this.rbtnTop.Tag = "1";
            this.rbtnTop.Text = "上";
            this.rbtnTop.UseVisualStyleBackColor = true;
            // 
            // rbtnLeftTop
            // 
            this.rbtnLeftTop.AutoSize = true;
            this.rbtnLeftTop.Checked = true;
            this.rbtnLeftTop.Location = new System.Drawing.Point(18, 30);
            this.rbtnLeftTop.Name = "rbtnLeftTop";
            this.rbtnLeftTop.Size = new System.Drawing.Size(49, 18);
            this.rbtnLeftTop.TabIndex = 0;
            this.rbtnLeftTop.TabStop = true;
            this.rbtnLeftTop.Tag = "0";
            this.rbtnLeftTop.Text = "左上";
            this.rbtnLeftTop.UseVisualStyleBackColor = true;
            // 
            // chkLockHWRatio
            // 
            this.chkLockHWRatio.EditValue = true;
            this.chkLockHWRatio.Location = new System.Drawing.Point(242, 123);
            this.chkLockHWRatio.Margin = new System.Windows.Forms.Padding(0);
            this.chkLockHWRatio.Name = "chkLockHWRatio";
            this.chkLockHWRatio.Properties.AutoWidth = true;
            this.chkLockHWRatio.Properties.Caption = "锁定宽高比";
            this.chkLockHWRatio.Size = new System.Drawing.Size(82, 19);
            this.chkLockHWRatio.TabIndex = 4;
            this.chkLockHWRatio.CheckedChanged += new System.EventHandler(this.checkEdit16_CheckedChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(198, 75);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(20, 14);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "mm";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(198, 101);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(20, 14);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "mm";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(335, 76);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(20, 14);
            this.labelControl6.TabIndex = 10;
            this.labelControl6.Text = "mm";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(335, 102);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(20, 14);
            this.labelControl7.TabIndex = 10;
            this.labelControl7.Text = "mm";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(20, 151);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(60, 14);
            this.labelControl8.TabIndex = 10;
            this.labelControl8.Text = "常用尺寸：";
            // 
            // txtNewHeight
            // 
            this.txtNewHeight.Format = null;
            this.txtNewHeight.IsInterger = false;
            this.txtNewHeight.Location = new System.Drawing.Point(242, 99);
            this.txtNewHeight.Margin = new System.Windows.Forms.Padding(0);
            this.txtNewHeight.Max = 3000D;
            this.txtNewHeight.Min = 0D;
            this.txtNewHeight.Name = "txtNewHeight";
            this.txtNewHeight.Number = 0D;
            this.txtNewHeight.ReadOnly = false;
            this.txtNewHeight.Size = new System.Drawing.Size(88, 22);
            this.txtNewHeight.Suffix = null;
            this.txtNewHeight.TabIndex = 3;
            this.txtNewHeight.TextSize = 9F;
            // 
            // txtNewWidth
            // 
            this.txtNewWidth.Format = null;
            this.txtNewWidth.IsInterger = false;
            this.txtNewWidth.Location = new System.Drawing.Point(107, 99);
            this.txtNewWidth.Margin = new System.Windows.Forms.Padding(0);
            this.txtNewWidth.Max = 3000D;
            this.txtNewWidth.Min = 0D;
            this.txtNewWidth.Name = "txtNewWidth";
            this.txtNewWidth.Number = 0D;
            this.txtNewWidth.ReadOnly = false;
            this.txtNewWidth.Size = new System.Drawing.Size(88, 22);
            this.txtNewWidth.Suffix = null;
            this.txtNewWidth.TabIndex = 2;
            this.txtNewWidth.TextSize = 9F;
            // 
            // txtCurrentHeight
            // 
            this.txtCurrentHeight.Enabled = false;
            this.txtCurrentHeight.Format = null;
            this.txtCurrentHeight.IsInterger = false;
            this.txtCurrentHeight.Location = new System.Drawing.Point(242, 73);
            this.txtCurrentHeight.Margin = new System.Windows.Forms.Padding(0);
            this.txtCurrentHeight.Max = 3000D;
            this.txtCurrentHeight.Min = 0D;
            this.txtCurrentHeight.Name = "txtCurrentHeight";
            this.txtCurrentHeight.Number = 0D;
            this.txtCurrentHeight.ReadOnly = false;
            this.txtCurrentHeight.Size = new System.Drawing.Size(88, 22);
            this.txtCurrentHeight.Suffix = null;
            this.txtCurrentHeight.TabIndex = 1;
            this.txtCurrentHeight.TextSize = 9F;
            // 
            // txtCurrentWidth
            // 
            this.txtCurrentWidth.Enabled = false;
            this.txtCurrentWidth.Format = null;
            this.txtCurrentWidth.IsInterger = false;
            this.txtCurrentWidth.Location = new System.Drawing.Point(107, 73);
            this.txtCurrentWidth.Margin = new System.Windows.Forms.Padding(0);
            this.txtCurrentWidth.Max = 3000D;
            this.txtCurrentWidth.Min = 0D;
            this.txtCurrentWidth.Name = "txtCurrentWidth";
            this.txtCurrentWidth.Number = 0D;
            this.txtCurrentWidth.ReadOnly = false;
            this.txtCurrentWidth.Size = new System.Drawing.Size(88, 22);
            this.txtCurrentWidth.Suffix = null;
            this.txtCurrentWidth.TabIndex = 0;
            this.txtCurrentWidth.TextSize = 9F;
            // 
            // FrmFigureSizeSet
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(395, 343);
            this.Controls.Add(this.chkLockHWRatio);
            this.Controls.Add(this.gbScaleCenter);
            this.Controls.Add(this.cmbCommonSize);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl26);
            this.Controls.Add(this.txtNewHeight);
            this.Controls.Add(this.txtNewWidth);
            this.Controls.Add(this.txtCurrentHeight);
            this.Controls.Add(this.txtCurrentWidth);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFigureSizeSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改图形尺寸";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCommonSize.Properties)).EndInit();
            this.gbScaleCenter.ResumeLayout(false);
            this.gbScaleCenter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkLockHWRatio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private ControlLibrary.Common.UCNumberInputer txtCurrentWidth;
        private ControlLibrary.Common.UCNumberInputer txtCurrentHeight;
        private ControlLibrary.Common.UCNumberInputer txtNewWidth;
        private ControlLibrary.Common.UCNumberInputer txtNewHeight;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCommonSize;
        private System.Windows.Forms.GroupBox gbScaleCenter;
        private DevExpress.XtraEditors.CheckEdit chkLockHWRatio;
        private System.Windows.Forms.RadioButton rbtnRightBottom;
        private System.Windows.Forms.RadioButton rbtnRight;
        private System.Windows.Forms.RadioButton rbtnRightTop;
        private System.Windows.Forms.RadioButton rbtnBottom;
        private System.Windows.Forms.RadioButton rbtnLeftBottom;
        private System.Windows.Forms.RadioButton rbtnMiddle;
        private System.Windows.Forms.RadioButton rbtnLeft;
        private System.Windows.Forms.RadioButton rbtnTop;
        private System.Windows.Forms.RadioButton rbtnLeftTop;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}