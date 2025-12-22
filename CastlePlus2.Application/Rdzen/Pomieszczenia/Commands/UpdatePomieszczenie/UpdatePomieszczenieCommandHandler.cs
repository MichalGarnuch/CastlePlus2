using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.UpdatePomieszczenie
{
    public class UpdatePomieszczenieCommandHandler
        : IRequestHandler<UpdatePomieszczenieCommand, PomieszczenieDto?>
    {
        private readonly IPomieszczenieRepository _repository;
        private readonly IMapper _mapper;

        public UpdatePomieszczenieCommandHandler(IPomieszczenieRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PomieszczenieDto?> Handle(UpdatePomieszczenieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetForUpdateAsync(request.Id, cancellationToken);
            if (entity is null)
                return null;

            entity.IdEncjiNadrzednej = request.IdEncjiNadrzednej;
            entity.KodPomieszczenia = request.KodPomieszczenia;
            entity.Powierzchnia = request.Powierzchnia;

            await _repository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PomieszczenieDto>(entity);
        }
    }
}
