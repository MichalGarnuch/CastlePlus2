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
                DataZamkniecia = request.DataZamkniecia
            };

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ZleceniePracyDto>(entity);
        }
    }
}