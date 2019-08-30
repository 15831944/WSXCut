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
	public class ArcDetails:AbstractDetailsBase
	{
		public override List<TypeParameters> GetTypeParas<T>(T type)
		{
			foreach (var arc in (IEnumerable<Arc>)type)
			{
				this.typeParas = new TypeParameters()
				{
					Shape = ShapeTypes.Arc,
					ArcParas = new ArcParameters()
					{
						Center = new PointF((float)arc.Center.X, (float)arc.Center.Y),
						Radius = (float)arc.Radius,
						StartAngle = (float)arc.StartAngle,
						EndAngle = (float)arc.EndAngle
					}
				};
				paraLists.Add(typeParas);
			}
			return paraLists;
		}
	}
}
