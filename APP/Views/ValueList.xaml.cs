using APP.Code;

namespace APP.Views;

public partial class ValueList : ContentView
{

	public List<ValueItem> ItemsList = new List<ValueItem>()
	{
		new ValueItem("Market Buy","1000", "Jun 22, 2023", "BTC"),
		new ValueItem("Market Sell", "2000","Jun 13, 2023", "SWOBL"),
		new ValueItem("Market Buy", "2000", "Jun 6, 2023", "ETH")
	};
	public ValueList()
	{
		InitializeComponent();

        ListViewContainer.ItemsSource = ItemsList;
    }
}