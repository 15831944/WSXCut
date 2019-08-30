using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;
using WSX.DrawService.DrawTool;
using WSX.DrawService.Utils;

namespace WSX.DrawService.CanvasControl
{
    //设置桥接
    public partial class UCCanvas
    {
        private UnitPoint bridgeStart = UnitPoint.Empty;//桥接起点
        private UnitPoint bridgeEnd = UnitPoint.Empty;//桥接终点
        private Func<BridgingModel> bridgeFunc;//弹出窗体回调
        public void OnBridge(Func<BridgingModel> func)
        {
            this.bridgeFunc = func;
            this.bridgeStart = UnitPoint.Empty;
            this.bridgeEnd = UnitPoint.Empty;
            this.CommandEscape();
            this.commandType = CommandType.Bridge;
            this.DoInvalidate(true);
        }

        private void MouseDownSetBridge(MouseEventArgs e)
        {
            UnitPoint mousePoint = this.ToUnit(this.mouseDownPoint);
            if (this.bridgeStart.IsEmpty)
            {
                this.bridgeStart = mousePoint;
            }
            else
            {
                this.bridgeEnd = mousePoint;
                BridgingModel param = this.bridgeFunc?.Invoke();
                if (param != null)
                {
                    //1.获取图形区域与桥接直线交叉的图形
                    var oldObjects = this.Model.DrawingLayer.Objects.FindAll(f => HitUtil.LineIntersectWithRect(this.bridgeStart, this.bridgeEnd, f.GetBoundingRectangle(BoundingRectangleType.All)));
                    bool isChanged = false;
                    //2.计算直线与图形的交点
                    var newObjects = BridgeHelper.GetBridgeObjects(oldObjects, this.bridgeStart, this.bridgeEnd, param, out isChanged);
                    if (isChanged)
                    {
                        this.Model.DoBridge(newObjects, oldObjects,true);
                    }
                }
                this.CommandEscape();
            }
        }
        private void MouseMoveSetBridge(MouseEventArgs e)
        {
            if (!this.bridgeStart.IsEmpty)
            {
                UnitPoint mousePoint = this.GetMousePoint();
                Rectangle invalidaterect = ScreenUtils.ConvertRectangle(ScreenUtils.ToScreenNormalized(this.canvasWrapper, ScreenUtils.GetRectangleF(this.bridgeStart, this.bridgeEnd, UCCanvas.GetThresholdWidth() / 2)));
                invalidaterect.Inflate(2, 2);
                RepaintStatic(invalidaterect);
                this.bridgeEnd = mousePoint;
                CanvasWrapper canvasWrapper = new CanvasWrapper(this, Graphics.FromHwnd(this.Handle), this.ClientRectangle);
                Pen pen = new Pen(Color.Bisque, 1)
                {
                    DashStyle = DashStyle.Custom,
                    DashPattern = new float[2] { 2, 3 }
                };
                canvasWrapper.DrawLine(canvasWrapper, pen, this.bridgeStart, this.bridgeEnd);
                canvasWrapper.Graphics.Dispose();
                canvasWrapper.Dispose();
            }
        }
    }
}
