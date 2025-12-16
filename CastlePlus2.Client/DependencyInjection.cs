using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace CastlePlus2.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddCastlePlus2Client(this IServiceCollection services)
    {
        services.AddMudServices();
        // tutaj dopniemy serwisy modułów, np. services.AddScoped<IRodzajMediowService, RodzajMediowService>();
        services.AddScoped<CastlePlus2.Client.Services.Media.IRodzajeMediowService, CastlePlus2.Client.Services.Media.RodzajeMediowService>();

        return services;
    }
}
