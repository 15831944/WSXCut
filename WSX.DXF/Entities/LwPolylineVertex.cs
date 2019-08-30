using System;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a <see cref="LwPolyline">LwPolyline</see> vertex.
    /// </summary>
    public class LwPolylineVertex :
        ICloneable
    {
        #region private fields

        private Vector2 position;
        private double startWidth;
        private double endWidth;
        private double bulge;

        #endregion

        #region constructors

        public LwPolylineVertex()
            : this(Vector2.Zero)
        {
        }

        public LwPolylineVertex(double x, double y)
            : this(new Vector2(x, y), 0.0)
        {
        }

        public LwPolylineVertex(double x, double y, double bulge)
            : this(new Vector2(x, y), bulge)
        {
        }

        public LwPolylineVertex(Vector2 position)
            : this(position, 0.0)
        {
        }

        public LwPolylineVertex(Vector2 position, double bulge)
        {
            this.position = position;
            this.bulge = bulge;
            this.startWidth = 0.0;
            this.endWidth = 0.0;
        }

        #endregion

        #region public properties

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public double StartWidth
        {
            get { return this.startWidth; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The LwPolylineVertex width must be equals or greater than zero.");
                this.startWidth = value;
            }
        }

        public double EndWidth
        {
            get { return this.endWidth; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "The LwPolylineVertex width must be equals or greater than zero.");
                this.endWidth = value;
            }
        }

        public double Bulge
        {
            get { return this.bulge; }
            set { this.bulge = value; }
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            return string.Format("{0}: ({1})", "LwPolylineVertex", this.position);
        }

        public object Clone()
        {
            return new LwPolylineVertex
            {
                Position = this.position,
                Bulge = this.bulge,
                StartWidth = this.startWidth,
                EndWidth = this.endWidth
            };
        }

        #endregion
    }
}