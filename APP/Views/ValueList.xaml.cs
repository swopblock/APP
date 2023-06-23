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
        ListViewContainer.ItemSelected += ListViewContainer_ItemSelected;
    }

    private void ListViewContainer_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        
    }

    private void ListViewContainer_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        //GoToDetails();       
    }

    private void GoToDetails()
    {
        Navigation.PushAsync(new OrderDetailsPage());
        //NavGoTo(nameof(OrderDetailsPage));
    }

    private async void NavGoTo(string page)
    {
        await Shell.Current.GoToAsync(page);
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        StackLayout layout = ((StackLayout)sender);

        layout.BackgroundColor = Color.FromArgb("#2F323A");

        GoToDetails();

        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(500),() => 
        {
            layout.BackgroundColor = Colors.Transparent;
        });
    }
}