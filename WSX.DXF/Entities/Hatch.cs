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
using WSX.DXF.Collections;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a hatch <see cref="EntityObject">entity</see>.
    /// </summary>
    public class Hatch :
        EntityObject
    {
        #region delegates and events

        public delegate void HatchBoundaryPathAddedEventHandler(Hatch sender, ObservableCollectionEventArgs<HatchBoundaryPath> e);

        public event HatchBoundaryPathAddedEventHandler HatchBoundaryPathAdded;

        protected virtual void OnHatchBoundaryPathAddedEvent(HatchBoundaryPath item)
        {
            HatchBoundaryPathAddedEventHandler ae = this.HatchBoundaryPathAdded;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<HatchBoundaryPath>(item));
        }

        public delegate void HatchBoundaryPathRemovedEventHandler(Hatch sender, ObservableCollectionEventArgs<HatchBoundaryPath> e);

        public event HatchBoundaryPathRemovedEventHandler HatchBoundaryPathRemoved;

        protected virtual void OnHatchBoundaryPathRemovedEvent(HatchBoundaryPath item)
        {
            HatchBoundaryPathRemovedEventHandler ae = this.HatchBoundaryPathRemoved;
            if (ae != null)
                ae(this, new ObservableCollectionEventArgs<HatchBoundaryPath>(item));
        }

        #endregion

        #region private fields

        private readonly ObservableCollection<HatchBoundaryPath> boundaryPaths;
        private HatchPattern pattern;
        private double elevation;
        private bool associative;

        #endregion

        #region constructors

        public Hatch(HatchPattern pattern, bool associative)
            : base(EntityType.Hatch, DxfObjectCode.Hatch)
        {
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));
            this.pattern = pattern;
            this.boundaryPaths = new ObservableCollection<HatchBoundaryPath>();
            this.boundaryPaths.BeforeAddItem += this.BoundaryPaths_BeforeAddItem;
            this.boundaryPaths.AddItem += this.BoundaryPaths_AddItem;
            this.boundaryPaths.BeforeRemoveItem += this.BoundaryPaths_BeforeRemoveItem;
            this.boundaryPaths.RemoveItem += this.BoundaryPaths_RemoveItem;
            this.associative = associative;
        }

        public Hatch(HatchPattern pattern, IEnumerable<HatchBoundaryPath> paths, bool associative)
            : base(EntityType.Hatch, DxfObjectCode.Hatch)
        {
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));
            this.pattern = pattern;

            if (paths == null)
                throw new ArgumentNullException(nameof(paths));
            this.boundaryPaths = new ObservableCollection<HatchBoundaryPath>();
            this.boundaryPaths.BeforeAddItem += this.BoundaryPaths_BeforeAddItem;
            this.boundaryPaths.AddItem += this.BoundaryPaths_AddItem;
            this.boundaryPaths.BeforeRemoveItem += this.BoundaryPaths_BeforeRemoveItem;
            this.boundaryPaths.RemoveItem += this.BoundaryPaths_RemoveItem;
            foreach (HatchBoundaryPath path in paths)
            {
                if (!associative)
                    path.ClearContour();
                this.boundaryPaths.Add(path);
            }

            this.associative = associative;
        }

        #endregion

        #region public properties

        public HatchPattern Pattern
        {
            get { return this.pattern; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.pattern = value;
            }
        }

        public ObservableCollection<HatchBoundaryPath> BoundaryPaths
        {
            get { return this.boundaryPaths; }
        }

        public bool Associative
        {
            get { return this.associative; }
        }

        public double Elevation
        {
            get { return this.elevation; }
            set { this.elevation = value; }
        }

        #endregion

        #region public methods

        public List<EntityObject> UnLinkBoundary()
        {
            List<EntityObject> boundary = new List<EntityObject>();
            this.associative = false;
            foreach (HatchBoundaryPath path in this.boundaryPaths)
            {
                foreach (EntityObject entity in path.Entities)
                {
                    entity.RemoveReactor(this);
                    boundary.Add(entity);
                }
                path.ClearContour();
            }
            return boundary;
        }

        public List<EntityObject> CreateBoundary(bool linkBoundary)
        {
            if (this.associative)
                this.UnLinkBoundary();

            this.associative = linkBoundary;
            List<EntityObject> boundary = new List<EntityObject>();
            Matrix3 trans = MathHelper.ArbitraryAxis(this.Normal);
            Vector3 pos = trans*new Vector3(0.0, 0.0, this.elevation);
            foreach (HatchBoundaryPath path in this.boundaryPaths)
            {
                foreach (HatchBoundaryPath.Edge edge in path.Edges)
                {
                    EntityObject entity = edge.ConvertTo();
                    switch (entity.Type)
                    {
                        case EntityType.Arc:
                            boundary.Add(ProcessArc((Arc) entity, trans, pos));
                            break;
                        case EntityType.Circle:
                            boundary.Add(ProcessCircle((Circle) entity, trans, pos));
                            break;
                        case EntityType.Ellipse:
                            boundary.Add(ProcessEllipse((Ellipse) entity, trans, pos));
                            break;
                        case EntityType.Line:
                            boundary.Add(ProcessLine((Line) entity, trans, pos));
                            break;
                        case EntityType.LightWeightPolyline:
                            // LwPolylines need an special treatment since their vertexes are expressed in object coordinates.
                            boundary.Add(ProcessLwPolyline((LwPolyline) entity, this.Normal, this.elevation));
                            break;
                        case EntityType.Spline:
                            boundary.Add(ProcessSpline((Spline) entity, trans, pos));
                            break;
                    }

                    if (this.associative)
                    {
                        path.AddContour(entity);
                        entity.AddReactor(this);
                        this.OnHatchBoundaryPathAddedEvent(path);
                    }
                }
            }
            return boundary;
        }

        #endregion

        #region private methods

        private static EntityObject ProcessArc(Arc arc, Matrix3 trans, Vector3 pos)
        {
            arc.Center = trans*arc.Center + pos;
            arc.Normal = trans*arc.Normal;
            return arc;
        }

        private static EntityObject ProcessCircle(Circle circle, Matrix3 trans, Vector3 pos)
        {
            circle.Center = trans*circle.Center + pos;
            circle.Normal = trans*circle.Normal;
            return circle;
        }

        private static Ellipse ProcessEllipse(Ellipse ellipse, Matrix3 trans, Vector3 pos)
        {
            ellipse.Center = trans*ellipse.Center + pos;
            ellipse.Normal = trans*ellipse.Normal;
            return ellipse;
        }

        private static Line ProcessLine(Line line, Matrix3 trans, Vector3 pos)
        {
            line.StartPoint = trans*line.StartPoint + pos;
            line.EndPoint = trans*line.EndPoint + pos;
            line.Normal = trans*line.Normal;
            return line;
        }

        private static LwPolyline ProcessLwPolyline(LwPolyline polyline, Vector3 normal, double elevation)
        {
            polyline.Elevation = elevation;
            polyline.Normal = normal;
            return polyline;
        }

        private static Spline ProcessSpline(Spline spline, Matrix3 trans, Vector3 pos)
        {
            foreach (SplineVertex vertex in spline.ControlPoints)
                vertex.Position = trans*vertex.Position + pos;

            spline.Normal = trans*spline.Normal;
            return spline;
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            Hatch entity = new Hatch((HatchPattern) this.pattern.Clone(), this.associative)
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
                //Hatch properties
                Elevation = this.elevation
            };

            foreach (HatchBoundaryPath path in this.boundaryPaths)
                entity.boundaryPaths.Add((HatchBoundaryPath) path.Clone());

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion

        #region HatchBoundaryPath collection events

        private void BoundaryPaths_BeforeAddItem(ObservableCollection<HatchBoundaryPath> sender, ObservableCollectionEventArgs<HatchBoundaryPath> e)
        {
            // null items are not allowed in the list.
            if (e.Item == null)
                e.Cancel = true;
            else if (this.boundaryPaths.Contains(e.Item))
                e.Cancel = true;

            e.Cancel = false;
        }

        private void BoundaryPaths_AddItem(ObservableCollection<HatchBoundaryPath> sender, ObservableCollectionEventArgs<HatchBoundaryPath> e)
        {
            if (this.associative)
            {
                foreach (EntityObject entity in e.Item.Entities)
                {
                    entity.AddReactor(this);
                }
            }
            else
            {
                e.Item.ClearContour();
            }
            this.OnHatchBoundaryPathAddedEvent(e.Item);
        }

        private void BoundaryPaths_BeforeRemoveItem(ObservableCollection<HatchBoundaryPath> sender, ObservableCollectionEventArgs<HatchBoundaryPath> e)
        {
        }

        private void BoundaryPaths_RemoveItem(ObservableCollection<HatchBoundaryPath> sender, ObservableCollectionEventArgs<HatchBoundaryPath> e)
        {
            if (this.associative)
            {
                foreach (EntityObject entity in e.Item.Entities)
                {
                    entity.RemoveReactor(this);
                }
            }
            this.OnHatchBoundaryPathRemovedEvent(e.Item);
        }

        #endregion
    }
}