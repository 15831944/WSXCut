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
using WSX.DXF.Tables;

namespace WSX.DXF.Objects
{
    /// <summary>
    /// Represent each of the elements that make up a MLineStyle.
    /// </summary>
    public class MLineStyleElement :
        IComparable<MLineStyleElement>,
        ICloneable
    {
        #region delegates and events

        public delegate void LinetypeChangedEventHandler(MLineStyleElement sender, TableObjectChangedEventArgs<Linetype> e);

        public event LinetypeChangedEventHandler LinetypeChanged;

        protected virtual Linetype OnLinetypeChangedEvent(Linetype oldLinetype, Linetype newLinetype)
        {
            LinetypeChangedEventHandler ae = this.LinetypeChanged;
            if (ae != null)
            {
                TableObjectChangedEventArgs<Linetype> eventArgs = new TableObjectChangedEventArgs<Linetype>(oldLinetype, newLinetype);
                ae(this, eventArgs);
                return eventArgs.NewValue;
            }
            return newLinetype;
        }

        #endregion

        #region private fields

        private double offset;
        private AciColor color;
        private Linetype linetype;

        #endregion

        #region constructors

        public MLineStyleElement(double offset)
            : this(offset, AciColor.ByLayer, Linetype.ByLayer)
        {
        }

        public MLineStyleElement(double offset, AciColor color, Linetype linetype)
        {
            this.offset = offset;
            this.color = color;
            this.linetype = linetype;
        }

        #endregion

        #region public properties

        public double Offset
        {
            get { return this.offset; }
            set { this.offset = value; }
        }

        public AciColor Color
        {
            get { return this.color; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.color = value;
            }
        }

        public Linetype Linetype
        {
            get { return this.linetype; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.linetype = this.OnLinetypeChangedEvent(this.linetype, value);
            }
        }

        #endregion

        #region implements IComparable

        public int CompareTo(MLineStyleElement other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return this.offset.CompareTo(other.offset);
        }

        public override bool Equals(object other)
        {
            if (other == null)
                return false;

            if (this.GetType() != other.GetType())
                return false;

            return this.Equals((MLineStyleElement) other);
        }

        public bool Equals(MLineStyleElement other)
        {
            if (other == null)
                return false;

            return MathHelper.IsEqual(this.offset, other.offset);
        }

        public override int GetHashCode()
        {
            return this.Offset.GetHashCode();
        }

        //////////////public static bool operator ==(MLineStyleElement u, MLineStyleElement v)
        //{
        //    if (ReferenceEquals(u, null) && ReferenceEquals(v, null))
        //        return true;

        //    if (ReferenceEquals(u, null) || ReferenceEquals(v, null))
        //        return false;

        //    return MathHelper.IsEqual(u.Offset, v.Offset);
        //}

        //////////////public static bool operator !=(MLineStyleElement u, MLineStyleElement v)
        //{
        //    if (ReferenceEquals(u, null) && ReferenceEquals(v, null))
        //        return false;

        //    if (ReferenceEquals(u, null) || ReferenceEquals(v, null))
        //        return true;

        //    return !MathHelper.IsEqual(u.offset, v.offset);
        //}

        //////////////public static bool operator <(MLineStyleElement u, MLineStyleElement v)
        //{
        //    if (ReferenceEquals(u, null) || ReferenceEquals(v, null))
        //        return false;

        //    return u.offset.CompareTo(v.offset) < 0;
        //}

        //////////////public static bool operator >(MLineStyleElement u, MLineStyleElement v)
        //{
        //    if (ReferenceEquals(u, null) || ReferenceEquals(v, null))
        //        return false;

        //    return u.offset.CompareTo(v.offset) > 0;
        //}

        #endregion

        #region implements ICloneable

        public object Clone()
        {
            return new MLineStyleElement(this.offset)
            {
                Color = (AciColor) this.Color.Clone(),
                Linetype = (Linetype) this.linetype.Clone()
            };
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return string.Format("{0}, color:{1}, line type:{2}", this.offset, this.color, this.linetype);
        }

        #endregion
    }
}