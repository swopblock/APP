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
        //Shell.Current.Navigation.RemovePage(this);
        //Console.WriteLine(Shell.Current.Navigation.NavigationStack);
        NavGoTo(nameof(OrderDetailsPage));
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PopToRootAsync();
        //NavGoTo(nameof(PortfolioPage));
    }

    private async void NavGoTo(string page)
    {
        
        await Shell.Current.GoToAsync(page);
    }
}