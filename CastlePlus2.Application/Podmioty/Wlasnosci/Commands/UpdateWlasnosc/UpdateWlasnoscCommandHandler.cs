using System;
using CastlePlus2.Application.Interfaces.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Wlasnosci.Commands.UpdateWlasnosc
{
    public class UpdateWlasnoscCommandHandler : IRequestHandler<UpdateWlasnoscCommand, bool>
    {
        private readonly IWlasnoscRepository _repo;

        public UpdateWlasnoscCommandHandler(IWlasnoscRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateWlasnoscCommand command, CancellationToken ct)
        {
            var entity = await _repo.GetForUpdateAsync(command.IdWlasnosci, ct);
            if (entity is null)
                return false;

            if (command.IdEncji == Guid.Empty)
                throw new InvalidOperationException("IdEncji jest wymagane.");

            if (command.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być > 0.");

            if (command.UdzialProcent <= 0 || command.UdzialProcent > 100)
                throw new InvalidOperationException("UdzialProcent musi być w zakresie (0, 100].");

            if (command.DoDnia.HasValue && command.DoDnia.Value < command.OdDnia)
                throw new InvalidOperationException("DoDnia nie może być wcześniejsze niż OdDnia.");

            if (!await _repo.EncjaExistsAsync(command.IdEncji, ct))
                throw new InvalidOperationException("Nie istnieje Encja o podanym IdEncji.");

            if (!await _repo.PodmiotExistsAsync(command.IdPodmiotu, ct))
                throw new InvalidOperationException("Nie istnieje Podmiot o podanym IdPodmiotu.");

            entity.IdEncji = command.IdEncji;
            entity.IdPodmiotu = command.IdPodmiotu;
            entity.UdzialProcent = command.UdzialProcent;
            entity.OdDnia = command.OdDnia;
            entity.DoDnia = command.DoDnia;

            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}