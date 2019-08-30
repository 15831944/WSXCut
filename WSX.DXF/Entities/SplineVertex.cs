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
    /// Represents a control point of a <see cref="Spline">spline</see>.
    /// </summary>
    public class SplineVertex :
        ICloneable
    {
        #region private fields

        private Vector3 position;
        private double weigth;

        #endregion

        #region constructors

        public SplineVertex(double x, double y, double z)
            : this(new Vector3(x, y, z), 1.0)
        {
        }

        public SplineVertex(double x, double y, double z, double w)
            : this(new Vector3(x, y, z), w)
        {
        }

        public SplineVertex(Vector2 position)
            : this(new Vector3(position.X, position.Y, 0.0), 1.0)
        {
        }

        public SplineVertex(Vector2 position, double weigth)
            : this(new Vector3(position.X, position.Y, 0.0), weigth)
        {
        }

        public SplineVertex(Vector3 position)
            : this(position, 1.0)
        {
        }

        public SplineVertex(Vector3 position, double weight)
        {
            if (weight <= 0)
                throw new ArgumentOutOfRangeException(nameof(weight), weight, "The spline vertex weight must be greater than zero.");
            this.position = position;
            this.weigth = weight;
        }

        #endregion

        #region public properties

        public Vector3 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public double Weigth
        {
            get { return this.weigth; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The spline vertex weight must be greater than zero.");
                this.weigth = value;
            }
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return string.Format("{0}: ({1}) w={2}", "SplineVertex", this.Position, this.Weigth);
        }

        public object Clone()
        {
            return new SplineVertex(this.position, this.weigth);
        }

        public string ToString(IFormatProvider provider)
        {
            return string.Format("{0}: ({1}) w={2}", "SplineVertex", this.Position.ToString(provider), this.Weigth.ToString(provider));
        }

        #endregion
    }
}