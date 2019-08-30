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
using Attribute = WSX.DXF.Entities.Attribute;

namespace WSX.DXF.Collections
{
    /// <summary>
    /// Represents a collection of <see cref="Entities.Attribute">Attributes</see>.
    /// </summary>
    public sealed class AttributeCollection :
        IReadOnlyList<Attribute>
    {
        #region private fields

        private readonly List<Attribute> innerArray;

        #endregion

        #region constructor

        public AttributeCollection()
        {
            this.innerArray = new List<Attribute>();
        }

        public AttributeCollection(IEnumerable<Attribute> attributes)
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));
            this.innerArray = new List<Attribute>(attributes);
        }

        #endregion

        #region public properties

        public int Count
        {
            get { return this.innerArray.Count; }
        }

        public static bool IsReadOnly
        {
            get { return true; }
        }

        public Attribute this[int index]
        {
            get { return this.innerArray[index]; }
        }

        #endregion

        #region public methods

        public bool Contains(Attribute item)
        {
            return this.innerArray.Contains(item);
        }

        public void CopyTo(Attribute[] array, int arrayIndex)
        {
            this.innerArray.CopyTo(array, arrayIndex);
        }

        public int IndexOf(Attribute item)
        {
            return this.innerArray.IndexOf(item);
        }

        public Attribute AttributeWithTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
                return null;
            foreach (Attribute att in this.innerArray)
            {
                if (att.Definition != null)
                    if (string.Equals(tag, att.Tag, StringComparison.OrdinalIgnoreCase))
                        return att;
            }

            return null;
        }

        public IEnumerator<Attribute> GetEnumerator()
        {
            return this.innerArray.GetEnumerator();
        }

        #endregion

        #region private methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}