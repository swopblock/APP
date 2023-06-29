using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.Code.Data.User;

namespace APP.Code
{
    public class PortfolioCircle
    { 

        public float totalSegments = 360;
        public float OuterRadius = 100;
        public float InnerRadius = 80;
        public PointF Center { get; set; }

        private ChartContainer charts { get; set; }

        private float lastAngle = 0;
        private float startAngle = 270;
        private float spacing = 1.7f;
        private float hLimit = -1;
        private float wLimit = -1;

        public PortfolioCircle(PointF center, ChartContainer ChartValues, List<PortfolioAsset> assets = null, float HeightLimit = -1, float WidthLimit = -1)
        {
            Center = center;

            charts = ChartValues;

            this.hLimit = HeightLimit;
            this.wLimit = WidthLimit;

            if (this.hLimit < 0) this.hLimit = OuterRadius;
            if (this.wLimit < 0) this.wLimit = OuterRadius;

            if (UserProfileData.PortfolioAssets == null)
            {
                if (assets != null)
                {

                    UserProfileData.PortfolioAssets = assets;

                    UpdateValues();
                }
                else
                {
                    UserProfileData.PortfolioAssets = UserProfileData.LoadDemo();

                    UpdateValues();
                }
            }
        }

        public float GetWidthLimit() { return wLimit;  }
        public float GetHeightLimit() { return hLimit; }    

        private decimal GetTotal()
        {
            decimal total = 0;

            for (int i = 0; i < UserProfileData.PortfolioAssets.Count; i++)
            {
                decimal price = (decimal)charts.GetPrice(UserProfileData.PortfolioAssets[i].Symbol);

                UserProfileData.PortfolioAssets[i].Price = price;

                total += (UserProfileData.PortfolioAssets[i].Amount * price);
            }

            return total;
        }

        private void UpdateValues()
        {
            decimal ttl = GetTotal();

            float startang = -90;

            for (int i = 0; i < UserProfileData.PortfolioAssets.Count; i++)
            {
                UserProfileData.PortfolioAssets[i].Percentage = 
                    (decimal)((UserProfileData.PortfolioAssets[i].Amount *
                    (decimal)charts.GetPrice(UserProfileData.PortfolioAssets[i].Symbol))
                    / ttl);
                UserProfileData.PortfolioAssets[i].StartAngle = startang;

                startang += (float)(UserProfileData.PortfolioAssets[i].Percentage * 360);
            }
        }

        public CurveSet GetNextCurve(int curveIndex)
        {
            string CurvePathBlock = "";
            string CurvePathLine = "";
            string CurevPathOuter = "";

            decimal ttl = GetTotal();

            UpdateValues();

            if (ttl > 0)
            {
                float per = 360 / totalSegments;
                float perAmount = (float)UserProfileData.PortfolioAssets[curveIndex].Percentage * 360;

                float totalAngle = 0;

                if (curveIndex == 0)
                {
                    lastAngle = startAngle;
                }

                float last = lastAngle;

                bool once = true;

                PointF InnerP1 = new PointF();
                PointF InnerP2 = new PointF();

                PointF OuterP1 = new PointF();
                PointF OuterP2 = new PointF();

                float cut = 1;

                if (curveIndex == UserProfileData.PortfolioAssets.Count - 1) cut = (spacing * (curveIndex + 1));

                while (totalAngle <= (int)(perAmount - (per * cut)))
                {
                    // get inner radius points on a circle
                    InnerP1 = GetCirclePoint(InnerRadius, lastAngle);
                    InnerP2 = GetCirclePoint(InnerRadius, MathUtil.GetNextAngle(lastAngle, per));

                    // get outer radius points on a circle
                    OuterP1 = GetCirclePoint(OuterRadius, lastAngle);
                    OuterP2 = GetCirclePoint(OuterRadius, MathUtil.GetNextAngle(lastAngle, per));

                    if (once) // only runs on the begining arc
                    {
                        string bx = BuildSVG.BoxValue(InnerP1, Center, wLimit, wLimit);

                        CurvePathBlock += bx;

                        CurvePathLine += bx;

                        once = false;
                    }

                    // update angle postion to new position
                    lastAngle = MathUtil.GetNextAngle(lastAngle, per);

                    CurvePathLine += BuildSVG.LineTo(InnerP2);

                    CurvePathBlock += BuildSVG.LineTo(InnerP2);

                    CurevPathOuter = BuildSVG.LineTo(OuterP1) + CurevPathOuter;// Line to

                    totalAngle += per;
                }

                // update circle points
                if (!once)
                {
                    if (curveIndex == UserProfileData.PortfolioAssets.Count - 1)
                    {
                        InnerP2 = GetCirclePoint(InnerRadius, 270f - (per/2));

                        OuterP1 = GetCirclePoint(OuterRadius, lastAngle);
                        OuterP2 = GetCirclePoint(OuterRadius, 270f - (per / 2));
                    }
                    else
                    {
                        InnerP2 = GetCirclePoint(InnerRadius, MathUtil.GetNextAngle(lastAngle, per));

                        OuterP1 = GetCirclePoint(OuterRadius, lastAngle);
                        OuterP2 = GetCirclePoint(OuterRadius, MathUtil.GetNextAngle(lastAngle, per));
                    }

                    CurvePathLine += BuildSVG.LineTo(InnerP2);

                    CurvePathBlock += BuildSVG.LineTo(InnerP2);

                    CurevPathOuter = BuildSVG.LineTo(OuterP1) + CurevPathOuter;

                    CurvePathBlock += BuildSVG.LineTo(OuterP2) + CurevPathOuter + " Z"; // close shape
                }

                // update angle to create a space between arcs
                lastAngle = MathUtil.GetNextAngle(lastAngle, per * spacing);

                totalAngle += per;
            }

            return new CurveSet(CurvePathBlock, CurvePathLine);
        }

       

        public string GetCircle()
        {
            string CurvePathLine = "";

            PointF InnerP1 = new PointF();

            for (float f = 0; f < 360; f += 0.36f)
            {
                InnerP1 = GetCirclePoint(InnerRadius, f);

                if(f == 0)
                {
                    CurvePathLine += BuildSVG.BoxValue(InnerP1, Center, wLimit, wLimit);
                }

                CurvePathLine += BuildSVG.LineTo(InnerP1);
            }

            InnerP1 = GetCirclePoint(InnerRadius, 0f);
            CurvePathLine += BuildSVG.LineTo(InnerP1.X, InnerP1.Y);

            CurvePathLine += " Z ";

            return CurvePathLine;
        }

        public string GetSubtractedCircle(float heightOverride = 0)
        {
            string CurvePathLine = "";

            PointF InnerP1 = new PointF();

            for (float f = 0; f < 360; f += 0.36f)
            {
                InnerP1 = GetCirclePoint(InnerRadius, f);

                if (f == 0)
                {
                    CurvePathLine += BuildSVG.MoveTo(InnerP1.X, InnerP1.Y);
                }

                CurvePathLine += BuildSVG.LineTo(InnerP1);
            }

            InnerP1 = GetCirclePoint(InnerRadius, 0f);

            CurvePathLine += BuildSVG.LineTo(InnerP1.X, InnerP1.Y);

            CurvePathLine += BuildSVG.LineTo(InnerP1.X, 0);
            CurvePathLine += BuildSVG.LineTo(0, 0);

            if(heightOverride > 0)
            {
                CurvePathLine += BuildSVG.LineTo(0, heightOverride);
                CurvePathLine += BuildSVG.LineTo(wLimit, heightOverride);
            }
            else
            {
                CurvePathLine += BuildSVG.LineTo(0, hLimit);
                CurvePathLine += BuildSVG.LineTo(wLimit, hLimit);
            }

            CurvePathLine += BuildSVG.LineTo(wLimit, 0);
            CurvePathLine += BuildSVG.LineTo(InnerP1.X, InnerP1.Y);
            CurvePathLine += " Z ";

            return CurvePathLine;
        }

        public PointF GetCirclePoint(float radius, float angle)
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
}
