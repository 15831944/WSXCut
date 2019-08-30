using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WSX.DrawService.Wrapper
{
    public class ScaleChangedEventArgs: EventArgs
    {
        public PointF StartPoint { get; set; }
        public PointF EndPoint { get; set; }
    }
}
