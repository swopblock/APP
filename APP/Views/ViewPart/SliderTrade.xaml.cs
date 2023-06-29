using System.Diagnostics;
using Microsoft.Maui.Controls;
namespace APP.Views;

public partial class SliderTrade : ContentView
{
	double currentWidth = 50;

    double posX = 0;

	double dWidth = 0;
    double dense = 0;

    public Color HtmlColor = Color.FromArgb("#ffffff");
    public Color AltColor = Color.FromArgb("#aaaaaa");

    public string symbol = "";
    public string altSymbol = "";
	public SliderTrade()
	{
		InitializeComponent();

        GetWidth();

        FingerButton.Fill = HtmlColor;
	}

    private void GetWidth()
    {
        if (dWidth == 0)
        {
            try
            {
                dWidth = (float)DeviceDisplay.Current.MainDisplayInfo.Width;
                dense = (float)DeviceDisplay.Current.MainDisplayInfo.Density;
            }
            catch
            {

            }

            if (dWidth != 0 && dense != 0)
            {

                dWidth /= dense;

                SliderButton.WidthRequest = (dWidth - 40);
            }
        }
    }

  
    private void GraphicsView_DragInteraction(object sender, TouchEventArgs e)
    {
        GetWidth();

        if (e != null)
        {
            if(e.Touches != null)
            {
                if(e.Touches.Length > 0)
                {
                    PointF touch = e.Touches.First();

                    posX = touch.X;

                    FingerButton.Fill = HtmlColor;

                    SetBar();
                }
            }
        }
    }

    public void SetBar()
    {
        float half = (float)(dWidth / 2);

        float val = (float)(half - posX);
        float wth = val;

        if (wth < 0) wth = -wth;

        if(wth > half - 40) { wth = half - 40; }

        double amount = 0;
        string sym = symbol;

        if(posX < half)
        {
            FingerButton.Margin = new Thickness(0, 0, wth, 0);
            
            FingerButton.WidthRequest = wth + 40;

            FingerButton.Fill = HtmlColor;

            ImgFinger.Margin = new Thickness(0, 0, (wth)*2, 0);

            amount = 100 - (GetPercent(posX, (half / 5), half - (half / 20)) * 100);
        }
        else
        {
            FingerButton.Margin = new Thickness(wth, 0, 0, 0);

            FingerButton.WidthRequest = wth + 40;

            FingerButton.Fill = AltColor;

            ImgFinger.Margin = new Thickness((wth) * 2, 0, 0, 0);

            amount = GetPercent(posX, half + (half / 20), dWidth - (half / 4)) * 100;

            sym = altSymbol;
        }

        var Trade = (TradeAmountPage)Parent.Parent.Parent;
        Trade.UpdateAmount(amount/100, sym);
    }

    private double GetPercent(double xpos, double low, double high)
    {
        double total = (high - low);
        double relPos = xpos - low;

        if (relPos < 0) { relPos = 0; }
        if (relPos > total) {  relPos = total; }

        return relPos / total;
    }
}