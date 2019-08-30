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

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents the hatch gradient pattern style.
    /// </summary>
    /// <remarks>
    /// Gradient patterns are only supported by AutoCad2004 and higher dxf versions. It will default to a solid pattern if saved as AutoCad2000.
    /// </remarks>
    public class HatchGradientPattern :
        HatchPattern
    {
        #region private fields

        private HatchGradientPatternType gradientType;
        private AciColor color1;
        private AciColor color2;
        private bool singleColor;
        private double tint;
        private bool centered;

        #endregion

        #region constructors

        public HatchGradientPattern()
            : this(string.Empty)
        {
        }

        public HatchGradientPattern(string description)
            : base("SOLID", description)
        {
            this.color1 = AciColor.Blue;
            this.color2 = AciColor.Yellow;
            this.singleColor = false;
            this.gradientType = HatchGradientPatternType.Linear;
            this.tint = 1.0;
            this.centered = true;
        }

        public HatchGradientPattern(AciColor color, double tint, HatchGradientPatternType type)
            : this(color, tint, type, string.Empty)
        {
        }

        public HatchGradientPattern(AciColor color, double tint, HatchGradientPatternType type, string description)
            : base("SOLID", description)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));
            this.color1 = color;
            this.color2 = this.Color2FromTint(tint);
            this.singleColor = true;
            this.gradientType = type;
            this.tint = tint;
            this.centered = true;
        }

        public HatchGradientPattern(AciColor color1, AciColor color2, HatchGradientPatternType type)
            : this(color1, color2, type, string.Empty)
        {
        }

        public HatchGradientPattern(AciColor color1, AciColor color2, HatchGradientPatternType type, string description)
            : base("SOLID", description)
        {
            if (color1 == null)
                throw new ArgumentNullException(nameof(color1));
            this.color1 = color1;
            if (color2 == null)
                throw new ArgumentNullException(nameof(color2));
            this.color2 = color2;
            this.singleColor = false;
            this.gradientType = type;
            this.tint = 1.0;
            this.centered = true;
        }

        #endregion

        #region public properties

        public HatchGradientPatternType GradientType
        {
            get { return this.gradientType; }
            set { this.gradientType = value; }
        }

        public AciColor Color1
        {
            get { return this.color1; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.color1 = value;
            }
        }

        public AciColor Color2
        {
            get { return this.color2; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.singleColor = false;
                this.color2 = value;
            }
        }

        public bool SingleColor
        {
            get { return this.singleColor; }
            set
            {
                if (value)
                    this.Color2 = this.Color2FromTint(this.tint);
                this.singleColor = value;
            }
        }

        public double Tint
        {
            get { return this.tint; }
            set
            {
                if (this.singleColor)
                    this.Color2 = this.Color2FromTint(value);
                this.tint = value;
            }
        }

        public bool Centered
        {
            get { return this.centered; }
            set { this.centered = value; }
        }

        #endregion

        #region private methods

        private AciColor Color2FromTint(double value)
        {
            double h, s, l;
            AciColor.ToHsl(this.color1, out h, out s, out l);
            return AciColor.FromHsl(h, s, value);
        }

        #endregion

        #region ICloneable

        public override object Clone()
        {
            HatchGradientPattern copy = new HatchGradientPattern
            {
                // Pattern
                Fill = this.Fill,
                Type = this.Type,
                Origin = this.Origin,
                Angle = this.Angle,
                Scale = this.Scale,
                Style = this.Style,
                // GraientPattern
                GradientType = this.gradientType,
                Color1 = (AciColor) this.color1.Clone(),
                Color2 = (AciColor) this.color2.Clone(),
                SingleColor = this.singleColor,
                Tint = this.tint,
                Centered = this.centered
            };

            return copy;
        }

        #endregion
    }
}