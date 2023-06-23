using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace APP.Views;

public partial class TitleWithExit : ContentView
{
	public TitleWithExit()
	{
        InitializeComponent();

        
        
	}

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
		Navigation.PopToRootAsync();
    }

    private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
    {
        Console.WriteLine();
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        Console.WriteLine();
    }

    private void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
    {
        Console.WriteLine();
    }

    private void Button_Pressed(object sender, EventArgs e)
    {

    }

    private void Button_Pressed_1(object sender, EventArgs e)
    {
        Navigation.PopToRootAsync();
    }
}