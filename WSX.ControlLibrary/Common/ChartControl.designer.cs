namespace WSX.ControlLibrary.Common
{
    partial class ChartControl
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
            this.SuspendLayout();
            // 
            // ChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.Name = "ChartControl";
            this.Size = new System.Drawing.Size(446, 270);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ChartControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChartControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChartControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChartControl_MouseUp);
            this.Resize += new System.EventHandler(this.ChartControl_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
