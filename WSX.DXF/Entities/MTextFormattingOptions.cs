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
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Options for the <see cref="MText">multiline text</see> entity text formatting.
    /// </summary>
    public class MTextFormattingOptions
    {
        public enum TextAligment
        {
                        Bottom = 0,

                        Center = 1,

                        Top = 2,

                        Default = 3
        }

        #region private fields

        private bool bold;
        private bool italic;
        private bool overline;
        private bool underline;
        private bool strikeThrough;
        private AciColor color;
        private string fontName;
        private TextAligment aligment;
        private double heightFactor;
        private double obliqueAngle;
        private double characterSpaceFactor;
        private double widthFactor;
        private readonly TextStyle style;

        #endregion

        #region constructors

        public MTextFormattingOptions(TextStyle style)
        {
            this.bold = false;
            this.italic = false;
            this.overline = false;
            this.underline = false;
            this.color = null;
            this.fontName = null;
            this.aligment = TextAligment.Default;
            this.heightFactor = 1.0;
            this.obliqueAngle = 0.0;
            this.characterSpaceFactor = 1.0;
            this.widthFactor = 1.0;
            this.style = style;
        }

        #endregion

        #region public properties

        public bool Bold
        {
            get { return this.bold; }
            set { this.bold = value; }
        }

        public bool Italic
        {
            get { return this.italic; }
            set { this.italic = value; }
        }

        public bool Overline
        {
            get { return this.overline; }
            set { this.overline = value; }
        }

        public bool Underline
        {
            get { return this.underline; }
            set { this.underline = value; }
        }

        public bool StrikeThrough
        {
            get { return this.strikeThrough; }
            set { this.strikeThrough = value; }
        }

        public AciColor Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public string FontName
        {
            get { return this.fontName; }
            set { this.fontName = value; }
        }

        public TextAligment Aligment
        {
            get { return this.aligment; }
            set { this.aligment = value; }
        }

        public double HeightFactor
        {
            get { return this.heightFactor; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The character percentage height must be greater than zero.");
                this.heightFactor = value;
            }
        }

        public double ObliqueAngle
        {
            get { return this.obliqueAngle; }
            set
            {
                if (value < -85.0 || value > 85.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The oblique angle valid values range from -85 to 85.");
                this.obliqueAngle = value;
            }
        }

        ///  Gets or sets the space between characters as a multiple of the original spacing between characters.
        public double CharacterSpaceFactor
        {
            get { return this.characterSpaceFactor; }
            set
            {
                if (value < 0.75 || value > 4)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The character space valid values range from a minimum of .75 to 4");
                this.characterSpaceFactor = value;
            }
        }

        public double WidthFactor
        {
            get { return this.widthFactor; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The width factor should be greater than zero.");
                this.widthFactor = value;
            }
        }

        #endregion

        #region public methods

        public string FormatText(string text)
        {
            string formattedText = text;
            if (this.overline)
                formattedText = string.Format("\\O{0}\\o", formattedText);
            if (this.underline)
                formattedText = string.Format("\\L{0}\\l", formattedText);
            if (this.strikeThrough)
                formattedText = string.Format("\\K{0}\\k", formattedText);
            if (this.color != null)
            {
                formattedText = this.color.UseTrueColor ?
                    string.Format("\\C{0};\\c{1};{2}", this.color.Index, AciColor.ToTrueColor(this.color), formattedText) :
                    string.Format("\\C{0};{1}", this.color.Index, formattedText);
            }
            if (this.fontName != null)
            {
                if (this.bold && this.italic)
                    formattedText = string.Format("\\f{0}|b1|i1;{1}", this.fontName, formattedText);
                else if (this.bold && !this.italic)
                    formattedText = string.Format("\\f{0}|b1|i0;{1}", this.fontName, formattedText);
                else if (!this.bold && this.italic)
                    formattedText = string.Format("\\f{0}|i1|b0;{1}", this.fontName, formattedText);
                else
                    formattedText = string.Format("\\F{0};{1}", this.fontName, formattedText);
            }
            else
            {
                if (this.bold && this.italic)
                    formattedText = string.Format("\\f{0}|b1|i1;{1}", this.style.FontFamilyName, formattedText);
                if (this.bold && !this.italic)
                    formattedText = string.Format("\\f{0}|b1|i0;{1}", this.style.FontFamilyName, formattedText);
                if (!this.bold && this.italic)
                    formattedText = string.Format("\\f{0}|i1|b0;{1}", this.style.FontFamilyName, formattedText);
            }
            if (this.aligment != TextAligment.Default)
                formattedText = string.Format("\\A{0};{1}", (int) this.aligment, formattedText);
            if (!MathHelper.IsOne(this.heightFactor))
                formattedText = string.Format("\\H{0}x;{1}", this.heightFactor, formattedText);
            if (!MathHelper.IsZero(this.obliqueAngle))
                formattedText = string.Format("\\Q{0};{1}", this.obliqueAngle, formattedText);
            if (!MathHelper.IsOne(this.characterSpaceFactor))
                formattedText = string.Format("\\T{0};{1}", this.characterSpaceFactor, formattedText);
            if (!MathHelper.IsOne(this.widthFactor))
                formattedText = string.Format("\\W{0};{1}", this.widthFactor, formattedText);
            return "{" + formattedText + "}";
        }

        #endregion
    }
}