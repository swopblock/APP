namespace APP.Views;

public partial class CryptoCard : ContentView
{
    float width = 0;
    float height = 0;
	public CryptoCard()
	{
		InitializeComponent();

        UpdateHeight();
    }

    public void UpdateHeight()
    {
        new Thread(() => {

            while (width == 0 && height == 0)
            {
                try
                {
                    width = (float)DeviceDisplay.Current.MainDisplayInfo.Width;
                }
                catch
                {
                    Thread.Sleep(10);
                }

                if (width != 0)
                {
                    width /= (float)DeviceDisplay.Current.MainDisplayInfo.Density;
                    height = width;
                }
            }

            Dispatcher.Dispatch(() => { 
                ContainerForGrids.HeightRequest = height;
                ContainerForGrids.WidthRequest = width;
            });

        }).Start();
    }


}