using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WSX.CommomModel.DrawModel
{
    public class UnitPointBulge
    {
        public UnitPoint Point { get; set; }
        [XmlAttribute("Bulge")]
        public double Bulge { get; set; }
        /// <summary>
        /// 显示虚线（飞切 ）
        /// </summary>
        [XmlAttribute("Dotted")]
        public bool Dotted { get; set; } = false;

        [XmlIgnore]
        public bool HasMicroConn { get; set; }
        [XmlIgnore]
        public float Position { get; set; }
        [XmlIgnore]
        public bool IsBasePoint { get; set; }
        public UnitPointBulge() { }
        public UnitPointBulge(UnitPoint unitPoint, double bulge = double.NaN,bool hasMicroConn=false, float position=0,bool isBasePoint=false)
        {
            this.Point = unitPoint;
            this.Position = position;
            this.Bulge = bulge;
            this.HasMicroConn = hasMicroConn;
            this.IsBasePoint = isBasePoint;
        }
    }
}
