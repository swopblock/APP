using APP.Code.Data.Orders;
using APP.Code.Data.User;

namespace APP;

public partial class OrderCompletePage : ContentPage
{
	public OrderCompletePage()
	{
		InitializeComponent();

        MoveUp.Loaded += MoveUp_Loaded;
        MoveDownLabels.Loaded += MoveDownLabels_Loaded;
        gridScale.Loaded += GridScale_Loaded;
    }

    private void GridScale_Loaded(object sender, EventArgs e)
    {
        ScaleTheGrid();
    }

    private void MoveDownLabels_Loaded(object sender, EventArgs e)
    {
        MoveDownStack();
    }
    private async void ScaleTheGrid()
    {
        await gridScale.ScaleTo(1, 400);
    }



    private async void MoveUpStack()
    {
        await MoveUp.TranslateTo(0, -200, 850);
    }

    private async void MoveDownStack()
    {
        await MoveDownLabels.TranslateTo(0, 200, 850);
    }

    private void MoveUp_Loaded(object sender, EventArgs e)
    {
        MoveUpStack();
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OrderDetailsPage());
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        button.Fill = Color.FromArgb("#FF00BC");
        Navigation.PopToRootAsync();
    }
}