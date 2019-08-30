using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.Layers
{
    public class MarkLayer
    {
        private Dictionary<string, List<PointF>> drawObjectMap = new Dictionary<string, List<PointF>>();
        private Dictionary<string, List<PointF>> drawObjectMapScreen = new Dictionary<string, List<PointF>>();
        private PointF markPoint;
        private PointF markPointScreen;
        private PointF markFlag;
        private PointF markFlagScreen;
        private PointF relativePos;
        private PointF relativePosScreen;
        private Color markFlagColor;

        private SemaphoreSlim mutex = new SemaphoreSlim(1, 1);
        private readonly UCCanvas myCanvas;

        private const int MAX_PATH_POINTS_COUNT = 5000;

        public MarkLayer(UCCanvas canvas)
        {
            this.myCanvas = canvas;
        }

        public void AddMarkPathPoint(string figureId, PointF point)
        {
            if (this.mutex.Wait(0))
            {
                if (!this.drawObjectMap.ContainsKey(figureId))
                {
                    this.drawObjectMap.Add(figureId, new List<PointF>());
                    this.drawObjectMapScreen.Add(figureId, new List<PointF>());
                }
                if (this.drawObjectMap[figureId].Count > 2)
                {
                    //Remove points in the same line
                    Func<PointF, PointF, double> getSlope = (a, b) => (a.Y - b.Y) / (a.X - b.X);
                    int lastIndex = this.drawObjectMap[figureId].Count - 1;
                    var p1 = this.drawObjectMap[figureId][lastIndex - 2];
                    var p2 = this.drawObjectMap[figureId][lastIndex - 1];
                    double slope1 = getSlope(p1, p2);
                    double slope2 = getSlope(p2, point);
                    if (Math.Abs(slope1 - slope2) < 0.00001 || (double.IsInfinity(slope1) && double.IsInfinity(slope2)))
                    {
                        this.drawObjectMap[figureId].RemoveAt(lastIndex);
                        this.drawObjectMapScreen[figureId].RemoveAt(lastIndex);
                    }
                }
                this.drawObjectMap[figureId].Add(point);
                this.drawObjectMapScreen[figureId].Add(this.myCanvas.ToScreen(new UnitPoint(point)));
                this.mutex.Release();
            }                  
        }

        public void UpdateMarkPoint(PointF point)
        {
            if (this.mutex.Wait(0))
            {
                this.markPoint = point;
                this.markPointScreen = this.myCanvas.ToScreen(new UnitPoint(point));
                this.mutex.Release();
            }
        }

        public void UpdateMarkFlag(PointF point, Color color)
        {
            this.markFlag = point;
            this.markFlagScreen = this.myCanvas.ToScreen(new UnitPoint(point));
            this.markFlagColor = color;
        }

        public void UpdateRelativePos(PointF point)
        {
            this.relativePos = point;
            this.relativePosScreen = this.myCanvas.ToScreen(new UnitPoint(point));
        }

        public void UpdateSceenPoints()
        {
            this.mutex.Wait();
            int total = this.drawObjectMap.Sum(x => x.Value.Count);
            if (total > MAX_PATH_POINTS_COUNT)
            {
                this.drawObjectMap.Clear();
                this.drawObjectMapScreen.Clear();
            }
            else
            {
                this.drawObjectMapScreen.Clear();
                foreach (var pair in this.drawObjectMap)
                {
                    var points = new List<PointF>();
                    foreach (var point in pair.Value)
                    {
                        points.Add(this.myCanvas.ToScreen(new UnitPoint(point)));
                    }
                    this.drawObjectMapScreen[pair.Key] = points;
                }
            }
            this.markPointScreen = this.myCanvas.ToScreen(new UnitPoint(this.markPoint));
            this.markFlagScreen = this.myCanvas.ToScreen(new UnitPoint(this.markFlag));
            this.relativePosScreen = this.myCanvas.ToScreen(new UnitPoint(this.relativePos));
            this.mutex.Release();
        }

        public void Draw(ICanvas canvas)
        {
            var markPathPoints = new List<List<PointF>>();
            var markPointTemp = new PointF();

            //var before = DateTime.Now;
            this.mutex.Wait();
            markPointTemp.X = this.markPointScreen.X;
            markPointTemp.Y = this.markPointScreen.Y;
            foreach (var m in this.drawObjectMapScreen.Values)
            {
                markPathPoints.Add(new List<PointF>(m));
            }
            this.mutex.Release();
            //Console.WriteLine((DateTime.Now - before).TotalMilliseconds);

            #region Draw machining path
            foreach (var m in markPathPoints)
            {             
                if (m.Count > 1)
                {                  
                    canvas.Graphics.DrawLines(new Pen(Brushes.Yellow, 1.5f), m.ToArray());
                }
                //else if (m.Count == 1)
                //{
                //    canvas.Graphics.FillEllipse(Brushes.Yellow, new RectangleF(new PointF(m[0].X - 2, m[0].Y - 2), new Size(4, 4)));
                //}
            }
            #endregion

            #region Draw real time position
            PointF centerPoint = markPointTemp;
            PointF pointV1 = new PointF(centerPoint.X + 20, centerPoint.Y + 20);
            PointF pointV2 = new PointF(centerPoint.X - 20, centerPoint.Y - 20);
            PointF pointH1 = new PointF(centerPoint.X - 20, centerPoint.Y + 20);
            PointF pointH2 = new PointF(centerPoint.X + 20, centerPoint.Y - 20);
            canvas.Graphics.DrawLine(Pens.Yellow, pointH1, pointH2);
            canvas.Graphics.DrawLine(Pens.Yellow, pointV1, pointV2);
            #endregion

            #region Draw mark position
            var p1 = this.markFlagScreen;
            //var p2 = new PointF(p1.X + 5, p1.Y - 10);
            //var p3 = new PointF(p1.X + 10, p1.Y - 10);
            //var p4 = new PointF(p1.X + 8, p1.Y - 6);
            //var p5 = new PointF(p1.X + 3, p1.Y - 6);
            var p2 = new PointF(p1.X + 8, p1.Y - 16);
            var p3 = new PointF(p1.X + 16, p1.Y - 16);
            var p4 = new PointF(p1.X + 12, p1.Y - 8);
            var p5 = new PointF(p1.X + 4, p1.Y - 8);
            var points = new PointF[] { p1, p2, p3, p4, p5 };
            canvas.Graphics.DrawLines(new Pen(this.markFlagColor, 1), points);
            #endregion

            #region Draw relative zero position
            var center = this.relativePosScreen.ToPoint();
            //canvas.Graphics.(Brushes.Red, new RectangleF(new PointF(center.X - 1, center.Y - 1), new SizeF(2, 2)));
            canvas.Graphics.DrawLine(new Pen(Color.Red, 2.0f), new Point(center.X - 1, center.Y), new Point(center.X + 1, center.Y));
            canvas.Graphics.DrawLine(new Pen(Color.LightGray, 2.0f), new Point(center.X - 3, center.Y), new Point(center.X - 10, center.Y));
            canvas.Graphics.DrawLine(new Pen(Color.LightGray, 2.0f), new Point(center.X + 3, center.Y), new Point(center.X + 10, center.Y));
            canvas.Graphics.DrawLine(new Pen(Color.LightGray, 2.0f), new Point(center.X, center.Y - 3), new Point(center.X, center.Y - 10));
            canvas.Graphics.DrawLine(new Pen(Color.LightGray, 2.0f), new Point(center.X, center.Y + 3), new Point(center.X, center.Y + 10));
            #endregion
        }

        public void ClearMark()
        {         
            this.mutex.Wait();
            this.drawObjectMap.Clear();
            this.drawObjectMapScreen.Clear();
            this.mutex.Release();
        }
    }
}
