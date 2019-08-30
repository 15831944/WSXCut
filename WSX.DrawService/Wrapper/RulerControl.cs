using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WSX.DrawService.Wrapper
{
    public partial class RulerControl : UserControl
    {
        private static readonly List<double> MIN_SCALE_LIST = new List<double> {0.001d,0.01d, 0.1d, 1d, 10d, 100d, 1000d, 10000d, 100000d };

        public double Min { get; set; }
        public double Max { get; set; }
        public double Current { get; set; }
        public RulerDirection Direction { get; set; }

        public RulerControl()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            #region Check Valid
            if (this.Direction == RulerDirection.Vertical)
            {
                if (this.Width > this.Height)
                {
                    return;
                }
            }

            if (this.Direction == RulerDirection.Horitonal)
            {
                if ((this.Width < this.Height))
                {
                    return;
                }
            }
            #endregion

            double total = this.Max - this.Min;
            double distancePer5Pixel = this.Direction == RulerDirection.Vertical ?
                                       (total / this.Height) * 5 :
                                       (total / this.Width) * 5;
           
            if (distancePer5Pixel > 0)
            {
                try
                {
                    double step = MIN_SCALE_LIST.First(x => x > distancePer5Pixel);
                    double start = (double)((int)(this.Min / step) * step);
                    var graphics = e.Graphics;
                    double ratio = this.Direction == RulerDirection.Vertical ? 
                                    this.Height / total : 
                                    this.Width / total;

                    double index = 0;
                    for (double i = start; i < this.Max; i += step)
                    {
                        index++;

                        bool condition1 = i % (step * 5) == 0;
                        bool condition2 = i % (step * 10) == 0;
                        if (step < 1)
                        {
                            int temp = (int)Math.Log10(1 / step);
                            double iTemp = Math.Round(i * Math.Pow(10, temp));
                            double stepTemp = Math.Round(step * Math.Pow(10, temp));

                            condition1 = iTemp % (stepTemp * 5) == 0;
                            condition2 = iTemp % (stepTemp * 10) == 0;
                        }

                        if (this.Direction == RulerDirection.Vertical)
                        {
                            #region Vertical Ruler
                            int x = 0;
                            int y = (int)(this.Height - (i - this.Min) * ratio);

                            if (condition1 && condition2)
                            {
                                x = 0;

                                if (y > 30)
                                {
                                    var brush = new SolidBrush(Color.Blue);
                                    if (i.ToString("0.##").Equals("0"))
                                    {
                                        brush.Color = Color.Red;
                                    }

                                    int offset = this.Height / 2 - y;
                                    graphics.TranslateTransform(this.Width / 2, this.Height / 2);
                                    graphics.RotateTransform(270);
                                    graphics.DrawString(i.ToString("0.##"), this.Font, brush, new Point(offset, -10));
                                    graphics.ResetTransform();
                                }
                            }
                            else if (condition1)
                            {
                                x = this.Width - 8;
                            }
                            else
                            {
                                x = this.Width - 5;
                            }
                            graphics.DrawLine(Pens.Blue, new Point(this.Width, y), new Point(x, y));
                            #endregion
                        }
                        else
                        {
                            #region Horizontal Ruler
                            int x = (int)((i - this.Min) * ratio);
                            int y = this.Height;

                            if (condition1 && condition2)
                            {
                                y = 0;

                                if (this.Width - x > 30)
                                {
                                    var brush = new SolidBrush(Color.Blue);
                                    if (i.ToString("0.##").Equals("0"))
                                    {
                                        brush.Color = Color.Red;
                                    }

                                    graphics.DrawString(i.ToString("0.##"), this.Font, brush, new Point(x, 0));
                                }
                            }
                            else if (condition1)
                            {
                                y = this.Height - 8;
                            }
                            else
                            {
                                y = this.Height - 5;
                            }
                            graphics.DrawLine(Pens.Blue, new Point(x, this.Height), new Point(x, y));
                            #endregion
                        }
                    }


                    if (this.Direction == RulerDirection.Horitonal)
                    {
                        int x = (int)((this.Current - this.Min) * 1.0 * this.Width / (this.Max - this.Min));
                        graphics.DrawLine(Pens.Red, new Point(x, this.Height), new Point(x, 0));
                    }
                    else
                    {
                        int y = (int)((this.Current - this.Min) * 1.0 * this.Height / (this.Max - this.Min));
                        graphics.DrawLine(Pens.Red, new Point(0, this.Height - y), new Point(this.Width, this.Height - y));
                    }
                }
                catch
                {

                }

            }

        }

      
    }
}
