using System.Collections.Generic;
using System.Drawing;

namespace WSX.Engine.Models
{
    public enum CoordinateTypes
    {
        Relative,
        Absolute,
    }

    public class Coordinate
    {
        private const int COORDINATE_COUNT = 11;
        private const int MARK_COUNT = 6;

        //public PointF RefZero { get; set; }
        public CoordinateTypes DisCoordinate { get; set; }
        public List<PointF> RefZeroSeries { get; set; }
        /// <summary>
        ///   0: floating coordinate
        /// >=1: working coordinate
        /// </summary>
        public int RefZeroIndex { get; set; }
        public List<PointF> MarkSeries { get; set; }
        public int MarkIndex { get; set; }

        public Coordinate()
        {
            this.RefZeroSeries = new List<PointF>();
            for (int i = 0; i < COORDINATE_COUNT; i++)
            {
                this.RefZeroSeries.Add(new PointF(0, 0));
            }

            this.MarkSeries = new List<PointF>();
            for (int i = 0; i < MARK_COUNT; i++)
            {
                this.MarkSeries.Add(new PointF(0, 0));
            }
        }
    }
}
