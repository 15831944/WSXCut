using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawTool;
using WSX.Engine;
using WSX.Engine.Models;
using WSX.Engine.Operation;
using WSX.Engine.Utilities;
using WSX.GlobalData.Model;
using WSX.Hardware.Models;
using WSX.ViewModels.Common;
using WSX.ViewModels.Model;

namespace WSX.ViewModels.CustomControl.Draw
{
    public class CanvasViewModel: ViewModelExtensions
    {
        private Func<bool, List<IDrawObject>> canvasHandler;
        private Func<List<IDrawObject>, string> parserFunc;
        private System.Timers.Timer canvasMonitor;
        private string canvasData;
      
        public double FineDistance { get; private set; } = double.NaN;
       
        public CanvasViewModel()
        {
            OperationEngine.Instance.OnMarkPathAdd += Engine_OnMarkPathAdd;
            OperationEngine.Instance.OnMarkPointChanged += Engine_OnMarkPointChanged;
            OperationEngine.Instance.OnStatusChanged += Engine_OnStatusChanged;

            Messenger.Default.Register<object>(this, "MarkFlagChanged", this.OnMarkFlagChanged);
            Messenger.Default.Register<object>(this, "RelativePosChanged", this.OnRelativePosChanged);
            Messenger.Default.Register<object>(this, "OnMoveFigures", this.OnFiguresMove);
            Messenger.Default.Register<object>(this, "GetDataProvider", this.OnDataProviderGet);
            Messenger.Default.Register<object>(this, "CanvasMonitorChanged", this.OnCanvasMonitorChanged);
            Messenger.Default.Register<object>(this, "OnPreview", this.OnPreview);
            Messenger.Default.Register<object>(this, "ClearMark", this.OnMarkClear);
            Messenger.Default.Register<object>(this, "CanvasStatusChanged", this.OnCanvasStatusChanged);
            Messenger.Default.Register<double>(this, "FineDistanceChanged", this.OnFineDistanceChanged);
            Messenger.Default.Register<object>(this, "GetDrawObjectsCnt", this.OnDrawObjectsCntGet);
            Messenger.Default.Register<object>(this, "OnOperation", this.OnOperation);

            this.canvasMonitor = new System.Timers.Timer(100);
            this.canvasMonitor.Elapsed += CanvasMonitor_Elapsed;
            //this.canvasMonitor.Start();        
        }

        #region Public methods
        public void InitCanvas()
        {
            var rect = SystemContext.SystemPara.GetMachiningRegion();       
            float width = rect.Width;
            float height = rect.Height;
            float xRatio = 3.0f;
            float yRatio = 1.5f;

            this.UpdateOutline(new RectangleF(0, 0, width, height));
            var size = new SizeF(width * xRatio, height * yRatio);
            double x = (width - width * xRatio) / 2.0;
            double y = (height - height * yRatio) / 2.0;
            var location = new PointF((float)x, (float)y);
            this.ExecuteCmd("SetCanvasView", new RectangleF(location, size));
        }

        public void UpdateCanvasPos(PointF p)
        {
            Messenger.Default.Send<object>(p, "UpdateCanvasPos");
        }

        public void UpdateMarkPoint()
        {
            //Set current mark position
            if (SystemContext.Hardware != null)
            {
                var posInfo = SystemContext.Hardware.Motor.CurrentPosInfo;
                float x1 = (float)posInfo[AxisTypes.AxisX];
                float y1 = (float)posInfo[AxisTypes.AxisY];
                this.Engine_OnMarkPointChanged(new PointF(x1, y1));
            }
        }

        public void InjectHandler(Func<bool, List<IDrawObject>> canvasHandler)
        {
            this.canvasHandler = canvasHandler;
        }

        public void InjectPaserFunc(Func<List<IDrawObject>, string> parserFunc)
        {
            this.parserFunc = parserFunc;
        }
        #endregion

        #region Engine callback
        private void Engine_OnStatusChanged(EngineStatus status)
        { 
            this.ExecuteCmd("AutoRefresh", status == EngineStatus.Running);
        }

        private void Engine_OnMarkPointChanged(PointF p)
        {
            //Target1: Canvas
            //Target2: MainView position bar
            this.ExecuteCmd("MarkPointChanged", p);
        }

        private void Engine_OnMarkPathAdd(string id, PointF p)
        {
            this.ExecuteCmd("MarkPathAdd", Tuple.Create(id, p));
        }
        #endregion

        #region Message handler
        private void OnMarkFlagChanged(object arg)
        {
            this.ExecuteCmd("MarkFlagChanged", arg);
        }

        private void OnRelativePosChanged(object arg)
        {
            this.ExecuteCmd("RelativePosChanged", arg);
        }

        private void OnFiguresMove(object arg)
        {
            var data = this.GetCurrent();
            if (!data.IsEmpty)
            {
                var p1 = data.OutLine.Points[0];
                int index = SystemContext.CoordinatePara.RefZeroIndex;
                var p2 = SystemContext.CoordinatePara.RefZeroSeries[index];
                var offset = new PointF(p2.X - p1.X, p2.Y - p1.Y);
                this.ExecuteCmd("FiguresMove", offset);
            }                 
        }

        private void OnDataProviderGet(object arg)
        {
            var tmp = arg as Tuple<bool, bool>;
            Messenger.Default.Send<object>(this.GetCurrent(tmp.Item1, tmp.Item2), "ReceiveDataProvider");
        }

        private void OnDrawObjectsCntGet(object arg)
        {
            var tmp = arg as Tuple<bool, bool>;
            var drawObjects = this.GetDrawObjects(tmp.Item1, tmp.Item2);
            int cnt = 0;
            if (drawObjects != null)
            {
                cnt = drawObjects.Count;
            }
            Messenger.Default.Send<object>(cnt, "ReceiveDrawObjectsCnt");
        }

        private void OnCanvasMonitorChanged(object arg)
        {
            bool enabled = (bool)arg;
            if (enabled)
            {
                bool selected = SystemContext.MachineControlPara.IsOnlyMachineSelected;
                this.canvasData = this.parserFunc.Invoke(this.GetDrawObjects(false, selected));
                this.canvasMonitor.Start();
            }
            else
            {
                this.canvasMonitor.Stop();               
            }
        }

        private void OnPreview(object arg)
        {
            var data = this.GetCurrent();
            if (data == null || data.IsEmpty)
            {
                return;
            }

            var points = data.OutLine.Points;
            var rect = new RectangleF(points[0], new SizeF(points[2].X - points[1].X, points[1].Y - points[0].Y));
            float x = (float)((1.2 * rect.Width - rect.Width) / 2.0);
            float y = (float)((1.2 * rect.Height - rect.Height) / 2.0);
            rect.Inflate(x, y);
            this.ExecuteCmd("SetCanvasView", rect);
        }

        private void OnOperation(object arg)
        {
            var para = arg as CanvasOperParameter;
            var data = this.GetCurrent();
            if (!data.IsEmpty)
            {
                if (para.OperType.IsMove())
                {
                    var p1 = data.OutLine.Points[0];
                    int index = SystemContext.CoordinatePara.RefZeroIndex;
                    var p2 = SystemContext.CoordinatePara.RefZeroSeries[index];
                    var offset = new PointF(p2.X - p1.X, p2.Y - p1.Y);
                    this.ExecuteCmd("FiguresMove", offset);
                    data.Move(offset);
                }

                if (para.OperType.IsPreview())
                {
                    var points = data.OutLine.Points;
                    var rect = new RectangleF(points[0], new SizeF(points[2].X - points[1].X, points[1].Y - points[0].Y));
                    float x = (float)((1.2 * rect.Width - rect.Width) / 2.0);
                    float y = (float)((1.2 * rect.Height - rect.Height) / 2.0);
                    rect.Inflate(x, y);
                    this.ExecuteCmd("SetCanvasView", rect);
                }

                if (para.Selected == true)
                {
                    data = this.GetCurrent(false, true);
                }
                Messenger.Default.Send<object>(data, "ReceiveDataProvider");
            }
        }

        private void OnMarkClear(object arg)
        {
            this.ExecuteCmd("MarkClear", null);
        }

        private void OnCanvasStatusChanged(object arg)
        {
            this.ExecuteCmd("CanvasStatusChanged", arg);
        }

        private void OnFineDistanceChanged(double distance)
        {
            this.FineDistance = distance;
        }
        #endregion

        #region Utilities
        private void CanvasMonitor_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.canvasMonitor.Stop();
            //var before = DateTime.Now;
            bool selected = SystemContext.MachineControlPara.IsOnlyMachineSelected;
            string str = this.parserFunc.Invoke(this.GetDrawObjects(false, selected));
            if (str != this.canvasData)
            {
                Messenger.Default.Send<object>(null, "DataProviderChanged");
            }
            else
            {
                this.canvasMonitor.Start();
            }
            //this.canvasData = str;
            //var period = DateTime.Now - before;
            //Console.WriteLine("Time = {0}ms",period.TotalMilliseconds);
        }

        private void UpdateOutline(RectangleF rect)
        {
            this.ExecuteCmd("UpdateOutline", rect);
        }
   
        private DataProvider GetCurrent(bool needToSort = false, bool selected = false)
        {
            //if (this.canvasHandler == null)
            //{
            //    return null;
            //}

            //var tmp = this.canvasHandler.Invoke(needToSort);
            //if (selected)
            //{
            //    tmp = tmp.Where(x => x.IsSelected).ToList();
            //}

            //var machineLayers = GlobalModel.Params.LayerConfig.LayerCrafts.Where(x => !x.Value.IsUnprocessed).Select(x => x.Key);
            //if (machineLayers.Any())
            //{
            //    tmp = tmp.Where(x => machineLayers.Contains(((DrawObjectBase)x).LayerId)).ToList();
            //}

            var tmp = this.GetDrawObjects(needToSort, selected);
            if (tmp == null)
            {
                return null;
            }

            var data = new DataProvider();
            var builder = new DataBuilder(data, tmp);
            DataDirector.Bulid(builder);

            return data;
        }

        private List<IDrawObject> GetDrawObjects(bool needToSort, bool selected)
        {
            if (this.canvasHandler == null)
            {
                return null;
            }

            var tmp = this.canvasHandler.Invoke(needToSort);
            if (selected)
            {
                tmp = tmp.Where(x => x.IsSelected).ToList();
            }

            var machineLayers = GlobalModel.Params.LayerConfig.LayerCrafts.Where(x => !x.Value.IsUnprocessed).Select(x => x.Key);
            if (machineLayers.Any())
            {
                tmp = tmp.Where(x => machineLayers.Contains(((DrawObjectBase)x).LayerId)).ToList();
            }

            return tmp;
        }
        #endregion
    }
}
 