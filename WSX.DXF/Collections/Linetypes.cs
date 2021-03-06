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
using System.IO;
using WSX.DXF.Tables;

namespace WSX.DXF.Collections
{
    /// <summary>
    /// Represents a collection of line types.
    /// </summary>
    public sealed class Linetypes :
        TableObjects<Linetype>
    {
        #region constructor

        internal Linetypes(DxfDocument document)
            : this(document, null)
        {
        }

        internal Linetypes(DxfDocument document, string handle)
            : base(document, DxfObjectCode.LinetypeTable, handle)
        {
            this.MaxCapacity = short.MaxValue;
        }

        #endregion

        #region public methods

        public List<string> NamesFromFile(string file)
        {
            string f = this.Owner.SupportFolders.FindFile(file);
            if (string.IsNullOrEmpty(f))
                throw new FileNotFoundException("The file has not been found.", file);

            return Linetype.NamesFromFile(f);
        }

        public void AddFromFile(string file, bool reload)
        {
            string f = this.Owner.SupportFolders.FindFile(file);
            if(string.IsNullOrEmpty(f))
                throw new FileNotFoundException("The LIN file has not been found.", file);

            List<string> names = Linetype.NamesFromFile(f);
            foreach (string name in names)
            {
                this.AddFromFile(f, name, reload);
            }
        }

        public bool AddFromFile(string file, string linetypeName, bool reload)
        {
            Linetype linetype;
            string f = this.Owner.SupportFolders.FindFile(file);
            if (string.IsNullOrEmpty(f))
                throw new FileNotFoundException("The LIN file has not been found.", file);

            linetype = Linetype.Load(f, linetypeName);

            if (linetype == null) return false;

            Linetype existing;
            if (this.TryGetValue(linetype.Name, out existing))
            {
                if (!reload) return false;

                existing.Description = linetype.Description;
                existing.Segments.Clear();
                existing.Segments.AddRange(linetype.Segments);         
                return true;
            }

            this.Add(linetype);
            return true;
        }

        public void Save(string file, bool overwrite)
        {
            if(overwrite) File.Delete(file);
            foreach (Linetype lt in this.list.Values)
            {
                if(!lt.IsReserved)
                    lt.Save(file);
            }
        }

        # endregion

        #region override methods

        internal override Linetype Add(Linetype linetype, bool assignHandle)
        {
            if (this.list.Count >= this.MaxCapacity)
                throw new OverflowException(string.Format("Table overflow. The maximum number of elements the table {0} can have is {1}", this.CodeName, this.MaxCapacity));
            if (linetype == null)
                throw new ArgumentNullException(nameof(linetype));

            Linetype add;

            if (this.list.TryGetValue(linetype.Name, out add))
                return add;

            if (assignHandle || string.IsNullOrEmpty(linetype.Handle))
                this.Owner.NumHandles = linetype.AsignHandle(this.Owner.NumHandles);

            foreach (LinetypeSegment segment in linetype.Segments)
            {
                if(segment.Type == LinetypeSegmentType.Text)               
                {
                    LinetypeTextSegment textSegment = (LinetypeTextSegment) segment;
                    textSegment.Style = this.Owner.TextStyles.Add(textSegment.Style);
                    this.Owner.TextStyles.References[textSegment.Style.Name].Add(linetype);
                }
                if (segment.Type == LinetypeSegmentType.Shape)
                {
                    LinetypeShapeSegment shapeSegment = (LinetypeShapeSegment)segment;
                    shapeSegment.Style = this.Owner.ShapeStyles.Add(shapeSegment.Style);
                    this.Owner.ShapeStyles.References[shapeSegment.Style.Name].Add(linetype);
                    if(!shapeSegment.Style.ContainsShapeName(shapeSegment.Name))
                        throw new ArgumentException("The linetype contains a shape segment which style does not contain a shape with the stored name.", nameof(linetype));
                }
            }

            this.list.Add(linetype.Name, linetype);
            this.references.Add(linetype.Name, new List<DxfObject>());

            linetype.Owner = this;

            linetype.NameChanged += this.Item_NameChanged;
            linetype.LinetypeSegmentAdded += this.Linetype_SegmentAdded;
            linetype.LinetypeSegmentRemoved += this.Linetype_SegmentRemoved;
            linetype.LinetypeTextSegmentStyleChanged += this.Linetype_TextSegmentStyleChanged;

            this.Owner.AddedObjects.Add(linetype.Handle, linetype);

            return linetype;
        }

        public override bool Remove(string name)
        {
            return this.Remove(this[name]);
        }

        public override bool Remove(Linetype item)
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

        #region Linetype events

        private void Item_NameChanged(TableObject sender, TableObjectChangedEventArgs<string> e)
        {
            if (this.Contains(e.NewValue))
                throw new ArgumentException("There is already another line type with the same name.");

            this.list.Remove(sender.Name);
            this.list.Add(e.NewValue, (Linetype) sender);

            List<DxfObject> refs = this.references[sender.Name];
            this.references.Remove(sender.Name);
            this.references.Add(e.NewValue, refs);
        }

        private void Linetype_SegmentAdded(Linetype sender, LinetypeSegmentChangeEventArgs e)
        {
            if (e.Item.Type == LinetypeSegmentType.Text)
            {
                LinetypeTextSegment textSegment = (LinetypeTextSegment)e.Item;
                textSegment.Style = this.Owner.TextStyles.Add(textSegment.Style);
                //this.Owner.TextStyles.References[textSegment.Style.Name].Add(sender);
            }
            if (e.Item.Type == LinetypeSegmentType.Shape)
            {
                LinetypeShapeSegment shapeSegment = (LinetypeShapeSegment)e.Item;
                shapeSegment.Style = this.Owner.ShapeStyles.Add(shapeSegment.Style);
                //this.Owner.ShapeStyles.References[shapeSegment.Name].Add(sender);
            }
        }

        private void Linetype_SegmentRemoved(Linetype sender, LinetypeSegmentChangeEventArgs e)
        {
            if (e.Item.Type == LinetypeSegmentType.Text)
            {
                this.Owner.TextStyles.References[((LinetypeTextSegment)e.Item).Style.Name].Remove(sender);
            }
            if (e.Item.Type == LinetypeSegmentType.Shape)
            {
                this.Owner.ShapeStyles.References[((LinetypeShapeSegment)e.Item).Name].Remove(sender);
            }
        }

        private void Linetype_TextSegmentStyleChanged(Linetype sender, TableObjectChangedEventArgs<TextStyle> e)
        {
            this.Owner.TextStyles.References[e.OldValue.Name].Remove(sender);

            e.NewValue = this.Owner.TextStyles.Add(e.NewValue);
            this.Owner.TextStyles.References[e.NewValue.Name].Add(sender);
        }

        #endregion
    }
}