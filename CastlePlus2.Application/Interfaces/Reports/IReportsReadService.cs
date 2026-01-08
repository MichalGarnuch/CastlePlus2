using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.Reports;

namespace CastlePlus2.Application.Interfaces.Reports;

public interface IReportsReadService
{
    Task<PodsumowanieOperacyjneRow> GetPodsumowanieAsync(CancellationToken ct);
    Task<IReadOnlyList<FakturyStatRow>> GetFakturyAsync(int take, CancellationToken ct);
}