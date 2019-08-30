namespace WSX.WSXCut.Views.Forms
{
    partial class FrmRingCut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRingCut));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.ckIsScanline = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtOutsideSize = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtMinLen = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtMaxAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtnAll = new System.Windows.Forms.RadioButton();
            this.rbtnAuto = new System.Windows.Forms.RadioButton();
            this.rbtnInner = new System.Windows.Forms.RadioButton();
            this.rbtnOuter = new System.Windows.Forms.RadioButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelOperator = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsScanline.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnlImage);
            this.groupBox3.Controls.Add(this.ckIsScanline);
            this.groupBox3.Controls.Add(this.labelControl7);
            this.groupBox3.Controls.Add(this.labelControl8);
            this.groupBox3.Controls.Add(this.labelControl9);
            this.groupBox3.Controls.Add(this.txtOutsideSize);
            this.groupBox3.Controls.Add(this.txtMinLen);
            this.groupBox3.Controls.Add(this.txtMaxAngle);
            this.groupBox3.Location = new System.Drawing.Point(21, 72);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(377, 130);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "环切参数";
            // 
            // pnlImage
            // 
            this.pnlImage.Location = new System.Drawing.Point(203, 21);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(168, 100);
            this.pnlImage.TabIndex = 26;
            // 
            // ckIsScanline
            // 
            this.ckIsScanline.Location = new System.Drawing.Point(30, 103);
            this.ckIsScanline.Margin = new System.Windows.Forms.Padding(0);
            this.ckIsScanline.Name = "ckIsScanline";
            this.ckIsScanline.Properties.AutoWidth = true;
            this.ckIsScanline.Properties.Caption = "环切线使用扫描方式";
            this.ckIsScanline.Size = new System.Drawing.Size(130, 19);
            this.ckIsScanline.TabIndex = 25;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(30, 77);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(60, 14);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "外引长度：";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(30, 48);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(60, 14);
            this.labelControl8.TabIndex = 12;
            this.labelControl8.Text = "最短边长：";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(30, 21);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(60, 14);
            this.labelControl9.TabIndex = 13;
            this.labelControl9.Text = "最大夹角：";
            // 
            // txtOutsideSize
            // 
            this.txtOutsideSize.Format = null;
            this.txtOutsideSize.IsInterger = false;
            this.txtOutsideSize.Location = new System.Drawing.Point(93, 72);
            this.txtOutsideSize.Margin = new System.Windows.Forms.Padding(0);
            this.txtOutsideSize.Max = 20D;
            this.txtOutsideSize.Min = 0.01D;
            this.txtOutsideSize.Name = "txtOutsideSize";
            this.txtOutsideSize.Number = 5D;
            this.txtOutsideSize.ReadOnly = false;
            this.txtOutsideSize.Size = new System.Drawing.Size(99, 22);
            this.txtOutsideSize.Suffix = "mm";
            this.txtOutsideSize.TabIndex = 14;
            this.txtOutsideSize.TextSize = 9F;
            // 
            // txtMinLen
            // 
            this.txtMinLen.Format = null;
            this.txtMinLen.IsInterger = false;
            this.txtMinLen.Location = new System.Drawing.Point(93, 46);
            this.txtMinLen.Margin = new System.Windows.Forms.Padding(0);
            this.txtMinLen.Max = 20D;
            this.txtMinLen.Min = 0.01D;
            this.txtMinLen.Name = "txtMinLen";
            this.txtMinLen.Number = 5D;
            this.txtMinLen.ReadOnly = false;
            this.txtMinLen.Size = new System.Drawing.Size(99, 22);
            this.txtMinLen.Suffix = "mm";
            this.txtMinLen.TabIndex = 14;
            this.txtMinLen.TextSize = 9F;
            // 
            // txtMaxAngle
            // 
            this.txtMaxAngle.Format = null;
            this.txtMaxAngle.IsInterger = false;
            this.txtMaxAngle.Location = new System.Drawing.Point(93, 19);
            this.txtMaxAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtMaxAngle.Max = 90D;
            this.txtMaxAngle.Min = 0.01D;
            this.txtMaxAngle.Name = "txtMaxAngle";
            this.txtMaxAngle.Number = 90D;
            this.txtMaxAngle.ReadOnly = false;
            this.txtMaxAngle.Size = new System.Drawing.Size(99, 22);
            this.txtMaxAngle.Suffix = "°";
            this.txtMaxAngle.TabIndex = 15;
            this.txtMaxAngle.TextSize = 9F;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnAll);
            this.groupBox2.Controls.Add(this.rbtnAuto);
            this.groupBox2.Controls.Add(this.rbtnInner);
            this.groupBox2.Controls.Add(this.rbtnOuter);
            this.groupBox2.Location = new System.Drawing.Point(21, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 110);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选项";
            // 
            // rbtnAll
            // 
            this.rbtnAll.Location = new System.Drawing.Point(30, 84);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(191, 18);
            this.rbtnAll.TabIndex = 23;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "内外都环切";
            this.rbtnAll.UseVisualStyleBackColor = true;
            // 
            // rbtnAuto
            // 
            this.rbtnAuto.Checked = true;
            this.rbtnAuto.Location = new System.Drawing.Point(30, 21);
            this.rbtnAuto.Name = "rbtnAuto";
            this.rbtnAuto.Size = new System.Drawing.Size(335, 18);
            this.rbtnAuto.TabIndex = 23;
            this.rbtnAuto.TabStop = true;
            this.rbtnAuto.Text = "自动（阳切外部环切，阴切内部环切）";
            this.rbtnAuto.UseVisualStyleBackColor = true;
            // 
            // rbtnInner
            // 
            this.rbtnInner.Location = new System.Drawing.Point(30, 42);
            this.rbtnInner.Name = "rbtnInner";
            this.rbtnInner.Size = new System.Drawing.Size(179, 18);
            this.rbtnInner.TabIndex = 23;
            this.rbtnInner.TabStop = true;
            this.rbtnInner.Text = "内部环切";
            this.rbtnInner.UseVisualStyleBackColor = true;
            // 
            // rbtnOuter
            // 
            this.rbtnOuter.Location = new System.Drawing.Point(30, 63);
            this.rbtnOuter.Name = "rbtnOuter";
            this.rbtnOuter.Size = new System.Drawing.Size(179, 18);
            this.rbtnOuter.TabIndex = 23;
            this.rbtnOuter.TabStop = true;
            this.rbtnOuter.Text = "外部环切";
            this.rbtnOuter.UseVisualStyleBackColor = true;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnCancelOperator);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 324);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(410, 48);
            this.panelControl2.TabIndex = 26;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(322, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 27);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCancelOperator
            // 
            this.btnCancelOperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelOperator.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelOperator.Image")));
            this.btnCancelOperator.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancelOperator.Location = new System.Drawing.Point(21, 9);
            this.btnCancelOperator.Name = "btnCancelOperator";
            this.btnCancelOperator.Size = new System.Drawing.Size(99, 27);
            this.btnCancelOperator.TabIndex = 5;
            this.btnCancelOperator.Text = "取消环切";
            this.btnCancelOperator.Click += new System.EventHandler(this.btnCancelOperator_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnOK.Location = new System.Drawing.Point(224, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 27);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确  定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(21, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "尖角环切";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 47);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(276, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "设定参数生成尖角环切，帮助切割形成锋利的尖角。";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(410, 66);
            this.panelControl1.TabIndex = 25;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // FrmRingCut
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(410, 372);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRingCut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "尖角环切";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsScanline.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnCancelOperator;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.CheckEdit ckIsScanline;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private ControlLibrary.Common.UCNumberInputer txtOutsideSize;
        private ControlLibrary.Common.UCNumberInputer txtMinLen;
        private ControlLibrary.Common.UCNumberInputer txtMaxAngle;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.RadioButton rbtnAll;
        private System.Windows.Forms.RadioButton rbtnAuto;
        private System.Windows.Forms.RadioButton rbtnInner;
        private System.Windows.Forms.RadioButton rbtnOuter;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private System.Windows.Forms.Panel pnlImage;
    }
}