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
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a Text <see cref="EntityObject">entity</see>.
    /// </summary>
    public class Text :
        EntityObject
    {
        #region delegates and events

        public delegate void TextStyleChangedEventHandler(Text sender, TableObjectChangedEventArgs<TextStyle> e);

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

        private TextAlignment alignment;
        private Vector3 position;
        private double obliqueAngle;
        private TextStyle style;
        private string text;
        private double height;
        private double widthFactor;
        private double rotation;

        #endregion

        #region constructors

        public Text()
            : this(string.Empty, Vector3.Zero, 1.0, TextStyle.Default)
        {
        }

        public Text(string text, Vector2 position, double height)
            : this(text, new Vector3(position.X, position.Y, 0.0), height, TextStyle.Default)
        {
        }

        public Text(string text, Vector3 position, double height)
            : this(text, position, height, TextStyle.Default)
        {
        }


        public Text(string text, Vector2 position, double height, TextStyle style)
            : this(text, new Vector3(position.X, position.Y, 0.0), height, style)
        {
        }

        public Text(string text, Vector3 position, double height, TextStyle style)
            : base(EntityType.Text, DxfObjectCode.Text)
        {
            this.text = text;
            this.position = position;
            this.alignment = TextAlignment.BaselineLeft;
            this.Normal = Vector3.UnitZ;
            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.style = style;
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height), this.text, "The Text height must be greater than zero.");
            this.height = height;
            this.widthFactor = style.WidthFactor;
            this.obliqueAngle = style.ObliqueAngle;
            this.rotation = 0.0;
        }

        #endregion

        #region public properties

        public Vector3 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public double Rotation
        {
            get { return this.rotation; }
            set { this.rotation = MathHelper.NormalizeAngle(value); }
        }

        public double Height
        {
            get { return this.height; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The Text height must be greater than zero.");
                this.height = value;
            }
        }

        public double WidthFactor
        {
            get { return this.widthFactor; }
            set
            {
                if (value < 0.01 || value > 100.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The Text width factor valid values range from 0.01 to 100.");
                this.widthFactor = value;
            }
        }

        public double ObliqueAngle
        {
            get { return this.obliqueAngle; }
            set
            {
                if (value < -85.0 || value > 85.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The Text oblique angle valid values range from -85 to 85.");
                this.obliqueAngle = value;
            }
        }

        public TextAlignment Alignment
        {
            get { return this.alignment; }
            set { this.alignment = value; }
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

        public string Value
        {
            get { return this.text; }
            set { this.text = value; }
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            Text entity = new Text
            {
                //EntityObject properties
                Layer = (Layer) this.Layer.Clone(),
                Linetype = (Linetype) this.Linetype.Clone(),
                Color = (AciColor) this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                IsVisible = this.IsVisible,
                //Text properties
                Position = this.position,
                Rotation = this.rotation,
                Height = this.height,
                WidthFactor = this.widthFactor,
                ObliqueAngle = this.obliqueAngle,
                Alignment = this.alignment,
                Style = (TextStyle) this.style.Clone(),
                Value = this.text
            };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}