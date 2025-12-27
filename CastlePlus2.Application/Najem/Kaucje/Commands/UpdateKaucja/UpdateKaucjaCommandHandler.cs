using CastlePlus2.Application.Interfaces.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Commands.UpdateKaucja
{
    public class UpdateKaucjaCommandHandler : IRequestHandler<UpdateKaucjaCommand, bool>
    {
        private readonly IKaucjaRepository _repo;

        public UpdateKaucjaCommandHandler(IKaucjaRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateKaucjaCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdOperacjiKaucji, ct);
            if (entity is null)
                return false;

            entity.IdUmowyNajmu = request.IdUmowyNajmu;
            entity.RodzajOperacji = request.RodzajOperacji.Trim();
            entity.Kwota = request.Kwota;
            entity.KodWaluty = request.KodWaluty.Trim().ToUpperInvariant();
            entity.DataOperacji = request.DataOperacji;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}