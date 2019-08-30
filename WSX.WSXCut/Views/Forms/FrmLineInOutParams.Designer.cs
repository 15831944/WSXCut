namespace WSX.WSXCut.Views.Forms
{
    partial class FrmLineInOutParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLineInOutParams));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckAddCircularHole = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.txtCircularHoleRadius = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtLineInRadius = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtLineInAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtLineInLength = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.cmbLineInTypes = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtLineOutRadius = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtLineOutAngle = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.txtLineOutLength = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.cmbLineOutTypes = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkSideLeadin = new DevExpress.XtraEditors.CheckEdit();
            this.chkVertexLeadIn = new DevExpress.XtraEditors.CheckEdit();
            this.rbtnOnlyChangeType = new System.Windows.Forms.RadioButton();
            this.rbtnFigureTotalLength = new System.Windows.Forms.RadioButton();
            this.rbtnAutoSelectSuitable = new System.Windows.Forms.RadioButton();
            this.txtFigureTotalLength = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkOnlyApplyInFigure = new DevExpress.XtraEditors.CheckEdit();
            this.chkOnlyApplyOutFigure = new DevExpress.XtraEditors.CheckEdit();
            this.ckOnlyApplyClosedFigure = new DevExpress.XtraEditors.CheckEdit();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckAddCircularHole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLineInTypes.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLineOutTypes.Properties)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSideLeadin.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVertexLeadIn.Properties)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkOnlyApplyInFigure.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOnlyApplyOutFigure.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckOnlyApplyClosedFigure.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 524);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(493, 48);
            this.panelControl2.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(405, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnOK.Location = new System.Drawing.Point(307, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 27);
            this.btnOK.TabIndex = 0;
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
            this.panelControl1.Size = new System.Drawing.Size(493, 66);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 47);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(132, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "为图形添加引入引出线。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(21, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(140, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "引入引出线设置";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckAddCircularHole);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.labelControl26);
            this.groupBox1.Controls.Add(this.txtCircularHoleRadius);
            this.groupBox1.Controls.Add(this.txtLineInRadius);
            this.groupBox1.Controls.Add(this.txtLineInAngle);
            this.groupBox1.Controls.Add(this.txtLineInLength);
            this.groupBox1.Controls.Add(this.cmbLineInTypes);
            this.groupBox1.Controls.Add(this.labelControl16);
            this.groupBox1.Controls.Add(this.labelControl13);
            this.groupBox1.Controls.Add(this.labelControl12);
            this.groupBox1.Controls.Add(this.labelControl7);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Location = new System.Drawing.Point(21, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 107);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "引入线";
            // 
            // ckAddCircularHole
            // 
            this.ckAddCircularHole.Location = new System.Drawing.Point(33, 75);
            this.ckAddCircularHole.Margin = new System.Windows.Forms.Padding(0);
            this.ckAddCircularHole.Name = "ckAddCircularHole";
            this.ckAddCircularHole.Properties.AutoWidth = true;
            this.ckAddCircularHole.Properties.Caption = "在引入线起点添加小圆孔";
            this.ckAddCircularHole.Size = new System.Drawing.Size(154, 19);
            this.ckAddCircularHole.TabIndex = 4;
            this.ckAddCircularHole.CheckedChanged += new System.EventHandler(this.ckAddCircularHole_CheckedChanged);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(219, 77);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(60, 14);
            this.labelControl6.TabIndex = 25;
            this.labelControl6.Text = "圆孔半径：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(243, 55);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 25;
            this.labelControl5.Text = "半径：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(33, 53);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 25;
            this.labelControl4.Text = "角度：";
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(243, 30);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(36, 14);
            this.labelControl26.TabIndex = 25;
            this.labelControl26.Text = "长度：";
            // 
            // txtCircularHoleRadius
            // 
            this.txtCircularHoleRadius.Format = null;
            this.txtCircularHoleRadius.IsInterger = false;
            this.txtCircularHoleRadius.Location = new System.Drawing.Point(287, 74);
            this.txtCircularHoleRadius.Margin = new System.Windows.Forms.Padding(0);
            this.txtCircularHoleRadius.Max = 9.9D;
            this.txtCircularHoleRadius.Min = 0D;
            this.txtCircularHoleRadius.Name = "txtCircularHoleRadius";
            this.txtCircularHoleRadius.Number = 0.5D;
            this.txtCircularHoleRadius.ReadOnly = false;
            this.txtCircularHoleRadius.Size = new System.Drawing.Size(104, 22);
            this.txtCircularHoleRadius.Suffix = null;
            this.txtCircularHoleRadius.TabIndex = 5;
            this.txtCircularHoleRadius.TextSize = 9F;
            // 
            // txtLineInRadius
            // 
            this.txtLineInRadius.Format = null;
            this.txtLineInRadius.IsInterger = false;
            this.txtLineInRadius.Location = new System.Drawing.Point(287, 51);
            this.txtLineInRadius.Margin = new System.Windows.Forms.Padding(0);
            this.txtLineInRadius.Max = 999D;
            this.txtLineInRadius.Min = 0D;
            this.txtLineInRadius.Name = "txtLineInRadius";
            this.txtLineInRadius.Number = 2D;
            this.txtLineInRadius.ReadOnly = false;
            this.txtLineInRadius.Size = new System.Drawing.Size(104, 22);
            this.txtLineInRadius.Suffix = null;
            this.txtLineInRadius.TabIndex = 3;
            this.txtLineInRadius.TextSize = 9F;
            // 
            // txtLineInAngle
            // 
            this.txtLineInAngle.Format = null;
            this.txtLineInAngle.IsInterger = false;
            this.txtLineInAngle.Location = new System.Drawing.Point(81, 51);
            this.txtLineInAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtLineInAngle.Max = 179D;
            this.txtLineInAngle.Min = 1D;
            this.txtLineInAngle.Name = "txtLineInAngle";
            this.txtLineInAngle.Number = 30D;
            this.txtLineInAngle.ReadOnly = false;
            this.txtLineInAngle.Size = new System.Drawing.Size(104, 22);
            this.txtLineInAngle.Suffix = null;
            this.txtLineInAngle.TabIndex = 2;
            this.txtLineInAngle.TextSize = 9F;
            // 
            // txtLineInLength
            // 
            this.txtLineInLength.Format = null;
            this.txtLineInLength.IsInterger = false;
            this.txtLineInLength.Location = new System.Drawing.Point(287, 28);
            this.txtLineInLength.Margin = new System.Windows.Forms.Padding(0);
            this.txtLineInLength.Max = 999D;
            this.txtLineInLength.Min = 0D;
            this.txtLineInLength.Name = "txtLineInLength";
            this.txtLineInLength.Number = 2D;
            this.txtLineInLength.ReadOnly = false;
            this.txtLineInLength.Size = new System.Drawing.Size(104, 22);
            this.txtLineInLength.Suffix = null;
            this.txtLineInLength.TabIndex = 1;
            this.txtLineInLength.TextSize = 9F;
            // 
            // cmbLineInTypes
            // 
            this.cmbLineInTypes.EditValue = "请选择";
            this.cmbLineInTypes.Location = new System.Drawing.Point(81, 27);
            this.cmbLineInTypes.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.cmbLineInTypes.Name = "cmbLineInTypes";
            this.cmbLineInTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLineInTypes.Properties.Items.AddRange(new object[] {
            "无",
            "直线",
            "圆弧",
            "直线+圆弧"});
            this.cmbLineInTypes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbLineInTypes.Size = new System.Drawing.Size(104, 20);
            this.cmbLineInTypes.TabIndex = 0;
            this.cmbLineInTypes.SelectedIndexChanged += new System.EventHandler(this.cmbLineInTypes_SelectedIndexChanged);
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(188, 53);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(6, 14);
            this.labelControl16.TabIndex = 23;
            this.labelControl16.Text = "°";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(394, 80);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(20, 14);
            this.labelControl13.TabIndex = 23;
            this.labelControl13.Text = "mm";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(394, 54);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(20, 14);
            this.labelControl12.TabIndex = 23;
            this.labelControl12.Text = "mm";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(394, 30);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(20, 14);
            this.labelControl7.TabIndex = 23;
            this.labelControl7.Text = "mm";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(31, 30);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 23;
            this.labelControl3.Text = "类型：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelControl8);
            this.groupBox2.Controls.Add(this.labelControl9);
            this.groupBox2.Controls.Add(this.labelControl10);
            this.groupBox2.Controls.Add(this.txtLineOutRadius);
            this.groupBox2.Controls.Add(this.txtLineOutAngle);
            this.groupBox2.Controls.Add(this.txtLineOutLength);
            this.groupBox2.Controls.Add(this.cmbLineOutTypes);
            this.groupBox2.Controls.Add(this.labelControl11);
            this.groupBox2.Controls.Add(this.labelControl15);
            this.groupBox2.Controls.Add(this.labelControl14);
            this.groupBox2.Controls.Add(this.labelControl17);
            this.groupBox2.Location = new System.Drawing.Point(21, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(460, 82);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "引出线";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(243, 55);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(36, 14);
            this.labelControl8.TabIndex = 25;
            this.labelControl8.Text = "半径：";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(33, 53);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(36, 14);
            this.labelControl9.TabIndex = 25;
            this.labelControl9.Text = "角度：";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(243, 30);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(36, 14);
            this.labelControl10.TabIndex = 25;
            this.labelControl10.Text = "长度：";
            // 
            // txtLineOutRadius
            // 
            this.txtLineOutRadius.Format = null;
            this.txtLineOutRadius.IsInterger = false;
            this.txtLineOutRadius.Location = new System.Drawing.Point(287, 51);
            this.txtLineOutRadius.Margin = new System.Windows.Forms.Padding(0);
            this.txtLineOutRadius.Max = 999D;
            this.txtLineOutRadius.Min = 0D;
            this.txtLineOutRadius.Name = "txtLineOutRadius";
            this.txtLineOutRadius.Number = 2D;
            this.txtLineOutRadius.ReadOnly = false;
            this.txtLineOutRadius.Size = new System.Drawing.Size(104, 22);
            this.txtLineOutRadius.Suffix = null;
            this.txtLineOutRadius.TabIndex = 3;
            this.txtLineOutRadius.TextSize = 9F;
            // 
            // txtLineOutAngle
            // 
            this.txtLineOutAngle.Format = null;
            this.txtLineOutAngle.IsInterger = false;
            this.txtLineOutAngle.Location = new System.Drawing.Point(81, 51);
            this.txtLineOutAngle.Margin = new System.Windows.Forms.Padding(0);
            this.txtLineOutAngle.Max = 179D;
            this.txtLineOutAngle.Min = 1D;
            this.txtLineOutAngle.Name = "txtLineOutAngle";
            this.txtLineOutAngle.Number = 30D;
            this.txtLineOutAngle.ReadOnly = false;
            this.txtLineOutAngle.Size = new System.Drawing.Size(104, 22);
            this.txtLineOutAngle.Suffix = null;
            this.txtLineOutAngle.TabIndex = 2;
            this.txtLineOutAngle.TextSize = 9F;
            // 
            // txtLineOutLength
            // 
            this.txtLineOutLength.Format = null;
            this.txtLineOutLength.IsInterger = false;
            this.txtLineOutLength.Location = new System.Drawing.Point(287, 28);
            this.txtLineOutLength.Margin = new System.Windows.Forms.Padding(0);
            this.txtLineOutLength.Max = 999D;
            this.txtLineOutLength.Min = 0D;
            this.txtLineOutLength.Name = "txtLineOutLength";
            this.txtLineOutLength.Number = 2D;
            this.txtLineOutLength.ReadOnly = false;
            this.txtLineOutLength.Size = new System.Drawing.Size(104, 22);
            this.txtLineOutLength.Suffix = null;
            this.txtLineOutLength.TabIndex = 1;
            this.txtLineOutLength.TextSize = 9F;
            // 
            // cmbLineOutTypes
            // 
            this.cmbLineOutTypes.EditValue = "请选择";
            this.cmbLineOutTypes.Location = new System.Drawing.Point(81, 27);
            this.cmbLineOutTypes.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.cmbLineOutTypes.Name = "cmbLineOutTypes";
            this.cmbLineOutTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLineOutTypes.Properties.Items.AddRange(new object[] {
            "无",
            "直线",
            "圆弧",
            "直线+圆弧"});
            this.cmbLineOutTypes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbLineOutTypes.Size = new System.Drawing.Size(104, 20);
            this.cmbLineOutTypes.TabIndex = 0;
            this.cmbLineOutTypes.SelectedIndexChanged += new System.EventHandler(this.cmbLineOutTypes_SelectedIndexChanged);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(31, 30);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(36, 14);
            this.labelControl11.TabIndex = 23;
            this.labelControl11.Text = "类型：";
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(394, 51);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(20, 14);
            this.labelControl15.TabIndex = 23;
            this.labelControl15.Text = "mm";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(394, 33);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(20, 14);
            this.labelControl14.TabIndex = 23;
            this.labelControl14.Text = "mm";
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(188, 51);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(6, 14);
            this.labelControl17.TabIndex = 23;
            this.labelControl17.Text = "°";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkSideLeadin);
            this.groupBox3.Controls.Add(this.chkVertexLeadIn);
            this.groupBox3.Controls.Add(this.rbtnOnlyChangeType);
            this.groupBox3.Controls.Add(this.rbtnFigureTotalLength);
            this.groupBox3.Controls.Add(this.rbtnAutoSelectSuitable);
            this.groupBox3.Controls.Add(this.txtFigureTotalLength);
            this.groupBox3.Location = new System.Drawing.Point(21, 274);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(460, 150);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "引线位置";
            // 
            // chkSideLeadin
            // 
            this.chkSideLeadin.Location = new System.Drawing.Point(49, 68);
            this.chkSideLeadin.Margin = new System.Windows.Forms.Padding(0);
            this.chkSideLeadin.Name = "chkSideLeadin";
            this.chkSideLeadin.Properties.AutoWidth = true;
            this.chkSideLeadin.Properties.Caption = "优先从边长引入";
            this.chkSideLeadin.Size = new System.Drawing.Size(106, 19);
            this.chkSideLeadin.TabIndex = 4;
            this.chkSideLeadin.CheckedChanged += new System.EventHandler(this.chkSideLeadin_CheckedChanged);
            // 
            // chkVertexLeadIn
            // 
            this.chkVertexLeadIn.Location = new System.Drawing.Point(49, 42);
            this.chkVertexLeadIn.Margin = new System.Windows.Forms.Padding(0);
            this.chkVertexLeadIn.Name = "chkVertexLeadIn";
            this.chkVertexLeadIn.Properties.AutoWidth = true;
            this.chkVertexLeadIn.Properties.Caption = "优先从顶点引入";
            this.chkVertexLeadIn.Size = new System.Drawing.Size(106, 19);
            this.chkVertexLeadIn.TabIndex = 4;
            this.chkVertexLeadIn.CheckedChanged += new System.EventHandler(this.chkVertexLeadIn_CheckedChanged);
            // 
            // rbtnOnlyChangeType
            // 
            this.rbtnOnlyChangeType.AutoSize = true;
            this.rbtnOnlyChangeType.Location = new System.Drawing.Point(33, 119);
            this.rbtnOnlyChangeType.Name = "rbtnOnlyChangeType";
            this.rbtnOnlyChangeType.Size = new System.Drawing.Size(181, 18);
            this.rbtnOnlyChangeType.TabIndex = 5;
            this.rbtnOnlyChangeType.TabStop = true;
            this.rbtnOnlyChangeType.Text = "不改变引线位置，只改变类型";
            this.rbtnOnlyChangeType.UseVisualStyleBackColor = true;
            // 
            // rbtnFigureTotalLength
            // 
            this.rbtnFigureTotalLength.AutoSize = true;
            this.rbtnFigureTotalLength.Location = new System.Drawing.Point(33, 95);
            this.rbtnFigureTotalLength.Name = "rbtnFigureTotalLength";
            this.rbtnFigureTotalLength.Size = new System.Drawing.Size(214, 18);
            this.rbtnFigureTotalLength.TabIndex = 3;
            this.rbtnFigureTotalLength.TabStop = true;
            this.rbtnFigureTotalLength.Text = "按照图形总长设定统一的位置(0~1)";
            this.rbtnFigureTotalLength.UseVisualStyleBackColor = true;
            // 
            // rbtnAutoSelectSuitable
            // 
            this.rbtnAutoSelectSuitable.AutoSize = true;
            this.rbtnAutoSelectSuitable.Checked = true;
            this.rbtnAutoSelectSuitable.Location = new System.Drawing.Point(33, 21);
            this.rbtnAutoSelectSuitable.Name = "rbtnAutoSelectSuitable";
            this.rbtnAutoSelectSuitable.Size = new System.Drawing.Size(157, 18);
            this.rbtnAutoSelectSuitable.TabIndex = 0;
            this.rbtnAutoSelectSuitable.TabStop = true;
            this.rbtnAutoSelectSuitable.Text = "自动选择合适的引入位置";
            this.rbtnAutoSelectSuitable.UseVisualStyleBackColor = true;
            this.rbtnAutoSelectSuitable.CheckedChanged += new System.EventHandler(this.rbtnAutoSelectSuitable_CheckedChanged);
            // 
            // txtFigureTotalLength
            // 
            this.txtFigureTotalLength.Format = null;
            this.txtFigureTotalLength.IsInterger = false;
            this.txtFigureTotalLength.Location = new System.Drawing.Point(252, 95);
            this.txtFigureTotalLength.Margin = new System.Windows.Forms.Padding(0);
            this.txtFigureTotalLength.Max = 1D;
            this.txtFigureTotalLength.Min = 0D;
            this.txtFigureTotalLength.Name = "txtFigureTotalLength";
            this.txtFigureTotalLength.Number = 0D;
            this.txtFigureTotalLength.ReadOnly = false;
            this.txtFigureTotalLength.Size = new System.Drawing.Size(104, 22);
            this.txtFigureTotalLength.Suffix = null;
            this.txtFigureTotalLength.TabIndex = 4;
            this.txtFigureTotalLength.TextSize = 9F;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkOnlyApplyInFigure);
            this.groupBox4.Controls.Add(this.chkOnlyApplyOutFigure);
            this.groupBox4.Controls.Add(this.ckOnlyApplyClosedFigure);
            this.groupBox4.Location = new System.Drawing.Point(21, 430);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(460, 86);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "选项";
            // 
            // chkOnlyApplyInFigure
            // 
            this.chkOnlyApplyInFigure.Location = new System.Drawing.Point(162, 53);
            this.chkOnlyApplyInFigure.Margin = new System.Windows.Forms.Padding(0);
            this.chkOnlyApplyInFigure.Name = "chkOnlyApplyInFigure";
            this.chkOnlyApplyInFigure.Properties.AutoWidth = true;
            this.chkOnlyApplyInFigure.Properties.Caption = "仅用于内膜图形";
            this.chkOnlyApplyInFigure.Size = new System.Drawing.Size(106, 19);
            this.chkOnlyApplyInFigure.TabIndex = 0;
            this.chkOnlyApplyInFigure.Visible = false;
            this.chkOnlyApplyInFigure.CheckedChanged += new System.EventHandler(this.chkOnlyApplyInFigure_CheckedChanged);
            // 
            // chkOnlyApplyOutFigure
            // 
            this.chkOnlyApplyOutFigure.Location = new System.Drawing.Point(33, 53);
            this.chkOnlyApplyOutFigure.Margin = new System.Windows.Forms.Padding(0);
            this.chkOnlyApplyOutFigure.Name = "chkOnlyApplyOutFigure";
            this.chkOnlyApplyOutFigure.Properties.AutoWidth = true;
            this.chkOnlyApplyOutFigure.Properties.Caption = "仅用于外膜图形";
            this.chkOnlyApplyOutFigure.Size = new System.Drawing.Size(106, 19);
            this.chkOnlyApplyOutFigure.TabIndex = 0;
            this.chkOnlyApplyOutFigure.Visible = false;
            this.chkOnlyApplyOutFigure.CheckedChanged += new System.EventHandler(this.chkOnlyApplyOutFigure_CheckedChanged);
            // 
            // ckOnlyApplyClosedFigure
            // 
            this.ckOnlyApplyClosedFigure.Location = new System.Drawing.Point(33, 28);
            this.ckOnlyApplyClosedFigure.Margin = new System.Windows.Forms.Padding(0);
            this.ckOnlyApplyClosedFigure.Name = "ckOnlyApplyClosedFigure";
            this.ckOnlyApplyClosedFigure.Properties.AutoWidth = true;
            this.ckOnlyApplyClosedFigure.Properties.Caption = "仅作用于封闭图形";
            this.ckOnlyApplyClosedFigure.Size = new System.Drawing.Size(118, 19);
            this.ckOnlyApplyClosedFigure.TabIndex = 0;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // FrmLineInOutParams
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(493, 572);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLineInOutParams";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "引入引出线参数";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckAddCircularHole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLineInTypes.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLineOutTypes.Properties)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSideLeadin.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVertexLeadIn.Properties)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkOnlyApplyInFigure.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOnlyApplyOutFigure.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckOnlyApplyClosedFigure.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbLineInTypes;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private ControlLibrary.Common.UCNumberInputer txtLineInLength;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private ControlLibrary.Common.UCNumberInputer txtLineInAngle;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private ControlLibrary.Common.UCNumberInputer txtLineInRadius;
        private DevExpress.XtraEditors.CheckEdit ckAddCircularHole;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private ControlLibrary.Common.UCNumberInputer txtCircularHoleRadius;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private ControlLibrary.Common.UCNumberInputer txtLineOutRadius;
        private ControlLibrary.Common.UCNumberInputer txtLineOutAngle;
        private ControlLibrary.Common.UCNumberInputer txtLineOutLength;
        private DevExpress.XtraEditors.ComboBoxEdit cmbLineOutTypes;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private System.Windows.Forms.GroupBox groupBox3;
        private ControlLibrary.Common.UCNumberInputer txtFigureTotalLength;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.CheckEdit ckOnlyApplyClosedFigure;
        private System.Windows.Forms.RadioButton rbtnOnlyChangeType;
        private System.Windows.Forms.RadioButton rbtnFigureTotalLength;
        private System.Windows.Forms.RadioButton rbtnAutoSelectSuitable;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.CheckEdit chkSideLeadin;
        private DevExpress.XtraEditors.CheckEdit chkVertexLeadIn;
        private DevExpress.XtraEditors.CheckEdit chkOnlyApplyInFigure;
        private DevExpress.XtraEditors.CheckEdit chkOnlyApplyOutFigure;
    }
}