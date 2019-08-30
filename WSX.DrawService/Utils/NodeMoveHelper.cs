using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class NodeMoveHelper
    {
        private List<INodePoint> nodePoints = new List<INodePoint>();
        private UnitPoint originalPoint = UnitPoint.Empty;
        private UnitPoint lastPoint = UnitPoint.Empty;
        private CanvasWrapper canvasWrapper;
        private List<IDrawObject> drawObjectsEdit = new List<IDrawObject>();

        public bool IsEmpty
        {
            get { return nodePoints.Count == 0; }
        }
        public NodeMoveHelper(CanvasWrapper canvas)
        {
            this.canvasWrapper = canvas;
        }
        public RectangleF HandleMouseMoveForNode(UnitPoint mouseunitpoint)
        {
            RectangleF r = RectangleF.Empty;
            if (nodePoints.Count == 0)
                return r;
            r = new RectangleF(originalPoint.Point, new Size(0, 0));
            r = RectangleF.Union(r, new RectangleF(mouseunitpoint.Point, new SizeF(0, 0)));
            if (lastPoint != UnitPoint.Empty)
                r = RectangleF.Union(r, new RectangleF(lastPoint.Point, new SizeF(0, 0)));

            lastPoint = mouseunitpoint;
            foreach (INodePoint p in nodePoints)
            {
                if (r == RectangleF.Empty)
                    r = p.GetClone().GetBoundingRectangle(BoundingRectangleType.NodeEditing);
                else
                    r = RectangleF.Union(r, p.GetClone().GetBoundingRectangle(BoundingRectangleType.NodeEditing));
                p.SetPosition(mouseunitpoint);
                //m_canvas.RepaintObject(p.GetClone());
            }
            return r;
        }
        public bool HandleMouseDown(UnitPoint mouseunitpoint, ref bool handled)
        {
            handled = false;
            if (nodePoints.Count == 0) // no nodes selected yet
            {
                if (canvasWrapper.DataModel.SelectedCount > 0)
                {
                    foreach (IDrawObject obj in canvasWrapper.DataModel.DrawingLayer.SelectedObjects)
                    {
                         if (obj.IsLocked == true) continue;
                        INodePoint p = obj.NodePoint(mouseunitpoint);
                        if (p != null)
                        {
                            nodePoints.Add(p);
                           this.drawObjectsEdit.Add(obj);
                        }
                    }
                }
                handled = nodePoints.Count > 0;
                if (handled)
                    originalPoint = mouseunitpoint;
                return handled;
            }
            // update selected nodes
            canvasWrapper.DataModel.MoveNodes(mouseunitpoint, nodePoints);
            nodePoints.Clear();
            this.drawObjectsEdit.Clear();
            handled = true;
            canvasWrapper.UCCanvas.DoInvalidate(true);
            return handled;
        }
        public void HandleCancelMove()
        {
            foreach (INodePoint nodePoint in nodePoints)
            {
                nodePoint.Cancel();
            }
            nodePoints.Clear();
        }
        public void DrawOriginalObjects(ICanvas canvas, RectangleF r)
        {
            foreach (INodePoint node in nodePoints)
                node.GetOriginal().Draw(canvas, r);
        }
        public void DrawObjects(ICanvas canvas, RectangleF r)
        {
            if (nodePoints.Count == 0)
                return;
            canvas.Graphics.DrawLine(Pens.Wheat, canvas.ToScreen(this.originalPoint), canvas.ToScreen(this.lastPoint));
            foreach (INodePoint node in nodePoints)
                node.GetClone().Draw(canvas, r);
        }
        public void OnKeyDown(ICanvas canvas, KeyEventArgs keyevent)
        {
            foreach (INodePoint nodepoint in nodePoints)
            {
                nodepoint.OnKeyDown(canvas, keyevent);
                if (keyevent.Handled)
                    return;

                IDrawObject drawobject = nodepoint.GetClone();
                if (drawobject == null)
                    continue;
                drawobject.OnKeyDown(canvas, keyevent);
            }
        }
    }
}
