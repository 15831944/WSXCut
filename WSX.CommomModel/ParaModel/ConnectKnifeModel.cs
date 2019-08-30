using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 接刀
    /// </summary>
    [Serializable]
    public class ConnectKnifeModel
    {
        /// <summary>
        /// 幅面长度
        /// </summary>
        public double FormatLength  { get; set; }
        /// <summary>
        /// 推动距离
        /// </summary>
        public double DriveDistance { get; set; }
        /// <summary>
        /// 分离方向
        /// </summary>
        public SeparationDirections SeparationDirection { get; set; }
    }
    /// <summary>
    /// 分割方向
    /// </summary>
    [Serializable]
    public enum SeparationDirections
    {
        /// <summary>
        /// 分割方向X
        /// </summary>
        X,
        /// <summary>
        /// 分割方向Y
        /// </summary>
        Y
    }
}
