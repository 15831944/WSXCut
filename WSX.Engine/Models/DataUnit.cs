using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WSX.CommomModel.Utilities;
using WSX.Engine.Utilities;

namespace WSX.Engine.Models
{
    [Serializable]
    public class DataUnit
    {
        public int LayerId { get; private set; }
        public int? AttachedLayerId { get; set; }
        public List<PointF> Points { get; private set; }
        public DataUnitTypes Id { get; private set; }
        public double Length { get; private set; }


        public DataUnit(DataUnitTypes id, int layerId, List<PointF> points)
        {
            this.Id = id;
            this.LayerId = layerId;
            this.Points = points;
            if (this.Points.Count > 1)
            {
                this.Length = MathEx.GetDistance(this.Points);
            }
        }

        public DataUnit CreateNew(double len, double offset)
        {
            //Check args if valid
            double len1 = len;
            double len2 = len + offset;

            if (len1 < 0)
            {
                len1 = 0;
            }
            if (len1 > this.Length)
            {
                len1 = this.Length;
            }

            if (len2 < 0)
            {
                len2 = 0;
            }
            if (len2 > this.Length)
            {
                len2 = this.Length;
            }

            if (len1 == len2)
            {
                return null;
            }

            var tmp = new List<double> { len1, len2 };
            len1 = tmp.Min();
            len2 = tmp.Max();

            var points = new List<PointF>();
            var item1 = this.GetPoints(len2);

            if (len1 == 0)
            {
                points.AddRange(item1);
            }
            else
            {
                var item2 = this.GetPoints(len1);
                int index = item2.Count - 1;
                int cnt = item1.Count - index;

                points.Add(item2[item2.Count - 1]);
                points.AddRange(item1.GetRange(index, cnt));
            }

            if (offset < 0)
            {
                points.Reverse();
            }

            return new DataUnit(this.Id, this.LayerId, points);
        }

        public bool TrySpilt(PointF point, out List<DataUnit> items)
        {
            int index = -1;        
            items = new List<DataUnit>();            
            if (this.Points.Count > 1)
            {
                for (int i = 0; i < this.Points.Count - 1; i++)
                {
                    var p1 = this.Points[i];
                    var p2 = this.Points[i + 1];

                    if (MathEx.IsInLine(point, p1, p2))
                    {
                        index = i;
                        break;
                    }

                    //if (MathEx.IsInLine(point, p1, p2))
                    //{
                    //    //double k1 = MathEx.GetSlope(p1, point);
                    //    //double k2 = MathEx.GetSlope(point, p2);
                    //    //if (Math.Abs(k1 - k2) < 0.001 || (double.IsInfinity(k1) && double.IsInfinity(k2)))
                    //    //{
                    //        index = i;
                    //        break;
                    //    //}
                    //}

                    //bool condition1 = point.X >= p1.X && point.X <= p2.X;
                    //bool condition2 = point.X <= p1.X && point.X >= p2.X;

                    //if (condition1 || condition2)
                    //{
                    //    double k1 = MathEx.GetSlope(p1, point);
                    //    double k2 = MathEx.GetSlope(point, p2);
                    //    if (Math.Abs(k1 - k2) < 0.001 || (double.IsInfinity(k1) && double.IsInfinity(k2)))
                    //    {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                }
            }
            if (index != -1)
            {
                var tmp1 = this.Points.GetRange(0, index + 1);
                tmp1.Add(point);
                var tmp2 = this.Points.GetRange(index + 1, this.Points.Count - (index + 1));
                tmp2.Insert(0, point);

                var part1 = CopyUtil.DeepCopy(this);
                part1.Points = tmp1;
                part1.Length = MathEx.GetDistance(part1.Points);
                var part2 = CopyUtil.DeepCopy(this);
                part2.Points = tmp2;
                part2.Length = MathEx.GetDistance(part2.Points);

                items.Add(part1);
                items.Add(part2);
            }
            return index != -1;
        }

        private List<PointF> GetPoints(double len)
        {
            var points = new List<PointF>();

            if (len > 0 && len < this.Length)
            {
                double total = 0;

                for (int i = 0; i < this.Points.Count - 1; i++)
                {
                    var p1 = this.Points[i];
                    var p2 = this.Points[i + 1];
                    double len1 = MathEx.GetDistance(p1, p2);
                    total += len1;

                    points.Add(p1);
                    if (total > len)
                    {
                        var newPoint = MathEx.GetPointBetweenTwoPoints(p1, p2, len1 - (total - len));
                        points.Add(newPoint);
                        break;
                    }
                }
            }
            else if (len >= this.Length)
            {
                points = new List<PointF>(this.Points.ToArray());
            }

            return points;
        }

        public static bool IsContinous(DataUnit data1, DataUnit data2)
        {
            var p1 = data1.Points[data1.Points.Count - 1];
            var p2 = data2.Points[0];
            return MathEx.GetDistance(p1, p2) < 0.001;
        }

        public static DataUnit JoinTwoDataUnit(DataUnit data1, DataUnit data2)
        {
            var p1 = data1.Points[data1.Points.Count - 1];
            var p2 = data2.Points[0];
            var points = new List<PointF> { p1, p2 };
            return new DataUnit(DataUnitTypes.Polyline, 0, points);
        }
    }
}
