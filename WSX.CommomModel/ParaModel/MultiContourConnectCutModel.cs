using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 多轮廓连切
    /// </summary>
    [Serializable]
    public class MultiContourConnectCutModel
    {
        /// <summary>
        /// 微连长度
        /// </summary>
        public double MicroConnectLength { get; set; }
        /// <summary>
        /// 最大连切长度
        /// </summary>
        public double MaxConnectLength { get; set; }
        /// <summary>
        /// 禁止修改切割路劲
        /// </summary>
        public bool IsBanModifyCutPath { get; set; }
    }
}
