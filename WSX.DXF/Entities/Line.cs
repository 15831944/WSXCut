using WSX.DXF.Tables;

namespace WSX.DXF.Entities
{

    public class Line :
        EntityObject
    {
        #region private fields

        private Vector3 start;
        private Vector3 end;
        private double thickness;

        #endregion
        #region constructors
     
        public Line()
            : this(Vector3.Zero, Vector3.Zero)
        {
        }
     
        public Line(Vector2 startPoint, Vector2 endPoint)
            : this(new Vector3(startPoint.X, startPoint.Y, 0.0), new Vector3(endPoint.X, endPoint.Y, 0.0))
        {
        }
     
        public Line(Vector3 startPoint, Vector3 endPoint)
            : base(EntityType.Line, DxfObjectCode.Line)
        {
            this.start = startPoint;
            this.end = endPoint;
            this.thickness = 0.0;
        }

        #endregion

        #region public properties
     
        public Vector3 StartPoint
        {
            get { return this.start; }
            set { this.start = value; }
        }
     
        public Vector3 EndPoint
        {
            get { return this.end; }
            set { this.end = value; }
        }
      
        public Vector3 Direction
        {
            get { return this.end - this.start; }
        }
      
        public double Thickness
        {
            get { return this.thickness; }
            set { this.thickness = value; }
        }

        #endregion

        #region public properties
    
        public void Reverse()
        {
            Vector3 tmp = this.start;
            this.start = this.end;
            this.end = tmp;
        }

        #endregion

        #region overrides
      
        public override object Clone()
        {
            Line entity = new Line
            {
                
                Layer = (Layer) this.Layer.Clone(),
                Linetype = (Linetype) this.Linetype.Clone(),
                Color = (AciColor) this.Color.Clone(),
                Lineweight = this.Lineweight,
                Transparency = (Transparency) this.Transparency.Clone(),
                LinetypeScale = this.LinetypeScale,
                Normal = this.Normal,
                IsVisible = this.IsVisible,
                
                StartPoint = this.start,
                EndPoint = this.end,
                Thickness = this.thickness
            };

            foreach (XData data in this.XData.Values)
                entity.XData.Add((XData) data.Clone());

            return entity;
        }

        #endregion
    }
}