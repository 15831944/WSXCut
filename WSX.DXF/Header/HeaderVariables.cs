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
using System.Collections.Generic;
using System.Text;
using WSX.DXF.Entities;
using WSX.DXF.Units;

namespace WSX.DXF.Header
{
    /// <summary>
    /// Represents the header variables of a dxf document.
    /// </summary>
    /// <remarks>
    /// The names of header variables are the same as they appear in the official dxf documentation but without the $.
    /// </remarks>
    public class HeaderVariables
    {
        #region private fields

        private readonly Dictionary<string, HeaderVariable> variables;

        #endregion

        #region constructors

        public HeaderVariables()
        {
            this.variables = new Dictionary<string, HeaderVariable>
            {
                {HeaderVariableCode.AcadVer, new HeaderVariable(HeaderVariableCode.AcadVer, DxfVersion.AutoCad2000)},
                {HeaderVariableCode.DwgCodePage, new HeaderVariable(HeaderVariableCode.DwgCodePage, "ANSI_" + Encoding.Default.WindowsCodePage)},
                {HeaderVariableCode.LastSavedBy, new HeaderVariable(HeaderVariableCode.LastSavedBy, Environment.UserName)},
                {HeaderVariableCode.HandleSeed, new HeaderVariable(HeaderVariableCode.HandleSeed, "1")},
                {HeaderVariableCode.Angbase, new HeaderVariable(HeaderVariableCode.Angbase, 0.0)},
                {HeaderVariableCode.Angdir, new HeaderVariable(HeaderVariableCode.Angdir, AngleDirection.CCW)},
                {HeaderVariableCode.AttMode, new HeaderVariable(HeaderVariableCode.AttMode, AttMode.Normal)},
                {HeaderVariableCode.AUnits, new HeaderVariable(HeaderVariableCode.AUnits, AngleUnitType.DecimalDegrees)},
                {HeaderVariableCode.AUprec, new HeaderVariable(HeaderVariableCode.AUprec, (short) 0)},
                {HeaderVariableCode.CeColor, new HeaderVariable(HeaderVariableCode.CeColor, AciColor.ByLayer)},
                {HeaderVariableCode.CeLtScale, new HeaderVariable(HeaderVariableCode.CeLtScale, 1.0)},
                {HeaderVariableCode.CeLtype, new HeaderVariable(HeaderVariableCode.CeLtype, "ByLayer")},
                {HeaderVariableCode.CeLweight, new HeaderVariable(HeaderVariableCode.CeLweight, Lineweight.ByLayer)},
                {HeaderVariableCode.CLayer, new HeaderVariable(HeaderVariableCode.CLayer, "0")},
                {HeaderVariableCode.CMLJust, new HeaderVariable(HeaderVariableCode.CMLJust, MLineJustification.Top)},
                {HeaderVariableCode.CMLScale, new HeaderVariable(HeaderVariableCode.CMLScale, 20.0)},
                {HeaderVariableCode.CMLStyle, new HeaderVariable(HeaderVariableCode.CMLStyle, "Standard")},
                {HeaderVariableCode.DimStyle, new HeaderVariable(HeaderVariableCode.DimStyle, "Standard")},
                {HeaderVariableCode.ExtMax, new HeaderVariable(HeaderVariableCode.ExtMax, Vector3.NaN)},
                {HeaderVariableCode.ExtMin, new HeaderVariable(HeaderVariableCode.ExtMin, Vector3.NaN)},
                {HeaderVariableCode.TextSize, new HeaderVariable(HeaderVariableCode.TextSize, 2.5)},
                {HeaderVariableCode.TextStyle, new HeaderVariable(HeaderVariableCode.TextStyle, "Standard")},
                {HeaderVariableCode.LUnits, new HeaderVariable(HeaderVariableCode.LUnits, LinearUnitType.Decimal)},
                {HeaderVariableCode.LUprec, new HeaderVariable(HeaderVariableCode.LUprec, (short) 4)},
                {HeaderVariableCode.Extnames, new HeaderVariable(HeaderVariableCode.Extnames, true)},
                {HeaderVariableCode.InsBase, new HeaderVariable(HeaderVariableCode.InsBase, Vector3.Zero)},
                {HeaderVariableCode.InsUnits, new HeaderVariable(HeaderVariableCode.InsUnits, DrawingUnits.Unitless)},
                {HeaderVariableCode.LtScale, new HeaderVariable(HeaderVariableCode.LtScale, 1.0)},
                {HeaderVariableCode.LwDisplay, new HeaderVariable(HeaderVariableCode.LwDisplay, false)},
                {HeaderVariableCode.PdMode, new HeaderVariable(HeaderVariableCode.PdMode, PointShape.Dot)},
                {HeaderVariableCode.PdSize, new HeaderVariable(HeaderVariableCode.PdSize, 0.0)},
                {HeaderVariableCode.PLineGen, new HeaderVariable(HeaderVariableCode.PLineGen, (short) 0)},
                {HeaderVariableCode.PsLtScale, new HeaderVariable(HeaderVariableCode.PsLtScale, (short) 1)},
                {HeaderVariableCode.TdCreate, new HeaderVariable(HeaderVariableCode.TdCreate, DateTime.Now)},
                {HeaderVariableCode.TduCreate, new HeaderVariable(HeaderVariableCode.TduCreate, DateTime.UtcNow)},
                {HeaderVariableCode.TdUpdate, new HeaderVariable(HeaderVariableCode.TdUpdate, DateTime.Now)},
                {HeaderVariableCode.TduUpdate, new HeaderVariable(HeaderVariableCode.TduUpdate, DateTime.UtcNow)},
                {HeaderVariableCode.TdinDwg, new HeaderVariable(HeaderVariableCode.TdinDwg, new TimeSpan())},
                {HeaderVariableCode.UcsOrg, new HeaderVariable(HeaderVariableCode.UcsOrg, Vector3.Zero)},
                {HeaderVariableCode.UcsXDir, new HeaderVariable(HeaderVariableCode.UcsXDir, Vector3.UnitX)},
                {HeaderVariableCode.UcsYDir, new HeaderVariable(HeaderVariableCode.UcsYDir, Vector3.UnitY)}
            };
        }

        #endregion

        #region public properties

        /// <exception cref="NotSupportedException">Only AutoCad2000 and higher dxf versions are supported.</exception>
        public DxfVersion AcadVer
        {
            get { return (DxfVersion) this.variables[HeaderVariableCode.AcadVer].Value; }
            set
            {
                if (value < DxfVersion.AutoCad2000)
                        throw new NotSupportedException("Only AutoCad2000 and newer dxf versions are supported.");
                this.variables[HeaderVariableCode.AcadVer].Value = value;
            }
        }

        public string HandleSeed
        {
            get { return (string) this.variables[HeaderVariableCode.HandleSeed].Value; }
            internal set { this.variables[HeaderVariableCode.HandleSeed].Value = value; }
        }

        public double Angbase
        {
            get { return (double) this.variables[HeaderVariableCode.Angbase].Value; }
            internal set { this.variables[HeaderVariableCode.Angbase].Value = value; }
        }

        public AngleDirection Angdir
        {
            get { return (AngleDirection) this.variables[HeaderVariableCode.Angdir].Value; }
            internal set { this.variables[HeaderVariableCode.Angdir].Value = value; }
        }

        public AttMode AttMode
        {
            get { return (AttMode) this.variables[HeaderVariableCode.AttMode].Value; }
            set { this.variables[HeaderVariableCode.AttMode].Value = value; }
        }

        public AngleUnitType AUnits
        {
            get { return (AngleUnitType) this.variables[HeaderVariableCode.AUnits].Value; }
            set { this.variables[HeaderVariableCode.AUnits].Value = value; }
        }

        public short AUprec
        {
            get { return (short) this.variables[HeaderVariableCode.AUprec].Value; }
            set
            {
                if (value < 0 || value > 8)
                    throw new ArgumentOutOfRangeException(nameof(value), "Valid values are integers from 0 to 8.");
                this.variables[HeaderVariableCode.AUprec].Value = value;
            }
        }

        public AciColor CeColor
        {
            get { return (AciColor) this.variables[HeaderVariableCode.CeColor].Value; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.variables[HeaderVariableCode.CeColor].Value = value;
            }
        }

        public double CeLtScale
        {
            get { return (double) this.variables[HeaderVariableCode.CeLtScale].Value; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The current entity line type scale must be greater than zero.");
                this.variables[HeaderVariableCode.CeLtScale].Value = value;
            }
        }

        public Lineweight CeLweight
        {
            get { return (Lineweight) this.variables[HeaderVariableCode.CeLweight].Value; }
            set { this.variables[HeaderVariableCode.CeLweight].Value = value; }
        }

        public string CeLtype
        {
            get { return (string) this.variables[HeaderVariableCode.CeLtype].Value; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value), "The current entity line type name should be at least one character long.");
                this.variables[HeaderVariableCode.CeLtype].Value = value;
            }
        }

        public string CLayer
        {
            get { return (string) this.variables[HeaderVariableCode.CLayer].Value; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value), "The current layer name should be at least one character long.");
                this.variables[HeaderVariableCode.CLayer].Value = value;
            }
        }

        public MLineJustification CMLJust
        {
            get { return (MLineJustification) this.variables[HeaderVariableCode.CMLJust].Value; }
            set { this.variables[HeaderVariableCode.CMLJust].Value = value; }
        }

        public double CMLScale
        {
            get { return (double) this.variables[HeaderVariableCode.CMLScale].Value; }
            set { this.variables[HeaderVariableCode.CMLScale].Value = value; }
        }

        public string CMLStyle
        {
            get { return (string) this.variables[HeaderVariableCode.CMLStyle].Value; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value), "The current multiline style name should be at least one character long.");
                this.variables[HeaderVariableCode.CMLStyle].Value = value;
            }
        }

        public string DimStyle
        {
            get { return (string) this.variables[HeaderVariableCode.DimStyle].Value; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value), "The current dimension style name should be at least one character long.");
                this.variables[HeaderVariableCode.DimStyle].Value = value;
            }
        }

        public double TextSize
        {
            get { return (double) this.variables[HeaderVariableCode.TextSize].Value; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The default text height must be greater than zero.");
                this.variables[HeaderVariableCode.TextSize].Value = value;
            }
        }

        public string TextStyle
        {
            get { return (string) this.variables[HeaderVariableCode.TextStyle].Value; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value), "The current text style name should be at least one character long.");
                this.variables[HeaderVariableCode.TextStyle].Value = value;
            }
        }

        public LinearUnitType LUnits
        {
            get { return (LinearUnitType) this.variables[HeaderVariableCode.LUnits].Value; }
            set
            {
                if (value == LinearUnitType.Architectural || value == LinearUnitType.Engineering)
                    this.InsUnits = DrawingUnits.Inches;
                this.variables[HeaderVariableCode.LUnits].Value = value;
            }
        }

        public short LUprec
        {
            get { return (short) this.variables[HeaderVariableCode.LUprec].Value; }
            set
            {
                if (value < 0 || value > 8)
                    throw new ArgumentOutOfRangeException(nameof(value), "Valid values are integers from 0 to 8.");
                this.variables[HeaderVariableCode.LUprec].Value = value;
            }
        }

        public string DwgCodePage
        {
            get { return (string) this.variables[HeaderVariableCode.DwgCodePage].Value; }
            internal set { this.variables[HeaderVariableCode.DwgCodePage].Value = value; }
        }

        public Vector3 ExtMax
        {
            get { return (Vector3) this.variables[HeaderVariableCode.ExtMax].Value; }
            internal set { this.variables[HeaderVariableCode.ExtMax].Value = value; }
        }

        public Vector3 ExtMin
        {
            get { return (Vector3)this.variables[HeaderVariableCode.ExtMin].Value; }
            internal set { this.variables[HeaderVariableCode.ExtMin].Value = value; }
        }

        /// 0 = Release 14 compatibility. Limits names to 31 characters in length.<br />
        /// 1 = AutoCAD 2000.<br />
        public bool Extnames
        {
            get { return (bool) this.variables[HeaderVariableCode.Extnames].Value; }
            internal set { this.variables[HeaderVariableCode.Extnames].Value = value; }
        }

        public Vector3 InsBase
        {
            get { return (Vector3) this.variables[HeaderVariableCode.InsBase].Value; }
            set { this.variables[HeaderVariableCode.InsBase].Value = value; }
        }

        public DrawingUnits InsUnits
        {
            get { return (DrawingUnits) this.variables[HeaderVariableCode.InsUnits].Value; }
            set { this.variables[HeaderVariableCode.InsUnits].Value = value; }
        }

        public string LastSavedBy
        {
            get { return (string) this.variables[HeaderVariableCode.LastSavedBy].Value; }
            set { this.variables[HeaderVariableCode.LastSavedBy].Value = value; }
        }

        public double LtScale
        {
            get { return (double) this.variables[HeaderVariableCode.LtScale].Value; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The global line type scale must be greater than zero.");
                this.variables[HeaderVariableCode.LtScale].Value = value;
            }
        }

        public bool LwDisplay
        {
            get { return (bool) this.variables[HeaderVariableCode.LwDisplay].Value; }
            set { this.variables[HeaderVariableCode.LwDisplay].Value = value; }
        }

        public PointShape PdMode
        {
            get { return (PointShape) this.variables[HeaderVariableCode.PdMode].Value; }
            set { this.variables[HeaderVariableCode.PdMode].Value = value; }
        }

        public double PdSize
        {
            get { return (double) this.variables[HeaderVariableCode.PdSize].Value; }
            set { this.variables[HeaderVariableCode.PdSize].Value = value; }
        }

        /// 1 = Line type is generated in a continuous pattern around vertexes of the polyline.<br />
        /// 0 = Each segment of the polyline starts and ends with a dash.
        public short PLineGen
        {
            get { return (short) this.variables[HeaderVariableCode.PLineGen].Value; }
            set
            {
                if (value != 0 && value != 1)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Accepted values are 0 or 1.");
                this.variables[HeaderVariableCode.PLineGen].Value = value;
            }
        }

        /// 1 = No special line type scaling.<br />
        /// 0 = Viewport scaling governs line type scaling.
        public short PsLtScale
        {
            get { return (short) this.variables[HeaderVariableCode.PsLtScale].Value; }
            set
            {
                if (value != 0 && value != 1)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Accepted values are 0 or 1.");
                this.variables[HeaderVariableCode.PsLtScale].Value = value;
            }
        }

        public DateTime TdCreate
        {
            get { return (DateTime) this.variables[HeaderVariableCode.TdCreate].Value; }
            set { this.variables[HeaderVariableCode.TdCreate].Value = value; }
        }

        public DateTime TduCreate
        {
            get { return (DateTime) this.variables[HeaderVariableCode.TduCreate].Value; }
            set { this.variables[HeaderVariableCode.TduCreate].Value = value; }
        }

        public DateTime TdUpdate
        {
            get { return (DateTime) this.variables[HeaderVariableCode.TdUpdate].Value; }
            set { this.variables[HeaderVariableCode.TdUpdate].Value = value; }
        }

        public DateTime TduUpdate
        {
            get { return (DateTime) this.variables[HeaderVariableCode.TduUpdate].Value; }
            set { this.variables[HeaderVariableCode.TduUpdate].Value = value; }
        }

        public TimeSpan TdinDwg
        {
            get { return (TimeSpan) this.variables[HeaderVariableCode.TdinDwg].Value; }
            set { this.variables[HeaderVariableCode.TdinDwg].Value = value; }
        }

        public Vector3 UcsOrg
        {
            get { return (Vector3)this.variables[HeaderVariableCode.UcsOrg].Value; }
            set { this.variables[HeaderVariableCode.UcsOrg].Value = value; }
        }

        public Vector3 UcsXDir
        {
            get { return (Vector3)this.variables[HeaderVariableCode.UcsXDir].Value; }
            set { this.variables[HeaderVariableCode.UcsXDir].Value = value; }
        }

        public Vector3 UcsYDir
        {
            get { return (Vector3)this.variables[HeaderVariableCode.UcsYDir].Value; }
            set { this.variables[HeaderVariableCode.UcsYDir].Value = value; }
        }

        #endregion

        #region internal properties

        internal ICollection<HeaderVariable> Values
        {
            get { return this.variables.Values; }
        }

        #endregion
    }
}