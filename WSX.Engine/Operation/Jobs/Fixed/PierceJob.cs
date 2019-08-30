using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WSX.Engine.Models;
using WSX.Engine.Utilities;
using WSX.GlobalData.Model;
using WSX.Hardware.Motor;

namespace WSX.Engine.Operation.Jobs.Fixed
{
    public class PierceJob :JobBase
    {
        private readonly PierceBroker broker;

        public PierceJob(List<DataUnit> dataCollection) : base(dataCollection)
        {
            var layerPara = GlobalModel.Params.LayerConfig.LayerCrafts[dataCollection[0].LayerId];
            var operType = SystemContext.MachineControlPara.OperationType;
            bool laserEnabled = true;
            if (operType == OperationTypes.EmptyMove || operType == OperationTypes.Outline || operType == OperationTypes.Step)
            {
                laserEnabled = false;           
            }
            var config = new PierceConfig
            {
                Level = layerPara.PierceLevel,
                Level1Para = layerPara.PierceLevel1,
                Level2Para = layerPara.PierceLevel2,
                Level3Para = layerPara.PierceLevel3,
                FollowEnabled = !layerPara.IsNoFollow,
                LaserEnabled = laserEnabled
            };
            this.broker = new PierceBroker(config);
            var engine = OperationEngine.Instance;
            broker.OnFollowHeightSet += x => engine.NotifyTargetFollowHeightChanged(x);
            broker.FollowHeightChanged += x => engine.NotifyActualFollowHeightChanged(x);          
            broker.OnLaserParaChanged += x =>engine.NotifyLaserParaChanged(x);
            broker.OnLaserEnabledChanged += x => engine.NotifyLaserEnabled(x);
            broker.OnBlowingEnabledChanged += x => engine.NotifyBlowingEnabled(x);
            broker.OnFollowChanged += x => engine.NotifyFollowEnabled(x);
        }

        public override MachineInformation MachineInfo => this.broker.MachineInfo;
       
        public override void Execute(CancellationToken token, Predicate<MotorInfoMap<double>> posInfoHandler, bool isFirst)
        {
            this.broker.Execute(token, isFirst);
            if (SystemContext.Hardware?.IsLaserOn == false)
            {
                SystemContext.Hardware?.LaserOn();
                OperationEngine.Instance.NotifyLaserEnabled(true);
            }
        }
    }
}
