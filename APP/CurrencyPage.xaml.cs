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
	public CurrencyPage()
	{
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        LoadCurrency(asset);
    }

    public void LoadCurrency(string asset)
	{

        PortfolioAsset assetAsset = UserProfileData.PortfolioAssets
            .Where(x=>x.Symbol == asset).FirstOrDefault();

        if (assetAsset != null)
        {
            
            CryptoCard card = new CryptoCard(assetAsset);

            CurrencyLabel.Text = assetAsset.Name;
            CurrencyLabel.TextColor = assetAsset.HtmlColor;
            CurencyNameBelow.Text = assetAsset.Name;
            CurencyNameBelow.TextColor = assetAsset.HtmlColor;

            Diversity.UpdateValue(assetAsset, 30);

            card.UpdateSize();
            // card.MakeChart(portfolioCircle);

            CurrencyContainer.Children.Add(card);
        }
    }
}