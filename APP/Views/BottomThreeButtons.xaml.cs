namespace APP.Views;

public partial class BottomThreeButtons : ContentView
{
	public BottomThreeButtons()
	{
		InitializeComponent();

		TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.Tapped += Tap_Tapped;

        TapGestureRecognizer cryptoTap = new TapGestureRecognizer();
        cryptoTap.Tapped += CryptoTap_Tapped;

        TapGestureRecognizer tradeTap = new TapGestureRecognizer();
        tradeTap.Tapped += TradeTap_Tapped;

        yourKeys.GestureRecognizers.Add(tap);
        yourCrypto.GestureRecognizers.Add(cryptoTap);
        yourTrade.GestureRecognizers.Add(tradeTap); 
    }

    private async void NavGoTo(string page)
    {
        await Shell.Current.GoToAsync(page);
    }
    private void TradeTap_Tapped(object sender, EventArgs e)
    {
        NavGoTo(nameof(TradeSelectPage));
        //Application.Current.MainPage = new TradeSelectPage();
    }

    private void CryptoTap_Tapped(object sender, EventArgs e)
    {
        string page = (string)BindingContext;

        if (page != "currency")
        {
            NavGoTo(nameof(CurrencyPage));
        }
        
    }

    private void Tap_Tapped(object sender, EventArgs e)
    {
        //Application.Current.MainPage = new MainPage();
        // NavGoTo(nameof(PortfolioPage));
        Shell.Current.Navigation.PopToRootAsync();
    }
}