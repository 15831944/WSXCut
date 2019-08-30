namespace WSX.WSXCut.Views.CustomControl.Draw
{
    partial class CanvasWrapper
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
            this.components = new System.ComponentModel.Container();
            this.drawingComponent1 = new WSX.DrawService.Wrapper.DrawingComponent();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // drawingComponent1
            // 
            this.drawingComponent1.CanvasEnabled = true;
            this.drawingComponent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawingComponent1.HRulerVisible = true;
            this.drawingComponent1.Location = new System.Drawing.Point(0, 0);
            this.drawingComponent1.Name = "drawingComponent1";
            this.drawingComponent1.Size = new System.Drawing.Size(800, 548);
            this.drawingComponent1.StaticRefreshMark = true;
            this.drawingComponent1.TabIndex = 0;
            this.drawingComponent1.VRulerVisible = true;
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 50;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // CanvasWrapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.drawingComponent1);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Name = "CanvasWrapper";
            this.Size = new System.Drawing.Size(800, 548);
            this.Load += new System.EventHandler(this.CanvasWrapper_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WSX.DrawService.Wrapper.DrawingComponent drawingComponent1;
        private System.Windows.Forms.Timer timerRefresh;
    }
}
