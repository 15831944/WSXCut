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
using WSX.DXF.Units;

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Represents the way alternate units are formatted in dimension entities.
    /// </summary>
    /// <remarks>Alternative units are not applicable for angular dimensions.</remarks>
    public class DimensionStyleAlternateUnits :
        ICloneable
    {
        #region private fields

        private bool dimalt;
        private LinearUnitType dimaltu;
        private bool stackedUnits;
        private short dimaltd;
        private double dimaltf;
        private double dimaltrnd;
        private string dimPrefix;
        private string dimSuffix;
        private bool suppressLinearLeadingZeros;
        private bool suppressLinearTrailingZeros;
        private bool suppressZeroFeet;
        private bool suppressZeroInches;

        #endregion

        #region constructors

        public DimensionStyleAlternateUnits()
        {
            this.dimalt = false;
            this.dimaltd = 2;
            this.dimPrefix = string.Empty;
            this.dimSuffix = string.Empty;
            this.dimaltf = 25.4;
            this.dimaltu = LinearUnitType.Decimal;
            this.stackedUnits = false;
            this.suppressLinearLeadingZeros = false;
            this.suppressLinearTrailingZeros = false;
            this.suppressZeroFeet = true;
            this.suppressZeroInches = true;
            this.dimaltrnd = 0.0;
        }

        #endregion

        #region public properties

        public bool Enabled
        {
            get { return this.dimalt; }
            set { this.dimalt = value; }
        }

        public short LengthPrecision
        {
            get { return this.dimaltd; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The length precision must be equals or greater than zero.");
                this.dimaltd = value;
            }
        }

        public string Prefix
        {
            get { return this.dimPrefix; }
            set { this.dimPrefix = value ?? string.Empty; }
        }

        public string Suffix
        {
            get { return this.dimSuffix; }
            set { this.dimSuffix = value ?? string.Empty; }
        }

        public double Multiplier
        {
            get { return this.dimaltf; }
            set
            {
                if (value <= 0.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The multiplier for alternate units must be greater than zero0.");
                this.dimaltf = value;
            }
        }

        public LinearUnitType LengthUnits
        {
            get { return this.dimaltu; }
            set { this.dimaltu = value; }
        }

        public bool StackUnits
        {
            get { return this.stackedUnits; }
            set { this.stackedUnits = value; }
        }

        public bool SuppressLinearLeadingZeros
        {
            get { return this.suppressLinearLeadingZeros; }
            set { this.suppressLinearLeadingZeros = value; }
        }

        public bool SuppressLinearTrailingZeros
        {
            get { return this.suppressLinearTrailingZeros; }
            set { this.suppressLinearTrailingZeros = value; }
        }

        public bool SuppressZeroFeet
        {
            get { return this.suppressZeroFeet; }
            set { this.suppressZeroFeet = value; }
        }

        public bool SuppressZeroInches
        {
            get { return this.suppressZeroInches; }
            set { this.suppressZeroInches = value; }
        }

        public double Roundoff
        {
            get { return this.dimaltrnd; }
            set
            {
                if (value < 0.000001 && !MathHelper.IsZero(value, double.Epsilon)) // ToDo check range of values
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The nearest value to round all distances must be equal or greater than 0.000001 or zero (no rounding off).");
                this.dimaltrnd = value;
            }
        }

        #endregion

        #region implements ICloneable

        public object Clone()
        {
            DimensionStyleAlternateUnits copy = new DimensionStyleAlternateUnits()
            {
                Enabled = this.dimalt,
                LengthUnits = this.dimaltu,
                StackUnits = this.stackedUnits,
                LengthPrecision = this.dimaltd,
                Multiplier = this.dimaltf,
                Roundoff = this.dimaltrnd,
                Prefix = this.dimPrefix,
                Suffix = this.dimSuffix,
                SuppressLinearLeadingZeros = this.suppressLinearLeadingZeros,
                SuppressLinearTrailingZeros = this.suppressLinearTrailingZeros,
                SuppressZeroFeet = this.suppressZeroFeet,
                SuppressZeroInches = this.suppressZeroInches
            };

            return copy;
        }

        #endregion
    }
}