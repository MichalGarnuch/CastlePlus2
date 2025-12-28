using CastlePlus2.Application.Interfaces.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Commands.UpdateSkladnikCzynszu
{
    public sealed class UpdateSkladnikCzynszuCommandHandler : IRequestHandler<UpdateSkladnikCzynszuCommand, bool>
    {
        private readonly ISkladnikCzynszuRepository _repo;

        public UpdateSkladnikCzynszuCommandHandler(ISkladnikCzynszuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateSkladnikCzynszuCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(cmd.IdSkladnikaCzynszu, ct);
            if (entity is null)
            {
                return false;
            }

            entity.IdUmowyNajmu = cmd.IdUmowyNajmu;
            entity.Nazwa = cmd.Nazwa;
            entity.KodJednostki = cmd.KodJednostki;
            entity.Stawka = cmd.Stawka;
            entity.IloscBazowa = cmd.IloscBazowa;
            entity.KodIndeksacji = cmd.KodIndeksacji;
            entity.OdDnia = cmd.OdDnia;
            entity.DoDnia = cmd.DoDnia;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}