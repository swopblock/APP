using APP.Code;
using APP.Code.Data.Orders;
using APP.Code.Data.User;

namespace APP;

public partial class TradeSelectPage : ContentPage
{
	public TradeSelectPage()
	{
		InitializeComponent();
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        PortfolioAsset asset = UserProfileData.LoadDemo().Where(x => x.Symbol == "BTC").FirstOrDefault();
        if (asset != null)
        {
            TradeAmountPage page = new TradeAmountPage(asset);
            page.BindingContext = asset;
            Navigation.PushAsync(page);
        }
        //NavGoTo(nameof(TradeAmountPage));
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