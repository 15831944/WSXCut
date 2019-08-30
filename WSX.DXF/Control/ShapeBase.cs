using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DXF.Entities;
using WSX.DXF.Models.Enums;
using WSX.DXF.Entities.Parameters;
namespace WSX.DXF.Control
{
	public class TypeParameters
	{
		public ShapeTypes Shape { get; set; }
		public LineParameters LineParas { get; set; }//直线 —— 起始点，结束点
		public CircleParameters CircleParas { get; set; }//圆 —— 圆心，半径
		public ArcParameters ArcParas { get; set; }//圆弧 —— 圆心，起始角度，终止角度
		public LwPolyLineParameters LwPolyLineParas { get; set; }//多线段 —— 点的集合

	}
}
