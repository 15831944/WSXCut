namespace WSX.WSXCut.Views.Forms
{
    partial class FrmConnectKnife
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConnectKnife));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnY = new System.Windows.Forms.RadioButton();
            this.rbtnX = new System.Windows.Forms.RadioButton();
            this.txtFormatLength = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDriveDistance = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 196);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(413, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(311, 10);
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
            this.btnOK.Location = new System.Drawing.Point(196, 10);
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
            this.panelControl1.Size = new System.Drawing.Size(413, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "该功能用于将图纸分成合适机床加工的两部分。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "接刀";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnY);
            this.groupBox1.Controls.Add(this.rbtnX);
            this.groupBox1.Location = new System.Drawing.Point(221, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 69);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分离方向";
            // 
            // rbtnY
            // 
            this.rbtnY.AutoSize = true;
            this.rbtnY.Location = new System.Drawing.Point(97, 33);
            this.rbtnY.Name = "rbtnY";
            this.rbtnY.Size = new System.Drawing.Size(33, 18);
            this.rbtnY.TabIndex = 0;
            this.rbtnY.TabStop = true;
            this.rbtnY.Text = "Y";
            this.rbtnY.UseVisualStyleBackColor = true;
            // 
            // rbtnX
            // 
            this.rbtnX.AutoSize = true;
            this.rbtnX.Location = new System.Drawing.Point(32, 33);
            this.rbtnX.Name = "rbtnX";
            this.rbtnX.Size = new System.Drawing.Size(32, 18);
            this.rbtnX.TabIndex = 0;
            this.rbtnX.TabStop = true;
            this.rbtnX.Text = "X";
            this.rbtnX.UseVisualStyleBackColor = true;
            // 
            // txtFormatLength
            // 
            this.txtFormatLength.Format = null;
            this.txtFormatLength.IsInterger = false;
            this.txtFormatLength.Location = new System.Drawing.Point(100, 117);
            this.txtFormatLength.Margin = new System.Windows.Forms.Padding(0);
            this.txtFormatLength.Max = 100D;
            this.txtFormatLength.Min = 0D;
            this.txtFormatLength.Name = "txtFormatLength";
            this.txtFormatLength.Number = 0D;
            this.txtFormatLength.ReadOnly = false;
            this.txtFormatLength.Size = new System.Drawing.Size(97, 22);
            this.txtFormatLength.Suffix = null;
            this.txtFormatLength.TabIndex = 31;
            this.txtFormatLength.TextSize = 9F;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl3.Appearance.Options.UseImageAlign = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.Location = new System.Drawing.Point(28, 119);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "幅面长度：";
            // 
            // txtDriveDistance
            // 
            this.txtDriveDistance.Format = null;
            this.txtDriveDistance.IsInterger = false;
            this.txtDriveDistance.Location = new System.Drawing.Point(100, 146);
            this.txtDriveDistance.Margin = new System.Windows.Forms.Padding(0);
            this.txtDriveDistance.Max = 100D;
            this.txtDriveDistance.Min = 0D;
            this.txtDriveDistance.Name = "txtDriveDistance";
            this.txtDriveDistance.Number = 0D;
            this.txtDriveDistance.ReadOnly = false;
            this.txtDriveDistance.Size = new System.Drawing.Size(97, 22);
            this.txtDriveDistance.Suffix = null;
            this.txtDriveDistance.TabIndex = 31;
            this.txtDriveDistance.TextSize = 9F;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl4.Appearance.Options.UseImageAlign = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl4.Location = new System.Drawing.Point(28, 148);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 30;
            this.labelControl4.Text = "推动距离：";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // FrmConnectKnife
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(413, 252);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtDriveDistance);
            this.Controls.Add(this.txtFormatLength);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConnectKnife";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "接刀";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnY;
        private System.Windows.Forms.RadioButton rbtnX;
        private ControlLibrary.Common.UCNumberInputer txtFormatLength;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private ControlLibrary.Common.UCNumberInputer txtDriveDistance;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}