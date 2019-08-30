namespace WSX.WSXCut.Views.CustomControl.RightPanel
{
    partial class UCMachineControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMachineControl));
            this.gbMachineControl = new System.Windows.Forms.GroupBox();
            this.cmbReturnPoint = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.checkEdgeDetection = new DevExpress.XtraEditors.CheckEdit();
            this.checkSoftLimit = new DevExpress.XtraEditors.CheckEdit();
            this.checkSelected = new DevExpress.XtraEditors.CheckEdit();
            this.checkReturnAfterStop = new DevExpress.XtraEditors.CheckEdit();
            this.checkReturn = new DevExpress.XtraEditors.CheckEdit();
            this.btnZero = new DevExpress.XtraEditors.SimpleButton();
            this.btnBreakPointStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnEmpty = new DevExpress.XtraEditors.SimpleButton();
            this.btnStop = new DevExpress.XtraEditors.SimpleButton();
            this.btnForward = new DevExpress.XtraEditors.SimpleButton();
            this.btnLocate = new DevExpress.XtraEditors.SimpleButton();
            this.btnSimulate = new DevExpress.XtraEditors.SimpleButton();
            this.btnPause = new DevExpress.XtraEditors.SimpleButton();
            this.btnBackward = new DevExpress.XtraEditors.SimpleButton();
            this.btnCircle = new DevExpress.XtraEditors.SimpleButton();
            this.btnOutline = new DevExpress.XtraEditors.SimpleButton();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.btnFastStart = new DevExpress.XtraEditors.SimpleButton();
            this.ucInputStep = new WSX.ControlLibrary.Common.UCNumberInputer();
            this.ucInputSpeed = new WSX.ControlLibrary.Common.SpeedInputer();
            this.gbMachineControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReturnPoint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdgeDetection.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSoftLimit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSelected.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkReturnAfterStop.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkReturn.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gbMachineControl
            // 
            this.gbMachineControl.Controls.Add(this.ucInputStep);
            this.gbMachineControl.Controls.Add(this.ucInputSpeed);
            this.gbMachineControl.Controls.Add(this.cmbReturnPoint);
            this.gbMachineControl.Controls.Add(this.labelControl1);
            this.gbMachineControl.Controls.Add(this.checkEdgeDetection);
            this.gbMachineControl.Controls.Add(this.checkSoftLimit);
            this.gbMachineControl.Controls.Add(this.checkSelected);
            this.gbMachineControl.Controls.Add(this.checkReturnAfterStop);
            this.gbMachineControl.Controls.Add(this.checkReturn);
            this.gbMachineControl.Controls.Add(this.btnZero);
            this.gbMachineControl.Controls.Add(this.btnBreakPointStart);
            this.gbMachineControl.Controls.Add(this.btnEmpty);
            this.gbMachineControl.Controls.Add(this.btnStop);
            this.gbMachineControl.Controls.Add(this.btnForward);
            this.gbMachineControl.Controls.Add(this.btnLocate);
            this.gbMachineControl.Controls.Add(this.btnSimulate);
            this.gbMachineControl.Controls.Add(this.btnPause);
            this.gbMachineControl.Controls.Add(this.btnBackward);
            this.gbMachineControl.Controls.Add(this.btnCircle);
            this.gbMachineControl.Controls.Add(this.btnOutline);
            this.gbMachineControl.Controls.Add(this.btnStart);
            this.gbMachineControl.Controls.Add(this.btnFastStart);
            this.gbMachineControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMachineControl.Font = new System.Drawing.Font("Tahoma", 9F);
            this.gbMachineControl.Location = new System.Drawing.Point(0, 0);
            this.gbMachineControl.Name = "gbMachineControl";
            this.gbMachineControl.Size = new System.Drawing.Size(258, 266);
            this.gbMachineControl.TabIndex = 0;
            this.gbMachineControl.TabStop = false;
            this.gbMachineControl.Text = "加工控制";
            // 
            // cmbReturnPoint
            // 
            this.cmbReturnPoint.EditValue = "零点";
            this.cmbReturnPoint.Location = new System.Drawing.Point(171, 137);
            this.cmbReturnPoint.Name = "cmbReturnPoint";
            this.cmbReturnPoint.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbReturnPoint.Properties.Items.AddRange(new object[] {
            "零点",
            "起点",
            "终点",
            "原点",
            "标记1",
            "标记2",
            "标记3",
            "标记4",
            "标记5",
            "标记6"});
            this.cmbReturnPoint.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbReturnPoint.Size = new System.Drawing.Size(80, 20);
            this.cmbReturnPoint.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(7, 239);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 14);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "回退、前进距离：";
            // 
            // checkEdgeDetection
            // 
            this.checkEdgeDetection.Location = new System.Drawing.Point(7, 214);
            this.checkEdgeDetection.Name = "checkEdgeDetection";
            this.checkEdgeDetection.Properties.AutoWidth = true;
            this.checkEdgeDetection.Properties.Caption = "加工前自动寻边";
            this.checkEdgeDetection.Size = new System.Drawing.Size(106, 19);
            this.checkEdgeDetection.TabIndex = 5;
            // 
            // checkSoftLimit
            // 
            this.checkSoftLimit.Location = new System.Drawing.Point(7, 195);
            this.checkSoftLimit.Name = "checkSoftLimit";
            this.checkSoftLimit.Properties.AutoWidth = true;
            this.checkSoftLimit.Properties.Caption = "启用软限位保护";
            this.checkSoftLimit.Size = new System.Drawing.Size(106, 19);
            this.checkSoftLimit.TabIndex = 4;
            this.checkSoftLimit.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.checkSoftLimit_EditValueChanging);
            // 
            // checkSelected
            // 
            this.checkSelected.Location = new System.Drawing.Point(7, 176);
            this.checkSelected.Name = "checkSelected";
            this.checkSelected.Properties.AutoWidth = true;
            this.checkSelected.Properties.Caption = "只加工选中的图形";
            this.checkSelected.Size = new System.Drawing.Size(118, 19);
            this.checkSelected.TabIndex = 3;
            // 
            // checkReturnAfterStop
            // 
            this.checkReturnAfterStop.Location = new System.Drawing.Point(7, 157);
            this.checkReturnAfterStop.Name = "checkReturnAfterStop";
            this.checkReturnAfterStop.Properties.AutoWidth = true;
            this.checkReturnAfterStop.Properties.Caption = "单击停止自动回零";
            this.checkReturnAfterStop.Size = new System.Drawing.Size(118, 19);
            this.checkReturnAfterStop.TabIndex = 2;
            // 
            // checkReturn
            // 
            this.checkReturn.Location = new System.Drawing.Point(7, 138);
            this.checkReturn.Name = "checkReturn";
            this.checkReturn.Properties.AutoWidth = true;
            this.checkReturn.Properties.Caption = "加工完成自动返回";
            this.checkReturn.Size = new System.Drawing.Size(118, 19);
            this.checkReturn.TabIndex = 0;
            // 
            // btnZero
            // 
            this.btnZero.AllowFocus = false;
            this.btnZero.Location = new System.Drawing.Point(171, 104);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(80, 25);
            this.btnZero.TabIndex = 1;
            this.btnZero.Text = "回零";
            // 
            // btnBreakPointStart
            // 
            this.btnBreakPointStart.AllowFocus = false;
            this.btnBreakPointStart.Image = ((System.Drawing.Image)(resources.GetObject("btnBreakPointStart.Image")));
            this.btnBreakPointStart.Location = new System.Drawing.Point(171, 77);
            this.btnBreakPointStart.Name = "btnBreakPointStart";
            this.btnBreakPointStart.Size = new System.Drawing.Size(80, 25);
            this.btnBreakPointStart.TabIndex = 1;
            this.btnBreakPointStart.Text = "断点继续";
            // 
            // btnEmpty
            // 
            this.btnEmpty.AllowFocus = false;
            this.btnEmpty.Image = ((System.Drawing.Image)(resources.GetObject("btnEmpty.Image")));
            this.btnEmpty.Location = new System.Drawing.Point(171, 50);
            this.btnEmpty.Name = "btnEmpty";
            this.btnEmpty.Size = new System.Drawing.Size(80, 25);
            this.btnEmpty.TabIndex = 1;
            this.btnEmpty.Text = "空走";
            // 
            // btnStop
            // 
            this.btnStop.AllowFocus = false;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(171, 23);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 25);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            // 
            // btnForward
            // 
            this.btnForward.AllowFocus = false;
            this.btnForward.Image = ((System.Drawing.Image)(resources.GetObject("btnForward.Image")));
            this.btnForward.Location = new System.Drawing.Point(89, 104);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(80, 25);
            this.btnForward.TabIndex = 1;
            this.btnForward.Text = "前进";
            // 
            // btnLocate
            // 
            this.btnLocate.AllowFocus = false;
            this.btnLocate.Image = ((System.Drawing.Image)(resources.GetObject("btnLocate.Image")));
            this.btnLocate.Location = new System.Drawing.Point(89, 77);
            this.btnLocate.Name = "btnLocate";
            this.btnLocate.Size = new System.Drawing.Size(80, 25);
            this.btnLocate.TabIndex = 1;
            this.btnLocate.Text = "断点定位";
            // 
            // btnSimulate
            // 
            this.btnSimulate.AllowFocus = false;
            this.btnSimulate.Image = ((System.Drawing.Image)(resources.GetObject("btnSimulate.Image")));
            this.btnSimulate.Location = new System.Drawing.Point(89, 50);
            this.btnSimulate.Name = "btnSimulate";
            this.btnSimulate.Size = new System.Drawing.Size(80, 25);
            this.btnSimulate.TabIndex = 1;
            this.btnSimulate.Text = "模拟";
            // 
            // btnPause
            // 
            this.btnPause.AllowFocus = false;
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(89, 23);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(80, 25);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = "暂停";
            // 
            // btnBackward
            // 
            this.btnBackward.AllowFocus = false;
            this.btnBackward.Image = ((System.Drawing.Image)(resources.GetObject("btnBackward.Image")));
            this.btnBackward.Location = new System.Drawing.Point(7, 104);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(80, 25);
            this.btnBackward.TabIndex = 1;
            this.btnBackward.Text = "回退";
            // 
            // btnCircle
            // 
            this.btnCircle.AllowFocus = false;
            this.btnCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnCircle.Image")));
            this.btnCircle.Location = new System.Drawing.Point(7, 77);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(80, 25);
            this.btnCircle.TabIndex = 1;
            this.btnCircle.Text = "循环加工";
            // 
            // btnOutline
            // 
            this.btnOutline.AllowFocus = false;
            this.btnOutline.Image = ((System.Drawing.Image)(resources.GetObject("btnOutline.Image")));
            this.btnOutline.Location = new System.Drawing.Point(7, 50);
            this.btnOutline.Name = "btnOutline";
            this.btnOutline.Size = new System.Drawing.Size(80, 25);
            this.btnOutline.TabIndex = 1;
            this.btnOutline.Text = "走边框";
            // 
            // btnStart
            // 
            this.btnStart.AllowFocus = false;
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.Location = new System.Drawing.Point(7, 23);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(80, 25);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "开始";
            // 
            // btnFastStart
            // 
            this.btnFastStart.AllowFocus = false;
            this.btnFastStart.Location = new System.Drawing.Point(7, 50);
            this.btnFastStart.Name = "btnFastStart";
            this.btnFastStart.Size = new System.Drawing.Size(162, 25);
            this.btnFastStart.TabIndex = 9;
            this.btnFastStart.Text = "不穿孔快速继续";
            this.btnFastStart.Visible = false;
            // 
            // ucInputStep
            // 
            this.ucInputStep.Appearance.Font = new System.Drawing.Font("Tahoma", 7F);
            this.ucInputStep.Appearance.Options.UseFont = true;
            this.ucInputStep.Format = null;
            this.ucInputStep.IsInterger = false;
            this.ucInputStep.Location = new System.Drawing.Point(101, 237);
            this.ucInputStep.Margin = new System.Windows.Forms.Padding(0);
            this.ucInputStep.Max = 100D;
            this.ucInputStep.Min = 0D;
            this.ucInputStep.Name = "ucInputStep";
            this.ucInputStep.Number = 10D;
            this.ucInputStep.ReadOnly = false;
            this.ucInputStep.Size = new System.Drawing.Size(65, 22);
            this.ucInputStep.Suffix = "毫米";
            this.ucInputStep.TabIndex = 7;
            this.ucInputStep.TextSize = 8F;
            // 
            // ucInputSpeed
            // 
            this.ucInputSpeed.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ucInputSpeed.Appearance.Options.UseFont = true;
            this.ucInputSpeed.Format = "0.###";
            this.ucInputSpeed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ucInputSpeed.IsInterger = false;
            this.ucInputSpeed.Location = new System.Drawing.Point(171, 237);
            this.ucInputSpeed.Margin = new System.Windows.Forms.Padding(0);
            this.ucInputSpeed.Max = 100D;
            this.ucInputSpeed.Min = 0D;
            this.ucInputSpeed.Name = "ucInputSpeed";
            this.ucInputSpeed.Number = 10D;
            this.ucInputSpeed.ReadOnly = false;
            this.ucInputSpeed.Size = new System.Drawing.Size(80, 20);
            this.ucInputSpeed.Suffix = "毫米/秒";
            this.ucInputSpeed.TabIndex = 8;
            this.ucInputSpeed.Tag = "Speed";
            this.ucInputSpeed.TextSize = 8F;
            // 
            // UCMachineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbMachineControl);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Name = "UCMachineControl";
            this.Size = new System.Drawing.Size(258, 266);
            this.gbMachineControl.ResumeLayout(false);
            this.gbMachineControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbReturnPoint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdgeDetection.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSoftLimit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkSelected.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkReturnAfterStop.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkReturn.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMachineControl;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit checkEdgeDetection;
        private DevExpress.XtraEditors.CheckEdit checkSoftLimit;
        private DevExpress.XtraEditors.CheckEdit checkSelected;
        private DevExpress.XtraEditors.CheckEdit checkReturnAfterStop;
        private DevExpress.XtraEditors.CheckEdit checkReturn;
        private DevExpress.XtraEditors.SimpleButton btnZero;
        private DevExpress.XtraEditors.SimpleButton btnBreakPointStart;
        private DevExpress.XtraEditors.SimpleButton btnEmpty;
        private DevExpress.XtraEditors.SimpleButton btnStop;
        private DevExpress.XtraEditors.SimpleButton btnForward;
        private DevExpress.XtraEditors.SimpleButton btnLocate;
        private DevExpress.XtraEditors.SimpleButton btnSimulate;
        private DevExpress.XtraEditors.SimpleButton btnPause;
        private DevExpress.XtraEditors.SimpleButton btnBackward;
        private DevExpress.XtraEditors.SimpleButton btnCircle;
        private DevExpress.XtraEditors.SimpleButton btnOutline;
        private DevExpress.XtraEditors.ComboBoxEdit cmbReturnPoint;
        private ControlLibrary.Common.UCNumberInputer ucInputStep;
        private DevExpress.XtraEditors.SimpleButton btnFastStart;
        private ControlLibrary.Common.SpeedInputer ucInputSpeed;
    }
}
