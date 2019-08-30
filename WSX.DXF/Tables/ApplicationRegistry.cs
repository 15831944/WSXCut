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

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Represents a registered application name to which the <see cref="XData">extended data</see> is associated.
    /// </summary>
    /// <remarks>
    /// Do not use the default "ACAD" application registry name for your own extended data, it is sometimes used by AutoCad to store internal data.
    /// Instead, create your own application registry name and store your extended data there.
    /// </remarks>
    public class ApplicationRegistry :
        TableObject
    {
        #region constants

        public const string DefaultName = "ACAD";

        public static ApplicationRegistry Default
        {
            get { return new ApplicationRegistry(DefaultName); }
        }

        #endregion

        #region constructors

        public ApplicationRegistry(string name)
            : this(name, true)
        {
        }

        internal ApplicationRegistry(string name, bool checkName)
            : base(name, DxfObjectCode.AppId, checkName)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "The application registry name should be at least one character long.");

            this.IsReserved = name.Equals(DefaultName, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region public properties

        public new ApplicationRegistries Owner
        {
            get { return (ApplicationRegistries) base.Owner; }
            internal set { base.Owner = value; }
        }

        #endregion

        #region overrides

        public override TableObject Clone(string newName)
        {
            ApplicationRegistry copy = new ApplicationRegistry(newName);

            foreach (XData data in this.XData.Values)
                copy.XData.Add((XData)data.Clone());

            return copy ;
        }

        public override object Clone()
        {
            return this.Clone(this.Name);
        }

        #endregion
    }
}