using System;
using System.Collections.Generic;

namespace WSX.CommomModel.DrawModel
{
    [Serializable]
    public class GroupParam
    {
        /// <summary>
        /// 群组编号集合【第一个数存一个非零值代表是一个群组的第一个图形，之后依次存放群组编号】
        /// </summary>
        public List<int> GroupSN { get; set; } 
        /// <summary>
        /// 图形排序编号
        /// </summary>
        public int FigureSN { get; set; }
        /// <summary>
        /// 图形显示编号
        /// </summary>
        public int ShowSN { get; set; }
    }
}
