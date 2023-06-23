using APP.Code;
using APP.Code.Data.User;
using APP.Views;
using System.ComponentModel;

namespace APP;

[QueryProperty(nameof(asset), nameof(asset))]
public partial class CurrencyPage : ContentPage
{
    [Bindable(true)]
    public string asset { get; set; }

    public Color UnselectColor = Color.FromArgb("#2F323A");
	public CurrencyPage()
	{
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        LoadCurrency(asset);
    }

    private void RectFirstTap(object sender, EventArgs e)
    {
       
    }
    private void RectOneTap(object sender, EventArgs e)
    {

    }
    private void RectTwoTap(object sender, EventArgs e)
    {

    }
    private void RectThreeTap(object sender, EventArgs e)
    {

    }

    public void LoadCurrency(string asset)
	{

        PortfolioAsset assetAsset = UserProfileData.PortfolioAssets
            .Where(x=>x.Symbol == asset).FirstOrDefault();

        if (assetAsset != null)
        {
            if(assetAsset.Symbol != "SWOBLR")
            {
                RectFirst.Source = "stargray.png";
               // RectFirst.Fill = UnselectColor;
            }
            
            if (assetAsset.Symbol != "SWOBL")
            {
                RectOne.Stroke = UnselectColor;
            }
            
            if (assetAsset.Symbol != "BTC")
            {
                RectTwo.Stroke = UnselectColor;
            }

            if (assetAsset.Symbol != "ETH")
            {
                RectThree.Stroke = UnselectColor;
            }

                btbuttons.currency = asset;

            CryptoCard card = new CryptoCard(assetAsset);

            CurrencyLabel.Text = assetAsset.Name;
            CurrencyLabel.TextColor = assetAsset.HtmlColor;
            CurencyNameBelow.Text = assetAsset.Name;
            CurencyNameBelow.TextColor = assetAsset.HtmlColor;

            Diversity.UpdateValue(assetAsset, 30);

            card.UpdateSize();
            card.MakeChart();

            CurrencyContainer.Children.Add(card);


        }
    }
}