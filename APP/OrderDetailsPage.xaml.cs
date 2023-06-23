using APP.Code.Data.Orders;
using APP.Code.Data.User;

namespace APP;

public partial class OrderDetailsPage : ContentPage
{
	public OrderDetailsPage()
	{
		InitializeComponent();

		

		this.BindingContext = UserProfileData.GetDemoOrders().First();

		//Shell shell = Shell.Current;

		//shell.ToolbarItems.Clear();
		//shell.MenuBarItems.Clear();
		//shell.InputTransparent = true;

  //      ContentPage p = this;

        //Application.Current.MainPage = this;
    }
}