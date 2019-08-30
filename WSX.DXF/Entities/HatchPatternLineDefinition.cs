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
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Defines a single line thats is part of a <see cref="HatchPattern">hatch pattern</see>.
    /// </summary>
    public class HatchPatternLineDefinition :
        ICloneable
    {
        #region private fields

        private double angle;
        private Vector2 origin;
        private Vector2 delta;
        private readonly List<double> dashPattern;

        #endregion

        #region constructor

        public HatchPatternLineDefinition()
        {
            this.angle = 0.0;
            this.origin = Vector2.Zero;
            this.delta = Vector2.Zero;
            this.dashPattern = new List<double>();
        }

        #endregion

        #region public properties

        public double Angle
        {
            get { return this.angle; }
            set { this.angle = MathHelper.NormalizeAngle(value); }
        }

        public Vector2 Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
        }

        public Vector2 Delta
        {
            get { return this.delta; }
            set { this.delta = value; }
        }

        public List<double> DashPattern
        {
            get { return this.dashPattern; }
        }

        #endregion

        #region overrides

        public object Clone()
        {
            HatchPatternLineDefinition copy = new HatchPatternLineDefinition
            {
                Angle = this.angle,
                Origin = this.origin,
                Delta = this.delta,
            };

            foreach (double dash in this.dashPattern)
                copy.DashPattern.Add(dash);

            return copy;
        }

        #endregion
    }
}