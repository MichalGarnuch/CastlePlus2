using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Commands.UpdateKontakt
{
    public class UpdateKontaktCommandHandler : IRequestHandler<UpdateKontaktCommand, KontaktDto?>
    {
        private readonly IKontaktRepository _repo;
        private readonly IMapper _mapper;

        public UpdateKontaktCommandHandler(IKontaktRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<KontaktDto?> Handle(UpdateKontaktCommand command, CancellationToken ct)
        {
            if (command.IdKontaktu <= 0)
                throw new InvalidOperationException("IdKontaktu musi być > 0.");

            var request = command.Request;
            request.Rodzaj = (request.Rodzaj ?? string.Empty).Trim();
            request.Wartosc = (request.Wartosc ?? string.Empty).Trim();

            if (request.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być > 0.");

            if (request.Rodzaj.Length == 0)
                throw new InvalidOperationException("Rodzaj jest wymagany.");

            if (request.Rodzaj.Length > 30)
                throw new InvalidOperationException("Rodzaj max 30 znaków.");

            if (request.Wartosc.Length == 0)
                throw new InvalidOperationException("Wartosc jest wymagana.");

            if (request.Wartosc.Length > 200)
                throw new InvalidOperationException("Wartosc max 200 znaków.");

            if (!await _repo.PodmiotExistsAsync(request.IdPodmiotu, ct))
                throw new InvalidOperationException("Nie istnieje Podmiot o podanym IdPodmiotu.");

            var entity = await _repo.GetByIdForUpdateAsync(command.IdKontaktu, ct);
            if (entity is null)
                return null;

            entity.IdPodmiotu = request.IdPodmiotu;
            entity.Rodzaj = request.Rodzaj;
            entity.Wartosc = request.Wartosc;

            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<KontaktDto>(entity);
        }
    }
}