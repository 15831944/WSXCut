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

using System.Collections.Generic;

namespace WSX.DXF.Objects
{
    internal class DictionaryObject :
        DxfObject
    {
        #region private fields

        private readonly Dictionary<string, string> entries;
        private bool isHardOwner;
        private DictionaryCloningFlags cloning;

        #endregion

        #region private fields

        internal DictionaryObject(DxfObject owner)
            : base(DxfObjectCode.Dictionary)
        {
            this.isHardOwner = false;
            this.cloning = DictionaryCloningFlags.KeepExisting;
            this.entries = new Dictionary<string, string>();
            this.Owner = owner;
        }

        #endregion

        #region public properties

        public Dictionary<string, string> Entries
        {
            get { return this.entries; }
        }

        public bool IsHardOwner
        {
            get { return this.isHardOwner; }
            set { this.isHardOwner = value; }
        }

        public DictionaryCloningFlags Cloning
        {
            get { return this.cloning; }
            set { this.cloning = value; }
        }

        #endregion
    }
}