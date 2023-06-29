using APP.Code.Data.Orders;
using APP.Code.Data.User;

namespace APP;

public partial class OrderDetailsPage : ContentPage
{
	public OrderDetailsPage()
	{
		InitializeComponent();

        

		this.BindingContext = UserProfileData.GetDemoOrders().First();
    }

    protected override bool OnBackButtonPressed()
    {
        Navigation.PopToRootAsync();

        return true;
    }

    private void ImageButton_Pressed(object sender, EventArgs e)
    {
        Navigation.PopToRootAsync();
    }
}