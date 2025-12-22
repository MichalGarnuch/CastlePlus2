using CastlePlus2.Client.Services.Media;
using CastlePlus2.Client.Services.Podmioty;
using CastlePlus2.Client.Services.Rdzen;
using CastlePlus2.Client.Services.Slowniki;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace CastlePlus2.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddCastlePlus2Client(this IServiceCollection services)
    {
        services.AddMudServices();
        // tutaj dopniemy serwisy modułów, np. services.AddScoped<IRodzajMediowService, RodzajMediowService>();
        services.AddScoped<IRodzajeMediowService, RodzajeMediowService>();
        services.AddScoped<IPrzylaczaService, PrzylaczaService>();
        services.AddScoped<ILicznikiService, LicznikiService>();

        services.AddScoped<IJednostkiMiaryService, JednostkiMiaryService>();
        services.AddScoped<IIndeksacjeService, IndeksacjeService>();
        services.AddScoped<IWalutyService, WalutyService>();

        services.AddScoped<IAdresyService, AdresyService>();
        services.AddScoped<IPrzypisaniaAdresowService, PrzypisaniaAdresowService>();
        services.AddScoped<INieruchomosciService, NieruchomosciService>();
        services.AddScoped<IBudynkiService, BudynkiService>(); 
        services.AddScoped<ILokaleService, LokaleService>();
        services.AddScoped<IPomieszczeniaService, PomieszczeniaService>();

        services.AddScoped<IPodmiotyService, PodmiotyService>();

        return services;
    }
}
