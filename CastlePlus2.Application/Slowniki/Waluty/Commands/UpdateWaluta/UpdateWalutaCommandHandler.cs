using MediatR;
using CastlePlus2.Application.Interfaces.Slowniki;

namespace CastlePlus2.Application.Slowniki.Waluty.Commands.UpdateWaluta
{
    public class UpdateWalutaCommandHandler : IRequestHandler<UpdateWalutaCommand, bool>
    {
        private readonly IWalutaRepository _repo;

        public UpdateWalutaCommandHandler(IWalutaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateWalutaCommand request, CancellationToken ct)
        {
            var kod = (request.KodWaluty ?? string.Empty).Trim().ToUpperInvariant();
            var nazwa = (request.Nazwa ?? string.Empty).Trim();

            var entity = await _repo.GetByKodAsync(kod, ct);
            if (entity is null)
                return false;

            entity.Nazwa = nazwa;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}