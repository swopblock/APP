using APP.Code.Data.Orders;
using Microsoft.Maui.Controls.PlatformConfiguration;

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

        AppShell shell = new AppShell();

        shell.InputTransparent = true;  

        MainPage = shell;
	}
}
