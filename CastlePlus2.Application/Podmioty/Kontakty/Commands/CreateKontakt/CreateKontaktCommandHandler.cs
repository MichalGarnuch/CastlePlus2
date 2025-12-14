using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Podmioty;
using CastlePlus2.Contracts.DTOs.Podmioty;
using CastlePlus2.Domain.Entities.Podmioty;
using MediatR;

namespace CastlePlus2.Application.Podmioty.Kontakty.Commands.CreateKontakt
{
    public class CreateKontaktCommandHandler : IRequestHandler<CreateKontaktCommand, KontaktDto>
    {
        private readonly IKontaktRepository _repo;
        private readonly IMapper _mapper;

        public CreateKontaktCommandHandler(IKontaktRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<KontaktDto> Handle(CreateKontaktCommand request, CancellationToken ct)
        {
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

            // FK: Podmiot musi istnieć
            if (!await _repo.PodmiotExistsAsync(request.IdPodmiotu, ct))
                throw new InvalidOperationException("Nie istnieje Podmiot o podanym IdPodmiotu.");

            var entity = new Kontakt
            {
                IdPodmiotu = request.IdPodmiotu,
                Rodzaj = request.Rodzaj,
                Wartosc = request.Wartosc
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<KontaktDto>(entity);
        }
    }
}
