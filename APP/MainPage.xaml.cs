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
        //new Thread(() =>
        //{
        //    Thread.CurrentThread.IsBackground = true;

        //    if (StaticTest.LoadingCircle == null)
        //    {
        //        StaticTest.LoadingCircle = StaticTest.GetCurves(270, 0);
        //    }
        //}).Start();



        GoToPage();
    }

    public async void GoToPage()
    {
        await Shell.Current.GoToAsync(nameof(PortfolioPage));
    }
}

