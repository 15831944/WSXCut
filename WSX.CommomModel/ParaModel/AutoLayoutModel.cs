using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 自动排样
    /// </summary>
    [Serializable]
    public class AutoLayoutModel
    {
        /// <summary>
        /// 零件选择
        /// </summary>
        public PartSelectTypes PartSelectType { get; set; }
        /// <summary>
        /// 板材选择
        /// </summary>
        public PlateSelectTypes PlateSelectType { get; set; }
        /// <summary>
        /// 板材长度
        /// </summary>
        public double PlateLength { get; set; }
        /// <summary>
        /// 板材宽度
        /// </summary>
        public double PlateWidth { get; set; }
        /// <summary>
        /// 板材材料
        /// </summary>
        public string PlateMaterials { get; set; }
        /// <summary>
        /// 板材厚度
        /// </summary>
        public double PlateThickness { get; set; }
        /// <summary>
        /// 零件间距
        /// </summary>
        public double PartSpacing { get; set; }
        /// <summary>
        /// 板材留边
        /// </summary>
        public double PlateEdge { get; set; }
        /// <summary>
        /// 智能选择
        /// </summary>
        public bool IsIntelligenceSelect { get; set; }
        /// <summary>
        /// 排样策略
        /// </summary>
        public LayoutStrategyTypes LayoutStrategyType { get; set; }
        /// <summary>
        /// 旋转角度
        /// </summary>
        public double RotateAngle { get; set; }
        /// <summary>
        /// 自动组合
        /// </summary>
        public bool IsAutoCombine { get; set; }
        /// <summary>
        /// 排样方向
        /// </summary>
        public LayoutDirections LayoutDirection { get; set; }
        /// <summary>
        /// 自动优化
        /// </summary>
        public bool IsAutoOptimize { get; set; }
        /// <summary>
        /// 自动优化次数
        /// </summary>
        public int AutoOptimizeCount { get; set; }
        /// <summary>
        /// 生成余料
        /// </summary>
        public bool IsCreateRemnantPart { get; set; }
        /// <summary>
        /// 余料类型
        /// </summary>
        public RemnantPartTypes RemnantPartType { get; set; }
        /// <summary>
        /// 余料最小宽度
        /// </summary>
        public double RemnantPartMinWidth { get; set; }
        /// <summary>
        /// 全部共边
        /// </summary>
        public bool IsAllCombineEdge { get; set; }
        /// <summary>
        /// 自动共边
        /// </summary>
        public bool IsAutoCombineEdge { get; set; }
        /// <summary>
        /// 共边最短长度
        /// </summary>
        public double CombineEdgeMinLength { get; set; }
        /// <summary>
        /// 共边最大数量
        /// </summary>
        public int CombineEdgeMaxCount { get; set; }
        /// <summary>
        /// 不同长度共边
        /// </summary>
        public bool IsDifferLengthCombineEdge { get; set; }
        /// <summary>
        /// 缺口长度
        /// </summary>
        public double GapLength { get; set; }
        /// <summary>
        /// 先清除之前的排样
        /// </summary>
        public bool IsClearPreviousLayout { get; set; }
    }
    /// <summary>
    /// 零件选择
    /// </summary>
    [Serializable]
    public enum PartSelectTypes
    {
        /// <summary>
        /// 所有零件
        /// </summary>
        All,
        /// <summary>
        /// 选中的零件
        /// </summary>
        Selected,
    }
    /// <summary>
    /// 板材选择
    /// </summary>
    [Serializable]
    public enum PlateSelectTypes
    {
        /// <summary>
        /// 所有板材
        /// </summary>
        All,
        /// <summary>
        /// 选择的板材
        /// </summary>
        Selected,
        /// <summary>
        /// 板材标准
        /// </summary>
        Standard
    }
    /// <summary>
    /// 排样策略
    /// </summary>
    [Serializable]
    public enum LayoutStrategyTypes
    {
        /// <summary>
        /// 阵列式
        /// </summary>
        Array,
    }
    /// <summary>
    /// 排样方向
    /// </summary>
    [Serializable]
    public enum LayoutDirections
    {
        /// <summary>
        /// 从左到右
        /// </summary>
        LeftToRight,
        /// <summary>
        /// 从右到左
        /// </summary>
        RightToLeft,
        /// <summary>
        /// 从上到下
        /// </summary>
        TopToBottom,
        /// <summary>
        /// 从下到上
        /// </summary>
        BottomToTop,
    }
    /// <summary>
    /// 余料类型
    /// </summary>
    [Serializable]
    public enum RemnantPartTypes
    {
        /// <summary>
        /// 割线
        /// </summary>
        SecantLine,
        /// <summary>
        /// 外框
        /// </summary>
        OutlineBorder
    }
}
