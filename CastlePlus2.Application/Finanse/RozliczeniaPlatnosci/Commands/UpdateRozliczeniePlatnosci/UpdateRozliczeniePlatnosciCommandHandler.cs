using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.UpdateRozliczeniePlatnosci
{
    public class UpdateRozliczeniePlatnosciCommandHandler
        : IRequestHandler<UpdateRozliczeniePlatnosciCommand, RozliczeniePlatnosciDto?>
    {
        private readonly IRozliczeniePlatnosciRepository _repo;
        private readonly IMapper _mapper;

        public UpdateRozliczeniePlatnosciCommandHandler(IRozliczeniePlatnosciRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RozliczeniePlatnosciDto?> Handle(UpdateRozliczeniePlatnosciCommand request, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(request.IdRozliczenia, ct);
            if (entity is null)
                return null;

            entity.IdPlatnosci = request.IdPlatnosci;
            entity.IdFaktury = request.IdFaktury;
            entity.Kwota = request.Kwota;

            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<RozliczeniePlatnosciDto>(entity);
        }
    }
}
