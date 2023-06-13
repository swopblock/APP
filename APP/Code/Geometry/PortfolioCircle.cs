using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Code
{
    public class PortfolioCircle
    {
        public List<PortfolioAsset> Assets { get; set; }

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

            if (assets == null)
            {
                Assets = new List<PortfolioAsset>();
            }
            else
            {
                Assets = assets;

                UpdateValues();
            }
        }

        public float GetWidthLimit() { return wLimit;  }
        public float GetHeightLimit() { return hLimit; }    

        private decimal GetTotal()
        {
            decimal total = 0;

            for (int i = 0; i < Assets.Count; i++)
            {
                decimal price = (decimal)charts.GetPrice(Assets[i].Symbol);

                Assets[i].Price = price;

                total += (Assets[i].Amount * price);
            }

            return total;
        }

        private void UpdateValues()
        {
            decimal ttl = GetTotal();

            for (int i = 0; i < Assets.Count; i++)
            {
                Assets[i].Percentage = (decimal)((Assets[i].Amount *
                    (decimal)charts.GetPrice(Assets[i].Symbol))
                    / ttl);
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
                float perAmount = (float)Assets[curveIndex].Percentage * 360;

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

                if (curveIndex == Assets.Count - 1) cut = (spacing * (curveIndex + 1));

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
                    if (curveIndex == Assets.Count - 1)
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

        public string GetSubtractedCircle()
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
            CurvePathLine += BuildSVG.LineTo(0, wLimit);
            CurvePathLine += BuildSVG.LineTo(wLimit, wLimit);
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

        public static List<PortfolioAsset> LoadDemo()
        {
            return new List<PortfolioAsset>
                    {
                new PortfolioAsset
                {
                    Name = "Bitcoin",
                    Symbol = "BTC",
                    Amount = 1.56784m,
                    PurchasePrice = 10000,
                    HtmlColor = Color.FromArgb("#f2a900"),
                    Image = "bitcoin.png"
                },
                new PortfolioAsset
                {
                    Name = "Ethereum",
                    Symbol = "ETH",
                    Amount = 30.98765m,
                    PurchasePrice = 1000,
                    HtmlColor = Color.FromArgb("#6f89ff"),
                    Image = "ethereum.png"
                },
                new PortfolioAsset
                {
                    Name = "Swobble",
                    Symbol = "SWOBL",
                    Amount = 10000,
                    PurchasePrice = 0.5m,
                    HtmlColor = Color.FromArgb("#00dd00"),
                    Image = "swopblock.png"
                },
                new PortfolioAsset
                {
                    Name = "Swobble Rewards",
                    Symbol = "SWOBLR",
                    Amount = 2000,
                     PurchasePrice = 0.1m,
                    HtmlColor = Color.FromArgb("#ff00bc"),
                    Image = "swopblockreward.png"
                }
            };
        }
    }
}
