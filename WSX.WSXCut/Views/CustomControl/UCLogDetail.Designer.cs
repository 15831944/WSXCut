namespace WSX.WSXCut.Views.CustomControl
{
    partial class UCLogDetail
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCLogDetail));
            this.tabLogDetailInfo = new DevExpress.XtraTab.XtraTabControl();
            this.tabDrawInfos = new DevExpress.XtraTab.XtraTabPage();
            this.rtxDrawInfos = new System.Windows.Forms.RichTextBox();
            this.tabSystemInfos = new DevExpress.XtraTab.XtraTabPage();
            this.rtxSystemInfos = new System.Windows.Forms.RichTextBox();
            this.tabAlarmInfos = new DevExpress.XtraTab.XtraTabPage();
            this.dgvAlarmInfos = new System.Windows.Forms.DataGridView();
            this.cTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAlarmInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tabLogDetailInfo)).BeginInit();
            this.tabLogDetailInfo.SuspendLayout();
            this.tabDrawInfos.SuspendLayout();
            this.tabSystemInfos.SuspendLayout();
            this.tabAlarmInfos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarmInfos)).BeginInit();
            this.SuspendLayout();
            // 
            // tabLogDetailInfo
            // 
            this.tabLogDetailInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLogDetailInfo.Location = new System.Drawing.Point(0, 0);
            this.tabLogDetailInfo.Name = "tabLogDetailInfo";
            this.tabLogDetailInfo.SelectedTabPage = this.tabDrawInfos;
            this.tabLogDetailInfo.Size = new System.Drawing.Size(965, 150);
            this.tabLogDetailInfo.TabIndex = 0;
            this.tabLogDetailInfo.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabDrawInfos,
            this.tabSystemInfos,
            this.tabAlarmInfos});
            // 
            // tabDrawInfos
            // 
            this.tabDrawInfos.Controls.Add(this.rtxDrawInfos);
            this.tabDrawInfos.Image = ((System.Drawing.Image)(resources.GetObject("tabDrawInfos.Image")));
            this.tabDrawInfos.Name = "tabDrawInfos";
            this.tabDrawInfos.Size = new System.Drawing.Size(959, 119);
            this.tabDrawInfos.Text = "绘图";
            // 
            // rtxDrawInfos
            // 
            this.rtxDrawInfos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxDrawInfos.Font = new System.Drawing.Font("Tahoma", 10F);
            this.rtxDrawInfos.Location = new System.Drawing.Point(0, 0);
            this.rtxDrawInfos.Name = "rtxDrawInfos";
            this.rtxDrawInfos.Size = new System.Drawing.Size(959, 119);
            this.rtxDrawInfos.TabIndex = 0;
            this.rtxDrawInfos.Text = "";
            this.rtxDrawInfos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtxDrawInfos_KeyDown);
            this.rtxDrawInfos.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.rtxDrawInfos_PreviewKeyDown);
            // 
            // tabSystemInfos
            // 
            this.tabSystemInfos.Controls.Add(this.rtxSystemInfos);
            this.tabSystemInfos.Image = ((System.Drawing.Image)(resources.GetObject("tabSystemInfos.Image")));
            this.tabSystemInfos.Name = "tabSystemInfos";
            this.tabSystemInfos.Size = new System.Drawing.Size(959, 119);
            this.tabSystemInfos.Text = "系统";
            // 
            // rtxSystemInfos
            // 
            this.rtxSystemInfos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxSystemInfos.HideSelection = false;
            this.rtxSystemInfos.Location = new System.Drawing.Point(0, 0);
            this.rtxSystemInfos.Name = "rtxSystemInfos";
            this.rtxSystemInfos.ReadOnly = true;
            this.rtxSystemInfos.Size = new System.Drawing.Size(959, 119);
            this.rtxSystemInfos.TabIndex = 0;
            this.rtxSystemInfos.Text = "";
            // 
            // tabAlarmInfos
            // 
            this.tabAlarmInfos.Controls.Add(this.dgvAlarmInfos);
            this.tabAlarmInfos.Image = ((System.Drawing.Image)(resources.GetObject("tabAlarmInfos.Image")));
            this.tabAlarmInfos.Name = "tabAlarmInfos";
            this.tabAlarmInfos.Size = new System.Drawing.Size(959, 119);
            this.tabAlarmInfos.Text = "报警";
            // 
            // dgvAlarmInfos
            // 
            this.dgvAlarmInfos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlarmInfos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cTime,
            this.cAlarmInfo,
            this.cID,
            this.cStatus,
            this.cOperation});
            this.dgvAlarmInfos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlarmInfos.Location = new System.Drawing.Point(0, 0);
            this.dgvAlarmInfos.Name = "dgvAlarmInfos";
            this.dgvAlarmInfos.RowTemplate.Height = 23;
            this.dgvAlarmInfos.Size = new System.Drawing.Size(959, 119);
            this.dgvAlarmInfos.TabIndex = 0;
            // 
            // cTime
            // 
            this.cTime.HeaderText = "时间";
            this.cTime.Name = "cTime";
            // 
            // cAlarmInfo
            // 
            this.cAlarmInfo.HeaderText = "报警信息";
            this.cAlarmInfo.Name = "cAlarmInfo";
            // 
            // cID
            // 
            this.cID.HeaderText = "ID";
            this.cID.Name = "cID";
            // 
            // cStatus
            // 
            this.cStatus.HeaderText = "状态";
            this.cStatus.Name = "cStatus";
            // 
            // cOperation
            // 
            this.cOperation.HeaderText = "操作";
            this.cOperation.Name = "cOperation";
            // 
            // UCLogDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabLogDetailInfo);
            this.Name = "UCLogDetail";
            this.Size = new System.Drawing.Size(965, 150);
            this.Load += new System.EventHandler(this.UCLogDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabLogDetailInfo)).EndInit();
            this.tabLogDetailInfo.ResumeLayout(false);
            this.tabDrawInfos.ResumeLayout(false);
            this.tabSystemInfos.ResumeLayout(false);
            this.tabAlarmInfos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarmInfos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabLogDetailInfo;
        private DevExpress.XtraTab.XtraTabPage tabDrawInfos;
        private DevExpress.XtraTab.XtraTabPage tabSystemInfos;
        private DevExpress.XtraTab.XtraTabPage tabAlarmInfos;
        private System.Windows.Forms.RichTextBox rtxDrawInfos;
        private System.Windows.Forms.RichTextBox rtxSystemInfos;
        private System.Windows.Forms.DataGridView dgvAlarmInfos;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAlarmInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOperation;
    }
}
