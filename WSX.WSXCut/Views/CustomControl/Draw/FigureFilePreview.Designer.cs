namespace WSX.WSXCut.Views.CustomControl.Draw
{
    partial class FigureFilePreview
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
            this.panelDrawing = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.ckPreView = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDrawing
            // 
            this.panelDrawing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDrawing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDrawing.Location = new System.Drawing.Point(0, 35);
            this.panelDrawing.Margin = new System.Windows.Forms.Padding(0);
            this.panelDrawing.Name = "panelDrawing";
            this.panelDrawing.Padding = new System.Windows.Forms.Padding(5, 5, 5, 20);
            this.panelDrawing.Size = new System.Drawing.Size(324, 256);
            this.panelDrawing.TabIndex = 3;
            this.panelDrawing.Tag = "";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(5, 11);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(95, 12);
            this.lblCount.TabIndex = 4;
            this.lblCount.Text = "Entities Count:";
            // 
            // ckPreView
            // 
            this.ckPreView.AutoSize = true;
            this.ckPreView.Checked = true;
            this.ckPreView.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckPreView.Location = new System.Drawing.Point(270, 9);
            this.ckPreView.Name = "ckPreView";
            this.ckPreView.Size = new System.Drawing.Size(48, 16);
            this.ckPreView.TabIndex = 5;
            this.ckPreView.Text = "预览";
            this.ckPreView.UseVisualStyleBackColor = true;
            this.ckPreView.CheckedChanged += new System.EventHandler(this.ckPreView_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckPreView);
            this.panel1.Controls.Add(this.lblCount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(324, 35);
            this.panel1.TabIndex = 4;
            this.panel1.Tag = "";
            // 
            // FigureFilePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDrawing);
            this.Controls.Add(this.panel1);
            this.Name = "FigureFilePreview";
            this.Size = new System.Drawing.Size(324, 291);
            this.Load += new System.EventHandler(this.FigureFilePreview_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDrawing;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.CheckBox ckPreView;
        private System.Windows.Forms.Panel panel1;
    }
}
