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
        Navigation.PushAsync(new OrderDetailsPage());
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        button.Fill = Color.FromArgb("#FF00BC");
        Navigation.PopToRootAsync();
    }
}