using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawTool;
using WSX.DrawService.Utils;
using WSX.GlobalData.Model;

namespace WSX.DrawService.Layers
{
    public class DrawingLayer : ICanvasLayer
    {
        public List<IDrawObject> SelectedObjects
        {
            get
            {
                return this.Objects.Where(r => r.IsSelected == true).ToList();
            }
        }

        /// <summary>
        /// 正常绘制图形时添加到图形对象集合
        /// </summary>
        /// <param name="drawObject"></param>
        public void AddObjectOnDrawing(IDrawObject drawObject)
        {
            this.Objects.Add(drawObject);
            this.UpdateSN();
        }

        /// <summary>
        /// 正常绘制图形时添加到图形对象集合
        /// </summary>
        /// <param name="drawObject"></param>
        public void AddObjectOnDrawing(List<IDrawObject> drawObjects)
        {
            this.Objects.AddRange(drawObjects);
        }

        public void UpdateSN()
        {
            int index = 0;
            this.Objects = this.Objects.OrderBy(r => r.GroupParam.FigureSN).ToList();
            this.Objects.ForEach(r =>
            {
                if (r.GroupParam.GroupSN.Count == 1 || r.GroupParam.GroupSN[0] != 0)
                {
                    r.GroupParam.ShowSN = ++index;
                }
                else
                {
                    r.GroupParam.ShowSN = 0;
                }
            });
        }

        /// <summary>
        /// 撤销删除时添加图形到图形对象集合
        /// </summary>
        /// <param name="drawObject"></param>
        public void AddObjectOnRedoUndo(IDrawObject drawObject)
        {
            this.Objects.Insert(drawObject.GroupParam.FigureSN - 1, drawObject);
            GlobalModel.TotalDrawObjectCount += 1;
            for (int i = drawObject.GroupParam.FigureSN; i < this.Objects.Count; i++)
            {
                this.Objects[i].GroupParam.FigureSN += 1;
            }
            this.UpdateSN();
        }

        /// <summary>
        /// 删除图形对象
        /// </summary>
        /// <param name="drawObjects"></param>
        /// <returns></returns>
        public void DeleteObjects(List<IDrawObject> drawObjects)
        {
            this.Objects = this.Objects.OrderBy(s => s.GroupParam.FigureSN).ToList();
            var result =drawObjects.OrderByDescending(r => r.GroupParam.FigureSN);
            foreach (var item in result)
            {
                this.Objects.RemoveAt(item.GroupParam.FigureSN - 1);
                GlobalModel.TotalDrawObjectCount--;
                for (int i = item.GroupParam.FigureSN - 1; i < this.Objects.Count; i++)
                {
                    this.Objects[i].GroupParam.FigureSN -= 1;
                }
            }
            this.UpdateSN();
        }

        #region ICanvasLayer
        public string LayerName
        {
            get
            {
                return "DrawingLayer";
            }
        }

        public List<IDrawObject> Objects { get; set; } = new List<IDrawObject>();

        public bool Locked { get; set; } = false;
        public bool Visible { get; set; } = true;

        /// <summary>
        /// 在绘图层上绘制图形对象
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="unitRectangle"></param>
        public void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            foreach (IDrawObject drawObject in this.Objects)
            {
                if (!drawObject.ObjectInRectangle(unitRectangle, true))
                {
                    continue;
                }
                drawObject.Draw(canvas, unitRectangle);
            }
        }        
        public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, List<IDrawObject> otherObject)
        {
            foreach (IDrawObject drawObject in otherObject)
            {
                ISnapPoint snapPoint = drawObject.SnapPoint(canvas, point, otherObject, null, null);
                if(snapPoint!=null)
                {
                    return snapPoint;
                }
            }
            return null;
        }
        #endregion

    }
}
