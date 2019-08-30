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

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a tolerance, indicates the amount by which the geometric characteristic can deviate from a perfect form.
    /// </summary>
    public class ToleranceValue :
        ICloneable
    {
        #region private fields

        private bool showDiameterSymbol;
        private string tolerance;
        private ToleranceMaterialCondition materialCondition;

        #endregion

        #region constructors

        public ToleranceValue()
        {
            this.showDiameterSymbol = false;
            this.tolerance = string.Empty;
            this.materialCondition = ToleranceMaterialCondition.None;
        }

        public ToleranceValue(bool showDiameterSymbol, string value, ToleranceMaterialCondition materialCondition)
        {
            this.showDiameterSymbol = showDiameterSymbol;
            this.tolerance = value;
            this.materialCondition = materialCondition;
        }

        #endregion

        #region public properties

        public bool ShowDiameterSymbol
        {
            get { return this.showDiameterSymbol; }
            set { this.showDiameterSymbol = value; }
        }

        public string Value
        {
            get { return this.tolerance; }
            set { this.tolerance = value; }
        }

        public ToleranceMaterialCondition MaterialCondition
        {
            get { return this.materialCondition; }
            set { this.materialCondition = value; }
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return new ToleranceValue
            {
                ShowDiameterSymbol = this.showDiameterSymbol,
                Value = this.tolerance,
                MaterialCondition = this.materialCondition
            };
        }

        #endregion
    }
}