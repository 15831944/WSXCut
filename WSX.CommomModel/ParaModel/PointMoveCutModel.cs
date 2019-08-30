using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    [Serializable]
    public class PointMoveCutModel
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

        #region 切割     
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
    }
}
