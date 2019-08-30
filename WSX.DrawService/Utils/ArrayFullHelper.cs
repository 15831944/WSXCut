using System;
using System.Collections.Generic;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawTool;
using WSX.GlobalData.Model;

namespace WSX.DrawService.Utils
{
    public class ArrayFullHelper
    {
        private UCCanvas uCCanvas;
        private ArrayFullModel arrayFullModel;
        private List<IDrawObject> allDrawObjects = new List<IDrawObject>();
        private List<IDrawObject> drawObjects = new List<IDrawObject>();
        private List<IDrawObject> marginDrawObjects = new List<IDrawObject>();
        public ArrayFullHelper(UCCanvas uCCanvas, List<IDrawObject> allDrawObjects, List<IDrawObject> drawObjects, ArrayFullModel arrayFull)
        {
            this.uCCanvas = uCCanvas;
            this.allDrawObjects = allDrawObjects;
            this.drawObjects = drawObjects;
            this.arrayFullModel = arrayFull;
        }
        public void ArrayRectangle()
        {
            //确认板材位置，所有图形最大尺寸
            GetAllDrawingsMaxMinPoint(allDrawObjects, out UnitPoint maxPoint, out UnitPoint minPoint);
            //获取布满坐标
            //左下角坐标
            UnitPoint p1 = new UnitPoint(maxPoint.X + 10, maxPoint.Y - arrayFullModel.PlateHeight);
            UnitPoint p2 = new UnitPoint(maxPoint.X + 10 + arrayFullModel.PlateWidth, maxPoint.Y);
            //计算数量
            CalRowAndColCount(drawObjects, out int rowCount, out int colCount);
            if (rowCount <= 0 || colCount <= 0) return;
            marginDrawObjects = new List<IDrawObject>();
            int rowCoeff = 1;
            int colCoeff = 1;
            //偏移间距
            UnitPoint offset = new UnitPoint(arrayFullModel.PartsSpacing, arrayFullModel.PartsSpacing);

            //零件左下角坐标
            GetAllDrawingsMaxMinPoint(drawObjects, out UnitPoint maxDrawObjectPoint, out UnitPoint minDrawObjectPoint);
           // UnitPoint drawObjectPoint = minDrawObjectPoint;
            //计算偏移距离            

            float thWidth = UCCanvas.GetThresholdWidth();
            double ofx = maxDrawObjectPoint.X - minDrawObjectPoint.X - thWidth;
            double ofy = maxDrawObjectPoint.Y - minDrawObjectPoint.Y - thWidth;
            int objcount = GlobalModel.TotalDrawObjectCount;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {                 
                    for (int k = 0; k < drawObjects.Count; k++)
                    {
                        IDrawObject drawObject = drawObjects[k].Clone();

                        drawObject.GroupParam.FigureSN = ++objcount;
                        if (i == 0 && j == 0)
                        {
                            drawObject.Move(new UnitPoint((p1.X - minDrawObjectPoint.X - thWidth / 2+arrayFullModel.PlateRetainEdge), (p1.Y - minDrawObjectPoint.Y - thWidth / 2+arrayFullModel.PlateRetainEdge)));
                        }
                        else
                        {
                            double movex = (p1.X - minDrawObjectPoint.X - thWidth / 2 + arrayFullModel.PlateRetainEdge);
                            movex = (movex + (j * (offset.X + ofx * colCoeff)));
                            double movey = (p1.Y - minDrawObjectPoint.Y - thWidth / 2 + arrayFullModel.PlateRetainEdge);
                            movey = movey + (i * (offset.Y + ofy * rowCoeff));
                            drawObject.Move(  new UnitPoint(movex, movey));
                        }
                        marginDrawObjects.Add(drawObject);
                    }
                }
            }

            double offsetWidth = (colCount-1)  * offset.X + colCount*ofx * colCoeff+arrayFullModel.PlateRetainEdge;
            double offsetHeight =( rowCount-1)  * offset.Y + rowCount*ofy * rowCoeff+arrayFullModel.PlateRetainEdge;

            double totalwidth = arrayFullModel.PlateWidth - arrayFullModel.PlateRetainEdge;
            double totalheight = arrayFullModel.PlateHeight - arrayFullModel.PlateRetainEdge;

            double lastWidth = totalwidth - offsetWidth;
            double lastHeight = totalheight - offsetHeight;
            //旋转
            List<IDrawObject> ldrawobjectsnew = new List<IDrawObject>();
            for (int k = 0; k < drawObjects.Count; k++)
            {
                IDrawObject drawObject = drawObjects[k].Clone();
                IDrawTranslation drawTranslation = drawObject as IDrawTranslation;
                {
                    UnitPoint centerAxis = new UnitPoint((maxDrawObjectPoint.X + minDrawObjectPoint.X) / 2, (maxDrawObjectPoint.Y + minDrawObjectPoint.Y) / 2);
                    drawTranslation.DoRotate(centerAxis, 90, false);
                }
                ldrawobjectsnew.Add(drawObject);
            }
            //确认旋转后最大尺寸
            GetAllDrawingsMaxMinPoint(ldrawobjectsnew, out UnitPoint maxPointc, out UnitPoint minPointc);
           
            ///偏移距离
            double ofcx = maxPointc.X - minPointc.X - thWidth;
            double ofcy = maxPointc.Y - minPointc.Y - thWidth;

            if (lastWidth>(offset.X+ ofcx))
            {
                 CalRowAndColCount2(drawObjects,  lastWidth, ofcx + offset.X,totalheight, ofcy + offset.Y,out int rowCount1, out int colCount1);

                //左下角坐标
                UnitPoint pc1 = new UnitPoint(maxPoint.X + 10+offsetWidth, maxPoint.Y - arrayFullModel.PlateHeight );

                for (int i = 0; i < rowCount1; i++)
                {
                    for (int j = 0; j < colCount1; j++)
                    {
                        for (int k = 0; k < drawObjects.Count; k++)
                        {
                            IDrawObject drawObject = ldrawobjectsnew[k].Clone();
                            drawObject.GroupParam.FigureSN = ++objcount;
                            if (i == 0 && j == 0)
                            {
                                drawObject.Move(new UnitPoint((pc1.X - minPointc.X - thWidth / 2)+arrayFullModel.PartsSpacing, (pc1.Y - minPointc.Y - thWidth / 2)+arrayFullModel.PlateRetainEdge));
                            }
                            else
                            {
                                drawObject.Move(
                                    new UnitPoint(
                                        ((pc1.X - minPointc.X - thWidth / 2)+arrayFullModel.PartsSpacing + (j * (offset.X + ofcx * colCoeff))),
                                        ((pc1.Y - minPointc.Y - thWidth / 2)+arrayFullModel.PlateRetainEdge + (i * (offset.Y + ofcy * rowCoeff)))
                                        )
                                        );
                            }
                            marginDrawObjects.Add(drawObject);
                        }
                    }
                }
            }

            if (lastHeight > (offset.Y + ofcy))
            {
                //CalRowAndColCount1(drawObjects, totalwidth ,lastHeight, out int rowCount1, out int colCount1);
                CalRowAndColCount2(drawObjects, totalwidth, ofcx + offset.X, lastHeight, ofcy + offset.Y, out int rowCount1, out int colCount1);

                //左下角坐标
                UnitPoint pc1 = new UnitPoint(maxPoint.X + 10, maxPoint.Y - arrayFullModel.PlateHeight + offsetHeight);


                for (int i = 0; i <rowCount1 ; i++)
                {
                    for (int j = 0; j < colCount1; j++)
                    {
                        for (int k = 0; k < ldrawobjectsnew.Count; k++)
                        {
                            IDrawObject drawObject = ldrawobjectsnew[k].Clone(); 
                            drawObject.GroupParam.FigureSN = ++objcount;
                            if (i == 0 && j == 0)
                            {
                                drawObject.Move(new UnitPoint((pc1.X - minPointc.X - thWidth / 2+arrayFullModel.PlateRetainEdge), (pc1.Y -minPointc.Y- thWidth / 2)+arrayFullModel.PartsSpacing));
                            }
                            else
                            {
                                drawObject.Move(
                                    new UnitPoint(
                                        ((pc1.X - minPointc.X - thWidth / 2 + arrayFullModel.PlateRetainEdge) + (j * (offset.X + ofcx * colCoeff))),
                                        ((p1.Y - minPointc.Y - thWidth / 2+offsetHeight+arrayFullModel.PartsSpacing) + (i * (offset.Y + ofcy * rowCoeff)))
                                        )
                                        );
                            }
                            marginDrawObjects.Add(drawObject);
                        }
                    }
                }
            }

            IDrawObject NewDrawObject = uCCanvas.Model.CreateObject("SingleRectangle", p1, null);
            NewDrawObject.OnMouseDown(uCCanvas.canvasWrapper, p2, null, null);
            DrawObjectBase drawData = NewDrawObject as DrawObjectBase;
            GlobalModel.TotalDrawObjectCount--;
            NewDrawObject.GroupParam.FigureSN = objcount + 1;
            drawData.IsBoard = true;
            marginDrawObjects.Add(NewDrawObject);

            if (drawData != null)
                drawData.LayerId = (int)WSX.GlobalData.Model.LayerId.White;

            if (GlobalModel.Params.ArrayFull.IsClearOriginalCompleted)
            {
                marginDrawObjects.ForEach(s => s.GroupParam.FigureSN = s.GroupParam.FigureSN - drawObjects.Count);
                uCCanvas.Model.ArrayObjects(drawObjects, marginDrawObjects);
            }
            else
            {
               uCCanvas.Model.ArrayObjects(null, marginDrawObjects);
            }
            uCCanvas.Model.DrawingLayer.UpdateSN();
        }

        private void GetAllDrawingsMaxMinPoint(List<IDrawObject> ldrawObjects, out UnitPoint maxPoint, out UnitPoint minPoint)
        {
            maxPoint = ((IDrawTranslation)ldrawObjects[0]).MaxValue;
            minPoint = ((IDrawTranslation)ldrawObjects[0]).MinValue;
            for (int i = 1; i < ldrawObjects.Count; i++)
            {
                UnitPoint tempMax = ((IDrawTranslation)ldrawObjects[i]).MaxValue;
                UnitPoint tempMin = ((IDrawTranslation)ldrawObjects[i]).MinValue;
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
        }

        private void CalRowAndColCount(List<IDrawObject> ldrawObjects, out int rowCount, out int colCount)
        {
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            GetAllDrawingsMaxMinPoint(ldrawObjects, out UnitPoint maxpoint, out UnitPoint minpoint);
            double width = maxpoint.X - minpoint.X - thresholdWidth;
            double height = maxpoint.Y - minpoint.Y - thresholdWidth;

            double totalwidth = arrayFullModel.PlateWidth - arrayFullModel.PlateRetainEdge;
            double totalheight = arrayFullModel.PlateHeight - arrayFullModel.PlateRetainEdge;
            double offset = arrayFullModel.PartsSpacing;
            colCount = Math.Abs((int)Math.Floor((totalwidth) / (width + offset)));
            rowCount = Math.Abs((int)Math.Floor((totalheight) / (height + offset)));
        }
        private void CalRowAndColCount2(List<IDrawObject> ldrawObjects, double totalWidth,double eachWidth, double totalHeight, double eachHeight, out int rowCount, out int colCount)
        {          
            colCount = Math.Abs((int)Math.Floor((totalWidth) / eachWidth));
            rowCount = Math.Abs((int)Math.Floor((totalHeight) / (eachHeight)));
        }
    }
}
