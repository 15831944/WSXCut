namespace WSX.WSXCut.Views.Forms
{
    partial class FrmBridging
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBridging));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtWidth = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtMaxDistance = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
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
            this.panelControl2.Size = new System.Drawing.Size(397, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(295, 10);
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
            this.btnOK.Location = new System.Drawing.Point(180, 10);
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
            this.panelControl1.Size = new System.Drawing.Size(397, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 43);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(307, 28);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "本功能可以将两个图形连接起来，多次使用可以实现对所有图形“一笔画”的效果。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "桥接";
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(40, 98);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(144, 14);
            this.labelControl26.TabIndex = 30;
            this.labelControl26.Text = "相邻曲线之间的最大距离：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(124, 123);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "桥接宽度：";
            // 
            // txtWidth
            // 
            this.txtWidth.Format = null;
            this.txtWidth.IsInterger = false;
            this.txtWidth.Location = new System.Drawing.Point(205, 121);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(0);
            this.txtWidth.Max = 99999D;
            this.txtWidth.Min = 0.01D;
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Number = 1D;
            this.txtWidth.ReadOnly = false;
            this.txtWidth.Size = new System.Drawing.Size(97, 22);
            this.txtWidth.Suffix = "mm";
            this.txtWidth.TabIndex = 31;
            this.txtWidth.TextSize = 9F;
            // 
            // txtMaxDistance
            // 
            this.txtMaxDistance.Format = null;
            this.txtMaxDistance.IsInterger = false;
            this.txtMaxDistance.Location = new System.Drawing.Point(205, 96);
            this.txtMaxDistance.Margin = new System.Windows.Forms.Padding(0);
            this.txtMaxDistance.Max = 99999D;
            this.txtMaxDistance.Min = 0.01D;
            this.txtMaxDistance.Name = "txtMaxDistance";
            this.txtMaxDistance.Number = 100D;
            this.txtMaxDistance.ReadOnly = false;
            this.txtMaxDistance.Size = new System.Drawing.Size(97, 22);
            this.txtMaxDistance.Suffix = "mm";
            this.txtMaxDistance.TabIndex = 31;
            this.txtMaxDistance.TextSize = 9F;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // FrmBridging
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(397, 225);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl26);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.txtMaxDistance);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBridging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "桥接";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
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
        private ControlLibrary.Common.UCNumberInputer txtMaxDistance;
        private ControlLibrary.Common.UCNumberInputer txtWidth;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}