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
	public class LineDetails : AbstractDetailsBase
	{
		public override List<TypeParameters> GetTypeParas<T>(T type) 
		{
			foreach (var line in (IEnumerable<Line>)type)
			{
				this.typeParas = new TypeParameters
				{
					Shape = ShapeTypes.Line,
					LineParas = new LineParameters()
					{
						StartPoint = new PointF((float)line.StartPoint.X, (float)line.StartPoint.Y),
						EndPoint = new PointF((float)line.EndPoint.X, (float)line.EndPoint.Y)
					}
				};
				this.paraLists.Add(typeParas);				
			}
			return paraLists;
		}
	}
}
