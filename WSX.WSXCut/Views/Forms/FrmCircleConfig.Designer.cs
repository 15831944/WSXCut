namespace WSX.WSXCut.Views.Forms
{
    partial class FrmCircleConfig
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnAbort = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroupModel = new DevExpress.XtraEditors.RadioGroup();
            this.checkEditClear = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditStart = new DevExpress.XtraEditors.CheckEdit();
            this.ucInputCnt = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.ucInputInterval = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditClear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditStart.Properties)).BeginInit();
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
            this.panelControl1.Size = new System.Drawing.Size(345, 66);
            this.panelControl1.TabIndex = 26;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(18, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(192, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "本功能用于循环加工时参数的设置。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(18, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "循环加工";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnAbort);
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 231);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(345, 48);
            this.panelControl2.TabIndex = 27;
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnAbort.Location = new System.Drawing.Point(18, 9);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(76, 27);
            this.btnAbort.TabIndex = 6;
            this.btnAbort.Text = "取消循环";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(257, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 27);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnOK.Location = new System.Drawing.Point(177, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 27);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            //this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(21, 87);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "循环次数：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(21, 113);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 29;
            this.labelControl4.Text = "循环间隔：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(21, 139);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 30;
            this.labelControl5.Text = "加工模式：";
            // 
            // radioGroupModel
            // 
            this.radioGroupModel.Location = new System.Drawing.Point(87, 134);
            this.radioGroupModel.Name = "radioGroupModel";
            this.radioGroupModel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroupModel.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupModel.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroupModel.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "正常"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "空走")});
            this.radioGroupModel.Size = new System.Drawing.Size(118, 24);
            this.radioGroupModel.TabIndex = 31;
            // 
            // checkEditClear
            // 
            this.checkEditClear.Location = new System.Drawing.Point(21, 170);
            this.checkEditClear.Name = "checkEditClear";
            this.checkEditClear.Properties.AutoWidth = true;
            this.checkEditClear.Properties.Caption = "清除之前的加工计数";
            this.checkEditClear.Size = new System.Drawing.Size(130, 19);
            this.checkEditClear.TabIndex = 32;
            // 
            // checkEditStart
            // 
            this.checkEditStart.Location = new System.Drawing.Point(21, 191);
            this.checkEditStart.Name = "checkEditStart";
            this.checkEditStart.Properties.AutoWidth = true;
            this.checkEditStart.Properties.Caption = "单击“确定”立即开始加工";
            this.checkEditStart.Size = new System.Drawing.Size(152, 19);
            this.checkEditStart.TabIndex = 33;
            // 
            // ucInputCnt
            // 
            this.ucInputCnt.Format = null;
            this.ucInputCnt.IsInterger = true;
            this.ucInputCnt.Location = new System.Drawing.Point(92, 84);
            this.ucInputCnt.Margin = new System.Windows.Forms.Padding(0);
            this.ucInputCnt.Max = 1000D;
            this.ucInputCnt.Min = 1D;
            this.ucInputCnt.Name = "ucInputCnt";
            this.ucInputCnt.Number = 2D;
            this.ucInputCnt.ReadOnly = false;
            this.ucInputCnt.Size = new System.Drawing.Size(81, 20);
            this.ucInputCnt.Suffix = null;
            this.ucInputCnt.TabIndex = 34;
            this.ucInputCnt.TextSize = 9F;
            // 
            // ucInputInterval
            // 
            this.ucInputInterval.Format = null;
            this.ucInputInterval.IsInterger = false;
            this.ucInputInterval.Location = new System.Drawing.Point(92, 110);
            this.ucInputInterval.Margin = new System.Windows.Forms.Padding(0);
            this.ucInputInterval.Max = 100D;
            this.ucInputInterval.Min = 1D;
            this.ucInputInterval.Name = "ucInputInterval";
            this.ucInputInterval.Number = 0D;
            this.ucInputInterval.ReadOnly = false;
            this.ucInputInterval.Size = new System.Drawing.Size(81, 20);
            this.ucInputInterval.Suffix = null;
            this.ucInputInterval.TabIndex = 35;
            this.ucInputInterval.TextSize = 9F;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(176, 87);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(12, 14);
            this.labelControl6.TabIndex = 36;
            this.labelControl6.Text = "次";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(176, 113);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(12, 14);
            this.labelControl7.TabIndex = 37;
            this.labelControl7.Text = "秒";
            // 
            // FrmCircleConfig
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 279);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.ucInputInterval);
            this.Controls.Add(this.ucInputCnt);
            this.Controls.Add(this.checkEditStart);
            this.Controls.Add(this.checkEditClear);
            this.Controls.Add(this.radioGroupModel);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmCircleConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "循环加工设置";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditClear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditStart.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnAbort;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.RadioGroup radioGroupModel;
        private DevExpress.XtraEditors.CheckEdit checkEditClear;
        private DevExpress.XtraEditors.CheckEdit checkEditStart;
        private ControlLibrary.Common.UCNumberInputer ucInputCnt;
        private ControlLibrary.Common.UCNumberInputer ucInputInterval;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
    }
}