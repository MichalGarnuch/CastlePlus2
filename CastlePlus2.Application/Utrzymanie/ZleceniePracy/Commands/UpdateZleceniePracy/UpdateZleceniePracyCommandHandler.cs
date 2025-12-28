using CastlePlus2.Application.Interfaces.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.UpdateZleceniePracy
{
    public class UpdateZleceniePracyCommandHandler : IRequestHandler<UpdateZleceniePracyCommand, bool>
    {
        private readonly IZleceniePracyRepository _repository;

        public UpdateZleceniePracyCommandHandler(IZleceniePracyRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateZleceniePracyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetForUpdateAsync(request.IdZlecenia, cancellationToken);
            if (entity is null)
                return false;

            entity.IdEncjiGospodarza = request.IdEncjiGospodarza;
            entity.Tytul = request.Tytul.Trim();
            entity.Opis = string.IsNullOrWhiteSpace(request.Opis) ? null : request.Opis.Trim();
            entity.Status = request.Status.Trim();
            entity.DataZamkniecia = request.DataZamkniecia;

            await _repository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}