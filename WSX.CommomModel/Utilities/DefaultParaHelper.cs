using System.Collections.Generic;
using System.Drawing;
using WSX.CommomModel.ParaModel;

namespace WSX.CommomModel.Utilities
{
    public class DefaultParaHelper
    {
        public static LayerCraftModel GetDefaultLayerCraftModel()
        {
            return new LayerCraftModel
            {
                CutSpeed = 100,
                LiftHeight = 10,
                NozzleHeight = 1,
                PowerPercent = 100,
                PowerValue = 1000,
                PulseDutyFactorPercent = 100,
                PulseFrequency = 1000,
                DelayTime = 200,
                SlowStartSpeed = 2,
                SlowStopSpeed = 2,
                PierceLevel1 = new PierceParameters
                {
                    StepTime = 1000,
                    NozzleHeight = 1,
                    GasPressure = 5,
                    PowerPercent = 100,
                    PowerValue = 1000,
                    PulseDutyFactorPercent = 100,
                    PulseFrequency = 5000,
                    DelayTime = 200,
                    ExtraPuffing = 500
                },
                PierceLevel2 = new PierceParameters
                {
                    StepTime = 1000,
                    NozzleHeight = 5,
                    GasPressure = 5,
                    PowerPercent = 100,
                    PowerValue = 1000,
                    PulseDutyFactorPercent = 100,
                    PulseFrequency = 100,
                    DelayTime = 200,
                    ExtraPuffing = 500
                },
                PierceLevel3 = new PierceParameters
                {
                    StepTime = 1000,
                    NozzleHeight = 15,
                    GasPressure = 5,
                    PowerPercent = 100,
                    PowerValue = 1000,
                    PulseDutyFactorPercent = 100,
                    PulseFrequency = 5000,
                    DelayTime = 200,
                    ExtraPuffing = 500
                },
                PwrCtrlPara = new PowerControlModel
                {
                    PowerCurve = new DataCurve
                    {
                        Points = new List<PointF> { new PointF(10, 50), new PointF(80, 100) }
                    },
                    FreqCurve = new DataCurve
                    {
                        Points = new List<PointF> { new PointF(10, 50), new PointF(80, 100) }
                    }
                }              
            };
        }

        public static LayerConfigModel GetDefaultLayerConfigModel()
        {
            var para = new LayerConfigModel
            {
                EmptyMoveSpeed = 200,
                EmptyMoveSpeedX = 200,
                EmptyMoveSpeedY = 200,
                EmptyMoveAcceleratedSpeed = 2000,
                EmptyMoveAcceleratedSpeedX = 2000,
                EmptyMoveAcceleratedSpeedY = 2000,
                CheckEdgeSpeed = 150,
                ProcessAcceleratedSpeed = 2000,
                CurveControlPrecision = 0.05,
                CornerControlPrecision = 0.1,
                DotBurstPulseFrequency = 5000,
                DotBurstPeakPower = 100,
                DefalutAirPressure = 4,
                FirstOpenAirDelay = 200,
                ExchangeAirDelay = 500,
                CoolingDotDelay = 1000,
                PauseBackspaceDistance = 2,
                FollowMaxHeight = 8,
                IsFrogStyleLift = true,
                UnLiftMaxEmptyMoveLength = 10,
                UnitTimeType = UnitTimeTypes.Millisecond,
                UnitSpeedType = UnitSpeedTypes.Millimeter_Second,
                UnitAcceleratedType = UnitAcceleratedTypes.MillimeterPerSecondSquared,
                UnitPressureType = UnitPressureTypes.BAR,
                LayerCooling = GetDefaultLayerCoolingModel(),
                LayerEvaporate = GetDefaultLayerEvaporateModel(),
                PointMoveCut = GetDefaultPointMoveModel()
            };

            for (int i = 0; i < 16; i++)
            {
                para.LayerCrafts[i + 1] = GetDefaultLayerCraftModel();
            }

            return para;
        }

        public static LayerCoolingModel GetDefaultLayerCoolingModel()
        {
            return new LayerCoolingModel
            {
                CoolingSpeed = 100,
                NozzleHeight = 1,
                LiftHeight = 10,
                GasPressure = 5
            };
        }

        public static LayerEvaporateModel GetDefaultLayerEvaporateModel()
        {
            return new LayerEvaporateModel
            {
                CutSpeed = 200,
                LiftHeight = 0,
                NozzleHeight = 15,
                GasPressure = 5,
                PowerPercent = 30,
                PulseFrequency = 1000,
                PulseDutyFactorPercent = 30,
                LaserOpenDelay = 200,
                PwrCtrlPara = new PowerControlModel
                {
                    PowerCurve = new DataCurve
                    {
                        Points = new List<PointF> { new PointF(10, 50), new PointF(80, 100) }
                    },
                    FreqCurve = new DataCurve
                    {
                        Points = new List<PointF> { new PointF(10, 50), new PointF(80, 100) }
                    }
                }
            };
        }

        public static PointMoveCutModel GetDefaultPointMoveModel()
        {
            return new PointMoveCutModel
            {
                LiftHeight = 10,
                NozzleHeight = 1,
                GasPressure = 5,
                PowerPercent = 100,
                PulseDutyFactorPercent = 100,
                PulseFrequency = 1000,
                DelayTime = 200,
                PierceLevel1 = new PierceParameters
                {
                    StepTime = 1000,
                    NozzleHeight = 1,
                    GasPressure = 5,
                    PowerPercent = 100,
                    PowerValue = 1000,
                    PulseDutyFactorPercent = 100,
                    PulseFrequency = 5000,
                    DelayTime = 200,
                    ExtraPuffing = 500
                },
                PierceLevel2 = new PierceParameters
                {
                    StepTime = 1000,
                    NozzleHeight = 5,
                    GasPressure = 5,
                    PowerPercent = 100,
                    PowerValue = 1000,
                    PulseDutyFactorPercent = 100,
                    PulseFrequency = 100,
                    DelayTime = 200,
                    ExtraPuffing = 500
                },
                PierceLevel3 = new PierceParameters
                {
                    StepTime = 1000,
                    NozzleHeight = 15,
                    GasPressure = 5,
                    PowerPercent = 100,
                    PowerValue = 1000,
                    PulseDutyFactorPercent = 100,
                    PulseFrequency = 5000,
                    DelayTime = 200,
                    ExtraPuffing = 500
                }
            };
        }
    }
}
