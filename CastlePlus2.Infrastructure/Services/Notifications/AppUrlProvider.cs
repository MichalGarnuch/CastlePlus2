using CastlePlus2.Application.Interfaces.Notifications;
using Microsoft.Extensions.Configuration;

namespace CastlePlus2.Infrastructure.Services.Notifications
{
    public sealed class AppUrlProvider : IAppUrlProvider
    {
        private readonly IConfiguration _configuration;

        public AppUrlProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetClientBaseUrl()
        {
            var url = _configuration["Client:BaseUrl"];
            return string.IsNullOrWhiteSpace(url) ? "https://localhost:5072" : url;
        }
    }
}