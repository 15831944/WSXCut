using System;
using System.Reflection;
using WSX.DXF.Models.Analyse;
using WSX.DXF.Models.Enums;
using WSX.DXF.Objects;

namespace WSX.DXF.Control
{
    public class ObjectFactory : ApplicationParameters
	{
		public static AbstractDetailsBase CreatShape(ShapeTypes shape)
		{
			Type type = null;
			Assembly assembly = Assembly.Load(DllName);
			switch (shape)
			{
				case ShapeTypes.Line:
					type = assembly.GetType(LineTypeName);
					break;
				case ShapeTypes.Circle:
					type = assembly.GetType(CircleTypeName);
					break;
				case ShapeTypes.Arc:
					type = assembly.GetType(ArcTypeName);
					break;
				case ShapeTypes.PLine:
					type = assembly.GetType(PLineTypeName);
					break;
			}
			AbstractDetailsBase aShape = Activator.CreateInstance(type) as AbstractDetailsBase;
			return aShape;
		}
		public static DxfDocument CreateDoc()
		{
			return new DxfDocument()
			{
				ActiveLayout = Layout.ModelSpaceName
			};
		}
	}

}
