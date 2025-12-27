using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.UpdatePomieszczenie
{
    public class UpdatePomieszczenieCommandHandler
        : IRequestHandler<UpdatePomieszczenieCommand, bool>
    {
        private readonly IPomieszczenieRepository _repository;

        public UpdatePomieszczenieCommandHandler(IPomieszczenieRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdatePomieszczenieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetForUpdateAsync(request.Id, cancellationToken);
            if (entity is null)
                return false;

            entity.IdEncjiNadrzednej = request.IdEncjiNadrzednej;
            entity.KodPomieszczenia = request.KodPomieszczenia;
            entity.Powierzchnia = request.Powierzchnia;

            await _repository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}