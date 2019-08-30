using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;
using WSX.DrawService.CanvasControl;
using WSX.GlobalData.Model;

namespace WSX.DrawService.Utils
{
    public  class ArrayAnnularHelper
    {
        private UCCanvas uCCanvas;
        private List<IDrawObject> drawObjects;
        private UnitPoint RotateCenterUnitPoint;
        public ArrayAnnularHelper(UCCanvas uCCanvas)
        {
            this.uCCanvas = uCCanvas;          

        }
        public bool HandleMouseDownForArrayAnnular(MouseEventArgs e)
        {
            RotateCenterUnitPoint=uCCanvas. ToUnit(e.Location);
            ArrayAnnular();
            return true;
        }
      
        public void HandleCancelArrayAnnular()
        {
            this.uCCanvas.DoInvalidate(true);         
        }
        public void ArrayAnnular()
        {
              this.drawObjects = uCCanvas.Model.DrawingLayer.SelectedObjects.ToList();
            if (GlobalModel.Params.ArrayAnnular.IsSetArrayCenterScope)
            {
                CalRotateCenterUnitPoint();
            }
            int count = GlobalModel.Params.ArrayAnnular.FigureCount;


            if (GlobalModel.Params.ArrayAnnular.ArrayStandardType == ArrayStandardTypes.ByAngleSpacing)//按角度间距
            {
                double angleSpace = GlobalModel.Params.ArrayAnnular.AngleSpace;

                AddArrayDrawObject(angleSpace, count);
            }
            else//按阵列范围
            {
                double arrayScope = GlobalModel.Params.ArrayAnnular.ArrayScope;
                double eachArrayAngle = arrayScope / count;
                AddArrayDrawObject(eachArrayAngle, count);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="angleSpace"></param>
        /// <param name="count"></param>       
        private void AddArrayDrawObject(double angleSpace,int count)
        {
            List<IDrawObject> ldrawobjects = new List<IDrawObject>();
            foreach (var item in drawObjects)
            {
                for (int i = 1; i < count; i++)
                {
                    IDrawObject drawObject = item.Clone();
                    IDrawTranslation drawTranslation = drawObject as IDrawTranslation;
                    if (drawTranslation != null)
                    {
                        drawTranslation.DoRotate(RotateCenterUnitPoint, i*angleSpace, false);//默认逆时针
                         //drawTranslation.DoRotate(RotateCenterUnitPoint, ((i-1) * angleSpace+addAngle)-90, true);//默认逆时针
                    }
                    drawObject.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;                    
                    ldrawobjects.Add(drawObject);
                }
            }
            if (ldrawobjects.Count > 0)
            {
                uCCanvas.Model.AddObjectOnDrawing(ldrawobjects);
            }
            uCCanvas.Model.DrawingLayer.UpdateSN();
        }
        public RectangleF GetMaxUnit()
        {
            float thWidth = UCCanvas.GetThresholdWidth();


            var tran = drawObjects[0] as IDrawTranslation;
            UnitPoint maxPoint = new UnitPoint();
            maxPoint = tran.MaxValue;
            UnitPoint minPoint = new UnitPoint();
            minPoint = tran.MinValue;
            for (int i = 1; i < drawObjects.Count; i++)
            {
                UnitPoint tempMax = ((IDrawTranslation)drawObjects[i]).MaxValue;
                UnitPoint tempMin = ((IDrawTranslation)drawObjects[i]).MinValue;
                if (tempMax.X > maxPoint.X)
                {
                    maxPoint.X = tempMax.X;
                }
                if (tempMax.Y > maxPoint.Y)
                {
                    maxPoint.Y = tempMax.Y;
                }
                if (tempMin.X < minPoint.X)
                {
                    minPoint.X = tempMin.X;
                }
                if (tempMin.Y < minPoint.Y)
                {
                    minPoint.Y = tempMin.Y;
                }
            }
            RectangleF rectangleLine = ScreenUtils.GetRectangleF(maxPoint, minPoint, thWidth / 2);
            return rectangleLine;
        }
        /// <summary>
        /// 计算阵列中心点
        /// </summary>
        public void CalRotateCenterUnitPoint()
        {
            bool isSetCenter = GlobalModel.Params.ArrayAnnular.IsSetArrayCenterScope;
            if(isSetCenter)
            {
                RectangleF rectangleF=   GetMaxUnit();
                double centerStartAngle = GlobalModel.Params.ArrayAnnular.CenterStartAngle;              
                RotateCenterUnitPoint = new UnitPoint((rectangleF.X+rectangleF.Width/2) - (Math.Cos(HitUtil.DegreesToRadians(centerStartAngle)) * GlobalModel.Params.ArrayAnnular.CenterCricleRadius), (rectangleF.Y+rectangleF.Height/2) - (Math.Sin(HitUtil.DegreesToRadians(centerStartAngle)) * GlobalModel.Params.ArrayAnnular.CenterCricleRadius));
               
            }
        }
        
    }
}
