using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 图层参数设置
    /// </summary>
    [Serializable]
    public class LayerConfigModel
    {
        #region 运动控制参数
        /// <summary>
        /// 空移速度
        /// </summary>
        public double EmptyMoveSpeed { get; set; }
        /// <summary>
        /// 空移速度X
        /// </summary>
        public double EmptyMoveSpeedX { get; set; }
        /// <summary>
        /// 空移速度Y
        /// </summary>
        public double EmptyMoveSpeedY { get; set; }
        /// <summary>
        /// 空移加速度
        /// </summary>
        public double EmptyMoveAcceleratedSpeed { get; set; }
        /// <summary>
        /// 空移加速度X
        /// </summary>
        public double EmptyMoveAcceleratedSpeedX { get; set; }
        /// <summary>
        /// 空移加速度Y
        /// </summary>
        public double EmptyMoveAcceleratedSpeedY { get; set; }
        /// <summary>
        /// 空移低通频率
        /// </summary>
        public double EmptyMoveLowpassHz { get; set; }
        /// <summary>
        /// 空移低通频率X
        /// </summary>
        public double EmptyMoveLowpassXHz { get; set; }
        /// <summary>
        /// 空移低通频率Y
        /// </summary>
        public double EmptyMoveLowpassYHz { get; set; }
        /// <summary>
        /// 检边速度
        /// </summary>
        public double CheckEdgeSpeed { get; set; }
        /// <summary>
        /// 加工加速度
        /// </summary>
        public double ProcessAcceleratedSpeed { get; set; }
        /// <summary>
        /// 加工低通频率
        /// </summary>
        public double ProcessLowpassHz { get; set; }
        /// <summary>
        /// 曲线控制精度
        /// </summary>
        public double CurveControlPrecision { get; set; }
        /// <summary>
        /// 拐角控制精度
        /// </summary>
        public double CornerControlPrecision { get; set; }
        /// <summary>
        /// XY空移参数设置
        /// </summary>
        public EmptyMoveParamTypes EmptyMoveParamType { get; set; }
        #endregion

        #region 默认参数
        /// <summary>
        /// 点射脉冲频率
        /// </summary>
        public double DotBurstPulseFrequency { get; set; }
        /// <summary>
        /// 点射峰值功率
        /// </summary>
        public double DotBurstPeakPower { get; set; }
        /// <summary>
        /// 默认气压
        /// </summary>
        public double DefalutAirPressure { get; set; }
        /// <summary>
        /// 开气延时
        /// </summary>
        public double OpenAirDelay { get; set; }
        /// <summary>
        /// 首点开气延时
        /// </summary>
        public double FirstOpenAirDelay { get; set; }
        /// <summary>
        /// 换气延时
        /// </summary>
        public double ExchangeAirDelay { get; set; }
        /// <summary>
        /// 冷却点延时
        /// </summary>
        public double CoolingDotDelay { get; set; }
        /// <summary>
        /// 暂停后退回距离
        /// </summary>
        public double PauseBackspaceDistance { get; set; }
        #endregion

        #region 单位选择
        /// <summary>
        /// 时间单位
        /// </summary>
        public UnitTimeTypes UnitTimeType { get; set; }
        /// <summary>
        /// 速度单位
        /// </summary>
        public UnitSpeedTypes UnitSpeedType { get; set; }
        /// <summary>
        /// 加速度单位
        /// </summary>
        public UnitAcceleratedTypes UnitAcceleratedType { get; set; }
        /// <summary>
        /// 气压单位
        /// </summary>
        public UnitPressureTypes UnitPressureType { get; set; }
        #endregion

        #region 跟随控制参数
        /// <summary>
        /// 跟随最大高度
        /// </summary>
        public double FollowMaxHeight { get; set; }
        /// <summary>
        /// 使用蛙跳式上抬
        /// </summary>
        public bool IsFrogStyleLift { get; set; }
        /// <summary>
        /// 空走时启用跟随
        /// </summary>
        public bool IsEnableFollowEmptyMove { get; set; }
        /// <summary>
        /// 加工时禁用跟随
        /// </summary>
        public bool IsDisableFollowProcessed { get; set; }
        /// <summary>
        /// 不跟随，只定位Z坐标
        /// </summary>
        public bool IsOnlyPositionZCoordinate { get; set; }
        /// <summary>
        /// Z轴定位坐标
        /// </summary>
        public double PositionZCoordinate { get; set; }
        /// <summary>
        /// 短距离不上抬的最大空移长度
        /// </summary>
        public double UnLiftMaxEmptyMoveLength { get; set; }
        /// <summary>
        /// 短距离空移优化
        /// </summary>
        public bool IsShortDistanceOptimization { get; set; }
        /// <summary>
        /// 渐进中变焦
        /// </summary>
        public bool IsFollowWithFocus { get; set; }
        #endregion

        #region 高级参数
        /// <summary>
        /// 启用NURBS样条插补
        /// </summary>
        public bool IsEnableNurmbsIntepolation { get; set; }
        /// <summary>
        /// 所有警报手动清除
        /// </summary>
        public bool IsAllAlarmsManuallyClear { get; set; }
        /// <summary>
        /// 自动分组预穿孔
        /// </summary>
        public bool IsGroupPrePiercing { get; set; }
        /// <summary>
        /// 微连使用飞切
        /// </summary>
        public bool IsMicroJointFlyingCut { get; set; }
        /// <summary>
        /// 启用机床保护
        /// </summary>
        public bool IsEnableMachineProtection { get; set; }
        /// <summary>
        /// 割缝补偿精度
        /// </summary>
        public double CompensatePrecision { get; set; }
        /// <summary>
        /// 飞行切割过切距离
        /// </summary>
        public double FlyingCutOverDistance { get; set; }
        #endregion

        /// <summary>
        /// 图层工艺参数
        /// </summary>
        public Dictionary<int, LayerCraftModel> LayerCrafts { get; set; } = new Dictionary<int, LayerCraftModel>();
        /// <summary>
        /// 冷却参数
        /// </summary>
        public LayerCoolingModel LayerCooling { get; set; } = new LayerCoolingModel();
        /// <summary>
        /// 蒸发参数
        /// </summary>
        public LayerEvaporateModel LayerEvaporate { get; set; } = new LayerEvaporateModel();
        /// <summary>
        /// 点动切割参数
        /// </summary>
        public PointMoveCutModel PointMoveCut { get; set; } = new PointMoveCutModel();
    }
    /// <summary>
    /// 空移参数设置
    /// </summary>
    [Serializable]
    public enum EmptyMoveParamTypes
    {
        /// <summary>
        /// 使用统一参数
        /// </summary>
        UseCommon,
        /// <summary>
        /// 使用单独的参数
        /// </summary>
        UseAlone,
    }
    /// <summary>
    /// 时间单位
    /// </summary>
    [Serializable]
    public enum UnitTimeTypes
    {       
        [Description("毫秒")]
        Millisecond,       
        [Description("秒")]
        Second,     
        [Description("分")]
        Minute
    }
    /// <summary>
    /// 速度单位
    /// </summary>
    [Serializable]
    public enum UnitSpeedTypes
    {        
        [Description("毫米/秒")]
        Millimeter_Second,      
        [Description("米/秒")]
        Meter_Second,      
        [Description("米/分")]
        Meter_Minute,      
        [Description("毫米/分")]
        Millimeter_Minute,
    }
    /// <summary>
    /// 加速度单位
    /// </summary>
    [Serializable]
    public enum UnitAcceleratedTypes
    {
        [Description("毫米/秒^2")]
        MillimeterPerSecondSquared,
        [Description("G")]
        G,
        [Description("米/分^2")]
        MeterPerMinuteSquared,
        [Description("米/秒^2")]
        MeterPerSecondSquared
    }
    /// <summary>
    /// 气压单位
    /// </summary>
    [Serializable]
    public enum UnitPressureTypes
    {
        [Description("BAR")]
        BAR,
        [Description("PSI")]
        PSI,
        [Description("MPa")]
        MPa,
    }
}
