namespace WSX.WSXCut.Views.Forms
{
    partial class FrmArrayRectangle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmArrayRectangle));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtColumnCount = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtRowCount = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtnOffsetSpacing = new System.Windows.Forms.RadioButton();
            this.rbtnOffsetRowCount = new System.Windows.Forms.RadioButton();
            this.txtOffsetColumn = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtOffsetRow = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtnBottom = new System.Windows.Forms.RadioButton();
            this.rbtnTop = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtnRight = new System.Windows.Forms.RadioButton();
            this.rbtnLeft = new System.Windows.Forms.RadioButton();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 325);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(388, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(286, 10);
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
            this.btnOK.Location = new System.Drawing.Point(171, 10);
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
            this.panelControl1.Size = new System.Drawing.Size(388, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "本功能根据给定的行数，列数和偏移量进行快速复制";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "阵列";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtColumnCount);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.txtRowCount);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Location = new System.Drawing.Point(35, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 56);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "阵列数量";
            // 
            // txtColumnCount
            // 
            this.txtColumnCount.Format = null;
            this.txtColumnCount.IsInterger = false;
            this.txtColumnCount.Location = new System.Drawing.Point(220, 24);
            this.txtColumnCount.Margin = new System.Windows.Forms.Padding(0);
            this.txtColumnCount.Max = 100D;
            this.txtColumnCount.Min = 0D;
            this.txtColumnCount.Name = "txtColumnCount";
            this.txtColumnCount.Number = 0D;
            this.txtColumnCount.ReadOnly = false;
            this.txtColumnCount.Size = new System.Drawing.Size(81, 22);
            this.txtColumnCount.Suffix = null;
            this.txtColumnCount.TabIndex = 31;
            this.txtColumnCount.TextSize = 9F;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(168, 24);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 30;
            this.labelControl4.Text = "列数：";
            // 
            // txtRowCount
            // 
            this.txtRowCount.Format = null;
            this.txtRowCount.IsInterger = false;
            this.txtRowCount.Location = new System.Drawing.Point(57, 25);
            this.txtRowCount.Margin = new System.Windows.Forms.Padding(0);
            this.txtRowCount.Max = 100D;
            this.txtRowCount.Min = 0D;
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Number = 0D;
            this.txtRowCount.ReadOnly = false;
            this.txtRowCount.Size = new System.Drawing.Size(81, 22);
            this.txtRowCount.Suffix = null;
            this.txtRowCount.TabIndex = 31;
            this.txtRowCount.TextSize = 9F;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 25);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "行数：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnOffsetSpacing);
            this.groupBox2.Controls.Add(this.rbtnOffsetRowCount);
            this.groupBox2.Controls.Add(this.txtOffsetColumn);
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Controls.Add(this.txtOffsetRow);
            this.groupBox2.Controls.Add(this.labelControl6);
            this.groupBox2.Location = new System.Drawing.Point(34, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(318, 73);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "偏移量";
            // 
            // rbtnOffsetSpacing
            // 
            this.rbtnOffsetSpacing.AutoSize = true;
            this.rbtnOffsetSpacing.Location = new System.Drawing.Point(128, 21);
            this.rbtnOffsetSpacing.Name = "rbtnOffsetSpacing";
            this.rbtnOffsetSpacing.Size = new System.Drawing.Size(49, 18);
            this.rbtnOffsetSpacing.TabIndex = 32;
            this.rbtnOffsetSpacing.Text = "间距";
            this.rbtnOffsetSpacing.UseVisualStyleBackColor = true;
            this.rbtnOffsetSpacing.CheckedChanged += new System.EventHandler(this.rbtnOffsetSpacing_CheckedChanged);
            // 
            // rbtnOffsetRowCount
            // 
            this.rbtnOffsetRowCount.AutoSize = true;
            this.rbtnOffsetRowCount.Location = new System.Drawing.Point(57, 21);
            this.rbtnOffsetRowCount.Name = "rbtnOffsetRowCount";
            this.rbtnOffsetRowCount.Size = new System.Drawing.Size(49, 18);
            this.rbtnOffsetRowCount.TabIndex = 32;
            this.rbtnOffsetRowCount.Text = "偏移";
            this.rbtnOffsetRowCount.UseVisualStyleBackColor = true;
            this.rbtnOffsetRowCount.CheckedChanged += new System.EventHandler(this.rbtnOffsetRowCount_CheckedChanged);
            // 
            // txtOffsetColumn
            // 
            this.txtOffsetColumn.Format = null;
            this.txtOffsetColumn.IsInterger = false;
            this.txtOffsetColumn.Location = new System.Drawing.Point(220, 45);
            this.txtOffsetColumn.Margin = new System.Windows.Forms.Padding(0);
            this.txtOffsetColumn.Max = 100D;
            this.txtOffsetColumn.Min = 0D;
            this.txtOffsetColumn.Name = "txtOffsetColumn";
            this.txtOffsetColumn.Number = 0D;
            this.txtOffsetColumn.ReadOnly = false;
            this.txtOffsetColumn.Size = new System.Drawing.Size(81, 22);
            this.txtOffsetColumn.Suffix = null;
            this.txtOffsetColumn.TabIndex = 31;
            this.txtOffsetColumn.TextSize = 9F;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(168, 45);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 30;
            this.labelControl5.Text = "列间距：";
            // 
            // txtOffsetRow
            // 
            this.txtOffsetRow.Format = null;
            this.txtOffsetRow.IsInterger = false;
            this.txtOffsetRow.Location = new System.Drawing.Point(57, 46);
            this.txtOffsetRow.Margin = new System.Windows.Forms.Padding(0);
            this.txtOffsetRow.Max = 100D;
            this.txtOffsetRow.Min = 0D;
            this.txtOffsetRow.Name = "txtOffsetRow";
            this.txtOffsetRow.Number = 0D;
            this.txtOffsetRow.ReadOnly = false;
            this.txtOffsetRow.Size = new System.Drawing.Size(81, 22);
            this.txtOffsetRow.Suffix = null;
            this.txtOffsetRow.TabIndex = 31;
            this.txtOffsetRow.TextSize = 9F;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(5, 46);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 30;
            this.labelControl6.Text = "行间距：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtnBottom);
            this.groupBox3.Controls.Add(this.rbtnTop);
            this.groupBox3.Location = new System.Drawing.Point(34, 238);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(149, 58);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "行方向";
            // 
            // rbtnBottom
            // 
            this.rbtnBottom.AutoSize = true;
            this.rbtnBottom.Location = new System.Drawing.Point(83, 21);
            this.rbtnBottom.Name = "rbtnBottom";
            this.rbtnBottom.Size = new System.Drawing.Size(49, 18);
            this.rbtnBottom.TabIndex = 32;
            this.rbtnBottom.Text = "向下";
            this.rbtnBottom.UseVisualStyleBackColor = true;
            // 
            // rbtnTop
            // 
            this.rbtnTop.AutoSize = true;
            this.rbtnTop.Location = new System.Drawing.Point(12, 21);
            this.rbtnTop.Name = "rbtnTop";
            this.rbtnTop.Size = new System.Drawing.Size(49, 18);
            this.rbtnTop.TabIndex = 32;
            this.rbtnTop.Text = "向上";
            this.rbtnTop.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtnRight);
            this.groupBox4.Controls.Add(this.rbtnLeft);
            this.groupBox4.Location = new System.Drawing.Point(208, 238);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(144, 58);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "列方向";
            // 
            // rbtnRight
            // 
            this.rbtnRight.AutoSize = true;
            this.rbtnRight.Location = new System.Drawing.Point(83, 21);
            this.rbtnRight.Name = "rbtnRight";
            this.rbtnRight.Size = new System.Drawing.Size(49, 18);
            this.rbtnRight.TabIndex = 32;
            this.rbtnRight.Text = "向右";
            this.rbtnRight.UseVisualStyleBackColor = true;
            // 
            // rbtnLeft
            // 
            this.rbtnLeft.AutoSize = true;
            this.rbtnLeft.Location = new System.Drawing.Point(12, 21);
            this.rbtnLeft.Name = "rbtnLeft";
            this.rbtnLeft.Size = new System.Drawing.Size(49, 18);
            this.rbtnLeft.TabIndex = 32;
            this.rbtnLeft.Text = "向左";
            this.rbtnLeft.UseVisualStyleBackColor = true;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(WSX.ViewModels.MainViewModel);
            // 
            // FrmArrayRectangle
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(388, 381);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmArrayRectangle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "阵列参数";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private ControlLibrary.Common.UCNumberInputer txtColumnCount;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private ControlLibrary.Common.UCNumberInputer txtRowCount;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.GroupBox groupBox2;
        private ControlLibrary.Common.UCNumberInputer txtOffsetColumn;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private ControlLibrary.Common.UCNumberInputer txtOffsetRow;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.RadioButton rbtnOffsetSpacing;
        private System.Windows.Forms.RadioButton rbtnOffsetRowCount;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtnBottom;
        private System.Windows.Forms.RadioButton rbtnTop;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtnRight;
        private System.Windows.Forms.RadioButton rbtnLeft;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}