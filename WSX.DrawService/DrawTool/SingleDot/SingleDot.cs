using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.LeadLine;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.DrawModel.RingCut;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Utilities;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawModel;
using WSX.DrawService.Layers;
using WSX.DrawService.Utils;
using WSX.DrawService.Wrapper;
using WSX.GlobalData;
using WSX.GlobalData.Messenger;
using WSX.GlobalData.Model;
using static WSX.DrawService.DrawCommand;

namespace WSX.DrawService.DrawTool
{
    public class SingleDot : DrawObjectBase, IDrawObject, IDrawTranslation, IDrawData
    {
        private float radius = 6f;
        public bool IsCloseFigure
        {
            get
            {
                return false;
            }
        }
        public UnitPoint StartMovePoint
        {
            get
            {
                return this.P1;
            }
            set { }
        }
        public UnitPoint EndMovePoint
        {
            get
            {
                return this.P1;
            }
        }
        public bool IsInnerCut { get; set; }
        public UnitPoint P1 { get; set; }
        public float OverCutLen { get; set; }
        public List<MicroConnectModel> MicroConnParams { get; set; }
        public CompensationModel CompensationParam { get; set; }
        public CornerRingModel CornerRingParam { get; set; }
        public LeadInLine LeadIn { get; set; }
        public LeadOutLine LeadOut { get; set; }
        public static string ObjectType
        {
            get
            {
                return "SingleDot";
            }
        }

        public SingleDot()
        {
        }
        public void Update()
        {
            this.IsCompleteDraw = true;
        }
        public SingleDot(UnitPoint point, float width, Color color)
        {
            this.P1 = point;
            Width = width;
            this.IsSelected = false;
        }
        #region DrawObject
        public override void InitializeFromModel(UnitPoint unitPoint, ISnapPoint snapPoint)
        {
            this.GroupParam.FigureSN = ++GlobalModel.TotalDrawObjectCount;
            this.GroupParam.ShowSN = this.GroupParam.FigureSN;
            this.LayerId = (int)GlobalModel.CurrentLayerId;
            this.P1 = unitPoint;
            this.OnMouseDown(null, unitPoint, snapPoint, null);
            this.IsSelected = true;
        }
        #endregion
        public virtual void Copy(SingleDot singleDot)
        {
            base.Copy(singleDot);
            this.P1 = singleDot.P1;
            this.GroupParam = CopyUtil.DeepCopy(singleDot.GroupParam);
            this.IsSelected = singleDot.IsSelected;
        }

        //public static float ThresholdWidth(ICanvas canvas,)

        #region IDrawObject
        private bool isvisiblity = true;
        public bool IsVisiblity
        {
            get { return isvisiblity; }
            set { isvisiblity = value; }
        }
        private bool islocked = false;
        public bool IsLocked
        {
            get { return islocked; }
            set { islocked = value; }
        }
        public FigureTypes FigureType { get { return FigureTypes.Point; } }
        public virtual string Id
        {
            get
            {
                return ObjectType;
            }
        }
        public bool IsCompleteDraw { get; set; }
        public bool IsSelected { get; set; }
        public UnitPoint FirstDrawPoint { get; set; }
        public UnitPoint RepeatStartingPoint
        {
            get
            {
                return UnitPoint.Empty;
            }
        }

        public double SizeLength
        {
            get
            {
                return 0.0;
            }
        }

        public IDrawObject Clone()
        {
            SingleDot singleDot = new SingleDot();
            singleDot.Copy(this);
            return singleDot;
        }

        public virtual void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            if (IsVisiblity == false) return;
            var items = this.GetBasic();
            DrawUtils.Draw(canvas, items, 0);
            if (this.IsSelected)
            {
                DrawUtils.DrawNode(canvas, this.P1);
            }
            this.ShowFigureSN(canvas);
        }

        public RectangleF GetBoundingRectangle(BoundingRectangleType rectangleType)
        {
            RectangleF rect = HitUtil.CircleBoundingRectangle(this.P1, UCCanvas.GetThresholdWidth(this.radius));
            return rect;
        }

        public string GetInfoAsString()
        {
            return string.Format("SigleDot@{0} -SD=", this.P1.ToString());
        }

        public void Move(UnitPoint offset)
        {
            this.P1 = new UnitPoint(this.P1.X + offset.X, this.P1.Y + offset.Y);
        }

        public INodePoint NodePoint(UnitPoint unitPoint)
        {
            if (HitUtil.CircleHitPoint(this.P1, UCCanvas.GetThresholdWidth(this.radius), unitPoint))
            {
                return new NodePointSingleDot(this);
            }
            return null;
        }

        public bool ObjectInRectangle(RectangleF rectangle, bool anyPoint)
        {
            RectangleF rect = HitUtil.CircleBoundingRectangle(this.P1, UCCanvas.GetThresholdWidth(this.radius));
            if (anyPoint)
            {
                UnitPoint lp1 = new UnitPoint(rectangle.Left, rectangle.Top);
                UnitPoint lp2 = new UnitPoint(rectangle.Left, rect.Bottom);
                if (HitUtil.CircleIntersectWithLine(this.P1, 3, lp1, lp2))
                    return true;
                lp1 = new UnitPoint(rectangle.Left, rectangle.Bottom);
                lp2 = new UnitPoint(rectangle.Right, rectangle.Bottom);
                if (HitUtil.CircleIntersectWithLine(this.P1, 3, lp1, lp2))
                    return true;
                lp1 = new UnitPoint(rectangle.Left, rectangle.Top);
                lp2 = new UnitPoint(rectangle.Right, rectangle.Top);
                if (HitUtil.CircleIntersectWithLine(this.P1, 3, lp1, lp2))
                    return true;
                lp1 = new UnitPoint(rectangle.Left, rectangle.Top);
                lp2 = new UnitPoint(rectangle.Right, rectangle.Top);
                if (HitUtil.CircleIntersectWithLine(this.P1, 3, lp1, lp2))
                    return true;

                lp1 = new UnitPoint(rectangle.Right, rectangle.Top);
                lp2 = new UnitPoint(rectangle.Right, rectangle.Bottom);
                if (HitUtil.CircleIntersectWithLine(this.P1, 3, lp1, lp2))
                    return true;
            }
            return rectangle.Contains(rect);
        }

        public void OnKeyDown(ICanvas canvas, KeyEventArgs e)
        {

        }

        public DrawObjectMouseDown OnMouseDown(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint, MouseEventArgs e)
        {
            return DrawObjectMouseDown.Done;
        }

        public void OnMouseMove(ICanvas canvas, UnitPoint unitPoint)
        {
            this.P1 = unitPoint;
        }

        public void OnMouseUp(ICanvas canvas, UnitPoint point, ISnapPoint snapPoint)
        {

        }

        public bool PointInObject(UnitPoint unitPoint)
        {
            RectangleF rectangleF = this.GetBoundingRectangle(BoundingRectangleType.All);
            if (rectangleF.Contains(unitPoint.Point) == false)
            {
                return false;
            }
            float thresholdWidth = UCCanvas.GetThresholdWidth();
            if (HitUtil.PointInPoint(this.P1, unitPoint, thresholdWidth))
            {
                return true;
            }
            return HitUtil.IsPointInCircle(this.P1, this.radius, unitPoint, thresholdWidth / 2);
        }

        public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint unitPoint, List<IDrawObject> drawObjects, Type[] runningSnapTypes, Type userSnapType)
        {
            return null;
        }


        #region 图形附加操作

        public void SetMicroConnectParams(bool isCancel, UnitPoint unitPoint, float microConnLen)
        {
            throw new NotImplementedException();//孤点不需要做微连操作
        }
        public void SetAutoMicroConnect(AutoMicroConParam param)
        {
            throw new NotImplementedException();
        }
        public void UpdateMicroConnect()
        {
            throw new NotImplementedException();
        }
        public void SetOuterCut()
        {
            throw new NotImplementedException();
        }

        public void SetInnerCut()
        {
            throw new NotImplementedException();
        }
        public void SetCoolPoint(UnitPoint unitPoint, bool isLeadIn = false, bool isCorner = false, double maxAngle = 0)
        {
            throw new NotImplementedException();
        }

        public void ClearCoolPoint()
        {
            throw new NotImplementedException();
        }
        #region 引入引出

        public void UpdateLeadwire()
        {

        }

        #endregion
        #endregion


        #region 显示附加信息

        public void ShowBoundRect(ICanvas canvas, int penIndex)
        {
            throw new NotImplementedException();
        }

        public void ShowFigureSN(ICanvas canvas)
        {
            if (AdditionalInfo.Instance.IsShowFigureSN)
            {
                if (this.GroupParam.ShowSN != 0)
                {
                    canvas.DrawSN(canvas, this.GroupParam.ShowSN.ToString(), this.P1);
                }
            }
        }

        public void ShowStartMovePoint(ICanvas canvas)
        {
            throw new NotImplementedException();
        }

        public void ShowMicroConnectFlag(ICanvas canvas)
        {
            throw new NotImplementedException();
        }
        #endregion


        #endregion

        #region IDrawTranslate
        public void Mirroring(double A, double B, double C)
        {
            if (B == 0)//水平镜像
            {
                double centerAxis = -C / A;
                this.P1 = new UnitPoint(2 * centerAxis - this.P1.X, this.P1.Y);
            }
            else if (A == 0)
            {
                double centerAxis = -C / B;
                this.P1 = new UnitPoint(this.P1.X, 2 * centerAxis - this.P1.Y);
            }
            else
            {
                this.P1 = Utils.Utils.MirrorAnyAlgorithm(this.P1, A, B, C);
            }
        }

        public UnitPoint MaxValue
        {
            get
            {
                return this.P1;
            }
        }

        public UnitPoint MinValue
        {
            get
            {
                return this.P1;
            }
        }
        public LineInOutParamsModel LeadInOutParams { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public GroupParam GroupParam { get; set; } = new GroupParam() { GroupSN = new List<int>() { 0 } };

        public void ReverseDirection()
        {

        }

        public void OverCutting(float pos,bool roundCut)
        {
            throw new NotImplementedException();
        }

        public void DoSizeChange(double centerX, double centerY, double coffX, double coffY)
        {
            this.P1 = Utils.Utils.ScaleAlgorithm(this.P1, centerX, centerY, coffX, coffY);
        }

        public void DoRotate(UnitPoint rotateCenter, double rotateAngle, bool isClockwise)
        {
            rotateAngle = HitUtil.DegreesToRadians(rotateAngle);
            if (!isClockwise) rotateAngle = -rotateAngle;
            this.P1 = Utils.Utils.RotateAlgorithm(this.P1, rotateCenter, rotateAngle);
        }

        public void DoSetLeadwire(LineInOutParamsModel leadwireModel)
        {

        }
        public void DoCompensation(CompensationModel param)
        {

        }
        public void DoCornerRing(CornerRingModel param)
        {

        }
        #endregion

        #region IDrawData
        public List<IDrawLite> GetBasic()
        {
            var items = new List<IDrawLite>();
            Brush brush = new SolidBrush(Color);
            if (this.IsSelected)
            {
                //canvas.DrawDot(canvas, DrawUtils.CustomPens[this.LayerId], this.P1, (float)canvas.ToUnit(4f));
                var dot = new DotLite
                {
                    DrawPen = DrawUtils.CustomPens[this.LayerId],
                    Point = this.P1,
                    Radius = 4.0f
                };
                items.Add(dot);
            }
            else
            {
                //canvas.DrawDot(canvas, (Brush)DrawUtils.SelectecedBrush, this.P1, (float)canvas.ToUnit(4f));
                var dot = new DotLite
                {
                    DrawBrush = (Brush)DrawUtils.SelectecedBrush,
                    Point = this.P1,
                    Radius = 4.0f
                };
                items.Add(dot);
            }
            return items;
        }

        public List<IDrawLite> GetCompensation()
        {
            return null;
        }

        public List<IDrawLite> GetCornerRing()
        {
            return null;
        }

        public List<IDrawLite> GetCoolingPoints()
        {
            return null;
        }

        public List<IDrawLite> GetLeadIn()
        {
            return null;
        }

        public List<IDrawLite> GetLeadOut()
        {
            return null;
        }
        #endregion
    }
}
