using System;
using System.Collections.Generic;

namespace WSX.DXF
{
    /// <summary>
    /// Utility math functions and constants.
    /// </summary>
    public static class MathHelper
    {
        #region constants

        public const double DegToRad = Math.PI/180.0;

        public const double RadToDeg = 180.0/Math.PI;

        public const double DegToGrad = 10.0/9.0;

        public const double GradToDeg = 9.0/10.0;

        public const double HalfPI = Math.PI*0.5;

        public const double PI = Math.PI;

        /// 3*PI/2 (270 degrees)
        public const double ThreeHalfPI = 3*Math.PI*0.5;

        /// 2*PI (360 degrees)
        public const double TwoPI = 2*Math.PI;

        #endregion

        #region public properties

        private static double epsilon = 1e-12;

        public static double Epsilon
        {
            get { return epsilon; }
            set { epsilon = value; }
        }

        #endregion

        #region static methods

        public static bool IsOne(double number)
        {
            return IsOne(number, Epsilon);
        }

        public static bool IsOne(double number, double threshold)
        {
            return IsZero(number - 1, threshold);
        }

        public static bool IsZero(double number)
        {
            return IsZero(number, Epsilon);
        }

        public static bool IsZero(double number, double threshold)
        {
            return number >= -threshold && number <= threshold;
        }

        public static bool IsEqual(double a, double b)
        {
            return IsEqual(a, b, Epsilon);
        }

        public static bool IsEqual(double a, double b, double threshold)
        {
            return IsZero(a - b, threshold);
        }

        public static Vector2 Transform(Vector2 point, double rotation, CoordinateSystem from, CoordinateSystem to)
        {
            // if the rotation is 0 no transformation is needed the transformation matrix is the identity
            if (IsZero(rotation))
                return point;

            double sin = Math.Sin(rotation);
            double cos = Math.Cos(rotation);
            if (from == CoordinateSystem.World && to == CoordinateSystem.Object)
            {
                return new Vector2(point.X*cos + point.Y*sin, -point.X*sin + point.Y*cos);
            }
            if (from == CoordinateSystem.Object && to == CoordinateSystem.World)
            {
                return new Vector2(point.X*cos - point.Y*sin, point.X*sin + point.Y*cos);
            }
            return point;
        }

        public static IList<Vector2> Transform(IEnumerable<Vector2> points, double rotation, CoordinateSystem from, CoordinateSystem to)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            // if the rotation is 0 no transformation is needed the transformation matrix is the identity
            if (IsZero(rotation))
                return new List<Vector2>(points);

            double sin = Math.Sin(rotation);
            double cos = Math.Cos(rotation);
            List<Vector2> transPoints;
            if (from == CoordinateSystem.World && to == CoordinateSystem.Object)
            {
                transPoints = new List<Vector2>();
                foreach (Vector2 p in points)
                    transPoints.Add(new Vector2(p.X*cos + p.Y*sin, -p.X*sin + p.Y*cos));
                return transPoints;
            }
            if (from == CoordinateSystem.Object && to == CoordinateSystem.World)
            {
                transPoints = new List<Vector2>();
                foreach (Vector2 p in points)
                    transPoints.Add(new Vector2(p.X*cos - p.Y*sin, p.X*sin + p.Y*cos));
                return transPoints;
            }
            return new List<Vector2>(points);
        }

        public static Vector3 Transform(Vector3 point, Vector3 zAxis, CoordinateSystem from, CoordinateSystem to)
        {
            // if the normal is (0,0,1) no transformation is needed the transformation matrix is the identity
            if (zAxis.Equals(Vector3.UnitZ))
                return point;

            Matrix3 trans = ArbitraryAxis(zAxis);
            if (from == CoordinateSystem.World && to == CoordinateSystem.Object)
            {
                trans = trans.Transpose();
                return trans*point;
            }
            if (from == CoordinateSystem.Object && to == CoordinateSystem.World)
            {
                return trans*point;
            }
            return point;
        }

        public static IList<Vector3> Transform(IEnumerable<Vector3> points, Vector3 zAxis, CoordinateSystem from, CoordinateSystem to)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            if (zAxis.Equals(Vector3.UnitZ))
                return new List<Vector3>(points);

            Matrix3 trans = ArbitraryAxis(zAxis);
            List<Vector3> transPoints;
            if (from == CoordinateSystem.World && to == CoordinateSystem.Object)
            {
                transPoints = new List<Vector3>();
                trans = trans.Transpose();
                foreach (Vector3 p in points)
                {
                    transPoints.Add(trans*p);
                }
                return transPoints;
            }
            if (from == CoordinateSystem.Object && to == CoordinateSystem.World)
            {
                transPoints = new List<Vector3>();
                foreach (Vector3 p in points)
                {
                    transPoints.Add(trans*p);
                }
                return transPoints;
            }
            return new List<Vector3>(points);
        }

        public static Matrix3 ArbitraryAxis(Vector3 zAxis)
        {
            zAxis.Normalize();
            Vector3 wY = Vector3.UnitY;
            Vector3 wZ = Vector3.UnitZ;
            Vector3 aX;

            if ((Math.Abs(zAxis.X) < 1/64.0) && (Math.Abs(zAxis.Y) < 1/64.0))
                aX = Vector3.CrossProduct(wY, zAxis);
            else
                aX = Vector3.CrossProduct(wZ, zAxis);

            aX.Normalize();

            Vector3 aY = Vector3.CrossProduct(zAxis, aX);
            aY.Normalize();

            return new Matrix3(aX.X, aY.X, zAxis.X, aX.Y, aY.Y, zAxis.Y, aX.Z, aY.Z, zAxis.Z);
        }

        public static double PointLineDistance(Vector3 p, Vector3 origin, Vector3 dir)
        {
            double t = Vector3.DotProduct(dir, p - origin);
            Vector3 pPrime = origin + t*dir;
            Vector3 vec = p - pPrime;
            double distanceSquared = Vector3.DotProduct(vec, vec);
            return Math.Sqrt(distanceSquared);
        }

        public static double PointLineDistance(Vector2 p, Vector2 origin, Vector2 dir)
        {
            double t = Vector2.DotProduct(dir, p - origin);
            Vector2 pPrime = origin + t*dir;
            Vector2 vec = p - pPrime;
            double distanceSquared = Vector2.DotProduct(vec, vec);
            return Math.Sqrt(distanceSquared);
        }

        public static int PointInSegment(Vector3 p, Vector3 start, Vector3 end)
        {
            Vector3 dir = end - start;
            Vector3 pPrime = p - start;
            double t = Vector3.DotProduct(dir, pPrime);
            if (t <= 0)
            {
                return -1;
            }
            double dot = Vector3.DotProduct(dir, dir);
            if (t >= dot)
            {
                return 1;
            }
            return 0;
        }

        public static int PointInSegment(Vector2 p, Vector2 start, Vector2 end)
        {
            Vector2 dir = end - start;
            Vector2 pPrime = p - start;
            double t = Vector2.DotProduct(dir, pPrime);
            if (t <= 0)
            {
                return -1;
            }
            double dot = Vector2.DotProduct(dir, dir);
            if (t >= dot)
            {
                return 1;
            }
            return 0;
        }

        public static Vector2 FindIntersection(Vector2 point0, Vector2 dir0, Vector2 point1, Vector2 dir1)
        {
            return FindIntersection(point0, dir0, point1, dir1, Epsilon);
        }

        public static Vector2 FindIntersection(Vector2 point0, Vector2 dir0, Vector2 point1, Vector2 dir1, double threshold)
        {
            // test for parallelism.
            if (Vector2.AreParallel(dir0, dir1, threshold))
                return new Vector2(double.NaN, double.NaN);

            // lines are not parallel
            Vector2 vect = point1 - point0;
            double cross = Vector2.CrossProduct(dir0, dir1);
            double s = (vect.X*dir1.Y - vect.Y*dir1.X)/cross;
            return point0 + s*dir0;
        }

        public static double NormalizeAngle(double angle)
        {
            double c = angle%360.0;
            if (c < 0)
                return 360.0 + c;
            return c;
        }

        public static double RoundToNearest(double number, double roundTo)
        {
            double multiplier = Math.Round(number/roundTo, 0);
            return multiplier * roundTo;
        }

        public static Vector3 RotateAboutAxis(Vector3 v, Vector3 axis, double angle)
        {
            Vector3 q = new Vector3();
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            q.X += (cos + (1 - cos)*axis.X*axis.X)*v.X;
            q.X += ((1 - cos)*axis.X*axis.Y - axis.Z*sin)*v.Y;
            q.X += ((1 - cos)*axis.X*axis.Z + axis.Y*sin)*v.Z;

            q.Y += ((1 - cos)*axis.X*axis.Y + axis.Z*sin)*v.X;
            q.Y += (cos + (1 - cos)*axis.Y*axis.Y)*v.Y;
            q.Y += ((1 - cos)*axis.Y*axis.Z - axis.X*sin)*v.Z;

            q.Z += ((1 - cos)*axis.X*axis.Z - axis.Y*sin)*v.X;
            q.Z += ((1 - cos)*axis.Y*axis.Z + axis.X*sin)*v.Y;
            q.Z += (cos + (1 - cos)*axis.Z*axis.Z)*v.Z;

            return q;
        }

        #endregion
    }
}