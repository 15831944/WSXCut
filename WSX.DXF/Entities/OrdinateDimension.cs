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
using WSX.DXF.Blocks;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents an ordinate dimension <see cref="EntityObject">entity</see>.
    /// </summary>
    public class OrdinateDimension :
        Dimension
    {
        #region private fields

        private double rotation;
        private OrdinateDimensionAxis axis;
        private Vector2 firstPoint;
        private Vector2 secondPoint;

        #endregion

        #region constructors

        public OrdinateDimension()
            : this(Vector2.Zero, new Vector2(0.5, 0), new Vector2(1.0, 0), OrdinateDimensionAxis.Y, DimensionStyle.Default)
        {
        }

        public OrdinateDimension(Vector2 origin, Vector2 featurePoint, Vector2 leaderEndPoint)
            : this(origin, featurePoint, leaderEndPoint, DimensionStyle.Default)
        {           
        }

        public OrdinateDimension(Vector2 origin, Vector2 featurePoint, Vector2 leaderEndPoint, DimensionStyle style)
            : base(DimensionType.Ordinate)
        {
            this.defPoint = origin;
            this.firstPoint = featurePoint;
            this.secondPoint = leaderEndPoint;
            this.textRefPoint = leaderEndPoint;
            Vector2 vec = leaderEndPoint - featurePoint;
            this.axis = vec.Y > vec.X ? OrdinateDimensionAxis.X : OrdinateDimensionAxis.Y;
            this.rotation = 0.0;
            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.Style = style;
        }

        public OrdinateDimension(Vector2 origin, Vector2 featurePoint, Vector2 leaderEndPoint, OrdinateDimensionAxis axis, DimensionStyle style)
            : base(DimensionType.Ordinate)
        {
            this.defPoint = origin;
            this.firstPoint = featurePoint;
            this.secondPoint = leaderEndPoint;
            this.textRefPoint = leaderEndPoint;
            this.axis = axis;
            this.rotation = 0.0;
            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.Style = style;
        }

        public OrdinateDimension(Vector2 origin, Vector2 featurePoint, double length, OrdinateDimensionAxis axis)
            : this(origin, featurePoint, length, axis, 0.0, DimensionStyle.Default)
        {
        }

        public OrdinateDimension(Vector2 origin, Vector2 featurePoint, double length, OrdinateDimensionAxis axis, DimensionStyle style)
            : this(origin, featurePoint, length, axis, 0.0, style)
        {
        }

        public OrdinateDimension(Vector2 origin, Vector2 featurePoint, double length, OrdinateDimensionAxis axis, double rotation)
            : this(origin, featurePoint, length, axis, rotation, DimensionStyle.Default)
        {
        }

        public OrdinateDimension(Vector2 origin, Vector2 featurePoint, double length, OrdinateDimensionAxis axis, double rotation, DimensionStyle style)
            : base(DimensionType.Ordinate)
        {
            this.defPoint = origin;
            this.rotation = MathHelper.NormalizeAngle(rotation);
            this.firstPoint = featurePoint;
            this.axis = axis;

            if (style == null)
                throw new ArgumentNullException(nameof(style));
            this.Style = style;

            double angle = rotation * MathHelper.DegToRad;

            if (this.Axis == OrdinateDimensionAxis.X) angle += MathHelper.HalfPI;
            this.secondPoint = Vector2.Polar(featurePoint, length, angle);
            this.textRefPoint = this.secondPoint;
        }

        #endregion

        #region public properties

        public Vector2 Origin
        {
            get { return this.defPoint; }
            set { this.defPoint = value; }
        }

        public Vector2 FeaturePoint
        {
            get { return this.firstPoint; }
            set { this.firstPoint = value; }
        }

        public Vector2 LeaderEndPoint
        {
            get { return this.secondPoint; }
            set { this.secondPoint = value; }
        }

        public double Rotation
        {
            get { return this.rotation; }
            set { MathHelper.NormalizeAngle(this.rotation = value); }
        }

        public OrdinateDimensionAxis Axis
        {
            get { return this.axis; }
            set { this.axis = value; }
        }

        public override double Measurement
        {
            get
            {
                Vector2 dirRef = Vector2.Rotate(this.axis == OrdinateDimensionAxis.X ? Vector2.UnitY : Vector2.UnitX, this.rotation*MathHelper.DegToRad);
                return MathHelper.PointLineDistance(this.firstPoint, this.defPoint, dirRef);
            }
        }

        #endregion

        #region overrides

        protected override void CalculteReferencePoints()
        {
            if (this.TextPositionManuallySet)
            {
                DimensionStyleFitTextMove moveText = this.Style.FitTextMove;
                DimensionStyleOverride styleOverride;
                if (this.StyleOverrides.TryGetValue(DimensionStyleOverrideType.FitTextMove, out styleOverride))
                {
                    moveText = (DimensionStyleFitTextMove)styleOverride.Value;
                }

                if (moveText != DimensionStyleFitTextMove.OverDimLineWithoutLeader)
                {
                    this.secondPoint = this.textRefPoint;
                }
            }
            else
            {
                this.textRefPoint = this.secondPoint;
            }
        }

        protected override Block BuildBlock(string name)
        {
            return DimensionBlock.Build(this, name);
        }

        public override object Clone()
        {
            OrdinateDimension entity = new OrdinateDimension
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
                DefinitionPoint = this.defPoint,
                TextReferencePoint = this.TextReferencePoint,
                TextPositionManuallySet = this.TextPositionManuallySet,
                TextRotation = this.TextRotation,
                AttachmentPoint = this.AttachmentPoint,
                LineSpacingStyle = this.LineSpacingStyle,
                LineSpacingFactor = this.LineSpacingFactor,
                UserText = this.UserText,
                Elevation = this.Elevation,
                //OrdinateDimension properties
                FeaturePoint = this.firstPoint,
                LeaderEndPoint = this.secondPoint,
                Rotation = this.rotation,
                Axis = this.axis
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