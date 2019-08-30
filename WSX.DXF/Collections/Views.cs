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
using WSX.DXF.Tables;

namespace WSX.DXF.Collections
{
    /// <summary>
    /// Represents a collection of views.
    /// </summary>
    public sealed class Views :
        TableObjects<View>
    {
        #region constructor

        internal Views(DxfDocument document)
            : this(document, null)
        {
        }

        internal Views(DxfDocument document, string handle)
            : base(document, DxfObjectCode.ViewTable, handle)
        {
            this.MaxCapacity = short.MaxValue;
        }

        #endregion

        #region override methods

        internal override View Add(View view, bool assignHandle)
        {
            if (this.list.Count >= this.MaxCapacity)
                throw new OverflowException(string.Format("Table overflow. The maximum number of elements the table {0} can have is {1}", this.CodeName, this.MaxCapacity));
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            View add;
            if (this.list.TryGetValue(view.Name, out add))
                return add;

            if (assignHandle || string.IsNullOrEmpty(view.Handle))
                this.Owner.NumHandles = view.AsignHandle(this.Owner.NumHandles);

            this.list.Add(view.Name, view);
            this.references.Add(view.Name, new List<DxfObject>());

            view.Owner = this;

            view.NameChanged += this.Item_NameChanged;

            this.Owner.AddedObjects.Add(view.Handle, view);

            return view;
        }

        public override bool Remove(string name)
        {
            return this.Remove(this[name]);
        }

        public override bool Remove(View item)
        {
            if (item == null)
                return false;

            if (!this.Contains(item))
                return false;

            if (item.IsReserved)
                return false;

            if (this.references[item.Name].Count != 0)
                return false;

            this.Owner.AddedObjects.Remove(item.Handle);
            this.references.Remove(item.Name);
            this.list.Remove(item.Name);

            item.Handle = null;
            item.Owner = null;

            item.NameChanged -= this.Item_NameChanged;

            return true;
        }

        #endregion

        #region UCS events

        private void Item_NameChanged(TableObject sender, TableObjectChangedEventArgs<string> e)
        {
            if (this.Contains(e.NewValue))
                throw new ArgumentException("There is already another View with the same name.");

            this.list.Remove(sender.Name);
            this.list.Add(e.NewValue, (View) sender);

            List<DxfObject> refs = this.references[sender.Name];
            this.references.Remove(sender.Name);
            this.references.Add(e.NewValue, refs);
        }

        #endregion
    }
}