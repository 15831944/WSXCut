namespace WSX.WSXCut.Views.Forms
{
    partial class FrmMultiContourConnectCut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMultiContourConnectCut));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.ckBanModifyCutPath = new DevExpress.XtraEditors.CheckEdit();
            this.txtMaxConnectLength = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtMicroConnectLength = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckBanModifyCutPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 169);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(344, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(242, 10);
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
            this.btnOK.Location = new System.Drawing.Point(127, 10);
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
            this.panelControl1.Size = new System.Drawing.Size(344, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "该功能用于将零件轮廓相连，减少穿孔次数。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(100, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "多轮廓连切";
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(85, 91);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(60, 14);
            this.labelControl26.TabIndex = 30;
            this.labelControl26.Text = "微连长度：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(61, 116);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(84, 14);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "最大连切长度：";
            // 
            // ckBanModifyCutPath
            // 
            this.ckBanModifyCutPath.Location = new System.Drawing.Point(97, 145);
            this.ckBanModifyCutPath.Margin = new System.Windows.Forms.Padding(0);
            this.ckBanModifyCutPath.Name = "ckBanModifyCutPath";
            this.ckBanModifyCutPath.Properties.AutoWidth = true;
            this.ckBanModifyCutPath.Properties.Caption = "禁止修改切割路劲";
            this.ckBanModifyCutPath.Size = new System.Drawing.Size(118, 19);
            this.ckBanModifyCutPath.TabIndex = 32;
            // 
            // txtMaxConnectLength
            // 
            this.txtMaxConnectLength.Format = null;
            this.txtMaxConnectLength.IsInterger = false;
            this.txtMaxConnectLength.Location = new System.Drawing.Point(163, 114);
            this.txtMaxConnectLength.Margin = new System.Windows.Forms.Padding(0);
            this.txtMaxConnectLength.Max = 100D;
            this.txtMaxConnectLength.Min = 0D;
            this.txtMaxConnectLength.Name = "txtMaxConnectLength";
            this.txtMaxConnectLength.Number = 0D;
            this.txtMaxConnectLength.ReadOnly = false;
            this.txtMaxConnectLength.Size = new System.Drawing.Size(97, 22);
            this.txtMaxConnectLength.Suffix = null;
            this.txtMaxConnectLength.TabIndex = 31;
            this.txtMaxConnectLength.TextSize = 9F;
            // 
            // txtMicroConnectLength
            // 
            this.txtMicroConnectLength.Format = null;
            this.txtMicroConnectLength.IsInterger = false;
            this.txtMicroConnectLength.Location = new System.Drawing.Point(163, 89);
            this.txtMicroConnectLength.Margin = new System.Windows.Forms.Padding(0);
            this.txtMicroConnectLength.Max = 100D;
            this.txtMicroConnectLength.Min = 0D;
            this.txtMicroConnectLength.Name = "txtMicroConnectLength";
            this.txtMicroConnectLength.Number = 0D;
            this.txtMicroConnectLength.ReadOnly = false;
            this.txtMicroConnectLength.Size = new System.Drawing.Size(97, 22);
            this.txtMicroConnectLength.Suffix = null;
            this.txtMicroConnectLength.TabIndex = 31;
            this.txtMicroConnectLength.TextSize = 9F;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(WSX.ViewModels.MainViewModel);
            // 
            // FrmMultiContourConnectCut
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(344, 225);
            this.Controls.Add(this.ckBanModifyCutPath);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl26);
            this.Controls.Add(this.txtMaxConnectLength);
            this.Controls.Add(this.txtMicroConnectLength);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMultiContourConnectCut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "多轮廓连切";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckBanModifyCutPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private ControlLibrary.Common.UCNumberInputer txtMicroConnectLength;
        private ControlLibrary.Common.UCNumberInputer txtMaxConnectLength;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit ckBanModifyCutPath;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}