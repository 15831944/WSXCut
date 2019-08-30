using System;

namespace WSX.DrawService.Wrapper
{
    partial class DrawingComponent
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.panelSection = new System.Windows.Forms.Panel();
            this.panelDrawing = new System.Windows.Forms.Panel();
            this.panelHRuler = new System.Windows.Forms.Panel();
            this.rulerControlH = new WSX.DrawService.Wrapper.RulerControl();
            this.panelVRuler = new System.Windows.Forms.Panel();
            this.rulerControlV = new WSX.DrawService.Wrapper.RulerControl();
            this.labelUint = new System.Windows.Forms.Label();
            this.dropDownMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripOperation = new System.Windows.Forms.ToolStrip();
            this.btnSelectPattern = new System.Windows.Forms.ToolStripButton();
            this.btnTranslationView = new System.Windows.Forms.ToolStripButton();
            this.btnSortMode = new System.Windows.Forms.ToolStripButton();
            this.btnNodeEditMode = new System.Windows.Forms.ToolStripButton();
            this.toolBtnRedo = new System.Windows.Forms.ToolStripButton();
            this.toolBtnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnSingleDot = new System.Windows.Forms.ToolStripButton();
            this.toolBtnLines = new System.Windows.Forms.ToolStripButton();
            this.btnMultiLine = new System.Windows.Forms.ToolStripButton();
            this.btnRectangle = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropdownButton1 = new WSX.DrawService.Wrapper.ToolStripDropdownButton();
            this.toolBtnCircleCR = new System.Windows.Forms.ToolStripButton();
            this.toolBtnArcCR = new System.Windows.Forms.ToolStripButton();
            this.toolBtnArc3P123 = new System.Windows.Forms.ToolStripButton();
            this.toolBtnAutoChamfer = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.SuspendLayout();
            this.panelSection.SuspendLayout();
            this.panelHRuler.SuspendLayout();
            this.panelVRuler.SuspendLayout();
            this.dropDownMenu.SuspendLayout();
            this.toolStripOperation.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(150, 175);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(150, 175);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // panelSection
            // 
            this.panelSection.BackColor = System.Drawing.SystemColors.Control;
            this.panelSection.Controls.Add(this.panelDrawing);
            this.panelSection.Controls.Add(this.panelHRuler);
            this.panelSection.Controls.Add(this.panelVRuler);
            this.panelSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSection.Location = new System.Drawing.Point(24, 0);
            this.panelSection.Name = "panelSection";
            this.panelSection.Size = new System.Drawing.Size(822, 613);
            this.panelSection.TabIndex = 2;
            // 
            // panelDrawing
            // 
            this.panelDrawing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDrawing.Location = new System.Drawing.Point(20, 20);
            this.panelDrawing.Name = "panelDrawing";
            this.panelDrawing.Size = new System.Drawing.Size(802, 593);
            this.panelDrawing.TabIndex = 2;
            this.panelDrawing.Tag = "";
            // 
            // panelHRuler
            // 
            this.panelHRuler.Controls.Add(this.rulerControlH);
            this.panelHRuler.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHRuler.Font = new System.Drawing.Font("Tahoma", 8F);
            this.panelHRuler.Location = new System.Drawing.Point(20, 0);
            this.panelHRuler.Name = "panelHRuler";
            this.panelHRuler.Size = new System.Drawing.Size(802, 20);
            this.panelHRuler.TabIndex = 1;
            // 
            // rulerControlH
            // 
            this.rulerControlH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.rulerControlH.Current = 0D;
            this.rulerControlH.Direction = WSX.DrawService.Wrapper.RulerDirection.Horitonal;
            this.rulerControlH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rulerControlH.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rulerControlH.Location = new System.Drawing.Point(0, 0);
            this.rulerControlH.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rulerControlH.Max = 0D;
            this.rulerControlH.Min = 0D;
            this.rulerControlH.Name = "rulerControlH";
            this.rulerControlH.Size = new System.Drawing.Size(802, 20);
            this.rulerControlH.TabIndex = 4;
            // 
            // panelVRuler
            // 
            this.panelVRuler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panelVRuler.Controls.Add(this.rulerControlV);
            this.panelVRuler.Controls.Add(this.labelUint);
            this.panelVRuler.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelVRuler.Font = new System.Drawing.Font("Tahoma", 8F);
            this.panelVRuler.Location = new System.Drawing.Point(0, 0);
            this.panelVRuler.Name = "panelVRuler";
            this.panelVRuler.Size = new System.Drawing.Size(20, 613);
            this.panelVRuler.TabIndex = 0;
            // 
            // rulerControlV
            // 
            this.rulerControlV.Current = 0D;
            this.rulerControlV.Direction = WSX.DrawService.Wrapper.RulerDirection.Vertical;
            this.rulerControlV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rulerControlV.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rulerControlV.Location = new System.Drawing.Point(0, 25);
            this.rulerControlV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rulerControlV.Max = 0D;
            this.rulerControlV.Min = 0D;
            this.rulerControlV.Name = "rulerControlV";
            this.rulerControlV.Size = new System.Drawing.Size(20, 588);
            this.rulerControlV.TabIndex = 3;
            // 
            // labelUint
            // 
            this.labelUint.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelUint.Font = new System.Drawing.Font("Consolas", 8F);
            this.labelUint.ForeColor = System.Drawing.Color.Blue;
            this.labelUint.Location = new System.Drawing.Point(0, 0);
            this.labelUint.Name = "labelUint";
            this.labelUint.Size = new System.Drawing.Size(20, 25);
            this.labelUint.TabIndex = 2;
            this.labelUint.Text = "mm";
            this.labelUint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dropDownMenu
            // 
            this.dropDownMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3});
            this.dropDownMenu.Name = "contextMenuStrip1";
            this.dropDownMenu.Size = new System.Drawing.Size(125, 70);
            // 
            // menuItem1
            // 
            this.menuItem1.Image = global::WSX.DrawService.Properties.Resources.roundrect;
            this.menuItem1.Name = "menuItem1";
            this.menuItem1.Size = new System.Drawing.Size(124, 22);
            this.menuItem1.Tag = "RoundRect";
            this.menuItem1.Text = "圆角矩形";
            this.menuItem1.Click += new System.EventHandler(this.toolBtnDrawingTool_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Image = global::WSX.DrawService.Properties.Resources.polygon;
            this.menuItem2.Name = "menuItem2";
            this.menuItem2.Size = new System.Drawing.Size(124, 22);
            this.menuItem2.Tag = "Hexagon";
            this.menuItem2.Text = "多边形";
            this.menuItem2.Click += new System.EventHandler(this.toolBtnDrawingTool_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Image = global::WSX.DrawService.Properties.Resources.star;
            this.menuItem3.Name = "menuItem3";
            this.menuItem3.Size = new System.Drawing.Size(124, 22);
            this.menuItem3.Tag = "StarCommon";
            this.menuItem3.Text = "星形";
            this.menuItem3.Click += new System.EventHandler(this.toolBtnDrawingTool_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(21, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(21, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(21, 6);
            // 
            // toolStripOperation
            // 
            this.toolStripOperation.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStripOperation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelectPattern,
            this.btnTranslationView,
            this.btnSortMode,
            this.btnNodeEditMode,
            this.toolStripSeparator1,
            this.toolBtnRedo,
            this.toolBtnUndo,
            this.toolStripSeparator2,
            this.btnSingleDot,
            this.toolBtnLines,
            this.btnMultiLine,
            this.btnRectangle,
            this.toolStripDropdownButton1,
            this.toolBtnCircleCR,
            this.toolBtnArcCR,
            this.toolBtnArc3P123,
            this.toolStripSeparator3,
            this.toolBtnAutoChamfer});
            this.toolStripOperation.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripOperation.Location = new System.Drawing.Point(0, 0);
            this.toolStripOperation.Name = "toolStripOperation";
            this.toolStripOperation.Size = new System.Drawing.Size(24, 613);
            this.toolStripOperation.TabIndex = 1;
            this.toolStripOperation.Text = "toolStrip1";
            // 
            // btnSelectPattern
            // 
            this.btnSelectPattern.Checked = true;
            this.btnSelectPattern.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnSelectPattern.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectPattern.Image = global::WSX.DrawService.Properties.Resources.select;
            this.btnSelectPattern.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectPattern.Name = "btnSelectPattern";
            this.btnSelectPattern.Size = new System.Drawing.Size(21, 20);
            this.btnSelectPattern.Tag = "Select";
            this.btnSelectPattern.ToolTipText = "选中图形";
            this.btnSelectPattern.Click += new System.EventHandler(this.btnSelectPattern_Click);
            // 
            // btnTranslationView
            // 
            this.btnTranslationView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTranslationView.Image = global::WSX.DrawService.Properties.Resources.pan;
            this.btnTranslationView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTranslationView.Name = "btnTranslationView";
            this.btnTranslationView.Size = new System.Drawing.Size(21, 20);
            this.btnTranslationView.Tag = "Pan";
            this.btnTranslationView.Text = "toolStripButton2";
            this.btnTranslationView.ToolTipText = "平移视图";
            this.btnTranslationView.Click += new System.EventHandler(this.btnTranslationView_Click);
            // 
            // btnSortMode
            // 
            this.btnSortMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSortMode.Image = global::WSX.DrawService.Properties.Resources.sort;
            this.btnSortMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSortMode.Name = "btnSortMode";
            this.btnSortMode.Size = new System.Drawing.Size(21, 20);
            this.btnSortMode.Tag = "SortMode";
            this.btnSortMode.ToolTipText = "排序模式";
            this.btnSortMode.Click += new System.EventHandler(this.btnSortMode_Click);
            // 
            // btnNodeEditMode
            // 
            this.btnNodeEditMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNodeEditMode.Image = global::WSX.DrawService.Properties.Resources.node;
            this.btnNodeEditMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNodeEditMode.Name = "btnNodeEditMode";
            this.btnNodeEditMode.Size = new System.Drawing.Size(21, 20);
            this.btnNodeEditMode.Tag = "NodeEditMode";
            this.btnNodeEditMode.ToolTipText = "节点编辑";
            this.btnNodeEditMode.Click += new System.EventHandler(this.btnNodeEditMode_Click);
            // 
            // toolBtnRedo
            // 
            this.toolBtnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnRedo.Image = global::WSX.DrawService.Properties.Resources.Redo;
            this.toolBtnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnRedo.Name = "toolBtnRedo";
            this.toolBtnRedo.Size = new System.Drawing.Size(21, 20);
            this.toolBtnRedo.Text = "重做";
            this.toolBtnRedo.Click += new System.EventHandler(this.toolBtnRedo_Click);
            // 
            // toolBtnUndo
            // 
            this.toolBtnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnUndo.Image = global::WSX.DrawService.Properties.Resources.Undo;
            this.toolBtnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnUndo.Name = "toolBtnUndo";
            this.toolBtnUndo.Size = new System.Drawing.Size(21, 20);
            this.toolBtnUndo.Text = "撤销";
            this.toolBtnUndo.Click += new System.EventHandler(this.toolBtnUndo_Click);
            // 
            // btnSingleDot
            // 
            this.btnSingleDot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSingleDot.Image = global::WSX.DrawService.Properties.Resources.dot;
            this.btnSingleDot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSingleDot.Name = "btnSingleDot";
            this.btnSingleDot.Size = new System.Drawing.Size(21, 20);
            this.btnSingleDot.Tag = "SingleDot";
            this.btnSingleDot.Text = "toolStripButton1";
            this.btnSingleDot.ToolTipText = "单点";
            this.btnSingleDot.Click += new System.EventHandler(this.toolBtnDrawingTool_Click);
            // 
            // toolBtnLines
            // 
            this.toolBtnLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnLines.Image = global::WSX.DrawService.Properties.Resources.line;
            this.toolBtnLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnLines.Name = "toolBtnLines";
            this.toolBtnLines.Size = new System.Drawing.Size(21, 20);
            this.toolBtnLines.Tag = "Lines";
            this.toolBtnLines.Text = "toolStripButton6";
            this.toolBtnLines.ToolTipText = "线";
            this.toolBtnLines.Click += new System.EventHandler(this.toolBtnDrawingTool_Click);
            // 
            // btnMultiLine
            // 
            this.btnMultiLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMultiLine.Image = global::WSX.DrawService.Properties.Resources.multiline;
            this.btnMultiLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMultiLine.Name = "btnMultiLine";
            this.btnMultiLine.Size = new System.Drawing.Size(21, 20);
            this.btnMultiLine.Tag = "MultiLine";
            this.btnMultiLine.Text = "toolStripButton1";
            this.btnMultiLine.ToolTipText = "多段线";
            this.btnMultiLine.Click += new System.EventHandler(this.toolBtnDrawingTool_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRectangle.Image = global::WSX.DrawService.Properties.Resources.rect;
            this.btnRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(21, 20);
            this.btnRectangle.Tag = "SingleRectangle";
            this.btnRectangle.ToolTipText = "矩形";
            this.btnRectangle.Click += new System.EventHandler(this.toolBtnDrawingTool_Click);
            // 
            // toolStripDropdownButton1
            // 
            this.toolStripDropdownButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripDropdownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropdownButton1.DropDown = this.dropDownMenu;
            this.toolStripDropdownButton1.Image = global::WSX.DrawService.Properties.Resources.polygon_menu;
            this.toolStripDropdownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropdownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropdownButton1.Name = "toolStripDropdownButton1";
            this.toolStripDropdownButton1.Size = new System.Drawing.Size(21, 26);
            // 
            // toolBtnCircleCR
            // 
            this.toolBtnCircleCR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnCircleCR.Image = global::WSX.DrawService.Properties.Resources.circle1;
            this.toolBtnCircleCR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnCircleCR.Name = "toolBtnCircleCR";
            this.toolBtnCircleCR.Size = new System.Drawing.Size(21, 20);
            this.toolBtnCircleCR.Tag = "CircleCR";
            this.toolBtnCircleCR.Text = "toolStripButton1";
            this.toolBtnCircleCR.ToolTipText = "圆心、半径画圆";
            this.toolBtnCircleCR.Click += new System.EventHandler(this.toolBtnDrawingTool_Click);
            // 
            // toolBtnArcCR
            // 
            this.toolBtnArcCR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnArcCR.Image = global::WSX.DrawService.Properties.Resources.circle2;
            this.toolBtnArcCR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnArcCR.Name = "toolBtnArcCR";
            this.toolBtnArcCR.Size = new System.Drawing.Size(21, 20);
            this.toolBtnArcCR.Tag = "ArcCR";
            this.toolBtnArcCR.Text = "toolStripButton2";
            this.toolBtnArcCR.ToolTipText = "圆心、半径画弧";
            this.toolBtnArcCR.Click += new System.EventHandler(this.toolBtnDrawingTool_Click);
            // 
            // toolBtnArc3P123
            // 
            this.toolBtnArc3P123.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnArc3P123.Image = global::WSX.DrawService.Properties.Resources.arc;
            this.toolBtnArc3P123.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnArc3P123.Name = "toolBtnArc3P123";
            this.toolBtnArc3P123.Size = new System.Drawing.Size(21, 20);
            this.toolBtnArc3P123.Tag = "Arc3P";
            this.toolBtnArc3P123.ToolTipText = "三点圆弧";
            this.toolBtnArc3P123.Click += new System.EventHandler(this.toolBtnDrawingTool_Click);
            // 
            // toolBtnAutoChamfer
            // 
            this.toolBtnAutoChamfer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnAutoChamfer.Image = global::WSX.DrawService.Properties.Resources.autoChamfer;
            this.toolBtnAutoChamfer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnAutoChamfer.Name = "toolBtnAutoChamfer";
            this.toolBtnAutoChamfer.Size = new System.Drawing.Size(21, 20);
            this.toolBtnAutoChamfer.Tag = "AutoChamfer";
            this.toolBtnAutoChamfer.ToolTipText = "倒圆角";
            this.toolBtnAutoChamfer.Click += new System.EventHandler(this.toolBtnAutoChamfer_Click);
            // 
            // DrawingComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelSection);
            this.Controls.Add(this.toolStripOperation);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "DrawingComponent";
            this.Size = new System.Drawing.Size(846, 613);
            this.Load += new System.EventHandler(this.DrawingComponent_Load);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.panelSection.ResumeLayout(false);
            this.panelHRuler.ResumeLayout(false);
            this.panelVRuler.ResumeLayout(false);
            this.dropDownMenu.ResumeLayout(false);
            this.toolStripOperation.ResumeLayout(false);
            this.toolStripOperation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

   
        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Panel panelSection;
        private System.Windows.Forms.Panel panelHRuler;
        private System.Windows.Forms.Panel panelVRuler;
        private System.Windows.Forms.Label labelUint;
        private System.Windows.Forms.Panel panelDrawing;
        private RulerControl rulerControlV;
        private RulerControl rulerControlH;
        private System.Windows.Forms.ContextMenuStrip dropDownMenu;
        private System.Windows.Forms.ToolStripButton btnSelectPattern;
        private System.Windows.Forms.ToolStripButton btnTranslationView;
        private System.Windows.Forms.ToolStripButton btnSortMode;
        private System.Windows.Forms.ToolStripButton btnNodeEditMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolBtnRedo;
        private System.Windows.Forms.ToolStripButton toolBtnUndo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSingleDot;
        private System.Windows.Forms.ToolStripButton toolBtnLines;
        private System.Windows.Forms.ToolStripButton btnMultiLine;
        private System.Windows.Forms.ToolStripButton btnRectangle;
        private System.Windows.Forms.ToolStripButton toolBtnCircleCR;
        private System.Windows.Forms.ToolStripButton toolBtnArcCR;
        private System.Windows.Forms.ToolStripButton toolBtnArc3P123;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip toolStripOperation;
        private ToolStripDropdownButton toolStripDropdownButton1;
        private System.Windows.Forms.ToolStripMenuItem menuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuItem3;
        private System.Windows.Forms.ToolStripButton toolBtnAutoChamfer;
    }
}
