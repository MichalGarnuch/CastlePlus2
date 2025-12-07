using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Queries.GetBudynekById
{
    /// <summary>
    /// Handler obsługujący zapytanie GetBudynekByIdQuery.
    /// </summary>
    public class GetBudynekByIdQueryHandler : IRequestHandler<GetBudynekByIdQuery, BudynekDto?>
    {
        private readonly IBudynekRepository _repository;
        private readonly IMapper _mapper;

        public GetBudynekByIdQueryHandler(IBudynekRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BudynekDto?> Handle(GetBudynekByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<BudynekDto>(entity);
        }
    }
}
