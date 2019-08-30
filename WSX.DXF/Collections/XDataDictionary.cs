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
    /// Represents a dictionary of <see cref="XData">XData</see>.
    /// </summary>
    public sealed class XDataDictionary :
        IDictionary<string, XData>
    {
        #region delegates and events

        public delegate void AddAppRegEventHandler(XDataDictionary sender, ObservableCollectionEventArgs<ApplicationRegistry> e);
        public event AddAppRegEventHandler AddAppReg;
        private void OnAddAppRegEvent(ApplicationRegistry item)
        {
            AddAppRegEventHandler ae = this.AddAppReg;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<ApplicationRegistry>(item));
        }

        public delegate void RemoveAppRegEventHandler(XDataDictionary sender, ObservableCollectionEventArgs<ApplicationRegistry> e);
        public event RemoveAppRegEventHandler RemoveAppReg;
        private void OnRemoveAppRegEvent(ApplicationRegistry item)
        {
            RemoveAppRegEventHandler ae = this.RemoveAppReg;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<ApplicationRegistry>(item));
        }

        #endregion

        #region private fields

        private readonly Dictionary<string, XData> innerDictionary;

        #endregion

        #region constructor

        public XDataDictionary()
        {
            this.innerDictionary = new Dictionary<string, XData>(StringComparer.OrdinalIgnoreCase);
        }

        public XDataDictionary(IEnumerable<XData> items)
        {
            this.innerDictionary = new Dictionary<string, XData>(StringComparer.OrdinalIgnoreCase);
            this.AddRange(items);
        }

        public XDataDictionary(int capacity)
        {
            this.innerDictionary = new Dictionary<string, XData>(capacity, StringComparer.OrdinalIgnoreCase);
        }

        #endregion

        #region public properties

        public XData this[string appId]
        {
            get { return this.innerDictionary[appId]; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (!string.Equals(value.ApplicationRegistry.Name, appId, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException(string.Format("The extended data application registry name {0} must be equal to the specified appId {1}.", value.ApplicationRegistry.Name, appId));

                this.innerDictionary[appId] = value;
            }
        }

        public ICollection<string> AppIds
        {
            get { return this.innerDictionary.Keys; }
        }

        public ICollection<XData> Values
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

        public void Add(XData item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            XData xdata;
            if (this.innerDictionary.TryGetValue(item.ApplicationRegistry.Name, out xdata))
            {
                xdata.XDataRecord.AddRange(item.XDataRecord);
            }
            else
            {
                this.innerDictionary.Add(item.ApplicationRegistry.Name, item);
                item.ApplicationRegistry.NameChanged += this.ApplicationRegistry_NameChanged;
                this.OnAddAppRegEvent(item.ApplicationRegistry);
            }
        }

        public void AddRange(IEnumerable<XData> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (XData data in items)
            {
                this.Add(data);
            }
        }

        public bool Remove(string appId)
        {
            if (!this.innerDictionary.ContainsKey(appId))
                return false;
            XData xdata = this.innerDictionary[appId];
            xdata.ApplicationRegistry.NameChanged -= this.ApplicationRegistry_NameChanged;
            this.innerDictionary.Remove(appId);
            this.OnRemoveAppRegEvent(xdata.ApplicationRegistry);
            return true;
        }

        public void Clear()
        {
            string[] ids = new string[this.innerDictionary.Count];
            this.innerDictionary.Keys.CopyTo(ids, 0);
            foreach (string appId in ids)
            {
                this.Remove(appId);
            }
        }

        public bool ContainsAppId(string appId)
        {
            return this.innerDictionary.ContainsKey(appId);
        }

        public bool ContainsValue(XData value)
        {
            return this.innerDictionary.ContainsValue(value);
        }

        public bool TryGetValue(string appId, out XData value)
        {
            return this.innerDictionary.TryGetValue(appId, out value);
        }

        public IEnumerator<KeyValuePair<string, XData>> GetEnumerator()
        {
            return this.innerDictionary.GetEnumerator();
        }

        #endregion

        #region private properties

        ICollection<string> IDictionary<string, XData>.Keys
        {
            get { return this.innerDictionary.Keys; }
        }

        #endregion

        #region private methods

        bool IDictionary<string, XData>.ContainsKey(string tag)
        {
            return this.innerDictionary.ContainsKey(tag);
        }

        void IDictionary<string, XData>.Add(string key, XData value)
        {
            this.Add(value);
        }

        void ICollection<KeyValuePair<string, XData>>.Add(KeyValuePair<string, XData> item)
        {
            this.Add(item.Value);
        }

        bool ICollection<KeyValuePair<string, XData>>.Remove(KeyValuePair<string, XData> item)
        {
            if (ReferenceEquals(item.Value, this.innerDictionary[item.Key]) && this.Remove(item.Key))
                return true;
            return false;
        }

        bool ICollection<KeyValuePair<string, XData>>.Contains(KeyValuePair<string, XData> item)
        {
            return ((IDictionary<string, XData>) this.innerDictionary).Contains(item);
        }

        void ICollection<KeyValuePair<string, XData>>.CopyTo(KeyValuePair<string, XData>[] array, int arrayIndex)
        {
            ((IDictionary<string, XData>) this.innerDictionary).CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region ApplicationRegistry events

        private void ApplicationRegistry_NameChanged(TableObject sender, TableObjectChangedEventArgs<string> e)
        {
            XData xdata = this.innerDictionary[e.OldValue];
            this.innerDictionary.Remove(e.OldValue);
            this.innerDictionary.Add(e.NewValue, xdata);
        }

        #endregion
    }
}