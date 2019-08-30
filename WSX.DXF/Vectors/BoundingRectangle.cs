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

namespace WSX.DXF
{
    /// <summary>
    /// Represents an axis aligned bounding rectangle.
    /// </summary>
    public class BoundingRectangle
    {
        #region private fields

        private Vector2 min;
        private Vector2 max;

        #endregion

        #region constructors

        public BoundingRectangle(Vector2 center, double majorAxis, double minorAxis, double rotation)
        {
            double rot = rotation*MathHelper.DegToRad;
            double a = majorAxis*0.5*Math.Cos(rot);
            double b = minorAxis*0.5*Math.Sin(rot);
            double c = majorAxis*0.5*Math.Sin(rot);
            double d = minorAxis*0.5*Math.Cos(rot);

            double width = Math.Sqrt(a*a + b*b)*2;
            double height = Math.Sqrt(c*c + d*d)*2;
            this.min = new Vector2(center.X - width*0.5, center.Y - height*0.5);
            this.max = new Vector2(center.X + width*0.5, center.Y + height*0.5);
        }

        public BoundingRectangle(Vector2 center, double radius)
        {
            this.min = new Vector2(center.X - radius, center.Y - radius);
            this.max = new Vector2(center.X + radius, center.Y + radius);
        }

        public BoundingRectangle(Vector2 center, double width, double height)
        {
            this.min = new Vector2(center.X - width*0.5, center.Y - height*0.5);
            this.max = new Vector2(center.X + width*0.5, center.Y + height*0.5);
        }

        public BoundingRectangle(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        public BoundingRectangle(IEnumerable<Vector2> points)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = double.MinValue;
            double maxY = double.MinValue;

            bool any = false;
            foreach (Vector2 point in points)
            {
                any = true;
                if (minX > point.X)
                    minX = point.X;
                if (minY > point.Y)
                    minY = point.Y;
                if (maxX < point.X)
                    maxX = point.X;
                if (maxY < point.Y)
                    maxY = point.Y;
            }
            if (any)
            {
                this.min = new Vector2(minX, minY);
                this.max = new Vector2(maxX, maxY);
            }
            else
            {
                this.min = new Vector2(double.MinValue, double.MinValue);
                this.max = new Vector2(double.MaxValue, double.MaxValue);
            }
        }

        #endregion

        #region public properties

        public Vector2 Min
        {
            get { return this.min; }
            set { this.min = value; }
        }

        public Vector2 Max
        {
            get { return this.max; }
            set { this.max = value; }
        }

        public Vector2 Center
        {
            get { return (this.min + this.max)*0.5; }
        }

        public double Radius
        {
            get { return Vector2.Distance(this.min, this.max)*0.5; }
        }

        public double Width
        {
            get { return this.max.X - this.min.X; }
        }

        public double Height
        {
            get { return this.max.Y - this.min.Y; }
        }

        #endregion

        #region public methods

        public bool PointInside(Vector2 point)
        {
            return point.X >= this.min.X && point.X <= this.max.X && point.Y >= this.min.Y && point.Y <= this.max.Y;
        }

        public static BoundingRectangle Union(BoundingRectangle aabr1, BoundingRectangle aabr2)
        {
            if (aabr1 == null && aabr2 == null)
                return null;
            if (aabr1 == null)
                return aabr2;
            if (aabr2 == null)
                return aabr1;

            Vector2 min = new Vector2();
            Vector2 max = new Vector2();
            for (int i = 0; i < 2; i++)
            {
                if (aabr1.Min[i] <= aabr2.Min[i])
                    min[i] = aabr1.Min[i];
                else
                    min[i] = aabr2.Min[i];

                if (aabr1.Max[i] >= aabr2.Max[i])
                    max[i] = aabr1.Max[i];
                else
                    max[i] = aabr2.Max[i];
            }
            return new BoundingRectangle(min, max);
        }

        public static BoundingRectangle Union(IEnumerable<BoundingRectangle> rectangles)
        {
            if (rectangles == null)
                throw new ArgumentNullException(nameof(rectangles));

            BoundingRectangle rtnAABR = null;
            foreach (BoundingRectangle aabr in rectangles)
                rtnAABR = Union(rtnAABR, aabr);

            return rtnAABR;
        }

        #endregion
    }
}