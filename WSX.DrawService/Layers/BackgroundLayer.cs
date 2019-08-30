using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.Utils;

namespace WSX.DrawService.Layers
{
    /// <summary>
    /// 背景图层
    /// </summary>
    public class BackgroundLayer : ICanvasLayer
    {
        private Font font = new Font("Arial Black", 25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        private Font fontAxis=new Font("Arial Black", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
        private SolidBrush solidBrush = new SolidBrush(Color.FromArgb(50, 200, 200, 200));
        private SolidBrush solidBrushAxis = new SolidBrush(Color.White);
        private SolidBrush backgroundBrush;
        private Color color = Color.Black;
        
        public Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
                this.backgroundBrush = new SolidBrush(this.color);
            }
        }

        public BackgroundLayer()
        {
            this.backgroundBrush = new SolidBrush(this.color);
        }

        #region ICanvasLayer
        public string LayerName
        {
            get
            {
                return "BackgroundLayer";
            }
        }

        public List<IDrawObject> Objects
        {
            get
            {
                return null;
            }
        }

        public bool Locked { get; set; }

        public bool Visible { get; set; }
        
        /// <summary>
        /// 绘制背景
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="unitRectangle"></param>
        public void Draw(ICanvas canvas, RectangleF unitRectangle)
        {
            RectangleF rectangleF = ScreenUtils.ToScreenNormalized(canvas, unitRectangle);
            canvas.Graphics.FillRectangle(this.backgroundBrush, rectangleF);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            PointF centerPoint = new PointF(rectangleF.Width / 2, rectangleF.Height / 2);
            canvas.Graphics.TranslateTransform(centerPoint.X, centerPoint.Y);
            canvas.Graphics.DrawRectangle(Pens.White, new Rectangle(new Point((int)(-centerPoint.X+12), (int)(centerPoint.Y-30)), new Size(10, 10)));
            canvas.Graphics.DrawLine(Pens.White, new Point((int)((int)(-centerPoint.X + 17)), (int)(centerPoint.Y - 30)), new Point((int)((-centerPoint.X + 17)), (int)(centerPoint.Y - 65)));
            canvas.Graphics.DrawLine(Pens.White, new Point((int)(-centerPoint.X + 22), (int)(centerPoint.Y -25)), new Point((int)(-centerPoint.X + 57), (int)(centerPoint.Y - 25)));
            canvas.Graphics.DrawString("Y",this.fontAxis,this.solidBrushAxis,-centerPoint.X+5,centerPoint.Y-60);
            canvas.Graphics.DrawString("X", this.fontAxis, this.solidBrushAxis, -centerPoint.X+40, centerPoint.Y - 25);
            canvas.Graphics.RotateTransform(-15);
            canvas.Graphics.DrawString("WSX (2019)", this.font, this.solidBrush, 0, 0, stringFormat);
            canvas.Graphics.ResetTransform();
        }

        public ISnapPoint SnapPoint(ICanvas canvas, UnitPoint point, List<IDrawObject> otherObject)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

    }
}
