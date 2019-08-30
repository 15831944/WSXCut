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
using System.Globalization;

namespace WSX.DXF
{
    /// <summary>
    /// Represents the transparency of a layer or an entity.
    /// </summary>
    /// <remarks>
    /// When the transparency of an entity is ByLayer the code 440 will not appear in the dxf,
    /// but for comparison purposes the ByLayer transparency is assigned a value of -1.
    /// </remarks>
    public class Transparency :
        ICloneable,
        IEquatable<Transparency>
    {
        #region private fields

        private short transparency;

        #endregion

        #region constants

        public static Transparency ByLayer
        {
            get { return new Transparency {transparency = -1}; }
        }

        public static Transparency ByBlock
        {
            get { return new Transparency {transparency = 100}; }
        }

        #endregion

        #region constructors

        public Transparency()
        {
            this.transparency = -1;
        }

        public Transparency(short value)
        {
            if (value < 0 || value > 90)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Accepted transparency values range from 0 to 90.");
            this.transparency = value;
        }

        #endregion

        #region public properties

        public bool IsByLayer
        {
            get { return this.transparency == -1; }
        }

        public bool IsByBlock
        {
            get { return this.transparency == 100; }
        }

        public short Value
        {
            get { return this.transparency; }
            set
            {
                if (value < 0 || value > 90)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Accepted transparency values range from 0 to 90.");
                this.transparency = value;
            }
        }

        #endregion

        #region public methods

        public static int ToAlphaValue(Transparency transparency)
        {
            if (transparency == null)
                throw new ArgumentNullException(nameof(transparency));

            byte alpha = (byte) (255*(100 - transparency.Value)/100.0);
            byte[] bytes = {alpha, 0, 0, 2};
            if (transparency.IsByBlock)
                bytes[3] = 1;
            return BitConverter.ToInt32(bytes, 0);
        }

        public static Transparency FromAlphaValue(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            short alpha = (short) (100 - (bytes[0]/255.0)*100);
            return FromCadIndex(alpha);
        }

        #endregion

        #region private methods

        private static Transparency FromCadIndex(short alpha)
        {
            if (alpha == -1)
                return ByLayer;
            if (alpha == 100)
                return ByBlock;

            return new Transparency(alpha);
        }

        #endregion

        #region implements ICloneable

        public object Clone()
        {
            return FromCadIndex(this.transparency);
        }

        #endregion

        #region implements IEquatable

        public bool Equals(Transparency other)
        {
            if (other == null)
                return false;

            return other.transparency == this.transparency;
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            if (this.transparency == -1)
                return "ByLayer";
            if (this.transparency == 100)
                return "ByBlock";

            return this.transparency.ToString(CultureInfo.CurrentCulture);
        }

        #endregion
    }
}