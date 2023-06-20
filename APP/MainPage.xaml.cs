using APP.Code;
using APP.Code.Data.Orders;
using APP.Code.Data.User;
using APP.Code.Geometry;
using APP.Views;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;

namespace APP;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        this.NavigatedTo += MainPage_NavigatedTo;

        GoToPage();
    }

    private void MainPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        GoToPage();
    }

    public async void GoToPage()
    {
        await Shell.Current.GoToAsync(nameof(PortfolioPage));
    }

    //public async void GoToPage()
    //{
    //    NavigationPage page = new NavigationPage(new PortfolioPage());
    //    await Navigation.PushAsync(page);
    //}
}

