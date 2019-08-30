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

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a polyface mesh vertex. 
    /// </summary>
    public class PolyfaceMeshVertex :
        DxfObject, ICloneable
    {
        #region private fields

        private readonly VertexTypeFlags flags;
        private Vector3 location;

        #endregion

        #region constructors

        public PolyfaceMeshVertex()
            : this(Vector3.Zero)
        {
        }

        public PolyfaceMeshVertex(double x, double y, double z)
            : this(new Vector3(x, y, z))
        {
        }

        public PolyfaceMeshVertex(Vector3 location)
            : base(DxfObjectCode.Vertex)
        {
            this.flags = VertexTypeFlags.PolyfaceMeshVertex | VertexTypeFlags.Polygon3dMesh;
            this.location = location;
        }

        #endregion

        #region public properties

        public Vector3 Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        internal VertexTypeFlags Flags
        {
            get { return this.flags; }
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return string.Format("{0}: {1}", "PolyfaceMeshVertex", this.location);
        }

        public object Clone()
        {
            return new PolyfaceMeshVertex(this.location);
        }

        #endregion
    }
}