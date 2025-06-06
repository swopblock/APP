using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;
using APP.Code;
using APP.Code.Data.User;
using APP.Views;
using APP.Code.Data.Orders;
using System.Diagnostics;

namespace APP;

public partial class WalletPage : ContentPage
{
    WalletCircle walletCircle = null;

    ChartContainer chartContainer = new ChartContainer();

    Rect rect = new Rect();
    Point centerPoint = new Point();

    public string TimeValue = "";

    bool currentPage = true;

    int time = (int)ExchangeData.CoinTimeScale.OneHour;
    int candleCount = 24;

    float width = 0;
    float height = 0;

    bool reloadable = true;

    bool added = false;

    int timerState = 0;

    public WalletPage()
    {
        InitializeComponent();

        Application.Current.UserAppTheme = AppTheme.Dark;

        currentPage = true;

        this.Loaded += WalletPage_Loaded;

        this.NavigatedTo += WalletPage_NavigatedTo;

        this.NavigatedFrom += WalletPage_NavigatedFrom;

        //Application.Current.MainPage.Title =

        if (reloadable)
        {
            StartTimer();
            reloadable = false;
        }
    }

    private void WalletPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {

    }

    private void WalletPage_Loaded(object sender, EventArgs e)
    {
        //Debug.WriteLine("");
    }

    private void WalletPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        currentPage = false;
    }

    private void StartTimer()
    {
        UpdateSize();

        chartContainer.StartData();

        UpdateValues();

        AddWalletAssets();

        Dispatcher.StartTimer(TimeSpan.FromSeconds(5), () =>
        {
            TimeValue = DateTime.Now.ToString("HH:mm:ss");
            OnPropertyChanged(nameof(TimeValue));

            return false;// currentPage; // Return true to keep the timer running
        });
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
        if (WalletAmount != null)
        {
            UpdateCircle(walletCircle, rect, centerPoint);

            if (timerState == 0)
            {
                AddWalletAssets();
            }
        }
    }

    public void AddWalletAssets()
    {
        if (walletCircle != null)
        {
            int row = 0;

            Grid tempgrid = new Grid();

            WalletCards.Children.Clear();

            added = true;

            double val = UserProfileData.WalletAssets.Count * 100;

            if (WalletCards.HeightRequest != val)
            {
                WalletCards.HeightRequest = val;
            }

            foreach (WalletAsset set in UserProfileData.WalletAssets)
            {
                CryptoCard card = new CryptoCard();

                card.BindingContext = set;

                card.Padding = new Thickness(0, 180 * row, 0, 0);

                card.ZIndex = row++;

                card.EnableClickable();

                tempgrid.Children.Add(card);

                if (set.Price == 0) added = false;
            }

            WalletCards.Children.Add(tempgrid);
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

                WalletCircle walletShape = new WalletCircle
                (
                    cnt,
                    chartContainer,
                    UserProfileData.LoadDemo(),
                    w * mx, w * mx
                );

                walletShape.InnerRadius = w + (w * 0.8f);
                walletShape.OuterRadius = w + (w * 1.9f);

                double cbrx = 0.5f;// cnt.X / (w * mx);
                double cbry = 0.45f;//cnt.Y / (w * my);

                centerPoint = new Point(cbrx, cbry);

                rect = new Rect(
                    10,
                    cnt.Y - (walletShape.InnerRadius * 0.5f),
                    walletShape.InnerRadius,
                    walletShape.InnerRadius);

                walletCircle = walletShape;
            }
        }
    }

    public void UpdateCircle(WalletCircle walletShape, Rect rect, Point Center, double mx = -1, double my = -1)
    {
        if (walletShape != null)
        {
            if (grid.Children != null)
                grid.Children.Clear();

            MakeBackCircle();

            MakeChart(walletShape, mx, my);

            MakeBackground();

            for (int i = 0; i < UserProfileData.WalletAssets.Count; i++)
            {
                CurveSet curv = walletShape.GetNextCurve(i);

                MakeCircle(curv, Center, UserProfileData.WalletAssets[i]);
            }
        }
    }

    public void MakeBackground()
    {
        if (walletCircle != null)
        {
            try
            {
                Geometry pathBackground =
               (Geometry)new PathGeometryConverter()
               .ConvertFromString(walletCircle.GetSubtractedCircle(height * 3));

                Microsoft.Maui.Controls.Shapes.Path pathBack = new(pathBackground);

                Color bk = Color.FromArgb("#000000");

                pathBack.Fill = bk;

                pathBack.ZIndex = 8;

                grid.Children.Add(pathBack);
            }
            catch { }
        }
    }
    public void MakeBackCircle()
    {
        try
        {
            if (walletCircle != null)
            {
                Geometry pathBackground =
               (Geometry)new PathGeometryConverter()
               .ConvertFromString(walletCircle.GetCircle());

                Microsoft.Maui.Controls.Shapes.Path pathBack = new(pathBackground);

                Color bk = Color.FromArgb("#000000");
                Color st = Color.FromArgb("#FFFFFF");

                LinearGradientBrush brush = new LinearGradientBrush(
                    GetLineGradient(st, bk),
                    new Point(-0.44, -0.44),
                    new Point(1.55, 1.55));

                pathBack.Fill = brush;

                pathBack.ZIndex = 0;

                grid.Children.Add(pathBack);
            }
        }
        catch { }
    }

    public void MakeChart(WalletCircle walletShape, double mx = -1, double my = -1)
    {
        if (walletShape != null)
        {
            WalletChart chart =
               new WalletChart(walletShape.Center,
                   walletShape.GetWidthLimit(),
                   walletShape.GetHeightLimit()
                   );

            chart.SetFingerPosition(mx, my);

            Pack pk = null;

            List<Pack> packCandles = chartContainer.CombinePacks(
                UserProfileData.WalletAssets.Select(x => x.Symbol).ToList(),
                time,
                candleCount);

            if (packCandles != null)
            {
                if (packCandles.Count > 0)
                {
                    List<Candle> cnds = chartContainer.SumTotal(UserProfileData.WalletAssets, packCandles);

                    if (cnds.Count > 0)
                    {
                        Candle last = cnds.LastOrDefault();
                        if (last != null)
                        {
                            double total = last.close;

                            WalletAmount.Text = total.ToString("c");
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

                            double start = cnds.First().close;
                            double end = cnds.Last().close;

                            double ratio = start / end;

                            Point first = new Point(0.3, 0.3);
                            Point second = new Point(0.8, 1.15);

                            //if (ratio > 1) // start is larger than end
                            //{
                            //    first = new Point(0.5, 0.5);
                            //    second = new Point(1.1, 1.1);
                            //}
                            //else
                            //{
                            //    first = new Point(0.5, 0.5);
                            //    second = new Point(1.1, 1.1);
                            //}

                            LinearGradientBrush brush = new LinearGradientBrush(
                            GetLineGradient(st, bk),
                            first,
                            second);

                            lineFill.Fill = brush;
                            //lineFill.SetValue(TitleProperty, "test");

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

                            LinearGradientBrush downbrush = new LinearGradientBrush(
                           GetLineGradient(st, bk),
                           new Point(0, 0.4),
                           new Point(0, 1.1));

                            Geometry downData =
                               (Geometry)new PathGeometryConverter()
                               .ConvertFromString(curv.GetLineDown());

                            Microsoft.Maui.Controls.Shapes.Path linedown = new(downData);

                           // linedown.Stroke = downbrush;
                            linedown.Fill = downbrush;
                            linedown.ZIndex = 0;

                            grid.Children.Add(linedown);

                            Geometry arcData =
                              (Geometry)new PathGeometryConverter()
                              .ConvertFromString(curv.GetArc());

                            Microsoft.Maui.Controls.Shapes.Path arcline = new(arcData);

                            arcline.Stroke = Colors.White;
                            arcline.StrokeThickness = 2;
                            arcline.ZIndex = 0;

                            grid.Children.Add(arcline);
                        }
                    }
                }
            }
        }
    }
    public void MakeCircle(CurveSet Set, Point Center, WalletAsset asset)
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

        pathTest.Fill = Gbrush; //Color.FromArgb("#55993333");// Gbrush;
        pathTest.ZIndex = 12;

        Geometry lineData =
           (Geometry)new PathGeometryConverter()
           .ConvertFromString(spc);

        Microsoft.Maui.Controls.Shapes.Path pathLine = new(lineData);

        pathLine.Stroke = clr;
        pathLine.ZIndex = 15;

        grid.Children.Add(pathTest);
        grid.Children.Add(pathLine);

        Debug.WriteLine(asset.Name);
    }

    public void SetTime(int timePer, int candlesNumber)
    {
        time = timePer;
        candleCount = candlesNumber;

        if (walletCircle != null)
        {
            UpdateCircle(walletCircle, rect, centerPoint);
        }
    }


    public GradientStopCollection GetGradient(Color clr)
    {
        Color clr1 = clr.MultiplyAlpha(0.91f);
        Color clr2 = clr.MultiplyAlpha(0.67f);
        Color clr3 = clr.MultiplyAlpha(0.47f);
        Color clr4 = clr.MultiplyAlpha(0.30f);
        Color clr5 = clr.MultiplyAlpha(0.17f);
        Color clr6 = clr.MultiplyAlpha(0.08f);
        Color clr7 = clr.MultiplyAlpha(0.02f);

        GradientStopCollection collection = new GradientStopCollection
        {
            new GradientStop(clr, 0.12f),
            new GradientStop(clr1, 0.14f),
            new GradientStop(clr2, 0.19f),
            new GradientStop(clr3, 0.25f),
            new GradientStop(clr4, 0.30f),
            new GradientStop(clr5, 0.35f),
            new GradientStop(clr6, 0.40f),
            new GradientStop(clr7, 0.44f),
            new GradientStop(Colors.Transparent, 0.48f),
        };

        /*
         * <radialGradient xmlns="http://www.w3.org/2000/svg" id="radial-gradient-5" cx="108.38" cy="230.55" fx="108.38" fy="230.55" r="154.06" gradientUnits="userSpaceOnUse">
         * <stop offset=".57" stop-color="#f2a900"/>
         * <stop offset=".59" stop-color="#f2a900" stop-opacity=".91"/>
         * <stop offset=".64" stop-color="#f2a900" stop-opacity=".67"/>
         * <stop offset=".7" stop-color="#f2a900" stop-opacity=".47"/>
         * <stop offset=".75" stop-color="#f2a900" stop-opacity=".3"/>
         * <stop offset=".8" stop-color="#f2a900" stop-opacity=".17"/>
         * <stop offset=".85" stop-color="#f2a900" stop-opacity=".08"/>
         * <stop offset=".89" stop-color="#f2a900" stop-opacity=".02"/>
         * <stop offset=".93" stop-color="#f2a900" stop-opacity="0"/>
         * </radialGradient>
         */

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

    private void GraphicsView_DragInteraction(object sender, TouchEventArgs e)
    {
        if (e != null)
        {
            if (e.Touches != null)
            {
                if (e.Touches.Length > 0)
                {
                    PointF touch = e.Touches.FirstOrDefault();

                    if (WalletAmount != null)
                    {
                        UpdateCircle(walletCircle, rect, centerPoint, touch.X, touch.Y);
                    }
                }
            }
        }
    }
}