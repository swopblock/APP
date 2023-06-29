using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace APP.Views;

public partial class TitleWithExit : ContentView
{
	public TitleWithExit()
	{
        InitializeComponent();        
	}
    private void Button_Pressed(object sender, EventArgs e)
    {
        Navigation.PopToRootAsync();
    }
}