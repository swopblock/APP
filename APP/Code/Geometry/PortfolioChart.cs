using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code
{
    public class PortfolioChart
    {
        private float wLimit = 0;
        private float hLimit = 0;
        private PointF center = new PointF();
        public PortfolioChart(PointF Center, float WidthLimit, float HeightLimit)
        {
            wLimit = WidthLimit;
            hLimit = HeightLimit;

            center = Center;
        }
        public CurveSet DrawLine(Point corner, List<Candle> candle)
        {
            Rect box = new Rect(corner, new Size(wLimit, hLimit * 0.5));
            string curve = "";
            string line = "";

            float max = (float)GetMax(candle);
            float min = (float)GetMin(candle);

            float w = (float)(box.Width / candle.Count) * 0.7f;

            float prx = 0;
            float pry = 0;

            Candle prcan = null;

            for (int i = 0; i < candle.Count; i++)
            {
                Candle can = candle[candle.Count - i - 1];

                float xc = (float)(i * w) + (float)box.X;
                float yt = GetTranslateY((float)can.close, min, max, true);
                float yc = (yt * (float)box.Height) + (float)(box.Y);

                if (i != 0)
                {
                    float vx = (prx + xc) / 2;
                    float vy = (pry + yc) / 2;

                    float wx = (vx + prx) / 2;
                    float wy = (vx + xc) / 2;

                    line += BuildSVG.QuadTo(wx, pry, vx, vy);
                    line += BuildSVG.QuadTo(wy, yc, xc, yc);
                    curve += BuildSVG.QuadTo(wx, pry, vx, vy);
                    curve += BuildSVG.QuadTo(wy, yc, xc, yc);
                }
                else
                {
                    PointF pf = new PointF(xc, yc);

                    line = BuildSVG.BoxValue(pf, center, wLimit, wLimit);
                    curve = BuildSVG.BoxValue(pf, center, wLimit, wLimit);
                }

                prx = xc;
                pry = yc;
            }

            float xf = (float)((candle.Count - 1) * w) + (float)box.X;

            float yf = (float)(box.Y + (box.Height * 1.8));

            curve += BuildSVG.LineTo(xf, yf);
            curve += BuildSVG.LineTo(0, yf);

            curve += " Z ";

            CurveSet set = new CurveSet(curve, line);

            if (curve.Contains("NaN") || line.Contains("NaN"))
            {
                set.SetError(true);
                return set;
            }

            return set;
        }
        public static double GetMax(List<Candle> data)
        {
            if (data != null)
            {
                if (data.Count > 0)
                {
                    List<double> d = data.Select(x => x.hi).ToList();

                    return d.Max();
                }
            }

            return 0;
        }

        public static double GetMin(List<Candle> data)
        {
            if (data != null)
            {
                if (data.Count > 0)
                {
                    List<double> d = data.Select(x => x.low).ToList();

                    return d.Min();
                }
            }

            return 0;
        }

        public static float GetMax(List<float> data)
        {
            float max = 0;

            max = data.Max();

            return max;
        }

        public static float GetMin(List<float> data)
        {
            float min = 0;

            min = data.Min();

            return min;
        }

        public static float GetTranslateX(float x, float low, float hi)
        {
            float dif = (low - hi);
            if (dif < 0) dif *= -1;

            float dfx = (x - low);
            if (dfx < 0) dfx *= -1;

            float xVal = (low / dif);

            return (xVal);
        }

        public static float GetTranslateY(float y, float low, float hi, bool yFlip = false)
        {
            float dif = (low - hi);
            if (dif < 0) dif *= -1;

            float dfy = (y - low);
            if (dfy < 0) dfy *= -1;

            float yVal = (dfy / dif);

            if (yFlip)
            {
                yVal = 1 - yVal;
            }

            return (yVal);
        }
    }
}
