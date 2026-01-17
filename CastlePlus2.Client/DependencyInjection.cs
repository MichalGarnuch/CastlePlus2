using CastlePlus2.Client.Services.Auth;
using CastlePlus2.Client.Services.Auth.Admin;
using CastlePlus2.Client.Services.Auth.Http;
using CastlePlus2.Client.Services.Auth.State;
using CastlePlus2.Client.Services.Dashboard;
using CastlePlus2.Client.Services.Dokumenty;
using CastlePlus2.Client.Services.Exports;
using CastlePlus2.Client.Services.Finanse;
using CastlePlus2.Client.Services.Media;
using CastlePlus2.Client.Services.Najem;
using CastlePlus2.Client.Services.Podmioty;
using CastlePlus2.Client.Services.Rdzen;
using CastlePlus2.Client.Services.Reports;
using CastlePlus2.Client.Services.Slowniki;
using CastlePlus2.Client.Services.Utrzymanie;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace CastlePlus2.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddCastlePlus2Client(this IServiceCollection services)
    {
        services.AddMudServices();

        services.AddAuthorizationCore();

        // ✅ krytyczne: provider jako konkretny typ
        services.AddScoped<CustomAuthStateProvider>();

        // ✅ i jako AuthenticationStateProvider (AuthorizeView/AuthorizeRouteView)
        services.AddScoped<AuthenticationStateProvider>(sp =>
            sp.GetRequiredService<CustomAuthStateProvider>());

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthAdminService, AuthAdminService>();
        services.AddTransient<BearerTokenHandler>();

        // DASHBOARD
        services.AddScoped<IDashboardService, DashboardService>();

        // MEDIA
        services.AddScoped<IRodzajeMediowService, RodzajeMediowService>();
        services.AddScoped<IPrzylaczaService, PrzylaczaService>();
        services.AddScoped<ILicznikiService, LicznikiService>();
        services.AddScoped<IOdczytyService, OdczytyService>();

        // SLOWNIK
        services.AddScoped<IJednostkiMiaryService, JednostkiMiaryService>();
        services.AddScoped<IIndeksacjeService, IndeksacjeService>();
        services.AddScoped<IWalutyService, WalutyService>();

        // RDZEN
        services.AddScoped<IAdresyService, AdresyService>();
        services.AddScoped<IPrzypisaniaAdresowService, PrzypisaniaAdresowService>();
        services.AddScoped<INieruchomosciService, NieruchomosciService>();
        services.AddScoped<IBudynkiService, BudynkiService>();
        services.AddScoped<ILokaleService, LokaleService>();
        services.AddScoped<IPomieszczeniaService, PomieszczeniaService>();
        services.AddScoped<IEncjeService, EncjeService>();
        services.AddScoped<IProcesyRdzenService, ProcesyRdzenService>();

        // PODMIOTY
        services.AddScoped<IPodmiotyService, PodmiotyService>();
        services.AddScoped<IWlasnosciService, WlasnosciService>();
        services.AddScoped<IProcesyPodmiotyService, ProcesyPodmiotyService>();
        services.AddScoped<IKontaktyService, KontaktyService>();

        // FINANSE
        services.AddScoped<IFakturyService, FakturyService>();
        services.AddScoped<IPlatnosciService, PlatnosciService>();
        services.AddScoped<IAlokacjeKosztowService, AlokacjeKosztowService>();
        services.AddScoped<IKategorieKosztowService, KategorieKosztowService>();
        services.AddScoped<IPozycjeKosztowService, PozycjeKosztowService>();
        services.AddScoped<IRozliczeniaPlatnosciService, RozliczeniaPlatnosciService>();
        services.AddScoped<IProcesyFinanseService, ProcesyFinanseService>();

        // NAJEM
        services.AddScoped<IPrzedmiotyNajmuService, PrzedmiotyNajmuService>();
        services.AddScoped<IKaucjeService, KaucjeService>();
        services.AddScoped<ISkladnikiCzynszuService, SkladnikiCzynszuService>();
        services.AddScoped<IUmowyNajmuService, UmowyNajmuService>();
        services.AddScoped<IProcesyNajmuService, ProcesyNajmuService>();

        // UTRZYMANIE
        services.AddScoped<IZleceniaPracyService, ZleceniaPracyService>();
        services.AddScoped<IPowiazaniaZleceniaService, PowiazaniaZleceniaService>();
        services.AddScoped<IZgloszeniaService, ZgloszeniaService>();

        // DOKUMENTY
        services.AddScoped<IDokumentyService, DokumentyService>();
        services.AddScoped<IPowiazaniaDokumentuService, PowiazaniaDokumentuService>();
        services.AddScoped<IProcesyDokumentowService, ProcesyDokumentowService>();

        // EXPORT
        services.AddScoped<IReportExportUrlService, ReportExportUrlService>();
        services.AddScoped<IReportDataPreviewService, ReportDataPreviewService>();
        services.AddScoped<IReportDocumentPreviewService, ReportDocumentPreviewService>();

        services.AddScoped<IReportExportDownloadService, ReportExportDownloadService>();
        // services.AddScoped<ReportExportDownloadService>();

        return services;
    }
}
