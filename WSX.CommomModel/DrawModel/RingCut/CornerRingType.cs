using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.DrawModel.RingCut
{
    public enum CornerRingType
    {
        /// <summary>
        /// 自动，阳切外部环切，阴切内部环切
        /// </summary>
        Auto,
        /// <summary>
        /// 内部环切
        /// </summary>
        Inner,
        /// <summary>
        /// 外部环切
        /// </summary>
        Outer,
        /// <summary>
        /// 内外都环切
        /// </summary>
        All
    }
}
