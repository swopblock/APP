using Microsoft.Maui.Controls.Shapes;
using APP.Code;
using APP.Code.Geometry;

namespace APP.Views;

public partial class CryptoCard : ContentView
{
    ChartContainer chartContainer = new ChartContainer();

    PortfolioCircle portfolioCircle = null;

    Rect rect = new Rect();
    Point centerPoint = new Point();

    int time = 300;
    int candleCount = 60;

    float width = 0;
    float height = 0;
    public CryptoCard()
	{
		InitializeComponent();

        new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = false;
            while (true)
            {
                Dispatcher.Dispatch(() =>
                {
                    UpdateSize();
                    MakeChart(portfolioCircle);
                });

                Thread.Sleep(5500);
            }
        }).Start();

      
    }

    public void UpdateSize()
    {
        float mx = 4;
        float my = 6;

        float mxH = mx / 2;
        float myH = my / 2;

        float w = 0;
        float h = 0;
        float dense = 1;

        if (width == 0 || height == 0)
        {
            try
            {
                width = (float)DeviceDisplay.Current.MainDisplayInfo.Width;
                height = (float)DeviceDisplay.Current.MainDisplayInfo.Height;

                dense = (float)DeviceDisplay.Current.MainDisplayInfo.Density;

                width /= dense;
                height /= dense;

                w = width;
                h = height;
            }
            catch
            {

            }
        }

        if(w != 0 || h != 0) 
        {      
            w /= mx;
            h /= mx;

                PointF cnt = new PointF(w * mxH, w * myH);

                PortfolioCircle portfolioShape = new PortfolioCircle
                (
                    cnt,
                    chartContainer,
                    PortfolioCircle.LoadDemo(),
                    w * mx, w * mx
                );

                portfolioShape.InnerRadius = w + (w * 0.8f);
                portfolioShape.OuterRadius = w + (w * 1.6f);

                rect = new Rect(
                    10,
                    0,
                    width,
                    width);

                portfolioCircle = portfolioShape;

                Dispatcher.Dispatch(() => {
                    ContainerForGrids.HeightRequest = width;
                    ContainerForGrids.WidthRequest = width;
                });
            
        }
    }

    public void MakeChart(PortfolioCircle portfolioShape)
    {
        if (portfolioShape != null)
        {
            PortfolioChart chart =
               new PortfolioChart(rect.Location,
                   portfolioShape.GetWidthLimit(),
                   portfolioShape.GetWidthLimit()
                   );

            Pack pk = null;

            List<Pack> packCandles = chartContainer.CombinePacks(
                portfolioShape.Assets.Select(x => x.Symbol).ToList(),
                time,
                candleCount);

            if (packCandles != null)
            {
                if (packCandles.Count > 0)
                {
                    List<Candle> cnds = chartContainer.SumTotal(portfolioShape.Assets, packCandles);

                    if (cnds.Count > 0)
                    {
                        CurveSet curv = chart.DrawLine(rect.Location, cnds);

                        if (!curv.Failed())
                        {
                            Color bk = Color.FromArgb("#000000");
                            Color st = Color.FromArgb("#FFFFFF");

                            Geometry pathFill =
                             (Geometry)new PathGeometryConverter()
                             .ConvertFromString(curv.GetCurve());//

                           // Microsoft.Maui.Controls.Shapes.Path lineFill = new(pathFill);

                            LinearGradientBrush brush = new LinearGradientBrush(
                            GetLineGradient(st, bk),
                            new Point(0.1, 0.1),
                            new Point(0.6, 0.9));

                            fillPath.Data = pathFill;
                            fillPath.Fill = brush;
                           // gridCrypto.Children.Add(lineFill);

                            Geometry pathData =
                               (Geometry)new PathGeometryConverter()
                               .ConvertFromString(curv.GetSpace());//

                           // Microsoft.Maui.Controls.Shapes.Path linepath = new(pathData);

                            linePath.Data = pathData;
                            linePath.Stroke = st;

                            //test2.Data = pathData;

                            //gridCrypto.Children.Add(linepath);
                        }
                    }
                }
            }
        }
    }
    public GradientStopCollection GetLineGradient(Color clr1, Color clr2)
    {
        GradientStopCollection collection = new GradientStopCollection
        {
            new GradientStop(clr1, 0f),
            new GradientStop(clr2, 0.5f)
        };

        return collection;
    }
}