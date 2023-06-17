using Microsoft.Maui.Controls.Shapes;
using APP.Code;
using APP.Code.Geometry;
using APP.Code.Data.User;

namespace APP.Views;

public partial class CryptoCard : ContentView
{
    ChartContainer chartContainer = new ChartContainer();

    PortfolioCircle portfolioCircle = null;

    PortfolioAsset assetDisplayed = null;

    Rect rect = new Rect();
    Point centerPoint = new Point();

    int time = 300;
    int candleCount = 60;

    float width = 0;
    float height = 0;
    public CryptoCard(PortfolioAsset asset)
	{
		InitializeComponent();

        assetDisplayed = asset; 

        BindingContext = asset;

        DragGestureRecognizer drag = new DragGestureRecognizer();
        DropGestureRecognizer drop = new DropGestureRecognizer();   
        TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.Tapped += Tap_Tapped;

        drag.DragStarting += Drag_DragStarting;
        drop.Drop += Drop_Drop;

        ContainerForGrids.GestureRecognizers.Add(drag);
        ContainerForGrids.GestureRecognizers.Add(drop);
        ContainerForGrids.GestureRecognizers.Add(tap);
    }

    private void Tap_Tapped(object sender, EventArgs e)
    {
        var page = new CurrencyPage(assetDisplayed);

        Application.Current.MainPage = page;
    }

    private void Drop_Drop(object sender, DropEventArgs e)
    {
        //var page = new CurrencyPage();

        //Application.Current.MainPage = page;
    }

    private void Drag_DropCompleted(object sender, DropCompletedEventArgs e)
    {
        //var page = new CurrencyPage();

        //Application.Current.MainPage = page;
    }

    private void Drag_DragStarting(object sender, DragStartingEventArgs e)
    {
        //throw new NotImplementedException();
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


        Circle.UpdateValue(assetDisplayed, 20);

        SetValueDirection(assetDisplayed.Price - assetDisplayed.PurchasePrice);

        decimal diff = (assetDisplayed.Price - assetDisplayed.PurchasePrice) / assetDisplayed.PurchasePrice;

        percentGain.Text = diff.ToString("0.00") + "%";

        BackgroundPanel.SetColor(assetDisplayed.HtmlColor);

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
                    UserProfileData.LoadDemo(),
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
                    Pack Selectedpack = packCandles.Where(x => x.Symbol == assetDisplayed.Symbol).FirstOrDefault();

                    if (Selectedpack != null)
                    {
                        List<Candle> cnds = Selectedpack.candles; //chartContainer.SumTotal(portfolioShape.Assets, packCandles);

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
    }

    public void SetValueDirection(decimal value)
    {
        if (value >= 0) 
        { 
            ValueUp.Opacity = 1;
            ValueDown.Opacity = 0;
        }
        else 
        { 
            ValueUp.Opacity = 0;
            ValueDown.Opacity = 1;
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