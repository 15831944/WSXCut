using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DXF.Entities;
using WSX.DXF.Models.Analyse;
using WSX.DXF.Models.Enums;
using WSX.DXF.Objects;
using WSX.DXF.Tables;

namespace WSX.DXF.Control
{
	public class DxfDataTranser
	{
		private static DxfDataTranser instance;
		private Func<List<TypeParameters>> updateFunc;
		private List<TypeParameters> valueList;
		private AbstractDetailsBase getDetails;
		private DxfDocument docWrite;
		private Layer layerWrite;
		private DxfDataTranser()
		{
			docWrite = ObjectFactory.CreateDoc();
			layerWrite = new Layer("Layer1") { Color = AciColor.Blue };
		}
		public static DxfDataTranser Instance
		{
			get
			{
				return instance ?? (instance = new DxfDataTranser());
			}
		}

		#region Read DXF File
		public void DocumentLoad(string doc)
		{
			DxfDocument dxfLoad = DxfDocument.Load(doc.ToString());
			valueList = new List<TypeParameters>();

			updateFunc = new Func<List<TypeParameters>>(() =>
			{

				#region Line Analyze

				getDetails = ObjectFactory.CreatShape(ShapeTypes.Line);
				var lineList = getDetails.GetTypeParas(dxfLoad.Lines);
				valueList.AddRange(lineList);

				#endregion

				#region Circle Analyze

				getDetails = ObjectFactory.CreatShape(ShapeTypes.Circle);
				var circleList = getDetails.GetTypeParas(dxfLoad.Circles);
				valueList.AddRange(circleList);

				#endregion

				#region Arc Analyze

				getDetails = ObjectFactory.CreatShape(ShapeTypes.Arc);
				var arcList = getDetails.GetTypeParas(dxfLoad.Arcs);
				valueList.AddRange(arcList);

				#endregion
				getDetails = ObjectFactory.CreatShape(ShapeTypes.PLine);
				var pLineList = getDetails.GetTypeParas(dxfLoad.LwPolylines);
				valueList.AddRange(pLineList);

				return valueList;
			});
		}

		#region Test Code
		//private void GetList(Entities.Line line)
		//{
		//	shapeBase = new ShapeBase();
		//	List<PointF> items = new List<PointF>()
		//	{
		//		new PointF((float)line.StartPoint.X,(float)line.StartPoint.Y),
		//		new PointF((float)line.EndPoint.X,(float)line.EndPoint.Y),
		//	};

		//	shapeBase.shape = "Line";
		//	shapeBase.lineItems = items;

		//	valueList.Add(shapeBase);
		//}
		#endregion

		public List<TypeParameters> GetUnderlyGraphics()
		{

			#region Test Graphic Write
			//WriteLine2DXF(new PointF(0, 0), new PointF(50, 50));
			//WriteCircle2DXF(new PointF(0, 0), 25);
			//WriteArc2DXF(new PointF(0, 0), 50, 0, 45);
			//List<PointF> vector2s = new List<PointF>()
			//{
			//	new PointF(0,0),
			//	new PointF(0,50),
			//	new PointF(50,50),
			//	new PointF(50,100),
			//	new PointF(100,100),
			//};

			//WriteLwPolyLine2DXF(vector2s, false);
			//SaveDocument2DXF("test2.dxf");
			#endregion

			return updateFunc?.Invoke();
		}
		#endregion

		#region Write DXF File

		public void WriteLine2DXF(PointF StartPoint, PointF EndPoint)
		{
			Line line = new Line(StartPoint, EndPoint)
			{
				Layer = layerWrite
			};
			docWrite.AddEntity(line);
		}
		public void WriteCircle2DXF(PointF CenterPoint, float Radius)
		{
			Circle circle = new Circle(CenterPoint, Radius)
			{
				Layer = layerWrite
			};
			docWrite.AddEntity(circle);
		}
		public void WriteArc2DXF(PointF CenterPoint, float Radius, float StartAngle, float EndAngle)
		{
			Arc arc = new Arc(CenterPoint, Radius, StartAngle, EndAngle)
			{
				Layer = layerWrite
			};
			docWrite.AddEntity(arc);
		}

		public void WriteLwPolyLine2DXF(List<PointF> pointFs, bool IsClose)
		{
			List<Vector2> vector2s = new List<Vector2>();
			foreach (var point in pointFs)
			{
				vector2s.Add(point);
			}
			LwPolyline lwPolyline = new LwPolyline(vector2s, IsClose)
			{
				Layer = layerWrite
			};
			docWrite.AddEntity(lwPolyline);
		}

		public void SaveDocument2DXF(string fileName)
		{
			if (docWrite != null)
			{
				docWrite.Save(fileName);
			}
			docWrite = ObjectFactory.CreateDoc();
		}

		#endregion

	}
}
