using System;
using System.Drawing;
using System.Threading;

namespace WSX.DXF
{
    /// <summary>
    /// Represent a two component vector of double precision.
    /// </summary>
    public struct Vector2 :
        IEquatable<Vector2>
    {
        #region private fields

        private double x;
        private double y;
        private bool isNormalized;

        #endregion

        #region constructors

        public Vector2(double value)
        {
            this.x = value;
            this.y = value;
            this.isNormalized = false;
        }

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.isNormalized = false;
        }

        public Vector2(double[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length != 2)
                throw new ArgumentOutOfRangeException(nameof(array), array.Length, "The dimension of the array must be two");
            this.x = array[0];
            this.y = array[1];
            this.isNormalized = false;
        }

        #endregion

        #region constants

        public static Vector2 Zero
        {
            get { return new Vector2(0, 0); }
        }

        public static Vector2 UnitX
        {
            get { return new Vector2(1, 0) {isNormalized = true}; }
        }

        public static Vector2 UnitY
        {
            get { return new Vector2(0, 1) {isNormalized = true}; }
        }

        public static Vector2 NaN
        {
            get { return new Vector2(double.NaN, double.NaN); }
        }

        #endregion

        #region public properties

        public double X
        {
            get { return this.x; }
            set
            {
                this.isNormalized = false;
                this.x = value;
            }
        }

        public double Y
        {
            get { return this.y; }
            set
            {
                this.isNormalized = false;
                this.y = value;
            }
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                this.isNormalized = false;
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }

        public bool IsNormalized
        {
            get { return this.isNormalized; }
        }

        #endregion

        #region static methods

        public static bool IsNaN(Vector2 u)
        {
            return double.IsNaN(u.X) || double.IsNaN(u.Y);
        }

        public static double DotProduct(Vector2 u, Vector2 v)
        {
            return u.X*v.X + u.Y*v.Y;
        }

        public static double CrossProduct(Vector2 u, Vector2 v)
        {
            return u.X*v.Y - u.Y*v.X;
        }

        public static Vector2 Perpendicular(Vector2 u)
        {
            return new Vector2(-u.Y, u.X) {isNormalized = u.IsNormalized};
        }

		public static implicit operator Vector2(PointF point)
		{
			return new Vector2(point.X, point.Y);
		}

		//public static explicit operator Vector2(PointF point)
		//{
		//	return new Vector2(point.X, point.Y);
		//}

		public static Vector2 Rotate(Vector2 u, double angle)
        {
            if (MathHelper.IsZero(angle))
                return u;
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            return new Vector2(u.X*cos - u.Y*sin, u.X*sin + u.Y*cos) {isNormalized = u.IsNormalized};
        }

        public static Vector2 Polar(Vector2 u, double distance, double angle)
        {
            Vector2 dir = new Vector2(Math.Cos(angle), Math.Sin(angle));
            return u + dir*distance;
        }

        public static double Distance(Vector2 u, Vector2 v)
        {
            return Math.Sqrt((u.X - v.X)*(u.X - v.X) + (u.Y - v.Y)*(u.Y - v.Y));
        }

        public static double SquareDistance(Vector2 u, Vector2 v)
        {
            return (u.X - v.X)*(u.X - v.X) + (u.Y - v.Y)*(u.Y - v.Y);
        }

        public static double Angle(Vector2 u)
        {
            double angle = Math.Atan2(u.Y, u.X);
            if (angle < 0)
                return MathHelper.TwoPI + angle;
            return angle;
        }

        public static double Angle(Vector2 u, Vector2 v)
        {
            Vector2 dir = v - u;
            return Angle(dir);
        }

        public static double AngleBetween(Vector2 u, Vector2 v)
        {
            double cos = DotProduct(u, v)/(u.Modulus()*v.Modulus());
            if (cos >= 1.0)
                return 0.0;
            if (cos <= -1.0)
                return Math.PI;

            return Math.Acos(cos);
        }

        public static Vector2 MidPoint(Vector2 u, Vector2 v)
        {
            return new Vector2((v.X + u.X)*0.5, (v.Y + u.Y)*0.5);
        }

        public static bool ArePerpendicular(Vector2 u, Vector2 v)
        {
            return ArePerpendicular(u, v, MathHelper.Epsilon);
        }

        public static bool ArePerpendicular(Vector2 u, Vector2 v, double threshold)
        {
            return MathHelper.IsZero(DotProduct(u, v), threshold);
        }

        public static bool AreParallel(Vector2 u, Vector2 v)
        {
            return AreParallel(u, v, MathHelper.Epsilon);
        }

        public static bool AreParallel(Vector2 u, Vector2 v, double threshold)
        {
            return MathHelper.IsZero(CrossProduct(u, v), threshold);
        }

        public static Vector2 Round(Vector2 u, int numDigits)
        {
            return new Vector2(Math.Round(u.X, numDigits), Math.Round(u.Y, numDigits));
        }

        public static Vector2 Normalize(Vector2 u)
        {
            if (u.isNormalized) return u;

            double mod = u.Modulus();
            if (MathHelper.IsZero(mod))
                return NaN;
            double modInv = 1/mod;
            return new Vector2(u.x*modInv, u.y*modInv) {isNormalized = true};
        }

        #endregion

        #region overloaded operators

        public static bool operator ==(Vector2 u, Vector2 v)
        {
            return Equals(u, v);
        }

        public static bool operator !=(Vector2 u, Vector2 v)
        {
            return !Equals(u, v);
        }

        public static Vector2 operator +(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X + v.X, u.Y + v.Y);
        }

        public static Vector2 Add(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X + v.X, u.Y + v.Y);
        }

        public static Vector2 operator -(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X - v.X, u.Y - v.Y);
        }

        public static Vector2 Subtract(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X - v.X, u.Y - v.Y);
        }

        public static Vector2 operator -(Vector2 u)
        {
            return new Vector2(-u.X, -u.Y) { isNormalized = u.IsNormalized };
        }

        public static Vector2 Negate(Vector2 u)
        {
            return new Vector2(-u.X, -u.Y) {isNormalized = u.IsNormalized};
        }

        public static Vector2 operator *(Vector2 u, double a)
        {
            return new Vector2(u.X*a, u.Y*a);
        }

        public static Vector2 Multiply(Vector2 u, double a)
        {
            return new Vector2(u.X*a, u.Y*a);
        }

        public static Vector2 operator *(double a, Vector2 u)
        {
            return new Vector2(u.X*a, u.Y*a);
        }

        public static Vector2 Multiply(double a, Vector2 u)
        {
            return new Vector2(u.X*a, u.Y*a);
        }

        public static Vector2 operator *(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X * v.X, u.Y * v.Y);
        }

        public static Vector2 Multiply(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X * v.X, u.Y * v.Y);
        }

        public static Vector2 operator /(double a, Vector2 u)
        {
            return new Vector2(a* u.X, a* u.Y);
        }

        public static Vector2 Divide(double a, Vector2 u)
        {
            return new Vector2(a * u.X, a * u.Y);
        }

        public static Vector2 operator /(Vector2 u, double a)
        {
            double invEscalar = 1/a;
            return new Vector2(u.X*invEscalar, u.Y*invEscalar);
        }

        public static Vector2 Divide(Vector2 u, double a)
        {
            double invEscalar = 1/a;
            return new Vector2(u.X*invEscalar, u.Y*invEscalar);
        }

        public static Vector2 operator /(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X / v.X, u.Y / v.Y);
        }

        public static Vector2 Divide(Vector2 u, Vector2 v)
        {
            return new Vector2(u.X / v.X, u.Y / v.Y);
        }

        #endregion

        #region public methods

        public void Normalize()
        {
            if (this.isNormalized) return;

            double mod = this.Modulus();
            if (MathHelper.IsZero(mod))
                this = NaN;
            else
            {
                double modInv = 1/mod;
                this.x *= modInv;
                this.y *= modInv;
            }
            this.isNormalized = true;
        }

        public double Modulus()
        {
            return Math.Sqrt(DotProduct(this, this));
        }

        public double[] ToArray()
        {
            return new[] {this.x, this.y};
        }

        #endregion

        #region comparison methods

        public static bool Equals(Vector2 a, Vector2 b)
        {
            return a.Equals(b, MathHelper.Epsilon);
        }

        public static bool Equals(Vector2 a, Vector2 b, double threshold)
        {
            return a.Equals(b, threshold);
        }

        public bool Equals(Vector2 other)
        {
            return this.Equals(other, MathHelper.Epsilon);
        }

        public bool Equals(Vector2 other, double threshold)
        {
            return MathHelper.IsEqual(other.X, this.x, threshold) && MathHelper.IsEqual(other.Y, this.y, threshold);
        }

        public override bool Equals(object other)
        {
            if (other is Vector2)
                return this.Equals((Vector2) other);
            return false;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return string.Format("{0}{2} {1}", this.x, this.y, Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator);
        }

        public string ToString(IFormatProvider provider)
        {
            return string.Format("{0}{2} {1}", this.x.ToString(provider), this.y.ToString(provider), Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator);
        }

        #endregion
    }
}