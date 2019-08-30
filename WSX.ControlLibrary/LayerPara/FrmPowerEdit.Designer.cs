namespace WSX.ControlLibrary.LayerPara
{
    partial class FrmPowerEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPowerEdit));
            this.tabControlEditor = new DevExpress.XtraTab.XtraTabControl();
            this.tabPagePower = new DevExpress.XtraTab.XtraTabPage();
            this.panelPwr = new System.Windows.Forms.Panel();
            this.xtraTabPageFreq = new DevExpress.XtraTab.XtraTabPage();
            this.panelFreq = new System.Windows.Forms.Panel();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoad = new DevExpress.XtraEditors.SimpleButton();
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlEditor)).BeginInit();
            this.tabControlEditor.SuspendLayout();
            this.tabPagePower.SuspendLayout();
            this.xtraTabPageFreq.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlEditor
            // 
            this.tabControlEditor.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.tabControlEditor.Appearance.Options.UseBackColor = true;
            this.tabControlEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlEditor.Location = new System.Drawing.Point(0, 0);
            this.tabControlEditor.Name = "tabControlEditor";
            this.tabControlEditor.SelectedTabPage = this.tabPagePower;
            this.tabControlEditor.Size = new System.Drawing.Size(659, 326);
            this.tabControlEditor.TabIndex = 0;
            this.tabControlEditor.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPagePower,
            this.xtraTabPageFreq});
            this.tabControlEditor.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabControlEditor_SelectedPageChanged);
            // 
            // tabPagePower
            // 
            this.tabPagePower.Appearance.PageClient.BackColor = System.Drawing.Color.Silver;
            this.tabPagePower.Appearance.PageClient.Options.UseBackColor = true;
            this.tabPagePower.Controls.Add(this.panelPwr);
            this.tabPagePower.Name = "tabPagePower";
            this.tabPagePower.Size = new System.Drawing.Size(653, 297);
            this.tabPagePower.Text = "功率";
            // 
            // panelPwr
            // 
            this.panelPwr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.panelPwr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPwr.Location = new System.Drawing.Point(0, 0);
            this.panelPwr.Name = "panelPwr";
            this.panelPwr.Size = new System.Drawing.Size(653, 297);
            this.panelPwr.TabIndex = 0;
            // 
            // xtraTabPageFreq
            // 
            this.xtraTabPageFreq.Appearance.PageClient.BackColor = System.Drawing.Color.Gainsboro;
            this.xtraTabPageFreq.Appearance.PageClient.Options.UseBackColor = true;
            this.xtraTabPageFreq.Controls.Add(this.panelFreq);
            this.xtraTabPageFreq.Name = "xtraTabPageFreq";
            this.xtraTabPageFreq.Size = new System.Drawing.Size(653, 297);
            this.xtraTabPageFreq.Text = "频率";
            // 
            // panelFreq
            // 
            this.panelFreq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.panelFreq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFreq.Location = new System.Drawing.Point(0, 0);
            this.panelFreq.Name = "panelFreq";
            this.panelFreq.Size = new System.Drawing.Size(653, 297);
            this.panelFreq.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 332);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存到文件";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Image = global::WSX.ControlLibrary.Properties.Resources.next_16x16;
            this.btnCopy.Location = new System.Drawing.Point(244, 332);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(100, 23);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "复制到频率";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(118, 332);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(100, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "从文件读取";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirm.Image")));
            this.btnConfirm.Location = new System.Drawing.Point(491, 332);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(572, 332);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmPowerEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 366);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControlEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmPowerEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "功率曲线编辑";
            ((System.ComponentModel.ISupportInitialize)(this.tabControlEditor)).EndInit();
            this.tabControlEditor.ResumeLayout(false);
            this.tabPagePower.ResumeLayout(false);
            this.xtraTabPageFreq.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabControlEditor;
        private DevExpress.XtraTab.XtraTabPage tabPagePower;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageFreq;
        private System.Windows.Forms.Panel panelFreq;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCopy;
        private DevExpress.XtraEditors.SimpleButton btnLoad;
        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.Panel panelPwr;
    }
}