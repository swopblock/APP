using System.Diagnostics;
using Microsoft.Maui.Controls;
namespace APP.Views;

public partial class SliderTrade : ContentView
{
	double currentWidth = 50;

    double posX = 0;

	double dWidth = 0;
    double dense = 0;

    string HtmlColor = "#ffffff";
	public SliderTrade()
	{
		InitializeComponent();

        GetWidth();

        FingerButton.Fill = Color.FromArgb(HtmlColor);
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

                    FingerButton.Fill = Color.FromArgb(HtmlColor);

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
        if(wth < 20) { wth = 20; }

        if(wth > half - 30) { wth = half - 30; }

        if(posX < half)
        {
            FingerButton.Margin = new Thickness(0, 0, wth - 20, 0);
            
            FingerButton.WidthRequest = wth + 40;

            ImgFinger.Margin = new Thickness(0, 0, (wth- 10)*2, 0);
        }
        else
        {
            FingerButton.Margin = new Thickness(wth - 20, 0, 0, 0);

            FingerButton.WidthRequest = wth + 40;


            ImgFinger.Margin = new Thickness((wth - 10) * 2, 0, 0, 0);
        }
       

        Debug.WriteLine(half);
    }
}