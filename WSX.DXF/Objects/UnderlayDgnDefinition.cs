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

using System.IO;
using WSX.DXF.Collections;
using WSX.DXF.Tables;

namespace WSX.DXF.Objects
{
    /// <summary>
    /// Represents a DGN underlay definition.
    /// </summary>
    public class UnderlayDgnDefinition :
        UnderlayDefinition
    {
        #region private fields

        private string layout;

        #endregion

        #region constructor

        public UnderlayDgnDefinition(string file)
            : this(Path.GetFileNameWithoutExtension(file), file)
        {
        }

        public UnderlayDgnDefinition(string name, string file)
            : base(name, file, UnderlayType.DGN)
        {
            this.layout = "Model";
        }

        #endregion

        #region public properties

        public string Layout
        {
            get { return this.layout; }
            set { this.layout = value; }
        }

        public new UnderlayDgnDefinitions Owner
        {
            get { return (UnderlayDgnDefinitions)base.Owner; }
            internal set { base.Owner = value; }
        }

        #endregion

        #region overrides

        public override TableObject Clone(string newName)
        {
            UnderlayDgnDefinition copy = new UnderlayDgnDefinition(newName, this.File)
            {
                Layout = this.layout
            };

            foreach (XData data in this.XData.Values)
                copy.XData.Add((XData)data.Clone());

            return copy;
        }

        public override object Clone()
        {
            return this.Clone(this.Name);
        }

        #endregion
    }
}