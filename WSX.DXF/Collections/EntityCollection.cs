#region WSX.DXF library, Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2016 Daniel Carvajal (haplokuon@gmail.com)
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
using System.Collections;
using System.Collections.Generic;
using WSX.DXF.Entities;

namespace WSX.DXF.Collections
{
    /// <summary>
    /// Represent a collection of <see cref="EntityObject">entities</see> that fire events when it is modified. 
    /// </summary>
    public class EntityCollection :
        IList<EntityObject>
    {
        #region delegates and events

        public delegate void BeforeAddItemEventHandler(EntityCollection sender, EntityCollectionEventArgs e);

        public event BeforeAddItemEventHandler BeforeAddItem;

        protected virtual bool OnBeforeAddItemEvent(EntityObject item)
        {
            BeforeAddItemEventHandler ae = this.BeforeAddItem;
            if (ae != null)
            {
                EntityCollectionEventArgs e = new EntityCollectionEventArgs(item);
                ae(this, e);
                return e.Cancel;
            }
            return false;
        }

        public delegate void AddItemEventHandler(EntityCollection sender, EntityCollectionEventArgs e);

        public event AddItemEventHandler AddItem;

        protected virtual void OnAddItemEvent(EntityObject item)
        {
            AddItemEventHandler ae = this.AddItem;
            if (ae != null)
                ae(this, new EntityCollectionEventArgs(item));
        }

        public delegate void RemoveItemEventHandler(EntityCollection sender, EntityCollectionEventArgs e);

        public event BeforeRemoveItemEventHandler BeforeRemoveItem;

        protected virtual bool OnBeforeRemoveItemEvent(EntityObject item)
        {
            BeforeRemoveItemEventHandler ae = this.BeforeRemoveItem;
            if (ae != null)
            {
                EntityCollectionEventArgs e = new EntityCollectionEventArgs(item);
                ae(this, e);
                return e.Cancel;
            }
            return false;
        }

        public delegate void BeforeRemoveItemEventHandler(EntityCollection sender, EntityCollectionEventArgs e);

        public event RemoveItemEventHandler RemoveItem;

        protected virtual void OnRemoveItemEvent(EntityObject item)
        {
            RemoveItemEventHandler ae = this.RemoveItem;
            if (ae != null)
                ae(this, new EntityCollectionEventArgs(item));
        }

        #endregion

        #region private fields

        private readonly List<EntityObject> innerArray;

        #endregion

        #region constructor

        public EntityCollection()
        {
            this.innerArray = new List<EntityObject>();
        }

        public EntityCollection(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "The collection capacity cannot be negative.");
            this.innerArray = new List<EntityObject>(capacity);
        }

        #endregion

        #region public properties

        public EntityObject this[int index]
        {
            get { return this.innerArray[index]; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                EntityObject remove = this.innerArray[index];

                if (this.OnBeforeRemoveItemEvent(remove))
                    return;
                if (this.OnBeforeAddItemEvent(value))
                    return;
                this.innerArray[index] = value;
                this.OnAddItemEvent(value);
                this.OnRemoveItemEvent(remove);
            }
        }

        public int Count
        {
            get { return this.innerArray.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region public methods

        public void Add(EntityObject item)
        {
            if (this.OnBeforeAddItemEvent(item))
                throw new ArgumentException("The entity cannot be added to the collection.", nameof(item));
            this.innerArray.Add(item);
            this.OnAddItemEvent(item);
        }

        public void AddRange(IEnumerable<EntityObject> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            foreach (EntityObject item in collection)
                this.Add(item);
        }

        public void Insert(int index, EntityObject item)
        {
            if (index < 0 || index >= this.innerArray.Count)
                throw new ArgumentOutOfRangeException(string.Format("The parameter index {0} must be in between {1} and {2}.", index, 0, this.innerArray.Count));
            if (this.OnBeforeRemoveItemEvent(this.innerArray[index]))
                return;
            if (this.OnBeforeAddItemEvent(item))
                throw new ArgumentException("The entity cannot be added to the collection.", nameof(item));
            this.OnRemoveItemEvent(this.innerArray[index]);
            this.innerArray.Insert(index, item);
            this.OnAddItemEvent(item);
        }

        public bool Remove(EntityObject item)
        {
            if (!this.innerArray.Contains(item))
                return false;
            if (this.OnBeforeRemoveItemEvent(item))
                return false;
            this.innerArray.Remove(item);
            this.OnRemoveItemEvent(item);
            return true;
        }

        public void Remove(IEnumerable<EntityObject> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (EntityObject item in items)
                this.Remove(item);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.innerArray.Count)
                throw new ArgumentOutOfRangeException(string.Format("The parameter index {0} must be in between {1} and {2}.", index, 0, this.innerArray.Count));
            EntityObject remove = this.innerArray[index];
            if (this.OnBeforeRemoveItemEvent(remove))
                return;
            this.innerArray.RemoveAt(index);
            this.OnRemoveItemEvent(remove);
        }

        public void Clear()
        {
            EntityObject[] entities = new EntityObject[this.innerArray.Count];
            this.innerArray.CopyTo(entities, 0);
            foreach (EntityObject item in entities)
                this.Remove(item);
        }

        public int IndexOf(EntityObject item)
        {
            return this.innerArray.IndexOf(item);
        }

        public bool Contains(EntityObject item)
        {
            return this.innerArray.Contains(item);
        }

        public void CopyTo(EntityObject[] array, int arrayIndex)
        {
            this.innerArray.CopyTo(array, arrayIndex);
        }

        public IEnumerator<EntityObject> GetEnumerator()
        {
            return this.innerArray.GetEnumerator();
        }

        #endregion

        #region private methods

        void ICollection<EntityObject>.Add(EntityObject item)
        {
            this.Add(item);
        }

        void IList<EntityObject>.Insert(int index, EntityObject item)
        {
            this.Insert(index, item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}