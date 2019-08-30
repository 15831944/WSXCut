using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.DrawService.DrawTool.MultiSegmentLine
{
    public enum MultiSegementLineCurrentPoint
    {
        //矩形，多边形
        StartPoint,
        EndPoint,
        //星形
        MidPoint,

        Done
    }
}
