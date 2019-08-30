using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 圆弧飞切的虚线连接参数
    /// </summary>
    [Serializable]
    public class ArcFlyingLeadOut
    {
        public bool HasLeadLine { set; get; } = false;
        public bool HasLeadArc { set; get; } = false;

        public bool HasBezierCurve { set; get; } = false;

        public double LeadlineDistance { set; get; } = 0.5;// 直线延伸长度

        public double BezierWide { set; get; } = 1.0;     // 确定贝塞尔曲线点的延伸长度

        /// <summary>
        /// 圆弧段的结束位置 圆的开始(结束位置) StartAngel 到 SweepEndAngle为切割完后的过切段(虚线)
        /// </summary>
        public float LeadEndAngle { set; get; }

        /// <summary>
        /// 即下一个圆的起始点 SweepEndAngle的结束点到LineEndPoint 直线空移(虚线)
        /// </summary>
        public UnitPoint LeadEndPoint { set; get; }


        //贝塞尔曲线 PreviousEndPoint到当前圆的StartMovePoint的直线切线  通过贝塞尔曲线连接 LeadEndPoint 到LeadEndPoint的直线切线
        public UnitPoint PreviousEndPoint { set; get; } = UnitPoint.Empty;

        public UnitPoint NextNextPoint { set; get; } = UnitPoint.Empty;

    }
}
