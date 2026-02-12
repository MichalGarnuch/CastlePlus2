using System;
using CastlePlus2.Domain.Entities.Najem;

namespace CastlePlus2.Application.Interfaces.Najem
{
    public interface IPrzedmiotNajmuRepository
    {
        Task AddAsync(PrzedmiotNajmu entity, CancellationToken ct);
        Task<PrzedmiotNajmu?> GetByIdAsync(long id, CancellationToken ct);
        Task<List<PrzedmiotNajmu>> GetAllAsync(CancellationToken ct);
        Task<PrzedmiotNajmu?> GetForUpdateAsync(long id, CancellationToken ct);
        Task<bool> ExistsOverlapAsync(Guid idEncji, DateOnly odDnia, DateOnly? doDnia, CancellationToken ct);
        Task<List<PrzedmiotNajmu>> GetOpenForUpdateByUmowaIdAsync(Guid idUmowyNajmu, DateOnly dataZakonczenia, CancellationToken ct);
        void Remove(PrzedmiotNajmu entity);
        Task<int> SaveChangesAsync(CancellationToken ct);
        Task<(List<PrzedmiotNajmu> Items, int TotalCount)> SearchPagedAsync(
            string? q,
            Guid? idUmowyNajmu,
            int page,
            int pageSize,
            CancellationToken ct);
    }
}