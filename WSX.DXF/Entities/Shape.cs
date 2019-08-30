#region WSX.DXF library, Copyright (C) 2009-2018 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2018 Daniel Carvajal (haplokuon@gmail.com)
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
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a shape entity.
    /// </summary>
    public class Shape :
        EntityObject
    {
        #region private fields

        private string name;
        private ShapeStyle style;
        private Vector3 position;
        private double size;
        private double rotation;
        private double obliqueAngle;
        private double widthFactor;
        private double thickness;

        #endregion

        #region constructors

        public Shape(string name, ShapeStyle style) : this(name, style, Vector3.Zero, 1.0, 0.0)
        {
        }

        public Shape(string name, ShapeStyle style, Vector3 position, double size, double rotation) : base(EntityType.Shape, DxfObjectCode.Shape)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            this.name = name;
            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.style = style;
            this.position = position;
            this.size = size;
            this.rotation = rotation;
            this.obliqueAngle = 0.0;
            this.widthFactor = 1.0;
            this.thickness = 0.0;
        }

        #endregion

        #region public properties

        public string Name
        {
            get { return this.name; }
        }

        public ShapeStyle Style
        {
            get { return this.style; }
            internal set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.style = value;
            }
        }

        public Vector3 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public double Size
        {
            get { return this.size; }
            set
            {
                if (MathHelper.IsZero(value))
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The shape cannot be zero.");
                this.size = value;
            }
        }

        public double Rotation
        {
            get { return this.rotation; }
            set { this.rotation = MathHelper.NormalizeAngle(value); }
        }

        public double ObliqueAngle
        {
            get { return this.obliqueAngle; }
            set { this.obliqueAngle = MathHelper.NormalizeAngle(value); }
        }

        public double WidthFactor
        {
            get { return this.widthFactor; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The shape width factor must be greater than zero.");
                this.widthFactor = value;
            }
        }

        public double Thickness
        {
            get { return this.thickness; }
            set { this.thickness = value; }
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            Shape entity = new Shape(this.name, (ShapeStyle)this.style.Clone())
            {
                //EntityObject properties
                Layer = (Layer)this.Layer.Clone(),
                Linetype = (Linetype)this.Linetype.Clone(),
                Color = (AciColor)this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency)this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                IsVisible = this.IsVisible,
                //Shape properties
                Position = this.position,
                Size = this.size,
                Rotation = this.rotation,
                ObliqueAngle = this.obliqueAngle,
                Thickness = this.thickness
        };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData)data.Clone());

            return entity;
        }

        #endregion
    }
}
