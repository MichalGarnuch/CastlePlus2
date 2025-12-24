using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Najem;

namespace CastlePlus2.Client.Services.Najem
{
    public interface IUmowyNajmuService
    {
        Task<List<UmowaNajmuDto>> GetAllAsync(CancellationToken ct = default);
        Task<UmowaNajmuDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}