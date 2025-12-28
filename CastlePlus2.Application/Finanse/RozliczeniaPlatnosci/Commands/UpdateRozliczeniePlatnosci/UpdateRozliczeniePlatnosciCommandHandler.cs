using CastlePlus2.Application.Interfaces.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.UpdateRozliczeniePlatnosci
{
    public class UpdateRozliczeniePlatnosciCommandHandler
        : IRequestHandler<UpdateRozliczeniePlatnosciCommand, bool>
    {
        private readonly IRozliczeniePlatnosciRepository _repo;

        public UpdateRozliczeniePlatnosciCommandHandler(IRozliczeniePlatnosciRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateRozliczeniePlatnosciCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdRozliczenia, ct);
            if (entity is null)
                return false;

            entity.IdPlatnosci = request.IdPlatnosci;
            entity.IdFaktury = request.IdFaktury;
            entity.Kwota = request.Kwota;

            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}