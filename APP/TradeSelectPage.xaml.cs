using APP.Code.Data.Orders;

namespace APP;

public partial class TradeSelectPage : ContentPage
{
	public TradeSelectPage()
	{
		InitializeComponent();
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
		//Application.Current.MainPage = new TradeAmountPage();
        NavGoTo(nameof(TradeAmountPage));
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        //Application.Current.MainPage = new TradeAmountPage();
        NavGoTo(nameof(TradeAmountPage));
    }

    private async void NavGoTo(string page)
    {
        await Shell.Current.GoToAsync(page);
    }
}