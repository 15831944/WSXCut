using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 切碎
    /// </summary>
    [Serializable]
    public class CutUpModel
    {
        /// <summary>
        /// 切碎方式
        /// </summary>
        public CutUpTypes CutUpType { get; set; }
        /// <summary>
        /// 切碎间距
        /// </summary>
        public double Spacing { get; set; }
        /// <summary>
        /// 经线数量
        /// </summary>
        public int LongitudeCount { get; set; }
        /// <summary>
        /// 纬线数量
        /// </summary>
        public int LatitudeCount { get; set; }
        /// <summary>
        /// 区分内外膜
        /// </summary>
        public bool IsDifferInsideOutside { get; set; }
    }
    /// <summary>
    /// 切碎方式
    /// </summary>
    [Serializable]
    public enum CutUpTypes
    {
        /// <summary>
        /// 线间距
        /// </summary>
        LineSpacing,
        /// <summary>
        /// 线数量
        /// </summary>
        LineCount
    }
}
