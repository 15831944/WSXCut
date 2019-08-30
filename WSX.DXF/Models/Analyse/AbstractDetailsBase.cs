using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DXF.Control;

namespace WSX.DXF.Models.Analyse
{
	public abstract class AbstractDetailsBase
	{
		public List<TypeParameters> paraLists { get; set; }
		public TypeParameters typeParas { get; set; }
		public abstract List<TypeParameters> GetTypeParas<T>(T type);

		public AbstractDetailsBase()
		{
			paraLists = new List<TypeParameters>();
			typeParas = new TypeParameters();
		}
	}
}
