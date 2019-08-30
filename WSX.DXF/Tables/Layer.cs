using System;
using WSX.DXF.Collections;

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Represents a layer.
    /// </summary>
    public class Layer :TableObject
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

        #endregion

        #region private fields

        private AciColor color;
        private bool isVisible;
        private bool isFrozen;
        private bool isLocked;
        private bool plot;
        private Linetype linetype;
        private Lineweight lineweight;
        private Transparency transparency;

        #endregion

        #region constants

        public const string DefaultName = "0";

        public static Layer Default
        {
            get { return new Layer(DefaultName); }
        }

        #endregion

        #region constructors

        public Layer(string name)
            : this(name, true)
        {
        }

        internal Layer(string name, bool checkName)
            : base(name, DxfObjectCode.Layer, checkName)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "The layer name should be at least one character long.");

            this.IsReserved = name.Equals(DefaultName, StringComparison.OrdinalIgnoreCase);
            this.color = AciColor.Default;
            this.linetype = Linetype.Continuous;
            this.isVisible = true;
            this.plot = true;
            this.lineweight = Lineweight.Default;
            this.transparency = new Transparency(0);
        }

        #endregion

        #region public properties

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

        public AciColor Color
        {
            get { return this.color; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (value.IsByLayer || value.IsByBlock)
                    throw new ArgumentException("The layer color cannot be ByLayer or ByBlock", nameof(value));
                this.color = value;
            }
        }

        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }

        public bool IsFrozen
        {
            get { return this.isFrozen; }
            set { this.isFrozen = value; }
        }

        public bool IsLocked
        {
            get { return this.isLocked; }
            set { this.isLocked = value; }
        }

        public bool Plot
        {
            get { return this.plot; }
            set { this.plot = value; }
        }

        public Lineweight Lineweight
        {
            get { return this.lineweight; }
            set
            {
                if (value == Lineweight.ByLayer || value == Lineweight.ByBlock)
                    throw new ArgumentException("The lineweight of a layer cannot be set to ByLayer or ByBlock.", nameof(value));
                this.lineweight = value;
            }
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

        public new Layers Owner
        {
            get { return (Layers) base.Owner; }
            internal set { base.Owner = value; }
        }

        #endregion

        #region overrides

        public override TableObject Clone(string newName)
        {
            Layer copy = new Layer(newName)
            {
                Color = (AciColor) this.Color.Clone(),
                IsVisible = this.isVisible,
                IsFrozen = this.isFrozen,
                IsLocked = this.isLocked,
                Plot = this.plot,
                Linetype = (Linetype) this.Linetype.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone()
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