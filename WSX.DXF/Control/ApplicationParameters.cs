using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.DXF.Control
{
	public class ApplicationParameters
	{
		protected static string ShapeDLL = ConfigurationManager.AppSettings["AbstractShape"];
		protected static string DllName = ShapeDLL.Split(',')[0];
		protected static string LineTypeName = ShapeDLL.Split(',')[1];
		protected static string CircleTypeName = ShapeDLL.Split(',')[2];
		protected static string ArcTypeName = ShapeDLL.Split(',')[3];
		protected static string PLineTypeName = ShapeDLL.Split(',')[4];
	}
}
