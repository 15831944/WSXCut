using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 蒸发参数
    /// </summary>
    [Serializable]
    public class LayerEvaporateModel
    {
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
        /// 关光前延时
        /// </summary>
        public double LaserOpenDelay { get; set; }
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
        /// <summary>
        /// 用户备注
        /// </summary>
        public string UserNotes { get; set; }
    }
}
