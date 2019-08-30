using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.LeadLine;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.DrawModel.RingCut;
using WSX.CommomModel.ParaModel;

namespace WSX.DrawService.CanvasControl
{
    /// <summary>
    /// 图形对象接口
    /// </summary>
    public interface IDrawObject
    {
        string Id { get; }
        double SizeLength { get; }
        bool IsCompleteDraw { get; set; }
        FigureTypes FigureType { get; }
        bool IsCloseFigure { get; }
        bool IsSelected { get; set; }
        bool IsInnerCut { get; set; }//阴切阳切
        float OverCutLen { get; set; }
        GroupParam GroupParam { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        bool IsVisiblity { get; set; }
        /// <summary>
        /// 是否锁住
        /// </summary>
        bool IsLocked { get; set; }
        LineInOutParamsModel LeadInOutParams { get; set; }
        UnitPoint FirstDrawPoint { get;  }
        IDrawObject Clone();
        bool PointInObject(UnitPoint unitPoint);
        bool ObjectInRectangle(RectangleF rectangle, bool anyPoint);
        void Draw(ICanvas canvas, RectangleF unitRectangle);
        RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType);
        void OnMouseMove(ICanvas canvas, UnitPoint unitPoint);
        DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint, MouseEventArgs e);
        void OnMouseUp(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint);
        void OnKeyDown(ICanvas canvas, KeyEventArgs e);
        UnitPoint RepeatStartingPoint { get; }
        INodePoint NodePoint(UnitPoint unitPoint);
        ISnapPoint SnapPoint(ICanvas canvas, UnitPoint unitPoint, List<IDrawObject> drawObjects, Type[] runningSnapTypes, Type userSnapType);
        void Move(UnitPoint offset);
        string GetInfoAsString();

        #region 显示附加信息

        /// <summary>
        /// 显示图形外框
        /// </summary>
        void ShowBoundRect(ICanvas canvas, int penIndex);

        /// <summary>
        /// 显示序号
        /// </summary>
        void ShowFigureSN(ICanvas canvas);

        /// <summary>
        /// 显示路径起点
        /// </summary>
        void ShowStartMovePoint(ICanvas canvas);

        /// <summary>
        /// 显示微连标记
        /// </summary>
        void ShowMicroConnectFlag(ICanvas canvas);

        #endregion

        #region 图形附加操作
        List<MicroConnectModel> MicroConnParams { get; set; }
        LeadInLine LeadIn { get; set; }
        LeadOutLine LeadOut { get; set; }
        UnitPoint StartMovePoint { get; set; }
        UnitPoint EndMovePoint { get; }
        void UpdateLeadwire();
        void Update();
        void SetMicroConnectParams(bool isCancel, UnitPoint unitPoint, float microConnLen);
        void SetAutoMicroConnect(AutoMicroConParam param);
        CompensationModel CompensationParam { get; set; }
        void SetOuterCut();
        void SetInnerCut();
        CornerRingModel CornerRingParam { get; set; }
        void SetCoolPoint(UnitPoint unitPoint, bool isLeadIn = false, bool isCorner = false, double maxAngle = 0);
        void ClearCoolPoint();
        #endregion



    }
}
