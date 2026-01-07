using System.Text;
using Microsoft.AspNetCore.Components;

namespace CastlePlus2.Client.Services.Exports;

public sealed class ReportExportUrlService : IReportExportUrlService
{
    private readonly NavigationManager _navigationManager;

    public ReportExportUrlService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public string BuildExportUrl(string basePath, IReadOnlyDictionary<string, string?>? queryParameters = null)
    {
        if (queryParameters is null || queryParameters.Count == 0)
        {
            return basePath;
        }

        var builder = new StringBuilder(basePath);
        builder.Append(basePath.Contains('?') ? '&' : '?');

        var isFirst = true;
        foreach (var (key, value) in queryParameters)
        {
            if (!isFirst)
            {
                builder.Append('&');
            }

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
}