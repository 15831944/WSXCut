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
    /// Represents a text linetype segment.
    /// </summary>
    public class LinetypeTextSegment :
        LinetypeSegment
    {
        #region delegates and events

        public delegate void TextStyleChangedEventHandler(LinetypeTextSegment sender, TableObjectChangedEventArgs<TextStyle> e);

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

        #endregion

        #region private fields

        private string text;
        private TextStyle style;
        private Vector2 offset;
        private LinetypeSegmentRotationType rotationType;
        private double rotation;
        private double scale;

        #endregion

        #region constructors

        public LinetypeTextSegment() : this(string.Empty, TextStyle.Default, 0.0, Vector2.Zero, LinetypeSegmentRotationType.Relative, 0.0, 1.0)
        {
        }

        public LinetypeTextSegment(string text, TextStyle style, double length) : this(text, style, length, Vector2.Zero, LinetypeSegmentRotationType.Relative, 0.0, 1.0)
        {
        }

        public LinetypeTextSegment(string text, TextStyle style, double length, Vector2 offset, LinetypeSegmentRotationType rotationType, double rotation, double scale) : base(LinetypeSegmentType.Text, length)
        {
            if (string.IsNullOrEmpty(text)) this.text = string.Empty;
            this.text = text;
            if (style == null)
                throw new ArgumentNullException(nameof(style), "The style must be a valid TextStyle.");
            this.style = style;
            this.offset = offset;
            this.rotationType = rotationType;
            this.rotation = MathHelper.NormalizeAngle(rotation);
            if (scale <= 0)
                throw new ArgumentOutOfRangeException(nameof(scale), scale, "The LinetypeTextSegment scale must be greater than zero.");
            this.scale = scale;
        }

        #endregion

        #region public properties

        public string Text
        {
            get { return this.text; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    this.text = string.Empty;
                this.text = value;
            }
        }

        public TextStyle Style
        {
            get { return this.style; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.style = this.OnTextStyleChangedEvent(this.style, value);
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
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The linetype text segment scale must be greater than zero.");
                this.scale = value;
            }
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            return new LinetypeTextSegment(this.text, (TextStyle) this.style.Clone(), this.Length, this.offset, this.rotationType, this.rotation, this.scale);
        }

        #endregion
    }
}