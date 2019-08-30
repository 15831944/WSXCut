using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawModel;
using WSX.DrawService.Layers;
using WSX.DrawService.Utils;

namespace WSX.DrawService.DrawTool
{
    public  class DrawUtils
    {
        static DrawUtils()
        {
            customPens = CustomPens;
            customSelectedPens = CustomSelectedPens;
            dottedPens = DottedPens;
        }
        private static List<Pen> customPens = null;
        /// <summary>
        /// 图形对象未选中时颜色集合
        /// </summary>
        public static List<Pen> CustomPens
        {
            get
            {
                if (customPens == null)
                {
                    customPens = new List<Pen>();
                    customPens.Add(new Pen(Color.Red, 1));
                    for (int i = 1; i <= LayerColors.DicLayerColors.Count; i++)
                    {
                        customPens.Add(new Pen(LayerColors.DicLayerColors[i], 1));
                    }
                    //最先加工、最后加工两个画笔，虚线点距稍大
                    customPens[15].DashPattern = new float[2] { 8, 8 };
                    customPens[16].DashPattern = new float[2] { 8, 8 };
                }
                return customPens;
            }
        }

        private static List<Pen> customSelectedPens = null;
        public static List<Pen> CustomSelectedPens
        {
            get
            {
                if (customSelectedPens == null)
                {
                    customSelectedPens = new List<Pen>();
                    customSelectedPens.Add(new Pen(Color.Red, 1)
                    {
                        DashStyle = DashStyle.Custom,
                        DashPattern = new float[2] { 2, 3 },
                        EndCap = LineCap.ArrowAnchor,
                        CustomEndCap = new AdjustableArrowCap(5, 5)
                    });
                    for (int i = 1; i <= LayerColors.DicLayerColors.Count; i++)
                    {
                        customSelectedPens.Add(
                            new Pen(LayerColors.DicLayerColors[i], 1)
                            {
                                DashStyle = DashStyle.Custom,
                                DashPattern = new float[2] { 2, 3 },
                                EndCap = LineCap.ArrowAnchor,
                                CustomEndCap = new AdjustableArrowCap(5, 5)
                            }
                            );
                    }
                }
                return customSelectedPens;
            }
        }

        private static Pen leadLinePen = null;
        public static Pen LeadLinePen
        {
            get
            {
                if (leadLinePen == null)
                {
                    leadLinePen = new Pen(Color.White, 1);
                }
                return leadLinePen;
            }
        }
        private static List<Pen> cornerRingPens = null;
        public static List<Pen> CornerRingPens
        {
            get
            {
                if (cornerRingPens == null)
                {
                    cornerRingPens = new List<Pen>();
                    float percent = 0.45f;
                    cornerRingPens.Add(new Pen(ColorHelper.ChangeBrightness(Color.Red, percent), 1));
                    for (int i = 1; i <= LayerColors.DicLayerColors.Count; i++)
                    {
                        Color color = LayerColors.DicLayerColors[i];
                        cornerRingPens.Add(
                            new Pen(ColorHelper.ChangeBrightness(color, percent), 1)
                            {
                                DashStyle = DashStyle.Custom,
                                DashPattern = new float[2] { 2, 3 },
                                EndCap = LineCap.ArrowAnchor,
                                CustomEndCap = new AdjustableArrowCap(5, 5)
                            });
                    }
                }
                return cornerRingPens;
            }
        }
        private static Pen selectedLeadLinePen = null;
        public static Pen SelectedLeadLinePen
        {
            get
            {
                if (selectedLeadLinePen == null)
                {
                    selectedLeadLinePen = new Pen(Color.White, 1)
                    {
                        DashStyle = DashStyle.Custom,
                        DashPattern = new float[2] { 2, 3 },
                        EndCap = LineCap.ArrowAnchor,
                        CustomEndCap = new AdjustableArrowCap(5, 5)
                    };
                }
                return selectedLeadLinePen;
            }
        }

        private static Pen selectedPen = null;
        public static Pen SelectecedPen
        {
            get
            {
                if (selectedPen == null)
                {
                    selectedPen = new Pen(Color.Magenta, 1);
                    selectedPen.DashStyle = DashStyle.Dash;
                }
                return selectedPen;
            }
        }

        private static List<Pen> dottedPens = null;
        public static List<Pen> DottedPens
        {
            get
            {
                if (dottedPens == null)
                {
                    dottedPens = new List<Pen>();
                    float percent = 0.45f;
                    dottedPens.Add(new Pen(ColorHelper.ChangeBrightness(Color.Red, percent), 1));
                    for (int i = 1; i <= LayerColors.DicLayerColors.Count; i++)
                    {
                        Color color = Color.White;//LayerColors.DicLayerColors[i];
                        dottedPens.Add(
                            new Pen(ColorHelper.ChangeBrightness(color, percent), 1)
                            {
                                DashStyle = DashStyle.Custom,
                                DashPattern = new float[2] { 2, 3 },
                                EndCap = LineCap.ArrowAnchor,
                                CustomEndCap = new AdjustableArrowCap(5, 5)
                            });
                    }
                }
                return dottedPens;
            }
        }

        private static Pen selectedOverCuttingPen = null;
        public static Pen SelectecedOverCuttingPen
        {
            get
            {
                if (selectedOverCuttingPen == null)
                {
                    selectedOverCuttingPen = new Pen(Color.Magenta, 3);
                    selectedOverCuttingPen.DashStyle = DashStyle.Dash;
                }
                return selectedOverCuttingPen;
            }
        }
        private static Pen selectedYellowPen = null;
        public static Pen SelectecedYeelowPen
        {
            get
            {
                if (selectedYellowPen == null)
                {
                    selectedYellowPen = new Pen(Color.Yellow, 3);
                    selectedYellowPen.DashStyle = DashStyle.Solid;
                }
                return selectedYellowPen;
            }
        }
        private static Pen selectedBluePen = null;
        public static Pen SelectecedBluePen
        {
            get
            {
                if (selectedBluePen == null)
                {
                    selectedBluePen = new Pen(Color.Blue, 3);
                    selectedBluePen.DashStyle = DashStyle.Solid;
                }
                return selectedBluePen;
            }
        }

        private static Brush selectedBrush = null;
        public static Brush SelectecedBrush
        {
            get
            {
                if (selectedBrush == null)
                {
                    selectedBrush = new SolidBrush(Color.Magenta);
                }
                return selectedBrush;
            }
        }

        private static Pen vacantPen = null;
        public static Pen VancantPen
        {
            get
            {
                if (vacantPen == null)
                {
                    vacantPen = new Pen(Color.Yellow, 1)
                    {
                        DashStyle = DashStyle.Custom,
                        DashPattern = new float[2] { 3, 4 },
                        EndCap = LineCap.ArrowAnchor,
                        CustomEndCap = new AdjustableArrowCap(5, 5)
                    };
                }
                return vacantPen;
            }
        }
        public static void DrawNode(ICanvas canvas, UnitPoint unitPoint)
        {
            RectangleF rectangleF = new RectangleF(canvas.ToScreen(unitPoint), new SizeF(0, 0));
            rectangleF.Inflate(4, 4);
            if (rectangleF.Right < 0 || rectangleF.Left > canvas.ClientRectangle.Width) return;
            if (rectangleF.Top < 0 || rectangleF.Bottom > canvas.ClientRectangle.Height) return;
            canvas.Graphics.FillRectangle(Brushes.Yellow, rectangleF);
            rectangleF.Inflate(1, 1);
            canvas.Graphics.DrawRectangle(Pens.Black, ScreenUtils.ConvertRectangle(rectangleF));
        }

        public static void DrawTriangleNode(ICanvas canvas, UnitPoint unitPoint)
        {
            PointF screenPoint = canvas.ToScreen(unitPoint);
            float size = 5;
            PointF[] pointFs = new PointF[]
            {
                new PointF(screenPoint.X-size,screenPoint.Y+size),
                new PointF(screenPoint.X,screenPoint.Y-size),
                new PointF(screenPoint.X+size,screenPoint.Y+size),
                //new PointF(screenPoint.X,screenPoint.Y-size),
                new PointF(screenPoint.X-size,screenPoint.Y+size)
            };
            canvas.Graphics.FillPolygon(Brushes.Green, pointFs);
            canvas.Graphics.DrawPolygon(new Pen(Color.Yellow, 1), pointFs);
        }

        public static void DrawCircleNode(ICanvas canvas, UnitPoint unitPoint)
        {
            RectangleF rectangleF = new RectangleF(canvas.ToScreen(unitPoint), new SizeF(0, 0));
            rectangleF.Inflate(4, 4);
            canvas.Graphics.FillEllipse(Brushes.Yellow, rectangleF);
            rectangleF.Inflate(1, 1);
            canvas.Graphics.DrawRectangle(Pens.Black, ScreenUtils.ConvertRectangle(rectangleF));
        }

        public static void DrawMicroFlag(ICanvas canvas, List<UnitPoint> microStartPoints)
        {
            if (microStartPoints != null)
            {
                microStartPoints.ForEach((start) =>
                {
                    RectangleF rectangleF = new RectangleF(canvas.ToScreen(start), new SizeF(0, 0));
                    rectangleF.Inflate(2.5f, 2.5f);
                    canvas.Graphics.DrawRectangle(Pens.White, ScreenUtils.ConvertRectangle(rectangleF));
                });
            }
        }
        public static void DrawCoolPoints(ICanvas canvas, bool isSelected, Pen pen, List<UnitPoint> points)
        {
            if (points != null)
            {
                points.ForEach((point) =>
                {
                    if (isSelected)
                    {
                        canvas.DrawDot(canvas, pen, point, (float)canvas.ToUnit(3f));
                    }
                    else
                    {
                        canvas.DrawDot(canvas, new SolidBrush(pen.Color), point, (float)canvas.ToUnit(3f));
                    }

                });
            }
        }

        public static List<IDrawLite> GetCoolPoints(bool isSelected, Pen pen, List<UnitPointBulge> points)
        {
            var items = new List<IDrawLite>();
            points.ForEach((point) =>
            {
                if (isSelected)
                {
                    //canvas.DrawDot(canvas, pen, point, (float)canvas.ToUnit(3f));
                    DotLite dot = new DotLite
                    {
                        DrawPen = pen,
                        Point = point.Point,
                        IsInCompensation = !point.IsBasePoint,//isInCompensation,
                        Radius = 3.0f
                    };
                    items.Add(dot);
                }
                else
                {
                    //canvas.DrawDot(canvas, new SolidBrush(pen.Color), point, (float)canvas.ToUnit(3f));
                    DotLite dot = new DotLite
                    {
                        DrawBrush = new SolidBrush(pen.Color),
                        Point = point.Point,
                        IsInCompensation = !point.IsBasePoint,//isInCompensation,
                        Radius = 3.0f
                    };
                    items.Add(dot);
                }
            });
            return items;
        }

        public static void Draw(ICanvas canvas, List<IDrawLite> items,float scale)
        {
            items.ForEach(m => m.Draw(canvas,scale));
        }
    }
}
