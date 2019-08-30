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

namespace WSX.DXF
{
    /// <summary>
    /// Represents the line weight of a layer or an entity.
    /// </summary>
    /// <remarks>The enum numeric value correspond to 1/100 mm.</remarks>
    public enum Lineweight
    {
        Default = -3,

        ByBlock = -2,

        ByLayer = -1,

        W0 = 0,

        W5 = 5,

        W9 = 9,

        W13 = 13,

        W15 = 15,

        W18 = 18,

        W20 = 20,

        W25 = 25,

        W30 = 30,

        W35 = 35,

        W40 = 40,

        W50 = 50,

        W53 = 53,

        W60 = 60,

        W70 = 70,

        W80 = 80,

        W90 = 90,

        W100 = 100,

        W106 = 106,

        W120 = 120,

        W140 = 140,

        W158 = 158,

        W200 = 200,

        W211 = 211
    }
}