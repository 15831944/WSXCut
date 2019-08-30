#region WSX.DXF library, Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a spline curve <see cref="EntityObject">entity</see> (NURBS Non-Uniform Rational B-Splines).
    /// </summary>
    public class Spline :
        EntityObject
    {
        #region private fields

        private readonly List<Vector3> fitPoints;
        private readonly SplineCreationMethod creationMethod;
        private Vector3? startTangent;
        private Vector3? endTangent;
        private SplineKnotParameterization knotParameterization;
        private double knotTolerance = 0.0000001;
        private double ctrlPointTolerance = 0.0000001;
        private double fitTolerance = 0.0000000001;
        private List<SplineVertex> controlPoints;
        private List<double> knots;
        private readonly SplinetypeFlags flags;
        private readonly short degree;
        private readonly bool isClosed;
        private readonly bool isPeriodic;

        #endregion

        #region constructors

        public Spline(IEnumerable<Vector3> fitPoints)
            : base(EntityType.Spline, DxfObjectCode.Spline)
        {
            this.degree = 3;
            this.isPeriodic = false;
            this.controlPoints = new List<SplineVertex>();
            this.knots = new List<double>();
            if (fitPoints == null)
                throw new ArgumentNullException(nameof(fitPoints));
            this.fitPoints = new List<Vector3>(fitPoints);
            this.creationMethod = SplineCreationMethod.FitPoints;
            this.isClosed = this.fitPoints[0].Equals(this.fitPoints[this.fitPoints.Count - 1]);
            this.flags = this.isClosed ? SplinetypeFlags.Closed | SplinetypeFlags.Rational : SplinetypeFlags.Rational;
        }

        public Spline(List<SplineVertex> controlPoints, List<double> knots, short degree)
            : this(controlPoints, knots, degree, new List<Vector3>(), SplineCreationMethod.ControlPoints, false)
        {
        }

        public Spline(List<SplineVertex> controlPoints)
            : this(controlPoints, 3, false)
        {
        }

        public Spline(List<SplineVertex> controlPoints, bool periodic)
            : this(controlPoints, 3, periodic)
        {
        }

        public Spline(List<SplineVertex> controlPoints, short degree)
            : this(controlPoints, degree, false)
        {
        }

        public Spline(List<SplineVertex> controlPoints, short degree, bool periodic)
            : base(EntityType.Spline, DxfObjectCode.Spline)
        {
            if (degree < 1 || degree > 10)
                throw new ArgumentOutOfRangeException(nameof(degree), degree, "The spline degree valid values range from 1 to 10.");
            if (controlPoints == null)
                throw new ArgumentNullException(nameof(controlPoints));
            if (controlPoints.Count < 2)
                throw new ArgumentException("The number of control points must be equal or greater than 2.");
            if (controlPoints.Count < degree + 1)
                throw new ArgumentException("The number of control points must be equal or greater than the spline degree + 1.");

            this.fitPoints = new List<Vector3>();
            this.degree = degree;
            this.creationMethod = SplineCreationMethod.ControlPoints;

            this.isPeriodic = periodic;
            if (this.isPeriodic)
            {
                this.isClosed = true;
                this.flags = SplinetypeFlags.Closed | SplinetypeFlags.Periodic | SplinetypeFlags.Rational;
            }
            else
            {
                this.isClosed = controlPoints[0].Position.Equals(controlPoints[controlPoints.Count - 1].Position);
                this.flags = this.isClosed ? SplinetypeFlags.Closed | SplinetypeFlags.Rational : SplinetypeFlags.Rational;
            }
            this.Create(controlPoints);
        }

        internal Spline(List<SplineVertex> controlPoints, List<double> knots, short degree, List<Vector3> fitPoints, SplineCreationMethod method, bool isPeriodic)
            : base(EntityType.Spline, DxfObjectCode.Spline)
        {
            if (degree < 1 || degree > 10)
                throw new ArgumentOutOfRangeException(nameof(degree), degree, "The spline degree valid values range from 1 to 10.");
            if (controlPoints == null)
                throw new ArgumentNullException(nameof(controlPoints));
            if (controlPoints.Count < 2)
                throw new ArgumentException("The number of control points must be equal or greater than 2.");
            if (controlPoints.Count < degree + 1)
                throw new ArgumentException("The number of control points must be equal or greater than the spline degree + 1.");
            if (knots == null)
                throw new ArgumentNullException(nameof(knots));
            if (knots.Count != controlPoints.Count + degree + 1)
                throw new ArgumentException("The number of knots must be equals to the number of control points + spline degree + 1.");

            this.fitPoints = fitPoints;
            this.controlPoints = controlPoints;
            this.knots = knots;
            this.degree = degree;
            this.creationMethod = method;

            this.isPeriodic = isPeriodic;
            if (this.isPeriodic)
            {
                this.isClosed = true;
                this.flags = SplinetypeFlags.Closed | SplinetypeFlags.Periodic | SplinetypeFlags.Rational;
            }
            else
            {
                this.isClosed = controlPoints[0].Position.Equals(controlPoints[controlPoints.Count - 1].Position);
                this.flags = this.isClosed ? SplinetypeFlags.Closed | SplinetypeFlags.Rational : SplinetypeFlags.Rational;
            }
        }

        #endregion

        #region public properties

        public List<Vector3> FitPoints
        {
            get { return this.fitPoints; }
        }

        public Vector3? StartTangent
        {
            get { return this.startTangent; }
            set { this.startTangent = value; }
        }

        public Vector3? EndTangent
        {
            get { return this.endTangent; }
            set { this.endTangent = value; }
        }

        public SplineKnotParameterization KnotParameterization
        {
            get { return this.knotParameterization; }
            set { this.knotParameterization = value; }
        }

        public SplineCreationMethod CreationMethod
        {
            get { return this.creationMethod; }
        }

        public double KnotTolerance
        {
            get { return this.knotTolerance; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The knot tolerance must be greater than zero.");
                this.knotTolerance = value;
            }
        }

        public double CtrlPointTolerance
        {
            get { return this.ctrlPointTolerance; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The control point tolerance must be greater than zero.");
                this.ctrlPointTolerance = value;
            }
        }

        public double FitTolerance
        {
            get { return this.fitTolerance; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The fit tolerance must be greater than zero.");
                this.fitTolerance = value;
            }
        }

        public short Degree
        {
            get { return this.degree; }
        }

        public bool IsClosed
        {
            get { return this.isClosed; }
        }

        public bool IsPeriodic
        {
            get { return this.isPeriodic; }
        }

        public IReadOnlyList<SplineVertex> ControlPoints
        {
            get { return this.controlPoints; }
        }

        public IReadOnlyList<double> Knots
        {
            get { return this.knots; }
        }

        #endregion

        #region internal properties

        internal SplinetypeFlags Flags
        {
            get { return this.flags; }
        }

        #endregion

        #region public methods

        public void Reverse()
        {
            this.fitPoints.Reverse();
            this.controlPoints.Reverse();
            Vector3? tmp = this.startTangent;
            this.startTangent = -this.endTangent;
            this.endTangent = -tmp;
        }

        public void SetUniformWeights(double weight)
        {
            foreach (SplineVertex controlPoint in this.controlPoints)
            {
                controlPoint.Weigth = weight;
            }
        }

        #endregion

        #region private methods

        private void Create(List<SplineVertex> points)
        {
            this.controlPoints = new List<SplineVertex>();

            int replicate = this.isPeriodic ? this.degree : 0;
            int numControlPoints = points.Count + replicate;

            foreach (SplineVertex controlPoint in points)
            {
                SplineVertex vertex = new SplineVertex(controlPoint.Position, controlPoint.Weigth);
                this.controlPoints.Add(vertex);
            }

            for (int i = 0; i < replicate; i++)
            {
                SplineVertex vertex = new SplineVertex(points[i].Position, points[i].Weigth);
                this.controlPoints.Add(vertex);
            }

            int numKnots = numControlPoints + this.degree + 1;
            this.knots = new List<double>(numKnots);

            double factor = 1.0/(numControlPoints - this.degree);
            if (!this.isPeriodic)
            {
                int i;
                for (i = 0; i <= this.degree; i++)
                    this.knots.Add(0.0);

                for (; i < numControlPoints; i++)
                    this.knots.Add(i - this.degree);

                for (; i < numKnots; i++)
                    this.knots.Add(numControlPoints - this.degree);
            }
            else
            {
                for (int i = 0; i < numKnots; i++)
                    this.knots.Add((i - this.degree)*factor);
            }
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            Spline entity;
            if (this.creationMethod == SplineCreationMethod.FitPoints)
            {
                entity = new Spline(new List<Vector3>(this.fitPoints))
                {
                    //EntityObject properties
                    Layer = (Layer) this.Layer.Clone(),
                    Linetype = (Linetype) this.Linetype.Clone(),
                    Color = (AciColor) this.Color.Clone(),
                    Lineweight = this.Lineweight,
                    Transparency = (Transparency) this.Transparency.Clone(),
                    LinetypeScale = this.LinetypeScale,
                    Normal = this.Normal,
                    IsVisible = this.IsVisible,
                    //Spline properties
                    KnotParameterization = this.KnotParameterization,
                    StartTangent = this.startTangent,
                    EndTangent = this.endTangent
                };
            }
            else
            {
                List<SplineVertex> copyControlPoints = new List<SplineVertex>(this.controlPoints.Count);
                foreach (SplineVertex vertex in this.controlPoints)
                {
                    copyControlPoints.Add((SplineVertex) vertex.Clone());
                }
                List<double> copyKnots = new List<double>(this.knots);

                entity = new Spline(copyControlPoints, copyKnots, this.degree)
                {
                    //EntityObject properties
                    Layer = (Layer) this.Layer.Clone(),
                    Linetype = (Linetype) this.Linetype.Clone(),
                    Color = (AciColor) this.Color.Clone(),
                    Lineweight = this.Lineweight,
                    Transparency = (Transparency) this.Transparency.Clone(),
                    LinetypeScale = this.LinetypeScale,
                    Normal = this.Normal
                    //Spline properties
                };
            }


            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion

        #region Nurbs evaluator provided by mikau16 based on Michael V. implementation, roughly follows the notation of http://cs.mtu.edu/~shene/PUBLICATIONS/2004/NURBS.pdf

        public List<Vector3> PolygonalVertexes(int precision)
        {
            if (this.controlPoints.Count == 0)
                throw new NotSupportedException("A spline entity with control points is required.");

            double u_start;
            double u_end;
            List<Vector3> vertexes = new List<Vector3>();

            // added a few fixes to make it work for open, closed, and periodic closed splines.
            if (!this.isClosed)
            {
                precision -= 1;
                u_start = this.knots[0];
                u_end = this.knots[this.knots.Count - 1];
            }
            else if (this.isPeriodic)
            {
                u_start = this.knots[this.degree];
                u_end = this.knots[this.knots.Count - this.degree - 1];
            }
            else
            {
                u_start = this.knots[0];
                u_end = this.knots[this.knots.Count - 1];
            }

            double u_delta = (u_end - u_start)/precision;

            for (int i = 0; i < precision; i++)
            {
                double u = u_start + u_delta*i;
                vertexes.Add(this.C(u));
            }

            if (!this.isClosed)
                vertexes.Add(this.controlPoints[this.controlPoints.Count - 1].Position);

            return vertexes;
        }

        public Polyline ToPolyline(int precision)
        {
            IEnumerable<Vector3> vertexes = this.PolygonalVertexes(precision);

            Polyline poly = new Polyline
            {
                Layer = (Layer) this.Layer.Clone(),
                Linetype = (Linetype) this.Linetype.Clone(),
                Color = (AciColor) this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                IsClosed = this.isClosed
            };
            foreach (Vector3 v in vertexes)
            {
                poly.Vertexes.Add(new PolylineVertex(v));
            }
            return poly;
        }

        private Vector3 C(double u)
        {
            Vector3 vectorSum = Vector3.Zero;
            double denominatorSum = 0.0;

            // optimization suggested by ThVoss
            for (int i = 0; i < this.controlPoints.Count; i++)
            {
                double n = this.N(i, this.degree, u);
                denominatorSum += n*this.controlPoints[i].Weigth;
                vectorSum += this.controlPoints[i].Weigth*n*this.controlPoints[i].Position;
            }

            // avoid possible divided by zero error, this should never happen
            if (Math.Abs(denominatorSum) < double.Epsilon)
                return Vector3.Zero;

            return (1.0/denominatorSum)*vectorSum;
        }

        private double N(int i, int p, double u)
        {
            if (p <= 0)
            {
                if (this.knots[i] <= u && u < this.knots[i + 1])
                    return 1;
                return 0.0;
            }

            double leftCoefficient = 0.0;
            if (!(Math.Abs(this.knots[i + p] - this.knots[i]) < double.Epsilon))
                leftCoefficient = (u - this.knots[i])/(this.knots[i + p] - this.knots[i]);

            double rightCoefficient = 0.0; // article contains error here, denominator is Knots[i + p + 1] - Knots[i + 1]
            if (!(Math.Abs(this.knots[i + p + 1] - this.knots[i + 1]) < double.Epsilon))
                rightCoefficient = (this.knots[i + p + 1] - u)/(this.knots[i + p + 1] - this.knots[i + 1]);

            return leftCoefficient*this.N(i, p - 1, u) + rightCoefficient*this.N(i + 1, p - 1, u);
        }

        #endregion
    }
}