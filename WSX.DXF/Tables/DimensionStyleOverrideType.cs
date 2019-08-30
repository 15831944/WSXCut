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

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Dimension style override types.
    /// </summary>
    /// <remarks>
    /// There is one dimension style override type for each property of the <see cref="DimensionStyle">DimensionStyle</see> class.
    /// The dimension style properties DIMBLK and DIMSAH are not available.
    /// The overrides always make use of the DIMBLK1 and DIMBLK2 setting the DIMSAH to true even when both arrow ends are the same.
    /// </remarks>
    public enum DimensionStyleOverrideType
    {
        DimLineColor,

        DimLineLinetype,

        DimLineLineweight,

        DimLine1Off,

        DimLine2Off,

        DimLineExtend,

        ExtLineColor,

        ExtLine1Linetype,

        ExtLine2Linetype,

        ExtLineLineweight,

        ExtLine1Off,

        ExtLine2Off,

        ExtLineOffset,

        ExtLineExtend,

        ExtLineFixed,

        ExtLineFixedLength,

        DimArrow1,

        DimArrow2,

        LeaderArrow,

        ArrowSize,

        CenterMarkSize,

        TextStyle,

        TextColor,

        TextFillColor,

        TextHeight,

        TextVerticalPlacement,

        TextHorizontalPlacement,

        TextInsideAlign,

        TextOutsideAlign,

        TextOffset,

        TextDirection,

        TextFractionHeightScale,

        FitDimLineForce,

        FitDimLineInside,

        DimScaleOverall,

        FitOptions,

        FitTextInside,

        FitTextMove,

        AngularPrecision,

        LengthPrecision,

        DimPrefix,

        DimSuffix,

        DecimalSeparator,

        DimScaleLinear,

        DimLengthUnits,

        DimAngularUnits,

        FractionalType,

        SuppressLinearLeadingZeros,

        SuppressLinearTrailingZeros,

        SuppressAngularLeadingZeros,

        SuppressAngularTrailingZeros,

        SuppressZeroFeet,

        SuppressZeroInches,

        DimRoundoff,

        AltUnitsEnabled ,

        AltUnitsLengthUnits,

        AltUnitsStackedUnits,

        AltUnitsLengthPrecision,

        AltUnitsMultiplier,

        AltUnitsRoundoff,

        AltUnitsPrefix,

        AltUnitsSuffix,

        AltUnitsSuppressLinearLeadingZeros,

        AltUnitsSuppressLinearTrailingZeros,

        AltUnitsSuppressZeroFeet,

        AltUnitsSuppressZeroInches,

        TolerancesDisplayMethod,

        TolerancesUpperLimit,

        TolerancesLowerLimit,

        TolerancesVerticalPlacement,

        TolerancesPrecision,

        TolerancesSuppressLinearLeadingZeros,

        TolerancesSuppressLinearTrailingZeros,

        TolerancesSuppressZeroFeet,

        TolerancesSuppressZeroInches,
    
        TolerancesAlternatePrecision,

        TolerancesAltSuppressLinearLeadingZeros,

        TolerancesAltSuppressLinearTrailingZeros,

        TolerancesAltSuppressZeroFeet,

        TolerancesAltSuppressZeroInches
    }
}