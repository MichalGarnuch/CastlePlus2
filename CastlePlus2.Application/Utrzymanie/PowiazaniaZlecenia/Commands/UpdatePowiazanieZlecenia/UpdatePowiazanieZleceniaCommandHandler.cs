using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.UpdatePowiazanieZlecenia
{
    public sealed class UpdatePowiazanieZleceniaCommandHandler
        : IRequestHandler<UpdatePowiazanieZleceniaCommand, PowiazanieZleceniaDto?>
    {
        private readonly IPowiazanieZleceniaRepository _repo;
        private readonly IZleceniePracyRepository _zlecenieRepo;
        private readonly IMapper _mapper;

        public UpdatePowiazanieZleceniaCommandHandler(
            IPowiazanieZleceniaRepository repo,
            IZleceniePracyRepository zlecenieRepo,
            IMapper mapper)
        {
            _repo = repo;
            _zlecenieRepo = zlecenieRepo;
            _mapper = mapper;
        }

        public async Task<PowiazanieZleceniaDto?> Handle(UpdatePowiazanieZleceniaCommand command, CancellationToken ct)
        {
            var entity = await _repo.GetByIdAsync(command.IdPowiazania, ct);
            if (entity is null)
                return null;

            var request = command.Request;

            if (request.IdZlecenia <= 0)
                throw new InvalidOperationException("IdZlecenia musi być > 0.");

            if (request.IdEncji == Guid.Empty)
                throw new InvalidOperationException("IdEncji nie może być pustym GUID.");

            var zlecenie = await _zlecenieRepo.GetByIdAsync(request.IdZlecenia, ct);
            if (zlecenie is null)
                throw new InvalidOperationException($"ZleceniePracy o IdZlecenia={request.IdZlecenia} nie istnieje.");

            if (entity.IdZlecenia != request.IdZlecenia || entity.IdEncji != request.IdEncji)
            {
                var exists = await _repo.ExistsAsync(request.IdZlecenia, request.IdEncji, ct);
                if (exists)
                    throw new InvalidOperationException("Takie powiązanie już istnieje (IdZlecenia + IdEncji).");
            }

            entity.IdZlecenia = request.IdZlecenia;
            entity.IdEncji = request.IdEncji;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<PowiazanieZleceniaDto>(entity);
        }
    }
}