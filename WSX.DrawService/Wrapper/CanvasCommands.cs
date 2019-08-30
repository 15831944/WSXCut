namespace WSX.DrawService.Wrapper
{
    public enum CanvasCommands
    {
        Redo,
        Undo,
        //Edit Command
        /// <summary>
        /// 选择模式
        /// </summary>
        SelectMode,
        /// <summary>
        /// 节点编辑模式
        /// </summary>
        NodeEditMode,
        /// <summary>
        /// 排序模式
        /// </summary>
        SortMode,
        /// <summary>
        /// 平移视图模式
        /// </summary>
        MoveViewMode,
        Pan,
        SetStartPoint,
        //Draw Command
        /// <summary>
        /// 直线
        /// </summary>
        Lines,
        /// <summary>
        /// 多段线
        /// </summary>
        MultiLine,
        /// <summary>
        /// 圆
        /// </summary>
        CircleCR,
        /// <summary>
        /// 三点圆弧
        /// </summary>
        Arc3P,
        /// <summary>
        /// 圆弧
        /// </summary>
        ArcCR,
        /// <summary>
        /// 孤立点
        /// </summary>
        SingleDot,
        /// <summary>
        /// 矩形
        /// </summary>
        SingleRectangle,
        /// <summary>
        /// 多边形
        /// </summary>
        Hexagon,
        /// <summary>
        /// 星形
        /// </summary>
        StarCommon,//
        /// <summary>
        /// 圆角矩形
        /// </summary>
        RoundRect,
        Group,
        /// <summary>
        /// 测量
        /// </summary>
        Measure,
    }
}
