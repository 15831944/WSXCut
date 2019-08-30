namespace WSX.ControlLibrary.LayerPara
{
    partial class UCPowerEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPowerEdit));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chartControlCurve = new WSX.ControlLibrary.Common.ChartControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbPatternPwr = new DevExpress.XtraEditors.ComboBoxEdit();
            this.checkAbs = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dgvCurve = new System.Windows.Forms.DataGridView();
            this.Speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Power = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnClearPwr = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemovePwr = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddPwr = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPatternPwr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkAbs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurve)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(653, 297);
            this.panel1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.chartControlCurve);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(186, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(467, 297);
            this.panel4.TabIndex = 0;
            // 
            // chartControlCurve
            // 
            this.chartControlCurve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControlCurve.EditEnabled = true;
            this.chartControlCurve.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chartControlCurve.Location = new System.Drawing.Point(0, 0);
            this.chartControlCurve.MinPointsCount = 2;
            this.chartControlCurve.Name = "chartControlCurve";
            this.chartControlCurve.Size = new System.Drawing.Size(467, 297);
            this.chartControlCurve.TabIndex = 0;
            this.chartControlCurve.XLengend = "Speed(%)";
            this.chartControlCurve.XMax = 100D;
            this.chartControlCurve.YLengend = "Power(%)";
            this.chartControlCurve.YMax = 100D;
            this.chartControlCurve.OnCurveDataChanged += new System.Action<object, WSX.ControlLibrary.Common.OnCurveChangedEventArgs>(this.chartControlCurve_OnCurveDataChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labelControl1);
            this.panel3.Controls.Add(this.cmbPatternPwr);
            this.panel3.Controls.Add(this.checkAbs);
            this.panel3.Controls.Add(this.panelControl1);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(186, 297);
            this.panel3.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 268);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "平滑方式：";
            // 
            // cmbPatternPwr
            // 
            this.cmbPatternPwr.Location = new System.Drawing.Point(69, 265);
            this.cmbPatternPwr.Name = "cmbPatternPwr";
            this.cmbPatternPwr.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPatternPwr.Properties.Items.AddRange(new object[] {
            "默认",
            "分段",
            "线性",
            "平滑"});
            this.cmbPatternPwr.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbPatternPwr.Size = new System.Drawing.Size(100, 20);
            this.cmbPatternPwr.TabIndex = 4;
            // 
            // checkAbs
            // 
            this.checkAbs.Location = new System.Drawing.Point(8, 241);
            this.checkAbs.Name = "checkAbs";
            this.checkAbs.Properties.AllowFocused = false;
            this.checkAbs.Properties.Caption = "绝对值显示";
            this.checkAbs.Size = new System.Drawing.Size(87, 19);
            this.checkAbs.TabIndex = 3;
            this.checkAbs.CheckedChanged += new System.EventHandler(this.checkAbs_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dgvCurve);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 34);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(186, 199);
            this.panelControl1.TabIndex = 0;
            // 
            // dgvCurve
            // 
            this.dgvCurve.AllowUserToAddRows = false;
            this.dgvCurve.AllowUserToDeleteRows = false;
            this.dgvCurve.AllowUserToResizeColumns = false;
            this.dgvCurve.AllowUserToResizeRows = false;
            this.dgvCurve.BackgroundColor = System.Drawing.Color.White;
            this.dgvCurve.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCurve.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurve.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Speed,
            this.Power});
            this.dgvCurve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCurve.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvCurve.Location = new System.Drawing.Point(2, 2);
            this.dgvCurve.MultiSelect = false;
            this.dgvCurve.Name = "dgvCurve";
            this.dgvCurve.RowHeadersVisible = false;
            this.dgvCurve.RowTemplate.Height = 23;
            this.dgvCurve.Size = new System.Drawing.Size(182, 195);
            this.dgvCurve.TabIndex = 0;
            this.dgvCurve.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvCurve_CellBeginEdit);
            this.dgvCurve.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCurve_CellEndEdit);
            // 
            // Speed
            // 
            this.Speed.HeaderText = "Speed";
            this.Speed.Name = "Speed";
            this.Speed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Speed.Width = 90;
            // 
            // Power
            // 
            this.Power.HeaderText = "Power";
            this.Power.Name = "Power";
            this.Power.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Power.Width = 90;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnClearPwr);
            this.panel5.Controls.Add(this.btnRemovePwr);
            this.panel5.Controls.Add(this.btnAddPwr);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(186, 34);
            this.panel5.TabIndex = 2;
            // 
            // btnClearPwr
            // 
            this.btnClearPwr.AllowFocus = false;
            this.btnClearPwr.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnClearPwr.Image = ((System.Drawing.Image)(resources.GetObject("btnClearPwr.Image")));
            this.btnClearPwr.Location = new System.Drawing.Point(117, 6);
            this.btnClearPwr.Name = "btnClearPwr";
            this.btnClearPwr.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnClearPwr.Size = new System.Drawing.Size(55, 21);
            this.btnClearPwr.TabIndex = 5;
            this.btnClearPwr.Text = "清除";
            this.btnClearPwr.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRemovePwr
            // 
            this.btnRemovePwr.AllowFocus = false;
            this.btnRemovePwr.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnRemovePwr.Image = ((System.Drawing.Image)(resources.GetObject("btnRemovePwr.Image")));
            this.btnRemovePwr.Location = new System.Drawing.Point(60, 6);
            this.btnRemovePwr.Name = "btnRemovePwr";
            this.btnRemovePwr.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnRemovePwr.Size = new System.Drawing.Size(55, 21);
            this.btnRemovePwr.TabIndex = 4;
            this.btnRemovePwr.Text = "删除";
            this.btnRemovePwr.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAddPwr
            // 
            this.btnAddPwr.AllowFocus = false;
            this.btnAddPwr.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.btnAddPwr.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPwr.Image")));
            this.btnAddPwr.Location = new System.Drawing.Point(3, 6);
            this.btnAddPwr.Name = "btnAddPwr";
            this.btnAddPwr.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnAddPwr.Size = new System.Drawing.Size(55, 21);
            this.btnAddPwr.TabIndex = 3;
            this.btnAddPwr.Text = "添加";
            this.btnAddPwr.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // UCPowerEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UCPowerEdit";
            this.Size = new System.Drawing.Size(653, 297);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPatternPwr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkAbs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurve)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private Common.ChartControl chartControlCurve;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPatternPwr;
        private DevExpress.XtraEditors.CheckEdit checkAbs;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.DataGridView dgvCurve;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Power;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraEditors.SimpleButton btnClearPwr;
        private DevExpress.XtraEditors.SimpleButton btnRemovePwr;
        private DevExpress.XtraEditors.SimpleButton btnAddPwr;
    }
}
