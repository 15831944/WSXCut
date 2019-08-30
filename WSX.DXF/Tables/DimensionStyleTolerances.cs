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
    /// Represents the way tolerances are formated in dimension entities
    /// </summary>
    public class DimensionStyleTolerances :
        ICloneable
    {
        #region private fields

        private DimensionStyleTolerancesDisplayMethod dimtol;
        private double dimtp;
        private double dimtm;
        private DimensionStyleTolerancesVerticalPlacement dimtolj;
        private short dimtdec;
        private bool suppressLinearLeadingZeros;
        private bool suppressLinearTrailingZeros;
        private bool suppressZeroFeet;
        private bool suppressZeroInches;
        private short dimalttd;
        private bool altSuppressLinearLeadingZeros;
        private bool altSuppressLinearTrailingZeros;
        private bool altSuppressZeroFeet;
        private bool altSuppressZeroInches;

        #endregion

        #region constructors

        public DimensionStyleTolerances()
        {
            this.dimtol = DimensionStyleTolerancesDisplayMethod.None;
            this.dimtm = 0.0;
            this.dimtp = 0.0;
            this.dimtolj = DimensionStyleTolerancesVerticalPlacement.Middle;
            this.dimtdec = 4;
            this.suppressLinearLeadingZeros = false;
            this.suppressLinearTrailingZeros = false;
            this.suppressZeroFeet = true;
            this.suppressZeroInches = true;
            this.dimalttd = 2;
            this.altSuppressLinearLeadingZeros = false;
            this.altSuppressLinearTrailingZeros = false;
            this.altSuppressZeroFeet = true;
            this.altSuppressZeroInches = true;
        }

        #endregion

        #region public properties

        public DimensionStyleTolerancesDisplayMethod DisplayMethod
        {
            get { return this.dimtol; }
            set { this.dimtol = value; }
        }

        public double UpperLimit
        {
            get { return this.dimtp; }
            set { this.dimtp = value; }
        }

        public double LowerLimit
        {
            get { return this.dimtm; }
            set { this.dimtm = value; }
        }

        public DimensionStyleTolerancesVerticalPlacement VerticalPlacement
        {
            get { return this.dimtolj; }
            set { this.dimtolj = value; }
        }

        public short Precision
        {
            get { return this.dimtdec; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The tolerance precision must be equals or greater than zero.");
                this.dimtdec = value;
            }
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

        public short AlternatePrecision
        {
            get { return this.dimalttd; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The alternate precision must be equals or greater than zero.");
                this.dimalttd = value;
            }
        }

        public bool AlternateSuppressLinearLeadingZeros
        {
            get { return this.altSuppressLinearLeadingZeros; }
            set { this.altSuppressLinearLeadingZeros = value; }
        }

        public bool AlternateSuppressLinearTrailingZeros
        {
            get { return this.altSuppressLinearTrailingZeros; }
            set { this.altSuppressLinearTrailingZeros = value; }
        }

        public bool AlternateSuppressZeroFeet
        {
            get { return this.altSuppressZeroFeet; }
            set { this.altSuppressZeroFeet = value; }
        }

        public bool AlternateSuppressZeroInches
        {
            get { return this.altSuppressZeroInches; }
            set { this.altSuppressZeroInches = value; }
        }

        #endregion

        #region implements ICloneable

        public object Clone()
        {
            return new DimensionStyleTolerances
            {
                DisplayMethod = this.dimtol,
                UpperLimit = this.dimtp,
                LowerLimit = this.dimtm,
                VerticalPlacement = this.dimtolj,
                Precision = this.dimtdec,
                SuppressLinearLeadingZeros = this.suppressLinearLeadingZeros,
                SuppressLinearTrailingZeros = this.suppressLinearTrailingZeros,
                SuppressZeroFeet = this.suppressZeroFeet,
                SuppressZeroInches = this.suppressZeroInches,
                AlternatePrecision = this.dimalttd,
                AlternateSuppressLinearLeadingZeros = this.altSuppressLinearLeadingZeros,
                AlternateSuppressLinearTrailingZeros = this.altSuppressLinearTrailingZeros,
                AlternateSuppressZeroFeet = this.altSuppressZeroFeet,
                AlternateSuppressZeroInches = this.altSuppressZeroInches,
            };
        }

        #endregion
    }
}