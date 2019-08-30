using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.DrawModel.RingCut;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawTool;
using WSX.DrawService.DrawTool.CircleTool;
using WSX.DrawService.DrawTool.MultiSegmentLine.MultiLine;
using WSX.DrawService.Layers;
using WSX.DrawService.Utils;
using WSX.DrawService.Utils.Undo;
using WSX.DrawService.Wrapper;
using WSX.GlobalData.Model;

namespace WSX.DrawService
{
    public class DataModel : IModel
    {
        private static Dictionary<string, Type> toolTypes = new Dictionary<string, Type>();
        private Dictionary<string, IDrawObject> dicDrawingTypes = new Dictionary<string, IDrawObject>();
        private UndoRedoBuffer undoRedoBuffer = new UndoRedoBuffer();
        private float zoom = 1.0f;
        private GridLayer gridLayer = new GridLayer();
        private BackgroundLayer backgroundLayer = new BackgroundLayer();

        public bool SelectEventEnabled { get; set; }
        private List<IDrawObject> copyObjectInClipBoard = new List<IDrawObject>();
        private UnitPoint copyLastPoint = new UnitPoint();

        public bool IsDirty
        {
            get
            {
                return this.undoRedoBuffer.Dirty;
            }
        }

        public UnitPoint CenterPoint { get; set; } = UnitPoint.Empty;

        public DataModel()
        {
            toolTypes.Clear();

            toolTypes["Line"] = typeof(LineCommon);
            toolTypes["Circle"] = typeof(DrawTool.CircleTool.Circle);
            toolTypes["SweepArc"] = typeof(DrawTool.Arcs.SweepArc);
            toolTypes["ThreePointsArc"] = typeof(DrawTool.Arcs.ThreePointsArc);
            toolTypes["SingleDot"] = typeof(SingleDot);
            toolTypes["RectangleCommom"] = typeof(RectangleCommon);
            toolTypes["Hexagon"] = typeof(PolygonCommon);
            toolTypes["StarCommon"] = typeof(StarCommon);
            toolTypes["RoundRect"] = typeof(RectangleRound);
            toolTypes["MultiLine"] = typeof(PolyLine);

            //toolTypes["Group"] = typeof(Group);
            this.CenterPoint = new UnitPoint(0, 0);
            this.SelectEventEnabled = true;
        }

        public static IDrawObject NewDrawObject(string objectType)
        {
            if (toolTypes.ContainsKey(objectType))
            {
                string type = toolTypes[objectType].ToString();
                return Assembly.GetExecutingAssembly().CreateInstance(type) as IDrawObject;
            }
            return null;
        }
        public void Clear()
        {
            this.DrawingLayer.Objects.Clear();
            this.undoRedoBuffer.Clear();
        }
        public void AddDrawTool(string key, IDrawObject drawObject)
        {
            this.dicDrawingTypes[key] = drawObject;
        }

        private DrawObjectBase CreateObject(string objectType)
        {
            if (this.dicDrawingTypes.ContainsKey(objectType))
            {
                return this.dicDrawingTypes[objectType].Clone() as DrawObjectBase;
            }
            return null;
        }

        #region IModel

        public float Zoom
        {
            get
            {
                return this.zoom;
            }
            set
            {
                this.zoom = value;
                if (this.zoom > 1000)
                {
                    this.zoom = 1000;
                }
                if (this.zoom < 0.001)
                {
                    this.zoom = 0.001f;
                }
                GlobalModel.ThresholdWidth = 10 * this.zoom;
            }
        }

        public ICanvasLayer BackgroundLayer
        {
            get
            {
                return this.backgroundLayer;
            }
        }

        public ICanvasLayer GridLayer
        {
            get
            {
                return this.gridLayer;
            }
        }
        public DrawingLayer DrawingLayer { get; } = new DrawingLayer();

        public int SelectedCount
        {
            get { return this.DrawingLayer.SelectedObjects.Count; }
        }

        List<IDrawObject> IModel.CopyObjectInClipBoard
        {
            get
            {
                return this.copyObjectInClipBoard;
            }
        }

        public DrawingPattern DrawingPattern { get; set; }

        public void AddObjectOnDrawing(IDrawObject drawObject)
        {
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandAdd(drawObject));
            }
            this.DrawingLayer.AddObjectOnDrawing(drawObject);
        }

        public void AddObjectOnDrawing(List<IDrawObject> drawObjects)
        {
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandAdd(drawObjects));
            }
            this.DrawingLayer.AddObjectOnDrawing(drawObjects);
        }

        public void AddObjectOnRedoUndo(IDrawObject drawObject)
        {
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandAdd(drawObject));
            }
            this.DrawingLayer.AddObjectOnRedoUndo(drawObject);
        }

        public void AddSelectedObject(IDrawObject drawObject)
        {
            drawObject.IsSelected = true;
        }
        public void RemoveSelectedObject(IDrawObject drawObject)
        {
            drawObject.IsSelected = false;
        }
        public void ClearSelectedObjects()
        {
            foreach (IDrawObject drawobject in this.DrawingLayer.SelectedObjects)
            {
                drawobject.IsSelected = false;
            }
        }
        public bool CanRedo()
        {
            return this.undoRedoBuffer.CanRedo;
        }

        public bool CanUndo()
        {
            return this.undoRedoBuffer.CanUndo;
        }

        public void CopyObjects(UnitPoint point, List<IDrawObject> drawObjects)
        {
            List<IDrawObject> tempDrawObjects = new List<IDrawObject>();
            foreach (IDrawObject drawObject in drawObjects)
            {
                IDrawObject tempDrawObject = drawObject.Clone();
                tempDrawObjects.Add(tempDrawObject);
                tempDrawObject.Move(point);
                tempDrawObject.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
                this.AddObjectOnDrawing(tempDrawObject);
            }
        }
        public void Copy()
        {
            if (this.SelectedCount != 0)
            {
                this.copyObjectInClipBoard.Clear();
                this.copyLastPoint = this.CalculateBoundingRectangle(this.DrawingLayer.SelectedObjects.ToList());
                foreach (IDrawObject drawObject in this.DrawingLayer.SelectedObjects)
                {
                    this.copyObjectInClipBoard.Add(drawObject.Clone());
                }
            }
        }

        private UnitPoint CalculateBoundingRectangle(List<IDrawObject> drawObjects)
        {
            UnitPoint maxPoint = ((IDrawTranslation)drawObjects[0]).MaxValue;
            UnitPoint minPoint = ((IDrawTranslation)drawObjects[0]).MinValue;
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
            return new UnitPoint((maxPoint.X + minPoint.X) / 2, (maxPoint.Y + minPoint.Y) / 2);
        }
        public void PasteObjects(UnitPoint offset)
        {
            double x = offset.X - this.copyLastPoint.X;
            double y = offset.Y - this.copyLastPoint.Y;
            offset = new UnitPoint(x, y);
            foreach (IDrawObject drawObject in this.copyObjectInClipBoard)
            {
                IDrawObject tempDrawObject = drawObject.Clone();
                tempDrawObject.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
                tempDrawObject.Move(offset);
                this.AddObjectOnDrawing(tempDrawObject);
            }
        }

        public IDrawObject CreateObject(string type, UnitPoint point, ISnapPoint snapPoint)
        {
            DrawObjectBase drawObjectBase = this.CreateObject(type);
            if (drawObjectBase != null)
            {
                drawObjectBase.InitializeFromModel(point, snapPoint);
            }
            return drawObjectBase as IDrawObject;
        }

        public void DeleteObjects(List<IDrawObject> drawObjects)
        {
            EditCommandRemove undoCommand = null;
            if (this.undoRedoBuffer.CanCapture)
            {
                undoCommand = new EditCommandRemove();
            }
            ((DrawingLayer)this.DrawingLayer).DeleteObjects(drawObjects);
            if (undoCommand != null)
            {
                undoCommand.AddLayerObjects(this.DrawingLayer, drawObjects);
                this.undoRedoBuffer.AddCommand(undoCommand);
            }
        }

        public bool DoRedo()
        {
            return this.undoRedoBuffer.DoRedo(this);
        }

        public bool DoUndo()
        {
            return this.undoRedoBuffer.DoUndo(this);
        }

        public List<IDrawObject> GetHitObjects(ICanvas canvas, RectangleF selection, bool anyPoint)
        {
            List<IDrawObject> selected = new List<IDrawObject>();
            if (this.DrawingLayer.Visible)
            {
                foreach (IDrawObject drawobject in this.DrawingLayer.Objects)
                {
                    if (drawobject.ObjectInRectangle(selection, anyPoint) && drawobject.IsLocked == false)
                    {
                        if (drawobject.GroupParam.GroupSN.Count != 1)
                        {
                            List<IDrawObject> tempResult = this.DrawingLayer.Objects.FindAll(r =>
                            {
                                if (r.GroupParam.GroupSN.Count != 1)
                                {
                                    return r.IsSelected = (r.GroupParam.GroupSN[1] == drawobject.GroupParam.GroupSN[1]);
                                }
                                return false;
                            });
                            selected.AddRange(tempResult);
                        }
                        else
                        {
                            drawobject.IsSelected = true;
                            selected.Add(drawobject);
                        }
                    }
                }
            }
            return selected;
        }

        public List<IDrawObject> GetHitObjects(ICanvas canvas, UnitPoint point)
        {
            List<IDrawObject> selected = new List<IDrawObject>();
            if (this.DrawingLayer.Visible)
            {
                foreach (IDrawObject drawobject in this.DrawingLayer.Objects)
                {
                    if (drawobject.PointInObject(point) && drawobject.IsLocked == false)
                    {
                        if (drawobject.GroupParam.GroupSN.Count != 1)
                        {
                            List<IDrawObject> tempResult = this.DrawingLayer.Objects.FindAll(r =>
                            {
                                if (r.GroupParam.GroupSN.Count != 1)
                                {
                                    return r.IsSelected = (r.GroupParam.GroupSN[1] == drawobject.GroupParam.GroupSN[1]);
                                }
                                return false;
                            });
                            selected.AddRange(tempResult);
                        }
                        else
                        {
                            drawobject.IsSelected = true;
                            selected.Add(drawobject);
                        }
                    }
                }
            }
            return selected;
        }

        public IDrawObject GetHitObject(UnitPoint point)
        {
            if (this.DrawingLayer.Visible)
            {
                foreach (IDrawObject drawobject in this.DrawingLayer.Objects)
                {
                    if (drawobject.PointInObject(point) && drawobject.IsLocked == false)
                    {
                        return drawobject;
                    }
                }
            }
            return null;
        }

        public void MoveNodes(UnitPoint position, List<INodePoint> nodePoints)
        {
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandNodeMove(nodePoints));
            }
            foreach (INodePoint node in nodePoints)
            {
                node.SetPosition(position);
                node.Finish();
            }
        }

        public void MoveObjects(UnitPoint offset, List<IDrawObject> drawObjects)
        {
            if (offset.X == 0 && offset.Y == 0) return;
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandMove(offset, drawObjects));
            }
            foreach (IDrawObject drawObject in drawObjects)
            {
                drawObject.Move(offset);
            }
        }

        public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, Type[] runningSnapTypes, Type userSnapType)
        {
            List<IDrawObject> objects = GetHitObjects(canvas, point);
            if (objects.Count == 0)
                return null;
            foreach (IDrawObject obj in objects)
            {
                ISnapPoint snap = obj.SnapPoint(canvas, point, objects, runningSnapTypes, userSnapType);
                if (snap != null)
                    return snap;
            }
            return null;
        }

        #region 图形操作 for redo and undo

        public void SetOverCutting(List<IDrawObject> drawObjects, float pos, bool roundCut)
        {
            if (drawObjects != null)
            {
                for (int i = 0; i < drawObjects.Count; i++)
                {
                    ((IDrawTranslation)drawObjects[i]).OverCutting(pos, roundCut);
                }
                if (this.undoRedoBuffer.CanCapture)
                {
                    this.undoRedoBuffer.AddCommand(new EditCommandOverCutting(drawObjects, pos, roundCut));
                }
            }
        }
        public void ReverseDirection(List<IDrawObject> drawObjects)
        {
            if (drawObjects != null)
            {
                foreach (var item in drawObjects)
                {
                    ((IDrawTranslation)item).ReverseDirection();
                }
                if (this.undoRedoBuffer.CanCapture)
                {
                    this.undoRedoBuffer.AddCommand(new EditCommandReverseDirection(drawObjects));
                }
            }
        }
        public void DoMirror(List<IDrawObject> drawObjects, double A, double B, double C)
        {
            foreach (var item in drawObjects)
            {
                ((IDrawTranslation)item).Mirroring(A, B, C);
            }
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandMirror(drawObjects, A, B, C));
            }
        }

        public void DoAligment(List<UnitPoint> offsets, List<IDrawObject> drawObjects)
        {
            if (drawObjects != null && drawObjects.Count > 0)
            {
                for (int i = 0; i < drawObjects.Count; i++)
                {
                    drawObjects[i].Move(offsets[i]);
                }
                if (this.undoRedoBuffer.CanCapture)
                {
                    this.undoRedoBuffer.AddCommand(new EditCommandAligment(offsets, drawObjects));
                }
            }
        }

        public void DoSizeChange(List<IDrawObject> drawObjects, double centerX, double centerY, double width, double height)
        {
            foreach (var item in drawObjects)
            {
                ((IDrawTranslation)item).DoSizeChange(centerX, centerY, width, height);
            }
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandScale(drawObjects, centerX, centerY, width, height));
            }
        }

        public void DoRotate(List<IDrawObject> drawObjects, UnitPoint rotateCenter, double rotateAngle, bool isClockwise)
        {
            foreach (var item in drawObjects)
            {
                ((IDrawTranslation)item).DoRotate(rotateCenter, rotateAngle, isClockwise);
            }
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandRotate(drawObjects, rotateCenter, rotateAngle, isClockwise));
            }
        }

        public void DoSetLeadwire(List<IDrawObject> drawObjects, List<LineInOutParamsModel> leadwireModels)
        {
            List<LineInOutParamsModel> leadwireModelOlds = new List<LineInOutParamsModel>();
            foreach (var item in drawObjects)
            {
                LineInOutParamsModel leadwireModelOld;
                if (item.LeadInOutParams.LineInType != item.LeadIn.LeadType || item.LeadInOutParams.LineOutType != item.LeadOut.LeadType)
                {
                    leadwireModelOld = new LineInOutParamsModel();
                    leadwireModelOld.LineInType = item.LeadIn.LeadType;
                    leadwireModelOld.LineInLength = item.LeadIn.Length;
                    leadwireModelOld.LineInAngle = item.LeadIn.Angle;
                    leadwireModelOld.LineInRadius = item.LeadIn.ArcRadius;
                    leadwireModelOld.IsAddCircularHole = item.LeadIn.LeadByHole;
                    leadwireModelOld.CircularHoleRadius = item.LeadIn.LeadHoleRadius;
                    leadwireModelOld.LineOutType = item.LeadOut.LeadType;
                    leadwireModelOld.LineOutLength = item.LeadOut.Length;
                    leadwireModelOld.LineOutAngle = item.LeadOut.Angle;
                    leadwireModelOld.LineOutRadius = item.LeadOut.ArcRadius;
                    //其余部分参数在wxf文件中没有保存，所以无法判别，产生bug如下:当是在最长边引入(起点在最长边的一半位置)，重新打开文件，按撤销会把起点会回到原始点
                }
                else
                {
                    leadwireModelOld = item.LeadInOutParams;
                }
                leadwireModelOlds.Add(leadwireModelOld);
            }
            int i = 0;
            foreach (var item in drawObjects)
            {
                ((IDrawTranslation)item).DoSetLeadwire(leadwireModels[i]);
                i++;
            }
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandSetLeadwire(drawObjects, leadwireModels, leadwireModelOlds));
            }
        }

        public void DoSort(Dictionary<int, int> indexs)
        {
            for (int i = 0; i < this.DrawingLayer.Objects.Count; i++)
            {
                this.DrawingLayer.Objects[i].GroupParam.FigureSN = indexs[i + 1];
            }
            this.DrawingLayer.Objects = this.DrawingLayer.Objects.OrderBy(r => r.GroupParam.FigureSN).ToList();
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandSort(indexs));
            }
            this.UpdateSN();
        }
        public void DoCompensation(List<IDrawObject> drawObjects, List<CompensationModel> parms)
        {
            if (drawObjects == null || drawObjects.Count == 0) return;
            var oldParams = CopyUtil.DeepCopy(drawObjects.Select(e => e.CompensationParam).ToList());
            for (int i = 0; i < drawObjects.Count; i++)
            {
                ((IDrawTranslation)drawObjects[i]).DoCompensation(parms[i]);
            }
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandCompensation(drawObjects, oldParams));
            }
        }
        public void DoCornerRing(List<IDrawObject> drawObjects, List<CornerRingModel> parms)
        {
            if (drawObjects == null || drawObjects.Count == 0) return;
            var oldParams = CopyUtil.DeepCopy(drawObjects.Select(e => e.CornerRingParam).ToList());
            for (int i = 0; i < drawObjects.Count; i++)
            {
                ((IDrawTranslation)drawObjects[i]).DoCornerRing(parms[i]);
            }
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandCornerRing(drawObjects, oldParams));
            }
        }
        public void DoOuterCut(List<IDrawObject> drawObjects)
        {
            if (drawObjects == null || drawObjects.Count == 0) return;
            var oldParams = CopyUtil.DeepCopy(drawObjects.Select(e => e.CompensationParam).ToList());
            for (int i = 0; i < drawObjects.Count; i++)
            {
                drawObjects[i].SetOuterCut();
            }
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandCompensation(drawObjects, oldParams));
            }
        }
        public void DoInnerCut(List<IDrawObject> drawObjects)
        {
            if (drawObjects == null || drawObjects.Count == 0) return;
            var oldParams = CopyUtil.DeepCopy(drawObjects.Select(e => e.CompensationParam).ToList());
            for (int i = 0; i < drawObjects.Count; i++)
            {
                drawObjects[i].SetInnerCut();
            }
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandCompensation(drawObjects, oldParams));
            }
        }
        public void DoMicroConnect(List<IDrawObject> drawObjects, List<List<MicroConnectModel>> parms, bool isUpdate)
        {
            if (drawObjects == null || drawObjects.Count == 0) return;
            if (isUpdate)
            {
                var oldParams = CopyUtil.DeepCopy(drawObjects.Select(e => e.MicroConnParams).ToList());
                for (int i = 0; i < drawObjects.Count; i++)
                {
                    drawObjects[i].MicroConnParams = CopyUtil.DeepCopy(parms[i]);
                    drawObjects[i].Update();
                }
                if (this.undoRedoBuffer.CanCapture)
                {
                    this.undoRedoBuffer.AddCommand(new EditCommandMicroConnect(drawObjects, oldParams));
                }
            }
            else
            {
                if (this.undoRedoBuffer.CanCapture)
                {
                    this.undoRedoBuffer.AddCommand(new EditCommandMicroConnect(drawObjects, parms));
                }
            }
        }

        public void DoGroup(List<List<IDrawObject>> drawObjects, bool isUndoScatter = false)
        {
            for (int i = 0; i < drawObjects.Count; i++)
            {
                int sn = ++GlobalModel.TotalGroupCount;
                drawObjects[i].ForEach(r => {
                    r.GroupParam.GroupSN.Insert(1, sn);
                    if (isUndoScatter)
                    {
                        //如果是撤销打散  打散时隐藏的飞切参数重新显示
                        Circle c = r as Circle;
                        if (c != null && c.IsFlyingCutScatter)
                        {
                            c.IsFlyingCutScatter = false;
                        }
                    }
                });
                //排序
                List<IDrawObject> sortObj = drawObjects[i].OrderBy(r => r.GroupParam.FigureSN).ToList();
                int minIndex = sortObj[0].GroupParam.FigureSN;
                List<IDrawObject> remainObj = this.DrawingLayer.Objects
                    .Where(r => (r.GroupParam.FigureSN > minIndex && !drawObjects[i].Contains(r))).OrderBy(r => r.GroupParam.FigureSN).ToList();
                sortObj.ForEach(r => { r.GroupParam.GroupSN[0] = 0; r.GroupParam.FigureSN = minIndex++; });
                remainObj.ForEach(r => r.GroupParam.FigureSN = minIndex++);
                sortObj[0].GroupParam.GroupSN[0] = sortObj[0].GroupParam.FigureSN;
            }
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandGroup(drawObjects));
            }
            this.UpdateSN();
        }
        public void DoBridge(List<IDrawObject> newObjects, List<IDrawObject> oldObjects, bool isUpdate)
        {
            //添加新的图形，删除旧的图形
            oldObjects?.ForEach(o => this.DrawingLayer.Objects.Remove(o));
            if (isUpdate)
            {
                this.DrawingLayer.AddObjectOnDrawing(newObjects);
                //排序，更新SN号
                this.DrawingLayer.Objects.Sort((x, y) =>
                {
                    if (x.GroupParam.FigureSN > y.GroupParam.FigureSN)
                        return 1;
                    else return -1;
                });
                int sn = 0;
                this.DrawingLayer.Objects.ForEach(e => e.GroupParam.FigureSN = ++sn);
                GlobalModel.TotalDrawObjectCount = sn;
            }
            else
            {
                //排序，更新SN号
                int count = this.DrawingLayer.Objects.Count + newObjects.Count;
                int index = 0;
                for (int sn = 1; sn <= count; sn++)
                {
                    if (!newObjects.Exists(n => n.GroupParam.FigureSN == sn))
                    {
                        this.DrawingLayer.Objects[index++].GroupParam.FigureSN = sn;
                    }
                }
                newObjects?.ForEach(o => this.DrawingLayer.Objects.Insert(o.GroupParam.FigureSN - 1, o));
                GlobalModel.TotalDrawObjectCount = count;
            }
            this.UpdateSN();
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandBridge(newObjects, oldObjects));
            }
        }

        public void DoGroupScatter(List<List<IDrawObject>> drawObjects)
        {
            for (int i = 0; i < drawObjects.Count; i++)
            {
                drawObjects[i].ForEach(r =>
                {
                    r.GroupParam.GroupSN[0] = 0;
                    r.GroupParam.GroupSN.RemoveAt(1);
                    Circle circle = r as Circle;
                    if (circle != null && circle.FlyingCutLeadOut != null)
                    {
                        circle.IsFlyingCutScatter = true;//隐藏飞切参数(只能隐藏，不能删除。撤销打散可以恢复)
                    }
                });
                List<IDrawObject> temp = drawObjects[i].FindAll(s => s.GroupParam.GroupSN.Count > 1);
                if (temp.Count > 0)
                {
                    GroupChildDrawobjtce(temp);
                }
            }

            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandGroupScatter(drawObjects));
            }
            this.UpdateSN();
            ///处理打散后数据
            void GroupChildDrawobjtce(List<IDrawObject> temp)
            {
                List<int> lgn = new List<int>();
                foreach (var item in temp)
                {
                    int gn = item.GroupParam.GroupSN[1];
                    if (lgn.Contains(gn) == false)
                    { lgn.Add(gn); }
                }
                foreach (var item in lgn)
                {
                    List<IDrawObject> t1 = temp.FindAll(s => s.GroupParam.GroupSN[1] == item);
                    int sn = t1.Min(s => s.GroupParam.FigureSN);
                    t1.OrderByDescending(s => s.GroupParam.FigureSN);
                    t1[0].GroupParam.GroupSN[0] = sn;
                }
            }
        }

        private void UpdateSN()
        {
            int index = 0;
            this.DrawingLayer.Objects = this.DrawingLayer.Objects.OrderBy(r => r.GroupParam.FigureSN).ToList();
            this.DrawingLayer.Objects.ForEach(r =>
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
        #endregion

        #region 阵列
        public void ArrayObjects(List<IDrawObject> DelDrawObject, List<IDrawObject> AddDrawObjects)
        {
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandArray(DelDrawObject, AddDrawObjects));
            }
            if (DelDrawObject != null)
            {
                this.DrawingLayer.DeleteObjects(DelDrawObject);
            }
            if (AddDrawObjects != null)
            {
                foreach (var item in AddDrawObjects)
                {
                    this.DrawingLayer.AddObjectOnRedoUndo(item);
                }
            }
            //this.DrawingLayer.AddObjectOnDrawing(AddDrawObjects);
        }
        #endregion

        #region 直线飞切 jiang

        public void DoArcFlyingCut(List<IDrawObject> drawObjects, List<IDrawObject> flyArcGroups = null, List<IDrawObject> drawObjectsnew = null, List<IDrawObject> drawObjectsold = null)
        {
            List<IDrawObject> NewDrawobjects = new List<IDrawObject>();
            if (flyArcGroups != null)
            {
                Circle circle = null;
                Circle newCircle = null;

                foreach (var idraw in flyArcGroups)
                {
                    //按零件飞起，可能有图形不是圆
                    if(!(idraw is Circle))
                    {
                        NewDrawobjects.Add(idraw.Clone());
                        continue;
                    }

                    circle = idraw as Circle;
                    //因超过设定距离，该group只有一个圆
                    if (circle.GroupParam.GroupSN[1] == 0)
                    {
                        NewDrawobjects.Add(circle.Clone());
                        ((Circle)NewDrawobjects[NewDrawobjects.Count - 1]).IsFlyingCut = false;
                        continue;
                    }
                    // 起点修改 StartAngle 
                    float StartAngle = (float)HitUtil.RadiansToDegrees(
                        HitUtil.LineAngleR(circle.Center, circle.StartMovePoint, 0));

                    newCircle = new Circle()
                    {
                        Center = circle.Center,
                        Radius = circle.Radius,
                        StartAngle = StartAngle,
                        StartMovePoint = circle.StartMovePoint,
                        EndMovePoint = circle.StartMovePoint,
                        IsFlyingCut = true,
                        FlyingCutLeadOut = circle.FlyingCutLeadOut,
                        GroupParam = circle.GroupParam
                    };
                    newCircle.LeadIn.Position = StartAngle / 360.0f;
                    newCircle.LeadOut.Position = StartAngle / 360.0f;
                    NewDrawobjects.Add(newCircle);
                }

                if (NewDrawobjects.Count == 0)
                    return;
            }

            if (drawObjectsnew == null)
            {
                if (NewDrawobjects == null || NewDrawobjects.Count <= 0)//重做
                {
                    NewDrawobjects = drawObjects.ToList();
                    this.DrawingLayer.DeleteObjects(drawObjectsold);
                }
                else
                {
                    int newCount = NewDrawobjects.Count;
                    int groupCount = NewDrawobjects.Max(o => o.GroupParam.GroupSN.Count > 1 ? o.GroupParam.GroupSN[1]: 0);
                    int minSelectedSn = drawObjects.Min(idraw => idraw.GroupParam.FigureSN);

                    //未被选中或者圆以外的图形，大于飞切的最小FigureSN的 加入到变化图形集合(方便做撤销和恢复)
                    int noChange = 0;
                    this.DrawingLayer.Objects.ForEach(idraw =>
                    {
                        if (!drawObjects.Contains(idraw) && idraw.GroupParam.FigureSN > minSelectedSn && !flyArcGroups.Contains(idraw))
                        {
                            drawObjects.Insert(noChange, idraw);
                            NewDrawobjects.Insert(noChange, idraw.Clone());
                            noChange++;
                        }
                    });

                    this.DrawingLayer.DeleteObjects(drawObjects);
                    int sn = this.DrawingLayer.Objects.Count;

                    int idx = 0;
                    NewDrawobjects.ForEach(r =>
                    {
                        r.GroupParam.FigureSN = ++sn;
                    });

                    idx = 0;
                    int baseGroupSN = GlobalModel.TotalGroupCount;
                    int previousGroupSN = -1;
                    int currentGroupSN = 0;
                    NewDrawobjects.ForEach(r =>
                    {
                        if (++idx > noChange && r.GroupParam.GroupSN.Count > 1)
                        {
                            if (r.GroupParam.GroupSN[1] == 0)
                            {
                                r.GroupParam.GroupSN.RemoveAt(1);
                            }
                            else
                            {
                                currentGroupSN = baseGroupSN + r.GroupParam.GroupSN[1];
                                r.GroupParam.GroupSN.RemoveAt(1);
                                r.GroupParam.GroupSN.Insert(1, currentGroupSN);
                                if (previousGroupSN != currentGroupSN)
                                {
                                    r.GroupParam.GroupSN[0] = r.GroupParam.FigureSN;
                                }
                                previousGroupSN = currentGroupSN;
                            }
                        }
                    });
                    GlobalModel.TotalGroupCount = GlobalModel.TotalGroupCount + groupCount;
                }

                GlobalModel.TotalDrawObjectCount = GlobalModel.TotalDrawObjectCount + NewDrawobjects.Count;
                this.DrawingLayer.AddObjectOnDrawing(NewDrawobjects);

                if (this.undoRedoBuffer.CanCapture)
                {
                    Circle circle = null;
                    foreach (var item in drawObjects)
                    {
                        circle = item as Circle;
                        if (circle != null)
                        {
                            circle.FlyingCutLeadOut = null;
                        }
                    }
                    drawObjects = drawObjects.OrderBy(c => c.GroupParam.FigureSN).ToList();
                    this.undoRedoBuffer.AddCommand(new EditCommandArcFlyingCut(drawObjects, NewDrawobjects));
                }
                this.UpdateSN();
            }
            else
            {
                ((DrawingLayer)this.DrawingLayer).DeleteObjects(drawObjectsnew);
                foreach (var item in drawObjects)
                    this.DrawingLayer.AddObjectOnRedoUndo(item);
            }
        }

        public void DoLineFlyingCut(List<IDrawObject> drawObjects, List<List<LineFlyCut>> flyLineGroups = null, int Gsn=0, List<IDrawObject> drawObjectsold = null)
        {
            List<IDrawObject> NewDrawobjects = new List<IDrawObject>();
            if (flyLineGroups != null)
            {
                //int dxCount = 0;
                foreach (var item in flyLineGroups)
                {
                    #region 已注释 直线上的多段分开
                    //foreach (var item1 in item)
                    //{
                    //    DrawTool.MultiSegmentLine.MultiSegmentLineBase multiSegmentLineBase = new DrawTool.MultiSegmentLine.MultiSegmentLineBase();
                    //    var obj = multiSegmentLineBase as DrawTool.MultiSegmentLine.MultiSegmentLineBase;

                    //    List<UnitPointBulge> s = new List<UnitPointBulge>();
                    //    s.Add(new UnitPointBulge(item1.StartPoint));
                    //    s.Add(new UnitPointBulge(item1.EndPoint));

                    //    obj.Points = s;
                    //    obj.IsCloseFigure = false;

                    //    multiSegmentLineBase.Update();
                    //    NewDrawobjects.Add(multiSegmentLineBase);
                    //}
                    #endregion

                    // 直线上的多段和虚线当中一个多段线
                    DrawTool.MultiSegmentLine.MultiSegmentLineBase multiSegmentLineBase = new DrawTool.MultiSegmentLine.MultiSegmentLineBase();
                    var obj = multiSegmentLineBase as DrawTool.MultiSegmentLine.MultiSegmentLineBase;
                    List<UnitPointBulge> s = new List<UnitPointBulge>();

                    for (int idx = 0; idx < item.Count; idx++)
                    {
                        var item1 = item[idx];
                        s.Add(new UnitPointBulge() { Bulge = double.NaN, Point = item1.StartPoint, Dotted = false });
                        s.Add(new UnitPointBulge() { Bulge = double.NaN, Point = item1.EndPoint, Dotted = true });
                        if (idx == item.Count - 1 && item1.ConnectLineStartPoint != null && !double.IsNaN(item1.ConnectLineStartPoint.X))
                        {
                            //obj.BezierParam = new Tuple<double, double, UnitPoint, UnitPoint>(0.5d, 1d, item1.ConnectLineStartPoint, item1.ConnectLineEndPoint);
                            obj.BezierParam = new BezierParamModel()
                            {
                                LeadlineDistance = 0.5d,
                                BezierWide = 1.0d,
                                ConnectStartPoint = item1.ConnectLineStartPoint,
                                ConnectEndPoint = item1.ConnectLineEndPoint,

                            };
                        }
                    }
                    obj.Points = s;
                    obj.IsCloseFigure = false;

                    multiSegmentLineBase.Update();
                    NewDrawobjects.Add(multiSegmentLineBase);
                }
            }

            if (Gsn == 0)
            {
                if (NewDrawobjects == null || NewDrawobjects.Count <= 0)//重做
                {
                    NewDrawobjects = drawObjects.ToList();
                    this.DrawingLayer.DeleteObjects(drawObjectsold);
                }
                else
                {
                    this.DrawingLayer.DeleteObjects(drawObjects);
                    int gsn = ++GlobalModel.TotalGroupCount;
                    NewDrawobjects.ForEach(r => r.GroupParam.GroupSN.Insert(1, gsn));
                    int sn = GlobalModel.TotalDrawObjectCount;
                    NewDrawobjects.ForEach(r => r.GroupParam.FigureSN = ++sn);
                    NewDrawobjects[0].GroupParam.GroupSN[0] = NewDrawobjects[0].GroupParam.FigureSN;
                }
                GlobalModel.TotalDrawObjectCount = GlobalModel.TotalDrawObjectCount + NewDrawobjects.Count;
                this.DrawingLayer.AddObjectOnDrawing(NewDrawobjects);
                if (this.undoRedoBuffer.CanCapture)
                {
                    this.undoRedoBuffer.AddCommand(new EditCommandLineFlyingCut(drawObjects, NewDrawobjects));
                }
                this.UpdateSN();
            }
            else
            {
                List<IDrawObject> NewDrawobjects1 = this.DrawingLayer.Objects.FindAll(s => s.GroupParam.GroupSN.Count > 1 && s.GroupParam.GroupSN[1] == Gsn).ToList();
                ((DrawingLayer)this.DrawingLayer).DeleteObjects(NewDrawobjects1);

                foreach (var item in drawObjects)
                {
                    this.DrawingLayer.AddObjectOnRedoUndo(item);
                }
            }
        }
        #endregion

        #region 共边
        public void DoCommonSide(List<IDrawObject> drawObjects, List<List<UnitPoint>> unitPoints = null, int Gsn = 0, List<IDrawObject> drawObjectsold = null)
        {
            List<IDrawObject> NewDrawobjects = new List<IDrawObject>();
            if (unitPoints != null)
            {
                foreach (var item in unitPoints)
                {
                    DrawTool.MultiSegmentLine.MultiSegmentLineBase multiSegmentLineBase = new DrawTool.MultiSegmentLine.MultiSegmentLineBase();
                    var obj = multiSegmentLineBase as DrawTool.MultiSegmentLine.MultiSegmentLineBase;

                    List<UnitPointBulge> s = new List<UnitPointBulge>();
                    foreach (var item1 in item)
                    {
                        s.Add(new UnitPointBulge(item1));
                    }
                    obj.Points = s;
                    if (item[0] == item[item.Count - 1])
                    {
                        obj.IsCloseFigure = true;
                    }
                    multiSegmentLineBase.Update();
                    NewDrawobjects.Add(multiSegmentLineBase);
                }
            }

            if (Gsn == 0)
            {
                if (NewDrawobjects == null || NewDrawobjects.Count <= 0)//重做
                {
                    NewDrawobjects = drawObjects.ToList();
                    this.DrawingLayer.DeleteObjects(drawObjectsold);
                }
                else
                {
                    this.DrawingLayer.DeleteObjects(drawObjects);
                    int gsn = ++GlobalModel.TotalGroupCount;
                    NewDrawobjects.ForEach(r => r.GroupParam.GroupSN.Insert(1, gsn));
                    int sn = GlobalModel.TotalDrawObjectCount;
                    NewDrawobjects.ForEach(r => r.GroupParam.FigureSN = ++sn);
                    NewDrawobjects[0].GroupParam.GroupSN[0] = NewDrawobjects[0].GroupParam.FigureSN;

                }
                GlobalModel.TotalDrawObjectCount = GlobalModel.TotalDrawObjectCount + NewDrawobjects.Count;
                this.DrawingLayer.AddObjectOnDrawing(NewDrawobjects);
                if (this.undoRedoBuffer.CanCapture)
                {
                    this.undoRedoBuffer.AddCommand(new EditCommandCommonSide(drawObjects, NewDrawobjects));
                }
                this.UpdateSN();
            }
            else
            {
                List<IDrawObject> NewDrawobjects1 = this.DrawingLayer.Objects.FindAll(s => s.GroupParam.GroupSN.Count > 1 && s.GroupParam.GroupSN[1] == Gsn).ToList();
                ((DrawingLayer)this.DrawingLayer).DeleteObjects(NewDrawobjects1);

                foreach (var item in drawObjects)
                {
                    this.DrawingLayer.AddObjectOnRedoUndo(item);
                }
            }
        }
        #endregion


        #region 倒圆角
        public void DoRoundAngle(List<IDrawObject> curDrawObjects, List<IDrawObject> oldDrawObjects)
        {
            if (oldDrawObjects != null)
            {
                this.DrawingLayer.DeleteObjects(oldDrawObjects);
            }
            if (curDrawObjects != null)
            {
                foreach (var item in curDrawObjects)
                {
                    this.DrawingLayer.AddObjectOnRedoUndo(item);
                }
            }
            if (this.undoRedoBuffer.CanCapture)
            {
                this.undoRedoBuffer.AddCommand(new EditCommandRoundAngle(curDrawObjects, oldDrawObjects));
            }
        }
        #endregion
        #endregion
    }
}
