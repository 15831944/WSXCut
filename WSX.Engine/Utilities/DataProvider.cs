using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WSX.Engine.Models;

namespace WSX.Engine.Utilities
{
    public class DataProvider
    {
        public List<DataUnit> DataCollection { get; set; } = new List<DataUnit>();
        public const double POSITIVE_WHOLE_LEN = 9999999;
        public const double NEGATIVE_WHOLE_LEN = -9999999;
        public DataUnit OutLine { get; set; }
        public double CurrentLen { get; set; }
        public double TotalLen
        {
            get
            {
                return this.GetLength(this.DataCollection);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return !this.DataCollection.Any();
            }
        }

        public int DrawObjectCount { get; set; }

        public RectangleF? GetOutlineRect()
        {
            RectangleF? rect = null;
            if (this.OutLine != null)
            {
                var points = this.OutLine.Points;
                float x = points[0].X;
                float y = points[0].Y;
                float width = points[2].X - points[1].X;
                float height = points[1].Y - points[0].Y;
                rect = new RectangleF(x, y, width, height);
            }
            return rect;
        }
 
        public DataProvider()
        {

        }

        public List<DataUnit> GetMovementData(double offset)
        {
            double len1 = this.CurrentLen;
            double len2 = this.CurrentLen + offset;
            if (len2 < 0)
            {
                len2 = 0;
            }

            var collection = new List<DataUnit>();
            var item1 = this.GetDataUnitCollection(len1);
            var item2 = this.GetDataUnitCollection(len2);
            if (Math.Abs(offset) > 0.0001)
            {
                collection.AddRange(this.GetOffset(item1, item2));
            }

            return collection;
        }

        public PointF? Locate(ref double len)
        {
            if (this.IsEmpty)
            {
                return null;
            }

            PointF? point = null;
            if (len == 0)
            {
                point = this.DataCollection[0].Points[0];
            }
            else
            {
                var items = this.GetDataUnitCollection(len);
                if (items.Any())
                {
                    if (items[items.Count - 1].LayerId == 0)
                    {
                        items[items.Count - 1] = this.DataCollection[items.Count - 1];
                        len = this.GetLength(items);
                    }
                    var tmp = items[items.Count - 1].Points;
                    point = tmp[tmp.Count - 1];
                }
            }
            return point;
        }

        public void Move(PointF offset)
        {
            foreach (var m in DataCollection)
            {
                for (int i = 0; i < m.Points.Count; i++)
                {
                    float x = m.Points[i].X;
                    float y = m.Points[i].Y;
                    m.Points[i] = new PointF(x + offset.X, y + offset.Y);
                }
            }

            var points = this.OutLine.Points;
            for (int i = 0; i < points.Count; i++)
            {
                float x = points[i].X;
                float y = points[i].Y;
                points[i] = new PointF(x + offset.X, y + offset.Y);
            }
        }

        private List<DataUnit> GetDataUnitCollection(double len)
        {
            var items = new List<DataUnit>();
            double len1 = 0;
            foreach (var m in this.DataCollection)
            {
                len1 += m.Length;
                if (len1 < len)
                {
                    items.Add(m);
                }
                else
                {
                    var item = m.CreateNew(0, m.Length - (len1 - len));
                    if (item != null)
                    {
                        items.Add(item);
                    }
                    break;
                }
            }
            return items;
        }

        private List<DataUnit> GetOffset(List<DataUnit> item1, List<DataUnit> item2)
        {
            var collection = new List<DataUnit>();
            double len1 = this.GetLength(item1);
            double len2 = this.GetLength(item2);

            if (len1 != len2)
            {
                Action<List<DataUnit>, List<DataUnit>> handler = (tmp1, tmp2) =>
                {
                    if (!tmp1.Any())
                    {
                        collection.AddRange(tmp2);
                    }
                    else
                    {
                        var data1 = tmp1[tmp1.Count - 1];
                        var data2 = tmp2[tmp1.Count - 1];
                        var firstPart = data2.CreateNew(data1.Length, data2.Length - data1.Length);
                        var leftPart = tmp2.GetRange(tmp1.Count, tmp2.Count - tmp1.Count);
                        if (firstPart != null)
                        {
                            collection.Add(firstPart);
                        }
                        collection.AddRange(leftPart);
                    }
                };

                if (len1 <= len2)
                {
                    handler(item1, item2);
                }
                else
                {
                    handler(item2, item1);

                    foreach (var m in collection)
                    {
                        m.Points.Reverse();
                    }
                    collection.Reverse();
                }

                //if final is empty move, then add extra path
                if (collection[collection.Count - 1].LayerId == 0)
                {
                    var item = this.DataCollection[item2.Count - 1];
                    double len = item2[item2.Count - 1].Length;
                    double offset = len1 < len2 ? POSITIVE_WHOLE_LEN : NEGATIVE_WHOLE_LEN;
                    var dataItem = item.CreateNew(len, offset);
                    collection.Add(dataItem);
                }
            }

            return collection;
        }

        private double GetLength(List<DataUnit> collection)
        {
            double len = 0;
            foreach (var m in collection)
            {
                len += m.Length;
            }
            return len;
        }

        #region Static methods
        public static bool Equals(DataProvider data1, DataProvider data2)
        {
            if (data1.DataCollection.Count != data2.DataCollection.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < data1.DataCollection.Count; i++)
                {
                    var tmp1 = data1.DataCollection[i];
                    var tmp2 = data2.DataCollection[i];
                    if (tmp1.Points.Count != tmp2.Points.Count)
                    {
                        return false;
                    }
                    else
                    {
                        for (int j = 0; j < tmp1.Points.Count; j++)
                        {
                            var p1 = tmp1.Points[j];
                            var p2 = tmp2.Points[j];
                            if (p1 != p2)
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }         
        }

        public static bool IsTranslational(DataProvider data1, DataProvider data2)
        {
            bool flag = true;
            double len1 = data1.TotalLen;
            double len2 = data2.TotalLen;
            if (Math.Abs(len1 - len2) < 0.001 && data1.DataCollection.Count == data2.DataCollection.Count)
            {
                var item1 = GetAllPoints(data1);
                var item2 = GetAllPoints(data2);
                if (item1.Count == item2.Count)
                {
                    double xDiff = item1[0].X - item2[0].X;
                    double yDiff = item1[0].Y - item2[0].Y;
                    for (int i = 1; i < item1.Count; i++)
                    {
                        double xDiff1 = item1[i].X - item2[i].X;
                        double yDiff1 = item1[i].Y - item2[i].Y;
                        if (Math.Abs(xDiff1 - xDiff) > 0.001 || Math.Abs(yDiff1 - yDiff) > 0.001)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public static List<PointF> GetAllPoints(DataProvider data)
        {
            var points = new List<PointF>();
            foreach (var m in data.DataCollection)
            {
                points.AddRange(m.Points);
            }
            return points;
        }
        #endregion
    }


}
