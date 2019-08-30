using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 微连参数
    /// </summary>
    [Serializable]
    public class MicroConnectParamsModel
    {
        /// <summary>
        /// 微连长度
        /// </summary>
        public double Length { get; set; }
        /// <summary>
        /// 应用于相似图形
        /// </summary>
        public bool IsApplyToSimilarFigure { get; set; }
    }
}
