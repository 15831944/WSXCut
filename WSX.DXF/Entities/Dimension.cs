using System;
using WSX.DXF.Blocks;
using WSX.DXF.Collections;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents the base class for a dimension <see cref="EntityObject">entity</see>.
    /// </summary>
    /// <reamarks>
    /// Once a dimension is added to the dxf document, its properties should not be modified or the changes will not be reflected in the saved dxf file.
    /// </reamarks>
    public abstract class Dimension :
        EntityObject
    {
        #region delegates and events

        public delegate void DimensionStyleChangedEventHandler(Dimension sender, TableObjectChangedEventArgs<DimensionStyle> e);

        public event DimensionStyleChangedEventHandler DimensionStyleChanged;

        protected virtual DimensionStyle OnDimensionStyleChangedEvent(DimensionStyle oldStyle, DimensionStyle newStyle)
        {
            DimensionStyleChangedEventHandler ae = this.DimensionStyleChanged;
            if (ae != null)
            {
                TableObjectChangedEventArgs<DimensionStyle> eventArgs = new TableObjectChangedEventArgs<DimensionStyle>(oldStyle, newStyle);
                ae(this, eventArgs);
                return eventArgs.NewValue;
            }
            return newStyle;
        }

        public delegate void DimensionBlockChangedEventHandler(Dimension sender, TableObjectChangedEventArgs<Block> e);

        public event DimensionBlockChangedEventHandler DimensionBlockChanged;

        protected virtual Block OnDimensionBlockChangedEvent(Block oldBlock, Block newBlock)
        {
            DimensionBlockChangedEventHandler ae = this.DimensionBlockChanged;
            if (ae != null)
            {
                TableObjectChangedEventArgs<Block> eventArgs = new TableObjectChangedEventArgs<Block>(oldBlock, newBlock);
                ae(this, eventArgs);
                return eventArgs.NewValue;
            }
            return newBlock;
        }

        #endregion

        #region delegates and events for style overrides

        public delegate void DimensionStyleOverrideAddedEventHandler(Dimension sender, DimensionStyleOverrideChangeEventArgs e);

        public event DimensionStyleOverrideAddedEventHandler DimensionStyleOverrideAdded;

        protected virtual void OnDimensionStyleOverrideAddedEvent(DimensionStyleOverride item)
        {
            DimensionStyleOverrideAddedEventHandler ae = this.DimensionStyleOverrideAdded;
            if (ae != null)
                ae(this, new DimensionStyleOverrideChangeEventArgs(item));
        }

        public delegate void DimensionStyleOverrideRemovedEventHandler(Dimension sender, DimensionStyleOverrideChangeEventArgs e);

        public event DimensionStyleOverrideRemovedEventHandler DimensionStyleOverrideRemoved;

        protected virtual void OnDimensionStyleOverrideRemovedEvent(DimensionStyleOverride item)
        {
            DimensionStyleOverrideRemovedEventHandler ae = this.DimensionStyleOverrideRemoved;
            if (ae != null)
                ae(this, new DimensionStyleOverrideChangeEventArgs(item));
        }

        #endregion

        #region protected fields

        protected Vector2 defPoint;
        protected Vector2 textRefPoint;
        private DimensionStyle style;
        private readonly DimensionType dimensionType;
        private MTextAttachmentPoint attachmentPoint;
        private MTextLineSpacingStyle lineSpacingStyle;
        private Block block;
        private double textRotation;
        private string userText;
        private double lineSpacing;
        private double elevation;
        private readonly DimensionStyleOverrideDictionary styleOverrides;
        private bool userTextPosition;

        #endregion

        #region constructors

        protected Dimension(DimensionType type)
            : base(EntityType.Dimension, DxfObjectCode.Dimension)
        {
            this.defPoint = Vector2.Zero;
            this.textRefPoint = Vector2.Zero;
            this.dimensionType = type;
            this.attachmentPoint = MTextAttachmentPoint.MiddleCenter;
            this.lineSpacingStyle = MTextLineSpacingStyle.AtLeast;
            this.lineSpacing = 1.0;
            this.block = null;
            this.style = DimensionStyle.Default;
            this.textRotation = 0.0;
            this.userText = null;
            this.elevation = 0.0;
            this.styleOverrides = new DimensionStyleOverrideDictionary();
            this.styleOverrides.BeforeAddItem += this.StyleOverrides_BeforeAddItem;
            this.styleOverrides.AddItem += this.StyleOverrides_AddItem;
            this.styleOverrides.BeforeRemoveItem += this.StyleOverrides_BeforeRemoveItem;
            this.styleOverrides.RemoveItem += this.StyleOverrides_RemoveItem;
        }

        #endregion

        #region internal properties

        internal Vector2 DefinitionPoint
        {
            get { return this.defPoint; }
            set { this.defPoint = value; }
        }

        #endregion

        #region public properties

        public bool TextPositionManuallySet
        {
            get { return this.userTextPosition; }
            set { this.userTextPosition = value; }
        }

        public Vector2 TextReferencePoint
        {
            get { return this.textRefPoint; }
            set
            {
                this.userTextPosition = true;
                this.textRefPoint = value;
            }
        }

        public DimensionStyle Style
        {
            get { return this.style; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.style = this.OnDimensionStyleChangedEvent(this.style, value);
            }
        }

        public DimensionStyleOverrideDictionary StyleOverrides
        {
            get { return this.styleOverrides; }
        }

        public DimensionType DimensionType
        {
            get { return this.dimensionType; }
        }

        public abstract double Measurement { get; }

        public MTextAttachmentPoint AttachmentPoint
        {
            get { return this.attachmentPoint; }
            set { this.attachmentPoint = value; }
        }

        public MTextLineSpacingStyle LineSpacingStyle
        {
            get { return this.lineSpacingStyle; }
            set { this.lineSpacingStyle = value; }
        }

        public double LineSpacingFactor
        {
            get { return this.lineSpacing; }
            set
            {
                if (value < 0.25 || value > 4.0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The line spacing factor valid values range from 0.25 to 4.00");
                this.lineSpacing = value;
            }
        }

        public Block Block
        {
            get { return this.block; }
            set { this.block = this.OnDimensionBlockChangedEvent(this.block, value); }
        }

        public double TextRotation
        {
            get { return this.textRotation; }
            set { this.textRotation = MathHelper.NormalizeAngle(value); }
        }

        public string UserText
        {
            get { return this.userText; }
            set { this.userText = value; }
        }

        public double Elevation
        {
            get { return this.elevation; }
            set { this.elevation = value; }
        }

        #endregion

        #region abstract methods

        protected abstract void CalculteReferencePoints();

        protected abstract Block BuildBlock(string name);

        #endregion

        #region public methods

        public void Update()
        {
            this.CalculteReferencePoints();

            if (this.block != null)
            {
                Block newBlock = this.BuildBlock(this.block.Name);
                this.block = this.OnDimensionBlockChangedEvent(this.block, newBlock);
            }
        }

        #endregion

        #region Dimension style overrides events

        private void StyleOverrides_BeforeAddItem(DimensionStyleOverrideDictionary sender, DimensionStyleOverrideDictionaryEventArgs e)
        {
            DimensionStyleOverride old;
            if (sender.TryGetValue(e.Item.Type, out old))
                if (ReferenceEquals(old.Value, e.Item.Value))
                    e.Cancel = true;
        }

        private void StyleOverrides_AddItem(DimensionStyleOverrideDictionary sender, DimensionStyleOverrideDictionaryEventArgs e)
        {
            this.OnDimensionStyleOverrideAddedEvent(e.Item);
        }

        private void StyleOverrides_BeforeRemoveItem(DimensionStyleOverrideDictionary sender, DimensionStyleOverrideDictionaryEventArgs e)
        {
        }

        private void StyleOverrides_RemoveItem(DimensionStyleOverrideDictionary sender, DimensionStyleOverrideDictionaryEventArgs e)
        {
            this.OnDimensionStyleOverrideRemovedEvent(e.Item);
        }

        #endregion
    }
}