using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WSX.CommomModel.DrawModel.MicroConn
{
    [Serializable]
    public class MicroConnectModel
    {
        [XmlAttribute("Pos")]
        public float Position { get; set; }
        [XmlAttribute("Size")]
        public float Size { get; set; }
        [XmlAttribute("Flags")]
        public MicroConnectFlags Flags { get; set; }
    }
}
