using System.Drawing;

namespace WSX.DXF.Entities.Parameters
{
    public class LineParameters
	{
		private PointF start;
		private PointF end;
		public PointF StartPoint
		{
			get { return this.start; }
			set { this.start = value; }
		}

		public PointF EndPoint
		{
			get { return this.end; }
			set { this.end = value; }
		}
	}
}
