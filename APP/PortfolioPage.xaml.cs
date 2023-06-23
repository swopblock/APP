using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;
using APP.Code;
using APP.Code.Data.User;
using APP.Views;
using APP.Code.Data.Orders;
using System.Diagnostics;

namespace APP;

public partial class PortfolioPage : ContentPage
{
    PortfolioCircle portfolioCircle = null;

    ChartContainer chartContainer = new ChartContainer();

    Rect rect = new Rect();
    Point centerPoint = new Point();

    public string TimeValue = "";

    bool currentPage = true;

    int time = 300;
    int candleCount = 60;

    float width = 0;
    float height = 0;

    bool added = false;

    int timerState = 0;

    public PortfolioPage()
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Dark;

        currentPage = true;

        this.NavigatedFrom += PortfolioPage_NavigatedFrom;

        //Application.Current.MainPage.Title =


        StartTimer();
    }

    private void PortfolioPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        currentPage = false;
    }

    private void StartTimer()
    {
        UpdateSize();

        chartContainer.StartData();

        UpdateValues();

        AddWalletAssets();

        //Dispatcher.StartTimer(TimeSpan.FromSeconds(5), () =>
        //{
        //    TimeValue = DateTime.Now.ToString("HH:mm:ss");
        //    OnPropertyChanged(nameof(TimeValue));

        //    return false;// currentPage; // Return true to keep the timer running
        //});
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        Dispatcher.Dispatch(() =>
        {
            UpdateValues();
        });

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void UpdateValues()
    {
        if (PortfolioAmount != null)
        {
            UpdateCircle(portfolioCircle, rect, centerPoint);

            //if (timerState == 0)
            {
                AddWalletAssets();
            }
        }
    }

    public void AddWalletAssets()
    {
        if (portfolioCircle != null)
        {
            int row = 0;

            Grid tempgrid = new Grid();

            WalletCards.Children.Clear();

            added = true;

            double val = UserProfileData.PortfolioAssets.Count * 100;

            if (WalletCards.HeightRequest != val)
            {
                WalletCards.HeightRequest = val;
            }

            foreach (PortfolioAsset set in UserProfileData.PortfolioAssets)
            {
                CryptoCard card = new CryptoCard(set);

                card.Padding = new Thickness(0, 180 * row, 0, 0);

                card.ZIndex = row++;

                card.EnableClickable();

                card.UpdateSize();

                tempgrid.Children.Add(card);

                if (set.Price == 0) added = false;
            }

            WalletCards.Children.Add(tempgrid);

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
        if (portfolioShape != null)
        {
            if (grid.Children != null)
                grid.Children.Clear();

            MakeBackCircle();

            MakeChart(portfolioShape);

            MakeBackground();

            for (int i = 0; i < UserProfileData.PortfolioAssets.Count; i++)
            {
                CurveSet curv = portfolioShape.GetNextCurve(i);

                MakeCircle(curv, Center, UserProfileData.PortfolioAssets[i]);
            }
        }
    }

    public void MakeBackground()
    {
        if (portfolioCircle != null)
        {
            try
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
            catch { }
        }
    }
    public void MakeBackCircle()
    {
        try
        {
            if (portfolioCircle != null)
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
        }
        catch { }
    }

    public void MakeChart(PortfolioCircle portfolioShape)
    {
        if (portfolioShape != null)
        {
            PortfolioChart chart =
               new PortfolioChart(portfolioShape.Center,
                   portfolioShape.GetWidthLimit(),
                   portfolioShape.GetHeightLimit()
                   );

            Pack pk = null;

            List<Pack> packCandles = chartContainer.CombinePacks(
                UserProfileData.PortfolioAssets.Select(x => x.Symbol).ToList(),
                time,
                candleCount);

            if (packCandles != null)
            {
                if (packCandles.Count > 0)
                {
                    List<Candle> cnds = chartContainer.SumTotal(UserProfileData.PortfolioAssets, packCandles);

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