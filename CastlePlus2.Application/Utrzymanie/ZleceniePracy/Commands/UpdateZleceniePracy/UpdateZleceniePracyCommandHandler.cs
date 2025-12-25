using System;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Utrzymanie;
using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.UpdateZleceniePracy
{
    public class UpdateZleceniePracyCommandHandler : IRequestHandler<UpdateZleceniePracyCommand, ZleceniePracyDto?>
    {
        private readonly IZleceniePracyRepository _repository;
        private readonly IMapper _mapper;

        public UpdateZleceniePracyCommandHandler(IZleceniePracyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ZleceniePracyDto?> Handle(UpdateZleceniePracyCommand request, CancellationToken cancellationToken)
        {
            if (request.IdZlecenia <= 0)
                throw new InvalidOperationException("Id zlecenia musi być > 0.");

            if (request.IdEncjiGospodarza == Guid.Empty)
                throw new InvalidOperationException("Id encji gospodarza jest wymagane.");

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

            if (request.DataZamkniecia is not null && request.DataZamkniecia.Value.Year < 1900)
                throw new InvalidOperationException("Data zamknięcia jest nieprawidłowa.");

            var entity = await _repository.GetForUpdateAsync(request.IdZlecenia, cancellationToken);
            if (entity is null)
                return null;

            entity.IdEncjiGospodarza = request.IdEncjiGospodarza;
            entity.Tytul = request.Tytul.Trim();
            entity.Opis = string.IsNullOrWhiteSpace(request.Opis) ? null : request.Opis.Trim();
            entity.Status = request.Status.Trim();
            entity.DataZamkniecia = request.DataZamkniecia;

            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ZleceniePracyDto>(entity);
        }
    }
}