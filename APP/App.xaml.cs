using APP.Code.Data.Orders;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace APP;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        if (StaticTest.GeometryContainer == null)
        {
            StaticTest.GeometryContainer = StaticTest.GetCurves(270, 0);
        }

        AppShell shell = new AppShell();
       // shell.InputTransparent = true;  

        MainPage = shell;
	}
}
