using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.CommomModel.FigureModel;

namespace WSX.DataCollection.Utilities
{
    /// <summary>
    /// 圆管切割数据计算
    /// </summary>
    public class PipeHelper
    {
        /// <summary>
        /// 圆管面切割
        /// </summary>
        /// <param name="isRotationX">是否x作为旋转轴</param>
        /// <param name="r">圆管半径</param>
        /// <param name="angle">倾斜角</param>
        /// <param name="step">取点步长，默认0.1</param>
        /// <returns>返回多段线</returns>
        public static List<FigureBaseModel> GetDataByPipePlane(bool isRotationX, double r, double angle, double step = 0.1)
        {
            angle = angle % 180;
            if (angle == 0 || angle == 180) return null;
            LwPolylineModel lines = new LwPolylineModel() { IsFill = false, LayerId = 1 };
            double moveAngle = 0;//平移
            if (angle > 90) { moveAngle = Math.PI; }
            double A = Math.PI / 180.0;
            double range = r * Math.Tan(A * Math.Abs(angle - 90));//阈值
            double x, y, theta;
            for (double i = 0; i < 360.0; i += step)
            {
                x = A * r * i;
                theta = A * i - moveAngle;
                y = range * (Math.Cos(theta) + 1);
                lines.Points.Add(new UnitPointBulge(isRotationX ? new UnitPoint(x, y) : new UnitPoint(y, x)));
            }
            return new List<FigureBaseModel>() { lines };
        }

        /// <summary>
        /// 圆管相贯线计算
        /// </summary>
        /// <param name="isRotationX">是否X作为旋转轴，否则Y</param>
        /// <param name="isDrill">是否钻孔，否则切断</param>
        /// <param name="isThrough">是否穿过待切管</param>
        /// <param name="R">待切管半径</param>
        /// <param name="angle">相贯角（倾斜角）</param>
        /// <param name="r">相贯半径</param>
        /// <param name="d">轴心距离</param>
        /// <param name="step">取点步长</param>
        /// <returns></returns>
        public static List<FigureBaseModel> GetDataByPipeIntersecte(bool isRotationX, bool isDrill, bool isThrough, double R, double angle, double r, double d, double step = 0.1)
        {
            // 圆管相贯线平面展开曲线方程,参考Url: https://www.ixueshu.com/document/06f64bb49e36f975.html
            // R的展开平面曲线方程：
            // X = R * α 
            // Y = 1 / Math.Cos(β) * (+/-Math.Sqrt(r * r - Math.Pow(R * Math.Sin(α) - d, 2)) - R * Math.Cos(α) * Math.Sin(β));
            // 参数说明：
            // 1.X->横坐标
            // 2.Y->纵坐标
            // 3.α ->旋转角[0, 2π]
            // 4.β->两轴倾斜夹角(不能等于0°或者180°)
            // 5.R->待切管半径
            // 6.r->相贯半径
            // 7.d->两圆管的轴心距离

            if (angle == 0 || angle == 180) return null;


            List<FigureBaseModel> rets = new List<FigureBaseModel>();
            //缓存四个区域的连续点
            List<UnitPointBulge> points1 = new List<UnitPointBulge>();
            List<UnitPointBulge> points2 = new List<UnitPointBulge>();
            List<UnitPointBulge> points3 = new List<UnitPointBulge>();
            List<UnitPointBulge> points4 = new List<UnitPointBulge>();
            //计算相贯线的坐标
            double A = Math.PI / 180.0;
            double beta = A * angle + Math.PI / 2;
            double x, y1, y2, alpha, sqrt, sinA, sinB, cosA, cosB;
            sinB = Math.Sin(beta);
            cosB = Math.Cos(beta);
            for (double i = 0; i < 360; i += step)
            {
                alpha = A * i;
                x = R * alpha;
                alpha = alpha - Math.PI / 2;//平移
                sinA = Math.Sin(alpha);
                cosA = Math.Cos(alpha);
                sqrt = Math.Sqrt(r * r - Math.Pow(R * sinA - d, 2));

                if (!double.IsNaN(sqrt))
                {
                    y1 = 1 / cosB * (-sqrt - R * cosA * sinB);//开方取负
                    y2 = 1 / cosB * (sqrt - R * cosA * sinB);//开方取正
                    if (i < 180)
                    {
                        points1.Add(isRotationX ? new UnitPointBulge(new UnitPoint(x, y1)) : new UnitPointBulge(new UnitPoint(y1, x)));
                        points2.Add(isRotationX ? new UnitPointBulge(new UnitPoint(x, y2)) : new UnitPointBulge(new UnitPoint(y2, x)));
                    }
                    else
                    {
                        points3.Add(isRotationX ? new UnitPointBulge(new UnitPoint(x, y1)) : new UnitPointBulge(new UnitPoint(y1, x)));
                        points4.Add(isRotationX ? new UnitPointBulge(new UnitPoint(x, y2)) : new UnitPointBulge(new UnitPoint(y2, x)));
                    }
                }
            }
            //筛选合适的点
            //根据轴心距离和半径，判断两圆管的相交情况，确定切断/钻孔/穿过的切割方式
            int cutStyle = 0;//切割方式0-钻孔，1-切断，2-穿过
            int lineNum = 1;//曲线数量,1条和2条
            bool isClose = false;//是否封闭

            double rmin = R + d - r;
            double rmax = R + d + r;
            double Rmin = 0;
            double Rmax = 2 * R;
            if (rmin == Rmin && rmax == Rmax)
            {
                //两圆管大小相同且正交
                if (isThrough) { cutStyle = 2; lineNum = 2; isClose = true; }
                else if (isDrill) { cutStyle = 0; lineNum = 1; isClose = true; }
                else { cutStyle = 1; lineNum = 1; isClose = false; }
            }
            else
            {
                if ((rmin < Rmin && rmax > rmin && rmax < Rmax) ||
                    (rmax > Rmax && rmin > Rmin && rmin < Rmax))
                {
                    //两圆管有部分相交,一条封闭曲线
                    cutStyle = 2;
                    lineNum = 1;
                    isClose = true;
                }
                else if (rmin < Rmin && rmax > Rmax)
                {
                    //待切管被完全包围，两条封闭曲线，取一条，即切断
                    cutStyle = 1;
                    lineNum = 1;
                    isClose = false;
                }
                else
                {
                    //待切管被打孔
                    if (isThrough) { cutStyle = 2; lineNum = 2; isClose = true; }
                    else { cutStyle = 0; lineNum = 1; isClose = true; }
                }
            }
            if (cutStyle == 0)
            {
                points2.Reverse();
                var line1 = new LwPolylineModel() { IsFill = isClose, LayerId = 1 }; 
                line1.Points.AddRange(points1);
                line1.Points.AddRange(points2);
                rets.Add(line1);
            }
            else if (cutStyle == 1)
            {
                var line1 = new LwPolylineModel() { IsFill = isClose, LayerId = 1 }; 
                line1.Points.AddRange(points1);
                line1.Points.AddRange(points3);
                rets.Add(line1);
            }
            else
            {
                if (lineNum == 2)
                {
                    points2.Reverse();
                    points4.Reverse();
                    var line1 = new LwPolylineModel() { IsFill = isClose, LayerId = 1 };
                    var line2 = new LwPolylineModel() { IsFill = isClose, LayerId = 1 };
                    line1.Points.AddRange(points1);
                    line1.Points.AddRange(points2);
                    line2.Points.AddRange(points4);
                    line2.Points.AddRange(points3);
                    rets.Add(line1);
                    rets.Add(line2);
                }
                else
                {
                    points2.Reverse();
                    points4.Reverse();
                    var line1 = new LwPolylineModel() { IsFill = isClose, LayerId = 1 }; 
                    line1.Points.AddRange(points1);
                    line1.Points.AddRange(points3);
                    line1.Points.AddRange(points4);
                    line1.Points.AddRange(points2);
                    rets.Add(line1);
                }
            }
            return rets;
        }
    }
}
