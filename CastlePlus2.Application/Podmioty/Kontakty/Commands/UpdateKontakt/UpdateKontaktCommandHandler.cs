using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Commands.UpdateKontakt
{
    public class UpdateKontaktCommandHandler : IRequestHandler<UpdateKontaktCommand, bool>
    {
        private readonly IKontaktRepository _repo;

        public UpdateKontaktCommandHandler(IKontaktRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(UpdateKontaktCommand command, CancellationToken ct)
        {
            if (command.IdKontaktu <= 0)
                throw new InvalidOperationException("IdKontaktu musi być > 0.");

            command.Rodzaj = (command.Rodzaj ?? string.Empty).Trim();
            command.Wartosc = (command.Wartosc ?? string.Empty).Trim();

            if (command.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być > 0.");

            if (command.Rodzaj.Length == 0)
                throw new InvalidOperationException("Rodzaj jest wymagany.");

            if (command.Rodzaj.Length > 30)
                throw new InvalidOperationException("Rodzaj max 30 znaków.");

            if (command.Wartosc.Length == 0)
                throw new InvalidOperationException("Wartosc jest wymagana.");

            if (command.Wartosc.Length > 200)
                throw new InvalidOperationException("Wartosc max 200 znaków.");

            if (!await _repo.PodmiotExistsAsync(command.IdPodmiotu, ct))
                throw new InvalidOperationException("Nie istnieje Podmiot o podanym IdPodmiotu.");

            var entity = await _repo.GetByIdForUpdateAsync(command.IdKontaktu, ct);
            if (entity is null)
                return false;

            entity.IdPodmiotu = command.IdPodmiotu;
            entity.Rodzaj = command.Rodzaj;
            entity.Wartosc = command.Wartosc;

            await _repo.SaveChangesAsync(ct);

            return true;
        }
    }
}