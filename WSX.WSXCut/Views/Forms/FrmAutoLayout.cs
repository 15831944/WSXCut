using DevExpress.XtraEditors.Repository;
using System;
using System.Collections;
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
    public partial class FrmAutoLayout : DevExpress.XtraEditors.XtraForm
    {
        public AutoLayoutModel Model { get; internal set; }
        private GridEditorCollection gridEditors;
        public FrmAutoLayout()
        {
            InitializeComponent();
        }

        public FrmAutoLayout(AutoLayoutModel autoLayout) : this()
        {
            this.Model = CopyUtil.DeepCopy(autoLayout);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new AutoLayoutModel();
                this.mvvmContext1.SetViewModel(typeof(AutoLayoutModel), this.Model);
                this.InitializeBindings();
            }
        }
        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<AutoLayoutModel>();
            fluent.SetBinding(this.rbtnPartAll, c => c.Checked, x => x.PartSelectType,
                m => { return PartSelectTypes.All == m; },
                r => { return PartSelectTypes.All; });
            fluent.SetBinding(this.rbtnPartSelected, c => c.Checked, x => x.PartSelectType,
                m => { return PartSelectTypes.Selected == m; },
                r => { return PartSelectTypes.Selected; });
            fluent.SetBinding(this.rbtnPlateAll, c => c.Checked, x => x.PlateSelectType,
                m => { return PlateSelectTypes.All == m; },
                r => { return PlateSelectTypes.All; });
            fluent.SetBinding(this.rbtnPlateSelected, c => c.Checked, x => x.PlateSelectType,
                m => { return PlateSelectTypes.Selected == m; },
                r => { return PlateSelectTypes.Selected; });
            fluent.SetBinding(this.rbtnPlateStandard, c => c.Checked, x => x.PlateSelectType,
                m => { return PlateSelectTypes.Standard == m; },
                r => { return PlateSelectTypes.Standard; });
            fluent.SetBinding(this.txtPlateLength.PopupEdit, c => c.Text, x => x.PlateLength);
            fluent.SetBinding(this.txtPlateWidth.PopupEdit, c => c.Text, x => x.PlateWidth);
            fluent.SetBinding(this.cmbPlateMaterials, c => c.Text, x => x.PlateMaterials);
            fluent.SetBinding(this.txtPlateThickness.PopupEdit, c => c.Text, x => x.PlateThickness);
            fluent.SetBinding(this.ckClearPreviousLayout, c => c.Checked, x => x.IsClearPreviousLayout);
            fluent.SetBinding(this.txtPartSpacing.PopupEdit, c => c.Text, x => x.PartSpacing);
            fluent.SetBinding(this.txtPlateEdge.PopupEdit, c => c.Text, x => x.PlateEdge);
            fluent.SetBinding(this.cmbLayoutStrategyTypes, c => c.SelectedIndex, x => x.LayoutStrategyType,
                m => { return (int)m; },
                r => { return (LayoutStrategyTypes)r; });
            fluent.SetBinding(this.ckAutoOptimize, c => c.Checked, x => x.IsAutoOptimize);
            fluent.SetBinding(this.txtAutoOptimizeCount.PopupEdit, c => c.Text, x => x.AutoOptimizeCount);
            fluent.SetBinding(this.ckCreateRemnantPart, c => c.Checked, x => x.IsCreateRemnantPart);
            fluent.SetBinding(this.cmbRemnantPartTypes, c => c.SelectedIndex, x => x.RemnantPartType,
                m => { return (int)m; },
                r => { return (RemnantPartTypes)r; });
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



        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == this.gridEditorValue)
            {
                GridEditorItem item = gridView1.GetRow(e.RowHandle) as GridEditorItem;
                if (item != null) e.RepositoryItem = item.RepositoryItem;
            }
        }
        void InitInplaceEditors()
        {
            this.gridEditors.Add(this.spEditorPartSpacing, "零件间距", this.Model.PartSpacing, "间距和留边", e =>
            {
                this.Model.PartSpacing = double.Parse(e + "");
                this.txtPartSpacing.PopupEdit.Text = this.Model.PartSpacing + "";
            });
            this.gridEditors.Add(this.spEditorPlateEdge, "板材留边", this.Model.PlateEdge, "间距和留边", e =>
            {
                this.Model.PlateEdge = double.Parse(e + "");
                this.txtPlateEdge.PopupEdit.Text = this.Model.PlateEdge + "";
            });
            this.gridEditors.Add(this.ckEditorIntelligenceSelect, "智能选择", this.Model.IsIntelligenceSelect, "排样策略", e =>
            {
                this.Model.IsIntelligenceSelect = (bool)e;
            });
            this.gridEditors.Add(this.cmbEditorLayoutStrategyType, "策略选择", this.cmbEditorLayoutStrategyType.Items[(int)this.Model.LayoutStrategyType], "排样策略", e =>
            {
                this.Model.LayoutStrategyType = (LayoutStrategyTypes)this.cmbEditorLayoutStrategyType.Items.IndexOf(e);
                this.cmbLayoutStrategyTypes.SelectedIndex = (int)this.Model.LayoutStrategyType;
            });
            object txtRotateAngle = this.Model.RotateAngle == 0 ? this.cmbEditorRotateAngle.Items[this.cmbEditorRotateAngle.Items.Count - 1] : this.Model.RotateAngle;
            this.gridEditors.Add(this.cmbEditorRotateAngle, "旋转角度", txtRotateAngle, "排样策略", e =>
            {
                if (!string.IsNullOrEmpty(e + ""))
                {
                    double value = 0;
                    double.TryParse(e + "", out value);
                    this.Model.RotateAngle = value;
                }
            });
            this.gridEditors.Add(this.ckEditorAutoCombine, "自动组合", this.Model.IsAutoCombine, "排样策略", e =>
            {
                this.Model.IsAutoCombine = (bool)e;
            });
            this.gridEditors.Add(this.cmbEditorLayoutDirection, "排样方向", this.cmbEditorLayoutDirection.Items[(int)this.Model.LayoutDirection], "排样策略", e =>
            {
                this.Model.LayoutDirection = (LayoutDirections)this.cmbEditorLayoutDirection.Items.IndexOf(e);
            });
            this.gridEditors.Add(this.ckEditorAutoOptimize, "启用自动优化", this.Model.IsAutoOptimize, "自动优化", e =>
            {
                this.Model.IsAutoOptimize = (bool)e;
                this.ckAutoOptimize.Checked = this.Model.IsAutoOptimize;
            });
            this.gridEditors.Add(this.spEditorAutoOptimizeCount, "自动优化次数", this.Model.AutoOptimizeCount, "自动优化", e =>
            {
                this.Model.AutoOptimizeCount = int.Parse(e + "");
                this.txtAutoOptimizeCount.PopupEdit.Text = this.Model.AutoOptimizeCount + "";
            });
            this.gridEditors.Add(this.ckEditorCreateRemnantPart, "生成涂料", this.Model.IsCreateRemnantPart, "余料", e =>
            {
                this.Model.IsCreateRemnantPart = (bool)e;
                this.ckCreateRemnantPart.Checked = this.Model.IsCreateRemnantPart;
            });

            this.gridEditors.Add(this.cmbEditorRemnantPartType, "余料类型", this.cmbEditorRemnantPartType.Items[(int)this.Model.RemnantPartType], "余料", e =>
            {
                this.Model.RemnantPartType = (RemnantPartTypes)this.cmbEditorRemnantPartType.Items.IndexOf(e);
                this.cmbRemnantPartTypes.SelectedIndex = (int)this.Model.RemnantPartType;
            });
            this.gridEditors.Add(this.spEditorRemnantPartMinWidth, "最小宽度", this.Model.RemnantPartMinWidth, "余料", e =>
            {
                this.Model.RemnantPartMinWidth = double.Parse(e + "");
            });
            this.gridEditors.Add(this.ckEditorAllCombineEdge, "全部共边", this.Model.IsAllCombineEdge, "自动共边", e =>
            {
                this.Model.IsAllCombineEdge = (bool)e;
            });
            this.gridEditors.Add(this.ckEditorAutoCombineEdge, "启用自动共边", this.Model.IsAutoCombineEdge, "自动共边", e =>
            {
                this.Model.IsAutoCombineEdge = (bool)e;
            });
            this.gridEditors.Add(this.spEditorCombineEdgeMinLength, "最短共边长度", this.Model.CombineEdgeMinLength, "自动共边", e =>
            {
                this.Model.CombineEdgeMinLength = double.Parse(e + "");
            });
            this.gridEditors.Add(this.spEditorCombineEdgeMaxCount, "最大共边数量", this.Model.CombineEdgeMaxCount, "自动共边", e =>
            {
                this.Model.CombineEdgeMaxCount = int.Parse(e + "");
            });
            this.gridEditors.Add(this.ckEditorDifferLengthCombineEdge, "不同长度共边", this.Model.IsDifferLengthCombineEdge, "自动共边", e =>
            {
                this.Model.IsDifferLengthCombineEdge = (bool)e;
            });
            this.gridEditors.Add(this.spEditorGapLength, "留缺口长度", this.Model.GapLength, "自动共边", e =>
            {
                this.Model.GapLength = double.Parse(e + "");
            });
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.gridControl1.Visible = !this.gridControl1.Visible;
            if (this.gridControl1.Visible)
            {
                this.gridEditors = new GridEditorCollection();
                this.InitInplaceEditors();
                this.gridControl1.DataSource = gridEditors;
                this.simpleButton3.Text = "<<常用设定";
            }
            else
            {
                this.simpleButton3.Text = "详细设定>>";
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string curtext = this.comboBoxEdit1.Text;
                string[] values = curtext.Replace(" ", "").Split('x');
                this.txtPlateLength.PopupEdit.Text = values[0].Replace("mm","");
                this.txtPlateWidth.PopupEdit.Text = values[1].Replace("mm", "");
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void rbtnPlateStandard_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Enabled = this.rbtnPlateStandard.Checked;
        }
    }

    public class GridEditorItem
    {
        string fName;
        object fValue;
        RepositoryItem fRepositoryItem;
        string group;
        Action<object> valueChanged;
        public GridEditorItem(RepositoryItem fRepositoryItem, string fName, object fValue, string group, Action<object> valueChanged)
        {
            this.fRepositoryItem = fRepositoryItem;
            this.fName = fName;
            this.fValue = fValue;
            this.group = group;
            this.valueChanged = valueChanged;
        }
        public string Name { get { return this.fName; } }
        public object Value { get { return this.fValue; } set { this.fValue = value; this.valueChanged?.Invoke(this.fValue); } }
        public RepositoryItem RepositoryItem { get { return this.fRepositoryItem; } }
        public string GroupName { get => group; }
    }

    public class GridEditorCollection : ArrayList
    {
        public GridEditorCollection()
        {
        }
        public new GridEditorItem this[int index] { get { return base[index] as GridEditorItem; } }
        public void Add(RepositoryItem fRepositoryItem, string fName, object fValue, string group, Action<object> valueChanged)
        {
            base.Add(new GridEditorItem(fRepositoryItem, fName, fValue, group, valueChanged));
        }
    }

}
