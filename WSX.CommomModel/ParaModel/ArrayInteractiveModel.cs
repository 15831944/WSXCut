using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 交互式阵列
    /// </summary>
    [Serializable]
    public class ArrayInteractiveModel
    {
        /// <summary>
        /// 行间距
        /// </summary>
        public double RowSpacing { get; set; }
        /// <summary>
        /// 列间距
        /// </summary>
        public double ColumnSpacing { get; set; }
        /// <summary>
        /// 阵列后删除原图
        /// </summary>
        public bool IsClearOriginalCompleted { get; set; }
    }
}
