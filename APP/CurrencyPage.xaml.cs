using APP.Code;
using APP.Views;

namespace APP;

public partial class CurrencyPage : ContentPage
{
	public CurrencyPage(PortfolioAsset asset)
	{
		InitializeComponent();

        LoadCurrency(asset);
	}

	public void LoadCurrency(PortfolioAsset asset)
	{
        CryptoCard card = new CryptoCard(asset);

        CurrencyLabel.Text = asset.Name;
        CurrencyLabel.TextColor = asset.HtmlColor;
        CurencyNameBelow.Text = asset.Name;
        CurencyNameBelow.TextColor = asset.HtmlColor;

        Diversity.UpdateValue(asset, 30);

        card.UpdateSize();
       // card.MakeChart(portfolioCircle);

        CurrencyContainer.Children.Add(card);
    }
}