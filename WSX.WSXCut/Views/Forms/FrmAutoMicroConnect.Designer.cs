namespace WSX.WSXCut.Views.Forms
{
    partial class FrmAutoMicroConnect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAutoMicroConnect));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ckNotApplyCorner = new DevExpress.XtraEditors.CheckEdit();
            this.ckNotApplyStartPoint = new DevExpress.XtraEditors.CheckEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnSpacing = new System.Windows.Forms.RadioButton();
            this.rbtnCount = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ckMaxSize = new DevExpress.XtraEditors.CheckEdit();
            this.ckMinSize = new DevExpress.XtraEditors.CheckEdit();
            this.txtMaxSize = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtMinSize = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmbApplySelectedTypes = new DevExpress.XtraEditors.ComboBoxEdit();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtMicroSize = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtCount = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtDistance = new WSX.ControlLibrary.Common.UCNumberInputer();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckNotApplyCorner.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckNotApplyStartPoint.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckMaxSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckMinSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbApplySelectedTypes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 259);
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
            this.labelControl2.Location = new System.Drawing.Point(24, 44);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 28);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "本功能根据给定的参数对图形进行微连，微连的地方实际切割时不开光，可以确保整个图形加工完成不掉落。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "自动微连";
            // 
            // ckNotApplyCorner
            // 
            this.ckNotApplyCorner.Location = new System.Drawing.Point(12, 49);
            this.ckNotApplyCorner.Margin = new System.Windows.Forms.Padding(0);
            this.ckNotApplyCorner.Name = "ckNotApplyCorner";
            this.ckNotApplyCorner.Properties.AutoWidth = true;
            this.ckNotApplyCorner.Properties.Caption = "拐角不微连";
            this.ckNotApplyCorner.Size = new System.Drawing.Size(82, 19);
            this.ckNotApplyCorner.TabIndex = 30;
            // 
            // ckNotApplyStartPoint
            // 
            this.ckNotApplyStartPoint.Location = new System.Drawing.Point(12, 25);
            this.ckNotApplyStartPoint.Margin = new System.Windows.Forms.Padding(0);
            this.ckNotApplyStartPoint.Name = "ckNotApplyStartPoint";
            this.ckNotApplyStartPoint.Properties.AutoWidth = true;
            this.ckNotApplyStartPoint.Properties.Caption = "起点不微连";
            this.ckNotApplyStartPoint.Size = new System.Drawing.Size(82, 19);
            this.ckNotApplyStartPoint.TabIndex = 30;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnSpacing);
            this.groupBox1.Controls.Add(this.rbtnCount);
            this.groupBox1.Location = new System.Drawing.Point(24, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 80);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "样式";
            // 
            // rbtnSpacing
            // 
            this.rbtnSpacing.Location = new System.Drawing.Point(12, 46);
            this.rbtnSpacing.Name = "rbtnSpacing";
            this.rbtnSpacing.Size = new System.Drawing.Size(117, 18);
            this.rbtnSpacing.TabIndex = 0;
            this.rbtnSpacing.Text = "按间隔距离微连";
            this.rbtnSpacing.UseVisualStyleBackColor = true;
            // 
            // rbtnCount
            // 
            this.rbtnCount.Checked = true;
            this.rbtnCount.Location = new System.Drawing.Point(12, 22);
            this.rbtnCount.Name = "rbtnCount";
            this.rbtnCount.Size = new System.Drawing.Size(117, 18);
            this.rbtnCount.TabIndex = 0;
            this.rbtnCount.TabStop = true;
            this.rbtnCount.Text = "按数量微连";
            this.rbtnCount.UseVisualStyleBackColor = true;
            this.rbtnCount.CheckedChanged += new System.EventHandler(this.rbtnNumber_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckNotApplyCorner);
            this.groupBox2.Controls.Add(this.ckNotApplyStartPoint);
            this.groupBox2.Location = new System.Drawing.Point(24, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(154, 80);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "特殊";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ckMaxSize);
            this.groupBox3.Controls.Add(this.ckMinSize);
            this.groupBox3.Controls.Add(this.txtMaxSize);
            this.groupBox3.Controls.Add(this.txtMinSize);
            this.groupBox3.Location = new System.Drawing.Point(184, 170);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(203, 80);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "尺寸过滤";
            // 
            // ckMaxSize
            // 
            this.ckMaxSize.Location = new System.Drawing.Point(3, 49);
            this.ckMaxSize.Margin = new System.Windows.Forms.Padding(0);
            this.ckMaxSize.Name = "ckMaxSize";
            this.ckMaxSize.Properties.AutoWidth = true;
            this.ckMaxSize.Properties.Caption = "最大尺寸：";
            this.ckMaxSize.Size = new System.Drawing.Size(82, 19);
            this.ckMaxSize.TabIndex = 30;
            this.ckMaxSize.CheckedChanged += new System.EventHandler(this.ckFilterMax_CheckedChanged);
            // 
            // ckMinSize
            // 
            this.ckMinSize.Location = new System.Drawing.Point(3, 25);
            this.ckMinSize.Margin = new System.Windows.Forms.Padding(0);
            this.ckMinSize.Name = "ckMinSize";
            this.ckMinSize.Properties.AutoWidth = true;
            this.ckMinSize.Properties.Caption = "最小尺寸：";
            this.ckMinSize.Size = new System.Drawing.Size(82, 19);
            this.ckMinSize.TabIndex = 30;
            this.ckMinSize.CheckedChanged += new System.EventHandler(this.ckFilterMin_CheckedChanged);
            // 
            // txtMaxSize
            // 
            this.txtMaxSize.Format = null;
            this.txtMaxSize.IsInterger = false;
            this.txtMaxSize.Location = new System.Drawing.Point(89, 51);
            this.txtMaxSize.Margin = new System.Windows.Forms.Padding(0);
            this.txtMaxSize.Max = 99999D;
            this.txtMaxSize.Min = 0D;
            this.txtMaxSize.Name = "txtMaxSize";
            this.txtMaxSize.Number = 100D;
            this.txtMaxSize.ReadOnly = false;
            this.txtMaxSize.Size = new System.Drawing.Size(97, 22);
            this.txtMaxSize.Suffix = "mm";
            this.txtMaxSize.TabIndex = 31;
            this.txtMaxSize.TextSize = 9F;
            // 
            // txtMinSize
            // 
            this.txtMinSize.Format = null;
            this.txtMinSize.IsInterger = false;
            this.txtMinSize.Location = new System.Drawing.Point(89, 22);
            this.txtMinSize.Margin = new System.Windows.Forms.Padding(0);
            this.txtMinSize.Max = 99999D;
            this.txtMinSize.Min = 0D;
            this.txtMinSize.Name = "txtMinSize";
            this.txtMinSize.Number = 10D;
            this.txtMinSize.ReadOnly = false;
            this.txtMinSize.Size = new System.Drawing.Size(97, 22);
            this.txtMinSize.Suffix = "mm";
            this.txtMinSize.TabIndex = 31;
            this.txtMinSize.TextSize = 9F;
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(191, 93);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(60, 14);
            this.labelControl26.TabIndex = 30;
            this.labelControl26.Text = "微连数量：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(191, 117);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "微连大小：";
            // 
            // cmbApplySelectedTypes
            // 
            this.cmbApplySelectedTypes.EditValue = "对选中图形生效";
            this.cmbApplySelectedTypes.Location = new System.Drawing.Point(191, 140);
            this.cmbApplySelectedTypes.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.cmbApplySelectedTypes.Name = "cmbApplySelectedTypes";
            this.cmbApplySelectedTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbApplySelectedTypes.Properties.Items.AddRange(new object[] {
            "对选中图形生效",
            "对所有图形生效"});
            this.cmbApplySelectedTypes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbApplySelectedTypes.Size = new System.Drawing.Size(179, 20);
            this.cmbApplySelectedTypes.TabIndex = 32;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(191, 93);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 33;
            this.labelControl4.Text = "间隔距离：";
            // 
            // txtMicroSize
            // 
            this.txtMicroSize.Format = null;
            this.txtMicroSize.IsInterger = false;
            this.txtMicroSize.Location = new System.Drawing.Point(273, 115);
            this.txtMicroSize.Margin = new System.Windows.Forms.Padding(0);
            this.txtMicroSize.Max = 99999D;
            this.txtMicroSize.Min = 0D;
            this.txtMicroSize.Name = "txtMicroSize";
            this.txtMicroSize.Number = 5D;
            this.txtMicroSize.ReadOnly = false;
            this.txtMicroSize.Size = new System.Drawing.Size(97, 22);
            this.txtMicroSize.Suffix = "mm";
            this.txtMicroSize.TabIndex = 31;
            this.txtMicroSize.TextSize = 9F;
            // 
            // txtCount
            // 
            this.txtCount.Format = null;
            this.txtCount.IsInterger = false;
            this.txtCount.Location = new System.Drawing.Point(273, 91);
            this.txtCount.Margin = new System.Windows.Forms.Padding(0);
            this.txtCount.Max = 99999D;
            this.txtCount.Min = 0D;
            this.txtCount.Name = "txtCount";
            this.txtCount.Number = 5D;
            this.txtCount.ReadOnly = false;
            this.txtCount.Size = new System.Drawing.Size(97, 22);
            this.txtCount.Suffix = null;
            this.txtCount.TabIndex = 31;
            this.txtCount.TextSize = 9F;
            // 
            // txtDistance
            // 
            this.txtDistance.Format = null;
            this.txtDistance.IsInterger = false;
            this.txtDistance.Location = new System.Drawing.Point(273, 91);
            this.txtDistance.Margin = new System.Windows.Forms.Padding(0);
            this.txtDistance.Max = 99999D;
            this.txtDistance.Min = 0D;
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.Number = 10D;
            this.txtDistance.ReadOnly = false;
            this.txtDistance.Size = new System.Drawing.Size(97, 22);
            this.txtDistance.Suffix = "mm";
            this.txtDistance.TabIndex = 34;
            this.txtDistance.TextSize = 9F;
            // 
            // FrmAutoMicroConnect
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 315);
            this.Controls.Add(this.cmbApplySelectedTypes);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtMicroSize);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.labelControl26);
            this.Controls.Add(this.txtDistance);
            this.Controls.Add(this.labelControl4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAutoMicroConnect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "自动微连";
            this.Load += new System.EventHandler(this.FrmAutoMicroConnect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckNotApplyCorner.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckNotApplyStartPoint.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckMaxSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckMinSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbApplySelectedTypes.Properties)).EndInit();
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
        private DevExpress.XtraEditors.CheckEdit ckNotApplyCorner;
        private DevExpress.XtraEditors.CheckEdit ckNotApplyStartPoint;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtnSpacing;
        private System.Windows.Forms.RadioButton rbtnCount;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.CheckEdit ckMaxSize;
        private DevExpress.XtraEditors.CheckEdit ckMinSize;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private ControlLibrary.Common.UCNumberInputer txtCount;
        private ControlLibrary.Common.UCNumberInputer txtMicroSize;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private ControlLibrary.Common.UCNumberInputer txtMaxSize;
        private ControlLibrary.Common.UCNumberInputer txtMinSize;
        private DevExpress.XtraEditors.ComboBoxEdit cmbApplySelectedTypes;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private ControlLibrary.Common.UCNumberInputer txtDistance;
    }
}