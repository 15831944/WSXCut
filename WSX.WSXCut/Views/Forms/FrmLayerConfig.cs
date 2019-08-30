using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Physics;
using WSX.CommomModel.Physics.Converters;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.LayerPara;
using WSX.ControlLibrary.Utilities;

namespace WSX.WSXCut.Views.Forms
{
    public partial class FrmLayerConfig : DevExpress.XtraEditors.XtraForm
    {
        public LayerConfigModel Model { get; internal set; }
        private Dictionary<int, UCLayerCraftPara> ucLayers = new Dictionary<int, UCLayerCraftPara>();
        private List<int> layers;
        private TabPage tabCoolingPara = null;
        private UCLayerCoolingPara ucLayerCoolingPara = null;
        private TabPage tabEvaporatePara = null;
        private UCLayerEvaporatePara ucLayerEvaporatePara = null;
        private UCPointMoveCutPara ucPointMoveCutPara = null;
      
        public FrmLayerConfig()
        {
            InitializeComponent();
        }

        public FrmLayerConfig(LayerConfigModel layerConfig, List<int> layers) : this()
        {
            this.Model = mvvmContext1.GetViewModel<LayerConfigModel>();
            this.layers = layers;         
            CopyUtil.CopyModel(this.Model, layerConfig ?? DefaultParaHelper.GetDefaultLayerConfigModel());          
            this.InitializeBindings();         
            this.InitializeTabPages(layers);
            this.UcLayer_OnPathRecoolingChanged(false, "冷却参数");
            this.UcLayer_OnEvaporationFilmChanged(false, "蒸发参数");                             
            var monitor = new UnitMonitor(this.tPageLayerGlobal, () => this.Model.RaisePropertiesChanged());
            monitor.Listen();          
        }

        public FrmLayerConfig(LayerConfigModel layerConfig): this()
        {
            this.Model = mvvmContext1.GetViewModel<LayerConfigModel>();        
            CopyUtil.CopyModel(this.Model, layerConfig ?? DefaultParaHelper.GetDefaultLayerConfigModel());           
            this.InitializeBindings();
            this.InitializePointMoveCutParaPage();
            var monitor = new UnitMonitor(this.tPageLayerGlobal, () => this.Model.RaisePropertiesChanged());
            monitor.Listen();
        }

        private void InitializePointMoveCutParaPage()
        {
            TabPage tab = new TabPage();
            tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            tab.Location = new System.Drawing.Point(4, 23);
            tab.Padding = new System.Windows.Forms.Padding(3);
            tab.Size = new System.Drawing.Size(731, 569);
            tab.Text = "点动时开激光参数";
            this.ucPointMoveCutPara = new UCPointMoveCutPara(this.Model.PointMoveCut) { Dock = DockStyle.Fill };
            tab.Controls.Add(this.ucPointMoveCutPara);
            this.tabControl1.TabPages.Add(tab);          
        } 

        private void InitializeTabPages(List<int> layers)
        {
            foreach (int layer in layers)
            {
                LayerCraftModel model = null;
                if (this.Model.LayerCrafts.ContainsKey(layer))
                {
                    model = this.Model.LayerCrafts[layer];
                }
                if (!this.ucLayers.ContainsKey(layer))
                {
                    TabPage tab = new TabPage();
                    tab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
                    tab.Location = new System.Drawing.Point(4, 23);
                    tab.Padding = new System.Windows.Forms.Padding(3);
                    tab.Size = new System.Drawing.Size(731, 569);
                    if (layer == 15)
                    {
                        tab.Text = "最先加工";
                    }
                    else if (layer == 16)
                    {
                        tab.Text = "最后加工";
                    }
                    else
                    {
                        tab.Text = "工艺" + layer;
                    }
                    var ucLayer = new UCLayerCraftPara(model, tab.Text) { Dock = DockStyle.Fill };
                    ucLayer.OnEvaporationFilmChanged += UcLayer_OnEvaporationFilmChanged;
                    ucLayer.OnPathRecoolingChanged += UcLayer_OnPathRecoolingChanged;
                    tab.Controls.Add(ucLayer);
                    this.tabControl1.TabPages.Add(tab);
                    this.ucLayers.Add(layer, ucLayer);
                }
            }
        }

        private void UcLayer_OnPathRecoolingChanged(bool obj, string key)
        {
            bool isShow = obj;
            if (!isShow) isShow = this.ucLayers.Values.Count(e => e.Model.IsPathRecooling == true && !e.key.Equals(key)) > 0;
            if (isShow)
            {
                if (tabCoolingPara == null)
                {
                    tabCoolingPara = new TabPage();
                    tabCoolingPara.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
                    tabCoolingPara.Location = new System.Drawing.Point(4, 23);
                    tabCoolingPara.Padding = new System.Windows.Forms.Padding(3);
                    tabCoolingPara.Size = new System.Drawing.Size(731, 569);
                    tabCoolingPara.Text = "冷却参数";
                    ucLayerCoolingPara = new UCLayerCoolingPara(this.Model.LayerCooling) { Dock = DockStyle.Fill };
                    tabCoolingPara.Controls.Add(ucLayerCoolingPara);

                }
                int index = this.tabControl1.TabPages.IndexOf(tabCoolingPara);
                if (index < 0)
                {
                    this.tabControl1.TabPages.Add(tabCoolingPara);
                }
            }
            else
            {
                if (tabCoolingPara != null)
                {
                    this.tabControl1.TabPages.Remove(tabCoolingPara);
                }
            }
        }

        private void UcLayer_OnEvaporationFilmChanged(bool obj, string key)
        {
            bool isShow = obj;
            if (!isShow) isShow = this.ucLayers.Values.Count(e => e.Model.IsEvaporationFilm == true && !e.key.Equals(key)) > 0;
            if (isShow)
            {
                if (tabEvaporatePara == null)
                {
                    tabEvaporatePara = new TabPage();
                    tabEvaporatePara.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
                    tabEvaporatePara.Location = new System.Drawing.Point(4, 23);
                    tabEvaporatePara.Padding = new System.Windows.Forms.Padding(3);
                    tabEvaporatePara.Size = new System.Drawing.Size(731, 569);
                    tabEvaporatePara.Text = "蒸发参数";
                    ucLayerEvaporatePara = new UCLayerEvaporatePara(this.Model.LayerEvaporate) { Dock = DockStyle.Fill };
                    tabEvaporatePara.Controls.Add(ucLayerEvaporatePara);

                }
                int index = this.tabControl1.TabPages.IndexOf(tabEvaporatePara);
                if (index < 0)
                {
                    this.tabControl1.TabPages.Add(tabEvaporatePara);
                }
            }
            else
            {
                if (tabEvaporatePara != null)
                {
                    this.tabControl1.TabPages.Remove(tabEvaporatePara);
                }
            }
        }

        private void InitializeBindings()
        {           
            var fluent = mvvmContext1.OfType<LayerConfigModel>();
            #region 运动控制参数
            //fluent.SetBinding(this.rbtnUseCommon, c => c.Checked, x => x.EmptyMoveParamType,
            //        m => { return EmptyMoveParamTypes.UseCommon == m; },
            //        r => { return EmptyMoveParamTypes.UseCommon; });
            //fluent.SetBinding(this.rbtnUseAlone, c => c.Checked, x => x.EmptyMoveParamType,
            //    m => { return EmptyMoveParamTypes.UseAlone == m; },
            //    r => { return EmptyMoveParamTypes.UseAlone; });
            fluent.SetBinding(this.txtEmptyMoveSpeed.PopupEdit, c => c.Text, x => x.EmptyMoveSpeed, m => SpeedUnitConverter.Convert(m), r => SpeedUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtEmptyMoveSpeedX.PopupEdit, c => c.Text, x => x.EmptyMoveSpeedX, m => SpeedUnitConverter.Convert(m), r => SpeedUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtEmptyMoveSpeedY.PopupEdit, c => c.Text, x => x.EmptyMoveSpeedY, m => SpeedUnitConverter.Convert(m), r => SpeedUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtEmptyMoveAcceleratedSpeed.PopupEdit, c => c.Text, x => x.EmptyMoveAcceleratedSpeed, m => AccelerationUnitConverter.Convert(m), r => AccelerationUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtEmptyMoveAcceleratedSpeedX.PopupEdit, c => c.Text, x => x.EmptyMoveSpeedX, m => AccelerationUnitConverter.Convert(m), r => AccelerationUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtEmptyMoveAcceleratedSpeedY.PopupEdit, c => c.Text, x => x.EmptyMoveSpeedY, m => AccelerationUnitConverter.Convert(m), r => AccelerationUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtEmptyMoveLowpassHz.PopupEdit, c => c.Text, x => x.EmptyMoveLowpassHz);
            fluent.SetBinding(this.txtEmptyMoveLowpassXHz.PopupEdit, c => c.Text, x => x.EmptyMoveLowpassXHz);
            fluent.SetBinding(this.txtEmptyMoveLowpassYHz.PopupEdit, c => c.Text, x => x.EmptyMoveLowpassYHz);
            fluent.SetBinding(this.txtCheckEdgeSpeed.PopupEdit, c => c.Text, x => x.CheckEdgeSpeed, m => SpeedUnitConverter.Convert(m), r => SpeedUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtProcessAcceleratedSpeed.PopupEdit, c => c.Text, x => x.ProcessAcceleratedSpeed, m => AccelerationUnitConverter.Convert(m), r => AccelerationUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtProcessLowpassHz.PopupEdit, c => c.Text, x => x.ProcessLowpassHz);
            fluent.SetBinding(this.spCurveControlPrecision, c => c.Value, x => x.CurveControlPrecision);
            fluent.SetBinding(this.spCornerControlPrecision, c => c.Value, x => x.CornerControlPrecision);
            #endregion
            #region 默认参数
            fluent.SetBinding(this.txtDotBurstPulseFrequency.PopupEdit, c => c.Text, x => x.DotBurstPulseFrequency);
            fluent.SetBinding(this.txtDotBurstPeakPower.PopupEdit, c => c.Text, x => x.DotBurstPeakPower);
            fluent.SetBinding(this.txtDefalutAirPressure.PopupEdit, c => c.Text, x => x.DefalutAirPressure, m => PressureUnitConverter.Convert(m), r => PressureUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtOpenAirDelay.PopupEdit, c => c.Text, x => x.OpenAirDelay, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtFirstOpenAirDelay.PopupEdit, c => c.Text, x => x.FirstOpenAirDelay, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtExchangeAirDelay.PopupEdit, c => c.Text, x => x.ExchangeAirDelay, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtCoolingDotDelay.PopupEdit, c => c.Text, x => x.CoolingDotDelay, m => TimeUnitConverter.Convert(m), r => TimeUnitConverter.ConvertBack(r));
            fluent.SetBinding(this.txtPauseBackspaceDistance.PopupEdit, c => c.Text, x => x.PauseBackspaceDistance);
            #endregion
            #region 单位选择
            fluent.SetBinding(this.cmbUnitTimeTypes, c => c.SelectedIndex, x => x.UnitTimeType,
                m => { return (int)m; },
                r => { return (UnitTimeTypes)r; });
            fluent.SetBinding(this.cmbUnitSpeedTypes, c => c.SelectedIndex, x => x.UnitSpeedType,
                m => { return (int)m; },
                r => { return (UnitSpeedTypes)r; });
            fluent.SetBinding(this.cmbUnitAcceleratedType, c => c.SelectedIndex, x => x.UnitAcceleratedType,
                m => { return (int)m; },
                r => { return (UnitAcceleratedTypes)r; });
            fluent.SetBinding(this.cmbUnitPressureTypes, c => c.SelectedIndex, x => x.UnitPressureType,
                m => { return (int)m; },
                r => { return (UnitPressureTypes)r; });
            #endregion
            #region 跟随控制
            fluent.SetBinding(this.txtFollowMaxHeight.PopupEdit, c => c.Text, x => x.FollowMaxHeight);
            fluent.SetBinding(this.ckFrogStyleLift, c => c.Checked, x => x.IsFrogStyleLift);
            fluent.SetBinding(this.ckEnableFollowEmptyMove, c => c.Checked, x => x.IsEnableFollowEmptyMove);
            fluent.SetBinding(this.ckDisableFollowProcessed, c => c.Checked, x => x.IsDisableFollowProcessed);
            fluent.SetBinding(this.ckOnlyPositionZCoordinate, c => c.Checked, x => x.IsOnlyPositionZCoordinate);
            fluent.SetBinding(this.txtPositionZCoordinate.PopupEdit, c => c.Text, x => x.PositionZCoordinate);
            fluent.SetBinding(this.txtUnLiftMaxEmptyMoveLength.PopupEdit, c => c.Text, x => x.UnLiftMaxEmptyMoveLength);
            fluent.SetBinding(this.ckShortDistanceOptimization, c => c.Checked, x => x.IsShortDistanceOptimization);
            fluent.SetBinding(this.ckIsFollowWithFocus, c => c.Checked, x => x.IsFollowWithFocus);
            #endregion
            #region 高级
            fluent.SetBinding(this.ckEnableNurmbsIntepolation, c => c.Checked, x => x.IsEnableNurmbsIntepolation);
            fluent.SetBinding(this.ckAllAlarmsManuallyClear, c => c.Checked, x => x.IsAllAlarmsManuallyClear);
            fluent.SetBinding(this.ckGroupPrePiercing, c => c.Checked, x => x.IsGroupPrePiercing);
            fluent.SetBinding(this.ckMicroJointFlyingCut, c => c.Checked, x => x.IsMicroJointFlyingCut);
            fluent.SetBinding(this.ckEnableMachineProtection, c => c.Checked, x => x.IsEnableMachineProtection);
            fluent.SetBinding(this.txtCompensatePrecision, c => c.Value, x => x.CompensatePrecision);
            fluent.SetBinding(this.txtFlyingCutOverDistance, c => c.Value, x => x.FlyingCutOverDistance);
            #endregion
        }
        /// <summary>
        /// 获取工艺控件参数，保存到全局model
        /// </summary>
        private void GetTabPagesModel()
        {
            foreach (int layerid in this.ucLayers.Keys)
            {
                Model.LayerCrafts[layerid] = this.ucLayers[layerid].Model;
            }
            if (this.ucLayerCoolingPara != null)
            {
                Model.LayerCooling = this.ucLayerCoolingPara.Model;
            }
            if (this.ucLayerEvaporatePara != null)
            {
                Model.LayerEvaporate = this.ucLayerEvaporatePara.Model;
            }
            if(this.ucPointMoveCutPara != null)
            {
                Model.PointMoveCut = this.ucPointMoveCutPara.Model;
            }           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnOK.Focus();    //make DevExpress binding available
            this.GetTabPagesModel();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmLayerCraftSet_Load(object sender, EventArgs e)
        {
            panelCtrlUseAlone.Visible = this.rbtnUseAlone.Checked;
            panelCtrlUseCommon.Visible = !this.rbtnUseAlone.Checked;
        }

        private void rbtnUseCommon_CheckedChanged(object sender, EventArgs e)
        {
            panelCtrlUseAlone.Visible = false;
            panelCtrlUseCommon.Visible = false;

            panelCtrlUseAlone.Visible = this.rbtnUseAlone.Checked;
            panelCtrlUseCommon.Visible = !this.rbtnUseAlone.Checked;
        }

        private void cmbUnitSpeedTypes_SelectedIndexChanged(object sender, EventArgs e)
        {             
            this.Model.UnitSpeedType = (UnitSpeedTypes)this.cmbUnitSpeedTypes.SelectedIndex;
            UnitObserverFacade.Instance.SpeedUnitObserver.ChangeUnit(this.Model.UnitSpeedType);
        }

        private void cmbUnitTimeTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Model.UnitTimeType = (UnitTimeTypes)this.cmbUnitTimeTypes.SelectedIndex;
            UnitObserverFacade.Instance.TimeUnitObserver.ChangeUnit(this.Model.UnitTimeType);
        }

        private void cmbUnitAcceleratedType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Model.UnitAcceleratedType = (UnitAcceleratedTypes)this.cmbUnitAcceleratedType.SelectedIndex;
            UnitObserverFacade.Instance.AccelerationUnitObserver.ChangeUnit(this.Model.UnitAcceleratedType);
        }

        private void cmbUnitPressureTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Model.UnitPressureType = (UnitPressureTypes)this.cmbUnitPressureTypes.SelectedIndex;
            UnitObserverFacade.Instance.PressureUnitObserver.ChangeUnit(this.Model.UnitPressureType);
        }
    }
}
