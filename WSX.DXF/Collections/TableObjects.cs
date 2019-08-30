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
using System.Collections;
using System.Collections.Generic;
using WSX.DXF.Tables;

namespace WSX.DXF.Collections
{
    /// <summary>
    /// Represents a list of table objects
    /// </summary>
    /// <typeparam name="T"><see cref="TableObject">TableObject</see>.</typeparam>
    public abstract class TableObjects<T> :
        DxfObject,
        IEnumerable<T> where T : TableObject
    {
        #region private fields

        private int maxCapacity = int.MaxValue;
        protected readonly Dictionary<string, T> list;
        protected Dictionary<string, List<DxfObject>> references;

        #endregion

        #region constructor

        protected TableObjects(DxfDocument document, string codeName, string handle)
            : base(codeName)
        {
            this.list = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
            this.references = new Dictionary<string, List<DxfObject>>(StringComparer.OrdinalIgnoreCase);
            this.Owner = document;

            if (string.IsNullOrEmpty(handle))
                this.Owner.NumHandles = base.AsignHandle(this.Owner.NumHandles);
            else
                this.Handle = handle;

            this.Owner.AddedObjects.Add(this.Handle, this);
        }

        #endregion

        #region public properties

        public T this[string name]
        {
            get
            {
                T item;
                return this.list.TryGetValue(name, out item) ? item : null;
            }
        }

        public ICollection<T> Items
        {
            get { return this.list.Values; }
        }

        public ICollection<string> Names
        {
            get { return this.list.Keys; }
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public int MaxCapacity
        {
            get { return this.maxCapacity; }
            internal set { this.maxCapacity = value; }
        }

        public new DxfDocument Owner
        {
            get { return (DxfDocument) base.Owner; }
            internal set { base.Owner = value; }
        }

        #endregion

        #region internal properties

        internal Dictionary<string, List<DxfObject>> References
        {
            get { return this.references; }
        }

        #endregion

        #region public methods

        public List<DxfObject> GetReferences(string name)
        {
            if (!this.Contains(name))
                return new List<DxfObject>();
            return new List<DxfObject>(this.references[name]);
        }

        public List<DxfObject> GetReferences(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return this.GetReferences(item.Name);
        }

        public bool Contains(string name)
        {
            return this.list.ContainsKey(name);
        }

        public bool Contains(T item)
        {
            return this.list.ContainsValue(item);
        }

        public bool TryGetValue(string name, out T item)
        {
            return this.list.TryGetValue(name, out item);
        }

        public T Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return this.Add(item, true);
        }

        internal abstract T Add(T item, bool assignHandle);

        public abstract bool Remove(string name);

        public abstract bool Remove(T item);

        public void Clear()
        {
            string[] names = new string[this.list.Count];
            this.list.Keys.CopyTo(names, 0);
            foreach (string o in names)
                this.Remove(o);
        }

        #endregion

        #region implements IEnumerator<T>

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.Values.GetEnumerator();
        }

        #endregion
    }
}