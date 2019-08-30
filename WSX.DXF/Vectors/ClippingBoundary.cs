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
using System.Collections.Generic;
using WSX.DXF.Entities;

namespace WSX.DXF
{
    /// <summary>
    /// Represent a clipping boundary to display specific portions of
    /// an <see cref="Image">Image</see>,
    /// an <see cref="Underlay">Underlay</see>,
    /// or a <see cref="Wipeout">Wipeout</see>.
    /// </summary>
    public class ClippingBoundary :
        ICloneable
    {
        #region private fields

        private readonly ClippingBoundaryType type;
        private readonly IReadOnlyList<Vector2> vertexes;

        #endregion

        #region constructors

        public ClippingBoundary(double x, double y, double width, double height)
        {
            this.type = ClippingBoundaryType.Rectangular;
            this.vertexes = new List<Vector2> {new Vector2(x, y), new Vector2(x + width, y + height)};
        }

        public ClippingBoundary(Vector2 firstCorner, Vector2 secondCorner)
        {
            this.type = ClippingBoundaryType.Rectangular;
            this.vertexes = new List<Vector2> {firstCorner, secondCorner};
        }

        public ClippingBoundary(IEnumerable<Vector2> vertexes)
        {
            this.type = ClippingBoundaryType.Polygonal;
            this.vertexes = new List<Vector2>(vertexes);
            if (this.vertexes.Count < 3)
                throw new ArgumentOutOfRangeException(nameof(vertexes), this.vertexes.Count, "The number of vertexes for the polygonal clipping boundary must be equal or greater than three.");
        }

        #endregion

        #region public properties

        public ClippingBoundaryType Type
        {
            get { return this.type; }
        }

        public IReadOnlyList<Vector2> Vertexes
        {
            get { return this.vertexes; }
        }

        #endregion

        #region overrides

        public object Clone()
        {
            return this.type == ClippingBoundaryType.Rectangular ? new ClippingBoundary(this.vertexes[0], this.vertexes[1]) : new ClippingBoundary(this.vertexes);
        }

        #endregion
    }
}