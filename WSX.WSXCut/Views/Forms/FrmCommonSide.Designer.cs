namespace WSX.WSXCut.Views.Forms
{
    partial class FrmCommonSide
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCommonSide));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtFrameFinal = new System.Windows.Forms.RadioButton();
            this.rbtFramedPriority = new System.Windows.Forms.RadioButton();
            this.rbtStairs = new System.Windows.Forms.RadioButton();
            this.rbtSerpentine = new System.Windows.Forms.RadioButton();
            this.rbtnHorizontalsAndVerticals = new System.Windows.Forms.RadioButton();
            this.mvvmContext1 = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.txtCutOutValue = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtRightBotton = new System.Windows.Forms.RadioButton();
            this.rbtRightTop = new System.Windows.Forms.RadioButton();
            this.rbtLeftBotton = new System.Windows.Forms.RadioButton();
            this.rbtLeftTop = new System.Windows.Forms.RadioButton();
            this.ckIsCutOut = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsCutOut.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl2.Controls.Add(this.btnCancel);
            this.panelControl2.Controls.Add(this.btnOK);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 234);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(509, 70);
            this.panelControl2.TabIndex = 28;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(387, 27);
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
            this.btnOK.Location = new System.Drawing.Point(272, 27);
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
            this.panelControl1.Size = new System.Drawing.Size(509, 77);
            this.panelControl1.TabIndex = 27;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.labelControl2.Location = new System.Drawing.Point(24, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(329, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "对矩形阵列进行共边，以提高切割效率。";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 23);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "矩形共边";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtFrameFinal);
            this.groupBox1.Controls.Add(this.rbtFramedPriority);
            this.groupBox1.Controls.Add(this.rbtStairs);
            this.groupBox1.Controls.Add(this.rbtSerpentine);
            this.groupBox1.Controls.Add(this.rbtnHorizontalsAndVerticals);
            this.groupBox1.Location = new System.Drawing.Point(33, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 118);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "共边样式";
            // 
            // rbtFrameFinal
            // 
            this.rbtFrameFinal.AutoSize = true;
            this.rbtFrameFinal.Location = new System.Drawing.Point(123, 51);
            this.rbtFrameFinal.Name = "rbtFrameFinal";
            this.rbtFrameFinal.Size = new System.Drawing.Size(73, 18);
            this.rbtFrameFinal.TabIndex = 3;
            this.rbtFrameFinal.Text = "外框最后";
            this.rbtFrameFinal.UseVisualStyleBackColor = true;
            this.rbtFrameFinal.CheckedChanged += new System.EventHandler(this.rbtFramedPriority_CheckedChanged);
            // 
            // rbtFramedPriority
            // 
            this.rbtFramedPriority.AutoSize = true;
            this.rbtFramedPriority.Location = new System.Drawing.Point(123, 27);
            this.rbtFramedPriority.Name = "rbtFramedPriority";
            this.rbtFramedPriority.Size = new System.Drawing.Size(73, 18);
            this.rbtFramedPriority.TabIndex = 2;
            this.rbtFramedPriority.Text = "外框最先";
            this.rbtFramedPriority.UseVisualStyleBackColor = true;
            this.rbtFramedPriority.CheckedChanged += new System.EventHandler(this.rbtFramedPriority_CheckedChanged);
            // 
            // rbtStairs
            // 
            this.rbtStairs.AutoSize = true;
            this.rbtStairs.Location = new System.Drawing.Point(7, 75);
            this.rbtStairs.Name = "rbtStairs";
            this.rbtStairs.Size = new System.Drawing.Size(85, 18);
            this.rbtStairs.TabIndex = 1;
            this.rbtStairs.Text = "逐个阶梯型";
            this.rbtStairs.UseVisualStyleBackColor = true;
            // 
            // rbtSerpentine
            // 
            this.rbtSerpentine.AutoSize = true;
            this.rbtSerpentine.Location = new System.Drawing.Point(7, 51);
            this.rbtSerpentine.Name = "rbtSerpentine";
            this.rbtSerpentine.Size = new System.Drawing.Size(73, 18);
            this.rbtSerpentine.TabIndex = 0;
            this.rbtSerpentine.Text = "逐个蛇形";
            this.rbtSerpentine.UseVisualStyleBackColor = true;
            // 
            // rbtnHorizontalsAndVerticals
            // 
            this.rbtnHorizontalsAndVerticals.AutoSize = true;
            this.rbtnHorizontalsAndVerticals.Location = new System.Drawing.Point(7, 27);
            this.rbtnHorizontalsAndVerticals.Name = "rbtnHorizontalsAndVerticals";
            this.rbtnHorizontalsAndVerticals.Size = new System.Drawing.Size(73, 18);
            this.rbtnHorizontalsAndVerticals.TabIndex = 0;
            this.rbtnHorizontalsAndVerticals.Text = "横平竖直";
            this.rbtnHorizontalsAndVerticals.UseVisualStyleBackColor = true;
            this.rbtnHorizontalsAndVerticals.CheckedChanged += new System.EventHandler(this.rbtnHorizontalsAndVerticals_CheckedChanged);
            // 
            // mvvmContext1
            // 
            this.mvvmContext1.ContainerControl = this;
            this.mvvmContext1.ViewModelType = typeof(WSX.ViewModels.MainViewModel);
            // 
            // txtCutOutValue
            // 
            this.txtCutOutValue.Format = null;
            this.txtCutOutValue.IsInterger = false;
            this.txtCutOutValue.Location = new System.Drawing.Point(347, 181);
            this.txtCutOutValue.Margin = new System.Windows.Forms.Padding(0);
            this.txtCutOutValue.Max = 360D;
            this.txtCutOutValue.Min = -360D;
            this.txtCutOutValue.Name = "txtCutOutValue";
            this.txtCutOutValue.Number = 0D;
            this.txtCutOutValue.ReadOnly = false;
            this.txtCutOutValue.Size = new System.Drawing.Size(97, 22);
            this.txtCutOutValue.Suffix = null;
            this.txtCutOutValue.TabIndex = 31;
            this.txtCutOutValue.TextSize = 9F;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtRightBotton);
            this.groupBox2.Controls.Add(this.rbtRightTop);
            this.groupBox2.Controls.Add(this.rbtLeftBotton);
            this.groupBox2.Controls.Add(this.rbtLeftTop);
            this.groupBox2.Location = new System.Drawing.Point(259, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 79);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "起始位置";
            // 
            // rbtRightBotton
            // 
            this.rbtRightBotton.AutoSize = true;
            this.rbtRightBotton.Location = new System.Drawing.Point(88, 51);
            this.rbtRightBotton.Name = "rbtRightBotton";
            this.rbtRightBotton.Size = new System.Drawing.Size(49, 18);
            this.rbtRightBotton.TabIndex = 3;
            this.rbtRightBotton.Text = "右下";
            this.rbtRightBotton.UseVisualStyleBackColor = true;
            // 
            // rbtRightTop
            // 
            this.rbtRightTop.AutoSize = true;
            this.rbtRightTop.Location = new System.Drawing.Point(88, 27);
            this.rbtRightTop.Name = "rbtRightTop";
            this.rbtRightTop.Size = new System.Drawing.Size(49, 18);
            this.rbtRightTop.TabIndex = 2;
            this.rbtRightTop.Text = "右上";
            this.rbtRightTop.UseVisualStyleBackColor = true;
            // 
            // rbtLeftBotton
            // 
            this.rbtLeftBotton.AutoSize = true;
            this.rbtLeftBotton.Location = new System.Drawing.Point(7, 51);
            this.rbtLeftBotton.Name = "rbtLeftBotton";
            this.rbtLeftBotton.Size = new System.Drawing.Size(49, 18);
            this.rbtLeftBotton.TabIndex = 0;
            this.rbtLeftBotton.Text = "左下";
            this.rbtLeftBotton.UseVisualStyleBackColor = true;
            // 
            // rbtLeftTop
            // 
            this.rbtLeftTop.AutoSize = true;
            this.rbtLeftTop.Checked = true;
            this.rbtLeftTop.Location = new System.Drawing.Point(7, 27);
            this.rbtLeftTop.Name = "rbtLeftTop";
            this.rbtLeftTop.Size = new System.Drawing.Size(49, 18);
            this.rbtLeftTop.TabIndex = 0;
            this.rbtLeftTop.TabStop = true;
            this.rbtLeftTop.Text = "左上";
            this.rbtLeftTop.UseVisualStyleBackColor = true;
            // 
            // ckIsCutOut
            // 
            this.ckIsCutOut.EditValue = true;
            this.ckIsCutOut.Location = new System.Drawing.Point(266, 179);
            this.ckIsCutOut.Margin = new System.Windows.Forms.Padding(0);
            this.ckIsCutOut.Name = "ckIsCutOut";
            this.ckIsCutOut.Properties.AutoWidth = true;
            this.ckIsCutOut.Properties.Caption = "启用过切";
            this.ckIsCutOut.Size = new System.Drawing.Size(70, 19);
            this.ckIsCutOut.TabIndex = 34;
            // 
            // FrmCommonSide
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(509, 304);
            this.Controls.Add(this.ckIsCutOut);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtCutOutValue);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCommonSide";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "矩形共边";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckIsCutOut.Properties)).EndInit();
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
        private System.Windows.Forms.RadioButton rbtSerpentine;
        private System.Windows.Forms.RadioButton rbtnHorizontalsAndVerticals;
        private ControlLibrary.Common.UCNumberInputer txtCutOutValue;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext1;
        private System.Windows.Forms.RadioButton rbtFrameFinal;
        private System.Windows.Forms.RadioButton rbtFramedPriority;
        private System.Windows.Forms.RadioButton rbtStairs;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtRightBotton;
        private System.Windows.Forms.RadioButton rbtRightTop;
        private System.Windows.Forms.RadioButton rbtLeftBotton;
        private System.Windows.Forms.RadioButton rbtLeftTop;
        private DevExpress.XtraEditors.CheckEdit ckIsCutOut;
    }
}