#region WSX.DXF library, Copyright (C) 2009-2017 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2017 Daniel Carvajal (haplokuon@gmail.com)
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
    /// Represents an ellipse <see cref="EntityObject">entity</see>.
    /// </summary>
    public class Ellipse :
        EntityObject
    {
        #region private fields

        private Vector3 center;
        private double majorAxis;
        private double minorAxis;
        private double rotation;
        private double startAngle;
        private double endAngle;
        private double thickness;

        #endregion

        #region constructors

        public Ellipse()
            : this(Vector3.Zero, 1.0, 0.5)
        {
        }

        public Ellipse(Vector2 center, double majorAxis, double minorAxis)
            : this(new Vector3(center.X, center.Y, 0.0), majorAxis, minorAxis)
        {
        }

        public Ellipse(Vector3 center, double majorAxis, double minorAxis)
            : base(EntityType.Ellipse, DxfObjectCode.Ellipse)
        {
            this.center = center;
            if (majorAxis <= 0)
                throw new ArgumentOutOfRangeException(nameof(majorAxis), majorAxis, "The major axis value must be greater than zero.");
            this.majorAxis = majorAxis;
            if (minorAxis <= 0)
                throw new ArgumentOutOfRangeException(nameof(minorAxis), minorAxis, "The minor axis value must be greater than zero.");
            this.minorAxis = minorAxis;
            this.startAngle = 0.0;
            this.endAngle = 0.0;
            this.rotation = 0.0;
            this.thickness = 0.0;
        }

        #endregion

        #region public properties

        public Vector3 Center
        {
            get { return this.center; }
            set { this.center = value; }
        }

        public double MajorAxis
        {
            get { return this.majorAxis; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The major axis value must be greater than zero.");
                this.majorAxis = value;
            }
        }

        public double MinorAxis
        {
            get { return this.minorAxis; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The minor axis value must be greater than zero.");
                this.minorAxis = value;
            }
        }

        public double Rotation
        {
            get { return this.rotation; }
            set { this.rotation = MathHelper.NormalizeAngle(value); }
        }

        public double StartAngle
        {
            get { return this.startAngle; }
            set { this.startAngle = MathHelper.NormalizeAngle(value); }
        }

        public double EndAngle
        {
            get { return this.endAngle; }
            set { this.endAngle = MathHelper.NormalizeAngle(value); }
        }

        public double Thickness
        {
            get { return this.thickness; }
            set { this.thickness = value; }
        }

        public bool IsFullEllipse
        {
            get { return MathHelper.IsEqual(this.startAngle, this.endAngle); }
        }

        #endregion

        #region public methods

        public Vector2 PolarCoordinateRelativeToCenter(double angle)
        {
            double a = this.MajorAxis*0.5;
            double b = this.MinorAxis*0.5;
            double radians = angle*MathHelper.DegToRad;

            double a1 = a*Math.Sin(radians);
            double b1 = b*Math.Cos(radians);

            double radius = (a*b)/Math.Sqrt(b1*b1 + a1*a1);

            // convert the radius back to cartesian coordinates
            return new Vector2(radius*Math.Cos(radians), radius*Math.Sin(radians));
        }

        public List<Vector2> PolygonalVertexes(int precision)
        {
            List<Vector2> points = new List<Vector2>();
            double beta = this.rotation * MathHelper.DegToRad;
            double sinbeta = Math.Sin(beta);
            double cosbeta = Math.Cos(beta);

            if (this.IsFullEllipse)
            {
                double delta = MathHelper.TwoPI/precision;
                for (int i = 0; i < precision; i++)
                {
                    double angle = delta*i;
                    double sinalpha = Math.Sin(angle);
                    double cosalpha = Math.Cos(angle);

                    double pointX = 0.5*(this.majorAxis*cosalpha*cosbeta - this.minorAxis*sinalpha*sinbeta);
                    double pointY = 0.5*(this.majorAxis*cosalpha*sinbeta + this.minorAxis*sinalpha*cosbeta);

                    points.Add(new Vector2(pointX, pointY));
                }
            }
            else
            {
                double start = this.startAngle;
                double end = this.endAngle;
                if (end < start) end += 360.0;
                double delta = (end - start) / precision;
                for (int i = 0; i <= precision; i++)
                {
                    Vector2 point = this.PolarCoordinateRelativeToCenter(start + delta*i);
                    // we need to apply the ellipse rotation to the local point
                    double pointX = point.X * cosbeta - point.Y * sinbeta;
                    double pointY = point.X * sinbeta + point.Y * cosbeta;
                    points.Add(new Vector2(pointX, pointY));
                }
            }
            return points;
        }

        public LwPolyline ToPolyline(int precision)
        {
            IEnumerable<Vector2> vertexes = this.PolygonalVertexes(precision);
            Vector3 ocsCenter = MathHelper.Transform(this.center, this.Normal, CoordinateSystem.World, CoordinateSystem.Object);
            LwPolyline poly = new LwPolyline
            {
                Layer = (Layer) this.Layer.Clone(),
                Linetype = (Linetype) this.Linetype.Clone(),
                Color = (AciColor) this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                Elevation = ocsCenter.Z,
                Thickness = this.Thickness,
                IsClosed = this.IsFullEllipse
            };

            foreach (Vector2 v in vertexes)
            {
                poly.Vertexes.Add(new LwPolylineVertex(v.X + ocsCenter.X, v.Y + ocsCenter.Y));
            }
            return poly;
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            Ellipse entity = new Ellipse
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
                //Ellipse properties
                Center = this.center,
                MajorAxis = this.majorAxis,
                MinorAxis = this.minorAxis,
                Rotation = this.rotation,
                StartAngle = this.startAngle,
                EndAngle = this.endAngle,
                Thickness = this.thickness
            };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}