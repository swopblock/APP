using APP.Code;
using System.Data;

namespace APP.Views;

public partial class TimePeriodLabels : ContentView
{
    public Color BasicColor = Color.FromArgb("#71868F");

    private PortfolioPage portPage = null;
    private CryptoCard cardPage = null;
    public TimePeriodLabels()
	{
		InitializeComponent();

        this.Loaded += TimePeriodLabels_Loaded;
    }

    private void TimePeriodLabels_Loaded(object sender, EventArgs e)
    {
        if (BindingContext != null)
        {
            BasicColor = (Color)BindingContext;

            ResetColors();

            bToday.TextColor = Colors.White;
        }

        if (portPage == null && cardPage == null)
        {

            try
            {
                portPage = (PortfolioPage)this.Parent.Parent.Parent.Parent;
            }
            catch
            {

            }

            try
            {
                cardPage = this.Parent.Parent.Parent as CryptoCard;
            }
            catch
            {

            }
        }
    }

    private void SetTime(int time, int count)
    {
        if(portPage != null) 
        {
            portPage.SetTime(time, count);
        }

        if(cardPage != null) 
        { 
            cardPage.SetTime(time, count);
        }
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        ResetColors();
        bLive.TextColor = Colors.White;
    }
    private void Button_Today(object sender, EventArgs e)
    {
        ResetColors();
        bToday.TextColor = Colors.White;

        SetTime((int)ExchangeData.CoinTimeScale.OneHour, 24);
    }

    private void Button_W(object sender, EventArgs e)
    {
        ResetColors();
        bW.TextColor = Colors.White;
        SetTime((int)ExchangeData.CoinTimeScale.SixHour, 24);
    }
    private void Button_M(object sender, EventArgs e)
    {
        ResetColors();
        bM.TextColor = Colors.White;
        SetTime((int)ExchangeData.CoinTimeScale.OneDay, 30);
    }
    private void Button_M3(object sender, EventArgs e)
    {
        ResetColors();
        bM3.TextColor = Colors.White;
        SetTime((int)ExchangeData.CoinTimeScale.OneDay, 90);
    }
    private void Button_Y(object sender, EventArgs e)
    {
        ResetColors();
        bY.TextColor = Colors.White;
        SetTime((int)ExchangeData.CoinTimeScale.OneDay, 360);
    }

    public void ResetColors()
    {
        bLive.TextColor = BasicColor;
        bToday.TextColor = BasicColor;
        bW.TextColor = BasicColor;
        bM.TextColor = BasicColor;
        bM3.TextColor = BasicColor;
        bY.TextColor = BasicColor;
    }
}