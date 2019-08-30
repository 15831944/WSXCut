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

namespace WSX.DXF.Header
{
    /// <summary>
    /// Strings system variables
    /// </summary>
    internal static class HeaderVariableCode
    {
        public const string AcadVer = "$ACADVER";

        public const string HandleSeed = "$HANDSEED";

        public const string Angbase = "$ANGBASE";

        /// 1 = Clockwise angles, 0 = Counterclockwise.
        public const string Angdir = "$ANGDIR";

        public const string AttMode = "$ATTMODE";

        public const string AUnits = "$AUNITS";

        public const string AUprec = "$AUPREC";

        public const string CeColor = "$CECOLOR";

        public const string CeLtScale = "$CELTSCALE";

        public const string CeLweight = "$CELWEIGHT";

        public const string CeLtype = "$CELTYPE";

        public const string CLayer = "$CLAYER";

        public const string CMLJust = "$CMLJUST";

        public const string CMLScale = "$CMLSCALE";

        public const string CMLStyle = "$CMLSTYLE";

        public const string DimStyle = "$DIMSTYLE";

        public const string ExtMax = "$EXTMAX";

        public const string ExtMin = "$EXTMIN";

        public const string TextSize = "$TEXTSIZE";

        public const string TextStyle = "$TEXTSTYLE";

        public const string LUnits = "$LUNITS";

        public const string LUprec = "$LUPREC";

        public const string DwgCodePage = "$DWGCODEPAGE";

        /// 0 = Release 14 compatibility. Limits names to 31 characters in length.<br />
        /// 1 = AutoCAD 2000.<br />
        public const string Extnames = "$EXTNAMES";

        public const string InsBase = "$INSBASE";

        public const string InsUnits = "$INSUNITS";

        public const string LastSavedBy = "$LASTSAVEDBY";

        /// 0 = Lineweight is not displayed
        /// 1 = Lineweight is displayed
        public const string LwDisplay = "$LWDISPLAY";

        public const string LtScale = "$LTSCALE";

        public const string PdMode = "$PDMODE";

        public const string PdSize = "$PDSIZE";

        /// 1 = Line type is generated in a continuous pattern around vertexes of the polyline.<br />
        /// 0 = Each segment of the polyline starts and ends with a dash.
        public const string PLineGen = "$PLINEGEN";

        /// 1 = No special line type scaling.<br />
        /// 0 = Viewport scaling governs line type scaling.
        public const string PsLtScale = "$PSLTSCALE";

        public const string TdCreate = "$TDCREATE";

        public const string TduCreate = "$TDUCREATE";

        public const string TdUpdate = "$TDUPDATE";

        public const string TduUpdate = "$TDUUPDATE";

        public const string TdinDwg = "$TDINDWG";

        public const string UcsOrg = "$UCSORG";

        public const string UcsXDir = "$UCSXDIR";

        public const string UcsYDir = "$UCSYDIR";
    }
}