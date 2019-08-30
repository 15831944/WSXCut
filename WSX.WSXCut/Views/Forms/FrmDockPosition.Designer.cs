namespace WSX.WSXCut.Views.Forms
{
    partial class FrmDockPosition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDockPosition));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.rbtnRelative = new System.Windows.Forms.RadioButton();
            this.rbtnAbsolute = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ckApplyToAllPlates = new DevExpress.XtraEditors.CheckEdit();
            this.ckExcludeUnprocessedFigure = new DevExpress.XtraEditors.CheckEdit();
            this.rbtnRightBottom = new System.Windows.Forms.RadioButton();
            this.rbtnRight = new System.Windows.Forms.RadioButton();
            this.rbtnRightTop = new System.Windows.Forms.RadioButton();
            this.rbtnBottom = new System.Windows.Forms.RadioButton();
            this.rbtnLeftBottom = new System.Windows.Forms.RadioButton();
            this.rbtnMiddle = new System.Windows.Forms.RadioButton();
            this.rbtnLeft = new System.Windows.Forms.RadioButton();
            this.rbtnTop = new System.Windows.Forms.RadioButton();
            this.rbtnLeftTop = new System.Windows.Forms.RadioButton();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckApplyToAllPlates.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckExcludeUnprocessedFigure.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 272);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(365, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(263, 10);
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
            this.btnOK.Location = new System.Drawing.Point(148, 10);
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
            this.panelControl1.Size = new System.Drawing.Size(365, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(300, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "本功能用于设置激光头相对于整个加工图形的停靠位置。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "停靠位置";
            // 
            // rbtnRelative
            // 
            this.rbtnRelative.AutoSize = true;
            this.rbtnRelative.Location = new System.Drawing.Point(24, 96);
            this.rbtnRelative.Name = "rbtnRelative";
            this.rbtnRelative.Size = new System.Drawing.Size(73, 18);
            this.rbtnRelative.TabIndex = 29;
            this.rbtnRelative.Text = "相对位置";
            this.rbtnRelative.UseVisualStyleBackColor = true;
            this.rbtnRelative.CheckedChanged += new System.EventHandler(this.rbtnRelative_CheckedChanged);
            // 
            // rbtnAbsolute
            // 
            this.rbtnAbsolute.AutoSize = true;
            this.rbtnAbsolute.Checked = true;
            this.rbtnAbsolute.Location = new System.Drawing.Point(24, 246);
            this.rbtnAbsolute.Name = "rbtnAbsolute";
            this.rbtnAbsolute.Size = new System.Drawing.Size(157, 18);
            this.rbtnAbsolute.TabIndex = 29;
            this.rbtnAbsolute.TabStop = true;
            this.rbtnAbsolute.Text = "绝对位置，通过鼠标指定";
            this.rbtnAbsolute.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckApplyToAllPlates);
            this.panel1.Controls.Add(this.ckExcludeUnprocessedFigure);
            this.panel1.Controls.Add(this.rbtnRightBottom);
            this.panel1.Controls.Add(this.rbtnRight);
            this.panel1.Controls.Add(this.rbtnRightTop);
            this.panel1.Controls.Add(this.rbtnBottom);
            this.panel1.Controls.Add(this.rbtnLeftBottom);
            this.panel1.Controls.Add(this.rbtnMiddle);
            this.panel1.Controls.Add(this.rbtnLeft);
            this.panel1.Controls.Add(this.rbtnTop);
            this.panel1.Controls.Add(this.rbtnLeftTop);
            this.panel1.Location = new System.Drawing.Point(43, 121);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 119);
            this.panel1.TabIndex = 30;
            // 
            // ckApplyToAllPlates
            // 
            this.ckApplyToAllPlates.Location = new System.Drawing.Point(15, 96);
            this.ckApplyToAllPlates.Margin = new System.Windows.Forms.Padding(0);
            this.ckApplyToAllPlates.Name = "ckApplyToAllPlates";
            this.ckApplyToAllPlates.Properties.AutoWidth = true;
            this.ckApplyToAllPlates.Properties.Caption = "应用于所有已排样板材";
            this.ckApplyToAllPlates.Size = new System.Drawing.Size(142, 19);
            this.ckApplyToAllPlates.TabIndex = 30;
            // 
            // ckExcludeUnprocessedFigure
            // 
            this.ckExcludeUnprocessedFigure.Location = new System.Drawing.Point(15, 75);
            this.ckExcludeUnprocessedFigure.Margin = new System.Windows.Forms.Padding(0);
            this.ckExcludeUnprocessedFigure.Name = "ckExcludeUnprocessedFigure";
            this.ckExcludeUnprocessedFigure.Properties.AutoWidth = true;
            this.ckExcludeUnprocessedFigure.Properties.Caption = "排除不加工图形";
            this.ckExcludeUnprocessedFigure.Size = new System.Drawing.Size(106, 19);
            this.ckExcludeUnprocessedFigure.TabIndex = 30;
            // 
            // rbtnRightBottom
            // 
            this.rbtnRightBottom.AutoSize = true;
            this.rbtnRightBottom.Location = new System.Drawing.Point(184, 51);
            this.rbtnRightBottom.Name = "rbtnRightBottom";
            this.rbtnRightBottom.Size = new System.Drawing.Size(49, 18);
            this.rbtnRightBottom.TabIndex = 29;
            this.rbtnRightBottom.Text = "右下";
            this.rbtnRightBottom.UseVisualStyleBackColor = true;
            // 
            // rbtnRight
            // 
            this.rbtnRight.AutoSize = true;
            this.rbtnRight.Location = new System.Drawing.Point(184, 27);
            this.rbtnRight.Name = "rbtnRight";
            this.rbtnRight.Size = new System.Drawing.Size(37, 18);
            this.rbtnRight.TabIndex = 29;
            this.rbtnRight.Text = "右";
            this.rbtnRight.UseVisualStyleBackColor = true;
            // 
            // rbtnRightTop
            // 
            this.rbtnRightTop.AutoSize = true;
            this.rbtnRightTop.Location = new System.Drawing.Point(184, 3);
            this.rbtnRightTop.Name = "rbtnRightTop";
            this.rbtnRightTop.Size = new System.Drawing.Size(49, 18);
            this.rbtnRightTop.TabIndex = 29;
            this.rbtnRightTop.Text = "右上";
            this.rbtnRightTop.UseVisualStyleBackColor = true;
            // 
            // rbtnBottom
            // 
            this.rbtnBottom.AutoSize = true;
            this.rbtnBottom.Location = new System.Drawing.Point(105, 51);
            this.rbtnBottom.Name = "rbtnBottom";
            this.rbtnBottom.Size = new System.Drawing.Size(37, 18);
            this.rbtnBottom.TabIndex = 29;
            this.rbtnBottom.Text = "下";
            this.rbtnBottom.UseVisualStyleBackColor = true;
            // 
            // rbtnLeftBottom
            // 
            this.rbtnLeftBottom.AutoSize = true;
            this.rbtnLeftBottom.Location = new System.Drawing.Point(15, 51);
            this.rbtnLeftBottom.Name = "rbtnLeftBottom";
            this.rbtnLeftBottom.Size = new System.Drawing.Size(49, 18);
            this.rbtnLeftBottom.TabIndex = 29;
            this.rbtnLeftBottom.Text = "左下";
            this.rbtnLeftBottom.UseVisualStyleBackColor = true;
            // 
            // rbtnMiddle
            // 
            this.rbtnMiddle.AutoSize = true;
            this.rbtnMiddle.Location = new System.Drawing.Point(105, 27);
            this.rbtnMiddle.Name = "rbtnMiddle";
            this.rbtnMiddle.Size = new System.Drawing.Size(37, 18);
            this.rbtnMiddle.TabIndex = 29;
            this.rbtnMiddle.Text = "中";
            this.rbtnMiddle.UseVisualStyleBackColor = true;
            // 
            // rbtnLeft
            // 
            this.rbtnLeft.AutoSize = true;
            this.rbtnLeft.Location = new System.Drawing.Point(15, 27);
            this.rbtnLeft.Name = "rbtnLeft";
            this.rbtnLeft.Size = new System.Drawing.Size(37, 18);
            this.rbtnLeft.TabIndex = 29;
            this.rbtnLeft.Text = "左";
            this.rbtnLeft.UseVisualStyleBackColor = true;
            // 
            // rbtnTop
            // 
            this.rbtnTop.AutoSize = true;
            this.rbtnTop.Location = new System.Drawing.Point(105, 3);
            this.rbtnTop.Name = "rbtnTop";
            this.rbtnTop.Size = new System.Drawing.Size(37, 18);
            this.rbtnTop.TabIndex = 29;
            this.rbtnTop.Text = "上";
            this.rbtnTop.UseVisualStyleBackColor = true;
            // 
            // rbtnLeftTop
            // 
            this.rbtnLeftTop.AutoSize = true;
            this.rbtnLeftTop.Checked = true;
            this.rbtnLeftTop.Location = new System.Drawing.Point(15, 3);
            this.rbtnLeftTop.Name = "rbtnLeftTop";
            this.rbtnLeftTop.Size = new System.Drawing.Size(49, 18);
            this.rbtnLeftTop.TabIndex = 29;
            this.rbtnLeftTop.TabStop = true;
            this.rbtnLeftTop.Text = "左上";
            this.rbtnLeftTop.UseVisualStyleBackColor = true;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // FrmDockPosition
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(365, 328);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rbtnAbsolute);
            this.Controls.Add(this.rbtnRelative);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDockPosition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "停靠位置";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckApplyToAllPlates.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckExcludeUnprocessedFigure.Properties)).EndInit();
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
        private System.Windows.Forms.RadioButton rbtnRelative;
        private System.Windows.Forms.RadioButton rbtnAbsolute;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtnRightBottom;
        private System.Windows.Forms.RadioButton rbtnRight;
        private System.Windows.Forms.RadioButton rbtnRightTop;
        private System.Windows.Forms.RadioButton rbtnBottom;
        private System.Windows.Forms.RadioButton rbtnLeftBottom;
        private System.Windows.Forms.RadioButton rbtnMiddle;
        private System.Windows.Forms.RadioButton rbtnLeft;
        private System.Windows.Forms.RadioButton rbtnTop;
        private System.Windows.Forms.RadioButton rbtnLeftTop;
        private DevExpress.XtraEditors.CheckEdit ckApplyToAllPlates;
        private DevExpress.XtraEditors.CheckEdit ckExcludeUnprocessedFigure;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}