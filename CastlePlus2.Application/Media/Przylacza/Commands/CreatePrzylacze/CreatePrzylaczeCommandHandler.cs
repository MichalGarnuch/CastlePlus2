using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Domain.Entities.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Commands.CreatePrzylacze
{
    public class CreatePrzylaczeCommandHandler : IRequestHandler<CreatePrzylaczeCommand, PrzylaczeDto>
    {
        private readonly IPrzylaczeRepository _przylaczeRepo;
        private readonly IRodzajMediumRepository _rodzajRepo;
        private readonly IMapper _mapper;

        public CreatePrzylaczeCommandHandler(
            IPrzylaczeRepository przylaczeRepo,
            IRodzajMediumRepository rodzajRepo,
            IMapper mapper)
        {
            _przylaczeRepo = przylaczeRepo;
            _rodzajRepo = rodzajRepo;
            _mapper = mapper;
        }

        public async Task<PrzylaczeDto> Handle(CreatePrzylaczeCommand request, CancellationToken ct)
        {
            // Minimalne walidacje w handlerze: kontroler ma być “głupi”.
            if (request.IdEncjiGospodarza == Guid.Empty)
                throw new InvalidOperationException("IdEncjiGospodarza jest wymagane.");

            request.KodRodzaju = (request.KodRodzaju ?? string.Empty).Trim();
            request.Opis = request.Opis?.Trim();

            if (request.KodRodzaju.Length == 0)
                throw new InvalidOperationException("KodRodzaju jest wymagany.");

            if (request.KodRodzaju.Length > 20)
                throw new InvalidOperationException("KodRodzaju max 20 znaków.");

            if (request.Opis is not null && request.Opis.Length > 200)
                throw new InvalidOperationException("Opis max 200 znaków.");

            // Walidacja FK -> Encja
            var encjaExists = await _przylaczeRepo.EncjaExistsAsync(request.IdEncjiGospodarza, ct);
            if (!encjaExists)
                throw new InvalidOperationException("Nie istnieje Encja o podanym IdEncjiGospodarza.");

            // Walidacja FK -> RodzajMedium
            var rodzaj = await _rodzajRepo.GetByIdAsync(request.KodRodzaju, ct);
            if (rodzaj is null)
                throw new InvalidOperationException("Nie istnieje RodzajMedium o podanym KodRodzaju.");

            var entity = new Przylacze
            {
                IdEncjiGospodarza = request.IdEncjiGospodarza,
                KodRodzaju = request.KodRodzaju,
                Opis = request.Opis
            };

            await _przylaczeRepo.AddAsync(entity, ct);
            await _przylaczeRepo.SaveChangesAsync(ct);

            var dto = _mapper.Map<PrzylaczeDto>(entity);
            dto.NazwaRodzaju = rodzaj.Nazwa; // wygodne pole na odczyt

            return dto;
        }
    }
}
