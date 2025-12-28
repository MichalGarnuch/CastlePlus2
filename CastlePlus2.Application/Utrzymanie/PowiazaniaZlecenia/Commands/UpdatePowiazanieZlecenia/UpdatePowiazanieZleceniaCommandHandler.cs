using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.PowiazaniaZlecenia.Commands.UpdatePowiazanieZlecenia
{
    public sealed class UpdatePowiazanieZleceniaCommandHandler
        : IRequestHandler<UpdatePowiazanieZleceniaCommand, bool>
    {
        private readonly IPowiazanieZleceniaRepository _repo;
        private readonly IZleceniePracyRepository _zlecenieRepo;

        public UpdatePowiazanieZleceniaCommandHandler(
            IPowiazanieZleceniaRepository repo,
            IZleceniePracyRepository zlecenieRepo)
        {
            _repo = repo;
            _zlecenieRepo = zlecenieRepo;
        }

        public async Task<bool> Handle(UpdatePowiazanieZleceniaCommand command, CancellationToken ct)
        {
            if (command.IdPowiazania <= 0)
                throw new InvalidOperationException("IdPowiazania musi być > 0.");

            if (command.IdZlecenia <= 0)
                throw new InvalidOperationException("IdZlecenia musi być > 0.");

            if (command.IdEncji == Guid.Empty)
                throw new InvalidOperationException("IdEncji nie może być pustym GUID.");

            var entity = await _repo.GetByIdAsync(command.IdPowiazania, ct);
            if (entity is null)
                return false;

            var zlecenie = await _zlecenieRepo.GetByIdAsync(command.IdZlecenia, ct);
            if (zlecenie is null)
                throw new InvalidOperationException($"ZleceniePracy o IdZlecenia={command.IdZlecenia} nie istnieje.");

            if (entity.IdZlecenia != command.IdZlecenia || entity.IdEncji != command.IdEncji)
            {
                var exists = await _repo.ExistsAsync(command.IdZlecenia, command.IdEncji, ct);
                if (exists)
                    throw new InvalidOperationException("Takie powiązanie już istnieje (IdZlecenia + IdEncji).");
            }

            entity.IdZlecenia = command.IdZlecenia;
            entity.IdEncji = command.IdEncji;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}