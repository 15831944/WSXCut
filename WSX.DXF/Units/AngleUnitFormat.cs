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
using System.Globalization;

namespace WSX.DXF.Units
{
    /// <summary>
    /// Utility methods to format a decimal angle in degrees to its different string representations.
    /// </summary>
    public static class AngleUnitFormat
    {
        #region public methods

        public static string ToDecimal(double angle, UnitStyleFormat format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            NumberFormatInfo numberFormat = new NumberFormatInfo
            {
                NumberDecimalSeparator = format.DecimalSeparator
            };

            return angle.ToString(DecimalNumberFormat(format), numberFormat) + format.DegreesSymbol;
        }

        public static string ToDegreesMinutesSeconds(double angle, UnitStyleFormat format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            double degrees = angle;
            double minutes = (degrees - (int) degrees)*60;
            double seconds = (minutes - (int) minutes)*60;

            NumberFormatInfo numberFormat = new NumberFormatInfo
            {
                NumberDecimalSeparator = format.DecimalSeparator
            };

            if (format.AngularDecimalPlaces == 0)
                return string.Format(numberFormat, "{0}" + format.DegreesSymbol, (int) Math.Round(degrees, 0));
            if (format.AngularDecimalPlaces == 1 || format.AngularDecimalPlaces == 2)
                return string.Format(numberFormat, "{0}" + format.DegreesSymbol + "{1}" + format.MinutesSymbol, (int) degrees, (int) Math.Round(minutes, 0));
            if (format.AngularDecimalPlaces == 3 || format.AngularDecimalPlaces == 4)
                return string.Format(numberFormat, "{0}" + format.DegreesSymbol + "{1}" + format.MinutesSymbol + "{2}" + format.SecondsSymbol, (int) degrees, (int) minutes, (int) Math.Round(seconds, 0));
            // the suppression of leading or trailing zeros is not applicable to DegreesMinutesSeconds angles format
            string f = "0." + new string('0', format.AngularDecimalPlaces - 4);
            return string.Format(numberFormat, "{0}" + format.DegreesSymbol + "{1}" + format.MinutesSymbol + "{2}" + format.SecondsSymbol, (int) degrees, (int) minutes, seconds.ToString(f, numberFormat));
        }

        public static string ToGradians(double angle, UnitStyleFormat format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            NumberFormatInfo numberFormat = new NumberFormatInfo
            {
                NumberDecimalSeparator = format.DecimalSeparator
            };

            return (angle*MathHelper.DegToGrad).ToString(DecimalNumberFormat(format), numberFormat) + format.GradiansSymbol;
        }

        public static string ToRadians(double angle, UnitStyleFormat format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            NumberFormatInfo numberFormat = new NumberFormatInfo
            {
                NumberDecimalSeparator = format.DecimalSeparator
            };
            return (angle*MathHelper.DegToRad).ToString(DecimalNumberFormat(format), numberFormat) + format.RadiansSymbol;
        }

        #endregion

        #region private methods

        private static string DecimalNumberFormat(UnitStyleFormat format)
        {
            char[] zeroes = new char[format.AngularDecimalPlaces + 2];
            if (format.SupressAngularLeadingZeros)
                zeroes[0] = '#';
            else
                zeroes[0] = '0';

            zeroes[1] = '.';

            for (int i = 2; i < zeroes.Length; i++)
            {
                if (format.SupressAngularTrailingZeros)
                    zeroes[i] = '#';
                else
                    zeroes[i] = '0';
            }
            return new string(zeroes);
        }

        #endregion
    }
}