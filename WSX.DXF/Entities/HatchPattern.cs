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
using System.Collections.Generic;
using System.IO;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a <see cref="Hatch">hatch</see> pattern style.
    /// </summary>
    public class HatchPattern :
        ICloneable
    {
        #region private fields

        private readonly string name;
        private readonly List<HatchPatternLineDefinition> lineDefinitions;
        private HatchStyle style;
        private HatchFillType fill;
        private HatchType type;
        private Vector2 origin;
        private double angle;
        private double scale;
        private string description;

        #endregion

        #region constructor

        public HatchPattern(string name)
            : this(name, null, string.Empty)
        {
        }

        public HatchPattern(string name, string description)
            : this(name, null, description)
        {
        }

        public HatchPattern(string name, IEnumerable<HatchPatternLineDefinition> lineDefinitions)
            : this(name, lineDefinitions, string.Empty)
        {
        }

        public HatchPattern(string name, IEnumerable<HatchPatternLineDefinition> lineDefinitions, string description)
        {
            this.name = string.IsNullOrEmpty(name) ? string.Empty : name;
            this.description = string.IsNullOrEmpty(description) ? string.Empty : description;
            this.style = HatchStyle.Normal;
            this.fill = this.name == "SOLID" ? HatchFillType.SolidFill : HatchFillType.PatternFill;
            this.type = HatchType.UserDefined;
            this.origin = Vector2.Zero;
            this.angle = 0.0;
            this.scale = 1.0;
            this.lineDefinitions = lineDefinitions == null ? new List<HatchPatternLineDefinition>() : new List<HatchPatternLineDefinition>(lineDefinitions);
        }

        #endregion

        #region predefined patterns

        public static HatchPattern Solid
        {
            get
            {
                HatchPattern pattern = new HatchPattern("SOLID", "Solid fill") {type = HatchType.Predefined};
                // this is the pattern line definition for solid fills as defined in the acad.pat, but it is not needed
                //HatchPatternLineDefinition lineDefinition = new HatchPatternLineDefinition
                //                                                {
                //                                                    Angle = 45,
                //                                                    Origin = Vector2.Zero,
                //                                                    Delta = new Vector2(0.0, 0.125)
                //                                                };
                //pattern.LineDefinitions.Add(lineDefinition);
                return pattern;
            }
        }

        public static HatchPattern Line
        {
            get
            {
                HatchPattern pattern = new HatchPattern("LINE", "Parallel horizontal lines");
                HatchPatternLineDefinition lineDefinition = new HatchPatternLineDefinition
                {
                    Angle = 0,
                    Origin = Vector2.Zero,
                    Delta = new Vector2(0.0, 0.125)
                };
                pattern.LineDefinitions.Add(lineDefinition);
                pattern.type = HatchType.Predefined;
                return pattern;
            }
        }

        public static HatchPattern Net
        {
            get
            {
                HatchPattern pattern = new HatchPattern("NET", "Horizontal / vertical grid");

                HatchPatternLineDefinition lineDefinition = new HatchPatternLineDefinition
                {
                    Angle = 0,
                    Origin = Vector2.Zero,
                    Delta = new Vector2(0.0, 0.125)
                };
                pattern.LineDefinitions.Add(lineDefinition);

                lineDefinition = new HatchPatternLineDefinition
                {
                    Angle = 90,
                    Origin = Vector2.Zero,
                    Delta = new Vector2(0.0, 0.125)
                };
                pattern.LineDefinitions.Add(lineDefinition);
                pattern.type = HatchType.Predefined;
                return pattern;
            }
        }

        public static HatchPattern Dots
        {
            get
            {
                HatchPattern pattern = new HatchPattern("DOTS", "A series of dots");
                HatchPatternLineDefinition lineDefinition = new HatchPatternLineDefinition
                {
                    Angle = 0,
                    Origin = Vector2.Zero,
                    Delta = new Vector2(0.03125, 0.0625),
                };
                lineDefinition.DashPattern.AddRange(new[] {0, -0.0625});
                pattern.LineDefinitions.Add(lineDefinition);
                pattern.type = HatchType.Predefined;
                return pattern;
            }
        }

        #endregion

        #region public properties

        public string Name
        {
            get { return this.name; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public HatchStyle Style
        {
            get { return this.style; }
            internal set { this.style = value; }
        }

        public HatchType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public HatchFillType Fill
        {
            get { return this.fill; }
            internal set { this.fill = value; }
        }

        public Vector2 Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
        }

        public double Angle
        {
            get { return this.angle; }
            set { this.angle = MathHelper.NormalizeAngle(value); }
        }

        public double Scale
        {
            get { return this.scale; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The scale can not be zero or less.");
                this.scale = value;
            }
        }

        public List<HatchPatternLineDefinition> LineDefinitions
        {
            get { return this.lineDefinitions; }
        }

        #endregion

        #region public methods

        public static HatchPattern FromFile(string file, string patternName)
        {
            HatchPattern pattern = null;

            using (StreamReader reader = new StreamReader(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), true))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        throw new FileLoadException("Unknown error reading pat file.", file);
                    // lines starting with semicolons are comments
                    if (line.StartsWith(";"))
                        continue;
                    // every pattern definition starts with '*'
                    if (!line.StartsWith("*"))
                        continue;

                    // reading pattern name and description
                    int endName = line.IndexOf(','); // the first semicolon divides the name from the description that might contain more semicolons
                    string name = line.Substring(1, endName - 1);
                    string description = line.Substring(endName + 1, line.Length - endName - 1);

                    // remove start and end spaces
                    description = description.Trim();
                    if (!name.Equals(patternName, StringComparison.OrdinalIgnoreCase))
                        continue;

                    // we have found the pattern name, the next lines of the file contains the pattern definition
                    line = reader.ReadLine();
                    if (line == null)
                        throw new FileLoadException("Unknown error reading pat file.", file);
                    pattern = new HatchPattern(name, description);

                    while (!reader.EndOfStream && !line.StartsWith("*") && !string.IsNullOrEmpty(line))
                    {
                        string[] tokens = line.Split(',');
                        double angle = double.Parse(tokens[0]);
                        Vector2 origin = new Vector2(double.Parse(tokens[1]), double.Parse(tokens[2]));
                        Vector2 delta = new Vector2(double.Parse(tokens[3]), double.Parse(tokens[4]));

                        HatchPatternLineDefinition lineDefinition = new HatchPatternLineDefinition
                        {
                            Angle = angle,
                            Origin = origin,
                            Delta = delta,
                        };

                        // the rest of the info is optional if it exists define the dash pattern definition
                        for (int i = 5; i < tokens.Length; i++)
                            lineDefinition.DashPattern.Add(double.Parse(tokens[i]));

                        pattern.LineDefinitions.Add(lineDefinition);
                        pattern.Type = HatchType.UserDefined;
                        line = reader.ReadLine();
                        if (line == null)
                            throw new FileLoadException("Unknown error reading pat file.", file);
                        line = line.Trim();
                    }
                    // there is no need to continue parsing the file, the info has been read
                    break;
                }
            }

            return pattern;
        }

        #endregion

        #region ICloneable

        public virtual object Clone()
        {
            HatchPattern copy = new HatchPattern(this.name, this.description)
            {
                Style = this.style,
                Fill = this.fill,
                Type = this.type,
                Origin = this.origin,
                Angle = this.angle,
                Scale = this.scale,
            };

            foreach (HatchPatternLineDefinition def in this.lineDefinitions)
                copy.LineDefinitions.Add((HatchPatternLineDefinition) def.Clone());

            return copy;
        }

        #endregion
    }
}