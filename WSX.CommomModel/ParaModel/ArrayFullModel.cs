using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 布满排样
    /// </summary>
    [Serializable]
    public class ArrayFullModel
    {
        /// <summary>
        /// 板材宽度
        /// </summary>
        public double PlateWidth { get; set; }
        /// <summary>
        /// 板材高度
        /// </summary>
        public double PlateHeight { get; set; }
        /// <summary>
        /// 零件间距
        /// </summary>
        public double PartsSpacing { get; set; }
        /// <summary>
        /// 板材留边
        /// </summary>
        public double PlateRetainEdge { get; set; }
        /// <summary>
        /// 自动组合
        /// </summary>
        public bool IsAutoCombination { get; set; }
        /// <summary>
        /// 禁止旋转
        /// </summary>
        public bool IsBanRotation { get; set; }
        /// <summary>
        /// 自动共边
        /// </summary>
        public bool IsAutoCommonEdge { get; set; }
        /// <summary>
        /// 完成后删除原图
        /// </summary>
        public bool IsClearOriginalCompleted { get; set; }
    }
}
