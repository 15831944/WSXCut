using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.DrawModel
{
    public interface IDrawLite
    {
        void Draw(ICanvas canvas,float scale);
    }
}
