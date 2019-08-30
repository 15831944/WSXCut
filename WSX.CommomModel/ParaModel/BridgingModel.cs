using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 桥接参数
    /// </summary>
    [Serializable]
    public class BridgingModel
    {
        /// <summary>
        /// 相邻曲线之间的最大距离
        /// </summary>
        public double MaxDistance { get; set; } = 100;
        /// <summary>
        /// 桥接宽度
        /// </summary>
        public double Width { get; set; } = 1;
    }
}
