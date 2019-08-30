using System;
using System.Collections.Generic;
using WSX.CommomModel.DrawModel;
using WSX.DrawService.CanvasControl;

namespace WSX.DrawService.Utils
{
    public class Utils
    {
        public static IDrawObject FindObjectTypeInList(IDrawObject caller,List<IDrawObject> drawObjects,Type type)
        {
            foreach (IDrawObject drawObject in drawObjects)
            {
                if(object.ReferenceEquals(caller,drawObject))
                {
                    continue;
                }
                if(drawObject.GetType()==type)
                {
                    return drawObject;
                }
            }
            return null;
        }


        public static UnitPoint ScaleAlgorithm(UnitPoint originalPoint,double scaleCenterX,double scaleCenterY,double scaleCoffX,double scaleCoffY)
        {
            double x = originalPoint.X + (scaleCoffX - 1) * (originalPoint.X - scaleCenterX);
            double y = originalPoint.Y + (scaleCoffY - 1) * (originalPoint.Y - scaleCenterY);
            return new UnitPoint(x, y);
        }

        public static UnitPoint RotateAlgorithm(UnitPoint originalPoint,UnitPoint rotateCenter,double rotateAngle)
        {
            double x = (originalPoint.X - rotateCenter.X) * Math.Cos(rotateAngle) + (originalPoint.Y - rotateCenter.Y) * Math.Sin(rotateAngle) + rotateCenter.X;
            double y = -(originalPoint.X - rotateCenter.X) * Math.Sin(rotateAngle) + (originalPoint.Y - rotateCenter.Y) * Math.Cos(rotateAngle) + rotateCenter.Y;
            return new UnitPoint(x, y);
        }

        public static UnitPoint MirrorAnyAlgorithm(UnitPoint originalPoint,double A,double B,double C)
        {
            double divideNumber = A * A + B * B;
            double fx0y0 = A * originalPoint.X + B * originalPoint.Y + C;
            double x = originalPoint.X - 2 * A * fx0y0 / divideNumber;
            double y = originalPoint.Y - 2 * B * fx0y0 / divideNumber;
            return new UnitPoint(x, y);
        }

    }
}
