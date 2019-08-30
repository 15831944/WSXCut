using System;
using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.DrawModel.RingCut;
using WSX.CommomModel.ParaModel;
using WSX.DrawService.DrawTool.CircleTool;
using WSX.DrawService.Layers;
using WSX.DrawService.Utils;

namespace WSX.DrawService.CanvasControl
{
    public interface IModel
    {
        /// <summary>
        /// 缩放比例
        /// </summary>
        float Zoom { get; set; }

        DrawingPattern DrawingPattern { get; set; }

        /// <summary>
        /// 背景层
        /// </summary>
        ICanvasLayer BackgroundLayer { get; }

        /// <summary>
        /// 网格层
        /// </summary>
        ICanvasLayer GridLayer { get; }

        /// <summary>
        /// 绘图层
        /// </summary>
        DrawingLayer DrawingLayer { get; }

        IDrawObject CreateObject(string type, UnitPoint point, ISnapPoint snapPoint);
        void Clear();

        /// <summary>
        /// 在指定的图层添加图形对象
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="drawObject"></param>
        void AddObjectOnDrawing(IDrawObject drawObject);
        void AddObjectOnDrawing(List<IDrawObject> drawObjects);

        void AddObjectOnRedoUndo(IDrawObject drawObject);
        /// <summary>
        /// 删除图形对象
        /// </summary>
        /// <param name="drawObjects"></param>
        void DeleteObjects(List<IDrawObject> drawObjects);

        /// <summary>
        /// 移动图形对象
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="drawObjects"></param>
        void MoveObjects(UnitPoint offset, List<IDrawObject> drawObjects);

        /// <summary>
        /// 剪贴板中的图形对象
        /// </summary>
        List<IDrawObject> CopyObjectInClipBoard { get; }

        /// <summary>
        /// 复制图形对象
        /// </summary>
        /// <param name="point"></param>
        /// <param name="drawObjects"></param>
        void CopyObjects(UnitPoint point, List<IDrawObject> drawObjects);

        void Copy();

        void PasteObjects(UnitPoint offset);

        /// <summary>
        /// 移动节点
        /// </summary>
        /// <param name="position"></param>
        /// <param name="nodePoints"></param>
        void MoveNodes(UnitPoint position, List<INodePoint> nodePoints);

        /// <summary>
        /// 获取画布上选中矩形区域内的对象集合
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="selection"></param>
        /// <param name="anyPoint"></param>
        /// <returns></returns>
        List<IDrawObject> GetHitObjects(ICanvas canvas, RectangleF selection, bool anyPoint);

        /// <summary>
        /// 根据鼠标位置获取命中图形对象
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        List<IDrawObject> GetHitObjects(ICanvas canvas, UnitPoint point);

        /// <summary>
        /// 根据鼠标点获取单个命中图形对象
        /// </summary>
        /// <param name="unitPoint">鼠标点</param>
        /// <returns></returns>
        IDrawObject GetHitObject(UnitPoint unitPoint);

        /// <summary>
        /// 添加选中图形对象
        /// </summary>
        /// <param name="drawObject"></param>
        void AddSelectedObject(IDrawObject drawObject);

        /// <summary>
        /// 移除选中图形对象
        /// </summary>
        /// <param name="drawObject"></param>
        void RemoveSelectedObject(IDrawObject drawObject);

        /// <summary>
        /// 选中的图形对象集合
        /// </summary>
        //List<IDrawObject> SelectedObjects { get; }

        /// <summary>
        /// 选中的图形对象数量
        /// </summary>
        int SelectedCount { get; }

        /// <summary>
        /// 清除选中的图形对象
        /// </summary>
        void ClearSelectedObjects();

        ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, Type[] runningSnapTypes, Type userSnapType);

        bool CanUndo();
        bool DoUndo();
        bool CanRedo();
        bool DoRedo();

        #region 图形操作（需要撤销重做）

        void SetOverCutting(List<IDrawObject> drawObjects, float pos, bool roundCut);

        void ReverseDirection(List<IDrawObject> drawObjects);

        void DoMirror(List<IDrawObject> drawObjects, double A, double B, double C);

        void DoAligment(List<UnitPoint> offsets, List<IDrawObject> drawObjects);
        void DoSizeChange(List<IDrawObject> drawObjects, double centerX, double centerY, double width, double height);

        void DoRotate(List<IDrawObject> drawObjects, UnitPoint rotateCenter, double rotateAngle, bool isClockwise);

        void DoSetLeadwire(List<IDrawObject> drawObjects, List<LineInOutParamsModel> leadwireModel);
        void DoSort(Dictionary<int, int> indexs);
        void DoCompensation(List<IDrawObject> drawObjects, List<CompensationModel> parms);
        void DoCornerRing(List<IDrawObject> drawObjects, List<CornerRingModel> parms);
        void DoOuterCut(List<IDrawObject> drawObjects);
        void DoInnerCut(List<IDrawObject> drawObjects);
        void DoMicroConnect(List<IDrawObject> drawObjects, List<List<MicroConnectModel>> parms, bool isUpdate);
        void DoGroup(List<List<IDrawObject>> drawObjects, bool isUndoScatter = false);
        void DoGroupScatter(List<List<IDrawObject>> drawObjects);
        void DoBridge(List<IDrawObject> newObjects, List<IDrawObject> oldObjects,bool isUpdate);
        void ArrayObjects(List<IDrawObject> deldrawObjects, List<IDrawObject> drawObjectsNew);

        void DoLineFlyingCut(List<IDrawObject> deldrawObjects, List<List<LineFlyCut>> flyLineGroups, int Gsn, List<IDrawObject> deldrawObjectsold);

        void DoArcFlyingCut(List<IDrawObject> drawObjects, List<IDrawObject> flyArcGroups = null, List<IDrawObject> drawObjectsnew = null, List<IDrawObject> drawObjectsold = null);

        void DoCommonSide(List<IDrawObject> deldrawObjects, List<List<UnitPoint>> unitPoints, int Gsn, List<IDrawObject> deldrawObjectsold);
        void DoRoundAngle(List<IDrawObject> curDrawObjects, List<IDrawObject> oldDrawObjects);
        #endregion
    }
}