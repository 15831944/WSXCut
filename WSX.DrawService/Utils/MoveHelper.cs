using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class MoveHelper
    {
        private List<IDrawObject> originals = new List<IDrawObject>();
        private UCCanvas ucCanvas;

        public UnitPoint OriginalPoint { get; private set; } = UnitPoint.Empty;
        public UnitPoint LastPoint { get; private set; } = UnitPoint.Empty;
        public List<IDrawObject> Copies { get; } = new List<IDrawObject>();
        public MoveHelper(UCCanvas ucCanvas)
        {
            this.ucCanvas = ucCanvas;
        }
        public bool IsEmpty
        {
            get
            {
                return this.Copies.Count == 0;
            }
        }
        public bool HandleMouseMoveForMove(UnitPoint mousePoint)
        {
            if (this.originals.Count == 0)
            {
                return false;
            }
            double x = mousePoint.X - this.LastPoint.X;
            double y = mousePoint.Y - this.LastPoint.Y;
            UnitPoint offset = new UnitPoint(x, y);
            this.LastPoint = mousePoint;
            foreach (IDrawObject obj in this.Copies)
            {
                obj.Move(offset);
            }
            this.ucCanvas.DoInvalidate(true);
            return true;
        }
        public void HandleCancelMove()
        {
            //foreach (IDrawObject obj in this.originals)
            //{
            //    this.ucCanvas.Model.AddSelectedObject(obj);
            //}
            //this.originals.Clear();
            //this.Copies.Clear();
            //this.ucCanvas.DoInvalidate(true);

            if (this.originals != null && this.originals.Count > 0)
            {
                this.ucCanvas.Model.DeleteObjects(originals);
             }
            foreach (IDrawObject obj in this.Copies)
            {
                this.ucCanvas.Model.AddSelectedObject(obj);
                this.ucCanvas.Model.AddObjectOnRedoUndo(obj);
            }
            this.originals.Clear();
            this.Copies.Clear();
            this.ucCanvas.DoInvalidate(true);
        }
        public void HandleMouseDownForMove(UnitPoint mousePoint, ISnapPoint snappoint)
        {
            if (snappoint != null)
            {
                mousePoint = snappoint.SnapPoint;
            }
            if (this.originals.Count == 0) // first step of move
            {
                foreach (IDrawObject obj in this.ucCanvas.Model.DrawingLayer.SelectedObjects)
                {
                    this.originals.Add(obj);
                    this.Copies.Add(obj.Clone());
                }
                //this.ucCanvas.Model.ClearSelectedObjects();
                this.OriginalPoint = mousePoint;
                this.LastPoint = mousePoint;
            }
            else // move complete
            {
                double x = mousePoint.X - this.OriginalPoint.X;
                double y = mousePoint.Y - this.OriginalPoint.Y;
                UnitPoint offset = new UnitPoint(x, y);
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)//do copy
                {
                    this.ucCanvas.Model.CopyObjects(offset, this.originals);
                }
                else // do move
                {
                    this.ucCanvas.Model.MoveObjects(offset, this.originals);
                    foreach (IDrawObject obj in this.originals)
                    {
                        this.ucCanvas.Model.AddSelectedObject(obj);
                    }
                }
                this.originals.Clear();
                this.Copies.Clear();
            }
            this.ucCanvas.DoInvalidate(true);
        }
       
        public void DrawObjects(ICanvas canvas, RectangleF r)
        {
            if (this.Copies.Count > 0)
            {
                //canvas.Graphics.DrawLine(Pens.Wheat, canvas.ToScreen(this.OriginalPoint), canvas.ToScreen(LastPoint));
                foreach (IDrawObject obj in Copies)
                {
                    obj.Draw(canvas, r);
                }
            }
        }
    }
}
