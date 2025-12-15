using CastlePlus2.Client; // To musi być widoczne
using Microsoft.Extensions.Logging;

namespace CastlePlus2.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                // WAŻNE: Używamy pełnej nazwy klasy App, żeby uniknąć błędu namespace
                .UseMauiApp<CastlePlus2.Mobile.App>() 
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // 1. Dodajemy WebView
            builder.Services.AddMauiBlazorWebView();

            // 2. HttpClient (Dla Androida 10.0.2.2, dla Windowsa localhost)
            string apiBaseUrl = DeviceInfo.Platform == DevicePlatform.Android 
                ? "http://10.0.2.2:5072/" 
                : "http://localhost:5072/";

            builder.Services.AddHttpClient("ApiClient", client => client.BaseAddress = new Uri(apiBaseUrl));
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

            // 3. Rejestracja Clienta
            builder.Services.AddCastlePlus2Client();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}