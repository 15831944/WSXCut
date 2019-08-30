namespace WSX.WSXCut.Views.Forms
{
    partial class FrmCompensation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCompensation));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelOperator = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtnAllOuter = new System.Windows.Forms.RadioButton();
            this.rbtnAllInner = new System.Windows.Forms.RadioButton();
            this.rbtnAuto = new System.Windows.Forms.RadioButton();
            this.ckCompensationNotClosedFigure = new DevExpress.XtraEditors.CheckEdit();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.rbtnFillet = new System.Windows.Forms.RadioButton();
            this.rbtnRightAngle = new System.Windows.Forms.RadioButton();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtInSideWidth = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckCompensationNotClosedFigure.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(474, 66);
            this.panelControl1.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 47);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(204, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "为图形生成指定参数的割缝补偿刀路。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(21, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "割缝补偿";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnCancelOperator);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 321);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(474, 48);
            this.panelControl2.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(386, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 27);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCancelOperator
            // 
            this.btnCancelOperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelOperator.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelOperator.Image")));
            this.btnCancelOperator.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancelOperator.Location = new System.Drawing.Point(21, 9);
            this.btnCancelOperator.Name = "btnCancelOperator";
            this.btnCancelOperator.Size = new System.Drawing.Size(104, 27);
            this.btnCancelOperator.TabIndex = 5;
            this.btnCancelOperator.Text = "取消补偿";
            this.btnCancelOperator.Click += new System.EventHandler(this.btnCancelOperator_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnOK.Location = new System.Drawing.Point(288, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 27);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确  定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnAllOuter);
            this.groupBox2.Controls.Add(this.rbtnAllInner);
            this.groupBox2.Controls.Add(this.rbtnAuto);
            this.groupBox2.Controls.Add(this.ckCompensationNotClosedFigure);
            this.groupBox2.Location = new System.Drawing.Point(21, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(441, 107);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选项";
            // 
            // rbtnAllOuter
            // 
            this.rbtnAllOuter.Location = new System.Drawing.Point(17, 49);
            this.rbtnAllOuter.Name = "rbtnAllOuter";
            this.rbtnAllOuter.Size = new System.Drawing.Size(141, 18);
            this.rbtnAllOuter.TabIndex = 24;
            this.rbtnAllOuter.Text = "全部外扩";
            this.rbtnAllOuter.UseVisualStyleBackColor = true;
            // 
            // rbtnAllInner
            // 
            this.rbtnAllInner.Location = new System.Drawing.Point(17, 72);
            this.rbtnAllInner.Name = "rbtnAllInner";
            this.rbtnAllInner.Size = new System.Drawing.Size(141, 18);
            this.rbtnAllInner.TabIndex = 24;
            this.rbtnAllInner.Text = "全部内缩";
            this.rbtnAllInner.UseVisualStyleBackColor = true;
            // 
            // rbtnAuto
            // 
            this.rbtnAuto.Checked = true;
            this.rbtnAuto.Location = new System.Drawing.Point(17, 26);
            this.rbtnAuto.Name = "rbtnAuto";
            this.rbtnAuto.Size = new System.Drawing.Size(141, 18);
            this.rbtnAuto.TabIndex = 24;
            this.rbtnAuto.TabStop = true;
            this.rbtnAuto.Text = "内膜内缩、外膜外扩";
            this.rbtnAuto.UseVisualStyleBackColor = true;
            // 
            // ckCompensationNotClosedFigure
            // 
            this.ckCompensationNotClosedFigure.EditValue = true;
            this.ckCompensationNotClosedFigure.Location = new System.Drawing.Point(228, 24);
            this.ckCompensationNotClosedFigure.Margin = new System.Windows.Forms.Padding(0);
            this.ckCompensationNotClosedFigure.Name = "ckCompensationNotClosedFigure";
            this.ckCompensationNotClosedFigure.Properties.AutoWidth = true;
            this.ckCompensationNotClosedFigure.Properties.Caption = "对不封闭图形进行补偿";
            this.ckCompensationNotClosedFigure.Size = new System.Drawing.Size(142, 19);
            this.ckCompensationNotClosedFigure.TabIndex = 25;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnlImage);
            this.groupBox3.Controls.Add(this.rbtnFillet);
            this.groupBox3.Controls.Add(this.rbtnRightAngle);
            this.groupBox3.Controls.Add(this.comboBoxEdit2);
            this.groupBox3.Controls.Add(this.labelControl6);
            this.groupBox3.Controls.Add(this.labelControl7);
            this.groupBox3.Controls.Add(this.labelControl8);
            this.groupBox3.Controls.Add(this.txtInSideWidth);
            this.groupBox3.Location = new System.Drawing.Point(21, 72);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(441, 130);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "补偿参数";
            // 
            // pnlImage
            // 
            this.pnlImage.Location = new System.Drawing.Point(228, 22);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(200, 100);
            this.pnlImage.TabIndex = 25;
            // 
            // rbtnFillet
            // 
            this.rbtnFillet.Location = new System.Drawing.Point(156, 87);
            this.rbtnFillet.Name = "rbtnFillet";
            this.rbtnFillet.Size = new System.Drawing.Size(49, 18);
            this.rbtnFillet.TabIndex = 24;
            this.rbtnFillet.Text = "圆角";
            this.rbtnFillet.UseVisualStyleBackColor = true;
            this.rbtnFillet.CheckedChanged += new System.EventHandler(this.rbtnClick_CheckedChanged);
            // 
            // rbtnRightAngle
            // 
            this.rbtnRightAngle.Checked = true;
            this.rbtnRightAngle.Location = new System.Drawing.Point(96, 87);
            this.rbtnRightAngle.Name = "rbtnRightAngle";
            this.rbtnRightAngle.Size = new System.Drawing.Size(54, 18);
            this.rbtnRightAngle.TabIndex = 24;
            this.rbtnRightAngle.TabStop = true;
            this.rbtnRightAngle.Text = "直角";
            this.rbtnRightAngle.UseVisualStyleBackColor = true;
            this.rbtnRightAngle.CheckedChanged += new System.EventHandler(this.rbtnClick_CheckedChanged);
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.EditValue = "请选择";
            this.comboBoxEdit2.Location = new System.Drawing.Point(93, 60);
            this.comboBoxEdit2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Properties.Items.AddRange(new object[] {
            "请选择"});
            this.comboBoxEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit2.Size = new System.Drawing.Size(111, 20);
            this.comboBoxEdit2.TabIndex = 23;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(30, 89);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "拐角处理：";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(30, 63);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(60, 14);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "常用配置：";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(30, 34);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(60, 14);
            this.labelControl8.TabIndex = 12;
            this.labelControl8.Text = "补偿宽度：";
            // 
            // txtInSideWidth
            // 
            this.txtInSideWidth.Format = null;
            this.txtInSideWidth.IsInterger = false;
            this.txtInSideWidth.Location = new System.Drawing.Point(93, 32);
            this.txtInSideWidth.Margin = new System.Windows.Forms.Padding(0);
            this.txtInSideWidth.Max = 10D;
            this.txtInSideWidth.Min = 0.01D;
            this.txtInSideWidth.Name = "txtInSideWidth";
            this.txtInSideWidth.Number = 1D;
            this.txtInSideWidth.ReadOnly = false;
            this.txtInSideWidth.Size = new System.Drawing.Size(111, 22);
            this.txtInSideWidth.Suffix = "mm";
            this.txtInSideWidth.TabIndex = 14;
            this.txtInSideWidth.TextSize = 9F;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // FrmCompensation
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(474, 369);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCompensation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "割缝补偿";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckCompensationNotClosedFigure.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private ControlLibrary.Common.UCNumberInputer txtInSideWidth;
        private DevExpress.XtraEditors.CheckEdit ckCompensationNotClosedFigure;
        private DevExpress.XtraEditors.SimpleButton btnCancelOperator;
        private System.Windows.Forms.RadioButton rbtnFillet;
        private System.Windows.Forms.RadioButton rbtnRightAngle;
        private System.Windows.Forms.RadioButton rbtnAllOuter;
        private System.Windows.Forms.RadioButton rbtnAllInner;
        private System.Windows.Forms.RadioButton rbtnAuto;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private System.Windows.Forms.Panel pnlImage;
    }
}