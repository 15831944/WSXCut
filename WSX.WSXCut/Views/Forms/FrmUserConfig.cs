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
using WSX.GlobalData.Model;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmUserConfig : DevExpress.XtraEditors.XtraForm
    {
        public UserConfigModel Model { get; set; }
        public FrmUserConfig()
        {
            InitializeComponent();
        }

        public FrmUserConfig(UserConfigModel userConfig) : this()
        {
            this.Model = CopyUtil.DeepCopy(userConfig);
            if (!this.mvvmContext1.IsDesignMode)
            {
                if (this.Model == null) this.Model = new UserConfigModel();
                this.mvvmContext1.SetViewModel(typeof(UserConfigModel), this.Model);
                InitializeBindings();
                InitializeBindings();
            }
        }

        private void InitializeBindings()
        {
            var fluent = mvvmContext1.OfType<UserConfigModel>();

            #region 自动优化
            fluent.SetBinding(this.ckAutoClearTinyFigure, c => c.Checked, x => x.IsAutoClearTinyFigure);
            fluent.SetBinding(this.txtTinyFigureLength.PopupEdit, c => c.Text, x => x.TinyFigureLength);
            fluent.SetBinding(this.ckClearRepeatLine, c => c.Checked, x => x.IsClearRepeatLine);
            fluent.SetBinding(this.txtRepeatLinePrecision.PopupEdit, c => c.Text, x => x.RepeatLinePrecision);
            fluent.SetBinding(this.ckAutoMergeLinkLine, c => c.Checked, x => x.IsAutoMergeLinkLine);
            fluent.SetBinding(this.txtLinkLinePrecision.PopupEdit, c => c.Text, x => x.LinkLinePrecision);
            fluent.SetBinding(this.cmbMergeLinkLineTypes, c => c.SelectedIndex, x => x.MergeLinkLineType,
                m => { return (int)m; },
                r => { return (MergeLinkLineTypes)r; });
            fluent.SetBinding(this.ckIsAutoCurveSmoothing, c => c.Checked, x => x.IsAutoCurveSmoothing);
            fluent.SetBinding(this.txtCurveSmoothingPrecision.PopupEdit, c => c.Text, x => x.CurveSmoothingPrecision);
            fluent.SetBinding(this.ckUnableBezierSmoothing, c => c.Checked, x => x.IsUnableBezierSmoothing);
            fluent.SetBinding(this.ckAutoDiscernCircle, c => c.Checked, x => x.IsAutoDiscernCircle);
            fluent.SetBinding(this.ckAutoSort, c => c.Checked, x => x.IsAutoSort);
            fluent.SetBinding(this.rbtnGrid, e => e.Checked, x => x.AutoSortType,
                m => { return AutoSortTypes.Grid == m; },
                r => { return AutoSortTypes.Grid; });
            fluent.SetBinding(this.rbtnShortestEmptyMove, e => e.Checked, x => x.AutoSortType,
                m => { return AutoSortTypes.ShortestEmptyMove == m; },
                r => { return AutoSortTypes.ShortestEmptyMove; });
            fluent.SetBinding(this.rbtnCutModel, e => e.Checked, x => x.AutoSortType,
                 m => { return AutoSortTypes.CutModel == m; },
                 r => { return AutoSortTypes.CutModel; });
            fluent.SetBinding(this.rbtnSmallFigurePriority, e => e.Checked, x => x.AutoSortType,
                 m => { return AutoSortTypes.SmallFigurePriority == m; },
                 r => { return AutoSortTypes.SmallFigurePriority; });
            fluent.SetBinding(this.rbtnMetricSystem, e => e.Checked, x => x.ImportUnitType,
                 m => { return ImportUnitTypes.MetricSystem == m; },
                 r => { return ImportUnitTypes.MetricSystem; });
            fluent.SetBinding(this.rbtnEnglishSystem, e => e.Checked, x => x.ImportUnitType,
                 m => { return ImportUnitTypes.EnglishSystem == m; },
                 r => { return ImportUnitTypes.EnglishSystem; });
            fluent.SetBinding(this.rbtnReadDxfUnit, e => e.Checked, x => x.ImportUnitType,
                 m => { return ImportUnitTypes.ReadDxfUnit == m; },
                 r => { return ImportUnitTypes.ReadDxfUnit; });
            fluent.SetBinding(this.ckAutoScatterGroups, c => c.Checked, x => x.IsAutoScatterGroups);
            fluent.SetBinding(this.ckAutoWordConvertCurve, c => c.Checked, x => x.IsAutoWordConvertCurve);
            fluent.SetBinding(this.ckIgnoreSeparateText, c => c.Checked, x => x.IsIgnoreSeparateText);
            fluent.SetBinding(this.ckAutoMapLayer, c => c.Checked, x => x.IsAutoMapLayer);
            #endregion

            #region 绘图板
            fluent.SetBinding(this.ckShowCoordinateMark, c => c.Checked, x => x.IsShowCoordinateMark);
            fluent.SetBinding(this.ckCoordinateMarkFollowZero, c => c.Checked, x => x.IsCoordinateMarkFollowZero);
            fluent.SetBinding(this.ckShowRulers, c => c.Checked, x => x.IsShowRulers);
            fluent.SetBinding(this.ckShowPathDirection, c => c.Checked, x => x.IsShowPathDirection);
            fluent.SetBinding(this.ckShowGrid, c => c.Checked, x => x.IsShowGrid);
            fluent.SetBinding(this.ckKeyPointAdsorb, c => c.Checked, x => x.IsKeyPointAdsorb);
            fluent.SetBinding(this.txtCursorSize.PopupEdit, c => c.Text, x => x.CursorSize);
            fluent.SetBinding(this.txtCursorCenterSize.PopupEdit, c => c.Text, x => x.CursorCenterSize);
            fluent.SetBinding(this.txtClampPointSise.PopupEdit, c => c.Text, x => x.ClampPointSise);
            fluent.SetBinding(this.txtFigureCapturePrecision.PopupEdit, c => c.Text, x => x.FigureCapturePrecision);
            fluent.SetBinding(this.txtAutoAdsorbDistance.PopupEdit, c => c.Text, x => x.AutoAdsorbDistance);
            fluent.SetBinding(this.cmbBackground, c => c.SelectedText, x => x.Background);
            fluent.SetBinding(this.cmbRulersColor, c => c.SelectedText, x => x.RulersColor);
            fluent.SetBinding(this.cmbClampPointColor, c => c.SelectedText, x => x.ClampPointColor);
            fluent.SetBinding(this.cmbRulersFont, c => c.SelectedText, x => x.RulersFont);
            fluent.SetBinding(this.ckUseTranslucenceBorder, c => c.Checked, x => x.IsUseTranslucenceBorder);
            fluent.SetBinding(this.rbtnDisable, e => e.Checked, x => x.OutlineBorderShowType,
               m => { return OutlineBorderShowTypes.Disable == m; },
               r => { return OutlineBorderShowTypes.Disable; });
            fluent.SetBinding(this.rbtnVisibleNotClosed, e => e.Checked, x => x.OutlineBorderShowType,
               m => { return OutlineBorderShowTypes.VisibleNotClosed == m; },
               r => { return OutlineBorderShowTypes.VisibleNotClosed; });
            fluent.SetBinding(this.rbtnVisibleAll, e => e.Checked, x => x.OutlineBorderShowType,
               m => { return OutlineBorderShowTypes.VisibleAll == m; },
               r => { return OutlineBorderShowTypes.VisibleAll; });
            fluent.SetBinding(this.ckUniqueColorShowOutLineBorder, c => c.Checked, x => x.IsUniqueColorShowOutLineBorder);
            fluent.SetBinding(this.ckAutoOpenRecentFile, c => c.Checked, x => x.IsAutoOpenRecentFile);
            fluent.SetBinding(this.ckBanFastDragAndCopy, c => c.Checked, x => x.IsBanFastDragAndCopy);
            fluent.SetBinding(this.ckUsePreviousBerthSet, c => c.Checked, x => x.IsUsePreviousBerthSet);
            fluent.SetBinding(this.ckAutoJumpToPartsByLayout, c => c.Checked, x => x.IsAutoJumpToPartsByLayout);
            fluent.SetBinding(this.ckAutoJumpToNextLayout, c => c.Checked, x => x.IsAutoJumpToNextLayout);
            #endregion
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
    }
}
