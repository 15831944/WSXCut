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
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a polyface mesh <see cref="EntityObject">entity</see>.
    /// </summary>
    /// <remarks>
    /// The maximum number of vertexes and faces that a PolyfaceMesh can have is short.MaxValue = 32767.
    /// </remarks>
    public class PolyfaceMesh :
        EntityObject
    {
        #region private fields

        private readonly List<PolyfaceMeshFace> faces;
        private readonly List<PolyfaceMeshVertex> vertexes;
        private readonly PolylinetypeFlags flags;
        private readonly EndSequence endSequence;

        #endregion

        #region constructors

        public PolyfaceMesh(IEnumerable<PolyfaceMeshVertex> vertexes, IEnumerable<PolyfaceMeshFace> faces)
            : base(EntityType.PolyfaceMesh, DxfObjectCode.Polyline)
        {
            this.flags = PolylinetypeFlags.PolyfaceMesh;
            if (vertexes == null)
                throw new ArgumentNullException(nameof(vertexes));
            this.vertexes = new List<PolyfaceMeshVertex>(vertexes);
            if (this.vertexes.Count < 3)
                throw new ArgumentOutOfRangeException(nameof(vertexes), this.vertexes.Count, "The polyface mesh faces list requires at least three points.");

            if (faces == null)
                throw new ArgumentNullException(nameof(vertexes));
            this.faces = new List<PolyfaceMeshFace>(faces);
            if (this.faces.Count < 1)
                throw new ArgumentOutOfRangeException(nameof(vertexes), this.faces.Count, "The polyface mesh faces list requires at least one face.");

            this.endSequence = new EndSequence(this);
        }

        #endregion

        #region public properties

        public IReadOnlyList<PolyfaceMeshVertex> Vertexes
        {
            get { return this.vertexes; }
        }

        public IReadOnlyList<PolyfaceMeshFace> Faces
        {
            get { return this.faces; }
        }

        #endregion

        #region internal properties

        internal PolylinetypeFlags Flags
        {
            get { return this.flags; }
        }

        internal EndSequence EndSequence
        {
            get { return this.endSequence; }
        }

        #endregion

        #region overrides

        internal override long AsignHandle(long entityNumber)
        {
            entityNumber = this.endSequence.AsignHandle(entityNumber);
            foreach (PolyfaceMeshVertex v in this.vertexes)
            {
                entityNumber = v.AsignHandle(entityNumber);
            }
            foreach (PolyfaceMeshFace f in this.faces)
            {
                entityNumber = f.AsignHandle(entityNumber);
            }
            return base.AsignHandle(entityNumber);
        }

        #endregion

        #region public methods

        public List<EntityObject> Explode()
        {
            List<EntityObject> entities = new List<EntityObject>();

            foreach (PolyfaceMeshFace face in this.Faces)
            {
                if (face.VertexIndexes.Count == 1)
                {
                    Point point = new Point
                    {
                        Layer = (Layer) this.Layer.Clone(),
                        Linetype = (Linetype) this.Linetype.Clone(),
                        Color = (AciColor) this.Color.Clone(),
                        Lineweight = this.Lineweight,
                        Transparency = (Transparency) this.Transparency.Clone(),
                        LinetypeScale = this.LinetypeScale,
                        Normal = this.Normal,
                        Position = this.Vertexes[Math.Abs(face.VertexIndexes[0]) - 1].Location,
                    };
                    entities.Add(point);
                    continue;
                }
                if (face.VertexIndexes.Count == 2)
                {
                    Line line = new Line
                    {
                        Layer = (Layer) this.Layer.Clone(),
                        Linetype = (Linetype) this.Linetype.Clone(),
                        Color = (AciColor) this.Color.Clone(),
                        Lineweight = this.Lineweight,
                        Transparency = (Transparency) this.Transparency.Clone(),
                        LinetypeScale = this.LinetypeScale,
                        Normal = this.Normal,
                        StartPoint = this.Vertexes[Math.Abs(face.VertexIndexes[0]) - 1].Location,
                        EndPoint = this.Vertexes[Math.Abs(face.VertexIndexes[1]) - 1].Location,
                    };
                    entities.Add(line);
                    continue;
                }

                Face3dEdgeFlags edgeVisibility = Face3dEdgeFlags.Visibles;

                short indexV1 = face.VertexIndexes[0];
                short indexV2 = face.VertexIndexes[1];
                short indexV3 = face.VertexIndexes[2];
                // Polyface mesh faces are made of 3 or 4 vertexes, we will repeat the third vertex if the number of face vertexes is three
                int indexV4 = face.VertexIndexes.Count == 3 ? face.VertexIndexes[2] : face.VertexIndexes[3];

                if (indexV1 < 0)
                    edgeVisibility = edgeVisibility | Face3dEdgeFlags.First;
                if (indexV2 < 0)
                    edgeVisibility = edgeVisibility | Face3dEdgeFlags.Second;
                if (indexV3 < 0)
                    edgeVisibility = edgeVisibility | Face3dEdgeFlags.Third;
                if (indexV4 < 0)
                    edgeVisibility = edgeVisibility | Face3dEdgeFlags.Fourth;

                Vector3 v1 = this.Vertexes[Math.Abs(indexV1) - 1].Location;
                Vector3 v2 = this.Vertexes[Math.Abs(indexV2) - 1].Location;
                Vector3 v3 = this.Vertexes[Math.Abs(indexV3) - 1].Location;
                Vector3 v4 = this.Vertexes[Math.Abs(indexV4) - 1].Location;

                Face3d face3d = new Face3d
                {
                    Layer = (Layer) this.Layer.Clone(),
                    Linetype = (Linetype) this.Linetype.Clone(),
                    Color = (AciColor) this.Color.Clone(),
                    Lineweight = this.Lineweight,
                    Transparency = (Transparency) this.Transparency.Clone(),
                    LinetypeScale = this.LinetypeScale,
                    Normal = this.Normal,
                    FirstVertex = v1,
                    SecondVertex = v2,
                    ThirdVertex = v3,
                    FourthVertex = v4,
                    EdgeFlags = edgeVisibility,
                };

                entities.Add(face3d);
            }
            return entities;
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            List<PolyfaceMeshVertex> copyVertexes = new List<PolyfaceMeshVertex>();
            foreach (PolyfaceMeshVertex vertex in this.vertexes)
            {
                copyVertexes.Add((PolyfaceMeshVertex) vertex.Clone());
            }
            List<PolyfaceMeshFace> copyFaces = new List<PolyfaceMeshFace>();
            foreach (PolyfaceMeshFace face in this.faces)
            {
                copyFaces.Add((PolyfaceMeshFace) face.Clone());
            }

            PolyfaceMesh entity = new PolyfaceMesh(copyVertexes, copyFaces)
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
                //PolyfaceMesh properties
            };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}