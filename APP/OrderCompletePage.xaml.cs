using APP.Code.Data.Orders;
using APP.Code.Data.User;

namespace APP;

public partial class OrderCompletePage : ContentPage
{
	public OrderCompletePage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        OrderDetail detail = UserProfileData.GetDemoOrders().FirstOrDefault();

        if (detail != null)
        {
            //Application.Current.MainPage = new OrderDetailsPage(detail);
            NavGoTo(nameof(OrderDetail));
        }
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        NavGoTo(nameof(PortfolioPage));
    }

    private async void NavGoTo(string page)
    {
        await Shell.Current.GoToAsync(page);
    }
}