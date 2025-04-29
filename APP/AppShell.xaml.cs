using Microsoft.Maui.Controls;
using System.Runtime.CompilerServices;

namespace APP;

public partial class AppShell : Shell
{
    public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(WalletPage), typeof(WalletPage));
        Routing.RegisterRoute(nameof(MarketPage), typeof(MarketPage));
        Routing.RegisterRoute(nameof(OrderCompletePage), typeof(OrderCompletePage));
        Routing.RegisterRoute(nameof(OrderDetailsPage), typeof(OrderDetailsPage));
        Routing.RegisterRoute(nameof(SendingOrderPage), typeof(SendingOrderPage));
        Routing.RegisterRoute(nameof(TradeCompletePage), typeof(TradeCompletePage));
        Routing.RegisterRoute(nameof(OrderPage), typeof(OrderPage));
        Routing.RegisterRoute(nameof(TradeAmountPage), typeof(TradeAmountPage));
        Routing.RegisterRoute(nameof(RewardsPage), typeof(RewardsPage));

        SetNavBarIsVisible(this, false);
        SetNavBarHasShadow(this, false);
        SetBackgroundColor(this, Colors.Transparent);
        this.ZIndex = -10;
        //InputTransparent = true;
	}
}
