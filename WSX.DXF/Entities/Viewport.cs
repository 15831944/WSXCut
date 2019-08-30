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

using System;
using System.Collections.Generic;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a view in paper space of the model.
    /// </summary>
    /// <remarks>
    /// The viewport with id equals 1 is the view of the paper space layout itself and it does not show the model.
    /// </remarks>
    public class Viewport :
        EntityObject
    {
        #region private fields

        private Vector3 center;
        private double width;
        private double height;
        private short stacking;
        private short id;
        private Vector2 viewCenter;
        private Vector2 snapBase;
        private Vector2 snapSpacing;
        private Vector2 gridSpacing;
        private Vector3 viewDirection;
        private Vector3 viewTarget;
        private double lensLength;
        private double frontClipPlane;
        private double backClipPlane;
        private double viewHeight;
        private double snapAngle;
        private double twistAngle;
        private short circleZoomPercent;
        private ViewportStatusFlags status;
        private readonly List<Layer> frozenLayers;
        private Vector3 ucsOrigin;
        private Vector3 ucsXAxis;
        private Vector3 ucsYAxis;
        private double elevation;
        private EntityObject boundary;

        #endregion

        #region constructors

        public Viewport()
            : this(2)
        {
            this.status |= ViewportStatusFlags.GridMode;
        }

        internal Viewport(short id)
            : base(EntityType.Viewport, DxfObjectCode.Viewport)
        {
            this.center = Vector3.Zero;
            this.width = 297;
            this.height = 210;
            this.stacking = id;
            this.id = id;
            this.viewCenter = Vector2.Zero;
            this.snapBase = Vector2.Zero;
            this.snapSpacing = new Vector2(10.0);
            this.gridSpacing = new Vector2(10.0);
            this.viewDirection = Vector3.UnitZ;
            this.viewTarget = Vector3.Zero;
            this.lensLength = 50.0;
            this.frontClipPlane = 0.0;
            this.backClipPlane = 0.0;
            this.viewHeight = 250;
            this.snapAngle = 0.0;
            this.twistAngle = 0.0;
            this.circleZoomPercent = 1000;
            this.status = ViewportStatusFlags.AdaptiveGridDisplay | ViewportStatusFlags.DisplayGridBeyondDrawingLimits | ViewportStatusFlags.CurrentlyAlwaysEnabled | ViewportStatusFlags.UcsIconVisibility;
            this.frozenLayers = new List<Layer>();
            this.ucsOrigin = Vector3.Zero;
            this.ucsXAxis = Vector3.UnitX;
            this.ucsYAxis = Vector3.UnitY;
            this.elevation = 0.0;
            this.boundary = null;
        }

        #endregion

        #region public properties

        public Vector3 Center
        {
            get { return this.center; }
            set { this.center = value; }
        }

        public double Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public double Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        /// -1 = On, but is fully off screen, or is one of the viewports that is not active because the $MAXACTVP count is currently being exceeded.<br />
        /// 0 = Off<br />
        /// 1 = Stacking value reserved for the layout view.
        public short Stacking
        {
            get { return this.stacking; }
            set
            {
                if (value < -1)
                    throw new ArgumentOutOfRangeException(nameof(value), "The stacking value must be greater than -1.");
                this.stacking = value;
            }
        }

        internal short Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public Vector2 ViewCenter
        {
            get { return this.viewCenter; }
            set { this.viewCenter = value; }
        }

        public Vector2 SnapBase
        {
            get { return this.snapBase; }
            set { this.snapBase = value; }
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
            get { return this.viewDirection; }
            set { this.viewDirection = value; }
        }

        public Vector3 ViewTarget
        {
            get { return this.viewTarget; }
            set { this.viewTarget = value; }
        }

        public double LensLength
        {
            get { return this.lensLength; }
            set { this.lensLength = value; }
        }

        public double FrontClipPlane
        {
            get { return this.frontClipPlane; }
            set { this.frontClipPlane = value; }
        }

        public double BackClipPlane
        {
            get { return this.backClipPlane; }
            set { this.backClipPlane = value; }
        }

        public double ViewHeight
        {
            get { return this.viewHeight; }
            set { this.viewHeight = value; }
        }

        public double SnapAngle
        {
            get { return this.snapAngle; }
            set { this.snapAngle = value; }
        }

        public double TwistAngle
        {
            get { return this.twistAngle; }
            set { this.twistAngle = value; }
        }

        public short CircleZoomPercent
        {
            get { return this.circleZoomPercent; }
            set { this.circleZoomPercent = value; }
        }

        public List<Layer> FrozenLayers
        {
            get { return this.frozenLayers; }
        }

        public ViewportStatusFlags Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public Vector3 UcsOrigin
        {
            get { return this.ucsOrigin; }
            set { this.ucsOrigin = value; }
        }

        public Vector3 UcsXAxis
        {
            get { return this.ucsXAxis; }
            set { this.ucsXAxis = value; }
        }

        public Vector3 UcsYAxis
        {
            get { return this.ucsYAxis; }
            set { this.ucsYAxis = value; }
        }

        public double Elevation
        {
            get { return this.elevation; }
            set { this.elevation = value; }
        }

        public EntityObject ClippingBoundary
        {
            get { return this.boundary; }
            set
            {
                if (this.boundary != null)
                    this.boundary.RemoveReactor(this);

                if (value == null)
                {
                    this.status &= ~ViewportStatusFlags.NonRectangularClipping;
                    this.boundary = null;
                    return;
                }

                BoundingRectangle abbr;
                switch (value.Type)
                {
                    case EntityType.Circle:
                        Circle circle = (Circle) value;
                        abbr = new BoundingRectangle(new Vector2(circle.Center.X, circle.Center.Y), circle.Radius);
                        break;
                    case EntityType.Ellipse:
                        Ellipse ellipse = (Ellipse) value;
                        abbr = new BoundingRectangle(new Vector2(ellipse.Center.X, ellipse.Center.Y), ellipse.MajorAxis, ellipse.MinorAxis, ellipse.Rotation);
                        break;
                    case EntityType.LightWeightPolyline:
                        LwPolyline lwPol = (LwPolyline) value;
                        abbr = new BoundingRectangle(lwPol.PolygonalVertexes(6, MathHelper.Epsilon, MathHelper.Epsilon));
                        break;
                    case EntityType.Polyline:
                        Polyline pol = (Polyline) value;
                        List<Vector2> pPoints = new List<Vector2>();
                        foreach (PolylineVertex point in pol.Vertexes)
                        {
                            pPoints.Add(new Vector2(point.Position.X, point.Position.Y));
                        }
                        abbr = new BoundingRectangle(pPoints);
                        break;
                    case EntityType.Spline:
                        Spline spline = (Spline) value;
                        List<Vector2> sPoints = new List<Vector2>();
                        foreach (SplineVertex point in spline.ControlPoints)
                        {
                            sPoints.Add(new Vector2(point.Position.X, point.Position.Y));
                        }
                        abbr = new BoundingRectangle(sPoints);
                        break;
                    default:
                        throw new ArgumentException("Only lightweight polylines, polylines, circles, ellipses and splines are allowed.");
                }
                this.width = abbr.Width;
                this.height = abbr.Height;
                this.center = new Vector3(abbr.Center.X, abbr.Center.Y, 0.0);
                this.boundary = value;
                this.boundary.AddReactor(this);
                this.status |= ViewportStatusFlags.NonRectangularClipping;
            }
        }

        #endregion

        #region overrides

        internal override long AsignHandle(long entityNumber)
        {
            if (this.boundary != null)
                entityNumber = this.boundary.AsignHandle(entityNumber);
            return base.AsignHandle(entityNumber);
        }


        public override object Clone()
        {
            Viewport viewport = new Viewport
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
                //viewport properties
                Center = this.center,
                Width = this.width,
                Height = this.height,
                Stacking = this.stacking,
                Id = this.id,
                ViewCenter = this.viewCenter,
                SnapBase = this.snapBase,
                SnapSpacing = this.snapSpacing,
                GridSpacing = this.gridSpacing,
                ViewDirection = this.viewDirection,
                ViewTarget = this.viewTarget,
                LensLength = this.lensLength,
                FrontClipPlane = this.frontClipPlane,
                BackClipPlane = this.backClipPlane,
                ViewHeight = this.viewHeight,
                SnapAngle = this.snapAngle,
                TwistAngle = this.twistAngle,
                CircleZoomPercent = this.circleZoomPercent,
                Status = this.status,
                UcsOrigin = this.ucsOrigin,
                UcsXAxis = this.ucsXAxis,
                UcsYAxis = this.ucsYAxis,
                Elevation = this.elevation,
            };

            foreach (XData data in this.XData.Values)
                viewport.XData.Add((XData) data.Clone());


            if (this.boundary != null)
                viewport.ClippingBoundary = (EntityObject) this.boundary.Clone();

            viewport.FrozenLayers.AddRange(this.frozenLayers.ToArray());
            return viewport;
        }

        #endregion
    }
}