using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code.Geometry
{
    public class BasicCircle
    {
        public Point Center { get; set; }

        float Radius = 10;

        public BasicCircle(Point center, float radius) 
        { 
            Center = center;
            Radius = radius;
        }
        public string GetCircle(float AngleStart, float Percent)
        {
            string CurvePathLine = "";

            PointF InnerP1 = new PointF();

            for (float f = AngleStart; f < AngleStart + Percent; f += 0.36f)
            {

                float ang = f;

                if(f < 0)
                {
                    ang = (360 + ang) % 360;
                }
                else if(f > 360)
                {
                    ang %= 360;
                }

                InnerP1 = GetCirclePoint(Radius, ang);

                if (f == AngleStart)
                {
                    CurvePathLine += BuildSVG.BoxArea(Center, InnerP1, Radius);
                }

                CurvePathLine += BuildSVG.LineTo(InnerP1);
            }

            //InnerP1 = GetCirclePoint(Radius, 0f);
            //CurvePathLine += BuildSVG.LineTo(InnerP1.X, InnerP1.Y);

            return CurvePathLine;
        }

        public PointF GetCirclePoint(float radius, float angle)
        {
            PointF pt = MathUtil.PointOnACircle(radius, angle, Center);

            return pt;
        }
    }
}
