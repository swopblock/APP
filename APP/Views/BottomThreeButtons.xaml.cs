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

    private void TradeTap_Tapped(object sender, EventArgs e)
    {
        Application.Current.MainPage = new TradeSelectPage();
    }

    private void CryptoTap_Tapped(object sender, EventArgs e)
    {
        Application.Current.MainPage = new CurrencyPage(new Code.PortfolioAsset());
    }

    private void Tap_Tapped(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();
    }
}