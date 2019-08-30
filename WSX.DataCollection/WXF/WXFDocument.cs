using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WSX.CommomModel.DrawModel;

namespace WSX.DataCollection.WXF
{
    /// <summary>
    /// wxf配置文件模型，用于保存xml的格式对象
    /// </summary>
    [XmlRoot("WXFDocument")]
    public class WXFDocument
    {
        /// <summary>
        /// 图形实体
        /// </summary>
        [XmlElement("Entities")]
        public Entities Entities { get; set; }
    }
}
