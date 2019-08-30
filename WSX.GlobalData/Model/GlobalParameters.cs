using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;

namespace WSX.GlobalData.Model
{
    /// <summary>
    /// 系统所有配置参数
    /// </summary>
    public class GlobalParameters
    {
        /// <summary>
        /// 用户参数
        /// </summary>
        public UserConfigModel UserConfig { get; set; } = new UserConfigModel();
        /// <summary>
        /// 停靠参数
        /// </summary>
        public DockPositionModel DockPosition { get; set; } = new DockPositionModel();
        /// <summary>
        /// 尖角环切
        /// </summary>
        //public SurroundCutModel SurroundCut { get; set; } = new SurroundCutModel();
        /// <summary>
        /// 自动微连
        /// </summary>
        //public AutoMicroConnectModel AutoMicroConnect { get; set; } = new AutoMicroConnectModel();
        ///// <summary>
        ///// 释放角、倒圆角半径
        ///// </summary>
        //public ReleaseRoundOrCircularBeadModel ReleaseAngleOrCircularBead { get; set; } = new ReleaseRoundOrCircularBeadModel();
        /// <summary>
        /// 自动添加冷却点
        /// </summary>
        //public AutoAddCoolingDotModel AutoAddCoolingDot { get; set; } = new AutoAddCoolingDotModel();
        /// <summary>
        /// 直线飞行切割
        /// </summary>
        public LineFlyingCutModel LineFlyingCut { get; set; } = new LineFlyingCutModel();
        /// <summary>
        /// 圆形飞行切割
        /// </summary>
        public ArcFlyingCutModel ArcFlyingCut { get; set; } = new ArcFlyingCutModel();
        /// <summary>
        /// 自动排样
        /// </summary>
        public AutoLayoutModel AutoLayout { get; set; } = new AutoLayoutModel();
        /// <summary>
        /// 阵列参数
        /// </summary>
        public ArrayRectangleModel ArrayRectangle { get; set; } = new ArrayRectangleModel();
        /// <summary>
        /// 交互式阵列
        /// </summary>
        public ArrayInteractiveModel ArrayInteractive { get; set; } = new ArrayInteractiveModel();
        /// <summary>
        /// 环形阵列
        /// </summary>
        public ArrayAnnularModel ArrayAnnular { get; set; } = new ArrayAnnularModel();
        /// <summary>
        /// 布满阵列
        /// </summary>
        public ArrayFullModel ArrayFull { get; set; } = new ArrayFullModel();
        /// <summary>
        /// 多轮廓连切
        /// </summary>
        public MultiContourConnectCutModel MultiContourConnectCut { get; set; } = new MultiContourConnectCutModel();
        /// <summary>
        /// 桥接参数
        /// </summary>
        public BridgingModel Bridging { get; set; } = new BridgingModel();
        /// <summary>
        /// 曲线平滑参数
        /// </summary>
        public CurveSmoothingModel CurveSmoothing { get; set; } = new CurveSmoothingModel();
        /// <summary>
        /// 去除极小图形
        /// </summary>
        public DeleteSmallFigureModel DeleteSmallFigure { get; set; } = new DeleteSmallFigureModel();
        /// <summary>
        /// 合并相连线
        /// </summary>
        public MergeConnectLineModel MergeConnectLine { get; set; } = new MergeConnectLineModel();
        /// <summary>
        /// 切碎
        /// </summary>
        public CutUpModel CutUp { get; set; } = new CutUpModel();
        /// <summary>
        /// 接刀
        /// </summary>
        public ConnectKnifeModel ConnectKnife { get; set; } = new ConnectKnifeModel();
        /// <summary>
        /// 图层参数设置
        /// </summary>
        public LayerConfigModel LayerConfig { get; set; } = new LayerConfigModel();
        /// <summary>
        /// 加工计数管理
        /// </summary>
        public MachineCountModel MachineCount { get; set; } = new MachineCountModel();
        /// <summary>
        /// 显示图形附加信息
        /// </summary>
        public AdditionalInfo AdditionalInfo { get { return AdditionalInfo.Instance; } set { AdditionalInfo.Instance = value; } }
        /// <summary>
        /// 矩形共边
        /// </summary>
        public CommonSideRectangleModel CommonSideRectangle { get; set; } = new CommonSideRectangleModel();

    }
}
