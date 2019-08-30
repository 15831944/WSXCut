using System;

namespace WSX.CommomModel.ParaModel
{
    /// <summary>
    /// 图形尺寸设置
    /// </summary>
    [Serializable]
    public class FigureSizeSetModel
    {
        public double Height { get; set; }
        public double Width { get; set; }
        //public bool LockHWRatio { get; set; }
        public double NHeight { get; set; }
        public double NWidth { get; set; }

        [Serializable]
        public enum ScaleCenter
        {
            LeftTop,
            Top,
            RightTop,
            Left,
            Middle,
            Right,
            LeftBottom,
            Bottom,
            RightBottom,
        }

        public ScaleCenter ScaleCenterPoint { get; set; }

    }
}
