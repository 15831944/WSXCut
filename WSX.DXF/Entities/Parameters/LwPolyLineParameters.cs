using System.Collections.Generic;
using System.Drawing;

namespace WSX.DXF.Entities.Parameters
{
    public class LwPolyLineParameters
	{
		private List<PointF> vertexes;
		private bool close;

		public List<PointF> Vertexes
		{
			get { return this.vertexes; }
			set { this.vertexes = value; }
		}

		public bool IsClosed
		{
			get { return this.close; }
			set { this.close = value; }
		}
	}
}
