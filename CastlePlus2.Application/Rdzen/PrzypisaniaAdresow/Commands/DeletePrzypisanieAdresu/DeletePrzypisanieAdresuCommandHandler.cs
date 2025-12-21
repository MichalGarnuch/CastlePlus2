using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.DeletePrzypisanieAdresu
{
    public sealed class DeletePrzypisanieAdresuCommandHandler
        : IRequestHandler<DeletePrzypisanieAdresuCommand, bool>
    {
        private readonly IPrzypisanieAdresuRepository _repo;

        public DeletePrzypisanieAdresuCommandHandler(IPrzypisanieAdresuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeletePrzypisanieAdresuCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.IdPrzypisaniaAdresu, ct);
            if (entity is null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}
