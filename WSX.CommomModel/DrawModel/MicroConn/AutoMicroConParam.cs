using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.DrawModel.MicroConn
{
    public class AutoMicroConParam
    {
        /// <summary>
        /// 是否使用数量的方式进行微连，否则使用间隔距离
        /// </summary>
        public bool  IsTypeCount{ get; set; }
        /// <summary>
        /// 数量或者是间距值，根据类型值来确定
        /// </summary>
        public double TypeValue { get; set; }
        /// <summary>
        /// 微连大小
        /// </summary>
        public double MicroSize { get; set; }
        /// <summary>
        /// 起点不微连
        /// </summary>
        public bool IsNotApplyStartPoint { get; set; }
        /// <summary>
        /// 拐角不微连
        /// </summary>
        public bool IsNotApplyCorner { get; set; }
        /// <summary>
        /// 是或否启用最小尺寸
        /// </summary>
        public bool IsMinSize { get; set; }
        /// <summary>
        /// 是或否启用最大尺寸
        /// </summary>
        public bool IsMaxSize { get; set; }
        /// <summary>
        /// 过滤最大尺寸
        /// </summary>
        public double MaxSize { get; set; }
        /// <summary>
        /// 过滤最小尺寸
        /// </summary>
        public double MinSize { get; set; }
    }
}
