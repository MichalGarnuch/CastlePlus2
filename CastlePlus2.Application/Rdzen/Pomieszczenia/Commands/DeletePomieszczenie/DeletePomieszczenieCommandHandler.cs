using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.DeletePomieszczenie
{
    public class DeletePomieszczenieCommandHandler
        : IRequestHandler<DeletePomieszczenieCommand, bool>
    {
        private readonly IPomieszczenieRepository _repository;

        public DeletePomieszczenieCommandHandler(IPomieszczenieRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeletePomieszczenieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetForUpdateAsync(request.Id, cancellationToken);
            if (entity is null)
                return false;

            _repository.Remove(entity);
            await _repository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
