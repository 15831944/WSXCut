using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.DrawService.Utils
{
    /// <summary>
    /// 颜色转换帮助类
    /// </summary>
    public class ColorHelper
    {
        /// <summary>
        /// 调整亮度
        /// </summary>
        /// <param name="color"></param>
        /// <param name="percent">0-1，百分比</param>
        /// <returns></returns>
        public static Color ChangeBrightness(Color color, double percent)
        {
            double hue, saturation, brightness;
            RGB2HSB(color.R, color.G, color.B, out hue, out saturation, out brightness);
            brightness = (float)(brightness * percent);
            return HSBtoRGB((float)hue, (float)saturation, (float)brightness);
        }
        private static void RGB2HSB(int red, int green, int blue, out double hue, out double saturation, out double brightness)
        {
            double r = (red / 255.0);
            double g = (green / 255.0);
            double b = (blue / 255.0);

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));

            hue = 0.0;
            if (max == r && g >= b)
            {
                if (max - min == 0) hue = 0.0;
                else hue = 60 * (g - b) / (max - min);
            }
            else if (max == r && g < b)
            {
                hue = 60 * (g - b) / (max - min) + 360;
            }
            else if (max == g)
            {
                hue = 60 * (b - r) / (max - min) + 120;
            }
            else if (max == b)
            {
                hue = 60 * (r - g) / (max - min) + 240;
            }

            saturation = (max == 0) ? 0.0 : (1.0 - ((double)min / (double)max));
            brightness = max;
        }
        /// <summary>
        /// HSB转RGB
        /// </summary>
        /// <param name="hue">色调</param>
        /// <param name="saturation">饱和度</param>
        /// <param name="brightness">亮度</param>
        /// <returns>返回：Color</returns>
        private static Color HSBtoRGB(float hue, float saturation, float brightness)
        {
            double r = 0;
            double g = 0;
            double b = 0;

            if (saturation == 0)
            {
                r = g = b = brightness;
            }
            else
            {
                // the color wheel consists of 6 sectors. Figure out which sector you're in.
                double sectorPos = hue / 60.0;
                int sectorNumber = (int)(Math.Floor(sectorPos));
                // get the fractional part of the sector
                double fractionalSector = sectorPos - sectorNumber;

                // calculate values for the three axes of the color. 
                double p = brightness * (1.0 - saturation);
                double q = brightness * (1.0 - (saturation * fractionalSector));
                double t = brightness * (1.0 - (saturation * (1 - fractionalSector)));

                // assign the fractional colors to r, g, and b based on the sector the angle is in.
                switch (sectorNumber)
                {
                    case 0:
                        r = brightness;
                        g = t;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = brightness;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = brightness;
                        b = t;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = brightness;
                        break;
                    case 4:
                        r = t;
                        g = p;
                        b = brightness;
                        break;
                    case 5:
                        r = brightness;
                        g = p;
                        b = q;
                        break;
                }
            }
            int red = Convert.ToInt32(r * 255);
            int green = Convert.ToInt32(g * 255);
            int blue = Convert.ToInt32(b * 255);
            return Color.FromArgb(Convert.ToByte(255), Convert.ToByte(red), Convert.ToByte(green), Convert.ToByte(blue));
        }
    }
}
