using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using WSX.CommomModel.Utilities;
using WSX.Engine.Models;
using WSX.Engine.Utilities;

namespace WSX.Engine.Operation.Batch
{
    public class SimulationBatch : IBatch
    {
        private const float LINE_SETP = 0.2f;
        private const int INTERVAL_MS = 2;
        private DelayHelper delayHelper = new DelayHelper();

        public void MakeMachining(DataProvider data, CancellationToken token)
        {
            var collection = this.GenerateData(data);
            this.Simulate(collection, token);
        }

        private List<Tuple<string, PointF>> GenerateData(DataProvider data)
        {
            var items = data.GetMovementData(DataProvider.POSITIVE_WHOLE_LEN);
            var collection = new List<Tuple<string, PointF>>();
            foreach (var m in items)
            {
                string id = m.LayerId == 0 ? null : Guid.NewGuid().ToString();
                if (m.Id == DataUnitTypes.Arc)
                {
                    collection.AddRange(m.Points.Select(x => Tuple.Create(id, x)));
                }
                else
                {
                    for (int i = 0; i < m.Points.Count - 1; i++)
                    {
                        var p1 = m.Points[i];
                        var p2 = m.Points[i + 1];
                        var tmp = MathEx.GetLinePoints(p1, p2, LINE_SETP);
                        collection.AddRange(tmp.Select(x => Tuple.Create(id, x)));
                    }
                }
            }
            return collection;
        }

        private void Simulate(List<Tuple<string, PointF>> collection, CancellationToken token)
        {
            foreach (var m in collection)
            {
                OperationEngine.Instance.NotifyMarkPointChanged(m.Item2);
                if (!string.IsNullOrEmpty(m.Item1))
                {
                    OperationEngine.Instance.NotityMarkPathAdd(m.Item1, m.Item2);
                }

                this.delayHelper.Delay(INTERVAL_MS);
                if (token.IsCancellationRequested)
                {
                    break;
                }
                //if (token.WaitHandle.WaitOne(INTERVAL_MS))
                //{
                //    break;
                //}
            }
        }
     
    }
}
