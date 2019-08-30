using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 飞切的光滑连接线参数 贝塞尔曲线参数
    /// </summary>
    [Serializable]
    public class BezierParamModel
    {
        /**
         *  线段P1/P2光滑连接 PB1/PB2  
         *  P1P2    引出一段直线距离为 LeadlineDistance 这得到一个点A  P1A延伸BezierWide得到B
         *  PB1/PB2 反向有一段引入直线距离为 LeadlineDistance 这得到一个点D  PB2D 反向延伸BezierWide得到C点
         *  ABCD的贝塞尔曲线以及两端直线组成平滑连接线
         * */
        /// <summary>
        /// 两条线段引出/引入一段直线的长度
        /// </summary>
        public double LeadlineDistance { get; set; }

        /// <summary>
        /// 贝塞尔曲线AB点的距离
        /// </summary>
        public double BezierWide { get; set; }

        /// <summary>
        /// 要连接的线段起点
        /// </summary>
        public UnitPoint ConnectStartPoint { set; get; }

        /// <summary>
        /// 要连接的线段终点
        /// </summary>
        public UnitPoint ConnectEndPoint { set; get; }
    }
}
