using APP.Code;
using APP.Code.Data.Orders;
using APP.Code.Data.User;

namespace APP;

public partial class TradeSelectPage : ContentPage
{
    public PortfolioAsset Asset { get; set; }
    public PortfolioAsset first { get; set; }
    public PortfolioAsset second { get; set; }  

	public TradeSelectPage()
	{
		InitializeComponent();

        this.Loaded += TradeSelectPage_Loaded;
    }

    protected override bool OnBackButtonPressed()
    {
        Navigation.RemovePage(this);

        return true;
    }


    private void TradeSelectPage_Loaded(object sender, EventArgs e)
    {
        Asset = (PortfolioAsset)BindingContext;

        if (Asset != null)
        {
            if (Asset.Symbol == "ETH")
            {
                first = UserProfileData.LoadDemo().Where(x => x.Symbol == "BTC").FirstOrDefault();
                second = UserProfileData.LoadDemo().Where(x => x.Symbol == "SWOBL").FirstOrDefault();
            }
            else if (Asset.Symbol == "BTC")
            {
                first = UserProfileData.LoadDemo().Where(x => x.Symbol == "ETH").FirstOrDefault();
                second = UserProfileData.LoadDemo().Where(x => x.Symbol == "SWOBL").FirstOrDefault();
            }
            else
            {
                first = UserProfileData.LoadDemo().Where(x => x.Symbol == "BTC").FirstOrDefault();
                second = UserProfileData.LoadDemo().Where(x => x.Symbol == "ETH").FirstOrDefault();
            }

            firstRect.Stroke = first.HtmlColor;
            secondRect.Stroke = second.HtmlColor;

            firstImage.Source = first.Image;
            secondImage.Source = second.Image;

            firstAmount.Text = first.Amount.ToString() + " " + first.Symbol;
            secondAmount.Text = second.Amount.ToString() + " " + second.Symbol;
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (Asset != null)
        {
            TradeAmountPage page = new TradeAmountPage(Asset);
            page.BindingContext = first;
            Navigation.PushAsync(page);
        }
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        if (Asset != null)
        {
            TradeAmountPage page = new TradeAmountPage(Asset);
            page.BindingContext = second;
            Navigation.PushAsync(page);
        }
    }

    private async void NavGoTo(string page)
    {
        await Shell.Current.GoToAsync(page);
    }
}