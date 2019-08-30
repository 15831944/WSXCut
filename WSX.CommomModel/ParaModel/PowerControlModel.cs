using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{

    public enum PatternTypes
    {
        Default,
        Segment,
        Linear,
        Smoothing
    }

    [Serializable]
    public class DataCurve
    {
        public PatternTypes Pattern { get; set; }
        public List<PointF> Points { get; set; } = new List<PointF>();

        //public DataCurve()
        //{
        //    this.Points = new List<PointF>();
        //    this.Points.Add(new PointF(10, 50));
        //    this.Points.Add(new PointF(80, 100));
        //}

        public double GetY(double x)
        {
            if (this.Points == null || this.Points.Count < 2)
            {
                return double.NaN;
            }

            double res = double.NaN;
            if (x <= this.Points.First().X)
            {
                res = this.Points.First().Y;
            }
            else if (x >= this.Points.Last().X)
            {
                res = this.Points.Last().Y;
            }
            else
            {
                var p1 = this.Points.Where(p => p.X > x).First();
                int index = this.Points.IndexOf(p1);
                var p2 = this.Points[index - 1];

                double k = (p1.Y - p2.Y) / (p1.X - p2.X);
                double b = p1.Y - k * p1.X;
                res = k * x + b;
            }
            return res;
        }
    }

    public class PowerControlModel
    {      
        public virtual bool ShowAbs { get; set; }
        public virtual bool PowerAdjustmentEnabled { get; set; }      
        public virtual bool FreqAdjustmentEnabled { get; set; }
        public DataCurve PowerCurve { get; set; } = new DataCurve();
        public DataCurve FreqCurve { get; set; } = new DataCurve();
    }
}
