using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.ParaModel;
using WSX.DrawService.CanvasControl;
using WSX.DrawService.DrawTool.MultiSegmentLine;

namespace WSX.DrawService.Utils
{
    public class CommonSideHelper
    {
        /// <summary>
        /// 共边边境距离，阈值
        /// </summary>
        public static double CommonSideBorderDistance = 0.1d;
        /// <summary>
        /// 用于委托添加线段的匿名函数，方便构成匿名函数列表，仅在此Helper类内部出现
        /// </summary>
        private delegate void temp_func();
        /// <summary>
        /// 用于委托添加线段的匿名函数，方便构成匿名函数列表，仅在此Helper类内部出现
        /// </summary>
        /// <param name="isClockWise">表示单个矩形内部画线的方向是否为顺时针</param>
        private delegate void temp_func1(bool isClockWise);
        public CommonSideHelper() { }

        /// <summary>
        /// 横平竖直的样式
        /// </summary>
        /// <param name="drawObjects">已选中的需要进行共边的图形集合</param>
        /// <param name="unitPoints">不需要的参数</param>
        /// <param name="commonSideRectangleModel">共边需要的参数的Model对象</param>
        /// <returns>共边完之后线段的集合</returns>
        public List<List<UnitPoint>> GetHorizontalsAndVerticals(List<IDrawObject> drawObjects, List<UnitPoint> unitPoints, CommonSideRectangleModel commonSideRectangleModel)
        {
            List<List<UnitPoint>> result = new List<List<UnitPoint>>();

            List<MultiSegmentLineBase> multiSegment_drawObjects = Array.ConvertAll(drawObjects.ToArray(), s => s as MultiSegmentLineBase).ToList();
            List<double> indexX = new List<double> { };
            List<double> indexY = new List<double> { };
            foreach (var item in multiSegment_drawObjects)
            {
                double temp_x = item.Points.Min(t => t.Point.X);
                if (!indexX.Exists(s => DoubleEqual(s, temp_x))) { indexX.Add(temp_x); }
                double temp_y = item.Points.Min(t => t.Point.Y);
                if (!indexY.Exists(s => DoubleEqual(s, temp_y))) { indexY.Add(temp_y); }
            }
            indexX.Sort((x, y) =>
            {
                if (x > y) { return 1; }
                else if (x < y) { return -1; }
                return 0;
            });
            indexY.Sort((x, y) =>
            {
                if (x > y) { return 1; }
                else if (x < y) { return -1; }
                return 0;
            });
            //阵列内矩形的X轴边长和Y轴边长
            double sideLengthX = multiSegment_drawObjects[0].Points.Max(t => t.Point.X) - multiSegment_drawObjects[0].Points.Min(t => t.Point.X);
            double sideLengthY = multiSegment_drawObjects[0].Points.Max(t => t.Point.Y) - multiSegment_drawObjects[0].Points.Min(t => t.Point.Y);
            //确认起始位置，isright和istop表示起始位置是否为右或上
            bool isRight = false;
            bool isTop = false;
            switch (commonSideRectangleModel.StartPostion)
            {
                case StartPositions.LeftTop:
                    isTop = true;
                    break;
                case StartPositions.LeftBotton:
                    break;
                case StartPositions.RightTop:
                    isTop = true;
                    isRight = true;
                    break;
                case StartPositions.RightBotton:
                    isRight = true;
                    break;
                default:
                    break;
            }
            //先画X轴方向的线
            for (int i = 0; i < indexY.Count + 1; i++)
            {
                int aim_i = isTop ? (indexY.Count - i) : i;
                if (i % 2 == (isRight ? 1 : 0))
                {
                    for (int j = 0; j < indexX.Count; j++)
                    {
                        CheckForExistence(j, aim_i, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 0);
                    }
                }
                else
                {
                    for (int j = indexX.Count - 1; j >= 0; j--)
                    {
                        CheckForExistence(j, aim_i, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 2);
                    }
                }
            }
            //再画Y轴方向的线
            for (int i = 0; i < indexX.Count + 1; i++)
            {
                int aim_i = isRight ? (indexX.Count - i) : i;
                if (i % 2 == (isTop ? 0 : 1))
                {
                    for (int j = indexY.Count - 1; j >= 0; j--)
                    {
                        CheckForExistence(aim_i, j, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 3);
                    }
                }
                else
                {
                    for (int j = 0; j < indexY.Count; j++)
                    {
                        CheckForExistence(aim_i, j, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 1);
                    }
                }
            }
            //将图形进行平移
            MoveTheFigure(indexX, indexY, sideLengthX, sideLengthY, result);
            return result;
        }

        /// <summary>
        /// 只有2个矩形的情况，2个矩形的大小位置都是没规律的，仅仅在阶梯样式里出现
        /// </summary>
        /// <param name="drawObjects">已选中的需要进行共边的图形集合</param>
        /// <param name="unitPoints">不需要的参数</param>
        /// <param name="commonSideRectangleModel">共边需要的参数的Model对象</param>
        /// <returns>共边完之后线段的集合</returns>
        public List<List<UnitPoint>> GetHorizontalsAndVerticals2(List<IDrawObject> drawObjects, List<UnitPoint> unitPoints, CommonSideRectangleModel commonSideRectangleModel)
        {
            double offset = UCCanvas.GetThresholdWidth() / 2;
            List<List<UnitPoint>> result = new List<List<UnitPoint>>();
            List<IDrawTranslation> trans_drawObjects = Array.ConvertAll(drawObjects.ToArray(), s => (IDrawTranslation)s).ToList();

            double Figure0center_x = (trans_drawObjects[0].MinValue.X + offset + trans_drawObjects[0].MaxValue.X - offset) / 2;
            double Figure0center_y = (trans_drawObjects[0].MinValue.Y + offset + trans_drawObjects[0].MaxValue.Y - offset) / 2;
            double Figure1center_x = (trans_drawObjects[1].MinValue.X + offset + trans_drawObjects[1].MaxValue.X - offset) / 2;
            double Figure1center_y = (trans_drawObjects[1].MinValue.Y + offset + trans_drawObjects[1].MaxValue.Y - offset) / 2;

            double spacing0 = Math.Abs(Figure1center_y - Figure0center_y)
                - (trans_drawObjects[1].MaxValue.Y - offset - Figure1center_y)
                - (trans_drawObjects[0].MaxValue.Y - offset - Figure0center_y);
            double spacing1 = (Math.Abs(Figure1center_x - Figure0center_x)
                - (trans_drawObjects[1].MaxValue.X - offset - Figure1center_x)
                - (trans_drawObjects[0].MaxValue.X - offset - Figure0center_x));

            if ((spacing0 > 0) && (spacing0 < CommonSideHelper.CommonSideBorderDistance))
            {
                spacing0 = ((Figure1center_y - Figure0center_y) > 0) ? -spacing0 : spacing0;
                //考虑到起始位置进行图形变量引用的交换
                if ((commonSideRectangleModel.StartPostion == StartPositions.LeftBotton) || (commonSideRectangleModel.StartPostion == StartPositions.RightBotton))
                {
                    if (spacing0 > 0)
                    {
                        IDrawTranslation temp = trans_drawObjects[0];
                        trans_drawObjects[0] = trans_drawObjects[1];
                        trans_drawObjects[1] = temp;
                        spacing0 = -spacing0;
                    }
                }
                if ((commonSideRectangleModel.StartPostion == StartPositions.LeftTop) || (commonSideRectangleModel.StartPostion == StartPositions.RightTop))
                {
                    if (spacing0 < 0)
                    {
                        IDrawTranslation temp = trans_drawObjects[0];
                        trans_drawObjects[0] = trans_drawObjects[1];
                        trans_drawObjects[1] = temp;
                        spacing0 = -spacing0;
                    }
                }
                List<UnitPoint> group0 = new List<UnitPoint>
                    {
                        new UnitPoint(trans_drawObjects[0].MinValue.X + offset, trans_drawObjects[0].MinValue.Y + offset),
                        new UnitPoint(trans_drawObjects[0].MinValue.X + offset, trans_drawObjects[0].MaxValue.Y - offset),
                        new UnitPoint(trans_drawObjects[0].MaxValue.X - offset, trans_drawObjects[0].MaxValue.Y - offset),
                        new UnitPoint(trans_drawObjects[0].MaxValue.X - offset, trans_drawObjects[0].MinValue.Y + offset),
                        new UnitPoint(trans_drawObjects[0].MinValue.X + offset, trans_drawObjects[0].MinValue.Y + offset)
                    };
                result.Add(group0);

                List<UnitPoint> group1 = new List<UnitPoint>();
                if (spacing0 > 0)
                {
                    if (trans_drawObjects[1].MinValue.X + offset < trans_drawObjects[0].MinValue.X + offset)
                    {
                        group1.Add(new UnitPoint(trans_drawObjects[0].MinValue.X + offset, trans_drawObjects[0].MinValue.Y + offset));
                    }
                    group1.Add(new UnitPoint(trans_drawObjects[1].MinValue.X + offset, trans_drawObjects[1].MaxValue.Y - offset + spacing0));
                    group1.Add(new UnitPoint(trans_drawObjects[1].MinValue.X + offset, trans_drawObjects[1].MinValue.Y + offset + spacing0));
                    group1.Add(new UnitPoint(trans_drawObjects[1].MaxValue.X - offset, trans_drawObjects[1].MinValue.Y + offset + spacing0));
                    group1.Add(new UnitPoint(trans_drawObjects[1].MaxValue.X - offset, trans_drawObjects[1].MaxValue.Y - offset + spacing0));
                    if (trans_drawObjects[1].MaxValue.X - offset > trans_drawObjects[0].MaxValue.X - offset)
                    {
                        group1.Add(new UnitPoint(trans_drawObjects[0].MaxValue.X - offset, trans_drawObjects[0].MinValue.Y + offset));
                    }
                }
                else
                {
                    if (trans_drawObjects[1].MinValue.X < trans_drawObjects[0].MinValue.X)
                    {
                        group1.Add(new UnitPoint(trans_drawObjects[0].MinValue.X + offset, trans_drawObjects[0].MaxValue.Y - offset));
                    }
                    group1.Add(new UnitPoint(trans_drawObjects[1].MinValue.X + offset, trans_drawObjects[1].MinValue.Y + offset + spacing0));
                    group1.Add(new UnitPoint(trans_drawObjects[1].MinValue.X + offset, trans_drawObjects[1].MaxValue.Y - offset + spacing0));
                    group1.Add(new UnitPoint(trans_drawObjects[1].MaxValue.X - offset, trans_drawObjects[1].MaxValue.Y - offset + spacing0));
                    group1.Add(new UnitPoint(trans_drawObjects[1].MaxValue.X - offset, trans_drawObjects[1].MinValue.Y + offset + spacing0));
                    if (trans_drawObjects[1].MaxValue.X > trans_drawObjects[0].MaxValue.X)
                    {
                        group1.Add(new UnitPoint(trans_drawObjects[0].MaxValue.X - offset, trans_drawObjects[0].MaxValue.Y - offset));
                    }
                }
                result.Add(group1);
            }
            else if ((spacing1 > 0) && (spacing1 < CommonSideHelper.CommonSideBorderDistance))
            {
                spacing1 = ((Figure1center_x - Figure0center_x) > 0) ? -spacing1 : spacing1;
                //考虑到起始位置进行图形变量引用的交换
                if ((commonSideRectangleModel.StartPostion == StartPositions.LeftTop) || (commonSideRectangleModel.StartPostion == StartPositions.LeftBotton))
                {
                    if (spacing1 > 0)
                    {
                        IDrawTranslation temp = trans_drawObjects[0];
                        trans_drawObjects[0] = trans_drawObjects[1];
                        trans_drawObjects[1] = temp;
                        spacing1 = -spacing1;
                    }
                }
                if ((commonSideRectangleModel.StartPostion == StartPositions.RightBotton) || (commonSideRectangleModel.StartPostion == StartPositions.RightTop))
                {
                    if (spacing1 < 0)
                    {
                        IDrawTranslation temp = trans_drawObjects[0];
                        trans_drawObjects[0] = trans_drawObjects[1];
                        trans_drawObjects[1] = temp;
                        spacing1 = -spacing1;
                    }
                }
                List<UnitPoint> group0 = new List<UnitPoint>
                    {
                        new UnitPoint(trans_drawObjects[0].MinValue.X + offset, trans_drawObjects[0].MinValue.Y + offset),
                        new UnitPoint(trans_drawObjects[0].MinValue.X + offset, trans_drawObjects[0].MaxValue.Y - offset),
                        new UnitPoint(trans_drawObjects[0].MaxValue.X - offset, trans_drawObjects[0].MaxValue.Y - offset),
                        new UnitPoint(trans_drawObjects[0].MaxValue.X - offset, trans_drawObjects[0].MinValue.Y + offset),
                        new UnitPoint(trans_drawObjects[0].MinValue.X + offset, trans_drawObjects[0].MinValue.Y + offset)
                    };
                result.Add(group0);

                List<UnitPoint> group1 = new List<UnitPoint>();
                if (spacing1 > 0)
                {
                    if (trans_drawObjects[1].MinValue.Y < trans_drawObjects[0].MinValue.Y)
                    {
                        group1.Add(new UnitPoint(trans_drawObjects[0].MinValue.X + offset, trans_drawObjects[0].MinValue.Y + offset));
                    }
                    group1.Add(new UnitPoint(trans_drawObjects[1].MaxValue.X - offset + spacing1, trans_drawObjects[1].MinValue.Y + offset));
                    group1.Add(new UnitPoint(trans_drawObjects[1].MinValue.X + offset + spacing1, trans_drawObjects[1].MinValue.Y + offset));
                    group1.Add(new UnitPoint(trans_drawObjects[1].MinValue.X + offset + spacing1, trans_drawObjects[1].MaxValue.Y - offset));
                    group1.Add(new UnitPoint(trans_drawObjects[1].MaxValue.X - offset + spacing1, trans_drawObjects[1].MaxValue.Y - offset));
                    if ((trans_drawObjects[1]).MaxValue.Y > (trans_drawObjects[0]).MaxValue.Y)
                    {
                        group1.Add(new UnitPoint((trans_drawObjects[0]).MinValue.X + offset, (trans_drawObjects[0]).MaxValue.Y - offset));
                    }
                }
                else
                {
                    if ((trans_drawObjects[1]).MinValue.Y < (trans_drawObjects[0]).MinValue.Y)
                    {
                        group1.Add(new UnitPoint((trans_drawObjects[0]).MaxValue.X - offset, (trans_drawObjects[0]).MinValue.Y + offset));
                    }
                    group1.Add(new UnitPoint((trans_drawObjects[1]).MinValue.X + offset + spacing1, (trans_drawObjects[1]).MinValue.Y + offset));
                    group1.Add(new UnitPoint((trans_drawObjects[1]).MaxValue.X - offset + spacing1, (trans_drawObjects[1]).MinValue.Y + offset));
                    group1.Add(new UnitPoint((trans_drawObjects[1]).MaxValue.X - offset + spacing1, (trans_drawObjects[1]).MaxValue.Y - offset));
                    group1.Add(new UnitPoint((trans_drawObjects[1]).MinValue.X + offset + spacing1, (trans_drawObjects[1]).MaxValue.Y - offset));
                    if ((trans_drawObjects[1]).MaxValue.Y > (trans_drawObjects[0]).MaxValue.Y)
                    {
                        group1.Add(new UnitPoint((trans_drawObjects[0]).MaxValue.X - offset, (trans_drawObjects[0]).MaxValue.Y - offset));
                    }
                }
                result.Add(group1);
            }

            return result;
        }

        /// <summary>
        /// 外框优先以及外框最后
        /// </summary>
        /// <param name="drawObjects">已选中的需要进行共边的图形集合</param>
        /// <param name="unitPoints">不需要的参数</param>
        /// <param name="commonSideRectangleModel">共边需要的参数的Model对象</param>
        /// <returns>共边完之后线段的集合</returns>
        public List<List<UnitPoint>> GetFramedPriority(List<IDrawObject> drawObjects, List<UnitPoint> unitPoints, CommonSideRectangleModel commonSideRectangleModel)
        {
            List<List<UnitPoint>> result = new List<List<UnitPoint>>();

            List<MultiSegmentLineBase> multiSegment_drawObjects = Array.ConvertAll(drawObjects.ToArray(), s => s as MultiSegmentLineBase).ToList();
            List<double> indexX = new List<double> { };
            List<double> indexY = new List<double> { };
            foreach (var item in multiSegment_drawObjects)
            {
                double temp_x = item.Points.Min(t => t.Point.X);
                if (!indexX.Exists(s => DoubleEqual(s, temp_x))) { indexX.Add(temp_x); }
                double temp_y = item.Points.Min(t => t.Point.Y);
                if (!indexY.Exists(s => DoubleEqual(s, temp_y))) { indexY.Add(temp_y); }
            }
            indexX.Sort((x, y) =>
            {
                if (x > y) { return 1; }
                else if (x < y) { return -1; }
                return 0;
            });
            indexY.Sort((x, y) =>
            {
                if (x > y) { return 1; }
                else if (x < y) { return -1; }
                return 0;
            });
            //求矩形的长和宽
            double sideLengthX = multiSegment_drawObjects[0].Points.Max(t => t.Point.X) - multiSegment_drawObjects[0].Points.Min(t => t.Point.X);
            double sideLengthY = multiSegment_drawObjects[0].Points.Max(t => t.Point.Y) - multiSegment_drawObjects[0].Points.Min(t => t.Point.Y);
            //确认起始位置
            bool isRight = false;
            bool isTop = false;
            switch (commonSideRectangleModel.StartPostion)
            {
                case StartPositions.LeftTop:
                    isTop = true;
                    break;
                case StartPositions.LeftBotton:
                    break;
                case StartPositions.RightTop:
                    isTop = true;
                    isRight = true;
                    break;
                case StartPositions.RightBotton:
                    isRight = true;
                    break;
                default:
                    break;
            }
            if (commonSideRectangleModel.CommonSideStyle == CommonSideStyles.FramedPriority)
            {
                List<temp_func> temp_Funcs = new List<temp_func>
                {
                    new temp_func(() =>
                    {
                        for (int i = 0; i < indexX.Count; i++)
                        {
                            CheckForExistence(i, 0, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 0);
                        }
                    }),
                    new temp_func(() =>
                    {
                        for (int i = 0; i < indexY.Count; i++)
                        {
                            CheckForExistence(indexX.Count, i, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 1);
                        }
                    }),
                    new temp_func(() =>
                    {
                        for (int i = indexX.Count - 1; i >= 0; i--)
                        {
                            CheckForExistence(i, indexY.Count, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 2);
                        }
                    }),
                    new temp_func(() =>
                    {
                        for (int i = indexY.Count - 1; i >= 0; i--)
                        {
                            CheckForExistence(0, i, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 3);
                        }
                    })
                };
                //画外框
                switch (commonSideRectangleModel.StartPostion)
                {
                    case StartPositions.LeftTop:
                        temp_Funcs[3](); temp_Funcs[0](); temp_Funcs[1](); temp_Funcs[2]();
                        break;
                    case StartPositions.LeftBotton:
                        temp_Funcs[0](); temp_Funcs[1](); temp_Funcs[2](); temp_Funcs[3]();
                        break;
                    case StartPositions.RightTop:
                        temp_Funcs[2](); temp_Funcs[3](); temp_Funcs[0](); temp_Funcs[1]();
                        break;
                    case StartPositions.RightBotton:
                        temp_Funcs[1](); temp_Funcs[2](); temp_Funcs[3](); temp_Funcs[0]();
                        break;
                    default:
                        break;
                }
                //画里面的十字交叉线
                for (int i = 1; i < indexY.Count; i++)
                {
                    int aim_i = isTop ? (indexY.Count - i) : i;
                    if (i % 2 == (isRight ? 0 : 1))
                    {
                        for (int j = 0; j < indexX.Count; j++)
                        {
                            CheckForExistence(j, aim_i, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 0);
                        }
                    }
                    else
                    {
                        for (int j = indexX.Count - 1; j >= 0; j--)
                        {
                            CheckForExistence(j, aim_i, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 2);
                        }
                    }
                }
                for (int i = 1; i < indexX.Count; i++)
                {
                    int aim_i = isRight ? (indexX.Count - i) : i;
                    if (i % 2 == (isTop ? 0 : 1))
                    {
                        for (int j = 0; j < indexY.Count; j++)
                        {
                            CheckForExistence(aim_i, j, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 1);
                        }
                    }
                    else
                    {
                        for (int j = indexY.Count - 1; j >= 0; j--)
                        {
                            CheckForExistence(aim_i, j, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 3);
                        }
                    }
                }
            }
            else if (commonSideRectangleModel.CommonSideStyle == CommonSideStyles.FrameFinal)
            {
                //画里面的十字交叉线
                for (int i = 1; i < indexY.Count; i++)
                {
                    int aim_i = isTop ? (indexY.Count - i) : i;
                    if (i % 2 == (isRight ? 0 : 1))
                    {
                        for (int j = 0; j < indexX.Count; j++)
                        {
                            CheckForExistence(j, aim_i, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 0);
                        }
                    }
                    else
                    {
                        for (int j = indexX.Count - 1; j >= 0; j--)
                        {
                            CheckForExistence(j, aim_i, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 2);
                        }
                    }
                }
                for (int i = 1; i < indexX.Count; i++)
                {
                    int aim_i = isRight ? (indexX.Count - i) : i;
                    if (i % 2 == (isTop ? 0 : 1))
                    {
                        for (int j = 0; j < indexY.Count; j++)
                        {
                            CheckForExistence(aim_i, j, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 1);
                        }
                    }
                    else
                    {
                        for (int j = indexY.Count - 1; j >= 0; j--)
                        {
                            CheckForExistence(aim_i, j, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 3);
                        }
                    }
                }
                List<temp_func> temp_Funcs = new List<temp_func>
                {
                    new temp_func(() =>
                    {
                        for (int i = 0; i < indexX.Count; i++)
                        {
                            CheckForExistence(i, 0, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 0);
                        }
                    }),
                    new temp_func(() =>
                    {
                        for (int i = 0; i < indexY.Count; i++)
                        {
                            CheckForExistence(indexX.Count, i, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 1);
                        }
                    }),
                    new temp_func(() =>
                    {
                        for (int i = indexX.Count - 1; i >= 0; i--)
                        {
                            CheckForExistence(i, indexY.Count, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 2);
                        }
                    }),
                    new temp_func(() =>
                    {
                        for (int i = indexY.Count - 1; i >= 0; i--)
                        {
                            CheckForExistence(0, i, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 3);
                        }
                    })
                };
                //画外框
                switch (commonSideRectangleModel.StartPostion)
                {
                    case StartPositions.LeftTop:
                        temp_Funcs[3](); temp_Funcs[0](); temp_Funcs[1](); temp_Funcs[2]();
                        break;
                    case StartPositions.LeftBotton:
                        temp_Funcs[0](); temp_Funcs[1](); temp_Funcs[2](); temp_Funcs[3]();
                        break;
                    case StartPositions.RightTop:
                        temp_Funcs[2](); temp_Funcs[3](); temp_Funcs[0](); temp_Funcs[1]();
                        break;
                    case StartPositions.RightBotton:
                        temp_Funcs[1](); temp_Funcs[2](); temp_Funcs[3](); temp_Funcs[0]();
                        break;
                    default:
                        break;
                }
            }
            //将图形进行平移
            MoveTheFigure(indexX, indexY, sideLengthX, sideLengthY, result);
            return result;
        }

        /// <summary>
        /// 逐个蛇形
        /// </summary>
        /// <param name="drawObjects">已选中的需要进行共边的图形集合</param>
        /// <param name="unitPoints">不需要的参数</param>
        /// <param name="commonSideRectangleModel">共边需要的参数的Model对象</param>
        /// <returns>共边完之后线段的集合</returns>
        public List<List<UnitPoint>> GetSerpentine(List<IDrawObject> drawObjects, List<UnitPoint> unitPoints, CommonSideRectangleModel commonSideRectangleModel)
        {
            List<List<UnitPoint>> result = new List<List<UnitPoint>>();

            List<MultiSegmentLineBase> multiSegment_drawObjects = Array.ConvertAll(drawObjects.ToArray(), s => s as MultiSegmentLineBase).ToList();
            List<double> indexX = new List<double> { };
            List<double> indexY = new List<double> { };
            foreach (var item in multiSegment_drawObjects)
            {
                double temp_x = item.Points.Min(t => t.Point.X);
                if (!indexX.Exists(s => DoubleEqual(s, temp_x))) { indexX.Add(temp_x); }
                double temp_y = item.Points.Min(t => t.Point.Y);
                if (!indexY.Exists(s => DoubleEqual(s, temp_y))) { indexY.Add(temp_y); }
            }
            indexX.Sort((x, y) =>
            {
                if (x > y) { return 1; }
                else if (x < y) { return -1; }
                return 0;
            });
            indexY.Sort((x, y) =>
            {
                if (x > y) { return 1; }
                else if (x < y) { return -1; }
                return 0;
            });
            //求矩形的长和宽
            double sideLengthX = multiSegment_drawObjects[0].Points.Max(t => t.Point.X) - multiSegment_drawObjects[0].Points.Min(t => t.Point.X);
            double sideLengthY = multiSegment_drawObjects[0].Points.Max(t => t.Point.Y) - multiSegment_drawObjects[0].Points.Min(t => t.Point.Y);
            //确认起始位置
            bool isRight = false;
            bool isTop = false;
            switch (commonSideRectangleModel.StartPostion)
            {
                case StartPositions.LeftTop:
                    isTop = true;
                    break;
                case StartPositions.LeftBotton:
                    break;
                case StartPositions.RightTop:
                    isTop = true;
                    isRight = true;
                    break;
                case StartPositions.RightBotton:
                    isRight = true;
                    break;
                default:
                    break;
            }
            //开始画。hashtable_row和hashtable_column代表此线段有没有被画过
            bool[,] hashtable_row = new bool[indexX.Count, indexY.Count + 1];
            bool[,] hashtable_column = new bool[indexX.Count + 1, indexY.Count];
            for (int i = 0; i < indexX.Count; i++)
            {
                for (int j = 0; j < indexY.Count + 1; j++)
                {
                    hashtable_row[i, j] = false;
                }
            }
            for (int i = 0; i < indexX.Count + 1; i++)
            {
                for (int j = 0; j < indexY.Count; j++)
                {
                    hashtable_column[i, j] = false;
                }
            }
            //通过control_reverse控制蛇形行走方向的反转
            for (int out_y = 0; out_y < indexY.Count; out_y++)
            {
                int real_outy = isTop ? (indexY.Count - 1 - out_y) : out_y;
                int control_reverse = out_y % 2;
                for (int out_x = 0; out_x < indexX.Count; out_x++)
                {
                    int real_outx = (control_reverse == (isRight ? 1 : 0)) ? out_x : (indexX.Count - 1 - out_x);
                    List<temp_func1> temp_Funcs = new List<temp_func1>
                    {
                        new temp_func1((s) =>
                            {
                                if (s)
                                {
                                    if (!hashtable_row[real_outx, real_outy])
                                    {
                                        CheckForExistence(real_outx, real_outy, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 2);
                                        hashtable_row[real_outx, real_outy] = true;
                                    }
                                }
                                else
                                {
                                    if(!hashtable_row[real_outx, real_outy])
                                    {
                                        CheckForExistence(real_outx, real_outy, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 0);
                                        hashtable_row[real_outx, real_outy] = true;
                                    }
                                }
                            }),
                        new temp_func1((s) =>
                            {
                                if (s)
                                {
                                    if (!hashtable_column[real_outx + 1, real_outy])
                                    {
                                        CheckForExistence(real_outx + 1, real_outy, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 3);
                                        hashtable_column[real_outx + 1, real_outy] = true;
                                    }
                                }
                                else
                                {
                                    if (!hashtable_column[real_outx + 1, real_outy])
                                    {
                                        CheckForExistence(real_outx + 1, real_outy, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 1);
                                        hashtable_column[real_outx + 1, real_outy] = true;
                                    }
                                }
                            }),
                        new temp_func1((s) =>
                            {
                                if (s)
                                {
                                    if (!hashtable_row[real_outx, real_outy + 1])
                                    {
                                        CheckForExistence(real_outx, real_outy + 1, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 0);
                                        hashtable_row[real_outx, real_outy + 1] = true;
                                    }
                                }
                                else
                                {
                                    if (!hashtable_row[real_outx, real_outy + 1])
                                    {
                                        CheckForExistence(real_outx, real_outy + 1, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 2);
                                        hashtable_row[real_outx, real_outy + 1] = true;
                                    }
                                }
                            }),
                        new temp_func1((s) =>
                            {
                                if (s)
                                {
                                    if (!hashtable_column[real_outx, real_outy])
                                    {
                                        CheckForExistence(real_outx, real_outy, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 1);
                                        hashtable_column[real_outx, real_outy] = true;
                                    }
                                }
                                else
                                {
                                    if (!hashtable_column[real_outx, real_outy])
                                    {
                                        CheckForExistence(real_outx, real_outy, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 3);
                                        hashtable_column[real_outx, real_outy] = true;
                                    }
                                }
                            })
                    };
                    switch (commonSideRectangleModel.StartPostion)
                    {
                        case StartPositions.LeftTop:
                            temp_Funcs[2](true);
                            temp_Funcs[1](true);
                            temp_Funcs[0](true);
                            temp_Funcs[3](true);
                            break;
                        case StartPositions.LeftBotton:
                            temp_Funcs[3](true);
                            temp_Funcs[2](true);
                            temp_Funcs[1](true);
                            temp_Funcs[0](true);
                            break;
                        case StartPositions.RightTop:
                            temp_Funcs[1](true);
                            temp_Funcs[0](true);
                            temp_Funcs[3](true);
                            temp_Funcs[2](true);
                            break;
                        case StartPositions.RightBotton:
                            temp_Funcs[0](true);
                            temp_Funcs[3](true);
                            temp_Funcs[2](true);
                            temp_Funcs[1](true);
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 逐个阶梯图
        /// </summary>
        /// <param name="drawObjects">已选中的需要进行共边的图形集合</param>
        /// <param name="unitPoints">不需要的参数</param>
        /// <param name="commonSideRectangleModel">共边需要的参数的Model对象</param>
        /// <returns>共边完之后线段的集合</returns>
        public List<List<UnitPoint>> GetStairs(List<IDrawObject> drawObjects, List<UnitPoint> unitPoints, CommonSideRectangleModel commonSideRectangleModel)
        {
            List<List<UnitPoint>> result = new List<List<UnitPoint>>();

            List<MultiSegmentLineBase> multiSegment_drawObjects = Array.ConvertAll(drawObjects.ToArray(), s => s as MultiSegmentLineBase).ToList();
            List<double> indexX = new List<double> { };
            List<double> indexY = new List<double> { };
            foreach (var item in multiSegment_drawObjects)
            {
                double temp_x = item.Points.Min(t => t.Point.X);
                if (!indexX.Exists(s => DoubleEqual(s, temp_x))) { indexX.Add(temp_x); }
                double temp_y = item.Points.Min(t => t.Point.Y);
                if (!indexY.Exists(s => DoubleEqual(s, temp_y))) { indexY.Add(temp_y); }
            }
            indexX.Sort((x, y) =>
            {
                if (x > y) { return 1; }
                else if (x < y) { return -1; }
                return 0;
            });
            indexY.Sort((x, y) =>
            {
                if (x > y) { return 1; }
                else if (x < y) { return -1; }
                return 0;
            });
            //求矩形的长和宽
            double sideLengthX = multiSegment_drawObjects[0].Points.Max(t => t.Point.X) - multiSegment_drawObjects[0].Points.Min(t => t.Point.X);
            double sideLengthY = multiSegment_drawObjects[0].Points.Max(t => t.Point.Y) - multiSegment_drawObjects[0].Points.Min(t => t.Point.Y);
            //确认起始位置
            bool isRight = false;
            bool isTop = false;
            switch (commonSideRectangleModel.StartPostion)
            {
                case StartPositions.LeftTop:
                    isTop = true;
                    break;
                case StartPositions.LeftBotton:
                    break;
                case StartPositions.RightTop:
                    isTop = true;
                    isRight = true;
                    break;
                case StartPositions.RightBotton:
                    isRight = true;
                    break;
                default:
                    break;
            }
            //开始画。hashtable_row和hashtable_column代表此线段有没有被画过
            bool[,] hashtable_row = new bool[indexX.Count, indexY.Count + 1];
            bool[,] hashtable_column = new bool[indexX.Count + 1, indexY.Count];
            for (int i = 0; i < indexX.Count; i++)
            {
                for (int j = 0; j < indexY.Count + 1; j++)
                {
                    hashtable_row[i, j] = false;
                }
            }
            for (int i = 0; i < indexX.Count + 1; i++)
            {
                for (int j = 0; j < indexY.Count; j++)
                {
                    hashtable_column[i, j] = false;
                }
            }

            for (int sum = 0; sum < indexX.Count + indexY.Count - 1; sum++)
            {
                for (int figure_x = 0; figure_x <= sum; figure_x++)
                {
                    bool clockWise = (sum % 2 == 1) ? true : false;
                    int real_figure_x = (sum % 2 == 1) ? figure_x : (sum - figure_x);
                    int real_figure_y = sum - real_figure_x;
                    if ((real_figure_x >= indexX.Count) || (real_figure_y >= indexY.Count)) { continue; }
                    if (isRight) { real_figure_x = indexX.Count - 1 - real_figure_x; clockWise = !clockWise; }
                    if (isTop) { real_figure_y = indexY.Count - 1 - real_figure_y; clockWise = !clockWise; }
                    List<temp_func1> temp_Funcs = new List<temp_func1>
                    {
                        new temp_func1((s) =>
                            {
                                if (s)
                                {
                                    if (!hashtable_row[real_figure_x, real_figure_y])
                                    {
                                        CheckForExistence(real_figure_x, real_figure_y, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 2);
                                        hashtable_row[real_figure_x, real_figure_y] = true;
                                    }
                                }
                                else
                                {
                                    if(!hashtable_row[real_figure_x, real_figure_y])
                                    {
                                        CheckForExistence(real_figure_x, real_figure_y, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 0);
                                        hashtable_row[real_figure_x, real_figure_y] = true;
                                    }
                                }
                            }),
                        new temp_func1((s) =>
                            {
                                if (s)
                                {
                                    if (!hashtable_column[real_figure_x + 1, real_figure_y])
                                    {
                                        CheckForExistence(real_figure_x + 1, real_figure_y, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 3);
                                        hashtable_column[real_figure_x + 1, real_figure_y] = true;
                                    }
                                }
                                else
                                {
                                    if (!hashtable_column[real_figure_x + 1, real_figure_y])
                                    {
                                        CheckForExistence(real_figure_x + 1, real_figure_y, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 1);
                                        hashtable_column[real_figure_x + 1, real_figure_y] = true;
                                    }
                                }
                            }),
                        new temp_func1((s) =>
                            {
                                if (s)
                                {
                                    if (!hashtable_row[real_figure_x, real_figure_y + 1])
                                    {
                                        CheckForExistence(real_figure_x, real_figure_y + 1, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 0);
                                        hashtable_row[real_figure_x, real_figure_y + 1] = true;
                                    }
                                }
                                else
                                {
                                    if (!hashtable_row[real_figure_x, real_figure_y + 1])
                                    {
                                        CheckForExistence(real_figure_x, real_figure_y + 1, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 2);
                                        hashtable_row[real_figure_x, real_figure_y + 1] = true;
                                    }
                                }
                            }),
                        new temp_func1((s) =>
                            {
                                if (s)
                                {
                                    if (!hashtable_column[real_figure_x, real_figure_y])
                                    {
                                        CheckForExistence(real_figure_x, real_figure_y, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 1);
                                        hashtable_column[real_figure_x, real_figure_y] = true;
                                    }
                                }
                                else
                                {
                                    if (!hashtable_column[real_figure_x, real_figure_y])
                                    {
                                        CheckForExistence(real_figure_x, real_figure_y, indexX, indexY, sideLengthX, sideLengthY, result, multiSegment_drawObjects, 3);
                                        hashtable_column[real_figure_x, real_figure_y] = true;
                                    }
                                }
                            })
                    };
                    switch (commonSideRectangleModel.StartPostion)
                    {
                        case StartPositions.LeftTop:
                            if (clockWise)
                            {
                                temp_Funcs[2](true);
                                temp_Funcs[1](true);
                                temp_Funcs[0](true);
                                temp_Funcs[3](true);
                            }
                            else
                            {
                                temp_Funcs[3](false);
                                temp_Funcs[0](false);
                                temp_Funcs[1](false);
                                temp_Funcs[2](false);
                            }
                            break;
                        case StartPositions.LeftBotton:
                            if (clockWise)
                            {
                                temp_Funcs[3](true);
                                temp_Funcs[2](true);
                                temp_Funcs[1](true);
                                temp_Funcs[0](true);
                            }
                            else
                            {
                                temp_Funcs[0](false);
                                temp_Funcs[1](false);
                                temp_Funcs[2](false);
                                temp_Funcs[3](false);
                            }
                            break;
                        case StartPositions.RightTop:
                            if (clockWise)
                            {
                                temp_Funcs[1](true);
                                temp_Funcs[0](true);
                                temp_Funcs[3](true);
                                temp_Funcs[2](true);
                            }
                            else
                            {
                                temp_Funcs[2](false);
                                temp_Funcs[3](false);
                                temp_Funcs[0](false);
                                temp_Funcs[1](false);
                            }
                            break;
                        case StartPositions.RightBotton:
                            if (clockWise)
                            {
                                temp_Funcs[0](true);
                                temp_Funcs[3](true);
                                temp_Funcs[2](true);
                                temp_Funcs[1](true);
                            }
                            else
                            {
                                temp_Funcs[1](false);
                                temp_Funcs[2](false);
                                temp_Funcs[3](false);
                                temp_Funcs[0](false);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 判断两个double类的数是否相等
        /// </summary>
        /// <param name="x">double数的比较，第一个数</param>
        /// <param name="y">double数的比较，第二个数</param>
        /// <returns>返回是否相等的结果</returns>
        private bool DoubleEqual(double x, double y)
        {
            if (Math.Abs(x - y) < 0.00001d)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 检查画这条线是否合理，并且将结果添加到result引用内
        /// </summary>
        /// <param name="index_x">要画的线的x轴索引</param>
        /// <param name="index_y">要画的线的y轴索引</param>
        /// <param name="indexX">X轴上面有几个图形</param>
        /// <param name="indexY">Y轴上面有几个图形</param>
        /// <param name="sideLengthX">X轴边长</param>
        /// <param name="sideLengthY">Y轴边长</param>
        /// <param name="result">画线的结果列表</param>
        /// <param name="multiSegment_drawObjects">选中图形集合的引用</param>
        /// <param name="direction">表示画线的方向，0是从左到右，1是从下到上，2是从右到左，3是从上到下</param>
        private void CheckForExistence(int index_x, int index_y, List<double> indexX, List<double> indexY, double sideLengthX, double sideLengthY, List<List<UnitPoint>> result, List<MultiSegmentLineBase> multiSegment_drawObjects, int direction = 0)
        {
            double start_x = indexX[0];
            double start_y = indexY[0];
            if ((direction == 0) || (direction == 2))
            {
                double check_x;
                double check_y = 0.0;
                if (index_y % indexY.Count == 0)
                {
                    check_x = indexX[index_x];
                    if (index_y == 0) { check_y = indexY[index_y]; }
                    else if (index_y == indexY.Count) { check_y = indexY[index_y - 1] + sideLengthY; }
                    foreach (MultiSegmentLineBase item in multiSegment_drawObjects)
                    {
                        if (!item.Points.Exists(t => DoubleEqual(t.Point.X, check_x))) { continue; }
                        if (!item.Points.Exists(t => DoubleEqual(t.Point.X, check_x + sideLengthX))) { continue; }
                        if (!item.Points.Exists(t => DoubleEqual(t.Point.Y, check_y))) { continue; }
                        List<UnitPoint> temp = new List<UnitPoint> { };
                        if (direction == 0)
                        {
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY));
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX + sideLengthX, start_y + index_y * sideLengthY));
                        }
                        else if (direction == 2)
                        {
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX + sideLengthX, start_y + index_y * sideLengthY));
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY));
                        }
                        result.Add(temp);
                        break;
                    }
                }
                else
                {
                    foreach (MultiSegmentLineBase item in multiSegment_drawObjects)
                    {
                        if (!item.Points.Exists(t => DoubleEqual(t.Point.X, indexX[index_x]))) { continue; }
                        if (!item.Points.Exists(t => DoubleEqual(t.Point.X, indexX[index_x] + sideLengthX))) { continue; }
                        if (
                            (!item.Points.Exists(t => DoubleEqual(t.Point.Y, indexY[index_y]))) &&
                            (!item.Points.Exists(t => DoubleEqual(t.Point.Y, indexY[index_y - 1] + sideLengthY)))
                            )
                        { continue; }
                        List<UnitPoint> temp = new List<UnitPoint> { };
                        if (direction == 0)
                        {
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY));
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX + sideLengthX, start_y + index_y * sideLengthY));
                        }
                        else if (direction == 2)
                        {
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX + sideLengthX, start_y + index_y * sideLengthY));
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY));
                        }
                        result.Add(temp);
                        break;
                    }
                }
            }
            else if ((direction == 1) || (direction == 3))
            {
                double check_x = 0.0;
                if (index_x % indexX.Count == 0)
                {
                    if (index_x == 0) { check_x = indexX[index_x]; }
                    else if (index_x == indexX.Count) { check_x = indexX[index_x - 1] + sideLengthX; }
                    foreach (MultiSegmentLineBase item in multiSegment_drawObjects)
                    {
                        if (!item.Points.Exists(t => DoubleEqual(t.Point.Y, indexY[index_y]))) { continue; }
                        if (!item.Points.Exists(t => DoubleEqual(t.Point.Y, indexY[index_y] + sideLengthY))) { continue; }
                        if (!item.Points.Exists(t => DoubleEqual(t.Point.X, check_x))) { continue; }
                        List<UnitPoint> temp = new List<UnitPoint> { };
                        if (direction == 1)
                        {
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY));
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY + sideLengthY));
                        }
                        else if (direction == 3)
                        {
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY + sideLengthY));
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY));
                        }
                        result.Add(temp);
                        break;
                    }
                }
                else
                {
                    foreach (MultiSegmentLineBase item in multiSegment_drawObjects)
                    {
                        if (!item.Points.Exists(t => DoubleEqual(t.Point.Y, indexY[index_y]))) { continue; }
                        if (!item.Points.Exists(t => DoubleEqual(t.Point.Y, indexY[index_y] + sideLengthY))) { continue; }
                        if (
                            (!item.Points.Exists(t => DoubleEqual(t.Point.X, indexX[index_x]))) &&
                            (!item.Points.Exists(t => DoubleEqual(t.Point.X, indexX[index_x - 1] + sideLengthX)))
                           )
                        { continue; }
                        List<UnitPoint> temp = new List<UnitPoint> { };
                        if (direction == 1)
                        {
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY));
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY + sideLengthY));
                        }
                        else if (direction == 3)
                        {
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY + sideLengthY));
                            temp.Add(new UnitPoint(start_x + index_x * sideLengthX, start_y + index_y * sideLengthY));
                        }
                        result.Add(temp);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 平移整个图形
        /// </summary>
        /// <param name="indexX">X轴上面有几个图形</param>
        /// <param name="indexY">Y轴上面有几个图形</param>
        /// <param name="sideLengthX">X轴边长</param>
        /// <param name="sideLengthY">Y轴边长</param>
        /// <param name="result">画线的结果列表</param>
        private void MoveTheFigure(List<double> indexX, List<double> indexY, double sideLengthX, double sideLengthY, List<List<UnitPoint>> result)
        {
            double offset_x = 0.0;
            if (indexX.Count > 1)
            {
                offset_x = (indexX.Count - 1) * (indexX[1] - indexX[0] - sideLengthX);
            }
            double offset_y = 0.0;
            if (indexY.Count > 1)
            {
                offset_y = (indexY.Count - 1) * (indexY[1] - indexY[0] - sideLengthY);
            }
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < result[i].Count; j++)
                {
                    result[i][j] = new UnitPoint(result[i][j].X + 0.5 * offset_x, result[i][j].Y + 0.5 * offset_y);
                }
            }
        }
    }
}