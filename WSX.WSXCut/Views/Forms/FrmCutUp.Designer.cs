namespace WSX.WSXCut.Views.Forms
{
    partial class FrmCutUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCutUp));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.ckDifferInsideOutside = new DevExpress.XtraEditors.CheckEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.txtSpacing = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnLineCount = new System.Windows.Forms.RadioButton();
            this.rbtnLineSpacing = new System.Windows.Forms.RadioButton();
            this.txtLongitudeCount = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtLatitudeCount = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckDifferInsideOutside.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.ckDifferInsideOutside);
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 187);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(434, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // ckDifferInsideOutside
            // 
            this.ckDifferInsideOutside.Location = new System.Drawing.Point(30, 16);
            this.ckDifferInsideOutside.Name = "ckDifferInsideOutside";
            this.ckDifferInsideOutside.Properties.AutoWidth = true;
            this.ckDifferInsideOutside.Properties.Caption = "先区分内外膜";
            this.ckDifferInsideOutside.Size = new System.Drawing.Size(94, 19);
            this.ckDifferInsideOutside.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(332, 10);
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
            this.btnOK.Location = new System.Drawing.Point(217, 10);
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
            this.panelControl1.Size = new System.Drawing.Size(434, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "该功能用于将需要掉落的零件进行切碎。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "切碎";
            // 
            // labelControl26
            // 
            this.labelControl26.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl26.Appearance.Options.UseImageAlign = true;
            this.labelControl26.Appearance.Options.UseTextOptions = true;
            this.labelControl26.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl26.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl26.Location = new System.Drawing.Point(200, 112);
            this.labelControl26.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(60, 14);
            this.labelControl26.TabIndex = 30;
            this.labelControl26.Text = "切碎间距：";
            // 
            // txtSpacing
            // 
            this.txtSpacing.Format = null;
            this.txtSpacing.IsInterger = false;
            this.txtSpacing.Location = new System.Drawing.Point(272, 110);
            this.txtSpacing.Margin = new System.Windows.Forms.Padding(0);
            this.txtSpacing.Max = 100D;
            this.txtSpacing.Min = 0D;
            this.txtSpacing.Name = "txtSpacing";
            this.txtSpacing.Number = 0D;
            this.txtSpacing.ReadOnly = false;
            this.txtSpacing.Size = new System.Drawing.Size(97, 22);
            this.txtSpacing.Suffix = null;
            this.txtSpacing.TabIndex = 31;
            this.txtSpacing.TextSize = 9F;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnLineCount);
            this.groupBox1.Controls.Add(this.rbtnLineSpacing);
            this.groupBox1.Location = new System.Drawing.Point(12, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 84);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "样式";
            // 
            // rbtnLineCount
            // 
            this.rbtnLineCount.AutoSize = true;
            this.rbtnLineCount.Location = new System.Drawing.Point(20, 51);
            this.rbtnLineCount.Name = "rbtnLineCount";
            this.rbtnLineCount.Size = new System.Drawing.Size(97, 18);
            this.rbtnLineCount.TabIndex = 0;
            this.rbtnLineCount.TabStop = true;
            this.rbtnLineCount.Text = "按切碎线数量";
            this.rbtnLineCount.UseVisualStyleBackColor = true;
            // 
            // rbtnLineSpacing
            // 
            this.rbtnLineSpacing.AutoSize = true;
            this.rbtnLineSpacing.Location = new System.Drawing.Point(20, 26);
            this.rbtnLineSpacing.Name = "rbtnLineSpacing";
            this.rbtnLineSpacing.Size = new System.Drawing.Size(97, 18);
            this.rbtnLineSpacing.TabIndex = 0;
            this.rbtnLineSpacing.TabStop = true;
            this.rbtnLineSpacing.Text = "按切碎线间距";
            this.rbtnLineSpacing.UseVisualStyleBackColor = true;
            this.rbtnLineSpacing.CheckedChanged += new System.EventHandler(this.rbtnLineSpacing_CheckedChanged);
            // 
            // txtLongitudeCount
            // 
            this.txtLongitudeCount.Format = null;
            this.txtLongitudeCount.IsInterger = false;
            this.txtLongitudeCount.Location = new System.Drawing.Point(272, 110);
            this.txtLongitudeCount.Margin = new System.Windows.Forms.Padding(0);
            this.txtLongitudeCount.Max = 100D;
            this.txtLongitudeCount.Min = 0D;
            this.txtLongitudeCount.Name = "txtLongitudeCount";
            this.txtLongitudeCount.Number = 0D;
            this.txtLongitudeCount.ReadOnly = false;
            this.txtLongitudeCount.Size = new System.Drawing.Size(97, 22);
            this.txtLongitudeCount.Suffix = null;
            this.txtLongitudeCount.TabIndex = 31;
            this.txtLongitudeCount.TextSize = 9F;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl3.Appearance.Options.UseImageAlign = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.Location = new System.Drawing.Point(200, 112);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "经线数量：";
            // 
            // txtLatitudeCount
            // 
            this.txtLatitudeCount.Format = null;
            this.txtLatitudeCount.IsInterger = false;
            this.txtLatitudeCount.Location = new System.Drawing.Point(272, 139);
            this.txtLatitudeCount.Margin = new System.Windows.Forms.Padding(0);
            this.txtLatitudeCount.Max = 100D;
            this.txtLatitudeCount.Min = 0D;
            this.txtLatitudeCount.Name = "txtLatitudeCount";
            this.txtLatitudeCount.Number = 0D;
            this.txtLatitudeCount.ReadOnly = false;
            this.txtLatitudeCount.Size = new System.Drawing.Size(97, 22);
            this.txtLatitudeCount.Suffix = null;
            this.txtLatitudeCount.TabIndex = 31;
            this.txtLatitudeCount.TextSize = 9F;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl4.Appearance.Options.UseImageAlign = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl4.Location = new System.Drawing.Point(200, 141);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 30;
            this.labelControl4.Text = "纬线数量：";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // FrmCutUp
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(434, 243);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl26);
            this.Controls.Add(this.txtLatitudeCount);
            this.Controls.Add(this.txtSpacing);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtLongitudeCount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCutUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "切碎";
            this.Load += new System.EventHandler(this.FrmCutUp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckDifferInsideOutside.Properties)).EndInit();
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
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private ControlLibrary.Common.UCNumberInputer txtSpacing;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnLineCount;
        private System.Windows.Forms.RadioButton rbtnLineSpacing;
        private ControlLibrary.Common.UCNumberInputer txtLongitudeCount;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private ControlLibrary.Common.UCNumberInputer txtLatitudeCount;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckEdit ckDifferInsideOutside;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}