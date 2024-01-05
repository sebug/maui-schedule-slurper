using maui_schedule_slurper.Repositories;
using maui_schedule_slurper.Services;
using maui_schedule_slurper.ViewModels;
using Microsoft.Extensions.Logging;

namespace maui_schedule_slurper;

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
			})
			.AddRepositories()
			.AddServices()
			.AddViewModels()
			.AddViews();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	private static MauiAppBuilder AddRepositories(this MauiAppBuilder appBuilder)
	{
		appBuilder.Services.AddSingleton<ScheduleDatabase>();
		appBuilder.Services.AddTransient<IDevroomRepository, DevroomRepository>();
		return appBuilder;
	}

	private static MauiAppBuilder AddServices(this MauiAppBuilder appBuilder)
	{
		appBuilder.Services.AddSingleton<ISynchronizationService, SynchronizationService>();
		return appBuilder;
	}

	private static MauiAppBuilder AddViewModels(this MauiAppBuilder appBuilder)
	{
		appBuilder.Services.AddTransient<MainViewModel>();
		return appBuilder;
	}

	private static MauiAppBuilder AddViews(this MauiAppBuilder appBuilder)
	{
		appBuilder.Services.AddSingleton<MainPage>();
		return appBuilder;
	}
}
