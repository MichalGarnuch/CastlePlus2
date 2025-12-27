using CastlePlus2.Client.Services.Dokumenty;
using CastlePlus2.Client.Services.Finanse;
using CastlePlus2.Client.Services.Media;
using CastlePlus2.Client.Services.Najem;
using CastlePlus2.Client.Services.Podmioty;
using CastlePlus2.Client.Services.Rdzen;
using CastlePlus2.Client.Services.Slowniki;
using CastlePlus2.Client.Services.Utrzymanie;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace CastlePlus2.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddCastlePlus2Client(this IServiceCollection services)
    {
        services.AddMudServices();
        // tutaj dopniemy serwisy modułów

        //MEDIA
        services.AddScoped<IRodzajeMediowService, RodzajeMediowService>();
        services.AddScoped<IPrzylaczaService, PrzylaczaService>();
        services.AddScoped<ILicznikiService, LicznikiService>();
        services.AddScoped<IOdczytyService, OdczytyService>();

        //SLOWNIK
        services.AddScoped<IJednostkiMiaryService, JednostkiMiaryService>();
        services.AddScoped<IIndeksacjeService, IndeksacjeService>();
        services.AddScoped<IWalutyService, WalutyService>();

        //RDZEN
        services.AddScoped<IAdresyService, AdresyService>();
        services.AddScoped<IPrzypisaniaAdresowService, PrzypisaniaAdresowService>();
        services.AddScoped<INieruchomosciService, NieruchomosciService>();
        services.AddScoped<IBudynkiService, BudynkiService>(); 
        services.AddScoped<ILokaleService, LokaleService>();
        services.AddScoped<IPomieszczeniaService, PomieszczeniaService>();
        services.AddScoped<IEncjeService, EncjeService>();

        //PODMIOTY
        services.AddScoped<IPodmiotyService, PodmiotyService>();
        services.AddScoped<IWlasnosciService, WlasnosciService>();
        services.AddScoped<IKontaktyService, KontaktyService>();

        //FINANSE
        services.AddScoped<IFakturyService, FakturyService>();
        services.AddScoped<IPlatnosciService, PlatnosciService>();
        services.AddScoped<IAlokacjeKosztowService, AlokacjeKosztowService>();
        services.AddScoped<IKategorieKosztowService, KategorieKosztowService>();
        services.AddScoped<IPozycjeKosztowService, PozycjeKosztowService>();
        services.AddScoped<IRozliczeniaPlatnosciService, RozliczeniaPlatnosciService>();

        //NAJEM
        services.AddScoped<IPrzedmiotyNajmuService, PrzedmiotyNajmuService>();
        services.AddScoped<IKaucjeService, KaucjeService>();
        services.AddScoped<ISkladnikiCzynszuService, SkladnikiCzynszuService>();
        services.AddScoped<IUmowyNajmuService, UmowyNajmuService>();

        //UTRZYMANIE
        services.AddScoped<IZleceniaPracyService, ZleceniaPracyService>();
        services.AddScoped<IPowiazaniaZleceniaService, PowiazaniaZleceniaService>();

        //DOKUMENTY
        services.AddScoped<IDokumentyService, DokumentyService>();
        services.AddScoped<IPowiazaniaDokumentuService, PowiazaniaDokumentuService>();

        return services;
    }
}
