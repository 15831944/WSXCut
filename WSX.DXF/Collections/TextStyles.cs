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
    /// Represents a collection of text styles.
    /// </summary>
    public sealed class TextStyles :
        TableObjects<TextStyle>
    {
        #region constructor

        internal TextStyles(DxfDocument document)
            : this(document, null)
        {
        }

        internal TextStyles(DxfDocument document, string handle)
            : base(document, DxfObjectCode.TextStyleTable, handle)
        {
            this.MaxCapacity = short.MaxValue;
        }

        #endregion

        #region override methods

        internal override TextStyle Add(TextStyle style, bool assignHandle)
        {
            if (this.list.Count >= this.MaxCapacity)
                throw new OverflowException(string.Format("Table overflow. The maximum number of elements the table {0} can have is {1}", this.CodeName, this.MaxCapacity));
            if (style == null)
                throw new ArgumentNullException(nameof(style));

            TextStyle add;
            if (this.list.TryGetValue(style.Name, out add))
                return add;

            if (assignHandle || string.IsNullOrEmpty(style.Handle))
                this.Owner.NumHandles = style.AsignHandle(this.Owner.NumHandles);

            this.list.Add(style.Name, style);
            this.references.Add(style.Name, new List<DxfObject>());

            style.Owner = this;

            style.NameChanged += this.Item_NameChanged;

            this.Owner.AddedObjects.Add(style.Handle, style);

            return style;
        }

        public override bool Remove(string name)
        {
            return this.Remove(this[name]);
        }

        public override bool Remove(TextStyle item)
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

        #region TextStyle events

        private void Item_NameChanged(TableObject sender, TableObjectChangedEventArgs<string> e)
        {
            if (this.Contains(e.NewValue))
                throw new ArgumentException("There is already another text style with the same name.");

            this.list.Remove(sender.Name);
            this.list.Add(e.NewValue, (TextStyle) sender);

            List<DxfObject> refs = this.references[sender.Name];
            this.references.Remove(sender.Name);
            this.references.Add(e.NewValue, refs);
        }

        #endregion
    }
}