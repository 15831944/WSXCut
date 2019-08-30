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
using WSX.DXF.Collections;

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Represents a document viewport.
    /// </summary>
    public class VPort :
        TableObject
    {
        #region private fields

        private Vector2 center;
        private Vector2 snapBasePoint;
        private Vector2 snapSpacing;
        private Vector2 gridSpacing;
        private Vector3 direction;
        private Vector3 target;
        private double height;
        private double aspectRatio;
        private bool showGrid;
        private bool snapMode;

        #endregion

        #region constants

        public const string DefaultName = "*Active";

        public static VPort Active
        {
            get { return new VPort(DefaultName, false); }
        }

        #endregion

        #region constructors

        public VPort(string name)
            : this(name, true)
        {
        }

        internal VPort(string name, bool checkName)
            : base(name, DxfObjectCode.VPort, checkName)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "The viewport name should be at least one character long.");

            this.IsReserved = name.Equals("*Active", StringComparison.OrdinalIgnoreCase);
            this.center = Vector2.Zero;
            this.snapBasePoint = Vector2.Zero;
            this.snapSpacing = new Vector2(0.5);
            this.gridSpacing = new Vector2(10.0);
            this.target = Vector3.Zero;
            this.direction = Vector3.UnitZ;
            this.height = 10;
            this.aspectRatio = 1.0;
            this.showGrid = true;
            this.snapMode = false;
        }

        #endregion

        #region public properties

        public Vector2 ViewCenter
        {
            get { return this.center; }
            set { this.center = value; }
        }

        public Vector2 SnapBasePoint
        {
            get { return this.snapBasePoint; }
            set { this.snapBasePoint = value; }
        }

        public Vector2 SnapSpacing
        {
            get { return this.snapSpacing; }
            set { this.snapSpacing = value; }
        }

        public Vector2 GridSpacing
        {
            get { return this.gridSpacing; }
            set { this.gridSpacing = value; }
        }

        public Vector3 ViewDirection
        {
            get { return this.direction; }
            set
            {
                this.direction = Vector3.Normalize(value);
                if (Vector3.IsNaN(this.direction))
                    throw new ArgumentException("The direction can not be the zero vector.", nameof(value));
            }
        }

        public Vector3 ViewTarget
        {
            get { return this.target; }
            set { this.target = value; }
        }

        public double ViewHeight
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public double ViewAspectRatio
        {
            get { return this.aspectRatio; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                        "The VPort aspect ratio must be greater than zero.");
                this.aspectRatio = value;
            }
        }

        public bool ShowGrid
        {
            get { return this.showGrid; }
            set { this.showGrid = value; }
        }

        public bool SnapMode
        {
            get { return this.snapMode; }
            set { this.snapMode = value; }
        }

        public new VPorts Owner
        {
            get { return (VPorts) base.Owner; }
            internal set { base.Owner = value; }
        }

        #endregion

        #region overrides

        public override TableObject Clone(string newName)
        {
            VPort copy = new VPort(newName)
            {
                ViewCenter = this.center,
                SnapBasePoint = this.snapBasePoint,
                SnapSpacing = this.snapSpacing,
                GridSpacing = this.gridSpacing,
                ViewTarget = this.target,
                ViewDirection = this.direction,
                ViewHeight = this.height,
                ViewAspectRatio = this.aspectRatio,
                ShowGrid = this.showGrid
            };

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