using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DXF.Control;
using WSX.DXF.Entities;
using WSX.DXF.Entities.Parameters;
using WSX.DXF.Models.Enums;

namespace WSX.DXF.Models.Analyse
{
	class CircleDetails : AbstractDetailsBase
	{
		public override List<TypeParameters> GetTypeParas<T>(T type)
		{
			foreach (var circle in (IEnumerable<Circle>)type)
			{
				this.typeParas = new TypeParameters
				{
					Shape = ShapeTypes.Circle,
					CircleParas = new CircleParameters()
					{
						Center = new PointF((float)circle.Center.X, (float)circle.Center.Y),
						Radius = (float)circle.Radius
					}
				};
				paraLists.Add(typeParas);
			}
			return paraLists;
		}
	}
}
