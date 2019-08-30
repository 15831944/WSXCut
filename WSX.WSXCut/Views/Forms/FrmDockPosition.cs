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

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmDockPosition : DevExpress.XtraEditors.XtraForm
    {
        public DockPositionModel Model { get; internal set; }
        public FrmDockPosition()
        {
            InitializeComponent();
        }

        public FrmDockPosition(DockPositionModel model) : this()
        {
            this.Model = model;
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new DockPositionModel();
                this.mvvmContext1.SetViewModel(typeof(DockPositionModel), this.Model);
                InitializeBindings();
            }
        }

        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<DockPositionModel>();
            fluent.SetBinding(this.rbtnRelative, c => c.Checked, x => x.DockType,
                m => { return DockTypes.Relative == m; },
                r => { return DockTypes.Relative; });
            fluent.SetBinding(this.rbtnAbsolute, c => c.Checked, x => x.DockType,
                m => { return DockTypes.Absolute == m; },
                r => { return DockTypes.Absolute; });
            fluent.SetBinding(this.rbtnLeftTop, e => e.Checked, x => x.RelativePosition,
                m => { return RelativePositions.LeftTop == m; },
                r => { return RelativePositions.LeftTop; });
            fluent.SetBinding(this.rbtnLeft, e => e.Checked, x => x.RelativePosition,
                m => { return RelativePositions.Left == m; },
                r => { return RelativePositions.Left; });
            fluent.SetBinding(this.rbtnLeftBottom, e => e.Checked, x => x.RelativePosition,
                m => { return RelativePositions.LeftBottom == m; },
                r => { return RelativePositions.LeftBottom; });
            fluent.SetBinding(this.rbtnTop, e => e.Checked, x => x.RelativePosition,
                m => { return RelativePositions.Top == m; },
                r => { return RelativePositions.Top; });
            fluent.SetBinding(this.rbtnMiddle, e => e.Checked, x => x.RelativePosition,
                m => { return RelativePositions.Middle == m; },
                r => { return RelativePositions.Middle; });
            fluent.SetBinding(this.rbtnBottom, e => e.Checked, x => x.RelativePosition,
                m => { return RelativePositions.Bottom == m; },
                r => { return RelativePositions.Bottom; });
            fluent.SetBinding(this.rbtnRightTop, e => e.Checked, x => x.RelativePosition,
                m => { return RelativePositions.RightTop == m; },
                r => { return RelativePositions.RightTop; });
            fluent.SetBinding(this.rbtnRight, e => e.Checked, x => x.RelativePosition,
                m => { return RelativePositions.Right == m; },
                r => { return RelativePositions.Right; });
            fluent.SetBinding(this.rbtnRightBottom, e => e.Checked, x => x.RelativePosition,
                m => { return RelativePositions.RightBottom == m; },
                r => { return RelativePositions.RightBottom; });
            fluent.SetBinding(this.ckExcludeUnprocessedFigure, c => c.Checked, x => x.IsExcludeUnprocessedFigure);
            fluent.SetBinding(this.ckApplyToAllPlates, c => c.Checked, x => x.IsApplyToAllPlates);
            fluent.SetBinding(this.panel1, c => c.Enabled, x => x.DockType,
                m => { return DockTypes.Relative == m; });
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

        private void rbtnRelative_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Enabled = this.rbtnRelative.Checked;
        }
    }
}
