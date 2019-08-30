using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.Utils;

namespace WSX.DrawService.Wrapper
{
    public class PositionChangedEventArgs : EventArgs
    {
        public UnitPoint CurrentPoint { get; set; }
    }
}
