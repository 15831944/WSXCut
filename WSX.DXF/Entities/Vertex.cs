using System;
using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a dxf Vertex.
    /// </summary>
    /// <remarks>
    /// The Vertex class holds all the information read from the dxf file even if its needed or not. For internal use only.
    /// </remarks>
    internal class Vertex :
        DxfObject
    {
        #region private fields

        private VertexTypeFlags flags;
        private Vector3 position;
        private short[] vertexIndexes;
        private double startWidth;
        private double endWidth;
        private double bulge;
        private AciColor color;
        private Layer layer;
        private Linetype linetype;

        #endregion

        #region constructors

        public Vertex()
            : this(Vector3.Zero)
        {
        }

        public Vertex(Vector2 position)
            : this(new Vector3(position.X, position.Y, 0.0))
        {
        }

        public Vertex(double x, double y, double z)
            : this(new Vector3(x, y, z))
        {
        }

        public Vertex(double x, double y)
            : this(new Vector3(x, y, 0.0))
        {
        }

        public Vertex(Vector3 position)
            : base(DxfObjectCode.Vertex)
        {
            this.flags = VertexTypeFlags.PolylineVertex;
            this.position = position;
            this.layer = Layer.Default;
            this.color = AciColor.ByLayer;
            this.linetype = Linetype.ByLayer;
            this.bulge = 0.0;
            this.startWidth = 0.0;
            this.endWidth = 0.0;
        }

        #endregion

        #region public properties

        public Vector3 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public short[] VertexIndexes
        {
            get { return this.vertexIndexes; }
            set { this.vertexIndexes = value; }
        }

        public double StartWidth
        {
            get { return this.startWidth; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The Vertex width must be equals or greater than zero.");
                this.startWidth = value;
            }
        }

        public double EndWidth
        {
            get { return this.endWidth; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The Vertex width must be equals or greater than zero.");
                this.endWidth = value;
            }
        }

        public double Bulge
        {
            get { return this.bulge; }
            set
            {
                if (this.bulge < 0.0 || this.bulge > 1.0f)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The bulge must be a value between zero and one");
                this.bulge = value;
            }
        }

        public VertexTypeFlags Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }

        public AciColor Color
        {
            get { return this.color; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.color = value;
            }
        }

        public Layer Layer
        {
            get { return this.layer; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.layer = value;
            }
        }

        public Linetype Linetype
        {
            get { return this.linetype; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.linetype = value;
            }
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return this.CodeName;
        }

        #endregion
    }
}