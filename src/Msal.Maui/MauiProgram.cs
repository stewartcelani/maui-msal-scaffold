using System.Reflection;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.Maui.LifecycleEvents;
using Msal.Maui.Authentication;
using Msal.Maui.Helpers;
using Msal.Maui.Services;
using Msal.Maui.Settings;
using Msal.Maui.Views;

namespace Msal.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(platform =>
                {
                    platform.OnActivityResult((activity, rc, result, data) =>
                    {
                        AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(rc, result,
                            data);
                    });
                });
#endif
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var assembly = Assembly.GetExecutingAssembly();
        var assemblyName = assembly.GetName().Name;
        using var appSettingsStream = assembly.GetManifestResourceStream($"{assemblyName}.appsettings.json");
        if (appSettingsStream == null) throw new Exception($"{assemblyName}.appsettings.json not found");
        var configuration = new ConfigurationBuilder()
            .AddJsonStream(appSettingsStream)
            .Build();
        builder.Configuration.AddConfiguration(configuration);

        var azureAdSettings =
            SettingsBinder.BindAndValidate<AzureAdSettings, AzureAdSettingsValidator>(builder.Configuration);
        builder.Services.AddSingleton(azureAdSettings);
        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
        builder.Services.AddSingleton<AppUser>();
        builder.Services.AddTransient<AuthenticationStateProvider, HybridAuthenticationStateProvider>();
        builder.Services.AddTransient<AuthenticatedView>();
        builder.Services.AddTransient<UnauthenticatedView>();
        builder.Services.AddTransient<INavigationService, NavigationService>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}