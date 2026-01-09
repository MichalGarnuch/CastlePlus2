using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.Reports;

namespace CastlePlus2.Client.Services.Reports;

public interface IReportDataPreviewService
{
    Task<ReportDataPreviewResponse> GetPreviewAsync(string reportKey, int take, CancellationToken ct = default);
}