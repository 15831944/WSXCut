using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawTool;
using WSX.DrawService.Utils;

namespace WSX.DrawService.Layers
{
    public class GridLayer : ICanvasLayer
    {
        public enum GridStyle { Dots, Lines }

        public SizeF spacing = new SizeF(1f, 1f);//12''

        private RectangleF outline;
        
        public SizeF Spacing
        {
            get
            {
                return this.spacing;
            }
            set
            {
                this.spacing = value;
            }
        }


        public int MinSize { get; set; } = 15;

        public GridStyle GridStyleP { get; set; } = GridStyle.Lines;

        public Color Color { get; set; } = Color.FromArgb(50, Color.Gray);

        public void Copy(GridLayer gridLayer)
        {
            this.Locked = gridLayer.Locked;
            this.spacing = gridLayer.spacing;
            this.MinSize = gridLayer.MinSize;
            this.GridStyleP = gridLayer.GridStyleP;
            this.Color = gridLayer.Color;
        }

        #region ICanvasLayer
        public string LayerName
        {
            get
            {
                return "GridLayer";
            }
        }

        public List<IDrawObject> Objects
        {
            get
            {
                return null;
            }
        }
        public bool Locked { get; set; } = false;

        public bool Visible
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 绘制网格
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="unitRectangle"></param>
        public void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            if (this.Locked) return;
            float gridX = this.Spacing.Width;
            float gridY = this.Spacing.Height;
            float gridScreenSizeX = canvas.ToScreen(gridX);
            float gridScreenSizeY = canvas.ToScreen(gridY);
            List<float> scales = new List<float> { 0.1f, 0.2f, 0.5f, 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000, 20000, 50000, 100000, 200000 };
            float temp = (float)(1 / gridScreenSizeX * 100);
            try
            {
                scales.Reverse();
                float gridSize = scales.First(x => x <= temp);
                gridX = gridSize;
                gridY = gridSize;
            }
            catch
            {
                return;
            }

            PointF leftPoint = unitRectangle.Location;
            PointF rightPoint = ScreenUtils.RightPoint(canvas, unitRectangle);
            float left = (float)Math.Round(leftPoint.X / gridX) * gridX;
            float top = unitRectangle.Height + unitRectangle.Y;
            float right = rightPoint.X;
            float bottom = (float)Math.Round(leftPoint.Y / gridY) * gridY;
            if(this.GridStyleP==GridStyle.Dots)
            {
                GDI gdi = new GDI();
                gdi.BeginGDI(canvas.Graphics);
                for (float x = left; x <=right; x+=gridX)
                {
                    for (float y = bottom; y <=top; y+=gridY)
                    {
                        PointF pointF = canvas.ToScreen(new UnitPoint(x, y));
                        gdi.SetPixel((int)pointF.X, (int)pointF.Y, Color.ToArgb());
                    }
                }
                gdi.EndGDI();
            }
            else if(this.GridStyleP==GridStyle.Lines)
            {
                Pen pen = new Pen(this.Color);
                GraphicsPath graphicsPath = new GraphicsPath();
                //画垂直线
                while(left<right)
                {
                    PointF p1 = canvas.ToScreen(new UnitPoint(left, leftPoint.Y));
                    PointF p2 = canvas.ToScreen(new UnitPoint(left, rightPoint.Y));
                    graphicsPath.AddLine(p1, p2);
                    graphicsPath.CloseFigure();
                    left += gridX;
                }
                //画水平线
                while (bottom < top)
                {
                    PointF p1 = canvas.ToScreen(new UnitPoint(leftPoint.X, bottom));
                    PointF p2 = canvas.ToScreen(new UnitPoint(rightPoint.X, bottom));
                    graphicsPath.AddLine(p1, p2);
                    graphicsPath.CloseFigure();
                    bottom += gridY;
                }
                canvas.Graphics.DrawPath(pen, graphicsPath);

                #region Draw Outline
                //double x = this.outlineWidth / 2.0;
                //double y = this.outlineHeight / 2.0;
                //PointF point1 = canvas.ToScreen(new UnitPoint(-x, -y));
                //PointF point2 = canvas.ToScreen(new UnitPoint(-x, y));
                //PointF point3 = canvas.ToScreen(new UnitPoint(x, y));
                //PointF point4 = canvas.ToScreen(new UnitPoint(x, -y));
                //PointF point5 = canvas.ToScreen(new UnitPoint(-x, -y));
                //canvas.Graphics.DrawLines(Pens.DimGray, new PointF[] { point1, point2, point3, point4, point5 });

                if (this.outline != null)
                {
                    float x = this.outline.Location.X;
                    float y = this.outline.Location.Y;
                    float width = this.outline.Width;
                    float height = this.outline.Height;
                    int offset = 4;

                    var point1 = canvas.ToScreen(new UnitPoint(x, y)).ToPoint();
                    var point2 = canvas.ToScreen(new UnitPoint(x, y + height)).ToPoint();
                    var point3 = canvas.ToScreen(new UnitPoint(x + width, y + height)).ToPoint();
                    var point4 = canvas.ToScreen(new UnitPoint(x + width, y)).ToPoint();
                    var point5 = canvas.ToScreen(new UnitPoint(-x, y)).ToPoint();
                    var point6 = new Point(point1.X + offset, point1.Y);
                    var point7 = new Point(point3.X, point3.Y + offset);

                    canvas.Graphics.FillRectangle(Brushes.DimGray, point6.X, point6.Y, Math.Abs(point3.X - point2.X), offset);
                    canvas.Graphics.FillRectangle(Brushes.DimGray, point7.X, point7.Y, offset, Math.Abs(point3.Y - point4.Y));
                    canvas.Graphics.DrawLines(Pens.LightGray, new Point[] { point1, point2, point3, point4, point5 });
                }
               
                #endregion
            }
        }

        public void UpdateOutline(RectangleF rect)
        {
            this.outline = rect;
        }
       
        public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, List<IDrawObject> otherObject)
        {
            if(this.Locked)
            {
                return null;
            }
            UnitPoint snapPoint = new UnitPoint();
            UnitPoint mousePoint = point;
            float gridX = this.Spacing.Width;
            float gridY = this.Spacing.Height;
            snapPoint.X=(float)(Math.Round(mousePoint.X/gridX))*gridX;
            snapPoint.Y = (float)(Math.Round(mousePoint.Y / gridY)) * gridY;
            double threshold = canvas.ToUnit(6);
            if(snapPoint.X<point.X-threshold||snapPoint.X>point.X+threshold)
            {
                return null;
            }
            if(snapPoint.Y<point.Y-threshold||snapPoint.Y>point.X+threshold)
            {
                return null;
            }
            return new GridSnapPoint(canvas, snapPoint);
        }

#endregion

    }
}
