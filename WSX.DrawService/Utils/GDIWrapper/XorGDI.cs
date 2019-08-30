using System;
using System.Drawing;

namespace WSX.DrawService.Utils
{
    public static class XorGDI
    {
        private static int NullBrush = 5;
        private static int Transparent = 1;

        public static void DrawLine(PenStyles penStyles,int penWidth,Color color,Graphics graphics,int x1,int y1,int x2,int y2)
        {
            IntPtr hdc = graphics.GetHdc();
            IntPtr pen = GDI.CreatePen(penStyles, penWidth, GDI.RGB(color.R, color.G, color.B));
            GDI.SetROP2(hdc, DrawingMode.R2_XORPEN);
            GDI.SetBkMode(hdc, Transparent);
            GDI.SetROP2(hdc, DrawingMode.R2_XORPEN);
            IntPtr oldPen = GDI.SelectObject(hdc, pen);
            GDI.MoveToEx(hdc, x1, y1, 0);
            GDI.LineTo(hdc, x2, y2);
            GDI.SelectObject(hdc, oldPen);
            GDI.DeleteObject(pen);
            graphics.Dispose();
        }

        public static void DrawRectangle(Graphics graphics,PenStyles penStyles,int penWidth,Color color,int x1,int y1,int x2,int y2)
        {
            IntPtr hdc = graphics.GetHdc();
            IntPtr pen = GDI.CreatePen(penStyles, penWidth, GDI.RGB(color.R, color.G, color.B));
            GDI.SetROP2(hdc, DrawingMode.R2_XORPEN);
            GDI.SetBkMode(hdc, Transparent);
            GDI.SetROP2(hdc, DrawingMode.R2_XORPEN);
            IntPtr oldPen = GDI.SelectObject(hdc, pen);
            IntPtr oldBrush = GDI.SelectObject(hdc, GDI.GetStockObject(NullBrush));
            GDI.Rectangle(hdc, x1, y1, x2, y2);
            GDI.SelectObject(hdc, oldPen);
            GDI.DeleteObject(pen);
            graphics.ReleaseHdc();
        }

        public static void DrawRectangle(Graphics graphics,PenStyles penStyles,int penWidth,Color color,PointF topLeft,PointF bottomRight)
        {
            DrawRectangle(graphics, penStyles, penWidth, color, (int)topLeft.X,(int)topLeft.Y, (int)bottomRight.X, (int)bottomRight.Y);
        }
    }
}
