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

namespace WSX.DXF.Collections
{
    /// <summary>
    /// Represent a collection of items that fire events when it is modified. 
    /// </summary>
    /// <typeparam name="T">Type of items.</typeparam>
    public class ObservableCollection<T> :
        IList<T>
    {
        #region delegates and events

        public delegate void AddItemEventHandler(ObservableCollection<T> sender, ObservableCollectionEventArgs<T> e);

        public delegate void BeforeAddItemEventHandler(ObservableCollection<T> sender, ObservableCollectionEventArgs<T> e);

        public delegate void RemoveItemEventHandler(ObservableCollection<T> sender, ObservableCollectionEventArgs<T> e);

        public delegate void BeforeRemoveItemEventHandler(ObservableCollection<T> sender, ObservableCollectionEventArgs<T> e);

        public event BeforeAddItemEventHandler BeforeAddItem;
        public event AddItemEventHandler AddItem;
        public event BeforeRemoveItemEventHandler BeforeRemoveItem;
        public event RemoveItemEventHandler RemoveItem;

        protected virtual void OnAddItemEvent(T item)
        {
            AddItemEventHandler ae = this.AddItem;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<T>(item));
        }

        protected virtual bool OnBeforeAddItemEvent(T item)
        {
            BeforeAddItemEventHandler ae = this.BeforeAddItem;
            if (ae != null)
            {
                ObservableCollectionEventArgs<T> e = new ObservableCollectionEventArgs<T>(item);
                ae(this, e);
                return e.Cancel;
            }
            return false;
        }

        protected virtual bool OnBeforeRemoveItemEvent(T item)
        {
            BeforeRemoveItemEventHandler ae = this.BeforeRemoveItem;
            if (ae != null)
            {
                ObservableCollectionEventArgs<T> e = new ObservableCollectionEventArgs<T>(item);
                ae(this, e);
                return e.Cancel;
            }
            return false;
        }

        protected virtual void OnRemoveItemEvent(T item)
        {
            RemoveItemEventHandler ae = this.RemoveItem;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<T>(item));
        }

        #endregion

        #region private fields

        private readonly List<T> innerArray;

        #endregion

        #region constructor

        public ObservableCollection()
        {
            this.innerArray = new List<T>();
        }

        public ObservableCollection(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "The collection capacity cannot be negative.");
            this.innerArray = new List<T>(capacity);
        }

        #endregion

        #region public properties

        public T this[int index]
        {
            get { return this.innerArray[index]; }
            set
            {
                T remove = this.innerArray[index];
                T add = value;

                if (this.OnBeforeRemoveItemEvent(remove))
                    return;
                if (this.OnBeforeAddItemEvent(add))
                    return;
                this.innerArray[index] = value;
                this.OnAddItemEvent(add);
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

        public void Reverse()
        {
            this.innerArray.Reverse();
        }

        public void Sort(Comparison<T> comparision)
        {
            this.innerArray.Sort(comparision);
        }

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            this.innerArray.Sort(index, count, comparer);
        }

        public void Sort(IComparer<T> comparer)
        {
            this.innerArray.Sort(comparer);
        }

        public void Sort()
        {
            this.innerArray.Sort();
        }

        public void Add(T item)
        {
            if (this.OnBeforeAddItemEvent(item))
                throw new ArgumentException("The item cannot be added to the collection.", nameof(item));
            this.innerArray.Add(item);
            this.OnAddItemEvent(item);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (T item in collection)
                this.Add(item);
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index >= this.innerArray.Count)
                throw new ArgumentOutOfRangeException(string.Format("The parameter index {0} must be in between {1} and {2}.", index, 0, this.innerArray.Count));
            if (this.OnBeforeRemoveItemEvent(this.innerArray[index]))
                return;
            if (this.OnBeforeAddItemEvent(item))
                throw new ArgumentException("The item cannot be added to the collection.", nameof(item));
            this.OnRemoveItemEvent(this.innerArray[index]);
            this.innerArray.Insert(index, item);
            this.OnAddItemEvent(item);
        }

        public bool Remove(T item)
        {
            if (!this.innerArray.Contains(item))
                return false;
            if (this.OnBeforeRemoveItemEvent(item))
                return false;
            this.innerArray.Remove(item);
            this.OnRemoveItemEvent(item);
            return true;
        }

        public void Remove(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (T item in items)
            {
                if (!this.innerArray.Contains(item))
                    return;
                if (this.OnBeforeRemoveItemEvent(item))
                    return;
                this.innerArray.Remove(item);
                this.OnRemoveItemEvent(item);
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.innerArray.Count)
                throw new ArgumentOutOfRangeException(string.Format("The parameter index {0} must be in between {1} and {2}.", index, 0, this.innerArray.Count));
            T remove = this.innerArray[index];
            if (this.OnBeforeRemoveItemEvent(remove))
                return;
            this.innerArray.RemoveAt(index);
            this.OnRemoveItemEvent(remove);
        }

        public void Clear()
        {
            T[] items = new T[this.innerArray.Count];
            this.innerArray.CopyTo(items, 0);
            foreach (T item in items)
            {
                this.Remove(item);
            }
        }

        public int IndexOf(T item)
        {
            return this.innerArray.IndexOf(item);
        }

        public bool Contains(T item)
        {
            return this.innerArray.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.innerArray.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.innerArray.GetEnumerator();
        }

        #endregion

        #region private fields

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        void IList<T>.Insert(int index, T item)
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