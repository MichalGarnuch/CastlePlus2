using MediatR;
using CastlePlus2.Application.Interfaces.Slowniki;

namespace CastlePlus2.Application.Slowniki.Waluty.Commands.DeleteWaluta
{
    public class DeleteWalutaCommandHandler : IRequestHandler<DeleteWalutaCommand>
    {
        private readonly IWalutaRepository _repo;

        public DeleteWalutaCommandHandler(IWalutaRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteWalutaCommand request, CancellationToken ct)
        {
            var kod = (request.KodWaluty ?? string.Empty).Trim().ToUpperInvariant();

            var entity = await _repo.GetByKodAsync(kod, ct);
            if (entity is null)
                return;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync(ct);
        }
    }
}
