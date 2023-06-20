using APP.Code.Data.Orders;

namespace APP;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

            if (StaticTest.LoadingCircle == null)
            {
                StaticTest.LoadingCircle = StaticTest.GetCurves(270, 0);
            }

        MainPage = new AppShell();
	}
}
