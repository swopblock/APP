using APP.Code;
using APP.Code.Data.User;
using APP.Views;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Diagnostics;

namespace APP;

[QueryProperty(nameof(asset), nameof(asset))]
public partial class CurrencyPage : ContentPage
{
    [Bindable(true)]
    public string asset { get; set; }

    List<PortfolioAsset> assetList = UserProfileData.LoadDemo();

    public Color UnselectColor = Color.FromArgb("#2F323A");
	public CurrencyPage()
	{
		InitializeComponent();

        this.Loaded += CurrencyPage_Loaded;

        Carousel.ItemsSource = assetList;
        Carousel.Loaded += Carousel_Loaded;
    }

    private void CurrencyPage_Loaded(object sender, EventArgs e)
    {
        LoadCurrency(asset);
    }

    private void Carousel_Loaded(object sender, EventArgs e)
    {
        Carousel.PropertyChanged += Carousel_PropertyChanged;
    }

    private void Carousel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {

    }

    private void Carousel_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {

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
            if(assetAsset.Symbol != "YOUR REWARDS")
            {
                RectFirst.Source = "stargray.png";
               // RectFirst.Fill = UnselectColor;
            }
            else
            {
                RectFirst.Source = "star.png";
            }
            
            if (assetAsset.Symbol != "SWOBL")
            {
                RectOne.Stroke = UnselectColor;
            }
            else
            {
                RectOne.Stroke = Color.FromArgb("#00DD00");
            }
            
            if (assetAsset.Symbol != "BTC")
            {
                RectTwo.Stroke = UnselectColor;
            }
            else
            {
                RectTwo.Stroke = Color.FromArgb("#F2A900");
            }

            if (assetAsset.Symbol != "ETH")
            {
                RectThree.Stroke = UnselectColor;
            }
            else
            {
                RectThree.Stroke = Color.FromArgb("#6F89FF");
            }

            btbuttons.currency = asset;

            amountText.Text = assetAsset.Amount + " " + assetAsset.Symbol;
            CurrencyLabel.Text = assetAsset.Name;
            CurrencyLabel.TextColor = assetAsset.HtmlColor;
            CurencyNameBelow.Text = assetAsset.Name;
            CurencyNameBelow.TextColor = assetAsset.HtmlColor;

            Diversity.UpdateValue(assetAsset, 30, assetAsset.StartAngle);
        }
    }

    protected override bool OnBackButtonPressed()
    {
        Navigation.RemovePage(this);

        return true;
    }

    private void ImageButton_Pressed(object sender, EventArgs e)
    {
        Navigation.PopToRootAsync();
    }
}