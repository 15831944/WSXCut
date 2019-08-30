using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.DrawModel.Compensation
{
    /// <summary>
    /// 补偿类型
    /// </summary>
    public enum CompensationType
    {
        /// <summary>
        /// 自动，内膜内缩，外膜外扩
        /// </summary>
        Auto,
        /// <summary>
        /// 全部内缩
        /// </summary>
        AllInner,
        /// <summary>
        /// 全部外扩
        /// </summary>
        AllOuter
    }
}
