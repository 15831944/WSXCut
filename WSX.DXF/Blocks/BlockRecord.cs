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
using WSX.DXF.Collections;
using WSX.DXF.Objects;
using WSX.DXF.Tables;
using WSX.DXF.Units;

namespace WSX.DXF.Blocks
{
    /// <summary>
    /// Represent the record of a block in the tables section.
    /// </summary>
    public class BlockRecord :
        DxfObject,
        IHasXData
    {
        #region delegates and events

        //public delegate void XDataAddAppRegEventHandler(EntityObject sender, ObservableCollectionEventArgs<ApplicationRegistry> e);

        public event XDataAddAppRegEventHandler XDataAddAppReg;

        protected virtual void OnXDataAddAppRegEvent(ApplicationRegistry item)
        {
            XDataAddAppRegEventHandler ae = this.XDataAddAppReg;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<ApplicationRegistry>(item));
        }

        //public delegate void XDataRemoveAppRegEventHandler(EntityObject sender, ObservableCollectionEventArgs<ApplicationRegistry> e);

        public event XDataRemoveAppRegEventHandler XDataRemoveAppReg;

        protected virtual void OnXDataRemoveAppRegEvent(ApplicationRegistry item)
        {
            XDataRemoveAppRegEventHandler ae = this.XDataRemoveAppReg;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<ApplicationRegistry>(item));
        }

        #endregion

        #region private fields

        private string name;
        private Layout layout;
        private static DrawingUnits defaultUnits = DrawingUnits.Unitless;
        private DrawingUnits units;
        private bool allowExploding;
        private bool scaleUniformly;
        private readonly XDataDictionary xData;

        #endregion

        #region constructors

        internal BlockRecord(string name)
            : base(DxfObjectCode.BlockRecord)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            this.name = name;
            this.layout = null;
            this.units = DefaultUnits;
            this.allowExploding = true;
            this.scaleUniformly = false;
            this.xData = new XDataDictionary();
        }

        #endregion

        #region public properties

        public string Name
        {
            get { return this.name; }
            internal set { this.name = value; }
        }

        public Layout Layout
        {
            get { return this.layout; }
            internal set { this.layout = value; }
        }

        public DrawingUnits Units
        {
            get { return this.units; }
            set { this.units = value; }
        }

        public static DrawingUnits DefaultUnits
        {
            get { return defaultUnits; }
            set { defaultUnits = value; }
        }

        public bool AllowExploding
        {
            get { return this.allowExploding; }
            set { this.allowExploding = value; }
        }

        public bool ScaleUniformly
        {
            get { return this.scaleUniformly; }
            set { this.scaleUniformly = value; }
        }

        public new BlockRecords Owner
        {
            get { return (BlockRecords) base.Owner; }
            internal set { base.Owner = value; }
        }

        public bool IsForInternalUseOnly
        {
            get { return this.name.StartsWith("*"); }
        }

        public XDataDictionary XData
        {
            get { return this.xData; }
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}