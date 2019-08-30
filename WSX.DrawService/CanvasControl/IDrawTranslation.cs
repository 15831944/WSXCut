using WSX.CommomModel.DrawModel;
using WSX.CommomModel.DrawModel.Compensation;
using WSX.CommomModel.DrawModel.RingCut;
using WSX.CommomModel.ParaModel;

namespace WSX.DrawService.CanvasControl
{
    public interface IDrawTranslation
    {
        /// <summary>
        /// 图像镜像
        /// </summary>
        /// <param name="A">对称轴X系数</param>
        /// <param name="B">对称轴Y系数</param>
        /// <param name="C">对称轴常数</param>
        void Mirroring(double A, double B, double C);

        /// <summary>
        /// 图形反向
        /// </summary>
        void ReverseDirection();

        void OverCutting(float pos,bool roundCut);

        UnitPoint MaxValue { get; }

        UnitPoint MinValue { get; }

        void DoSizeChange(double x, double y, double width, double height);
        void DoRotate(UnitPoint rotateCenter, double rotateAngle, bool isClockwise);
        void DoSetLeadwire(LineInOutParamsModel leadwireModel);
        void DoCompensation(CompensationModel param);
        void DoCornerRing(CornerRingModel param);
    }
}
