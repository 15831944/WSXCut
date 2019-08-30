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

using System.Collections.Generic;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a <see cref="MLine">multiline</see> vertex.
    /// </summary>
    public class MLineVertex
    {
        #region private fields

        private Vector2 location;
        private readonly Vector2 direction;
        private readonly Vector2 miter;
        private readonly List<double>[] distances;

        #endregion

        #region constructors

        internal MLineVertex(Vector2 location, Vector2 direction, Vector2 miter, List<double>[] distances)
        {
            this.location = location;
            this.direction = direction;
            this.miter = miter;
            this.distances = distances;
        }

        #endregion

        #region public properties

        public Vector2 Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        public Vector2 Direction
        {
            get { return this.direction; }
        }

        public Vector2 Miter
        {
            get { return this.miter; }
        }

        public List<double>[] Distances
        {
            get { return this.distances; }
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return string.Format("{0}: ({1})", "MLineVertex", this.location);
        }

        public object Clone()
        {
            List<double>[] copyDistances = new List<double>[this.distances.Length];
            for (int i = 0; i < this.distances.Length; i++)
            {
                copyDistances[i] = new List<double>();
                copyDistances[i].AddRange(this.distances[i]);
            }
            return new MLineVertex(this.location, this.direction, this.miter, copyDistances);
        }

        #endregion
    }
}