using CastlePlus2.Application.Interfaces.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Commands.DeleteZleceniePracy
{
    public class DeleteZleceniePracyCommandHandler : IRequestHandler<DeleteZleceniePracyCommand, bool>
    {
        private readonly IZleceniePracyRepository _repository;

        public DeleteZleceniePracyCommandHandler(IZleceniePracyRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteZleceniePracyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetForUpdateAsync(request.IdZlecenia, cancellationToken);
            if (entity is null)
                return false;

            await _repository.RemoveAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}