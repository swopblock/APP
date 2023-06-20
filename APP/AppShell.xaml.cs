using Microsoft.Maui.Controls;

namespace APP;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(PortfolioPage), typeof(PortfolioPage));
        Routing.RegisterRoute(nameof(CurrencyPage), typeof(CurrencyPage));
        Routing.RegisterRoute(nameof(OrderCompletePage), typeof(OrderCompletePage));
        Routing.RegisterRoute(nameof(OrderDetailsPage), typeof(OrderDetailsPage));
        Routing.RegisterRoute(nameof(SendingOrderPage), typeof(SendingOrderPage));
        Routing.RegisterRoute(nameof(TradeCompletePage), typeof(TradeCompletePage));
        Routing.RegisterRoute(nameof(TradeSelectPage), typeof(TradeSelectPage));
        Routing.RegisterRoute(nameof(TradeAmountPage), typeof(TradeAmountPage));
        Routing.RegisterRoute(nameof(RewardsPage), typeof(RewardsPage));

        SetNavBarIsVisible(this, false);
	}
}
