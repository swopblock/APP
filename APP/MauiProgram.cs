using APP.Code;
using APP.Views;
using APP.Views.ViewPart;

namespace APP;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("guida-bold-pro.otf", "GuidaBold");
                fonts.AddFont("guida-light-pro.otf", "GuidaLight");
            });

		builder.Services.AddTransient<PortfolioPage>();
        builder.Services.AddTransient<CurrencyPage>();
        builder.Services.AddTransient<OrderDetailsPage>();
        builder.Services.AddTransient<OrderCompletePage>();
        builder.Services.AddTransient<TradeSelectPage>();
        builder.Services.AddTransient<TradeAmountPage>();
        builder.Services.AddTransient<TradeCompletePage>();
        builder.Services.AddTransient<RewardsPage>();
        builder.Services.AddTransient<SendingOrderPage>();
        builder.Services.AddTransient<IconValueStack>();
        builder.Services.AddTransient<PercentCircle>();
        builder.Services.AddTransient<CryptoCard>();
        builder.Services.AddTransient<RoundBox>();
        builder.Services.AddTransient<PoweredBy>();
        builder.Services.AddTransient<BottomThreeButtons>();
        builder.Services.AddTransient<OrderList>();
        builder.Services.AddTransient<PortfolioAsset>();

        return builder.Build();
	}
}
