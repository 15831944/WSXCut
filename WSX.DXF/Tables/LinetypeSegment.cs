using System;

namespace WSX.DXF.Tables
{
    /// <summary>
    /// Base class for the three kinds of linetype segments simple, text, and shape.
    /// </summary>
    public abstract class LinetypeSegment :ICloneable
    {
        #region private fields

        private readonly LinetypeSegmentType type;
        private double length ;

        #endregion

        #region constructors

        protected LinetypeSegment(LinetypeSegmentType type, double length)
        {
            this.type = type;
            this.length = length;
        }

        #endregion

        #region public properties

        public LinetypeSegmentType Type
        {
            get { return this.type; }
        }

        public double Length
        {
            get { return this.length; }
            set { this.length = value; }
        }

        #endregion

        #region implements ICloneable

        public abstract object Clone();

        #endregion      
    }
}
