using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Platnosci.Commands.CreatePlatnosc
{
    public class CreatePlatnoscCommandHandler : IRequestHandler<CreatePlatnoscCommand, PlatnoscDto>
    {
        private readonly IPlatnoscRepository _repo;
        private readonly IMapper _mapper;

        public CreatePlatnoscCommandHandler(IPlatnoscRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PlatnoscDto> Handle(CreatePlatnoscCommand request, CancellationToken ct)
        {
            if (request.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być > 0.");

            if (request.KodWaluty is null || request.KodWaluty.Trim().Length != 3)
                throw new InvalidOperationException("KodWaluty musi mieć dokładnie 3 znaki (char(3)).");

            if (request.Kwota <= 0)
                throw new InvalidOperationException("Kwota musi być > 0.");

            var entity = new Platnosc
            {
                IdPodmiotu = request.IdPodmiotu,
                DataPlatnosci = request.DataPlatnosci.Date,
                KodWaluty = request.KodWaluty.Trim().ToUpperInvariant(),
                Kwota = request.Kwota
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<PlatnoscDto>(entity);
        }
    }
}
