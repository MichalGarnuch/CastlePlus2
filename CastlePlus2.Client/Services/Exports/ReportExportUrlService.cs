using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace CastlePlus2.Client.Services.Exports;

public sealed class ReportExportUrlService : IReportExportUrlService
{
    private readonly NavigationManager _navigationManager;
    private readonly string? _apiBaseUrl;

    public ReportExportUrlService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
    {
        _navigationManager = navigationManager;
        _apiBaseUrl = httpClient.BaseAddress?.ToString() ?? configuration["Api:BaseUrl"];
    }

    public string BuildExportUrl(string basePath, IReadOnlyDictionary<string, string?>? queryParameters = null)
    {
        var absoluteBase = ToAbsoluteUrl(basePath);

        if (queryParameters is null || queryParameters.Count == 0)
            return absoluteBase;

        var builder = new StringBuilder(absoluteBase);
        builder.Append(absoluteBase.Contains('?') ? '&' : '?');

        var isFirst = true;
        foreach (var (key, value) in queryParameters)
        {
            if (!isFirst) builder.Append('&');

            builder.Append(Uri.EscapeDataString(key));
            builder.Append('=');
            builder.Append(Uri.EscapeDataString(value ?? string.Empty));

            isFirst = false;
        }

        return builder.ToString();
    }

    public void OpenExportUrl(string basePath, IReadOnlyDictionary<string, string?>? queryParameters = null)
    {
        var url = BuildExportUrl(basePath, queryParameters);
        _navigationManager.NavigateTo(url, forceLoad: true);
    }

    private string ToAbsoluteUrl(string pathOrUrl)
    {
        // jeśli już absolutny - zostaw
        if (Uri.TryCreate(pathOrUrl, UriKind.Absolute, out var abs))
            return abs.ToString();

        // jeśli masz skonfigurowany adres API - użyj jego jako bazy
        if (!string.IsNullOrWhiteSpace(_apiBaseUrl) &&
            Uri.TryCreate(_apiBaseUrl, UriKind.Absolute, out var apiBase))
        {
            // ważne: wymuś, żeby basePath było traktowane jako ścieżka od root
            var normalized = pathOrUrl.StartsWith('/') ? pathOrUrl : "/" + pathOrUrl;
            return new Uri(apiBase, normalized).ToString();
        }

        // fallback: bieżąca baza klienta
        var clientBase = new Uri(_navigationManager.BaseUri, UriKind.Absolute);
        return new Uri(clientBase, pathOrUrl).ToString();
    }
}
