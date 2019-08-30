using System;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

namespace WSX.CommomModel.DrawModel
{
    [Serializable]
    /// <summary>
    /// 单位点
    /// </summary>
    public struct UnitPoint
    {
        public static UnitPoint Empty;

        static UnitPoint()
        {
            Empty = new UnitPoint();
            Empty.X = double.NaN;
            Empty.Y = double.NaN;
        }
        public UnitPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public UnitPoint(PointF p)
        {
            X = p.X;
            Y = p.Y;
        }
        public static bool operator ==(UnitPoint left, UnitPoint right)
        {
            if (left.X == right.X)
                return left.Y == right.Y;
            if (left.IsEmpty && right.IsEmpty) // after changing Empty to use NaN this extra check is required
                return true;
            return false;
        }
        public static bool operator !=(UnitPoint left, UnitPoint right)
        {
            return !(left == right);
        }
        public static UnitPoint operator +(UnitPoint left, UnitPoint right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
        }
        public static UnitPoint operator -(UnitPoint left, UnitPoint right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }
        public static UnitPoint operator *(UnitPoint left, double right)
        {
            left.X *= right;
            left.Y *= right;
            return left;
        }
        public static UnitPoint operator /(UnitPoint left, double right)
        {
            left.X /= right;
            left.Y /= right;
            return left;
        }

        public bool IsEmpty
        {
            get
            {
                return double.IsNaN(X) && double.IsNaN(Y);
            }
        }
        [XmlAttribute("X")]
        public double X { get; set; }
        [XmlAttribute("Y")]
        public double Y { get; set; }
        public PointF Point
        {
            get { return new PointF((float)X, (float)Y); }
        }
        public override string ToString()
        {
            return string.Format("{{X={0}, Y={1}}}", XmlConvert.ToString(Math.Round(X, 8)), XmlConvert.ToString(Math.Round(Y, 8)));
        }
        public override bool Equals(object obj)
        {
            if (obj is UnitPoint)
                return (this == (UnitPoint)obj);
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public string PosAsString()
        {
            return string.Format("[{0:f4}, {1:f4}]", X, Y);
        }
    }
}
