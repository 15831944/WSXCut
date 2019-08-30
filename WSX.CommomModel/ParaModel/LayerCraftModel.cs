using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 工艺图层参数
    /// </summary>
    [Serializable]
    public class LayerCraftModel
    {
        /// <summary>
        /// 材料类型
        /// </summary>
        public string MaterialType { get; set; }
        /// <summary>
        /// 厚度
        /// </summary>
        public double Thickness { get; set; }
        /// <summary>
        /// 喷嘴
        /// </summary>
        public string Nozzle { get; set; }
        /// <summary>
        /// 短距离不上抬
        /// </summary>
        public bool IsShortUnLift { get; set; }
        /// <summary>
        /// 预穿孔
        /// </summary>
        public bool IsPrePierce { get; set; }
        /// <summary>
        /// 蒸发去膜
        /// </summary>
        public bool IsEvaporationFilm { get; set; }
        /// <summary>
        /// 二次冷却
        /// </summary>
        public bool IsPathRecooling { get; set; }
        /// <summary>
        /// 多次
        /// </summary>
        public bool IsMultiTime { get; set; }
        /// <summary>
        /// 次数
        /// </summary>
        public int MultiTime { get; set; }
        /// <summary>
        /// 不关气
        /// </summary>
        public bool IsKeepPuffing { get; set; }
        /// <summary>
        /// 不加工
        /// </summary>
        public bool IsUnprocessed { get; set; }
        /// <summary>
        /// 不跟随
        /// </summary>
        public bool IsNoFollow { get; set; }
        /// <summary>
        /// 加工方式
        /// </summary>
        public ProcessedTypes ProcessedType { get; set; }

        #region 切割
        /// <summary>
        /// 切割速度
        /// </summary>
        public double CutSpeed { get; set; }
        /// <summary>
        /// 上抬高度
        /// </summary>
        public double LiftHeight { get; set; }
        /// <summary>
        /// 喷嘴高度
        /// </summary>
        public double NozzleHeight { get; set; }
        /// <summary>
        /// 气体种类
        /// </summary>
        public string GasKind { get; set; } 
        /// <summary>
        /// 气压
        /// </summary>
        public double GasPressure { get; set; }
        /// <summary>
        /// 峰值功率值
        /// </summary>
        public double PowerValue { get; set; }
        /// <summary>
        /// 峰值功率百分比
        /// </summary>
        public double PowerPercent { get; set; }
        /// <summary>
        /// 占空比
        /// </summary>
        public double PulseDutyFactorPercent { get; set; }
        /// <summary>
        /// 脉冲频率
        /// </summary>
        public double PulseFrequency { get; set; }
        /// <summary>
        /// 光斑直径
        /// </summary>
        public double BeamSize { get; set; }
        /// <summary>
        /// 焦点位置
        /// </summary>
        public double FocusPosition { get; set; }
        /// <summary>
        /// 停留时间
        /// </summary>
        public double DelayTime { get; set; }
        /// <summary>
        /// 关光前延时
        /// </summary>
        public double LaserOffDelay { get; set; }
        /// <summary>
        /// 慢速起步
        /// </summary>
        public bool IsSlowStart { get; set; }
        /// <summary>
        /// 慢速停止
        /// </summary>
        public bool IsSlowStop { get; set; }
        /// <summary>
        /// 起步距离
        /// </summary>
        public double SlowStartDistance { get; set; }
        /// <summary>
        /// 停止距离
        /// </summary>
        public double SlowStopDistance { get; set; }
        /// <summary>
        /// 起步速度
        /// </summary>
        public double SlowStartSpeed { get; set; }
        /// <summary>
        /// 停止速度
        /// </summary>
        public double SlowStopSpeed { get; set; }
        ///// <summary>
        ///// 动态调节功率
        ///// </summary>
        //public bool IsDynamicAdjustPower { get; set; }
        ///// <summary>
        ///// 动态调节频率
        ///// </summary>
        //public bool IsDynamicAdjustFrequency { get; set; }
        ///// <summary>
        ///// 绝对值显示
        ///// </summary>
        //public bool IsShowAbs { get; set; }
        /// <summary>
        /// 功率控制参数
        /// </summary>
        public PowerControlModel PwrCtrlPara { get; set; } = new PowerControlModel();
        #endregion 

        #region 穿孔
        /// <summary>
        /// 穿孔等级
        /// </summary>
        public PierceLevels PierceLevel { get; set; }
        /// <summary>
        /// 等级1参数
        /// </summary>
        public PierceParameters PierceLevel1 { get; set; } = new PierceParameters();
        /// <summary>
        /// 等级2参数
        /// </summary>
        public PierceParameters PierceLevel2 { get; set; } = new PierceParameters();
        /// <summary>
        /// 等级3参数
        /// </summary>
        public PierceParameters PierceLevel3 { get; set; } = new PierceParameters();
        #endregion

        /// <summary>
        /// 用户备注
        /// </summary>
        public string UserNotes { get; set; }
    }
    /// <summary>
    /// 加工方式
    /// </summary>
    [Serializable]
    public enum ProcessedTypes
    {
        /// <summary>
        /// 标准
        /// </summary>
        Standard,
        /// <summary>
        /// 固定高度
        /// </summary>
        FixedHeight,
        /// <summary>
        /// 外部跟随
        /// </summary>
        ExternalFollow
    }
    /// <summary>
    /// 穿孔等级
    /// </summary>
    [Serializable]
    public enum PierceLevels
    {
        /// <summary>
        /// 不穿孔
        /// </summary>
        None,
        /// <summary>
        /// 等级1
        /// </summary>
        Level1,
        /// <summary>
        /// 等级2
        /// </summary>
        Level2,
        /// <summary>
        /// 等级3
        /// </summary>
        Level3,
    }
    /// <summary>
    /// 穿孔参数
    /// </summary>
    [Serializable]
    public class PierceParameters
    {
        /// <summary>
        /// 是否渐进
        /// </summary>
        public bool IsStepTime { get; set; }
        /// <summary>
        /// 渐进时间
        /// </summary>
        public double StepTime { get; set; }
        /// <summary>
        /// 喷嘴高度
        /// </summary>
        public double NozzleHeight { get; set; }
        /// <summary>
        /// 气体种类
        /// </summary>
        public string GasKind { get; set; }
        /// <summary>
        /// 气压
        /// </summary>
        public double GasPressure { get; set; }
        /// <summary>
        /// 峰值功率值
        /// </summary>
        public double PowerValue { get; set; }
        /// <summary>
        /// 峰值功率百分比
        /// </summary>
        public double PowerPercent { get; set; }
        /// <summary>
        /// 占空比
        /// </summary>
        public double PulseDutyFactorPercent { get; set; }
        /// <summary>
        /// 脉冲频率
        /// </summary>
        public double PulseFrequency { get; set; }
        /// <summary>
        /// 光斑直径
        /// </summary>
        public double BeamSize { get; set; }
        /// <summary>
        /// 焦点位置
        /// </summary>
        public double FocusPosition { get; set; }
        /// <summary>
        /// 停留时间
        /// </summary>
        public double DelayTime { get; set; }
        /// <summary>
        /// 停光吹气
        /// </summary>
        public bool IsExtraPuffing { get; set; }
        /// <summary>
        /// 停光吹气
        /// </summary>
        public double ExtraPuffing { get; set; }
    }
}
