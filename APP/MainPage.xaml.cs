namespace APP;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Liked {count} time";
		else
			CounterBtn.Text = $"Liked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

