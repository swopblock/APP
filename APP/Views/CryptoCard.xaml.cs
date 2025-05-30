using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Controls;
using APP.Code;
using APP.Code.Geometry;
using APP.Code.Data.User;

namespace APP.Views;

public partial class CryptoCard : ContentView
{
    ChartContainer chartContainer = new ChartContainer();

    WalletAsset assetDisplayed = null;

    bool isClickable = false;
    bool ShowGain = true;

    Rect rect = new Rect();
    Point centerPoint = new Point();

    int time = (int)ExchangeData.CoinTimeScale.OneHour;
    int candleCount = 24;

    float width = 0;
    float height = 0;
    public CryptoCard()
	{
		InitializeComponent();

        this.Loaded += CryptoCard_Loaded;

        TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.Tapped += Tap_Tapped;

        ContainerForGrids.GestureRecognizers.Add(tap);
    }

    private void CryptoCard_Loaded(object sender, EventArgs e)
    {
        WalletAsset asset = (WalletAsset)BindingContext;

        if (asset != null)
        {
            if (!isClickable)
            {
                try
                {
                    MarketPage page = (MarketPage)Parent.Parent.Parent.Parent.Parent.Parent;

                    if (page != null)
                    {
                        page.LoadMarket(asset.Symbol);
                    }
                }
                catch { }
                }

            if (asset.Symbol == "YOUR REWARDS") ShowGain = false;

            if(!ShowGain)
            {
                gainStack.Opacity = 0;
            }

            assetDisplayed = UserProfileData.WalletAssets.Where(x=>x.Symbol == asset.Symbol).FirstOrDefault();

            if (assetDisplayed != null)
            {
                BindingContext = assetDisplayed;

                UpdateSize();

                if (!isClickable)
                {
                    MakeChart();
                }
            }
        }
    }

    public void SetTime(int timePer, int candlesNumber)
    {
        time = timePer;
        candleCount = candlesNumber;

        MakeChart();
    }

    public void EnableClickable()
    {
        isClickable = true;
    }

    private void ShowTransientPage()
    {
        if (isClickable)
        {
            MarketPage page = new MarketPage();
            page.asset = assetDisplayed.Symbol;

            Navigation.PushAsync(page);
        }
    }

    private void Tap_Tapped(object sender, EventArgs e)
    {
        ShowTransientPage();
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

        if (isClickable)
        {
            Circle.UpdateValue(assetDisplayed, 25, assetDisplayed.StartAngle);
        }
        else
        {
            Circle.Opacity = 0;
        }

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

                rect = new Rect(
                    10,
                    0,
                    width,
                    width);

                Dispatcher.Dispatch(() => {
                    ContainerForGrids.HeightRequest = width;
                    ContainerForGrids.WidthRequest = width;
                });
            
        }
    }

    public void MakeChart()
    {
        WalletChart chart =
           new WalletChart(rect.Location,
               width * 1.28f,
               height * 0.5f
               );

        Pack pk = null;

        List<Pack> packCandles = chartContainer.CombinePacks(
            UserProfileData.WalletAssets.Select(x => x.Symbol).ToList(),
            time,
            candleCount);

        if (packCandles != null)
        {
            if (packCandles.Count > 0)
            {
                Pack Selectedpack = packCandles.Where(x => x.Symbol == assetDisplayed.Symbol).FirstOrDefault();

                if (Selectedpack != null)
                {
                    List<Candle> cnds = Selectedpack.candles; //chartContainer.SumTotal(walletShape.Assets, packCandles);

                    if (cnds.Count > 0)
                    {
                        CurveSet curv = chart.DrawLine(rect.Location, cnds);

                        if (!curv.Failed())
                        {
                            Color bk = Color.FromArgb("#000000");
                            Color st = assetDisplayed.HtmlColor;

                            Geometry pathFill =
                             (Geometry)new PathGeometryConverter()
                             .ConvertFromString(curv.GetCurve());

                            LinearGradientBrush brush = new LinearGradientBrush(
                            GetLineGradient(st, bk),
                            new Point(0.2, 0.2),
                            new Point(0.5, 1.55));

                            fillPath.Data = pathFill;
                            fillPath.Fill = brush;

                            Geometry pathData =
                               (Geometry)new PathGeometryConverter()
                               .ConvertFromString(curv.GetSpace());//

                            linePath.Data = pathData;
                            linePath.Stroke = st;
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