using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace CastlePlus2.Client
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCastlePlus2Client(this IServiceCollection services)
        {
            services.AddMudServices();
            // Tu będziesz dodawać swoje serwisy
            return services;
        }
    }
}