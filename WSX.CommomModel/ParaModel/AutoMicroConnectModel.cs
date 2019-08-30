using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 自动微连
    /// </summary>
    [Serializable]
    public class AutoMicroConnectModel
    {
        /// <summary>
        /// 微连方式
        /// </summary>
        public MicroConnectTypes MicroConnectType { get; set; }
        /// <summary>
        /// 微连数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 间隔距离
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// 微连大小
        /// </summary>
        public double Size { get; set; }
        /// <summary>
        /// 微连有效方式
        /// </summary>
        public MicroConnectValidTypes MicroConnectValidType { get; set; }
        /// <summary>
        /// 起点不微连
        /// </summary>
        public bool IsStartPointUnusable { get; set; }
        /// <summary>
        /// 拐角不微连
        /// </summary>
        public bool IsCornerUnusable { get; set; }
        /// <summary>
        /// 最小尺寸
        /// </summary>
        public bool IsUseMinSize { get; set; }
        /// <summary>
        /// 最大尺寸
        /// </summary>
        public bool IsUseMaxSize { get; set; }
        /// <summary>
        /// 过滤最大尺寸
        /// </summary>
        public double FilterMaxSize { get; set; }
        /// <summary>
        /// 过滤最小尺寸
        /// </summary>
        public double FilterMinSize { get; set; }
    }
    /// <summary>
    /// 微连样式
    /// </summary>
    [Serializable]
    public enum MicroConnectTypes
    {
        /// <summary>
        /// 按数量
        /// </summary>
        Number,
        /// <summary>
        /// 按间距
        /// </summary>
        Spacing,
    }
    /// <summary>
    /// 微连有效方式
    /// </summary>
    [Serializable]
    public enum MicroConnectValidTypes
    {
        /// <summary>
        /// 所有图形有效
        /// </summary>
        All,
        /// <summary>
        /// 选中图形有效
        /// </summary>
        Selected
    }
}
