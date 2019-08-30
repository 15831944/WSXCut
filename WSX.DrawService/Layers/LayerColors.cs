using System.Collections.Generic;
using System.Drawing;
using WSX.GlobalData.Model;

namespace WSX.DrawService.Layers
{
    public class LayerColors
    {
        public static readonly Dictionary<int, Color> DicLayerColors = new Dictionary<int, Color>();
        static LayerColors()
        {
            DicLayerColors.Add((int)LayerId.One, Color.FromArgb(77, 255, 77));
            DicLayerColors.Add((int)LayerId.Two, Color.FromArgb(255, 166, 211));
            DicLayerColors.Add((int)LayerId.Three, Color.FromArgb(255, 255, 121));
            DicLayerColors.Add((int)LayerId.Four, Color.FromArgb(255, 166, 166));
            DicLayerColors.Add((int)LayerId.Five, Color.FromArgb(166, 77, 255));
            DicLayerColors.Add((int)LayerId.Six, Color.FromArgb(77, 166, 166));
            DicLayerColors.Add((int)LayerId.Seven, Color.FromArgb(255, 166, 121));
            DicLayerColors.Add((int)LayerId.Eight, Color.FromArgb(77, 166, 77));
            DicLayerColors.Add((int)LayerId.Nine, Color.FromArgb(255, 77, 166));
            DicLayerColors.Add((int)LayerId.Ten, Color.FromArgb(77, 166, 255));
            DicLayerColors.Add((int)LayerId.Eleven, Color.FromArgb(77, 255, 166));
            DicLayerColors.Add((int)LayerId.Twelve, Color.FromArgb(255, 77, 255));
            DicLayerColors.Add((int)LayerId.Thirteen, Color.FromArgb(77, 77, 255));
            DicLayerColors.Add((int)LayerId.Fourteen, Color.FromArgb(166, 166, 255));
            DicLayerColors.Add((int)LayerId.Fifteen, Color.FromArgb(0, 255, 255));
            DicLayerColors.Add((int)LayerId.Sixteen, Color.FromArgb(255, 255, 0));
            DicLayerColors.Add((int)LayerId.White, Color.FromArgb(255, 255, 255));   
        }
    }
}
