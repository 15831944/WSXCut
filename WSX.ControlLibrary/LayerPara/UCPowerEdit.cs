using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using WSX.CommomModel.ParaModel;
using WSX.CommomModel.Physics;
using WSX.CommomModel.Physics.Converters;
using WSX.CommomModel.Utilities;
using WSX.ControlLibrary.Common;

namespace WSX.ControlLibrary.LayerPara
{
    public partial class UCPowerEdit : DevExpress.XtraEditors.XtraUserControl
    {
        private const string PWR_CURVE_ID = "PwrCurve";
        private const string FRE_CURVE_ID = "FreCurve";
        private readonly string id;
        private string xSuffix;
        private string ySuffix;
       
        public DataCurveInfo Curve { get; private set; }

        public UCPowerEdit()
        {
            InitializeComponent();
  
            PropertyInfo pi = this.dgvCurve.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dgvCurve, true, null);
        }

        public UCPowerEdit(string id, DataCurveInfo curve) : this()
        {
            this.id = id;
            this.Curve = curve;
            this.InitializeCurve();
        }

        public void UpdateCurveData(DataCurveInfo curve)
        {
            this.Curve = curve;
            this.InitializeCurve();
        }

        private void InitializeCurve()
        {
            this.chartControlCurve.Clear();
            this.chartControlCurve.AddCurve(new Curve
            {
                Id = this.id,
                Points = CopyUtil.DeepCopyBaseOnJSon(this.Curve.Data.Points),
                DisColor = this.id == PWR_CURVE_ID ? Color.Red: Color.Blue,
                Pattern = this.Curve.Data.Pattern
            });

            string xLegend = null;
            string yLegend = null;
            double xMax = 0;
            double yMax = 0;

            bool showAbs = this.Curve.ShowAbs;
            if (showAbs)
            {
                string str1 = this.Curve.SpeedUnit.GetDescription();
                xLegend = $"Speed({str1})";
                yLegend = this.id == PWR_CURVE_ID ? "Power(W)" : "Frequency(Hz)";
                xMax = double.Parse(SpeedUnitConverter.Convert(this.Curve.Speed));
                yMax = Constants.MaxPower * this.Curve.PowerPercentage / 100.0;
                this.xSuffix = str1;
                this.ySuffix = this.id == PWR_CURVE_ID ? "W" : "Hz";
            }
            else
            {
                xLegend = "Speed(%)";
                yLegend = this.id == PWR_CURVE_ID ? "Power(%)" : "Frequency(%)";
                xMax = 100;
                yMax = 100;
                this.xSuffix = "%";
                this.ySuffix = "%";
            }

            this.chartControlCurve.XLengend = xLegend;
            this.chartControlCurve.YLengend = yLegend;
            this.chartControlCurve.XMax = xMax;
            this.chartControlCurve.YMax = yMax;

            this.UpdateDGV();
            this.checkAbs.Checked = showAbs;
            this.cmbPatternPwr.SelectedIndex = (int)this.Curve.Data.Pattern;

            if (this.id == PWR_CURVE_ID)
            {
                this.dgvCurve.Columns[1].HeaderText = "Power";
            }
            else
            {
                this.dgvCurve.Columns[1].HeaderText = "Frequency";
            }
        }

        private void UpdateDGV()
        {
            this.dgvCurve.Rows.Clear();
            foreach (var m in this.Curve.Data.Points)
            {
                int index = this.dgvCurve.Rows.Add();
                DataGridViewRow row = this.dgvCurve.Rows[index];
                double x = m.X;
                double y = m.Y;
                if (this.Curve.ShowAbs)
                {
                    x *= double.Parse(SpeedUnitConverter.Convert(this.Curve.Speed)) / 100.0;
                    if (id == PWR_CURVE_ID)
                    {
                        y *= Constants.MaxPower / 100.0;
                    }
                    else
                    {
                        y *= this.Curve.Freq / 100.0;
                    }              
                }
                row.Cells[0].Value = x.ToString("0.00") + this.xSuffix;
                row.Cells[1].Value = y.ToString("0.00") + this.ySuffix;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.dgvCurve.SelectedCells.Count >= 1)
            {
                int rowIndex = this.dgvCurve.SelectedCells[0].RowIndex;
                int columnIndex = this.dgvCurve.SelectedCells[0].ColumnIndex;
                if (rowIndex == this.dgvCurve.RowCount - 1)
                {
                    rowIndex--;
                }

                var p1 = this.Curve.Data.Points[rowIndex];
                var p2 = this.Curve.Data.Points[rowIndex + 1];
                float x = (p1.X + p2.X) / 2.0f;
                float y = (p1.Y + p2.Y) / 2.0f;
                this.Curve.Data.Points.Insert(rowIndex + 1, new PointF(x, y));
                this.InitializeCurve();
                this.dgvCurve.Rows[rowIndex + 1].Cells[columnIndex].Selected = true;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.Curve.Data.Points.Count <= 2)
            {
                return;
            }

            if (this.dgvCurve.SelectedCells.Count >= 1)
            {
                int rowIndex = this.dgvCurve.SelectedCells[0].RowIndex;
                int columnIndex = this.dgvCurve.SelectedCells[0].ColumnIndex;
                this.Curve.Data.Points.RemoveAt(rowIndex);
                this.InitializeCurve();
                this.dgvCurve.Rows[rowIndex - 1].Cells[columnIndex].Selected = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Curve.Data.Points.Clear();
            this.Curve.Data.Points.Add(new PointF(10, 50));
            this.Curve.Data.Points.Add(new PointF(80, 100));
            this.InitializeCurve();
        }

        private void dgvCurve_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string content = (string)this.dgvCurve.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string suffix = e.ColumnIndex == 0 ? this.xSuffix : this.ySuffix;
            this.dgvCurve.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = content.Remove(content.IndexOf(suffix));
        }

        private void dgvCurve_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string content = (string)this.dgvCurve.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (double.TryParse(content, out double res))
            {
                string suffix = e.ColumnIndex == 0 ? this.xSuffix : this.ySuffix;
                if (!content.Contains(suffix))
                {
                    if (this.OnPagePwrCellChanged(res, e.RowIndex, e.ColumnIndex))
                    {
                        this.BeginInvoke(new Action(() => this.InitializeCurve()));
                    }
                    else
                    {
                        this.dgvCurve.Rows[e.RowIndex].Cells[e.ColumnIndex].Value += suffix;
                    }
                }
            }
            else
            {
                this.BeginInvoke(new Action(() => this.InitializeCurve()));
            }
        }

        private bool OnPagePwrCellChanged(double cellValue, int rowIndex, int columnIndex)
        {
            bool showAbs = this.Curve.ShowAbs;
            if (showAbs)
            {
                if (columnIndex == 0)
                {
                    double speed = SpeedUnitConverter.ConvertBack(cellValue.ToString());
                    cellValue = speed / this.Curve.Speed * 100.0;
                }
                if (columnIndex == 1)
                {
                    double power = double.Parse(cellValue.ToString());
                    cellValue = power / Constants.MaxPower * 100.0;
                }
            }

            var points = this.Curve.Data.Points;
            double oldValue = 0;
            if (columnIndex == 0)
            {
                oldValue = points[rowIndex].X;
            }
            else
            {
                oldValue = points[rowIndex].Y;
            }

            bool flag = false;
            if (Math.Abs(cellValue - oldValue) > 0.0001)
            {
                flag = true;
                double low = 0;
                double high = 0;

                if (rowIndex == 0)
                {
                    if (columnIndex == 0)
                    {
                        low = 0;
                        high = points[rowIndex + 1].X;
                        if (cellValue > low && cellValue < high)
                        {
                            points[rowIndex] = new PointF((float)cellValue, points[rowIndex].Y);
                        }
                    }
                    else
                    {
                        low = 0;
                        high = points[rowIndex + 1].Y;
                        if (cellValue > low && cellValue < high)
                        {
                            points[rowIndex] = new PointF(points[rowIndex].X, (float)cellValue);
                        }
                    }
                }
                else if (rowIndex == this.Curve.Data.Points.Count - 1)
                {
                    if (columnIndex == 0)
                    {
                        low = points[rowIndex - 1].X;
                        high = 100;
                        if (cellValue > low && cellValue < high)
                        {
                            points[rowIndex] = new PointF((float)cellValue, points[rowIndex].Y);
                        }
                    }
                    else
                    {
                        low = points[rowIndex - 1].Y;
                        high = 100;
                        if (cellValue > low && cellValue < high)
                        {
                            points[rowIndex] = new PointF(points[rowIndex].X, (float)cellValue);
                        }
                    }
                }
                else
                {
                    if (columnIndex == 0)
                    {
                        low = points[rowIndex - 1].X;
                        high = points[rowIndex + 1].X;
                        if (cellValue > low && cellValue < high)
                        {
                            points[rowIndex] = new PointF((float)cellValue, points[rowIndex].Y);
                        }
                    }
                    else
                    {
                        low = points[rowIndex - 1].Y;
                        high = points[rowIndex + 1].Y;
                        if (cellValue > low && cellValue < high)
                        {
                            points[rowIndex] = new PointF(points[rowIndex].X, (float)cellValue);
                        }
                    }
                }
            }

            return flag;
        }

        private void checkAbs_CheckedChanged(object sender, EventArgs e)
        {
            this.Curve.ShowAbs = this.checkAbs.Checked;
            this.InitializeCurve();
        }

        private void chartControlCurve_OnCurveDataChanged(object sender, OnCurveChangedEventArgs e)
        {
            this.Curve.Data.Points = CopyUtil.DeepCopy(e.CurveData);
            this.InitializeCurve();
        }
    }

    public class DataCurveInfo
    {
        public DataCurve Data { get; set; }
        public bool ShowAbs { get; set; }
        public UnitSpeedTypes SpeedUnit { get; set; }
        public double Speed { get; set; }
        public double Freq { get; set; }
        public double PowerPercentage { get; set; }
    }
}
