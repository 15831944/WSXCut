using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DrawService.DrawTool.Arcs;
using WSX.DrawService.DrawTool.CircleTool;
using WSX.DrawService.DrawTool.MultiSegmentLine;

namespace WSX.DrawService.Utils
{
    public class OverCutHelper
    {
        public static void SetArcOverCut(ArcBase arc)
        {
            float pos = arc.LeadOut.Pos;
            if (Math.Abs(arc.LeadOut.Pos) < 0.000001)//封口
            {

            }
            else if (pos < 0)//缺口,从圆弧尾部去掉pos长的距离
            {
                arc.AngleSweep -= (float)(Math.Abs(pos) / arc.SizeLength) * arc.AngleSweep;
                arc.Update();
            }
            else//过切/多圈
            {

            }

        } 

        public static void B(Circle circle)
        {

        }

        public static void C(MultiSegmentLineBase multiSegmentLine)
        {

        }
    }
}
