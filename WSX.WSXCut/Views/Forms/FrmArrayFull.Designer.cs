namespace WSX.WSXCut.Views.Forms
{
    partial class FrmArrayFull
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmArrayFull));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.ckClearOriginalCompleted = new DevExpress.XtraEditors.CheckEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPlateWidth = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtPlateHeight = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.ckAutoCombination = new DevExpress.XtraEditors.CheckEdit();
            this.ckAutoCommonEdge = new DevExpress.XtraEditors.CheckEdit();
            this.ckBanRotation = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPartsSpacing = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtPlateRetainEdge = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckClearOriginalCompleted.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckAutoCombination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckAutoCommonEdge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBanRotation.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.ckClearOriginalCompleted);
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 297);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(467, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // ckClearOriginalCompleted
            // 
            this.ckClearOriginalCompleted.Location = new System.Drawing.Point(41, 17);
            this.ckClearOriginalCompleted.Margin = new System.Windows.Forms.Padding(0);
            this.ckClearOriginalCompleted.Name = "ckClearOriginalCompleted";
            this.ckClearOriginalCompleted.Properties.AutoWidth = true;
            this.ckClearOriginalCompleted.Properties.Caption = "完成后删除原图";
            this.ckClearOriginalCompleted.Size = new System.Drawing.Size(106, 19);
            this.ckClearOriginalCompleted.TabIndex = 32;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(365, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 31);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnOK.Location = new System.Drawing.Point(250, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(89, 31);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确  定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(467, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "本功能按照给定的零件和板材进行快速的布满排样。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "布满排样";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPlateWidth);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.txtPlateHeight);
            this.groupBox1.Location = new System.Drawing.Point(24, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(430, 66);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "板材大小";
            // 
            // txtPlateWidth
            // 
            this.txtPlateWidth.Format = null;
            this.txtPlateWidth.IsInterger = false;
            this.txtPlateWidth.Location = new System.Drawing.Point(81, 31);
            this.txtPlateWidth.Margin = new System.Windows.Forms.Padding(0);
            this.txtPlateWidth.Max = 100D;
            this.txtPlateWidth.Min = 0D;
            this.txtPlateWidth.Name = "txtPlateWidth";
            this.txtPlateWidth.Number = 0D;
            this.txtPlateWidth.ReadOnly = false;
            this.txtPlateWidth.Size = new System.Drawing.Size(97, 22);
            this.txtPlateWidth.Suffix = null;
            this.txtPlateWidth.TabIndex = 31;
            this.txtPlateWidth.TextSize = 9F;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(17, 32);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 33;
            this.labelControl3.Text = "板材宽度：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(211, 32);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 33;
            this.labelControl4.Text = "板材长度：";
            // 
            // txtPlateHeight
            // 
            this.txtPlateHeight.Format = null;
            this.txtPlateHeight.IsInterger = false;
            this.txtPlateHeight.Location = new System.Drawing.Point(275, 31);
            this.txtPlateHeight.Margin = new System.Windows.Forms.Padding(0);
            this.txtPlateHeight.Max = 100D;
            this.txtPlateHeight.Min = 0D;
            this.txtPlateHeight.Name = "txtPlateHeight";
            this.txtPlateHeight.Number = 0D;
            this.txtPlateHeight.ReadOnly = false;
            this.txtPlateHeight.Size = new System.Drawing.Size(97, 22);
            this.txtPlateHeight.Suffix = null;
            this.txtPlateHeight.TabIndex = 31;
            this.txtPlateHeight.TextSize = 9F;
            // 
            // ckAutoCombination
            // 
            this.ckAutoCombination.Location = new System.Drawing.Point(17, 63);
            this.ckAutoCombination.Margin = new System.Windows.Forms.Padding(0);
            this.ckAutoCombination.Name = "ckAutoCombination";
            this.ckAutoCombination.Properties.AutoWidth = true;
            this.ckAutoCombination.Properties.Caption = "自动组合";
            this.ckAutoCombination.Size = new System.Drawing.Size(70, 19);
            this.ckAutoCombination.TabIndex = 32;
            // 
            // ckAutoCommonEdge
            // 
            this.ckAutoCommonEdge.Location = new System.Drawing.Point(17, 88);
            this.ckAutoCommonEdge.Margin = new System.Windows.Forms.Padding(0);
            this.ckAutoCommonEdge.Name = "ckAutoCommonEdge";
            this.ckAutoCommonEdge.Properties.AutoWidth = true;
            this.ckAutoCommonEdge.Properties.Caption = "自动共边";
            this.ckAutoCommonEdge.Size = new System.Drawing.Size(70, 19);
            this.ckAutoCommonEdge.TabIndex = 32;
            // 
            // ckBanRotation
            // 
            this.ckBanRotation.Location = new System.Drawing.Point(211, 63);
            this.ckBanRotation.Margin = new System.Windows.Forms.Padding(0);
            this.ckBanRotation.Name = "ckBanRotation";
            this.ckBanRotation.Properties.AutoWidth = true;
            this.ckBanRotation.Properties.Caption = "禁止旋转";
            this.ckBanRotation.Size = new System.Drawing.Size(70, 19);
            this.ckBanRotation.TabIndex = 32;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(17, 33);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 33;
            this.labelControl5.Text = "零件间距：";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(211, 34);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.TabIndex = 33;
            this.labelControl6.Text = "板材留边：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPartsSpacing);
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Controls.Add(this.ckAutoCombination);
            this.groupBox2.Controls.Add(this.labelControl6);
            this.groupBox2.Controls.Add(this.ckAutoCommonEdge);
            this.groupBox2.Controls.Add(this.ckBanRotation);
            this.groupBox2.Controls.Add(this.txtPlateRetainEdge);
            this.groupBox2.Location = new System.Drawing.Point(24, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(430, 124);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "排样参数";
            // 
            // txtPartsSpacing
            // 
            this.txtPartsSpacing.Format = null;
            this.txtPartsSpacing.IsInterger = false;
            this.txtPartsSpacing.Location = new System.Drawing.Point(81, 32);
            this.txtPartsSpacing.Margin = new System.Windows.Forms.Padding(0);
            this.txtPartsSpacing.Max = 100D;
            this.txtPartsSpacing.Min = 0D;
            this.txtPartsSpacing.Name = "txtPartsSpacing";
            this.txtPartsSpacing.Number = 0D;
            this.txtPartsSpacing.ReadOnly = false;
            this.txtPartsSpacing.Size = new System.Drawing.Size(97, 22);
            this.txtPartsSpacing.Suffix = null;
            this.txtPartsSpacing.TabIndex = 31;
            this.txtPartsSpacing.TextSize = 9F;
            // 
            // txtPlateRetainEdge
            // 
            this.txtPlateRetainEdge.Format = null;
            this.txtPlateRetainEdge.IsInterger = false;
            this.txtPlateRetainEdge.Location = new System.Drawing.Point(275, 33);
            this.txtPlateRetainEdge.Margin = new System.Windows.Forms.Padding(0);
            this.txtPlateRetainEdge.Max = 100D;
            this.txtPlateRetainEdge.Min = 0D;
            this.txtPlateRetainEdge.Name = "txtPlateRetainEdge";
            this.txtPlateRetainEdge.Number = 0D;
            this.txtPlateRetainEdge.ReadOnly = false;
            this.txtPlateRetainEdge.Size = new System.Drawing.Size(97, 22);
            this.txtPlateRetainEdge.Suffix = null;
            this.txtPlateRetainEdge.TabIndex = 31;
            this.txtPlateRetainEdge.TextSize = 9F;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(WSX.ViewModels.MainViewModel);
            // 
            // FrmArrayFull
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(467, 353);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmArrayFull";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "布满排样";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckClearOriginalCompleted.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckAutoCombination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckAutoCommonEdge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckBanRotation.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private ControlLibrary.Common.UCNumberInputer txtPlateWidth;
        private ControlLibrary.Common.UCNumberInputer txtPlateHeight;
        private DevExpress.XtraEditors.CheckEdit ckClearOriginalCompleted;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckEdit ckAutoCombination;
        private DevExpress.XtraEditors.CheckEdit ckAutoCommonEdge;
        private DevExpress.XtraEditors.CheckEdit ckBanRotation;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private ControlLibrary.Common.UCNumberInputer txtPartsSpacing;
        private ControlLibrary.Common.UCNumberInputer txtPlateRetainEdge;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}