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
using System.IO;
using WSX.DXF.Collections;

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Represent a shape style.
    /// </summary>
    public class ShapeStyle :
        TableObject
    {
        #region private fields

        private readonly string file;
        private readonly double size;
        private readonly double widthFactor;
        private readonly double obliqueAngle;

        #endregion

        #region constants

        public static ShapeStyle Default
        {
            get { return new ShapeStyle("LTYPESHP.SHX");}
        }

        #endregion

        #region constructors

        public ShapeStyle(string file)
            : this(Path.GetFileNameWithoutExtension(file), file, 0.0, 1.0, 0.0)
        {
        }

        public ShapeStyle(string name, string file)
            : this(name, file, 0.0, 1.0, 0.0)
        {
        }

        internal ShapeStyle(string name, string file, double size, double widthFactor, double obliqueAngle)
            : base(name, DxfObjectCode.TextStyle, true)
        {
            if (string.IsNullOrEmpty(file))
                throw new ArgumentNullException(nameof(file));
            this.file = file;
            this.size = size;
            this.widthFactor = widthFactor;
            this.obliqueAngle = obliqueAngle;
        }

        #endregion

        #region public properties

        public string File
        {
            get { return this.file; }
        }

        public double Size
        {
            get { return this.size; }
        }

        public double WidthFactor
        {
            get { return this.widthFactor; }
        }

        public double ObliqueAngle
        {
            get { return this.obliqueAngle; }
        }

        public new ShapeStyles Owner
        {
            get { return (ShapeStyles)base.Owner; }
            internal set { base.Owner = value; }
        }

        #endregion

        #region public methods

        public bool ContainsShapeName(string name)
        {
            string f = Path.ChangeExtension(this.file, "SHP");
            if (this.Owner != null)
                f = this.Owner.Owner.SupportFolders.FindFile(f);
            else
                if(!System.IO.File.Exists(f)) f = string.Empty;

            // we will look for the shape name in the SHP file         
            if (string.IsNullOrEmpty(f)) return false;

            using (StreamReader reader = new StreamReader(System.IO.File.Open(f, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), true))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        throw new FileLoadException("Unknown error reading SHP file.", f);
                    // lines starting with semicolons are comments
                    if (line.StartsWith(";"))
                        continue;
                    // every shape definition starts with '*'
                    if (!line.StartsWith("*"))
                        continue;

                    string[] tokens = line.TrimStart('*').Split(',');
                    if (string.Equals(name, tokens[2], StringComparison.InvariantCultureIgnoreCase))
                        return true; //the shape style that contains a shape with the specified name has been found
                }
            }
            // there are no shape styles that contain a shape with the specified name
            return false;
        }

        public short ShapeNumber(string name)
        {
            // we will look for the shape name in the SHP file
            string f = Path.ChangeExtension(this.file, "SHP");
            if (this.Owner != null)
                f = this.Owner.Owner.SupportFolders.FindFile(f);
            else
                if (!System.IO.File.Exists(f)) f = string.Empty;

            if (string.IsNullOrEmpty(f)) return 0;

            using (StreamReader reader = new StreamReader(System.IO.File.Open(f, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), true))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        throw new FileLoadException("Unknown error reading SHP file.", f);
                    // lines starting with semicolons are comments
                    if (line.StartsWith(";"))
                        continue;
                    // every shape definition starts with '*'
                    if (!line.StartsWith("*"))
                        continue;

                    string[] tokens = line.TrimStart('*').Split(',');
                    // the third item is the name of the shape
                    if (string.Equals(tokens[2], name, StringComparison.InvariantCultureIgnoreCase))
                        return short.Parse(tokens[0]);
                }
            }
            return 0;
        }

        public string ShapeName(short number)
        {
            // we will look for the shape name in the SHP file
            string f = Path.ChangeExtension(this.file, "SHP");
            if (this.Owner != null)
                f = this.Owner.Owner.SupportFolders.FindFile(f);
            else
                if (!System.IO.File.Exists(f)) f = string.Empty;

            if (string.IsNullOrEmpty(f)) return string.Empty;

            using (StreamReader reader = new StreamReader(System.IO.File.Open(f, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), true))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        throw new FileLoadException("Unknown error reading SHP file.", f);
                    // lines starting with semicolons are comments
                    if (line.StartsWith(";"))
                        continue;
                    // every shape definition starts with '*'
                    if (!line.StartsWith("*"))
                        continue;

                    string[] tokens = line.TrimStart('*').Split(',');
                    // the first item is the number of the shape
                    if (short.Parse(tokens[0]) == number)
                        return tokens[2];
                }
            }

            return string.Empty;
        }

        #endregion

        #region overrides

        public override TableObject Clone(string newName)
        {
            ShapeStyle copy = new ShapeStyle(newName, this.file, this.size, this.widthFactor, this.obliqueAngle);

            foreach (XData data in this.XData.Values)
                copy.XData.Add((XData)data.Clone());

            return copy;
        }

        public override object Clone()
        {
            return this.Clone(this.Name);
        }

        #endregion
    }
}