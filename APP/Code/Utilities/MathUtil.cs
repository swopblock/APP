using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code
{
    public class MathUtil
    {
        public static PointF PointOnACircle(float radius, float angleDegrees, PointF Origin)
        {
            float x = (float)(radius * Math.Cos(angleDegrees * Math.PI / 180f)) + Origin.X;
            float y = (float)(radius * Math.Sin(angleDegrees * Math.PI / 180f)) + Origin.Y;

            return new PointF
            {
                X = MathF.Round(x, 2),
                Y = MathF.Round(y, 2)
            };
        }

        public static float GetNextAngle(float angle, float Increment)
        {
            if (angle + Increment <= 360)
            {
                return angle + Increment;
            }
            else
            {
                return (angle + Increment) % 360;
            }
        }

        public static double AbsoluteValue(double d)
        {
            if (d > 0) return d;
            else return -d;
        }

        public static float CapValue(float value, float lowCap, float highCap)
        {
            if (value > highCap)
            {
                return highCap;
            }
            if (value < lowCap)
            {
                return lowCap;
            }

            return value;
        }

        public static float Distance(PointF pt1, PointF pt2)
        {
            return (float)Math.Sqrt(
                Math.Pow(pt2.X - pt1.X, 2) +
                Math.Pow(pt2.Y - pt2.Y, 2));
        }
        public static float CalculateYIntercept(float slope, float x1, float y1)
        {
            float yIntercept = y1 - slope * x1;
            return yIntercept;
        }

        public static PointF RelocatePointX(PointF one, PointF two, float x)
        {
            float dist = Distance(one, two);
            float mx = (one.X - two.X) / dist;
            float my = (one.Y - two.Y) / dist;

            float mslope = my / mx;

            float yinter = CalculateYIntercept(mslope, one.X, one.Y);

            // y  = (mslope * x) + b

            PointF ptf = new PointF(x, mslope * x + yinter);

            return ptf;// new PointF(x + one.X, (mslope * x * -1) + (one.Y));
        }

        //public static PointF LinesIntersect(PointF one, PointF two, PointF first, PointF last)
        //{
        //    //float A1 = one.Y - two.Y;
        //    //float B1 = one.X - two.X;
        //    //float C1 = A1 * two.X + B1 * two.Y;

        //    //float A2 = first.Y - last.Y;
        //    //float B2 = first.X - last.X;
        //    //float C2 = A2 * last.X + B2 * last.Y;

        //    //float delta = A1 * B2 - A2 * B1;

        //    float m1 = (two.Y - one.Y) / (two.X - one.X);
        //    float m2 = (last.Y - first.Y) / (last.X - first.X);

        //    if ((m1 - m2) != 0)
        //    {
        //        float b1 = one.Y - m1 * one.X;
        //        float b2 = first.Y - m2 * first.X;

        //        float x = (b2 - b1) / (m1 - m2);
        //        float y = m1 * x + b1;

        //        return new PointF(x, y);
        //    }

        //    return new PointF(-1, -1);
        //}
    }
}
