using CastlePlus2.Application.Interfaces.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Commands.UpdatePrzedmiotNajmu
{
    public class UpdatePrzedmiotNajmuCommandHandler : IRequestHandler<UpdatePrzedmiotNajmuCommand, bool>
    {
        private readonly IPrzedmiotNajmuRepository _repo;

        public UpdatePrzedmiotNajmuCommandHandler(IPrzedmiotNajmuRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdatePrzedmiotNajmuCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdPrzedmiotuNajmu, ct);
            if (entity == null)
            {
                return false;
            }

            entity.IdUmowyNajmu = request.IdUmowyNajmu;
            entity.IdEncji = request.IdEncji;
            entity.UdzialProcent = request.UdzialProcent;
            entity.OdDnia = request.OdDnia;
            entity.DoDnia = request.DoDnia;

            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}