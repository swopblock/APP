using APP.Code.Data.Orders;

namespace APP;

public partial class TradeAmountPage : ContentPage
{
    OrderDetail detail = new OrderDetail();
	public TradeAmountPage()
	{
		InitializeComponent();

        BindingContext = detail;
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

    }
}