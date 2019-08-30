using System;
using System.Collections.Generic;
using WSX.DXF.Blocks;
using WSX.DXF.Collections;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a generic entity.
    /// </summary>
    public abstract class EntityObject:DxfObject,IHasXData,ICloneable
    {
        #region delegates and events

        public delegate void LayerChangedEventHandler(EntityObject sender, TableObjectChangedEventArgs<Layer> e);
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

        public delegate void LinetypeChangedEventHandler(EntityObject sender, TableObjectChangedEventArgs<Linetype> e);
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

        private readonly EntityType type;
        private AciColor color;
        private Layer layer;
        private Linetype linetype;
        private Lineweight lineweight;
        private Transparency transparency;
        private double linetypeScale;
        private bool isVisible;
        private Vector3 normal;
        private readonly XDataDictionary xData;
        private readonly List<DxfObject> reactors;

        #endregion

        #region constructors

        protected EntityObject(EntityType type, string dxfCode)
            : base(dxfCode)
        {
            this.type = type;
            this.color = AciColor.ByLayer;
            this.layer = Layer.Default;
            this.linetype = Linetype.ByLayer;
            this.lineweight = Lineweight.ByLayer;
            this.transparency = Transparency.ByLayer;
            this.linetypeScale = 1.0;
            this.isVisible = true;
            this.normal = Vector3.UnitZ;
            this.reactors = new List<DxfObject>();
            this.xData = new XDataDictionary();
            this.xData.AddAppReg += this.XData_AddAppReg;
            this.xData.RemoveAppReg += this.XData_RemoveAppReg;
        }

        #endregion

        #region public properties

        public IReadOnlyList<DxfObject> Reactors
        {
            get { return this.reactors; }
        }

        public EntityType Type
        {
            get { return this.type; }
        }

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

        public new Block Owner
        {
            get { return (Block) base.Owner; }
            internal set { base.Owner = value; }
        }

        public XDataDictionary XData
        {
            get { return this.xData; }
        }

        #endregion

        #region internal methods

        internal void AddReactor(DxfObject o)
        {
            this.reactors.Add(o);
        }

        internal bool RemoveReactor(DxfObject o)
        {
            return this.reactors.Remove(o);
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return this.type.ToString();
        }

        #endregion

        #region ICloneable

        public abstract object Clone();

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