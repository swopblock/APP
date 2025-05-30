using APP.Code;
using APP.Code.Data.Orders;
using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;

namespace APP;

public partial class SendingOrderPage : ContentPage
{
    float angle = 270;
    int index = 0;
    float total = 0;
    float count = 0;

    bool went = false;

    WalletAsset asset = null;
    public SendingOrderPage()
	{
		InitializeComponent();

        went = false;

        this.Loaded += SendingOrderPage_Loaded;
    }

    private void SendingOrderPage_Loaded(object sender, EventArgs e)
    {
        StartTimer();

        asset = (WalletAsset)BindingContext;

        RadialGradientBrush Gbrush =
            new RadialGradientBrush(
                GetGradient(asset.HtmlColor),
                new Point(0.5,0.5), 1);

        LineCurve.Stroke = asset.HtmlColor;
        LightCurve.Fill = Gbrush;
    }



    private void StartTimer()
    {
        Dispatcher.StartTimer(TimeSpan.FromMilliseconds(50), () =>
        {
            count++;
            OnPropertyChanged(nameof(count));
            if(count < 300)
            return true;
            else
            {
                return false;
            }
        });
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        if (index < StaticTest.GeometryContainer.LoadingCircle.Count 
            && index < StaticTest.GeometryContainer.LoadingLine.Count)
        {
            LightCurve.Data = StaticTest.GeometryContainer.LoadingCircle[index];
            LineCurve.Data = StaticTest.GeometryContainer.LoadingLine[index];

            index++;
        }
        else
        {
            if (!went)
            {
                Navigation.PushAsync(new OrderCompletePage());
                went = true;
            }
        }
    }
    public GradientStopCollection GetGradient(Color clr)
    {
        Color clrh = clr.MultiplyAlpha(0.4f);
        Color clrm = clr.MultiplyAlpha(0.1f);

        GradientStopCollection collection = new GradientStopCollection
        {
            new GradientStop(clr, 0f),
            new GradientStop(clr, 0.2f),
            new GradientStop(clrh, 0.34f),
            new GradientStop(clrm, 0.5f),
            new GradientStop(Colors.Transparent, 0.65f),
        };

        return collection;
    }

}