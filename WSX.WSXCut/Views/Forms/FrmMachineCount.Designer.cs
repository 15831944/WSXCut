namespace WSX.WSXCut.Views.Forms
{
    partial class FrmMachineCount
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnEditPWD = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.ckAutoSuspend = new DevExpress.XtraEditors.CheckEdit();
            this.cmbSortTypes = new DevExpress.XtraEditors.ComboBoxEdit();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.spFinishedCount = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl52 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.txtPlanTotalCount = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbPlanTotalCount = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckAutoSuspend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSortTypes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spFinishedCount.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPlanTotalCount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnEditPWD);
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 262);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(427, 56);
            this.panelControl2.TabIndex = 3;
            // 
            // btnEditPWD
            // 
            this.btnEditPWD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditPWD.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnEditPWD.Location = new System.Drawing.Point(26, 10);
            this.btnEditPWD.Name = "btnEditPWD";
            this.btnEditPWD.Size = new System.Drawing.Size(89, 31);
            this.btnEditPWD.TabIndex = 0;
            this.btnEditPWD.Text = "修改密码";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(325, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 31);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnOK.Location = new System.Drawing.Point(230, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(89, 31);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
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
            this.panelControl1.Size = new System.Drawing.Size(427, 77);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "管理与加工统计相关的参数，控制加工次数、自动暂停等";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(120, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "加工计数管理";
            // 
            // labelControl26
            // 
            this.labelControl26.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl26.Appearance.Options.UseImageAlign = true;
            this.labelControl26.Appearance.Options.UseTextOptions = true;
            this.labelControl26.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl26.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl26.Location = new System.Drawing.Point(42, 22);
            this.labelControl26.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(84, 14);
            this.labelControl26.TabIndex = 0;
            this.labelControl26.Text = "计划加工件数：";
            // 
            // ckAutoSuspend
            // 
            this.ckAutoSuspend.Location = new System.Drawing.Point(30, 27);
            this.ckAutoSuspend.Margin = new System.Windows.Forms.Padding(0);
            this.ckAutoSuspend.Name = "ckAutoSuspend";
            this.ckAutoSuspend.Properties.AutoWidth = true;
            this.ckAutoSuspend.Properties.Caption = "在一下时间后暂停：";
            this.ckAutoSuspend.Size = new System.Drawing.Size(130, 19);
            this.ckAutoSuspend.TabIndex = 0;
            // 
            // cmbSortTypes
            // 
            this.cmbSortTypes.EditValue = "不做任何动作";
            this.cmbSortTypes.Location = new System.Drawing.Point(156, 71);
            this.cmbSortTypes.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.cmbSortTypes.Name = "cmbSortTypes";
            this.cmbSortTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSortTypes.Properties.Items.AddRange(new object[] {
            "不做任何动作",
            "弹出对话框提示",
            "禁止再继续加工"});
            this.cmbSortTypes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSortTypes.Size = new System.Drawing.Size(195, 20);
            this.cmbSortTypes.TabIndex = 6;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // spFinishedCount
            // 
            this.spFinishedCount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spFinishedCount.Location = new System.Drawing.Point(156, 45);
            this.spFinishedCount.Name = "spFinishedCount";
            this.spFinishedCount.Properties.Appearance.Options.UseTextOptions = true;
            this.spFinishedCount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.spFinishedCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spFinishedCount.Properties.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.spFinishedCount.Size = new System.Drawing.Size(97, 20);
            this.spFinishedCount.TabIndex = 3;
            // 
            // labelControl52
            // 
            this.labelControl52.Location = new System.Drawing.Point(30, 48);
            this.labelControl52.Name = "labelControl52";
            this.labelControl52.Size = new System.Drawing.Size(96, 14);
            this.labelControl52.TabIndex = 2;
            this.labelControl52.Text = "已完成加工件数：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.txtPlanTotalCount);
            this.groupBox1.Controls.Add(this.cmbSortTypes);
            this.groupBox1.Controls.Add(this.spFinishedCount);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl52);
            this.groupBox1.Controls.Add(this.labelControl26);
            this.groupBox1.Location = new System.Drawing.Point(13, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 99);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计件管理";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClear.Location = new System.Drawing.Point(279, 41);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(72, 26);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "清零";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtPlanTotalCount
            // 
            this.txtPlanTotalCount.Format = null;
            this.txtPlanTotalCount.IsInterger = false;
            this.txtPlanTotalCount.Location = new System.Drawing.Point(156, 20);
            this.txtPlanTotalCount.Margin = new System.Windows.Forms.Padding(0);
            this.txtPlanTotalCount.Max = 100D;
            this.txtPlanTotalCount.Min = 0D;
            this.txtPlanTotalCount.Name = "txtPlanTotalCount";
            this.txtPlanTotalCount.Number = 0D;
            this.txtPlanTotalCount.ReadOnly = false;
            this.txtPlanTotalCount.Size = new System.Drawing.Size(97, 20);
            this.txtPlanTotalCount.Suffix = null;
            this.txtPlanTotalCount.TabIndex = 1;
            this.txtPlanTotalCount.TextSize = 9F;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(54, 72);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 14);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "完成计划后：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckAutoSuspend);
            this.groupBox2.Controls.Add(this.cmbPlanTotalCount);
            this.groupBox2.Location = new System.Drawing.Point(13, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 66);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "自动暂停";
            // 
            // cmbPlanTotalCount
            // 
            this.cmbPlanTotalCount.EditValue = "10分钟";
            this.cmbPlanTotalCount.Location = new System.Drawing.Point(227, 26);
            this.cmbPlanTotalCount.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.cmbPlanTotalCount.Name = "cmbPlanTotalCount";
            this.cmbPlanTotalCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPlanTotalCount.Properties.Items.AddRange(new object[] {
            "1分钟",
            "2分钟",
            "5分钟",
            "10分钟",
            "30分钟",
            "1小时",
            "2小时",
            "3小时",
            "4小时",
            "5小时",
            "6小时"});
            this.cmbPlanTotalCount.Size = new System.Drawing.Size(124, 20);
            this.cmbPlanTotalCount.TabIndex = 1;
            // 
            // FrmMachineCount
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(427, 318);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMachineCount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "加工计数管理";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckAutoSuspend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSortTypes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spFinishedCount.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbPlanTotalCount.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private ControlLibrary.Common.UCNumberInputer txtPlanTotalCount;
        private DevExpress.XtraEditors.CheckEdit ckAutoSuspend;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSortTypes;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.SpinEdit spFinishedCount;
        private DevExpress.XtraEditors.LabelControl labelControl52;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnEditPWD;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPlanTotalCount;
    }
}