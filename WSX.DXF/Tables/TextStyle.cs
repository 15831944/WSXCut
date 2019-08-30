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
using System.IO;
using WSX.DXF.Collections;
using System.Drawing.Text;

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Represents a text style.
    /// </summary>
    public class TextStyle :
        TableObject
    {
        #region private fields

        private string file;
        private string bigFont;
        private double height;
        private bool isBackward;
        private bool isUpsideDown;
        private bool isVertical;
        private double obliqueAngle;
        private double widthFactor;
        private FontStyle fontStyle;
        private string fontFamilyName;

        #endregion

        #region constants

        public const string DefaultName = "Standard";

        public static TextStyle Default
        {
            get { return new TextStyle(DefaultName, "simplex.shx"); }
        }

        #endregion

        #region constructors

        public TextStyle(string font)
            : this(Path.GetFileNameWithoutExtension(font), font)
        {
        }

        public TextStyle(string name, string font)
            : this(name, font, true)
        {
        }

        internal TextStyle(string name, string font, bool checkName)
            : base(name, DxfObjectCode.TextStyle, checkName)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "The text style name should be at least one character long.");
            this.IsReserved = name.Equals(DefaultName, StringComparison.OrdinalIgnoreCase);

            if (string.IsNullOrEmpty(font))
                throw new ArgumentNullException(nameof(font));

            if (!Path.GetExtension(font).Equals(".TTF", StringComparison.InvariantCultureIgnoreCase) &&
                !Path.GetExtension(font).Equals(".SHX", StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException("Only true type TTF fonts and acad compiled shape SHX fonts are allowed.");

            this.file = font;
            this.bigFont = string.Empty;
            this.widthFactor = 1.0;
            this.obliqueAngle = 0.0;
            this.height = 0.0;
            this.isVertical = false;
            this.isBackward = false;
            this.isUpsideDown = false;
            this.fontFamilyName = TrueTypeFontFamilyName(font);
            this.fontStyle = FontStyle.Regular;
        }

        public TextStyle(string fontFamily, FontStyle fontStyle)
            : this(fontFamily, fontFamily, fontStyle, true)
        {
        }

        public TextStyle(string name, string fontFamily, FontStyle fontStyle)
            : this(name, fontFamily, fontStyle, true)
        {
        }

        internal TextStyle(string name, string fontFamily, FontStyle fontStyle, bool checkName)
            : base(name, DxfObjectCode.TextStyle, checkName)
        {
            this.file = string.Empty;
            this.bigFont = string.Empty;
            this.widthFactor = 1.0;
            this.obliqueAngle = 0.0;
            this.height = 0.0;
            this.isVertical = false;
            this.isBackward = false;
            this.isUpsideDown = false;
            if(string.IsNullOrEmpty(fontFamily))
                throw new ArgumentNullException(nameof(fontFamily));
            this.fontFamilyName = fontFamily;
            this.fontStyle = fontStyle;
        }

        #endregion

        #region public properties

        public string FontFile
        {
            get { return this.file; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value));

                if (!Path.GetExtension(value).Equals(".TTF", StringComparison.InvariantCultureIgnoreCase) &&
                    !Path.GetExtension(value).Equals(".SHX", StringComparison.InvariantCultureIgnoreCase))
                    throw new ArgumentException("Only true type TTF fonts and acad compiled shape SHX fonts are allowed.");

                this.fontFamilyName = TrueTypeFontFamilyName(value);
                this.bigFont = string.Empty;
                this.file = value;
            }
        }

        public string BigFont
        {
            get { return this.bigFont; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    this.bigFont = string.Empty;
                else
                {
                    if (string.IsNullOrEmpty(this.file))
                        throw new NullReferenceException("The Big Font is only applicable for SHX Asian fonts.");
                    if (!Path.GetExtension(this.file).Equals(".SHX", StringComparison.InvariantCultureIgnoreCase))
                        throw new NullReferenceException("The Big Font is only applicable for SHX Asian fonts.");
                    if(!Path.GetExtension(value).Equals(".SHX", StringComparison.InvariantCultureIgnoreCase))
                        throw new ArgumentException("The Big Font is only applicable for SHX Asian fonts.", nameof(value));
                    this.bigFont = value;
                }               
            }
        }

        public string FontFamilyName
        {
            get { return this.fontFamilyName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value));
                this.file = string.Empty;
                this.bigFont = string.Empty;
                this.fontFamilyName = value;
            }
        }

        public FontStyle FontStyle
        {
            get { return this.fontStyle; }
            set { this.fontStyle = value; }
        }

        public bool IsTrueType
        {
            get { return !string.IsNullOrEmpty(this.FontFamilyName); }
        }

        public double Height
        {
            get { return this.height; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The TextStyle height must be equals or greater than zero.");
                this.height = value;
            }
        }

        public double WidthFactor
        {
            get { return this.widthFactor; }
            set
            {
                if (value < 0.01 || value > 100.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The TextStyle width factor valid values range from 0.01 to 100.");
                this.widthFactor = value;
            }
        }

        public double ObliqueAngle
        {
            get { return this.obliqueAngle; }
            set
            {
                if (value < -85.0 || value > 85.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The TextStyle oblique angle valid values range from -85 to 85.");
                this.obliqueAngle = value;
            }
        }

        public bool IsVertical
        {
            get { return this.isVertical; }
            set { this.isVertical = value; }
        }

        public bool IsBackward
        {
            get { return this.isBackward; }
            set { this.isBackward = value; }
        }

        public bool IsUpsideDown
        {
            get { return this.isUpsideDown; }
            set { this.isUpsideDown = value; }
        }

        public new TextStyles Owner
        {
            get { return (TextStyles) base.Owner; }
            internal set { base.Owner = value; }
        }

        #endregion

        #region private methods

        private static string TrueTypeFontFamilyName(string ttfFont)
        {
            if (string.IsNullOrEmpty(ttfFont)) throw new ArgumentNullException(nameof(ttfFont));

            // the following information is only applied to TTF not SHX fonts
            if (!Path.GetExtension(ttfFont).Equals(".TTF", StringComparison.InvariantCultureIgnoreCase))
                return string.Empty;

            // try to find the file in the specified directory, if not try it in the fonts system folder
            string fontFile;
            if (File.Exists(ttfFont))
                fontFile = Path.GetFullPath(ttfFont);
            else
            {
                fontFile = string.Format("{0}{1}{2}", Environment.GetFolderPath(Environment.SpecialFolder.Fonts), Path.DirectorySeparatorChar, Path.GetFileName(ttfFont));
                // if the ttf does not even exist in the font system folder 
                if (!File.Exists(fontFile)) return string.Empty;
            }

            PrivateFontCollection fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile(fontFile);
            return fontCollection.Families[0].Name;
        }

        #endregion

        #region overrides

        public override TableObject Clone(string newName)
        {
            TextStyle copy;

            if (string.IsNullOrEmpty(this.file))
            {
                copy = new TextStyle(newName, this.fontFamilyName, this.fontStyle)
                {
                    Height = this.height,
                    IsBackward = this.isBackward,
                    IsUpsideDown = this.isUpsideDown,
                    IsVertical = this.isVertical,
                    ObliqueAngle = this.obliqueAngle,
                    WidthFactor = this.widthFactor
                };
            }
            else
            {
                copy = new TextStyle(newName, this.file)
                {
                    Height = this.height,
                    IsBackward = this.isBackward,
                    IsUpsideDown = this.isUpsideDown,
                    IsVertical = this.isVertical,
                    ObliqueAngle = this.obliqueAngle,
                    WidthFactor = this.widthFactor
                };
            }
            
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