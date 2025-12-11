using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Queries.GetLokalById
{
    /// <summary>
    /// Handler obsługujący zapytanie GetLokalByIdQuery.
    /// </summary>
    public class GetLokalByIdQueryHandler
        : IRequestHandler<GetLokalByIdQuery, LokalDto?>
    {
        private readonly ILokalRepository _lokalRepository;
        private readonly IMapper _mapper;

        public GetLokalByIdQueryHandler(
            ILokalRepository lokalRepository,
            IMapper mapper)
        {
            _lokalRepository = lokalRepository;
            _mapper = mapper;
        }

        public async Task<LokalDto?> Handle(
            GetLokalByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _lokalRepository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
            {
                return null;
            }

            return _mapper.Map<LokalDto>(entity);
        }
    }
}
