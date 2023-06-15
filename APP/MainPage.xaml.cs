using APP.Code;
using APP.Code.Data.User;
using APP.Code.Geometry;
using APP.Views;
using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;

namespace APP;

public partial class MainPage : ContentPage
{
    PortfolioCircle portfolioCircle = null;

    ChartContainer chartContainer = new ChartContainer();

    Rect rect = new Rect();
    Point centerPoint = new Point();

    public string TimeValue = "";

    int time = 300;
    int candleCount = 60;

    float width = 0;
    float height = 0;

    bool added = false;

    public MainPage()
	{
		InitializeComponent();

        button.Clicked += Button_Clicked;

        //NavigationPage.SetHasNavigationBar(this, false);
        //Shell.SetTabBarIsVisible(this, false);

        Application.Current.UserAppTheme = AppTheme.Dark;

        StartTimer();

        new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;

            while (true)
            {
                Dispatcher.Dispatch(() =>
                {
                    UpdateSize();
                    UpdateCircle(portfolioCircle, rect, centerPoint);

                    if (!added)
                    {
                        AddWalletAssets();
                    }
                });

                Thread.Sleep(2000);
            }

        });//.Start();
    }
    private void StartTimer()
    {
        Dispatcher.StartTimer(TimeSpan.FromSeconds(3), () =>
        {
            TimeValue = DateTime.Now.ToString("HH:mm:ss");
            OnPropertyChanged(nameof(TimeValue));
            return true; // Return true to keep the timer running
        });
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PortfolioAmount.Text = TimeValue;
        UpdateSize();
        UpdateCircle(portfolioCircle, rect, centerPoint);

        if (!added)
        {
            AddWalletAssets();
        }
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void AddWalletAssets()
    {
        if (portfolioCircle != null)
        {
            int row = 0;

            WalletCards.Children.Clear();

            added = true;

            WalletCards.HeightRequest = portfolioCircle.Assets.Count * 100;

            foreach (PortfolioAsset set in portfolioCircle.Assets)
            {
                CryptoCard card = new CryptoCard(set);

                card.Padding = new Thickness(0, 180 * row, 0, 0);
                card.ZIndex = row++;

                ///card.GestureRecognizers.Add(gesture)

                card.UpdateSize();
                //card.MakeChart(portfolioCircle);

                WalletCards.Children.Add(card);

                if(set.Price == 0) added = false;
            }

           //  
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        //var page = new OrderDetailsPage();

        //Application.Current.MainPage = page;
    }

    public void UpdateSize()
    {
        float mx = 4;
        float my = 6;

        float mxH = mx / 2;
        float myH = my / 2;

        float w = 0;
        float h = 0;

        try
        {
            w = (float)DeviceDisplay.Current.MainDisplayInfo.Width;
            h = (float)DeviceDisplay.Current.MainDisplayInfo.Height;
        }
        catch
        {

        }

        if (w != 0 || h != 0)
        {
            w /= mx;
            h /= mx;

            w /= (float)DeviceDisplay.Current.MainDisplayInfo.Density;
            h /= (float)DeviceDisplay.Current.MainDisplayInfo.Density;

            if (w != width || height != h)
            {
                width = w;
                height = h;

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

                double cbrx = 0.5f;// cnt.X / (w * mx);
                double cbry = 0.525f;//cnt.Y / (w * my);

                centerPoint = new Point(cbrx, cbry);

                rect = new Rect(
                    1,
                    cnt.Y - (portfolioShape.InnerRadius * 0.5f),
                    portfolioShape.InnerRadius,
                    portfolioShape.InnerRadius);

                portfolioCircle = portfolioShape;
            }
        }
    }

    public void UpdateCircle(PortfolioCircle portfolioShape, Rect rect, Point Center)
    {
        if(grid.Children != null)
            grid.Children.Clear();

        MakeBackCircle();

        MakeChart(portfolioShape);

        MakeBackground();

        for (int i = 0; i < portfolioShape.Assets.Count; i++)
        {
            CurveSet curv = portfolioShape.GetNextCurve(i);

            MakeCircle(curv, Center, portfolioShape.Assets[i]);
        }
    }

    public void MakeBackground()
    {
        Geometry pathBackground =
       (Geometry)new PathGeometryConverter()
       .ConvertFromString(portfolioCircle.GetSubtractedCircle());

        Microsoft.Maui.Controls.Shapes.Path pathBack = new(pathBackground);

        Color bk = Color.FromArgb("#000000");

        pathBack.Fill = bk;

        pathBack.ZIndex = 0;

        grid.Children.Add(pathBack);
    }
    public void MakeBackCircle()
    {
        Geometry pathBackground =
       (Geometry)new PathGeometryConverter()
       .ConvertFromString(portfolioCircle.GetCircle());

        Microsoft.Maui.Controls.Shapes.Path pathBack = new(pathBackground);

        Color bk = Color.FromArgb("#000000");
        Color st = Color.FromArgb("#FFFFFF");

        LinearGradientBrush brush = new LinearGradientBrush(
            GetLineGradient(st, bk), 
            new Point(0, 0), 
            new Point(0.75f, 0.75f));

        pathBack.Fill = brush;

        pathBack.ZIndex = 0;

        grid.Children.Add(pathBack);
    }

    public void MakeChart(PortfolioCircle portfolioShape)
    {
        PortfolioChart chart =
           new PortfolioChart(portfolioShape.Center,
               portfolioShape.GetWidthLimit(),
               portfolioShape.GetHeightLimit()
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
                    Candle last = cnds.LastOrDefault();
                    if (last != null)
                    {
                        double total = last.close;

                        PortfolioAmount.Text = total.ToString("c");
                    }
                    CurveSet curv = chart.DrawLine(rect.Location, cnds);

                    if (!curv.Failed())
                    {
                        Color bk = Color.FromArgb("#000000");
                        Color st = Color.FromArgb("#FFFFFF");

                        Geometry pathFill =
                         (Geometry)new PathGeometryConverter()
                         .ConvertFromString(curv.GetCurve());

                        Microsoft.Maui.Controls.Shapes.Path lineFill = new(pathFill);

                        LinearGradientBrush brush = new LinearGradientBrush(
                        GetLineGradient(st, bk),
                        new Point(0.5, 0.4),
                        new Point(0.6, 0.9));

                        lineFill.Fill = brush;

                        lineFill.ZIndex = 0;

                        grid.Children.Add(lineFill);

                        Geometry pathData =
                           (Geometry)new PathGeometryConverter()
                           .ConvertFromString(curv.GetSpace());

                        Microsoft.Maui.Controls.Shapes.Path linepath = new(pathData);

                        linepath.Stroke = st;
                        linepath.StrokeThickness = 2;
                        linepath.ZIndex = 0;

                        grid.Children.Add(linepath);
                    }
                }
            }
        }
    }
    public void MakeCircle(CurveSet Set, Point Center, PortfolioAsset asset)
    {
        string path = Set.GetCurve();
        string spc = Set.GetSpace();

        Geometry pathData =
            (Geometry)new PathGeometryConverter()
            .ConvertFromString(path);

        Microsoft.Maui.Controls.Shapes.Path pathTest = new(pathData);

        Color clr = asset.HtmlColor;

        RadialGradientBrush Gbrush =
            new RadialGradientBrush(GetGradient(clr), Center, 1);

        pathTest.Fill = Gbrush;
        pathTest.ZIndex = 0;

        Geometry lineData =
           (Geometry)new PathGeometryConverter()
           .ConvertFromString(spc);

        Microsoft.Maui.Controls.Shapes.Path pathLine = new(lineData);

        pathLine.Stroke = clr;
        pathLine.ZIndex = 0;

        grid.Children.Add(pathTest);
        grid.Children.Add(pathLine);
    }


    public GradientStopCollection GetGradient(Color clr)
    {
        Color clrh = clr.MultiplyAlpha(0.4f);
        Color clrm = clr.MultiplyAlpha(0.1f);

        GradientStopCollection collection = new GradientStopCollection
        {
            new GradientStop(clr, 0f),
            new GradientStop(clr, 0.1f),
            new GradientStop(clrh, 0.24f),
            new GradientStop(clrm, 0.4f),
            new GradientStop(Colors.Transparent, 0.45f),
        };

        return collection;
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

