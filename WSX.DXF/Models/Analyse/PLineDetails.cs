using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DXF.Control;
using WSX.DXF.Entities;
using WSX.DXF.Models.Enums;
using WSX.DXF.Entities.Parameters;
namespace WSX.DXF.Models.Analyse
{
	class PLineDetails : AbstractDetailsBase
	{
		public override List<TypeParameters> GetTypeParas<T>(T type)
		{
			foreach (var pLines in (IEnumerable<LwPolyline>)type)
			{
				List<PointF> pLineList = new List<PointF>();
				foreach (var pLine in pLines.Vertexes)
				{
					pLineList.Add(new PointF((float)pLine.Position.X, (float)pLine.Position.Y));
				}
				this.typeParas = new TypeParameters()
				{
					Shape = ShapeTypes.PLine,
					LwPolyLineParas = new LwPolyLineParameters()
					{
						Vertexes = pLineList,
						IsClosed = pLines.IsClosed
					}
				};
				paraLists.Add(typeParas);
			}
			return paraLists;
		}
	}
}
