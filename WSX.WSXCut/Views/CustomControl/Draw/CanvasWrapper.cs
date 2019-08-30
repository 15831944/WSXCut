using DevExpress.Utils.MVVM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.MicroConn;
using WSX.CommomModel.DrawModel.RingCut;
using WSX.CommomModel.ParaModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawTool;
using WSX.DrawService.Wrapper;
using WSX.Engine.Utilities;
using WSX.ViewModels.CustomControl.Draw;
using WSX.WSXCut.Utils;

namespace WSX.WSXCut.Views.CustomControl.Draw
{
    public partial class CanvasWrapper : UserControl
    {
        private CanvasViewModel viewModel;
        public event EventHandler OnSortDrawObjects;

        public CanvasWrapper()
        {
            InitializeComponent();
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            var context = new MVVMContext();
            context.ContainerControl = this;
            context.ViewModelType = typeof(CanvasViewModel);

            var fluent = context.OfType<CanvasViewModel>();
            this.viewModel = context.GetViewModel<CanvasViewModel>();
            this.viewModel.InjectHandler(needToSort =>
            {
                if (needToSort)
                {
                    this.OnSortDrawObjects?.Invoke(this, EventArgs.Empty);
                }
                var tmp = this.drawingComponent1.GetDrawObjects();
                var part1 = tmp.Where(x => ((DrawObjectBase)x).LayerId == 15).ToList();
                var part2 = tmp.Where(x => ((DrawObjectBase)x).LayerId < 15 || ((DrawObjectBase)x).LayerId == 17).ToList();
                var part3 = tmp.Where(x => ((DrawObjectBase)x).LayerId == 16).ToList();
                if (part1.Any() || part3.Any())
                {
                    tmp.Clear();
                    tmp.AddRange(part1);
                    tmp.AddRange(part2);
                    tmp.AddRange(part3);
                    for (int i = 0; i < tmp.Count; i++)
                    {
                        tmp[i].GroupParam.FigureSN = (i + 1);
                    }
                }
                return tmp;
            });
            this.viewModel.InjectPaserFunc(x => 
            {
                var items = FigureHelper.ToFigureBaseModel(x);
                items.ForEach(m => m.IsSelected = false);
                return JsonConvert.SerializeObject(items);
             });        
            this.viewModel.Register("MarkClear", this.ClearMark);
            this.viewModel.Register("MarkPointChanged", this.UpdateMarkPoint);
            this.viewModel.Register("MarkPathAdd", this.AddMarkPathPoint);
            this.viewModel.Register("AutoRefresh", this.OnAutoRefreshChanged);
            this.viewModel.Register("MarkFlagChanged", this.UpdateMarkFlag);
            this.viewModel.Register("RelativePosChanged", this.UpdateRelativePos);
            this.viewModel.Register("FiguresMove", this.MoveAll);
            this.viewModel.Register("SetCanvasView", this.SetCanvasView);
            this.viewModel.Register("CanvasStatusChanged", this.OnStatusChanged);
            this.viewModel.Register("UpdateOutline", this.UpdateOutline);

            this.viewModel.InitCanvas();
            this.drawingComponent1.OnPositionChanged += (sender, e) =>
            {
                PointF point = new PointF
                {
                    X = (float)e.CurrentPoint.X,
                    Y = (float)e.CurrentPoint.Y
                };
                this.viewModel.UpdateCanvasPos(point);
            };
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (double.IsNaN(this.viewModel.FineDistance))
            {
                //Move canvas
            }
            else
            {
                //Move selected figures
                var items = this.drawingComponent1.GetDrawObjects().Where(x => x.IsSelected);
                if (items.Any())
                {
                    var offset = new UnitPoint();
                    var distance = this.viewModel.FineDistance;
                    switch (keyData)
                    {
                        case Keys.Up:
                            offset.Y = distance;
                            break;
                        case Keys.Down:
                            offset.Y = -distance;
                            break;
                        case Keys.Left:
                            offset.X = -distance;
                            break;
                        case Keys.Right:
                            offset.X = distance;
                            break;
                    }
                    foreach (var m in items)
                    {
                        m.Move(offset);
                    }
                    this.drawingComponent1.InvidateCanvas(true);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CanvasWrapper_Load(object sender, EventArgs e)
        {
            //var viewModel = (CanvasViewModel)MVVMContext.GetViewModel(this);
            this.viewModel.UpdateMarkPoint();
        }

        private void OnStatusChanged(object arg)
        {
            this.BeginInvoke(new Action(() =>
            {
                bool enabled = (bool)arg;
                this.drawingComponent1.CanvasEnabled = enabled;
                if (enabled)
                {
                    this.drawingComponent1.ExcuteCommand(CanvasCommands.SelectMode);
                }
                else
                {
                    this.drawingComponent1.ExcuteCommand(CanvasCommands.Pan);
                }
            }));
        }

        private void SetCanvasView(object arg)
        {
            this.drawingComponent1.SetCanvasView((RectangleF)arg);
        }

        private void MoveAll(object arg)
        {
            this.drawingComponent1.MoveAll((PointF)arg);
        }

        private void UpdateMarkFlag(object arg)
        {
            var info = arg as Tuple<PointF, Color>;
            this.drawingComponent1.UpdateMarkFlag(info.Item1, info.Item2);
        }

        private void UpdateRelativePos(object arg)
        {
            this.drawingComponent1.UpdateRelativePos((PointF)arg);
        }

        private void OnAutoRefreshChanged(object arg)
        {
            this.BeginInvoke(new Action(() =>
            {
                bool enabled = (bool)arg;
                this.timerRefresh.Enabled = enabled;
                //if (!enabled)
                //{
                //    this.drawingComponent1.InvidateCanvas(false);
                //}
                this.drawingComponent1.StaticRefreshMark = !enabled;
            }));
        }

        private void AddMarkPathPoint(object arg)
        {
            var info = arg as Tuple<string, PointF>;
            this.drawingComponent1.AddMarkPathPoint(info.Item1, info.Item2);
        }

        private void UpdateMarkPoint(object arg)
        {
            var p = (PointF)arg;
            var rect = this.drawingComponent1.GetRegion();         
            if (!rect.Contains(p.X, p.Y))
            {
                this.BeginInvoke(new Action(() =>
                {
                    var center = new PointF(rect.X + rect.Width / 2.0f, rect.Y + rect.Height / 2.0f);
                    var offset = new PointF(p.X - center.X, p.Y - center.Y);
                    if (Math.Abs(offset.X) > rect.Width || Math.Abs(offset.Y) > rect.Height)
                    {
                        offset = new PointF(offset.X * 1.0f, offset.Y * 1.0f);
                        rect.Offset(offset);
                    }
                    else
                    {
                        rect.Inflate(rect.Width / 6.0f, rect.Height / 6.0f);
                    }
                    this.drawingComponent1.SetCanvasView(rect);
                }));
            }
            this.drawingComponent1.UpdateMarkPoint(p);
        }

        private void ClearMark(object arg)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.drawingComponent1.ClearMark();
                this.drawingComponent1.InvidateCanvas(true);
            }));
        }

        private void UpdateOutline(object arg)
        {
            this.drawingComponent1.UpdateOutline((RectangleF)arg);
        }

        public void OnLayerIdChanged()
        {
            this.drawingComponent1.OnLayerIdChanged();
        }

        public void OnMachineEnabledChanged(bool enabled)
        {
            this.drawingComponent1.OnMachineEnabledChanged(enabled);
        }

        #region 来自窗体对画布图形对象的操作

        #region 平移

        public void MovePosition()
        {
            this.drawingComponent1.MovePosition();
        }

        /// <summary>
        /// 交互式缩放
        /// </summary>
        public void InteractScale()
        {
            this.drawingComponent1.InteractScale();
        }

        public RectangleF GetDrawingObjectSize()
        {
            return this.drawingComponent1.GetDrawingObjectSize();
        }

        public void DoSizeChange(double centerX, double centerY, double width, double height)
        {
            this.drawingComponent1.DoSizeChange(centerX, centerY, width, height);
        }

        public void OnRotate(double rotateAngle, bool isClockwise)
        {
            this.drawingComponent1.OnRotate(rotateAngle, isClockwise);
        }

        public void OnRotateAny()
        {
            this.drawingComponent1.OnRotateAny();
        }

        public void OnSetLeadwire(LineInOutParamsModel leadwireModel)
        {
            this.drawingComponent1.DoSetLeadwire(leadwireModel);
        }

        public void OnClearLeadWire()
        {
            this.drawingComponent1.DoClearLeadWire();
        }

        public void OnCheckLeadInOrOutWire(bool InterOrExterModel)
        {
            this.drawingComponent1.DoCheckLeadInOrOutWire(InterOrExterModel);
        }
        #endregion

        #region 镜像阵列
        public void OnHorizonalMirror(object args)
        {
            this.drawingComponent1.OnHorizonalMirror(args);
        }

        public void OnVerticalMirror(object args)
        {
            this.drawingComponent1.OnVerticalMirror(args);
        }

        public void OnMirrorAny()
        {
            this.drawingComponent1.OnMirrorAny();
        }

        public void OnArray(object args)
        {
            this.drawingComponent1.OnArray(args);
        }
        #endregion

        #region 对齐
        public void OnLeftAligment(object args)
        {
            this.drawingComponent1.OnLeftAligment(args);
        }

        public void OnRightAligment(object args)
        {
            this.drawingComponent1.OnRightAligment(args);
        }
        public void OnTopAligment(object args)
        {
            this.drawingComponent1.OnTopAligment(args);
        }
        public void OnBottomAligment(object args)
        {
            this.drawingComponent1.OnBottomAligment(args);
        }
        public void OnCenterAligment(object args)
        {
            this.drawingComponent1.OnCenterAligment(args);
        }
        public void OnHorizonalCenter(object args)
        {
            this.drawingComponent1.OnHorizonalCenter(args);
        }
        public void OnVerticalCenter(object args)
        {
            this.drawingComponent1.OnVerticalCenter(args);
        }
        #endregion

        #region 工艺
        public void OnSettingStartPoint(object arg)
        {
            this.drawingComponent1.OnSettingStartPoint(arg);
        }

        public void OnReverseDirection(object arg)
        {
            this.drawingComponent1.OnReverseDirction(arg);
        }

        #region 封口、缺口、过切、多圈
        public void OnOverCutting(float pos, bool roundCut)
        {
            this.drawingComponent1.OnOverCutting(pos, roundCut);
        }

        #endregion
        public void OnRoundAngle(float radius)
        {
            this.drawingComponent1.OnRoundAngle(radius);
        }

        public void OnReleaseAngle(float radius)
        {
            this.drawingComponent1.OnReleaseAngle(radius);
        }

        public void OnAutoRoundAngle(float radius)
        {
            this.drawingComponent1.OnAutoRoundAngle(radius);
            //this.drawingComponent1.DoRefresh();
        }

        public void OnMicroConncect(float microConnLen, bool isApplyingToSimilarGraphics)
        {
            this.drawingComponent1.OnMicroConncect(microConnLen, isApplyingToSimilarGraphics);
        }
        public void OnAutoMicroConnect(bool isAllFigure, AutoMicroConParam param)
        {
            this.drawingComponent1.OnAutoMicroConnect(isAllFigure, param);
        }
        public void OnMicroConnectBlowOpen()
        {
            this.drawingComponent1.OnMicroConnectBlowOpen();
        }
        public void OnCompensation(bool allFigure, CompensationModel param)
        {
            this.drawingComponent1.OnCompensation(allFigure, param);
        }
        public void OnOuterCut()
        {
            this.drawingComponent1.OnOuterCut();
        }
        public void OnInnerCut()
        {
            this.drawingComponent1.OnInnerCut();
        }
        public void OnCornerRing(CornerRingModel param)
        {
            this.drawingComponent1.OnCornerRing(param);
        }
        public void OnClearMicroConnect()
        {
            this.drawingComponent1.OnClearMicroConnect();
        }
        public void OnSetCoolPoint()
        {
            this.drawingComponent1.OnSetCoolPoint();
        }
        public void OnSetAutoCoolPoint(bool isLeadIn, bool isCorner, double maxAngle)
        {
            this.drawingComponent1.OnSetAutoCoolPoint(isLeadIn, isCorner, maxAngle);
        }
        public void OnClearCoolPoint()
        {
            this.drawingComponent1.OnClearCoolPoint();
        }

        public void OnShowCarft(int layerId, bool isShow)
        {
            this.drawingComponent1.DoShowLayerCraft(layerId, isShow);
        }
        public void OnOnlyShowCarft(int layerId)
        {
            this.drawingComponent1.DoOnlyShowLayerCraft(layerId);
        }
        public void OnLockCarft(int layerId, bool isLock)
        {
            this.drawingComponent1.DoLockLayerCraft(layerId, isLock);
        }
        #endregion

        #region 显示附加信息

        /// <summary>
        /// 显示图形外框
        /// </summary>
        public void OnShowBoundRect()
        {
            this.drawingComponent1.DoRefresh();
        }

        /// <summary>
        /// 红色显示不封闭图形
        /// </summary>
        public void OnShowNotClosedFigure()
        {
            this.drawingComponent1.DoRefresh();
        }

        /// <summary>
        /// 显示序号
        /// </summary>
        public void OnShowFigureSN()
        {
            this.drawingComponent1.DoRefresh();
        }

        /// <summary>
        /// 显示路径起点
        /// </summary>
        public void OnShowStartMovePoint()
        {
            this.drawingComponent1.DoRefresh();
        }

        /// <summary>
        /// 显示加工路径
        /// </summary>
        public void OnShowMachinePath()
        {
            this.drawingComponent1.DoRefresh();
        }

        /// <summary>
        /// 显示加工路径
        /// </summary>
        public void OnShowMicroConnectFlag()
        {
            this.drawingComponent1.DoRefresh();
        }

        /// <summary>
        /// 显示空移路径
        /// </summary>
        public void OnShowVacantPath()
        {
            this.drawingComponent1.DoRefresh();
        }

        #endregion
        #endregion

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            this.drawingComponent1.InvidateCanvas(false);
        }

        public UCCanvas GetDrawCanvas()
        {
            return this.drawingComponent1.UCCanvas;
        }

        public List<int> GetLayerIdCollection()
        {
            var items = this.drawingComponent1.GetDrawObjects();
            var res = items.Select(x => (int)((DrawObjectBase)x).LayerId).Distinct().ToList();
            res.Sort();
            return res;
        }
        public DrawingComponent GetDrawingComponent()
        {
            return this.drawingComponent1;
        }

        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        public void OnSort(string sortType, UnitPoint unitPointStart, bool sortProhibitChangDirection, bool sortDistinguishInsideOutside, bool SortShadeCutOutermost, bool groupInside)
        {
            this.drawingComponent1.DoSort(sortType, unitPointStart, sortProhibitChangDirection, sortDistinguishInsideOutside, SortShadeCutOutermost, groupInside);
        }
        #endregion

        #region 选择

        public void OnSelectAll()
        {
            this.drawingComponent1.DoSelectAll();
        }
        public void OnCancelSelectAll()
        {
            this.drawingComponent1.DoCancelSelectAll();
        }
        public void OnInvertSelect()
        {
            this.drawingComponent1.DoInvertSelect();
        }

        public void OnBanFastDragCopy()
        {
            this.drawingComponent1.DoBanFastDragCopy();
        }
        public void OnSelectGapFigure()
        {
            this.drawingComponent1.DoSelectGapFigurey();
        }

        public void OnSelectAllInternalModes()
        {
            this.drawingComponent1.DoSelectAllInternalModes();
        }
        public void OnSelectAllExternalModes()
        {
            this.drawingComponent1.DoSelectAllExternalModes();
        }

        public void OnSelectLayerCraft(int layerId)
        {
            this.drawingComponent1.DoSelectLayerCraft(layerId);
        }
        public void OnSelectAllMultiLines()
        {

            this.drawingComponent1.DoSelectAllMultiLines();
        }
        public void OnSelectAllCircles()
        {

            this.drawingComponent1.DoSelectAllCircles();

        }
        public void OnSelectAllBezierCurves()
        {
            this.drawingComponent1.DoSelectAllBezierCurves();
        }

        public void OnCopy()
        {
            this.drawingComponent1.DoCopy();
        }
        public void OnCut()
        {
            this.drawingComponent1.DoCut();
        }
        public void OnPaste()
        {
            this.drawingComponent1.DoPaste();
        }


        public void OnDelete()
        {
            this.drawingComponent1.DoDelete();
        }



        #endregion

        #region 群组
        public void OnGroup()
        {
            this.drawingComponent1.DoGroup();
        }
        public void OnGroupSelectAll()
        {
            this.drawingComponent1.DoGroupSelectAll();
        }

        public void OnGroupScatterAll()
        {
            this.drawingComponent1.DoGroupScatterAll();
        }

        public void OnGroupScatterSelected()
        {
            this.drawingComponent1.DoGroupScatterSelected();
        }
        #endregion

        #region 桥接 
        public void OnBridge(Func<BridgingModel> func)
        {
            this.drawingComponent1.OnBridge(func);
        }
        #endregion

        #region 测量
        public void OnMeasure()
        {
            this.drawingComponent1.DoMeasure();
        }
        #endregion


        #region 飞切
        public void OnLineFlyCut(LineFlyingCutModel lineFlyingCutModel)
        {
            this.drawingComponent1.DoLineFlyingCut(lineFlyingCutModel);
        }

        public void OnArcFlyCut(ArcFlyingCutModel arcFlyingCutModel)
        {
            this.drawingComponent1.DoArcFlyingCut(arcFlyingCutModel);
        }
        #endregion

        #region 共边
        public void OnCommonSide(CommonSideRectangleModel commonSideRectangleModel)
        {
            this.drawingComponent1.DoCommonSide(commonSideRectangleModel);
        }
        public bool IsCommonSide()
        {
           return   this.drawingComponent1.IsCommonSide();
        }
        #endregion

        
    }
}
