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

namespace WSX.DXF.Objects
{
    /// <summary>
    /// Defines the plot settings flag.
    /// </summary>
    /// <remarks>Bit flag.</remarks>
    [Flags]
    public enum PlotFlags
    {
        PlotViewportBorders = 1,

        ShowPlotStyles = 2,

        PlotCentered = 4,

        PlotHidden = 8,

        UseStandardScale = 16,

        PlotPlotStyles = 32,

        ScaleLineweights = 64,

        PrintLineweights = 128,

        DrawViewportsFirst = 512,

        ModelType = 1024,

        UpdatePaper = 2048,

        ZoomToPaperOnUpdate = 4096,

        Initializing = 8192,

        PrevPlotInit = 16384
    }
}