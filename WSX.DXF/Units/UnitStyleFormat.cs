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

namespace WSX.DXF.Units
{
    /// <summary>
    /// Represents the parameters to convert linear and angular units to its string representation.
    /// </summary>
    public class UnitStyleFormat
    {
        #region private fields

        private short linearDecimalPlaces;
        private short angularDecimalPlaces;
        private string decimalSeparator;
        private string feetInchesSeparator;
        private string degreesSymbol;
        private string minutesSymbol;
        private string secondsSymbol;
        private string radiansSymbol;
        private string gradiansSymbol;
        private string feetSymbol;
        private string inchesSymbol;
        private double fractionHeigthScale;
        private FractionFormatType fractionType;
        private bool supressLinearLeadingZeros;
        private bool supressLinearTrailingZeros;
        private bool supressAngularLeadingZeros;
        private bool supressAngularTrailingZeros;
        private bool supressZeroFeet;
        private bool supressZeroInches;

        #endregion

        #region constructors

        public UnitStyleFormat()
        {
            this.linearDecimalPlaces = 2;
            this.angularDecimalPlaces = 0;
            this.decimalSeparator = ".";
            this.feetInchesSeparator = "-";
            this.degreesSymbol = "°";
            this.minutesSymbol = "\'";
            this.secondsSymbol = "\"";
            this.radiansSymbol = "r";
            this.gradiansSymbol = "g";
            this.feetSymbol = "\'";
            this.inchesSymbol = "\"";
            this.fractionHeigthScale = 1.0;
            this.fractionType = FractionFormatType.Horizontal;
            this.supressLinearLeadingZeros = false;
            this.supressLinearTrailingZeros = false;
            this.supressAngularLeadingZeros = false;
            this.supressAngularTrailingZeros = false;
            this.supressZeroFeet = true;
            this.supressZeroInches = true;
        }

        #endregion

        #region public properties

        public short LinearDecimalPlaces
        {
            get { return this.linearDecimalPlaces; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The number of decimal places must be equals or greater than zero.");
                this.linearDecimalPlaces = value;
            }
        }

        public short AngularDecimalPlaces
        {
            get { return this.angularDecimalPlaces; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The number of decimal places must be equals or greater than zero.");
                this.angularDecimalPlaces = value;
            }
        }

        public string DecimalSeparator
        {
            get { return this.decimalSeparator; }
            set { this.decimalSeparator = value; }
        }

        public string FeetInchesSeparator
        {
            get { return this.feetInchesSeparator; }
            set { this.feetInchesSeparator = value; }
        }

        public string DegreesSymbol
        {
            get { return this.degreesSymbol; }
            set { this.degreesSymbol = value; }
        }

        public string MinutesSymbol
        {
            get { return this.minutesSymbol; }
            set { this.minutesSymbol = value; }
        }

        public string SecondsSymbol
        {
            get { return this.secondsSymbol; }
            set { this.secondsSymbol = value; }
        }

        public string RadiansSymbol
        {
            get { return this.radiansSymbol; }
            set { this.radiansSymbol = value; }
        }

        public string GradiansSymbol
        {
            get { return this.gradiansSymbol; }
            set { this.gradiansSymbol = value; }
        }

        public string FeetSymbol
        {
            get { return this.feetSymbol; }
            set { this.feetSymbol = value; }
        }

        public string InchesSymbol
        {
            get { return this.inchesSymbol; }
            set { this.inchesSymbol = value; }
        }

        public double FractionHeightScale
        {
            get { return this.fractionHeigthScale; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The fraction height scale must be greater than zero.");
                this.fractionHeigthScale = value;
            }
        }

        public FractionFormatType FractionType
        {
            get { return this.fractionType; }
            set { this.fractionType = value; }
        }

        public bool SupressLinearLeadingZeros
        {
            get { return this.supressLinearLeadingZeros; }
            set { this.supressLinearLeadingZeros = value; }
        }

        public bool SupressLinearTrailingZeros
        {
            get { return this.supressLinearTrailingZeros; }
            set { this.supressLinearTrailingZeros = value; }
        }

        public bool SupressAngularLeadingZeros
        {
            get { return this.supressAngularLeadingZeros; }
            set { this.supressAngularLeadingZeros = value; }
        }

        public bool SupressAngularTrailingZeros
        {
            get { return this.supressAngularTrailingZeros; }
            set { this.supressAngularTrailingZeros = value; }
        }

        public bool SupressZeroFeet
        {
            get { return this.supressZeroFeet; }
            set { this.supressZeroFeet = value; }
        }

        public bool SupressZeroInches
        {
            get { return this.supressZeroInches; }
            set { this.supressZeroInches = value; }
        }

        #endregion
    }
}