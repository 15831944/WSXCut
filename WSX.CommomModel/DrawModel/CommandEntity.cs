using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.DrawModel
{
    public class CommandEntity
    {
        /// <summary>
        /// 命令
        /// </summary>
        public string CanvasCommand { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public List<Patameter> Parameters { get; set; }
        /// <summary>
        /// 命令说明
        /// </summary>
        public string CommandExplain { get; set; }
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsComplete { get; set; } = false;
        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; } = false;
        /// <summary>
        /// 是否支持输入参数值，默认支持
        /// </summary>
        public bool IsWritePatameter { get; set; } = true;
    }

    public class Patameter
    {
        /// <summary>
        /// 参数ID
        /// </summary>
        public int ParameterId { get; set; }
        /// <summary>
        /// 参数说明
        /// </summary>
        public string Explain { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>

        public string ParameterValue { get; set; }

        /// <summary>
        /// 参数值数据格式
        /// </summary>
        public string ParameterValueDataFormat { get; set; }
        /// <summary>
        /// 默认参数命令
        /// </summary>
        public List<string> DefaultParameterCommand { get; set; }
        public bool IsShowValue { get; set; } = true;        
        /// <summary>
        /// 子命令
        /// </summary>
        public List<Patameter> SubPatameter { get; set; }
    }
}
