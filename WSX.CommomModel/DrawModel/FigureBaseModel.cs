using System.Collections.Generic;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.LeadLine;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.DrawModel.RingCut;

namespace WSX.CommomModel.DrawModel
{
    /// <summary>
    /// 图形基类
    /// </summary>
    public abstract class FigureBaseModel
    {
        /// <summary>
        /// 图形类型
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public FigureTypes Type { get; protected set; }
        /// <summary>
        /// 图层通道，所在图层(默认1-15)
        /// </summary>
        [XmlAttribute("LayerId")]
        public int LayerId { get; set; }

        /// <summary>
        /// 是否封闭图形
        /// </summary>
        [XmlAttribute("IsFill")]
        public bool IsFill { get; set; }
        /// <summary>
        /// 是否顺时针
        /// </summary>
        [XmlAttribute("IsClockwise")]
        public bool IsClockwise { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        [XmlAttribute("IsSelected")]
        public bool IsSelected { get; set; }
        /// <summary>
        /// 是否阴切
        /// </summary>
        [XmlAttribute("IsInnerCut")]
        public bool IsInnerCut { get; set; }
        /// <summary>
        /// 引入线
        /// </summary>
        [XmlElement("LeadIn")]
        public LeadInLine LeadIn { get; set; } = new LeadInLine();
        /// <summary>
        /// 引出线
        /// </summary>
        [XmlElement("LeadOut")]
        public LeadOutLine LeadOut { get; set; } = new LeadOutLine();
        /// <summary>
        /// 微连参数集合
        /// </summary>
        public List<MicroConnectModel> MicroConnects { get; set; }
        /// <summary>
        /// 补偿参数
        /// </summary>
        public CompensationModel CompensationParam { get; set; }
        /// <summary>
        /// 拐角环切参数
        /// </summary>
        public CornerRingModel CornerRingParam { get; set; }
        /// <summary>
        /// 群组相关参数
        /// </summary>
        public GroupParam GroupParam { get; set; }// = new GroupParam();
    }
}
