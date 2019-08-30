using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{
    /// <summary>
    /// Represents a point <see cref="EntityObject">entity</see>.
    /// </summary>
    public class Point :EntityObject
    {
        #region private fields

        private Vector3 position;
        private double thickness;
        private double rotation;

        #endregion

        #region constructors

        public Point(Vector3 position)
            : base(EntityType.Point, DxfObjectCode.Point)
        {
            this.position = position;
            this.thickness = 0.0f;
            this.rotation = 0.0;
        }

        public Point(Vector2 position)
            : this(new Vector3(position.X, position.Y, 0.0))
        {
        }

        public Point(double x, double y, double z)
            : this(new Vector3(x, y, z))
        {
        }

        public Point()
            : this(Vector3.Zero)
        {
        }

        #endregion

        #region public properties

        public Vector3 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public double Thickness
        {
            get { return this.thickness; }
            set { this.thickness = value; }
        }

        public double Rotation
        {
            get { return this.rotation; }
            set { this.rotation = MathHelper.NormalizeAngle(value); }
        }

        #endregion

        #region overrides

        public override object Clone()
        {
            Point entity = new Point
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
                //Point properties
                Position = this.position,
                Rotation = this.rotation,
                Thickness = this.thickness
            };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}