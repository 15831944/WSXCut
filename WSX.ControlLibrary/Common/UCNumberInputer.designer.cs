namespace WSX.ControlLibrary.Common
{
    partial class UCNumberInputer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PopupEdit = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.PopupEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // PopupEdit
            // 
            this.PopupEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PopupEdit.EditValue = "1";
            this.PopupEdit.Location = new System.Drawing.Point(0, 0);
            this.PopupEdit.Name = "PopupEdit";
            this.PopupEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PopupEdit.Properties.PopupFormMinSize = new System.Drawing.Size(156, 165);
            this.PopupEdit.Properties.PopupFormSize = new System.Drawing.Size(156, 165);
            this.PopupEdit.Properties.PopupSizeable = false;
            this.PopupEdit.Properties.ShowPopupCloseButton = false;
            this.PopupEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.PopupEdit.Properties.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.PopupEdit_Properties_QueryPopUp);
            this.PopupEdit.Size = new System.Drawing.Size(101, 20);
            this.PopupEdit.TabIndex = 0;
            this.PopupEdit.Popup += new System.EventHandler(this.popupContainerEdit1_Popup);
            this.PopupEdit.Enter += new System.EventHandler(this.PopupEdit_Enter);
            this.PopupEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PopupEdit_KeyPress);
            this.PopupEdit.Leave += new System.EventHandler(this.PopupEdit_Leave);
            // 
            // UCNumberInputer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PopupEdit);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCNumberInputer";
            this.Size = new System.Drawing.Size(101, 20);
            ((System.ComponentModel.ISupportInitialize)(this.PopupEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.PopupContainerEdit PopupEdit;
    }
}
