using APP.Code;
using APP.Code.Data.Orders;

namespace APP;

public partial class TradeAmountPage : ContentPage
{
    OrderDetail detail = new OrderDetail();
    PortfolioAsset SelectedAsset { get; set; }

    double posX = 0;

    bool went = false;
	public TradeAmountPage(PortfolioAsset asset)
	{
		InitializeComponent();

        SelectedAsset = asset;

        BindingContext = asset;

        slider.HtmlColor = asset.HtmlColor;


        went = false;
	}

    public void UpdateAmount(double amount)
    {
        detail.Amount = (decimal)amount;

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

        CompleteSlider.Margin = new Thickness(margin, 0, 0, 0);

        if (margin > (SubmitOrder.Width - 70) && !went)
        {
            NavGoTo(nameof(SendingOrderPage));
            went = true;
        }
    }

    private async void NavGoTo(string page)
    {
        await Shell.Current.GoToAsync(page);
    }
}