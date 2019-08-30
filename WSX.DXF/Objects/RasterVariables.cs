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

using WSX.DXF.Units;

namespace WSX.DXF.Objects
{
    /// <summary>
    /// Represents the variables applied to bitmaps.
    /// </summary>
    public class RasterVariables :
        DxfObject
    {
        #region private fields

        private bool displayFrame;
        private ImageDisplayQuality quality;
        private ImageUnits units;

        #endregion

        #region constructors

        public RasterVariables()
            : base(DxfObjectCode.RasterVariables)
        {
            this.displayFrame = true;
            this.quality = ImageDisplayQuality.High;
            this.units = ImageUnits.Unitless;
        }

        #endregion

        #region public properties

        public bool DisplayFrame
        {
            get { return this.displayFrame; }
            set { this.displayFrame = value; }
        }

        public ImageDisplayQuality DisplayQuality
        {
            get { return this.quality; }
            set { this.quality = value; }
        }

        public ImageUnits Units
        {
            get { return this.units; }
            set { this.units = value; }
        }

        #endregion
    }
}