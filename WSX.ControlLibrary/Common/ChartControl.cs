using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using WSX.CommomModel.ParaModel;

namespace WSX.ControlLibrary.Common
{
    public partial class ChartControl : UserControl
    {
        private const float X_MIN = 0;
        private const float X_MAX = 100;
        private const float Y_MIN = 0;
        private const float Y_MAX = 100;
        private const float X_INTERVAL = 10;
        private const float Y_INTERVAL = 10;
        private const float X_MARGIN = 30;
        private const float Y_MARGIN = 20;

        private float xRatio = 0;
        private float yRatio = 0;

        //private List<PointF> points;
        private int pointIndex = -1;
        private int movePointIndex = -1;
        private const int CAPTURE_THRESHOLD = 2;
        private bool moveFlag = false;
        private PointF? tipLocation;
        private double xMax = 100;
        private double yMax = 100;
        private string xLengend = "Speed(%)";
        private string yLengend = "Power(%)";
        private List<Curve> series { get; set; } = new List<Curve>();

        private object syncRoot = new object();

        public event Action<object, OnCurveChangedEventArgs> OnCurveDataChanged;

        public double XMax
        {
            get
            {
                return this.xMax;
            }
            set
            {
                this.xMax = value;
                this.Invalidate();
            }
        }

        public double YMax
        {
            get
            {
                return this.yMax;
            }
            set
            {
                this.yMax = value;
                this.Invalidate();
            }
        } 

        public string XLengend
        {
            get
            {
                return this.xLengend;
            }
            set
            {
                this.xLengend = value;
                this.Invalidate();
            }
        }

        public string YLengend
        {
            get
            {
                return this.yLengend;
            }
            set
            {
                this.yLengend = value;
                this.Invalidate();
            }
        } 
        
        public bool EditEnabled { get; set; }

        public int MinPointsCount { get; set; }

        //public List<PointF> Points { get; set; }

        //public List<PointF> Points
        //{
        //    get
        //    {
        //        lock (this.syncRoot)
        //        {
        //            return this.points;
        //        }
        //    }
        //    set
        //    {
        //        lock (this.syncRoot)
        //        {
        //            this.points = value;
        //            this.Invalidate();
        //        }
        //    }
        //}

        public ChartControl()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.MinPointsCount = 2;
        }

        public void AddCurve(Curve curve)
        {
            this.series.Add(curve);
        }

        public List<PointF> GetPoints(string id)
        {
            List<PointF> points = null;
            try
            {
                var item = this.series.Where(x => x.Id == id).ToList();
                if (item.Any())
                {
                    points = item[0].Points;
                }
            }
            catch
            {

            }
            return points;
        }

        public void Clear()
        {
            this.series.Clear();
            this.Invalidate();
        }

        public void Remove(string id)
        {
            try
            {
                var item = this.series.Where(x => x.Id == id).ToList();
                if (item.Any())
                {
                    this.series.Remove(item[0]);
                    this.Invalidate();
                }
            }
            catch
            {

            }
        }

        #region Event handler
        private void ChartControl_Paint(object sender, PaintEventArgs e)
        {
            if (this.Width < 50 || this.Height < 50)
            {
                return;
            }

            this.UpdateRatio();
            this.DrawAxis(e.Graphics);
            this.DrawGrid(e.Graphics);
            foreach (var m in this.series)
            {
                this.DrawCurve(e.Graphics, m);
            }
            this.DrawTip(e.Graphics);
        }

        private void ChartControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.series.Count != 1 || !this.EditEnabled)
            {
                return;
            }

            if (this.pointIndex != -1)
            {
                this.moveFlag = true;
                this.movePointIndex = this.pointIndex;
            }

            if (e.Clicks == 2)
            {
                var p = this.ToUnitPoint(new PointF(e.X, e.Y));
                var points = this.series[0].Points;
                if (this.IsInCurve(p, points))
                {
                    int index = this.GetIndex(p, points);
                    if (pointIndex != -1)
                    {
                        if (points.Count > this.MinPointsCount)
                        {
                            points.RemoveAt(pointIndex);
                            this.pointIndex = -1;
                        }
                    }
                    else
                    {
                        int index2 = points.Count - 1;
                        for (int i = 0; i < points.Count; i++)
                        {
                            var p2 = points[i];
                            if (p2.X > p.X)
                            {
                                index2 = i;
                                break;
                            }
                        }

                        points.Insert(index2, p);
                    }

                    this.RaiseOnCurveChangedEvent();
                    this.Invalidate();
                }
            }
        }

        private void ChartControl_MouseUp(object sender, MouseEventArgs e)
        {
            this.moveFlag = false;
        }

        private void ChartControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.series.Count != 1 || !this.EditEnabled)
            {
                return;
            }          

            bool needToInvidate = false;
            var points = this.series[0].Points;
            var unitPoint = this.ToUnitPoint(new PointF(e.X, e.Y));
            this.UpdateSelectedIndex(unitPoint, points);

            #region Move node
            if (this.moveFlag)
            {
                var temp = new List<PointF>(points.ToArray());
                var startPoint = new PointF(0, 0);
                var endPoint = new PointF(100, 100);
                temp.Insert(0, startPoint);
                temp.Add(endPoint);

                var tempIndex = this.movePointIndex + 1;
                var lastPoint = temp[tempIndex - 1];
                var nextPoint = temp[tempIndex + 1];

                bool condition1 = unitPoint.X >= lastPoint.X && unitPoint.X <= nextPoint.X;
                bool condition2 = unitPoint.Y >= lastPoint.Y && unitPoint.Y <= nextPoint.Y;
                //bool condition1 = unitPoint.X >= lastPoint.X + 1 && unitPoint.X <= nextPoint.X - 1;
                //bool condition2 = unitPoint.Y >= lastPoint.Y + 1 && unitPoint.Y <= nextPoint.Y - 1;
                if (condition1 && condition2)
                {
                    points[this.movePointIndex] = unitPoint;
                    this.RaiseOnCurveChangedEvent();
                    needToInvidate = true;
                }
            }
            #endregion

            if (this.IsInCurve(unitPoint, points))
            {
                this.tipLocation = unitPoint;
                needToInvidate = true;
            }
            else
            {
                if (this.tipLocation != null)
                {
                    needToInvidate = true;
                    this.tipLocation = null;
                }
            }

            if (needToInvidate)
            {
                this.Invalidate();
            }
        }

        private void ChartControl_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        #endregion

        #region Draw Curve
        private void UpdateRatio()
        {
            this.xRatio = (this.Width - 2 * X_MARGIN) / (X_MAX - X_MIN);
            this.yRatio = (this.Height - 2 * Y_MARGIN) / (Y_MAX - Y_MIN);
        }

        private void DrawGrid(Graphics e)
        {
            //e.Clear(this.BackColor);

            var pen = new Pen(Color.FromArgb(170, 170, 170));
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            //Draw vertical lines
            for (float i = X_INTERVAL; i <= X_MAX; i += X_INTERVAL)
            {
                var p1 = this.ToScreen(new PointF(i, Y_MIN));
                var p2 = this.ToScreen(new PointF(i, Y_MAX));
                e.DrawLine(pen, p1, p2);
            }

            //Draw horizontal lines
            for (float i = Y_INTERVAL; i <= Y_MAX; i += Y_INTERVAL)
            {
                var p1 = this.ToScreen(new PointF(X_MIN, i));
                var p2 = this.ToScreen(new PointF(X_MAX, i));
                e.DrawLine(pen, p1, p2);
            }
        }

        private void DrawAxis(Graphics e)
        {
            var pen = new Pen(Color.DimGray, 1.5f);
            pen.CustomEndCap = new AdjustableArrowCap(3, 3);

            var p1 = this.ToScreen(new PointF(X_MIN, Y_MIN));
            var p2 = this.ToScreen(new PointF(X_MIN, Y_MAX));
            p2.Y -= 10;
            var p3 = this.ToScreen(new PointF(X_MIN, Y_MAX));
            p3.Y -= 12;
            e.DrawLine(pen, p1, p2);
            e.DrawString(this.YLengend, new Font("Arial", 8.0f), new SolidBrush(Color.Black), p3);

            p1 = this.ToScreen(new PointF(X_MIN, Y_MIN));
            p2 = this.ToScreen(new PointF(X_MAX, Y_MIN));
            p2.X += 10;
            p3 = new PointF(p2.X - 55.0f, p2.Y - 15.0f);
            e.DrawLine(pen, p1, p2);
            if (!this.XLengend.Equals("Speed(%)") && !this.XLengend.Equals("Speed(G)"))
            {
                p3.X -= 20;
            }
            e.DrawString(this.XLengend, new Font("Arial", 8.0f), new SolidBrush(Color.Black), p3);

            double xRatio = this.XMax / (X_MAX - X_MIN);
            for (float i = 0; i <= X_MAX; i += X_INTERVAL)
            {
                var p = this.ToScreen(new PointF(i - 2.0f, Y_MIN));
                e.DrawString((i * xRatio).ToString("0.###"), new Font("Arial", 8.0f), new SolidBrush(Color.Black), p);
            }

            double yRatio = this.YMax / (Y_MAX - Y_MIN);
            for (float i = Y_INTERVAL; i <= Y_MAX; i += Y_INTERVAL)
            {
                var p = this.ToScreen(new PointF(X_MIN, i));
                p.X = 2;
                p.Y -= 6;
                //if (i >= 100)
                //{
                //    p.X = 0;
                //}
                e.DrawString((i * yRatio).ToString("0.###"), new Font("Arial", 8.0f), new SolidBrush(Color.Black), p);
            }
        }

        private void DrawCurve(Graphics e, Curve curve)
        {
            Pen pen = new Pen(curve.DisColor);
            List<PointF> newPoints = new List<PointF>();
            //e.SmoothingMode = SmoothingMode.AntiAlias;
            var points = curve.Points;

            int index = 0;
            foreach (var point in points)
            {
                var p = this.ToScreen(point);
                newPoints.Add(p);
                if (index != this.pointIndex)
                {
                    e.DrawEllipse(Pens.Black, new RectangleF(new PointF(p.X - 3, p.Y - 3), new SizeF(6, 6)));
                    //e.FillEllipse(Brushes.Blue, new RectangleF(new PointF(p.X - 3, p.Y - 3), new SizeF(6, 6)));
                }
                else
                {
                    e.DrawEllipse(Pens.Black, new RectangleF(new PointF(p.X - 6, p.Y - 6), new SizeF(12, 12)));
                    //e.FillEllipse(Brushes.Blue, new RectangleF(new PointF(p.X - 6, p.Y - 5), new SizeF(12, 12)));
                }
                index++;
            }

            //e.SmoothingMode = SmoothingMode.None;
            if (newPoints.Count > 1)
            {
                var temp = new List<PointF>(newPoints.ToArray());
                var startPoint = this.ToScreen(new PointF(0, points[0].Y));
                var endPoint = this.ToScreen(new PointF(100, points[points.Count - 1].Y));
                temp.Insert(0, startPoint);
                temp.Add(endPoint);
                e.DrawLines(pen, temp.ToArray());
            }
        }

        private void DrawTip(Graphics e)
        {
            if (this.series.Count != 1 || !this.EditEnabled)
            {
                return;
            }

            if (this.tipLocation != null)
            {
                var location = this.tipLocation.Value;
                if (location.Y < 20)
                {
                    location.Y = 20;
                }
                if (location.X > X_MAX - 20)
                {
                    location.X = (X_MAX - 20);
                }

                var p = this.ToScreen(location);
                p.X += 10;
                p.Y += 10;

                double x = this.tipLocation.Value.X;
                double y = this.tipLocation.Value.Y;
                double xRatio = this.XMax / (X_MAX - X_MIN);
                double yRatio = this.YMax / (Y_MAX - Y_MIN);
                x *= xRatio;
                y *= yRatio;

                e.DrawString(string.Format("{0}, {1}\n双击插入或删除", x.ToString("0.00") + this.GetXUnit(), y.ToString("0.00") + this.GetYUnit()), new Font("Arial", 8.0f), Brushes.Blue, p);
            }
        }

        private string GetXUnit()
        {
            int index1 = this.XLengend.IndexOf('(');
            int index2 = this.XLengend.IndexOf(')');
            return this.XLengend.Substring(index1 + 1, index2 - index1 - 1);
        }

        private string GetYUnit()
        {
            int index1 = this.YLengend.IndexOf('(');
            int index2 = this.YLengend.IndexOf(')');
            return this.YLengend.Substring(index1 + 1, index2 - index1 - 1);
        }

        private void RaiseOnCurveChangedEvent()
        {
            this.OnCurveDataChanged?.Invoke(this, new OnCurveChangedEventArgs { CurveData = new List<PointF>(this.series[0].Points.ToArray()) });
        }
        #endregion

        #region Utility
        private void UpdateSelectedIndex(PointF unitPoint, List<PointF> points)
        {
            if (!points.Any())
            {
                return;
            }

            int index = -1;
            for (int i = 0; i < points.Count; i++)
            {
                //var p1 = this.ToScreen(this.Points[i]);
                //var p2 = point;
                var p1 = points[i];
                var p2 = unitPoint;
                float distance = this.GetDistance(p1, p2);
                if (distance < CAPTURE_THRESHOLD)
                {
                    index = i;
                    break;
                }
            }

            if (this.pointIndex != index)
            {
                this.pointIndex = index;
                this.Invalidate();
            }
        }

        private float GetDistance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private PointF ToScreen(PointF unitPoint)
        {
            float x = X_MARGIN + unitPoint.X * this.xRatio;
            float y = this.Height - (Y_MARGIN + unitPoint.Y * this.yRatio);
            return new PointF(x, y);
        }

        private PointF ToUnitPoint(PointF point)
        {
            float x = (point.X - X_MARGIN) / this.xRatio;
            float y = (this.Height - point.Y - Y_MARGIN) / this.yRatio;
            return new PointF(x, y);
        }

        private bool IsInCurve(PointF unitPoint, List<PointF> points)
        {
            bool flag = false;
            if (points.Any() && unitPoint.X <= points[points.Count - 1].X && unitPoint.X >= points[0].X)
            {
                float y = this.GetUnitY(unitPoint.X, points);
                float distance = this.GetDistance(new PointF(unitPoint.X, y), unitPoint);
                if (distance < CAPTURE_THRESHOLD)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public float GetUnitY(float x, List<PointF> points)
        {
            float y = 0;
            float xMin = points[0].X;
            float xMax = points[points.Count - 1].X;

            if (x <= xMin)
            {
                y = points[0].Y;
            }
            else if (x >= xMax)
            {
                y = points[points.Count - 1].Y;
            }
            else
            {
                int index = 0;
                bool isValid = false;
                foreach (var m in points)
                {
                    if (m.X >= x)
                    {
                        isValid = true;
                        break;
                    }
                    index++;
                }

                if (isValid)
                {
                    var p1 = points[index - 1];
                    var p2 = points[index];

                    float k = (p1.Y - p2.Y) / (p1.X - p2.X);
                    float b = p1.Y - k * p1.X;
                    y = k * x + b;
                }
            }

            return y;
        }

        private int GetIndex(PointF unitPoint, List<PointF> points)
        {
            int index = -1;

            for (int i = 0; i < points.Count; i++)
            {
                //var tmp1 = this.ToScreen(point);
                //var tmp2 = this.ToScreen(this.Points[i]);
                var tmp1 = unitPoint;
                var tmp2 = points[i];
                if (Math.Abs(tmp1.X - tmp2.X) < CAPTURE_THRESHOLD)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }
        #endregion
    }

    public class OnCurveChangedEventArgs : EventArgs
    {
        public List<PointF> CurveData { get; set; }
    }

    [Serializable]
    public class Curve
    {
        public string Id { get; set; }
        public List<PointF> Points { get; set; } = new List<PointF>();
        public Color DisColor { get; set; } = Color.Black;
        public PatternTypes Pattern { get; set; }

        public Curve()
        {

        }
    }
}
