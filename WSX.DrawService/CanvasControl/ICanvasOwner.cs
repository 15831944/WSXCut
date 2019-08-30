using WSX.CommomModel.DrawModel;

namespace WSX.DrawService.CanvasControl
{
    public interface ICanvasOwner
    {
        /// <summary>
        /// 设置位置信息
        /// </summary>
        /// <param name="unitPoint"></param>
        void SetPositionInfo(UnitPoint unitPoint);

        /// <summary>
        ///设置捕获信息
        /// </summary>
        /// <param name="snap"></param>
        void SetSnapInfo(ISnapPoint snap);
    }
}
