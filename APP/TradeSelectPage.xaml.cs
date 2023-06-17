namespace APP;

public partial class TradeSelectPage : ContentPage
{
	public TradeSelectPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
		Application.Current.MainPage = new TradeAmountPage();
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        Application.Current.MainPage = new TradeAmountPage();
    }
}