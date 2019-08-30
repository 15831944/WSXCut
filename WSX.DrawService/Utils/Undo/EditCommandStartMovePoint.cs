using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandStartMovePoint:EditCommandBase
    {
        private UnitPoint offset;
        private IDrawObject drawObject;
        private ICanvas canvas;
        public EditCommandStartMovePoint(ICanvas canvas,UnitPoint offset,IDrawObject drawObject)
        {
            this.offset = offset;
            this.drawObject = drawObject;
            this.canvas = canvas;
        }

        public override bool DoUndo(IModel dataModel)
        {
            if(this.drawObject!=null&&this.canvas!=null&&this.offset != UnitPoint.Empty)
            {
                UnitPoint startMovePoint = new UnitPoint(this.drawObject.StartMovePoint.X + offset.X, this.drawObject.StartMovePoint.Y + offset.Y);
                dataModel.SetStartMovePoint(canvas, startMovePoint, drawObject);
            }
            return true;
        }

        public override bool DoRedo(IModel dataModel)
        {
            if (this.drawObject != null && this.canvas != null && this.offset != UnitPoint.Empty)
            {
                UnitPoint startMovePoint = new UnitPoint(this.drawObject.StartMovePoint.X - offset.X, this.drawObject.StartMovePoint.Y - offset.Y);
                dataModel.SetStartMovePoint(canvas, startMovePoint, drawObject);
            }
            return true;
        }
    }
}
