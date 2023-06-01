using APP.Code;

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
    }
}