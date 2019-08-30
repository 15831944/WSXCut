using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 用户参数
    /// </summary>
    [Serializable]
    public class UserConfigModel
    {
        #region 自动优化
        /// <summary>
        /// 自动清除微小图形
        /// </summary>
        public bool IsAutoClearTinyFigure { get; set; }
        /// <summary>
        /// 最小图形长度
        /// </summary>
        public double TinyFigureLength { get; set; }
        /// <summary>
        /// 去除重复线
        /// </summary>
        public bool IsClearRepeatLine { get; set; }
        /// <summary>
        /// 重复线精度
        /// </summary>
        public double RepeatLinePrecision { get; set; }
        /// <summary>
        /// 自动合并相连线
        /// </summary>
        public bool IsAutoMergeLinkLine { get; set; }
        /// <summary>
        /// 相连线精度
        /// </summary>
        public double LinkLinePrecision { get; set; }
        /// <summary>
        /// 合并相连线按类型优先
        /// </summary>
        public MergeLinkLineTypes MergeLinkLineType { get; set; }
        /// <summary>
        /// 打开文件自动曲线平滑
        /// </summary>
        public bool IsAutoCurveSmoothing { get; set; }
        /// <summary>
        /// 自动曲线平滑精度
        /// </summary>
        public double CurveSmoothingPrecision { get; set; }
        /// <summary>
        /// 不对Bezier曲线进行平滑
        /// </summary>
        public bool IsUnableBezierSmoothing { get; set; }
        /// <summary>
        /// 自动识别dxf文件中的整圆
        /// </summary>
        public bool IsAutoDiscernCircle { get; set; }
        /// <summary>
        /// 自动排序
        /// </summary>
        public bool IsAutoSort { get; set; }
        /// <summary>
        /// 自动排序方式
        /// </summary>
        public AutoSortTypes AutoSortType { get; set; }
        /// <summary>
        /// 导入单位方式
        /// </summary>
        public ImportUnitTypes ImportUnitType { get; set; }
        /// <summary>
        /// 自动打撒dxf文件中的群组
        /// </summary>
        public bool IsAutoScatterGroups { get; set; }
        /// <summary>
        /// 自动文字转换为曲线
        /// </summary>
        public bool IsAutoWordConvertCurve { get; set; }
        /// <summary>
        /// 导入零件忽略单独的文字
        /// </summary>
        public bool IsIgnoreSeparateText { get; set; }
        /// <summary>
        /// 自动映射Dxf文件中的图层
        /// </summary>
        public bool IsAutoMapLayer { get; set; }
        #endregion

        #region 绘图板
        /// <summary>
        /// 显示坐标符号
        /// </summary>
        public bool IsShowCoordinateMark { get; set; }
        /// <summary>
        /// 坐标符号跟随零点
        /// </summary>
        public bool IsCoordinateMarkFollowZero { get; set; }
        /// <summary>
        /// 显示标尺
        /// </summary>
        public bool IsShowRulers { get; set; }
        /// <summary>
        /// 显示路径方向
        /// </summary>
        public bool IsShowPathDirection { get; set; }
        /// <summary>
        /// 显示网格
        /// </summary>
        public bool IsShowGrid { get; set; }
        /// <summary>
        /// 关键点自动吸附
        /// </summary>
        public bool IsKeyPointAdsorb { get; set; }
        /// <summary>
        /// 光标大小
        /// </summary>
        public double CursorSize { get; set; }
        /// <summary>
        /// 光标中心大小
        /// </summary>
        public double CursorCenterSize { get; set; }
        /// <summary>
        /// 夹点大小
        /// </summary>
        public double ClampPointSise { get; set; }
        /// <summary>
        /// 图形拾取精度
        /// </summary>
        public double FigureCapturePrecision { get; set; }
        /// <summary>
        /// 自动吸附距离
        /// </summary>
        public double AutoAdsorbDistance { get; set; }
        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color Background { get; set; }
        /// <summary>
        /// 标尺颜色
        /// </summary>
        public Color RulersColor { get; set; }
        /// <summary>
        /// 夹点颜色
        /// </summary>
        public Color ClampPointColor { get; set; }
        /// <summary>
        /// 标尺字体
        /// </summary>
        public string RulersFont { get; set; }
        /// <summary>
        /// 使用半透明选择框
        /// </summary>
        public bool IsUseTranslucenceBorder { get; set; }
        /// <summary>
        /// 外框显示方式
        /// </summary>
        public OutlineBorderShowTypes OutlineBorderShowType { get; set; }
        /// <summary>
        /// 单独颜色显示外框
        /// </summary>
        public bool IsUniqueColorShowOutLineBorder { get; set; }
        /// <summary>
        /// 自动打开最近文件
        /// </summary>
        public bool IsAutoOpenRecentFile { get; set; }
        /// <summary>
        /// 禁止鼠标和键盘快速拖动、复制
        /// </summary>
        public bool IsBanFastDragAndCopy { get; set; }
        /// <summary>
        /// 打开文件，使用上一次的停靠点设置
        /// </summary>
        public bool IsUsePreviousBerthSet { get; set; }
        /// <summary>
        /// 排样零件时自动跳转到零件
        /// </summary>
        public bool IsAutoJumpToPartsByLayout { get; set; }
        /// <summary>
        /// 达到预定加工次数后，自动跳转到排样结果
        /// </summary>
        public bool IsAutoJumpToNextLayout { get; set; }

        #endregion
    }

    /// <summary>
    /// 合并相连线优先方式
    /// </summary>
    [Serializable]
    public enum MergeLinkLineTypes
    {
        /// <summary>
        /// 方向优先
        /// </summary>
        Direction,
        /// <summary>
        /// 长度优先
        /// </summary>
        Length,
        /// <summary>
        /// 距离优先
        /// </summary>
        Distance
    }
    /// <summary>
    /// 自动排序方式
    /// </summary>
    [Serializable]
    public enum AutoSortTypes
    {
        /// <summary>
        /// 网格排序
        /// </summary>
        Grid,
        /// <summary>
        /// 最短空移
        /// </summary>
        ShortestEmptyMove,
        /// <summary>
        /// 刀模排序
        /// </summary>
        CutModel,
        /// <summary>
        /// 小图优先
        /// </summary>
        SmallFigurePriority,
    }
    /// <summary>
    /// 导入单位
    /// </summary>
    [Serializable]
    public enum ImportUnitTypes
    {
        /// <summary>
        /// 公制
        /// </summary>
        MetricSystem,
        /// <summary>
        /// 英制
        /// </summary>
        EnglishSystem,
        /// <summary>
        /// 读取Dxf文件格式单位
        /// </summary>
        ReadDxfUnit
    }
    /// <summary>
    /// 外框显示方式
    /// </summary>
    [Serializable]
    public enum OutlineBorderShowTypes
    {
        /// <summary>
        /// 不显示
        /// </summary>
        Disable,
        /// <summary>
        /// 显示不封闭图形外框
        /// </summary>
        VisibleNotClosed,
        /// <summary>
        /// 显示所有图形外框
        /// </summary>
        VisibleAll,
    }
}
