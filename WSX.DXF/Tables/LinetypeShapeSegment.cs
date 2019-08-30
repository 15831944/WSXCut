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

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Represents a shape linetype segment.
    /// </summary>
    public class LinetypeShapeSegment :
        LinetypeSegment
    {
        #region private fields

        private string name;
        private ShapeStyle style;
        private Vector2 offset;
        private LinetypeSegmentRotationType rotationType;
        private double rotation;
        private double scale;

        #endregion

        #region constructors

        public LinetypeShapeSegment(string name, ShapeStyle style) : this(name, style, 0.0, Vector2.Zero, LinetypeSegmentRotationType.Relative, 0.0, 1.0)
        {
        }

        public LinetypeShapeSegment(string name, ShapeStyle style, double length) : this(name, style, length, Vector2.Zero, LinetypeSegmentRotationType.Relative, 0.0, 1.0)
        {
        }

        public LinetypeShapeSegment(string name, ShapeStyle style, double length, Vector2 offset, LinetypeSegmentRotationType rotationType, double rotation, double scale) : base(LinetypeSegmentType.Shape, length)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "The linetype shape name should be at least one character long.");
            this.name = name;
            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.style = style;
            this.offset = offset;
            this.rotationType = rotationType;
            this.rotation = MathHelper.NormalizeAngle(rotation);
            if (scale <= 0)
                throw new ArgumentOutOfRangeException(nameof(scale), scale, "The LinetypeShepeSegment scale must be greater than zero.");
            this.scale = scale;
        }

        #endregion

        #region public properties

        public string Name
        {
            get { return this.name; }
            internal set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value), "The linetype shape name should be at least one character long.");
                this.name = value;
            }
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

        public Vector2 Offset
        {
            get { return this.offset; }
            set { this.offset = value; }
        }

        public LinetypeSegmentRotationType RotationType
        {
            get { return this.rotationType; }
            set { this.rotationType = value; }
        }

        public double Rotation
        {
            get { return this.rotation; }
            set { this.rotation = MathHelper.NormalizeAngle(value); }
        }

        public double Scale
        {
            get { return this.scale; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The linetype shape segment scale must be greater than zero.");
                this.scale = value;
            }
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            return new LinetypeShapeSegment(this.name, (ShapeStyle) this.style.Clone(), this.Length)
            {
                Offset = this.offset,
                RotationType = this.rotationType,
                Rotation = this.rotation,
                Scale = this.scale
            };
        }

        #endregion
    }
}