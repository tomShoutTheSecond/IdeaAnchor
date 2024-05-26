using CommunityToolkit.Maui;
using IdeaAnchor.Database;
using IdeaAnchor.Services;
using IdeaAnchor.Pages;
using IdeaAnchor.ViewModels;
using IdeaAnchor.MenuProviders;
using Controls.UserDialogs.Maui;

namespace IdeaAnchor;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseUserDialogs()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FontAwesome");
            });

        builder.Services.AddTransient<AppShell>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<IdeaPage>();
        builder.Services.AddTransient<IdeaViewModel>();
		builder.Services.AddTransient<IdeasListPage>();
        builder.Services.AddTransient<IdeasListViewModel>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsViewModel>();
        builder.Services.AddTransient<SubscriptionPage>();
        builder.Services.AddTransient<SubscriptionViewModel>();

        builder.Services.AddSingleton<IdeaDatabase>();
        builder.Services.AddSingleton<SettingsMenuProvider>();
        builder.Services.AddSingleton<ImportExportService>();
        builder.Services.AddSingleton<KeyboardService>();

        return builder.Build();
	}
}

