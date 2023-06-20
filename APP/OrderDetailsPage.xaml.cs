using APP.Code.Data.Orders;
using APP.Code.Data.User;

namespace APP;

public partial class OrderDetailsPage : ContentPage
{
	public OrderDetailsPage()
	{
		InitializeComponent();

		

		this.BindingContext = UserProfileData.GetDemoOrders().First();

        //Application.Current.MainPage = this;
    }
}