using System;
using System.Drawing;

namespace WSX.DXF.Entities.Parameters
{
    public class ArcParameters
	{
		private PointF center;
		private float radius;
		private float startAngle;
		private float endAngle;

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
					throw new ArgumentOutOfRangeException(nameof(value), value, "The arc radius must be greater than zero.");
				this.radius = value;
			}
		}

		public float StartAngle
		{
			get { return this.startAngle; }
			set { this.startAngle = (float)MathHelper.NormalizeAngle(value); }
		}

		public float EndAngle
		{
			get { return this.endAngle; }
			set { this.endAngle = (float)MathHelper.NormalizeAngle(value); }
		}
	}
}
