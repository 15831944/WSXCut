using System.Collections.Generic;
using System.Linq;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class EditCommandAdd:EditCommandBase
    {
        private List<IDrawObject> drawObjects = null;
        private IDrawObject drawObject;

        public EditCommandAdd(IDrawObject drawObject)
        {
            this.drawObject = drawObject;
        }

        public EditCommandAdd(List<IDrawObject> drawObjects)
        {
            this.drawObjects = drawObjects;
        }

        /// <summary>
        /// 撤销操作
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public override bool DoUndo(IModel dataModel)
        {
            if(this.drawObject!=null)
            {
                dataModel.DeleteObjects(new List<IDrawObject> { this.drawObject });
            }
            if(this.drawObjects!=null)
            {
                dataModel.DeleteObjects(this.drawObjects);
            }
            return true;
        }

        /// <summary>
        /// 重做操作
        /// </summary>
        /// <param name="dataMode"></param>
        /// <returns></returns>
        public override bool DoRedo(IModel dataMode)
        {
            if(this.drawObject!=null)
            {
                //dataMode.AddObject(this.drawObject);
                dataMode.AddObjectOnRedoUndo(this.drawObject);
            }
            if(this.drawObjects!=null)
            {
                this.drawObjects = this.drawObjects.OrderBy(d=>d.GroupParam.FigureSN).ToList();
                foreach (IDrawObject drawObject in this.drawObjects)
                {
                    //dataMode.AddObject(drawObject);
                    dataMode.AddObjectOnRedoUndo(drawObject);
                }
            }
            return true;
        }


    }
}
