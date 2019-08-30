namespace WSX.WSXCut.Views.CustomControl.RightPanel
{
    partial class UCMachineCount
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
            this.OnDisposing();
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCurTime = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalTime = new DevExpress.XtraEditors.LabelControl();
            this.btnManager = new DevExpress.XtraEditors.SimpleButton();
            this.lblPlanTotalCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblFinishCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCurTime);
            this.groupBox1.Controls.Add(this.lblTotalTime);
            this.groupBox1.Controls.Add(this.btnManager);
            this.groupBox1.Controls.Add(this.lblPlanTotalCount);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.lblFinishCount);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "加工计数";
            // 
            // lblCurTime
            // 
            this.lblCurTime.Location = new System.Drawing.Point(67, 32);
            this.lblCurTime.Name = "lblCurTime";
            this.lblCurTime.Size = new System.Drawing.Size(90, 14);
            this.lblCurTime.TabIndex = 4;
            this.lblCurTime.Text = "10小时10分10秒";
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalTime.Appearance.Options.UseForeColor = true;
            this.lblTotalTime.Location = new System.Drawing.Point(161, 32);
            this.lblTotalTime.Margin = new System.Windows.Forms.Padding(5, 3, 3, 0);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(100, 14);
            this.lblTotalTime.TabIndex = 4;
            this.lblTotalTime.Text = "[10小时10分10秒]";
            this.lblTotalTime.Visible = false;
            // 
            // btnManager
            // 
            this.btnManager.Location = new System.Drawing.Point(165, 68);
            this.btnManager.Margin = new System.Windows.Forms.Padding(0);
            this.btnManager.Name = "btnManager";
            this.btnManager.Size = new System.Drawing.Size(69, 25);
            this.btnManager.TabIndex = 5;
            this.btnManager.Text = "管理";
            // 
            // lblPlanTotalCount
            // 
            this.lblPlanTotalCount.Location = new System.Drawing.Point(68, 73);
            this.lblPlanTotalCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblPlanTotalCount.Name = "lblPlanTotalCount";
            this.lblPlanTotalCount.Size = new System.Drawing.Size(21, 14);
            this.lblPlanTotalCount.TabIndex = 4;
            this.lblPlanTotalCount.Text = "100";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 73);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "计划数量：";
            // 
            // lblFinishCount
            // 
            this.lblFinishCount.Location = new System.Drawing.Point(68, 52);
            this.lblFinishCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblFinishCount.Name = "lblFinishCount";
            this.lblFinishCount.Size = new System.Drawing.Size(7, 14);
            this.lblFinishCount.TabIndex = 4;
            this.lblFinishCount.Text = "0";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(33, 52);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "计件：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(33, 31);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "计时：";
            // 
            // UCMachineCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UCMachineCount";
            this.Size = new System.Drawing.Size(266, 116);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblPlanTotalCount;
        private DevExpress.XtraEditors.LabelControl lblFinishCount;
        private DevExpress.XtraEditors.LabelControl lblCurTime;
        private DevExpress.XtraEditors.SimpleButton btnManager;
        private DevExpress.XtraEditors.LabelControl lblTotalTime;
    }
}
