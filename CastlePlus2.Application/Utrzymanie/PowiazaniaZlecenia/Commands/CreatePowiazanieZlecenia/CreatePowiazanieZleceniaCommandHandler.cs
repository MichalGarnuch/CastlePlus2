using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Domain.Entities.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.CreatePowiazanieZlecenia
{
    public class CreatePowiazanieZleceniaCommandHandler
        : IRequestHandler<CreatePowiazanieZleceniaCommand, PowiazanieZleceniaDto>
    {
        private readonly IPowiazanieZleceniaRepository _repo;
        private readonly IZleceniePracyRepository _zlecenieRepo;
        private readonly IMapper _mapper;

        public CreatePowiazanieZleceniaCommandHandler(
            IPowiazanieZleceniaRepository repo,
            IZleceniePracyRepository zlecenieRepo,
            IMapper mapper)
        {
            _repo = repo;
            _zlecenieRepo = zlecenieRepo;
            _mapper = mapper;
        }

        public async Task<PowiazanieZleceniaDto> Handle(CreatePowiazanieZleceniaCommand request, CancellationToken ct)
        {
            if (request.IdZlecenia <= 0)
                throw new InvalidOperationException("IdZlecenia musi być > 0.");

            if (request.IdEncji == Guid.Empty)
                throw new InvalidOperationException("IdEncji nie może być pustym GUID.");

            // Czytelny błąd zanim poleci FK.
            var zlecenie = await _zlecenieRepo.GetByIdAsync(request.IdZlecenia, ct);
            if (zlecenie is null)
                throw new InvalidOperationException($"ZleceniePracy o IdZlecenia={request.IdZlecenia} nie istnieje.");

            var exists = await _repo.ExistsAsync(request.IdZlecenia, request.IdEncji, ct);
            if (exists)
                throw new InvalidOperationException("Takie powiązanie już istnieje (IdZlecenia + IdEncji).");

            var entity = new PowiazanieZlecenia
            {
                IdZlecenia = request.IdZlecenia,
                IdEncji = request.IdEncji
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<PowiazanieZleceniaDto>(entity);
        }
    }
}
