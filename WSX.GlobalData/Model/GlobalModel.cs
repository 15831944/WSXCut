using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.GlobalData.Model
{
    public class GlobalModel
    {
        public static string ConfigFileName { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "Configs\\GlobalConfigs.json";
        /// <summary>
        /// 当前图层ID
        /// </summary>
        public static LayerId CurrentLayerId { get; set; }

        /// <summary>
        /// 图形对象总数
        /// </summary>
        public static int TotalDrawObjectCount { get; set; }
        /// <summary>
        /// 群组对象总数
        /// </summary>
        public static int TotalGroupCount { get; set; }
        /// <summary>
        /// 阈值宽度
        /// </summary>
        public static float ThresholdWidth { get; set; } = 10;
        /// <summary>
        /// 系统所有配置参数
        /// </summary>
        public static GlobalParameters Params { get; set; } = new GlobalParameters();
    }

    public enum LayerId
    {
        One =1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Eleven,
        Twelve,
        Thirteen,
        Fourteen,
        Fifteen,
        Sixteen,
        White,
    }
}
