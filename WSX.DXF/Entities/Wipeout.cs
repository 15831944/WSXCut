#region WSX.DXF library, Copyright (C) 2009-2017 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2017 Daniel Carvajal (haplokuon@gmail.com)
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
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a wipeout <see cref="EntityObject">entity</see>.
    /// </summary>
    /// <remarks>
    /// The Wipeout dxf definition includes three variables for brightness, contrast, and fade but those variables have no effect; in AutoCad you cannot even change them.<br/>
    /// The Wipeout entity is related with the system variable WIPEOUTFRAME but this variable is not saved in a dxf.
    /// </remarks>
    public class Wipeout :
        EntityObject
    {
        #region private fields

        private ClippingBoundary clippingBoundary;
        private double elevation;

        #endregion

        #region constructors

        public Wipeout(double x, double y, double width, double height)
            : this(new ClippingBoundary(x, y, width, height))
        {
        }

        public Wipeout(Vector2 firstCorner, Vector2 secondCorner)
            : this(new ClippingBoundary(firstCorner, secondCorner))
        {
        }

        public Wipeout(IEnumerable<Vector2> vertexes)
            : this(new ClippingBoundary(vertexes))
        {
        }

        public Wipeout(ClippingBoundary clippingBoundary)
            : base(EntityType.Wipeout, DxfObjectCode.Wipeout)
        {
            if (clippingBoundary == null)
                throw new ArgumentNullException(nameof(clippingBoundary));
            this.clippingBoundary = clippingBoundary;
            this.elevation = 0.0;
        }

        #endregion

        #region public properties

        public ClippingBoundary ClippingBoundary
        {
            get { return this.clippingBoundary; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.clippingBoundary = value;
            }
        }

        public double Elevation
        {
            get { return this.elevation; }
            set { this.elevation = value; }
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            Wipeout entity = new Wipeout((ClippingBoundary) this.ClippingBoundary.Clone())
            {
                //EntityObject properties
                Layer = (Layer) this.Layer.Clone(),
                Linetype = (Linetype) this.Linetype.Clone(),
                Color = (AciColor) this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                IsVisible = this.IsVisible,
                //Wipeout properties
                Elevation = this.elevation
            };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}