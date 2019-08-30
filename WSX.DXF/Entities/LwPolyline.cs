using System;
using System.Collections.Generic;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a light weight polyline <see cref="EntityObject">entity</see>.
    /// </summary>
    /// <remarks>
    /// Light weight polylines are bidimensional polylines that can hold information about the width of the lines and arcs that compose them.
    /// </remarks>
    public class LwPolyline:EntityObject
    {
        #region private fields

        private readonly List<LwPolylineVertex> vertexes;
        private PolylinetypeFlags flags;
        private double elevation;
        private double thickness;

        #endregion

        #region constructors

        public LwPolyline()
            : this(new List<LwPolylineVertex>())
        {
        }

        public LwPolyline(IEnumerable<Vector2> vertexes)
            : this(vertexes, false)
        {
        }

        public LwPolyline(IEnumerable<Vector2> vertexes, bool isClosed)
            : base(EntityType.LightWeightPolyline, DxfObjectCode.LightWeightPolyline)
        {
            if (vertexes == null)
                throw new ArgumentNullException(nameof(vertexes));
            this.vertexes = new List<LwPolylineVertex>();
            foreach (Vector2 vertex in vertexes)
                this.vertexes.Add(new LwPolylineVertex(vertex));
            this.elevation = 0.0;
            this.thickness = 0.0;
            this.flags = isClosed ? PolylinetypeFlags.ClosedPolylineOrClosedPolygonMeshInM : PolylinetypeFlags.OpenPolyline;
        }

        public LwPolyline(IEnumerable<LwPolylineVertex> vertexes)
            : this(vertexes, false)
        {
        }

        public LwPolyline(IEnumerable<LwPolylineVertex> vertexes, bool isClosed)
            : base(EntityType.LightWeightPolyline, DxfObjectCode.LightWeightPolyline)
        {
            if (vertexes == null)
                throw new ArgumentNullException(nameof(vertexes));
            this.vertexes = new List<LwPolylineVertex>(vertexes);
            this.elevation = 0.0;
            this.thickness = 0.0;
            this.flags = isClosed ? PolylinetypeFlags.ClosedPolylineOrClosedPolygonMeshInM : PolylinetypeFlags.OpenPolyline;
        }

        #endregion

        #region public properties

        public List<LwPolylineVertex> Vertexes
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

        public double Thickness
        {
            get { return this.thickness; }
            set { this.thickness = value; }
        }

        public double Elevation
        {
            get { return this.elevation; }
            set { this.elevation = value; }
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

        internal PolylinetypeFlags Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }

        #endregion

        #region public methods

        public void Reverse()
        {
            this.vertexes.Reverse();
        }

        public void SetConstantWidth(double width)
        {
            foreach (LwPolylineVertex v in this.vertexes)
            {
                v.StartWidth = width;
                v.EndWidth = width;
            }
        }

        public List<EntityObject> Explode()
        {
            List<EntityObject> entities = new List<EntityObject>();
            int index = 0;
            foreach (LwPolylineVertex vertex in this.Vertexes)
            {
                double bulge = vertex.Bulge;
                Vector2 p1;
                Vector2 p2;

                if (index == this.Vertexes.Count - 1)
                {
                    if (!this.IsClosed)
                        break;
                    p1 = new Vector2(vertex.Position.X, vertex.Position.Y);
                    p2 = new Vector2(this.vertexes[0].Position.X, this.vertexes[0].Position.Y);
                }
                else
                {
                    p1 = new Vector2(vertex.Position.X, vertex.Position.Y);
                    p2 = new Vector2(this.vertexes[index + 1].Position.X, this.vertexes[index + 1].Position.Y);
                }

                if (MathHelper.IsZero(bulge))
                {
                    // the polyline edge is a line
                    Vector3 start = MathHelper.Transform(new Vector3(p1.X, p1.Y, this.elevation), this.Normal, CoordinateSystem.Object, CoordinateSystem.World);
                    Vector3 end = MathHelper.Transform(new Vector3(p2.X, p2.Y, this.elevation), this.Normal, CoordinateSystem.Object, CoordinateSystem.World);

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
                        Thickness = this.Thickness,
                    });
                }
                else
                {
                    // the polyline edge is an arc
                    double theta = 4*Math.Atan(Math.Abs(bulge));
                    double c = Vector2.Distance(p1, p2);
                    double r = (c/2)/Math.Sin(theta/2);

                    // avoid arcs with very small radius, draw a line instead
                    if (MathHelper.IsZero(r))
                    {
                        // the polyline edge is a line
                        Vector3 start = MathHelper.Transform(new Vector3(p1.X, p1.Y, this.elevation), this.Normal, CoordinateSystem.Object, CoordinateSystem.World);
                        Vector3 end = MathHelper.Transform(new Vector3(p2.X, p2.Y, this.elevation), this.Normal, CoordinateSystem.Object, CoordinateSystem.World);

                        entities.Add(new Line
                        {
                            Layer = (Layer)this.Layer.Clone(),
                            Linetype = (Linetype)this.Linetype.Clone(),
                            Color = (AciColor)this.Color.Clone(),
                            Lineweight = this.Lineweight,
                            Transparency = (Transparency)this.Transparency.Clone(),
                            LinetypeScale = this.LinetypeScale,
                            Normal = this.Normal,
                            StartPoint = start,
                            EndPoint = end,
                            Thickness = this.Thickness,
                        });
                    }
                    else
                    {
                        double gamma = (Math.PI - theta)/2;
                        double phi = Vector2.Angle(p1, p2) + Math.Sign(bulge)*gamma;
                        Vector2 center = new Vector2(p1.X + r*Math.Cos(phi), p1.Y + r*Math.Sin(phi));
                        double startAngle;
                        double endAngle;
                        if (bulge > 0)
                        {
                            startAngle = MathHelper.RadToDeg*Vector2.Angle(p1 - center);
                            endAngle = startAngle + MathHelper.RadToDeg*theta;
                        }
                        else
                        {
                            endAngle = MathHelper.RadToDeg*Vector2.Angle(p1 - center);
                            startAngle = endAngle - MathHelper.RadToDeg*theta;
                        }
                        Vector3 point = MathHelper.Transform(new Vector3(center.X, center.Y, this.elevation), this.Normal,
                            CoordinateSystem.Object,
                            CoordinateSystem.World);
                        entities.Add(new Arc
                        {
                            Layer = (Layer) this.Layer.Clone(),
                            Linetype = (Linetype) this.Linetype.Clone(),
                            Color = (AciColor) this.Color.Clone(),
                            Lineweight = this.Lineweight,
                            Transparency = (Transparency) this.Transparency.Clone(),
                            LinetypeScale = this.LinetypeScale,
                            Normal = this.Normal,
                            Center = point,
                            Radius = r,
                            StartAngle = startAngle,
                            EndAngle = endAngle,
                            Thickness = this.Thickness,
                        });
                    }
                }
                index++;
            }

            return entities;
        }

        public List<Vector2> PolygonalVertexes(int bulgePrecision, double weldThreshold, double bulgeThreshold)
        {
            List<Vector2> ocsVertexes = new List<Vector2>();

            int index = 0;

            foreach (LwPolylineVertex vertex in this.Vertexes)
            {
                double bulge = vertex.Bulge;
                Vector2 p1;
                Vector2 p2;

                if (index == this.Vertexes.Count - 1)
                {
                    p1 = new Vector2(vertex.Position.X, vertex.Position.Y);
                    p2 = new Vector2(this.vertexes[0].Position.X, this.vertexes[0].Position.Y);
                    // ignore bulge value of last vertex for open polylines
                    if(!this.IsClosed) bulge = 0;
                }
                else
                {
                    p1 = new Vector2(vertex.Position.X, vertex.Position.Y);
                    p2 = new Vector2(this.vertexes[index + 1].Position.X, this.vertexes[index + 1].Position.Y);
                }

                if (!p1.Equals(p2, weldThreshold))
                {
                    if (MathHelper.IsZero(bulge) || bulgePrecision == 0)
                    {
                        ocsVertexes.Add(p1);
                    }
                    else
                    {
                        double c = Vector2.Distance(p1, p2);
                        if (c >= bulgeThreshold)
                        {
                            double s = (c / 2) * Math.Abs(bulge);
                            double r = ((c / 2) * (c / 2) + s * s) / (2 * s);
                            double theta = 4 * Math.Atan(Math.Abs(bulge));
                            double gamma = (Math.PI - theta) / 2;
                            double phi = Vector2.Angle(p1, p2) + Math.Sign(bulge) * gamma;
                            Vector2 center = new Vector2(p1.X + r * Math.Cos(phi), p1.Y + r * Math.Sin(phi));
                            Vector2 a1 = p1 - center;
                            double angle = Math.Sign(bulge) * theta / (bulgePrecision + 1);
                            ocsVertexes.Add(p1);
                            for (int i = 1; i <= bulgePrecision; i++)
                            {
                                Vector2 curvePoint = new Vector2();
                                Vector2 prevCurvePoint = new Vector2(this.vertexes[this.vertexes.Count - 1].Position.X, this.vertexes[this.vertexes.Count - 1].Position.Y);
                                curvePoint.X = center.X + Math.Cos(i * angle) * a1.X - Math.Sin(i * angle) * a1.Y;
                                curvePoint.Y = center.Y + Math.Sin(i * angle) * a1.X + Math.Cos(i * angle) * a1.Y;

                                if (!curvePoint.Equals(prevCurvePoint, weldThreshold) && !curvePoint.Equals(p2, weldThreshold))
                                {
                                    ocsVertexes.Add(curvePoint);
                                }
                            }
                        }
                        else
                        {
                            ocsVertexes.Add(p1);
                        }
                    }
                }
                index++;
            }

            return ocsVertexes;
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            LwPolyline entity = new LwPolyline
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
                //LwPolyline properties
                Elevation = this.elevation,
                Thickness = this.thickness,
                Flags = this.flags
            };

            foreach (LwPolylineVertex vertex in this.vertexes)
                entity.Vertexes.Add((LwPolylineVertex) vertex.Clone());

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}