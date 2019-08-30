using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DrawService.DrawModel;

namespace WSX.DrawService.CanvasControl
{
    public interface IDrawData
    {
        //List<IDrawLite> GetOriginal();
        //List<IDrawLite> GetOriginalCompensation();
        List<IDrawLite> GetBasic();
        List<IDrawLite> GetCompensation();
        List<IDrawLite> GetCornerRing();
        List<IDrawLite> GetCoolingPoints();
        List<IDrawLite> GetLeadIn();
        List<IDrawLite> GetLeadOut();
    }
}
