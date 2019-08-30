using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 曲线平滑
    /// </summary>
    [Serializable]
    public class CurveSmoothingModel
    {
        /// <summary>
        /// 平滑精度
        /// </summary>
       public double PrecisionSize { get; set; }
    }
}
