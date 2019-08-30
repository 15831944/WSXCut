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
using WSX.DXF.Blocks;
using WSX.DXF.Collections;
using WSX.DXF.Units;

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Represents a dimension style.
    /// </summary>
    public class DimensionStyle :
        TableObject
    {
        #region delegates and events

        public delegate void LinetypeChangedEventHandler(TableObject sender, TableObjectChangedEventArgs<Linetype> e);

        public event LinetypeChangedEventHandler LinetypeChanged;

        protected virtual Linetype OnLinetypeChangedEvent(Linetype oldLinetype, Linetype newLinetype)
        {
            LinetypeChangedEventHandler ae = this.LinetypeChanged;
            if (ae != null)
            {
                TableObjectChangedEventArgs<Linetype> eventArgs = new TableObjectChangedEventArgs<Linetype>(oldLinetype, newLinetype);
                ae(this, eventArgs);
                return eventArgs.NewValue;
            }
            return newLinetype;
        }

        public delegate void TextStyleChangedEventHandler(TableObject sender, TableObjectChangedEventArgs<TextStyle> e);

        public event TextStyleChangedEventHandler TextStyleChanged;

        protected virtual TextStyle OnTextStyleChangedEvent(TextStyle oldTextStyle, TextStyle newTextStyle)
        {
            TextStyleChangedEventHandler ae = this.TextStyleChanged;
            if (ae != null)
            {
                TableObjectChangedEventArgs<TextStyle> eventArgs = new TableObjectChangedEventArgs<TextStyle>(oldTextStyle, newTextStyle);
                ae(this, eventArgs);
                return eventArgs.NewValue;
            }
            return newTextStyle;
        }

        public delegate void BlockChangedEventHandler(TableObject sender, TableObjectChangedEventArgs<Block> e);

        public event BlockChangedEventHandler BlockChanged;

        protected virtual Block OnBlockChangedEvent(Block oldBlock, Block newBlock)
        {
            BlockChangedEventHandler ae = this.BlockChanged;
            if (ae != null)
            {
                TableObjectChangedEventArgs<Block> eventArgs = new TableObjectChangedEventArgs<Block>(oldBlock, newBlock);
                ae(this, eventArgs);
                return eventArgs.NewValue;
            }
            return newBlock;
        }

        #endregion

        #region private fields

        // dimension and extension lines
        private AciColor dimclrd;
        private Linetype dimltype;
        private Lineweight dimlwd;
        private bool dimsd1;
        private bool dimsd2;
        private double dimdle;
        private double dimdli;

        private AciColor dimclre;
        private Linetype dimltex1;
        private Linetype dimltex2;
        private Lineweight dimlwe;
        private bool dimse1;
        private bool dimse2;
        private double dimexo;
        private double dimexe;
        private bool dimfxlon;
        private double dimfxl;

        // symbols and arrows
        private double dimasz;
        private double dimcen;
        private Block dimldrblk;
        private Block dimblk1;
        private Block dimblk2;

        // text
        private TextStyle dimtxsty;
        private AciColor dimclrt;
        private AciColor dimtfillclr;
        private double dimtxt;
        private DimensionStyleTextHorizontalPlacement dimjust;
        private DimensionStyleTextVerticalPlacement dimtad;
        private double dimgap;
        private bool dimtih;
        private bool dimtoh;
        private DimensionStyleTextDirection dimtxtdirection;
        private double dimtfac;

        // fit
        private bool dimtofl;
        private bool dimsoxd;
        private double dimscale;
        private DimensionStyleFitOptions dimatfit;
        private bool dimtix;
        private DimensionStyleFitTextMove dimtmove;

        // primary units
        private short dimadec;
        private short dimdec;
        private string dimPrefix;
        private string dimSuffix;
        private char dimdsep;
        private double dimlfac;
        private LinearUnitType dimlunit;
        private AngleUnitType dimaunit;
        private FractionFormatType dimfrac;
        private bool suppressLinearLeadingZeros;
        private bool suppressLinearTrailingZeros;
        private bool suppressZeroFeet;
        private bool suppressZeroInches;
        private bool suppressAngularLeadingZeros;
        private bool suppressAngularTrailingZeros;
        private double dimrnd;

        // alternate units
        private DimensionStyleAlternateUnits alternateUnits;

        // tolerances
        private DimensionStyleTolerances tolerances;

        #endregion

        #region constants

        public const string DefaultName = "Standard";

        public static DimensionStyle Default
        {
            get { return new DimensionStyle(DefaultName); }
        }

        public static DimensionStyle Iso25
        {
            get
            {
                DimensionStyle style = new DimensionStyle("ISO-25")
                {
                    DimBaselineSpacing = 3.75,
                    ExtLineExtend = 1.25,
                    ExtLineOffset = 0.625,
                    ArrowSize = 2.5,
                    CenterMarkSize = 2.5,
                    TextHeight = 2.5,
                    TextOffset = 0.625,
                    TextOutsideAlign = true,
                    TextInsideAlign = true,
                    TextVerticalPlacement = DimensionStyleTextVerticalPlacement.Above,
                    FitDimLineForce = true,
                    DecimalSeparator = ',',
                    LengthPrecision = 2,
                    SuppressLinearTrailingZeros = true,
                    AlternateUnits =
                    {
                        LengthPrecision = 3,
                        Multiplier = 0.0394
                    },
                    Tolerances =
                    {
                        VerticalPlacement = DimensionStyleTolerancesVerticalPlacement.Bottom,
                        Precision = 2,
                        SuppressLinearTrailingZeros = true,
                        AlternatePrecision = 3
                    }
                };
                return style;
            }
        }

        #endregion

        #region constructors

        public DimensionStyle(string name)
            : this(name, true)
        {
        }

        internal DimensionStyle(string name, bool checkName)
            : base(name, DxfObjectCode.DimStyle, checkName)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "The dimension style name should be at least one character long.");

            this.IsReserved = name.Equals(DefaultName, StringComparison.OrdinalIgnoreCase);

            // dimension and extension lines
            this.dimclrd = AciColor.ByBlock;
            this.dimltype = Linetype.ByBlock;
            this.dimlwd = Lineweight.ByBlock;
            this.dimdle = 0.0;
            this.dimdli = 0.38;
            this.dimsd1 = false;
            this.dimsd2 = false;

            this.dimclre = AciColor.ByBlock;
            this.dimltex1 = Linetype.ByBlock;
            this.dimltex2 = Linetype.ByBlock;
            this.dimlwe = Lineweight.ByBlock;
            this.dimse1 = false;
            this.dimse2 = false;
            this.dimexo = 0.0625;
            this.dimexe = 0.18;
            this.dimfxlon = false;
            this.dimfxl = 1.0;

            // symbols and arrows
            this.dimldrblk = null;
            this.dimblk1 = null;
            this.dimblk2 = null;
            this.dimasz = 0.18;
            this.dimcen = 0.09;

            // text
            this.dimtxsty = TextStyle.Default;
            this.dimclrt = AciColor.ByBlock;
            this.dimtfillclr = null;
            this.dimtxt = 0.18;
            this.dimtad = DimensionStyleTextVerticalPlacement.Centered;
            this.dimjust = DimensionStyleTextHorizontalPlacement.Centered;
            this.dimgap = 0.09;
            this.dimtih = false;
            this.dimtoh = false;
            this.dimtxtdirection = DimensionStyleTextDirection.LeftToRight;
            this.dimtfac = 1.0;

            // fit
            this.dimtofl = false;
            this.dimsoxd = true;
            this.dimscale = 1.0;
            this.dimatfit = DimensionStyleFitOptions.BestFit;
            this.dimtix = false;
            this.dimtmove = DimensionStyleFitTextMove.BesideDimLine;

            // primary units
            this.dimdec = 4;
            this.dimadec = 0;
            this.dimPrefix = string.Empty;
            this.dimSuffix = string.Empty;
            this.dimdsep = '.';
            this.dimlfac = 1.0;
            this.dimaunit = AngleUnitType.DecimalDegrees;
            this.dimlunit = LinearUnitType.Decimal;
            this.dimfrac = FractionFormatType.Horizontal;
            this.suppressLinearLeadingZeros = false;
            this.suppressLinearTrailingZeros = false;
            this.suppressZeroFeet = true;
            this.suppressZeroInches = true;
            this.suppressAngularLeadingZeros = false;
            this.suppressAngularTrailingZeros = false;
            this.dimrnd = 0.0;

            // alternate units
            this.alternateUnits = new DimensionStyleAlternateUnits();

            // tolerances
            this.tolerances = new DimensionStyleTolerances();
        }

        #endregion

        #region public properties

        #region dimension and extension lines

        public AciColor DimLineColor
        {
            get { return this.dimclrd; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.dimclrd = value;
            }
        }

        public Linetype DimLineLinetype
        {
            get { return this.dimltype; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.dimltype = this.OnLinetypeChangedEvent(this.dimltype, value);
            }
        }

        public Lineweight DimLineLineweight
        {
            get { return this.dimlwd; }
            set { this.dimlwd = value; }
        }

        public bool DimLine1Off
        {
            get { return this.dimsd1; }
            set { this.dimsd1 = value; }
        }

        public bool DimLine2Off
        {
            get { return this.dimsd2; }
            set { this.dimsd2 = value; }
        }

        public double DimLineExtend
        {
            get { return this.dimdle; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The DimLineExtend must be equals or greater than zero.");
                this.dimdle = value;
            }
        }

        public double DimBaselineSpacing
        {
            get { return this.dimdli; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The DimBaselineSpacing must be equals or greater than zero.");
                this.dimdli = value;
            }
        }

        public AciColor ExtLineColor
        {
            get { return this.dimclre; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.dimclre = value;
            }
        }

        public Linetype ExtLine1Linetype
        {
            get { return this.dimltex1; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.dimltex1 = this.OnLinetypeChangedEvent(this.dimltex1, value);
            }
        }

        public Linetype ExtLine2Linetype
        {
            get { return this.dimltex2; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.dimltex2 = this.OnLinetypeChangedEvent(this.dimltex2, value);
            }
        }

        public Lineweight ExtLineLineweight
        {
            get { return this.dimlwe; }
            set { this.dimlwe = value; }
        }

        public bool ExtLine1Off
        {
            get { return this.dimse1; }
            set { this.dimse1 = value; }
        }

        public bool ExtLine2Off
        {
            get { return this.dimse2; }
            set { this.dimse2 = value; }
        }

        public double ExtLineOffset
        {
            get { return this.dimexo; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The ExtLineOffset must be equals or greater than zero.");
                this.dimexo = value;
            }
        }

        public double ExtLineExtend
        {
            get { return this.dimexe; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The ExtLineExtend must be equals or greater than zero.");
                this.dimexe = value;
            }
        }

        public bool ExtLineFixed
        {
            get { return this.dimfxlon; }
            set { this.dimfxlon = value; }
        }

        public double ExtLineFixedLength
        {
            get { return this.dimfxl; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The ExtLineFixedLength must be equals or greater than zero.");
                this.dimfxl = value;
            }
        }

        #endregion

        #region symbols and arrows

        public Block DimArrow1
        {
            get { return this.dimblk1; }
            set { this.dimblk1 = value == null ? null : this.OnBlockChangedEvent(this.dimblk1, value); }
        }

        public Block DimArrow2
        {
            get { return this.dimblk2; }
            set { this.dimblk2 = value == null ? null : this.OnBlockChangedEvent(this.dimblk2, value); }
        }

        public Block LeaderArrow
        {
            get { return this.dimldrblk; }
            set { this.dimldrblk = value == null ? null : this.OnBlockChangedEvent(this.dimldrblk, value); }
        }

        public double ArrowSize
        {
            get { return this.dimasz; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The ArrowSize must be equals or greater than zero.");
                this.dimasz = value;
            }
        }

        /// 0 - No center marks or lines are drawn.<br />
        public double CenterMarkSize
        {
            get { return this.dimcen; }
            set { this.dimcen = value; }
        }

        #endregion

        #region text appearance

        public TextStyle TextStyle
        {
            get { return this.dimtxsty; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.dimtxsty = this.OnTextStyleChangedEvent(this.dimtxsty, value);
            }
        }

        public AciColor TextColor
        {
            get { return this.dimclrt; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.dimclrt = value;
            }
        }

        public AciColor TextFillColor
        {
            get { return this.dimtfillclr; }
            set { this.dimtfillclr = value; }
        }

        public double TextHeight
        {
            get { return this.dimtxt; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The TextHeight must be greater than zero.");
                this.dimtxt = value;
            }
        }

        public DimensionStyleTextHorizontalPlacement TextHorizontalPlacement
        {
            get { return this.dimjust; }
            set { this.dimjust = value; }
        }

        public DimensionStyleTextVerticalPlacement TextVerticalPlacement
        {
            get { return this.dimtad; }
            set { this.dimtad = value; }
        }

        public double TextOffset
        {
            get { return this.dimgap; }
            set { this.dimgap = value; }
        }

        public bool TextInsideAlign
        {
            get { return this.dimtih; }
            set { this.dimtih = value; }
        }

        public bool TextOutsideAlign
        {
            get { return this.dimtoh; }
            set { this.dimtoh = value; }
        }

        public DimensionStyleTextDirection TextDirection
        {
            get { return this.dimtxtdirection; }
            set { this.dimtxtdirection = value; }
        }

        public double TextFractionHeightScale
        {
            get { return this.dimtfac; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The TextFractionHeightScale must be greater than zero.");
                this.dimtfac = value;
            }
        }

        #endregion

        #region fit

        public bool FitDimLineForce
        {
            get { return this.dimtofl; }
            set { this.dimtofl = value; }
        }

        public bool FitDimLineInside
        {
            get { return this.dimsoxd; }
            set { this.dimsoxd = value; }
        }

        public double DimScaleOverall
        {
            get { return this.dimscale; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The DimScaleOverall must be greater than zero.");
                this.dimscale = value;
            }
        }

        public DimensionStyleFitOptions FitOptions
        {
            get { return this.dimatfit; }
            set { this.dimatfit = value; }
        }

        public bool FitTextInside
        {
            get { return this.dimtix; }
            set { this.dimtix = value; }
        }

        public DimensionStyleFitTextMove FitTextMove
        {
            get { return this.dimtmove; }
            set { this.dimtmove = value; }
        }

        #endregion

        #region primary units

        public short AngularPrecision
        {
            get { return this.dimadec; }
            set
            {
                if (value < -1)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The AngularPrecision must be greater than -1.");
                this.dimadec = value;
            }
        }

        public short LengthPrecision
        {
            get { return this.dimdec; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The LengthPrecision must be equals or greater than zero.");
                this.dimdec = value;
            }
        }

        public string DimPrefix
        {
            get { return this.dimPrefix; }
            set { this.dimPrefix = value ?? string.Empty; }
        }

        public string DimSuffix
        {
            get { return this.dimSuffix; }
            set { this.dimSuffix = value ?? string.Empty; }
        }

        public char DecimalSeparator
        {
            get { return this.dimdsep; }
            set { this.dimdsep = value; }
        }

        public double DimScaleLinear
        {
            get { return this.dimlfac; }
            set
            {
                if (MathHelper.IsZero(value))
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The scale factor cannot be zero.");
                this.dimlfac = value;
            }
        }

        public LinearUnitType DimLengthUnits
        {
            get { return this.dimlunit; }
            set { this.dimlunit = value; }
        }

        public AngleUnitType DimAngularUnits
        {
            get { return this.dimaunit; }
            set
            {
                if (value == AngleUnitType.SurveyorUnits)
                    throw new ArgumentException("Surveyor's units are not applicable in angular dimensions.");
                this.dimaunit = value;
            }
        }

        public FractionFormatType FractionType
        {
            get { return this.dimfrac; }
            set { this.dimfrac = value; }
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

        public bool SuppressAngularLeadingZeros
        {
            get { return this.suppressAngularLeadingZeros; }
            set { this.suppressAngularLeadingZeros = value; }
        }

        public bool SuppressAngularTrailingZeros
        {
            get { return this.suppressAngularTrailingZeros; }
            set { this.suppressAngularTrailingZeros = value; }
        }

        public double DimRoundoff
        {
            get { return this.dimrnd; }
            set
            {
                if (value < 0.000001 && !MathHelper.IsZero(value, double.Epsilon))
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The nearest value to round all distances must be equal or greater than 0.000001 or zero (no rounding off).");
                this.dimrnd = value;
            }
        }

        #endregion

        #region alternate units

        public DimensionStyleAlternateUnits AlternateUnits
        {
            get { return this.alternateUnits; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.alternateUnits = value;
            }
        }

        #endregion

        #region tolerances

        public DimensionStyleTolerances Tolerances
        {
            get { return this.tolerances; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.tolerances = value;
            }
        }

        #endregion

        public new DimensionStyles Owner
        {
            get { return (DimensionStyles) base.Owner; }
            internal set { base.Owner = value; }
        }

        #endregion

        #region overrides

        public override TableObject Clone(string newName)
        {
            DimensionStyle copy = new DimensionStyle(newName)
            {
                // dimension lines
                DimLineColor = (AciColor) this.dimclrd.Clone(),
                DimLineLinetype = (Linetype) this.dimltype.Clone(),
                DimLineLineweight = this.dimlwd,
                DimLine1Off = this.dimsd1,
                DimLine2Off = this.dimsd2,
                DimBaselineSpacing = this.dimdli,
                DimLineExtend = this.dimdle,

                // extension lines
                ExtLineColor = (AciColor) this.dimclre.Clone(),
                ExtLine1Linetype = (Linetype) this.dimltex1.Clone(),
                ExtLine2Linetype = (Linetype) this.dimltex2.Clone(),
                ExtLineLineweight = this.dimlwe,
                ExtLine1Off = this.dimse1,
                ExtLine2Off = this.dimse2,
                ExtLineOffset = this.dimexo,
                ExtLineExtend = this.dimexe,

                // symbols and arrows
                ArrowSize = this.dimasz,
                CenterMarkSize = this.dimcen,
                LeaderArrow  = (Block) this.dimldrblk?.Clone(),
                DimArrow1 = (Block) this.dimblk1?.Clone(),
                DimArrow2 = (Block) this.dimblk2?.Clone(),

                // text appearance
                TextStyle = (TextStyle) this.dimtxsty.Clone(),
                TextColor = (AciColor) this.dimclrt.Clone(),
                TextFillColor = (AciColor) this.dimtfillclr?.Clone(),
                TextHeight = this.dimtxt,
                TextHorizontalPlacement = this.dimjust,
                TextVerticalPlacement = this.dimtad,
                TextOffset = this.dimgap,
                TextFractionHeightScale = this.dimtfac,

                // fit
                FitDimLineForce = this.dimtofl,
                FitDimLineInside = this.dimsoxd,
                DimScaleOverall = this.dimscale,
                FitOptions = this.dimatfit,
                FitTextInside = this.dimtix,
                FitTextMove = this.dimtmove,

                // primary units
                AngularPrecision = this.dimadec,
                LengthPrecision = this.dimdec,
                DimPrefix = this.dimPrefix,
                DimSuffix = this.dimSuffix,
                DecimalSeparator = this.dimdsep,
                DimScaleLinear = this.dimlfac,
                DimLengthUnits = this.dimlunit,
                DimAngularUnits = this.dimaunit,
                FractionType = this.dimfrac,
                SuppressLinearLeadingZeros = this.suppressLinearLeadingZeros,
                SuppressLinearTrailingZeros = this.suppressLinearTrailingZeros,
                SuppressZeroFeet = this.suppressZeroFeet,
                SuppressZeroInches = this.suppressZeroInches,
                SuppressAngularLeadingZeros = this.suppressAngularLeadingZeros,
                SuppressAngularTrailingZeros = this.suppressAngularTrailingZeros,
                DimRoundoff = this.dimrnd,

                // alternate units
                AlternateUnits = (DimensionStyleAlternateUnits) this.alternateUnits.Clone(),

                // tolerances
                Tolerances = (DimensionStyleTolerances) this.tolerances.Clone()
            };

            foreach (XData data in this.XData.Values)
                copy.XData.Add((XData)data.Clone());

            return copy;
        }

        public override object Clone()
        {
            return this.Clone(this.Name);
        }

        #endregion
    }
}