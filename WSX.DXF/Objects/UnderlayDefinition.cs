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

using WSX.DXF.Tables;

namespace WSX.DXF.Objects
{
    /// <summary>
    /// Represents an underlay definition.
    /// </summary>
    public abstract class UnderlayDefinition :
        TableObject
    {
        #region private fields

        private readonly UnderlayType type;
        private readonly string file;

        #endregion

        #region constructor

        protected UnderlayDefinition(string name, string file, UnderlayType type)
            : base(name, DxfObjectCode.UnderlayDefinition, false)
        {
            this.file = file;
            this.type = type;
            switch (type)
            {
                case UnderlayType.DGN:
                    this.CodeName = DxfObjectCode.UnderlayDgnDefinition;
                    break;
                case UnderlayType.DWF:
                    this.CodeName = DxfObjectCode.UnderlayDwfDefinition;
                    break;
                case UnderlayType.PDF:
                    this.CodeName = DxfObjectCode.UnderlayPdfDefinition;
                    break;
            }
        }

        #endregion

        #region public properties

        public UnderlayType Type
        {
            get { return this.type; }
        }

        public string File
        {
            get { return this.file; }
        }

        #endregion
    }
}