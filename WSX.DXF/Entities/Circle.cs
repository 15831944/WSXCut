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
    /// Represents a circle <see cref="EntityObject">entity</see>.
    /// </summary>
    public class Circle :
        EntityObject
    {
        #region private fields

        private Vector3 center;
        private double radius;
        private double thickness;

        #endregion

        #region constructors

        public Circle()
            : this(Vector3.Zero, 1.0)
        {
        }

        public Circle(Vector3 center, double radius)
            : base(EntityType.Circle, DxfObjectCode.Circle)
        {
            this.center = center;
            if (radius <= 0)
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "The circle radius must be greater than zero.");
            this.radius = radius;
            this.thickness = 0.0;
        }

        public Circle(Vector2 center, double radius)
            : this(new Vector3(center.X, center.Y, 0.0), radius)
        {
        }

        #endregion

        #region public properties

        public Vector3 Center
        {
            get { return this.center; }
            set { this.center = value; }
        }

        public double Radius
        {
            get { return this.radius; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The circle radius must be greater than zero.");
                this.radius = value;
            }
        }

        public double Thickness
        {
            get { return this.thickness; }
            set { this.thickness = value; }
        }

        #endregion

        #region public methods

        public List<Vector2> PolygonalVertexes(int precision)
        {
            if (precision < 3)
                throw new ArgumentOutOfRangeException(nameof(precision), precision, "The circle precision must be greater or equal to three");

            List<Vector2> ocsVertexes = new List<Vector2>();

            double delta = MathHelper.TwoPI/precision;

            for (int i = 0; i < precision; i++)
            {
                double angle = delta*i;
                double sine = this.radius*Math.Sin(angle);
                double cosine = this.radius*Math.Cos(angle);
                ocsVertexes.Add(new Vector2(cosine, sine));
            }
            return ocsVertexes;
        }

        public LwPolyline ToPolyline(int precision)
        {
            IEnumerable<Vector2> vertexes = this.PolygonalVertexes(precision);
            Vector3 ocsCenter = MathHelper.Transform(this.Center, this.Normal, CoordinateSystem.World, CoordinateSystem.Object);

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
                Thickness = this.thickness,
                IsClosed = true
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
            Circle entity = new Circle
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
                //Circle properties
                Center = this.center,
                Radius = this.radius,
                Thickness = this.thickness
            };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}