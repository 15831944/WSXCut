namespace WSX.DrawService.CanvasControl
{
    partial class UCCanvas
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEndDrawing = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEndSetStartPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEndSetMicroConn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEndSetSmooth = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEndSetRelease = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEndSetCoolPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEndSetBridge = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSort = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSortGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSortShortestMove = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSortKnife = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSortSmallFigurePriority = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSortInsideToOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSortLeftToRight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSortRightToLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSortTopToBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSortBottomToTop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSortClockWise = new System.Windows.Forms.ToolStripMenuItem();
            this.tsGroupSortAntiClock = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEndDrawing,
            this.tsEndSetStartPoint,
            this.tsEndSetMicroConn,
            this.tsEndSetSmooth,
            this.tsEndSetRelease,
            this.tsEndSetCoolPoint,
            this.tsEndSetBridge,
            this.tsCopy,
            this.tsPaste,
            this.tsCut,
            this.tsDelete,
            this.tsUndo,
            this.tsRedo,
            this.tsGroupSort});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 334);
            // 
            // tsEndDrawing
            // 
            this.tsEndDrawing.Name = "tsEndDrawing";
            this.tsEndDrawing.Size = new System.Drawing.Size(180, 22);
            this.tsEndDrawing.Text = "结束绘图";
            this.tsEndDrawing.Click += new System.EventHandler(this.tsEndDrawing_Click);
            // 
            // tsEndSetStartPoint
            // 
            this.tsEndSetStartPoint.Name = "tsEndSetStartPoint";
            this.tsEndSetStartPoint.Size = new System.Drawing.Size(180, 22);
            this.tsEndSetStartPoint.Text = "结束起点设置";
            this.tsEndSetStartPoint.Click += new System.EventHandler(this.tsEndCommandEscape_Click);
            // 
            // tsEndSetMicroConn
            // 
            this.tsEndSetMicroConn.Name = "tsEndSetMicroConn";
            this.tsEndSetMicroConn.Size = new System.Drawing.Size(180, 22);
            this.tsEndSetMicroConn.Text = "取消微连";
            this.tsEndSetMicroConn.Click += new System.EventHandler(this.tsEndCommandEscape_Click);
            // 
            // tsEndSetSmooth
            // 
            this.tsEndSetSmooth.Name = "tsEndSetSmooth";
            this.tsEndSetSmooth.Size = new System.Drawing.Size(180, 22);
            this.tsEndSetSmooth.Text = "取消倒圆角";
            this.tsEndSetSmooth.Click += new System.EventHandler(this.tsEndCommandEscape_Click);
            // 
            // tsEndSetRelease
            // 
            this.tsEndSetRelease.Name = "tsEndSetRelease";
            this.tsEndSetRelease.Size = new System.Drawing.Size(180, 22);
            this.tsEndSetRelease.Text = "取消释放角";
            this.tsEndSetRelease.Click += new System.EventHandler(this.tsEndCommandEscape_Click);
            // 
            // tsEndSetCoolPoint
            // 
            this.tsEndSetCoolPoint.Name = "tsEndSetCoolPoint";
            this.tsEndSetCoolPoint.Size = new System.Drawing.Size(180, 22);
            this.tsEndSetCoolPoint.Text = "取消冷却点";
            this.tsEndSetCoolPoint.CheckStateChanged += new System.EventHandler(this.tsEndCommandEscape_Click);
            // 
            // tsEndSetBridge
            // 
            this.tsEndSetBridge.Name = "tsEndSetBridge";
            this.tsEndSetBridge.Size = new System.Drawing.Size(180, 22);
            this.tsEndSetBridge.Text = "取消桥接";
            this.tsEndSetBridge.Click += new System.EventHandler(this.tsEndCommandEscape_Click);
            // 
            // tsCopy
            // 
            this.tsCopy.Name = "tsCopy";
            this.tsCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsCopy.Size = new System.Drawing.Size(180, 22);
            this.tsCopy.Text = "复制";
            this.tsCopy.Click += new System.EventHandler(this.Copy_Click);
            // 
            // tsPaste
            // 
            this.tsPaste.Name = "tsPaste";
            this.tsPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsPaste.Size = new System.Drawing.Size(180, 22);
            this.tsPaste.Text = "粘贴";
            this.tsPaste.Click += new System.EventHandler(this.Paste_Click);
            // 
            // tsCut
            // 
            this.tsCut.Name = "tsCut";
            this.tsCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsCut.Size = new System.Drawing.Size(180, 22);
            this.tsCut.Text = "剪切";
            this.tsCut.Click += new System.EventHandler(this.tsCut_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsDelete.Size = new System.Drawing.Size(180, 22);
            this.tsDelete.Text = "删除";
            this.tsDelete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // tsUndo
            // 
            this.tsUndo.Image = global::WSX.DrawService.Properties.Resources.Undo;
            this.tsUndo.Name = "tsUndo";
            this.tsUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsUndo.Size = new System.Drawing.Size(180, 22);
            this.tsUndo.Text = "撤销";
            this.tsUndo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // tsRedo
            // 
            this.tsRedo.Image = global::WSX.DrawService.Properties.Resources.Redo;
            this.tsRedo.Name = "tsRedo";
            this.tsRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.tsRedo.Size = new System.Drawing.Size(180, 22);
            this.tsRedo.Text = "重做";
            this.tsRedo.Click += new System.EventHandler(this.Redo_Click);
            // 
            // tsGroupSort
            // 
            this.tsGroupSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsGroupSortGrid,
            this.tsGroupSortShortestMove,
            this.tsGroupSortKnife,
            this.tsGroupSortSmallFigurePriority,
            this.tsGroupSortInsideToOut,
            this.tsGroupSortLeftToRight,
            this.tsGroupSortRightToLeft,
            this.tsGroupSortTopToBottom,
            this.tsGroupSortBottomToTop,
            this.tsGroupSortClockWise,
            this.tsGroupSortAntiClock});
            this.tsGroupSort.Name = "tsGroupSort";
            this.tsGroupSort.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSort.Text = "群组内部排序";
            // 
            // tsGroupSortGrid
            // 
            this.tsGroupSortGrid.Name = "tsGroupSortGrid";
            this.tsGroupSortGrid.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSortGrid.Tag = "SortGrid";
            this.tsGroupSortGrid.Text = "网格排序";
            this.tsGroupSortGrid.Click += new System.EventHandler(this.tsGroupSort_Click);
            // 
            // tsGroupSortShortestMove
            // 
            this.tsGroupSortShortestMove.Name = "tsGroupSortShortestMove";
            this.tsGroupSortShortestMove.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSortShortestMove.Tag = "SortShortestMove";
            this.tsGroupSortShortestMove.Text = "局部最短空移";
            this.tsGroupSortShortestMove.Click += new System.EventHandler(this.tsGroupSort_Click);
            // 
            // tsGroupSortKnife
            // 
            this.tsGroupSortKnife.Name = "tsGroupSortKnife";
            this.tsGroupSortKnife.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSortKnife.Tag = "SortKnife";
            this.tsGroupSortKnife.Text = "刀模排序";
            this.tsGroupSortKnife.Click += new System.EventHandler(this.tsGroupSort_Click);
            // 
            // tsGroupSortSmallFigurePriority
            // 
            this.tsGroupSortSmallFigurePriority.Name = "tsGroupSortSmallFigurePriority";
            this.tsGroupSortSmallFigurePriority.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSortSmallFigurePriority.Tag = "SortSmallFigurePriority";
            this.tsGroupSortSmallFigurePriority.Text = "小图优先";
            this.tsGroupSortSmallFigurePriority.Click += new System.EventHandler(this.tsGroupSort_Click);
            // 
            // tsGroupSortInsideToOut
            // 
            this.tsGroupSortInsideToOut.Name = "tsGroupSortInsideToOut";
            this.tsGroupSortInsideToOut.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSortInsideToOut.Tag = "SortInsideToOut";
            this.tsGroupSortInsideToOut.Text = "由内到外";
            this.tsGroupSortInsideToOut.Click += new System.EventHandler(this.tsGroupSort_Click);
            // 
            // tsGroupSortLeftToRight
            // 
            this.tsGroupSortLeftToRight.Name = "tsGroupSortLeftToRight";
            this.tsGroupSortLeftToRight.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSortLeftToRight.Tag = "SortLeftToRight";
            this.tsGroupSortLeftToRight.Text = "从左到右";
            this.tsGroupSortLeftToRight.Click += new System.EventHandler(this.tsGroupSort_Click);
            // 
            // tsGroupSortRightToLeft
            // 
            this.tsGroupSortRightToLeft.Name = "tsGroupSortRightToLeft";
            this.tsGroupSortRightToLeft.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSortRightToLeft.Tag = "SortRightToLeft";
            this.tsGroupSortRightToLeft.Text = "从右到左";
            this.tsGroupSortRightToLeft.Click += new System.EventHandler(this.tsGroupSort_Click);
            // 
            // tsGroupSortTopToBottom
            // 
            this.tsGroupSortTopToBottom.Name = "tsGroupSortTopToBottom";
            this.tsGroupSortTopToBottom.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSortTopToBottom.Tag = "SortTopToBottom";
            this.tsGroupSortTopToBottom.Text = "从上到下";
            this.tsGroupSortTopToBottom.Click += new System.EventHandler(this.tsGroupSort_Click);
            // 
            // tsGroupSortBottomToTop
            // 
            this.tsGroupSortBottomToTop.Name = "tsGroupSortBottomToTop";
            this.tsGroupSortBottomToTop.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSortBottomToTop.Tag = "SortBottomToTop";
            this.tsGroupSortBottomToTop.Text = "从下到上";
            this.tsGroupSortBottomToTop.Click += new System.EventHandler(this.tsGroupSort_Click);
            // 
            // tsGroupSortClockWise
            // 
            this.tsGroupSortClockWise.Name = "tsGroupSortClockWise";
            this.tsGroupSortClockWise.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSortClockWise.Tag = "SortClockwise";
            this.tsGroupSortClockWise.Text = "顺时针";
            this.tsGroupSortClockWise.Click += new System.EventHandler(this.tsGroupSort_Click);
            // 
            // tsGroupSortAntiClock
            // 
            this.tsGroupSortAntiClock.Name = "tsGroupSortAntiClock";
            this.tsGroupSortAntiClock.Size = new System.Drawing.Size(180, 22);
            this.tsGroupSortAntiClock.Tag = "SortAnticlockwise";
            this.tsGroupSortAntiClock.Text = "逆时针";
            this.tsGroupSortAntiClock.Click += new System.EventHandler(this.tsGroupSort_Click);
            // 
            // UCCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "UCCanvas";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsCopy;
        private System.Windows.Forms.ToolStripMenuItem tsPaste;
        private System.Windows.Forms.ToolStripMenuItem tsCut;
        private System.Windows.Forms.ToolStripMenuItem tsDelete;
        private System.Windows.Forms.ToolStripMenuItem tsUndo;
        private System.Windows.Forms.ToolStripMenuItem tsRedo;
        private System.Windows.Forms.ToolStripMenuItem tsEndDrawing;
        private System.Windows.Forms.ToolStripMenuItem tsEndSetStartPoint;
        private System.Windows.Forms.ToolStripMenuItem tsEndSetMicroConn;
        private System.Windows.Forms.ToolStripMenuItem tsEndSetSmooth;
        private System.Windows.Forms.ToolStripMenuItem tsEndSetRelease;
        private System.Windows.Forms.ToolStripMenuItem tsEndSetCoolPoint;
        private System.Windows.Forms.ToolStripMenuItem tsEndSetBridge;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSort;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSortGrid;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSortShortestMove;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSortKnife;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSortSmallFigurePriority;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSortInsideToOut;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSortLeftToRight;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSortRightToLeft;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSortTopToBottom;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSortBottomToTop;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSortClockWise;
        private System.Windows.Forms.ToolStripMenuItem tsGroupSortAntiClock;
    }
}
