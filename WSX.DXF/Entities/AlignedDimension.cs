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
using WSX.DXF.Blocks;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a dimension <see cref="EntityObject">entity</see> that is aligned the reference line.
    /// </summary>
    public class AlignedDimension :
        Dimension
    {
        #region private fields

        private Vector2 firstRefPoint;
        private Vector2 secondRefPoint;
        private double offset;

        #endregion

        #region constructors

        public AlignedDimension()
            : this(Vector2.Zero, Vector2.UnitX, 0.1)
        {
        }

        public AlignedDimension(Line referenceLine, double offset)
            : this(referenceLine, offset, Vector3.UnitZ, DimensionStyle.Default)
        {
        }

        public AlignedDimension(Line referenceLine, double offset, DimensionStyle style)
            : this(referenceLine, offset, Vector3.UnitZ, style)
        {
        }

        public AlignedDimension(Line referenceLine, double offset, Vector3 normal)
            : this(referenceLine, offset, normal, DimensionStyle.Default)
        {
        }

        public AlignedDimension(Line referenceLine, double offset, Vector3 normal, DimensionStyle style)
            : base(DimensionType.Aligned)
        {
            if (referenceLine == null)
                throw new ArgumentNullException(nameof(referenceLine));

            IList<Vector3> ocsPoints = MathHelper.Transform(
                new List<Vector3> { referenceLine.StartPoint, referenceLine.EndPoint }, normal, CoordinateSystem.World, CoordinateSystem.Object);
            this.firstRefPoint = new Vector2(ocsPoints[0].X, ocsPoints[0].Y);
            this.secondRefPoint = new Vector2(ocsPoints[1].X, ocsPoints[1].Y);

            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "The offset value must be equal or greater than zero.");
            this.offset = offset;
            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.Style = style;
            this.Normal = normal;
            this.Elevation = ocsPoints[0].Z;
            this.Update();
        }

        public AlignedDimension(Vector2 firstPoint, Vector2 secondPoint, double offset)
            : this(firstPoint, secondPoint, offset, DimensionStyle.Default)
        {
        }

        public AlignedDimension(Vector2 firstPoint, Vector2 secondPoint, double offset, DimensionStyle style)
            : base(DimensionType.Aligned)
        {
            this.firstRefPoint = firstPoint;
            this.secondRefPoint = secondPoint;
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "The offset value must be equal or greater than zero.");
            this.offset = offset;
            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.Style = style;
            this.Update();
        }

        #endregion

        #region public properties

        public Vector2 FirstReferencePoint
        {
            get { return this.firstRefPoint; }
            set { this.firstRefPoint = value; }
        }

        public Vector2 SecondReferencePoint
        {
            get { return this.secondRefPoint; }
            set { this.secondRefPoint = value; }
        }

        public Vector2 DimLinePosition
        {
            get { return this.defPoint; }
        }

        public double Offset
        {
            get { return this.offset; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "The offset value must be equal or greater than zero.");
                this.offset = value;
            }
        }

        public override double Measurement
        {
            get { return Vector2.Distance(this.firstRefPoint, this.secondRefPoint); }
        }

        #endregion

        #region public methods

        public void SetDimensionLinePosition(Vector2 point)
        {
            Vector2 refDir = Vector2.Normalize(this.secondRefPoint - this.firstRefPoint);
            Vector2 offsetDir = point - this.firstRefPoint;

            double cross = Vector2.CrossProduct(refDir, offsetDir);
            if (cross < 0)
            {
                Vector2 tmp = this.firstRefPoint;
                this.firstRefPoint = this.secondRefPoint;
                this.secondRefPoint = tmp;
                refDir *= -1;
            }

            Vector2 vec = Vector2.Perpendicular(refDir);
            this.offset = MathHelper.PointLineDistance(point, this.firstRefPoint, refDir);
            this.defPoint = this.secondRefPoint + this.offset * vec;

            if (!this.TextPositionManuallySet)
            {
                DimensionStyleOverride styleOverride;
                double textGap = this.Style.TextOffset;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.TextOffset, out styleOverride))
                {
                    textGap = (double)styleOverride.Value;
                }
                double scale = this.Style.DimScaleOverall;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.DimScaleOverall, out styleOverride))
                {
                    scale = (double)styleOverride.Value;
                }

                double gap = this.offset + textGap * scale;
                this.textRefPoint = Vector2.MidPoint(this.firstRefPoint, this.secondRefPoint) + gap * vec;
            }
        }

        #endregion

        #region overrides

        protected override void CalculteReferencePoints()
        {
            DimensionStyleOverride styleOverride;

            Vector2 ref1 = this.FirstReferencePoint;
            Vector2 ref2 = this.SecondReferencePoint;
            Vector2 dirRef = ref2 - ref1;
            Vector2 dirDesp = Vector2.Normalize(Vector2.Perpendicular(dirRef));
            Vector2 vec = this.offset* dirDesp;
            Vector2 dimRef1 = ref1 + vec;
            Vector2 dimRef2 = ref2 + vec;

            this.defPoint = dimRef2;

            if (this.TextPositionManuallySet)
            {
                DimensionStyleFitTextMove moveText = this.Style.FitTextMove;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.FitTextMove, out styleOverride))
                {
                    moveText = (DimensionStyleFitTextMove) styleOverride.Value;
                }

                if (moveText == DimensionStyleFitTextMove.BesideDimLine)
                {
                    this.SetDimensionLinePosition(this.textRefPoint);
                }
            }
            else
            {
                double textGap = this.Style.TextOffset;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.TextOffset, out styleOverride))
                {
                    textGap = (double) styleOverride.Value;
                }
                double scale = this.Style.DimScaleOverall;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.DimScaleOverall, out styleOverride))
                {
                    scale = (double) styleOverride.Value;
                }

                double gap = textGap*scale;
                this.textRefPoint = Vector2.MidPoint(dimRef1, dimRef2) + gap*dirDesp;
            }
        }

        protected override Block BuildBlock(string name)
        {
            return DimensionBlock.Build(this, name);
        }

        public override object Clone()
        {
            AlignedDimension entity = new AlignedDimension
            {
                //EntityObject properties
                Layer = (Layer) this.Layer.Clone(),
                Linetype = (Linetype) this.Linetype.Clone(),
                Color = (AciColor) this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                IsVisible = this.IsVisible,
                //Dimension properties
                Style = (DimensionStyle) this.Style.Clone(),
                DefinitionPoint = this.DefinitionPoint,
                TextReferencePoint = this.TextReferencePoint,
                TextPositionManuallySet = this.TextPositionManuallySet,
                TextRotation = this.TextRotation,
                AttachmentPoint = this.AttachmentPoint,
                LineSpacingStyle = this.LineSpacingStyle,
                LineSpacingFactor = this.LineSpacingFactor,
                UserText = this.UserText,
                Elevation = this.Elevation,
                //AlignedDimension properties
                FirstReferencePoint = this.firstRefPoint,
                SecondReferencePoint = this.secondRefPoint,
                Offset = this.offset
            };

            foreach (DimensionStyleOverride styleOverride in this.StyleOverrides.Values)
            {
                object copy;
                ICloneable value = styleOverride.Value as ICloneable;
                copy = value != null ? value.Clone() : styleOverride.Value;

                entity.StyleOverrides.Add(new DimensionStyleOverride(styleOverride.Type, copy));
            }

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}