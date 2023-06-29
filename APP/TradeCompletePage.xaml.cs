namespace APP;

public partial class TradeCompletePage : ContentPage
{
	public TradeCompletePage()
	{
		InitializeComponent();
	}

    protected override bool OnBackButtonPressed()
    {
        Navigation.PopToRootAsync();

        return true;
    }

}