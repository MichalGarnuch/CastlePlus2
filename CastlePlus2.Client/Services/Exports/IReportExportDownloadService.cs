using CastlePlus2.Contracts.Exports;
using System.Threading.Tasks;

namespace CastlePlus2.Client.Services.Exports;

public interface IReportExportDownloadService
{
    Task<ReportExportLinkResponse> CreateLinkAsync(
        string reportKey,
        ExportFormat format,
        bool archive,
        int take);

    string ToAbsolute(string relative);
}
