using CastlePlus2.Client;
using CastlePlus2.Client.Services.Auth.Http;
using CastlePlus2.Client.Services.Auth.Storage;
using CastlePlus2.Mobile.Services.Auth;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace CastlePlus2.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<CastlePlus2.Mobile.App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMudServices();
            builder.Services.AddMauiBlazorWebView();

            // 1) Client DI (rejestruje BearerTokenHandler)
            builder.Services.AddCastlePlus2Client();

            // 2) Token store dla MAUI
            builder.Services.AddScoped<IAccessTokenStore, SecureStorageTokenStore>();

            // 3) HttpClient
            string apiBaseUrl = DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5072/"
                : "http://localhost:5072/";

            builder.Services.AddHttpClient("ApiClient", client => client.BaseAddress = new Uri(apiBaseUrl))
                .AddHttpMessageHandler<BearerTokenHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
