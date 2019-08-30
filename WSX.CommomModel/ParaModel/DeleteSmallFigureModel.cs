using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 去除小图形
    /// </summary>
    [Serializable]
    public class DeleteSmallFigureModel
    {
        /// <summary>
        /// 微小图形长度
        /// </summary>
        public double FigureLength { get; set; }
    }
}
