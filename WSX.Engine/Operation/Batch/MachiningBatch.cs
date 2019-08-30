using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using WSX.Engine.Models;
using WSX.Engine.Operation.Process;
using WSX.Engine.Utilities;
using WSX.GlobalData.Model;
using WSX.Hardware.Models;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation.Batch
{
    public class MachiningBatch : IBatch
    {             
        private PointF point;
        private CancellationToken token;         
        private DataProvider data;
        private double totalLen;
        private string figureId;
        private const double MIN_SAMPLE_DISTANCE = 0.1;

        public void MakeMachining(DataProvider data, CancellationToken token)
        {
            this.data = data;
            this.totalLen = this.data.TotalLen;
            this.token = token;

            var collection = this.GetDataCollection(data);
            if (!collection.Any())
            {
                return;
            }

            this.point = collection[0].Points[0];
            this.MoveTo(this.point);

            int offset = 1;
            for (int i = 0; i < collection.Count; i += offset)
            {
                var items = this.GetContinous(i, collection);            
                offset = items.Count;
                
                this.figureId = this.GetFigureId(items[0].LayerId);
                if (!string.IsNullOrEmpty(this.figureId))
                {
                    OperationEngine.Instance.NotityMarkPathAdd(this.figureId, items[0].Points[0]);
                }

                var process = ProcessFactory.CreateNewProcess(items);
                //process.IsFirst = i == 0;
                //var next = (i + offset == collection.Count) ? null : collection[i + offset];
                //if (next != null && next.LayerId == Constants.EmptyLayerId)
                //{
                //    double limit = GlobalModel.Params.LayerConfig.UnLiftMaxEmptyMoveLength;
                //    process.IsEmptyMoveShort = next.Length < limit;
                //}
                this.UpdateProcess(process, collection, items);
                process.Execute(token, this.HandlePosInfo);
              
                token.ThrowIfCancellationRequested();
            }
        }

        private void UpdateProcess(IProcess process, List<DataUnit> collection, List<DataUnit> subCollection)
        {
            int index = collection.IndexOf(subCollection[0]);
            int offset = subCollection.Count;
            process.IsFirst = index == 0;
            var next = (index + offset == collection.Count) ? null : collection[index + offset];
            if (next != null && next.LayerId == Constants.EmptyLayerId)
            {
                double limit = GlobalModel.Params.LayerConfig.UnLiftMaxEmptyMoveLength;
                process.IsEmptyMoveShort = next.Length < limit;
            }
            if (next != null && next.LayerId == Constants.CoolingPointLayerId)
            {
                process.IsNeedToBlowingOff = false;
                process.IsNeedToFollowOff = false;
            }         
        }

        private bool HandlePosInfo(MotorInfoMap<double> posInfo)
        {
            bool valid = false;
            //Update len of dataprovider
            float x = (float)posInfo[AxisTypes.AxisX];
            float y = (float)posInfo[AxisTypes.AxisY];
            PointF p1 = new PointF(x, y);
            //if (!string.IsNullOrEmpty(this.figureId))
            //{
            //    OperationEngine.Instance.NotityMarkPathAdd(this.figureId, p1);
            //}
            double len = MathEx.GetDistance(p1, this.point);
            if (len > MIN_SAMPLE_DISTANCE)
            {
                valid = true;
                this.point = p1;

                bool condition1 = SystemContext.MachineControlPara.OperationType == OperationTypes.Step;
                bool condition2 = SystemContext.MachineControlPara.Step < 0;
                if (condition1 && condition2)
                {
                    this.data.CurrentLen -= len;
                }
                else
                {
                    this.data.CurrentLen += len;
                }

                //Report len, update mark
                OperationEngine.Instance.NotifyMarkPointChanged(p1);
                if (!string.IsNullOrEmpty(this.figureId))
                {
                    OperationEngine.Instance.NotityMarkPathAdd(this.figureId, p1);
                }
                OperationEngine.Instance.ReportMotorPos(posInfo);
                OperationEngine.Instance.ReportProgress(this.data.CurrentLen / totalLen);
            }

            return valid;
        }

        private string GetFigureId(int layerId)
        {
            string figureId = null;
            var operType = SystemContext.MachineControlPara.OperationType;
            bool condition1 = layerId != 0;
            bool condition2 = operType == OperationTypes.Outline && layerId == 0;
            if (condition1 || condition2)
            {
                figureId = Guid.NewGuid().ToString();
            }      
            return figureId;
        }

        private List<DataUnit> GetDataCollection(DataProvider dataProvider)
        {
            var para = SystemContext.MachineControlPara;

            switch (para.OperationType)
            {
                case OperationTypes.Step:
                  return dataProvider.GetMovementData(para.Step);
                case OperationTypes.Outline:
                    var item = new List<DataUnit> { dataProvider.OutLine };
                    item[0].Points.Add(dataProvider.OutLine.Points[0]);
                    return item;
                default:
                  return dataProvider.GetMovementData(DataProvider.POSITIVE_WHOLE_LEN);
            }                
        }

        private List<DataUnit> GetContinous(int index, List<DataUnit> items)
        {
            var item = items[index];
            var collection = new List<DataUnit>() { item };

            for (int i = index; i < items.Count - 1; i++)
            {
                var tmp1 = items[i];
                var tmp2 = items[i + 1];

                if (tmp1.LayerId == tmp2.LayerId)
                { 
                    collection.Add(tmp2);
                }
                else
                {
                    break;
                }
            }

            return collection;
        }

        private void MoveTo(PointF point)
        {
            double speed = GlobalModel.Params.LayerConfig.EmptyMoveSpeed;
            double acceleration = GlobalModel.Params.LayerConfig.EmptyMoveAcceleratedSpeed;
            SystemContext.Hardware?.MoveTo(point, speed, acceleration, this.token, (posInfo, speedInfo) =>
            {
                float x = (float)posInfo[AxisTypes.AxisX];
                float y = (float)posInfo[AxisTypes.AxisY];
                PointF p1 = new PointF(x, y);
                OperationEngine.Instance.NotifyMarkPointChanged(p1);
                OperationEngine.Instance.ReportMotorPos(posInfo);
                OperationEngine.Instance.ReportMotorSpeed(speedInfo);
            });
        }   
    }
}
