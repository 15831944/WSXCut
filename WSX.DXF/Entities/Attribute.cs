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
    /// Represents a attribute <see cref="EntityObject">entity</see>.
    /// </summary>
    /// <remarks>
    /// The attribute position, rotation, height and width factor values also includes the transformation of the <see cref="Insert">Insert</see> entity to which it belongs.<br />
    /// During the attribute initialization a copy of all attribute definition properties will be copied,
    /// so any changes made to the attribute definition will only be applied to new attribute instances and not to existing ones.
    /// This behavior is to allow imported <see cref="Insert">Insert</see> entities to have attributes without definition in the block, 
    /// although this might sound not totally correct it is allowed by AutoCad.
    /// </remarks>
    public class Attribute :
        DxfObject,
        ICloneable
    {
        #region delegates and events

        public delegate void LayerChangedEventHandler(Attribute sender, TableObjectChangedEventArgs<Layer> e);

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

        public delegate void LinetypeChangedEventHandler(Attribute sender, TableObjectChangedEventArgs<Linetype> e);

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

        public delegate void TextStyleChangedEventHandler(Attribute sender, TableObjectChangedEventArgs<TextStyle> e);

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

        private AciColor color;
        private Layer layer;
        private Linetype linetype;
        private Lineweight lineweight;
        private Transparency transparency;
        private double linetypeScale;
        private bool isVisible;
        private Vector3 normal;

        private AttributeDefinition definition;
        private string tag;
        private object attValue;
        private TextStyle style;
        private Vector3 position;
        private AttributeFlags flags;
        private double height;
        private double widthFactor;
        private double obliqueAngle;
        private double rotation;
        private TextAlignment alignment;

        #endregion

        #region constructor

        internal Attribute()
            : base(DxfObjectCode.Attribute)
        {
        }

        public Attribute(AttributeDefinition definition)
            : base(DxfObjectCode.Attribute)
        {
            if (definition == null)
                throw new ArgumentNullException(nameof(definition));

            this.color = definition.Color;
            this.layer = definition.Layer;
            this.linetype = definition.Linetype;
            this.lineweight = definition.Lineweight;
            this.linetypeScale = definition.LinetypeScale;
            this.transparency = definition.Transparency;
            this.isVisible = definition.IsVisible;
            this.normal = definition.Normal;

            this.definition = definition;
            this.tag = definition.Tag;
            this.attValue = definition.Value;
            this.style = definition.Style;
            this.position = definition.Position;
            this.flags = definition.Flags;
            this.height = definition.Height;
            this.widthFactor = definition.WidthFactor;
            this.obliqueAngle = definition.ObliqueAngle;
            this.rotation = definition.Rotation;
            this.alignment = definition.Alignment;
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

        public new Insert Owner
        {
            get { return (Insert) base.Owner; }
            internal set { base.Owner = value; }
        }

        public AttributeDefinition Definition
        {
            get { return this.definition; }
            internal set { this.definition = value; }
        }

        public string Tag
        {
            get { return this.tag; }
            internal set { this.tag = value; }
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

        #endregion

        #region overrides

        public object Clone()
        {
            Attribute entity = new Attribute
            {
                //EntityObject properties
                Layer = (Layer) this.Layer.Clone(),
                Linetype = (Linetype) this.Linetype.Clone(),
                Color = (AciColor) this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                IsVisible = this.isVisible,
                //Attribute properties
                Definition = (AttributeDefinition) this.definition?.Clone(),
                Tag = this.tag,
                Height = this.height,
                WidthFactor = this.widthFactor,
                ObliqueAngle = this.obliqueAngle,
                Value = this.attValue,
                Style = this.style,
                Position = this.position,
                Flags = this.flags,
                Rotation = this.rotation,
                Alignment = this.alignment
            };

            return entity;
        }

        #endregion
    }
}