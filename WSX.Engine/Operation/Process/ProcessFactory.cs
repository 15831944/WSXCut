using System.Collections.Generic;
using WSX.Engine.Models;
using WSX.Engine.Operation.Jobs;
using WSX.Engine.Operation.Jobs.Fixed;
using WSX.Engine.Operation.Jobs.Movable;

namespace WSX.Engine.Operation.Process
{
    public class ProcessFactory
    {
        public static IProcess CreateNewProcess(List<DataUnit> dataCollection)
        {
            IProcess process = new MachineProcess();
            foreach (var m in dataCollection)
            {
                process.AddJob(CreateNewJob(m));
            }
            process.Optimize();
            return process;
        }

        private static IJob CreateNewJob(DataUnit data)
        {
            IJob job = null;
            var items = new List<DataUnit> { data };
            switch (data.LayerId)
            {
                case Constants.EmptyLayerId:
                    job = new EmptyMoveJob(items);
                    break;
                case Constants.CoolingLayerId:
                    job = new CoolingJob(items);
                    break;
                case Constants.EvaporationLayerId:
                    job = new EvaporationJob(items);
                    break;
                case Constants.CoolingPointLayerId:
                    job = new PointCoolingJob(items);           
                    break; 
                default:
                    if (data.Id == DataUnitTypes.PointCut)
                    {
                        job = new PointCutJob(items);
                    }
                    else if (data.Id == DataUnitTypes.PointPierce)
                    {
                        job = new PierceJob(items);
                    }
                    else
                    {
                        job = new CutJob(items);
                    }
                    break;
            }
            return job;
        }

        //private static void ThrowIfDiscontinuous(List<DataUnit> dataCollection)
        //{
        //    var tmp = dataCollection.Select(x => x.LayerId).Distinct().ToList();
        //    if (tmp.Count != 1)
        //    {
        //        throw new ArgumentException();
        //    }
        //}
    }
}

