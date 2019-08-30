namespace WSX.DrawService
{
    public class DrawCommand
    {
        public enum DrawDataFormat
        {
            DoubleFormat,
            PointFormat,
            StringFormat,
            FloatFormat,

            /// <summary>
            /// point   or float
            /// </summary>
            PointOrFloatFormat,

            IntFormat,
            PointOrCharFormat
        }
    }
}