using CastlePlus2.Client;
using CastlePlus2.Client.Services.Auth.Http;
using CastlePlus2.Client.Services.Auth.Storage;
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

            // 1) Client DI (CustomAuthStateProvider + serwisy)
            builder.Services.AddCastlePlus2Client();

            // 2) Token store dla MAUI
            builder.Services.AddScoped<IAccessTokenStore, InMemoryAccessTokenStore>();
            // builder.Services.AddScoped<IAccessTokenStore, SecureStorageTokenStore>();

            // 3) HttpClient base url
            string apiBaseUrl = DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5072/"
                : "http://localhost:5072/";

            // 4) Handlery
            builder.Services.AddTransient<BearerTokenHandler>();
            builder.Services.AddTransient<AuthorizationFailureHandler>();

            // 5) HttpClient: Bearer -> AuthFailure(401) -> HttpClientHandler
            builder.Services.AddScoped<HttpClient>(sp =>
            {
                var bearer = sp.GetRequiredService<BearerTokenHandler>();
                var authFailure = sp.GetRequiredService<AuthorizationFailureHandler>();

                authFailure.InnerHandler = new HttpClientHandler();
                bearer.InnerHandler = authFailure;

                return new HttpClient(bearer)
                {
                    BaseAddress = new Uri(apiBaseUrl)
                };
            });

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}