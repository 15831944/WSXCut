using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 冷却参数
    /// </summary>
    [Serializable]
    public class LayerCoolingModel
    {
        /// <summary>
        /// 冷却速度
        /// </summary>
        public double CoolingSpeed { get; set; }
        /// <summary>
        /// 喷嘴高度
        /// </summary>
        public double NozzleHeight { get; set; }
        /// <summary>
        /// 上抬高度
        /// </summary>
        public double LiftHeight { get; set; }
        /// <summary>
        /// 气体种类
        /// </summary>
        public string GasKind { get; set; }
        /// <summary>
        /// 气压
        /// </summary>
        public double GasPressure { get; set; }
        /// <summary>
        /// 用户备注
        /// </summary>
        public string UserNotes { get; set; }
    }
}
