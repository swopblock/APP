using APP.Code;
using APP.Code.Data.Orders;
using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;

namespace APP;

public partial class SendingOrderPage : ContentPage
{
    float angle = 270;
    int index = 0;
    float total = 0;
    float count = 0;
    public SendingOrderPage()
	{
		InitializeComponent();

        this.Loaded += SendingOrderPage_Loaded;
    }

    private void SendingOrderPage_Loaded(object sender, EventArgs e)
    {
        StartTimer();
    }



    private void StartTimer()
    {
        Dispatcher.StartTimer(TimeSpan.FromMilliseconds(20), () =>
        {
            count++;
            OnPropertyChanged(nameof(count));
            if(count < 300)
            return true;
            else
            {
                return false;
            }
        });
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        if(index < StaticTest.LoadingCircle.Count)
        {
            LineCurve.Data = StaticTest.LoadingCircle[index++];
        }
        else
        {
            //Application.Current.MainPage = new OrderCompletePage();
            NavGoTo(nameof(OrderCompletePage));
        }
    }

    private async void NavGoTo(string page)
    {
        await Shell.Current.GoToAsync(page);
    }
}