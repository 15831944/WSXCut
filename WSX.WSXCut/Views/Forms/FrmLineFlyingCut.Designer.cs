namespace WSX.WSXCut.Views.Forms
{
    partial class FrmLineFlyingCut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLineFlyingCut));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnRightBottom = new System.Windows.Forms.RadioButton();
            this.rbtnLeftBottom = new System.Windows.Forms.RadioButton();
            this.rbtnRightTop = new System.Windows.Forms.RadioButton();
            this.rbtnLeftTop = new System.Windows.Forms.RadioButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.txtMaxConnectDistance = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtDistanceDeviation = new WSX.ControlLibrary.Common.UCNumberInputer();
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
            this.panelControl2.Location = new System.Drawing.Point(0, 295);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(400, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(298, 10);
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
            this.btnOK.Location = new System.Drawing.Point(183, 10);
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
            this.panelControl1.Size = new System.Drawing.Size(400, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "本功能实现对规则的阵列图形进行飞行切割。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(120, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "直线飞行切割";
            // 
            // labelControl26
            // 
            this.labelControl26.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl26.Appearance.Options.UseImageAlign = true;
            this.labelControl26.Appearance.Options.UseTextOptions = true;
            this.labelControl26.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl26.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl26.Location = new System.Drawing.Point(63, 205);
            this.labelControl26.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(84, 14);
            this.labelControl26.TabIndex = 30;
            this.labelControl26.Text = "允许距离偏差：";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl3.Appearance.Options.UseImageAlign = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.Location = new System.Drawing.Point(39, 237);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(108, 14);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "光滑连接最大距离：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnRightBottom);
            this.groupBox1.Controls.Add(this.rbtnLeftBottom);
            this.groupBox1.Controls.Add(this.rbtnRightTop);
            this.groupBox1.Controls.Add(this.rbtnLeftTop);
            this.groupBox1.Location = new System.Drawing.Point(24, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 78);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "起刀位置";
            // 
            // rbtnRightBottom
            // 
            this.rbtnRightBottom.AutoSize = true;
            this.rbtnRightBottom.Location = new System.Drawing.Point(206, 45);
            this.rbtnRightBottom.Name = "rbtnRightBottom";
            this.rbtnRightBottom.Size = new System.Drawing.Size(49, 18);
            this.rbtnRightBottom.TabIndex = 0;
            this.rbtnRightBottom.TabStop = true;
            this.rbtnRightBottom.Text = "右下";
            this.rbtnRightBottom.UseVisualStyleBackColor = true;
            // 
            // rbtnLeftBottom
            // 
            this.rbtnLeftBottom.AutoSize = true;
            this.rbtnLeftBottom.Location = new System.Drawing.Point(61, 46);
            this.rbtnLeftBottom.Name = "rbtnLeftBottom";
            this.rbtnLeftBottom.Size = new System.Drawing.Size(49, 18);
            this.rbtnLeftBottom.TabIndex = 0;
            this.rbtnLeftBottom.TabStop = true;
            this.rbtnLeftBottom.Text = "左下";
            this.rbtnLeftBottom.UseVisualStyleBackColor = true;
            // 
            // rbtnRightTop
            // 
            this.rbtnRightTop.AutoSize = true;
            this.rbtnRightTop.Location = new System.Drawing.Point(206, 21);
            this.rbtnRightTop.Name = "rbtnRightTop";
            this.rbtnRightTop.Size = new System.Drawing.Size(49, 18);
            this.rbtnRightTop.TabIndex = 0;
            this.rbtnRightTop.TabStop = true;
            this.rbtnRightTop.Text = "右上";
            this.rbtnRightTop.UseVisualStyleBackColor = true;
            // 
            // rbtnLeftTop
            // 
            this.rbtnLeftTop.AutoSize = true;
            this.rbtnLeftTop.Location = new System.Drawing.Point(61, 22);
            this.rbtnLeftTop.Name = "rbtnLeftTop";
            this.rbtnLeftTop.Size = new System.Drawing.Size(49, 18);
            this.rbtnLeftTop.TabIndex = 0;
            this.rbtnLeftTop.TabStop = true;
            this.rbtnLeftTop.Text = "左上";
            this.rbtnLeftTop.UseVisualStyleBackColor = true;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl4.Appearance.Image")));
            this.labelControl4.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl4.Appearance.Options.UseImage = true;
            this.labelControl4.Appearance.Options.UseImageAlign = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.labelControl4.Location = new System.Drawing.Point(265, 197);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(32, 32);
            this.labelControl4.TabIndex = 30;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl5.Appearance.Image")));
            this.labelControl5.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl5.Appearance.Options.UseImage = true;
            this.labelControl5.Appearance.Options.UseImageAlign = true;
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.labelControl5.Location = new System.Drawing.Point(265, 232);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(32, 32);
            this.labelControl5.TabIndex = 30;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // txtMaxConnectDistance
            // 
            this.txtMaxConnectDistance.Format = null;
            this.txtMaxConnectDistance.IsInterger = false;
            this.txtMaxConnectDistance.Location = new System.Drawing.Point(165, 237);
            this.txtMaxConnectDistance.Margin = new System.Windows.Forms.Padding(0);
            this.txtMaxConnectDistance.Max = 100D;
            this.txtMaxConnectDistance.Min = 0D;
            this.txtMaxConnectDistance.Name = "txtMaxConnectDistance";
            this.txtMaxConnectDistance.Number = 0D;
            this.txtMaxConnectDistance.ReadOnly = false;
            this.txtMaxConnectDistance.Size = new System.Drawing.Size(97, 22);
            this.txtMaxConnectDistance.Suffix = null;
            this.txtMaxConnectDistance.TabIndex = 31;
            this.txtMaxConnectDistance.TextSize = 9F;
            // 
            // txtDistanceDeviation
            // 
            this.txtDistanceDeviation.Format = null;
            this.txtDistanceDeviation.IsInterger = false;
            this.txtDistanceDeviation.Location = new System.Drawing.Point(165, 203);
            this.txtDistanceDeviation.Margin = new System.Windows.Forms.Padding(0);
            this.txtDistanceDeviation.Max = 100D;
            this.txtDistanceDeviation.Min = 0D;
            this.txtDistanceDeviation.Name = "txtDistanceDeviation";
            this.txtDistanceDeviation.Number = 0D;
            this.txtDistanceDeviation.ReadOnly = false;
            this.txtDistanceDeviation.Size = new System.Drawing.Size(97, 22);
            this.txtDistanceDeviation.Suffix = null;
            this.txtDistanceDeviation.TabIndex = 31;
            this.txtDistanceDeviation.TextSize = 9F;
            // 
            // FrmLineFlyingCut
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 351);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl26);
            this.Controls.Add(this.txtMaxConnectDistance);
            this.Controls.Add(this.txtDistanceDeviation);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLineFlyingCut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "飞行切割";
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
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private ControlLibrary.Common.UCNumberInputer txtDistanceDeviation;
        private ControlLibrary.Common.UCNumberInputer txtMaxConnectDistance;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnRightBottom;
        private System.Windows.Forms.RadioButton rbtnLeftBottom;
        private System.Windows.Forms.RadioButton rbtnRightTop;
        private System.Windows.Forms.RadioButton rbtnLeftTop;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}