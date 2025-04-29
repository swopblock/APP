using APP.Code;
using APP.Code.Data.User;
using Microsoft.Maui.Graphics.Text;

namespace APP.Views;

public partial class BottomThreeButtons : ContentView
{
    public string market = "";
	public BottomThreeButtons()
	{
		InitializeComponent();

        this.Loaded += BottomThreeButtons_Loaded;

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

    private void BottomThreeButtons_Loaded(object sender, EventArgs e)
    {
        string value = (string)BindingContext;

        if(value == "market")
        {
            cryptoImage.Source = "circle_graph.png";
            cryptoLabel.TextColor = Color.FromArgb("#ffffff");
        }
        else if (value == "trade")
        {
            tradeImage.Source = "trade.png";
            tradeLabel.TextColor = Color.FromArgb("#ffffff");
        }
        else
        {
            keysImage.Source = "key.png";
            keyLabel.TextColor = Color.FromArgb("#ffffff");
        }
    }
    private void TradeTap_Tapped(object sender, EventArgs e)
    {
        WalletAsset asset = UserProfileData.LoadDemo().Where(x=>x.Symbol == market).FirstOrDefault();

        OrderPage page = new OrderPage();
        page.BindingContext = asset;

        Navigation.PushAsync(page);
    }

    private void CryptoTap_Tapped(object sender, EventArgs e)
    {
        string page = (string)BindingContext;

        if (page != "market")
        {
            Navigation.PushAsync(new MarketPage());
        }
        
    }

    private void Tap_Tapped(object sender, EventArgs e)
    {
        //Application.Current.MainPage = new MainPage();
        // NavGoTo(nameof(WalletPage));
        Navigation.PopToRootAsync();
    }
}