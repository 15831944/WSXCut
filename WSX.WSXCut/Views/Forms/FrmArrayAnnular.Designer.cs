namespace WSX.WSXCut.Views.Forms
{
    partial class FrmArrayAnnular
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmArrayAnnular));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.ckSetArrayCenterScope = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnByArrayScope = new System.Windows.Forms.RadioButton();
            this.rbtnByAngleSpacing = new System.Windows.Forms.RadioButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.txtAngleSpace = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtCenterStartAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtFigureCount = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtCenterCricleRadius = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtArrayScope = new WSX.ControlLibrary.Common.UCNumberInputer();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckSetArrayCenterScope.Properties)).BeginInit();
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
            this.panelControl2.Location = new System.Drawing.Point(0, 322);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(524, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(422, 10);
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
            this.btnOK.Location = new System.Drawing.Point(307, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(89, 31);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确  定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ckSetArrayCenterScope
            // 
            this.ckSetArrayCenterScope.Location = new System.Drawing.Point(40, 181);
            this.ckSetArrayCenterScope.Margin = new System.Windows.Forms.Padding(0);
            this.ckSetArrayCenterScope.Name = "ckSetArrayCenterScope";
            this.ckSetArrayCenterScope.Properties.AutoWidth = true;
            this.ckSetArrayCenterScope.Properties.Caption = "设置阵列中心范围";
            this.ckSetArrayCenterScope.Size = new System.Drawing.Size(118, 19);
            this.ckSetArrayCenterScope.TabIndex = 32;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(524, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "该功能用于将图形绕中心点旋转阵列。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "环形阵列";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnByArrayScope);
            this.groupBox1.Controls.Add(this.rbtnByAngleSpacing);
            this.groupBox1.Location = new System.Drawing.Point(33, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 84);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "阵列标准";
            // 
            // rbtnByArrayScope
            // 
            this.rbtnByArrayScope.AutoSize = true;
            this.rbtnByArrayScope.Location = new System.Drawing.Point(7, 51);
            this.rbtnByArrayScope.Name = "rbtnByArrayScope";
            this.rbtnByArrayScope.Size = new System.Drawing.Size(85, 18);
            this.rbtnByArrayScope.TabIndex = 0;
            this.rbtnByArrayScope.TabStop = true;
            this.rbtnByArrayScope.Text = "按阵列范围";
            this.rbtnByArrayScope.UseVisualStyleBackColor = true;
            // 
            // rbtnByAngleSpacing
            // 
            this.rbtnByAngleSpacing.AutoSize = true;
            this.rbtnByAngleSpacing.Location = new System.Drawing.Point(7, 27);
            this.rbtnByAngleSpacing.Name = "rbtnByAngleSpacing";
            this.rbtnByAngleSpacing.Size = new System.Drawing.Size(85, 18);
            this.rbtnByAngleSpacing.TabIndex = 0;
            this.rbtnByAngleSpacing.TabStop = true;
            this.rbtnByAngleSpacing.Text = "按角度间距";
            this.rbtnByAngleSpacing.UseVisualStyleBackColor = true;
            this.rbtnByAngleSpacing.CheckedChanged += new System.EventHandler(this.rbtnByAngleSpacing_CheckedChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(233, 94);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 33;
            this.labelControl3.Text = "阵列范围：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(233, 122);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 33;
            this.labelControl4.Text = "图形数量：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(36, 216);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(96, 14);
            this.labelControl5.TabIndex = 33;
            this.labelControl5.Text = "阵列中心圆半径：";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 244);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(120, 14);
            this.labelControl6.TabIndex = 33;
            this.labelControl6.Text = "图形相对中心起始角：";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(233, 94);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(60, 14);
            this.labelControl7.TabIndex = 35;
            this.labelControl7.Text = "角度间距：";
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(WSX.ViewModels.MainViewModel);
            // 
            // txtAngleSpace
            // 
            this.txtAngleSpace.Format = null;
            this.txtAngleSpace.IsInterger = false;
            this.txtAngleSpace.Location = new System.Drawing.Point(296, 92);
            this.txtAngleSpace.Margin = new System.Windows.Forms.Padding(0);
            this.txtAngleSpace.Max = 360D;
            this.txtAngleSpace.Min = -360D;
            this.txtAngleSpace.Name = "txtAngleSpace";
            this.txtAngleSpace.Number = 0D;
            this.txtAngleSpace.ReadOnly = false;
            this.txtAngleSpace.Size = new System.Drawing.Size(97, 22);
            this.txtAngleSpace.Suffix = null;
            this.txtAngleSpace.TabIndex = 34;
            this.txtAngleSpace.TextSize = 9F;
            // 
            // txtCenterStartAngle
            // 
            this.txtCenterStartAngle.Format = null;
            this.txtCenterStartAngle.IsInterger = false;
            this.txtCenterStartAngle.Location = new System.Drawing.Point(144, 243);
            this.txtCenterStartAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtCenterStartAngle.Max = 360D;
            this.txtCenterStartAngle.Min = -360D;
            this.txtCenterStartAngle.Name = "txtCenterStartAngle";
            this.txtCenterStartAngle.Number = 0D;
            this.txtCenterStartAngle.ReadOnly = false;
            this.txtCenterStartAngle.Size = new System.Drawing.Size(97, 22);
            this.txtCenterStartAngle.Suffix = null;
            this.txtCenterStartAngle.TabIndex = 31;
            this.txtCenterStartAngle.TextSize = 9F;
            // 
            // txtFigureCount
            // 
            this.txtFigureCount.Format = null;
            this.txtFigureCount.IsInterger = true;
            this.txtFigureCount.Location = new System.Drawing.Point(296, 121);
            this.txtFigureCount.Margin = new System.Windows.Forms.Padding(0);
            this.txtFigureCount.Max = 100000D;
            this.txtFigureCount.Min = 0D;
            this.txtFigureCount.Name = "txtFigureCount";
            this.txtFigureCount.Number = 0D;
            this.txtFigureCount.ReadOnly = false;
            this.txtFigureCount.Size = new System.Drawing.Size(97, 22);
            this.txtFigureCount.Suffix = null;
            this.txtFigureCount.TabIndex = 31;
            this.txtFigureCount.TextSize = 9F;
            // 
            // txtCenterCricleRadius
            // 
            this.txtCenterCricleRadius.Format = null;
            this.txtCenterCricleRadius.IsInterger = false;
            this.txtCenterCricleRadius.Location = new System.Drawing.Point(144, 214);
            this.txtCenterCricleRadius.Margin = new System.Windows.Forms.Padding(0);
            this.txtCenterCricleRadius.Max = 100000D;
            this.txtCenterCricleRadius.Min = 0D;
            this.txtCenterCricleRadius.Name = "txtCenterCricleRadius";
            this.txtCenterCricleRadius.Number = 0D;
            this.txtCenterCricleRadius.ReadOnly = false;
            this.txtCenterCricleRadius.Size = new System.Drawing.Size(97, 22);
            this.txtCenterCricleRadius.Suffix = null;
            this.txtCenterCricleRadius.TabIndex = 31;
            this.txtCenterCricleRadius.TextSize = 9F;
            // 
            // txtArrayScope
            // 
            this.txtArrayScope.Format = null;
            this.txtArrayScope.IsInterger = false;
            this.txtArrayScope.Location = new System.Drawing.Point(296, 92);
            this.txtArrayScope.Margin = new System.Windows.Forms.Padding(0);
            this.txtArrayScope.Max = 360D;
            this.txtArrayScope.Min = -360D;
            this.txtArrayScope.Name = "txtArrayScope";
            this.txtArrayScope.Number = 0D;
            this.txtArrayScope.ReadOnly = false;
            this.txtArrayScope.Size = new System.Drawing.Size(97, 22);
            this.txtArrayScope.Suffix = null;
            this.txtArrayScope.TabIndex = 31;
            this.txtArrayScope.TextSize = 9F;
            // 
            // FrmArrayAnnular
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(524, 378);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.txtAngleSpace);
            this.Controls.Add(this.ckSetArrayCenterScope);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtCenterStartAngle);
            this.Controls.Add(this.txtFigureCount);
            this.Controls.Add(this.txtCenterCricleRadius);
            this.Controls.Add(this.txtArrayScope);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmArrayAnnular";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "环形阵列";
            this.Load += new System.EventHandler(this.FrmArrayAnnular_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckSetArrayCenterScope.Properties)).EndInit();
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
        private ControlLibrary.Common.UCNumberInputer txtArrayScope;
        private DevExpress.XtraEditors.CheckEdit ckSetArrayCenterScope;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnByArrayScope;
        private System.Windows.Forms.RadioButton rbtnByAngleSpacing;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private ControlLibrary.Common.UCNumberInputer txtCenterCricleRadius;
        private ControlLibrary.Common.UCNumberInputer txtCenterStartAngle;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private ControlLibrary.Common.UCNumberInputer txtAngleSpace;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private ControlLibrary.Common.UCNumberInputer txtFigureCount;
    }
}