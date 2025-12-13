using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.RozliczeniaPlatnosci.Commands.CreateRozliczeniePlatnosci
{
    public class CreateRozliczeniePlatnosciCommandHandler
        : IRequestHandler<CreateRozliczeniePlatnosciCommand, RozliczeniePlatnosciDto>
    {
        private readonly IRozliczeniePlatnosciRepository _repo;
        private readonly IMapper _mapper;

        public CreateRozliczeniePlatnosciCommandHandler(IRozliczeniePlatnosciRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RozliczeniePlatnosciDto> Handle(CreateRozliczeniePlatnosciCommand request, CancellationToken ct)
        {
            if (request.IdPlatnosci <= 0)
                throw new InvalidOperationException("IdPlatnosci musi być > 0.");

            if (request.IdFaktury <= 0)
                throw new InvalidOperationException("IdFaktury musi być > 0.");

            if (request.Kwota <= 0)
                throw new InvalidOperationException("Kwota musi być > 0.");

            var entity = new RozliczeniePlatnosci
            {
                IdPlatnosci = request.IdPlatnosci,
                IdFaktury = request.IdFaktury,
                Kwota = request.Kwota
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<RozliczeniePlatnosciDto>(entity);
        }
    }
}
