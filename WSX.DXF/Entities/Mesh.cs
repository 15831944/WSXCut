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
    /// Represents a mesh <see cref="EntityObject">entity</see>.
    /// </summary>
    /// <remarks>
    /// Use this entity to overcome the limitations of the PolyfaceMesh, but, keep in mind that this entity was first introduced in AutoCad 2010.<br/>
    /// The maximum number of faces a mesh can have is 16000000 (16 millions).
    /// </remarks>
    public class Mesh :
        EntityObject
    {
        #region private fields

        private const int MaxFaces = 16000000;
        private readonly IReadOnlyList<Vector3> vertexes;
        private readonly IReadOnlyList<int[]> faces;
        private readonly IReadOnlyList<MeshEdge> edges;
        private byte subdivisionLevel;

        #endregion

        #region constructors

        public Mesh(IEnumerable<Vector3> vertexes, IEnumerable<int[]> faces)
            : this(vertexes, faces, null)
        {
        }

        public Mesh(IEnumerable<Vector3> vertexes, IEnumerable<int[]> faces, IEnumerable<MeshEdge> edges)
            : base(EntityType.Mesh, DxfObjectCode.Mesh)
        {
            if (vertexes == null)
                throw new ArgumentNullException(nameof(vertexes));
            this.vertexes = new List<Vector3>(vertexes);
            if (faces == null)
                throw new ArgumentNullException(nameof(faces));
            this.faces = new List<int[]>(faces);
            if (this.faces.Count > MaxFaces)
                throw new ArgumentOutOfRangeException(nameof(faces), this.faces.Count, string.Format("The maximum number of faces in a mesh is {0}", MaxFaces));
            this.edges = edges == null ? new List<MeshEdge>() : new List<MeshEdge>(edges);
            this.subdivisionLevel = 0;
        }

        #endregion

        #region public properties

        public IReadOnlyList<Vector3> Vertexes
        {
            get { return this.vertexes; }
        }

        public IReadOnlyList<int[]> Faces
        {
            get { return this.faces; }
        }

        public IReadOnlyList<MeshEdge> Edges
        {
            get { return this.edges; }
        }

        public byte SubdivisionLevel
        {
            get { return this.subdivisionLevel; }
            set { this.subdivisionLevel = value; }
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            List<Vector3> copyVertexes = new List<Vector3>(this.vertexes.Count);
            List<int[]> copyFaces = new List<int[]>(this.faces.Count);
            List<MeshEdge> copyEdges = null;

            copyVertexes.AddRange(this.vertexes);
            foreach (int[] face in this.faces)
            {
                int[] copyFace = new int[face.Length];
                face.CopyTo(copyFace, 0);
                copyFaces.Add(copyFace);
            }
            if (this.edges != null)
            {
                copyEdges = new List<MeshEdge>(this.edges.Count);
                foreach (MeshEdge meshEdge in this.edges)
                {
                    copyEdges.Add((MeshEdge) meshEdge.Clone());
                }
            }

            Mesh entity = new Mesh(copyVertexes, copyFaces, copyEdges)
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
                //Mesh properties
                SubdivisionLevel = this.subdivisionLevel
            };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}