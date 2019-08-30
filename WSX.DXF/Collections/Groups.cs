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
using WSX.DXF.Entities;
using WSX.DXF.Objects;
using WSX.DXF.Tables;

namespace WSX.DXF.Collections
{
    /// <summary>
    /// Represents a collection of groups.
    /// </summary>
    public sealed class Groups :
        TableObjects<Group>
    {
        #region constructor

        internal Groups(DxfDocument document)
            : this(document, null)
        {
        }

        internal Groups(DxfDocument document, string handle)
            : base(document, DxfObjectCode.GroupDictionary, handle)
        {
            this.MaxCapacity = int.MaxValue;
        }

        #endregion

        #region override methods

        internal override Group Add(Group group, bool assignHandle)
        {
            if (this.list.Count >= this.MaxCapacity)
                throw new OverflowException(string.Format("Table overflow. The maximum number of elements the table {0} can have is {1}", this.CodeName, this.MaxCapacity));
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            // if no name has been given to the group a generic name will be created
            if (group.IsUnnamed && string.IsNullOrEmpty(group.Name))
                group.SetName("*A" + this.Owner.GroupNamesIndex++, false);

            Group add;
            if (this.list.TryGetValue(group.Name, out add))
                return add;

            if (assignHandle || string.IsNullOrEmpty(group.Handle))
                this.Owner.NumHandles = group.AsignHandle(this.Owner.NumHandles);

            this.list.Add(group.Name, group);
            this.references.Add(group.Name, new List<DxfObject>());
            foreach (EntityObject entity in group.Entities)
            {
                if (entity.Owner != null)
                {
                    // the group and its entities must belong to the same document
                    if (!ReferenceEquals(entity.Owner.Owner.Owner.Owner, this.Owner))
                        throw new ArgumentException("The group and their entities must belong to the same document. Clone them instead.");
                }
                else
                {
                    // only entities not owned by anyone need to be added
                    this.Owner.AddEntity(entity);
                }
                this.references[group.Name].Add(entity);
            }

            group.Owner = this;

            group.NameChanged += this.Item_NameChanged;
            group.EntityAdded += this.Group_EntityAdded;
            group.EntityRemoved += this.Group_EntityRemoved;

            this.Owner.AddedObjects.Add(group.Handle, group);

            return group;
        }

        public override bool Remove(string name)
        {
            return this.Remove(this[name]);
        }

        public override bool Remove(Group item)
        {
            if (item == null)
                return false;

            if (!this.Contains(item))
                return false;

            if (item.IsReserved)
                return false;

            foreach (EntityObject entity in item.Entities)
            {
                entity.RemoveReactor(item);
            }

            this.Owner.AddedObjects.Remove(item.Handle);
            this.references.Remove(item.Name);
            this.list.Remove(item.Name);

            item.Handle = null;
            item.Owner = null;

            item.NameChanged -= this.Item_NameChanged;
            item.EntityAdded -= this.Group_EntityAdded;
            item.EntityRemoved -= this.Group_EntityRemoved;

            return true;
        }

        #endregion

        #region Group events

        private void Item_NameChanged(TableObject sender, TableObjectChangedEventArgs<string> e)
        {
            if (this.Contains(e.NewValue))
                throw new ArgumentException("There is already another dimension style with the same name.");

            this.list.Remove(sender.Name);
            this.list.Add(e.NewValue, (Group) sender);

            List<DxfObject> refs = this.references[sender.Name];
            this.references.Remove(sender.Name);
            this.references.Add(e.NewValue, refs);
        }

        void Group_EntityAdded(Group sender, GroupEntityChangeEventArgs e)
        {
            if (e.Item.Owner != null)
            {
                // the group and its entities must belong to the same document
                if (!ReferenceEquals(e.Item.Owner.Owner.Owner.Owner, this.Owner))
                    throw new ArgumentException("The group and the entity must belong to the same document. Clone it instead.");
            }
            else
            {
                // only entities not owned by anyone will be added
                this.Owner.AddEntity(e.Item);
            }

            this.references[sender.Name].Add(e.Item);
        }

        void Group_EntityRemoved(Group sender, GroupEntityChangeEventArgs e)
        {
            this.references[sender.Name].Remove(e.Item);
        }

        #endregion
    }
}