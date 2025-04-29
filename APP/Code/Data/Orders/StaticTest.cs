using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code.Data.Orders
{
    public class StaticTest
    {
        //public static WalletPage Wallet = new WalletPage();

        public static GeometryContainer GeometryContainer = null;

        public static GeometryContainer GetCurves(float angle, float total)
        {
            GeometryContainer geometryContainer = new GeometryContainer();    

            float mv = 30;
            float degrees = 30;

            List<Microsoft.Maui.Controls.Shapes.Geometry> sets = new List<Microsoft.Maui.Controls.Shapes.Geometry>();
            List<Microsoft.Maui.Controls.Shapes.Geometry> line = new List<Microsoft.Maui.Controls.Shapes.Geometry>();
            //Thread.Sleep(10);
            while (total < 360)
            {
                CurveSet set = GetNextBasicCurve(new PointF(150, 150), degrees, angle, 100, 150);

                string curv = set.GetCurve();
                string spce = set.GetSpace();

                Microsoft.Maui.Controls.Shapes.Geometry pathCurve =
                    (Microsoft.Maui.Controls.Shapes.Geometry)new PathGeometryConverter()
                    .ConvertFromString(curv);

                sets.Add(pathCurve);

                Microsoft.Maui.Controls.Shapes.Geometry pathLine =
                    (Microsoft.Maui.Controls.Shapes.Geometry)new PathGeometryConverter()
                    .ConvertFromString(spce);

                line.Add(pathLine);

                degrees += mv;

                total += mv;
            }

            geometryContainer.LoadingCircle = sets;
            geometryContainer.LoadingLine = line;

            return geometryContainer;
        }

        public static CurveSet GetNextBasicCurve(PointF Center, float degrees, float startAngle, float InnerRadius, float OuterRadius)
        {
            string CurvePathBlock = "";
            string CurvePathLine = "";
            string CurevPathOuter = "";

            float totalAngle = startAngle;

            float angleInc = 0.5f;

            float finalangle = totalAngle + degrees;

            bool once = true;

            PointF InnerP1 = new PointF();
            PointF InnerP2 = new PointF();

            PointF OuterP1 = new PointF();
            PointF OuterP2 = new PointF();

            float cut = 1;

            while (totalAngle <= (int)(finalangle))
            {
                // get inner radius points on a circle
                InnerP1 = GetCirclePoint(InnerRadius, startAngle, Center, OuterRadius * 2);
                InnerP2 = GetCirclePoint(InnerRadius, MathUtil.GetNextAngle(startAngle, angleInc), Center, OuterRadius * 2);

                // get outer radius points on a circle
                OuterP1 = GetCirclePoint(OuterRadius, startAngle, Center, OuterRadius * 2);
                OuterP2 = GetCirclePoint(OuterRadius, MathUtil.GetNextAngle(startAngle, angleInc), Center, OuterRadius * 2);

                if (once) // only runs on the begining arc
                {
                    string bx = BuildSVG.BoxValue(InnerP1, Center, OuterRadius * 2, OuterRadius * 2);

                    CurvePathBlock += bx;

                    CurvePathLine += bx;

                    once = false;
                }

                // update angle postion to new position
                startAngle = MathUtil.GetNextAngle(startAngle, angleInc);

                CurvePathLine += BuildSVG.LineTo(InnerP2);

                CurvePathBlock += BuildSVG.LineTo(InnerP2);

                CurevPathOuter = BuildSVG.LineTo(OuterP1) + CurevPathOuter;// Line to

                totalAngle += angleInc;
            }

            // update circle points
            if (!once)
            {
                InnerP2 = GetCirclePoint(InnerRadius, MathUtil.GetNextAngle(startAngle, angleInc), Center, OuterRadius * 2);

                OuterP1 = GetCirclePoint(OuterRadius, startAngle, Center, OuterRadius * 2);
                OuterP2 = GetCirclePoint(OuterRadius, MathUtil.GetNextAngle(startAngle, angleInc), Center, OuterRadius * 2);


                CurvePathLine += BuildSVG.LineTo(InnerP2);

                CurvePathBlock += BuildSVG.LineTo(InnerP2);

                CurevPathOuter = BuildSVG.LineTo(OuterP1) + CurevPathOuter;

                CurvePathBlock += BuildSVG.LineTo(OuterP2) + CurevPathOuter + " Z"; // close shape
            }

            return new CurveSet(CurvePathBlock, CurvePathLine);
        }
        public static PointF GetCirclePoint(float radius, float angle, PointF Center, float wLimit)
        {
            PointF pt = MathUtil.PointOnACircle(radius, angle, Center);

            PointF Intersect = new PointF(-1, -1);

            if (pt.X < 0 || pt.X > wLimit)
            {
                if (pt.X > Center.X)
                {
                    Intersect = MathUtil.RelocatePointX(
                        Center,
                        pt,
                        wLimit);
                }
                else
                {
                    Intersect = MathUtil.RelocatePointX(
                        Center,
                        pt,
                        0);
                }
            }
            else
            {
                return pt;
            }

            return Intersect;
        }
    }

    public class GeometryContainer
    {
        public List<Microsoft.Maui.Controls.Shapes.Geometry> LoadingCircle = null;
        public List<Microsoft.Maui.Controls.Shapes.Geometry> LoadingLine = null;
    }
}
