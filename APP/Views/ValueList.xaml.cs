using APP.Code;
using APP.Code.Data.Orders;
using APP.Code.Data.User;

namespace APP.Views;

public partial class ValueList : ContentView
{
	public List<ValueItem> ItemsList = new List<ValueItem>()
	{
		new ValueItem("Market Buy","1000", "Jun 22, 2023", "bitcoin.png"),
		new ValueItem("Market Sell", "2000","Jun 13, 2023", "swopblock.png"),
		new ValueItem("Market Buy", "2000", "Jun 6, 2023", "swopblockreward.png")
	};
	public ValueList()
	{
		InitializeComponent();

        ListViewContainer.ItemsSource = ItemsList;
        ListViewContainer.ItemTapped += ListViewContainer_ItemTapped;
    }

    private void ListViewContainer_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        //int inx = e.ItemIndex;

        //List<OrderDetail> orders = UserProfileData.GetDemoOrders();

        //if (inx >= 0 && inx < orders.Count)
        //{
            GoToDetails();
        //}
       
    }

    private void GoToDetails()
    {
        //var page = new OrderDetailsPage();

        //Application.Current.MainPage = page;

        NavGoTo(nameof(OrderDetailsPage));

        //await Navigation.PushAsync(page); 
    }

    private async void NavGoTo(string page)
    {
        await Shell.Current.GoToAsync(page);
    }
}