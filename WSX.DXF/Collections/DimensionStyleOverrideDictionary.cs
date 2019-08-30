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
using WSX.DXF.Tables;

namespace WSX.DXF.Collections
{
    /// <summary>
    /// Represents a dictionary of <see cref="DimensionStyleOverride">DimensionStyleOverrides</see>.
    /// </summary>
    public sealed class DimensionStyleOverrideDictionary :
        IDictionary<DimensionStyleOverrideType, DimensionStyleOverride>
    {
        #region delegates and events

        public delegate void BeforeAddItemEventHandler(DimensionStyleOverrideDictionary sender, DimensionStyleOverrideDictionaryEventArgs e);

        public event BeforeAddItemEventHandler BeforeAddItem;

        private bool OnBeforeAddItemEvent(DimensionStyleOverride item)
        {
            BeforeAddItemEventHandler ae = this.BeforeAddItem;
            if (ae != null)
            {
                DimensionStyleOverrideDictionaryEventArgs e = new DimensionStyleOverrideDictionaryEventArgs(item);
                ae(this, e);
                return e.Cancel;
            }
            return false;
        }

        public delegate void AddItemEventHandler(DimensionStyleOverrideDictionary sender, DimensionStyleOverrideDictionaryEventArgs e);

        public event AddItemEventHandler AddItem;

        private void OnAddItemEvent(DimensionStyleOverride item)
        {
            AddItemEventHandler ae = this.AddItem;
            if (ae != null)
                ae(this, new DimensionStyleOverrideDictionaryEventArgs(item));
        }

        public delegate void BeforeRemoveItemEventHandler(DimensionStyleOverrideDictionary sender, DimensionStyleOverrideDictionaryEventArgs e);

        public event BeforeRemoveItemEventHandler BeforeRemoveItem;

        private bool OnBeforeRemoveItemEvent(DimensionStyleOverride item)
        {
            BeforeRemoveItemEventHandler ae = this.BeforeRemoveItem;
            if (ae != null)
            {
                DimensionStyleOverrideDictionaryEventArgs e = new DimensionStyleOverrideDictionaryEventArgs(item);
                ae(this, e);
                return e.Cancel;
            }
            return false;
        }

        public delegate void RemoveItemEventHandler(DimensionStyleOverrideDictionary sender, DimensionStyleOverrideDictionaryEventArgs e);

        public event RemoveItemEventHandler RemoveItem;

        private void OnRemoveItemEvent(DimensionStyleOverride item)
        {
            RemoveItemEventHandler ae = this.RemoveItem;
            if (ae != null)
                ae(this, new DimensionStyleOverrideDictionaryEventArgs(item));
        }

        #endregion

        #region private fields

        private readonly Dictionary<DimensionStyleOverrideType, DimensionStyleOverride> innerDictionary;

        #endregion

        #region constructor

        public DimensionStyleOverrideDictionary()
        {
            this.innerDictionary = new Dictionary<DimensionStyleOverrideType, DimensionStyleOverride>();
        }

        public DimensionStyleOverrideDictionary(int capacity)
        {
            this.innerDictionary = new Dictionary<DimensionStyleOverrideType, DimensionStyleOverride>(capacity);
        }

        #endregion

        #region public properties

        public DimensionStyleOverride this[DimensionStyleOverrideType type]
        {
            get { return this.innerDictionary[type]; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (type != value.Type)
                    throw new ArgumentException(string.Format("The dictionary type: {0}, and the DimensionStyleOverride type: {1}, must be the same", type, value.Type));

                DimensionStyleOverride remove = this.innerDictionary[type];
                if (this.OnBeforeRemoveItemEvent(remove))
                    return;
                if (this.OnBeforeAddItemEvent(value))
                    return;
                this.innerDictionary[type] = value;
                this.OnAddItemEvent(value);
                this.OnRemoveItemEvent(remove);
            }
        }

        public ICollection<DimensionStyleOverrideType> Types
        {
            get { return this.innerDictionary.Keys; }
        }

        public ICollection<DimensionStyleOverride> Values
        {
            get { return this.innerDictionary.Values; }
        }

        public int Count
        {
            get { return this.innerDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region public methods

        public void Add(DimensionStyleOverrideType type, object value)
        {
            this.Add(new DimensionStyleOverride(type, value));
        }

        public void Add(DimensionStyleOverride item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (this.OnBeforeAddItemEvent(item))
                throw new ArgumentException(string.Format("The DimensionStyleOverride {0} cannot be added to the collection.", item), nameof(item));
            this.innerDictionary.Add(item.Type, item);
            this.OnAddItemEvent(item);
        }

        public void AddRange(IEnumerable<DimensionStyleOverride> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            // we will make room for so the collection will fit without having to resize the internal array during the Add method
            foreach (DimensionStyleOverride item in collection)
                this.Add(item);
        }

        public bool Remove(DimensionStyleOverrideType type)
        {
            DimensionStyleOverride remove;
            if (!this.innerDictionary.TryGetValue(type, out remove))
                return false;
            if (this.OnBeforeRemoveItemEvent(remove))
                return false;
            this.innerDictionary.Remove(type);
            this.OnRemoveItemEvent(remove);
            return true;
        }

        public void Clear()
        {
            DimensionStyleOverrideType[] types = new DimensionStyleOverrideType[this.innerDictionary.Count];
            this.innerDictionary.Keys.CopyTo(types, 0);
            foreach (DimensionStyleOverrideType tag in types)
            {
                this.Remove(tag);
            }
        }

        public bool ContainsType(DimensionStyleOverrideType type)
        {
            return this.innerDictionary.ContainsKey(type);
        }

        public bool ContainsValue(DimensionStyleOverride value)
        {
            return this.innerDictionary.ContainsValue(value);
        }

        public bool TryGetValue(DimensionStyleOverrideType type, out DimensionStyleOverride value)
        {
            return this.innerDictionary.TryGetValue(type, out value);
        }

        public IEnumerator<KeyValuePair<DimensionStyleOverrideType, DimensionStyleOverride>> GetEnumerator()
        {
            return this.innerDictionary.GetEnumerator();
        }

        #endregion

        #region private properties

        ICollection<DimensionStyleOverrideType> IDictionary<DimensionStyleOverrideType, DimensionStyleOverride>.Keys
        {
            get { return this.innerDictionary.Keys; }
        }

        #endregion

        #region private methods

        bool IDictionary<DimensionStyleOverrideType, DimensionStyleOverride>.ContainsKey(DimensionStyleOverrideType tag)
        {
            return this.innerDictionary.ContainsKey(tag);
        }

        void IDictionary<DimensionStyleOverrideType, DimensionStyleOverride>.Add(DimensionStyleOverrideType key, DimensionStyleOverride value)
        {
            this.Add(value);
        }

        void ICollection<KeyValuePair<DimensionStyleOverrideType, DimensionStyleOverride>>.Add(KeyValuePair<DimensionStyleOverrideType, DimensionStyleOverride> item)
        {
            this.Add(item.Value);
        }

        bool ICollection<KeyValuePair<DimensionStyleOverrideType, DimensionStyleOverride>>.Remove(KeyValuePair<DimensionStyleOverrideType, DimensionStyleOverride> item)
        {
            if (!ReferenceEquals(item.Value, this.innerDictionary[item.Key]))
                return false;
            return this.Remove(item.Key);
        }

        bool ICollection<KeyValuePair<DimensionStyleOverrideType, DimensionStyleOverride>>.Contains(KeyValuePair<DimensionStyleOverrideType, DimensionStyleOverride> item)
        {
            return ((IDictionary<DimensionStyleOverrideType, DimensionStyleOverride>) this.innerDictionary).Contains(item);
        }

        void ICollection<KeyValuePair<DimensionStyleOverrideType, DimensionStyleOverride>>.CopyTo(KeyValuePair<DimensionStyleOverrideType, DimensionStyleOverride>[] array, int arrayIndex)
        {
            ((IDictionary<DimensionStyleOverrideType, DimensionStyleOverride>) this.innerDictionary).CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}