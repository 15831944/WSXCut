using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WSX.Engine.Models;
using WSX.Engine.Operation.Jobs;
using WSX.Engine.Operation.Jobs.Fixed;
using WSX.Engine.Operation.Jobs.Movable;
using WSX.Engine.Utilities;
using WSX.GlobalData.Model;
using WSX.Hardware.Motor;
using WSX.Logger;

namespace WSX.Engine.Operation.Process
{
    public class MachineProcess : IProcess
    {
        private List<IJob> jobs = new List<IJob>();
        private bool blowingEnabled;
        private bool laserEnabled;
        private bool pierceDisabled;
        private bool followEnabled;
        private CancellationToken token;
        private ILayerConfig layerConfig;

        public MachineInformation MachineInfo
        {
            get
            {
                //var infos = new List<MachineInformation>();
                //foreach (var m in this.jobs)
                //{
                //    infos.Add(m.MachineInfo);
                //}
                //var info = MachineInformation.Combine(infos);
                //info.Delay += this.GetTotalBlowingOnDelay();
                //return info;
              
                var infos = new MachineInformation[this.jobs.Count];
                int step = 1000;
                int total = this.jobs.Count;
                int cnt = total / step + 1;            
                var res = Parallel.For(0, cnt, x => 
                {
                    int begin = x * step;
                    int len = step;
                    if (x == cnt - 1)
                    {
                        len = total - begin;
                    }
                    var part = this.jobs.GetRange(begin, len);
                    int index = begin;
                    foreach (var m in part)
                    {
                        infos[index] = m.MachineInfo;
                        index++;
                    }
                });
                var info = MachineInformation.Combine(new List<MachineInformation>(infos));
                info.Delay += this.GetTotalBlowingOnDelay();
                return info;
            }
        }

        public bool? IsEmptyMoveShort { get; set; }

        public bool IsFirst { get; set; }

        public bool IsNeedToLaserOff { get; set; } = true;

        public bool IsNeedToBlowingOff { get; set; } = true;

        public bool IsNeedToFollowOff { get; set; } = true;

        public void Execute(CancellationToken token, Predicate<MotorInfoMap<double>> posInfoHandler)
        {            
            this.token = token;
            var first = this.jobs.First().GetDataCollection().First();
            this.layerConfig = this.jobs[0] as ILayerConfig;
            this.UpdateControlInfo();

            try
            {
                this.MoveZToCutHeight();
                this.BlowingOn();
                this.LaserOn();

                foreach (var m in this.jobs.SkipWhile(x => pierceDisabled && x.GetType() == typeof(PierceJob)))
                {                  
                    this.layerConfig = m as ILayerConfig;                    
                    m.Execute(token, posInfoHandler, this.IsFirst);
                    this.IsFirst = false;
                }
            }
            catch(Exception ex)
            {
                LogUtil.Instance.Error(ex.Message);
            }
            finally
            {
                this.LaserOff();
                this.BlowingOff();
                this.MoveZToLiftHeight();
            }

            token.ThrowIfCancellationRequested();
        }

        public void AddJob(IJob job)
        {
            this.jobs.Add(job);
        }

        private void UpdateControlInfo()
        {
            var operType = SystemContext.MachineControlPara.OperationType;
            if (operType == OperationTypes.EmptyMove || operType == OperationTypes.Outline || operType == OperationTypes.Step)
            {
                this.blowingEnabled = false;
                this.laserEnabled = false;
                this.pierceDisabled = true;  //Skip pierce when empty move
                if (operType == OperationTypes.EmptyMove)
                {
                    this.followEnabled = GlobalModel.Params.LayerConfig.IsEnableFollowEmptyMove;
                }
            }
            else
            {
                this.blowingEnabled = true;
                this.laserEnabled = true;
                this.pierceDisabled = operType == OperationTypes.FastMachinig;
                this.followEnabled = !GlobalModel.Params.LayerConfig.IsDisableFollowProcessed;
            }       
        }

        public void Optimize()
        {
            if (this.jobs.Count < 2)
            {
                return;
            }

            var tmp = new List<IJob>();
            var job = this.jobs[0];
            tmp.Add(job);
            for (int i = 0; i < this.jobs.Count - 1; i++)
            {
                var t1 = this.jobs[i].GetType();
                var t2 = this.jobs[i + 1].GetType();
                if (t1 == t2)
                {
                    foreach (var m in this.jobs[i + 1].GetDataCollection())
                    {
                        job.AddData(m);
                    }
                }
                else
                {
                    job = this.jobs[i + 1];
                    tmp.Add(job);
                }
            }
            this.jobs = tmp;
        }

        private void LaserOn()
        {
            if (this.laserEnabled)
            {
                var laserPara = this.layerConfig.LaserPara;
                if (laserPara != null)
                {
                    SystemContext.Hardware?.LaserOn(laserPara);
                    OperationEngine.Instance.NotifyLaserParaChanged(laserPara);
                    OperationEngine.Instance.NotifyLaserEnabled(true);
                    this.RunWaitTask(this.layerConfig.LaserOnDelay);
                }
            }
        }

        private void LaserOff()
        {
            if (this.laserEnabled && this.layerConfig.NeedToLaserOff)
            {
                this.RunWaitTask(this.layerConfig.LaserOffDelay);
                SystemContext.Hardware?.LaserOff();
                OperationEngine.Instance.NotifyLaserEnabled(false);
            }
        }

        private void BlowingOn()
        {
            if (this.blowingEnabled)
            {
                var para = this.layerConfig.GasPara;
                if (para != null)
                {
                    SystemContext.Hardware?.SetBlowingPara(para.Item1, para.Item2);
                    if (SystemContext.Hardware?.IsBlowingOn == false)
                    {
                        SystemContext.Hardware?.BlowingOn();
                        OperationEngine.Instance.NotifyBlowingEnabled(true);
                        double delay = GlobalModel.Params.LayerConfig.OpenAirDelay;
                        if (this.IsFirst)
                        {
                            delay += GlobalModel.Params.LayerConfig.FirstOpenAirDelay;
                        }
                        this.RunWaitTask((int)delay);
                    }
                }
            }
        }

        private void BlowingOff()
        {
            if (this.blowingEnabled && this.layerConfig.NeedToBlowingOff && this.IsNeedToBlowingOff)
            {
                SystemContext.Hardware?.BlowingOff();
                OperationEngine.Instance.NotifyBlowingEnabled(false);
            }
        }

        private void MoveZToCutHeight()
        {
            double height = this.layerConfig.NozzleHeight;
            if (!double.IsNaN(height) && this.followEnabled)
            {
                //System.Diagnostics.Debug.WriteLine("NozzeleHeight = " + height);
                bool isOnlyLocate = GlobalModel.Params.LayerConfig.IsOnlyPositionZCoordinate;
                if (isOnlyLocate)
                {
                    height = GlobalModel.Params.LayerConfig.PositionZCoordinate - height;
                    SystemContext.Hardware?.MoveZ(height, false, true, this.token);
                }
                else
                {
                    SystemContext.Hardware?.MoveZ(height);
                    SystemContext.Hardware?.FollowOn();
                    OperationEngine.Instance.NotifyFollowEnabled(true);
                    OperationEngine.Instance.NotifyTargetFollowHeightChanged(height);
                }
            }
            else
            {
                OperationEngine.Instance.NotifyFollowEnabled(false);
            }
        }

        private void MoveZToLiftHeight()
        {
            double height = this.layerConfig.LiftHeight;
            //Don't lift Z when distance is short
            if(!this.layerConfig.NeedToLift && this.IsEmptyMoveShort == true)          
            {
                height = double.NaN;
                //System.Diagnostics.Debug.WriteLine("*************** UnLift when short distance *********************");
            }

            if (!double.IsNaN(height) && this.followEnabled)
            {
                //System.Diagnostics.Debug.WriteLine("Lift Height = " + height);
                bool skipWait = GlobalModel.Params.LayerConfig.IsFrogStyleLift && this.IsEmptyMoveShort == false;
                SystemContext.Hardware?.MoveZ(height, true, !skipWait, this.token);
            }
        }

        private void RunWaitTask(double delayMS)
        {
            if (!double.IsNaN(delayMS))
            {
                this.token.WaitHandle.WaitOne((int)delayMS);
                //this.token.ThrowIfCancellationRequested();
            }
        }

        private double GetTotalBlowingOnDelay()
        {
            var tmp = jobs.Where(x => x is EmptyMoveJob || x is CutJob || x is PointCutJob).ToList();
            int cnt = 1;
            for (int i = 1; i < tmp.Count; i++)
            {
                var item1 = tmp[i - 1];
                var item2 = tmp[i];
                if (item2 is EmptyMoveJob)
                {
                    int id = item1.GetDataCollection()[0].LayerId;
                    var layerPara = GlobalModel.Params.LayerConfig.LayerCrafts[id];
                    if (!layerPara.IsKeepPuffing)
                    {
                        cnt++;
                    }
                }
            }
            double delay = GlobalModel.Params.LayerConfig.OpenAirDelay * cnt;
            if (this.IsFirst)
            {
                delay += GlobalModel.Params.LayerConfig.FirstOpenAirDelay;
            }
            return delay / 1000.0;
        }

    }
}
