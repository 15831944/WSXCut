using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.GlobalData
{
    public class MainEvent
    {
        /// <summary>
        /// 起点
        /// </summary>
        public readonly static string OnStartPoint = "OnStartPoint";
        /// <summary>
        /// 停靠
        /// </summary>
        public readonly static string OnDockPosition = "OnDockPosition";
        /// <summary>
        /// 补偿
        /// </summary>
        public readonly static string OnGapCompensation = "OnGapCompensation";
        /// <summary>
        /// 阳切
        /// </summary>
        public readonly static string OnOuterCut = "OnOuterCut";
        /// <summary>
        /// 阴切
        /// </summary>
        public readonly static string OnInnerCut = "OnInnerCut";
        /// <summary>
        /// 环切
        /// </summary>
        public readonly static string OnSurroundCut = "OnSurroundCut";
        /// <summary>
        /// 微连
        /// </summary>
        public readonly static string OnMicroConnectItem = "OnMicroConnectItem";
        /// <summary>
        /// 反向
        /// </summary>
        public readonly static string OnReverseItem = "OnReverseItem";
        /// <summary>
        /// 排序
        /// </summary>
        public readonly static string OnSortItem = "OnSortItem";
        /// <summary>
        /// 阵列
        /// </summary>
        public readonly static string OnArrayLayoutItem = "OnArrayLayoutItem";
        /// <summary>
        /// 桥接
        /// </summary>
        public readonly static string OnBridging = "OnBridging";
        /// <summary>
        /// 测量
        /// </summary>
        public readonly static string OnMeasure = "OnMeasure";
        /// <summary>
        /// 直线
        /// </summary>
        public readonly static string OnLine = "OnLine";
        /// <summary>
        /// 矩形
        /// </summary>
        public readonly static string OnRectangleItem = "OnRectangleItem";
        /// <summary>
        /// 圆
        /// </summary>
        public readonly static string OnCircleItem = "OnCircleItem";
        /// <summary>
        /// 多段线
        /// </summary>
        public readonly static string OnMultiLineItem = "OnMultiLineItem";
        /// <summary>
        /// 单点
        /// </summary>
        public readonly static string OnDot = "OnDot";
        /// <summary>
        /// 文字
        /// </summary>
        public readonly static string OnWord = "OnWord";
        /// <summary>
        /// 群组
        /// </summary>
        public readonly static string OnGroupItem = "OnGroupItem";
        /// <summary>
        /// 模拟
        /// </summary>
        public readonly static string OnSimulate = "OnSimulate";
        /// <summary>
        /// 模拟范围
        /// </summary>
        public readonly static string OnRangeSimulateSpeed = "OnRangeSimulateSpeed";
        /// <summary>
        /// 寻边
        /// </summary>
        public readonly static string OnFindEdgeItem = "OnFindEdgeItem";
        /// <summary>
        /// PLC过程
        /// </summary>
        public readonly static string OnPLCProcessItem = "OnPLCProcessItem";
        /// <summary>
        /// 回原点
        /// </summary>
        public readonly static string OnGoOriginItem = "OnGoOriginItem";
        /// <summary>
        /// 误差测定
        /// </summary>
        public readonly static string OnErrorMeasure = "OnErrorMeasure";
        /// <summary>
        /// 选择
        /// </summary>
        public readonly static string OnSelect = "OnSelect";
        /// <summary>
        /// 节点编辑
        /// </summary>
        public readonly static string OnEditNode = "OnEditNode";
        /// <summary>
        /// 平移视图
        /// </summary>
        public readonly static string OnTranslateView = "OnTranslateView";
        /// <summary>
        /// 排序模式
        /// </summary>
        public readonly static string OnSortMode = "OnSortMode";
        /// <summary>
        /// 全选
        /// </summary>
        public readonly static string OnSelectAll = "OnSelectAll";
        /// <summary>
        /// 取消选择
        /// </summary>
        public readonly static string OnCancelSelect = "OnCancelSelect";
        /// <summary>
        /// 批量修改
        /// </summary>
        public readonly static string OnBatchChange = "OnBatchChange";
        /// <summary>
        /// 禁止快速拖动和复制
        /// </summary>
        public readonly static string OnBanFastDragCopy = "OnBanFastDragCopy";
        /// <summary>
        /// 选择不封闭图形
        /// </summary>
        public readonly static string OnSelectGapFigure = "OnSelectGapFigure";
        /// <summary>
        /// 选择相似图形（区分角度）
        /// </summary>
        public readonly static string OnSelectSimilarFigure = "OnSelectSimilarFigure";
        /// <summary>
        /// 选择所有外模
        /// </summary>
        public readonly static string OnSelectAllExternalModes = "OnSelectAllExternalModes";
        /// <summary>
        /// 选择所有内模
        /// </summary>
        public readonly static string OnSelectAllInternalModes = "OnSelectAllInternalModes";
        /// <summary>
        /// 选择所有小于指定尺寸的图形
        /// </summary>
        public readonly static string OnSelectAllFigureSmaller = "OnSelectAllFigureSmaller";
        /// <summary>
        /// 锁定背景
        /// </summary>
        public readonly static string OnLockSelectBackground = "OnLockSelectBackground";
        /// <summary>
        /// 剪切
        /// </summary>
        public readonly static string OnCut = "OnCut";
        /// <summary>
        /// 复制
        /// </summary>
        public readonly static string OnCopy = "OnCopy";
        /// <summary>
        /// 带基点复制
        /// </summary>
        public readonly static string OnCopyBasePoint = "OnCopyBasePoint";
        /// <summary>
        /// 粘贴
        /// </summary>
        public readonly static string OnPaste = "OnPaste";
        /// <summary>
        /// 删除
        /// </summary>
        public readonly static string OnDelete = "OnDelete";
        /// <summary>
        /// 选择所有多段线
        /// </summary>
        public readonly static string OnSelectAllMultiLines = "OnSelectAllMultiLines";
        /// <summary>
        /// 选择所有圆
        /// </summary>
        public readonly static string OnSelectAllCircles = "OnSelectAllCircles";
        /// <summary>
        /// 选择所有Bezier曲线
        /// </summary>
        public readonly static string OnSelectAllBezierCurves = "OnSelectAllBezierCurves";
        /// <summary>
        /// 工艺0
        /// </summary>
        public readonly static string OnSelectLayerCraft0 = "OnSelectLayerCraft0";
        /// <summary>
        /// 工艺1
        /// </summary>
        public readonly static string OnSelectLayerCraft1 = "OnSelectLayerCraft1";
        /// <summary>
        /// 工艺2
        /// </summary>
        public readonly static string OnSelectLayerCraft2 = "OnSelectLayerCraft2";
        /// <summary>
        /// 显示不封闭图形外框
        /// </summary>
        public readonly static string OnDisplayGapFigureFrame = "OnDisplayGapFigureFrame";
        /// <summary>
        /// 红色显示不封闭图形
        /// </summary>
        public readonly static string OnRedDisplayGapFigure = "OnRedDisplayGapFigure";
        /// <summary>
        /// 显示序号
        /// </summary>
        public readonly static string OnDisplaySerialNumber = "OnDisplaySerialNumber";
        /// <summary>
        /// 显示路径起点
        /// </summary>
        public readonly static string OnDispalyPathStartPoint = "OnDispalyPathStartPoint";
        /// <summary>
        /// 显示加工路径
        /// </summary>
        public readonly static string OnDisplayProcessPath = "OnDisplayProcessPath";
        /// <summary>
        /// 显示空移路径
        /// </summary>
        public readonly static string OnDisplayEmptyMovePath = "OnDisplayEmptyMovePath";
        /// <summary>
        /// 图形居中
        /// </summary>
        public readonly static string OnFigureCenter = "OnFigureCenter";
        /// <summary>
        /// 工艺3
        /// </summary>
        public readonly static string OnSelectLayerCraft3 = "OnSelectLayerCraft3";
        /// <summary>
        /// 工艺4
        /// </summary>
        public readonly static string OnSelectLayerCraft4 = "OnSelectLayerCraft4";
        /// <summary>
        /// 工艺5
        /// </summary>
        public readonly static string OnSelectLayerCraft5 = "OnSelectLayerCraft5";
        /// <summary>
        /// 工艺6
        /// </summary>
        public readonly static string OnSelectLayerCraft6 = "OnSelectLayerCraft6";
        /// <summary>
        /// 工艺7
        /// </summary>
        public readonly static string OnSelectLayerCraft7 = "OnSelectLayerCraft7";
        /// <summary>
        /// 工艺8
        /// </summary>
        public readonly static string OnSelectLayerCraft8 = "OnSelectLayerCraft8";
        /// <summary>
        /// 工艺9
        /// </summary>
        public readonly static string OnSelectLayerCraft9 = "OnSelectLayerCraft9";
        /// <summary>
        /// 工艺10
        /// </summary>
        public readonly static string OnSelectLayerCraft10 = "OnSelectLayerCraft10";
        /// <summary>
        /// 工艺11
        /// </summary>
        public readonly static string OnSelectLayerCraft11 = "OnSelectLayerCraft11";
        /// <summary>
        /// 工艺12
        /// </summary>
        public readonly static string OnSelectLayerCraft12 = "OnSelectLayerCraft12";
        /// <summary>
        /// 工艺13
        /// </summary>
        public readonly static string OnSelectLayerCraft13 = "OnSelectLayerCraft13";
        /// <summary>
        /// 工艺14
        /// </summary>
        public readonly static string OnSelectLayerCraft14 = "OnSelectLayerCraft14";
        /// <summary>
        /// 工艺15
        /// </summary>
        public readonly static string OnSelectLayerCraft15 = "OnSelectLayerCraft15";
        /// <summary>
        /// 工艺16
        /// </summary>
        public readonly static string OnSelectLayerCraft16 = "OnSelectLayerCraft16";
        /// <summary>
        /// 返回文件底图
        /// </summary>
        public readonly static string OnBackFileBottomFigure = "OnBackFileBottomFigure";
        /// <summary>
        /// 公制（Metric Unit）
        /// </summary>
        public readonly static string OnMetricUnit = "OnMetricUnit";
        /// <summary>
        /// 英制（Imperail Unit）
        /// </summary>
        public readonly static string OnImperailUnit = "OnImperailUnit";
        /// <summary>
        /// English
        /// </summary>
        public readonly static string OnEnglish = "OnEnglish";
        /// <summary>
        /// 简体中文
        /// </summary>
        public readonly static string OnSimplifiedChinese = "OnSimplifiedChinese";
        /// <summary>
        /// 显示背景
        /// </summary>
        public readonly static string OnShowBackgroundCarft = "OnShowBackgroundCarft";
        /// <summary>
        /// 显示工艺1
        /// </summary>
        public readonly static string OnShowCarft1 = "OnShowCarft1";
        /// <summary>
        /// 显示工艺2
        /// </summary>
        public readonly static string OnShowCarft2 = "OnShowCarft2";
        /// <summary>
        /// 显示工艺3
        /// </summary>
        public readonly static string OnShowCarft3 = "OnShowCarft3";
        /// <summary>
        /// 显示工艺4
        /// </summary>
        public readonly static string OnShowCarft4 = "OnShowCarft4";
        /// <summary>
        /// 显示工艺5
        /// </summary>
        public readonly static string OnShowCarft5 = "OnShowCarft5";
        /// <summary>
        /// 显示工艺6
        /// </summary>
        public readonly static string OnShowCarft6 = "OnShowCarft6";
        /// <summary>
        /// 显示工艺7
        /// </summary>
        public readonly static string OnShowCarft7 = "OnShowCarft7";
        /// <summary>
        /// 显示工艺8
        /// </summary>
        public readonly static string OnShowCarft8 = "OnShowCarft8";
        /// <summary>
        /// 显示工艺9
        /// </summary>
        public readonly static string OnShowCarft9 = "OnShowCarft9";
        /// <summary>
        /// 显示工艺10
        /// </summary>
        public readonly static string OnShowCarft10 = "OnShowCarft10";
        /// <summary>
        /// 显示工艺11
        /// </summary>
        public readonly static string OnShowCarft11 = "OnShowCarft11";
        /// <summary>
        /// 显示工艺12
        /// </summary>
        public readonly static string OnShowCarft12 = "OnShowCarft12";
        /// <summary>
        /// 显示工艺13
        /// </summary>
        public readonly static string OnShowCarft13 = "OnShowCarft13";
        /// <summary>
        /// 显示工艺14
        /// </summary>
        public readonly static string OnShowCarft14 = "OnShowCarft14";
        /// <summary>
        /// 显示工艺15
        /// </summary>
        public readonly static string OnShowCarft15 = "OnShowCarft15";
        /// <summary>
        /// 显示工艺16
        /// </summary>
        public readonly static string OnShowCarft16 = "OnShowCarft16";
        /// <summary>
        /// 显示全部
        /// </summary>
        public readonly static string OnShowAllCarft = "OnShowAllCarft";
        /// <summary>
        /// 只显示工艺1
        /// </summary>
        public readonly static string OnOnlyShowCarft1 = "OnOnlyShowCarft1";
        /// <summary>
        /// 只显示工艺2
        /// </summary>
        public readonly static string OnOnlyShowCarft2 = "OnOnlyShowCarft2";
        /// <summary>
        /// 只显示工艺3
        /// </summary>
        public readonly static string OnOnlyShowCarft3 = "OnOnlyShowCarft3";
        /// <summary>
        /// 只显示工艺4
        /// </summary>
        public readonly static string OnOnlyShowCarft4 = "OnOnlyShowCarft4";
        /// <summary>
        /// 只显示工艺5
        /// </summary>
        public readonly static string OnOnlyShowCarft5 = "OnOnlyShowCarft5";
        /// <summary>
        /// 只显示工艺6
        /// </summary>
        public readonly static string OnOnlyShowCarft6 = "OnOnlyShowCarft6";
        /// <summary>
        /// 只显示工艺7
        /// </summary>
        public readonly static string OnOnlyShowCarft7 = "OnOnlyShowCarft7";
        /// <summary>
        /// 只显示工艺8
        /// </summary>
        public readonly static string OnOnlyShowCarft8 = "OnOnlyShowCarft8";
        /// <summary>
        /// 只显示工艺9
        /// </summary>
        public readonly static string OnOnlyShowCarft9 = "OnOnlyShowCarft9";
        /// <summary>
        /// 只显示工艺10
        /// </summary>
        public readonly static string OnOnlyShowCarft10 = "OnOnlyShowCarft10";
        /// <summary>
        /// 只显示工艺11
        /// </summary>
        public readonly static string OnOnlyShowCarft11 = "OnOnlyShowCarft11";
        /// <summary>
        /// 只显示工艺12
        /// </summary>
        public readonly static string OnOnlyShowCarft12 = "OnOnlyShowCarft12";
        /// <summary>
        /// 只显示工艺13
        /// </summary>
        public readonly static string OnOnlyShowCarft13 = "OnOnlyShowCarft13";
        /// <summary>
        /// 只显示工艺14
        /// </summary>
        public readonly static string OnOnlyShowCarft14 = "OnOnlyShowCarft14";
        /// <summary>
        /// 只显示工艺15
        /// </summary>
        public readonly static string OnOnlyShowCarft15 = "OnOnlyShowCarft15";
        /// <summary>
        /// 只显示工艺16
        /// </summary>
        public readonly static string OnOnlyShowCarft16 = "OnOnlyShowCarft16";
        /// <summary>
        /// 锁定背景
        /// </summary>
        public readonly static string OnLockCarftBackground = "OnLockCarftBackground";
        /// <summary>
        /// 锁定工艺1
        /// </summary>
        public readonly static string OnLockCarft1 = "OnLockCarft1";
        /// <summary>
        /// 锁定工艺2
        /// </summary>
        public readonly static string OnLockCarft2 = "OnLockCarft2";
        /// <summary>
        /// 锁定工艺3
        /// </summary>
        public readonly static string OnLockCarft3 = "OnLockCarft3";
        /// <summary>
        /// 锁定工艺4
        /// </summary>
        public readonly static string OnLockCarft4 = "OnLockCarft4";
        /// <summary>
        /// 锁定工艺5
        /// </summary>
        public readonly static string OnLockCarft5 = "OnLockCarft5";
        /// <summary>
        /// 锁定工艺6
        /// </summary>
        public readonly static string OnLockCarft6 = "OnLockCarft6";
        /// <summary>
        /// 锁定工艺7
        /// </summary>
        public readonly static string OnLockCarft7 = "OnLockCarft7";
        /// <summary>
        /// 锁定工艺8
        /// </summary>
        public readonly static string OnLockCarft8 = "OnLockCarft8";
        /// <summary>
        /// 锁定工艺9
        /// </summary>
        public readonly static string OnLockCarft9 = "OnLockCarft9";
        /// <summary>
        /// 锁定工艺10
        /// </summary>
        public readonly static string OnLockCarft10 = "OnLockCarft10";
        /// <summary>
        /// 锁定工艺11
        /// </summary>
        public readonly static string OnLockCarft11 = "OnLockCarft11";
        /// <summary>
        /// 锁定工艺12
        /// </summary>
        public readonly static string OnLockCarft12 = "OnLockCarft12";
        /// <summary>
        /// 锁定工艺13
        /// </summary>
        public readonly static string OnLockCarft13 = "OnLockCarft13";
        /// <summary>
        /// 锁定工艺14
        /// </summary>
        public readonly static string OnLockCarft14 = "OnLockCarft14";
        /// <summary>
        /// 锁定工艺15
        /// </summary>
        public readonly static string OnLockCarft15 = "OnLockCarft15";
        /// <summary>
        /// 锁定工艺16
        /// </summary>
        public readonly static string OnLockCarft16 = "OnLockCarft16";
        /// <summary>
        /// 繁体中文
        /// </summary>
        public readonly static string OnTraditionalChinese = "OnTraditionalChinese";
        /// <summary>
        /// 删除所有排样零件
        /// </summary>
        public readonly static string OnDeleteAllLayoutPart = "OnDeleteAllLayoutPart";
        /// <summary>
        /// 将选中图形设为板材
        /// </summary>
        public readonly static string OnSetSheetMaterial = "OnSetSheetMaterial";
        /// <summary>
        /// 删除所有排样板材
        /// </summary>
        public readonly static string OnDeleteAllSheetMaterial = "OnDeleteAllSheetMaterial";
        /// <summary>
        /// 新建
        /// </summary>
        public readonly static string OnNewFile = "OnNewFile";
        /// <summary>
        /// 打开
        /// </summary>
        public readonly static string OnOpenFile = "OnOpenFile";
        /// <summary>
        /// 保存
        /// </summary>
        public readonly static string OnSaveFile = "OnSaveFile";
        /// <summary>
        /// WXD格式（默认）
        /// </summary>
        public readonly static string OnSaveFileWXF = "OnSaveFileWXF";
        /// <summary>
        /// 参数备份
        /// </summary>
        public readonly static string OnBackUpParam = "OnBackUpParam";
        /// <summary>
        /// AutoCAD DXF格式
        /// </summary>
        public readonly static string OnSaveFileAutoCADDXF = "OnSaveFileAutoCADDXF";
        /// <summary>
        /// 导入零件
        /// </summary>
        public readonly static string OnImportPart = "OnImportPart";
        /// <summary>
        /// 导入标准零件
        /// </summary>
        public readonly static string OnImportStandardPart = "OnImportStandardPart";
        /// <summary>
        /// 导入LXD文件
        /// </summary>
        public readonly static string OnImportFileLXD = "OnImportFileLXD";
        /// <summary>
        /// 导入DXF文件
        /// </summary>
        public readonly static string OnImportFileDXF = "OnImportFileDXF";
        /// <summary>
        /// 导入PLT格式文件
        /// </summary>
        public readonly static string OnImportFilePLT = "OnImportFilePLT";
        /// <summary>
        /// 导入AI路劲文件
        /// </summary>
        public readonly static string OnImportFileAI = "OnImportFileAI";
        /// <summary>
        /// 导入GerBer文件
        /// </summary>
        public readonly static string OnImportFileGerBer = "OnImportFileGerBer";
        /// <summary>
        /// NC加工代码文件
        /// </summary>
        public readonly static string OnImportFileNC = "OnImportFileNC";
        /// <summary>
        /// 加工报告单
        /// </summary>
        public readonly static string OnProcessingReport = "OnProcessingReport";
        /// <summary>
        /// 排样报告
        /// </summary>
        public readonly static string OnLayoutReport = "OnLayoutReport";
        /// <summary>
        /// 使用报告
        /// </summary>
        public readonly static string OnUseReport = "OnUseReport";
        /// <summary>
        /// 用户参数
        /// </summary>
        public readonly static string OnSetUserParam = "OnSetUserParam";
        /// <summary>
        /// 控制卡监控
        /// </summary>
        public readonly static string OnControlCardMonitor = "OnControlCardMonitor";
        /// <summary>
        /// 扩展IO板监控
        /// </summary>
        public readonly static string OnExtendIOMonitor = "OnExtendIOMonitor";
        /// <summary>
        /// 气体DA校正
        /// </summary>
        public readonly static string OnGasDACorrection = "OnGasDACorrection";
        /// <summary>
        /// NT监控
        /// </summary>
        public readonly static string OnBCS100Monitor = "OnBCS100Monitor";
        /// <summary>
        /// 拷机测试
        /// </summary>
        public readonly static string OnBurnTest = "OnBurnTest";
        /// <summary>
        /// 引线
        /// </summary>
        public readonly static string OnLeadInOrOutWireItem = "OnLeadInOrOutWireItem";
        /// <summary>
        /// 清除引入引出线
        /// </summary>
        public readonly static string OnClearLeadInOrOutWire = "OnClearLeadInOrOutWire";
        /// <summary>
        /// 清除微连
        /// </summary>
        public readonly static string OnClearMicroConnect = "OnClearMicroConnect";
        /// <summary>
        /// 取消补偿
        /// </summary>
        public readonly static string OnCancelGapCompensation = "OnCancelGapCompensation";
        /// <summary>
        /// 标记5
        /// </summary>
        public readonly static string barButtonItem65 = "barButtonItem65";
        /// <summary>
        /// 标记6
        /// </summary>
        public readonly static string barButtonItem66 = "barButtonItem66";
        /// <summary>
        /// 尺寸
        /// </summary>
        public readonly static string OnFigureSizeItem = "OnFigureSizeItem";
        /// <summary>
        /// 显示微连标记
        /// </summary>
        public readonly static string OnDisplayMicroHyphen = "OnDisplayMicroHyphen";
        /// <summary>
        /// 平移
        /// </summary>
        public readonly static string OnTranslation = "OnTranslation";
        /// <summary>
        /// 交互式缩放
        /// </summary>
        public readonly static string OnInteractiveZoom = "OnInteractiveZoom";
        /// <summary>
        /// 左对齐
        /// </summary>
        public readonly static string OnAlignLeft = "OnAlignLeft";
        /// <summary>
        /// 右对齐
        /// </summary>
        public readonly static string OnAlignRight = "OnAlignRight";
        /// <summary>
        /// 水平居中
        /// </summary>
        public readonly static string OnAlignHorizontalCenter = "OnAlignHorizontalCenter";
        /// <summary>
        /// 底部对齐
        /// </summary>
        public readonly static string OnAlignBottom = "OnAlignBottom";
        /// <summary>
        /// 垂直居中
        /// </summary>
        public readonly static string OnAlignVerticalCenter = "OnAlignVerticalCenter";
        /// <summary>
        /// 居中对齐
        /// </summary>
        public readonly static string OnAlignCenter = "OnAlignCenter";
        /// <summary>
        /// 水平镜像
        /// </summary>
        public readonly static string OnMirrorHorizontal = "OnMirrorHorizontal";
        /// <summary>
        /// 垂直镜像
        /// </summary>
        public readonly static string OnMirrorVertical = "OnMirrorVertical";
        /// <summary>
        /// 任意角度镜像
        /// </summary>
        public readonly static string OnMirrorAnyAngle = "OnMirrorAnyAngle";
        /// <summary>
        /// 逆时针旋转90°
        /// </summary>
        public readonly static string OnAnticlockwise90 = "OnAnticlockwise90";
        /// <summary>
        /// 顺时针旋转90°
        /// </summary>
        public readonly static string OnClockwise90 = "OnClockwise90";
        /// <summary>
        /// 旋转180°
        /// </summary>
        public readonly static string OnRotate180 = "OnRotate180";
        /// <summary>
        /// 任意角度旋转
        /// </summary>
        public readonly static string OnRotateAnyAngle = "OnRotateAnyAngle";
        /// <summary>
        /// 释放角
        /// </summary>
        public readonly static string OnReleaseAngle = "OnReleaseAngle";
        /// <summary>
        /// 倒圆角
        /// </summary>
        public readonly static string OnReverseCircularBead = "OnReverSecircularBead";
        /// <summary>
        /// 冷却点
        /// </summary>
        public readonly static string OnCoolingDotItem = "OnCoolingDotItem";
        /// <summary>
        /// 最前
        /// </summary>
        public readonly static string OnFrontMost = "OnFrontMost";
        /// <summary>
        /// 最后
        /// </summary>
        public readonly static string OnBackMost = "OnBackMost";
        /// <summary>
        /// 向前
        /// </summary>
        public readonly static string OnForward = "OnForward";
        /// <summary>
        /// 向后
        /// </summary>
        public readonly static string OnBackward = "OnBackward";
        /// <summary>
        /// 上一个
        /// </summary>
        public readonly static string OnPreviousTarget = "OnPreviousTarget";
        /// <summary>
        /// 下一个
        /// </summary>
        public readonly static string OnNextTarget = "OnNextTarget";
        /// <summary>
        /// 排样
        /// </summary>
        public readonly static string OnStockLayoutItem = "OnStockLayoutItem";
        /// <summary>
        /// 曲线平滑
        /// </summary>
        public readonly static string OnCurveSmoothing = "OnCurveSmoothing";
        /// <summary>
        /// 曲线分割
        /// </summary>
        public readonly static string OnCurveSegment = "OnCurveSegment";
        /// <summary>
        /// 去除重复线
        /// </summary>
        public readonly static string OnDeleteRepeatLine = "OnDeleteRepeatLine";
        /// <summary>
        /// 去除小图形
        /// </summary>
        public readonly static string OnDeleteSmallFigure = "OnDeleteSmallFigure";
        /// <summary>
        /// 合并相连线
        /// </summary>
        public readonly static string OnMergeConnectLine = "OnMergeConnectLine";
        /// <summary>
        /// 切碎
        /// </summary>
        public readonly static string OnCutUp = "OnCutUp";
        /// <summary>
        /// 切刀
        /// </summary>
        public readonly static string OnConnectKnife = "OnConnectKnife";
        /// <summary>
        /// DXF图形映射
        /// </summary>
        public readonly static string OnDXFFigureMap = "OnDXFFigureMap";
        /// <summary>
        /// 矩形
        /// </summary>
        public readonly static string OnRectangle = "OnRectangle";
        /// <summary>
        /// 圆角矩形
        /// </summary>
        public readonly static string OnFilletRectangle = "OnFilletRectangle";
        /// <summary>
        /// 跑道形
        /// </summary>
        public readonly static string OnTrackRectangle = "OnTrackRectangle";
        /// <summary>
        /// 整圆
        /// </summary>
        public readonly static string OnCircle = "OnCircle";
        /// <summary>
        /// 三点圆弧
        /// </summary>
        public readonly static string OnThreePointArc = "OnThreePointArc";
        /// <summary>
        /// 扫描式圆弧
        /// </summary>
        public readonly static string OnScanArc = "OnScanArc";
        /// <summary>
        /// 新椭圆
        /// </summary>
        public readonly static string OnNewEllipse = "OnNewEllipse";
        /// <summary>
        /// 替换圆形定位孔为孤立点
        /// </summary>
        public readonly static string OnCircleReplaceToAcnode = "OnCircleReplaceToAcnode";
        /// <summary>
        /// 替换为圆
        /// </summary>
        public readonly static string OnReplaceToCircle = "OnReplaceToCircle";
        /// <summary>
        /// 多线段
        /// </summary>
        public readonly static string OnMultiLine = "OnMultiLine";
        /// <summary>
        /// 多边形
        /// </summary>
        public readonly static string OnPolygon = "OnPolygon";
        /// <summary>
        /// 星形
        /// </summary>
        public readonly static string OnStellate = "OnStellate";
        /// <summary>
        /// 选择所有群组
        /// </summary>
        public readonly static string OnGroupSelectAll = "OnGroupSelectAll";
        /// <summary>
        /// 打散选中群组
        /// </summary>
        public readonly static string OnGroupScatterSelected = "OnGroupScatterSelected";
        /// <summary>
        /// 打散全部群组
        /// </summary>
        public readonly static string OnGroupScatterAll = "OnGroupScatterAll";
        /// <summary>
        /// 炸开图形
        /// </summary>
        public readonly static string OnGroupBlowOpen = "OnGroupBlowOpen";
        /// <summary>
        /// 多轮廓共切群组
        /// </summary>
        public readonly static string OnMultiContourCotangent = "OnMultiContourCotangent";
        /// <summary>
        /// 炸开多轮廓共切群组
        /// </summary>
        public readonly static string OnBlowOpenMultiContourCotangent = "OnBlowOpenMultiContourCotangent";
        /// <summary>
        /// 自动微连
        /// </summary>
        public readonly static string OnMicroConnectionAuto = "OnMicroConnectionAuto";
        /// <summary>
        /// 炸开微连
        /// </summary>
        public readonly static string OnMicroConnectBlowOpen = "OnMicroConnectBlowOpen";
        /// <summary>
        /// 反向
        /// </summary>
        public readonly static string OnReverse = "OnReverse";
        /// <summary>
        /// 顺时针
        /// </summary>
        public readonly static string OnClockwise = "OnClockwise";
        /// <summary>
        /// 逆时针
        /// </summary>
        public readonly static string OnAnticlockwise = "OnAnticlockwise";
        /// <summary>
        /// 自动冷却点
        /// </summary>
        public readonly static string OnCoolingDotAuto = "OnCoolingDotAuto";
        /// <summary>
        /// 清除冷却点
        /// </summary>
        public readonly static string OnCoolingDotClear = "OnCoolingDotClear";
        /// <summary>
        /// 网格排序
        /// </summary>
        public readonly static string OnSortGrid = "OnSortGrid";
        /// <summary>
        /// 局部最短空移
        /// </summary>
        public readonly static string OnSortShortestMove = "OnSortShortestMove";
        /// <summary>
        /// 刀摸排序
        /// </summary>
        public readonly static string OnSortKnife = "OnSortKnife";
        /// <summary>
        /// 小图优先
        /// </summary>
        public readonly static string OnSortSmallFigurePriority = "OnSortSmallFigurePriority";
        /// <summary>
        /// 由内到外
        /// </summary>
        public readonly static string OnSortInsideToOut = "OnSortInsideToOut";
        /// <summary>
        /// 从左到右
        /// </summary>
        public readonly static string OnSortLeftToRight = "OnSortLeftToRight";
        /// <summary>
        /// 从右到左
        /// </summary>
        public readonly static string OnSortRightToLeft = "OnSortRightToLeft";
        /// <summary>
        /// 从上到下
        /// </summary>
        public readonly static string OnSortTopToBottom = "OnSortTopToBottom";
        /// <summary>
        /// 从下到上
        /// </summary>
        public readonly static string OnSortBottomToTop = "OnSortBottomToTop";
        /// <summary>
        /// 顺时针
        /// </summary>
        public readonly static string OnSortClockwise = "OnSortClockwise";
        /// <summary>
        /// 逆时针
        /// </summary>
        public readonly static string OnSortAnticlockwise = "OnSortAnticlockwise";
        /// <summary>
        /// 禁止排序改变方向
        /// </summary>
        public readonly static string OnSortProhibitChangDirection = "OnSortProhibitChangDirection";
        /// <summary>
        /// 排序时区分内外摸
        /// </summary>
        public readonly static string OnSortDistinguishInsideOutside = "OnSortDistinguishInsideOutside";
        /// <summary>
        /// 最外层为阴切
        /// </summary>
        public readonly static string OnSortShadeCutOutermost = "OnSortShadeCutOutermost";
        /// <summary>
        /// 矩形阵列
        /// </summary>
        public readonly static string OnArrayLayoutRectangle = "OnArrayLayoutRectangle";
        /// <summary>
        /// 交互式阵列
        /// </summary>
        public readonly static string OnArrayLayoutInteractive = "OnArrayLayoutInteractive";
        /// <summary>
        /// 环形阵列
        /// </summary>
        public readonly static string OnArrayLayoutAnnular = "OnArrayLayoutAnnular";
        /// <summary>
        /// 布满
        /// </summary>
        public readonly static string OnArrayLayoutFull = "OnArrayLayoutFull";
        /// <summary>
        /// 直线飞行切割
        /// </summary>
        public readonly static string OnFlyingCutLine = "OnFlyingCutLine";
        /// <summary>
        /// 圆弧飞行切割
        /// </summary>
        public readonly static string OnFlyingCutArc = "OnFlyingCutArc";
        /// <summary>
        /// C字形共边
        /// </summary>
        public readonly static string OnCommonSideTypeC = "OnCommonSideTypeC";
        /// <summary>
        /// 0.5倍
        /// </summary>
        public readonly static string OnFigureSize0_5 = "OnFigureSize0_5";
        /// <summary>
        /// 2倍
        /// </summary>
        public readonly static string OnFigureSize2 = "OnFigureSize2";
        /// <summary>
        /// 4倍
        /// </summary>
        public readonly static string OnFigureSize4 = "OnFigureSize4";
        /// <summary>
        /// 8倍
        /// </summary>
        public readonly static string OnFigureSize8 = "OnFigureSize8";
        /// <summary>
        /// 10倍
        /// </summary>
        public readonly static string OnFigureSize10 = "OnFigureSize10";
        /// <summary>
        /// 检查引入引出
        /// </summary>
        public readonly static string OnCheckLeadInOrOutWire = "OnCheckLeadInOrOutWire";
        /// <summary>
        /// 区分内外模式
        /// </summary>
        public readonly static string OnLeadInOrOutWireDifferentiateMode = "OnLeadInOrOutWireDifferentiateMode";
        /// <summary>
        /// 清除排样结果
        /// </summary>
        public readonly static string OnStockLayoutClear = "OnStockLayoutClear";
        /// <summary>
        /// 顶部对齐
        /// </summary>
        public readonly static string OnAlignTop = "OnAlignTop";
        /// <summary>
        /// 电容寻边
        /// </summary>
        public readonly static string OnCapacitanceEdge = "OnCapacitanceEdge";
        /// <summary>
        /// 光电寻边
        /// </summary>
        public readonly static string OnDotEdge = "OnDotEdge";
        /// <summary>
        /// 手动寻边
        /// </summary>
        public readonly static string OnManualEdge = "OnManualEdge";
        /// <summary>
        /// 加工完自动清除寻边角度
        /// </summary>
        public readonly static string OnAutoClearEdgeProcessed = "OnAutoClearEdgeProcessed";
        /// <summary>
        /// 清除寻边角度
        /// </summary>
        public readonly static string OnClearEdgeAngle = "OnClearEdgeAngle";
        /// <summary>
        /// 编辑PLC过程
        /// </summary>
        public readonly static string OnEditPLCProcess = "OnEditPLCProcess";
        /// <summary>
        /// 切换到工作台A
        /// </summary>
        public readonly static string OnChangeToWorkbenchA = "OnChangeToWorkbenchA";
        /// <summary>
        /// 切换到工作台B
        /// </summary>
        public readonly static string OnChangeToWorkbenchB = "OnChangeToWorkbenchB";
        /// <summary>
        /// 自定义过程1
        /// </summary>
        public readonly static string OnCustomProcess1 = "OnCustomProcess1";
        /// <summary>
        /// 自定义过程2
        /// </summary>
        public readonly static string OnCustomProcess2 = "OnCustomProcess2";
        /// <summary>
        /// 自定义过程3
        /// </summary>
        public readonly static string OnCustomProcess3 = "OnCustomProcess3";
        /// <summary>
        /// 自定义过程4
        /// </summary>
        public readonly static string OnCustomProcess4 = "OnCustomProcess4";
        /// <summary>
        /// 自定义过程5
        /// </summary>
        public readonly static string OnCustomProcess5 = "OnCustomProcess5";
        /// <summary>
        /// 自定义过程6
        /// </summary>
        public readonly static string OnCustomProcess6 = "OnCustomProcess6";
        /// <summary>
        /// 自定义过程7
        /// </summary>
        public readonly static string OnCustomProcess7 = "OnCustomProcess7";
        /// <summary>
        /// 自定义过程8
        /// </summary>
        public readonly static string OnCustomProcess8 = "OnCustomProcess8";
        /// <summary>
        /// 自定义过程9
        /// </summary>
        public readonly static string OnCustomProcess9 = "OnCustomProcess9";
        /// <summary>
        /// 自定义过程10
        /// </summary>
        public readonly static string OnCustomProcess10 = "OnCustomProcess10";
        /// <summary>
        /// 自定义过程11
        /// </summary>
        public readonly static string OnCustomProcess11 = "OnCustomProcess11";
        /// <summary>
        /// 自定义过程12
        /// </summary>
        public readonly static string OnCustomProcess12 = "OnCustomProcess12";
        /// <summary>
        /// 自定义过程13
        /// </summary>
        public readonly static string OnCustomProcess13 = "OnCustomProcess13";
        /// <summary>
        /// 自定义过程14
        /// </summary>
        public readonly static string OnCustomProcess14 = "OnCustomProcess14";
        /// <summary>
        /// 自定义过程15
        /// </summary>
        public readonly static string OnCustomProcess15 = "OnCustomProcess15";
        /// <summary>
        /// 自定义过程16
        /// </summary>
        public readonly static string OnCustomProcess16 = "OnCustomProcess16";
        /// <summary>
        /// 自定义过程17
        /// </summary>
        public readonly static string OnCustomProcess17 = "OnCustomProcess17";
        /// <summary>
        /// 自定义过程18
        /// </summary>
        public readonly static string OnCustomProcess18 = "OnCustomProcess18";
        /// <summary>
        /// 自定义过程19
        /// </summary>
        public readonly static string OnCustomProcess19 = "OnCustomProcess19";
        /// <summary>
        /// 自定义过程20
        /// </summary>
        public readonly static string OnCustomProcess20 = "OnCustomProcess20";
        /// <summary>
        /// 保存加工任务
        /// </summary>
        public readonly static string OnSaveProcessTask = "OnSaveProcessTask";
        /// <summary>
        /// 载入加工任务
        /// </summary>
        public readonly static string OnLoadProcessTask = "OnLoadProcessTask";
        /// <summary>
        /// 全部回原点
        /// </summary>
        public readonly static string OnGoOriginAll = "OnGoOriginAll";
        /// <summary>
        /// X轴回原点
        /// </summary>
        public readonly static string OnGoOriginX = "OnGoOriginX";
        /// <summary>
        /// Y轴回原点
        /// </summary>
        public readonly static string OnGoOriginY = "OnGoOriginY";
        /// <summary>
        /// 龙门初始化
        /// </summary>
        public readonly static string OnGantryInit = "OnGantryInit";
        /// <summary>
        /// 回原点时执行龙门同步
        /// </summary>
        public readonly static string OnExecuteGantrySyncGoOrigin = "OnExecuteGantrySyncGoOrigin";
        /// <summary>
        /// 调高器先回原点
        /// </summary>
        public readonly static string OnAdjustHeightenGoOrigin = "OnAdjustHeightenGoOrigin";
        /// <summary>
        /// 电动调焦也回原点
        /// </summary>
        public readonly static string OnElectricFocusgoOrigin = "OnElectricFocusgoOrigin";
        /// <summary>
        /// 光路调试
        /// </summary>
        public readonly static string OnLightPathDebug = "OnLightPathDebug";
        /// <summary>
        /// 停止（Hold）
        /// </summary>
        public readonly static string OnBCSStop = "OnBCSStop";
        /// <summary>
        /// 回停靠
        /// </summary>
        public readonly static string OnBCSGoBerth = "OnBCSGoBerth";
        /// <summary>
        /// 回原点
        /// </summary>
        public readonly static string OnBCSGoOrigin = "OnBCSGoOrigin";
        /// <summary>
        /// 1mm
        /// </summary>
        public readonly static string OnBCSFollow1mm = "OnBCSFollow1mm";
        /// <summary>
        /// 2mm
        /// </summary>
        public readonly static string OnBCSFollow2mm = "OnBCSFollow2mm";
        /// <summary>
        /// 3mm
        /// </summary>
        public readonly static string OnBCSFollow3mm = "OnBCSFollow3mm";
        /// <summary>
        /// 4mm
        /// </summary>
        public readonly static string OnBCSFollow4mm = "OnBCSFollow4mm";
        /// <summary>
        /// 5mm
        /// </summary>
        public readonly static string OnBCSFollow5mm = "OnBCSFollow5mm";
        /// <summary>
        /// 6mm
        /// </summary>
        public readonly static string OnBCSFollow6mm = "OnBCSFollow6mm";
        /// <summary>
        /// 7mm
        /// </summary>
        public readonly static string OnBCSFollow7mm = "OnBCSFollow7mm";
        /// <summary>
        /// 8mm
        /// </summary>
        public readonly static string OnBCSFollow8mm = "OnBCSFollow8mm";
        /// <summary>
        /// 9mm
        /// </summary>
        public readonly static string OnBCSFollow9mm = "OnBCSFollow9mm";
        /// <summary>
        /// 10mm
        /// </summary>
        public readonly static string OnBCSFollow10mm = "OnBCSFollow10mm";
        /// <summary>
        /// 跟随然后停止
        /// </summary>
        public readonly static string barButtonItem266 = "barButtonItem266";
        /// <summary>
        /// 一键标定
        /// </summary>
        public readonly static string OnBCSKeyCalibration = "OnBCSKeyCalibration";
        /// <summary>
        /// 注册加密
        /// </summary>
        public readonly static string OnBCSRegisteredEncrypt = "OnBCSRegisteredEncrypt";
        /// <summary>
        /// 升级
        /// </summary>
        public readonly static string OnBCSUpdate = "OnBCSUpdate";
        /// <summary>
        /// 保存板外跟随参照高度
        /// </summary>
        public readonly static string OnBCSSaveParams = "OnBCSSaveParams";
        /// <summary>
        /// 记录板面Z坐标
        /// </summary>
        public readonly static string OnBCSRecordZCoordinate = "OnBCSRecordZCoordinate";
        /// <summary>
        /// 1mm
        /// </summary>
        public readonly static string OnBCSFollowStop1mm = "OnBCSFollowStop1mm";
        /// <summary>
        /// 2mm
        /// </summary>
        public readonly static string OnBCSFollowStop2mm = "OnBCSFollowStop2mm";
        /// <summary>
        /// 3mm.
        /// </summary>
        public readonly static string barButtonItem275 = "barButtonItem275";
        /// <summary>
        /// 3mm
        /// </summary>
        public readonly static string OnBCSFollowStop3mm = "OnBCSFollowStop3mm";
        /// <summary>
        /// 4mm
        /// </summary>
        public readonly static string OnBCSFollowStop4mm = "OnBCSFollowStop4mm";
        /// <summary>
        /// 5mm
        /// </summary>
        public readonly static string OnBCSFollowStop5mm = "OnBCSFollowStop5mm";
        /// <summary>
        /// 6
        /// </summary>
        public readonly static string barButtonItem279 = "barButtonItem279";
        /// <summary>
        /// 6mm
        /// </summary>
        public readonly static string OnBCSFollowStop6mm = "OnBCSFollowStop6mm";
        /// <summary>
        /// 7mm
        /// </summary>
        public readonly static string OnBCSFollowStop7mm = "OnBCSFollowStop7mm";
        /// <summary>
        /// 8mm
        /// </summary>
        public readonly static string OnBCSFollowStop8mm = "OnBCSFollowStop8mm";
        /// <summary>
        /// 9mm
        /// </summary>
        public readonly static string OnBCSFollowStop9mm = "OnBCSFollowStop9mm";
        /// <summary>
        /// 10mm
        /// </summary>
        public readonly static string OnBCSFollowStop10mm = "OnBCSFollowStop10mm";
        /// <summary>
        /// 15mm
        /// </summary>
        public readonly static string OnBCSFollowStop15mm = "OnBCSFollowStop15mm";
        /// <summary>
        /// 20mm
        /// </summary>
        public readonly static string OnBCSFollowStop20mm = "OnBCSFollowStop20mm";
        /// <summary>
        /// 25mm
        /// </summary>
        public readonly static string OnBCSFollowStop25mm = "OnBCSFollowStop25mm";
        /// <summary>
        /// 30mm
        /// </summary>
        public readonly static string OnBCSFollowStop30mm = "OnBCSFollowStop30mm";
        /// <summary>
        /// 1mm↓
        /// </summary>
        public readonly static string OnEmptyMoveRelative1mm = "OnEmptyMoveRelative1mm";
        /// <summary>
        /// 2mm↓
        /// </summary>
        public readonly static string OnEmptyMoveRelative2mm = "OnEmptyMoveRelative2mm";
        /// <summary>
        /// 3mm↓
        /// </summary>
        public readonly static string OnEmptyMoveRelative3mm = "OnEmptyMoveRelative3mm";
        /// <summary>
        /// 4mm↓
        /// </summary>
        public readonly static string OnEmptyMoveRelative4mm = "OnEmptyMoveRelative4mm";
        /// <summary>
        /// 5mm↓
        /// </summary>
        public readonly static string OnEmptyMoveRelative5mm = "OnEmptyMoveRelative5mm";
        /// <summary>
        /// 6mm↓
        /// </summary>
        public readonly static string OnEmptyMoveRelative6mm = "OnEmptyMoveRelative6mm";
        /// <summary>
        /// 7mm↓
        /// </summary>
        public readonly static string OnEmptyMoveRelative7mm = "OnEmptyMoveRelative7mm";
        /// <summary>
        /// 8mm↓
        /// </summary>
        public readonly static string OnEmptyMoveRelative8mm = "OnEmptyMoveRelative8mm";
        /// <summary>
        /// 9mm↓
        /// </summary>
        public readonly static string OnEmptyMoveRelative9mm = "OnEmptyMoveRelative9mm";
        /// <summary>
        /// 10mm↓
        /// </summary>
        public readonly static string OnEmptyMoveRelative10mm = "OnEmptyMoveRelative10mm";
        /// <summary>
        /// -1mm↑
        /// </summary>
        public readonly static string OnEmptyMoveRelativeMinus1mm = "OnEmptyMoveRelativeMinus1mm";
        /// <summary>
        /// -2mm↑
        /// </summary>
        public readonly static string OnEmptyMoveRelativeMinus2mm = "OnEmptyMoveRelativeMinus2mm";
        /// <summary>
        /// -3mm↑
        /// </summary>
        public readonly static string OnEmptyMoveRelativeMinus3mm = "OnEmptyMoveRelativeMinus3mm";
        /// <summary>
        /// -4mm↑
        /// </summary>
        public readonly static string OnEmptyMoveRelativeMinus4mm = "OnEmptyMoveRelativeMinus4mm";
        /// <summary>
        /// -5mm↑
        /// </summary>
        public readonly static string OnEmptyMoveRelativeMinus5mm = "OnEmptyMoveRelativeMinus5mm";
        /// <summary>
        /// -6mm↑
        /// </summary>
        public readonly static string OnEmptyMoveRelativeMinus6mm = "OnEmptyMoveRelativeMinus6mm";
        /// <summary>
        /// -7mm↑
        /// </summary>
        public readonly static string OnEmptyMoveRelativeMinus7mm = "OnEmptyMoveRelativeMinus7mm";
        /// <summary>
        /// -8mm↑
        /// </summary>
        public readonly static string OnEmptyMoveRelativeMinus8mm = "OnEmptyMoveRelativeMinus8mm";
        /// <summary>
        /// -9mm↑
        /// </summary>
        public readonly static string OnEmptyMoveRelativeMinus9mm = "OnEmptyMoveRelativeMinus9mm";
        /// <summary>
        /// -10mm↑
        /// </summary>
        public readonly static string OnEmptyMoveRelativeMinus10mm = "OnEmptyMoveRelativeMinus10mm";
        /// <summary>
        /// Z=0
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ0 = "OnMoveAbsolutePositionZ0";
        /// <summary>
        /// Z=1
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ1 = "OnMoveAbsolutePositionZ1";
        /// <summary>
        /// Z=2
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ2 = "OnMoveAbsolutePositionZ2";
        /// <summary>
        /// Z=3
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ3 = "OnMoveAbsolutePositionZ3";
        /// <summary>
        /// Z=4
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ4 = "OnMoveAbsolutePositionZ4";
        /// <summary>
        /// Z=5
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ5 = "OnMoveAbsolutePositionZ5";
        /// <summary>
        /// Z=6
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ6 = "OnMoveAbsolutePositionZ6";
        /// <summary>
        /// Z=7
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ7 = "OnMoveAbsolutePositionZ7";
        /// <summary>
        /// Z=8
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ8 = "OnMoveAbsolutePositionZ8";
        /// <summary>
        /// Z=9
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ9 = "OnMoveAbsolutePositionZ9";
        /// <summary>
        /// Z=10
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ10 = "OnMoveAbsolutePositionZ10";
        /// <summary>
        /// Z=15
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ15 = "OnMoveAbsolutePositionZ15";
        /// <summary>
        /// Z=20
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ20 = "OnMoveAbsolutePositionZ20";
        /// <summary>
        /// Z=25
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ25 = "OnMoveAbsolutePositionZ25";
        /// <summary>
        /// Z=30
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ30 = "OnMoveAbsolutePositionZ30";
        /// <summary>
        /// Z=40
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ40 = "OnMoveAbsolutePositionZ40";
        /// <summary>
        /// Z=50
        /// </summary>
        public readonly static string OnMoveAbsolutePositionZ50 = "OnMoveAbsolutePositionZ50";
        /// <summary>
        /// QCW
        /// </summary>
        public readonly static string OnQCW = "OnQCW";
        /// <summary>
        /// 龙门误差监控
        /// </summary>
        public readonly static string OnGantryErrorMonitor = "OnGantryErrorMonitor";
        /// <summary>
        /// 反选
        /// </summary>
        public readonly static string OnInvertSelect = "OnInvertSelect";
        /// <summary>
        /// 降低速度
        /// </summary>
        public readonly static string OnSubSpeed = "OnSubSpeed";
        /// <summary>
        /// 增加速度
        /// </summary>
        public readonly static string OnAddSpeed = "OnAddSpeed";
        /// <summary>
        /// 添加画图日志
        /// </summary>
        public readonly static string OnAddLogDrawInfos = "OnAddLogDrawInfos";
        /// <summary>
        /// 添加系统日志
        /// </summary>
        public readonly static string OnAddLogSystemInfos = "OnAddLogSystemInfos";
        /// <summary>
        /// 添加警告日志
        /// </summary>
        public readonly static string OnAddLogAlarmInfos = "OnAddLogAlarmInfos";

        /// <summary>
        /// 绘图界面绘图命令
        /// </summary>
        public readonly static string OnInDataDrawCommand = "OnInDataDrawCommand";

    }
}
