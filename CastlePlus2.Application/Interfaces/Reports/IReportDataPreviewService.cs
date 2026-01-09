using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.Reports;

namespace CastlePlus2.Application.Interfaces.Reports;

public interface IReportDataPreviewService
{
    Task<ReportDataPreviewResponse> BuildAsync(string reportKey, int take, CancellationToken ct);
}