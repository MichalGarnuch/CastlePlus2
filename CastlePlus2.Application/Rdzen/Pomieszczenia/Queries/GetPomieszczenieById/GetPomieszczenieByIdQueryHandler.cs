using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Queries.GetPomieszczenieById
{
    public class GetPomieszczenieByIdQueryHandler
        : IRequestHandler<GetPomieszczenieByIdQuery, PomieszczenieDto?>
    {
        private readonly IPomieszczenieRepository _repository;
        private readonly IMapper _mapper;

        public GetPomieszczenieByIdQueryHandler(
            IPomieszczenieRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PomieszczenieDto?> Handle(
            GetPomieszczenieByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<PomieszczenieDto>(entity);
        }
    }
}
