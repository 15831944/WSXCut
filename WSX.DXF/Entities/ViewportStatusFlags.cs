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
    /// viewport status flags
    /// </summary>
    [Flags]
    public enum ViewportStatusFlags
    {
        PerspectiveMode = 1,

        FrontClipping = 2,

        BackClipping = 4,

        UcsFollow = 8,

        FrontClipNotAtEye = 16,

        UcsIconVisibility = 32,

        UcsIconAtOrigin = 64,

        FastZoom = 128,

        SnapMode = 256,

        GridMode = 512,

        IsometricSnapStyle = 1024,

        HidePlotMode = 2048,

        IsoPairTop = 4096,

        IsoPairRight = 8192,

        ViewportZoomLocking = 16384,

        CurrentlyAlwaysEnabled = 32768,

        NonRectangularClipping = 65536,

        ViewportOff = 131072,

        DisplayGridBeyondDrawingLimits = 262144,

        AdaptiveGridDisplay = 524288,

        SubdivisionGridBelowSpacing = 1048576
    }
}