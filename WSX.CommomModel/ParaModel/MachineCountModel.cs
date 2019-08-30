using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 加工计数管理
    /// </summary>
    [Serializable]
    public class MachineCountModel
    {
        /// <summary>
        /// 计划加工总数
        /// </summary>
        public int PlanTotalCount { get; set; }
        /// <summary>
        /// 已完成件数
        /// </summary>
        public int FinishedCount { get; set; }
        /// <summary>
        /// 加工结束后动作
        /// </summary>
        public MachineFinishedTypes MachineFinishedType { get; set; }
        /// <summary>
        /// 自动暂停
        /// </summary>
        public bool IsAutoSuspend { get; set; }
        /// <summary>
        /// 在一下时间后暂停,时间单位-秒
        /// </summary>
        public long TotalSecond { get; set; }
    }
    /// <summary>
    /// 加工结束后
    /// </summary>
    [Serializable]
    public enum MachineFinishedTypes
    {
        /// <summary>
        /// 不做任何动作
        /// </summary>
        None,
        /// <summary>
        /// 弹出对话框提示
        /// </summary>
        ShowTips,
        /// <summary>
        /// 禁止继续加工
        /// </summary>
        BanMachine
    }

}
