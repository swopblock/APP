using APP.Code.Data.Orders;

namespace APP;

public partial class OrderDetailsPage : ContentPage
{
	public OrderDetailsPage(OrderDetail detail)
	{
		InitializeComponent();

		this.BindingContext = detail;
    }
}