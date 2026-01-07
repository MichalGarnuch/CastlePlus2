using System.Collections.Generic;

namespace CastlePlus2.Client.Services.Exports;

public interface IReportExportUrlService
{
    string BuildExportUrl(string basePath, IReadOnlyDictionary<string, string?>? queryParameters = null);
    void OpenExportUrl(string basePath, IReadOnlyDictionary<string, string?>? queryParameters = null);
}