using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WSX.DrawService.CanvasControl;
using WSX.Engine.Models;

namespace WSX.Engine.Utilities
{
    public class DataBuilder
    {
        private readonly DataProvider dataProvider;
        private readonly List<IDrawObject> drawObjects;

        public DataBuilder(DataProvider data, List<IDrawObject> drawObjects)
        {
            this.dataProvider = data;
            this.drawObjects = drawObjects;
        }

        public void CalMovementData()
        {
            //foreach (var m in this.drawObjects)
            //{
            //    var parser = new DrawObjectParser(m);
            //    this.dataProvider.DataCollection.AddRange(parser.Parse());
            //}
            //this.dataProvider.DrawObjectCount = this.drawObjects.Count;

            int step = 5000;
            int total = this.drawObjects.Count;
            int cnt = total / step + 1;
            var tmp = new List<DataUnit>[total];
            var res = Parallel.For(0, cnt, x =>
            {
                int begin = x * step;
                int len = step;
                if (x == cnt - 1)
                {
                    len = total - begin;
                }
                var part = this.drawObjects.GetRange(begin, len);
                int index = begin;
                foreach (var m in part)
                {
                    var parser = new DrawObjectParser(m);
                    tmp[index] = parser.Parse();
                    index++;
                }
            });

            foreach (var m in tmp)
            {
                this.dataProvider.DataCollection.AddRange(m);
            }

            this.dataProvider.DrawObjectCount = this.drawObjects.Count;
        }

        public void CalOutLine()
        {
            var collection = this.dataProvider.DataCollection;
            if (!collection.Any())
            {
                return;
            }

            var points = collection[0].Points;
            double xMin = points.Min(p => p.X);
            double xMax = points.Max(p => p.X);
            double yMin = points.Min(p => p.Y);
            double yMax = points.Max(p => p.Y);

            if (this.dataProvider.DataCollection.Count > 1)
            {
                for (int i = 1; i < collection.Count; i++)
                {
                    points = collection[i].Points;

                    double xMin1 = points.Min(p => p.X);
                    if (xMin > xMin1)
                    {
                        xMin = xMin1;
                    }

                    double xMax1 = points.Max(p => p.X);
                    if (xMax < xMax1)
                    {
                        xMax = xMax1;
                    }

                    double yMin1 = points.Min(p => p.Y);
                    if (yMin > yMin1)
                    {
                        yMin = yMin1;
                    }

                    double yMax1 = points.Max(p => p.Y);
                    if (yMax < yMax1)
                    {
                        yMax = yMax1;
                    }
                }
            }

            double width = Math.Abs(xMax - xMin);
            double height = Math.Abs(yMax - yMin);
            if (width < 1 || height < 1)
            {
                xMin -= 1;
                xMax += 1;
                yMin -= 1;
                yMax += 1;
            }

            var items = new List<PointF>
            {
                new PointF((float)xMin, (float)yMin),
                new PointF((float)xMin, (float)yMax),
                new PointF((float)xMax, (float)yMax),
                new PointF((float)xMax, (float)yMin)
            };

            this.dataProvider.OutLine = new DataUnit(DataUnitTypes.Polyline, 0, items);
        }

        public void InsertEmptyLine()
        {
            var collection = this.dataProvider.DataCollection;
            if (!collection.Any())
            {
                return;
            }

            var newCollection = new List<DataUnit>();
          
            newCollection.Add(collection[0]);
            for (int i = 0; i < collection.Count - 1; i++)
            {
                var data1 = collection[i];
                var data2 = collection[i + 1];

                if (!DataUnit.IsContinous(data1, data2))
                {
                    newCollection.Add(DataUnit.JoinTwoDataUnit(data1, data2));
                }
                newCollection.Add(data2);
            }

            this.dataProvider.DataCollection = newCollection;
        }
    }
}
