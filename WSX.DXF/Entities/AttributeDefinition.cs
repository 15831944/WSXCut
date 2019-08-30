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
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents an attribute definition <see cref="EntityObject">entity</see>.
    /// </summary>
    /// <remarks>
    /// AutoCad allows to have duplicate tags in the attribute definitions list, but this library does not.
    /// To have duplicate tags is not recommended in any way, since there will be now way to know which is the definition associated to the insert attribute.
    /// </remarks>
    public class AttributeDefinition :
        DxfObject,
        ICloneable,
        IHasXData
    {
        #region delegates and events

        public delegate void LayerChangedEventHandler(AttributeDefinition sender, TableObjectChangedEventArgs<Layer> e);
        public event LayerChangedEventHandler LayerChanged;
        protected virtual Layer OnLayerChangedEvent(Layer oldLayer, Layer newLayer)
        {
            LayerChangedEventHandler ae = this.LayerChanged;
            if (ae != null)
            {
                TableObjectChangedEventArgs<Layer> eventArgs = new TableObjectChangedEventArgs<Layer>(oldLayer, newLayer);
                ae(this, eventArgs);
                return eventArgs.NewValue;
            }
            return newLayer;
        }

        public delegate void LinetypeChangedEventHandler(AttributeDefinition sender, TableObjectChangedEventArgs<Linetype> e);
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

        public delegate void TextStyleChangedEventHandler(AttributeDefinition sender, TableObjectChangedEventArgs<TextStyle> e);
        public event TextStyleChangedEventHandler TextStyleChange;
        protected virtual TextStyle OnTextStyleChangedEvent(TextStyle oldTextStyle, TextStyle newTextStyle)
        {
            TextStyleChangedEventHandler ae = this.TextStyleChange;
            if (ae != null)
            {
                TableObjectChangedEventArgs<TextStyle> eventArgs = new TableObjectChangedEventArgs<TextStyle>(oldTextStyle, newTextStyle);
                ae(this, eventArgs);
                return eventArgs.NewValue;
            }
            return newTextStyle;
        }

        public event XDataAddAppRegEventHandler XDataAddAppReg;
        protected virtual void OnXDataAddAppRegEvent(ApplicationRegistry item)
        {
            XDataAddAppRegEventHandler ae = this.XDataAddAppReg;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<ApplicationRegistry>(item));
        }

        public event XDataRemoveAppRegEventHandler XDataRemoveAppReg;
        protected virtual void OnXDataRemoveAppRegEvent(ApplicationRegistry item)
        {
            XDataRemoveAppRegEventHandler ae = this.XDataRemoveAppReg;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<ApplicationRegistry>(item));
        }

        #endregion

        #region private fields

        private AciColor color;
        private Layer layer;
        private Linetype linetype;
        private Lineweight lineweight;
        private Transparency transparency;
        private double linetypeScale;
        private bool isVisible;
        private Vector3 normal;

        private readonly string tag;
        private string prompt;
        private object attValue;
        private TextStyle style;
        private Vector3 position;
        private AttributeFlags flags;
        private double height;
        private double widthFactor;
        private double obliqueAngle;
        private double rotation;
        private TextAlignment alignment;

        private readonly XDataDictionary xData;

        #endregion

        #region constructor

        public AttributeDefinition(string tag)
            : this(tag, TextStyle.Default)
        {
        }

        public AttributeDefinition(string tag, TextStyle style)
            : this(tag, MathHelper.IsZero(style.Height) ? 1.0 : style.Height, style)
        {
        }

        public AttributeDefinition(string tag, double textHeight, TextStyle style)
            : base(DxfObjectCode.AttributeDefinition)
        {
            if (string.IsNullOrEmpty(tag))
                throw new ArgumentNullException(nameof(tag));

            if (tag.Contains(" "))
                throw new ArgumentException("The tag string cannot contain spaces.", nameof(tag));
            this.tag = tag;
            this.flags = AttributeFlags.Visible;
            this.prompt = string.Empty;
            this.attValue = null;
            this.position = Vector3.Zero;
            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.style = style;
            if (textHeight <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(textHeight), this.attValue, "The attribute definition text height must be greater than zero.");
            this.height = textHeight;
            this.widthFactor = style.WidthFactor;
            this.obliqueAngle = style.ObliqueAngle;
            this.rotation = 0.0;
            this.alignment = TextAlignment.BaselineLeft;

            this.color = AciColor.ByLayer;
            this.layer = Layer.Default;
            this.linetype = Linetype.ByLayer;
            this.lineweight = Lineweight.ByLayer;
            this.transparency = Transparency.ByLayer;
            this.linetypeScale = 1.0;
            this.isVisible = true;
            this.normal = Vector3.UnitZ;

            this.xData = new XDataDictionary();
            this.xData.AddAppReg += this.XData_AddAppReg;
            this.xData.RemoveAppReg += this.XData_RemoveAppReg;

        }

        #endregion

        #region public property

        public AciColor Color
        {
            get { return this.color; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.color = value;
            }
        }

        public Layer Layer
        {
            get { return this.layer; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.layer = this.OnLayerChangedEvent(this.layer, value);
            }
        }

        public Linetype Linetype
        {
            get { return this.linetype; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.linetype = this.OnLinetypeChangedEvent(this.linetype, value);
            }
        }

        public Lineweight Lineweight
        {
            get { return this.lineweight; }
            set { this.lineweight = value; }
        }

        public Transparency Transparency
        {
            get { return this.transparency; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.transparency = value;
            }
        }

        public double LinetypeScale
        {
            get { return this.linetypeScale; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The line type scale must be greater than zero.");
                this.linetypeScale = value;
            }
        }

        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }

        public Vector3 Normal
        {
            get { return this.normal; }
            set
            {
                this.normal = Vector3.Normalize(value);
                if (Vector3.IsNaN(this.normal))
                    throw new ArgumentException("The normal can not be the zero vector.", nameof(value));
            }
        }

        public string Tag
        {
            get { return this.tag; }
        }

        public string Prompt
        {
            get { return this.prompt; }
            set { this.prompt = value; }
        }

        public double Height
        {
            get { return this.height; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The height should be greater than zero.");
                this.height = value;
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

        public object Value
        {
            get { return this.attValue; }
            set { this.attValue = value; }
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

        public Vector3 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public AttributeFlags Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }

        public double Rotation
        {
            get { return this.rotation; }
            set { this.rotation = MathHelper.NormalizeAngle(value); }
        }

        public TextAlignment Alignment
        {
            get { return this.alignment; }
            set { this.alignment = value; }
        }

        public new Block Owner
        {
            get { return (Block)base.Owner; }
            internal set { base.Owner = value; }
        }

        public XDataDictionary XData
        {
            get { return this.xData; }
        }

        #endregion

        #region overrides

        public object Clone()
        {
            AttributeDefinition entity = new AttributeDefinition(this.tag)
            {
                //Attribute definition properties
                Layer = (Layer) this.Layer.Clone(),
                Linetype = (Linetype) this.Linetype.Clone(),
                Color = (AciColor) this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                IsVisible = this.IsVisible,
                Prompt = this.prompt,
                Value = this.attValue,
                Height = this.height,
                WidthFactor = this.widthFactor,
                ObliqueAngle = this.obliqueAngle,
                Style = this.style,
                Position = this.position,
                Flags = this.flags,
                Rotation = this.rotation,
                Alignment = this.alignment
            };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion

        #region XData events

        private void XData_AddAppReg(XDataDictionary sender, ObservableCollectionEventArgs<ApplicationRegistry> e)
        {
            this.OnXDataAddAppRegEvent(e.Item);
        }

        private void XData_RemoveAppReg(XDataDictionary sender, ObservableCollectionEventArgs<ApplicationRegistry> e)
        {
            this.OnXDataRemoveAppRegEvent(e.Item);
        }

        #endregion

    }
}