using System;
using System.Drawing;

namespace WSX.DrawService.Utils
{
    public class GDI
    {
        private IntPtr hdc;
        private Graphics graphics;

        public void BeginGDI(Graphics graphics)
        {
            this.graphics = graphics;
            this.hdc = this.graphics.GetHdc();
        }

        public void EndGDI()
        {
            this.graphics.ReleaseHdc();
        }

        public IntPtr CreatePEN(PenStyles penStyles,int width,int color)
        {
            return CreatePen(penStyles, width, color);
        }

        public bool DeleteOBJECT(IntPtr obj)
        {
            return DeleteObject(obj);
        }

        public IntPtr SelectObject(IntPtr hgdiobj)
        {
            return SelectObject(this.hdc, hgdiobj);
        }

        public void MoveTo(int x,int y)
        {
            MoveToEx(this.hdc, x, y,0);
        }

        public void LineTo(int x,int y)
        {
            LineTo(this.hdc, x, y);
        }

        public int SetROP2(DrawingMode drawingMode)
        {
            return SetROP2(this.hdc, drawingMode);
        }

        public void SetPixel(int x,int y,int color)
        {
            SetPixelV(this.hdc, x, y, color & 0x00ffffff);
        }

        public static int RGB(int red,int green,int blue)
        {
            return (red | (green << 8) | (blue << 16));
        }


        #region c++ wrapper

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern void SetPixelV(IntPtr hdc, int x, int y, int color);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern int SetROP2(IntPtr hdc, DrawingMode fnDrawMode);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern bool MoveToEx(IntPtr hdc, int X, int Y, int oldp);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern IntPtr CreatePen(PenStyles fnPenStyle, int nWidth, int crColor);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern void Rectangle(IntPtr hdc, int X1, int Y1, int X2, int Y2);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern IntPtr GetStockObject(int brStyle);

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hdc, int iBkMode);

        #endregion
    }
}
