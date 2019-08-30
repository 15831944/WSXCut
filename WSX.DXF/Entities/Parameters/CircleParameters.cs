using System;
using System.Drawing;

namespace WSX.DXF.Entities.Parameters
{
    public class CircleParameters
	{
		private PointF center;
		private float radius;

		public PointF Center
		{
			get { return this.center; }
			set { this.center = value; }
		}

		public float Radius
		{
			get { return this.radius; }
			set
			{
				if (value <= 0)
					throw new ArgumentOutOfRangeException(nameof(value), value, "圆的半径不得小于0");
				this.radius = value;
			}
		}
	}
}
