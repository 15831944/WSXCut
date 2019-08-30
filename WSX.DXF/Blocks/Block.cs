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
using System.Collections.Generic;
using WSX.DXF.Collections;
using WSX.DXF.Entities;
using WSX.DXF.Header;
using WSX.DXF.Objects;
using WSX.DXF.Tables;

namespace WSX.DXF.Blocks
{
    /// <summary>
    /// Represents a block definition.
    /// </summary>
    /// <remarks>
    /// Avoid to add any kind of dimensions to the block's entities list, programs loading DXF files with them seems to behave in a weird fashion.
    /// This is not applicable when working in the Model and Paper space blocks.
    /// </remarks>
    public class Block :
        TableObject
    {
        #region delegates and events

        public delegate void LayerChangedEventHandler(Block sender, TableObjectChangedEventArgs<Layer> e);

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

        public delegate void EntityAddedEventHandler(Block sender, BlockEntityChangeEventArgs e);

        public event EntityAddedEventHandler EntityAdded;

        protected virtual void OnEntityAddedEvent(EntityObject item)
        {
            EntityAddedEventHandler ae = this.EntityAdded;
            if (ae != null)
                ae(this, new BlockEntityChangeEventArgs(item));
        }

        public delegate void EntityRemovedEventHandler(Block sender, BlockEntityChangeEventArgs e);

        public event EntityRemovedEventHandler EntityRemoved;

        protected virtual void OnEntityRemovedEvent(EntityObject item)
        {
            EntityRemovedEventHandler ae = this.EntityRemoved;
            if (ae != null)
                ae(this, new BlockEntityChangeEventArgs(item));
        }

        public delegate void AttributeDefinitionAddedEventHandler(Block sender, BlockAttributeDefinitionChangeEventArgs e);

        public event AttributeDefinitionAddedEventHandler AttributeDefinitionAdded;

        protected virtual void OnAttributeDefinitionAddedEvent(AttributeDefinition item)
        {
            AttributeDefinitionAddedEventHandler ae = this.AttributeDefinitionAdded;
            if (ae != null)
                ae(this, new BlockAttributeDefinitionChangeEventArgs(item));
        }

        public delegate void AttributeDefinitionRemovedEventHandler(Block sender, BlockAttributeDefinitionChangeEventArgs e);

        public event AttributeDefinitionRemovedEventHandler AttributeDefinitionRemoved;

        protected virtual void OnAttributeDefinitionRemovedEvent(AttributeDefinition item)
        {
            AttributeDefinitionRemovedEventHandler ae = this.AttributeDefinitionRemoved;
            if (ae != null)
                ae(this, new BlockAttributeDefinitionChangeEventArgs(item));
        }

        #endregion

        #region private fields

        private readonly EntityCollection entities;
        private readonly AttributeDefinitionDictionary attributes;
        private string description;
        private readonly EndBlock end;
        private BlockTypeFlags flags;
        private Layer layer;
        private Vector3 origin;
        private readonly string xrefFile;
        private bool forInternalUse;

        #endregion

        #region constants

        public const string DefaultModelSpaceName = "*Model_Space";

        public const string DefaultPaperSpaceName = "*Paper_Space";

        public static Block ModelSpace
        {
            get { return new Block(DefaultModelSpaceName, null, null, false); }
        }

        public static Block PaperSpace
        {
            get { return new Block(DefaultPaperSpaceName, null, null, false); }
        }

        #endregion

        #region constructors

        public Block(string name, string xrefFile)
            : this(name, xrefFile, false)
        {
        }

        public Block(string name, string xrefFile, bool overlay)
            : this(name, null, null, true)
        {
            if (string.IsNullOrEmpty(xrefFile))
                throw new ArgumentNullException(nameof(xrefFile));

            this.xrefFile = xrefFile;
            this.flags = BlockTypeFlags.XRef | BlockTypeFlags.ResolvedExternalReference;
            if (overlay)
                this.flags |= BlockTypeFlags.XRefOverlay;
        }

        public Block(string name)
            : this(name, null, null, true)
        {
        }

        public Block(string name, IEnumerable<EntityObject> entities)
            : this(name, entities, null, true)
        {
        }

        public Block(string name, IEnumerable<EntityObject> entities, IEnumerable<AttributeDefinition> attributes)
            : this(name, entities, attributes, true)
        {
        }

        internal Block(string name, IEnumerable<EntityObject> entities, IEnumerable<AttributeDefinition> attributes, bool checkName)
            : base(name, DxfObjectCode.Block, checkName)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            this.IsReserved = string.Equals(name, DefaultModelSpaceName, StringComparison.OrdinalIgnoreCase);
            this.forInternalUse = name.StartsWith("*");
            this.description = string.Empty;
            this.origin = Vector3.Zero;
            this.layer = Layer.Default;
            this.xrefFile = string.Empty;
            this.Owner = new BlockRecord(name);
            this.flags = BlockTypeFlags.None;
            this.end = new EndBlock(this);

            this.entities = new EntityCollection();
            this.entities.BeforeAddItem += this.Entities_BeforeAddItem;
            this.entities.AddItem += this.Entities_AddItem;
            this.entities.BeforeRemoveItem += this.Entities_BeforeRemoveItem;
            this.entities.RemoveItem += this.Entities_RemoveItem;
            if (entities != null) this.entities.AddRange(entities);

            this.attributes = new AttributeDefinitionDictionary();
            this.attributes.BeforeAddItem += this.AttributeDefinitions_BeforeAddItem;
            this.attributes.AddItem += this.AttributeDefinitions_ItemAdd;
            this.attributes.BeforeRemoveItem += this.AttributeDefinitions_BeforeRemoveItem;
            this.attributes.RemoveItem += this.AttributeDefinitions_RemoveItem;
            if (attributes != null) this.attributes.AddRange(attributes);
        }

        #endregion

        #region public properties

        public new string Name
        {
            get { return base.Name; }
            set
            {
                if (this.forInternalUse)
                    throw new ArgumentException("Blocks for internal use cannot be renamed.", nameof(value));
                base.Name = value;
                this.Record.Name = value;
            }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = string.IsNullOrEmpty(value) ? string.Empty : value; }
        }

        public Vector3 Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
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

        public EntityCollection Entities
        {
            get { return this.entities; }
        }

        public AttributeDefinitionDictionary AttributeDefinitions
        {
            get { return this.attributes; }
        }

        public BlockRecord Record
        {
            get { return (BlockRecord) this.Owner; }
        }

        public BlockTypeFlags Flags
        {
            get { return this.flags; }
            internal set { this.flags = value; }
        }

        public string XrefFile
        {
            get { return this.xrefFile; }
        }

        public bool IsXRef
        {
            get { return this.flags.HasFlag(BlockTypeFlags.XRef); }
        }

        ////////public bool IsForInternalUseOnly
        //{
        //    get { return this.forInternalUse; }
        //}

        #endregion

        #region internal properties

        internal EndBlock End
        {
            get { return this.end; }
        }

        #endregion

        #region public methods

        public static Block Create(DxfDocument doc, string name)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));

            Block block = new Block(name) {Origin = doc.DrawingVariables.InsBase};
            block.Record.Units = doc.DrawingVariables.InsUnits;

            foreach (EntityObject entity in doc.Layouts[Layout.ModelSpaceName].AssociatedBlock.Entities)
            {
                // entities with reactors will be handle by the entity that controls it
                // and will not be added to the block automatically
                if (entity.Reactors.Count > 0)
                {
                    // only entities that belong only to groups will be added
                    // blocks cannot contain groups
                    bool add = false;
                    foreach (DxfObject reactor in entity.Reactors)
                    {
                        Group group = reactor as Group;
                        if (group != null)
                        {
                            add = true;
                        }
                        else
                        {
                            add = false; // at least one reactor is not a group, skip the entity
                            break;
                        }
                    }
                    if(!add) continue;
                }

                EntityObject clone = (EntityObject) entity.Clone();
                block.Entities.Add(clone);
            }

            foreach (AttributeDefinition attdef in doc.Layouts[Layout.ModelSpaceName].AssociatedBlock.AttributeDefinitions.Values)
            {
                AttributeDefinition clone = (AttributeDefinition) attdef.Clone();
                block.AttributeDefinitions.Add(clone);
            }

            return block;
        }

        public static Block Load(string file)
        {
            return Load(file, null);
        }

        public static Block Load(string file, string name)
        {
#if DEBUG
            DxfDocument dwg = DxfDocument.Load(file);
#else
            DxfDocument dwg;
            try 
            {
                dwg = DxfDocument.Load(file);
            }
            catch
            {
                return null;
            }
#endif

            string blkName = string.IsNullOrEmpty(name) ? dwg.Name : name;
            return Create(dwg, blkName);
        }

        public bool Save(string file, DxfVersion version)
        {
            return this.Save(file, version, false);
        }

        public bool Save(string file, DxfVersion version, bool isBinary)
        {
            DxfDocument dwg = new DxfDocument(version);
            dwg.DrawingVariables.InsBase = this.origin;
            dwg.DrawingVariables.InsUnits = this.Record.Units;

            foreach (AttributeDefinition attdef in this.attributes.Values)
            {
                if(!dwg.Layouts[Layout.ModelSpaceName].AssociatedBlock.AttributeDefinitions.ContainsTag(attdef.Tag))
                    dwg.Layouts[Layout.ModelSpaceName].AssociatedBlock.AttributeDefinitions.Add((AttributeDefinition) attdef.Clone());
            }

            foreach (EntityObject entity in this.entities)
                dwg.Layouts[Layout.ModelSpaceName].AssociatedBlock.Entities.Add((EntityObject) entity.Clone());


            return dwg.Save(file, isBinary);
        }

        #endregion

        #region internal methods

        internal new void SetName(string newName, bool checkName)
        {
            base.SetName(newName, checkName);
            this.Record.Name = newName;
            this.forInternalUse = newName.StartsWith("*");
        }

        #endregion

        #region overrides

        private static TableObject Clone(Block block, string newName, bool checkName)
        {
            if (block.Record.Layout != null && !IsValidName(newName))
                throw new ArgumentException("*Model_Space and *Paper_Space# blocks can only be cloned with a new valid name.");

            Block copy = new Block(newName, null, null, checkName)
            {
                Description = block.description,
                Flags = block.flags,
                Layer = (Layer)block.Layer.Clone(),
                Origin = block.origin
            };

            // remove anonymous flag for renamed anonymous blocks
            if (checkName)
                copy.Flags &= ~BlockTypeFlags.AnonymousBlock;

            foreach (EntityObject e in block.entities)
                copy.entities.Add((EntityObject)e.Clone());

            foreach (AttributeDefinition a in block.attributes.Values)
                copy.attributes.Add((AttributeDefinition)a.Clone());

            foreach (XData data in block.XData.Values)
                copy.XData.Add((XData)data.Clone());

            foreach (XData data in block.Record.XData.Values)
                copy.Record.XData.Add((XData)data.Clone());

            return copy;
        }

        public override TableObject Clone(string newName)
        {
            return Clone(this, newName, true) ;
        }

        public override object Clone()
        {
            return Clone(this, this.Name, !this.flags.HasFlag(BlockTypeFlags.AnonymousBlock));
        }

        internal override long AsignHandle(long entityNumber)
        {
            entityNumber = this.Owner.AsignHandle(entityNumber);
            entityNumber = this.end.AsignHandle(entityNumber);
            foreach (AttributeDefinition attdef in this.attributes.Values)
            {
                entityNumber = attdef.AsignHandle(entityNumber);
            }
            return base.AsignHandle(entityNumber);
        }

        #endregion

        #region Entities collection events

        private void Entities_BeforeAddItem(EntityCollection sender, EntityCollectionEventArgs e)
        {
            // null items, entities already owned by another Block, attribute definitions and attributes are not allowed in the entities list.
            if (e.Item == null)
                e.Cancel = true;
            else if (this.entities.Contains(e.Item))
                e.Cancel = true;
            else if (this.Flags.HasFlag(BlockTypeFlags.ExternallyDependent))
                e.Cancel = true;
            else if (e.Item.Owner != null)
            {
                // if the block does not belong to a document, all entities which owner is not null will be rejected
                if (this.Record.Owner == null)
                    e.Cancel = true;
                // if the block belongs to a document, the entity will be added to the block only if both, the block and the entity document, are the same
                // this is handled by the BlocksRecordCollection
            }
            else
                e.Cancel = false;
        }

        private void Entities_AddItem(EntityCollection sender, EntityCollectionEventArgs e)
        {
            if (e.Item.Type == EntityType.Leader)
            {
                Leader leader = (Leader) e.Item;
                if (leader.Annotation != null)
                {
                    this.entities.Add(leader.Annotation);
                }
            }
            else if (e.Item.Type == EntityType.Hatch)
            {
                Hatch hatch = (Hatch) e.Item;
                foreach (HatchBoundaryPath path in hatch.BoundaryPaths)
                {
                    foreach (EntityObject entity in path.Entities)
                        this.entities.Add(entity);
                }
            }

            this.OnEntityAddedEvent(e.Item);
            e.Item.Owner = this;
        }

        private void Entities_BeforeRemoveItem(EntityCollection sender, EntityCollectionEventArgs e)
        {
            if (e.Item.Reactors.Count > 0)
                e.Cancel = true;
            else
                // only items owned by the actual block can be removed
                e.Cancel = !ReferenceEquals(e.Item.Owner, this);
        }

        private void Entities_RemoveItem(EntityCollection sender, EntityCollectionEventArgs e)
        {
            this.OnEntityRemovedEvent(e.Item);
            e.Item.Owner = null;
        }

        #endregion

        #region Attributes dictionary events

        private void AttributeDefinitions_BeforeAddItem(AttributeDefinitionDictionary sender, AttributeDefinitionDictionaryEventArgs e)
        {
            // attributes with the same tag, and attribute definitions already owned by another Block are not allowed in the attributes list.
            if (e.Item == null)
                e.Cancel = true;
            else if (this.Flags.HasFlag(BlockTypeFlags.ExternallyDependent))
                e.Cancel = true;
            else if(this.Name.StartsWith(DefaultPaperSpaceName)) // paper space blocks do not contain attribute definitions
                e.Cancel = true;
            else if (this.attributes.ContainsTag(e.Item.Tag))
                e.Cancel = true;
            else if (e.Item.Owner != null)
            {
                // if the block does not belong to a document, all attribute definitions which owner is not null will be rejected
                if (this.Record.Owner == null)
                    e.Cancel = true;
                // if the block belongs to a document, the entity will be added to the block only if both, the block and the attribute definitions document, are the same
                // this is handled by the BlocksRecordCollection
            }
            else
                e.Cancel = false;
        }

        private void AttributeDefinitions_ItemAdd(AttributeDefinitionDictionary sender, AttributeDefinitionDictionaryEventArgs e)
        {
            this.OnAttributeDefinitionAddedEvent(e.Item);
            e.Item.Owner = this;
            // the block has attributes
            this.flags |= BlockTypeFlags.NonConstantAttributeDefinitions;
        }

        private void AttributeDefinitions_BeforeRemoveItem(AttributeDefinitionDictionary sender, AttributeDefinitionDictionaryEventArgs e)
        {
            // only attribute definitions owned by the actual block can be removed
            e.Cancel = !ReferenceEquals(e.Item.Owner, this) ;
        }

        private void AttributeDefinitions_RemoveItem(AttributeDefinitionDictionary sender, AttributeDefinitionDictionaryEventArgs e)
        {
            this.OnAttributeDefinitionRemovedEvent(e.Item);
            e.Item.Owner = null;
            if (this.attributes.Count == 0)
                this.flags &= ~BlockTypeFlags.NonConstantAttributeDefinitions;
        }

        #endregion
    }
}