namespace WSX.WSXCut.Views.Forms
{
    partial class FrmArrayInteractive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmArrayInteractive));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.ckClearOriginalCompleted = new DevExpress.XtraEditors.CheckEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtColumnSpacing = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtRowSpacing = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckClearOriginalCompleted.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
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
            this.panelControl2.Location = new System.Drawing.Point(0, 170);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(407, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // ckClearOriginalCompleted
            // 
            this.ckClearOriginalCompleted.Location = new System.Drawing.Point(18, 17);
            this.ckClearOriginalCompleted.Margin = new System.Windows.Forms.Padding(0);
            this.ckClearOriginalCompleted.Name = "ckClearOriginalCompleted";
            this.ckClearOriginalCompleted.Properties.AutoWidth = true;
            this.ckClearOriginalCompleted.Properties.Caption = "阵列后删除原图";
            this.ckClearOriginalCompleted.Size = new System.Drawing.Size(106, 19);
            this.ckClearOriginalCompleted.TabIndex = 32;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(305, 10);
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
            this.btnOK.Location = new System.Drawing.Point(190, 10);
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
            this.panelControl1.Size = new System.Drawing.Size(407, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "通过鼠标拖动划定区域，对选中图形进行快速阵列复制";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "阵列";
            // 
            // labelControl26
            // 
            this.labelControl26.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl26.Appearance.Image")));
            this.labelControl26.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl26.Appearance.Options.UseImage = true;
            this.labelControl26.Appearance.Options.UseImageAlign = true;
            this.labelControl26.Appearance.Options.UseTextOptions = true;
            this.labelControl26.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl26.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl26.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl26.Location = new System.Drawing.Point(113, 97);
            this.labelControl26.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(71, 20);
            this.labelControl26.TabIndex = 30;
            this.labelControl26.Text = "行间距：";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl3.Appearance.Image")));
            this.labelControl3.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl3.Appearance.Options.UseImage = true;
            this.labelControl3.Appearance.Options.UseImageAlign = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(113, 124);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(71, 20);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "列间距：";
            // 
            // txtColumnSpacing
            // 
            this.txtColumnSpacing.Format = null;
            this.txtColumnSpacing.IsInterger = false;
            this.txtColumnSpacing.Location = new System.Drawing.Point(197, 127);
            this.txtColumnSpacing.Margin = new System.Windows.Forms.Padding(0);
            this.txtColumnSpacing.Max = 100D;
            this.txtColumnSpacing.Min = 0D;
            this.txtColumnSpacing.Name = "txtColumnSpacing";
            this.txtColumnSpacing.Number = 0D;
            this.txtColumnSpacing.ReadOnly = false;
            this.txtColumnSpacing.Size = new System.Drawing.Size(97, 22);
            this.txtColumnSpacing.Suffix = null;
            this.txtColumnSpacing.TabIndex = 31;
            this.txtColumnSpacing.TextSize = 9F;
            // 
            // txtRowSpacing
            // 
            this.txtRowSpacing.Format = null;
            this.txtRowSpacing.IsInterger = false;
            this.txtRowSpacing.Location = new System.Drawing.Point(197, 98);
            this.txtRowSpacing.Margin = new System.Windows.Forms.Padding(0);
            this.txtRowSpacing.Max = 100D;
            this.txtRowSpacing.Min = 0D;
            this.txtRowSpacing.Name = "txtRowSpacing";
            this.txtRowSpacing.Number = 0D;
            this.txtRowSpacing.ReadOnly = false;
            this.txtRowSpacing.Size = new System.Drawing.Size(97, 22);
            this.txtRowSpacing.Suffix = null;
            this.txtRowSpacing.TabIndex = 31;
            this.txtRowSpacing.TextSize = 9F;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(WSX.ViewModels.MainViewModel);
            // 
            // FrmArrayInteractive
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(407, 226);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl26);
            this.Controls.Add(this.txtColumnSpacing);
            this.Controls.Add(this.txtRowSpacing);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmArrayInteractive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "交互式阵列";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckClearOriginalCompleted.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
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
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private ControlLibrary.Common.UCNumberInputer txtRowSpacing;
        private ControlLibrary.Common.UCNumberInputer txtColumnSpacing;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit ckClearOriginalCompleted;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}