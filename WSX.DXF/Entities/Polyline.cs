using System;
using System.Collections.Generic;
using WSX.DXF.Collections;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a generic polyline <see cref="EntityObject">entity</see>.
    /// </summary>
    public class Polyline :
        EntityObject
    {
        #region private fields

        private readonly EndSequence endSequence;
        private readonly ObservableCollection<PolylineVertex> vertexes;
        private PolylinetypeFlags flags;
        private PolylineSmoothType smoothType;

        #endregion

        #region constructors

        public Polyline()
            : this(new List<PolylineVertex>(), false)
        {
        }

        public Polyline(IEnumerable<Vector3> vertexes)
            : this(vertexes, false)
        {
        }

        public Polyline(IEnumerable<Vector3> vertexes, bool isClosed)
            : base(EntityType.Polyline, DxfObjectCode.Polyline)
        {
            if (vertexes == null)
                throw new ArgumentNullException(nameof(vertexes));
            this.vertexes = new ObservableCollection<PolylineVertex>();
            this.vertexes.BeforeAddItem += this.Vertexes_BeforeAddItem;
            this.vertexes.AddItem += this.Vertexes_AddItem;
            this.vertexes.BeforeRemoveItem += this.Vertexes_BeforeRemoveItem;
            this.vertexes.RemoveItem += this.Vertexes_RemoveItem;

            foreach (Vector3 vertex in vertexes)
                this.vertexes.Add(new PolylineVertex(vertex));

            this.flags = isClosed ? PolylinetypeFlags.ClosedPolylineOrClosedPolygonMeshInM | PolylinetypeFlags.Polyline3D : PolylinetypeFlags.Polyline3D;
            this.smoothType = PolylineSmoothType.NoSmooth;
            this.endSequence = new EndSequence(this);
        }


        public Polyline(IEnumerable<PolylineVertex> vertexes)
            : this(vertexes, false)
        {
        }

        public Polyline(IEnumerable<PolylineVertex> vertexes, bool isClosed)
            : base(EntityType.Polyline, DxfObjectCode.Polyline)
        {
            if (vertexes == null)
                throw new ArgumentNullException(nameof(vertexes));
            this.vertexes = new ObservableCollection<PolylineVertex>();
            this.vertexes.BeforeAddItem += this.Vertexes_BeforeAddItem;
            this.vertexes.AddItem += this.Vertexes_AddItem;
            this.vertexes.BeforeRemoveItem += this.Vertexes_BeforeRemoveItem;
            this.vertexes.RemoveItem += this.Vertexes_RemoveItem;

            this.vertexes.AddRange(vertexes);

            this.flags = isClosed ? PolylinetypeFlags.ClosedPolylineOrClosedPolygonMeshInM | PolylinetypeFlags.Polyline3D : PolylinetypeFlags.Polyline3D;
            this.smoothType = PolylineSmoothType.NoSmooth;
            this.endSequence = new EndSequence(this);
        }

        #endregion

        #region public properties

        public ObservableCollection<PolylineVertex> Vertexes
        {
            get { return this.vertexes; }
        }

        public bool IsClosed
        {
            get { return this.flags.HasFlag(PolylinetypeFlags.ClosedPolylineOrClosedPolygonMeshInM); }
            set
            {
                if (value)
                    this.flags |= PolylinetypeFlags.ClosedPolylineOrClosedPolygonMeshInM;
                else
                    this.flags &= ~PolylinetypeFlags.ClosedPolylineOrClosedPolygonMeshInM;
            }
        }

        public bool LinetypeGeneration
        {
            get { return this.flags.HasFlag(PolylinetypeFlags.ContinuousLinetypePattern); }
            set
            {
                if (value)
                    this.flags |= PolylinetypeFlags.ContinuousLinetypePattern;
                else
                    this.flags &= ~PolylinetypeFlags.ContinuousLinetypePattern;
            }
        }

        #endregion

        #region internal properties

        internal PolylineSmoothType SmoothType
        {
            get { return this.smoothType; }
            set { this.smoothType = value; }
        }

        internal PolylinetypeFlags Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }

        internal EndSequence EndSequence
        {
            get { return this.endSequence; }
        }

        #endregion

        #region public methods

        public void Reverse()
        {
            this.vertexes.Reverse();
        }

        public List<EntityObject> Explode()
        {
            List<EntityObject> entities = new List<EntityObject>();
            int index = 0;
            foreach (PolylineVertex vertex in this.Vertexes)
            {
                Vector3 start;
                Vector3 end;

                if (index == this.Vertexes.Count - 1)
                {
                    if (!this.IsClosed)
                        break;
                    start = vertex.Position;
                    end = this.vertexes[0].Position;
                }
                else
                {
                    start = vertex.Position;
                    end = this.vertexes[index + 1].Position;
                }

                entities.Add(new Line
                {
                    Layer = (Layer) this.Layer.Clone(),
                    Linetype = (Linetype) this.Linetype.Clone(),
                    Color = (AciColor) this.Color.Clone(),
                    Lineweight = this.Lineweight,
                    Transparency = (Transparency) this.Transparency.Clone(),
                    LinetypeScale = this.LinetypeScale,
                    Normal = this.Normal,
                    StartPoint = start,
                    EndPoint = end,
                });

                index++;
            }

            return entities;
        }

        #endregion

        #region overrides

        internal override long AsignHandle(long entityNumber)
        {
            foreach (PolylineVertex v in this.vertexes)
            {
                entityNumber = v.AsignHandle(entityNumber);
            }
            entityNumber = this.endSequence.AsignHandle(entityNumber);

            return base.AsignHandle(entityNumber);
        }

        public override object Clone()
        {
            Polyline entity = new Polyline
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
                //Polyline properties
                Flags = this.flags
            };

            foreach (PolylineVertex vertex in this.vertexes)
                entity.Vertexes.Add((PolylineVertex) vertex.Clone());

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion

        #region Entities collection events

        private void Vertexes_BeforeAddItem(ObservableCollection<PolylineVertex> sender, ObservableCollectionEventArgs<PolylineVertex> e)
        {
            // null items and vertexes that belong to another polyline are not allowed.
            if (e.Item == null)
                e.Cancel = true;
            else if (e.Item.Owner != null)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        private void Vertexes_AddItem(ObservableCollection<PolylineVertex> sender, ObservableCollectionEventArgs<PolylineVertex> e)
        {
            // if the polyline already belongs to a document
            if (this.Owner != null)
            {
                // get the document
                DxfDocument doc = this.Owner.Record.Owner.Owner;
                doc.NumHandles = e.Item.AsignHandle(doc.NumHandles);
            }
            e.Item.Owner = this;
        }

        private void Vertexes_BeforeRemoveItem(ObservableCollection<PolylineVertex> sender, ObservableCollectionEventArgs<PolylineVertex> e)
        {
        }

        private void Vertexes_RemoveItem(ObservableCollection<PolylineVertex> sender, ObservableCollectionEventArgs<PolylineVertex> e)
        {
            e.Item.Handle = null;
            e.Item.Owner = null;
        }

        #endregion

    }
}