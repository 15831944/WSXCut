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

namespace WSX.DXF.Header
{
    /// <summary>
    /// Defines the shape of the point entities.
    /// </summary>
    public enum PointShape
    {
        Dot = 0,

        Empty = 1,

        Plus = 2,

        Cross = 3,

        Line = 4,

        CircleDot = 32,

        CircleEmpty = 33,

        CirclePlus = 34,

        CircleCross = 35,

        CircleLine = 36,

        SquareDot = 64,

        SquareEmpty = 65,

        SquarePlus = 66,

        SquareCross = 67,

        SquareLine = 68,

        CircleSquareDot = 96,

        CircleSquareEmpty = 97,

        CircleSquarePlus = 98,

        CircleSquareCross = 99,

        CircleSquareLine = 100
    }
}