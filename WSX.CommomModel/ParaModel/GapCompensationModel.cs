using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 割缝补偿
    /// </summary>
    [Serializable]
    public class GapCompensationModel
    {
        /// <summary>
        /// 外补宽度
        /// </summary>
        public double OutSideWidth { get; set; }
        /// <summary>
        /// 内补宽度
        /// </summary>
        public double InSideWidth { get; set; }
        /// <summary>
        /// 拐角处理方式
        /// </summary>
        public CornerProcessTypes CornerProcessType { get; set; }
        /// <summary>
        /// 割缝补偿方式
        /// </summary>
        public GapCompensationTypes GapCompensationType { get; set; }
        /// <summary>
        /// 补偿不封闭图形
        /// </summary>
        public bool IsCompensationNotClosedFigure { get; set; }
    }
    /// <summary>
    /// 拐角处理方式
    /// </summary>
    [Serializable]
    public enum CornerProcessTypes
    {
        /// <summary>
        /// 直角
        /// </summary>
        RightAngle,
        /// <summary>
        /// 圆角
        /// </summary>
        Fillet,
    }
    /// <summary>
    /// 割缝补偿方式
    /// </summary>
    [Serializable]
    public enum GapCompensationTypes
    {
        /// <summary>
        /// 内膜内缩，外膜外扩
        /// </summary>
        ChokingAndOutsideEnlarge,
        /// <summary>
        /// 全部内缩
        /// </summary>
        AllChoking,
        /// <summary>
        /// 全部外扩
        /// </summary>
        AllOutsideEnlarge,
    }
}
