using System.Drawing;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Layers;

namespace WSX.DrawService.DrawTool
{
    public abstract class DrawObjectBase
    {
        public float Width { get; set; }
        public Color Color
        {
            get
            {
                return LayerColors.DicLayerColors[this.LayerId]; 
            }
        }
        /// <summary>
        /// 图形对象所在图层ID
        /// </summary>
        public int LayerId { get; set; } = (int)GlobalData.Model.GlobalModel.CurrentLayerId;
        public abstract void InitializeFromModel(UnitPoint unitPoint, ISnapPoint snapPoint);
        public virtual void Copy(DrawObjectBase drawObjectBase)
        {
            this.LayerId = drawObjectBase.LayerId;
        }
        /// <summary>
        /// 是否为板材
        /// </summary>
        public bool IsBoard { get; set; }=false;
    }
}
