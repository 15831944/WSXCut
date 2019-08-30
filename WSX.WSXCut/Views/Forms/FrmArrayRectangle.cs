using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmArrayRectangle : DevExpress.XtraEditors.XtraForm
    {
        public ArrayRectangleModel Model { get; internal set; }
        public FrmArrayRectangle()
        {
            InitializeComponent();
        }

        public FrmArrayRectangle(ArrayRectangleModel arrayLayout) : this()
        {
            this.Model = CopyUtil.DeepCopy(arrayLayout);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new ArrayRectangleModel();
                Model.RowCount = 4;Model.ColumnCount = 4;
                Model.OffsetType = ArrayOffsetTypes.Spacing;
                Model.OffsetRow = 0.05;Model.OffsetColumn = 0.05;
                Model.ArrayRowDirection = ArrayRowDirections.Bottom;
                Model.ArrayColumnDirection = ArrayColumnDirections.Right;
                this.mvvmContext1.SetViewModel(typeof(ArrayRectangleModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<ArrayRectangleModel>();
            fluent.SetBinding(this.txtRowCount.PopupEdit, c => c.Text, x => x.RowCount);
            fluent.SetBinding(this.txtColumnCount.PopupEdit, c => c.Text, x => x.ColumnCount);
            fluent.SetBinding(this.rbtnOffsetRowCount, c => c.Checked, x => x.OffsetType,
                m => { return ArrayOffsetTypes.RowCount == m; },
                r => { return ArrayOffsetTypes.RowCount; });
            fluent.SetBinding(this.rbtnOffsetSpacing, c => c.Checked, x => x.OffsetType,
                m => { return ArrayOffsetTypes.Spacing == m; },
                r => { return ArrayOffsetTypes.Spacing; });
            fluent.SetBinding(this.txtOffsetRow.PopupEdit, c => c.Text, x => x.OffsetRow);
            fluent.SetBinding(this.txtOffsetColumn.PopupEdit, c => c.Text, x => x.OffsetColumn);
            fluent.SetBinding(this.rbtnTop, c => c.Checked, x => x.ArrayRowDirection,
                m => { return ArrayRowDirections.Top == m; },
                r => { return ArrayRowDirections.Top; });
            fluent.SetBinding(this.rbtnBottom, c => c.Checked, x => x.ArrayRowDirection,
                m => { return ArrayRowDirections.Bottom == m; },
                r => { return ArrayRowDirections.Bottom; });
            fluent.SetBinding(this.rbtnLeft, c => c.Checked, x => x.ArrayColumnDirection,
                m => { return ArrayColumnDirections.Left == m; },
                r => { return ArrayColumnDirections.Left; });
            fluent.SetBinding(this.rbtnRight, c => c.Checked, x => x.ArrayColumnDirection,
                m => { return ArrayColumnDirections.Right == m; },
                r => { return ArrayColumnDirections.Right; });
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Focus();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbtnOffsetRowCount_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnOffsetRowCount.Checked)
            {
                this.labelControl6.Text = "行偏移";
                this.labelControl5.Text = "列偏移";
            }
        }

        private void rbtnOffsetSpacing_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtnOffsetSpacing.Checked)
            {
                this.labelControl6.Text = "行间距";
                this.labelControl5.Text = "列间距";
            }
        }
    }
}
