using CastlePlus2.Application.Interfaces.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.DeleteZleceniePracy
{
    public class DeleteZleceniePracyCommandHandler : IRequestHandler<DeleteZleceniePracyCommand>
    {
        private readonly IZleceniePracyRepository _repository;

        public DeleteZleceniePracyCommandHandler(IZleceniePracyRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteZleceniePracyCommand request, CancellationToken cancellationToken)
        {
            if (request.IdZlecenia <= 0)
                return;

            var entity = await _repository.GetForUpdateAsync(request.IdZlecenia, cancellationToken);
            if (entity is null)
                return;

            await _repository.RemoveAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}