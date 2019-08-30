namespace WSX.WSXCut.Views.Forms
{
    partial class FrmArcFlyingCut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmArcFlyingCut));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.ckFirstSort = new DevExpress.XtraEditors.CheckEdit();
            this.ckFlyingByPart = new DevExpress.XtraEditors.CheckEdit();
            this.cmbSortTypes = new DevExpress.XtraEditors.ComboBoxEdit();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.txtMaxConnectSpace = new WSX.ControlLibrary.Common.UCNumberInputer();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckFirstSort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckFlyingByPart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSortTypes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 262);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(422, 56);
            this.panelControl2.TabIndex = 28;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(320, 10);
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
            this.btnOK.Location = new System.Drawing.Point(205, 10);
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
            this.panelControl1.Size = new System.Drawing.Size(422, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "本功能实现对圆按顺序进行飞行切割。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(120, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "圆弧飞行切割";
            // 
            // labelControl26
            // 
            this.labelControl26.Appearance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelControl26.Appearance.Options.UseImageAlign = true;
            this.labelControl26.Appearance.Options.UseTextOptions = true;
            this.labelControl26.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl26.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl26.Location = new System.Drawing.Point(63, 115);
            this.labelControl26.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(132, 14);
            this.labelControl26.TabIndex = 30;
            this.labelControl26.Text = "飞行连接两圆最大距离：";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("labelControl4.Appearance.Image")));
            this.labelControl4.Appearance.Options.UseImage = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.labelControl4.Location = new System.Drawing.Point(304, 107);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(32, 32);
            this.labelControl4.TabIndex = 30;
            // 
            // ckFirstSort
            // 
            this.ckFirstSort.Location = new System.Drawing.Point(63, 160);
            this.ckFirstSort.Margin = new System.Windows.Forms.Padding(0);
            this.ckFirstSort.Name = "ckFirstSort";
            this.ckFirstSort.Properties.AutoWidth = true;
            this.ckFirstSort.Properties.Caption = "圆弧先排序在飞切";
            this.ckFirstSort.Size = new System.Drawing.Size(118, 19);
            this.ckFirstSort.TabIndex = 32;
            // 
            // ckFlyingByPart
            // 
            this.ckFlyingByPart.Location = new System.Drawing.Point(63, 194);
            this.ckFlyingByPart.Margin = new System.Windows.Forms.Padding(0);
            this.ckFlyingByPart.Name = "ckFlyingByPart";
            this.ckFlyingByPart.Properties.AutoWidth = true;
            this.ckFlyingByPart.Properties.Caption = "按零件飞切";
            this.ckFlyingByPart.Size = new System.Drawing.Size(82, 19);
            this.ckFlyingByPart.TabIndex = 32;
            // 
            // cmbSortTypes
            // 
            this.cmbSortTypes.EditValue = "局部最短空移";
            this.cmbSortTypes.Location = new System.Drawing.Point(204, 159);
            this.cmbSortTypes.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.cmbSortTypes.Name = "cmbSortTypes";
            this.cmbSortTypes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSortTypes.Properties.Items.AddRange(new object[] {
            "局部最短空移",
            "从内到外",
            "从左到右",
            "从右到左",
            "从上到下",
            "从下到上",
            "顺时针",
            "逆时针"});
            this.cmbSortTypes.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSortTypes.Size = new System.Drawing.Size(99, 20);
            this.cmbSortTypes.TabIndex = 33;
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            // 
            // txtMaxConnectSpace
            // 
            this.txtMaxConnectSpace.Format = null;
            this.txtMaxConnectSpace.IsInterger = false;
            this.txtMaxConnectSpace.Location = new System.Drawing.Point(204, 113);
            this.txtMaxConnectSpace.Margin = new System.Windows.Forms.Padding(0);
            this.txtMaxConnectSpace.Max = 100D;
            this.txtMaxConnectSpace.Min = 0D;
            this.txtMaxConnectSpace.Name = "txtMaxConnectSpace";
            this.txtMaxConnectSpace.Number = 0D;
            this.txtMaxConnectSpace.ReadOnly = false;
            this.txtMaxConnectSpace.Size = new System.Drawing.Size(97, 22);
            this.txtMaxConnectSpace.Suffix = null;
            this.txtMaxConnectSpace.TabIndex = 31;
            this.txtMaxConnectSpace.TextSize = 9F;
            // 
            // FrmArcFlyingCut
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(422, 318);
            this.Controls.Add(this.cmbSortTypes);
            this.Controls.Add(this.ckFlyingByPart);
            this.Controls.Add(this.ckFirstSort);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl26);
            this.Controls.Add(this.txtMaxConnectSpace);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmArcFlyingCut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "飞行切割";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckFirstSort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckFlyingByPart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSortTypes.Properties)).EndInit();
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
        private ControlLibrary.Common.UCNumberInputer txtMaxConnectSpace;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckEdit ckFirstSort;
        private DevExpress.XtraEditors.CheckEdit ckFlyingByPart;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSortTypes;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
    }
}