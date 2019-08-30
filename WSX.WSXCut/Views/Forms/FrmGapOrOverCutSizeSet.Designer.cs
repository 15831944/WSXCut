namespace WSX.WSXCut.Views.Forms
{
    partial class FrmGapOrOverCutSizeSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGapOrOverCutSizeSet));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.lblMainTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.lblSubTitle = new DevExpress.XtraEditors.LabelControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.lblUnit = new System.Windows.Forms.Label();
            this.txtSize = new WSX.ControlLibrary.Common.UCNumberInputer();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.lblDescription);
            this.panelControl1.Controls.Add(this.lblMainTitle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(307, 66);
            this.panelControl1.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(21, 47);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(168, 14);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "本功能用于设置缺口尺寸大小。";
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainTitle.Appearance.Options.UseFont = true;
            this.lblMainTitle.Location = new System.Drawing.Point(21, 13);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(120, 23);
            this.lblMainTitle.TabIndex = 0;
            this.lblMainTitle.Text = "缺口大小设置";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 152);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(307, 48);
            this.panelControl2.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(219, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 27);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnOK.Location = new System.Drawing.Point(121, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 27);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确  定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Location = new System.Drawing.Point(85, 101);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(60, 14);
            this.lblSubTitle.TabIndex = 10;
            this.lblSubTitle.Text = "缺口大小：";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(249, 99);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(27, 14);
            this.lblUnit.TabIndex = 12;
            this.lblUnit.Text = "mm";
            // 
            // txtSize
            // 
            this.txtSize.Format = null;
            this.txtSize.IsInterger = false;
            this.txtSize.Location = new System.Drawing.Point(149, 98);
            this.txtSize.Margin = new System.Windows.Forms.Padding(0);
            this.txtSize.Max = 100D;
            this.txtSize.Min = 0D;
            this.txtSize.Name = "txtSize";
            this.txtSize.Number = 0D;
            this.txtSize.ReadOnly = false;
            this.txtSize.Size = new System.Drawing.Size(97, 22);
            this.txtSize.Suffix = null;
            this.txtSize.TabIndex = 11;
            this.txtSize.TextSize = 9F;
            // 
            // FrmGapOrOverCutSizeSet
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(307, 200);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.lblSubTitle);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGapOrOverCutSizeSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "缺口大小设置";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.LabelControl lblMainTitle;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl lblSubTitle;
        private ControlLibrary.Common.UCNumberInputer txtSize;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private System.Windows.Forms.Label lblUnit;
    }
}