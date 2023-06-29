using APP.Code;
using APP.Code.Data.Orders;

namespace APP;

public partial class TradeAmountPage : ContentPage
{

    /*
     * 
     * Amount page has to come first because you dont know
     * how much currency you can afford to buy until 
     * you select what you are buying it with
     * 
     */
    public OrderDetail detail = new OrderDetail();
    PortfolioAsset SelectedAsset { get; set; }

    PortfolioAsset Asset { get; set; }

    double posX = 0;

    bool went = false;
	public TradeAmountPage(PortfolioAsset asset)
	{
		InitializeComponent();

        SelectedAsset = asset;

        this.Loaded += TradeAmountPage_Loaded;

        went = false;
	}

    protected override bool OnBackButtonPressed()
    {
        Navigation.RemovePage(this);

        return true;
    }

    private void TradeAmountPage_Loaded(object sender, EventArgs e)
    {
        slider.HtmlColor = SelectedAsset.HtmlColor;

        Asset = (PortfolioAsset)BindingContext;

        slider.AltColor = Asset.HtmlColor;

        slider.symbol = SelectedAsset.Symbol;
        slider.altSymbol = Asset.Symbol;

        numberPad.symbol = SelectedAsset.Symbol;
        numberPad.altSymbol = Asset.Symbol;
    }

    public void UpdateAmount(double amount, string symbol, bool percent = true, bool endsInDot = false)
    {
        decimal amt = (decimal)amount;

        SymbolName.Text = symbol;
        SymbolNameNum.Text = symbol;

        if (SelectedAsset.Symbol == symbol)
        {
            SymbolName.TextColor = SelectedAsset.HtmlColor;
            SymbolNameNum.TextColor = SelectedAsset.HtmlColor;
            amountSlide.TextColor = SelectedAsset.HtmlColor;
            amountType.TextColor = SelectedAsset.HtmlColor;

            submitColor.Stroke = Asset.HtmlColor;
            submitImage.Source = Asset.Image;

            if (percent)
            {
                detail.Amount = SelectedAsset.Amount * amt;
            }
            else
            {
                amt = Math.Clamp(amt, 0, SelectedAsset.Amount);
                detail.Amount = amt;
            }
        }
        else
        {
            SymbolName.TextColor = Asset.HtmlColor;
            SymbolNameNum.TextColor = Asset.HtmlColor;
            amountType.TextColor= Asset.HtmlColor;
            amountSlide.TextColor= Asset.HtmlColor;

            submitImage.Source= SelectedAsset.Image;
            submitColor.Stroke= SelectedAsset.HtmlColor;

            if (percent)
            {
                detail.Amount = Asset.Amount * amt;
            }
            else
            {
                amt = Math.Clamp(amt, 0, Asset.Amount);
                detail.Amount = amt;
            }
        }

        amountSlide.Text = Math.Round(detail.Amount, 6).ToString();
        amountType.Text = Math.Round(detail.Amount, 6).ToString();

        if(detail.Amount != 0 ) 
        { 
            ThreeButtons.IsVisible = false;
            SubmitOrder.IsVisible = true;
        }
        else 
        { 
            ThreeButtons.IsVisible = true;
            SubmitOrder.IsVisible = false;
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		slideTrade.IsVisible = false;
		numPad.IsVisible = true;
    }

    private void ImageButton_Clicked_1(object sender, EventArgs e)
    {
        slideTrade.IsVisible = true;
        numPad.IsVisible = false;
    }

    private void GraphicsView_DragInteraction(object sender, TouchEventArgs e)
    {
        if (e != null)
        {
            if (e.Touches != null)
            {
                if (e.Touches.Length > 0)
                {
                    PointF touch = e.Touches.First();

                    posX = touch.X;

                    UpdateSlider();
                }
            }
        }
    }

    private void UpdateSlider()
    {
        double margin = posX - 35;

        if(margin < 0)  margin = 0;

        if (margin > (SubmitOrder.Width - 70) && !went)
        {
            SendingOrderPage page = new SendingOrderPage();
            page.BindingContext = SelectedAsset;
            Navigation.PushAsync(page);
            went = true;
        }
        else
        {
            CompleteSlider.Margin = new Thickness(margin, 0, 0, 0);
        }
    }

    private async void NavGoTo(string page)
    {
        await Shell.Current.GoToAsync(page);
    }
}