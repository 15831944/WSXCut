using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 自动添加冷却点
    /// </summary>
    [Serializable]
    public class AutoAddCoolingDotModel
    {
        /// <summary>
        /// 引入点冷却
        /// </summary>
        public bool IsLeadinPointCooling { get; set; }
        /// <summary>
        /// 尖角冷却
        /// </summary>
        public bool IsSharpAngleCooling { get; set; }
        /// <summary>
        /// 最大夹角
        /// </summary>
        public double MaxAngle { get; set; }
    }
}
