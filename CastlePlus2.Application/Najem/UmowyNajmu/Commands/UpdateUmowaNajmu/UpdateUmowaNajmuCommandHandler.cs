using CastlePlus2.Application.Interfaces.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Commands.UpdateUmowaNajmu
{
    public sealed class UpdateUmowaNajmuCommandHandler : IRequestHandler<UpdateUmowaNajmuCommand, bool>
    {
        private readonly IUmowaNajmuRepository _repo;

        public UpdateUmowaNajmuCommandHandler(IUmowaNajmuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateUmowaNajmuCommand cmd, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(cmd.Id, ct);
            if (entity is null) return false;

            entity.IdWynajmujacego = cmd.IdWynajmujacego;
            entity.IdNajemcy = cmd.IdNajemcy;
            entity.DataZawarcia = cmd.DataZawarcia.Date;
            entity.DataPoczatku = cmd.DataPoczatku.Date;
            entity.DataZakonczenia = cmd.DataZakonczenia?.Date;
            entity.KodWaluty = cmd.KodWaluty;
            entity.KodIndeksacji = cmd.KodIndeksacji;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}