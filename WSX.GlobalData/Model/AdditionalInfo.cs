using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.GlobalData.Model
{
    /// <summary>
    /// 显示图形附加信息
    /// </summary>
    [Serializable]
    public class AdditionalInfo
    {
        private static AdditionalInfo instance;
        public AdditionalInfo() { }
        public static AdditionalInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AdditionalInfo();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        /// <summary>
        /// 是否显示图形外框
        /// </summary>
        public bool IsShowBoundRect { get; set; }

        /// <summary>
        /// 红色显示不封闭图形
        /// </summary>
        public bool IsShowNotClosedFigureInRed { get; set; }

        /// <summary>
        /// 显示序号
        /// </summary>
        public bool IsShowFigureSN { get; set; }

        /// <summary>
        /// 显示路径起点
        /// </summary>
        public bool IsShowStartMovePoint { get; set; }

        /// <summary>
        /// 显示加工路径
        /// </summary>
        public bool IsShowMachinePath { get; set; }

        /// <summary>
        /// 显示微连标记
        /// </summary>
        public bool IsShowMicroConnectFlag { get; set; }

        /// <summary>
        /// 显示空移路径
        /// </summary>
        public bool IsShowVacantPath { get; set; }
    }
}
