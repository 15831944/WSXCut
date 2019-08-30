namespace WSX.WSXCut.Views.Forms
{
    partial class FrmMicroConnectParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMicroConnectParams));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkApplyingToSimilarGraphics = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtMicroConnLen = new WSX.ControlLibrary.Common.UCNumberInputer();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkApplyingToSimilarGraphics.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOk);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 180);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(313, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(211, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 31);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnOk.Location = new System.Drawing.Point(96, 10);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(89, 31);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "确  定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(313, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "本功能用于设置微连长度。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "微连参数";
            // 
            // chkApplyingToSimilarGraphics
            // 
            this.chkApplyingToSimilarGraphics.Location = new System.Drawing.Point(65, 135);
            this.chkApplyingToSimilarGraphics.Margin = new System.Windows.Forms.Padding(0);
            this.chkApplyingToSimilarGraphics.Name = "chkApplyingToSimilarGraphics";
            this.chkApplyingToSimilarGraphics.Properties.AutoWidth = true;
            this.chkApplyingToSimilarGraphics.Properties.Caption = "将微连应用于相似图形";
            this.chkApplyingToSimilarGraphics.Size = new System.Drawing.Size(142, 19);
            this.chkApplyingToSimilarGraphics.TabIndex = 30;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(65, 107);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "微连长度：";
            // 
            // txtMicroConnLen
            // 
            this.txtMicroConnLen.Format = null;
            this.txtMicroConnLen.IsInterger = false;
            this.txtMicroConnLen.Location = new System.Drawing.Point(128, 105);
            this.txtMicroConnLen.Margin = new System.Windows.Forms.Padding(0);
            this.txtMicroConnLen.Max = 100D;
            this.txtMicroConnLen.Min = 0.01D;
            this.txtMicroConnLen.Name = "txtMicroConnLen";
            this.txtMicroConnLen.Number = 5D;
            this.txtMicroConnLen.ReadOnly = false;
            this.txtMicroConnLen.Size = new System.Drawing.Size(95, 22);
            this.txtMicroConnLen.Suffix = "mm";
            this.txtMicroConnLen.TabIndex = 31;
            this.txtMicroConnLen.TextSize = 9F;
            // 
            // FrmMicroConnectParams
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(313, 236);
            this.Controls.Add(this.chkApplyingToSimilarGraphics);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtMicroConnLen);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMicroConnectParams";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微连参数";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkApplyingToSimilarGraphics.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chkApplyingToSimilarGraphics;
        private ControlLibrary.Common.UCNumberInputer txtMicroConnLen;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}