using System;
using System.Threading;

namespace WSX.DXF
{
    /// <summary>
    /// Represent a three component vector of double precision.
    /// </summary>
    public struct Vector3 :
        IEquatable<Vector3>
    {
        #region private fields

        private double x;
        private double y;
        private double z;
        private bool isNormalized;

        #endregion

        #region constructors

        public Vector3(double value)
        {
            this.x = value;
            this.y = value;
            this.z = value;
            this.isNormalized = false;
        }

        public Vector3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.isNormalized = false;
        }

        public Vector3(double[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length != 3)
                throw new ArgumentOutOfRangeException(nameof(array), array.Length, "The dimension of the array must be three.");
            this.x = array[0];
            this.y = array[1];
            this.z = array[2];
            this.isNormalized = false;
        }

        #endregion

        #region constants

        public static Vector3 Zero
        {
            get { return new Vector3(0, 0, 0); }
        }

        public static Vector3 UnitX
        {
            get { return new Vector3(1, 0, 0) {isNormalized = true}; }
        }

        public static Vector3 UnitY
        {
            get { return new Vector3(0, 1, 0) {isNormalized = true}; }
        }

        public static Vector3 UnitZ
        {
            get { return new Vector3(0, 0, 1) {isNormalized = true}; }
        }

        public static Vector3 NaN
        {
            get { return new Vector3(double.NaN, double.NaN, double.NaN); }
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

        public double Z
        {
            get { return this.z; }
            set
            {
                this.isNormalized = false;
                this.z = value;
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
                    case 2:
                        return this.z;
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
                    case 2:
                        this.z = value;
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

        ///  Returns a value indicating if any component of the specified vector evaluates to a value that is not a number <see cref="System.Double.NaN"/>.
        public static bool IsNaN(Vector3 u)
        {
            return double.IsNaN(u.X) || double.IsNaN(u.Y) || double.IsNaN(u.Z);
        }

        public static double DotProduct(Vector3 u, Vector3 v)
        {
            return u.X*v.X + u.Y*v.Y + u.Z*v.Z;
        }

        public static Vector3 CrossProduct(Vector3 u, Vector3 v)
        {
            double a = u.Y*v.Z - u.Z*v.Y;
            double b = u.Z*v.X - u.X*v.Z;
            double c = u.X*v.Y - u.Y*v.X;
            return new Vector3(a, b, c);
        }

        public static double Distance(Vector3 u, Vector3 v)
        {
            return Math.Sqrt((u.X - v.X)*(u.X - v.X) + (u.Y - v.Y)*(u.Y - v.Y) + (u.Z - v.Z)*(u.Z - v.Z));
        }

        public static double SquareDistance(Vector3 u, Vector3 v)
        {
            return (u.X - v.X)*(u.X - v.X) + (u.Y - v.Y)*(u.Y - v.Y) + (u.Z - v.Z)*(u.Z - v.Z);
        }

        public static double AngleBetween(Vector3 u, Vector3 v)
        {
            double cos = DotProduct(u, v)/(u.Modulus()*v.Modulus());
            if (cos >= 1.0)
                return 0.0;
            if (cos <= -1.0)
                return Math.PI;

            return Math.Acos(cos);
        }

        public static Vector3 MidPoint(Vector3 u, Vector3 v)
        {
            return new Vector3((v.X + u.X)*0.5, (v.Y + u.Y)*0.5, (v.Z + u.Z)*0.5);
        }

        public static bool ArePerpendicular(Vector3 u, Vector3 v)
        {
            return ArePerpendicular(u, v, MathHelper.Epsilon);
        }

        public static bool ArePerpendicular(Vector3 u, Vector3 v, double threshold)
        {
            return MathHelper.IsZero(DotProduct(u, v), threshold);
        }

        public static bool AreParallel(Vector3 u, Vector3 v)
        {
            return AreParallel(u, v, MathHelper.Epsilon);
        }

        public static bool AreParallel(Vector3 u, Vector3 v, double threshold)
        {
            Vector3 cross = CrossProduct(u, v);

            if (!MathHelper.IsZero(cross.X, threshold))
                return false;
            if (!MathHelper.IsZero(cross.Y, threshold))
                return false;
            if (!MathHelper.IsZero(cross.Z, threshold))
                return false;
            return true;
        }

        public static Vector3 Round(Vector3 u, int numDigits)
        {
            return new Vector3(Math.Round(u.X, numDigits), Math.Round(u.Y, numDigits), Math.Round(u.Z, numDigits));
        }

        public static Vector3 Normalize(Vector3 u)
        {
            if (u.isNormalized) return u;

            double mod = u.Modulus();
            if (MathHelper.IsZero(mod))
                return NaN;
            double modInv = 1/mod;
            Vector3 vec = new Vector3(u.x*modInv, u.y*modInv, u.z*modInv);
            vec.isNormalized = true;
            return vec;
        }

        #endregion

        #region overloaded operators

        public static bool operator ==(Vector3 u, Vector3 v)
        {
            return Equals(u, v);
        }

        public static bool operator !=(Vector3 u, Vector3 v)
        {
            return !Equals(u, v);
        }

        public static Vector3 operator +(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        }

        public static Vector3 Add(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        }

        public static Vector3 operator -(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        }

        public static Vector3 Subtract(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        }

        public static Vector3 operator -(Vector3 u)
        {
            return new Vector3(-u.X, -u.Y, -u.Z) { isNormalized = u.IsNormalized };
        }

        public static Vector3 Negate(Vector3 u)
        {
            return new Vector3(-u.X, -u.Y, -u.Z) { isNormalized = u.IsNormalized };
        }

        public static Vector3 operator *(Vector3 u, double a)
        {
            return new Vector3(u.X*a, u.Y*a, u.Z*a);
        }

        public static Vector3 Multiply(Vector3 u, double a)
        {
            return new Vector3(u.X*a, u.Y*a, u.Z*a);
        }

        public static Vector3 operator *(double a, Vector3 u)
        {
            return new Vector3(u.X*a, u.Y*a, u.Z*a);
        }

        public static Vector3 Multiply(double a, Vector3 u)
        {
            return new Vector3(u.X*a, u.Y*a, u.Z*a);
        }

        public static Vector3 operator *(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X * v.X, u.Y * v.Y, u.Z * v.Z);
        }

        public static Vector3 Multiply(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X * v.X, u.Y * v.Y, u.Z * v.Z);
        }

        public static Vector3 operator /(double a, Vector3 u)
        {
            return new Vector3(a * u.X, a * u.Y, a * u.Z);
        }

        public static Vector3 Divide(double a, Vector3 u)
        {
            return new Vector3(a * u.X, a * u.Y, a * u.Z);
        }

        public static Vector3 operator /(Vector3 u, double a)
        {
            double invEscalar = 1/a;
            return new Vector3(u.X*invEscalar, u.Y*invEscalar, u.Z*invEscalar);
        }

        public static Vector3 Divide(Vector3 u, double a)
        {
            double invEscalar = 1/a;
            return new Vector3(u.X*invEscalar, u.Y*invEscalar, u.Z*invEscalar);
        }

        public static Vector3 operator /(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X / v.X, u.Y / v.Y, u.Z / v.Z);
        }

        public static Vector3 Divide(Vector3 u, Vector3 v)
        {
            return new Vector3(u.X / v.X, u.Y / v.Y, u.Z / v.Z);
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
                this.z *= modInv;
            }
            this.isNormalized = true;
        }

        public double Modulus()
        {
            return Math.Sqrt(DotProduct(this, this));
        }

        public double[] ToArray()
        {
            return new[] {this.x, this.y, this.z};
        }

        #endregion

        #region comparison methods

        public static bool Equals(Vector3 a, Vector3 b)
        {
            return a.Equals(b, MathHelper.Epsilon);
        }

        public static bool Equals(Vector3 a, Vector3 b, double threshold)
        {
            return a.Equals(b, threshold);
        }

        public bool Equals(Vector3 other)
        {
            return this.Equals(other, MathHelper.Epsilon);
        }

        public bool Equals(Vector3 other, double threshold)
        {
            return MathHelper.IsEqual(other.X, this.x, threshold) && MathHelper.IsEqual(other.Y, this.y, threshold) && MathHelper.IsEqual(other.Z, this.z, threshold);
        }

        public override bool Equals(object other)
        {
            if (other is Vector3)
                return this.Equals((Vector3) other);
            return false;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return string.Format("{0}{3} {1}{3} {2}", this.x, this.y, this.z, Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator);
        }

        public string ToString(IFormatProvider provider)
        {
            return string.Format("{0}{3} {1}{3} {2}", this.x.ToString(provider), this.y.ToString(provider), this.z.ToString(provider), Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator);
        }

        #endregion
    }
}