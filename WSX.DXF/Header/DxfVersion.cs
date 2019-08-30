#region WSX.DXF library, Copyright (C) 2009-2017 Daniel Carvajal (haplokuon@gmail.com)

//                        WSX.DXF library
// Copyright (C) 2009-2017 Daniel Carvajal (haplokuon@gmail.com)
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
    /// The AutoCAD drawing database version number.
    /// </summary>
    public enum DxfVersion
    {
        [StringValue("Unknown")]
        Unknown = 0,

        [StringValue("AC1006")]
        AutoCad10 = 1,

        [StringValue("AC1009")]
        AutoCad12 = 2,

        [StringValue("AC1012")]
        AutoCad13 = 3,

        [StringValue("AC1014")]
        AutoCad14 = 4,

        [StringValue("AC1015")]
        AutoCad2000 = 5,

        [StringValue("AC1018")]
        AutoCad2004 = 6,

        [StringValue("AC1021")]
        AutoCad2007 = 7,

        [StringValue("AC1024")]
        AutoCad2010 = 8,

        [StringValue("AC1027")]
        AutoCad2013 = 9,

        [StringValue("AC1032")]
        AutoCad2018 = 10
    }
}