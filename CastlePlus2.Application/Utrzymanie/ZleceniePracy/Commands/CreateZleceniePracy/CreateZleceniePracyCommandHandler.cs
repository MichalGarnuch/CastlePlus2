using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using CastlePlus2.Domain.Entities.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.CreateZleceniePracy
{
    public class CreateZleceniePracyCommandHandler
        : IRequestHandler<CreateZleceniePracyCommand, ZleceniePracyDto>
    {
        private readonly IZleceniePracyRepository _repository;
        private readonly IMapper _mapper;

        public CreateZleceniePracyCommandHandler(IZleceniePracyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ZleceniePracyDto> Handle(CreateZleceniePracyCommand request, CancellationToken cancellationToken)
        {
            // Minimalna walidacja „kodowa” (szczegółową walidację można później przenieść do FluentValidation).
            if (string.IsNullOrWhiteSpace(request.Tytul))
                throw new InvalidOperationException("Tytuł zlecenia jest wymagany.");

            if (request.Tytul.Length > 200)
                throw new InvalidOperationException("Tytuł zlecenia nie może przekraczać 200 znaków.");

            if (!string.IsNullOrWhiteSpace(request.Opis) && request.Opis.Length > 1000)
                throw new InvalidOperationException("Opis nie może przekraczać 1000 znaków.");

            if (string.IsNullOrWhiteSpace(request.Status))
                throw new InvalidOperationException("Status jest wymagany.");

            if (request.Status.Length > 20)
                throw new InvalidOperationException("Status nie może przekraczać 20 znaków.");

            // Encja prosta (nie TPT). PK to IDENTITY bigint – nie ustawiamy IdZlecenia.
            var entity = new ZleceniePracy
            {
                IdEncjiGospodarza = request.IdEncjiGospodarza,
                Tytul = request.Tytul.Trim(),
                Opis = string.IsNullOrWhiteSpace(request.Opis) ? null : request.Opis.Trim(),
                Status = request.Status.Trim(),

                // DataUtworzenia ma DEFAULT w SQL, ale ustawiamy też w kodzie,
                // żeby zachowanie było przewidywalne nawet przy zmianie bazy.
                DataUtworzenia = DateTime.UtcNow,
                DataZamkniecia = null
            };

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ZleceniePracyDto>(entity);
        }
    }
}
